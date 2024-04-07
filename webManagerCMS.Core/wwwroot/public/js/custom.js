// RUN IMMEDIATELY
svg4everybody();
var lng = 0;
var rNum = '';

// ON DOCUMENT READY
$(document).ready(function(){
	header.init();
	$('#header').headroom();
	scrollToAnchor.init();
	slideshow.init();
	filmStrip.init();
	imgSlider.init();
	magnificPopup.init();
	collision.init();
	introslider.init();
	logoSlider.init();
	map.init();
	searchForm.init();

	lng = parseInt($("#idlng").text());
	rNum = $("#rNum").text().replace(/^\s+|\s+$/g, '');

	// === cookie policy ===
	CookieGdprBarHandler.init({
		idLang: lng,
		idLangFallBack: 0,
		cookieName: $("#wtCName").text(),
		cookieExpires: 365,
		gdprMoreInfoUrl: $("#gdpr-url").text(),
		agreementUrl: null,
		gtmInitCodeStr: $("#gtm-initcode").text(),
		rNum: rNum
	}, null);

	init_formValid();
	init_gMap();

	// events - MORE BUTTON
	if ($('.events--load-more').length) {
		$('.events--load-more').on('click', function (event) {
			event.preventDefault();

			var cnt = parseInt($(this).attr('data-cntnext'));
			var tiles = $(this).parents('.--events');

			tiles.find('.gs_reveal.d-none').each(function () {
				if (cnt > 0) {
					$(this).removeClass('d-none');
					$(this).find('.no-lazyload').removeClass('no-lazyload').addClass('lazyload');
					cnt--;
				} else {
					return false;
				}
			});

			if (tiles.find('.gs_reveal.d-none').length == 0) $(this).remove();
		});
	}

	if ($('.map__map').length) {
		var d1 = loadScripts([
			"https://maps.googleapis.com/maps/api/js?key=AIzaSyAS0Rq-dLhdwmPeuqUqGnz5Yw11zahgNvE&sensor=true&libraries=places",
			"/public/js/plugins/infobox_packed.js"
		], function () {
			if ($('#project-map').length > 0) {
				init_gMapProjects();
			}
		});
	}

	$('a.js-copy').on('click', function (event) {
		event.preventDefault();

		navigator.clipboard.writeText($(this).attr('href'))
			.then(() => {
				var $tooltip = $(this).parent().find('.tooltip');
				$tooltip.fadeIn(500, function () {
					setTimeout(function () {
						$tooltip.fadeOut(500);
					}, 2000); 
				});
			});
	});
});

Fancybox.bind("[data-fancybox]", {
	// Your custom options
	compact: false,
	idle: false,

	animated: false,
	showClass: false,
	hideClass: false,

	dragToClose: false,
	contentClick: false,
	mainClass: 'thumbs-right',
	wheel: false,
	defaultDisplay: 'flex',
	Thumbs: false,

	Images: {
		// Disable animation from/to thumbnail on start/close
		zoom: false,
		wheel: false,
	},

	Toolbar: {
		display: {
			left: [],
			middle: ['infobar'],
			right: ['close'],
		},
	},
	Carousel: {
		transition: 'fadeFast',
		preload: 3,

		Navigation: {
			prevTpl: '<svg width="23.56" height="26.413" viewBox="0 0 23.56 26.413"><g transform="translate(23.56 26.413) rotate(180)"><path d="M18.027,0l8.387,23.518H21.242L13.728,2.445H12.464L5.171,23.56H0L8.138,0Z" transform="translate(23.56) rotate(90)" fill-rule="evenodd"/></g></svg>',
			nextTpl: '<svg width="23.56" height="26.413" viewBox="0 0 23.56 26.413"><path d="M18.027,0l8.387,23.518H21.242L13.728,2.445H12.464L5.171,23.56H0L8.138,0Z" transform="translate(23.56) rotate(90)" fill-rule="evenodd"/></svg>',
		},
	},

	Thumbs: {
		type: 'classic',
		Carousel: {
			dragFree: false,
			slidesPerPage: 'auto',
			Navigation: false,
			axis: 'y',
			center: false,
			fill: true,
		},
	},
});

function init_formValid() {
	if ($('.form--validate').length) {
		$('.form--validate').each(function () {
			if ($(this).find('.field--required').length && !$(this).hasClass('search-form__input')) {
				$(this).submit(function (event) {

					var target = this;
					var out = true;
					var first = true;

					$(this).find('.field--required').each(function () {

						var obj1;

						if ($(this).hasClass('file-field')) {
							obj1 = $(this).find("input[type='file']");
							if (obj1.length) {
								obj1.parent().children('label').removeClass("field--error");

								var num = parseInt(obj1[0].files.length);
								if (num == 0) {
									out = false;
									obj1.parent().children('label').addClass("field--error");
								}
							}
						} else if ($(this).css('display') !== 'none') {

							obj1 = $(this).find('input,select,textarea');
							var val1 = obj1.val();

							obj1.parent().removeClass("field--error");
							obj1.parent().removeClass("field--error");

							var test = true;
							var val2 = $(this).attr("data-PM");
							if (val2 != "" && val2 != undefined && val2 != null) {
								test = ($('#' + val2 + ':checked').length > 0);
							}

							if (test) {
								if (obj1.attr("type") == "checkbox") {
									if (!obj1.is(':checked')) {
										out = false;
										obj1.parent().addClass("field--error");
									}
								} else {
									if (obj1.attr("type") == "email") {
										if (val1 == '' || val1 == undefined || val1 == null) {
											out = false;
											obj1.parent().addClass("field--error");
										} else {
											if (!validateEmail(val1)) {
												out = false;
												obj1.parent().addClass("field--error");
											}
										};
									} else {

										if (val1 == '' || val1 == undefined || val1 == null) {
											out = false;
											obj1.parent().addClass("field--error");
										};

									}
								}
							}
						}

						if (!out && first) {
							first = false;
							scroll2Obj1(obj1.parent(), 500, "swing");
							obj1.focus();
						}

					})
					if (out) {
						switch (target.id) {
							case "form-newsletter-banner":
								submitLargeNewsletterBannerForm();
								break;
							case "form-newsletter-banner-small":
								submitSmallNewsletterBannerForm();
								break;
						}
					}
					return out;
				});
			}
		});
	}
}

function scroll2Obj1(obj, time, anim) {
	if (obj.length) {
		var p = 0;
		if ($('header.header').length) p = $('header.header').height();
		p += 100;
		$('html,body').stop().animate({ scrollTop: obj.offset().top - p }, time, anim);
	}
}

function validateEmail(email) {
	var re = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,10}$/i;

	return re.test(email);
}

function submitLargeNewsletterBannerForm() {
	submitNewsletterBannerForm($('#newsletterbanner_email').val());
}

function submitSmallNewsletterBannerForm() {
	submitNewsletterBannerForm($('#newsletterbanner_small_email').val());
}

function submitNewsletterBannerForm(email) {
	if ($('#form-newsletter-banner').length || $('#form-newsletter-banner-small').length) {
		event.preventDefault();

		var val1 = email;
		var val2 = window.location.href;
		var url = "/AspService/_service_NewsletterForm_01.asp?InsMess=1";
		url += encodeURI('&email=' + val1);
		url += encodeURI('&rNum=' + rNum);
		url += encodeURI('&u=' + val2);
		url += ('&RND=' + Math.random());

		$.ajax({
			method: "GET",
			url: url,
			dataType: "text"
		}).done(function (data) {

			if (data) {
				$('.newsletter__form .alert--error').hide();
				$('.newsletter__form .alert--success').stop().delay(200).fadeIn();
			} else {
				$('.newsletter__form .alert--success').hide();
				$('.newsletter__form .alert--error').stop().delay(200).fadeIn();
			}

			setTimeout(function () {
				$('#newsletterbanner_email').val('');
				$('#newsletterbanner_small_email').val('');
			}, 1000); 
		});

		return false;
	}
}

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
					zoom: 17,
					mapTypeId: google.maps.MapTypeId.ROADMAP,
					gestureHandling: 'cooperative',
					disableDefaultUI: false,
					icon: image,
					styles: [{ "featureType": "all", "elementType": "geometry.fill", "stylers": [{ "visibility": "on" }, { "color": "#F0F0F0" }] }, { "featureType": "all", "elementType": "geometry.stroke", "stylers": [{ "visibility": "on" }, { "color": "#000000" }, { "weight": "0.5" }] }, { "featureType": "poi", "elementType": "labels", "stylers": [{ "visibility": "off" }] }, { "featureType": "road", "elementType": "geometry.fill", "stylers": [{ "visibility": "on" }, { "color": "#FFFFFF" }] }, { "featureType": "road", "elementType": "geometry.stroke", "stylers": [{ "visibility": "off" }] }, { "featureType": "road", "elementType": "labels.text", "stylers": [{ "visibility": "simplified" }, { "color": "#000000" }] }, { "featureType": "road", "elementType": "labels.icon", "stylers": [{ "visibility": "off" }] }, { "featureType": "transit", "elementType": "labels", "stylers": [{ "visibility": "off" }] }, { "featureType": "water", "elementType": "geometry.fill", "stylers": [{ "visibility": "on" }, { "color": "#FFFFFF" }] }, { "featureType": "water", "elementType": "labels", "stylers": [{ "visibility": "simplified" }, { "color": "#000000" }] }]
				};

				var mapClass = $(this).parent().find(".map");
				var map = new google.maps.Map(mapClass[0], mapProp);
				var image = '/public/images/kamzlin-pin.svg';
				const icon = {
					url: image,
					scaledSize: new google.maps.Size(50, 50),
					anchor: {
						x: 25,
						y: 25
					}
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

function init_gMapProjects() {
	// === projects map ===

	// map settings
	var parseGps = function (text) {
		return text.replace(/\s/, '').split(/,|;/);
	};

	var pmc = $('#project-mapCenter').text(); //def-plugin
	if (MyIsNull(pmc)) pmc = "49.22745151440501, 17.68095093420664|15"; //default

	var pos = "";
	var zoom = 15;

	if (pmc !== undefined && pmc.indexOf("|") >= 0) {
		var arr = pmc.split("|");
		pos = arr[0];
		zoom = (parseInt(arr[1]) <= 0) ? zoom : parseInt(arr[1]);
	} else {
		pos = pmc;
	}
	var latlng = parseGps(pos);

	var eventsMapCenter = new google.maps.LatLng(latlng[0], latlng[1]),
		eventsMapOptions = {
			center: eventsMapCenter,
			scrollwheel: false,
			zoom: zoom,
			mapTypeControl: false,
			mapTypeControlOptions:
			{
				style: google.maps.MapTypeControlStyle.HORIZONTAL_BAR,
				position: google.maps.ControlPosition.BOTTOM_CENTER,
				mapTypeIds: [google.maps.MapTypeId.ROADMAP, 'KamZlin']
			},
			panControl: true,
			gestureHandling: 'cooperative',
			disableDefaultUI: false,
			panControlOptions:
			{
				position: google.maps.ControlPosition.RIGHT_BOTTOM
			},
			zoomControl: true,
			zoomControlOptions:
			{
				style: google.maps.ZoomControlStyle.LARGE,
				position: google.maps.ControlPosition.RIGHT_TOP
			},
			scaleControl: false,
			streetViewControl: false
		},
		mapType = new google.maps.StyledMapType([{ "featureType": "all", "elementType": "geometry.fill", "stylers": [{ "visibility": "on" }, { "color": "#F0F0F0" }] }, { "featureType": "all", "elementType": "geometry.stroke", "stylers": [{ "visibility": "on" }, { "color": "#000000" }, { "weight": "0.5" }] }, { "featureType": "poi", "elementType": "labels", "stylers": [{ "visibility": "off" }] }, { "featureType": "road", "elementType": "geometry.fill", "stylers": [{ "visibility": "on" }, { "color": "#FFFFFF" }] }, { "featureType": "road", "elementType": "geometry.stroke", "stylers": [{ "visibility": "off" }] }, { "featureType": "road", "elementType": "labels.text", "stylers": [{ "visibility": "simplified" }, { "color": "#000000" }] }, { "featureType": "road", "elementType": "labels.icon", "stylers": [{ "visibility": "off" }] }, { "featureType": "transit", "elementType": "labels", "stylers": [{ "visibility": "off" }] }, { "featureType": "water", "elementType": "geometry.fill", "stylers": [{ "visibility": "on" }, { "color": "#FFFFFF" }] }, { "featureType": "water", "elementType": "labels", "stylers": [{ "visibility": "simplified" }, { "color": "#000000" }] }],
			{
				name: 'KamZlin'
			});

	// create map
	var eventsMap = new google.maps.Map(document.getElementById('project-map'), eventsMapOptions);
	eventsMap.mapTypes.set('KamZlin', mapType);
	eventsMap.setMapTypeId('KamZlin');

	var events = [];
	var items = $('.projects__map-inner').children('.map__popup').each(function () {
		var loc = $(this).find('.location').text();
		var latlng = parseGps(loc);
		var tit = $(this).find('.title').text();
		var txt = $(this).find('.text').html();
		var popupClass = $(this).attr('class');

		var polygon;
		var polygonJson = $(this).find('.polygon').text();
		if (testJSON(polygonJson)) {
			polygonJson = JSON.parse(polygonJson);
			polygon = new google.maps.Polygon({
				paths: transformCoordinates(polygonJson),
				strokeColor: "#1B1B19",
				strokeOpacity: 1,
				strokeWeight: 4,
				fillColor: "#D75758",
				fillOpacity: 0.8,
			});
		} else {
			polygon = null;
		}

		events.push({
			position: new google.maps.LatLng(latlng[0], latlng[1]),
			mapMarker: null,
			title: tit,
			infoBox: new InfoBox({
				content: txt,
				boxClass: popupClass,
				alignBottom: true,
				pixelOffset: new google.maps.Size(15, 430),
				closeBoxURL: ''
			}),
			polygon: polygon
		});

	});

	// create markers for events
	var eventsMap_markers = [];
	for (var i = 0; i < events.length; i++) {
		events[i].mapMarker = new google.maps.Marker({
			position: events[i].position,
			map: eventsMap,
			title: events[i].title,
			icon:
			{
				url: '/public/images/kamzlin-pin.svg',
				scaledSize: new google.maps.Size(50, 50),
				anchor: {
					x: 25,
					y: 25
				}
			},
			clickable: false,
			cursor: 'auto',
			project: events[i]
		}); // mapMarker
		eventsMap_markers.push(events[i].mapMarker);

		//add listeners
		google.maps.event.addListener(events[i].mapMarker, 'click', function () {
			var project = this.project;
			if (project.infoBox.getVisible()) {
				project.infoBox.close();
				if (project.polygon != null) {
					project.polygon.setMap(null);
				}
			} else {
				for (var j = 0; j < events.length; j++) {
					events[j].infoBox.close();
					if (events[j].polygon != null) {
						events[j].polygon.setMap(null);
					}
				}
				project.infoBox.open(eventsMap, project.mapMarker);
				project.infoBox.addListener("domready", function () {
					$(".map__cross").on("click", function (e) {
						e.preventDefault();

						for (var j = 0; j < events.length; j++) {
							events[j].infoBox.close();
							if (events[j].polygon != null) {
								events[j].polygon.setMap(null);
							}						}
					});
				});
				if (project.polygon != null) {
					project.polygon.setMap(eventsMap);
				}
			}
		});
	} // for
}

function loadScripts(scripts, callback, err) {
	var deferred = $.Deferred();
	function loadScript(scripts, callback, err, i) {
		$.ajax({
			url: scripts[i],
			dataType: "script",
			cache: true,
			success: function () {
				if (i + 1 < scripts.length) {
					loadScript(scripts, callback, err, i + 1);
				} else {
					if (callback) {
						callback();
					}
					deferred.resolve();
				}
			},
			error: function (xhr, ajaxOptions, thrownError) {
				err();
			}
		});
	}
	loadScript(scripts, callback, err, 0);

	return deferred;
}

function MyIsNull(strData) {
	if (strData != undefined && strData != '' && strData != null) {
		return false;
	} else {
		return true;
	}
}

function testJSON(text) {
	if (typeof text !== "string") {
		return false;
	}
	try {
		JSON.parse(text);
		return true;
	} catch (error) {
		return false;
	}
}

function transformCoordinates(jsonData) {
	const coordinates = jsonData.coordinates[0];
	const transformedData = coordinates.map(coord => {
		return { lat: coord[1], lng: coord[0] };
	});
	return transformedData;
}
