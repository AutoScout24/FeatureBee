$(function () {
    /*
    wrapper to attach events to the condition template, as the template
    is rendered by handlebars after page initialization
    */

    $.widget("as24.conditionify", {
        options: {
            conditions: [],
            triggerAdd: 'data-add-condition-value',
            triggerDelete: 'data-delete',
            triggerNew: 'data-add-condition',
            
            add: function () { },
            delete: function () { },
            new : function () { }
        },
        
        baseTemplate : null,
        templates: [],
        
        render: function (name, type, data) {
            var self = this;
            
            $("body").on("click", '[' + self.options.triggerAdd + '="' + type + '"]', function () { self._renderItem(this, name, type, data); });

            $(data).each(function (index, value) {
                $("body").off("click", '[' + self.options.triggerDelete + '="{ "' + type + '" : "' + value + '" }"]');
                $("body").on("click", '[' + self.options.triggerDelete + '="{ "' + type + '" : "' + value + '" }"]', function () {
                    var data = JSON.parse($(this).attr(self.options.triggerDelete));
                    self.options.delete({ name: name, type: type, values: [ data[type] ] });
                    $(this).parent().remove();
                });
            });

            self.renderAddNewCondition(name);
        },
        
        renderAddNewCondition: function (name) {
            var self = this;
            $("body").off("click", '[' + self.options.triggerNew + '="' + name + '"]');
            $("body").on("click", '[' + self.options.triggerNew + '="' + name + '"]', function () {
                var btn = $(this);
                btn.find('[data-select="condition"]').remove();
                var item = $('<ul data-select="condition" class="nav nav-pills nav-stacked" />');
                item.hide();
                item.insertBefore(btn);
                $.each(self.options.conditions, function (index, value) {
                    var li = $('<li />');
                    var a = $('<a />');
                    a.text(value.type);
                    a.data(value.type);
                    item.append(li.append(a));
                    a.click(function () {
                        self.options.new({ name: name, type: value.type });
                        btn.parent().find('[data-select="condition"]').hide('fast');
                        btn.parent().find('[data-select="condition"]').remove();
                    });
                });
                btn.hide('fast');
                item.show('fast');
            });
        },

        _renderItem : function(ele, name, type, data) {
            var self = this;
            var template = self.templates[type];
            if (template) {
                var element = $(self.templates[type](data));
                element.attr('data-adding', 'true');
                $(ele).siblings('[data-adding]').remove();
                $(ele).after(element);
                $(element).find('[data-action="add"]').on('click', function() {
                    var datastate = {
                        name: name,
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
        },

        _create: function () {
            var self = this;
            self.baseTemplate = Handlebars.compile($(self.element).find('[data-container="condition-item"]').html().trim());
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