var source = [
            { title: "a", team: 'asm', index: 0 },
            { title: "b", team: 'dealer', index: 1 },
            { title: "lala", team: 'asm', index: 0 },
            { title: "tata", team: 'asm', index: 2 },
            { title: "erw", team: '', index: 0 }
];

$(function() {
    // Declare a proxy to reference the hub. 
    var boardHub = $.connection.boardHub;

    // Create a function that the hub can call to broadcast new items.
    boardHub.client.newItemAdded = function (item) {
        $.Comm('page', 'itemChanged').publish(item);
    };

    $.Comm('page', 'itemChanged').subscribe(function(item) {
        source.push(item);
    });

    var editTemplate = $('[data-edit-item="template"]').html();
    var compilededitTemplate = Handlebars.compile(editTemplate);

    $.connection.hub.start().done(function() {
        $('#board').boardify({
            states: "[data-state]",
            template: '[data-item]',
            source: function() { return source; },
            subscribeToItemChanged: function(obj) {
                boardHub.server.move(obj.title, obj.oldIndex, obj.index);
                for (var i = source.length - 1; i >= 0; i--) {
                    if (source[i].title == obj.data.title) {
                        source[i] = obj.data;
                        boardHub.server.move("new!", "new Team");
                        break;
                    }
                }
                ;
            },
            subscribeToItemSelected: function (obj) {
                $('[data-edit-item="template"]').formify();
                $('[data-edit-item="template"]').formify('open', { title: obj.data.title, team: obj.data.team });
            }
        });
        $('#board').boardify('subscribeFor', 'page', 'itemChanged', $.boardifySubscribers.refresh);
        $('[data-edit-item="newFeature"]').formify({
            save: function(data) {
                boardHub.server.addNewItem(data.title, data.team);
            }
        });
        
        $('[data-open="newFeature"]').click(function () { $('[data-edit-item="newFeature"]').formify('open'); });
        $('#kill').click(function() { $('#board').boardify('destroy'); });
    });

});