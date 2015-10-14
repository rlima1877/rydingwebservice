function initMap() {
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 12,
        minZoom: 3,
        center: new google.maps.LatLng(39.95, -75.15),
    });

    var marker;
    var markerIcon = new google.maps.MarkerImage("/Content/Images/bus.png", null, null, null, new google.maps.Size(32, 32));

    map.addListener('click', function (event) {
        if (marker != null)
            marker.setMap(null);
        var latitude = event.latLng.lat();
        var longitude = event.latLng.lng();
        
        marker = new google.maps.Marker({
            position: event.latLng,
            map: map,
            icon: markerIcon,
        });
        google.maps.event.addListener(marker, "click", function (e) {
            var content = "Latitude: " + e.latLng.lat() + " Longitude: " + e.latLng.lng();
            var infoWindow = new google.maps.InfoWindow({
                content: content
            });
            infoWindow.open(map, marker);
            map.setZoom(15);
            map.setCenter(marker.getPosition());
        });
    });

    function SetMarker(lat, lon) {
        if (marker != null)
            marker.setMap(null);
        marker = new google.maps.Marker({
            position: new google.maps.LatLng(lat, lon),
            map: map,
            icon: markerIcon,
        });
        google.maps.event.addListener(marker, "click", function (e) {
            var content = "Latitude: " + e.latLng.lat() + " Longitude: " + e.latLng.lng();
            var infoWindow = new google.maps.InfoWindow({
                content: content
            });
            infoWindow.open(map, marker);
            map.setZoom(15);
            map.setCenter(marker.getPosition());
        });
    }

    $("#GoButton").on('click', function () {
        var dropdownlist = document.getElementById("busnumber");
        var busnumber = dropdownlist.options[dropdownlist.selectedIndex].value;
        var URL = window.location.toString();
        var Base_URL = URL.substring(0, URL.indexOf('Map'));
        
        $.ajax({
            type: "GET",
            url: Base_URL + "getbuslocation?num=" + busnumber,
            dataType: "json",
            success: function (result) {
                SetMarker(result.Latitude, result.Longitude);
            },
            error: function () {

            }
        });
    });
}