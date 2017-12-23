"use strict";
/**
 * Javascript required to build and refresh the Overview/Index page content dynamically,
 * collected into a namespace object to avoid global name clashes.
 */
$.ajaxSetup({
    // Disable caching of AJAX responses
    cache: false
});
var OverviewIndex = new function () {

   

    // Views on this page
    var views = {    
        merchantsTable: $("#merchant-list-table tbody")
    };

   

    // View templates loaded from the page HTML
    var viewTemplates = {
        merchant: Tools.extractTemplate("#template__merchant-details"),
      
};

    /**
     * Get JSON data from the server and initialise the views
     */
    this.initialise = function() {       

        $.ajax({
            url: "/Overview/MerchantData",
            success: function (merchantData) { addMerchantsToUi(merchantData); },
            cache: false
        });
    };







    /**
     * Add all the UI elements for the machines once we've fetched them via AJAX
     * @param {} data JSON data received from the server
     */
    function addMerchantsToUi(data) {
        
        for (var i = 0; i < data.Merchants.length; i++) {
            var merchant = data.Merchants[i];         

            updateMerchantUi( merchant);
          
        }

       
    }

    
    /**
     * Update the UI for a machine
     * 
     * @param {} machineUi  Machine UI element
     * @param {} machine    Machine data
     */
    function updateMerchantUi( merchant) {

         var merchantRow = viewTemplates.merchant
         .clone()
         merchantRow.find(".merchant-name strong").text(merchant.MerchantName).attr("href", "/Machine/" + merchant.Id);
         merchantRow.find(".merchant-id").text(merchant.MerchantId);
         merchantRow.find(".created-by").text(merchant.CreatedBy);
         merchantRow.find(".created-on").text(Format.asDateAndTime(merchant.CreatedDate));
         merchantRow.find(".last-updated-by").text(merchant.LastUpdatedBy);
         merchantRow.find(".last-updated-on").text(Format.asDateAndTime(merchant.LastUpdatedDate));
         merchantRow.find(".seller").text(merchant.Seller.SellersName);
         merchantRow.find(".acquiring-bank").text(merchant.AcquiringBank.Bank);
         views.merchantsTable.append(merchantRow);
    }

    /**
     * Update the totals shown at the top of the screen
     * 
     * @param {} data Data containing the totals
     */
    function updateTotals(data) {
        if (!data) {
            return;
        }

        $("#offline-total").text(data.OfflineCount);
        $("#full-total").text(data.FullCount);
        $("#errors-total").text(data.ErrorCount);
        $("#updates-total").text(data.UpdateCount);

        $(".system-offline-icon").toggleClass("active", data.OfflineCount > 0);
        $(".system-full-icon").toggleClass("active", data.FullCount > 0);
        $(".system-error-icon").toggleClass("active", data.ErrorCount > 0);
        $(".system-update-icon").toggleClass("active", data.UpdateCount > 0);
    }

    /**
     * Wire up the SignalR hub and implement any event handlers relevant to this page
     */
    function initialiseSignalR() {

        // Initialise the SignalR hub
        var machineHub = $.connection.machineHub;
        
        /**
         * Machine status is updated
         */
        machineHub.client.updateMachineStatus = function(data) {
            var machineUi = $("#" + data.Machine.Id + "-machine-status");
            var machine = data.Machine;

            updateMachineUi(machineUi, machine);
            updateTotals(data);
        };

        /**
         * Machine is updated (machine startup event)
         */
        machineHub.client.updateMachine = function(data) {
            var machineUi = $("#" + data.Machine.Id + "-machine-status");
            var machine = data.Machine;

            updateMachineUi(machineUi, machine);

          
        };

        $.connection.hub.start().done();
    }
}();
