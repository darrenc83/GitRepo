﻿"use strict";

/**
 * Javascript required to build and refresh the Report "Offline" page content dynamically,
 * collected into a namespace object to avoid global name clashes.
 */
var ReportHopper = new function () {

    var rangeStart = "";
    var rangeEnd = "";
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
            function (start, end) {
                dateRangeSelected(start, end);
            },
            "Last 7 Days");

        rangeStart = moment().startOf("day").subtract(6, "day");
        rangeEnd = moment().endOf("day");

        $(".download-report-btn").bind("click", downloadCsv)
    };

    /**
     * Filter the machines list when the user types in the search field
     * @param {} searchTerm     Search text entered by the user
     */
    function filterSearch(searchTerm) {

        if (!searchTerm)
        {
            searchTerm = $(".search-box").val().trim();
        }

        $("#machineDetails .report-row").each(function () {
            filterMachines(this, searchTerm);
        });
    }
    
    function filterMachines(current, searchTerm)
    {
        var matchesFilter = ($(current).text().toLowerCase().indexOf(searchTerm.toLowerCase()) > -1);

        var id = $(current).attr("data-id");
        $("#summaryContainer .report-row[data-id=" + id +"]").toggle(matchesFilter);
        $("#minimimLevelBreakdownContainer .report-row[data-id=" + id + "]").toggle(matchesFilter);
        $(current).toggle(matchesFilter);
        if (!matchesFilter) {
            $(current).find('.select').removeClass('ui-selected');
        }
    }

    function downloadCsv() {
        window.location.assign(window.location.href + "/DownloadCsv?" + $.param({
            start: rangeStart.format(serverDateFormat),
            end: rangeEnd.format(serverDateFormat)
        }));
    }

    function dateRangeSelected(start, end)
    {
        rangeStart = moment(start).startOf("day");
        rangeEnd = moment(end).endOf("day");
        minimumLevelBreakdownRefreshView();
        summaryRefreshView();
    }

    function startLoading()
    {
        if (loadingCount === 0)
        {
            Tools.showLoading();
        }

        loadingCount++;
    }

    function finishedLoading()
    {
        loadingCount--;
        if(loadingCount === 0)
        {
            Tools.hideLoading();
        }
    }

    function minimumLevelBreakdownRefreshView()
    {
        startLoading();
        $.get("/Report/Hopper/MinimumLevelBreakdown",
            { start: moment(rangeStart).format(serverDateFormat), end: moment(rangeEnd).format(serverDateFormat) },
            function (data) {
                $("#minimimLevelBreakdownContainer").html(data);
                filterSearch();
                finishedLoading();
            }
        );
    }

    function summaryRefreshView()
    {
        startLoading();
        $.get("/Report/Hopper/Summary",
            { start: moment(rangeStart).format(serverDateFormat), end: moment(rangeEnd).format(serverDateFormat) },
            function (data) {
                $("#summaryContainer").html(data);
                filterSearch();
                finishedLoading();
            }
        );
    }
}();