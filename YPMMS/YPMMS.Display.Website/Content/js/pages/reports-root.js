

var reportsRoot = {};

$(document).ready(function(){
  'use strict';
  var currencyCode = localStorage.getItem("CurrencyCode");

    //THIS IS TO MAKE SURE THAT THE CHECKBOXES AND LABELS MATCH UP
  $('.long-list-non-selectable-table tr').each(function () {
      var number = $(this).index() + 1;
      $(this).find('.add-to-compare input').attr('id', "cb_" + number);
      $(this).find('.add-to-compare label').attr('for', "cb_" + number);
  });


    //THIS IS for the best performance and worst performance button tabs////////////

  $(document).on('click', '.toggle-buttons .best-btn-selector', function () {
      $(this).siblings().removeClass('selected');
      $(this).addClass('selected');
      $(this).closest('.toggle-buttons').siblings('.worst').addClass('hidden');
      $(this).closest('.toggle-buttons').siblings('.best').removeClass('hidden');
  });
  $(document).on('click', '.toggle-buttons .worst-btn-selector', function () {
      $(this).siblings().removeClass('selected');
      $(this).addClass('selected');
      $(this).closest('.toggle-buttons').siblings('.best').addClass('hidden');
      $(this).closest('.toggle-buttons').siblings('.worst').removeClass('hidden');
  });


  $(document).on('click', '.expand-close-btn', function () {
      var Content = $(this).closest('.report-summary-ranking').find('.ranking-content');
      if (Content.is(":visible")) {
          $(this).children('.expand-icon').removeClass('minus');
          Content.slideUp();
      }
      else {
          $(this).children('.expand-icon').addClass('minus');
          Content.slideDown();
      }
  });

//THIS IS FOR THE REPORTS GRAPH////////////


    //begins the function on load
  function refreshAll()
  {
    createGraph();
    selectedBarInformation();
    getTotals();
    dailyorMonthly();
  }
  reportsRoot.refreshAll = refreshAll;

  refreshAll();

  function selectIncomePerformanceDays() 
  {
	$(".days").addClass('selected').siblings('a').removeClass('selected');
	$('.graph-container.month-view').addClass('hidden');
	$('.graph-container.day-view').removeClass('hidden');
  }

  reportsRoot.selectIncomePerformanceDays = selectIncomePerformanceDays;

$(document).on('click', '.time-selection-buttons .days',function() {

    selectIncomePerformanceDays();
	
});


$(document).on('click', '.time-selection-buttons .months', function () {
    $(this).addClass('selected').siblings('a').removeClass('selected');
    $('.graph-container').addClass('hidden');
    $('.graph-container.month-view').removeClass('hidden');
});

$(document).on('click touchstart', '.graph-body .bars-outer:not(.disabled)', function () {
    $(this).addClass('selected').siblings().removeClass('selected');
});

    //this is adding a disabled class onto any bar that doesn't have an data attribute for totals
$('.bars-outer').each(function () {
    var bob = $(this).children('.fill');
    if (!bob.attr('data-total-money')) {
        $(this).addClass('disabled');
    }
});

    //this is getting the information about the currently selected bar to show the correct info in the bottom display section //////////////////
function selectedBarInformation() {
    var selectedMoneyAmount = $('.graph-container:not(.hidden) .bars-outer.selected .fill').data('total-money');
    var month = $('.graph-container:not(.hidden) .bars-outer.selected .fill').data('month') || $('.graph-body.days').data('month-belong');
    var year = $('.graph-container:not(.hidden) .graph-body').data('year');

    //selectedMoneyAmount = Format.asCurrency(selectedMoneyAmount, currencyCode, false);

    $('.graph-information .total-amount').html(selectedMoneyAmount);
    $('.graph-information .date-title').html(month + ' ' + year);

    if (selectedMoneyAmount == null) {
        $('.total-amount').html('- -');
        //$('.date-title').html('- -');
    }

    //$('.graph-body:not(.hidden).months .title div').text(year);
    //$('.graph-body:not(.hidden).days .title div').text(month);
}




    //this is taking all the monthly money figures and calculating all of them to give a yearly figure.

function getTotals() {
    var yearlyTotal = 0.0;
    $('.graph-body.months .fill[data-total-money]').each(function () {

        var valuesToCalculate = $(this).data('total-money');

        yearlyTotal += parseFloat(valuesToCalculate);

        $(this).closest('.graph-container').siblings('.graph-information').find('.yearly-total-title').html(Format.asCurrency(yearlyTotal, currencyCode,false));
    });
}




    //this function is using all the data provided to create the graph. eg. the Y axis labels, the bars etc.
function createGraph() {

    //this is used the get the values from the attribute and then used to calculate the highest value
    var getFigures = $(".graph-container:not(.hidden) .fill").map(function () {
        return $(this).data('total-money');
    }).get();

    var highestTotal = Math.max.apply(Math, getFigures);
    var highestTotalRounded = Math.round(highestTotal / 10) * 10;
    var calc = highestTotal / 5;
    //this is making sure that there are no excessive decimals being shown on the labels
    var nodecimals = calc | 0; // => 1

    console.log(nodecimals + " nodecimals");
    console.log(calc + " calc");


    //this is telling each bar what value should be inside
    var bartop = highestTotalRounded;
    var bar2 = highestTotalRounded - (nodecimals);
    var bar3 = highestTotalRounded - (nodecimals * 2);
    var bar4 = highestTotalRounded - (nodecimals * 3);
    var bar5 = highestTotalRounded - (nodecimals * 4);
    var barbottom = 0

    $('.graph-body').each(function () {
        var labels = [bartop, bar2, bar3, bar4, bar5, barbottom];
        $(this).find('.y-axis .amount').each(function (i) {
            $(this).html('<small> ' + Format.asCurrency("", currencyCode, false) + '</small>' + labels[i]);
        });
    });

    //this is making sure that the bars get the highest value and then the money is figured out as a percentage to generate the bars
    $('.graph-container:not(.hidden) .fill').each(function () {
        var money = $(this).attr("data-total-money");
        var division = money / highestTotal * 100 + "%";

        $(this).height(0).animate({
            height: division
        }, 400);
    });
    //this handles what happens when you hover over the bars - this is displaying the information of the bar that is hovered in the bottom display./// 
    $(document).on('mouseenter', '.graph-container:not(.hidden) .bars-outer:not(.disabled)', function () {
        var $thisChild = $(this).children('.fill');
        var displayMoney =  $thisChild.attr("data-total-money");
        var thisMonth = $thisChild.data('month') || $(this).closest('.graph-body').data('month-belong');
        var thisYear = $(this).closest('.graph-body').data('year');
        var thisDay = $thisChild.data('day');

        selectedBarInformation();

        if ($('.graph-container:not(.hidden) .graph-body').hasClass('days')) {
            $('.graph-information .date-title').html(thisDay + ' ' + thisMonth + ' ' + thisYear);
        }
        else {
            $('.graph-information .date-title').html(thisMonth + ' ' + thisYear);
        }

        //this is saying that the bar doesn't have a data attrubute then do thos
        if (displayMoney == null) {
            $('.total-amount').html('- -');
            //$('.date-title').html('- -');
        }
        else {
            $('.total-amount').html(Format.asCurrency(displayMoney, currencyCode,false));
        }
    });
    //when mouse leaves the bar this is making sure it should revert back to display the currently selected bar's information in the bottom section////
    $(document).on('mouseleave', '.graph-container:not(.hidden)', function () {
        selectedBarInformation();
    });
}

$(document).on('click', '.time-selection-buttons a', function () {
    createGraph();
    selectedBarInformation();
    getTotals()
    dailyorMonthly();

})

function dailyorMonthly() {
    if (!$('.graph-container:not(.hidden)').children('.graph-body').hasClass('months')) {
        $('.income-for-date-info p').html("Daily Income Total");
    }
    else {
        $('.income-for-date-info p').html("Monthly Income Total");
    }
}



    //END OF CONTENT

});
