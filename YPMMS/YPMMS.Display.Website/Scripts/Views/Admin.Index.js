"use strict";

/**
 * Javascript required to build and refresh the Admin/Index page content dynamically,
 * collected into a namespace object to avoid global name clashes.
 */
var AdminIndex = new function () {

    // ID of the oldest system event currently loaded. Used for loading the next page
    // when requested.
    var minSystemEventId = 0;

    // Views loaded from the page HTML
    var views = {
        systemEventsList: $("#system-events-list"),
        showMoreButton: $('#show-more-btn')
    };

    // View templates loaded from the page HTML
    var viewTemplates = {
        systemEvent: Tools.extractTemplate("#template__system-event", true)
    };

    /**
     * Get JSON data from the server and initialise the views
     */
    this.initialise = function () {
        getSystemEvents(true);
        initialiseSignalR();

        // Handler for the Show More button
        views.showMoreButton.click(function() {
            if (!$(this).hasClass("disabled")) {
                getSystemEvents(false);
            }
        });

        SystemEvents.markEventsViewed();
    };

    /**
     * Get system event data from the server
     */
    function getSystemEvents(clearAll, fromDateTime, toDateTime) {
        $.get("/SystemEvents/GetEvents",
            {
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
                }

                SystemEvents.updateSystemEventsUi(
                    data.Events,
                    viewTemplates.systemEvent,
                    views.systemEventsList,
                    clearAll);
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

            getSystemEvents(true);
        };

        $.connection.hub.start().done();
    }
}();