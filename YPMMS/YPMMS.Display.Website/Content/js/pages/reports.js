
$(document).ready(function () {
    'use strict';

    toggleShadowRight();
    toggleShadowMachineList();
    controlTheShadows();





    //$('.header-inside li').click(function () {
    //    $('.reportDropdown').hide();
    //    $(this).children('.reportDropdown').show();
    //});

    /*end filter scripts*/


    $(document).on('mouseenter', 'body', function() {
        controlTheShadows();
        toggleShadowRight();
    });

    $('.switch-view').click(function () {

        if (!$(this).hasClass('disabled')) {
            var switchView = $(this).attr("data-switch-view");
            $(this).addClass('selected').siblings().removeClass('selected');
            console.log(switchView);
            $('.reports-details-header').addClass('hidden');
            $('.view').addClass('hidden').removeClass('active');
            $('.' + switchView).removeClass('hidden').addClass('active');
            toggleShadowRight();
        }
    });

    $('body').click(function () {
        if ($('#reports-systems-24-hours .view2').hasClass('active')) {
            $('.time-view').addClass('selected');
        }
        else {
            $('.time-view').removeClass('selected').siblings().addClass('selected');
        }
    });


    $('#reports-systems-24-hours .block-view').click(function () {
        $(this).addClass('selected').siblings().removeClass('selected');
        $('.view').addClass('hidden').removeClass('active');
        $('.week-view').removeClass('hidden').addClass('active');
        toggleShadowRight();

    });

    $('#reports-systems-24-hours .time-view').click(function () {
        $(this).addClass('selected').siblings().removeClass('selected');
        $('.view').addClass('hidden').removeClass('active');
        $('.view2').removeClass('hidden').addClass('active');
        toggleShadowRight();
    });



    ////////////////////////////////////////////////////////////////////////////////////////
    ///////////    REPORTS
    ////////////////////////////////////////////////////////////////////////////////////////  



    //THIS IS THE VIEW SECTION THAT CHANGES WHETHER YOU SEE THE INFORMATION AS TIME OR NUMBERS
    //$('.reports-details-header .view-change .time-view').click(function(){
    //	$(this).addClass('selected').siblings().removeClass('selected');
    //	$('#reports-systems-24-hours').removeClass('second-view');
    //});
    //$('.reports-details-header .view-change .block-view').click(function(){
    //	$(this).addClass('selected').siblings().removeClass('selected');
    //	$('#reports-systems-24-hours').addClass('second-view');
    //});

    //THIS IS FOR THE REPORTS - to make the information in the reports table to scroll left and right when clicked

    $(document).on('click', '.right-arrow, .pillars.right', function () {
        var container = $(this).closest('.actual-information');
        scrollContainer(container, 'right');
        controlTheShadows()
    });


    $(document).on('click', '.left-arrow, .pillars.left', function () {
        var container = $(this).closest('.actual-information');
        scrollContainer(container, 'left');
        controlTheShadows()
    });

    function scrollContainer(containerToScroll, direction) {

        // Do nothing if the container's already scrolling
        if (containerToScroll.is(':animated')) {
            return;
        }

        var currentPosition = containerToScroll.scrollLeft();
        var newPosition;

        if (direction === 'left') {
            newPosition = currentPosition - 200;
        }
        else if (direction === 'right') {
            newPosition = currentPosition + 200;
        }
        else {
            // direction is some other value we don't support, so just do nothing
            //console.log("WARNING: direction parameter must be 'left' or 'right': '" + direction + "' is not valid");
            return;
        }

        containerToScroll.animate({
            scrollLeft: newPosition
        }, 400);
    }


    //CALCULATE WHETHER THE HEADER IS LESS THAN THE CONTAINER BLOCK - this is to determine if there will be an overscroll
    function toggleShadowRight() {
        var headerContainerWidth = $('.right-block:not(.hidden) .actual-information').width();
        var headerWidth = $('.right-block:not(.hidden) .actual-information .header-inside').width();
        //console.log(headerContainerWidth + "width");
        //console.log(headerWidth + "inside");
        $('.right-block:not(.hidden) .shadow.right').toggle(headerWidth > headerContainerWidth);
        $('.right-block:not(.hidden) .right-arrow').toggle(headerWidth > headerContainerWidth);
    }

    //CALCULATE WHETHER THE HEADER IS LESS THAN THE CONTAINER BLOCK - THIS IS ON THE MACHINES LIST - LEFT BLOCK 
    //*dont confuse with the left pillar on each block
    function toggleShadowMachineList() {
        var headerContainerWidth = $('.left-block .actual-information').width();
        var headerWidth = $('.left-block .actual-information .header-inside').width();
        //console.log(headerContainerWidth + "width");
        //console.log(headerWidth + "inside");

        $('.left-block .shadow.right').toggle(headerWidth > headerContainerWidth);
    }



    //This is used for calculating where the content is scrolled to so it can hide and show arrows and shadows.  ****///
    function controlTheShadows() {
        $('.actual-information').each(function () {
            $(this).scroll(function () {

                var content = $(this).find('.header-inside').width()
                var scrollEnd = content - $(this).width();

                //console.log(scrollEnd + " calculate how much scroll is needed");

                //this is saying how much the div scrolls left to the end
                var scrollDetect = $(this).scrollLeft()
                //console.log(scrollDetect);


                if (scrollDetect == '0') {
                    //$(this).css("background", "red");
                    $(this).find('.left-arrow').fadeOut(200);
                    $(this).find('.shadow.left').fadeOut(200);
                }
                else {
                    //$(this).css("background", "blue");
                    $(this).find('.left-arrow').fadeIn(200);
                    $(this).find('.shadow.left').fadeIn(200);
                }

                if (scrollDetect == scrollEnd) {
                    //$(this).css("background", "blanchedalmond");
                    $(this).find('.right-arrow').fadeOut(200);
                    $(this).find('.shadow.right').fadeOut(200);
                }
                else {
                    $(this).find('.right-arrow').fadeIn(200);
                    $(this).find('.shadow.right').fadeIn(200);
                }

            });
        });
    }

    //THIS IS FOR THE REPORTS - IT MAKES SURE THE HEADERS ON THE TABLE STICK TO THE TOP WHEN THEY ARE SCROLLED ////////////
    $(window).scroll(function () {

        var range = 0
        var height = $('.main-header').outerHeight(); // this gets the height of the main header and will be used to make sure that the sticky div stops below this rather than at very top of the window

        var stick = $(this).scrollTop() + 0
        var div_top = $('.actual-information').offset().top;
        var topOfWindow = $(window).scrollTop();
        var calc = (topOfWindow - div_top + range);
        //console.log(topOfWindow);
        //console.log(div_top + "i am the div at the top");
        //console.log(calc + "calc");
        //console.log(stick + "what is stick");

        if (calc > 1) {


            $('.fix-this-at-top').css({
                'top': stick - div_top + height
            });
        }
        else {

            $('.fix-this-at-top').css({
                'top': 0
            });
        }
    });




    //END OF CONTENT

});
