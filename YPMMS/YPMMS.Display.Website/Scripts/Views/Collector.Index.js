"use strict";

/**
 * Javascript required to build and refresh the Collector/Index page content dynamically,
 * collected into a namespace object to avoid global name clashes.
 */
var CollectorIndex = new function(){

    // Local variable
    var collectorId = 0;

    // Views loaded from the page HTML
    var views = {
        collectionHistoryTable: $("#collection-history-table tbody"),
        refillStatusView: $("#collector-refill-system"),
        noRefillPlaceholderView: $("#collection-status"),
        coinsRefillSection: $(".currency-column.coins"),
        billsRefillSection: $(".currency-column.bills"),
        coinsRefillTotal: $(".currency-column.coins .total"),
        billsRefillTotal: $(".currency-column.bills .total"),
        collectionSiteName: $("#collector-refill-machine-site-name"),
        collectionSystemName: $("#collector-refill-machine-name")
    };

    // Templates loaded from the page HTML
    var viewTemplates = {
        collectionRow: Tools.extractTemplate("#template__collection-row"),
        refillDenomination: Tools.extractTemplate("#template__refill-denom")
    };

    /**
     * Get JSON data from the server and initialise the views
     * @param {} collectorId    ID of the collector we will display data for
     */
    this.initialise = function(collectorIdParam) {
        collectorId = collectorIdParam;
        getCollectionsData();

        // Load the machine data
        $.get("/Collector/CurrentMachineData",
            {
                collectorId: collectorId
            },
            function(machine) {
                updateRefillView(machine);
            });


        // Set up the date picker on the collections list
        DatePicker.setup(
            "#reportrange",
            "#reportrange span",
            function(start, end) {
                getCollectionsData(start, end);
            });

        initialiseSignalR();
    };

    /**
     * Fetch collections data for the current collector, optionally bounded by dates.
     * 
     * @param {} from   [Optional] Start date/time for collections to fetch, as a Date object
     * @param {} to     [Optional] End date/time for collections to fetch, as a Date object
     */
    function getCollectionsData(from, to) {
        var fromParam = from ? from.toISOString() : null;
        var toParam = to ? to.toISOString() : null;

        // Load the collections data
        $.get("/Collector/CollectionsData",
            {
                collectorId: collectorId,
                from: fromParam,
                to: toParam
            },
            function(collectionEvents) {
                addCollectionsToUi(collectionEvents);
            });
    }

    /**
     * Add each collection as a row in the table
     * 
     * @param {} collectionEvents 
     * @returns {} 
     */
    function addCollectionsToUi(collectionEvents) {

        // Remove any current rows
        $(".collection-history-clickable-row").remove();

        // Convert dates from strings to Date objects then
        // sort the collection events into descending date order
        for (var i = 0; i < collectionEvents.length; i++) {
            collectionEvents[i].Timestamp = new Date(collectionEvents[i].Timestamp);
        }
        collectionEvents.sort(function(a, b) {
            if (a.Timestamp < b.Timestamp) {
                return 1;
            }
            else if (a.Timestamp > b.Timestamp) {
                return -1;
            }
            return 0;
        });

        for (var j = 0; j < collectionEvents.length; j++) {
            var collection = collectionEvents[j];
            var collectionRow = buildCollectionRow(collection);
            views.collectionHistoryTable.append(collectionRow);
        }
    }

    /**
     * Build a row element containing details of a collection
     * @param {} collection Collection to display
     * @returns {}          Table row element displaying this collection
     */
    function buildCollectionRow(collection) {
        var collectionRow = viewTemplates.collectionRow
            .clone()
            // The complicated-looking syntax is to avoid a closure error:
            // see http://www.mennovanslooten.nl/blog/post/62
            .click(function(id) {
                return function() {
                    window.location = "/Collection/" + id;
                }
            }(collection.Id));

        collectionRow.find(".date").text(Format.asDate(collection.Timestamp));
        collectionRow.find(".time").text(Format.asTime(collection.Timestamp));
        collectionRow.find(".site-name").text(collection.Machine.Site.Name);
        collectionRow.find(".machine-name").text(collection.Machine.Name);
        collectionRow.find(".site-postcode").text(collection.Machine.Site.Postcode);
        if (collection.IsCollection) {
            collectionRow.find(".total-collected")
                .find(".collected-value").text(Format.asCurrency(collection.TotalValue)).end()
                .find(".currency").text(collection.Currency).end();
        } else {
            collectionRow.find(".total-refilled")
                .find(".refilled-value").text(Format.asCurrency(collection.TotalValue)).end()
                .find(".currency").text(collection.Currency).end();
        }

        return collectionRow;
    }

    /**
     * Update the refill table view
     * 
     * @param {} machine    Machine object with current denomination levels
     * @returns {} 
     */
    function updateRefillView(machine) {
        machine = machine || null;

        // Show the correct view, depending on whether this collector has a current machine
        views.refillStatusView.toggleClass("hidden", (machine == null));
        views.noRefillPlaceholderView.toggleClass("hidden", (machine != null));
        if (!machine) {
            return;
        }

        views.collectionSiteName.text(machine.Site.Name);
        views.collectionSystemName.text(machine.Name);

        machine.Denominations.sort(function(a, b) {
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
    }

    /**
     * Wire up the SignalR hub and implement any event handlers relevant to this page
     */
    function initialiseSignalR() {
        
        // Initialise the SignalR hub
        var machineHub = $.connection.machineHub;

        /**
         * Collection is added
         */
        machineHub.client.updateCollection = function (collection) {

            // Ignore if it isn't for this collector, or the date is not within the currently displayed range
            var date = new Date(collection.Timestamp);

            if (collection.Collector.Id !== collectorId
                || (DatePicker.startDate && (date < DatePicker.startDate))
                || (DatePicker.endDate && (date > DatePicker.endDate))) {
                return;
            }

            var collectionRow = buildCollectionRow(collection);
            views.collectionHistoryTable
                .find(".collection-history-clickable-row:first")
                .before(collectionRow);
        };

        /**
         * Collector status is updated
         */
        machineHub.client.updateCollector = function (model) {
            updateRefillView(model.Machine);
        }

        $.connection.hub.start().done();
    }
}();