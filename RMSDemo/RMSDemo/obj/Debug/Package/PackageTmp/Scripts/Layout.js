$(document).ready(function () {
    $(document).on('click', '.collapaseBox h3', function () {
        $(this).closest('.collapaseBox').toggleClass('active')
    });

    $(document).on('click', '.header-menu > li.isDropdown', function () {
        $(this).toggleClass('active')
    });

    
})