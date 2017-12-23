"use strict";

/**
 * Javascript required to build and refresh the Machine/Index page content dynamically,
 * collected into a namespace object to avoid global name clashes.
 */
$.ajaxSetup({
    // Disable caching of AJAX responses
    cache: false
});
var MachineIndex = new function () {

    // Local variables
    var machineId = 0;
    var googleApi = null;
    var chart = null;
    var chartCurrency = null;

    var reportYear = undefined;
    var reportMonth = undefined;
    var loadingCount = 0;
    var showLoadingPopup = false;


    // Views loaded from the page HTML
    var views = {
        collectionContainer: $("#collection-times"),
        floatLevelsSection: $(".float-level-update-fields"),
        updateFloatLevelsButton: $("#apply-float-levels-btn"),
        machineDetails: $(".individual-system-information"),
        manifestTable: $("#components-information-table"),
        bactaTable: $("#components-bacta-table"),
        coinFloatLevels: $(".coin-float-levels"),
        billFloatLevels: $(".bill-float-levels"),
        chartHeadingStartDate: $("#chart-heading-start-date"),
        chartHeadingEndDate: $("#chart-heading-end-date")
    };

    // View templates loaded from the page HTML
    var viewTemplates = {
        // Note: must extract the row template BEFORE the section template
        collectionRow: Tools.extractTemplate("#template__collection-row"),
        collectionDateSection: Tools.extractTemplate("#template__collection-date-section"),
        machineManifestRow: Tools.extractTemplate("#template__machine-manifest-row"),
        bactaManifestRow: Tools.extractTemplate("#template__bacta-manifest-row"),
        floatLevelInput: Tools.extractTemplate("#template__float-level")
    };

    /**
     * Get JSON data from the server and initialise the views
     */
    this.initialise = function (machineIdParam, googleApiParam) {
        machineId = machineIdParam;
        googleApi = googleApiParam;

        //Set up the date picker on the collections list
        DatePicker.setup(
            "#reportrange",
            "#reportrange span",
            function (start, end) {
                getCollectionsData(start, end);
            });

        // Add handler for the Update Float Levels button
        views.updateFloatLevelsButton.click(function () {
            updateFloatLevels();
        });

        // Initialise Google charts (can only be done once)
        // and once that's ready, request the page's data
        googleApi.charts.load(
            "current",
            {
                packages: ["corechart"]
            });
        googleApi.charts.setOnLoadCallback(function () {
            // Request data
            getMachineData();
            getCollectionsData(DatePicker.startDate, DatePicker.endDate);
        });

        initialiseSignalR();

        reportYear = moment().year();
        reportMonth = moment().month() + 1;
        bindMonthsArrowButtons();
        bindDaysArrowButtons();
        bindMonthsSelectionClick();
    };

    function bindMonthsSelectionClick() {
        $(document).on('click touchstart', '#incomePerformanceMonthsGraphContainer .graph-body .bars-outer:not(.disabled)', function () {
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

    function monthsRightArrowButtonClick() {
        if (reportYear < moment().year())
            reportYear++;
        showLoadingPopup = true;
        monthsRefreshView();
        daysRefreshView();
    }

    function monthsLeftArrowButtonClick() {
        reportYear--;
        showLoadingPopup = true;
        monthsRefreshView();
        daysRefreshView();
    }

    function daysRightArrowButtonClick() {
        if (reportMonth < moment().month() + 1)
            reportMonth++;
        daysRefreshView();
    }

    function daysLeftArrowButtonClick() {
        if (reportMonth > 1)
            reportMonth--;
        daysRefreshView();
    }

    function startLoading() {
        if (loadingCount === 0) {
            if (showLoadingPopup)
                Tools.showLoading();
        }

        loadingCount++;
    }

    function finishedLoading() {
        loadingCount--;
        if (loadingCount === 0) {
            if (showLoadingPopup)
                Tools.hideLoading();
        }
    }

    function monthsRefreshView() {
        startLoading();

        $.get("/Report/ReportShared/IncomePerformanceMonthsGraph",
            { year: reportYear, machineId: machineId },
            function (data) {
                $("#incomePerformanceMonthsGraphContainer").html(data);
                reportsRoot.refreshAll();
                bindMonthsArrowButtons();
                finishedLoading();
            }
        );
    }

    function daysRefreshView(selectDaysView, refreshAll) {

        if (refreshAll == null || refreshAll == undefined)
            refreshAll = true;

        startLoading();

        $.get("/Report/ReportShared/IncomePerformanceDaysGraph",
            { year: reportYear, month: reportMonth, machineId: machineId },
            function (data) {
                $("#incomePerformanceDaysGraphContainer").html(data);
                $("#incomePerformanceDaysGraphContainer .graph-body.days").removeClass("hidden");
                if (selectDaysView)
                    reportsRoot.selectIncomePerformanceDays();

                if (refreshAll)
                    reportsRoot.refreshAll();

                bindDaysArrowButtons();
                finishedLoading();
            }
        );
    }

    /**
     * Fetch machine data for the current machine
     */
    function getMachineData() {
        // Load the collections data
        $.get("/Machine/GetMachine/" + machineId,
            function (machine) {
                updateMachineUi(machine);
            });
    };




    /**
     * Fetch collections data for the current machine, optionally bounded by dates.
     * 
     * @param {} from   [Optional] Start date/time for collections to fetch, as a Date object
     * @param {} to     [Optional] End date/time for collections to fetch, as a Date object
     */
    function getCollectionsData(from, to) {
        var fromParam = from ? from.toISOString() : null;
        var toParam = to ? to.toISOString() : null;

        // Load the collections data
        $.get("/Machine/CollectionsData",
            {
                machineId: machineId,
                from: fromParam,
                to: toParam
            },
            function (collectionEventsByDate) {
                addCollectionsToUi(collectionEventsByDate);
                updateChart(collectionEventsByDate);
            });
    };

    /**
     * Update the machine details section
     * 
     * @param {} machine    Machine model object
     * @returns {} 
     */
    function updateMachineUi(machine) {
        // Update the machine details at the top of the page and in the system info dialog
        // (machineDetails is based on a class rather than an ID so matches both)
        views.machineDetails
            .find(".system-name").text(machine.Name).end()
            .find(".site-name").text(machine.Site.Name).end()
            .find(".address").text(machine.Site.FullAddress).end()
            .find(".country").text(machine.Site.Country).end()
            .find(".assigned-collectors").text("TODO").end()
            .find("#machine-status")
                .toggleClass("warning", machine.StatusIsFull)
                .toggleClass("error", machine.StatusIsError)
                .text(machine.StatusDescription)
            .end();

        // Update the float levels UI
        if (machine.Denominations && machine.Denominations.length) {
            $(".float-edit-input-container").remove();

            // Sort by ascending value
            machine.Denominations.sort(function (a, b) {
                return a.Value - b.Value;
            });

            for (var i = 0; i < machine.Denominations.length; i++) {
                var denom = machine.Denominations[i];
                var inputId = "denom-" + denom.Id;
                var floatLevelUi = viewTemplates.floatLevelInput.clone()
                    .find("label").attr("for", inputId).text(Format.asCurrency(denom.Value, denom.Currency)).end()
                    .find(".float-level-update-field").attr("id", inputId).val(denom.FloatLevel).end();

                floatLevelUi.appendTo(denom.Type === "Coin" ? views.coinFloatLevels : views.billFloatLevels);
            }
        }

        // for bacta cash system, display the primary and secondary data
        if (machine.SystemType == "BactaCash") {
            $.get("/Machine/GetBacta/" + machineId,
                function (bacta_machine) {
                    // build table rows
                    $(".bacta-manifest-row").remove();
                    $.each(bacta_machine, function (k, v) {
                        // ignore these elements
                        if (k != "Id" && k != 'Timestamp' && k != 'MachineId') {
                            viewTemplates.bactaManifestRow.clone()
                                .find(".bacta-info-item").text(k).end()
                                .find(".bacta-info-value").text(v).end()
                                .appendTo(views.bactaTable);
                        }
                    });
                });
        }


        // Update the machine manifests UI
        if (machine.Manifests && machine.Manifests.length) {
            $(".machine-manifest-row").remove();
            for (var j = 0; j < machine.Manifests.length; j++) {
                var manifest = machine.Manifests[j];
                viewTemplates.machineManifestRow.clone()
                    .find(".device-code").text(manifest.Device).end()
                    .find(".version").text(manifest.Version).end()
                    .find(".build-revision").html(manifest.BuildRevision || "&mdash;").end()
                    .appendTo(views.manifestTable);
            }
        }

        // Update the map
        updateMap(machine.Site.Latitude, machine.Site.Longitude);
    };

    $.extend(jQuery.fn.dataTableExt.oSort, {
        "date-uk-pre": function (a) {
            return parseInt(moment(a, "DD/MM/YYYY").format("X"), 10);
        },
        "date-uk-desc": function (a, b) {
            return b - a;
        },
        "date-uk-asc": function (a, b) {
            return a - b;
        }
        
    });



    /**
     * Add each collection as a row in the table
     * 
     * @param {} collectionEvents    Collection events for this machine
     * @returns {} 
     */
    function addCollectionsToUi(collectionEventsByDate) {

        // Clear the table out
        $(".collection-date-section").remove();


        var currencyCode = localStorage.getItem("CurrencyCode");
        var totalCollected = 0;
        //var html = '<div class="tebs-collections"><table class="mobile-responsive individual-system-information table" id="tebsCollectionList">';
        //html += '<tr><th>Collected date</th><th>Bag code</th><th>Collector</th><th>Collected</th></tr>';
        //for (var i = 0; i < collectionEventsByDate.length; i++) {

        //    html += '<tr>';
        //    html += '<td>' + Format.asDateAndTime(collectionEventsByDate[i].Timestamp) + '</td>';
        //    html += '<td><a href="/Machine/TebsBag/' + collectionEventsByDate[i].TebsCode +  '">' + collectionEventsByDate[i].TebsCode.replace(/^0+/,'')  + '</a></td>';               
        //    html += '<td>' + parseInt(collectionEventsByDate[i].KeyId).toString() + '</td>';
        //    html += '<td>' + Format.asCurrency(collectionEventsByDate[i].amount,currencyCode, true) + '</td>';
        //    html += '</tr>';

        //}


        //$('#collection-times').html(html);
        //html += '</table></div>';


        //dynamically create data table and push array of data into it.
        var dataSet = [];
        var html = '<table class="mobile-responsive individual-system-information table" id="tebsCollectionList">';
        for (var i = 0; i < collectionEventsByDate.length; i++) {

            dataSet.push([Format.asDateAndTime(collectionEventsByDate[i].Timestamp), '<a href="/Machine/TebsBag/' + collectionEventsByDate[i].TebsCode + '">' + collectionEventsByDate[i].TebsCode.replace(/^0+/, '') + '</a>', parseInt(collectionEventsByDate[i].KeyId).toString(), Format.asCurrency(collectionEventsByDate[i].amount, currencyCode, true)]);
        }
              
        html += '</table>';
        $('#collection-times').html(html);
        $.extend(jQuery.fn.dataTableExt.oSort);
        $('#tebsCollectionList').DataTable({
            "order": [[ 0, "desc" ]],
            data: dataSet,            
            columns: [
                { title: "Collected date" },
                { title: "Bag code" },
                { title: "Collector Id" },
                { title: "Amount collected" }
            ],
            columnDefs: [
              
                { className: "dt-right", targets: 3 },
                 { "type": "date-uk", "targets": 0 }
            ]
        });

    };

    /**
     * Update the float levels for this machine
     */
    function updateFloatLevels() {
        var data = {
            MachineId: machineId,
            Levels: []
        };

        views.floatLevelsSection
            .find("input.float-level-update-field")
            .each(function () {
                data.Levels.push({
                    DenominationId: $(this).prop("id").replace("denom-", ""),
                    Level: $(this).val()
                });
            });

        $.post("/Machine/UpdateFloatLevels", data);
    };

    /**
     * Update the collections bar chart
     * 
     * @param {} collectionsByDate 
     * @returns {} 
     */
    function updateChart(collectionsByDate) {
        // Update the heading
        views.chartHeadingStartDate.text(Format.asDate(DatePicker.startDate));
        views.chartHeadingEndDate.text(Format.asDate(DatePicker.endDate));

        // Create and populate the data table.

        var data = new googleApi.visualization.DataTable();
        data.addColumn("string", "Date");
        data.addColumn("number", "Collected Cash");
        data.addColumn("number", "Refilled Cash");

        var currency = "";

        // Get dates in reverse
        var dates = [];
        for (var d in collectionsByDate) {
            if (collectionsByDate.hasOwnProperty(d)) {
                dates.unshift(d);
            }
        }

        for (var i = 0; i < dates.length; i++) {
            var date = dates[i];
            var collections = collectionsByDate[date];
            var totalCollected = 0;
            var totalRefilled = 0;
            for (var j = 0; j < collections.length; j++) {
                totalCollected += collections[j].TotalCollected;
                totalRefilled += collections[j].TotalRefilled;
            }

            var blah = collections.Country;
            var moreblah = Format.asCurrency(blah);
            currency = chartCurrency || moreblah;
            var dateField = Format.asDate(date);
            data.addRow([
                {
                    v: dateField,
                    f: dateField
                },
                totalCollected,
                totalRefilled
            ]);
        }

        var formatter = new googleApi.visualization.NumberFormat({
            prefix: currency,
            negativeParens: true
        });
        formatter.format(data, 1); // Apply formatter to first column
        formatter.format(data, 2); // Apply formatter to second column

        //chart = chart || new googleApi.visualization.ColumnChart($("#chart_div")[0]);
        //chart.draw(
        //    data, {
        //        vAxis: {
        //            title: ("Total Cash (" + currency + ")")
        //        },
        //        'legend': "top",
        //        colors: [
        //            "#4986a2",
        //            "#c6c6c6"
        //        ]
        //    }
        //);
    };

    /**
     * Update the machine map
     * 
     * @param {} latitude   Latitude of the machine site
     * @param {} longitude  Longitude of the machine site
     */
    function updateMap(latitude, longitude) {
        var isDraggable = $(document).width() > 1000 ? true : false;
        var image = "/Content/img/map/pin.png";

        var mapOptions = {
            center: new googleApi.maps.LatLng(latitude, longitude),
            zoom: 11,
            mapTypeId: googleApi.maps.MapTypeId.ROADMAP,
            scrollwheel: false,
            draggable: isDraggable,
            styles: MapDefaults.styles
        };
        var map = new googleApi.maps.Map(document.getElementById("map"), mapOptions);
        // ReSharper disable once UnusedLocals
        var marker = new googleApi.maps.Marker({
            position: {
                lat: latitude,
                lng: longitude
            },
            map: map,
            title: 212,
            icon: image
        });
    };


    /**
     * Wire up the SignalR hub and implement any event handlers relevant to this page
     */
    function initialiseSignalR() {
        // Initialise the SignalR hub
        var machineHub = $.connection.machineHub;

        /**
         * Machine status is updated
         */
        machineHub.client.updateMachineStatus = function (model) {

            var machine = model.Machine;
            if (machine.Id !== machineId) {
                return;
            }
            updateMachineUi(machine);
        };


        /**
         * Collection is added
         */
        machineHub.client.updateCollection = function () {


            getCollectionsData(DatePicker.startDate, DatePicker.endDate);
            showLoadingPopup = false;
            daysRefreshView();
            monthsRefreshView();

        };

        /**
         * Machine is updated (machine startup event)
         */

        machineHub.client.updateMachine = function (model) {

            if (model.Machine.SystemType == 'CashInTebs' || model.Machine.SystemType == 'CashInBnv') {

                $.get("/Machine/Bag/" + machineId,
                    function (bagData) {

                        // MACHINE DETAILS PAGE
                        var machine = model.Machine;
                        if ($('#' + machine.Id + '-machine-details').length) {

                            var currency = machine.Denominations[0].Currency;

                            // Update date/time
                            $('.last-update')
                                .find('.date').text(machine.Date).end()
                                .find('.time').text(machine.Time).end();

                            for (var i = 0; i < machine.Denominations.length; i++) {
                                var denom = machine.Denominations[i];

                                for (var j = 0; j < bagData.count.length; j++) {
                                    if (denom.ValueInPence == (bagData.count[j].Value * 100)) {
                                        $('.cashbox-level-' + denom.ValueInPence).text(bagData.count[j].Number);
                                        $('.collectable-level-' + denom.ValueInPence).text(bagData.count[j].Number);
                                        $('.total-level-' + denom.ValueInPence).text(bagData.count[j].Number);
                                        break;
                                    }
                                }
                            }

                            // Totals
                            $('.cashbox-value-coins-total').text(Format.asCurrency(machine.CoinsCashboxValue, currency));
                            $('.cashbox-value-bills-total').text(Format.asCurrency(bagData.total[0].Value, currency));
                            $('.cashbox-value-total').text(Format.asCurrency(bagData.total[0].Value, currency));

                            $('.stored-value-coins-total').text(Format.asCurrency(machine.CoinsStoredValue, currency));
                            $('.stored-value-bills-total').text(Format.asCurrency(machine.BillsStoredValue, currency));
                            $('.stored-value-total').text(Format.asCurrency(machine.StoredValue, currency));

                            $('.total-value-coins-total').text(Format.asCurrency(machine.CoinsTotalValue, currency));
                            $('.total-value-bills-total').text(Format.asCurrency(bagData.total[0].Value, currency));
                            $('.total-value-total').text(Format.asCurrency(bagData.total[0].Value, currency));

                            $('.float-value-coins-total').text(Format.asCurrency(machine.CoinsFloatValue, currency));
                            $('.float-value-bills-total').text(Format.asCurrency(machine.BillsFloatValue, currency));
                            $('.float-value-total').text(Format.asCurrency(machine.FloatValue, currency));

                            $('.collectable-value-coins-total').text(Format.asCurrency(machine.CoinsCollectableValue, currency));
                            $('.collectable-value-bills-total').text(Format.asCurrency(bagData.total[0].Value, currency));
                            $('.collectable-value-total').text(Format.asCurrency(bagData.total[0].Value, currency));
                        }
                    });
            } else {
                // MACHINE DETAILS PAGE
                var machine = model.Machine;
                if ($('#' + machine.Id + '-machine-details').length) {
                    var currency = machine.Denominations[0].Currency;
                    // Update date/time
                    $('.last-update')
                        .find('.date').text(machine.Date).end()
                        .find('.time').text(machine.Time).end();

                    for (var i = 0; i < machine.Denominations.length; i++) {
                        var denom = machine.Denominations[i];
                        $('.stored-level-' + denom.ValueInPence).text(denom.StoredLevel);
                        $('.cashbox-level-' + denom.ValueInPence).text(denom.CashboxLevel);
                        $('.float-level-' + denom.ValueInPence).text(denom.FloatLevel);
                        $('.collectable-level-' + denom.ValueInPence).text(denom.CollectableLevel);
                        $('.total-level-' + denom.ValueInPence).text(denom.TotalLevel);
                    }

                    // Totals
                    $('.cashbox-value-coins-total').text(Format.asCurrency(machine.CoinsCashboxValue, currency));
                    $('.cashbox-value-bills-total').text(Format.asCurrency(machine.BillsCashboxValue, currency));
                    $('.cashbox-value-total').text(Format.asCurrency(machine.CashboxValue, currency));

                    $('.stored-value-coins-total').text(Format.asCurrency(machine.CoinsStoredValue, currency));
                    $('.stored-value-bills-total').text(Format.asCurrency(machine.BillsStoredValue, currency));
                    $('.stored-value-total').text(Format.asCurrency(machine.StoredValue, currency));

                    $('.total-value-coins-total').text(Format.asCurrency(machine.CoinsTotalValue, currency));
                    $('.total-value-bills-total').text(Format.asCurrency(machine.BillsTotalValue, currency));
                    $('.total-value-total').text(Format.asCurrency(machine.TotalValue, currency));

                    $('.float-value-coins-total').text(Format.asCurrency(machine.CoinsFloatValue, currency));
                    $('.float-value-bills-total').text(Format.asCurrency(machine.BillsFloatValue, currency));
                    $('.float-value-total').text(Format.asCurrency(machine.FloatValue, currency));

                    $('.collectable-value-coins-total').text(Format.asCurrency(machine.CoinsCollectableValue, currency));
                    $('.collectable-value-bills-total').text(Format.asCurrency(machine.BillsCollectableValue, currency));
                    $('.collectable-value-total').text(Format.asCurrency(machine.CollectableValue, currency));
                }
            }
        };

        $.connection.hub.start().done();
    };



}();