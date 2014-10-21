(function ($) {

    var $f = $.sample = {

        version: "1.0.0.0",

        //Enums
        Event: {
            Load: "load",
            Error: "error"
        },

        AjaxEvents: {
            BeforeSend: "beforeSend",
            Error: "error",
            Success: "success",
            Complete: "complete"
        },

        State: {
            Disabled: "sa-state-disabled",
            Hidden: "sa-state-hidden"
        },

        Property: {
            Instance: "sa-instance"
        },

        mapEvents: function (events, options, $element) {
            var mappedEvent = {};
            for (var event in events) {
                if (events.hasOwnProperty(event) && options.hasOwnProperty(events[event])) {
                    mappedEvent[events[event]] = options[events[event]];
                }
            }

            $f.on($element, mappedEvent);
        },

        create: function (query, settings) {
            var component = [];
            query.each(function () {
                var $$ = $(this);
                //Check if this selector is already initialized.
                if (!$$.data($f.Property.Instance)) {
                    component.push(settings.init(this, settings.options));
                    $$.data($f.Property.Instance, component);
                    $f.trigger(this, $f.Event.Load);
                } else {
                    component.push($$.data($f.Property.Instance)[0]);
                }
            });

            //return an array if more than one instance was selected
            return (component.length == 1) ? component[0] : component;
        },

        preventDefault: function (e) {
            e.preventDefault();
        },

        ajaxError: function (element, eventName, xhr, status) {
            var prevented = this.trigger(element, eventName,
                {
                    XMLHttpRequest: xhr,
                    textStatus: status
                });

            if (!prevented) {
                if (status == 'error' && xhr.status != '0')
                    alert('Error! The requested URL returned ' + xhr.status + ' - ' + xhr.statusText);
                if (status == 'timeout')
                    alert('Error! Server timeout.');
            }

            return prevented;
        },

        on: function (context, events) {
            var $element = $(context.element ? context.element : context);
            $.each(events, function (eventName) {
                if ($.isFunction(this)) $element.on(eventName, this);
            });
        },

        trigger: function (element, eventName, e) {
            if (!eventName && e) {
                eventName = e.type;
            }

            e = $.extend(e || {}, new $.Event(eventName));
            $(element).trigger(e);
            return e.isDefaultPrevented();
        }
    };

    //.Sample() namespace and Plugin definitions
    $.fn.Sample = function (options) {
        var $selector = $(this.selector);
        var defaults = {
            toggleClick: function (options) {
                return $f.create($selector, {
                    init: function ($selector, options) {
                        return new $f.toggleClick($selector, options);
                    },
                    options: options
                });
            }
        };

        return $.fn.extend(defaults, options);
    };

})(jQuery);

