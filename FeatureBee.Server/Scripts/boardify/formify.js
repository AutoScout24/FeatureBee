$(function () {
    $.widget("as24.formify", {
        options: {
            width: 500,

            // callbacks
            save: function () { },
            cancel: function () { }
        },

        open: function (item) {
            var self = this;
            
            var html = self.compiledTemplate(item);
            if (self.dialog) {
                self.dialog.dialog('destroy');
            }
            self.dialog = $(html).dialog({
                modal: true,
                width: self.options.width,
                open: function(event) {
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