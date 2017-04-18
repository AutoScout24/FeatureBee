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
        var isJQueryLoaded = false, isJQueryUiLoaded = false, isHandlebarsLoaded = false, jQueryCookiesLoaded = false;

        var allLoaded = function () {
            return isJQueryLoaded && isHandlebarsLoaded && isJQueryUiLoaded && jQueryCookiesLoaded;
        };

        var callbackIfComplete = function () {
            if (allLoaded()) callbackOnComplete();
        };

        /******** Load jQuery if not present *********/
        var jQueryLoaded = function (callback) {
            isJQueryLoaded = true;

            loadJQueryUi(callback);
            callbackIfComplete();
        };

        var jQueryUiLoaded = function (callback) {
            isJQueryUiLoaded = true;
            if (callback) callback();
            callbackIfComplete();
        };

        var loadJQueryUi = function (callback) {
            if ($.ui === undefined) {
                scriptLoader("//code.jquery.com/ui/1.10.3/jquery-ui.js", function () {
                    jQueryUiLoaded(callback);
                });
            }
            else {
                jQueryUiLoaded(callback);
            }
        };

        if (window.jQuery === undefined || window.jQuery.fn.jquery !== '2.0.1') {
            var existedBefore = window.jQuery;
            var currentDollar = $;
            scriptLoader("//code.jquery.com/jquery-2.0.3.min.js", function () {

                // load addons
                scriptLoader("//cdnjs.cloudflare.com/ajax/libs/jquery-cookie/1.3.1/jquery.cookie.min.js", function () {
                    jQueryCookiesLoaded = true;
                    jQueryLoaded(function () {
                        // Restore globally scoped jQuery variables to the first (original) version loaded
                        if (existedBefore) {
                            jQueryFeatureBee = window.jQuery.noConflict(true);
                        } else {
                            jQueryFeatureBee = window.jQuery;
                        }

                        $ = currentDollar;
                    });
                });
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
            var featuresUrl = '{{FEATURES_URL}}';
            var cookieName = "featureBee";

            var loadFeatures = function (onComplete) {
                $.get(featuresUrl + '?t=' + new Date().getTime()).done(function (features) {
                    var featureBeeCookie = $.cookie(cookieName);
                    $.each(features, function (index, value) {
                        if (featureBeeCookie && featureBeeCookie.indexOf("#" + value.Name + "=true#") !== -1)
                            value.GodModeState = "On";
                        else {
                            value.GodModeState = "Off";
                        }
                    });
                    onComplete(features);
                });
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
                        loadFeatures(function(features) {
                            var featureItems = self.template(features);
                            self.scrollContent.append($(featureItems));
                            var width = 400;

                            self.scrollContent.find('.feature-bee-scroll-content-item').each(function() {
                                width += $(this).outerWidth();
                            });

                            $('.feature-bee-scroll-content').width(width);
                            
                            var toggleOn = "On", toggleOff = "Off";
                            self.scrollContent.find('[data-toggle-target]').click(function () {
                                var state = $(this).attr("data-toogle-state");
                                if (state == toggleOn) {
                                    self._removeByCookie($(this).attr("data-toggle-target"));
                                    $(this).attr("data-toogle-state", toggleOff);
                                    $(this).removeClass(toggleOn).addClass(toggleOff);
                                    $(this).text(toggleOff);
                                } else {
                                    self._addByCookie($(this).attr("data-toggle-target"));
                                    $(this).attr("data-toogle-state", toggleOn);
                                    $(this).removeClass(toggleOff).addClass(toggleOn);
                                    $(this).text(toggleOn);
                                }
                            });
                            setTimeout(function() { self._sizeScrollbar(); }, 10);
                        });
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

                _cleanCookie: function (name) {
                    var cookieValue = $.cookie(cookieName);
                    if (cookieValue == null) return null;
                    cookieValue = cookieValue.replace("#" + name + "=true#", "#");
                    cookieValue = cookieValue.replace("#" + name + "=false#", "#");
                    if (cookieValue.replace(/^#*$/, "") === "") {
                        cookieValue = null;
                    }
                    return cookieValue;
                },

                _writeCookie: function (name, enabled) {
                    var cookieValue = this._cleanCookie(name);
                    if (cookieValue == null)
                        cookieValue = "#";
                    cookieValue += name + "=" + enabled + "#";
                    $.cookie(cookieName, cookieValue, { path: '/' });
                },

                _removeByCookie: function (name) {
                    this._writeCookie(name, false);
                },

                _addByCookie: function (name) {
                    this._writeCookie(name, true);
                },

                _create: function() {
                    var self = this;
                    //scrollpane parts
                    self.scrollPane = $(".feature-bee-scroll-pane"),
                    self.scrollContent = $(".feature-bee-scroll-content");

                    // load and add items
                    var source = $(".feature-bee-scroll-content-items-template").html().trim();
                    self.template = Handlebars.compile(source);

                    //build slider
                    self.scrollbar = $(".feature-bee-scroll-bar").slider({
                        slide: function(event, ui) {
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
                    self.handleHelper = self.scrollbar.find(".ui-slider-handle").mousedown(function() { self.scrollbar.width(self.handleHelper.width()); }).mouseup(function() { self.scrollbar.width("100%"); }).append("<span class='ui-icon ui-icon-grip-dotted-vertical'></span>").wrap("<div class='ui-handle-helper-parent'></div>").parent();

                    //change overflow to hidden now that slider handles the scrolling
                    self.scrollPane.css("overflow", "hidden");

                    //change handle position on window resize
                    $(window).resize(function() {
                        self._resetValue();
                        self._sizeScrollbar();
                        self._reflowContent();
                    });

                    $('.feature-bee-hide').click(function() {
                        $(self.element).hide('fast');
                        self.options.close();
                    });

                }
            });

            $('.feature-bee-loading').hide();
            $('.feature-bee-show').show('medium');
            $('.feature-bee-tray-container').featureBeeTrayIcon();
        });
    };

    $('.feature-bee-show').hide();
    $('.feature-bee-panel').hide();
    $('.feature-bee-loading').show();
    setTimeout(function() { loadRequiredScripts(main); }, 3000);
})();
