function toggleMenu(elm) {
    var $elm = $(elm),
        $parent = $elm.closest('.header-menu-inner'),
        $target = $parent.find('.menu-list-items');

    $target.slideToggle();
}

function openSubMenu(elm, targetElm) {
    var $elm = $(elm),
        $tabs = $('.menu-list-items a'),
        $targetElm = $('#' + targetElm),
        $targetWrapper = $('#menu-item-details'),
        $otherElms = $targetWrapper.find('.menu-item-details');


    if ($elm.hasClass('active')) {
        $(elm).removeClass('active');
        $targetWrapper.fadeOut();
        $targetElm.fadeOut();
    } else {
        $tabs.removeClass('active');
        $(elm).addClass('active');
        $targetWrapper.children('div').css('display', 'none');
        $targetElm.fadeIn();
        $targetWrapper.fadeIn();
    }
}

function closeSubmenu() {
    var $targetWrapper = $('#menu-item-details'),
        $targetElm = $targetWrapper.find('.menu-item-details');

    $targetWrapper.fadeOut();
    $targetElm.fadeOut();
}