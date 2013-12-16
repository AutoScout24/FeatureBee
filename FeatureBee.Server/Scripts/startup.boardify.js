var source = [
            { title: "a", team: 'asm', index: 0 },
            { title: "b", team: 'dealer', index: 1 },
            { title: "lala", team: 'asm', index: 0 },
            { title: "tata", team: 'asm', index: 2 },
            { title: "erw", team: '', index: 0 }
];

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

    $.Comm('page', 'itemChanged').subscribe(function(item) {
        source.push(item);
    });

    $.connection.hub.start().done(function () {
        var editItem = $('[data-edit-item="edit"]');
        var formEdit = editItem.clone().appendTo(editItem.parent()).formify({
            save: function (data) {
                boardHub.server.editItem(data.id, data.title, data.team, data.index);
            }
        });
        var formNew = editItem.clone().appendTo(editItem.parent()).formify({
            save: function (data) {
                boardHub.server.addNewItem(data.title, data.team);
            }
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
                boardHub.server.moveItem(obj.data.title, obj.data.oldIndex, obj.data.index);
            },
            subscribeToItemSelected: function (obj) {
                formEdit.formify('open', { title: obj.data.title, team: obj.data.team, index: obj.data.index });
            }
        });
        
        $('#board').boardify('subscribeFor', 'page', 'itemChanged', $.boardifySubscribers.refresh);
        $('#board').boardify('subscribeFor', 'page', 'itemMoved', $.boardifySubscribers.refresh);
    });

});