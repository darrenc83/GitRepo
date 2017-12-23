"use strict";

/**
 * Wrapper class around the date range picker from http://www.daterangepicker.com/.
 * This allows us to include a date picker a page and hook up a callback with the
 * minimum of repeated code.
 */
var DatePicker = new function (){

    /**
     * Currently selected start date/time
     */
    this.startDate = null;

    /**
     * Currently selected end date/time
     */
    this.endDate = null;

    /**
     * Set up a date picker on a specified element
     * 
     * @param {} containerSelector  jQuery-style selector for the date picker host element
     * @param {} captionSelector    jQuery-style selector for the element displaying the date picker caption
     * @param {} callback           Callback function to be invoked when the date picker is used (with parameters start, end)
     */
    this.setup = function (containerSelector, captionSelector, callback, range) {

        var dateFormat = "D MMMM YYYY";
        var self = this;

        // Display the date picker
        var datePicker = $(containerSelector)
            .daterangepicker({
                    "dateLimit": { "days": 30 },
                    "showDropdowns": true,
                    "timePicker": true,
                    ranges: {
                        "Today": [moment().startOf("day"), moment().endOf("day")],
                        "Yesterday": [
                            moment().startOf("day").subtract(1, "days"), moment().endOf("day").subtract(1, "days")
                        ],
                        "Last 7 Days": [moment().startOf("day").subtract(6, "days"), moment().endOf("day")],
                        "Last 30 Days": [moment().startOf("day").subtract(29, "days"), moment().endOf("day")],
                        "This Month": [moment().startOf("month"), moment().endOf("month")],
                        "Last Month": [
                            moment().subtract(1, "month").startOf("month"),
                            moment().subtract(1, "month").endOf("month")
                        ]
                    }
                },
                // Callback when a date range is picked
                function (start, end) {
                    self.startDate = start.toDate();
                    self.endDate = end.toDate();
                    callback(self.startDate, self.endDate);
                    setDatePickerCaption();
                })
            .data("daterangepicker");

        // Default to last 30 days
        var rangeKey = "Last 30 Days"
        if(range)
            rangeKey = range;

        var defaultRange = datePicker.ranges[rangeKey];
        self.startDate = defaultRange[0].toDate();
        self.endDate = defaultRange[1].toDate();

        datePicker.setStartDate(self.startDate);
        datePicker.setEndDate(self.endDate);
        setDatePickerCaption();

        function setDatePickerCaption() {
            $(captionSelector).text(datePicker.startDate.format(dateFormat) + " - " + datePicker.endDate.format(dateFormat));
        }
    }
}();
