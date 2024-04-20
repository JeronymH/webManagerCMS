// RUN IMMEDIATELY
svg4everybody();
var lng = 0;
var rNum = '';

// ON DOCUMENT READY
$(document).ready(function(){
    header.init();
    scrollToAnchor.init();
    backToTop.init();
	gsReveal.init();

	lng = parseInt($("#idlng").text());
	rNum = $("#rNum").text().replace(/^\s+|\s+$/g, '');

	init_gMap();
});

function init_gMap() {
	if ($(".gMap").length) {

		initializeMap = function () {

			$(".gMap").each(function () {
				var mapLoc = $(this).parent().find(".map-loc").text();
				mapLoc = mapLoc.replace(/\s/, '').split(/,|;/);

				var myCenter = new google.maps.LatLng(mapLoc[0], mapLoc[1]);
				var mapProp = {
					center: myCenter,
					scrollwheel: false,
					zoom: 14,
					mapTypeId: google.maps.MapTypeId.ROADMAP,
					gestureHandling: 'cooperative',
					disableDefaultUI: false,
					icon: image,
					styles: [{ "featureType": "administrative", "elementType": "labels.text.fill", "stylers": [{ "color": "#444444" }] }, { "featureType": "landscape", "elementType": "all", "stylers": [{ "color": "#f4f2f0" }, { "saturation": "0" }] }, { "featureType": "poi", "elementType": "all", "stylers": [{ "visibility": "off" }] }, { "featureType": "road", "elementType": "all", "stylers": [{ "saturation": -100 }, { "lightness": 45 }] }, { "featureType": "road.highway", "elementType": "all", "stylers": [{ "visibility": "simplified" }] }, { "featureType": "road.arterial", "elementType": "labels.icon", "stylers": [{ "visibility": "off" }] }, { "featureType": "transit", "elementType": "all", "stylers": [{ "visibility": "off" }] }, { "featureType": "water", "elementType": "all", "stylers": [{ "color": "#46bcec" }, { "visibility": "on" }] }]
					};

				var mapClass = $(this).parent().find(".map");
				var map = new google.maps.Map(mapClass[0], mapProp);
				var image = '/public/images/miriki-pin.svg';
				const icon = {
					url: image,
					scaledSize: new google.maps.Size(50, 50)
				};
				var markerLoc = new google.maps.LatLng(mapLoc[0], mapLoc[1]);
				var marker = new google.maps.Marker({
					position: markerLoc,
					icon: icon
				});
				marker.setMap(map);
			});


		};

		var gMapInit = document.createElement("script");
		gMapInit.type = "text/javascript";
		gMapInit.src = "https://maps.googleapis.com/maps/api/js?key=AIzaSyAS0Rq-dLhdwmPeuqUqGnz5Yw11zahgNvE&callback=initializeMap";
		document.body.appendChild(gMapInit);

	}
}
