$(function () {
    $.boardifySubscribers = {        
        refresh: 0
    };

    $.widget("as24.boardify", {
        // default options
        options: {
            states: "[data-state]",
            template: "[data-item]",
            boardItem: 'data-board-item',
            boardItemAttribute : '',
            itemStateSelector: 'index',
            minumumIndexWithNotes : 0,
            maximumIndexWithNotes : 1,

            source: function () { return []; },

            // communicatorId
            publisher: 'boardify',
            streams: {
                moved: 'item-moved',
                selected: 'item-selected',
                itemsCreated: 'items-created'
            },
            communicator: function (publisher, why) {
                return $.Comm ? $.Comm(publisher, why) : { subscribe: function() {}, publish: function () {} };
            },

            // callbacks
            change: null,
            subscribeToItemsCreated: function () { },
            subscribeToItemChanged: function () { },
            subscribeToItemSelected: function () { }
        },

        subscribeTo: function (to, what) {
            this._commPub(to, what);
        },

        subscribeFor: function (publisher, why, doWhat) {
            var self = this;
            var action = function () { };
            
            if (doWhat === $.boardifySubscribers.refresh) {
                action = function() { self._refresh(); };
            }

            self.options.communicator(publisher, why).subscribe(action);
        },

        onItemChanged: function () {
            this._refresh();
        },

        dropElementTo: function (draggable, dropTarget, dropIndex) {
            var self = this;
            var data = draggable.data();
            data.oldIndex = data[self.options.itemStateSelector];
            data[self.options.itemStateSelector] = dropIndex;
            $(draggable).detach().css({ top: 0, left: 0 }).appendTo(dropTarget);
            
            self._commPub(self.options.streams.moved, { sender: self, element: draggable, data: data });
        },

        // the constructor
        _create: function () {
            var self = this;
            var ele = $(this.element);
            var source = ele.find(self.options.template).html();
            self.compiledTemplate = Handlebars.compile(source);
            $(self.options.template).hide();

            var states = ele.find(self.options.states);
            states.each(function (i, state) {
                $(state).droppable({
                    drop: function (ev, ui) {
                        self.dropElementTo(ui.draggable, this, i);
                    }
                });
            });
            
            // register subscriptions
            self._commSub(self.options.streams.selected, self.options.subscribeToItemSelected);
            self._commSub(self.options.streams.moved, self.options.subscribeToItemChanged);
            self._commSub(self.options.streams.itemsCreated, self.options.subscribeToItemsCreated);
            
            this.option(this.options);
        },

        _commPub: function (to, what) {
            this.options.communicator(this.options.publisher, to).publish(what);
        },

        _commSub: function (to, withWhat) {
            this.options.communicator(this.options.publisher, to).subscribe(withWhat);
        },

        _cleanBoardItems: function (states, self) {
            states.find(self.options.boardItemAttribute).draggable('destroy');
            states.find(self.options.boardItemAttribute).each(function (i, value) {
                $(value).off();
                $(value).remove();
            });
        },

        // called when created, and later when changing options
        _refresh: function () {
            var self = this;
            var ele = $(this.element);
            var states = ele.find(self.options.states);
            
            self._cleanBoardItems(states, self);
            self.elements = [];
            $.each(self.options.source(), function (i, item) {
                var html = self.compiledTemplate(item).trim();
                var element = $(html).clone();
                element.attr(self.options.boardItem, true);
                self._rotate(element, Math.floor((Math.random() * 10) - 5));
                element.data(item);
                self.elements.push(item);
                var currentStateIndex = item[self.options.itemStateSelector];
                if (currentStateIndex < self.options.minumumIndexWithNotes || currentStateIndex > self.options.maximumIndexWithNotes) {
                    element.find('.label').hide();
                } else {
                    element.find('.label').show();
                }
                var currentState = $(states[currentStateIndex]);
                currentState.append(element);
                element.on('dblclick', function (e) {
                    self._commPub(self.options.streams.selected, {
                        sender: self, element: element, data: item
                    });
                    
                    e.stopImmediatePropagation();
                    e.stopPropagation();
                    return false;
                });
            });

            self._commPub(self.options.streams.itemsCreated, { sender: self, data: self.elements });
            
            states.find(this.options.boardItemAttribute).draggable();

            self._alignUi(states);
            // trigger a callback/event
            this._trigger("change");
        },
        
        _alignUi: function (states) {
            var maxHeight = -1;
            
            $(states).each(function () {
                $(this).height('auto');
            });
            
            $(states).each(function () {
                maxHeight = maxHeight > $(this).height() ? maxHeight : $(this).height();
            });

            $(states).each(function () {
                $(this).height(maxHeight);
            });
        },

        // events bound via _on are removed automatically
        // revert other modifications here
        _destroy: function () {
            var self = this;
            $(self.options.template).show();
            // remove generated elements
            var states = self.element.find(self.options.states);
            self._cleanBoardItems(states, self);
            states.each(function (i, state) {
                $(state).droppable('destroy');
            });
        },

        // _setOptions is called with a hash of all options that are changing
        // always refresh when changing options
        _setOptions: function () {
            var self = this;
            // _super and _superApply handle keeping the right this-context
            self._superApply(arguments);
            self._refresh();
        },

        // _setOption is called for each individual option that is changing
        _setOption: function (key, value) {
            if (key == "boardItem") {
                this._super("boardItemAttribute", "[" + value + "]");
            }
            
            this._super(key, value);
        },
        
        _rotate: function(ele, degrees) {
            $(ele).css({
                '-webkit-transform': 'rotate(' + degrees + 'deg)',
                '-moz-transform': 'rotate(' + degrees + 'deg)',
                '-ms-transform': 'rotate(' + degrees + 'deg)',
                'transform': 'rotate(' + degrees + 'deg)'
            });
        }
    });
});