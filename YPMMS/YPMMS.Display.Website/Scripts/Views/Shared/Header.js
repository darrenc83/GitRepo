"use strict";

/**
 * Javascript required by the Header.cshtml partial view
 */
var Header = new function() {

    var views = {
        systemEventsList: $("#header-system-events-list"),
        systemEventCounts: $(".header-unread-notifications-count")
    };

    var viewTemplates = {
        systemEvent: Tools.extractTemplate("#template__header-system-event")
    };

    this.initialise = function() {
        //getSystemEvents();
       // initialiseSignalR();
    };

    /**
     * Get system event data from the server
     */
    function getSystemEvents() {


        $.get("/SystemEvents/GetLatestEvents", function (data) {
            SystemEvents.updateSystemEventsUi(
                data.Events,
                viewTemplates.systemEvent,
                views.systemEventsList,
                true);

            views.systemEventCounts
                .text(parseInt(data.UnreadCount) > 10 ? '+': data.UnreadCount)
                .toggle(data.UnreadCount > 0);
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