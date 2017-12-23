"use strict";

/**
 * Javascript required to build and refresh the Report "Offline" page content dynamically,
 * collected into a namespace object to avoid global name clashes.
 */
var ReportRoot = new function () {

    var reportYear = undefined;
    var reportMonth = undefined;
    var dateFormat = "ddd DD/MM/YYYY";
    var loadingCount = 0;
   
    this.initialise = function () {
        reportYear = moment().year();
        reportMonth = moment().month() + 1;
        bindMonthsArrowButtons();
        bindDaysArrowButtons();
        bindMonthsSelectionClick();
    };

    function bindMonthsSelectionClick()
    {
        $(document).on('click touchstart', '#incomePerformanceMonthsGraphContainer .graph-body .bars-outer:not(.disabled)', function() {
            reportMonth = $(this).attr("data-month");
            daysRefreshView(false, false);
	    });
    }

    function bindMonthsArrowButtons() {
        $("#monthsLeftArrowButton").bind("click", monthsLeftArrowButtonClick);
        $("#monthsRightArrowButton").bind("click", monthsRightArrowButtonClick);
    }

    function bindDaysArrowButtons() {
        $("#daysLeftArrowButton").bind("click", daysLeftArrowButtonClick);
        $("#daysRightArrowButton").bind("click", daysRightArrowButtonClick);
    }

    function monthsRightArrowButtonClick()
    {
        if (reportYear < moment().year())
            reportYear++;
        monthsRefreshView();
        daysRefreshView();
    }

    function monthsLeftArrowButtonClick()
    {
        reportYear--;
        monthsRefreshView();
        daysRefreshView();
    }

    function daysRightArrowButtonClick() {
        if (reportMonth < moment().month() + 1)
            reportMonth++;
        daysRefreshView();
    }

    function daysLeftArrowButtonClick() {
        if(reportMonth > 1)
            reportMonth--;
        daysRefreshView();
    }

    function startLoading() {
        if (loadingCount === 0) {
            Tools.showLoading();
        }

        loadingCount++;
    }

    function finishedLoading() {
        loadingCount--;
        if (loadingCount === 0) {
            Tools.hideLoading();
        }
    }

    function monthsRefreshView()
    {
        startLoading();

        $.get("/Report/ReportShared/IncomePerformanceMonthsGraph",
            { year: reportYear },
            function (data) {
                $("#incomePerformanceMonthsGraphContainer").html(data);
                reportsRoot.refreshAll();
                bindMonthsArrowButtons();
                bindMonthsSelectionClick();
                finishedLoading();
            }
        );
    }

    function daysRefreshView(selectDaysView, refreshAll) {

        if (refreshAll == null || refreshAll == undefined)
            refreshAll = true;

        startLoading();

        $.get("/Report/ReportShared/IncomePerformanceDaysGraph",
            { year: reportYear, month: reportMonth },
            function (data) {
                $("#incomePerformanceDaysGraphContainer").html(data);
                $("#incomePerformanceDaysGraphContainer .graph-body.days").removeClass("hidden");
                if (selectDaysView)
                    reportsRoot.selectIncomePerformanceDays();

                if(refreshAll)
                    reportsRoot.refreshAll();

                bindDaysArrowButtons();
                finishedLoading();
            }
        );
    }

}();