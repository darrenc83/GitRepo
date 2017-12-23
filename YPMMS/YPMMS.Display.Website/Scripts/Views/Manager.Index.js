"use strict";

/**
 * Javascript required to build and refresh the Manager/Index page content dynamically,
 * collected into a namespace object to avoid global name clashes.
 */
var ManagerIndex = new function (){

    // Views loaded from the page HTML
    var views = {
        sitesList: $("#sites-list"),
        machinesLists: $("#machines-lists")
    };

    // Templates loaded from the page HTML
    var viewTemplates = {
        site: Tools.extractTemplate("#template__site-details"),
        // Note: machine template MUST be extracted before the machines list template
        machine: Tools.extractTemplate("#template__site_machine"),
        machineList: Tools.extractTemplate("#template__site-machines-list")
    };

    /**
     * Get JSON data from the server and initialise the views
     */
    this.initialise = function() {
        // Load the machine data
        $.get("/Manager/MachinesData",
            function(machinesBySite) {
                updateMachinesView(machinesBySite);
            });
    };

    /**
     * Populate the lists of sites and machines
     */
    function updateMachinesView(machinesBySite) {
        // Remove all previous contents
        $(".site-details").remove();
        $(".site-machines-list").remove();

        var counter = 1;
        for (var site in machinesBySite) {
            if (machinesBySite.hasOwnProperty(site)) {

                // Add to the sites list                
                viewTemplates.site.clone()
                    .attr("data-menu", counter)
                    .find(".site-name").text(site).end()
                    .find(".systems-count").text(machinesBySite[site].length).end()
                    .appendTo(views.sitesList);

                // Add machines to the machines list
                var machines = machinesBySite[site];
                var machinesList = viewTemplates.machineList.clone()
                    .attr("id", counter)
                    .find("h1").text(site).end();

                for (var i = 0; i < machines.length; i++) {
                    var machine = machines[i];
                    viewTemplates.machine.clone()
                        .find("a")
                            .attr("href", "/Manager/" + machine.Id)
                            .text(machine.Name)
                        .end()
                        .appendTo(machinesList);
                }

                machinesList.appendTo(views.machinesLists);

                counter++;
            }
        }

        $(".site-details:first").addClass("selected");
        $(".site-machines-list:first").addClass("active-menu");
    };

}();