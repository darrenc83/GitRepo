$(document).ready(function () {
    'use strict';

    spaceBarScroller();

    //this is a bit of a hack to reinitialise the space bar scroller function because the third party plugin isn't using jquery .on 
    $(document).on('mouseover', '.right-block', function () {
        spaceBarScroller();
    });

    function spaceBarScroller() {
        //KINETIC SCROLL - This is for anything that needs to be scrolled by clicking and dragging rather than using the scrollbars///////////////////////////
        var $scroller = $('.drag-scroll')
        //this iis making sure that the space bar is held down to allow scrolling
        $(document).keydown(function (e) {
            if (e.keyCode === 32 && e.target.nodeName === 'BODY') {
                $scroller.kinetic('attach');
                //event.preventDefault();
                //event.stopPropagation();
                return false;
            }
            else {
                $scroller.kinetic('detach');
            }
        });
        $(document).keyup(function (e) {
            if (e.keyCode === 32 && e.target.nodeName === 'BODY') {
                $scroller.kinetic('detach');
                $('.fader-empty').css('cursor', 'pointer');

                event.stopPropagation();
            }
        });
    }



    //this triggers bootstraps tooltip
    $(function () {
        $('[data-toggle="tooltip"]').tooltip()
    })



    //this is making sure that when text is truncated in the list that the full text is shown on hover
    $(document).on('mouseenter',
      ".selectable-list-style1 .select div, .system-details header li p, .system-details .cell",
       function () {
           var $this = $(this);
           if (this.offsetWidth < this.scrollWidth && !$this.attr('title')) {
               $this.tooltip({
                   title: $this.text(),
                   placement: "bottom"
               });
               $this.tooltip('show');
           }
       });






    //this is used to make sure the prev and continue buttons become fixed to the top on scroll (currently for mobile)
    $(window).scroll(function () {

        var elementToFix = $('.frozen_top');
        if (elementToFix.length === 0) {
            return;
        }
        var range = 0

        var div_top = elementToFix.offset().top;
        var topOfWindow = $(window).scrollTop();
        var calc = (topOfWindow - div_top + range);
        var height = elementToFix.outerHeight();

        //console.log("div position" + div_top);
        //console.log("top of window" + topOfWindow);
        //console.log("height" + height);
        //console.log("calc" + calc);

        if (calc > 1) {
            $('.frozen_top .setup-arrows-inner').addClass('fixed');
        }

        if (calc < 0) {
            $('.frozen_top .setup-arrows-inner').removeClass('fixed');
        }
    });

    //SELECT LIST - Single select list - when different li is clicked then others are deselected/////////////////////////// 

    $('.single-select li').click(function () {
        $(this).siblings().removeClass('selected').end().addClass('selected');
    });

    //THIS IS TO MAKE SURE THE CORRECT TAB IS SELECTED BASED ON THE HREF OF THE LINK AND THE URL OF THE PAGE (IF IT IS A TAB THAT TRIGGERS PAGE RELOAD RATHER
    //THAN CHANGING CONTENT DYNAMICALLY)
    $('.tabs-container .tab').each(function () {
        var path = window.location.href;
        var current = path.substring(path.lastIndexOf('/') + 1);
        var url = $(this).attr('href');

        if (url == current) {
            $(this).addClass('selected');
        };
    });

    //TABS - Use this to flip between content using tabs
    $('.tabs-container:not(.page-reload-tabs) .tab').click(function () {
        $(this).siblings().removeClass('selected').end().addClass('selected');

        var myTab = $(this).attr('data-tab');
        $('.tab-content').addClass('hidden');
        $('#' + myTab).removeClass('hidden');
    });

    //SELECTABLE SCROLL - THIS IS FOR LISTS WHERE YOU CAN DRAG AND HOLD TO SELECT///////////////////////////
    $('.selectable-scrollable').selectableScroll({
        filter: 'li',
        scrollSnapX: 5, // When the selection is that pixels near to the top/bottom edges, start to scroll
        scrollSnapY: 5, // When the selection is that pixels near to the side edges, start to scroll
        scrollAmount: 25, // In pixels
        distance: 0, // In pixels
        scrollIntervalTime: 100 // In milliseconds
    });


    //custom selectable scroll////////////
    $('.custom-selectable-list').selectableScroll({
        filter: 'li',
        scrollSnapX: 5, // When the selection is that pixels near to the top/bottom edges, start to scroll
        scrollSnapY: 5, // When the selection is that pixels near to the side edges, start to scroll
        scrollAmount: 25, // In pixels
        distance: 0, // In pixels
        scrollIntervalTime: 100 // In milliseconds
    });



    //this gets the width of the widest div and applies the width to all other divs further down the row to make sure they are all in line




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



    // Blur handler for the haruki input fields
    $('.input__field--haruki')
        .blur(function () {
            $(this)
                .parents('.input--haruki')
                .first()
                .toggleClass('input--filled', $(this).val().trim() !== '');
        })
        .blur().first().focus();



    //THIS IS FOR THE MENU ICON TO REVEAL THE NAVIGATION  ON MOBILE OR SMALL BROWSERS///////////////////////////
    $('#hamburger').click(function () {
        if ($('#overall-wrapper').hasClass('active')) {
            $(this).removeClass('open');
            $('#overall-wrapper').removeClass('active');
            $('#sidebar .menu').fadeOut(300);
            //$('html').removeClass('menu-open');//
            window.setTimeout(function () { $("html").removeClass("menu-open"); }, 300);
        } else {
            $(this).addClass('open');
            $('#overall-wrapper').addClass('active');
            $('html').addClass('menu-open');
            $('#sidebar .menu').fadeIn(100);

        }

    });



    //THIS IS USED FOR THE MAIN MENU TO ALLOW DROPDOWNS FOR SUB MENU ITEMS///////////////////////////


    $('.dropdown').click(function () {

        $(this).children('.child-items').slideToggle(function () {

            if ($(this).is(":hidden")) {
                $(this).parents('.dropdown').removeClass('selected');
            }
            else {
                $(this).parents('.dropdown').addClass('selected');
            }

        });

        //preventing the child items triggering the click even on the parent
        $(".child-items").click(function (e) {
            e.stopPropagation();
        });




    });


    //POPUPS////////////////////////////////////////////////////////////////
    $(document).on('click', '.popupTrigger', function () {
        if (!$(this).hasClass('disabled')) {
            var myPopup = $(this).attr("data-popup");
            $('#' + myPopup).stop().fadeIn(300);
        }
    });

    //this makes sure that a user can click outside of the popup and it makes it hide but clicking within hides it
    $(document).mouseup(function (e) {
        var container = $(".popup-container");

        if (!container.is(e.target) // if the target of the click isn't the container...
            && container.has(e.target).length === 0) // ... nor a descendant of the container
        {
            container.hide();
        }
    });






    ////CASH VIEW MOBILE DROPDOWN//////////////////////////////////////////////////////////////////////////////////////////////////
    //this is for on an individual machine page - the cash table in mobile view will act as a dropdown - this will trigger it/////////////

    $('.cashViewTrigger').click(function () {
        if (!$(this).hasClass('disabled')) {
            var myModal = $(this).attr("data-reveal-cash");
            $('#' + myModal).toggle();

            if ($('#' + myModal).is(":visible")) {
                $(this).children('.dropdown').addClass('open');

            }
            else {
                $(this).children('.dropdown').removeClass('open');
            }

        }
    });



    ////DIALOGS//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //this is for dialogs and places an overlay in the background////////////////////////////////////////////////////////////////
    $('.close-dialog, .cancel-btn').click(function () {
        $('.dialog').fadeOut(300);
        $('.overlay').fadeOut(300);
        $('body').removeClass('stop-scroll-on-body');
        $('#overall-wrapper').removeClass('stop-scroll-on-body');

    });
    //this is doing exactly the same as above and closes the dialog if the escape key is pressed
    $(document).keyup(function (e) {
        if (e.keyCode === 27) { // escape key maps to keycode `27`
            $('.dialog').hide();
            $('.overlay').hide();
            $('body').removeClass('stop-scroll-on-body');
            $('#overall-wrapper').removeClass('stop-scroll-on-body');

        }
    });

    $(document).on('click', '.modalTrigger', function () {
        if (!$(this).hasClass('disabled')) {
            var myModal = $(this).attr("data-modal");
            $('#' + myModal).stop().fadeIn(300);
            $('.overlay').fadeIn(300);
            $('body').addClass('stop-scroll-on-body');
            $('#overall-wrapper').addClass('stop-scroll-on-body');
        }
    });


    //COLLECTIONS REFILL - this is to make the div switch when clicking on refill system. this should show a refill table// 
    $('#collection-status .refill-switch').click(function () {
        $('#collection-status').hide();
        $('#collector-refill-system').removeClass('hidden');
    });
    $('#collector-refill-system .refill-switch').click(function () {
        $('#collector-refill-system').addClass('hidden');
        $('#collection-status').fadeIn(300);
    });



    $(document)
        // Click handler for the two-stage menu selectors, e.g. in ManagerRefill
        .on('click', '.menuTrigger', function () {

            var menuItemId = $(this).attr("data-menu");
            var fadeInTime = 0;

            // Desktop version
            if ($(window).width() > 1400) {
                $(this).addClass('selected').siblings().removeClass('selected');
            }
                // Mobile version
            else {
                $('.second-navigation').addClass("slide-in");
                $('.first-navigation').addClass("nudge");
            }

            // Hide th previous 2nd-level section and show the selected one
            $('#' + menuItemId).stop().fadeIn(fadeInTime).siblings('ul').hide();

        })
        // Click handler for the "Go back" button in the mobile 2nd-level menu section
        .on('click', '.multiple-navigation .go-back', function () {
            $('.second-navigation').removeClass("slide-in");
            $('.first-navigation').removeClass("nudge");
        });


    //END OF CONTENT

});
