$(function () {
    // Declare a proxy to reference the hub. 
    var boardHub = $.connection.boardHub;

    // Create a function that the hub can call to broadcast new items.
    boardHub.client.newItemAdded = function (item) {
        $.Comm('page', 'itemChanged').publish(item);
    };

    boardHub.client.itemEdited = function (item) {
        $.Comm('page', 'itemChanged').publish(item);
    };

    boardHub.client.itemMoved = function (item) {
        $.Comm('page', 'itemMoved').publish(item);
    };

    $.connection.hub.start().done(function () {
        var handlebar = new handleBar(new conditionTemplates());

        var form = new forms();

        menu.register(form);

        board.create(form);
    });

    var board = {
        create: function (form) {
            $('#board').boardify({
                states: "[data-state]",
                template: '[data-item]',
                source: function () {
                    var data = null;
                    jQuery.ajaxSetup({ async: false });
                    $.post('/FeatureBee/Features').done(function (d) {
                        data = d;
                    });
                    jQuery.ajaxSetup({ async: true });

                    return data;
                },
                subscribeToItemChanged: function (obj) {
                    boardHub.server.moveItem(obj.data.name, obj.data.oldIndex, obj.data.index);
                },
                subscribeToItemSelected: function (obj) {
                    form.openEdit(obj.data);
                }
            });

            $('#board').boardify('subscribeFor', 'page', 'itemChanged', $.boardifySubscribers.refresh);
            $('#board').boardify('subscribeFor', 'page', 'itemMoved', $.boardifySubscribers.refresh);
        }
    };

    var conditionTemplates = function () {
        var templates = [];
        $('[data-template]').each(function (index, value) {
            templates.push({
                type: $(value).attr('data-template'),
                template: $(value)
            });
        });

        var c = $('body').conditionify({
            conditions: templates
        });

        this.render = function (type, element, data) {
            c.conditionify('render', type, element, data);
        };
    };

    var handleBar = function (templates) {
        var self = this;

        var registerSelectHelper = function() {
            window.Handlebars.registerHelper('select', function (value, options) {
                var $el = $('<select />').html(options.fn(this));
                $el.find('[value=' + value + ']').attr({ 'selected': 'selected' });
                return $el.html();
            });
        };

        var registerConditionHelper = function() {
            window.Handlebars.registerHelper('condition', function(type) {
                var $el = $('<div />');
                templates.render(type, $el);
                return $el.html();
            });
        };

        var initHandleBar = function() {
            registerSelectHelper();
            registerConditionHelper();
        };

        initHandleBar();
    };

    var menu = {
        register: function (form) {
            $('[data-open="newFeature"]').click(form.openNew);
        }
    };

    var forms = function () {
        var self = this;

        var createForm = function (usingItem, callback) {
            return usingItem.clone().appendTo(usingItem.parent()).formify({
                save: function (data) {
                    callback(data);
                },
                width: $(window).width() - 180
            });
        };

        var createEditForm = function (usingItem) {
            return createForm(usingItem, function (data) {
                boardHub.server.editItem(data.oldName,
                    {
                        name: data.name,
                        team: data.team,
                        link: data.link,
                        index: data.index
                    });
            });
        };

        var createNewForm = function (usingItem) {
            return createForm(usingItem, function (data) {
                boardHub.server.addNewItem(
                    {
                        name: data.name,
                        team: data.team,
                        link: data.link,
                        index: 0
                    });
            });
        };

        var editItem = $('[data-edit-item="edit"]');
        var formEdit = createEditForm(editItem);
        var formNew = createNewForm(editItem);
        
        this.openEdit = function (data) {
            formEdit.formify('open', data);
        };

        this.openNew = function () {
            formNew.formify('open');
        };
    };


});