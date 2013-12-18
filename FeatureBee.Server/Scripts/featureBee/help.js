$(function() {
    /*
    wrapper to toggle the help tooltips. registers
    the widget automatically as well
    */

    $.widget("as24.help", {
        tooltips: [],
        open : false,
        
        _getPosition : function(name) {
            var position = {};
            switch (name) {
                case 'top':
                    position = { my: 'center bottom', at: 'center top-10' };
                    break;
                case 'bottom':
                    position = { my: 'center top', at: 'right bottom+10' };
                    break;
                case 'left':
                    position = { my: 'right center', at: 'left-10 center' };
                    break;
                case 'right':
                    position = { my: 'left center', at: 'right+10 center' };
                    break;
            }
            position.collision = 'none';
            return position;
        },

        _create: function () {
            var self = this;
            $("[data-help]").click(function (event) {
                $("[data-tooltip]").each(function () {
                    var position = $(this).attr('data-tooltip-position');
                    $(this).attr('title', $(this).attr('data-tooltip'));
                    self.tooltips.push($(this).tooltip({
                        position: self._getPosition(position),
                        tooltipClass: position,
                        close: function() {
                            $(this).tooltip('destroy');
                            $(this).attr('title', null);
                        }
                    }));
                    $(this).tooltip("open");
                });
                
                var firstBoardItem = $('[data-board-item]').first();
                firstBoardItem.attr('title', 'This is a Feature. You can see its name, the team it belongs to, and the conditions it currently has. Doubleclick it to edit it. ');
                self.tooltips.push(firstBoardItem.tooltip({
                    position: self._getPosition('right'),
                    tooltipClass: 'right',
                    close: function () { $(this).tooltip('destroy'); $(this).attr('title', null); }
                }));
                firstBoardItem.tooltip("open");
                
                var firstCondition = $('[data-board-item]').not(':first').find(' .label').first();
                firstCondition.attr('title', 'This is a condition. It controls who can see the feature when in the second step.');
                self.tooltips.push(firstCondition.tooltip({
                    position: self._getPosition('bottom'),
                    tooltipClass: 'bottom',
                    close: function () { $(this).tooltip('destroy'); $(this).attr('title', null); }
                }));
                firstCondition.tooltip("open");
                self.open = true;
                event.preventDefault();
                event.stopPropagation();
            });

            $(document).click(function() {
                if (self.open) {
                    $.each(self.tooltips, function() {
                        $(this).tooltip('close');
                    });
                    self.tooltips = [];
                    self.open = false;
                }
            });
        }
    });

    $(document).help();
});