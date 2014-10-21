
(function ($) {

    var $f = $.sample; //Base

    //Plug-in Specific Enums
    var ClientEvents = {
        Load: "load",
        Toggle: "toggle",
        SwitchOn: "switchOn",
        SwitchOff: "switchOff"
    };

    var State = {
        Selected: "sa-state-selected"
    };

    //Extend the base States
    State = $.extend($f.State, State);

    $.extend($f, {

        toggleButton: function (element, options) {
            var $element = $(element);
            var _self = this;
            var $value = $element.find("[data=sa-value]");

            if (options) {
                $f.mapEvents(ClientEvents, options, $element);
            }

            var clickableItems = '.sa-switch';
            $(clickableItems, $element).on("click", function (e) {
                var $target = $(e.target);
                if (!$element.hasClass(State.Disabled)) {
                    _toggleItem($target, e, true);
                }
            });

            //Private   
            var _toggleItem = function ($target, e, triggerEvent) {
                if (!$target.hasClass(State.Selected)) {
                    $target.addClass(State.Selected);

                    var onOffEvent = null;
                    if ($target.hasClass("sa-switch-on")) {
                        $target.next().toggleClass(State.Selected);
                        $value.val(true);
                        onOffEvent = $.Event(ClientEvents.SwitchOn);
                    }
                    else {
                        $target.prev().toggleClass(State.Selected);
                        $value.val(false);
                        onOffEvent = $.Event(ClientEvents.SwitchOff);
                    }

                    if (triggerEvent) {
                        var evt = $.Event(ClientEvents.Toggle);
                        evt.value = ($value.val() === 'true');
                        $f.trigger($element, null, evt);

                        onOffEvent.value = ($value.val() === 'true');
                        $f.trigger($element, null, onOffEvent);
                    }
                }
            };

            //Public    
            _self.isOn = function () {
                return _self.value();
            };

            _self.isOff = function () {
                return _self.value();
            };

            _self.value = function () {
                return ($value.val() === 'true');
            };

            _self.selectedLabel = function () {
                return $element.find(".sa-selected").text();
            };

            _self.toggle = function (onOff) {
                //Have they specified what they want to toggle it to? If not - just set it to the opposite of what it is now
                if (typeof onOff === "undefined") {
                    onOff = !(_self.value());
                }

                if (onOff) {
                    _self._toggleItem($(".sa-switch-on", $element));
                } else {
                    _self._toggleItem($(".sa-switch-off", $element));
                }
            };

            _self.enable = function () {
                if ($element.hasClass(State.Disabled)) {
                    $element.toggleClass(State.Disabled);
                }
            },
                _self.disable = function () {
                    if (!$element.hasClass(State.Disabled)) {
                        $element.toggleClass(State.Disabled);
                    }
                };
        }
    });
})(jQuery);

