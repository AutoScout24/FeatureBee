$(function() {
    $.widget("as24.conditionify", {
        options: {
            conditions: [],
            triggerAdd: 'data-add-condition',
            triggerDelete: 'data-delete',
            
            add: function () { },
            delete: function () { }
        },
        
        baseTemplate : null,
        templates: [],
        
        render: function (name, type, toElement, data) {
            var self = this;
            $("body").on("click", '[' + self.options.triggerAdd + '="' + type + '"]', function () {
                var template = self.templates[type];
                if (template) {
                    var element = $(self.templates[type](data));
                    element.attr('data-adding', 'true');
                    $(this).siblings('[data-adding]').remove();
                    $(this).after(element);
                    $(element).find('[data-action="add"]').on('click', function () {
                        var datastate = {
                            name : name,
                            type: type,
                            values: []
                        };
                        $(element).find('[data-value]').each(function(index, value) {
                            datastate.values.push($(this).val());
                        });
                        
                        self.options.add(datastate);
                        data.push(datastate.values[0]);
                        $(element).parent().html(self.baseTemplate({ name: name, type: type, values: data }));
                        $(element).remove();
                    });
                }
            });

            $(data).each(function (index, value) {
                $("body").off("click", '[' + self.options.triggerDelete + '="{ "' + type + '" : "' + value + '" }"]');
                $("body").on("click", '[' + self.options.triggerDelete + '="{ "' + type + '" : "' + value + '" }"]', function () {
                    var data = JSON.parse($(this).attr(self.options.triggerDelete));
                    self.options.delete({ name: name, type: type, values: [ data[type] ] });
                    $(this).parent().remove();
                });
            });
        },

        _create: function () {
            var self = this;
            self.baseTemplate = Handlebars.compile($(self.element).html().trim());
            for (var i = 0; i < this.options.conditions.length; i++) {
                var condition = this.options.conditions[i];
                var source = $(condition.template).html().trim();
                var template = Handlebars.compile(source);
                this.templates[condition.type] = template;
            }
        },
        
        _destroy: function () {
            $("body").off("click", '[' + self.options.triggerDelete + ']');
            $("body").off("click", '[' + self.options.triggerAdd + ']');
        }
    });
});