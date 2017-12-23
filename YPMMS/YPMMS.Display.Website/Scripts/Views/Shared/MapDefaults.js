﻿/**
 * Shared default values for Google Maps
 */
var MapDefaults = {

    /**
     * Styles to apply to all Google maps across the site
     */
    styles: [
        {
            featureType: 'water',
            elementType: 'all',
            stylers: [{ hue: '#d7ebef' }, { saturation: -5 }, { lightness: 54 }, { visibility: 'on' }]
        }, {
            featureType: 'landscape',
            elementType: 'all',
            stylers: [{ hue: '#eceae6' }, { saturation: -49 }, { lightness: 22 }, { visibility: 'on' }]
        }, {
            featureType: 'poi.park',
            elementType: 'all',
            stylers: [{ hue: '#dddbd7' }, { saturation: -81 }, { lightness: 34 }, { visibility: 'on' }]
        }, {
            featureType: 'poi.medical',
            elementType: 'all',
            stylers: [{ hue: '#dddbd7' }, { saturation: -80 }, { lightness: -2 }, { visibility: 'on' }]
        }, {
            featureType: 'poi.school',
            elementType: 'all',
            stylers: [{ hue: '#c8c6c3' }, { saturation: -91 }, { lightness: -7 }, { visibility: 'on' }]
        }, {
            featureType: 'landscape.natural',
            elementType: 'all',
            stylers: [{ hue: '#c8c6c3' }, { saturation: -71 }, { lightness: -18 }, { visibility: 'on' }]
        }, {
            featureType: 'road.highway',
            elementType: 'all',
            stylers: [{ hue: '#dddbd7' }, { saturation: -92 }, { lightness: 60 }, { visibility: 'on' }]
        }, {
            featureType: 'poi',
            elementType: 'all',
            stylers: [{ hue: '#dddbd7' }, { saturation: -81 }, { lightness: 34 }, { visibility: 'on' }]
        }, {
            featureType: 'road.arterial',
            elementType: 'all',
            stylers: [{ hue: '#dddbd7' }, { saturation: -92 }, { lightness: 37 }, { visibility: 'on' }]
        }, {
            featureType: 'transit',
            elementType: 'geometry',
            stylers: [{ hue: '#c8c6c3' }, { saturation: 4 }, { lightness: 10 }, { visibility: 'on' }]
        }
    ]
};