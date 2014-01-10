$(function () {
    /*
    wrapper for jquery dialog
    */
    
    $.widget("as24.formify", {
        options: {
            width: 500,
            
            // communicatorId
            publisher: 'page',
            streams: {
                conditionsChanged: 'conditionsChanged'
            },
            communicator: function (publisher, why) {
                return $.Comm ? $.Comm(publisher, why) : { subscribe: function () { }, publish: function () { }, unsubscribe : function () { } };
            },
            source : function () {},

            // callbacks
            save: function () { },
            cancel: function () { }
        },
        
        _subscribeForReload: function (forName) {
            var self = this;
            var action = function (item) { self.open(self.source(item)); };
            var streams = self.options.streams;
            self.options.communicator(self.options.publisher, streams.conditionsChanged + ':' + forName).subscribe(action);
        },
        
        _unSubscribeForReload: function (forName) {
            var self = this;
            var streams = self.options.streams;
            self.options.communicator(self.options.publisher, streams.conditionsChanged + ':' + forName).empty();
        },

        open: function (item) {
            var self = this;
            
            var html = self.compiledTemplate(item);
            if (self.dialog) {
                self.dialog.dialog('destroy');
            }
            if (item && item.name) {
                self._unSubscribeForReload(item.name);
                self._subscribeForReload(item.name);
            }
            self.dialog = $(html).dialog({
                modal: true,
                width: self.options.width,
                open: function (event) {
                    $('.ui-dialog-buttonpane').find('button:contains("Cancel")').removeClass().addClass('btn btn-default');
                    $('.ui-dialog-buttonpane').find('button:contains("Save")').removeClass().addClass('btn btn-primary');
                },
                buttons: [
                {
                    text: "Cancel",
                    click: function () {
                        self.options.cancel();
                        $(this).dialog("close");
                    }
                },
                {
                    text: "Save",
                    click: function () {
                        self.options.save($(this).find('form').serializeObject());
                        $(this).dialog("close");
                    }
                }
                ],
                close: function () {
                    $(this).dialog("destroy");
                    self.dialog = null;
                    if (item && item.name) {
                        self._unSubscribeForReload(item.name);
                    }
                }
            });
        },

        // the constructor
        _create: function () {
            var self = this;
            var source = $(this.element).html().trim();
            self.compiledTemplate = Handlebars.compile(source);
        },

        _destroy: function () {
        }
    });
});