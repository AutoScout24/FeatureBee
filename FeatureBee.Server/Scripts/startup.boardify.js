$(function () {
    var initHandleBar = function() {
        window.Handlebars.registerHelper('select', function(value, options) {
            var $el = $('<select />').html(options.fn(this));
            $el.find('[value=' + value + ']').attr({ 'selected': 'selected' });
            return $el.html();
        });
    };
    
    initHandleBar();

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
        var editItem = $('[data-edit-item="edit"]');
        var formEdit = editItem.clone().appendTo(editItem.parent()).formify({
            save: function (data) {
                boardHub.server.editItem(data.oldName,
                {
                    name: data.name,
                    team : data.team, 
                    link : data.link, 
                    index : data.index
                });
            },
            width: $(window).width() - 180
        });
        var formNew = editItem.clone().appendTo(editItem.parent()).formify({
            save: function (data) {
                boardHub.server.addNewItem({
                    name: data.name,
                    team: data.team,
                    link: data.link,
                    index: 0
                });
            },
            width: $(window).width() - 180
        });
        $('[data-open="newFeature"]').click(function () { formNew.formify('open'); });

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
            subscribeToItemChanged: function(obj) {
                boardHub.server.moveItem(obj.data.name, obj.data.oldIndex, obj.data.index);
            },
            subscribeToItemSelected: function (obj) {
                formEdit.formify('open', obj.data);
            }
        });
        
        $('#board').boardify('subscribeFor', 'page', 'itemChanged', $.boardifySubscribers.refresh);
        $('#board').boardify('subscribeFor', 'page', 'itemMoved', $.boardifySubscribers.refresh);
    });

});