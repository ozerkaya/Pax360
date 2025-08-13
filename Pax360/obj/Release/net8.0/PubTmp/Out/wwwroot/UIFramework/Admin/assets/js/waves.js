;(function(window) {
    'use strict';

    var Waves = Waves || {};
    var $$ = document.querySelectorAll.bind(document);

    function isWindow(obj) {
        return obj != null && obj === obj.window;
    }

    function getWindow(elem) {
        return isWindow(elem) ? elem : elem.nodeType === 9 && elem.defaultView;
    }

    function offset(elem) {
        if (!elem || !elem.ownerDocument) {
            return { top: 0, left: 0 };
        }

        var docElem = elem.ownerDocument.documentElement;
        var box = { top: 0, left: 0 };

        if (typeof elem.getBoundingClientRect !== 'undefined') {
            box = elem.getBoundingClientRect();
        }
        var win = getWindow(elem.ownerDocument);
        return {
            top: box.top + win.pageYOffset - docElem.clientTop,
            left: box.left + win.pageXOffset - docElem.clientLeft
        };
    }

    function convertStyle(obj) {
        var style = '';
        for (var a in obj) {
            if (obj.hasOwnProperty(a)) {
                style += (a + ':' + obj[a] + ';');
            }
        }
        return style;
    }

    var Effect = {
        duration: 750,

        show: function(e, element) {
            if (!e || e.button === 2) {
                return false;
            }

            var el = element || this;
            if (!el) return false;

            var ripple = document.createElement('div');
            ripple.className = 'waves-ripple';
            el.appendChild(ripple);

            var pos = offset(el);
            var relativeY = (e.touches ? e.touches[0].pageY : e.pageY) - pos.top;
            var relativeX = (e.touches ? e.touches[0].pageX : e.pageX) - pos.left;
            var scale = 'scale(' + ((el.clientWidth / 100) * 10) + ')';

            ripple.setAttribute('data-hold', Date.now());
            ripple.setAttribute('data-scale', scale);
            ripple.setAttribute('data-x', relativeX);
            ripple.setAttribute('data-y', relativeY);

            var rippleStyle = {
                'top': relativeY + 'px',
                'left': relativeX + 'px'
            };

            ripple.className = ripple.className + ' waves-notransition';
            ripple.setAttribute('style', convertStyle(rippleStyle));
            ripple.className = ripple.className.replace('waves-notransition', '');

            rippleStyle['-webkit-transform'] = scale;
            rippleStyle['-moz-transform'] = scale;
            rippleStyle['-ms-transform'] = scale;
            rippleStyle['-o-transform'] = scale;
            rippleStyle.transform = scale;
            rippleStyle.opacity = '1';

            var duration = Effect.duration + 'ms';
            rippleStyle['-webkit-transition-duration'] = duration;
            rippleStyle['-moz-transition-duration'] = duration;
            rippleStyle['-o-transition-duration'] = duration;
            rippleStyle['transition-duration'] = duration;

            ripple.setAttribute('style', convertStyle(rippleStyle));
            return true;
        },

        hide: function(e) {
            TouchHandler.touchup(e);

            var el = this;
            var ripples = el.getElementsByClassName('waves-ripple');
            if (ripples.length === 0) return false;

            var ripple = ripples[ripples.length - 1];
            var relativeX = ripple.getAttribute('data-x');
            var relativeY = ripple.getAttribute('data-y');
            var scale = ripple.getAttribute('data-scale');

            var diff = Date.now() - Number(ripple.getAttribute('data-hold'));
            var delay = Math.max(0, 350 - diff);

            setTimeout(function() {
                var style = {
                    'top': relativeY + 'px',
                    'left': relativeX + 'px',
                    'opacity': '0',
                    '-webkit-transition-duration': Effect.duration + 'ms',
                    '-moz-transition-duration': Effect.duration + 'ms',
                    '-o-transition-duration': Effect.duration + 'ms',
                    'transition-duration': Effect.duration + 'ms',
                    '-webkit-transform': scale,
                    '-moz-transform': scale,
                    '-ms-transform': scale,
                    '-o-transform': scale,
                    'transform': scale
                };

                ripple.setAttribute('style', convertStyle(style));

                setTimeout(function() {
                    try {
                        el.removeChild(ripple);
                    } catch(e) {
                        return false;
                    }
                }, Effect.duration);
            }, delay);
        },

        wrapInput: function(elements) {
            for (var a = 0; a < elements.length; a++) {
                var el = elements[a];

                if (el.tagName.toLowerCase() === 'input') {
                    var parent = el.parentNode;
                    if (parent.tagName.toLowerCase() === 'i' && 
                        parent.className.indexOf('waves-effect') !== -1) {
                        continue;
                    }

                    var wrapper = document.createElement('i');
                    wrapper.className = el.className + ' waves-input-wrapper';
                    el.className = 'waves-button-input';

                    var elementStyle = el.getAttribute('style');
                    if (elementStyle) {
                        wrapper.setAttribute('style', elementStyle);
                    }
                    el.removeAttribute('style');

                    parent.replaceChild(wrapper, el);
                    wrapper.appendChild(el);
                }
            }
        }
    };

    var TouchHandler = {
        touches: 0,
        allowEvent: function(e) {
            if (!e) return false;
            
            var allow = true;
            var type = e.type.toLowerCase();
            
            if (type === 'touchstart') {
                TouchHandler.touches += 1;
            } else if (/^(touchend|touchcancel)$/.test(type)) {
                setTimeout(function() {
                    if (TouchHandler.touches > 0) {
                        TouchHandler.touches -= 1;
                    }
                }, 500);
            } else if (type === 'mousedown' && TouchHandler.touches > 0) {
                allow = false;
            }

            return allow;
        },
        touchup: function(e) {
            return TouchHandler.allowEvent(e);
        }
    };

    function getWavesEffectElement(e) {
        if (!e || TouchHandler.allowEvent(e) === false) {
            return null;
        }

        var element = null;
        var target = e.target || e.srcElement;

        while (target.parentElement) {
            if ((!(target instanceof SVGElement) && 
                target.className.indexOf('waves-effect') !== -1) || 
                target.classList.contains('waves-effect')) {
                    element = target;
                    break;
            }
            target = target.parentElement;
        }

        return element;
    }

    function showEffect(e) {
        var element = getWavesEffectElement(e);
        if (element) {
            Effect.show(e, element);

            if ('ontouchstart' in window) {
                element.addEventListener('touchend', Effect.hide, false);
                element.addEventListener('touchcancel', Effect.hide, false);
            }

            element.addEventListener('mouseup', Effect.hide, false);
            element.addEventListener('mouseleave', Effect.hide, false);
        }
    }

    Waves.displayEffect = function(options) {
        options = options || {};

        if ('duration' in options) {
            Effect.duration = options.duration;
        }

        Effect.wrapInput($$.call(document, '.waves-effect'));

        if ('ontouchstart' in window) {
            document.body.addEventListener('touchstart', showEffect, false);
        }

        document.body.addEventListener('mousedown', showEffect, false);
    };

    Waves.attach = function(element) {
        if (!element) return;

        if (element.tagName.toLowerCase() === 'input') {
            Effect.wrapInput([element]);
            element = element.parentElement;
        }

        if ('ontouchstart' in window) {
            element.addEventListener('touchstart', showEffect, false);
        }

        element.addEventListener('mousedown', showEffect, false);
    };

    window.Waves = Waves;

    document.addEventListener('DOMContentLoaded', function() {
        Waves.displayEffect();
    }, false);

})(window);
