"use strict";

/**
 * Javascript required to build and refresh the Report "Offline" page content dynamically,
 * collected into a namespace object to avoid global name clashes.
 */
var ReportOffline = new function () {

    var rangeStart = "";
    var rangeEnd = "";
    var hourViewCurrentDate = "";
    var dateFormat = "ddd DD/MM/YYYY";
    var serverDateFormat = "YYYY-MM-DD";
    var loadingCount = 0;

    this.initialise = function () {

        $(".search-box")
            .on("change keyup",
                function () {
                    filterSearch($(this).val().trim());
            })
        .on("cut paste",
            function () {
              setTimeout(function () {
                  $(".search-box").keyup();
              }, 50);
            });

        // Set up the date picker on the collections list
        DatePicker.setup(
            "#reportrange",
            "#reportrange span",
            function(start, end) { dateRangeSelected(start, end) },
            "Last 7 Days");

        rangeStart = moment().startOf("day").subtract(6, "day");
        rangeEnd = moment().endOf("day");
        hourViewCurrentDate = rangeStart;
        hourViewRefreshDateCaption();

        $("#hourViewBackOneDay").bind("click", hourViewBackOneDay);
        $("#hourViewForwardOneDay").bind("click", hourViewForwardOneDay);

        $(".download-report-btn").bind("click", downloadCsv)
    };


    /**
     * Filter the machines list when the user types in the search field
     * @param {} searchTerm     Search text entered by the user
     */
    function filterSearch(searchTerm) {

        if (!searchTerm) {
            searchTerm = $(".search-box").val().trim();
        }

        $("#machineDetails .report-row").each(function () {
            filterMachines(this, searchTerm);
        });
    }

    function filterMachines(current, searchTerm) {
        var matchesFilter = ($(current).text().toLowerCase().indexOf(searchTerm.toLowerCase()) > -1);

        var id = $(current).attr("data-id");
        $("#daysViewContainer .report-row[data-id=" + id + "]").toggle(matchesFilter);
        $("#hourViewContainer .report-row[data-id=" + id + "]").toggle(matchesFilter);
        $("#hourTotalViewContainer .report-row[data-id=" + id + "]").toggle(matchesFilter);

        $(current).toggle(matchesFilter);
        if (!matchesFilter) {
            $(current).find('.select').removeClass('ui-selected');
        }
    }

    function downloadCsv()
    {
        window.location.assign(window.location.href + "/DownloadCsv?" + $.param({
            start: rangeStart.format(serverDateFormat),
            end: rangeEnd.format(serverDateFormat)
        }));
    }

    function dateRangeSelected(start, end) {
        rangeStart = moment(start).startOf("day");
        rangeEnd = moment(end).endOf("day");
        hourViewCurrentDate = rangeStart;
        hourViewRefreshView();
        hourTotalRefreshView();
        dayViewRefreshView();
    }

    function hourViewBackOneDay() {

        hourViewCurrentDate = moment(hourViewCurrentDate).subtract(1, "day");
        if (hourViewCurrentDate < moment(rangeStart)) {
            hourViewCurrentDate = rangeStart;
            return;
        }

        hourViewRefreshView();
    }
    function hourViewForwardOneDay() {

        hourViewCurrentDate = moment(hourViewCurrentDate).add(1, "day");
        var beginningOfLastDayInRange = moment(rangeEnd).startOf("day");
        if (hourViewCurrentDate > beginningOfLastDayInRange)
        {
            hourViewCurrentDate = beginningOfLastDayInRange;
            return;
        }
        hourViewRefreshView();
    }

    function hourViewRefreshDateCaption()
    {
        $("#hourViewDateRangeDetail").text(moment(hourViewCurrentDate).format(dateFormat));
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


    function hourViewRefreshView()
    {
        startLoading();
        $.get("/Report/Offline/Hours", 
            { viewDate: moment(hourViewCurrentDate).format(serverDateFormat) },
            function (data) {
                hourViewRefreshDateCaption();
                $("#hourViewContainer").html(data);
                filterSearch();
                finishedLoading();
            }
        );
    }

    function hourTotalRefreshView()
    {
        startLoading();
        $.get("/Report/Offline/HoursTotal",
            { start: moment(rangeStart).format(serverDateFormat), end: moment(rangeEnd).format(serverDateFormat) },
            function (data) {
                $("#hourTotalViewContainer").html(data);
                filterSearch();
                finishedLoading();
            }
        );
    }

    function dayViewRefreshView()
    {
        startLoading();
        $.get("/Report/Offline/Days",
            { start: moment(rangeStart).format(serverDateFormat), end: moment(rangeEnd).format(serverDateFormat) },
            function (data) {
                $("#daysViewContainer").html(data);
                filterSearch();
                finishedLoading();
            }
        );
    }
}();