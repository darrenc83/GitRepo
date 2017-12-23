



$(document).ready(function(){
  'use strict';

    //THIS IS LOOKING AT FILTERING OPTIONS BASED ON KEYWORDS. The rel on the checkboxes makes sure that it does not trigger out of the class//
    //this could be useful to use for individual column filtering//

    $(".filter-checkboxes input:checkbox").click(function () {
        var showAll = true;
        $('tr').not('.first').hide();
        $('input[type=checkbox]').each(function () {
            if ($(this)[0].checked) {
                showAll = false;
                var status = $(this).attr('rel');
                var value = $(this).val();
 
                $('td.' + status + '[rel="' + value + '"]').parent('tr').show();
            }
        });
        if(showAll){
            $('tr').show();
        }
    });

    //THIS IS THE FUNCTION TO MAKE THE TABLES SORTABLE
	
    $(function() {
        try {
		    $(".tablesorter").tablesorter({
                debug: true,
                selectorHeaders: 'thead th.sortable, thead.sortable-all th'
            })
	        /// $("a.append").click(appendData);
	    } catch (e)
    { }
	});
	


	
 
 //END OF CONTENT
  
});
