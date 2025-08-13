/**
 * Theme: Zircos Admin Template
 * Author: Coderthemes
 * Module/App: Core js
 */

// Portlet Widget
!function ($) {
    "use strict";

    var Portlet = function () {
        this.$body = $("body"),
            this.$portletIdentifier = ".portlet",
            this.$portletCloser = '.portlet a[data-toggle="remove"]',
            this.$portletRefresher = '.portlet a[data-toggle="reload"]'
    };

    // Portlet işlemlerini başlatma
    Portlet.prototype.init = function () {
        var $this = this;

        // Portlet kapatma
        $(document).on("click", this.$portletCloser, function (ev) {
            ev.preventDefault();
            var $portlet = $(this).closest($this.$portletIdentifier);
            var $portletParent = $portlet.parent();

            $portlet.slideUp("slow", function () {
                $(this).remove();
            });
            if ($portletParent.children().length === 0) {
                $portletParent.slideUp("slow", function () {
                    $(this).remove();
                });
            }
        });

        // Portlet yenileme
        $(document).on("click", this.$portletRefresher, function (ev) {
            ev.preventDefault();
            var $portlet = $(this).closest($this.$portletIdentifier);
            $portlet.append('<div class="panel-disabled"><div class="portlet-loader"></div></div>');
            var $pd = $portlet.find('.panel-disabled');
            const getSecureRandom = () => {
                if (window.crypto && window.crypto.getRandomValues) {
                    const array = new Uint32Array(1);
                    window.crypto.getRandomValues(array);
                    return array[0] / (0xFFFFFFFF + 1); 
                } else {
                    throw new Error("Secure random not supported in this environment");
                }
            };
            const delay = 500 + 300 * getSecureRandom() * 5;
            setTimeout(function () {
                $pd.fadeOut('fast', function () {
                    $pd.remove();
                });
            }, delay);
        });
    };

    // Portlet nesnesini tanımlama
    $.Portlet = new Portlet();
    $.Portlet.Constructor = Portlet;

}(window.jQuery);

// Components
!function ($) {
    "use strict";

    var Components = function () {};

    // Tooltip başlatma
    Components.prototype.initTooltipPlugin = function () {
        $.fn.tooltip && $('[data-toggle="tooltip"]').tooltip();
    };

    // Popover başlatma
    Components.prototype.initPopoverPlugin = function () {
        $.fn.popover && $('[data-toggle="popover"]').popover();
    };

    // Custom modal başlatma
    Components.prototype.initCustomModalPlugin = function () {
        $('[data-plugin="custommodal"]').on('click', function (e) {
            Custombox.open({
                target: $(this).attr("href"),
                effect: $(this).attr("data-animation"),
                overlaySpeed: $(this).attr("data-overlaySpeed"),
                overlayColor: $(this).attr("data-overlayColor")
            });
            e.preventDefault();
        });
    };

    // Nicescroll başlatma
    Components.prototype.initNiceScrollPlugin = function () {
        $.fn.niceScroll && $(".nicescroll").niceScroll({ cursorcolor: '#98a6ad', cursorwidth: '6px', cursorborderradius: '5px' });
    };

    // Slimscroll başlatma
    Components.prototype.initSlimScrollPlugin = function () {
        $.fn.slimScroll && $(".slimscroll-alt").slimScroll({ position: 'right', size: "5px", color: '#98a6ad', wheelStep: 10 });
    };

    // Diğer bileşenleri başlatma
    Components.prototype.initRangeSlider = function () {
        $.fn.slider && $('[data-plugin="range-slider"]').slider({});
    };
    Components.prototype.initSwitchery = function () {
        $('[data-plugin="switchery"]').each(function () {
        });
    };
    Components.prototype.initMultiSelect = function () {
        $('[data-plugin="multiselect"]').length > 0 && $('[data-plugin="multiselect"]').multiSelect($(this).data());
    };
    Components.prototype.initPeityCharts = function () {
        $('[data-plugin="peity-pie"]').each(function () {
            var colors = $(this).attr('data-colors') ? $(this).attr('data-colors').split(",") : [];
            var width = $(this).attr('data-width') || 20;
            var height = $(this).attr('data-height') || 20;
            $(this).peity("pie", { fill: colors, width: width, height: height });
        });
    };
    Components.prototype.initKnob = function () {
        $('[data-plugin="knob"]').each(function () {
            $(this).knob();
        });
    };
    Components.prototype.initCounterUp = function () {
        $('[data-plugin="counterup"]').each(function () {
            $(this).counterUp({
                delay: $(this).attr('data-delay') || 100,
                time: $(this).attr('data-time') || 1200
            });
        });
    };

    // Bileşenlerin tamamını başlatma
    Components.prototype.init = function () {
        this.initTooltipPlugin();
        this.initPopoverPlugin();
        this.initNiceScrollPlugin();
        this.initSlimScrollPlugin();
        this.initCustomModalPlugin();
        this.initRangeSlider();
        this.initSwitchery();
        this.initMultiSelect();
        this.initPeityCharts();
        this.initKnob();
        this.initCounterUp();
        $.Portlet.init();
    };

    // Components nesnesini tanımlama
    $.Components = new Components();
    $.Components.Constructor = Components;

}(window.jQuery);

// Ana modülü başlatma
(function ($) {
    "use strict";
    $.Components.init();
})(window.jQuery);
