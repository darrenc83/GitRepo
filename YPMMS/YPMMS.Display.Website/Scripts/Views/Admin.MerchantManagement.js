"use strict";

/**
 * Javascript required to build and refresh the Collector/SystemManagement page content dynamically,
 * collected into a namespace object to avoid global name clashes.
 */
$.ajaxSetup({
    // Disable caching of AJAX responses
    cache: false
});
var AdminMerchantManagement = new function () {

    var selectedMerchantIds = [];
   // var applyFloatValuesToSelectedMachines = false;

    // Views loaded from the page HTML
    var views = {
        machinesListContainer: $("#admin-full-merchant-list"),
        merchantList: $("#merchant-list"),       
        deleteSystemsButton: $("#delete-systems-btn"),
        loginPasswordField: $('#login-password'),
        deleteSystemsDialogButton: $('#delete-systems-dialog-btn'),
        merchantsTable: $("#merchant-list-table tbody")
    };

    // Templates loaded from the page HTML
    var viewTemplates = {
       
        merchant: Tools.extractTemplate("#template__merchant-details")
       
    };

    /**
     * Get JSON data from the server and initialise the views
     */
    this.initialise = function () {
      

        getMerchantList();

        $(".search-box")
            .on("change keyup",
                function() {
                    filterMachinesList($(this).val().trim());
                })
            .on("cut paste",
                function() {
                    setTimeout(function() {
                        $(".search-box").keyup();
                    }, 50);
                });


        //views.merchantListContainer.selectableScroll({
        //    filter: "div.select",
        //    cancel: ".edit-system-btn",
        //    scrollSnapX: 5, // When the selection is that pixels near to the top/bottom edges, start to scroll
        //    scrollSnapY: 5, // When the selection is that pixels near to the side edges, start to scroll
        //    scrollAmount: 25, // In pixels
        //    distance: 0, // In pixels (Distance in pixels that the mouse must move to start a selection)
        //    scrollIntervalTime: 100, // In milliseconds
        //    selected: function () {
        //        views.machineListActionButtons.removeClass("disabled");
        //    },
        //    unselected: function () {
        //        views.machineListActionButtons.toggleClass("disabled", $(".select .ui-selected").length === 0);
        //    }
        //});
              

        // Initialise caps lock watcher
        $(window)
            .capslockstate()
            .on({
                "capsOn": function() {
                    $(".caps-lock-alert").fadeIn();
                },
                "capsOff": function() {
                    $(".caps-lock-alert").fadeOut();
                }
            });
    }

    /**
     * Request the list of machines from the server
     */
    function getMerchantList() {

         $.ajax({
             url: "/Admin/MerchantData",
            success: function (merchantData) { addMerchantsToUi(merchantData); },
            cache: false
        });
       
    }

    function addMerchantsToUi(data) {
        
        for (var i = 0; i < data.length; i++) {
            var merchant = data[i];         

            updateMerchantsListUi( merchant);
          
        }

       
    }

    /**
     * Update the list of machines
     * @param {} machines   List of machine objects to show
     */
    function updateMerchantsListUi(merchant) {
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
        merchantRow.find(".edit-system-btn").click(function (merchantId) {
                        return function () {
                            window.location = "/Admin/EditMerchant/" + merchantId;
                        }
                    }(merchant.Id)).end()
        
        views.merchantsTable.append(merchantRow);
        //$(".machine-details").remove();

        //for (var i = 0; i < merchants.length; i++) {
        //    var merchant = merchants[i];

        //    viewTemplates.merchant.clone()
        //        .attr("id", "merchant-" + merchant.Id)
        //          .find(".merchant-id").text(merchant.MerchantId).end()
        //        .find(".merchant-name").text(merchant.MerchantName).end()
        //        //.find(".site-name").text(merchant.Site.Name).end()
        //        //.find(".address").text(merchant.Site.ShortAddress).end()
        //        .find(".edit-system-btn").click(function (merchantId) {
        //            return function () {
        //                window.location = "/Admin/EditSystem/" + merchantId;
        //            }
        //        }(merchant.Id)).end()
        //        .appendTo(views.merchantList);
        //}
    };

    /**
     * Filter the machines list when the user types in the search field
     * @param {} searchTerm     Search text entered by the user
     */
    function filterMachinesList(searchTerm) {
        $(".merchant-summary-row").each(function () {
            var matchesFilter = ($(this).text().toLowerCase().indexOf(searchTerm.toLowerCase()) > -1);
            $(this).toggle(matchesFilter);
            if (!matchesFilter) {
                $(this).find('.select').removeClass('ui-selected');
            }
        });
    }

  

    function prepareDeleteSystemsDialog() {
        // Hide any previously shown error/success/warning indications
        $(".error-message, .empty-message, .success-icon").hide();

        // Clear the password field
        views.loginPasswordField.val("");
    }

    function deleteSelectedSystems() {

        // Hide any previously shown error/success/warning indications
        $(".error-message, .empty-message, .success-icon").hide();

        // Check a password has been provided
        var password = views.loginPasswordField.val().trim();

        if (password === "") {
            views.loginPasswordField.siblings(".empty-message").fadeIn();
            return;
        }

        getSelectedMachineIds();

        $.post("/Admin/DeleteMachines",
            {
                Password: password,
                MachineIds: selectedMachineIds
            }, function() {
                views.loginPasswordField.siblings(".success-icon").fadeIn();
                setTimeout(function() {
                    $(".cancel-btn").click();
                    getMerchantList();
                }, 400);
            })
            .fail(function() {
                views.loginPasswordField.siblings(".error-message").fadeIn();
            });
    }

    /**
     * Get the machine IDs currently selected in the systems list
     * @returns {}  Array of selected machine IDs, or empty array if none are selected
     */
    function getSelectedMachineIds() {
        selectedMerchantIds = [];

        views.merchantList
            .find(".select.ui-selected")
            .each(function () {
                var parentId = $(this).parents(".merchant-details").attr("id");
                var merchantId = parentId.replace("merchant-", "");
                selectedMerchantIds.push(merchantId);
            });

        return selectedMachineIds;
    }
}();