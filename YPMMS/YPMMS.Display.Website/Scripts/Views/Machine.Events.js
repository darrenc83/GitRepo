"use strict";

var MachineEvents = new function() {

    var machineId;

    // ID of the oldest system event currently loaded. Used for loading the next page
    // when requested.
    var minSystemEventId = 0;

    var views = {
        machineName: $("#machine-name"),
        systemEventsList: $("#system-events-list"),
        showMoreButton: $("#show-more-btn")
    };

    var viewTemplates = {
        systemEvent: Tools.extractTemplate("#template__system-event")
    };

    /**
     * Get JSON data from the server and initialise the views
     */
    this.initialise = function(machineIdParam) {

        machineId = machineIdParam;

        getSystemEvents();
        initialiseSignalR();

        // Handler for the Show More button
        views.showMoreButton.click(function() {
            if (!$(this).hasClass("disabled")) {
                getSystemEvents(false);
            }
        });

    };

    /**
     * Get system event data from the server
     */
    function getSystemEvents(clearAll, fromDateTime, toDateTime) {
        $.get("/SystemEvents/GetEventsForMachine",
            {
                machineId: machineId,
                beforeId: minSystemEventId,
                from: fromDateTime,
                to: toDateTime
            },
            function (data) {

                // If we've got no more events, disable the Show More button
                views.showMoreButton.toggleClass("disabled", !data.MoreAvailable);

                // Store the lowest system event ID we've received
                if (data.Events && data.Events.length > 0) {
                    minSystemEventId = data.Events[data.Events.length - 1].Id;
                    views.machineName.text(data.Events[0].MachineName);
                }

                SystemEvents.updateSystemEventsUi(
                    data.Events,
                    viewTemplates.systemEvent,
                    views.systemEventsList,
                    true); // clearAll
            });
    }

    /**
     * Initialise SignalR endpoints
     * @returns {} 
     */
    function initialiseSignalR() {
        // Initialise the SignalR hub
        var machineHub = $.connection.machineHub;

        /**
         * System events are added
         */
        machineHub.client.updateSystemEvents = function () {

            getSystemEvents();
        };

        $.connection.hub.start().done();
    }
}();