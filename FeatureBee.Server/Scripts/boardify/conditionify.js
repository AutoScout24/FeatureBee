$(function() {
    $.widget("as24.conditionify", {
        options: {
            conditions: [],
            trigger: 'data-add-condition',
            
            save : function () {}
        },
        
        templates: [],
        
        render: function (type, toElement, data) {
            var self = this;
            $("body").on("click", '[' + self.options.trigger + '="' + type + '"]', function () {
                var template = self.templates[type];
                if (template) {
                    var element = $(self.templates[type](data));
                    element.attr('data-adding', 'true');
                    $(this).siblings('[data-adding]').remove();
                    $(this).after(element);
                    $(element).find('[data-action="add"]').click(function() {
                        self.options.save();
                        $(element).remove();
                    });
                }
            });
            
        },

        _create: function () {
            for (var i = 0; i < this.options.conditions.length; i++) {
                var condition = this.options.conditions[i];
                var source = $(condition.template).html().trim();
                var template = Handlebars.compile(source);
                this.templates[condition.type] = template;
            }
        },
        
        _destroy: function() {
        }
    });
});