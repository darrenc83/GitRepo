$(window).scroll(function () {
    var scrollAmount = $(window).scrollTop() / 1.5;
    scrollAmount = Math.round(scrollAmount);

    if ($(window).width() > 768) {
        //$('#map .gm-style > div:first-child > div:first-child').css('margin-top', scrollAmount + 'px'); // old script
        $('.has-parallax .gm-style').css('margin-top', scrollAmount + 'px');
        $('.has-parallax .leaflet-map-pane').css('margin-top', scrollAmount + 'px');

        if ($('#slider').hasClass('has-parallax')) {
            $(".homepage-slider").css('top', scrollAmount + 'px');
        }
    }
});
