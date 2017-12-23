"use strict";

/**
 * Javascript required to build and refresh the Manager/RefillSystem page content dynamically,
 * collected into a namespace object to avoid global name clashes.
 */
var ManagerRefillSystem = new function (){

    // Local variable
    var machineId = 0;

    // Views loaded from the page HTML
    var views = {
        managerRefillsContainer: $("#collection-times"),
        refillStatusView: $("#collector-refill-system"),
        noRefillPlaceholderView: $("#collection-status"),
        coinsRefillSection: $(".currency-column.coins"),
        billsRefillSection: $(".currency-column.bills"),
        coinsRefillTotal: $(".currency-column.coins .total"),
        billsRefillTotal: $(".currency-column.bills .total")
    };

    // View templates loaded from the page HTML
    var viewTemplates = {
        // Note: must extract the row template BEFORE the section template
        managerRefillRow: Tools.extractTemplate("#template__collection-row"),
        managerRefillDateSection: Tools.extractTemplate("#template__collection-date-section"),
        refillDenomination: Tools.extractTemplate("#template__refill-denom")
    };

    /**
     * Get JSON data from the server and initialise the views
     */
    this.initialise = function(machineIdParam) {
        machineId = machineIdParam;

        // Set up the date picker on the collections list
        DatePicker.setup(
            "#reportrange",
            "#reportrange span",
            function(start, end) {
                getManagerRefillsData(start, end);
            });

        getManagerRefillsData();
        getMachineData();
        initialiseSignalR();
    };

    /**
        * Fetch machine data for the current machine
        */
    function getMachineData() {
        // Load the collections data
        $.get("/Machine/GetMachine/" + machineId,
            function(machine) {
                updateRefillView(machine);
            });
    };
    
    /**
     * Fetch manager refills data for the current machine, optionally bounded by dates.
     * 
     * @param {} from   [Optional] Start date/time for refills to fetch, as a Date object
     * @param {} to     [Optional] End date/time for refills to fetch, as a Date object
     */
    function getManagerRefillsData(from, to) {
        var fromParam = from ? from.toISOString() : null;
        var toParam = to ? to.toISOString() : null;



        // Load the collections data
        $.get("/Manager/RefillsData",
            {
                machineId: machineId,
                from: fromParam,
                to: toParam
            },
            function (refillsByDate) {
                addManagerRefillsToUi(refillsByDate);
            });
    };

    /**
     * Add the manager refills to the table, grouped by date
     * 
     * @param {} refillsByDate    Manager refill events for this machine, grouped by date
     */
    function addManagerRefillsToUi(refillsByDate) {

        // Clear the table out
        $(".collection-date-section").remove();

        // Loop around the dates in the collections data, creating a header row for each date
        for (var date in refillsByDate) {
            if (refillsByDate.hasOwnProperty(date)) {
                var totalRefilled = 0;
                var currency = "";
                var dateSection = viewTemplates.managerRefillDateSection.clone();
                dateSection.find(".day-date").text(Format.asDate(date));

                // Loop around the collections for the current date
                for (var i = 0; i < refillsByDate[date].length; i++) {
                    var collectionEvent = refillsByDate[date][i];
                    totalRefilled += collectionEvent.TotalRefilled;
                    currency = collectionEvent.Currency;

                    var managerRefillRow = viewTemplates.managerRefillRow.clone();
                    managerRefillRow.find("a").attr("href", "/Collection/" + collectionEvent.Id).text(Format.asTime(collectionEvent.Timestamp));
                    managerRefillRow.find(".refill").text(Format.asCurrency(collectionEvent.TotalRefilled, collectionEvent.Currency, true));
                    managerRefillRow.toggleClass("error", collectionEvent.IsError);
                    dateSection.append(managerRefillRow);
                }

                dateSection.find(".full-day-refilled").text(Format.asCurrency(totalRefilled, currency, true));
                views.managerRefillsContainer.append(dateSection);
            }
        }
    };
    
    /**
     * Update the refill table view
     * 
     * @param {} machine    Machine object with current denomination levels
     */
    function updateRefillView(machine) {
        machine = machine || null;

        // Show the correct view, depending on whether this collector has a current machine
        views.refillStatusView.toggleClass("hidden", (machine == null));
        views.noRefillPlaceholderView.toggleClass("hidden", (machine != null));
        if (!machine) {
            return;
        }

        machine.Denominations.sort(function (a, b) {
            return a.Value - b.Value;
        });

        $(".refill-denom").remove();

        for (var i = 0; i < machine.Denominations.length; i++) {
            var denom = machine.Denominations[i];
            var cssClass = (denom.IsOver ? "overfill" : (denom.IsUnder ? "due" : "disabled"));
            // Negative values (i.e. refill required) are shown without a minus sign (i.e. number of coins/bills still needed).
            // Positive values (i.e. level already over the float) are shown with a plus sign.
            var floatVariance = (denom.IsOver ? "+" + denom.FloatVarianceLevel : Math.abs(denom.FloatVarianceLevel));
            viewTemplates.refillDenomination.clone()
                .addClass(cssClass)
                .find(".denom-value").text(Format.asCurrency(denom.Value, denom.Currency)).end()
                .find(".float-variance").text(floatVariance).end()
                .appendTo(denom.Type === "Coin" ? views.coinsRefillSection : views.billsRefillSection);
        }
        views.billsRefillTotal.text(Format.asCurrency(machine.BillsRefillDueValue, machine.Currency));
        views.coinsRefillTotal.text(Format.asCurrency(machine.CoinsRefillDueValue, machine.Currency));
    };

    /**
     * Wire up the SignalR hub and implement any event handlers relevant to this page
     */
    function initialiseSignalR() {
        // Initialise the SignalR hub
        var machineHub = $.connection.machineHub;

        machineHub.client.updateMachineStatus = function (model) {
            var machine = model.Machine;
            if (machine.Id !== machineId) {
                return;
            }
            updateRefillView(machine);
            getManagerRefillsData();
        };

        $.connection.hub.start().done();
    };
}();