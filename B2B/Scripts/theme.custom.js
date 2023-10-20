/* Add here all your JS customizations */

(function ($) {

    'use strict';

    //Modal
    $(document).on('hidden.bs.modal', '.modal', function (event) {
        $(this).removeClass('fv-modal-stack');
        $('body').data('fv_open_modals', $('body').data('fv_open_modals') - 1);
    });

    $(document).on('shown.bs.modal', '.modal', function (event) {
        // keep track of the number of open modals
        if (typeof ($('body').data('fv_open_modals')) == 'undefined') {
            $('body').data('fv_open_modals', 0);
        }

        // if the z-index of this modal has been set, ignore.
        if ($(this).hasClass('fv-modal-stack')) {
            return;
        }

        $(this).addClass('fv-modal-stack');

        $('body').data('fv_open_modals', $('body').data('fv_open_modals') + 1);

        $(this).css('z-index', 100040 + (10 * $('body').data('fv_open_modals')));

        $('.modal-backdrop').not('.fv-modal-stack').css('z-index', 100039 + (10 * $('body').data('fv_open_modals')));

        $('.modal-backdrop').not('fv-modal-stack').addClass('fv-modal-stack');

    });

}).apply(this, [jQuery]);