(function () {

    var scriptLoader = function (src, callback) {
        var scriptTag = document.createElement('script');
        scriptTag.setAttribute("type", "text/javascript");
        scriptTag.setAttribute("src", src);
        if (scriptTag.readyState) {
            scriptTag.onreadystatechange = function () { // For old versions of IE
                if (this.readyState == 'complete' || this.readyState == 'loaded') {
                    callback();
                }
            };
        } else { // Other browsers
            scriptTag.onload = callback;
        }
        // Try to find the head, otherwise default to the documentElement
        (document.getElementsByTagName("head")[0] || document.documentElement).appendChild(scriptTag);
    };

    // Localize required scripts
    var jQueryFeatureBee;
    var handlebarsFeatureBee;

    var loadRequiredScripts = function (callbackOnComplete) {
        var isJQueryLoaded = false, isJQueryUiLoaded = false, isHandlebarsLoaded = false;

        var allLoaded = function () {
            return isJQueryLoaded && isHandlebarsLoaded && isJQueryUiLoaded;
        };

        var callbackIfComplete = function () {
            if (allLoaded()) callbackOnComplete();
        };

        /******** Load jQuery if not present *********/
        var jQueryLoaded = function () {
            isJQueryLoaded = true;
            callbackIfComplete();

            if (jQueryFeatureBee.ui === undefined) {
                scriptLoader("//code.jquery.com/ui/1.10.3/jquery-ui.js", function () {
                    jQueryUiLoaded();
                });
            }
            else {
                jQueryUiLoaded();
            }
        };

        var jQueryUiLoaded = function () {
            isJQueryUiLoaded = true;
            callbackIfComplete();
        };

        if (window.jQuery === undefined || window.jQuery.fn.jquery !== '2.0.1') {
            var existedBefore = window.jQuery;
            scriptLoader("//code.jquery.com/jquery-2.0.3.min.js", function () {
                // Restore globally scoped jQuery variables to the first (original) version loaded
                if (existedBefore) {
                    jQueryFeatureBee = window.jQuery.noConflict(true);
                } else {
                    jQueryFeatureBee = window.jQuery;
                }
                jQueryLoaded();
            });
        } else {
            // The jQuery version on the window is the one we want to use
            jQueryFeatureBee = window.jQuery;
            jQueryLoaded();
        }

        /******* Load Handlebars if not present ****/
        var handelbarsLoaded = function () {
            isHandlebarsLoaded = true;
            callbackIfComplete();
        };
        if (window.Handlebars === undefined) {
            scriptLoader("//cdnjs.cloudflare.com/ajax/libs/handlebars.js/1.1.2/handlebars.min.js", handelbarsLoaded);
        } else {
            handlebarsFeatureBee = window.Handlebars;
            handelbarsLoaded();
        }

        return this;
    };

    /******** Our main function ********/
    var main = function () {
        jQueryFeatureBee(document).ready(function ($) {
            var featuresUrl = '/featurebee.axd/features';

            var loadFeatures = function () {
                return [
                    {
                        "Name": "a",
                        "Conditions": [{ "Type": "culture", "Values": ["de-DE", "de-AT"] }, { "Type": "browser", "Values": ["chrome", "firefox"] }],
                        "State": "In Development",
                        "GodModeState": "On"
                    }, {
                        "Name": "b",
                        "Conditions": [],
                        "State": "Released",
                        "GodModeState": "Off"
                    },
                    {
                        "Name": "lala",
                        "Conditions": [{ "Type": "culture", "Values": ["de-AT"] }, { "Type": "trafficDistribution", "Values": ["0%", "50%"] }],
                        "State": "In Development",
                        "GodModeState": "On"
                    }, {
                        "Name": "tata",
                        "Conditions": [],
                        "State": "Under Test",
                        "GodModeState": "Off"
                    }, {
                        "Name": "erw",
                        "Conditions": [],
                        "State": "In Development",
                        "GodModeState": "Off"
                    }];
            };

            $.widget("as24.featureBeeTrayIcon", {
                _create: function () {
                    var showButton = $(this.element).find('.feature-bee-show');
                    var content = $(this.element).find('.feature-bee-panel');
                    content.hide();
                    $(content).featureBeeBar({
                        close: function () { showButton.show('fast'); }
                    });
                    showButton.click(function () {
                        showButton.hide('fast');
                        $(content).featureBeeBar('open');
                    });
                }
            });

            $.widget("as24.featureBeeBar", {
                options: {
                    hide: function () { }
                },

                open: function () {
                    var self = this;
                    $(this.element).show('fast', function () {
                        setTimeout(function () { self._sizeScrollbar(); }, 10);
                    });
                },

                _sizeScrollbar: function () {
                    var remainder = this.scrollContent.width() - this.scrollPane.width();
                    var proportion = remainder / this.scrollContent.width();
                    var handleSize = this.scrollPane.width() - (proportion * this.scrollPane.width());
                    this.scrollbar.find(".ui-slider-handle").css({
                        width: handleSize,
                        "margin-left": -handleSize / 2
                    });
                    this.handleHelper.width("").width(this.scrollbar.width() - handleSize);
                },

                _resetValue: function () {
                    var remainder = this.scrollPane.width() - this.scrollContent.width();
                    var leftVal = this.scrollContent.css("margin-left") === "auto" ? 0 :
                        parseInt(this.scrollContent.css("margin-left"));
                    var percentage = Math.round(leftVal / remainder * 100);
                    this.scrollbar.slider("value", percentage);
                },

                _reflowContent: function () {
                    var showing = this.scrollContent.width() + parseInt(this.scrollContent.css("margin-left"), 10);
                    var gap = this.scrollPane.width() - showing;
                    if (gap > 0) {
                        this.scrollContent.css("margin-left", parseInt(this.scrollContent.css("margin-left"), 10) + gap);
                    }
                },

                _create: function () {
                    var self = this;
                    //scrollpane parts
                    self.scrollPane = $(".feature-bee-scroll-pane"),
                      self.scrollContent = $(".feature-bee-scroll-content");

                    // load and add items
                    var source = $(".feature-bee-scroll-content-items-template").html().trim();
                    var template = Handlebars.compile(source);

                    var features = template(loadFeatures());
                    self.scrollContent.append($(features));
                    var width = 400;
                    self.scrollContent.find('.feature-bee-scroll-content-item').each(function () {
                        width += $(this).outerWidth();
                    });

                    $('.feature-bee-scroll-content').width(width);

                    //build slider
                    self.scrollbar = $(".feature-bee-scroll-bar").slider({
                        slide: function (event, ui) {
                            if (self.scrollContent.width() > self.scrollPane.width()) {
                                self.scrollContent.css("margin-left", Math.round(
                                  ui.value / 100 * (self.scrollPane.width() - self.scrollContent.width())
                                ) + "px");
                            } else {
                                self.scrollContent.css("margin-left", 0);
                            }
                        }
                    });

                    //append icon to handle
                    self.handleHelper = self.scrollbar.find(".ui-slider-handle").mousedown(function () { self.scrollbar.width(self.handleHelper.width()); }).mouseup(function () { self.scrollbar.width("100%"); }).append("<span class='ui-icon ui-icon-grip-dotted-vertical'></span>").wrap("<div class='ui-handle-helper-parent'></div>").parent();

                    //change overflow to hidden now that slider handles the scrolling
                    self.scrollPane.css("overflow", "hidden");

                    //change handle position on window resize
                    $(window).resize(function () {
                        self._resetValue();
                        self._sizeScrollbar();
                        self.reflowContent();
                    });

                    $('.feature-bee-hide').click(function () {
                        $(self.element).hide('fast');
                        self.options.close();
                    });
                }
            });

            $('.feature-bee-tray-container').featureBeeTrayIcon();

        });
    };

    loadRequiredScripts(main);
})();