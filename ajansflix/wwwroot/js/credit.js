"use strict";

var $t;

$('.js-input-cart-number').on('keyup change', function () {
    var card_number = '';
    $t = $(this);

    if ($t.val().length > 3) {
        $t.next().focus();
    }

    $('.js-input-cart-number').each(function () {
        card_number += $(this).val() + ' ';

        if ($(this).val().length == 4) {
            $(this).next().focus();
        }
    })

    $('.credit-card-box .number').html(card_number);
});

$('.js-card-holder').on('keyup change', function () {
    $t = $(this);
    $('.credit-card-box .card-holder div').html($t.val());
});

$('.js-card-holder').on('keyup change', function () {
    $t = $(this);
    $('.credit-card-box .card-holder div').html($t.val());
});

$('.js-card-expiration-month, .js-card-expiration-year').change(function () {
    var m = $('#card-expiration-month option').index($('#card-expiration-month option:selected'));
    var y = $('#card-expiration-year').val().substr(2, 2);
    m = (m < 10) ? '0' + m : m;
    $('.card-expiration-date div').html(m + '/' + y);
})

$('.js-card-ccv').on('focus', function () {
    $('.credit-card-box').addClass('hover');
}).on('blur', function () {
    $('.credit-card-box').removeClass('hover');
}).on('keyup change', function () {
    $('.ccv div').html($(this).val());
});