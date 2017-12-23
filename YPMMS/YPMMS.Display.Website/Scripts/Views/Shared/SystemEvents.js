"use strict";

/**
 * JavaScript for handling system events in the UI
 */
var SystemEvents = new function() {
    
    /**
     * Update the LastViewedEventsTime value for the current user
     */
    this.markEventsViewed = function() {
        $.post("/SystemEvents/MarkViewed");
    };

    /**
     * Update the system events list UI
     * @param {} events     System events fetched from the server
     * @param {} template   Template for the element used to display a system event
     * @param {} container  Container where system event elements are added
     * @param {} clearAll   If true, clear all previous events first
     */
    this.updateSystemEventsUi = function (events, template, container, clearAll) {

        if (clearAll) {
            container.find(".system-event").remove();
        }


        for (var i = 0; i < events.length; i++) {
            var event = events[i];

            var eventTypeClass = SystemEvents.getEventTypeClass(event);
            var linkUrl = SystemEvents.getEventLinkUrl(event);

            var eventCaption = event.EventCaption;
            if (event.UserName) {
                eventCaption += " [by " + event.UserName + "]";
            }
            var eventUi = template.clone()
                .find(".system-event-type").text(event.EventTypeDescription + ": ").end()
                .find(".system-event-detail").text(eventCaption).end()
                .find(".system-event-date").text(Format.asEventDateAndTime(event.Timestamp)).end()
                .addClass(eventTypeClass)
                .toggleClass("unread", event.IsNew);

            if (linkUrl) {
                eventUi
                    .addClass("clickable")
                    .click(function (linkUrl) {
                        return function () {
                            window.location = linkUrl;
                        }
                    }(linkUrl));
            }
            eventUi.appendTo(container);
        }
    }

    /**
     * Get the CSS class applicable to an event in the UI
     * @param {} event 
     */
    this.getEventTypeClass = function(event) {
        switch (event.EventType) {
            case "MachineCollected":
                return "collection";
            case "MachineRefilled":
                return "refill";
            case "MachineUpdateAvailable":
                return "update";
            case "MachineStatusAdded":
                if (event.MachineStatus === "Offline") {
                    return "offline";
                } else if (event.MachineStatusFull) {
                    return "full";
                } else if (event.MachineStatusError) {
                    return "error";
                }
                break;

            default:
                return null;
        }
    };

    /**
     * Get the link that should be followed when an event is clicked
     * @param {} event  Event to be clicked
     * @returns {}      URL to be visited when event is clicked, or null if none applicable
     */
    this.getEventLinkUrl = function(event) {
        switch (event.EventType) {

            case 'BillsFull':
            case 'BillJam':
            case 'CoinJam':
            case 'PowerRemoved':
            case 'CoinsFull':
            case 'Offline':
            case 'StackerFull':
            case 'SystemMovementDetected':
            case 'SecurityLoopBreak':
            case 'BillSystemUpdated':
            case 'CoinSystemUpdated':
            case 'LcSystemUpdated':
            case 'BillSystemSoftwareError':
            case 'CoinSystemSoftwareError':
            case 'LcSystemSoftwareError':
            case 'BillPathJam':
            case 'BillStackerJam':
            case 'Online':
            case 'PowerRestored':

            case 'TicketsLow':

            case 'TicketsJam':

            case 'CoinMechError':

            case 'CalibrationError':

            case 'PayoutOutofService':

            case 'PayoutJam':

            case 'BillFraudDetected':

            case 'CoinFraudDetected':

            case 'BillSystemInhibited':

            case 'CoinSystemInhibited':

            case 'CoinSystemReset':

            case 'BillSystemReset':
            case "MachineStatusAdded":
            case "MachineStatusRemoved":
            case "MachineAdded":
            case "MachineDetailsEdited":
            case "MachineUpdateAvailable":
            case "MachineAssignedToSite":
                return "/Machine/" + event.MachineId;
            case "MachineCollected":
            case "MachineRefilled":
                return "/Collection/" + event.CollectionEventId;
            case "CollectorAdded":
            case "CollectorAssignedToMachine":
                return "/Collector/" + event.CollectorId;
            default:
                return null;
        }
    };

}();