$(function () {
    // board hub => board specific functions. 
    var boardHub = $.connection.boardHub;
    // edit panel hub => edit dialog specific functions.
    var editPanelHub = $.connection.editPanelHub;
    var form;
    
    boardHub.client.featureCreated = function (item) {
        $.Comm('page', 'itemChanged').publish(item);
    };

    boardHub.client.featureReleased = function (item) {
        $.Comm('page', 'itemMoved').publish(item);
    };
    
    boardHub.client.featureTested = function (item) {
        $.Comm('page', 'itemMoved').publish(item);
    };

    boardHub.client.featureRollbacked = function (item) {
        $.Comm('page', 'itemMoved').publish(item);
    };

    editPanelHub.client.itemEdited = function (item) {
        $.Comm('page', 'itemChanged').publish(item);
    };

    editPanelHub.client.conditionValueAddedToFeature = function (item) {
        $.Comm('page', 'valueAddedTo:' + item.name).publish(item);
    };
    
    editPanelHub.client.conditionValueRemovedFromFeature = function (item) {
        $.Comm('page', 'valueRemovedFrom:' + item.name).publish(item);
    };
    
    editPanelHub.client.conditionCreatedForFeature = function (item) {
        $.Comm('page', 'conditionCreatedTo:' + item.name).publish(item);
    };

    var boot = {
        loadPrerequisite: function () {
            form = new forms();
            return this;
        },

        loadTemplates: function () {
            handleBar(new conditionTemplates(form));
            return this;
        },
   
        loadMenu: function () {
            $('[data-open="newFeature"]').click(form.openNew);
            return this;
        },
        
        loadBoard: function () {
            board();
            return this;
        }
    };

    var board = function() {
        $('#board').boardify({
            states: "[data-state]",
            template: '[data-item]',
            source: function() {
                var data = null;
                jQuery.ajaxSetup({ async: false });
                $.get('/api/features').done(function(d) {
                    data = d;
                });
                jQuery.ajaxSetup({ async: true });

                return data;
            },
            subscribeToItemChanged: function(obj) {
                boardHub.server.moveItem(obj.data.name, obj.data.oldIndex, obj.data.index);
            },
            subscribeToItemSelected: function(obj) {
                form.openEdit(obj.data);
            }
        });

        $('#board').boardify('subscribeFor', 'page', 'itemChanged', $.boardifySubscribers.refresh);
        $('#board').boardify('subscribeFor', 'page', 'itemMoved', $.boardifySubscribers.refresh);
    };

    var conditionTemplates = function () {
        var templates = [];
        $('[data-template]').each(function (index, value) {
            templates.push({
                type: $(value).attr('data-template'),
                template: $(value)
            });
        });

        var c = $('[data-container="condition"]').conditionify({
            conditions: templates,
            add: function (data) {
                editPanelHub.server.addConditionValue(data.name, data.type, data.values);
            },
            delete: function (data) {
                editPanelHub.server.removeConditionValue(data.name, data.type, data.values);
            },
            new: function (data) {
                editPanelHub.server.createCondition(data.name, data.type);
            }
        });

        this.render = function (name, type, element, data) {
            c.conditionify('render', name, type, element, data);
        };
        
        this.renderAddNewCondition = function (name) {
            c.conditionify('renderAddNewCondition', name);
        };
    };

    var handleBar = function(templates) {
        var self = this;
        
        Handlebars.registerHelper('setIndex', function (value) {
            this.outerindex = Number(value);
        });

        window.Handlebars.registerHelper('select', function(value, options) {
            var $el = $('<select />').html(options.fn(this));
            $el.find('[value=' + value + ']').attr({ 'selected': 'selected' });
            return $el.html();
        });
        window.Handlebars.registerHelper('condition', function(name, type, conditions, options) {
            var $el = $(options.fn({ type: type, values: conditions }).trim());
            templates.render(name, type, conditions);
            return $el.html();
        });
        window.Handlebars.registerHelper('emptyCondition', function (name) {
            templates.renderAddNewCondition(name);
            return "";
        });
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
                editPanelHub.server.editItem(data.oldName,
                    {
                        name: data.name,
                        team: data.team,
                        link: data.link,
                        index: data.index,
                        conditions: data.conditions
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
                        conditions: data.conditions
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

    $.connection.hub.start().done(function () {
        boot.loadPrerequisite().loadTemplates().loadMenu().loadBoard();
    });
});