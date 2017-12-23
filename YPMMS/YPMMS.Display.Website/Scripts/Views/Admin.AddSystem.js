"use strict";

/**
 * Javascript required to build and refresh the Admin/AddSystem page content dynamically,
 * collected into a namespace object to avoid global name clashes.
 */
$.ajaxSetup({
    // Disable caching of AJAX responses
    cache: false
});
var AdminAddSystem = new function () {

    // Local variables
    var editMode = false;
    var merchant = {};        // Machine object built up as the user enters details
    var sites = [];
    var mapsApi = null;
    var searchedSite = null; // Site object obtained from Google by searching for the address entered by the user
    var map = null;
    var markers = [];
    var markerIcons = {};
    var topMarkerZIndex = 0;
    var markerClusterer = null;


    // Views loaded from the page HTML
    var views = {
        // Machine page
        idField: $("#id-field"),
        merchantIdField: $("#merchant-id-field"),
        merchantNameField: $("#merchant-name-field"),
        merchantApprovedDate: $("#approved-date"),
        merchantWelcomeCallDate: $("#welcome-call-date"),
        merchantApproxLiveDate: $("#approx-live-date"),
        merchantPageNextButton: $("#add-new-merchant-page .next-btn"),

        // Seller page
       
        sellerPageNextButton: $("#add-new-seller-page .next-btn"),
        siteSelectSiteAddressTab: $("#select-site-address-tab"),
       

        // Summary page
        summaryConfirmButton: $("#add-new-system-page-summary #confirm-details"),
        summaryEditDetailsButton: $("#add-new-system-page-summary #edit-details"),
        summaryMachineId: $("#summary-machine-id"),
        summaryMachineName: $("#summary-machine-name"),
        summarySiteName: $("#summary-site-name"),
        summarySiteAddress: $("#summary-site-address"),

        // General
        previousButtons: $(".previous-btn"),
        pages: $("section.content"),
        sitesList: $("#sites-list")
    };

    // Templates loaded from the page HTML
    var viewTemplates = {
        site: Tools.extractTemplate("#template__site-details")
    };

    // Default text from the page, to restore where necessary
    var defaultText = {
        selectedSiteName: "",
        selectedSiteAddress: ""
    };

    /**
     * Get JSON data from the server and initialise the views
     */
    this.initialise = function (googleApi, editMerchantId) {

       
        mapsApi = googleApi.maps;

        editMode = (editMerchantId ? true : false);

        if (editMode) {
            initialiseEditMode(editMerchantId);
        }

        GetDropDownData();

        $(".add-mode-heading").toggle(!editMode);
        $(".edit-mode-heading").toggle(editMode);

        //defaultText.selectedSiteName = views.selectedSiteNameHeading.text();
        //defaultText.selectedSiteAddress = views.selectedSiteAddressCaption.text();

        views.previousButtons.click(function () {
            goToPrevious();
        });

        $("#continue-to-terminal-selection").click(function () {
            ValidateSellerSelected();
        })

        views.merchantPageNextButton.click(function () {
           
            validateMerchantDetails();
        });


        //views.sellerPageNextButton.click(function () {
            
        //    if (!$(views.merchantPageNextButton).hasClass("disabled")) {
        //        goToNext();
        //    }
        //});
        //views.siteSearchButton.click(function () {
        //    searchForAddress();
        //});
        //views.siteSaveLocationButton.click(function () {
        //    if (!$(this).hasClass("disabled")) {
        //        saveSiteLocation();
        //    }
        //});
        views.summaryEditDetailsButton.click(function () {
            goToFirst();
        });
        views.summaryConfirmButton.click(function () {
            if (editMode) {
                editMachine();
            }
            else {
                addMachine();
            }
        });
        //views.siteSelectSiteAddressTab.click(function () {
        //    clearSiteCreationUi();
        //});
        //views.siteCreateNewSiteAddressTab.click(function () {
        //    clearSiteSelectionUi();
        //});

        //views.siteAddressResultsContainer.hide();

        //markerIcons = {
        //    normal: {
        //        url: "/Content/img/map/pin.png",
        //        size: new mapsApi.Size(43, 54),
        //        origin: new mapsApi.Point(0, 0),
        //        anchor: new mapsApi.Point(21, 51)
        //    },
        //    selected: {
        //        url: "/Content/img/map/map-marker-selected-site.png",
        //        size: new mapsApi.Size(56, 75),
        //        origin: new mapsApi.Point(0, 0),
        //        anchor: new mapsApi.Point(28, 66)
        //    }
        //};

        // Kick everything off by getting a list of sites from the server
        //getSitesList(null, editMode);
    };



    function GetDropDownData()
    {
        $.get("/Seller/GetSellers",
           function (sellertData) {
               var seller = sellertData;
               var options = '';
               var myselect = $('<select>');
               $.each(seller, function (index, key) {
                   myselect.append($('<option></option>').val(seller[index].Id).html(seller[index].SellersName));
               });
               $('#sellerDropDown').append(myselect.html());       

           });

        $.get("/Terminal/GetTerminals",
          function (terminalData) {
              var terminal = terminalData;
              var options = '';
              var myselect = $('<select>');
              $.each(terminal, function (index, key) {
                  myselect.append($('<option></option>').val(terminal[index].Id).html(terminal[index].Terminaltype));
              });
              $('#terminalDropDown').append(myselect.html());

          });

    }



    /**
     * Do any initialisation of the UI that's required when we're in Edit Mode.
     * 
     * @param {} machineId  ID of the machine being edited
     */
    function initialiseEditMode(merchantId) {

        $.get("/Merchant/GetMerchant/" + merchantId,
            function (merchantData) {
               var merchant = merchantData;

                views.idField
                    .addClass("disabled")
                    .parents(".input--haruki").addClass("input--filled").end()
                    .val(merchant.Id);

                views.merchantIdField
                   .parents(".input--haruki").addClass("input--filled").end()
                   .val(merchant.MerchantId);

                views.merchantNameField
                    .parents(".input--haruki").addClass("input--filled").end()
                    .removeAttr("disabled")
                    .val(merchant.MerchantName);

              
            });
    }

    /**
     * (Re-)fetch the list of sites from the server
     * @param {} callback   [Optional] function to call when done
     */
    function getSitesList(callback, selectCurrentMachineSite) {
        $.get("/Admin/SitesData", function (sitesData) {
            sites = sitesData;
            updateSitesListUi(selectCurrentMachineSite);

            if (callback) {
                callback();
            }
        });
    }

    /**
     * (Re-)populate the sites list UI
     */
    function updateSitesListUi(selectCurrentMachineSite) {
        $(".site-details").remove();

        for (var i = 0; i < sites.length; i++) {
            var site = sites[i];

            // Add list item
            viewTemplates.site.clone()
                .attr("id", "site-" + site.Id)
                .find(".site-name").text(site.Name).end()
                .find(".site-address").text(site.FullAddress).end()
                // The complicated-looking syntax is to avoid a closure error:
                // see http://www.mennovanslooten.nl/blog/post/62
                .click(function (site) {
                    return function () {
                        setSelectedSite(site, {
                            zoomMap: true
                        });
                    }
                }(site))
                .appendTo(views.sitesList);
        }

        if (selectCurrentMachineSite) {
            setSelectedSite(machine.Site, {
                zoomMap: true,
                scrollList: true
            });
        }
    }

    /**
     * Show a bubble over an input field warning that it is empty 
     * @param {} field  Field to show bubble for
     */
    function highlightEmptyField(field) {
        field.siblings(".empty-message").fadeIn();
    }

    /**
     * Show a bubble over an input field displaying an error message
     * @param {} field          Field to show the bubble for
     * @param {} errorMessage   [Optional] Error message to display (omit to get the default error message for this field)
     */
    function highlightErrorField(field, errorMessage) {
        var errorBubble = field.siblings(".error-message");
        if (errorMessage) {
            errorBubble.text(errorMessage);
        }
        errorBubble.fadeIn();
    }

    /**
     * Show a success icon in a field that has valid contents
     * @param {} field  Field to show the tick in
     */
    function highlightSuccessField(field) {
        field.siblings(".success-icon").fadeIn();
    }

    /**
     * Hide any error/empty bubbles or success icons that are displayed on input fields
     */
    function clearAllFieldHighlights() {
        $(".error-message, .empty-message, .success-icon").hide();
    }



    function ValidateSellerSelected()
    {
        var sellerSelectedError = false;
        //clear any existing errors first
        clearAllFieldHighlights();

        var e = document.getElementById("sellerDropDown");
        var selectedSeller = e.options[e.selectedIndex].value;
             
        if (selectedSeller == 0)
        {
            sellerSelectedError = true;
        }

        if (sellerSelectedError) {
            $("#continue-to-terminal-selection").addClass("disabled");
            highlightEmptyField($("#sellerDropDown"));
            return;
        }

        else {
            $("#continue-to-terminal-selection").removeClass("disabled");
            clearAllFieldHighlights();
            goToNext();
        }


    }

    /**
     * Validate the details entered on the Machine page, and proceed to next page if valid
     */
    function validateMerchantDetails() {
        
        var idError = false;
        var nameError = false;
        var approvedDate = false;
        var welcomeCallDate = false;
        var approxLiveDate = false;

        //clear any existing errors first
        clearAllFieldHighlights();

        // Simple client-side validation first

        // Validate merchant ID
        var merchantId = views.merchantIdField.val().trim();
        idError = (merchantId === "");
        if (idError) {
            highlightEmptyField(views.merchantIdField);
        }

        // Validate merchant name
        var merchantName = views.merchantNameField.val().trim();
        nameError = (merchantName === "");
        if (nameError) {
            highlightEmptyField(views.merchantNameField);
        }

        // Validate approved date
        var merchantApprovedDate = views.merchantApprovedDate.val().trim();
        approvedDate = (merchantApprovedDate === "");
        if (approvedDate) {
            highlightEmptyField(views.merchantApprovedDate);
        }

        // Validate welcome call date
        var WelcomeCallDate = views.merchantWelcomeCallDate.val().trim();
        welcomeCallDate = (WelcomeCallDate === "");
        if (welcomeCallDate) {
            highlightEmptyField(views.merchantWelcomeCallDate);
        }

        // Validate approx live date
        var ApproxLiveDate = views.merchantApproxLiveDate.val().trim();
        approxLiveDate = (ApproxLiveDate === "");
        if (approxLiveDate) {
            highlightEmptyField(views.merchantApproxLiveDate);
        }


      //  merchantWelcomeCallDate: $("#welcome-call-date"),
       // merchantApproxLiveDate: $("#approx-live-date"),



        if (idError && nameError) {
            views.merchantPageNextButton.addClass("disabled");
            return;
        }

        else
        {
            views.merchantPageNextButton.removeClass("disabled");
        }

        // Finish by validating on the server
        $.get("/Admin/validateMerchantDetails",
            {
                merchantId: merchantId,
                merchantName: merchantName,
                editMode: editMode
            },
            function (response) {
                if (response.IdError && !idError && !editMode) {
                    idError = true;
                    highlightErrorField(views.merchantIdField, response.IdErrorMessage);
                }

                if (response.NameError && !nameError) {
                    nameError = true;
                    highlightErrorField(views.merchantNameField, response.NameErrorMessage);
                }

                if (!idError && !editMode) {
                    highlightSuccessField(views.merchantIdField);
                }
                if (!nameError) {
                    highlightSuccessField(views.merchantNameField);
                }
                if (!idError && !nameError) {
                    setTimeout(function () {
                        onValidated();
                    }, 400);
                }
            });


        function onValidated() {
            merchant.Id = merchantId;
            views.summaryMachineId.text(merchantId);

            merchant.MerchantName = merchantName;
            views.summaryMachineName.text(merchantName);
           
            goToNext();
            //views.sellerPageNextButton.toggleClass("disabled");
            //updateSitesMap();
        }
    }

    /**
     * Display the map of sites
     */
    function updateSitesMap() {
        // Add map
        map = map || new mapsApi.Map(views.siteAddressMap[0],
        {
            zoom: 10,
            center: { lat: 0, lng: 0 },
            styles: MapDefaults.styles
        });

        var mapBounds = new mapsApi.LatLngBounds();

        // Remove all existing markers from the map
        removeMapMarkers();

        for (var i = 0; i < sites.length; i++) {
            var site = sites[i];

            // Build the marker
            var latLng = new mapsApi.LatLng(site.Latitude, site.Longitude);
            mapBounds.extend(latLng);
            var marker = new mapsApi.Marker({
                position: latLng,
                title: site.Name,
                clickable: true,
                icon: markerIcons.normal
            });
            marker.siteId = site.Id;

            // Set the click listener
            // The complicated-looking syntax is to avoid a closure error:
            // see http://www.mennovanslooten.nl/blog/post/62
            marker.addListener("click", function (site) {
                return function () {
                    setSelectedSite(site, {
                        scrollList: true
                    });
                }
            }(site));

            markers.push(marker);
        }

        var clusterOptions = {
            zoomOnClick: true,
            imagePath: "/Content/img/map/m"
        };
        // ReSharper disable once UnusedLocals
        markerClusterer = new MarkerClusterer(map, markers, clusterOptions);

        topMarkerZIndex = topMarkerZIndex || mapsApi.Marker.MAX_ZINDEX;

        map.fitBounds(mapBounds);
    }

    /**
     * Remove all markers from the map
     */
    function removeMapMarkers() {
        while (markers.length > 0) {
            var marker = markers.pop();
            if (markerClusterer) {
                markerClusterer.removeMarker(marker);
            }
            marker.setMap(null);
        }
    }

    /**
     * Clear any site selection made in the "Select a Site Address" section
     * (when switching to the "Create a New Site Address" section)
     */
    function clearSiteSelectionUi() {
        removeMapMarkers();
        setSelectedSite(null);
        clearAllFieldHighlights();
    }

    /**
     * Clear any site creation details entered in the "Create a New Site Address" section
     * (when switching to the "Select a Site Address" section)
     */
    function clearSiteCreationUi() {
        clearAllFieldHighlights();
        updateSitesMap();

        views.siteNameField.val("");
        views.siteAddressField.val("");
        views.siteAddressResults.text("");
        views.siteAddressResultsContainer.hide();
    }

    /**
     * Do a Google API search for the address entered by the user
     */
    function searchForAddress() {
        clearAllFieldHighlights();

        // Get the address entered (show error if empty)
        var address = views.siteAddressField.val().trim();
        if (address === "") {
            highlightEmptyField(views.siteAddressField);
            return;
        }

        new mapsApi.Geocoder().geocode(
            {
                region: "uk",   // Bias results towards the UK - e.g. "Birmingham" should find Birmingham UK, not Alabama
                address: address
            },
            function (results, status) {
                handleSearchResults(results, status);
            });
    }

    /**
     * Handle the results returned by a Google Geocode lookup
     * 
     * @param {} results    Results object returned by the API
     * @param {} status     Status code returned by the API
     * @returns {} 
     */
    function handleSearchResults(results, status) {
        if (status !== "OK") {
            onSearchError("Address could not be found. Please try again.");
            return;
        }

        var result = results[0];
        if (!storeGeocodeResult(result)) {
            onSearchError("Please enter a more specific address");
            return;
        }

        views.siteAddressResultsContainer.fadeIn();
        views.siteSaveLocationButton.removeClass("disabled");

        // Show the map marker
        removeMapMarkers();

        var markerPosition = result.geometry.location;

        var marker = new mapsApi.Marker({
            map: map,
            position: markerPosition,
            icon: markerIcons.normal,
            draggable: true,
            title: "Drag to move"
        });
        markers.push(marker);
        map.setZoom(14);
        map.setCenter(result.geometry.location);

        // Handler for when the marker is dragged:
        // we pass the new latLng back to geocode() to get a new address
        marker.addListener("dragend",
            function (position) {
                onSearchResultsMarkerDragged(
                    position.latLng,
                    // onSuccess: Lookup was successful, so update the stored marker position
                    function () {
                        markerPosition = position.latLng;
                        views.siteAddressField.val(searchedSite.Postcode);
                    },
                    // onError: Lookup was unsuccessful, so revert to the previous marker position
                    function () {
                        marker.setPosition(markerPosition);
                    });
            });
    }

    /**
     * Handle an error encountered when searching for a site address
     * @param {} errorMessage   Error message to show
     * @returns {} 
     */
    function onSearchError(errorMessage) {
        highlightErrorField(views.siteAddressField, errorMessage);
        views.siteAddressResultsContainer.hide();
        views.siteAddressResults.text("");
        views.siteSaveLocationButton.addClass("disabled");
    }

    /**
     * Do a fresh lookup when the search results marker is dragged.
     * Returns true if the lookup found a valid address.
     * 
     * @param {} newLatLng  New lat/lng position the marker was dragged to
     * @returns {}          True if the new position was successfully recolved to an address
     */
    function onSearchResultsMarkerDragged(newLatLng, onSuccess, onError) {
        new mapsApi.Geocoder().geocode(
            {
                location: newLatLng
            },
            function (results, status) {
                if ((status === "OK") && storeGeocodeResult(results[0])) {
                    onSuccess();
                }
                else {
                    onError();
                }
            }
        );
    }

    /**
     * Store the address details returned by Google in the local machine's Site object
     * @param {} result     Google Geocode result
     * @returns             True if address is detailed enough, otherwise false
     */
    function storeGeocodeResult(result) {
        var site = {};

        site.Latitude = result.geometry.location.lat();
        site.Longitude = result.geometry.location.lng();

        for (var i = 0; i < result.address_components.length; i++) {
            var component = result.address_components[i];
            var componentText = component.long_name;
            var componentTypes = component.types;

            if (componentTypes.indexOf("street_number") > -1) {
                site.Address = componentText + " " + (site.Address || "");
            }
            if (componentTypes.indexOf("route") > -1) {
                site.Address = (site.Address || "") + componentText;
            }
            else if (componentTypes.indexOf("postal_town") > -1) {
                site.Town = componentText;
            }
            else if (componentTypes.indexOf("postal_code") > -1) {
                site.Postcode = componentText;
            }
            else if (componentTypes.indexOf("administrative_area_level_2") > -1) {
                site.Area = componentText;
            }
            else if (componentTypes.indexOf("country") > -1) {
                site.Country = componentText;
            }
        }

        if (site.Address && site.Postcode && site.Country) {
            searchedSite = site;

            views.siteAddressResults.text(result.formatted_address);

            return true;
        }

        return false;
    }

    /**
     * Add a new site based on the user's input
     */
    function saveSiteLocation() {
        clearAllFieldHighlights();

        var readyToValidate = true;

        // Validate machine name
        var siteName = views.siteNameField.val().trim();
        if (siteName === "") {
            highlightEmptyField(views.siteNameField);
            readyToValidate = false;
        }

        if (!searchedSite) {
            highlightEmptyField(views.siteAddressField);
            readyToValidate = false;
        }

        if (readyToValidate) {
            $.get("/Admin/ValidateSiteDetails?siteName=" + siteName,
                function (response) {
                    if (response.NameError) {
                        highlightErrorField(views.siteNameField, response.NameErrorMessage);
                    } else {
                        highlightSuccessField(views.siteNameField);
                        searchedSite.Name = siteName;
                        addSite();
                    }
                });
        }
    }

    /**
     * Validate the details entered on the Site page, and proceed to next page if valid
     */
    function validateSiteDetails() {
        if (machine.Site) {
            views.summarySiteName.text(machine.Site.Name);
            views.summarySiteAddress.text(machine.Site.FullAddress);

            goToNext();
        }
    }

    /**
     * Set the currently selected site
     * @param {} site       Site that's been selected
     * @param {} options    Options with boolean values for zoomMap and scrollList
     */
    function setSelectedSite(site, options) {
        machine.Site = (site || null);
        views.selectedSiteNameHeading.text(site ? site.Name : defaultText.selectedSiteName);
        views.selectedSiteAddressCaption.text(site ? site.FullAddress : defaultText.selectedSiteAddress);

        if (site) {
            // Select the item that matches site.Id...
            var listItem = views.sitesList.find("#site-" + site.Id);
            listItem.addClass("selected").siblings().removeClass("selected");

            // Highlight the map marker
            for (var i = 0; i < markers.length; i++) {
                var marker = markers[i];
                if (marker.siteId === site.Id) {
                    marker.setZIndex(++topMarkerZIndex);
                    marker.setIcon(markerIcons.selected);
                }
                else {
                    marker.setIcon(markerIcons.normal);
                }
            }

            if (options.zoomMap) {
                map = map || new mapsApi.Map(views.siteAddressMap[0],
                                    {
                                        zoom: 10,
                                        center: { lat: 0, lng: 0 },
                                        styles: MapDefaults.styles
                                    });
                map.setCenter({ lat: site.Latitude, lng: site.Longitude });
                map.setZoom(16);
            }

            if (options.scrollList) {
                listItem[0].scrollIntoView({
                    behavior: "smooth"
                });
            }
        }
        else {
            $(".site-details").removeClass("selected");
        }

        var siteSelected = (machine.Site === null);
        views.sitePageNextButton.toggleClass("disabled", siteSelected);
    }

    /**
     * Add the machine once the user confirms the details
     */
    function addMachine() {
        $.post(
            "/Admin/AddMachine",
            machine,
            // Success callback
            function () {
                window.location = "/Admin/SystemManagement";
            })
            // Error callback
            .fail(function () {
                // TODO: handle error
            });
    }

    /**
     * Add the machine once the user confirms the details
     */
    function editMachine() {
        $.post(
            "/Admin/EditMachine",
            machine,
            // Success callback
            function () {
                window.location = "/Admin/SystemManagement";
            })
            // Error callback
            .fail(function () {
                // TODO: handle error
            });
    }

    /**
     * Add a new site
     */
    function addSite() {
        $.post(
            "/Admin/AddSite",
            searchedSite,
            // Success callback
            function (addedSite) {
                // Refresh the sites list
                getSitesList(function () {
                    // Switch to the "Select a Site Address" tab
                    views.siteSelectSiteAddressTab.click();
                    updateSitesMap();
                    setSelectedSite(addedSite, {
                        zoomMap: true,
                        scrollList: true
                    });
                });
            })
            // Error callback
            .fail(function () {
                // TODO: handle error
            })
            .always(function () {
                // Reset searched site to null
                searchedSite = null;
            });
    }

    /**
     * Go to the previous page of the wizard
     */
    function goToPrevious() {
        $("section.content:not(.hidden)")
            .addClass("hidden")
            .prev()
            .removeClass("hidden");
    }

    /**
     * Go to the next page of the wizard
     */
    function goToNext() {
       
        $("section.content:not(.hidden)")
            .addClass("hidden")
            .next()
            .removeClass("hidden");
    }

    /**
     * Go to the first page of the wizard
     */
    function goToFirst() {
        $("section.content").addClass("hidden").first().removeClass("hidden");
    }
}();