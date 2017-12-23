
$(document).ready(function(){
  'use strict';



//if ($(window).width() > 768) {
    $(window).scroll(function() {
        $('.animation-test').each(function(){
        var imagePos = $(this).offset().top;

        var topOfWindow = $(window).scrollTop();
            if (imagePos < topOfWindow+500) {
                $(this).addClass("custom-animate");
            }
        });
    });
$('.element-to-hide').css('visibility', 'hidden');
//}



//if ($(window).width() > 768) {
    $(window).scroll(function() {
        $('.animation-sooner').each(function(){
        var imagePos = $(this).offset().top;

        var topOfWindow = $(window).scrollTop();
            if (imagePos < topOfWindow+900) {
                $(this).addClass("custom-animate");
            }
        });
    });
$('.element-to-hide').css('visibility', 'hidden');
//}


 
var header = $('.mooey');
var test = $('.another');
var range = 80;

$(window).on('scroll', function () {
  
    var scrollTop = $(this).scrollTop();
    var offset = header.offset().top;
    var height = header.outerHeight();
    offset = offset + height / 2;
    var calc = 1 - (scrollTop - offset + range) / range;
  
    header.css({ 'opacity': calc });
  
    if ( calc > '1' ) {
      header.css({ 'opacity': 1 });
    } else if ( calc < '0' ) {
      header.css({ 'opacity': 0 });
    }
  
});


$(window).scroll(function () {
        console.log(scrollAmount);
        var scrollAmount = $(window).scrollTop() / 1.5;
        scrollAmount = Math.round(scrollAmount);


        if ($(window).width() > 768) {


                //$('#map .gm-style > div:first-child > div:first-child').css('margin-top', scrollAmount + 'px'); // old script
                $('.has-parallax').css('margin-top', scrollAmount + 'px');
                $('.spoon').css('margin-top', scrollAmount + 'px');

            if($('#slider').hasClass('has-parallax')){
                $(".homepage-slider").css('top', scrollAmount + 'px');
            }
        }
    });



function sticky_relocate() {
    var window_top = $(window).scrollTop();
    var div_top = $('#sticky-anchor').offset().top;
    if (window_top > div_top) {
        $('#sticky').addClass('stick');
        $('.hello').fadeIn(900);
    } else {
        $('#sticky').removeClass('stick');
        $('.hello').fadeOut(300);
    }
}

$(function () {
    $(window).scroll(sticky_relocate);
    sticky_relocate();
});



 //END OF CONTENT
  
});
