"use strict";

/**
 * Utility functions for formatting dates, times and currency in a standardised format
 * across the site.
 */
var Format = new function(){

    /**
     * Format a decimal value as a currency value, i.e. to 2 decimal places, optionally
     * prepended with a currency symbol or abbreviation
     * 
     * @param {} value                  Numeric currency value
     * @param {} currencyCode           [Optional] ISO 4217 currency code (e.g. GBP, USB, EUR).
     * @param {} emptyStringForZero     [Optional] If true, an empty string is returned if value is 0
     * @returns {}                      Formatted currency string
     */
    this.asCurrency = function(value, currencyCode, emptyStringForZero) {
        if (value === 0 && emptyStringForZero === true) {
            return "";
        }

        return (currencyCode)
            ? value.toLocaleString(undefined,
            {
                style: "currency",
                currency: currencyCode,
                currencyDisplay: "symbol"
            })
            : value.toLocaleString(undefined,
            {
                style: "decimal",
                useGrouping: true,
                minimumFractionDigits: 2,
                maximumFractionDigits: 2
            });
    };

    /**
     * Format a Date value as a short date string
     * @param {} date   Date to format
     * @returns {}      Date formatted as a locale-specific short date string
     */
    this.asDate = function(date) {
        // Convert if not already a Date
        date = (date instanceof Date) ? date : new Date(date);
        return date.toLocaleDateString();
    };

    /**
     * Format a Date value as a short time string
     * @param {} date   Date to format
     * @returns {}      Date formatted as a locale-specific short time string
     */
    this.asTime = function(date) {
        // Convert if not already a Date
        date = (date instanceof Date) ? date : new Date(date);
        return date.toLocaleTimeString();
    };

    /**
     * Format a Date value as a short date string
     * @param {} date   Date to format
     * @returns {}      Date formatted as a locale-specific short date string
     */
    this.asDateAndTime = function(date) {
        // Convert if not already a Date
        date = (date instanceof Date) ? date : new Date(date);
        return this.asDate(date) + " " + this.asTime(date);
    };

    /**
     * Format a Date value as required in the events and notifications UI
     * @param {} date   Date to format
     * @returns {}      Date formatted as required
     */
    this.asEventDateAndTime = function(date) {
        // Convert if not already a Date
        date = (date instanceof Date) ? date : new Date(date);

        var now = moment();

        if (now.isSame(date, "day")) {
            return "Today - " + this.asTime(date);
        }
        if (now.subtract(1, "days").isSame(date, "day")) {
            return "Yesterday - " + this.asTime(date);
        }

        return this.asDate(date) + " - " + this.asTime(date);
    }
}();
