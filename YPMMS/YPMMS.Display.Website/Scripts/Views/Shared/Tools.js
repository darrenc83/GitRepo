"use strict";

/**
 * Shared utility functions used across the site
 */
var Tools = new function(){

    /**
     * Extract a template element from the current page, removing its template ID and hidden class
     * 
     * @param {} selector   jQuery-style selector for the template element
     */
    this.extractTemplate = function(selector) {
        return $(selector).detach().removeClass("hidden").removeAttr("id");
    }

    this.showLoading = function () {
        var popup = $(".loading-message-corner");
        popup.addClass("loading");
    }

    this.hideLoading = function () {
        var popup = $(".loading-message-corner");
        popup.removeClass("loading");
    }
}();