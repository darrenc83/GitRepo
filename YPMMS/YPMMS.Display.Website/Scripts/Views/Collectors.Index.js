"use strict";

/**
 * Javascript required to build and refresh the Collectors/Index page content dynamically,
 * collected into a namespace object to avoid global name clashes.
 */
var CollectorsIndex = new function() {

    var views = {
        collectorsTable: $("#collector-user-list-table tbody")
    };

    // View templates loaded from the page HTML
    var viewTemplates = {
        collector: Tools.extractTemplate("#template__collector-details")
    };

    /**
     * Get JSON data from the server and initialise the views
     */
    this.initialise = function() {
        updateCollectors();
        initialiseSignalR();
    };

    /**
     * Request fresh Collectors data from the server and update the UI
     */
    function updateCollectors() {
        // Load the data
        $.get("/Collectors/CollectorsData",
                function (collectors) {
                    addCollectorsToUi(collectors);
                });
    }

    /**
     * Add all the UI elements for the machines once we've fetched them via AJAX
     * 
     * @param {} data JSON data received from the server
     */
    function addCollectorsToUi(collectors) {

        // Remove all
        $(".collector-summary-row").remove();

        // Sort collectors by name
        collectors.sort(function (a, b) {
            return a.Name.localeCompare(b.Name);
        });

        for (var i = 0; i < collectors.length; i++) {
            var collector = collectors[i];
            var collectorRow = viewTemplates.collector
                .clone()
                .toggleClass("error", collector.IsStatusError)
                // The complicated-looking syntax is to avoid a closure error:
                // see http://www.mennovanslooten.nl/blog/post/62
                .click(function (id) {
                    return function() {
                        window.location = "/Collector/" + id;
                    }
                }(collector.Id));

            collectorRow.find(".collector-name strong").text(collector.Name);
            collectorRow.find(".collector-id").text(collector.Id);
            collectorRow.find(".collector-key-id").text(collector.KeyId);
            collectorRow.find(".collector-email").text(collector.Email);
            collectorRow.find(".collector-phone").html(collector.PhoneNumber || "&mdash;");
            collectorRow.find(".collector-country").text(collector.Country);
            collectorRow.find(".collector-collection-status").text(collector.StatusDescription);
            collectorRow.find(".collector-machine-id").html(collector.MachineId || "&mdash;");
            collectorRow.find(".collector-last-update-time").text(Format.asDateAndTime(collector.LastUpdate));
            collectorRow.find(".collector-assigned-machines").text(collector.AssignedMachines);

            views.collectorsTable.append(collectorRow);
        }
    }

    /**
     * Wire up the SignalR hub and implement any event handlers relevant to this page
     */
    function initialiseSignalR() {
        // Initialise the SignalR hub
        var machineHub = $.connection.machineHub;

        /**
         * Collector status is updated
         */
        machineHub.client.updateCollector = function () {
            updateCollectors();
        }

        $.connection.hub.start().done();
    }
}();