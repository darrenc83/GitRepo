




$(document).ready(function(){
  'use strict';
  
  
  
  
  $(document).ready(function() {
    // run test on initial page load
    checkSize();

    // run test on resize of the window
    $(window).resize(checkSize);
});

//Function to the css rule
function checkSize(){
    if ($("#overall-wrapper").css("padding") === "0px" ){
        // your code here
		
		
		
	  

	$(function(){
	  // Bind the swipeleftHandler callback function to the swipe event on div.box
	  $( "div.box" ).on( "swipeleft", swipeleftHandler );
	 
	  // Callback function references the event target and adds the 'swipeleft' class to it
	  function swipeleftHandler( event ){
		$( event.target ).addClass( "swipeleft" );
	  }
	});
	
	
	$(function(){
	  // Bind the swipeleftHandler callback function to the swipe event on div.box
	  $( "#overall-wrapper" ).on( "swiperight", swiperightHandler );
	 
	  // Callback function references the event target and adds the 'swipeleft' class to it
	  function swiperightHandler( event ){
		$( event.target ).addClass( "swipeleft" );
		$( "#overall-wrapper" ).addClass('active');
		$('html').addClass('menu-open');
		$('#hamburger').addClass('open');
	
	  }
	  
	  $( "#overall-wrapper" ).on( "swipeleft", swipeleftHandler );
	 
	  // Callback function references the event target and adds the 'swipeleft' class to it
	  function swipeleftHandler( event ){
		$( event.target ).addClass( "swipeleft" );
		$( "#overall-wrapper" ).removeClass('active');
		$('#hamburger').removeClass('open');
		window.setTimeout(function(){$("html").removeClass("menu-open");}, 300);
	
	  }
	  
	});	
		
		
		
		
		
		
		
    }
}

  
  



 //END OF CONTENT
  
});