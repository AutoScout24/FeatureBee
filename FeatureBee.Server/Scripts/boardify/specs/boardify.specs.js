// include external files like so:
/// <reference path="~/Scripts/jquery-1.9.1.js" />
/// <reference path="~/Scripts/jquery-ui-1.10.3.js" />
/// <reference path="~/Scripts/handlebars-v1.1.2.js" />
/// <reference path="~/Scripts/boardify/comm.js" />
/// <reference path="/Scripts/jasmine/jasmine.js" />
/// <reference path="/Scripts/jasmine/jasmine-html.js" />
/// <reference path="~/Scripts/boardify/boardify.js" />

describe("boardify", function () {
    var itemsCreated = [];
    var itemMoved = null;
    var commMock = function (publisher, why) {
        var innerComm = $.Comm(publisher, why);

        var comm = {            
            publish: function(obj) {
                if (why == "items-created") {
                    itemsCreated = obj.data;
                }
                if (why == "item-moved") {
                    itemMoved = obj.data;
                }
                innerComm.publish(obj);
            },
            subscribe: function (obj) {
                innerComm.subscribe(obj);
            }
        };
        return comm;
    };

    beforeEach(function() {
        $('body').append('<div id="board">' + 
            '<div data-state="In Development"></div>' +
            '<div data-state="In Test"></div>' +
            '<div data-state="Released"></div>'  + 
            '<div data-item="template" class="template"><div data-id="{{title}}" class="post-it {{team}}">{{title}} {{team}}</div></div>' +
        '</div>');
    });

    it("should create a board", function() {
        var board = $('#board').boardify();
        expect(board).not.toBeNull();
    });

    describe("when board is created with a source", function () {
        var source = [
            { title: "a", team: 'asm', index: 0 },
            { title: "b", team: 'dealer', index: 1 },
            { title: "c", team: 'asm', index: 0 },
            { title: "d", team: 'asm', index: 2 },
            { title: "e", team: '', index: 0 }
        ];

        beforeEach(function() {
            var board = $('#board').boardify({
                source: function () { return source; },
                communicator: commMock
            });
        });

        it("should show all items on the board", function() {
            expect(itemsCreated.length).toBe(source.length);
        });

        describe("when item in source is changed", function () {
            beforeEach(function () {
                var board = $('#board').boardify({
                    source: function () { return source; },
                    communicator: commMock
                });
                board.boardify('dropElementTo', $('[data-id="a"]'), $('[data-state="In Test"]'), 1);
            });

            it("should notify the subscribers", function() {
                expect(itemMoved).not.toBeNull();
            });
            
            it("should return the changed item", function () {
                expect(itemMoved.title).toBe('a');
            });
            
            it("should return the old and new index", function () {
                expect(itemMoved.oldIndex).toBe(0);
                expect(itemMoved.index).toBe(1);
            });
        });
        
        describe("when item in source is modified from the outside", function () {
            beforeEach(function () {
                var board = $('#board').boardify({
                    source: function () { return source; },
                    communicator: commMock
                });
                $('#board').boardify('subscribeFor', 'page', 'itemChanged', $.boardifySubscribers.refresh);
                source[0].index = 1;
                commMock('page', 'itemChanged').publish(source[0]);
            });

            it("should be changed on the board", function () {
                expect($('#board').find('[data-id="' + source[0].title + '"]').parent().attr('data-state')).toBe("In Test");
            });
        });
    });

    afterEach(function() {
        $('#board').boardify('destroy');
        $('#board').remove();
    });
});