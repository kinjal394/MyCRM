///*!
// * FullCalendar v2.1.1
// * Docs & License: http://arshaw.com/fullcalendar/
// * (c) 2013 Adam Shaw
// */

//(function(factory) {
//	if (typeof define === 'function' && define.amd) {
//		define([ 'jquery', 'moment' ], factory);
//	}
//	else {
//		factory(jQuery, moment);
//	}
//})(function($, moment) {

//;;

//var defaults = {

//	lang: 'en',

//	defaultTimedEventDuration: '02:00:00',
//	defaultAllDayEventDuration: { days: 1 },
//	forceEventDuration: false,
//	nextDayThreshold: '09:00:00', // 9am

//	// display
//	defaultView: 'month',
//	aspectRatio: 1.35,
//	header: {
//		left: 'title',
//		center: '',
//		right: 'today prev,next'
//	},
//	weekends: true,
//	weekNumbers: false,

//	weekNumberTitle: 'W',
//	weekNumberCalculation: 'local',
	
//	//editable: false,
	
//	// event ajax
//	lazyFetching: true,
//	startParam: 'start',
//	endParam: 'end',
//	timezoneParam: 'timezone',

//	timezone: false,

//	//allDayDefault: undefined,
	
//	// time formats
//	titleFormat: {
//		month: 'MMMM YYYY', // like "September 1986". each language will override this
//		week: 'll', // like "Sep 4 1986"
//		day: 'LL' // like "September 4 1986"
//	},
//	columnFormat: {
//		month: 'ddd', // like "Sat"
//		week: generateWeekColumnFormat,
//		day: 'dddd' // like "Saturday"
//	},
//	timeFormat: { // for event elements
//		'default': generateShortTimeFormat
//	},

//	displayEventEnd: {
//		month: false,
//		basicWeek: false,
//		'default': true
//	},
	
//	// locale
//	isRTL: false,
//	defaultButtonText: {
//		prev: "prev",
//		next: "next",
//		prevYear: "prev year",
//		nextYear: "next year",
//		today: 'today',
//		month: 'month',
//		week: 'week',
//		day: 'day'
//	},

//	buttonIcons: {
//		prev: 'left-single-arrow',
//		next: 'right-single-arrow',
//		prevYear: 'left-double-arrow',
//		nextYear: 'right-double-arrow'
//	},
	
//	// jquery-ui theming
//	theme: false,
//	themeButtonIcons: {
//		prev: 'circle-triangle-w',
//		next: 'circle-triangle-e',
//		prevYear: 'seek-prev',
//		nextYear: 'seek-next'
//	},

//	dragOpacity: .75,
//	dragRevertDuration: 500,
//	dragScroll: true,
	
//	//selectable: false,
//	unselectAuto: true,
	
//	dropAccept: '*',

//	eventLimit: false,
//	eventLimitText: 'more',
//	eventLimitClick: 'popover',
//	dayPopoverFormat: 'LL',
	
//	handleWindowResize: true,
//	windowResizeDelay: 200 // milliseconds before a rerender happens
	
//};


//function generateShortTimeFormat(options, langData) {
//	return langData.longDateFormat('LT')
//		.replace(':mm', '(:mm)')
//		.replace(/(\Wmm)$/, '($1)') // like above, but for foreign langs
//		.replace(/\s*a$/i, 't'); // convert to AM/PM/am/pm to lowercase one-letter. remove any spaces beforehand
//}


//function generateWeekColumnFormat(options, langData) {
//	var format = langData.longDateFormat('L'); // for the format like "MM/DD/YYYY"
//	format = format.replace(/^Y+[^\w\s]*|[^\w\s]*Y+$/g, ''); // strip the year off the edge, as well as other misc non-whitespace chars
//	if (options.isRTL) {
//		format += ' ddd'; // for RTL, add day-of-week to end
//	}
//	else {
//		format = 'ddd ' + format; // for LTR, add day-of-week to beginning
//	}
//	return format;
//}


//var langOptionHash = {
//	en: {
//		columnFormat: {
//			week: 'ddd M/D' // override for english. different from the generated default, which is MM/DD
//		},
//		dayPopoverFormat: 'dddd, MMMM D'
//	}
//};


//// right-to-left defaults
//var rtlDefaults = {
//	header: {
//		left: 'next,prev today',
//		center: '',
//		right: 'title'
//	},
//	buttonIcons: {
//		prev: 'right-single-arrow',
//		next: 'left-single-arrow',
//		prevYear: 'right-double-arrow',
//		nextYear: 'left-double-arrow'
//	},
//	themeButtonIcons: {
//		prev: 'circle-triangle-e',
//		next: 'circle-triangle-w',
//		nextYear: 'seek-prev',
//		prevYear: 'seek-next'
//	}
//};

//;;

//var fc = $.fullCalendar = { version: "2.1.1" };
//var fcViews = fc.views = {};


//$.fn.fullCalendar = function(options) {
//	var args = Array.prototype.slice.call(arguments, 1); // for a possible method call
//	var res = this; // what this function will return (this jQuery object by default)

//	this.each(function(i, _element) { // loop each DOM element involved
//		var element = $(_element);
//		var calendar = element.data('fullCalendar'); // get the existing calendar object (if any)
//		var singleRes; // the returned value of this single method call

//		// a method call
//		if (typeof options === 'string') {
//			if (calendar && $.isFunction(calendar[options])) {
//				singleRes = calendar[options].apply(calendar, args);
//				if (!i) {
//					res = singleRes; // record the first method call result
//				}
//				if (options === 'destroy') { // for the destroy method, must remove Calendar object data
//					element.removeData('fullCalendar');
//				}
//			}
//		}
//		// a new calendar initialization
//		else if (!calendar) { // don't initialize twice
//			calendar = new Calendar(element, options);
//			element.data('fullCalendar', calendar);
//			calendar.render();
//		}
//	});
	
//	return res;
//};


//// function for adding/overriding defaults
//function setDefaults(d) {
//	mergeOptions(defaults, d);
//}


//// Recursively combines option hash-objects.
//// Better than `$.extend(true, ...)` because arrays are not traversed/copied.
////
//// called like:
////     mergeOptions(target, obj1, obj2, ...)
////
//function mergeOptions(target) {

//	function mergeIntoTarget(name, value) {
//		if ($.isPlainObject(value) && $.isPlainObject(target[name]) && !isForcedAtomicOption(name)) {
//			// merge into a new object to avoid destruction
//			target[name] = mergeOptions({}, target[name], value); // combine. `value` object takes precedence
//		}
//		else if (value !== undefined) { // only use values that are set and not undefined
//			target[name] = value;
//		}
//	}

//	for (var i=1; i<arguments.length; i++) {
//		$.each(arguments[i], mergeIntoTarget);
//	}

//	return target;
//}


//// overcome sucky view-option-hash and option-merging behavior messing with options it shouldn't
//function isForcedAtomicOption(name) {
//	// Any option that ends in "Time" or "Duration" is probably a Duration,
//	// and these will commonly be specified as plain objects, which we don't want to mess up.
//	return /(Time|Duration)$/.test(name);
//}
//// FIX: find a different solution for view-option-hashes and have a whitelist
//// for options that can be recursively merged.

//;;

////var langOptionHash = {}; // initialized in defaults.js
//fc.langs = langOptionHash; // expose


//// Initialize jQuery UI Datepicker translations while using some of the translations
//// for our own purposes. Will set this as the default language for datepicker.
//// Called from a translation file.
//fc.datepickerLang = function(langCode, datepickerLangCode, options) {
//	var langOptions = langOptionHash[langCode];

//	// initialize FullCalendar's lang hash for this language
//	if (!langOptions) {
//		langOptions = langOptionHash[langCode] = {};
//	}

//	// merge certain Datepicker options into FullCalendar's options
//	mergeOptions(langOptions, {
//		isRTL: options.isRTL,
//		weekNumberTitle: options.weekHeader,
//		titleFormat: {
//			month: options.showMonthAfterYear ?
//				'YYYY[' + options.yearSuffix + '] MMMM' :
//				'MMMM YYYY[' + options.yearSuffix + ']'
//		},
//		defaultButtonText: {
//			// the translations sometimes wrongly contain HTML entities
//			prev: stripHtmlEntities(options.prevText),
//			next: stripHtmlEntities(options.nextText),
//			today: stripHtmlEntities(options.currentText)
//		}
//	});

//	// is jQuery UI Datepicker is on the page?
//	if ($.datepicker) {

//		// Register the language data.
//		// FullCalendar and MomentJS use language codes like "pt-br" but Datepicker
//		// does it like "pt-BR" or if it doesn't have the language, maybe just "pt".
//		// Make an alias so the language can be referenced either way.
//		$.datepicker.regional[datepickerLangCode] =
//			$.datepicker.regional[langCode] = // alias
//				options;

//		// Alias 'en' to the default language data. Do this every time.
//		$.datepicker.regional.en = $.datepicker.regional[''];

//		// Set as Datepicker's global defaults.
//		$.datepicker.setDefaults(options);
//	}
//};


//// Sets FullCalendar-specific translations. Also sets the language as the global default.
//// Called from a translation file.
//fc.lang = function(langCode, options) {
//	var langOptions;

//	if (options) {
//		langOptions = langOptionHash[langCode];

//		// initialize the hash for this language
//		if (!langOptions) {
//			langOptions = langOptionHash[langCode] = {};
//		}

//		mergeOptions(langOptions, options || {});
//	}

//	// set it as the default language for FullCalendar
//	defaults.lang = langCode;
//};
//;;

 
//function Calendar(element, instanceOptions) {
//	var t = this;



//	// Build options object
//	// -----------------------------------------------------------------------------------
//	// Precedence (lowest to highest): defaults, rtlDefaults, langOptions, instanceOptions

//	instanceOptions = instanceOptions || {};

//	var options = mergeOptions({}, defaults, instanceOptions);
//	var langOptions;

//	// determine language options
//	if (options.lang in langOptionHash) {
//		langOptions = langOptionHash[options.lang];
//	}
//	else {
//		langOptions = langOptionHash[defaults.lang];
//	}

//	if (langOptions) { // if language options exist, rebuild...
//		options = mergeOptions({}, defaults, langOptions, instanceOptions);
//	}

//	if (options.isRTL) { // is isRTL, rebuild...
//		options = mergeOptions({}, defaults, rtlDefaults, langOptions || {}, instanceOptions);
//	}


	
//	// Exports
//	// -----------------------------------------------------------------------------------

//	t.options = options;
//	t.render = render;
//	t.destroy = destroy;
//	t.refetchEvents = refetchEvents;
//	t.reportEvents = reportEvents;
//	t.reportEventChange = reportEventChange;
//	t.rerenderEvents = renderEvents; // `renderEvents` serves as a rerender. an API method
//	t.changeView = changeView;
//	t.select = select;
//	t.unselect = unselect;
//	t.prev = prev;
//	t.next = next;
//	t.prevYear = prevYear;
//	t.nextYear = nextYear;
//	t.today = today;
//	t.gotoDate = gotoDate;
//	t.incrementDate = incrementDate;
//	t.zoomTo = zoomTo;
//	t.getDate = getDate;
//	t.getCalendar = getCalendar;
//	t.getView = getView;
//	t.option = option;
//	t.trigger = trigger;



//	// Language-data Internals
//	// -----------------------------------------------------------------------------------
//	// Apply overrides to the current language's data


//	// Returns moment's internal locale data. If doesn't exist, returns English.
//	// Works with moment-pre-2.8
//	function getLocaleData(langCode) {
//		var f = moment.localeData || moment.langData;
//		return f.call(moment, langCode) ||
//			f.call(moment, 'en'); // the newer localData could return null, so fall back to en
//	}


//	var localeData = createObject(getLocaleData(options.lang)); // make a cheap copy

//	if (options.monthNames) {
//		localeData._months = options.monthNames;
//	}
//	if (options.monthNamesShort) {
//		localeData._monthsShort = options.monthNamesShort;
//	}
//	if (options.dayNames) {
//		localeData._weekdays = options.dayNames;
//	}
//	if (options.dayNamesShort) {
//		localeData._weekdaysShort = options.dayNamesShort;
//	}
//	if (options.firstDay != null) {
//		var _week = createObject(localeData._week); // _week: { dow: # }
//		_week.dow = options.firstDay;
//		localeData._week = _week;
//	}



//	// Calendar-specific Date Utilities
//	// -----------------------------------------------------------------------------------


//	t.defaultAllDayEventDuration = moment.duration(options.defaultAllDayEventDuration);
//	t.defaultTimedEventDuration = moment.duration(options.defaultTimedEventDuration);


//	// Builds a moment using the settings of the current calendar: timezone and language.
//	// Accepts anything the vanilla moment() constructor accepts.
//	t.moment = function() {
//		var mom;

//		if (options.timezone === 'local') {
//			mom = fc.moment.apply(null, arguments);

//			// Force the moment to be local, because fc.moment doesn't guarantee it.
//			if (mom.hasTime()) { // don't give ambiguously-timed moments a local zone
//				mom.local();
//			}
//		}
//		else if (options.timezone === 'UTC') {
//			mom = fc.moment.utc.apply(null, arguments); // process as UTC
//		}
//		else {
//			mom = fc.moment.parseZone.apply(null, arguments); // let the input decide the zone
//		}

//		if ('_locale' in mom) { // moment 2.8 and above
//			mom._locale = localeData;
//		}
//		else { // pre-moment-2.8
//			mom._lang = localeData;
//		}

//		return mom;
//	};


//	// Returns a boolean about whether or not the calendar knows how to calculate
//	// the timezone offset of arbitrary dates in the current timezone.
//	t.getIsAmbigTimezone = function() {
//		return options.timezone !== 'local' && options.timezone !== 'UTC';
//	};


//	// Returns a copy of the given date in the current timezone of it is ambiguously zoned.
//	// This will also give the date an unambiguous time.
//	t.rezoneDate = function(date) {
//		return t.moment(date.toArray());
//	};


//	// Returns a moment for the current date, as defined by the client's computer,
//	// or overridden by the `now` option.
//	t.getNow = function() {
//		var now = options.now;
//		if (typeof now === 'function') {
//			now = now();
//		}
//		return t.moment(now);
//	};


//	// Calculates the week number for a moment according to the calendar's
//	// `weekNumberCalculation` setting.
//	t.calculateWeekNumber = function(mom) {
//		var calc = options.weekNumberCalculation;

//		if (typeof calc === 'function') {
//			return calc(mom);
//		}
//		else if (calc === 'local') {
//			return mom.week();
//		}
//		else if (calc.toUpperCase() === 'ISO') {
//			return mom.isoWeek();
//		}
//	};


//	// Get an event's normalized end date. If not present, calculate it from the defaults.
//	t.getEventEnd = function(event) {
//		if (event.end) {
//			return event.end.clone();
//		}
//		else {
//			return t.getDefaultEventEnd(event.allDay, event.start);
//		}
//	};


//	// Given an event's allDay status and start date, return swhat its fallback end date should be.
//	t.getDefaultEventEnd = function(allDay, start) { // TODO: rename to computeDefaultEventEnd
//		var end = start.clone();

//		if (allDay) {
//			end.stripTime().add(t.defaultAllDayEventDuration);
//		}
//		else {
//			end.add(t.defaultTimedEventDuration);
//		}

//		if (t.getIsAmbigTimezone()) {
//			end.stripZone(); // we don't know what the tzo should be
//		}

//		return end;
//	};



//	// Date-formatting Utilities
//	// -----------------------------------------------------------------------------------


//	// Like the vanilla formatRange, but with calendar-specific settings applied.
//	t.formatRange = function(m1, m2, formatStr) {

//		// a function that returns a formatStr // TODO: in future, precompute this
//		if (typeof formatStr === 'function') {
//			formatStr = formatStr.call(t, options, localeData);
//		}

//		return formatRange(m1, m2, formatStr, null, options.isRTL);
//	};


//	// Like the vanilla formatDate, but with calendar-specific settings applied.
//	t.formatDate = function(mom, formatStr) {

//		// a function that returns a formatStr // TODO: in future, precompute this
//		if (typeof formatStr === 'function') {
//			formatStr = formatStr.call(t, options, localeData);
//		}

//		return formatDate(mom, formatStr);
//	};


	
//	// Imports
//	// -----------------------------------------------------------------------------------


//	EventManager.call(t, options);
//	var isFetchNeeded = t.isFetchNeeded;
//	var fetchEvents = t.fetchEvents;



//	// Locals
//	// -----------------------------------------------------------------------------------


//	var _element = element[0];
//	var header;
//	var headerElement;
//	var content;
//	var tm; // for making theme classes
//	var currentView;
//	var suggestedViewHeight;
//	var windowResizeProxy; // wraps the windowResize function
//	var ignoreWindowResize = 0;
//	var date;
//	var events = [];
	
	
	
//	// Main Rendering
//	// -----------------------------------------------------------------------------------


//	if (options.defaultDate != null) {
//		date = t.moment(options.defaultDate);
//	}
//	else {
//		date = t.getNow();
//	}
	
	
//	function render(inc) {
//		if (!content) {
//			initialRender();
//		}
//		else if (elementVisible()) {
//			// mainly for the public API
//			calcSize();
//			renderView(inc);
//		}
//	}
	
	
//	function initialRender() {
//		tm = options.theme ? 'ui' : 'fc';
//		element.addClass('fc');

//		if (options.isRTL) {
//			element.addClass('fc-rtl');
//		}
//		else {
//			element.addClass('fc-ltr');
//		}

//		if (options.theme) {
//			element.addClass('ui-widget');
//		}
//		else {
//			element.addClass('fc-unthemed');
//		}

//		content = $("<div class='fc-view-container'/>").prependTo(element);

//		header = new Header(t, options);
//		headerElement = header.render();
//		if (headerElement) {
//			element.prepend(headerElement);
//		}

//		changeView(options.defaultView);

//		if (options.handleWindowResize) {
//			windowResizeProxy = debounce(windowResize, options.windowResizeDelay); // prevents rapid calls
//			$(window).resize(windowResizeProxy);
//		}
//	}
	
	
//	function destroy() {

//		if (currentView) {
//			currentView.destroy();
//		}

//		header.destroy();
//		content.remove();
//		element.removeClass('fc fc-ltr fc-rtl fc-unthemed ui-widget');

//		$(window).unbind('resize', windowResizeProxy);
//	}
	
	
//	function elementVisible() {
//		return element.is(':visible');
//	}
	
	

//	// View Rendering
//	// -----------------------------------------------------------------------------------


//	function changeView(viewName) {
//		renderView(0, viewName);
//	}


//	// Renders a view because of a date change, view-type change, or for the first time
//	function renderView(delta, viewName) {
//		ignoreWindowResize++;

//		// if viewName is changing, destroy the old view
//		if (currentView && viewName && currentView.name !== viewName) {
//			header.deactivateButton(currentView.name);
//			freezeContentHeight(); // prevent a scroll jump when view element is removed
//			if (currentView.start) { // rendered before?
//				currentView.destroy();
//			}
//			currentView.el.remove();
//			currentView = null;
//		}

//		// if viewName changed, or the view was never created, create a fresh view
//		if (!currentView && viewName) {
//			currentView = new fcViews[viewName](t);
//			currentView.el =  $("<div class='fc-view fc-" + viewName + "-view' />").appendTo(content);
//			header.activateButton(viewName);
//		}

//		if (currentView) {

//			// let the view determine what the delta means
//			if (delta) {
//				date = currentView.incrementDate(date, delta);
//			}

//			// render or rerender the view
//			if (
//				!currentView.start || // never rendered before
//				delta || // explicit date window change
//				!date.isWithin(currentView.intervalStart, currentView.intervalEnd) // implicit date window change
//			) {
//				if (elementVisible()) {

//					freezeContentHeight();
//					if (currentView.start) { // rendered before?
//						currentView.destroy();
//					}
//					currentView.render(date);
//					unfreezeContentHeight();

//					// need to do this after View::render, so dates are calculated
//					updateTitle();
//					updateTodayButton();

//					getAndRenderEvents();
//				}
//			}
//		}

//		unfreezeContentHeight(); // undo any lone freezeContentHeight calls
//		ignoreWindowResize--;
//	}
	
	

//	// Resizing
//	// -----------------------------------------------------------------------------------


//	t.getSuggestedViewHeight = function() {
//		if (suggestedViewHeight === undefined) {
//			calcSize();
//		}
//		return suggestedViewHeight;
//	};


//	t.isHeightAuto = function() {
//		return options.contentHeight === 'auto' || options.height === 'auto';
//	};
	
	
//	function updateSize(shouldRecalc) {
//		if (elementVisible()) {

//			if (shouldRecalc) {
//				_calcSize();
//			}

//			ignoreWindowResize++;
//			currentView.updateSize(true); // isResize=true. will poll getSuggestedViewHeight() and isHeightAuto()
//			ignoreWindowResize--;

//			return true; // signal success
//		}
//	}


//	function calcSize() {
//		if (elementVisible()) {
//			_calcSize();
//		}
//	}
	
	
//	function _calcSize() { // assumes elementVisible
//		if (typeof options.contentHeight === 'number') { // exists and not 'auto'
//			suggestedViewHeight = options.contentHeight;
//		}
//		else if (typeof options.height === 'number') { // exists and not 'auto'
//			suggestedViewHeight = options.height - (headerElement ? headerElement.outerHeight(true) : 0);
//		}
//		else {
//			suggestedViewHeight = Math.round(content.width() / Math.max(options.aspectRatio, .5));
//		}
//	}
	
	
//	function windowResize(ev) {
//		if (
//			!ignoreWindowResize &&
//			ev.target === window && // so we don't process jqui "resize" events that have bubbled up
//			currentView.start // view has already been rendered
//		) {
//			if (updateSize(true)) {
//				currentView.trigger('windowResize', _element);
//			}
//		}
//	}
	
	
	
//	/* Event Fetching/Rendering
//	-----------------------------------------------------------------------------*/
//	// TODO: going forward, most of this stuff should be directly handled by the view


//	function refetchEvents() { // can be called as an API method
//		destroyEvents(); // so that events are cleared before user starts waiting for AJAX
//		fetchAndRenderEvents();
//	}


//	function renderEvents() { // destroys old events if previously rendered
//		if (elementVisible()) {
//			freezeContentHeight();
//			currentView.destroyEvents(); // no performance cost if never rendered
//			currentView.renderEvents(events);
//			unfreezeContentHeight();
//		}
//	}


//	function destroyEvents() {
//		freezeContentHeight();
//		currentView.destroyEvents();
//		unfreezeContentHeight();
//	}
	

//	function getAndRenderEvents() {
//		if (!options.lazyFetching || isFetchNeeded(currentView.start, currentView.end)) {
//			fetchAndRenderEvents();
//		}
//		else {
//			renderEvents();
//		}
//	}


//	function fetchAndRenderEvents() {
//		fetchEvents(currentView.start, currentView.end);
//			// ... will call reportEvents
//			// ... which will call renderEvents
//	}

	
//	// called when event data arrives
//	function reportEvents(_events) {
//		events = _events;
//		renderEvents();
//	}


//	// called when a single event's data has been changed
//	function reportEventChange() {
//		renderEvents();
//	}



//	/* Header Updating
//	-----------------------------------------------------------------------------*/


//	function updateTitle() {
//		header.updateTitle(currentView.title);
//	}


//	function updateTodayButton() {
//		var now = t.getNow();
//		if (now.isWithin(currentView.intervalStart, currentView.intervalEnd)) {
//			header.disableButton('today');
//		}
//		else {
//			header.enableButton('today');
//		}
//	}
	


//	/* Selection
//	-----------------------------------------------------------------------------*/
	

//	function select(start, end) {

//		start = t.moment(start);
//		if (end) {
//			end = t.moment(end);
//		}
//		else if (start.hasTime()) {
//			end = start.clone().add(t.defaultTimedEventDuration);
//		}
//		else {
//			end = start.clone().add(t.defaultAllDayEventDuration);
//		}

//		currentView.select(start, end);
//	}
	

//	function unselect() { // safe to be called before renderView
//		if (currentView) {
//			currentView.unselect();
//		}
//	}
	
	
	
//	/* Date
//	-----------------------------------------------------------------------------*/
	
	
//	function prev() {
//		renderView(-1);
//	}
	
	
//	function next() {
//		renderView(1);
//	}
	
	
//	function prevYear() {
//		date.add(-1, 'years');
//		renderView();
//	}
	
	
//	function nextYear() {
//		date.add(1, 'years');
//		renderView();
//	}
	
	
//	function today() {
//		date = t.getNow();
//		renderView();
//	}
	
	
//	function gotoDate(dateInput) {
//		date = t.moment(dateInput);
//		renderView();
//	}
	
	
//	function incrementDate(delta) {
//		date.add(moment.duration(delta));
//		renderView();
//	}


//	// Forces navigation to a view for the given date.
//	// `viewName` can be a specific view name or a generic one like "week" or "day".
//	function zoomTo(newDate, viewName) {
//		var viewStr;
//		var match;

//		if (!viewName || fcViews[viewName] === undefined) { // a general view name, or "auto"
//			viewName = viewName || 'day';
//			viewStr = header.getViewsWithButtons().join(' '); // space-separated string of all the views in the header

//			// try to match a general view name, like "week", against a specific one, like "agendaWeek"
//			match = viewStr.match(new RegExp('\\w+' + capitaliseFirstLetter(viewName)));

//			// fall back to the day view being used in the header
//			if (!match) {
//				match = viewStr.match(/\w+Day/);
//			}

//			viewName = match ? match[0] : 'agendaDay'; // fall back to agendaDay
//		}

//		date = newDate;
//		changeView(viewName);
//	}
	
	
//	function getDate() {
//		return date.clone();
//	}



//	/* Height "Freezing"
//	-----------------------------------------------------------------------------*/


//	function freezeContentHeight() {
//		content.css({
//			width: '100%',
//			height: content.height(),
//			overflow: 'hidden'
//		});
//	}


//	function unfreezeContentHeight() {
//		content.css({
//			width: '',
//			height: '',
//			overflow: ''
//		});
//	}
	
	
	
//	/* Misc
//	-----------------------------------------------------------------------------*/
	

//	function getCalendar() {
//		return t;
//	}

	
//	function getView() {
//		return currentView;
//	}
	
	
//	function option(name, value) {
//		if (value === undefined) {
//			return options[name];
//		}
//		if (name == 'height' || name == 'contentHeight' || name == 'aspectRatio') {
//			options[name] = value;
//			updateSize(true); // true = allow recalculation of height
//		}
//	}
	
	
//	function trigger(name, thisObj) {
//		if (options[name]) {
//			return options[name].apply(
//				thisObj || _element,
//				Array.prototype.slice.call(arguments, 2)
//			);
//		}
//	}

//}

//;;

///* Top toolbar area with buttons and title
//----------------------------------------------------------------------------------------------------------------------*/
//// TODO: rename all header-related things to "toolbar"

//function Header(calendar, options) {
//	var t = this;
	
//	// exports
//	t.render = render;
//	t.destroy = destroy;
//	t.updateTitle = updateTitle;
//	t.activateButton = activateButton;
//	t.deactivateButton = deactivateButton;
//	t.disableButton = disableButton;
//	t.enableButton = enableButton;
//	t.getViewsWithButtons = getViewsWithButtons;
	
//	// locals
//	var el = $();
//	var viewsWithButtons = [];
//	var tm;


//	function render() {
//		var sections = options.header;

//		tm = options.theme ? 'ui' : 'fc';

//		if (sections) {
//			el = $("<div class='fc-toolbar'/>")
//				.append(renderSection('left'))
//				.append(renderSection('right'))
//				.append(renderSection('center'))
//				.append('<div class="fc-clear"/>');

//			return el;
//		}
//	}
	
	
//	function destroy() {
//		el.remove();
//	}
	
	
//	function renderSection(position) {
//		var sectionEl = $('<div class="fc-' + position + '"/>');
//		var buttonStr = options.header[position];

//		if (buttonStr) {
//			$.each(buttonStr.split(' '), function(i) {
//				var groupChildren = $();
//				var isOnlyButtons = true;
//				var groupEl;

//				$.each(this.split(','), function(j, buttonName) {
//					var buttonClick;
//					var themeIcon;
//					var normalIcon;
//					var defaultText;
//					var customText;
//					var innerHtml;
//					var classes;
//					var button;

//					if (buttonName == 'title') {
//						groupChildren = groupChildren.add($('<h2>&nbsp;</h2>')); // we always want it to take up height
//						isOnlyButtons = false;
//					}
//					else {
//						if (calendar[buttonName]) { // a calendar method
//							buttonClick = function() {
//								calendar[buttonName]();
//							};
//						}
//						else if (fcViews[buttonName]) { // a view name
//							buttonClick = function() {
//								calendar.changeView(buttonName);
//							};
//							viewsWithButtons.push(buttonName);
//						}
//						if (buttonClick) {

//							// smartProperty allows different text per view button (ex: "Agenda Week" vs "Basic Week")
//							themeIcon = smartProperty(options.themeButtonIcons, buttonName);
//							normalIcon = smartProperty(options.buttonIcons, buttonName);
//							defaultText = smartProperty(options.defaultButtonText, buttonName);
//							customText = smartProperty(options.buttonText, buttonName);

//							if (customText) {
//								innerHtml = htmlEscape(customText);
//							}
//							else if (themeIcon && options.theme) {
//								innerHtml = "<span class='ui-icon ui-icon-" + themeIcon + "'></span>";
//							}
//							else if (normalIcon && !options.theme) {
//								innerHtml = "<span class='fc-icon fc-icon-" + normalIcon + "'></span>";
//							}
//							else {
//								innerHtml = htmlEscape(defaultText || buttonName);
//							}

//							classes = [
//								'fc-' + buttonName + '-button',
//								tm + '-button',
//								tm + '-state-default'
//							];

//							button = $( // type="button" so that it doesn't submit a form
//								'<button type="button" class="' + classes.join(' ') + '">' +
//									innerHtml +
//								'</button>'
//								)
//								.click(function() {
//									// don't process clicks for disabled buttons
//									if (!button.hasClass(tm + '-state-disabled')) {

//										buttonClick();

//										// after the click action, if the button becomes the "active" tab, or disabled,
//										// it should never have a hover class, so remove it now.
//										if (
//											button.hasClass(tm + '-state-active') ||
//											button.hasClass(tm + '-state-disabled')
//										) {
//											button.removeClass(tm + '-state-hover');
//										}
//									}
//								})
//								.mousedown(function() {
//									// the *down* effect (mouse pressed in).
//									// only on buttons that are not the "active" tab, or disabled
//									button
//										.not('.' + tm + '-state-active')
//										.not('.' + tm + '-state-disabled')
//										.addClass(tm + '-state-down');
//								})
//								.mouseup(function() {
//									// undo the *down* effect
//									button.removeClass(tm + '-state-down');
//								})
//								.hover(
//									function() {
//										// the *hover* effect.
//										// only on buttons that are not the "active" tab, or disabled
//										button
//											.not('.' + tm + '-state-active')
//											.not('.' + tm + '-state-disabled')
//											.addClass(tm + '-state-hover');
//									},
//									function() {
//										// undo the *hover* effect
//										button
//											.removeClass(tm + '-state-hover')
//											.removeClass(tm + '-state-down'); // if mouseleave happens before mouseup
//									}
//								);

//							groupChildren = groupChildren.add(button);
//						}
//					}
//				});

//				if (isOnlyButtons) {
//					groupChildren
//						.first().addClass(tm + '-corner-left').end()
//						.last().addClass(tm + '-corner-right').end();
//				}

//				if (groupChildren.length > 1) {
//					groupEl = $('<div/>');
//					if (isOnlyButtons) {
//						groupEl.addClass('fc-button-group');
//					}
//					groupEl.append(groupChildren);
//					sectionEl.append(groupEl);
//				}
//				else {
//					sectionEl.append(groupChildren); // 1 or 0 children
//				}
//			});
//		}

//		return sectionEl;
//	}
	
	
//	function updateTitle(text) {
//		el.find('h2').text(text);
//	}
	
	
//	function activateButton(buttonName) {
//		el.find('.fc-' + buttonName + '-button')
//			.addClass(tm + '-state-active');
//	}
	
	
//	function deactivateButton(buttonName) {
//		el.find('.fc-' + buttonName + '-button')
//			.removeClass(tm + '-state-active');
//	}
	
	
//	function disableButton(buttonName) {
//		el.find('.fc-' + buttonName + '-button')
//			.attr('disabled', 'disabled')
//			.addClass(tm + '-state-disabled');
//	}
	
	
//	function enableButton(buttonName) {
//		el.find('.fc-' + buttonName + '-button')
//			.removeAttr('disabled')
//			.removeClass(tm + '-state-disabled');
//	}


//	function getViewsWithButtons() {
//		return viewsWithButtons;
//	}

//}

//;;

//fc.sourceNormalizers = [];
//fc.sourceFetchers = [];

//var ajaxDefaults = {
//	dataType: 'json',
//	cache: false
//};

//var eventGUID = 1;


//function EventManager(options) { // assumed to be a calendar
//	var t = this;
	
	
//	// exports
//	t.isFetchNeeded = isFetchNeeded;
//	t.fetchEvents = fetchEvents;
//	t.addEventSource = addEventSource;
//	t.removeEventSource = removeEventSource;
//	t.updateEvent = updateEvent;
//	t.renderEvent = renderEvent;
//	t.removeEvents = removeEvents;
//	t.clientEvents = clientEvents;
//	t.mutateEvent = mutateEvent;
	
	
//	// imports
//	var trigger = t.trigger;
//	var getView = t.getView;
//	var reportEvents = t.reportEvents;
//	var getEventEnd = t.getEventEnd;
	
	
//	// locals
//	var stickySource = { events: [] };
//	var sources = [ stickySource ];
//	var rangeStart, rangeEnd;
//	var currentFetchID = 0;
//	var pendingSourceCnt = 0;
//	var loadingLevel = 0;
//	var cache = [];


//	$.each(
//		(options.events ? [ options.events ] : []).concat(options.eventSources || []),
//		function(i, sourceInput) {
//			var source = buildEventSource(sourceInput);
//			if (source) {
//				sources.push(source);
//			}
//		}
//	);
	
	
	
//	/* Fetching
//	-----------------------------------------------------------------------------*/
	
	
//	function isFetchNeeded(start, end) {
//		return !rangeStart || // nothing has been fetched yet?
//			// or, a part of the new range is outside of the old range? (after normalizing)
//			start.clone().stripZone() < rangeStart.clone().stripZone() ||
//			end.clone().stripZone() > rangeEnd.clone().stripZone();
//	}
	
	
//	function fetchEvents(start, end) {
//		rangeStart = start;
//		rangeEnd = end;
//		cache = [];
//		var fetchID = ++currentFetchID;
//		var len = sources.length;
//		pendingSourceCnt = len;
//		for (var i=0; i<len; i++) {
//			fetchEventSource(sources[i], fetchID);
//		}
//	}
	
	
//	function fetchEventSource(source, fetchID) {
//		_fetchEventSource(source, function(events) {
//			var isArraySource = $.isArray(source.events);
//			var i;
//			var event;

//			if (fetchID == currentFetchID) {

//				if (events) {
//					for (i=0; i<events.length; i++) {
//						event = events[i];

//						// event array sources have already been convert to Event Objects
//						if (!isArraySource) {
//							event = buildEvent(event, source);
//						}

//						if (event) {
//							cache.push(event);
//						}
//					}
//				}

//				pendingSourceCnt--;
//				if (!pendingSourceCnt) {
//					reportEvents(cache);
//				}
//			}
//		});
//	}
	
	
//	function _fetchEventSource(source, callback) {
//		var i;
//		var fetchers = fc.sourceFetchers;
//		var res;

//		for (i=0; i<fetchers.length; i++) {
//			res = fetchers[i].call(
//				t, // this, the Calendar object
//				source,
//				rangeStart.clone(),
//				rangeEnd.clone(),
//				options.timezone,
//				callback
//			);

//			if (res === true) {
//				// the fetcher is in charge. made its own async request
//				return;
//			}
//			else if (typeof res == 'object') {
//				// the fetcher returned a new source. process it
//				_fetchEventSource(res, callback);
//				return;
//			}
//		}

//		var events = source.events;
//		if (events) {
//			if ($.isFunction(events)) {
//				pushLoading();
//				events.call(
//					t, // this, the Calendar object
//					rangeStart.clone(),
//					rangeEnd.clone(),
//					options.timezone,
//					function(events) {
//						callback(events);
//						popLoading();
//					}
//				);
//			}
//			else if ($.isArray(events)) {
//				callback(events);
//			}
//			else {
//				callback();
//			}
//		}else{
//			var url = source.url;
//			if (url) {
//				var success = source.success;
//				var error = source.error;
//				var complete = source.complete;

//				// retrieve any outbound GET/POST $.ajax data from the options
//				var customData;
//				if ($.isFunction(source.data)) {
//					// supplied as a function that returns a key/value object
//					customData = source.data();
//				}
//				else {
//					// supplied as a straight key/value object
//					customData = source.data;
//				}

//				// use a copy of the custom data so we can modify the parameters
//				// and not affect the passed-in object.
//				var data = $.extend({}, customData || {});

//				var startParam = firstDefined(source.startParam, options.startParam);
//				var endParam = firstDefined(source.endParam, options.endParam);
//				var timezoneParam = firstDefined(source.timezoneParam, options.timezoneParam);

//				if (startParam) {
//					data[startParam] = rangeStart.format();
//				}
//				if (endParam) {
//					data[endParam] = rangeEnd.format();
//				}
//				if (options.timezone && options.timezone != 'local') {
//					data[timezoneParam] = options.timezone;
//				}

//				pushLoading();
//				$.ajax($.extend({}, ajaxDefaults, source, {
//					data: data,
//					success: function(events) {
//						events = events || [];
//						var res = applyAll(success, this, arguments);
//						if ($.isArray(res)) {
//							events = res;
//						}
//						callback(events);
//					},
//					error: function() {
//						applyAll(error, this, arguments);
//						callback();
//					},
//					complete: function() {
//						applyAll(complete, this, arguments);
//						popLoading();
//					}
//				}));
//			}else{
//				callback();
//			}
//		}
//	}
	
	
	
//	/* Sources
//	-----------------------------------------------------------------------------*/
	

//	function addEventSource(sourceInput) {
//		var source = buildEventSource(sourceInput);
//		if (source) {
//			sources.push(source);
//			pendingSourceCnt++;
//			fetchEventSource(source, currentFetchID); // will eventually call reportEvents
//		}
//	}


//	function buildEventSource(sourceInput) { // will return undefined if invalid source
//		var normalizers = fc.sourceNormalizers;
//		var source;
//		var i;

//		if ($.isFunction(sourceInput) || $.isArray(sourceInput)) {
//			source = { events: sourceInput };
//		}
//		else if (typeof sourceInput === 'string') {
//			source = { url: sourceInput };
//		}
//		else if (typeof sourceInput === 'object') {
//			source = $.extend({}, sourceInput); // shallow copy
//		}

//		if (source) {

//			// TODO: repeat code, same code for event classNames
//			if (source.className) {
//				if (typeof source.className === 'string') {
//					source.className = source.className.split(/\s+/);
//				}
//				// otherwise, assumed to be an array
//			}
//			else {
//				source.className = [];
//			}

//			// for array sources, we convert to standard Event Objects up front
//			if ($.isArray(source.events)) {
//				source.origArray = source.events; // for removeEventSource
//				source.events = $.map(source.events, function(eventInput) {
//					return buildEvent(eventInput, source);
//				});
//			}

//			for (i=0; i<normalizers.length; i++) {
//				normalizers[i].call(t, source);
//			}

//			return source;
//		}
//	}


//	function removeEventSource(source) {
//		sources = $.grep(sources, function(src) {
//			return !isSourcesEqual(src, source);
//		});
//		// remove all client events from that source
//		cache = $.grep(cache, function(e) {
//			return !isSourcesEqual(e.source, source);
//		});
//		reportEvents(cache);
//	}


//	function isSourcesEqual(source1, source2) {
//		return source1 && source2 && getSourcePrimitive(source1) == getSourcePrimitive(source2);
//	}


//	function getSourcePrimitive(source) {
//		return (
//			(typeof source === 'object') ? // a normalized event source?
//				(source.origArray || source.url || source.events) : // get the primitive
//				null
//		) ||
//		source; // the given argument *is* the primitive
//	}
	
	
	
//	/* Manipulation
//	-----------------------------------------------------------------------------*/


//	function updateEvent(event) {

//		event.start = t.moment(event.start);
//		if (event.end) {
//			event.end = t.moment(event.end);
//		}

//		mutateEvent(event);
//		propagateMiscProperties(event);
//		reportEvents(cache); // reports event modifications (so we can redraw)
//	}


//	var miscCopyableProps = [
//		'title',
//		'url',
//		'allDay',
//		'className',
//		'editable',
//		'color',
//		'backgroundColor',
//		'borderColor',
//		'textColor'
//	];

//	function propagateMiscProperties(event) {
//		var i;
//		var cachedEvent;
//		var j;
//		var prop;

//		for (i=0; i<cache.length; i++) {
//			cachedEvent = cache[i];
//			if (cachedEvent._id == event._id && cachedEvent !== event) {
//				for (j=0; j<miscCopyableProps.length; j++) {
//					prop = miscCopyableProps[j];
//					if (event[prop] !== undefined) {
//						cachedEvent[prop] = event[prop];
//					}
//				}
//			}
//		}
//	}

	
	
//	function renderEvent(eventData, stick) {
//		var event = buildEvent(eventData);
//		if (event) {
//			if (!event.source) {
//				if (stick) {
//					stickySource.events.push(event);
//					event.source = stickySource;
//				}
//				cache.push(event);
//			}
//			reportEvents(cache);
//		}
//	}
	
	
//	function removeEvents(filter) {
//		var eventID;
//		var i;

//		if (filter == null) { // null or undefined. remove all events
//			filter = function() { return true; }; // will always match
//		}
//		else if (!$.isFunction(filter)) { // an event ID
//			eventID = filter + '';
//			filter = function(event) {
//				return event._id == eventID;
//			};
//		}

//		// Purge event(s) from our local cache
//		cache = $.grep(cache, filter, true); // inverse=true

//		// Remove events from array sources.
//		// This works because they have been converted to official Event Objects up front.
//		// (and as a result, event._id has been calculated).
//		for (i=0; i<sources.length; i++) {
//			if ($.isArray(sources[i].events)) {
//				sources[i].events = $.grep(sources[i].events, filter, true);
//			}
//		}

//		reportEvents(cache);
//	}
	
	
//	function clientEvents(filter) {
//		if ($.isFunction(filter)) {
//			return $.grep(cache, filter);
//		}
//		else if (filter != null) { // not null, not undefined. an event ID
//			filter += '';
//			return $.grep(cache, function(e) {
//				return e._id == filter;
//			});
//		}
//		return cache; // else, return all
//	}
	
	
	
//	/* Loading State
//	-----------------------------------------------------------------------------*/
	
	
//	function pushLoading() {
//		if (!(loadingLevel++)) {
//			trigger('loading', null, true, getView());
//		}
//	}
	
	
//	function popLoading() {
//		if (!(--loadingLevel)) {
//			trigger('loading', null, false, getView());
//		}
//	}
	
	
	
//	/* Event Normalization
//	-----------------------------------------------------------------------------*/

//	function buildEvent(data, source) { // source may be undefined!
//		var out = {};
//		var start;
//		var end;
//		var allDay;
//		var allDayDefault;

//		if (options.eventDataTransform) {
//			data = options.eventDataTransform(data);
//		}
//		if (source && source.eventDataTransform) {
//			data = source.eventDataTransform(data);
//		}

//		start = t.moment(data.start || data.date); // "date" is an alias for "start"
//		if (!start.isValid()) {
//			return;
//		}

//		end = null;
//		if (data.end) {
//			end = t.moment(data.end);
//			if (!end.isValid()) {
//				return;
//			}
//		}

//		allDay = data.allDay;
//		if (allDay === undefined) {
//			allDayDefault = firstDefined(
//				source ? source.allDayDefault : undefined,
//				options.allDayDefault
//			);
//			if (allDayDefault !== undefined) {
//				// use the default
//				allDay = allDayDefault;
//			}
//			else {
//				// all dates need to have ambig time for the event to be considered allDay
//				allDay = !start.hasTime() && (!end || !end.hasTime());
//			}
//		}

//		// normalize the date based on allDay
//		if (allDay) {
//			// neither date should have a time
//			if (start.hasTime()) {
//				start.stripTime();
//			}
//			if (end && end.hasTime()) {
//				end.stripTime();
//			}
//		}
//		else {
//			// force a time/zone up the dates
//			if (!start.hasTime()) {
//				start = t.rezoneDate(start);
//			}
//			if (end && !end.hasTime()) {
//				end = t.rezoneDate(end);
//			}
//		}

//		// Copy all properties over to the resulting object.
//		// The special-case properties will be copied over afterwards.
//		$.extend(out, data);

//		if (source) {
//			out.source = source;
//		}

//		out._id = data._id || (data.id === undefined ? '_fc' + eventGUID++ : data.id + '');

//		if (data.className) {
//			if (typeof data.className == 'string') {
//				out.className = data.className.split(/\s+/);
//			}
//			else { // assumed to be an array
//				out.className = data.className;
//			}
//		}
//		else {
//			out.className = [];
//		}

//		out.allDay = allDay;
//		out.start = start;
//		out.end = end;

//		if (options.forceEventDuration && !out.end) {
//			out.end = getEventEnd(out);
//		}

//		backupEventDates(out);

//		return out;
//	}



//	/* Event Modification Math
//	-----------------------------------------------------------------------------------------*/


//	// Modify the date(s) of an event and make this change propagate to all other events with
//	// the same ID (related repeating events).
//	//
//	// If `newStart`/`newEnd` are not specified, the "new" dates are assumed to be `event.start` and `event.end`.
//	// The "old" dates to be compare against are always `event._start` and `event._end` (set by EventManager).
//	//
//	// Returns an object with delta information and a function to undo all operations.
//	//
//	function mutateEvent(event, newStart, newEnd) {
//		var oldAllDay = event._allDay;
//		var oldStart = event._start;
//		var oldEnd = event._end;
//		var clearEnd = false;
//		var newAllDay;
//		var dateDelta;
//		var durationDelta;
//		var undoFunc;

//		// if no new dates were passed in, compare against the event's existing dates
//		if (!newStart && !newEnd) {
//			newStart = event.start;
//			newEnd = event.end;
//		}

//		// NOTE: throughout this function, the initial values of `newStart` and `newEnd` are
//		// preserved. These values may be undefined.

//		// detect new allDay
//		if (event.allDay != oldAllDay) { // if value has changed, use it
//			newAllDay = event.allDay;
//		}
//		else { // otherwise, see if any of the new dates are allDay
//			newAllDay = !(newStart || newEnd).hasTime();
//		}

//		// normalize the new dates based on allDay
//		if (newAllDay) {
//			if (newStart) {
//				newStart = newStart.clone().stripTime();
//			}
//			if (newEnd) {
//				newEnd = newEnd.clone().stripTime();
//			}
//		}

//		// compute dateDelta
//		if (newStart) {
//			if (newAllDay) {
//				dateDelta = dayishDiff(newStart, oldStart.clone().stripTime()); // treat oldStart as allDay
//			}
//			else {
//				dateDelta = dayishDiff(newStart, oldStart);
//			}
//		}

//		if (newAllDay != oldAllDay) {
//			// if allDay has changed, always throw away the end
//			clearEnd = true;
//		}
//		else if (newEnd) {
//			durationDelta = dayishDiff(
//				// new duration
//				newEnd || t.getDefaultEventEnd(newAllDay, newStart || oldStart),
//				newStart || oldStart
//			).subtract(dayishDiff(
//				// subtract old duration
//				oldEnd || t.getDefaultEventEnd(oldAllDay, oldStart),
//				oldStart
//			));
//		}

//		undoFunc = mutateEvents(
//			clientEvents(event._id), // get events with this ID
//			clearEnd,
//			newAllDay,
//			dateDelta,
//			durationDelta
//		);

//		return {
//			dateDelta: dateDelta,
//			durationDelta: durationDelta,
//			undo: undoFunc
//		};
//	}


//	// Modifies an array of events in the following ways (operations are in order):
//	// - clear the event's `end`
//	// - convert the event to allDay
//	// - add `dateDelta` to the start and end
//	// - add `durationDelta` to the event's duration
//	//
//	// Returns a function that can be called to undo all the operations.
//	//
//	function mutateEvents(events, clearEnd, forceAllDay, dateDelta, durationDelta) {
//		var isAmbigTimezone = t.getIsAmbigTimezone();
//		var undoFunctions = [];

//		$.each(events, function(i, event) {
//			var oldAllDay = event._allDay;
//			var oldStart = event._start;
//			var oldEnd = event._end;
//			var newAllDay = forceAllDay != null ? forceAllDay : oldAllDay;
//			var newStart = oldStart.clone();
//			var newEnd = (!clearEnd && oldEnd) ? oldEnd.clone() : null;

//			// NOTE: this function is responsible for transforming `newStart` and `newEnd`,
//			// which were initialized to the OLD values first. `newEnd` may be null.

//			// normlize newStart/newEnd to be consistent with newAllDay
//			if (newAllDay) {
//				newStart.stripTime();
//				if (newEnd) {
//					newEnd.stripTime();
//				}
//			}
//			else {
//				if (!newStart.hasTime()) {
//					newStart = t.rezoneDate(newStart);
//				}
//				if (newEnd && !newEnd.hasTime()) {
//					newEnd = t.rezoneDate(newEnd);
//				}
//			}

//			// ensure we have an end date if necessary
//			if (!newEnd && (options.forceEventDuration || +durationDelta)) {
//				newEnd = t.getDefaultEventEnd(newAllDay, newStart);
//			}

//			// translate the dates
//			newStart.add(dateDelta);
//			if (newEnd) {
//				newEnd.add(dateDelta).add(durationDelta);
//			}

//			// if the dates have changed, and we know it is impossible to recompute the
//			// timezone offsets, strip the zone.
//			if (isAmbigTimezone) {
//				if (+dateDelta || +durationDelta) {
//					newStart.stripZone();
//					if (newEnd) {
//						newEnd.stripZone();
//					}
//				}
//			}

//			event.allDay = newAllDay;
//			event.start = newStart;
//			event.end = newEnd;
//			backupEventDates(event);

//			undoFunctions.push(function() {
//				event.allDay = oldAllDay;
//				event.start = oldStart;
//				event.end = oldEnd;
//				backupEventDates(event);
//			});
//		});

//		return function() {
//			for (var i=0; i<undoFunctions.length; i++) {
//				undoFunctions[i]();
//			}
//		};
//	}

//}


//// updates the "backup" properties, which are preserved in order to compute diffs later on.
//function backupEventDates(event) {
//	event._allDay = event.allDay;
//	event._start = event.start.clone();
//	event._end = event.end ? event.end.clone() : null;
//}

//;;

///* FullCalendar-specific DOM Utilities
//----------------------------------------------------------------------------------------------------------------------*/


//// Given the scrollbar widths of some other container, create borders/margins on rowEls in order to match the left
//// and right space that was offset by the scrollbars. A 1-pixel border first, then margin beyond that.
//function compensateScroll(rowEls, scrollbarWidths) {
//	if (scrollbarWidths.left) {
//		rowEls.css({
//			'border-left-width': 1,
//			'margin-left': scrollbarWidths.left - 1
//		});
//	}
//	if (scrollbarWidths.right) {
//		rowEls.css({
//			'border-right-width': 1,
//			'margin-right': scrollbarWidths.right - 1
//		});
//	}
//}


//// Undoes compensateScroll and restores all borders/margins
//function uncompensateScroll(rowEls) {
//	rowEls.css({
//		'margin-left': '',
//		'margin-right': '',
//		'border-left-width': '',
//		'border-right-width': ''
//	});
//}


//// Given a total available height to fill, have `els` (essentially child rows) expand to accomodate.
//// By default, all elements that are shorter than the recommended height are expanded uniformly, not considering
//// any other els that are already too tall. if `shouldRedistribute` is on, it considers these tall rows and 
//// reduces the available height.
//function distributeHeight(els, availableHeight, shouldRedistribute) {

//	// *FLOORING NOTE*: we floor in certain places because zoom can give inaccurate floating-point dimensions,
//	// and it is better to be shorter than taller, to avoid creating unnecessary scrollbars.

//	var minOffset1 = Math.floor(availableHeight / els.length); // for non-last element
//	var minOffset2 = Math.floor(availableHeight - minOffset1 * (els.length - 1)); // for last element *FLOORING NOTE*
//	var flexEls = []; // elements that are allowed to expand. array of DOM nodes
//	var flexOffsets = []; // amount of vertical space it takes up
//	var flexHeights = []; // actual css height
//	var usedHeight = 0;

//	undistributeHeight(els); // give all elements their natural height

//	// find elements that are below the recommended height (expandable).
//	// important to query for heights in a single first pass (to avoid reflow oscillation).
//	els.each(function(i, el) {
//		var minOffset = i === els.length - 1 ? minOffset2 : minOffset1;
//		var naturalOffset = $(el).outerHeight(true);

//		if (naturalOffset < minOffset) {
//			flexEls.push(el);
//			flexOffsets.push(naturalOffset);
//			flexHeights.push($(el).height());
//		}
//		else {
//			// this element stretches past recommended height (non-expandable). mark the space as occupied.
//			usedHeight += naturalOffset;
//		}
//	});

//	// readjust the recommended height to only consider the height available to non-maxed-out rows.
//	if (shouldRedistribute) {
//		availableHeight -= usedHeight;
//		minOffset1 = Math.floor(availableHeight / flexEls.length);
//		minOffset2 = Math.floor(availableHeight - minOffset1 * (flexEls.length - 1)); // *FLOORING NOTE*
//	}

//	// assign heights to all expandable elements
//	$(flexEls).each(function(i, el) {
//		var minOffset = i === flexEls.length - 1 ? minOffset2 : minOffset1;
//		var naturalOffset = flexOffsets[i];
//		var naturalHeight = flexHeights[i];
//		var newHeight = minOffset - (naturalOffset - naturalHeight); // subtract the margin/padding

//		if (naturalOffset < minOffset) { // we check this again because redistribution might have changed things
//			$(el).height(newHeight);
//		}
//	});
//}


//// Undoes distrubuteHeight, restoring all els to their natural height
//function undistributeHeight(els) {
//	els.height('');
//}


//// Given `els`, a jQuery set of <td> cells, find the cell with the largest natural width and set the widths of all the
//// cells to be that width.
//// PREREQUISITE: if you want a cell to take up width, it needs to have a single inner element w/ display:inline
//function matchCellWidths(els) {
//	var maxInnerWidth = 0;

//	els.find('> *').each(function(i, innerEl) {
//		var innerWidth = $(innerEl).outerWidth();
//		if (innerWidth > maxInnerWidth) {
//			maxInnerWidth = innerWidth;
//		}
//	});

//	maxInnerWidth++; // sometimes not accurate of width the text needs to stay on one line. insurance

//	els.width(maxInnerWidth);

//	return maxInnerWidth;
//}


//// Turns a container element into a scroller if its contents is taller than the allotted height.
//// Returns true if the element is now a scroller, false otherwise.
//// NOTE: this method is best because it takes weird zooming dimensions into account
//function setPotentialScroller(containerEl, height) {
//	containerEl.height(height).addClass('fc-scroller');

//	// are scrollbars needed?
//	if (containerEl[0].scrollHeight - 1 > containerEl[0].clientHeight) { // !!! -1 because IE is often off-by-one :(
//		return true;
//	}

//	unsetScroller(containerEl); // undo
//	return false;
//}


//// Takes an element that might have been a scroller, and turns it back into a normal element.
//function unsetScroller(containerEl) {
//	containerEl.height('').removeClass('fc-scroller');
//}


///* General DOM Utilities
//----------------------------------------------------------------------------------------------------------------------*/


//// borrowed from https://github.com/jquery/jquery-ui/blob/1.11.0/ui/core.js#L51
//function getScrollParent(el) {
//	var position = el.css('position'),
//		scrollParent = el.parents().filter(function() {
//			var parent = $(this);
//			return (/(auto|scroll)/).test(
//				parent.css('overflow') + parent.css('overflow-y') + parent.css('overflow-x')
//			);
//		}).eq(0);

//	return position === 'fixed' || !scrollParent.length ? $(el[0].ownerDocument || document) : scrollParent;
//}


//// Given a container element, return an object with the pixel values of the left/right scrollbars.
//// Left scrollbars might occur on RTL browsers (IE maybe?) but I have not tested.
//// PREREQUISITE: container element must have a single child with display:block
//function getScrollbarWidths(container) {
//	var containerLeft = container.offset().left;
//	var containerRight = containerLeft + container.width();
//	var inner = container.children();
//	var innerLeft = inner.offset().left;
//	var innerRight = innerLeft + inner.outerWidth();

//	return {
//		left: innerLeft - containerLeft,
//		right: containerRight - innerRight
//	};
//}


//// Returns a boolean whether this was a left mouse click and no ctrl key (which means right click on Mac)
//function isPrimaryMouseButton(ev) {
//	return ev.which == 1 && !ev.ctrlKey;
//}


///* FullCalendar-specific Misc Utilities
//----------------------------------------------------------------------------------------------------------------------*/


//// Creates a basic segment with the intersection of the two ranges. Returns undefined if no intersection.
//// Expects all dates to be normalized to the same timezone beforehand.
//function intersectionToSeg(subjectStart, subjectEnd, intervalStart, intervalEnd) {
//	var segStart, segEnd;
//	var isStart, isEnd;

//	if (subjectEnd > intervalStart && subjectStart < intervalEnd) { // in bounds at all?

//		if (subjectStart >= intervalStart) {
//			segStart = subjectStart.clone();
//			isStart = true;
//		}
//		else {
//			segStart = intervalStart.clone();
//			isStart =  false;
//		}

//		if (subjectEnd <= intervalEnd) {
//			segEnd = subjectEnd.clone();
//			isEnd = true;
//		}
//		else {
//			segEnd = intervalEnd.clone();
//			isEnd = false;
//		}

//		return {
//			start: segStart,
//			end: segEnd,
//			isStart: isStart,
//			isEnd: isEnd
//		};
//	}
//}


//function smartProperty(obj, name) { // get a camel-cased/namespaced property of an object
//	obj = obj || {};
//	if (obj[name] !== undefined) {
//		return obj[name];
//	}
//	var parts = name.split(/(?=[A-Z])/),
//		i = parts.length - 1, res;
//	for (; i>=0; i--) {
//		res = obj[parts[i].toLowerCase()];
//		if (res !== undefined) {
//			return res;
//		}
//	}
//	return obj['default'];
//}


///* Date Utilities
//----------------------------------------------------------------------------------------------------------------------*/

//var dayIDs = [ 'sun', 'mon', 'tue', 'wed', 'thu', 'fri', 'sat' ];


//// Diffs the two moments into a Duration where full-days are recorded first, then the remaining time.
//// Moments will have their timezones normalized.
//function dayishDiff(a, b) {
//	return moment.duration({
//		days: a.clone().stripTime().diff(b.clone().stripTime(), 'days'),
//		ms: a.time() - b.time()
//	});
//}


//function isNativeDate(input) {
//	return  Object.prototype.toString.call(input) === '[object Date]' || input instanceof Date;
//}


//function dateCompare(a, b) { // works with Moments and native Dates
//	return a - b;
//}


///* General Utilities
//----------------------------------------------------------------------------------------------------------------------*/

//fc.applyAll = applyAll; // export


//// Create an object that has the given prototype. Just like Object.create
//function createObject(proto) {
//	var f = function() {};
//	f.prototype = proto;
//	return new f();
//}


//// Copies specifically-owned (non-protoype) properties of `b` onto `a`.
//// FYI, $.extend would copy *all* properties of `b` onto `a`.
//function extend(a, b) {
//	for (var i in b) {
//		if (b.hasOwnProperty(i)) {
//			a[i] = b[i];
//		}
//	}
//}


//function applyAll(functions, thisObj, args) {
//	if ($.isFunction(functions)) {
//		functions = [ functions ];
//	}
//	if (functions) {
//		var i;
//		var ret;
//		for (i=0; i<functions.length; i++) {
//			ret = functions[i].apply(thisObj, args) || ret;
//		}
//		return ret;
//	}
//}


//function firstDefined() {
//	for (var i=0; i<arguments.length; i++) {
//		if (arguments[i] !== undefined) {
//			return arguments[i];
//		}
//	}
//}


//function htmlEscape(s) {
//	return (s + '').replace(/&/g, '&amp;')
//		.replace(/</g, '&lt;')
//		.replace(/>/g, '&gt;')
//		.replace(/'/g, '&#039;')
//		.replace(/"/g, '&quot;')
//		.replace(/\n/g, '<br />');
//}


//function stripHtmlEntities(text) {
//	return text.replace(/&.*?;/g, '');
//}


//function capitaliseFirstLetter(str) {
//	return str.charAt(0).toUpperCase() + str.slice(1);
//}


//// Returns a function, that, as long as it continues to be invoked, will not
//// be triggered. The function will be called after it stops being called for
//// N milliseconds.
//// https://github.com/jashkenas/underscore/blob/1.6.0/underscore.js#L714
//function debounce(func, wait) {
//	var timeoutId;
//	var args;
//	var context;
//	var timestamp; // of most recent call
//	var later = function() {
//		var last = +new Date() - timestamp;
//		if (last < wait && last > 0) {
//			timeoutId = setTimeout(later, wait - last);
//		}
//		else {
//			timeoutId = null;
//			func.apply(context, args);
//			if (!timeoutId) {
//				context = args = null;
//			}
//		}
//	};

//	return function() {
//		context = this;
//		args = arguments;
//		timestamp = +new Date();
//		if (!timeoutId) {
//			timeoutId = setTimeout(later, wait);
//		}
//	};
//}

//;;

//var ambigDateOfMonthRegex = /^\s*\d{4}-\d\d$/;
//var ambigTimeOrZoneRegex =
//	/^\s*\d{4}-(?:(\d\d-\d\d)|(W\d\d$)|(W\d\d-\d)|(\d\d\d))((T| )(\d\d(:\d\d(:\d\d(\.\d+)?)?)?)?)?$/;


//// Creating
//// -------------------------------------------------------------------------------------------------

//// Creates a new moment, similar to the vanilla moment(...) constructor, but with
//// extra features (ambiguous time, enhanced formatting). When gived an existing moment,
//// it will function as a clone (and retain the zone of the moment). Anything else will
//// result in a moment in the local zone.
//fc.moment = function() {
//	return makeMoment(arguments);
//};

//// Sames as fc.moment, but forces the resulting moment to be in the UTC timezone.
//fc.moment.utc = function() {
//	var mom = makeMoment(arguments, true);

//	// Force it into UTC because makeMoment doesn't guarantee it.
//	if (mom.hasTime()) { // don't give ambiguously-timed moments a UTC zone
//		mom.utc();
//	}

//	return mom;
//};

//// Same as fc.moment, but when given an ISO8601 string, the timezone offset is preserved.
//// ISO8601 strings with no timezone offset will become ambiguously zoned.
//fc.moment.parseZone = function() {
//	return makeMoment(arguments, true, true);
//};

//// Builds an FCMoment from args. When given an existing moment, it clones. When given a native
//// Date, or called with no arguments (the current time), the resulting moment will be local.
//// Anything else needs to be "parsed" (a string or an array), and will be affected by:
////    parseAsUTC - if there is no zone information, should we parse the input in UTC?
////    parseZone - if there is zone information, should we force the zone of the moment?
//function makeMoment(args, parseAsUTC, parseZone) {
//	var input = args[0];
//	var isSingleString = args.length == 1 && typeof input === 'string';
//	var isAmbigTime;
//	var isAmbigZone;
//	var ambigMatch;
//	var output; // an object with fields for the new FCMoment object

//	if (moment.isMoment(input)) {
//		output = moment.apply(null, args); // clone it

//		// the ambig properties have not been preserved in the clone, so reassign them
//		if (input._ambigTime) {
//			output._ambigTime = true;
//		}
//		if (input._ambigZone) {
//			output._ambigZone = true;
//		}
//	}
//	else if (isNativeDate(input) || input === undefined) {
//		output = moment.apply(null, args); // will be local
//	}
//	else { // "parsing" is required
//		isAmbigTime = false;
//		isAmbigZone = false;

//		if (isSingleString) {
//			if (ambigDateOfMonthRegex.test(input)) {
//				// accept strings like '2014-05', but convert to the first of the month
//				input += '-01';
//				args = [ input ]; // for when we pass it on to moment's constructor
//				isAmbigTime = true;
//				isAmbigZone = true;
//			}
//			else if ((ambigMatch = ambigTimeOrZoneRegex.exec(input))) {
//				isAmbigTime = !ambigMatch[5]; // no time part?
//				isAmbigZone = true;
//			}
//		}
//		else if ($.isArray(input)) {
//			// arrays have no timezone information, so assume ambiguous zone
//			isAmbigZone = true;
//		}
//		// otherwise, probably a string with a format

//		if (parseAsUTC) {
//			output = moment.utc.apply(moment, args);
//		}
//		else {
//			output = moment.apply(null, args);
//		}

//		if (isAmbigTime) {
//			output._ambigTime = true;
//			output._ambigZone = true; // ambiguous time always means ambiguous zone
//		}
//		else if (parseZone) { // let's record the inputted zone somehow
//			if (isAmbigZone) {
//				output._ambigZone = true;
//			}
//			else if (isSingleString) {
//				output.zone(input); // if not a valid zone, will assign UTC
//			}
//		}
//	}

//	return new FCMoment(output);
//}

//// Our subclass of Moment.
//// Accepts an object with the internal Moment properties that should be copied over to
//// `this` object (most likely another Moment object). The values in this data must not
//// be referenced by anything else (two moments sharing a Date object for example).
//function FCMoment(internalData) {
//	extend(this, internalData);
//}

//// Chain the prototype to Moment's
//FCMoment.prototype = createObject(moment.fn);

//// We need this because Moment's implementation won't create an FCMoment,
//// nor will it copy over the ambig flags.
//FCMoment.prototype.clone = function() {
//	return makeMoment([ this ]);
//};


//// Time-of-day
//// -------------------------------------------------------------------------------------------------

//// GETTER
//// Returns a Duration with the hours/minutes/seconds/ms values of the moment.
//// If the moment has an ambiguous time, a duration of 00:00 will be returned.
////
//// SETTER
//// You can supply a Duration, a Moment, or a Duration-like argument.
//// When setting the time, and the moment has an ambiguous time, it then becomes unambiguous.
//FCMoment.prototype.time = function(time) {
//	if (time == null) { // getter
//		return moment.duration({
//			hours: this.hours(),
//			minutes: this.minutes(),
//			seconds: this.seconds(),
//			milliseconds: this.milliseconds()
//		});
//	}
//	else { // setter

//		delete this._ambigTime; // mark that the moment now has a time

//		if (!moment.isDuration(time) && !moment.isMoment(time)) {
//			time = moment.duration(time);
//		}

//		// The day value should cause overflow (so 24 hours becomes 00:00:00 of next day).
//		// Only for Duration times, not Moment times.
//		var dayHours = 0;
//		if (moment.isDuration(time)) {
//			dayHours = Math.floor(time.asDays()) * 24;
//		}

//		// We need to set the individual fields.
//		// Can't use startOf('day') then add duration. In case of DST at start of day.
//		return this.hours(dayHours + time.hours())
//			.minutes(time.minutes())
//			.seconds(time.seconds())
//			.milliseconds(time.milliseconds());
//	}
//};

//// Converts the moment to UTC, stripping out its time-of-day and timezone offset,
//// but preserving its YMD. A moment with a stripped time will display no time
//// nor timezone offset when .format() is called.
//FCMoment.prototype.stripTime = function() {
//	var a = this.toArray(); // year,month,date,hours,minutes,seconds as an array

//	// set the internal UTC flag
//	moment.fn.utc.call(this); // call the original method, because we don't want to affect _ambigZone

//	this.year(a[0]) // TODO: find a way to do this in one shot
//		.month(a[1])
//		.date(a[2])
//		.hours(0)
//		.minutes(0)
//		.seconds(0)
//		.milliseconds(0);

//	// Mark the time as ambiguous. This needs to happen after the .utc() call, which calls .zone(), which
//	// clears all ambig flags. Same concept with the .year/month/date calls in the case of moment-timezone.
//	this._ambigTime = true;
//	this._ambigZone = true; // if ambiguous time, also ambiguous timezone offset

//	return this; // for chaining
//};

//// Returns if the moment has a non-ambiguous time (boolean)
//FCMoment.prototype.hasTime = function() {
//	return !this._ambigTime;
//};


//// Timezone
//// -------------------------------------------------------------------------------------------------

//// Converts the moment to UTC, stripping out its timezone offset, but preserving its
//// YMD and time-of-day. A moment with a stripped timezone offset will display no
//// timezone offset when .format() is called.
//FCMoment.prototype.stripZone = function() {
//	var a = this.toArray(); // year,month,date,hours,minutes,seconds as an array
//	var wasAmbigTime = this._ambigTime;

//	moment.fn.utc.call(this); // set the internal UTC flag

//	this.year(a[0]) // TODO: find a way to do this in one shot
//		.month(a[1])
//		.date(a[2])
//		.hours(a[3])
//		.minutes(a[4])
//		.seconds(a[5])
//		.milliseconds(a[6]);

//	if (wasAmbigTime) {
//		// the above call to .utc()/.zone() unfortunately clears the ambig flags, so reassign
//		this._ambigTime = true;
//	}

//	// Mark the zone as ambiguous. This needs to happen after the .utc() call, which calls .zone(), which
//	// clears all ambig flags. Same concept with the .year/month/date calls in the case of moment-timezone.
//	this._ambigZone = true;

//	return this; // for chaining
//};

//// Returns of the moment has a non-ambiguous timezone offset (boolean)
//FCMoment.prototype.hasZone = function() {
//	return !this._ambigZone;
//};

//// this method implicitly marks a zone
//FCMoment.prototype.zone = function(tzo) {

//	if (tzo != null) {
//		// FYI, the delete statements need to be before the .zone() call or else chaos ensues
//		// for reasons I don't understand. 
//		delete this._ambigTime;
//		delete this._ambigZone;
//	}

//	return moment.fn.zone.apply(this, arguments);
//};

//// this method implicitly marks a zone
//FCMoment.prototype.local = function() {
//	var a = this.toArray(); // year,month,date,hours,minutes,seconds as an array
//	var wasAmbigZone = this._ambigZone;

//	// will happen anyway via .local()/.zone(), but don't want to rely on internal implementation
//	delete this._ambigTime;
//	delete this._ambigZone;

//	moment.fn.local.apply(this, arguments);

//	if (wasAmbigZone) {
//		// If the moment was ambiguously zoned, the date fields were stored as UTC.
//		// We want to preserve these, but in local time.
//		this.year(a[0]) // TODO: find a way to do this in one shot
//			.month(a[1])
//			.date(a[2])
//			.hours(a[3])
//			.minutes(a[4])
//			.seconds(a[5])
//			.milliseconds(a[6]);
//	}

//	return this; // for chaining
//};

//// this method implicitly marks a zone
//FCMoment.prototype.utc = function() {

//	// will happen anyway via .local()/.zone(), but don't want to rely on internal implementation
//	delete this._ambigTime;
//	delete this._ambigZone;

//	return moment.fn.utc.apply(this, arguments);
//};


//// Formatting
//// -------------------------------------------------------------------------------------------------

//FCMoment.prototype.format = function() {
//	if (arguments[0]) {
//		return formatDate(this, arguments[0]); // our extended formatting
//	}
//	if (this._ambigTime) {
//		return momentFormat(this, 'YYYY-MM-DD');
//	}
//	if (this._ambigZone) {
//		return momentFormat(this, 'YYYY-MM-DD[T]HH:mm:ss');
//	}
//	return momentFormat(this); // default moment original formatting
//};

//FCMoment.prototype.toISOString = function() {
//	if (this._ambigTime) {
//		return momentFormat(this, 'YYYY-MM-DD');
//	}
//	if (this._ambigZone) {
//		return momentFormat(this, 'YYYY-MM-DD[T]HH:mm:ss');
//	}
//	return moment.fn.toISOString.apply(this, arguments);
//};


//// Querying
//// -------------------------------------------------------------------------------------------------

//// Is the moment within the specified range? `end` is exclusive.
//FCMoment.prototype.isWithin = function(start, end) {
//	var a = commonlyAmbiguate([ this, start, end ]);
//	return a[0] >= a[1] && a[0] < a[2];
//};

//// When isSame is called with units, timezone ambiguity is normalized before the comparison happens.
//// If no units are specified, the two moments must be identically the same, with matching ambig flags.
//FCMoment.prototype.isSame = function(input, units) {
//	var a;

//	if (units) {
//		a = commonlyAmbiguate([ this, input ], true); // normalize timezones but don't erase times
//		return moment.fn.isSame.call(a[0], a[1], units);
//	}
//	else {
//		input = fc.moment.parseZone(input); // normalize input
//		return moment.fn.isSame.call(this, input) &&
//			Boolean(this._ambigTime) === Boolean(input._ambigTime) &&
//			Boolean(this._ambigZone) === Boolean(input._ambigZone);
//	}
//};

//// Make these query methods work with ambiguous moments
//$.each([
//	'isBefore',
//	'isAfter'
//], function(i, methodName) {
//	FCMoment.prototype[methodName] = function(input, units) {
//		var a = commonlyAmbiguate([ this, input ]);
//		return moment.fn[methodName].call(a[0], a[1], units);
//	};
//});


//// Misc Internals
//// -------------------------------------------------------------------------------------------------

//// given an array of moment-like inputs, return a parallel array w/ moments similarly ambiguated.
//// for example, of one moment has ambig time, but not others, all moments will have their time stripped.
//// set `preserveTime` to `true` to keep times, but only normalize zone ambiguity.
//function commonlyAmbiguate(inputs, preserveTime) {
//	var outputs = [];
//	var anyAmbigTime = false;
//	var anyAmbigZone = false;
//	var i;

//	for (i=0; i<inputs.length; i++) {
//		outputs.push(fc.moment.parseZone(inputs[i]));
//		anyAmbigTime = anyAmbigTime || outputs[i]._ambigTime;
//		anyAmbigZone = anyAmbigZone || outputs[i]._ambigZone;
//	}

//	for (i=0; i<outputs.length; i++) {
//		if (anyAmbigTime && !preserveTime) {
//			outputs[i].stripTime();
//		}
//		else if (anyAmbigZone) {
//			outputs[i].stripZone();
//		}
//	}

//	return outputs;
//}

//;;

//// Single Date Formatting
//// -------------------------------------------------------------------------------------------------


//// call this if you want Moment's original format method to be used
//function momentFormat(mom, formatStr) {
//	return moment.fn.format.call(mom, formatStr);
//}


//// Formats `date` with a Moment formatting string, but allow our non-zero areas and
//// additional token.
//function formatDate(date, formatStr) {
//	return formatDateWithChunks(date, getFormatStringChunks(formatStr));
//}


//function formatDateWithChunks(date, chunks) {
//	var s = '';
//	var i;

//	for (i=0; i<chunks.length; i++) {
//		s += formatDateWithChunk(date, chunks[i]);
//	}

//	return s;
//}


//// addition formatting tokens we want recognized
//var tokenOverrides = {
//	t: function(date) { // "a" or "p"
//		return momentFormat(date, 'a').charAt(0);
//	},
//	T: function(date) { // "A" or "P"
//		return momentFormat(date, 'A').charAt(0);
//	}
//};


//function formatDateWithChunk(date, chunk) {
//	var token;
//	var maybeStr;

//	if (typeof chunk === 'string') { // a literal string
//		return chunk;
//	}
//	else if ((token = chunk.token)) { // a token, like "YYYY"
//		if (tokenOverrides[token]) {
//			return tokenOverrides[token](date); // use our custom token
//		}
//		return momentFormat(date, token);
//	}
//	else if (chunk.maybe) { // a grouping of other chunks that must be non-zero
//		maybeStr = formatDateWithChunks(date, chunk.maybe);
//		if (maybeStr.match(/[1-9]/)) {
//			return maybeStr;
//		}
//	}

//	return '';
//}


//// Date Range Formatting
//// -------------------------------------------------------------------------------------------------
//// TODO: make it work with timezone offset

//// Using a formatting string meant for a single date, generate a range string, like
//// "Sep 2 - 9 2013", that intelligently inserts a separator where the dates differ.
//// If the dates are the same as far as the format string is concerned, just return a single
//// rendering of one date, without any separator.
//function formatRange(date1, date2, formatStr, separator, isRTL) {
//	var localeData;

//	date1 = fc.moment.parseZone(date1);
//	date2 = fc.moment.parseZone(date2);

//	localeData = (date1.localeData || date1.lang).call(date1); // works with moment-pre-2.8

//	// Expand localized format strings, like "LL" -> "MMMM D YYYY"
//	formatStr = localeData.longDateFormat(formatStr) || formatStr;
//	// BTW, this is not important for `formatDate` because it is impossible to put custom tokens
//	// or non-zero areas in Moment's localized format strings.

//	separator = separator || ' - ';

//	return formatRangeWithChunks(
//		date1,
//		date2,
//		getFormatStringChunks(formatStr),
//		separator,
//		isRTL
//	);
//}
//fc.formatRange = formatRange; // expose


//function formatRangeWithChunks(date1, date2, chunks, separator, isRTL) {
//	var chunkStr; // the rendering of the chunk
//	var leftI;
//	var leftStr = '';
//	var rightI;
//	var rightStr = '';
//	var middleI;
//	var middleStr1 = '';
//	var middleStr2 = '';
//	var middleStr = '';

//	// Start at the leftmost side of the formatting string and continue until you hit a token
//	// that is not the same between dates.
//	for (leftI=0; leftI<chunks.length; leftI++) {
//		chunkStr = formatSimilarChunk(date1, date2, chunks[leftI]);
//		if (chunkStr === false) {
//			break;
//		}
//		leftStr += chunkStr;
//	}

//	// Similarly, start at the rightmost side of the formatting string and move left
//	for (rightI=chunks.length-1; rightI>leftI; rightI--) {
//		chunkStr = formatSimilarChunk(date1, date2, chunks[rightI]);
//		if (chunkStr === false) {
//			break;
//		}
//		rightStr = chunkStr + rightStr;
//	}

//	// The area in the middle is different for both of the dates.
//	// Collect them distinctly so we can jam them together later.
//	for (middleI=leftI; middleI<=rightI; middleI++) {
//		middleStr1 += formatDateWithChunk(date1, chunks[middleI]);
//		middleStr2 += formatDateWithChunk(date2, chunks[middleI]);
//	}

//	if (middleStr1 || middleStr2) {
//		if (isRTL) {
//			middleStr = middleStr2 + separator + middleStr1;
//		}
//		else {
//			middleStr = middleStr1 + separator + middleStr2;
//		}
//	}

//	return leftStr + middleStr + rightStr;
//}


//var similarUnitMap = {
//	Y: 'year',
//	M: 'month',
//	D: 'day', // day of month
//	d: 'day', // day of week
//	// prevents a separator between anything time-related...
//	A: 'second', // AM/PM
//	a: 'second', // am/pm
//	T: 'second', // A/P
//	t: 'second', // a/p
//	H: 'second', // hour (24)
//	h: 'second', // hour (12)
//	m: 'second', // minute
//	s: 'second' // second
//};
//// TODO: week maybe?


//// Given a formatting chunk, and given that both dates are similar in the regard the
//// formatting chunk is concerned, format date1 against `chunk`. Otherwise, return `false`.
//function formatSimilarChunk(date1, date2, chunk) {
//	var token;
//	var unit;

//	if (typeof chunk === 'string') { // a literal string
//		return chunk;
//	}
//	else if ((token = chunk.token)) {
//		unit = similarUnitMap[token.charAt(0)];
//		// are the dates the same for this unit of measurement?
//		if (unit && date1.isSame(date2, unit)) {
//			return momentFormat(date1, token); // would be the same if we used `date2`
//			// BTW, don't support custom tokens
//		}
//	}

//	return false; // the chunk is NOT the same for the two dates
//	// BTW, don't support splitting on non-zero areas
//}


//// Chunking Utils
//// -------------------------------------------------------------------------------------------------


//var formatStringChunkCache = {};


//function getFormatStringChunks(formatStr) {
//	if (formatStr in formatStringChunkCache) {
//		return formatStringChunkCache[formatStr];
//	}
//	return (formatStringChunkCache[formatStr] = chunkFormatString(formatStr));
//}


//// Break the formatting string into an array of chunks
//function chunkFormatString(formatStr) {
//	var chunks = [];
//	var chunker = /\[([^\]]*)\]|\(([^\)]*)\)|(LT|(\w)\4*o?)|([^\w\[\(]+)/g; // TODO: more descrimination
//	var match;

//	while ((match = chunker.exec(formatStr))) {
//		if (match[1]) { // a literal string inside [ ... ]
//			chunks.push(match[1]);
//		}
//		else if (match[2]) { // non-zero formatting inside ( ... )
//			chunks.push({ maybe: chunkFormatString(match[2]) });
//		}
//		else if (match[3]) { // a formatting token
//			chunks.push({ token: match[3] });
//		}
//		else if (match[5]) { // an unenclosed literal string
//			chunks.push(match[5]);
//		}
//	}

//	return chunks;
//}

//;;

///* A rectangular panel that is absolutely positioned over other content
//------------------------------------------------------------------------------------------------------------------------
//Options:
//	- className (string)
//	- content (HTML string or jQuery element set)
//	- parentEl
//	- top
//	- left
//	- right (the x coord of where the right edge should be. not a "CSS" right)
//	- autoHide (boolean)
//	- show (callback)
//	- hide (callback)
//*/

//function Popover(options) {
//	this.options = options || {};
//}


//Popover.prototype = {

//	isHidden: true,
//	options: null,
//	el: null, // the container element for the popover. generated by this object
//	documentMousedownProxy: null, // document mousedown handler bound to `this`
//	margin: 10, // the space required between the popover and the edges of the scroll container


//	// Shows the popover on the specified position. Renders it if not already
//	show: function() {
//		if (this.isHidden) {
//			if (!this.el) {
//				this.render();
//			}
//			this.el.show();
//			this.position();
//			this.isHidden = false;
//			this.trigger('show');
//		}
//	},


//	// Hides the popover, through CSS, but does not remove it from the DOM
//	hide: function() {
//		if (!this.isHidden) {
//			this.el.hide();
//			this.isHidden = true;
//			this.trigger('hide');
//		}
//	},


//	// Creates `this.el` and renders content inside of it
//	render: function() {
//		var _this = this;
//		var options = this.options;

//		this.el = $('<div class="fc-popover"/>')
//			.addClass(options.className || '')
//			.css({
//				// position initially to the top left to avoid creating scrollbars
//				top: 0,
//				left: 0
//			})
//			.append(options.content)
//			.appendTo(options.parentEl);

//		// when a click happens on anything inside with a 'fc-close' className, hide the popover
//		this.el.on('click', '.fc-close', function() {
//			_this.hide();
//		});

//		if (options.autoHide) {
//			$(document).on('mousedown', this.documentMousedownProxy = $.proxy(this, 'documentMousedown'));
//		}
//	},


//	// Triggered when the user clicks *anywhere* in the document, for the autoHide feature
//	documentMousedown: function(ev) {
//		// only hide the popover if the click happened outside the popover
//		if (this.el && !$(ev.target).closest(this.el).length) {
//			this.hide();
//		}
//	},


//	// Hides and unregisters any handlers
//	destroy: function() {
//		this.hide();

//		if (this.el) {
//			this.el.remove();
//			this.el = null;
//		}

//		$(document).off('mousedown', this.documentMousedownProxy);
//	},


//	// Positions the popover optimally, using the top/left/right options
//	position: function() {
//		var options = this.options;
//		var origin = this.el.offsetParent().offset();
//		var width = this.el.outerWidth();
//		var height = this.el.outerHeight();
//		var windowEl = $(window);
//		var viewportEl = getScrollParent(this.el);
//		var viewportTop;
//		var viewportLeft;
//		var viewportOffset;
//		var top; // the "position" (not "offset") values for the popover
//		var left; //

//		// compute top and left
//		top = options.top || 0;
//		if (options.left !== undefined) {
//			left = options.left;
//		}
//		else if (options.right !== undefined) {
//			left = options.right - width; // derive the left value from the right value
//		}
//		else {
//			left = 0;
//		}

//		if (viewportEl.is(window) || viewportEl.is(document)) { // normalize getScrollParent's result
//			viewportEl = windowEl;
//			viewportTop = 0; // the window is always at the top left
//			viewportLeft = 0; // (and .offset() won't work if called here)
//		}
//		else {
//			viewportOffset = viewportEl.offset();
//			viewportTop = viewportOffset.top;
//			viewportLeft = viewportOffset.left;
//		}

//		// if the window is scrolled, it causes the visible area to be further down
//		viewportTop += windowEl.scrollTop();
//		viewportLeft += windowEl.scrollLeft();

//		// constrain to the view port. if constrained by two edges, give precedence to top/left
//		if (options.viewportConstrain !== false) {
//			top = Math.min(top, viewportTop + viewportEl.outerHeight() - height - this.margin);
//			top = Math.max(top, viewportTop + this.margin);
//			left = Math.min(left, viewportLeft + viewportEl.outerWidth() - width - this.margin);
//			left = Math.max(left, viewportLeft + this.margin);
//		}

//		this.el.css({
//			top: top - origin.top,
//			left: left - origin.left
//		});
//	},


//	// Triggers a callback. Calls a function in the option hash of the same name.
//	// Arguments beyond the first `name` are forwarded on.
//	// TODO: better code reuse for this. Repeat code
//	trigger: function(name) {
//		if (this.options[name]) {
//			this.options[name].apply(this, Array.prototype.slice.call(arguments, 1));
//		}
//	}

//};

//;;

///* A "coordinate map" converts pixel coordinates into an associated cell, which has an associated date
//------------------------------------------------------------------------------------------------------------------------
//Common interface:

//	CoordMap.prototype = {
//		build: function() {},
//		getCell: function(x, y) {}
//	};

//*/

///* Coordinate map for a grid component
//----------------------------------------------------------------------------------------------------------------------*/

//function GridCoordMap(grid) {
//	this.grid = grid;
//}


//GridCoordMap.prototype = {

//	grid: null, // reference to the Grid
//	rows: null, // the top-to-bottom y coordinates. including the bottom of the last item
//	cols: null, // the left-to-right x coordinates. including the right of the last item

//	containerEl: null, // container element that all coordinates are constrained to. optionally assigned
//	minX: null,
//	maxX: null, // exclusive
//	minY: null,
//	maxY: null, // exclusive


//	// Queries the grid for the coordinates of all the cells
//	build: function() {
//		this.grid.buildCoords(
//			this.rows = [],
//			this.cols = []
//		);
//		this.computeBounds();
//	},


//	// Given a coordinate of the document, gets the associated cell. If no cell is underneath, returns null
//	getCell: function(x, y) {
//		var cell = null;
//		var rows = this.rows;
//		var cols = this.cols;
//		var r = -1;
//		var c = -1;
//		var i;

//		if (this.inBounds(x, y)) {

//			for (i = 0; i < rows.length; i++) {
//				if (y >= rows[i][0] && y < rows[i][1]) {
//					r = i;
//					break;
//				}
//			}

//			for (i = 0; i < cols.length; i++) {
//				if (x >= cols[i][0] && x < cols[i][1]) {
//					c = i;
//					break;
//				}
//			}

//			if (r >= 0 && c >= 0) {
//				cell = { row: r, col: c };
//				cell.grid = this.grid;
//				cell.date = this.grid.getCellDate(cell);
//			}
//		}

//		return cell;
//	},


//	// If there is a containerEl, compute the bounds into min/max values
//	computeBounds: function() {
//		var containerOffset;

//		if (this.containerEl) {
//			containerOffset = this.containerEl.offset();
//			this.minX = containerOffset.left;
//			this.maxX = containerOffset.left + this.containerEl.outerWidth();
//			this.minY = containerOffset.top;
//			this.maxY = containerOffset.top + this.containerEl.outerHeight();
//		}
//	},


//	// Determines if the given coordinates are in bounds. If no `containerEl`, always true
//	inBounds: function(x, y) {
//		if (this.containerEl) {
//			return x >= this.minX && x < this.maxX && y >= this.minY && y < this.maxY;
//		}
//		return true;
//	}

//};


///* Coordinate map that is a combination of multiple other coordinate maps
//----------------------------------------------------------------------------------------------------------------------*/

//function ComboCoordMap(coordMaps) {
//	this.coordMaps = coordMaps;
//}


//ComboCoordMap.prototype = {

//	coordMaps: null, // an array of CoordMaps


//	// Builds all coordMaps
//	build: function() {
//		var coordMaps = this.coordMaps;
//		var i;

//		for (i = 0; i < coordMaps.length; i++) {
//			coordMaps[i].build();
//		}
//	},


//	// Queries all coordMaps for the cell underneath the given coordinates, returning the first result
//	getCell: function(x, y) {
//		var coordMaps = this.coordMaps;
//		var cell = null;
//		var i;

//		for (i = 0; i < coordMaps.length && !cell; i++) {
//			cell = coordMaps[i].getCell(x, y);
//		}

//		return cell;
//	}

//};

//;;

///* Tracks mouse movements over a CoordMap and raises events about which cell the mouse is over.
//----------------------------------------------------------------------------------------------------------------------*/
//// TODO: implement scrolling

//function DragListener(coordMap, options) {
//	this.coordMap = coordMap;
//	this.options = options || {};
//}


//DragListener.prototype = {

//	coordMap: null,
//	options: null,

//	isListening: false,
//	isDragging: false,

//	// the cell/date the mouse was over when listening started
//	origCell: null,
//	origDate: null,

//	// the cell/date the mouse is over
//	cell: null,
//	date: null,

//	// coordinates of the initial mousedown
//	mouseX0: null,
//	mouseY0: null,

//	// handler attached to the document, bound to the DragListener's `this`
//	mousemoveProxy: null,
//	mouseupProxy: null,

//	scrollEl: null,
//	scrollBounds: null, // { top, bottom, left, right }
//	scrollTopVel: null, // pixels per second
//	scrollLeftVel: null, // pixels per second
//	scrollIntervalId: null, // ID of setTimeout for scrolling animation loop
//	scrollHandlerProxy: null, // this-scoped function for handling when scrollEl is scrolled

//	scrollSensitivity: 30, // pixels from edge for scrolling to start
//	scrollSpeed: 200, // pixels per second, at maximum speed
//	scrollIntervalMs: 50, // millisecond wait between scroll increment


//	// Call this when the user does a mousedown. Will probably lead to startListening
//	mousedown: function(ev) {
//		if (isPrimaryMouseButton(ev)) {

//			ev.preventDefault(); // prevents native selection in most browsers

//			this.startListening(ev);

//			// start the drag immediately if there is no minimum distance for a drag start
//			if (!this.options.distance) {
//				this.startDrag(ev);
//			}
//		}
//	},


//	// Call this to start tracking mouse movements
//	startListening: function(ev) {
//		var scrollParent;
//		var cell;

//		if (!this.isListening) {

//			// grab scroll container and attach handler
//			if (ev && this.options.scroll) {
//				scrollParent = getScrollParent($(ev.target));
//				if (!scrollParent.is(window) && !scrollParent.is(document)) {
//					this.scrollEl = scrollParent;

//					// scope to `this`, and use `debounce` to make sure rapid calls don't happen
//					this.scrollHandlerProxy = debounce($.proxy(this, 'scrollHandler'), 100);
//					this.scrollEl.on('scroll', this.scrollHandlerProxy);
//				}
//			}

//			this.computeCoords(); // relies on `scrollEl`

//			// get info on the initial cell, date, and coordinates
//			if (ev) {
//				cell = this.getCell(ev);
//				this.origCell = cell;
//				this.origDate = cell ? cell.date : null;

//				this.mouseX0 = ev.pageX;
//				this.mouseY0 = ev.pageY;
//			}

//			$(document)
//				.on('mousemove', this.mousemoveProxy = $.proxy(this, 'mousemove'))
//				.on('mouseup', this.mouseupProxy = $.proxy(this, 'mouseup'))
//				.on('selectstart', this.preventDefault); // prevents native selection in IE<=8

//			this.isListening = true;
//			this.trigger('listenStart', ev);
//		}
//	},


//	// Recomputes the drag-critical positions of elements
//	computeCoords: function() {
//		this.coordMap.build();
//		this.computeScrollBounds();
//	},


//	// Called when the user moves the mouse
//	mousemove: function(ev) {
//		var minDistance;
//		var distanceSq; // current distance from mouseX0/mouseY0, squared

//		if (!this.isDragging) { // if not already dragging...
//			// then start the drag if the minimum distance criteria is met
//			minDistance = this.options.distance || 1;
//			distanceSq = Math.pow(ev.pageX - this.mouseX0, 2) + Math.pow(ev.pageY - this.mouseY0, 2);
//			if (distanceSq >= minDistance * minDistance) { // use pythagorean theorem
//				this.startDrag(ev);
//			}
//		}

//		if (this.isDragging) {
//			this.drag(ev); // report a drag, even if this mousemove initiated the drag
//		}
//	},


//	// Call this to initiate a legitimate drag.
//	// This function is called internally from this class, but can also be called explicitly from outside
//	startDrag: function(ev) {
//		var cell;

//		if (!this.isListening) { // startDrag must have manually initiated
//			this.startListening();
//		}

//		if (!this.isDragging) {
//			this.isDragging = true;
//			this.trigger('dragStart', ev);

//			// report the initial cell the mouse is over
//			cell = this.getCell(ev);
//			if (cell) {
//				this.cellOver(cell, true);
//			}
//		}
//	},


//	// Called while the mouse is being moved and when we know a legitimate drag is taking place
//	drag: function(ev) {
//		var cell;

//		if (this.isDragging) {
//			cell = this.getCell(ev);

//			if (!isCellsEqual(cell, this.cell)) { // a different cell than before?
//				if (this.cell) {
//					this.cellOut();
//				}
//				if (cell) {
//					this.cellOver(cell);
//				}
//			}

//			this.dragScroll(ev); // will possibly cause scrolling
//		}
//	},


//	// Called when a the mouse has just moved over a new cell
//	cellOver: function(cell) {
//		this.cell = cell;
//		this.date = cell.date;
//		this.trigger('cellOver', cell, cell.date);
//	},


//	// Called when the mouse has just moved out of a cell
//	cellOut: function() {
//		if (this.cell) {
//			this.trigger('cellOut', this.cell);
//			this.cell = null;
//			this.date = null;
//		}
//	},


//	// Called when the user does a mouseup
//	mouseup: function(ev) {
//		this.stopDrag(ev);
//		this.stopListening(ev);
//	},


//	// Called when the drag is over. Will not cause listening to stop however.
//	// A concluding 'cellOut' event will NOT be triggered.
//	stopDrag: function(ev) {
//		if (this.isDragging) {
//			this.stopScrolling();
//			this.trigger('dragStop', ev);
//			this.isDragging = false;
//		}
//	},


//	// Call this to stop listening to the user's mouse events
//	stopListening: function(ev) {
//		if (this.isListening) {

//			// remove the scroll handler if there is a scrollEl
//			if (this.scrollEl) {
//				this.scrollEl.off('scroll', this.scrollHandlerProxy);
//				this.scrollHandlerProxy = null;
//			}

//			$(document)
//				.off('mousemove', this.mousemoveProxy)
//				.off('mouseup', this.mouseupProxy)
//				.off('selectstart', this.preventDefault);

//			this.mousemoveProxy = null;
//			this.mouseupProxy = null;

//			this.isListening = false;
//			this.trigger('listenStop', ev);

//			this.origCell = this.cell = null;
//			this.origDate = this.date = null;
//		}
//	},


//	// Gets the cell underneath the coordinates for the given mouse event
//	getCell: function(ev) {
//		return this.coordMap.getCell(ev.pageX, ev.pageY);
//	},


//	// Triggers a callback. Calls a function in the option hash of the same name.
//	// Arguments beyond the first `name` are forwarded on.
//	trigger: function(name) {
//		if (this.options[name]) {
//			this.options[name].apply(this, Array.prototype.slice.call(arguments, 1));
//		}
//	},


//	// Stops a given mouse event from doing it's native browser action. In our case, text selection.
//	preventDefault: function(ev) {
//		ev.preventDefault();
//	},


//	/* Scrolling
//	------------------------------------------------------------------------------------------------------------------*/


//	// Computes and stores the bounding rectangle of scrollEl
//	computeScrollBounds: function() {
//		var el = this.scrollEl;
//		var offset;

//		if (el) {
//			offset = el.offset();
//			this.scrollBounds = {
//				top: offset.top,
//				left: offset.left,
//				bottom: offset.top + el.outerHeight(),
//				right: offset.left + el.outerWidth()
//			};
//		}
//	},


//	// Called when the dragging is in progress and scrolling should be updated
//	dragScroll: function(ev) {
//		var sensitivity = this.scrollSensitivity;
//		var bounds = this.scrollBounds;
//		var topCloseness, bottomCloseness;
//		var leftCloseness, rightCloseness;
//		var topVel = 0;
//		var leftVel = 0;

//		if (bounds) { // only scroll if scrollEl exists

//			// compute closeness to edges. valid range is from 0.0 - 1.0
//			topCloseness = (sensitivity - (ev.pageY - bounds.top)) / sensitivity;
//			bottomCloseness = (sensitivity - (bounds.bottom - ev.pageY)) / sensitivity;
//			leftCloseness = (sensitivity - (ev.pageX - bounds.left)) / sensitivity;
//			rightCloseness = (sensitivity - (bounds.right - ev.pageX)) / sensitivity;

//			// translate vertical closeness into velocity.
//			// mouse must be completely in bounds for velocity to happen.
//			if (topCloseness >= 0 && topCloseness <= 1) {
//				topVel = topCloseness * this.scrollSpeed * -1; // negative. for scrolling up
//			}
//			else if (bottomCloseness >= 0 && bottomCloseness <= 1) {
//				topVel = bottomCloseness * this.scrollSpeed;
//			}

//			// translate horizontal closeness into velocity
//			if (leftCloseness >= 0 && leftCloseness <= 1) {
//				leftVel = leftCloseness * this.scrollSpeed * -1; // negative. for scrolling left
//			}
//			else if (rightCloseness >= 0 && rightCloseness <= 1) {
//				leftVel = rightCloseness * this.scrollSpeed;
//			}
//		}

//		this.setScrollVel(topVel, leftVel);
//	},


//	// Sets the speed-of-scrolling for the scrollEl
//	setScrollVel: function(topVel, leftVel) {

//		this.scrollTopVel = topVel;
//		this.scrollLeftVel = leftVel;

//		this.constrainScrollVel(); // massages into realistic values

//		// if there is non-zero velocity, and an animation loop hasn't already started, then START
//		if ((this.scrollTopVel || this.scrollLeftVel) && !this.scrollIntervalId) {
//			this.scrollIntervalId = setInterval(
//				$.proxy(this, 'scrollIntervalFunc'), // scope to `this`
//				this.scrollIntervalMs
//			);
//		}
//	},


//	// Forces scrollTopVel and scrollLeftVel to be zero if scrolling has already gone all the way
//	constrainScrollVel: function() {
//		var el = this.scrollEl;

//		if (this.scrollTopVel < 0) { // scrolling up?
//			if (el.scrollTop() <= 0) { // already scrolled all the way up?
//				this.scrollTopVel = 0;
//			}
//		}
//		else if (this.scrollTopVel > 0) { // scrolling down?
//			if (el.scrollTop() + el[0].clientHeight >= el[0].scrollHeight) { // already scrolled all the way down?
//				this.scrollTopVel = 0;
//			}
//		}

//		if (this.scrollLeftVel < 0) { // scrolling left?
//			if (el.scrollLeft() <= 0) { // already scrolled all the left?
//				this.scrollLeftVel = 0;
//			}
//		}
//		else if (this.scrollLeftVel > 0) { // scrolling right?
//			if (el.scrollLeft() + el[0].clientWidth >= el[0].scrollWidth) { // already scrolled all the way right?
//				this.scrollLeftVel = 0;
//			}
//		}
//	},


//	// This function gets called during every iteration of the scrolling animation loop
//	scrollIntervalFunc: function() {
//		var el = this.scrollEl;
//		var frac = this.scrollIntervalMs / 1000; // considering animation frequency, what the vel should be mult'd by

//		// change the value of scrollEl's scroll
//		if (this.scrollTopVel) {
//			el.scrollTop(el.scrollTop() + this.scrollTopVel * frac);
//		}
//		if (this.scrollLeftVel) {
//			el.scrollLeft(el.scrollLeft() + this.scrollLeftVel * frac);
//		}

//		this.constrainScrollVel(); // since the scroll values changed, recompute the velocities

//		// if scrolled all the way, which causes the vels to be zero, stop the animation loop
//		if (!this.scrollTopVel && !this.scrollLeftVel) {
//			this.stopScrolling();
//		}
//	},


//	// Kills any existing scrolling animation loop
//	stopScrolling: function() {
//		if (this.scrollIntervalId) {
//			clearInterval(this.scrollIntervalId);
//			this.scrollIntervalId = null;

//			// when all done with scrolling, recompute positions since they probably changed
//			this.computeCoords();
//		}
//	},


//	// Get called when the scrollEl is scrolled (NOTE: this is delayed via debounce)
//	scrollHandler: function() {
//		// recompute all coordinates, but *only* if this is *not* part of our scrolling animation
//		if (!this.scrollIntervalId) {
//			this.computeCoords();
//		}
//	}

//};


//// Returns `true` if the cells are identically equal. `false` otherwise.
//// They must have the same row, col, and be from the same grid.
//// Two null values will be considered equal, as two "out of the grid" states are the same.
//function isCellsEqual(cell1, cell2) {

//	if (!cell1 && !cell2) {
//		return true;
//	}

//	if (cell1 && cell2) {
//		return cell1.grid === cell2.grid &&
//			cell1.row === cell2.row &&
//			cell1.col === cell2.col;
//	}

//	return false;
//}

//;;

///* Creates a clone of an element and lets it track the mouse as it moves
//----------------------------------------------------------------------------------------------------------------------*/

//function MouseFollower(sourceEl, options) {
//	this.options = options = options || {};
//	this.sourceEl = sourceEl;
//	this.parentEl = options.parentEl ? $(options.parentEl) : sourceEl.parent(); // default to sourceEl's parent
//}


//MouseFollower.prototype = {

//	options: null,

//	sourceEl: null, // the element that will be cloned and made to look like it is dragging
//	el: null, // the clone of `sourceEl` that will track the mouse
//	parentEl: null, // the element that `el` (the clone) will be attached to

//	// the initial position of el, relative to the offset parent. made to match the initial offset of sourceEl
//	top0: null,
//	left0: null,

//	// the initial position of the mouse
//	mouseY0: null,
//	mouseX0: null,

//	// the number of pixels the mouse has moved from its initial position
//	topDelta: null,
//	leftDelta: null,

//	mousemoveProxy: null, // document mousemove handler, bound to the MouseFollower's `this`

//	isFollowing: false,
//	isHidden: false,
//	isAnimating: false, // doing the revert animation?


//	// Causes the element to start following the mouse
//	start: function(ev) {
//		if (!this.isFollowing) {
//			this.isFollowing = true;

//			this.mouseY0 = ev.pageY;
//			this.mouseX0 = ev.pageX;
//			this.topDelta = 0;
//			this.leftDelta = 0;

//			if (!this.isHidden) {
//				this.updatePosition();
//			}

//			$(document).on('mousemove', this.mousemoveProxy = $.proxy(this, 'mousemove'));
//		}
//	},


//	// Causes the element to stop following the mouse. If shouldRevert is true, will animate back to original position.
//	// `callback` gets invoked when the animation is complete. If no animation, it is invoked immediately.
//	stop: function(shouldRevert, callback) {
//		var _this = this;
//		var revertDuration = this.options.revertDuration;

//		function complete() {
//			this.isAnimating = false;
//			_this.destroyEl();

//			this.top0 = this.left0 = null; // reset state for future updatePosition calls

//			if (callback) {
//				callback();
//			}
//		}

//		if (this.isFollowing && !this.isAnimating) { // disallow more than one stop animation at a time
//			this.isFollowing = false;

//			$(document).off('mousemove', this.mousemoveProxy);

//			if (shouldRevert && revertDuration && !this.isHidden) { // do a revert animation?
//				this.isAnimating = true;
//				this.el.animate({
//					top: this.top0,
//					left: this.left0
//				}, {
//					duration: revertDuration,
//					complete: complete
//				});
//			}
//			else {
//				complete();
//			}
//		}
//	},


//	// Gets the tracking element. Create it if necessary
//	getEl: function() {
//		var el = this.el;

//		if (!el) {
//			this.sourceEl.width(); // hack to force IE8 to compute correct bounding box
//			el = this.el = this.sourceEl.clone()
//				.css({
//					position: 'absolute',
//					visibility: '', // in case original element was hidden (commonly through hideEvents())
//					display: this.isHidden ? 'none' : '', // for when initially hidden
//					margin: 0,
//					right: 'auto', // erase and set width instead
//					bottom: 'auto', // erase and set height instead
//					width: this.sourceEl.width(), // explicit height in case there was a 'right' value
//					height: this.sourceEl.height(), // explicit width in case there was a 'bottom' value
//					opacity: this.options.opacity || '',
//					zIndex: this.options.zIndex
//				})
//				.appendTo(this.parentEl);
//		}

//		return el;
//	},


//	// Removes the tracking element if it has already been created
//	destroyEl: function() {
//		if (this.el) {
//			this.el.remove();
//			this.el = null;
//		}
//	},


//	// Update the CSS position of the tracking element
//	updatePosition: function() {
//		var sourceOffset;
//		var origin;

//		this.getEl(); // ensure this.el

//		// make sure origin info was computed
//		if (this.top0 === null) {
//			this.sourceEl.width(); // hack to force IE8 to compute correct bounding box
//			sourceOffset = this.sourceEl.offset();
//			origin = this.el.offsetParent().offset();
//			this.top0 = sourceOffset.top - origin.top;
//			this.left0 = sourceOffset.left - origin.left;
//		}

//		this.el.css({
//			top: this.top0 + this.topDelta,
//			left: this.left0 + this.leftDelta
//		});
//	},


//	// Gets called when the user moves the mouse
//	mousemove: function(ev) {
//		this.topDelta = ev.pageY - this.mouseY0;
//		this.leftDelta = ev.pageX - this.mouseX0;

//		if (!this.isHidden) {
//			this.updatePosition();
//		}
//	},


//	// Temporarily makes the tracking element invisible. Can be called before following starts
//	hide: function() {
//		if (!this.isHidden) {
//			this.isHidden = true;
//			if (this.el) {
//				this.el.hide();
//			}
//		}
//	},


//	// Show the tracking element after it has been temporarily hidden
//	show: function() {
//		if (this.isHidden) {
//			this.isHidden = false;
//			this.updatePosition();
//			this.getEl().show();
//		}
//	}

//};

//;;

///* A utility class for rendering <tr> rows.
//----------------------------------------------------------------------------------------------------------------------*/
//// It leverages methods of the subclass and the View to determine custom rendering behavior for each row "type"
//// (such as highlight rows, day rows, helper rows, etc).

//function RowRenderer(view) {
//	this.view = view;
//}


//RowRenderer.prototype = {

//	view: null, // a View object
//	cellHtml: '<td/>', // plain default HTML used for a cell when no other is available


//	// Renders the HTML for a row, leveraging custom cell-HTML-renderers based on the `rowType`.
//	// Also applies the "intro" and "outro" cells, which are specified by the subclass and views.
//	// `row` is an optional row number.
//	rowHtml: function(rowType, row) {
//		var view = this.view;
//		var renderCell = this.getHtmlRenderer('cell', rowType);
//		var cellHtml = '';
//		var col;
//		var date;

//		row = row || 0;

//		for (col = 0; col < view.colCnt; col++) {
//			date = view.cellToDate(row, col);
//			cellHtml += renderCell(row, col, date);
//		}

//		cellHtml = this.bookendCells(cellHtml, rowType, row); // apply intro and outro

//		return '<tr>' + cellHtml + '</tr>';
//	},


//	// Applies the "intro" and "outro" HTML to the given cells.
//	// Intro means the leftmost cell when the calendar is LTR and the rightmost cell when RTL. Vice-versa for outro.
//	// `cells` can be an HTML string of <td>'s or a jQuery <tr> element
//	// `row` is an optional row number.
//	bookendCells: function(cells, rowType, row) {
//		var view = this.view;
//		var intro = this.getHtmlRenderer('intro', rowType)(row || 0);
//		var outro = this.getHtmlRenderer('outro', rowType)(row || 0);
//		var isRTL = view.opt('isRTL');
//		var prependHtml = isRTL ? outro : intro;
//		var appendHtml = isRTL ? intro : outro;

//		if (typeof cells === 'string') {
//			return prependHtml + cells + appendHtml;
//		}
//		else { // a jQuery <tr> element
//			return cells.prepend(prependHtml).append(appendHtml);
//		}
//	},


//	// Returns an HTML-rendering function given a specific `rendererName` (like cell, intro, or outro) and a specific
//	// `rowType` (like day, eventSkeleton, helperSkeleton), which is optional.
//	// If a renderer for the specific rowType doesn't exist, it will fall back to a generic renderer.
//	// We will query the View object first for any custom rendering functions, then the methods of the subclass.
//	getHtmlRenderer: function(rendererName, rowType) {
//		var view = this.view;
//		var generalName; // like "cellHtml"
//		var specificName; // like "dayCellHtml". based on rowType
//		var provider; // either the View or the RowRenderer subclass, whichever provided the method
//		var renderer;

//		generalName = rendererName + 'Html';
//		if (rowType) {
//			specificName = rowType + capitaliseFirstLetter(rendererName) + 'Html';
//		}

//		if (specificName && (renderer = view[specificName])) {
//			provider = view;
//		}
//		else if (specificName && (renderer = this[specificName])) {
//			provider = this;
//		}
//		else if ((renderer = view[generalName])) {
//			provider = view;
//		}
//		else if ((renderer = this[generalName])) {
//			provider = this;
//		}

//		if (typeof renderer === 'function') {
//			return function(row) {
//				return renderer.apply(provider, arguments) || ''; // use correct `this` and always return a string
//			};
//		}

//		// the rendered can be a plain string as well. if not specified, always an empty string.
//		return function() {
//			return renderer || '';
//		};
//	}

//};

//;;

///* An abstract class comprised of a "grid" of cells that each represent a specific datetime
//----------------------------------------------------------------------------------------------------------------------*/

//function Grid(view) {
//	RowRenderer.call(this, view); // call the super-constructor
//	this.coordMap = new GridCoordMap(this);
//}


//Grid.prototype = createObject(RowRenderer.prototype); // declare the super-class
//$.extend(Grid.prototype, {

//	el: null, // the containing element
//	coordMap: null, // a GridCoordMap that converts pixel values to datetimes
//	cellDuration: null, // a cell's duration. subclasses must assign this ASAP


//	// Renders the grid into the `el` element.
//	// Subclasses should override and call this super-method when done.
//	render: function() {
//		this.bindHandlers();
//	},


//	// Called when the grid's resources need to be cleaned up
//	destroy: function() {
//		// subclasses can implement
//	},


//	/* Coordinates & Cells
//	------------------------------------------------------------------------------------------------------------------*/


//	// Populates the given empty arrays with the y and x coordinates of the cells
//	buildCoords: function(rows, cols) {
//		// subclasses must implement
//	},


//	// Given a cell object, returns the date for that cell
//	getCellDate: function(cell) {
//		// subclasses must implement
//	},


//	// Given a cell object, returns the element that represents the cell's whole-day
//	getCellDayEl: function(cell) {
//		// subclasses must implement
//	},


//	// Converts a range with an inclusive `start` and an exclusive `end` into an array of segment objects
//	rangeToSegs: function(start, end) {
//		// subclasses must implement
//	},


//	/* Handlers
//	------------------------------------------------------------------------------------------------------------------*/


//	// Attach handlers to `this.el`, using bubbling to listen to all ancestors.
//	// We don't need to undo any of this in a "destroy" method, because the view will simply remove `this.el` from the
//	// DOM and jQuery will be smart enough to garbage collect the handlers.
//	bindHandlers: function() {
//		var _this = this;

//		this.el.on('mousedown', function(ev) {
//			if (
//				!$(ev.target).is('.fc-event-container *, .fc-more') && // not an an event element, or "more.." link
//				!$(ev.target).closest('.fc-popover').length // not on a popover (like the "more.." events one)
//			) {
//				_this.dayMousedown(ev);
//			}
//		});

//		this.bindSegHandlers(); // attach event-element-related handlers. in Grid.events.js
//	},


//	// Process a mousedown on an element that represents a day. For day clicking and selecting.
//	dayMousedown: function(ev) {
//		var _this = this;
//		var view = this.view;
//		var isSelectable = view.opt('selectable');
//		var dates = null; // the inclusive dates of the selection. will be null if no selection
//		var start; // the inclusive start of the selection
//		var end; // the *exclusive* end of the selection
//		var dayEl;

//		// this listener tracks a mousedown on a day element, and a subsequent drag.
//		// if the drag ends on the same day, it is a 'dayClick'.
//		// if 'selectable' is enabled, this listener also detects selections.
//		var dragListener = new DragListener(this.coordMap, {
//			//distance: 5, // needs more work if we want dayClick to fire correctly
//			scroll: view.opt('dragScroll'),
//			dragStart: function() {
//				view.unselect(); // since we could be rendering a new selection, we want to clear any old one
//			},
//			cellOver: function(cell, date) {
//				if (dragListener.origDate) { // click needs to have started on a cell

//					dayEl = _this.getCellDayEl(cell);

//					dates = [ date, dragListener.origDate ].sort(dateCompare);
//					start = dates[0];
//					end = dates[1].clone().add(_this.cellDuration);

//					if (isSelectable) {
//						_this.renderSelection(start, end);
//					}
//				}
//			},
//			cellOut: function(cell, date) {
//				dates = null;
//				_this.destroySelection();
//			},
//			listenStop: function(ev) {
//				if (dates) { // started and ended on a cell?
//					if (dates[0].isSame(dates[1])) {
//						view.trigger('dayClick', dayEl[0], start, ev);
//					}
//					if (isSelectable) {
//						// the selection will already have been rendered. just report it
//						view.reportSelection(start, end, ev);
//					}
//				}
//			}
//		});

//		dragListener.mousedown(ev); // start listening, which will eventually initiate a dragStart
//	},


//	/* Event Dragging
//	------------------------------------------------------------------------------------------------------------------*/


//	// Renders a visual indication of a event being dragged over the given date(s).
//	// `end` can be null, as well as `seg`. See View's documentation on renderDrag for more info.
//	// A returned value of `true` signals that a mock "helper" event has been rendered.
//	renderDrag: function(start, end, seg) {
//		// subclasses must implement
//	},


//	// Unrenders a visual indication of an event being dragged
//	destroyDrag: function() {
//		// subclasses must implement
//	},


//	/* Event Resizing
//	------------------------------------------------------------------------------------------------------------------*/


//	// Renders a visual indication of an event being resized.
//	// `start` and `end` are the updated dates of the event. `seg` is the original segment object involved in the drag.
//	renderResize: function(start, end, seg) {
//		// subclasses must implement
//	},


//	// Unrenders a visual indication of an event being resized.
//	destroyResize: function() {
//		// subclasses must implement
//	},


//	/* Event Helper
//	------------------------------------------------------------------------------------------------------------------*/


//	// Renders a mock event over the given date(s).
//	// `end` can be null, in which case the mock event that is rendered will have a null end time.
//	// `sourceSeg` is the internal segment object involved in the drag. If null, something external is dragging.
//	renderRangeHelper: function(start, end, sourceSeg) {
//		var view = this.view;
//		var fakeEvent;

//		// compute the end time if forced to do so (this is what EventManager does)
//		if (!end && view.opt('forceEventDuration')) {
//			end = view.calendar.getDefaultEventEnd(!start.hasTime(), start);
//		}

//		fakeEvent = sourceSeg ? createObject(sourceSeg.event) : {}; // mask the original event object if possible
//		fakeEvent.start = start;
//		fakeEvent.end = end;
//		fakeEvent.allDay = !(start.hasTime() || (end && end.hasTime())); // freshly compute allDay

//		// this extra className will be useful for differentiating real events from mock events in CSS
//		fakeEvent.className = (fakeEvent.className || []).concat('fc-helper');

//		// if something external is being dragged in, don't render a resizer
//		if (!sourceSeg) {
//			fakeEvent.editable = false;
//		}

//		this.renderHelper(fakeEvent, sourceSeg); // do the actual rendering
//	},


//	// Renders a mock event
//	renderHelper: function(event, sourceSeg) {
//		// subclasses must implement
//	},


//	// Unrenders a mock event
//	destroyHelper: function() {
//		// subclasses must implement
//	},


//	/* Selection
//	------------------------------------------------------------------------------------------------------------------*/


//	// Renders a visual indication of a selection. Will highlight by default but can be overridden by subclasses.
//	renderSelection: function(start, end) {
//		this.renderHighlight(start, end);
//	},


//	// Unrenders any visual indications of a selection. Will unrender a highlight by default.
//	destroySelection: function() {
//		this.destroyHighlight();
//	},


//	/* Highlight
//	------------------------------------------------------------------------------------------------------------------*/


//	// Puts visual emphasis on a certain date range
//	renderHighlight: function(start, end) {
//		// subclasses should implement
//	},


//	// Removes visual emphasis on a date range
//	destroyHighlight: function() {
//		// subclasses should implement
//	},



//	/* Generic rendering utilities for subclasses
//	------------------------------------------------------------------------------------------------------------------*/


//	// Renders a day-of-week header row
//	headHtml: function() {
//		return '' +
//			'<div class="fc-row ' + this.view.widgetHeaderClass + '">' +
//				'<table>' +
//					'<thead>' +
//						this.rowHtml('head') + // leverages RowRenderer
//					'</thead>' +
//				'</table>' +
//			'</div>';
//	},


//	// Used by the `headHtml` method, via RowRenderer, for rendering the HTML of a day-of-week header cell
//	headCellHtml: function(row, col, date) {
//		var view = this.view;
//		var calendar = view.calendar;
//		var colFormat = view.opt('columnFormat');

//		return '' +
//			'<th class="fc-day-header ' + view.widgetHeaderClass + ' fc-' + dayIDs[date.day()] + '">' +
//				htmlEscape(calendar.formatDate(date, colFormat)) +
//			'</th>';
//	},


//	// Renders the HTML for a single-day background cell
//	bgCellHtml: function(row, col, date) {
//		var view = this.view;
//		var classes = this.getDayClasses(date);

//		classes.unshift('fc-day', view.widgetContentClass);

//		return '<td class="' + classes.join(' ') + '" data-date="' + date.format() + '"></td>';
//	},


//	// Computes HTML classNames for a single-day cell
//	getDayClasses: function(date) {
//		var view = this.view;
//		var today = view.calendar.getNow().stripTime();
//		var classes = [ 'fc-' + dayIDs[date.day()] ];

//		if (
//			view.name === 'month' &&
//			date.month() != view.intervalStart.month()
//		) {
//			classes.push('fc-other-month');
//		}

//		if (date.isSame(today, 'day')) {
//			classes.push(
//				'fc-today',
//				view.highlightStateClass
//			);
//		}
//		else if (date < today) {
//			classes.push('fc-past');
//		}
//		else {
//			classes.push('fc-future');
//		}

//		return classes;
//	}

//});

//;;

///* Event-rendering and event-interaction methods for the abstract Grid class
//----------------------------------------------------------------------------------------------------------------------*/

//$.extend(Grid.prototype, {

//	mousedOverSeg: null, // the segment object the user's mouse is over. null if over nothing
//	isDraggingSeg: false, // is a segment being dragged? boolean
//	isResizingSeg: false, // is a segment being resized? boolean


//	// Renders the given events onto the grid
//	renderEvents: function(events) {
//		// subclasses must implement
//	},


//	// Retrieves all rendered segment objects in this grid
//	getSegs: function() {
//		// subclasses must implement
//	},


//	// Unrenders all events. Subclasses should implement, calling this super-method first.
//	destroyEvents: function() {
//		this.triggerSegMouseout(); // trigger an eventMouseout if user's mouse is over an event
//	},


//	// Renders a `el` property for each seg, and only returns segments that successfully rendered
//	renderSegs: function(segs, disableResizing) {
//		var view = this.view;
//		var html = '';
//		var renderedSegs = [];
//		var i;

//		// build a large concatenation of event segment HTML
//		for (i = 0; i < segs.length; i++) {
//			html += this.renderSegHtml(segs[i], disableResizing);
//		}

//		// Grab individual elements from the combined HTML string. Use each as the default rendering.
//		// Then, compute the 'el' for each segment. An el might be null if the eventRender callback returned false.
//		$(html).each(function(i, node) {
//			var seg = segs[i];
//			var el = view.resolveEventEl(seg.event, $(node));
//			if (el) {
//				el.data('fc-seg', seg); // used by handlers
//				seg.el = el;
//				renderedSegs.push(seg);
//			}
//		});

//		return renderedSegs;
//	},


//	// Generates the HTML for the default rendering of a segment
//	renderSegHtml: function(seg, disableResizing) {
//		// subclasses must implement
//	},


//	// Converts an array of event objects into an array of segment objects
//	eventsToSegs: function(events, intervalStart, intervalEnd) {
//		var _this = this;

//		return $.map(events, function(event) {
//			return _this.eventToSegs(event, intervalStart, intervalEnd); // $.map flattens all returned arrays together
//		});
//	},


//	// Slices a single event into an array of event segments.
//	// When `intervalStart` and `intervalEnd` are specified, intersect the events with that interval.
//	// Otherwise, let the subclass decide how it wants to slice the segments over the grid.
//	eventToSegs: function(event, intervalStart, intervalEnd) {
//		var eventStart = event.start.clone().stripZone(); // normalize
//		var eventEnd = this.view.calendar.getEventEnd(event).stripZone(); // compute (if necessary) and normalize
//		var segs;
//		var i, seg;

//		if (intervalStart && intervalEnd) {
//			seg = intersectionToSeg(eventStart, eventEnd, intervalStart, intervalEnd);
//			segs = seg ? [ seg ] : [];
//		}
//		else {
//			segs = this.rangeToSegs(eventStart, eventEnd); // defined by the subclass
//		}

//		// assign extra event-related properties to the segment objects
//		for (i = 0; i < segs.length; i++) {
//			seg = segs[i];
//			seg.event = event;
//			seg.eventStartMS = +eventStart;
//			seg.eventDurationMS = eventEnd - eventStart;
//		}

//		return segs;
//	},


//	/* Handlers
//	------------------------------------------------------------------------------------------------------------------*/


//	// Attaches event-element-related handlers to the container element and leverage bubbling
//	bindSegHandlers: function() {
//		var _this = this;
//		var view = this.view;

//		$.each(
//			{
//				mouseenter: function(seg, ev) {
//					_this.triggerSegMouseover(seg, ev);
//				},
//				mouseleave: function(seg, ev) {
//					_this.triggerSegMouseout(seg, ev);
//				},
//				click: function(seg, ev) {
//					return view.trigger('eventClick', this, seg.event, ev); // can return `false` to cancel
//				},
//				mousedown: function(seg, ev) {
//					if ($(ev.target).is('.fc-resizer') && view.isEventResizable(seg.event)) {
//						_this.segResizeMousedown(seg, ev);
//					}
//					else if (view.isEventDraggable(seg.event)) {
//						_this.segDragMousedown(seg, ev);
//					}
//				}
//			},
//			function(name, func) {
//				// attach the handler to the container element and only listen for real event elements via bubbling
//				_this.el.on(name, '.fc-event-container > *', function(ev) {
//					var seg = $(this).data('fc-seg'); // grab segment data. put there by View::renderEvents

//					// only call the handlers if there is not a drag/resize in progress
//					if (seg && !_this.isDraggingSeg && !_this.isResizingSeg) {
//						return func.call(this, seg, ev); // `this` will be the event element
//					}
//				});
//			}
//		);
//	},


//	// Updates internal state and triggers handlers for when an event element is moused over
//	triggerSegMouseover: function(seg, ev) {
//		if (!this.mousedOverSeg) {
//			this.mousedOverSeg = seg;
//			this.view.trigger('eventMouseover', seg.el[0], seg.event, ev);
//		}
//	},


//	// Updates internal state and triggers handlers for when an event element is moused out.
//	// Can be given no arguments, in which case it will mouseout the segment that was previously moused over.
//	triggerSegMouseout: function(seg, ev) {
//		ev = ev || {}; // if given no args, make a mock mouse event

//		if (this.mousedOverSeg) {
//			seg = seg || this.mousedOverSeg; // if given no args, use the currently moused-over segment
//			this.mousedOverSeg = null;
//			this.view.trigger('eventMouseout', seg.el[0], seg.event, ev);
//		}
//	},


//	/* Dragging
//	------------------------------------------------------------------------------------------------------------------*/


//	// Called when the user does a mousedown on an event, which might lead to dragging.
//	// Generic enough to work with any type of Grid.
//	segDragMousedown: function(seg, ev) {
//		var _this = this;
//		var view = this.view;
//		var el = seg.el;
//		var event = seg.event;
//		var newStart, newEnd;

//		// A clone of the original element that will move with the mouse
//		var mouseFollower = new MouseFollower(seg.el, {
//			parentEl: view.el,
//			opacity: view.opt('dragOpacity'),
//			revertDuration: view.opt('dragRevertDuration'),
//			zIndex: 2 // one above the .fc-view
//		});

//		// Tracks mouse movement over the *view's* coordinate map. Allows dragging and dropping between subcomponents
//		// of the view.
//		var dragListener = new DragListener(view.coordMap, {
//			distance: 5,
//			scroll: view.opt('dragScroll'),
//			listenStart: function(ev) {
//				mouseFollower.hide(); // don't show until we know this is a real drag
//				mouseFollower.start(ev);
//			},
//			dragStart: function(ev) {
//				_this.triggerSegMouseout(seg, ev); // ensure a mouseout on the manipulated event has been reported
//				_this.isDraggingSeg = true;
//				view.hideEvent(event); // hide all event segments. our mouseFollower will take over
//				view.trigger('eventDragStart', el[0], event, ev, {}); // last argument is jqui dummy
//			},
//			cellOver: function(cell, date) {
//				var origDate = seg.cellDate || dragListener.origDate;
//				var res = _this.computeDraggedEventDates(seg, origDate, date);
//				newStart = res.start;
//				newEnd = res.end;

//				if (view.renderDrag(newStart, newEnd, seg)) { // have the view render a visual indication
//					mouseFollower.hide(); // if the view is already using a mock event "helper", hide our own
//				}
//				else {
//					mouseFollower.show();
//				}
//			},
//			cellOut: function() { // called before mouse moves to a different cell OR moved out of all cells
//				newStart = null;
//				view.destroyDrag(); // unrender whatever was done in view.renderDrag
//				mouseFollower.show(); // show in case we are moving out of all cells
//			},
//			dragStop: function(ev) {
//				var hasChanged = newStart && !newStart.isSame(event.start);

//				// do revert animation if hasn't changed. calls a callback when finished (whether animation or not)
//				mouseFollower.stop(!hasChanged, function() {
//					_this.isDraggingSeg = false;
//					view.destroyDrag();
//					view.showEvent(event);
//					view.trigger('eventDragStop', el[0], event, ev, {}); // last argument is jqui dummy

//					if (hasChanged) {
//						view.eventDrop(el[0], event, newStart, ev); // will rerender all events...
//					}
//				});
//			},
//			listenStop: function() {
//				mouseFollower.stop(); // put in listenStop in case there was a mousedown but the drag never started
//			}
//		});

//		dragListener.mousedown(ev); // start listening, which will eventually lead to a dragStart
//	},


//	// Given a segment, the dates where a drag began and ended, calculates the Event Object's new start and end dates
//	computeDraggedEventDates: function(seg, dragStartDate, dropDate) {
//		var view = this.view;
//		var event = seg.event;
//		var start = event.start;
//		var end = view.calendar.getEventEnd(event);
//		var delta;
//		var newStart;
//		var newEnd;

//		if (dropDate.hasTime() === dragStartDate.hasTime()) {
//			delta = dayishDiff(dropDate, dragStartDate);
//			newStart = start.clone().add(delta);
//			if (event.end === null) { // do we need to compute an end?
//				newEnd = null;
//			}
//			else {
//				newEnd = end.clone().add(delta);
//			}
//		}
//		else {
//			// if switching from day <-> timed, start should be reset to the dropped date, and the end cleared
//			newStart = dropDate;
//			newEnd = null; // end should be cleared
//		}

//		return { start: newStart, end: newEnd };
//	},


//	/* Resizing
//	------------------------------------------------------------------------------------------------------------------*/


//	// Called when the user does a mousedown on an event's resizer, which might lead to resizing.
//	// Generic enough to work with any type of Grid.
//	segResizeMousedown: function(seg, ev) {
//		var _this = this;
//		var view = this.view;
//		var el = seg.el;
//		var event = seg.event;
//		var start = event.start;
//		var end = view.calendar.getEventEnd(event);
//		var newEnd = null;
//		var dragListener;

//		function destroy() { // resets the rendering
//			_this.destroyResize();
//			view.showEvent(event);
//		}

//		// Tracks mouse movement over the *grid's* coordinate map
//		dragListener = new DragListener(this.coordMap, {
//			distance: 5,
//			scroll: view.opt('dragScroll'),
//			dragStart: function(ev) {
//				_this.triggerSegMouseout(seg, ev); // ensure a mouseout on the manipulated event has been reported
//				_this.isResizingSeg = true;
//				view.trigger('eventResizeStart', el[0], event, ev, {}); // last argument is jqui dummy
//			},
//			cellOver: function(cell, date) {
//				// compute the new end. don't allow it to go before the event's start
//				if (date.isBefore(start)) { // allows comparing ambig to non-ambig
//					date = start;
//				}
//				newEnd = date.clone().add(_this.cellDuration); // make it an exclusive end

//				if (newEnd.isSame(end)) {
//					newEnd = null;
//					destroy();
//				}
//				else {
//					_this.renderResize(start, newEnd, seg);
//					view.hideEvent(event);
//				}
//			},
//			cellOut: function() { // called before mouse moves to a different cell OR moved out of all cells
//				newEnd = null;
//				destroy();
//			},
//			dragStop: function(ev) {
//				_this.isResizingSeg = false;
//				destroy();
//				view.trigger('eventResizeStop', el[0], event, ev, {}); // last argument is jqui dummy

//				if (newEnd) {
//					view.eventResize(el[0], event, newEnd, ev); // will rerender all events...
//				}
//			}
//		});

//		dragListener.mousedown(ev); // start listening, which will eventually lead to a dragStart
//	},


//	/* Rendering Utils
//	------------------------------------------------------------------------------------------------------------------*/


//	// Generic utility for generating the HTML classNames for an event segment's element
//	getSegClasses: function(seg, isDraggable, isResizable) {
//		var event = seg.event;
//		var classes = [
//			'fc-event',
//			seg.isStart ? 'fc-start' : 'fc-not-start',
//			seg.isEnd ? 'fc-end' : 'fc-not-end'
//		].concat(
//			event.className,
//			event.source ? event.source.className : []
//		);

//		if (isDraggable) {
//			classes.push('fc-draggable');
//		}
//		if (isResizable) {
//			classes.push('fc-resizable');
//		}

//		return classes;
//	},


//	// Utility for generating a CSS string with all the event skin-related properties
//	getEventSkinCss: function(event) {
//		var view = this.view;
//		var source = event.source || {};
//		var eventColor = event.color;
//		var sourceColor = source.color;
//		var optionColor = view.opt('eventColor');
//		var backgroundColor =
//			event.backgroundColor ||
//			eventColor ||
//			source.backgroundColor ||
//			sourceColor ||
//			view.opt('eventBackgroundColor') ||
//			optionColor;
//		var borderColor =
//			event.borderColor ||
//			eventColor ||
//			source.borderColor ||
//			sourceColor ||
//			view.opt('eventBorderColor') ||
//			optionColor;
//		var textColor =
//			event.textColor ||
//			source.textColor ||
//			view.opt('eventTextColor');
//		var statements = [];
//		if (backgroundColor) {
//			statements.push('background-color:' + backgroundColor);
//		}
//		if (borderColor) {
//			statements.push('border-color:' + borderColor);
//		}
//		if (textColor) {
//			statements.push('color:' + textColor);
//		}
//		return statements.join(';');
//	}

//});


///* Event Segment Utilities
//----------------------------------------------------------------------------------------------------------------------*/


//// A cmp function for determining which segments should take visual priority
//function compareSegs(seg1, seg2) {
//	return seg1.eventStartMS - seg2.eventStartMS || // earlier events go first
//		seg2.eventDurationMS - seg1.eventDurationMS || // tie? longer events go first
//		seg2.event.allDay - seg1.event.allDay || // tie? put all-day events first (booleans cast to 0/1)
//		(seg1.event.title || '').localeCompare(seg2.event.title); // tie? alphabetically by title
//}


//;;

///* A component that renders a grid of whole-days that runs horizontally. There can be multiple rows, one per week.
//----------------------------------------------------------------------------------------------------------------------*/

//function DayGrid(view) {
//	Grid.call(this, view); // call the super-constructor
//}


//DayGrid.prototype = createObject(Grid.prototype); // declare the super-class
//$.extend(DayGrid.prototype, {

//	numbersVisible: false, // should render a row for day/week numbers? manually set by the view
//	cellDuration: moment.duration({ days: 1 }), // required for Grid.event.js. Each cell is always a single day
//	bottomCoordPadding: 0, // hack for extending the hit area for the last row of the coordinate grid

//	rowEls: null, // set of fake row elements
//	dayEls: null, // set of whole-day elements comprising the row's background
//	helperEls: null, // set of cell skeleton elements for rendering the mock event "helper"
//	highlightEls: null, // set of cell skeleton elements for rendering the highlight


//	// Renders the rows and columns into the component's `this.el`, which should already be assigned.
//	// isRigid determins whether the individual rows should ignore the contents and be a constant height.
//	// Relies on the view's colCnt and rowCnt. In the future, this component should probably be self-sufficient.
//	render: function(isRigid) {
//		var view = this.view;
//		var html = '';
//		var row;

//		for (row = 0; row < view.rowCnt; row++) {
//			html += this.dayRowHtml(row, isRigid);
//		}
//		this.el.html(html);

//		this.rowEls = this.el.find('.fc-row');
//		this.dayEls = this.el.find('.fc-day');

//		// run all the day cells through the dayRender callback
//		this.dayEls.each(function(i, node) {
//			var date = view.cellToDate(Math.floor(i / view.colCnt), i % view.colCnt);
//			view.trigger('dayRender', null, date, $(node));
//		});

//		Grid.prototype.render.call(this); // call the super-method
//	},


//	destroy: function() {
//		this.destroySegPopover();
//	},


//	// Generates the HTML for a single row. `row` is the row number.
//	dayRowHtml: function(row, isRigid) {
//		var view = this.view;
//		var classes = [ 'fc-row', 'fc-week', view.widgetContentClass ];

//		if (isRigid) {
//			classes.push('fc-rigid');
//		}

//		return '' +
//			'<div class="' + classes.join(' ') + '">' +
//				'<div class="fc-bg">' +
//					'<table>' +
//						this.rowHtml('day', row) + // leverages RowRenderer. calls dayCellHtml()
//					'</table>' +
//				'</div>' +
//				'<div class="fc-content-skeleton">' +
//					'<table>' +
//						(this.numbersVisible ?
//							'<thead>' +
//								this.rowHtml('number', row) + // leverages RowRenderer. View will define render method
//							'</thead>' :
//							''
//							) +
//					'</table>' +
//				'</div>' +
//			'</div>';
//	},


//	// Renders the HTML for a whole-day cell. Will eventually end up in the day-row's background.
//	// We go through a 'day' row type instead of just doing a 'bg' row type so that the View can do custom rendering
//	// specifically for whole-day rows, whereas a 'bg' might also be used for other purposes (TimeGrid bg for example).
//	dayCellHtml: function(row, col, date) {
//		return this.bgCellHtml(row, col, date);
//	},


//	/* Coordinates & Cells
//	------------------------------------------------------------------------------------------------------------------*/


//	// Populates the empty `rows` and `cols` arrays with coordinates of the cells. For CoordGrid.
//	buildCoords: function(rows, cols) {
//		var colCnt = this.view.colCnt;
//		var e, n, p;

//		this.dayEls.slice(0, colCnt).each(function(i, _e) { // iterate the first row of day elements
//			e = $(_e);
//			n = e.offset().left;
//			if (i) {
//				p[1] = n;
//			}
//			p = [ n ];
//			cols[i] = p;
//		});
//		p[1] = n + e.outerWidth();

//		this.rowEls.each(function(i, _e) {
//			e = $(_e);
//			n = e.offset().top;
//			if (i) {
//				p[1] = n;
//			}
//			p = [ n ];
//			rows[i] = p;
//		});
//		p[1] = n + e.outerHeight() + this.bottomCoordPadding; // hack to extend hit area of last row
//	},


//	// Converts a cell to a date
//	getCellDate: function(cell) {
//		return this.view.cellToDate(cell); // leverages the View's cell system
//	},


//	// Gets the whole-day element associated with the cell
//	getCellDayEl: function(cell) {
//		return this.dayEls.eq(cell.row * this.view.colCnt + cell.col);
//	},


//	// Converts a range with an inclusive `start` and an exclusive `end` into an array of segment objects
//	rangeToSegs: function(start, end) {
//		return this.view.rangeToSegments(start, end); // leverages the View's cell system
//	},


//	/* Event Drag Visualization
//	------------------------------------------------------------------------------------------------------------------*/


//	// Renders a visual indication of an event hovering over the given date(s).
//	// `end` can be null, as well as `seg`. See View's documentation on renderDrag for more info.
//	// A returned value of `true` signals that a mock "helper" event has been rendered.
//	renderDrag: function(start, end, seg) {
//		var opacity;

//		// always render a highlight underneath
//		this.renderHighlight(
//			start,
//			end || this.view.calendar.getDefaultEventEnd(true, start)
//		);

//		// if a segment from the same calendar but another component is being dragged, render a helper event
//		if (seg && !seg.el.closest(this.el).length) {

//			this.renderRangeHelper(start, end, seg);

//			opacity = this.view.opt('dragOpacity');
//			if (opacity !== undefined) {
//				this.helperEls.css('opacity', opacity);
//			}

//			return true; // a helper has been rendered
//		}
//	},


//	// Unrenders any visual indication of a hovering event
//	destroyDrag: function() {
//		this.destroyHighlight();
//		this.destroyHelper();
//	},


//	/* Event Resize Visualization
//	------------------------------------------------------------------------------------------------------------------*/


//	// Renders a visual indication of an event being resized
//	renderResize: function(start, end, seg) {
//		this.renderHighlight(start, end);
//		this.renderRangeHelper(start, end, seg);
//	},


//	// Unrenders a visual indication of an event being resized
//	destroyResize: function() {
//		this.destroyHighlight();
//		this.destroyHelper();
//	},


//	/* Event Helper
//	------------------------------------------------------------------------------------------------------------------*/


//	// Renders a mock "helper" event. `sourceSeg` is the associated internal segment object. It can be null.
//	renderHelper: function(event, sourceSeg) {
//		var helperNodes = [];
//		var rowStructs = this.renderEventRows([ event ]);

//		// inject each new event skeleton into each associated row
//		this.rowEls.each(function(row, rowNode) {
//			var rowEl = $(rowNode); // the .fc-row
//			var skeletonEl = $('<div class="fc-helper-skeleton"><table/></div>'); // will be absolutely positioned
//			var skeletonTop;

//			// If there is an original segment, match the top position. Otherwise, put it at the row's top level
//			if (sourceSeg && sourceSeg.row === row) {
//				skeletonTop = sourceSeg.el.position().top;
//			}
//			else {
//				skeletonTop = rowEl.find('.fc-content-skeleton tbody').position().top;
//			}

//			skeletonEl.css('top', skeletonTop)
//				.find('table')
//					.append(rowStructs[row].tbodyEl);

//			rowEl.append(skeletonEl);
//			helperNodes.push(skeletonEl[0]);
//		});

//		this.helperEls = $(helperNodes); // array -> jQuery set
//	},


//	// Unrenders any visual indication of a mock helper event
//	destroyHelper: function() {
//		if (this.helperEls) {
//			this.helperEls.remove();
//			this.helperEls = null;
//		}
//	},


//	/* Highlighting
//	------------------------------------------------------------------------------------------------------------------*/


//	// Renders an emphasis on the given date range. `start` is an inclusive, `end` is exclusive.
//	renderHighlight: function(start, end) {
//		var segs = this.rangeToSegs(start, end);
//		var highlightNodes = [];
//		var i, seg;
//		var el;

//		// build an event skeleton for each row that needs it
//		for (i = 0; i < segs.length; i++) {
//			seg = segs[i];
//			el = $(
//				this.highlightSkeletonHtml(seg.leftCol, seg.rightCol + 1) // make end exclusive
//			);
//			el.appendTo(this.rowEls[seg.row]);
//			highlightNodes.push(el[0]);
//		}

//		this.highlightEls = $(highlightNodes); // array -> jQuery set
//	},


//	// Unrenders any visual emphasis on a date range
//	destroyHighlight: function() {
//		if (this.highlightEls) {
//			this.highlightEls.remove();
//			this.highlightEls = null;
//		}
//	},


//	// Generates the HTML used to build a single-row "highlight skeleton", a table that frames highlight cells
//	highlightSkeletonHtml: function(startCol, endCol) {
//		var colCnt = this.view.colCnt;
//		var cellHtml = '';

//		if (startCol > 0) {
//			cellHtml += '<td colspan="' + startCol + '"/>';
//		}
//		if (endCol > startCol) {
//			cellHtml += '<td colspan="' + (endCol - startCol) + '" class="fc-highlight" />';
//		}
//		if (colCnt > endCol) {
//			cellHtml += '<td colspan="' + (colCnt - endCol) + '"/>';
//		}

//		cellHtml = this.bookendCells(cellHtml, 'highlight');

//		return '' +
//			'<div class="fc-highlight-skeleton">' +
//				'<table>' +
//					'<tr>' +
//						cellHtml +
//					'</tr>' +
//				'</table>' +
//			'</div>';
//	}

//});

//;;

///* Event-rendering methods for the DayGrid class
//----------------------------------------------------------------------------------------------------------------------*/

//$.extend(DayGrid.prototype, {

//	segs: null,
//	rowStructs: null, // an array of objects, each holding information about a row's event-rendering


//	// Render the given events onto the Grid and return the rendered segments
//	renderEvents: function(events) {
//		var rowStructs = this.rowStructs = this.renderEventRows(events);
//		var segs = [];

//		// append to each row's content skeleton
//		this.rowEls.each(function(i, rowNode) {
//			$(rowNode).find('.fc-content-skeleton > table').append(
//				rowStructs[i].tbodyEl
//			);
//			segs.push.apply(segs, rowStructs[i].segs);
//		});

//		this.segs = segs;
//	},


//	// Retrieves all segment objects that have been rendered
//	getSegs: function() {
//		return (this.segs || []).concat(
//			this.popoverSegs || [] // segs rendered in the "more" events popover
//		);
//	},


//	// Removes all rendered event elements
//	destroyEvents: function() {
//		var rowStructs;
//		var rowStruct;

//		Grid.prototype.destroyEvents.call(this); // call the super-method

//		rowStructs = this.rowStructs || [];
//		while ((rowStruct = rowStructs.pop())) {
//			rowStruct.tbodyEl.remove();
//		}

//		this.segs = null;
//		this.destroySegPopover(); // removes the "more.." events popover
//	},


//	// Uses the given events array to generate <tbody> elements that should be appended to each row's content skeleton.
//	// Returns an array of rowStruct objects (see the bottom of `renderEventRow`).
//	renderEventRows: function(events) {
//		var segs = this.eventsToSegs(events);
//		var rowStructs = [];
//		var segRows;
//		var row;

//		segs = this.renderSegs(segs); // returns a new array with only visible segments
//		segRows = this.groupSegRows(segs); // group into nested arrays

//		// iterate each row of segment groupings
//		for (row = 0; row < segRows.length; row++) {
//			rowStructs.push(
//				this.renderEventRow(row, segRows[row])
//			);
//		}

//		return rowStructs;
//	},


//	// Builds the HTML to be used for the default element for an individual segment
//	renderSegHtml: function(seg, disableResizing) {
//		var view = this.view;
//		var isRTL = view.opt('isRTL');
//		var event = seg.event;
//		var isDraggable = view.isEventDraggable(event);
//		var isResizable = !disableResizing && event.allDay && seg.isEnd && view.isEventResizable(event);
//		var classes = this.getSegClasses(seg, isDraggable, isResizable);
//		var skinCss = this.getEventSkinCss(event);
//		var timeHtml = '';
//		var titleHtml;

//		classes.unshift('fc-day-grid-event');

//		// Only display a timed events time if it is the starting segment
//		if (!event.allDay && seg.isStart) {
//			timeHtml = '<span class="fc-time">' + htmlEscape(view.getEventTimeText(event)) + '</span>';
//		}

//		titleHtml =
//			'<span class="fc-title">' +
//				(htmlEscape(event.title || '') || '&nbsp;') + // we always want one line of height
//			'</span>';
		
//		return '<a class="' + classes.join(' ') + '"' +
//				(event.url ?
//					' href="' + htmlEscape(event.url) + '"' :
//					''
//					) +
//				(skinCss ?
//					' style="' + skinCss + '"' :
//					''
//					) +
//			'>' +
//				'<div class="fc-content">' +
//					(isRTL ?
//						titleHtml + ' ' + timeHtml : // put a natural space in between
//						timeHtml + ' ' + titleHtml   //
//						) +
//				'</div>' +
//				(isResizable ?
//					'<div class="fc-resizer"/>' :
//					''
//					) +
//			'</a>';
//	},


//	// Given a row # and an array of segments all in the same row, render a <tbody> element, a skeleton that contains
//	// the segments. Returns object with a bunch of internal data about how the render was calculated.
//	renderEventRow: function(row, rowSegs) {
//		var view = this.view;
//		var colCnt = view.colCnt;
//		var segLevels = this.buildSegLevels(rowSegs); // group into sub-arrays of levels
//		var levelCnt = Math.max(1, segLevels.length); // ensure at least one level
//		var tbody = $('<tbody/>');
//		var segMatrix = []; // lookup for which segments are rendered into which level+col cells
//		var cellMatrix = []; // lookup for all <td> elements of the level+col matrix
//		var loneCellMatrix = []; // lookup for <td> elements that only take up a single column
//		var i, levelSegs;
//		var col;
//		var tr;
//		var j, seg;
//		var td;

//		// populates empty cells from the current column (`col`) to `endCol`
//		function emptyCellsUntil(endCol) {
//			while (col < endCol) {
//				// try to grab a cell from the level above and extend its rowspan. otherwise, create a fresh cell
//				td = (loneCellMatrix[i - 1] || [])[col];
//				if (td) {
//					td.attr(
//						'rowspan',
//						parseInt(td.attr('rowspan') || 1, 10) + 1
//					);
//				}
//				else {
//					td = $('<td/>');
//					tr.append(td);
//				}
//				cellMatrix[i][col] = td;
//				loneCellMatrix[i][col] = td;
//				col++;
//			}
//		}

//		for (i = 0; i < levelCnt; i++) { // iterate through all levels
//			levelSegs = segLevels[i];
//			col = 0;
//			tr = $('<tr/>');

//			segMatrix.push([]);
//			cellMatrix.push([]);
//			loneCellMatrix.push([]);

//			// levelCnt might be 1 even though there are no actual levels. protect against this.
//			// this single empty row is useful for styling.
//			if (levelSegs) {
//				for (j = 0; j < levelSegs.length; j++) { // iterate through segments in level
//					seg = levelSegs[j];

//					emptyCellsUntil(seg.leftCol);

//					// create a container that occupies or more columns. append the event element.
//					td = $('<td class="fc-event-container"/>').append(seg.el);
//					if (seg.leftCol != seg.rightCol) {
//						td.attr('colspan', seg.rightCol - seg.leftCol + 1);
//					}
//					else { // a single-column segment
//						loneCellMatrix[i][col] = td;
//					}

//					while (col <= seg.rightCol) {
//						cellMatrix[i][col] = td;
//						segMatrix[i][col] = seg;
//						col++;
//					}

//					tr.append(td);
//				}
//			}

//			emptyCellsUntil(colCnt); // finish off the row
//			this.bookendCells(tr, 'eventSkeleton');
//			tbody.append(tr);
//		}

//		return { // a "rowStruct"
//			row: row, // the row number
//			tbodyEl: tbody,
//			cellMatrix: cellMatrix,
//			segMatrix: segMatrix,
//			segLevels: segLevels,
//			segs: rowSegs
//		};
//	},


//	// Stacks a flat array of segments, which are all assumed to be in the same row, into subarrays of vertical levels.
//	buildSegLevels: function(segs) {
//		var levels = [];
//		var i, seg;
//		var j;

//		// Give preference to elements with certain criteria, so they have
//		// a chance to be closer to the top.
//		segs.sort(compareSegs);
		
//		for (i = 0; i < segs.length; i++) {
//			seg = segs[i];

//			// loop through levels, starting with the topmost, until the segment doesn't collide with other segments
//			for (j = 0; j < levels.length; j++) {
//				if (!isDaySegCollision(seg, levels[j])) {
//					break;
//				}
//			}
//			// `j` now holds the desired subrow index
//			seg.level = j;

//			// create new level array if needed and append segment
//			(levels[j] || (levels[j] = [])).push(seg);
//		}

//		// order segments left-to-right. very important if calendar is RTL
//		for (j = 0; j < levels.length; j++) {
//			levels[j].sort(compareDaySegCols);
//		}

//		return levels;
//	},


//	// Given a flat array of segments, return an array of sub-arrays, grouped by each segment's row
//	groupSegRows: function(segs) {
//		var view = this.view;
//		var segRows = [];
//		var i;

//		for (i = 0; i < view.rowCnt; i++) {
//			segRows.push([]);
//		}

//		for (i = 0; i < segs.length; i++) {
//			segRows[segs[i].row].push(segs[i]);
//		}

//		return segRows;
//	}

//});


//// Computes whether two segments' columns collide. They are assumed to be in the same row.
//function isDaySegCollision(seg, otherSegs) {
//	var i, otherSeg;

//	for (i = 0; i < otherSegs.length; i++) {
//		otherSeg = otherSegs[i];

//		if (
//			otherSeg.leftCol <= seg.rightCol &&
//			otherSeg.rightCol >= seg.leftCol
//		) {
//			return true;
//		}
//	}

//	return false;
//}


//// A cmp function for determining the leftmost event
//function compareDaySegCols(a, b) {
//	return a.leftCol - b.leftCol;
//}

//;;

///* Methods relate to limiting the number events for a given day on a DayGrid
//----------------------------------------------------------------------------------------------------------------------*/

//$.extend(DayGrid.prototype, {


//	segPopover: null, // the Popover that holds events that can't fit in a cell. null when not visible
//	popoverSegs: null, // an array of segment objects that the segPopover holds. null when not visible


//	destroySegPopover: function() {
//		if (this.segPopover) {
//			this.segPopover.hide(); // will trigger destruction of `segPopover` and `popoverSegs`
//		}
//	},


//	// Limits the number of "levels" (vertically stacking layers of events) for each row of the grid.
//	// `levelLimit` can be false (don't limit), a number, or true (should be computed).
//	limitRows: function(levelLimit) {
//		var rowStructs = this.rowStructs || [];
//		var row; // row #
//		var rowLevelLimit;

//		for (row = 0; row < rowStructs.length; row++) {
//			this.unlimitRow(row);

//			if (!levelLimit) {
//				rowLevelLimit = false;
//			}
//			else if (typeof levelLimit === 'number') {
//				rowLevelLimit = levelLimit;
//			}
//			else {
//				rowLevelLimit = this.computeRowLevelLimit(row);
//			}

//			if (rowLevelLimit !== false) {
//				this.limitRow(row, rowLevelLimit);
//			}
//		}
//	},


//	// Computes the number of levels a row will accomodate without going outside its bounds.
//	// Assumes the row is "rigid" (maintains a constant height regardless of what is inside).
//	// `row` is the row number.
//	computeRowLevelLimit: function(row) {
//		var rowEl = this.rowEls.eq(row); // the containing "fake" row div
//		var rowHeight = rowEl.height(); // TODO: cache somehow?
//		var trEls = this.rowStructs[row].tbodyEl.children();
//		var i, trEl;

//		// Reveal one level <tr> at a time and stop when we find one out of bounds
//		for (i = 0; i < trEls.length; i++) {
//			trEl = trEls.eq(i).removeClass('fc-limited'); // get and reveal
//			if (trEl.position().top + trEl.outerHeight() > rowHeight) {
//				return i;
//			}
//		}

//		return false; // should not limit at all
//	},


//	// Limits the given grid row to the maximum number of levels and injects "more" links if necessary.
//	// `row` is the row number.
//	// `levelLimit` is a number for the maximum (inclusive) number of levels allowed.
//	limitRow: function(row, levelLimit) {
//		var _this = this;
//		var view = this.view;
//		var rowStruct = this.rowStructs[row];
//		var moreNodes = []; // array of "more" <a> links and <td> DOM nodes
//		var col = 0; // col #
//		var cell;
//		var levelSegs; // array of segment objects in the last allowable level, ordered left-to-right
//		var cellMatrix; // a matrix (by level, then column) of all <td> jQuery elements in the row
//		var limitedNodes; // array of temporarily hidden level <tr> and segment <td> DOM nodes
//		var i, seg;
//		var segsBelow; // array of segment objects below `seg` in the current `col`
//		var totalSegsBelow; // total number of segments below `seg` in any of the columns `seg` occupies
//		var colSegsBelow; // array of segment arrays, below seg, one for each column (offset from segs's first column)
//		var td, rowspan;
//		var segMoreNodes; // array of "more" <td> cells that will stand-in for the current seg's cell
//		var j;
//		var moreTd, moreWrap, moreLink;

//		// Iterates through empty level cells and places "more" links inside if need be
//		function emptyCellsUntil(endCol) { // goes from current `col` to `endCol`
//			while (col < endCol) {
//				cell = { row: row, col: col };
//				segsBelow = _this.getCellSegs(cell, levelLimit);
//				if (segsBelow.length) {
//					td = cellMatrix[levelLimit - 1][col];
//					moreLink = _this.renderMoreLink(cell, segsBelow);
//					moreWrap = $('<div/>').append(moreLink);
//					td.append(moreWrap);
//					moreNodes.push(moreWrap[0]);
//				}
//				col++;
//			}
//		}

//		if (levelLimit && levelLimit < rowStruct.segLevels.length) { // is it actually over the limit?
//			levelSegs = rowStruct.segLevels[levelLimit - 1];
//			cellMatrix = rowStruct.cellMatrix;

//			limitedNodes = rowStruct.tbodyEl.children().slice(levelLimit) // get level <tr> elements past the limit
//				.addClass('fc-limited').get(); // hide elements and get a simple DOM-nodes array

//			// iterate though segments in the last allowable level
//			for (i = 0; i < levelSegs.length; i++) {
//				seg = levelSegs[i];
//				emptyCellsUntil(seg.leftCol); // process empty cells before the segment

//				// determine *all* segments below `seg` that occupy the same columns
//				colSegsBelow = [];
//				totalSegsBelow = 0;
//				while (col <= seg.rightCol) {
//					cell = { row: row, col: col };
//					segsBelow = this.getCellSegs(cell, levelLimit);
//					colSegsBelow.push(segsBelow);
//					totalSegsBelow += segsBelow.length;
//					col++;
//				}

//				if (totalSegsBelow) { // do we need to replace this segment with one or many "more" links?
//					td = cellMatrix[levelLimit - 1][seg.leftCol]; // the segment's parent cell
//					rowspan = td.attr('rowspan') || 1;
//					segMoreNodes = [];

//					// make a replacement <td> for each column the segment occupies. will be one for each colspan
//					for (j = 0; j < colSegsBelow.length; j++) {
//						moreTd = $('<td class="fc-more-cell"/>').attr('rowspan', rowspan);
//						segsBelow = colSegsBelow[j];
//						cell = { row: row, col: seg.leftCol + j };
//						moreLink = this.renderMoreLink(cell, [ seg ].concat(segsBelow)); // count seg as hidden too
//						moreWrap = $('<div/>').append(moreLink);
//						moreTd.append(moreWrap);
//						segMoreNodes.push(moreTd[0]);
//						moreNodes.push(moreTd[0]);
//					}

//					td.addClass('fc-limited').after($(segMoreNodes)); // hide original <td> and inject replacements
//					limitedNodes.push(td[0]);
//				}
//			}

//			emptyCellsUntil(view.colCnt); // finish off the level
//			rowStruct.moreEls = $(moreNodes); // for easy undoing later
//			rowStruct.limitedEls = $(limitedNodes); // for easy undoing later
//		}
//	},


//	// Reveals all levels and removes all "more"-related elements for a grid's row.
//	// `row` is a row number.
//	unlimitRow: function(row) {
//		var rowStruct = this.rowStructs[row];

//		if (rowStruct.moreEls) {
//			rowStruct.moreEls.remove();
//			rowStruct.moreEls = null;
//		}

//		if (rowStruct.limitedEls) {
//			rowStruct.limitedEls.removeClass('fc-limited');
//			rowStruct.limitedEls = null;
//		}
//	},


//	// Renders an <a> element that represents hidden event element for a cell.
//	// Responsible for attaching click handler as well.
//	renderMoreLink: function(cell, hiddenSegs) {
//		var _this = this;
//		var view = this.view;

//		return $('<a class="fc-more"/>')
//			.text(
//				this.getMoreLinkText(hiddenSegs.length)
//			)
//			.on('click', function(ev) {
//				var clickOption = view.opt('eventLimitClick');
//				var date = view.cellToDate(cell);
//				var moreEl = $(this);
//				var dayEl = _this.getCellDayEl(cell);
//				var allSegs = _this.getCellSegs(cell);

//				// rescope the segments to be within the cell's date
//				var reslicedAllSegs = _this.resliceDaySegs(allSegs, date);
//				var reslicedHiddenSegs = _this.resliceDaySegs(hiddenSegs, date);

//				if (typeof clickOption === 'function') {
//					// the returned value can be an atomic option
//					clickOption = view.trigger('eventLimitClick', null, {
//						date: date,
//						dayEl: dayEl,
//						moreEl: moreEl,
//						segs: reslicedAllSegs,
//						hiddenSegs: reslicedHiddenSegs
//					}, ev);
//				}

//				if (clickOption === 'popover') {
//					_this.showSegPopover(date, cell, moreEl, reslicedAllSegs);
//				}
//				else if (typeof clickOption === 'string') { // a view name
//					view.calendar.zoomTo(date, clickOption);
//				}
//			});
//	},


//	// Reveals the popover that displays all events within a cell
//	showSegPopover: function(date, cell, moreLink, segs) {
//		var _this = this;
//		var view = this.view;
//		var moreWrap = moreLink.parent(); // the <div> wrapper around the <a>
//		var topEl; // the element we want to match the top coordinate of
//		var options;

//		if (view.rowCnt == 1) {
//			topEl = this.view.el; // will cause the popover to cover any sort of header
//		}
//		else {
//			topEl = this.rowEls.eq(cell.row); // will align with top of row
//		}

//		options = {
//			className: 'fc-more-popover',
//			content: this.renderSegPopoverContent(date, segs),
//			parentEl: this.el,
//			top: topEl.offset().top,
//			autoHide: true, // when the user clicks elsewhere, hide the popover
//			viewportConstrain: view.opt('popoverViewportConstrain'),
//			hide: function() {
//				// destroy everything when the popover is hidden
//				_this.segPopover.destroy();
//				_this.segPopover = null;
//				_this.popoverSegs = null;
//			}
//		};

//		// Determine horizontal coordinate.
//		// We use the moreWrap instead of the <td> to avoid border confusion.
//		if (view.opt('isRTL')) {
//			options.right = moreWrap.offset().left + moreWrap.outerWidth() + 1; // +1 to be over cell border
//		}
//		else {
//			options.left = moreWrap.offset().left - 1; // -1 to be over cell border
//		}

//		this.segPopover = new Popover(options);
//		this.segPopover.show();
//	},


//	// Builds the inner DOM contents of the segment popover
//	renderSegPopoverContent: function(date, segs) {
//		var view = this.view;
//		var isTheme = view.opt('theme');
//		var title = date.format(view.opt('dayPopoverFormat'));
//		var content = $(
//			'<div class="fc-header ' + view.widgetHeaderClass + '">' +
//				'<span class="fc-close ' +
//					(isTheme ? 'ui-icon ui-icon-closethick' : 'fc-icon fc-icon-x') +
//				'"></span>' +
//				'<span class="fc-title">' +
//					htmlEscape(title) +
//				'</span>' +
//				'<div class="fc-clear"/>' +
//			'</div>' +
//			'<div class="fc-body ' + view.widgetContentClass + '">' +
//				'<div class="fc-event-container"></div>' +
//			'</div>'
//		);
//		var segContainer = content.find('.fc-event-container');
//		var i;

//		// render each seg's `el` and only return the visible segs
//		segs = this.renderSegs(segs, true); // disableResizing=true
//		this.popoverSegs = segs;

//		for (i = 0; i < segs.length; i++) {

//			// because segments in the popover are not part of a grid coordinate system, provide a hint to any
//			// grids that want to do drag-n-drop about which cell it came from
//			segs[i].cellDate = date;

//			segContainer.append(segs[i].el);
//		}

//		return content;
//	},


//	// Given the events within an array of segment objects, reslice them to be in a single day
//	resliceDaySegs: function(segs, dayDate) {
//		var events = $.map(segs, function(seg) {
//			return seg.event;
//		});
//		var dayStart = dayDate.clone().stripTime();
//		var dayEnd = dayStart.clone().add(1, 'days');

//		return this.eventsToSegs(events, dayStart, dayEnd);
//	},


//	// Generates the text that should be inside a "more" link, given the number of events it represents
//	getMoreLinkText: function(num) {
//		var view = this.view;
//		var opt = view.opt('eventLimitText');

//		if (typeof opt === 'function') {
//			return opt(num);
//		}
//		else {
//			return '+' + num + ' ' + opt;
//		}
//	},


//	// Returns segments within a given cell.
//	// If `startLevel` is specified, returns only events including and below that level. Otherwise returns all segs.
//	getCellSegs: function(cell, startLevel) {
//		var segMatrix = this.rowStructs[cell.row].segMatrix;
//		var level = startLevel || 0;
//		var segs = [];
//		var seg;

//		while (level < segMatrix.length) {
//			seg = segMatrix[level][cell.col];
//			if (seg) {
//				segs.push(seg);
//			}
//			level++;
//		}

//		return segs;
//	}

//});

//;;

///* A component that renders one or more columns of vertical time slots
//----------------------------------------------------------------------------------------------------------------------*/

//function TimeGrid(view) {
//	Grid.call(this, view); // call the super-constructor
//}


//TimeGrid.prototype = createObject(Grid.prototype); // define the super-class
//$.extend(TimeGrid.prototype, {

//	slotDuration: null, // duration of a "slot", a distinct time segment on given day, visualized by lines
//	snapDuration: null, // granularity of time for dragging and selecting

//	minTime: null, // Duration object that denotes the first visible time of any given day
//	maxTime: null, // Duration object that denotes the exclusive visible end time of any given day

//	dayEls: null, // cells elements in the day-row background
//	slatEls: null, // elements running horizontally across all columns

//	slatTops: null, // an array of top positions, relative to the container. last item holds bottom of last slot

//	highlightEl: null, // cell skeleton element for rendering the highlight
//	helperEl: null, // cell skeleton element for rendering the mock event "helper"


//	// Renders the time grid into `this.el`, which should already be assigned.
//	// Relies on the view's colCnt. In the future, this component should probably be self-sufficient.
//	render: function() {
//		this.processOptions();

//		this.el.html(this.renderHtml());

//		this.dayEls = this.el.find('.fc-day');
//		this.slatEls = this.el.find('.fc-slats tr');

//		this.computeSlatTops();

//		Grid.prototype.render.call(this); // call the super-method
//	},


//	// Renders the basic HTML skeleton for the grid
//	renderHtml: function() {
//		return '' +
//			'<div class="fc-bg">' +
//				'<table>' +
//					this.rowHtml('slotBg') + // leverages RowRenderer, which will call slotBgCellHtml
//				'</table>' +
//			'</div>' +
//			'<div class="fc-slats">' +
//				'<table>' +
//					this.slatRowHtml() +
//				'</table>' +
//			'</div>';
//	},


//	// Renders the HTML for a vertical background cell behind the slots.
//	// This method is distinct from 'bg' because we wanted a new `rowType` so the View could customize the rendering.
//	slotBgCellHtml: function(row, col, date) {
//		return this.bgCellHtml(row, col, date);
//	},


//	// Generates the HTML for the horizontal "slats" that run width-wise. Has a time axis on a side. Depends on RTL.
//	slatRowHtml: function() {
//		var view = this.view;
//		var calendar = view.calendar;
//		var isRTL = view.opt('isRTL');
//		var html = '';
//		var slotNormal = this.slotDuration.asMinutes() % 15 === 0;
//		var slotTime = moment.duration(+this.minTime); // wish there was .clone() for durations
//		var slotDate; // will be on the view's first day, but we only care about its time
//		var minutes;
//		var axisHtml;

//		// Calculate the time for each slot
//		while (slotTime < this.maxTime) {
//			slotDate = view.start.clone().time(slotTime); // will be in UTC but that's good. to avoid DST issues
//			minutes = slotDate.minutes();

//			axisHtml =
//				'<td class="fc-axis fc-time ' + view.widgetContentClass + '" ' + view.axisStyleAttr() + '>' +
//					((!slotNormal || !minutes) ? // if irregular slot duration, or on the hour, then display the time
//						'<span>' + // for matchCellWidths
//							htmlEscape(calendar.formatDate(slotDate, view.opt('axisFormat'))) +
//						'</span>' :
//						''
//						) +
//				'</td>';

//			html +=
//				'<tr ' + (!minutes ? '' : 'class="fc-minor"') + '>' +
//					(!isRTL ? axisHtml : '') +
//					'<td class="' + view.widgetContentClass + '"/>' +
//					(isRTL ? axisHtml : '') +
//				"</tr>";

//			slotTime.add(this.slotDuration);
//		}

//		return html;
//	},


//	// Parses various options into properties of this object
//	processOptions: function() {
//		var view = this.view;
//		var slotDuration = view.opt('slotDuration');
//		var snapDuration = view.opt('snapDuration');

//		slotDuration = moment.duration(slotDuration);
//		snapDuration = snapDuration ? moment.duration(snapDuration) : slotDuration;

//		this.slotDuration = slotDuration;
//		this.snapDuration = snapDuration;
//		this.cellDuration = snapDuration; // important to assign this for Grid.events.js

//		this.minTime = moment.duration(view.opt('minTime'));
//		this.maxTime = moment.duration(view.opt('maxTime'));
//	},


//	// Slices up a date range into a segment for each column
//	rangeToSegs: function(rangeStart, rangeEnd) {
//		var view = this.view;
//		var segs = [];
//		var seg;
//		var col;
//		var cellDate;
//		var colStart, colEnd;

//		// normalize
//		rangeStart = rangeStart.clone().stripZone();
//		rangeEnd = rangeEnd.clone().stripZone();

//		for (col = 0; col < view.colCnt; col++) {
//			cellDate = view.cellToDate(0, col); // use the View's cell system for this
//			colStart = cellDate.clone().time(this.minTime);
//			colEnd = cellDate.clone().time(this.maxTime);
//			seg = intersectionToSeg(rangeStart, rangeEnd, colStart, colEnd);
//			if (seg) {
//				seg.col = col;
//				segs.push(seg);
//			}
//		}

//		return segs;
//	},


//	/* Coordinates
//	------------------------------------------------------------------------------------------------------------------*/


//	// Called when there is a window resize/zoom and we need to recalculate coordinates for the grid
//	resize: function() {
//		this.computeSlatTops();
//		this.updateSegVerticals();
//	},


//	// Populates the given empty `rows` and `cols` arrays with offset positions of the "snap" cells.
//	// "Snap" cells are different the slots because they might have finer granularity.
//	buildCoords: function(rows, cols) {
//		var colCnt = this.view.colCnt;
//		var originTop = this.el.offset().top;
//		var snapTime = moment.duration(+this.minTime);
//		var p = null;
//		var e, n;

//		this.dayEls.slice(0, colCnt).each(function(i, _e) {
//			e = $(_e);
//			n = e.offset().left;
//			if (p) {
//				p[1] = n;
//			}
//			p = [ n ];
//			cols[i] = p;
//		});
//		p[1] = n + e.outerWidth();

//		p = null;
//		while (snapTime < this.maxTime) {
//			n = originTop + this.computeTimeTop(snapTime);
//			if (p) {
//				p[1] = n;
//			}
//			p = [ n ];
//			rows.push(p);
//			snapTime.add(this.snapDuration);
//		}
//		p[1] = originTop + this.computeTimeTop(snapTime); // the position of the exclusive end
//	},


//	// Gets the datetime for the given slot cell
//	getCellDate: function(cell) {
//		var view = this.view;
//		var calendar = view.calendar;

//		return calendar.rezoneDate( // since we are adding a time, it needs to be in the calendar's timezone
//			view.cellToDate(0, cell.col) // View's coord system only accounts for start-of-day for column
//				.time(this.minTime + this.snapDuration * cell.row)
//		);
//	},


//	// Gets the element that represents the whole-day the cell resides on
//	getCellDayEl: function(cell) {
//		return this.dayEls.eq(cell.col);
//	},


//	// Computes the top coordinate, relative to the bounds of the grid, of the given date.
//	// A `startOfDayDate` must be given for avoiding ambiguity over how to treat midnight.
//	computeDateTop: function(date, startOfDayDate) {
//		return this.computeTimeTop(
//			moment.duration(
//				date.clone().stripZone() - startOfDayDate.clone().stripTime()
//			)
//		);
//	},


//	// Computes the top coordinate, relative to the bounds of the grid, of the given time (a Duration).
//	computeTimeTop: function(time) {
//		var slatCoverage = (time - this.minTime) / this.slotDuration; // floating-point value of # of slots covered
//		var slatIndex;
//		var slatRemainder;
//		var slatTop;
//		var slatBottom;

//		// constrain. because minTime/maxTime might be customized
//		slatCoverage = Math.max(0, slatCoverage);
//		slatCoverage = Math.min(this.slatEls.length, slatCoverage);

//		slatIndex = Math.floor(slatCoverage); // an integer index of the furthest whole slot
//		slatRemainder = slatCoverage - slatIndex;
//		slatTop = this.slatTops[slatIndex]; // the top position of the furthest whole slot

//		if (slatRemainder) { // time spans part-way into the slot
//			slatBottom = this.slatTops[slatIndex + 1];
//			return slatTop + (slatBottom - slatTop) * slatRemainder; // part-way between slots
//		}
//		else {
//			return slatTop;
//		}
//	},


//	// Queries each `slatEl` for its position relative to the grid's container and stores it in `slatTops`.
//	// Includes the the bottom of the last slat as the last item in the array.
//	computeSlatTops: function() {
//		var tops = [];
//		var top;

//		this.slatEls.each(function(i, node) {
//			top = $(node).position().top;
//			tops.push(top);
//		});

//		tops.push(top + this.slatEls.last().outerHeight()); // bottom of the last slat

//		this.slatTops = tops;
//	},


//	/* Event Drag Visualization
//	------------------------------------------------------------------------------------------------------------------*/


//	// Renders a visual indication of an event being dragged over the specified date(s).
//	// `end` and `seg` can be null. See View's documentation on renderDrag for more info.
//	renderDrag: function(start, end, seg) {
//		var opacity;

//		if (seg) { // if there is event information for this drag, render a helper event
//			this.renderRangeHelper(start, end, seg);

//			opacity = this.view.opt('dragOpacity');
//			if (opacity !== undefined) {
//				this.helperEl.css('opacity', opacity);
//			}

//			return true; // signal that a helper has been rendered
//		}
//		else {
//			// otherwise, just render a highlight
//			this.renderHighlight(
//				start,
//				end || this.view.calendar.getDefaultEventEnd(false, start)
//			);
//		}
//	},


//	// Unrenders any visual indication of an event being dragged
//	destroyDrag: function() {
//		this.destroyHelper();
//		this.destroyHighlight();
//	},


//	/* Event Resize Visualization
//	------------------------------------------------------------------------------------------------------------------*/


//	// Renders a visual indication of an event being resized
//	renderResize: function(start, end, seg) {
//		this.renderRangeHelper(start, end, seg);
//	},


//	// Unrenders any visual indication of an event being resized
//	destroyResize: function() {
//		this.destroyHelper();
//	},


//	/* Event Helper
//	------------------------------------------------------------------------------------------------------------------*/


//	// Renders a mock "helper" event. `sourceSeg` is the original segment object and might be null (an external drag)
//	renderHelper: function(event, sourceSeg) {
//		var res = this.renderEventTable([ event ]);
//		var tableEl = res.tableEl;
//		var segs = res.segs;
//		var i, seg;
//		var sourceEl;

//		// Try to make the segment that is in the same row as sourceSeg look the same
//		for (i = 0; i < segs.length; i++) {
//			seg = segs[i];
//			if (sourceSeg && sourceSeg.col === seg.col) {
//				sourceEl = sourceSeg.el;
//				seg.el.css({
//					left: sourceEl.css('left'),
//					right: sourceEl.css('right'),
//					'margin-left': sourceEl.css('margin-left'),
//					'margin-right': sourceEl.css('margin-right')
//				});
//			}
//		}

//		this.helperEl = $('<div class="fc-helper-skeleton"/>')
//			.append(tableEl)
//				.appendTo(this.el);
//	},


//	// Unrenders any mock helper event
//	destroyHelper: function() {
//		if (this.helperEl) {
//			this.helperEl.remove();
//			this.helperEl = null;
//		}
//	},


//	/* Selection
//	------------------------------------------------------------------------------------------------------------------*/


//	// Renders a visual indication of a selection. Overrides the default, which was to simply render a highlight.
//	renderSelection: function(start, end) {
//		if (this.view.opt('selectHelper')) { // this setting signals that a mock helper event should be rendered
//			this.renderRangeHelper(start, end);
//		}
//		else {
//			this.renderHighlight(start, end);
//		}
//	},


//	// Unrenders any visual indication of a selection
//	destroySelection: function() {
//		this.destroyHelper();
//		this.destroyHighlight();
//	},


//	/* Highlight
//	------------------------------------------------------------------------------------------------------------------*/


//	// Renders an emphasis on the given date range. `start` is inclusive. `end` is exclusive.
//	renderHighlight: function(start, end) {
//		this.highlightEl = $(
//			this.highlightSkeletonHtml(start, end)
//		).appendTo(this.el);
//	},


//	// Unrenders the emphasis on a date range
//	destroyHighlight: function() {
//		if (this.highlightEl) {
//			this.highlightEl.remove();
//			this.highlightEl = null;
//		}
//	},


//	// Generates HTML for a table element with containers in each column, responsible for absolutely positioning the
//	// highlight elements to cover the highlighted slots.
//	highlightSkeletonHtml: function(start, end) {
//		var view = this.view;
//		var segs = this.rangeToSegs(start, end);
//		var cellHtml = '';
//		var col = 0;
//		var i, seg;
//		var dayDate;
//		var top, bottom;

//		for (i = 0; i < segs.length; i++) { // loop through the segments. one per column
//			seg = segs[i];

//			// need empty cells beforehand?
//			if (col < seg.col) {
//				cellHtml += '<td colspan="' + (seg.col - col) + '"/>';
//				col = seg.col;
//			}

//			// compute vertical position
//			dayDate = view.cellToDate(0, col);
//			top = this.computeDateTop(seg.start, dayDate);
//			bottom = this.computeDateTop(seg.end, dayDate); // the y position of the bottom edge

//			// generate the cell HTML. bottom becomes negative because it needs to be a CSS value relative to the
//			// bottom edge of the zero-height container.
//			cellHtml +=
//				'<td>' +
//					'<div class="fc-highlight-container">' +
//						'<div class="fc-highlight" style="top:' + top + 'px;bottom:-' + bottom + 'px"/>' +
//					'</div>' +
//				'</td>';

//			col++;
//		}

//		// need empty cells after the last segment?
//		if (col < view.colCnt) {
//			cellHtml += '<td colspan="' + (view.colCnt - col) + '"/>';
//		}

//		cellHtml = this.bookendCells(cellHtml, 'highlight');

//		return '' +
//			'<div class="fc-highlight-skeleton">' +
//				'<table>' +
//					'<tr>' +
//						cellHtml +
//					'</tr>' +
//				'</table>' +
//			'</div>';
//	}

//});

//;;

///* Event-rendering methods for the TimeGrid class
//----------------------------------------------------------------------------------------------------------------------*/

//$.extend(TimeGrid.prototype, {

//	segs: null, // segment objects rendered in the component. null of events haven't been rendered yet
//	eventSkeletonEl: null, // has cells with event-containers, which contain absolutely positioned event elements


//	// Renders the events onto the grid and returns an array of segments that have been rendered
//	renderEvents: function(events) {
//		var res = this.renderEventTable(events);

//		this.eventSkeletonEl = $('<div class="fc-content-skeleton"/>').append(res.tableEl);
//		this.el.append(this.eventSkeletonEl);

//		this.segs = res.segs;
//	},


//	// Retrieves rendered segment objects
//	getSegs: function() {
//		return this.segs || [];
//	},


//	// Removes all event segment elements from the view
//	destroyEvents: function() {
//		Grid.prototype.destroyEvents.call(this); // call the super-method

//		if (this.eventSkeletonEl) {
//			this.eventSkeletonEl.remove();
//			this.eventSkeletonEl = null;
//		}

//		this.segs = null;
//	},


//	// Renders and returns the <table> portion of the event-skeleton.
//	// Returns an object with properties 'tbodyEl' and 'segs'.
//	renderEventTable: function(events) {
//		var tableEl = $('<table><tr/></table>');
//		var trEl = tableEl.find('tr');
//		var segs = this.eventsToSegs(events);
//		var segCols;
//		var i, seg;
//		var col, colSegs;
//		var containerEl;

//		segs = this.renderSegs(segs); // returns only the visible segs
//		segCols = this.groupSegCols(segs); // group into sub-arrays, and assigns 'col' to each seg

//		this.computeSegVerticals(segs); // compute and assign top/bottom

//		for (col = 0; col < segCols.length; col++) { // iterate each column grouping
//			colSegs = segCols[col];
//			placeSlotSegs(colSegs); // compute horizontal coordinates, z-index's, and reorder the array

//			containerEl = $('<div class="fc-event-container"/>');

//			// assign positioning CSS and insert into container
//			for (i = 0; i < colSegs.length; i++) {
//				seg = colSegs[i];
//				seg.el.css(this.generateSegPositionCss(seg));

//				// if the height is short, add a className for alternate styling
//				if (seg.bottom - seg.top < 30) {
//					seg.el.addClass('fc-short');
//				}

//				containerEl.append(seg.el);
//			}

//			trEl.append($('<td/>').append(containerEl));
//		}

//		this.bookendCells(trEl, 'eventSkeleton');

//		return  {
//			tableEl: tableEl,
//			segs: segs
//		};
//	},


//	// Refreshes the CSS top/bottom coordinates for each segment element. Probably after a window resize/zoom.
//	updateSegVerticals: function() {
//		var segs = this.segs;
//		var i;

//		if (segs) {
//			this.computeSegVerticals(segs);

//			for (i = 0; i < segs.length; i++) {
//				segs[i].el.css(
//					this.generateSegVerticalCss(segs[i])
//				);
//			}
//		}
//	},


//	// For each segment in an array, computes and assigns its top and bottom properties
//	computeSegVerticals: function(segs) {
//		var i, seg;

//		for (i = 0; i < segs.length; i++) {
//			seg = segs[i];
//			seg.top = this.computeDateTop(seg.start, seg.start);
//			seg.bottom = this.computeDateTop(seg.end, seg.start);
//		}
//	},


//	// Renders the HTML for a single event segment's default rendering
//	renderSegHtml: function(seg, disableResizing) {
//		var view = this.view;
//		var event = seg.event;
//		var isDraggable = view.isEventDraggable(event);
//		var isResizable = !disableResizing && seg.isEnd && view.isEventResizable(event);
//		var classes = this.getSegClasses(seg, isDraggable, isResizable);
//		var skinCss = this.getEventSkinCss(event);
//		var timeText;
//		var fullTimeText; // more verbose time text. for the print stylesheet
//		var startTimeText; // just the start time text

//		classes.unshift('fc-time-grid-event');

//		if (view.isMultiDayEvent(event)) { // if the event appears to span more than one day...
//			// Don't display time text on segments that run entirely through a day.
//			// That would appear as midnight-midnight and would look dumb.
//			// Otherwise, display the time text for the *segment's* times (like 6pm-midnight or midnight-10am)
//			if (seg.isStart || seg.isEnd) {
//				timeText = view.getEventTimeText(seg.start, seg.end);
//				fullTimeText = view.getEventTimeText(seg.start, seg.end, 'LT');
//				startTimeText = view.getEventTimeText(seg.start, null);
//			}
//		} else {
//			// Display the normal time text for the *event's* times
//			timeText = view.getEventTimeText(event);
//			fullTimeText = view.getEventTimeText(event, 'LT');
//			startTimeText = view.getEventTimeText(event.start, null);
//		}

//		return '<a class="' + classes.join(' ') + '"' +
//			(event.url ?
//				' href="' + htmlEscape(event.url) + '"' :
//				''
//				) +
//			(skinCss ?
//				' style="' + skinCss + '"' :
//				''
//				) +
//			'>' +
//				'<div class="fc-content">' +
//					(timeText ?
//						'<div class="fc-time"' +
//						' data-start="' + htmlEscape(startTimeText) + '"' +
//						' data-full="' + htmlEscape(fullTimeText) + '"' +
//						'>' +
//							'<span>' + htmlEscape(timeText) + '</span>' +
//						'</div>' :
//						''
//						) +
//					(event.title ?
//						'<div class="fc-title">' +
//							htmlEscape(event.title) +
//						'</div>' :
//						''
//						) +
//				'</div>' +
//				'<div class="fc-bg"/>' +
//				(isResizable ?
//					'<div class="fc-resizer"/>' :
//					''
//					) +
//			'</a>';
//	},


//	// Generates an object with CSS properties/values that should be applied to an event segment element.
//	// Contains important positioning-related properties that should be applied to any event element, customized or not.
//	generateSegPositionCss: function(seg) {
//		var view = this.view;
//		var isRTL = view.opt('isRTL');
//		var shouldOverlap = view.opt('slotEventOverlap');
//		var backwardCoord = seg.backwardCoord; // the left side if LTR. the right side if RTL. floating-point
//		var forwardCoord = seg.forwardCoord; // the right side if LTR. the left side if RTL. floating-point
//		var props = this.generateSegVerticalCss(seg); // get top/bottom first
//		var left; // amount of space from left edge, a fraction of the total width
//		var right; // amount of space from right edge, a fraction of the total width

//		if (shouldOverlap) {
//			// double the width, but don't go beyond the maximum forward coordinate (1.0)
//			forwardCoord = Math.min(1, backwardCoord + (forwardCoord - backwardCoord) * 2);
//		}

//		if (isRTL) {
//			left = 1 - forwardCoord;
//			right = backwardCoord;
//		}
//		else {
//			left = backwardCoord;
//			right = 1 - forwardCoord;
//		}

//		props.zIndex = seg.level + 1; // convert from 0-base to 1-based
//		props.left = left * 100 + '%';
//		props.right = right * 100 + '%';

//		if (shouldOverlap && seg.forwardPressure) {
//			// add padding to the edge so that forward stacked events don't cover the resizer's icon
//			props[isRTL ? 'marginLeft' : 'marginRight'] = 10 * 2; // 10 is a guesstimate of the icon's width 
//		}

//		return props;
//	},


//	// Generates an object with CSS properties for the top/bottom coordinates of a segment element
//	generateSegVerticalCss: function(seg) {
//		return {
//			top: seg.top,
//			bottom: -seg.bottom // flipped because needs to be space beyond bottom edge of event container
//		};
//	},


//	// Given a flat array of segments, return an array of sub-arrays, grouped by each segment's col
//	groupSegCols: function(segs) {
//		var view = this.view;
//		var segCols = [];
//		var i;

//		for (i = 0; i < view.colCnt; i++) {
//			segCols.push([]);
//		}

//		for (i = 0; i < segs.length; i++) {
//			segCols[segs[i].col].push(segs[i]);
//		}

//		return segCols;
//	}

//});


//// Given an array of segments that are all in the same column, sets the backwardCoord and forwardCoord on each.
//// Also reorders the given array by date!
//function placeSlotSegs(segs) {
//	var levels;
//	var level0;
//	var i;

//	segs.sort(compareSegs); // order by date
//	levels = buildSlotSegLevels(segs);
//	computeForwardSlotSegs(levels);

//	if ((level0 = levels[0])) {

//		for (i = 0; i < level0.length; i++) {
//			computeSlotSegPressures(level0[i]);
//		}

//		for (i = 0; i < level0.length; i++) {
//			computeSlotSegCoords(level0[i], 0, 0);
//		}
//	}
//}


//// Builds an array of segments "levels". The first level will be the leftmost tier of segments if the calendar is
//// left-to-right, or the rightmost if the calendar is right-to-left. Assumes the segments are already ordered by date.
//function buildSlotSegLevels(segs) {
//	var levels = [];
//	var i, seg;
//	var j;

//	for (i=0; i<segs.length; i++) {
//		seg = segs[i];

//		// go through all the levels and stop on the first level where there are no collisions
//		for (j=0; j<levels.length; j++) {
//			if (!computeSlotSegCollisions(seg, levels[j]).length) {
//				break;
//			}
//		}

//		seg.level = j;

//		(levels[j] || (levels[j] = [])).push(seg);
//	}

//	return levels;
//}


//// For every segment, figure out the other segments that are in subsequent
//// levels that also occupy the same vertical space. Accumulate in seg.forwardSegs
//function computeForwardSlotSegs(levels) {
//	var i, level;
//	var j, seg;
//	var k;

//	for (i=0; i<levels.length; i++) {
//		level = levels[i];

//		for (j=0; j<level.length; j++) {
//			seg = level[j];

//			seg.forwardSegs = [];
//			for (k=i+1; k<levels.length; k++) {
//				computeSlotSegCollisions(seg, levels[k], seg.forwardSegs);
//			}
//		}
//	}
//}


//// Figure out which path forward (via seg.forwardSegs) results in the longest path until
//// the furthest edge is reached. The number of segments in this path will be seg.forwardPressure
//function computeSlotSegPressures(seg) {
//	var forwardSegs = seg.forwardSegs;
//	var forwardPressure = 0;
//	var i, forwardSeg;

//	if (seg.forwardPressure === undefined) { // not already computed

//		for (i=0; i<forwardSegs.length; i++) {
//			forwardSeg = forwardSegs[i];

//			// figure out the child's maximum forward path
//			computeSlotSegPressures(forwardSeg);

//			// either use the existing maximum, or use the child's forward pressure
//			// plus one (for the forwardSeg itself)
//			forwardPressure = Math.max(
//				forwardPressure,
//				1 + forwardSeg.forwardPressure
//			);
//		}

//		seg.forwardPressure = forwardPressure;
//	}
//}


//// Calculate seg.forwardCoord and seg.backwardCoord for the segment, where both values range
//// from 0 to 1. If the calendar is left-to-right, the seg.backwardCoord maps to "left" and
//// seg.forwardCoord maps to "right" (via percentage). Vice-versa if the calendar is right-to-left.
////
//// The segment might be part of a "series", which means consecutive segments with the same pressure
//// who's width is unknown until an edge has been hit. `seriesBackwardPressure` is the number of
//// segments behind this one in the current series, and `seriesBackwardCoord` is the starting
//// coordinate of the first segment in the series.
//function computeSlotSegCoords(seg, seriesBackwardPressure, seriesBackwardCoord) {
//	var forwardSegs = seg.forwardSegs;
//	var i;

//	if (seg.forwardCoord === undefined) { // not already computed

//		if (!forwardSegs.length) {

//			// if there are no forward segments, this segment should butt up against the edge
//			seg.forwardCoord = 1;
//		}
//		else {

//			// sort highest pressure first
//			forwardSegs.sort(compareForwardSlotSegs);

//			// this segment's forwardCoord will be calculated from the backwardCoord of the
//			// highest-pressure forward segment.
//			computeSlotSegCoords(forwardSegs[0], seriesBackwardPressure + 1, seriesBackwardCoord);
//			seg.forwardCoord = forwardSegs[0].backwardCoord;
//		}

//		// calculate the backwardCoord from the forwardCoord. consider the series
//		seg.backwardCoord = seg.forwardCoord -
//			(seg.forwardCoord - seriesBackwardCoord) / // available width for series
//			(seriesBackwardPressure + 1); // # of segments in the series

//		// use this segment's coordinates to computed the coordinates of the less-pressurized
//		// forward segments
//		for (i=0; i<forwardSegs.length; i++) {
//			computeSlotSegCoords(forwardSegs[i], 0, seg.forwardCoord);
//		}
//	}
//}


//// Find all the segments in `otherSegs` that vertically collide with `seg`.
//// Append into an optionally-supplied `results` array and return.
//function computeSlotSegCollisions(seg, otherSegs, results) {
//	results = results || [];

//	for (var i=0; i<otherSegs.length; i++) {
//		if (isSlotSegCollision(seg, otherSegs[i])) {
//			results.push(otherSegs[i]);
//		}
//	}

//	return results;
//}


//// Do these segments occupy the same vertical space?
//function isSlotSegCollision(seg1, seg2) {
//	return seg1.bottom > seg2.top && seg1.top < seg2.bottom;
//}


//// A cmp function for determining which forward segment to rely on more when computing coordinates.
//function compareForwardSlotSegs(seg1, seg2) {
//	// put higher-pressure first
//	return seg2.forwardPressure - seg1.forwardPressure ||
//		// put segments that are closer to initial edge first (and favor ones with no coords yet)
//		(seg1.backwardCoord || 0) - (seg2.backwardCoord || 0) ||
//		// do normal sorting...
//		compareSegs(seg1, seg2);
//}

//;;

///* An abstract class from which other views inherit from
//----------------------------------------------------------------------------------------------------------------------*/
//// Newer methods should be written as prototype methods, not in the monster `View` function at the bottom.

//View.prototype = {

//	calendar: null, // owner Calendar object
//	coordMap: null, // a CoordMap object for converting pixel regions to dates
//	el: null, // the view's containing element. set by Calendar

//	// important Moments
//	start: null, // the date of the very first cell
//	end: null, // the date after the very last cell
//	intervalStart: null, // the start of the interval of time the view represents (1st of month for month view)
//	intervalEnd: null, // the exclusive end of the interval of time the view represents

//	// used for cell-to-date and date-to-cell calculations
//	rowCnt: null, // # of weeks
//	colCnt: null, // # of days displayed in a week

//	isSelected: false, // boolean whether cells are user-selected or not

//	// subclasses can optionally use a scroll container
//	scrollerEl: null, // the element that will most likely scroll when content is too tall
//	scrollTop: null, // cached vertical scroll value

//	// classNames styled by jqui themes
//	widgetHeaderClass: null,
//	widgetContentClass: null,
//	highlightStateClass: null,

//	// document handlers, bound to `this` object
//	documentMousedownProxy: null,
//	documentDragStartProxy: null,


//	// Serves as a "constructor" to suppliment the monster `View` constructor below
//	init: function() {
//		var tm = this.opt('theme') ? 'ui' : 'fc';

//		this.widgetHeaderClass = tm + '-widget-header';
//		this.widgetContentClass = tm + '-widget-content';
//		this.highlightStateClass = tm + '-state-highlight';

//		// save references to `this`-bound handlers
//		this.documentMousedownProxy = $.proxy(this, 'documentMousedown');
//		this.documentDragStartProxy = $.proxy(this, 'documentDragStart');
//	},


//	// Renders the view inside an already-defined `this.el`.
//	// Subclasses should override this and then call the super method afterwards.
//	render: function() {
//		this.updateSize();
//		this.trigger('viewRender', this, this, this.el);

//		// attach handlers to document. do it here to allow for destroy/rerender
//		$(document)
//			.on('mousedown', this.documentMousedownProxy)
//			.on('dragstart', this.documentDragStartProxy); // jqui drag
//	},


//	// Clears all view rendering, event elements, and unregisters handlers
//	destroy: function() {
//		this.unselect();
//		this.trigger('viewDestroy', this, this, this.el);
//		this.destroyEvents();
//		this.el.empty(); // removes inner contents but leaves the element intact

//		$(document)
//			.off('mousedown', this.documentMousedownProxy)
//			.off('dragstart', this.documentDragStartProxy);
//	},


//	// Used to determine what happens when the users clicks next/prev. Given -1 for prev, 1 for next.
//	// Should apply the delta to `date` (a Moment) and return it.
//	incrementDate: function(date, delta) {
//		// subclasses should implement
//	},


//	/* Dimensions
//	------------------------------------------------------------------------------------------------------------------*/


//	// Refreshes anything dependant upon sizing of the container element of the grid
//	updateSize: function(isResize) {
//		if (isResize) {
//			this.recordScroll();
//		}
//		this.updateHeight();
//		this.updateWidth();
//	},


//	// Refreshes the horizontal dimensions of the calendar
//	updateWidth: function() {
//		// subclasses should implement
//	},


//	// Refreshes the vertical dimensions of the calendar
//	updateHeight: function() {
//		var calendar = this.calendar; // we poll the calendar for height information

//		this.setHeight(
//			calendar.getSuggestedViewHeight(),
//			calendar.isHeightAuto()
//		);
//	},


//	// Updates the vertical dimensions of the calendar to the specified height.
//	// if `isAuto` is set to true, height becomes merely a suggestion and the view should use its "natural" height.
//	setHeight: function(height, isAuto) {
//		// subclasses should implement
//	},


//	// Given the total height of the view, return the number of pixels that should be used for the scroller.
//	// Utility for subclasses.
//	computeScrollerHeight: function(totalHeight) {
//		var both = this.el.add(this.scrollerEl);
//		var otherHeight; // cumulative height of everything that is not the scrollerEl in the view (header+borders)

//		// fuckin IE8/9/10/11 sometimes returns 0 for dimensions. this weird hack was the only thing that worked
//		both.css({
//			position: 'relative', // cause a reflow, which will force fresh dimension recalculation
//			left: -1 // ensure reflow in case the el was already relative. negative is less likely to cause new scroll
//		});
//		otherHeight = this.el.outerHeight() - this.scrollerEl.height(); // grab the dimensions
//		both.css({ position: '', left: '' }); // undo hack

//		return totalHeight - otherHeight;
//	},


//	// Called for remembering the current scroll value of the scroller.
//	// Should be called before there is a destructive operation (like removing DOM elements) that might inadvertently
//	// change the scroll of the container.
//	recordScroll: function() {
//		if (this.scrollerEl) {
//			this.scrollTop = this.scrollerEl.scrollTop();
//		}
//	},


//	// Set the scroll value of the scroller to the previously recorded value.
//	// Should be called after we know the view's dimensions have been restored following some type of destructive
//	// operation (like temporarily removing DOM elements).
//	restoreScroll: function() {
//		if (this.scrollTop !== null) {
//			this.scrollerEl.scrollTop(this.scrollTop);
//		}
//	},


//	/* Events
//	------------------------------------------------------------------------------------------------------------------*/


//	// Renders the events onto the view.
//	// Should be overriden by subclasses. Subclasses should call the super-method afterwards.
//	renderEvents: function(events) {
//		this.segEach(function(seg) {
//			this.trigger('eventAfterRender', seg.event, seg.event, seg.el);
//		});
//		this.trigger('eventAfterAllRender');
//	},


//	// Removes event elements from the view.
//	// Should be overridden by subclasses. Should call this super-method FIRST, then subclass DOM destruction.
//	destroyEvents: function() {
//		this.segEach(function(seg) {
//			this.trigger('eventDestroy', seg.event, seg.event, seg.el);
//		});
//	},


//	// Given an event and the default element used for rendering, returns the element that should actually be used.
//	// Basically runs events and elements through the eventRender hook.
//	resolveEventEl: function(event, el) {
//		var custom = this.trigger('eventRender', event, event, el);

//		if (custom === false) { // means don't render at all
//			el = null;
//		}
//		else if (custom && custom !== true) {
//			el = $(custom);
//		}

//		return el;
//	},


//	// Hides all rendered event segments linked to the given event
//	showEvent: function(event) {
//		this.segEach(function(seg) {
//			seg.el.css('visibility', '');
//		}, event);
//	},


//	// Shows all rendered event segments linked to the given event
//	hideEvent: function(event) {
//		this.segEach(function(seg) {
//			seg.el.css('visibility', 'hidden');
//		}, event);
//	},


//	// Iterates through event segments. Goes through all by default.
//	// If the optional `event` argument is specified, only iterates through segments linked to that event.
//	// The `this` value of the callback function will be the view.
//	segEach: function(func, event) {
//		var segs = this.getSegs();
//		var i;

//		for (i = 0; i < segs.length; i++) {
//			if (!event || segs[i].event._id === event._id) {
//				func.call(this, segs[i]);
//			}
//		}
//	},


//	// Retrieves all the rendered segment objects for the view
//	getSegs: function() {
//		// subclasses must implement
//	},


//	/* Event Drag Visualization
//	------------------------------------------------------------------------------------------------------------------*/


//	// Renders a visual indication of an event hovering over the specified date.
//	// `end` is a Moment and might be null.
//	// `seg` might be null. if specified, it is the segment object of the event being dragged.
//	//       otherwise, an external event from outside the calendar is being dragged.
//	renderDrag: function(start, end, seg) {
//		// subclasses should implement
//	},


//	// Unrenders a visual indication of event hovering
//	destroyDrag: function() {
//		// subclasses should implement
//	},


//	// Handler for accepting externally dragged events being dropped in the view.
//	// Gets called when jqui's 'dragstart' is fired.
//	documentDragStart: function(ev, ui) {
//		var _this = this;
//		var dropDate = null;
//		var dragListener;

//		if (this.opt('droppable')) { // only listen if this setting is on

//			// listener that tracks mouse movement over date-associated pixel regions
//			dragListener = new DragListener(this.coordMap, {
//				cellOver: function(cell, date) {
//					dropDate = date;
//					_this.renderDrag(date);
//				},
//				cellOut: function() {
//					dropDate = null;
//					_this.destroyDrag();
//				}
//			});

//			// gets called, only once, when jqui drag is finished
//			$(document).one('dragstop', function(ev, ui) {
//				_this.destroyDrag();
//				if (dropDate) {
//					_this.trigger('drop', ev.target, dropDate, ev, ui);
//				}
//			});

//			dragListener.startDrag(ev); // start listening immediately
//		}
//	},


//	/* Selection
//	------------------------------------------------------------------------------------------------------------------*/


//	// Selects a date range on the view. `start` and `end` are both Moments.
//	// `ev` is the native mouse event that begin the interaction.
//	select: function(start, end, ev) {
//		this.unselect(ev);
//		this.renderSelection(start, end);
//		this.reportSelection(start, end, ev);
//	},


//	// Renders a visual indication of the selection
//	renderSelection: function(start, end) {
//		// subclasses should implement
//	},


//	// Called when a new selection is made. Updates internal state and triggers handlers.
//	reportSelection: function(start, end, ev) {
//		this.isSelected = true;
//		this.trigger('select', null, start, end, ev);
//	},


//	// Undoes a selection. updates in the internal state and triggers handlers.
//	// `ev` is the native mouse event that began the interaction.
//	unselect: function(ev) {
//		if (this.isSelected) {
//			this.isSelected = false;
//			this.destroySelection();
//			this.trigger('unselect', null, ev);
//		}
//	},


//	// Unrenders a visual indication of selection
//	destroySelection: function() {
//		// subclasses should implement
//	},


//	// Handler for unselecting when the user clicks something and the 'unselectAuto' setting is on
//	documentMousedown: function(ev) {
//		var ignore;

//		// is there a selection, and has the user made a proper left click?
//		if (this.isSelected && this.opt('unselectAuto') && isPrimaryMouseButton(ev)) {

//			// only unselect if the clicked element is not identical to or inside of an 'unselectCancel' element
//			ignore = this.opt('unselectCancel');
//			if (!ignore || !$(ev.target).closest(ignore).length) {
//				this.unselect(ev);
//			}
//		}
//	}

//};


//// We are mixing JavaScript OOP design patterns here by putting methods and member variables in the closed scope of the
//// constructor. Going forward, methods should be part of the prototype.
//function View(calendar) {
//	var t = this;
	
//	// exports
//	t.calendar = calendar;
//	t.opt = opt;
//	t.trigger = trigger;
//	t.isEventDraggable = isEventDraggable;
//	t.isEventResizable = isEventResizable;
//	t.eventDrop = eventDrop;
//	t.eventResize = eventResize;
	
//	// imports
//	var reportEventChange = calendar.reportEventChange;
	
//	// locals
//	var options = calendar.options;
//	var nextDayThreshold = moment.duration(options.nextDayThreshold);


//	t.init(); // the "constructor" that concerns the prototype methods
	
	
//	function opt(name) {
//		var v = options[name];
//		if ($.isPlainObject(v) && !isForcedAtomicOption(name)) {
//			return smartProperty(v, t.name);
//		}
//		return v;
//	}

	
//	function trigger(name, thisObj) {
//		return calendar.trigger.apply(
//			calendar,
//			[name, thisObj || t].concat(Array.prototype.slice.call(arguments, 2), [t])
//		);
//	}
	


//	/* Event Editable Boolean Calculations
//	------------------------------------------------------------------------------*/

	
//	function isEventDraggable(event) {
//		var source = event.source || {};

//		return firstDefined(
//			event.startEditable,
//			source.startEditable,
//			opt('eventStartEditable'),
//			event.editable,
//			source.editable,
//			opt('editable')
//		);
//	}
	
	
//	function isEventResizable(event) {
//		var source = event.source || {};

//		return firstDefined(
//			event.durationEditable,
//			source.durationEditable,
//			opt('eventDurationEditable'),
//			event.editable,
//			source.editable,
//			opt('editable')
//		);
//	}
	
	
	
//	/* Event Elements
//	------------------------------------------------------------------------------*/


//	// Compute the text that should be displayed on an event's element.
//	// Based off the settings of the view. Possible signatures:
//	//   .getEventTimeText(event, formatStr)
//	//   .getEventTimeText(startMoment, endMoment, formatStr)
//	//   .getEventTimeText(startMoment, null, formatStr)
//	// `timeFormat` is used but the `formatStr` argument can be used to override.
//	t.getEventTimeText = function(event, formatStr) {
//		var start;
//		var end;

//		if (typeof event === 'object' && typeof formatStr === 'object') {
//			// first two arguments are actually moments (or null). shift arguments.
//			start = event;
//			end = formatStr;
//			formatStr = arguments[2];
//		}
//		else {
//			// otherwise, an event object was the first argument
//			start = event.start;
//			end = event.end;
//		}

//		formatStr = formatStr || opt('timeFormat');

//		if (end && opt('displayEventEnd')) {
//			return calendar.formatRange(start, end, formatStr);
//		}
//		else {
//			return calendar.formatDate(start, formatStr);
//		}
//	};

	
	
//	/* Event Modification Reporting
//	---------------------------------------------------------------------------------*/

	
//	function eventDrop(el, event, newStart, ev) {
//		var mutateResult = calendar.mutateEvent(event, newStart, null);

//		trigger(
//			'eventDrop',
//			el,
//			event,
//			mutateResult.dateDelta,
//			function() {
//				mutateResult.undo();
//				reportEventChange();
//			},
//			ev,
//			{} // jqui dummy
//		);

//		reportEventChange();
//	}


//	function eventResize(el, event, newEnd, ev) {
//		var mutateResult = calendar.mutateEvent(event, null, newEnd);

//		trigger(
//			'eventResize',
//			el,
//			event,
//			mutateResult.durationDelta,
//			function() {
//				mutateResult.undo();
//				reportEventChange();
//			},
//			ev,
//			{} // jqui dummy
//		);

//		reportEventChange();
//	}


//	// ====================================================================================================
//	// Utilities for day "cells"
//	// ====================================================================================================
//	// The "basic" views are completely made up of day cells.
//	// The "agenda" views have day cells at the top "all day" slot.
//	// This was the obvious common place to put these utilities, but they should be abstracted out into
//	// a more meaningful class (like DayEventRenderer).
//	// ====================================================================================================


//	// For determining how a given "cell" translates into a "date":
//	//
//	// 1. Convert the "cell" (row and column) into a "cell offset" (the # of the cell, cronologically from the first).
//	//    Keep in mind that column indices are inverted with isRTL. This is taken into account.
//	//
//	// 2. Convert the "cell offset" to a "day offset" (the # of days since the first visible day in the view).
//	//
//	// 3. Convert the "day offset" into a "date" (a Moment).
//	//
//	// The reverse transformation happens when transforming a date into a cell.


//	// exports
//	t.isHiddenDay = isHiddenDay;
//	t.skipHiddenDays = skipHiddenDays;
//	t.getCellsPerWeek = getCellsPerWeek;
//	t.dateToCell = dateToCell;
//	t.dateToDayOffset = dateToDayOffset;
//	t.dayOffsetToCellOffset = dayOffsetToCellOffset;
//	t.cellOffsetToCell = cellOffsetToCell;
//	t.cellToDate = cellToDate;
//	t.cellToCellOffset = cellToCellOffset;
//	t.cellOffsetToDayOffset = cellOffsetToDayOffset;
//	t.dayOffsetToDate = dayOffsetToDate;
//	t.rangeToSegments = rangeToSegments;
//	t.isMultiDayEvent = isMultiDayEvent;


//	// internals
//	var hiddenDays = opt('hiddenDays') || []; // array of day-of-week indices that are hidden
//	var isHiddenDayHash = []; // is the day-of-week hidden? (hash with day-of-week-index -> bool)
//	var cellsPerWeek;
//	var dayToCellMap = []; // hash from dayIndex -> cellIndex, for one week
//	var cellToDayMap = []; // hash from cellIndex -> dayIndex, for one week
//	var isRTL = opt('isRTL');


//	// initialize important internal variables
//	(function() {

//		if (opt('weekends') === false) {
//			hiddenDays.push(0, 6); // 0=sunday, 6=saturday
//		}

//		// Loop through a hypothetical week and determine which
//		// days-of-week are hidden. Record in both hashes (one is the reverse of the other).
//		for (var dayIndex=0, cellIndex=0; dayIndex<7; dayIndex++) {
//			dayToCellMap[dayIndex] = cellIndex;
//			isHiddenDayHash[dayIndex] = $.inArray(dayIndex, hiddenDays) != -1;
//			if (!isHiddenDayHash[dayIndex]) {
//				cellToDayMap[cellIndex] = dayIndex;
//				cellIndex++;
//			}
//		}

//		cellsPerWeek = cellIndex;
//		if (!cellsPerWeek) {
//			throw 'invalid hiddenDays'; // all days were hidden? bad.
//		}

//	})();


//	// Is the current day hidden?
//	// `day` is a day-of-week index (0-6), or a Moment
//	function isHiddenDay(day) {
//		if (moment.isMoment(day)) {
//			day = day.day();
//		}
//		return isHiddenDayHash[day];
//	}


//	function getCellsPerWeek() {
//		return cellsPerWeek;
//	}


//	// Incrementing the current day until it is no longer a hidden day, returning a copy.
//	// If the initial value of `date` is not a hidden day, don't do anything.
//	// Pass `isExclusive` as `true` if you are dealing with an end date.
//	// `inc` defaults to `1` (increment one day forward each time)
//	function skipHiddenDays(date, inc, isExclusive) {
//		var out = date.clone();
//		inc = inc || 1;
//		while (
//			isHiddenDayHash[(out.day() + (isExclusive ? inc : 0) + 7) % 7]
//		) {
//			out.add(inc, 'days');
//		}
//		return out;
//	}


//	//
//	// TRANSFORMATIONS: cell -> cell offset -> day offset -> date
//	//

//	// cell -> date (combines all transformations)
//	// Possible arguments:
//	// - row, col
//	// - { row:#, col: # }
//	function cellToDate() {
//		var cellOffset = cellToCellOffset.apply(null, arguments);
//		var dayOffset = cellOffsetToDayOffset(cellOffset);
//		var date = dayOffsetToDate(dayOffset);
//		return date;
//	}

//	// cell -> cell offset
//	// Possible arguments:
//	// - row, col
//	// - { row:#, col:# }
//	function cellToCellOffset(row, col) {
//		var colCnt = t.colCnt;

//		// rtl variables. wish we could pre-populate these. but where?
//		var dis = isRTL ? -1 : 1;
//		var dit = isRTL ? colCnt - 1 : 0;

//		if (typeof row == 'object') {
//			col = row.col;
//			row = row.row;
//		}
//		var cellOffset = row * colCnt + (col * dis + dit); // column, adjusted for RTL (dis & dit)

//		return cellOffset;
//	}

//	// cell offset -> day offset
//	function cellOffsetToDayOffset(cellOffset) {
//		var day0 = t.start.day(); // first date's day of week
//		cellOffset += dayToCellMap[day0]; // normlize cellOffset to beginning-of-week
//		return Math.floor(cellOffset / cellsPerWeek) * 7 + // # of days from full weeks
//			cellToDayMap[ // # of days from partial last week
//				(cellOffset % cellsPerWeek + cellsPerWeek) % cellsPerWeek // crazy math to handle negative cellOffsets
//			] -
//			day0; // adjustment for beginning-of-week normalization
//	}

//	// day offset -> date
//	function dayOffsetToDate(dayOffset) {
//		return t.start.clone().add(dayOffset, 'days');
//	}


//	//
//	// TRANSFORMATIONS: date -> day offset -> cell offset -> cell
//	//

//	// date -> cell (combines all transformations)
//	function dateToCell(date) {
//		var dayOffset = dateToDayOffset(date);
//		var cellOffset = dayOffsetToCellOffset(dayOffset);
//		var cell = cellOffsetToCell(cellOffset);
//		return cell;
//	}

//	// date -> day offset
//	function dateToDayOffset(date) {
//		return date.clone().stripTime().diff(t.start, 'days');
//	}

//	// day offset -> cell offset
//	function dayOffsetToCellOffset(dayOffset) {
//		var day0 = t.start.day(); // first date's day of week
//		dayOffset += day0; // normalize dayOffset to beginning-of-week
//		return Math.floor(dayOffset / 7) * cellsPerWeek + // # of cells from full weeks
//			dayToCellMap[ // # of cells from partial last week
//				(dayOffset % 7 + 7) % 7 // crazy math to handle negative dayOffsets
//			] -
//			dayToCellMap[day0]; // adjustment for beginning-of-week normalization
//	}

//	// cell offset -> cell (object with row & col keys)
//	function cellOffsetToCell(cellOffset) {
//		var colCnt = t.colCnt;

//		// rtl variables. wish we could pre-populate these. but where?
//		var dis = isRTL ? -1 : 1;
//		var dit = isRTL ? colCnt - 1 : 0;

//		var row = Math.floor(cellOffset / colCnt);
//		var col = ((cellOffset % colCnt + colCnt) % colCnt) * dis + dit; // column, adjusted for RTL (dis & dit)
//		return {
//			row: row,
//			col: col
//		};
//	}


//	//
//	// Converts a date range into an array of segment objects.
//	// "Segments" are horizontal stretches of time, sliced up by row.
//	// A segment object has the following properties:
//	// - row
//	// - cols
//	// - isStart
//	// - isEnd
//	//
//	function rangeToSegments(start, end) {

//		var rowCnt = t.rowCnt;
//		var colCnt = t.colCnt;
//		var segments = []; // array of segments to return

//		// day offset for given date range
//		var dayRange = computeDayRange(start, end); // convert to a whole-day range
//		var rangeDayOffsetStart = dateToDayOffset(dayRange.start);
//		var rangeDayOffsetEnd = dateToDayOffset(dayRange.end); // an exclusive value

//		// first and last cell offset for the given date range
//		// "last" implies inclusivity
//		var rangeCellOffsetFirst = dayOffsetToCellOffset(rangeDayOffsetStart);
//		var rangeCellOffsetLast = dayOffsetToCellOffset(rangeDayOffsetEnd) - 1;

//		// loop through all the rows in the view
//		for (var row=0; row<rowCnt; row++) {

//			// first and last cell offset for the row
//			var rowCellOffsetFirst = row * colCnt;
//			var rowCellOffsetLast = rowCellOffsetFirst + colCnt - 1;

//			// get the segment's cell offsets by constraining the range's cell offsets to the bounds of the row
//			var segmentCellOffsetFirst = Math.max(rangeCellOffsetFirst, rowCellOffsetFirst);
//			var segmentCellOffsetLast = Math.min(rangeCellOffsetLast, rowCellOffsetLast);

//			// make sure segment's offsets are valid and in view
//			if (segmentCellOffsetFirst <= segmentCellOffsetLast) {

//				// translate to cells
//				var segmentCellFirst = cellOffsetToCell(segmentCellOffsetFirst);
//				var segmentCellLast = cellOffsetToCell(segmentCellOffsetLast);

//				// view might be RTL, so order by leftmost column
//				var cols = [ segmentCellFirst.col, segmentCellLast.col ].sort();

//				// Determine if segment's first/last cell is the beginning/end of the date range.
//				// We need to compare "day offset" because "cell offsets" are often ambiguous and
//				// can translate to multiple days, and an edge case reveals itself when we the
//				// range's first cell is hidden (we don't want isStart to be true).
//				var isStart = cellOffsetToDayOffset(segmentCellOffsetFirst) == rangeDayOffsetStart;
//				var isEnd = cellOffsetToDayOffset(segmentCellOffsetLast) + 1 == rangeDayOffsetEnd;
//				                                                   // +1 for comparing exclusively

//				segments.push({
//					row: row,
//					leftCol: cols[0],
//					rightCol: cols[1],
//					isStart: isStart,
//					isEnd: isEnd
//				});
//			}
//		}

//		return segments;
//	}


//	// Returns the date range of the full days the given range visually appears to occupy.
//	// Returns object with properties `start` (moment) and `end` (moment, exclusive end).
//	function computeDayRange(start, end) {
//		var startDay = start.clone().stripTime(); // the beginning of the day the range starts
//		var endDay;
//		var endTimeMS;

//		if (end) {
//			endDay = end.clone().stripTime(); // the beginning of the day the range exclusively ends
//			endTimeMS = +end.time(); // # of milliseconds into `endDay`

//			// If the end time is actually inclusively part of the next day and is equal to or
//			// beyond the next day threshold, adjust the end to be the exclusive end of `endDay`.
//			// Otherwise, leaving it as inclusive will cause it to exclude `endDay`.
//			if (endTimeMS && endTimeMS >= nextDayThreshold) {
//				endDay.add(1, 'days');
//			}
//		}

//		// If no end was specified, or if it is within `startDay` but not past nextDayThreshold,
//		// assign the default duration of one day.
//		if (!end || endDay <= startDay) {
//			endDay = startDay.clone().add(1, 'days');
//		}

//		return { start: startDay, end: endDay };
//	}


//	// Does the given event visually appear to occupy more than one day?
//	function isMultiDayEvent(event) {
//		var range = computeDayRange(event.start, event.end);

//		return range.end.diff(range.start, 'days') > 1;
//	}

//}

//;;

///* An abstract class for the "basic" views, as well as month view. Renders one or more rows of day cells.
//----------------------------------------------------------------------------------------------------------------------*/
//// It is a manager for a DayGrid subcomponent, which does most of the heavy lifting.
//// It is responsible for managing width/height.

//function BasicView(calendar) {
//	View.call(this, calendar); // call the super-constructor
//	this.dayGrid = new DayGrid(this);
//	this.coordMap = this.dayGrid.coordMap; // the view's date-to-cell mapping is identical to the subcomponent's
//}


//BasicView.prototype = createObject(View.prototype); // define the super-class
//$.extend(BasicView.prototype, {

//	dayGrid: null, // the main subcomponent that does most of the heavy lifting

//	dayNumbersVisible: false, // display day numbers on each day cell?
//	weekNumbersVisible: false, // display week numbers along the side?

//	weekNumberWidth: null, // width of all the week-number cells running down the side

//	headRowEl: null, // the fake row element of the day-of-week header


//	// Renders the view into `this.el`, which should already be assigned.
//	// rowCnt, colCnt, and dayNumbersVisible have been calculated by a subclass and passed here.
//	render: function(rowCnt, colCnt, dayNumbersVisible) {

//		// needed for cell-to-date and date-to-cell calculations in View
//		this.rowCnt = rowCnt;
//		this.colCnt = colCnt;

//		this.dayNumbersVisible = dayNumbersVisible;
//		this.weekNumbersVisible = this.opt('weekNumbers');
//		this.dayGrid.numbersVisible = this.dayNumbersVisible || this.weekNumbersVisible;

//		this.el.addClass('fc-basic-view').html(this.renderHtml());

//		this.headRowEl = this.el.find('thead .fc-row');

//		this.scrollerEl = this.el.find('.fc-day-grid-container');
//		this.dayGrid.coordMap.containerEl = this.scrollerEl; // constrain clicks/etc to the dimensions of the scroller

//		this.dayGrid.el = this.el.find('.fc-day-grid');
//		this.dayGrid.render(this.hasRigidRows());

//		View.prototype.render.call(this); // call the super-method
//	},


//	// Make subcomponents ready for cleanup
//	destroy: function() {
//		this.dayGrid.destroy();
//		View.prototype.destroy.call(this); // call the super-method
//	},


//	// Builds the HTML skeleton for the view.
//	// The day-grid component will render inside of a container defined by this HTML.
//	renderHtml: function() {
//		return '' +
//			'<table>' +
//				'<thead>' +
//					'<tr>' +
//						'<td class="' + this.widgetHeaderClass + '">' +
//							this.dayGrid.headHtml() + // render the day-of-week headers
//						'</td>' +
//					'</tr>' +
//				'</thead>' +
//				'<tbody>' +
//					'<tr>' +
//						'<td class="' + this.widgetContentClass + '">' +
//							'<div class="fc-day-grid-container">' +
//								'<div class="fc-day-grid"/>' +
//							'</div>' +
//						'</td>' +
//					'</tr>' +
//				'</tbody>' +
//			'</table>';
//	},


//	// Generates the HTML that will go before the day-of week header cells.
//	// Queried by the DayGrid subcomponent when generating rows. Ordering depends on isRTL.
//	headIntroHtml: function() {
//		if (this.weekNumbersVisible) {
//			return '' +
//				'<th class="fc-week-number ' + this.widgetHeaderClass + '" ' + this.weekNumberStyleAttr() + '>' +
//					'<span>' + // needed for matchCellWidths
//						htmlEscape(this.opt('weekNumberTitle')) +
//					'</span>' +
//				'</th>';
//		}
//	},


//	// Generates the HTML that will go before content-skeleton cells that display the day/week numbers.
//	// Queried by the DayGrid subcomponent. Ordering depends on isRTL.
//	numberIntroHtml: function(row) {
//		if (this.weekNumbersVisible) {
//			return '' +
//				'<td class="fc-week-number" ' + this.weekNumberStyleAttr() + '>' +
//					'<span>' + // needed for matchCellWidths
//						this.calendar.calculateWeekNumber(this.cellToDate(row, 0)) +
//					'</span>' +
//				'</td>';
//		}
//	},


//	// Generates the HTML that goes before the day bg cells for each day-row.
//	// Queried by the DayGrid subcomponent. Ordering depends on isRTL.
//	dayIntroHtml: function() {
//		if (this.weekNumbersVisible) {
//			return '<td class="fc-week-number ' + this.widgetContentClass + '" ' +
//				this.weekNumberStyleAttr() + '></td>';
//		}
//	},


//	// Generates the HTML that goes before every other type of row generated by DayGrid. Ordering depends on isRTL.
//	// Affects helper-skeleton and highlight-skeleton rows.
//	introHtml: function() {
//		if (this.weekNumbersVisible) {
//			return '<td class="fc-week-number" ' + this.weekNumberStyleAttr() + '></td>';
//		}
//	},


//	// Generates the HTML for the <td>s of the "number" row in the DayGrid's content skeleton.
//	// The number row will only exist if either day numbers or week numbers are turned on.
//	numberCellHtml: function(row, col, date) {
//		var classes;

//		if (!this.dayNumbersVisible) { // if there are week numbers but not day numbers
//			return '<td/>'; //  will create an empty space above events :(
//		}

//		classes = this.dayGrid.getDayClasses(date);
//		classes.unshift('fc-day-number');

//		return '' +
//			'<td class="' + classes.join(' ') + '" data-date="' + date.format() + '">' +
//				date.date() +
//			'</td>';
//	},


//	// Generates an HTML attribute string for setting the width of the week number column, if it is known
//	weekNumberStyleAttr: function() {
//		if (this.weekNumberWidth !== null) {
//			return 'style="width:' + this.weekNumberWidth + 'px"';
//		}
//		return '';
//	},


//	// Determines whether each row should have a constant height
//	hasRigidRows: function() {
//		var eventLimit = this.opt('eventLimit');
//		return eventLimit && typeof eventLimit !== 'number';
//	},


//	/* Dimensions
//	------------------------------------------------------------------------------------------------------------------*/


//	// Refreshes the horizontal dimensions of the view
//	updateWidth: function() {
//		if (this.weekNumbersVisible) {
//			// Make sure all week number cells running down the side have the same width.
//			// Record the width for cells created later.
//			this.weekNumberWidth = matchCellWidths(
//				this.el.find('.fc-week-number')
//			);
//		}
//	},


//	// Adjusts the vertical dimensions of the view to the specified values
//	setHeight: function(totalHeight, isAuto) {
//		var eventLimit = this.opt('eventLimit');
//		var scrollerHeight;

//		// reset all heights to be natural
//		unsetScroller(this.scrollerEl);
//		uncompensateScroll(this.headRowEl);

//		this.dayGrid.destroySegPopover(); // kill the "more" popover if displayed

//		// is the event limit a constant level number?
//		if (eventLimit && typeof eventLimit === 'number') {
//			this.dayGrid.limitRows(eventLimit); // limit the levels first so the height can redistribute after
//		}

//		scrollerHeight = this.computeScrollerHeight(totalHeight);
//		this.setGridHeight(scrollerHeight, isAuto);

//		// is the event limit dynamically calculated?
//		if (eventLimit && typeof eventLimit !== 'number') {
//			this.dayGrid.limitRows(eventLimit); // limit the levels after the grid's row heights have been set
//		}

//		if (!isAuto && setPotentialScroller(this.scrollerEl, scrollerHeight)) { // using scrollbars?

//			compensateScroll(this.headRowEl, getScrollbarWidths(this.scrollerEl));

//			// doing the scrollbar compensation might have created text overflow which created more height. redo
//			scrollerHeight = this.computeScrollerHeight(totalHeight);
//			this.scrollerEl.height(scrollerHeight);

//			this.restoreScroll();
//		}
//	},


//	// Sets the height of just the DayGrid component in this view
//	setGridHeight: function(height, isAuto) {
//		if (isAuto) {
//			undistributeHeight(this.dayGrid.rowEls); // let the rows be their natural height with no expanding
//		}
//		else {
//			distributeHeight(this.dayGrid.rowEls, height, true); // true = compensate for height-hogging rows
//		}
//	},


//	/* Events
//	------------------------------------------------------------------------------------------------------------------*/


//	// Renders the given events onto the view and populates the segments array
//	renderEvents: function(events) {
//		this.dayGrid.renderEvents(events);

//		this.updateHeight(); // must compensate for events that overflow the row

//		View.prototype.renderEvents.call(this, events); // call the super-method
//	},


//	// Retrieves all segment objects that are rendered in the view
//	getSegs: function() {
//		return this.dayGrid.getSegs();
//	},


//	// Unrenders all event elements and clears internal segment data
//	destroyEvents: function() {
//		View.prototype.destroyEvents.call(this); // do this before dayGrid's segs have been cleared

//		this.recordScroll(); // removing events will reduce height and mess with the scroll, so record beforehand
//		this.dayGrid.destroyEvents();

//		// we DON'T need to call updateHeight() because:
//		// A) a renderEvents() call always happens after this, which will eventually call updateHeight()
//		// B) in IE8, this causes a flash whenever events are rerendered
//	},


//	/* Event Dragging
//	------------------------------------------------------------------------------------------------------------------*/


//	// Renders a visual indication of an event being dragged over the view.
//	// A returned value of `true` signals that a mock "helper" event has been rendered.
//	renderDrag: function(start, end, seg) {
//		return this.dayGrid.renderDrag(start, end, seg);
//	},


//	// Unrenders the visual indication of an event being dragged over the view
//	destroyDrag: function() {
//		this.dayGrid.destroyDrag();
//	},


//	/* Selection
//	------------------------------------------------------------------------------------------------------------------*/


//	// Renders a visual indication of a selection
//	renderSelection: function(start, end) {
//		this.dayGrid.renderSelection(start, end);
//	},


//	// Unrenders a visual indications of a selection
//	destroySelection: function() {
//		this.dayGrid.destroySelection();
//	}

//});

//;;

///* A month view with day cells running in rows (one-per-week) and columns
//----------------------------------------------------------------------------------------------------------------------*/

//setDefaults({
//	fixedWeekCount: true
//});

//fcViews.month = MonthView; // register the view

//function MonthView(calendar) {
//	BasicView.call(this, calendar); // call the super-constructor
//}


//MonthView.prototype = createObject(BasicView.prototype); // define the super-class
//$.extend(MonthView.prototype, {

//	name: 'month',


//	incrementDate: function(date, delta) {
//		return date.clone().stripTime().add(delta, 'months').startOf('month');
//	},


//	render: function(date) {
//		var rowCnt;

//		this.intervalStart = date.clone().stripTime().startOf('month');
//		this.intervalEnd = this.intervalStart.clone().add(1, 'months');

//		this.start = this.intervalStart.clone();
//		this.start = this.skipHiddenDays(this.start); // move past the first week if no visible days
//		this.start.startOf('week');
//		this.start = this.skipHiddenDays(this.start); // move past the first invisible days of the week

//		this.end = this.intervalEnd.clone();
//		this.end = this.skipHiddenDays(this.end, -1, true); // move in from the last week if no visible days
//		this.end.add((7 - this.end.weekday()) % 7, 'days'); // move to end of week if not already
//		this.end = this.skipHiddenDays(this.end, -1, true); // move in from the last invisible days of the week

//		rowCnt = Math.ceil( // need to ceil in case there are hidden days
//			this.end.diff(this.start, 'weeks', true) // returnfloat=true
//		);
//		if (this.isFixedWeeks()) {
//			this.end.add(6 - rowCnt, 'weeks');
//			rowCnt = 6;
//		}

//		this.title = this.calendar.formatDate(this.intervalStart, this.opt('titleFormat'));

//		BasicView.prototype.render.call(this, rowCnt, this.getCellsPerWeek(), true); // call the super-method
//	},


//	// Overrides the default BasicView behavior to have special multi-week auto-height logic
//	setGridHeight: function(height, isAuto) {

//		isAuto = isAuto || this.opt('weekMode') === 'variable'; // LEGACY: weekMode is deprecated

//		// if auto, make the height of each row the height that it would be if there were 6 weeks
//		if (isAuto) {
//			height *= this.rowCnt / 6;
//		}

//		distributeHeight(this.dayGrid.rowEls, height, !isAuto); // if auto, don't compensate for height-hogging rows
//	},


//	isFixedWeeks: function() {
//		var weekMode = this.opt('weekMode'); // LEGACY: weekMode is deprecated
//		if (weekMode) {
//			return weekMode === 'fixed'; // if any other type of weekMode, assume NOT fixed
//		}

//		return this.opt('fixedWeekCount');
//	}

//});

//;;

///* A week view with simple day cells running horizontally
//----------------------------------------------------------------------------------------------------------------------*/
//// TODO: a WeekView mixin for calculating dates and titles

//fcViews.basicWeek = BasicWeekView; // register this view

//function BasicWeekView(calendar) {
//	BasicView.call(this, calendar); // call the super-constructor
//}


//BasicWeekView.prototype = createObject(BasicView.prototype); // define the super-class
//$.extend(BasicWeekView.prototype, {

//	name: 'basicWeek',


//	incrementDate: function(date, delta) {
//		return date.clone().stripTime().add(delta, 'weeks').startOf('week');
//	},


//	render: function(date) {

//		this.intervalStart = date.clone().stripTime().startOf('week');
//		this.intervalEnd = this.intervalStart.clone().add(1, 'weeks');

//		this.start = this.skipHiddenDays(this.intervalStart);
//		this.end = this.skipHiddenDays(this.intervalEnd, -1, true);

//		this.title = this.calendar.formatRange(
//			this.start,
//			this.end.clone().subtract(1), // make inclusive by subtracting 1 ms
//			this.opt('titleFormat'),
//			' \u2014 ' // emphasized dash
//		);

//		BasicView.prototype.render.call(this, 1, this.getCellsPerWeek(), false); // call the super-method
//	}
	
//});
//;;

///* A view with a single simple day cell
//----------------------------------------------------------------------------------------------------------------------*/

//fcViews.basicDay = BasicDayView; // register this view

//function BasicDayView(calendar) {
//	BasicView.call(this, calendar); // call the super-constructor
//}


//BasicDayView.prototype = createObject(BasicView.prototype); // define the super-class
//$.extend(BasicDayView.prototype, {

//	name: 'basicDay',


//	incrementDate: function(date, delta) {
//		var out = date.clone().stripTime().add(delta, 'days');
//		out = this.skipHiddenDays(out, delta < 0 ? -1 : 1);
//		return out;
//	},


//	render: function(date) {

//		this.start = this.intervalStart = date.clone().stripTime();
//		this.end = this.intervalEnd = this.start.clone().add(1, 'days');

//		this.title = this.calendar.formatDate(this.start, this.opt('titleFormat'));

//		BasicView.prototype.render.call(this, 1, 1, false); // call the super-method
//	}

//});
//;;

///* An abstract class for all agenda-related views. Displays one more columns with time slots running vertically.
//----------------------------------------------------------------------------------------------------------------------*/
//// Is a manager for the TimeGrid subcomponent and possibly the DayGrid subcomponent (if allDaySlot is on).
//// Responsible for managing width/height.

//setDefaults({
//	allDaySlot: true,
//	allDayText: 'all-day',

//	scrollTime: '06:00:00',

//	slotDuration: '00:30:00',

//	axisFormat: generateAgendaAxisFormat,
//	timeFormat: {
//		agenda: generateAgendaTimeFormat
//	},

//	minTime: '00:00:00',
//	maxTime: '24:00:00',
//	slotEventOverlap: true
//});

//var AGENDA_ALL_DAY_EVENT_LIMIT = 5;


//function generateAgendaAxisFormat(options, langData) {
//	return langData.longDateFormat('LT')
//		.replace(':mm', '(:mm)')
//		.replace(/(\Wmm)$/, '($1)') // like above, but for foreign langs
//		.replace(/\s*a$/i, 'a'); // convert AM/PM/am/pm to lowercase. remove any spaces beforehand
//}


//function generateAgendaTimeFormat(options, langData) {
//	return langData.longDateFormat('LT')
//		.replace(/\s*a$/i, ''); // remove trailing AM/PM
//}


//function AgendaView(calendar) {
//	View.call(this, calendar); // call the super-constructor

//	this.timeGrid = new TimeGrid(this);

//	if (this.opt('allDaySlot')) { // should we display the "all-day" area?
//		this.dayGrid = new DayGrid(this); // the all-day subcomponent of this view

//		// the coordinate grid will be a combination of both subcomponents' grids
//		this.coordMap = new ComboCoordMap([
//			this.dayGrid.coordMap,
//			this.timeGrid.coordMap
//		]);
//	}
//	else {
//		this.coordMap = this.timeGrid.coordMap;
//	}
//}


//AgendaView.prototype = createObject(View.prototype); // define the super-class
//$.extend(AgendaView.prototype, {

//	timeGrid: null, // the main time-grid subcomponent of this view
//	dayGrid: null, // the "all-day" subcomponent. if all-day is turned off, this will be null

//	axisWidth: null, // the width of the time axis running down the side

//	noScrollRowEls: null, // set of fake row elements that must compensate when scrollerEl has scrollbars

//	// when the time-grid isn't tall enough to occupy the given height, we render an <hr> underneath
//	bottomRuleEl: null,
//	bottomRuleHeight: null,


//	/* Rendering
//	------------------------------------------------------------------------------------------------------------------*/


//	// Renders the view into `this.el`, which has already been assigned.
//	// `colCnt` has been calculated by a subclass and passed here.
//	render: function(colCnt) {

//		// needed for cell-to-date and date-to-cell calculations in View
//		this.rowCnt = 1;
//		this.colCnt = colCnt;

//		this.el.addClass('fc-agenda-view').html(this.renderHtml());

//		// the element that wraps the time-grid that will probably scroll
//		this.scrollerEl = this.el.find('.fc-time-grid-container');
//		this.timeGrid.coordMap.containerEl = this.scrollerEl; // don't accept clicks/etc outside of this

//		this.timeGrid.el = this.el.find('.fc-time-grid');
//		this.timeGrid.render();

//		// the <hr> that sometimes displays under the time-grid
//		this.bottomRuleEl = $('<hr class="' + this.widgetHeaderClass + '"/>')
//			.appendTo(this.timeGrid.el); // inject it into the time-grid

//		if (this.dayGrid) {
//			this.dayGrid.el = this.el.find('.fc-day-grid');
//			this.dayGrid.render();

//			// have the day-grid extend it's coordinate area over the <hr> dividing the two grids
//			this.dayGrid.bottomCoordPadding = this.dayGrid.el.next('hr').outerHeight();
//		}

//		this.noScrollRowEls = this.el.find('.fc-row:not(.fc-scroller *)'); // fake rows not within the scroller

//		View.prototype.render.call(this); // call the super-method

//		this.resetScroll(); // do this after sizes have been set
//	},


//	// Make subcomponents ready for cleanup
//	destroy: function() {
//		this.timeGrid.destroy();
//		if (this.dayGrid) {
//			this.dayGrid.destroy();
//		}
//		View.prototype.destroy.call(this); // call the super-method
//	},


//	// Builds the HTML skeleton for the view.
//	// The day-grid and time-grid components will render inside containers defined by this HTML.
//	renderHtml: function() {
//		return '' +
//			'<table>' +
//				'<thead>' +
//					'<tr>' +
//						'<td class="' + this.widgetHeaderClass + '">' +
//							this.timeGrid.headHtml() + // render the day-of-week headers
//						'</td>' +
//					'</tr>' +
//				'</thead>' +
//				'<tbody>' +
//					'<tr>' +
//						'<td class="' + this.widgetContentClass + '">' +
//							(this.dayGrid ?
//								'<div class="fc-day-grid"/>' +
//								'<hr class="' + this.widgetHeaderClass + '"/>' :
//								''
//								) +
//							'<div class="fc-time-grid-container">' +
//								'<div class="fc-time-grid"/>' +
//							'</div>' +
//						'</td>' +
//					'</tr>' +
//				'</tbody>' +
//			'</table>';
//	},


//	// Generates the HTML that will go before the day-of week header cells.
//	// Queried by the TimeGrid subcomponent when generating rows. Ordering depends on isRTL.
//	headIntroHtml: function() {
//		var date;
//		var weekNumber;
//		var weekTitle;
//		var weekText;

//		if (this.opt('weekNumbers')) {
//			date = this.cellToDate(0, 0);
//			weekNumber = this.calendar.calculateWeekNumber(date);
//			weekTitle = this.opt('weekNumberTitle');

//			if (this.opt('isRTL')) {
//				weekText = weekNumber + weekTitle;
//			}
//			else {
//				weekText = weekTitle + weekNumber;
//			}

//			return '' +
//				'<th class="fc-axis fc-week-number ' + this.widgetHeaderClass + '" ' + this.axisStyleAttr() + '>' +
//					'<span>' + // needed for matchCellWidths
//						htmlEscape(weekText) +
//					'</span>' +
//				'</th>';
//		}
//		else {
//			return '<th class="fc-axis ' + this.widgetHeaderClass + '" ' + this.axisStyleAttr() + '></th>';
//		}
//	},


//	// Generates the HTML that goes before the all-day cells.
//	// Queried by the DayGrid subcomponent when generating rows. Ordering depends on isRTL.
//	dayIntroHtml: function() {
//		return '' +
//			'<td class="fc-axis ' + this.widgetContentClass + '" ' + this.axisStyleAttr() + '>' +
//				'<span>' + // needed for matchCellWidths
//					(this.opt('allDayHtml') || htmlEscape(this.opt('allDayText'))) +
//				'</span>' +
//			'</td>';
//	},


//	// Generates the HTML that goes before the bg of the TimeGrid slot area. Long vertical column.
//	slotBgIntroHtml: function() {
//		return '<td class="fc-axis ' + this.widgetContentClass + '" ' + this.axisStyleAttr() + '></td>';
//	},


//	// Generates the HTML that goes before all other types of cells.
//	// Affects content-skeleton, helper-skeleton, highlight-skeleton for both the time-grid and day-grid.
//	// Queried by the TimeGrid and DayGrid subcomponents when generating rows. Ordering depends on isRTL.
//	introHtml: function() {
//		return '<td class="fc-axis" ' + this.axisStyleAttr() + '></td>';
//	},


//	// Generates an HTML attribute string for setting the width of the axis, if it is known
//	axisStyleAttr: function() {
//		if (this.axisWidth !== null) {
//			 return 'style="width:' + this.axisWidth + 'px"';
//		}
//		return '';
//	},


//	/* Dimensions
//	------------------------------------------------------------------------------------------------------------------*/

//	updateSize: function(isResize) {
//		if (isResize) {
//			this.timeGrid.resize();
//		}
//		View.prototype.updateSize.call(this, isResize);
//	},


//	// Refreshes the horizontal dimensions of the view
//	updateWidth: function() {
//		// make all axis cells line up, and record the width so newly created axis cells will have it
//		this.axisWidth = matchCellWidths(this.el.find('.fc-axis'));
//	},


//	// Adjusts the vertical dimensions of the view to the specified values
//	setHeight: function(totalHeight, isAuto) {
//		var eventLimit;
//		var scrollerHeight;

//		if (this.bottomRuleHeight === null) {
//			// calculate the height of the rule the very first time
//			this.bottomRuleHeight = this.bottomRuleEl.outerHeight();
//		}
//		this.bottomRuleEl.hide(); // .show() will be called later if this <hr> is necessary

//		// reset all dimensions back to the original state
//		this.scrollerEl.css('overflow', '');
//		unsetScroller(this.scrollerEl);
//		uncompensateScroll(this.noScrollRowEls);

//		// limit number of events in the all-day area
//		if (this.dayGrid) {
//			this.dayGrid.destroySegPopover(); // kill the "more" popover if displayed

//			eventLimit = this.opt('eventLimit');
//			if (eventLimit && typeof eventLimit !== 'number') {
//				eventLimit = AGENDA_ALL_DAY_EVENT_LIMIT; // make sure "auto" goes to a real number
//			}
//			if (eventLimit) {
//				this.dayGrid.limitRows(eventLimit);
//			}
//		}

//		if (!isAuto) { // should we force dimensions of the scroll container, or let the contents be natural height?

//			scrollerHeight = this.computeScrollerHeight(totalHeight);
//			if (setPotentialScroller(this.scrollerEl, scrollerHeight)) { // using scrollbars?

//				// make the all-day and header rows lines up
//				compensateScroll(this.noScrollRowEls, getScrollbarWidths(this.scrollerEl));

//				// the scrollbar compensation might have changed text flow, which might affect height, so recalculate
//				// and reapply the desired height to the scroller.
//				scrollerHeight = this.computeScrollerHeight(totalHeight);
//				this.scrollerEl.height(scrollerHeight);

//				this.restoreScroll();
//			}
//			else { // no scrollbars
//				// still, force a height and display the bottom rule (marks the end of day)
//				this.scrollerEl.height(scrollerHeight).css('overflow', 'hidden'); // in case <hr> goes outside
//				this.bottomRuleEl.show();
//			}
//		}
//	},


//	// Sets the scroll value of the scroller to the intial pre-configured state prior to allowing the user to change it.
//	resetScroll: function() {
//		var _this = this;
//		var scrollTime = moment.duration(this.opt('scrollTime'));
//		var top = this.timeGrid.computeTimeTop(scrollTime);

//		// zoom can give weird floating-point values. rather scroll a little bit further
//		top = Math.ceil(top);

//		if (top) {
//			top++; // to overcome top border that slots beyond the first have. looks better
//		}

//		function scroll() {
//			_this.scrollerEl.scrollTop(top);
//		}

//		scroll();
//		setTimeout(scroll, 0); // overrides any previous scroll state made by the browser
//	},


//	/* Events
//	------------------------------------------------------------------------------------------------------------------*/


//	// Renders events onto the view and populates the View's segment array
//	renderEvents: function(events) {
//		var dayEvents = [];
//		var timedEvents = [];
//		var daySegs = [];
//		var timedSegs;
//		var i;

//		// separate the events into all-day and timed
//		for (i = 0; i < events.length; i++) {
//			if (events[i].allDay) {
//				dayEvents.push(events[i]);
//			}
//			else {
//				timedEvents.push(events[i]);
//			}
//		}

//		// render the events in the subcomponents
//		timedSegs = this.timeGrid.renderEvents(timedEvents);
//		if (this.dayGrid) {
//			daySegs = this.dayGrid.renderEvents(dayEvents);
//		}

//		// the all-day area is flexible and might have a lot of events, so shift the height
//		this.updateHeight();

//		View.prototype.renderEvents.call(this, events); // call the super-method
//	},


//	// Retrieves all segment objects that are rendered in the view
//	getSegs: function() {
//		return this.timeGrid.getSegs().concat(
//			this.dayGrid ? this.dayGrid.getSegs() : []
//		);
//	},


//	// Unrenders all event elements and clears internal segment data
//	destroyEvents: function() {
//		View.prototype.destroyEvents.call(this); // do this before the grids' segs have been cleared

//		// if destroyEvents is being called as part of an event rerender, renderEvents will be called shortly
//		// after, so remember what the scroll value was so we can restore it.
//		this.recordScroll();

//		// destroy the events in the subcomponents
//		this.timeGrid.destroyEvents();
//		if (this.dayGrid) {
//			this.dayGrid.destroyEvents();
//		}

//		// we DON'T need to call updateHeight() because:
//		// A) a renderEvents() call always happens after this, which will eventually call updateHeight()
//		// B) in IE8, this causes a flash whenever events are rerendered
//	},


//	/* Event Dragging
//	------------------------------------------------------------------------------------------------------------------*/


//	// Renders a visual indication of an event being dragged over the view.
//	// A returned value of `true` signals that a mock "helper" event has been rendered.
//	renderDrag: function(start, end, seg) {
//		if (start.hasTime()) {
//			return this.timeGrid.renderDrag(start, end, seg);
//		}
//		else if (this.dayGrid) {
//			return this.dayGrid.renderDrag(start, end, seg);
//		}
//	},


//	// Unrenders a visual indications of an event being dragged over the view
//	destroyDrag: function() {
//		this.timeGrid.destroyDrag();
//		if (this.dayGrid) {
//			this.dayGrid.destroyDrag();
//		}
//	},


//	/* Selection
//	------------------------------------------------------------------------------------------------------------------*/


//	// Renders a visual indication of a selection
//	renderSelection: function(start, end) {
//		if (start.hasTime() || end.hasTime()) {
//			this.timeGrid.renderSelection(start, end);
//		}
//		else if (this.dayGrid) {
//			this.dayGrid.renderSelection(start, end);
//		}
//	},


//	// Unrenders a visual indications of a selection
//	destroySelection: function() {
//		this.timeGrid.destroySelection();
//		if (this.dayGrid) {
//			this.dayGrid.destroySelection();
//		}
//	}

//});

//;;

///* A week view with an all-day cell area at the top, and a time grid below
//----------------------------------------------------------------------------------------------------------------------*/
//// TODO: a WeekView mixin for calculating dates and titles

//fcViews.agendaWeek = AgendaWeekView; // register the view

//function AgendaWeekView(calendar) {
//	AgendaView.call(this, calendar); // call the super-constructor
//}


//AgendaWeekView.prototype = createObject(AgendaView.prototype); // define the super-class
//$.extend(AgendaWeekView.prototype, {

//	name: 'agendaWeek',


//	incrementDate: function(date, delta) {
//		return date.clone().stripTime().add(delta, 'weeks').startOf('week');
//	},


//	render: function(date) {

//		this.intervalStart = date.clone().stripTime().startOf('week');
//		this.intervalEnd = this.intervalStart.clone().add(1, 'weeks');

//		this.start = this.skipHiddenDays(this.intervalStart);
//		this.end = this.skipHiddenDays(this.intervalEnd, -1, true);

//		this.title = this.calendar.formatRange(
//			this.start,
//			this.end.clone().subtract(1), // make inclusive by subtracting 1 ms
//			this.opt('titleFormat'),
//			' \u2014 ' // emphasized dash
//		);

//		AgendaView.prototype.render.call(this, this.getCellsPerWeek()); // call the super-method
//	}

//});

//;;

///* A day view with an all-day cell area at the top, and a time grid below
//----------------------------------------------------------------------------------------------------------------------*/

//fcViews.agendaDay = AgendaDayView; // register the view

//function AgendaDayView(calendar) {
//	AgendaView.call(this, calendar); // call the super-constructor
//}


//AgendaDayView.prototype = createObject(AgendaView.prototype); // define the super-class
//$.extend(AgendaDayView.prototype, {

//	name: 'agendaDay',


//	incrementDate: function(date, delta) {
//		var out = date.clone().stripTime().add(delta, 'days');
//		out = this.skipHiddenDays(out, delta < 0 ? -1 : 1);
//		return out;
//	},


//	render: function(date) {

//		this.start = this.intervalStart = date.clone().stripTime();
//		this.end = this.intervalEnd = this.start.clone().add(1, 'days');

//		this.title = this.calendar.formatDate(this.start, this.opt('titleFormat'));

//		AgendaView.prototype.render.call(this, 1); // call the super-method
//	}

//});

//;;

//});
/*!
 * FullCalendar v3.0.1
 * Docs & License: http://fullcalendar.io/
 * (c) 2016 Adam Shaw
 */
!function(t){"function"==typeof define&&define.amd?define(["jquery","moment"],t):"object"==typeof exports?module.exports=t(require("jquery"),require("moment")):t(jQuery,moment)}(function(t,e){function n(t){return q(t,qt)}function i(t,e){e.left&&t.css({"border-left-width":1,"margin-left":e.left-1}),e.right&&t.css({"border-right-width":1,"margin-right":e.right-1})}function r(t){t.css({"margin-left":"","margin-right":"","border-left-width":"","border-right-width":""})}function s(){t("body").addClass("fc-not-allowed")}function o(){t("body").removeClass("fc-not-allowed")}function l(e,n,i){var r=Math.floor(n/e.length),s=Math.floor(n-r*(e.length-1)),o=[],l=[],u=[],d=0;a(e),e.each(function(n,i){var a=n===e.length-1?s:r,c=t(i).outerHeight(!0);c<a?(o.push(i),l.push(c),u.push(t(i).height())):d+=c}),i&&(n-=d,r=Math.floor(n/o.length),s=Math.floor(n-r*(o.length-1))),t(o).each(function(e,n){var i=e===o.length-1?s:r,a=l[e],d=u[e],c=i-(a-d);a<i&&t(n).height(c)})}function a(t){t.height("")}function u(e){var n=0;return e.find("> *").each(function(e,i){var r=t(i).outerWidth();r>n&&(n=r)}),n++,e.width(n),n}function d(t,e){var n,i=t.add(e);return i.css({position:"relative",left:-1}),n=t.outerHeight()-e.outerHeight(),i.css({position:"",left:""}),n}function c(e){var n=e.css("position"),i=e.parents().filter(function(){var e=t(this);return/(auto|scroll)/.test(e.css("overflow")+e.css("overflow-y")+e.css("overflow-x"))}).eq(0);return"fixed"!==n&&i.length?i:t(e[0].ownerDocument||document)}function h(t,e){var n=t.offset(),i=n.left-(e?e.left:0),r=n.top-(e?e.top:0);return{left:i,right:i+t.outerWidth(),top:r,bottom:r+t.outerHeight()}}function f(t,e){var n=t.offset(),i=p(t),r=n.left+y(t,"border-left-width")+i.left-(e?e.left:0),s=n.top+y(t,"border-top-width")+i.top-(e?e.top:0);return{left:r,right:r+t[0].clientWidth,top:s,bottom:s+t[0].clientHeight}}function g(t,e){var n=t.offset(),i=n.left+y(t,"border-left-width")+y(t,"padding-left")-(e?e.left:0),r=n.top+y(t,"border-top-width")+y(t,"padding-top")-(e?e.top:0);return{left:i,right:i+t.width(),top:r,bottom:r+t.height()}}function p(t){var e=t.innerWidth()-t[0].clientWidth,n={left:0,right:0,top:0,bottom:t.innerHeight()-t[0].clientHeight};return v()&&"rtl"==t.css("direction")?n.left=e:n.right=e,n}function v(){return null===Zt&&(Zt=m()),Zt}function m(){var e=t("<div><div/></div>").css({position:"absolute",top:-1e3,left:0,border:0,padding:0,overflow:"scroll",direction:"rtl"}).appendTo("body"),n=e.children(),i=n.offset().left>e.offset().left;return e.remove(),i}function y(t,e){return parseFloat(t.css(e))||0}function S(t){return 1==t.which&&!t.ctrlKey}function w(t){if(void 0!==t.pageX)return t.pageX;var e=t.originalEvent.touches;return e?e[0].pageX:void 0}function E(t){if(void 0!==t.pageY)return t.pageY;var e=t.originalEvent.touches;return e?e[0].pageY:void 0}function D(t){return/^touch/.test(t.type)}function b(t){t.addClass("fc-unselectable").on("selectstart",C)}function C(t){t.preventDefault()}function H(t){return!!window.addEventListener&&(window.addEventListener("scroll",t,!0),!0)}function T(t){return!!window.removeEventListener&&(window.removeEventListener("scroll",t,!0),!0)}function x(t,e){var n={left:Math.max(t.left,e.left),right:Math.min(t.right,e.right),top:Math.max(t.top,e.top),bottom:Math.min(t.bottom,e.bottom)};return n.left<n.right&&n.top<n.bottom&&n}function R(t,e){return{left:Math.min(Math.max(t.left,e.left),e.right),top:Math.min(Math.max(t.top,e.top),e.bottom)}}function I(t){return{left:(t.left+t.right)/2,top:(t.top+t.bottom)/2}}function k(t,e){return{left:t.left-e.left,top:t.top-e.top}}function M(e){var n,i,r=[],s=[];for("string"==typeof e?s=e.split(/\s*,\s*/):"function"==typeof e?s=[e]:t.isArray(e)&&(s=e),n=0;n<s.length;n++)i=s[n],"string"==typeof i?r.push("-"==i.charAt(0)?{field:i.substring(1),order:-1}:{field:i,order:1}):"function"==typeof i&&r.push({func:i});return r}function L(t,e,n){var i,r;for(i=0;i<n.length;i++)if(r=B(t,e,n[i]))return r;return 0}function B(t,e,n){return n.func?n.func(t,e):z(t[n.field],e[n.field])*(n.order||1)}function z(e,n){return e||n?null==n?-1:null==e?1:"string"===t.type(e)||"string"===t.type(n)?String(e).localeCompare(String(n)):e-n:0}function F(t,e){var n,i,r,s,o=t.start,l=t.end,a=e.start,u=e.end;if(l>a&&o<u)return o>=a?(n=o.clone(),r=!0):(n=a.clone(),r=!1),l<=u?(i=l.clone(),s=!0):(i=u.clone(),s=!1),{start:n,end:i,isStart:r,isEnd:s}}function N(t,n){return e.duration({days:t.clone().stripTime().diff(n.clone().stripTime(),"days"),ms:t.time()-n.time()})}function G(t,n){return e.duration({days:t.clone().stripTime().diff(n.clone().stripTime(),"days")})}function A(t,n,i){return e.duration(Math.round(t.diff(n,i,!0)),i)}function O(t,e){var n,i,r;for(n=0;n<Xt.length&&(i=Xt[n],r=V(i,t,e),!(r>=1&&ot(r)));n++);return i}function V(t,n,i){return null!=i?i.diff(n,t,!0):e.isDuration(n)?n.as(t):n.end.diff(n.start,t,!0)}function P(t,e,n){var i;return W(n)?(e-t)/n:(i=n.asMonths(),Math.abs(i)>=1&&ot(i)?e.diff(t,"months",!0)/i:e.diff(t,"days",!0)/n.asDays())}function _(t,e){var n,i;return W(t)||W(e)?t/e:(n=t.asMonths(),i=e.asMonths(),Math.abs(n)>=1&&ot(n)&&Math.abs(i)>=1&&ot(i)?n/i:t.asDays()/e.asDays())}function Y(t,n){var i;return W(t)?e.duration(t*n):(i=t.asMonths(),Math.abs(i)>=1&&ot(i)?e.duration({months:i*n}):e.duration({days:t.asDays()*n}))}function W(t){return Boolean(t.hours()||t.minutes()||t.seconds()||t.milliseconds())}function j(t){return"[object Date]"===Object.prototype.toString.call(t)||t instanceof Date}function U(t){return/^\d+\:\d+(?:\:\d+\.?(?:\d{3})?)?$/.test(t)}function q(t,e){var n,i,r,s,o,l,a={};if(e)for(n=0;n<e.length;n++){for(i=e[n],r=[],s=t.length-1;s>=0;s--)if(o=t[s][i],"object"==typeof o)r.unshift(o);else if(void 0!==o){a[i]=o;break}r.length&&(a[i]=q(r))}for(n=t.length-1;n>=0;n--){l=t[n];for(i in l)i in a||(a[i]=l[i])}return a}function Z(t){var e=function(){};return e.prototype=t,new e}function $(t,e){for(var n in t)X(t,n)&&(e[n]=t[n])}function X(t,e){return Kt.call(t,e)}function K(e){return/undefined|null|boolean|number|string/.test(t.type(e))}function Q(e,n,i){if(t.isFunction(e)&&(e=[e]),e){var r,s;for(r=0;r<e.length;r++)s=e[r].apply(n,i)||s;return s}}function J(){for(var t=0;t<arguments.length;t++)if(void 0!==arguments[t])return arguments[t]}function tt(t){return(t+"").replace(/&/g,"&amp;").replace(/</g,"&lt;").replace(/>/g,"&gt;").replace(/'/g,"&#039;").replace(/"/g,"&quot;").replace(/\n/g,"<br />")}function et(t){return t.replace(/&.*?;/g,"")}function nt(e){var n=[];return t.each(e,function(t,e){null!=e&&n.push(t+":"+e)}),n.join(";")}function it(e){var n=[];return t.each(e,function(t,e){null!=e&&n.push(t+'="'+tt(e)+'"')}),n.join(" ")}function rt(t){return t.charAt(0).toUpperCase()+t.slice(1)}function st(t,e){return t-e}function ot(t){return t%1===0}function lt(t,e){var n=t[e];return function(){return n.apply(t,arguments)}}function at(t,e,n){var i,r,s,o,l,a=function(){var u=+new Date-o;u<e?i=setTimeout(a,e-u):(i=null,n||(l=t.apply(s,r),s=r=null))};return function(){s=this,r=arguments,o=+new Date;var u=n&&!i;return i||(i=setTimeout(a,e)),u&&(l=t.apply(s,r),s=r=null),l}}function ut(e,n){return e&&e.then&&"resolved"!==e.state()?n?e.then(n):void 0:t.when(n())}function dt(n,i,r){var s,o,l,a,u=n[0],d=1==n.length&&"string"==typeof u;return e.isMoment(u)||j(u)||void 0===u?a=e.apply(null,n):(s=!1,o=!1,d?Qt.test(u)?(u+="-01",n=[u],s=!0,o=!0):(l=Jt.exec(u))&&(s=!l[5],o=!0):t.isArray(u)&&(o=!0),a=i||s?e.utc.apply(e,n):e.apply(null,n),s?(a._ambigTime=!0,a._ambigZone=!0):r&&(o?a._ambigZone=!0:d&&a.utcOffset(u))),a._fullCalendar=!0,a}function ct(t,e){return ee.format.call(t,e)}function ht(t,e){return ft(t,yt(e))}function ft(t,e){var n,i="";for(n=0;n<e.length;n++)i+=gt(t,e[n]);return i}function gt(t,e){var n,i;return"string"==typeof e?e:(n=e.token)?ie[n]?ie[n](t):ct(t,n):e.maybe&&(i=ft(t,e.maybe),i.match(/[1-9]/))?i:""}function pt(t,e,n,i,r){var s;return t=jt.moment.parseZone(t),e=jt.moment.parseZone(e),s=t.localeData(),n=s.longDateFormat(n)||n,i=i||" - ",vt(t,e,yt(n),i,r)}function vt(t,e,n,i,r){var s,o,l,a,u=t.clone().stripZone(),d=e.clone().stripZone(),c="",h="",f="",g="",p="";for(o=0;o<n.length&&(s=mt(t,e,u,d,n[o]),s!==!1);o++)c+=s;for(l=n.length-1;l>o&&(s=mt(t,e,u,d,n[l]),s!==!1);l--)h=s+h;for(a=o;a<=l;a++)f+=gt(t,n[a]),g+=gt(e,n[a]);return(f||g)&&(p=r?g+i+f:f+i+g),c+p+h}function mt(t,e,n,i,r){var s,o;return"string"==typeof r?r:!!((s=r.token)&&(o=re[s.charAt(0)],o&&n.isSame(i,o)))&&ct(t,s)}function yt(t){return t in se?se[t]:se[t]=St(t)}function St(t){for(var e,n=[],i=/\[([^\]]*)\]|\(([^\)]*)\)|(LTS|LT|(\w)\4*o?)|([^\w\[\(]+)/g;e=i.exec(t);)e[1]?n.push(e[1]):e[2]?n.push({maybe:St(e[2])}):e[3]?n.push({token:e[3]}):e[5]&&n.push(e[5]);return n}function wt(){}function Et(t,e){var n;return X(e,"constructor")&&(n=e.constructor),"function"!=typeof n&&(n=e.constructor=function(){t.apply(this,arguments)}),n.prototype=Z(t.prototype),$(e,n.prototype),$(t,n),n}function Dt(t,e){$(e,t.prototype)}function bt(t,e){return!t&&!e||!(!t||!e)&&(t.component===e.component&&Ct(t,e)&&Ct(e,t))}function Ct(t,e){for(var n in t)if(!/^(component|left|right|top|bottom)$/.test(n)&&t[n]!==e[n])return!1;return!0}function Ht(t){return{start:t.start.clone(),end:t.end?t.end.clone():null,allDay:t.allDay}}function Tt(t){var e=Rt(t);return"background"===e||"inverse-background"===e}function xt(t){return"inverse-background"===Rt(t)}function Rt(t){return J((t.source||{}).rendering,t.rendering)}function It(t){var e,n,i={};for(e=0;e<t.length;e++)n=t[e],(i[n._id]||(i[n._id]=[])).push(n);return i}function kt(t,e){return t.start-e.start}function Mt(n){var i,r,s,o,l=jt.dataAttrPrefix;return l&&(l+="-"),i=n.data(l+"event")||null,i&&(i="object"==typeof i?t.extend({},i):{},r=i.start,null==r&&(r=i.time),s=i.duration,o=i.stick,delete i.start,delete i.time,delete i.duration,delete i.stick),null==r&&(r=n.data(l+"start")),null==r&&(r=n.data(l+"time")),null==s&&(s=n.data(l+"duration")),null==o&&(o=n.data(l+"stick")),r=null!=r?e.duration(r):null,s=null!=s?e.duration(s):null,o=Boolean(o),{eventProps:i,startTime:r,duration:s,stick:o}}function Lt(t,e){var n,i;for(n=0;n<e.length;n++)if(i=e[n],i.leftCol<=t.rightCol&&i.rightCol>=t.leftCol)return!0;return!1}function Bt(t,e){return t.leftCol-e.leftCol}function zt(t){var e,n,i,r=[];for(e=0;e<t.length;e++){for(n=t[e],i=0;i<r.length&&Gt(n,r[i]).length;i++);n.level=i,(r[i]||(r[i]=[])).push(n)}return r}function Ft(t){var e,n,i,r,s;for(e=0;e<t.length;e++)for(n=t[e],i=0;i<n.length;i++)for(r=n[i],r.forwardSegs=[],s=e+1;s<t.length;s++)Gt(r,t[s],r.forwardSegs)}function Nt(t){var e,n,i=t.forwardSegs,r=0;if(void 0===t.forwardPressure){for(e=0;e<i.length;e++)n=i[e],Nt(n),r=Math.max(r,1+n.forwardPressure);t.forwardPressure=r}}function Gt(t,e,n){n=n||[];for(var i=0;i<e.length;i++)At(t,e[i])&&n.push(e[i]);return n}function At(t,e){return t.bottom>e.top&&t.top<e.bottom}function Ot(n,i){function r(t){t._locale=U}function s(){$?u()&&(g(),d()):o()}function o(){n.addClass("fc"),n.on("click.fc","a[data-goto]",function(e){var n=t(this),i=n.data("goto"),r=j.moment(i.date),s=i.type,o=K.opt("navLink"+rt(s)+"Click");"function"==typeof o?o(r,e):("string"==typeof o&&(s=o),N(r,s))}),j.bindOption("theme",function(t){X=t?"ui":"fc",n.toggleClass("ui-widget",t),n.toggleClass("fc-unthemed",!t)}),j.bindOptions(["isRTL","locale"],function(t){n.toggleClass("fc-ltr",!t),n.toggleClass("fc-rtl",t)}),$=t("<div class='fc-view-container'/>").prependTo(n),q=j.header=new _t(j),l(),d(j.options.defaultView),j.options.handleWindowResize&&(J=at(m,j.options.windowResizeDelay),t(window).resize(J))}function l(){q.render(),q.el&&n.prepend(q.el)}function a(){K&&K.removeElement(),q.removeElement(),$.remove(),n.removeClass("fc fc-ltr fc-rtl fc-unthemed ui-widget"),n.off(".fc"),J&&t(window).unbind("resize",J)}function u(){return n.is(":visible")}function d(e,n){lt++,K&&e&&K.type!==e&&(A(),c()),!K&&e&&(K=j.view=ot[e]||(ot[e]=j.instantiateView(e)),K.setElement(t("<div class='fc-view fc-"+e+"-view' />").appendTo($)),q.activateButton(e)),K&&(tt=K.massageCurrentDate(tt),K.displaying&&tt>=K.intervalStart&&tt<K.intervalEnd||u()&&(K.display(tt,n),O(),H(),T(),E())),O(),lt--}function c(){q.deactivateButton(K.type),K.removeElement(),K=j.view=null}function h(){lt++,A();var t=K.type,e=K.queryScroll();c(),d(t,e),O(),lt--}function f(t){if(u())return t&&p(),lt++,K.updateSize(!0),lt--,!0}function g(){u()&&p()}function p(){var t=j.options.contentHeight,e=j.options.height;Q="number"==typeof t?t:"function"==typeof t?t():"number"==typeof e?e-v():"function"==typeof e?e()-v():"parent"===e?n.parent().height()-v():Math.round($.width()/Math.max(j.options.aspectRatio,.5))}function v(){return q.el?q.el.outerHeight(!0):0}function m(t){!lt&&t.target===window&&K.start&&f(!0)&&K.trigger("windowResize",st)}function y(){D()}function S(t){it(j.getEventSourcesByMatchArray(t))}function w(){u()&&(A(),K.displayEvents(ut),O())}function E(){!j.options.lazyFetching||et(K.start,K.end)?D():w()}function D(){nt(K.start,K.end)}function b(t){ut=t,w()}function C(){w()}function H(){q.updateTitle(K.title)}function T(){var t=j.getNow();t>=K.intervalStart&&t<K.intervalEnd?q.disableButton("today"):q.enableButton("today")}function x(t,e){K.select(j.buildSelectSpan.apply(j,arguments))}function R(){K&&K.unselect()}function I(){tt=K.computePrevDate(tt),d()}function k(){tt=K.computeNextDate(tt),d()}function M(){tt.add(-1,"years"),d()}function L(){tt.add(1,"years"),d()}function B(){tt=j.getNow(),d()}function z(t){tt=j.moment(t).stripZone(),d()}function F(t){tt.add(e.duration(t)),d()}function N(t,e){var n;e=e||"day",n=j.getViewSpec(e)||j.getUnitViewSpec(e),tt=t.clone(),d(n?n.type:null)}function G(){return j.applyTimezone(tt)}function A(){$.css({width:"100%",height:$.height(),overflow:"hidden"})}function O(){$.css({width:"",height:"",overflow:""})}function V(){return j}function P(){return K}function _(t,e){var n;if("string"==typeof t){if(void 0===e)return j.options[t];n={},n[t]=e,Y(n)}else"object"==typeof t&&Y(t)}function Y(t){var e,n=0;for(e in t)j.dynamicOverrides[e]=t[e];j.viewSpecCache={},j.populateOptionsHash();for(e in t)j.triggerOptionHandlers(e),n++;if(1===n){if("height"===e||"contentHeight"===e||"aspectRatio"===e)return void f(!0);if("defaultDate"===e)return;if("businessHours"===e)return void(K&&(K.unrenderBusinessHours(),K.renderBusinessHours()));if("timezone"===e)return j.rezoneArrayEventSources(),void y()}l(),ot={},h()}function W(t,e){var n=Array.prototype.slice.call(arguments,2);if(e=e||st,this.triggerWith(t,e,n),j.options[t])return j.options[t].apply(e,n)}var j=this;j.render=s,j.destroy=a,j.refetchEvents=y,j.refetchEventSources=S,j.reportEvents=b,j.reportEventChange=C,j.rerenderEvents=w,j.changeView=d,j.select=x,j.unselect=R,j.prev=I,j.next=k,j.prevYear=M,j.nextYear=L,j.today=B,j.gotoDate=z,j.incrementDate=F,j.zoomTo=N,j.getDate=G,j.getCalendar=V,j.getView=P,j.option=_,j.trigger=W,j.dynamicOverrides={},j.viewSpecCache={},j.optionHandlers={},j.overrides=t.extend({},i),j.populateOptionsHash();var U;j.bindOptions(["locale","monthNames","monthNamesShort","dayNames","dayNamesShort","firstDay","weekNumberCalculation"],function(t,e,n,i,s,o,l){if("iso"===l&&(l="ISO"),U=Z(Pt(t)),e&&(U._months=e),n&&(U._monthsShort=n),i&&(U._weekdays=i),s&&(U._weekdaysShort=s),null==o&&"ISO"===l&&(o=1),null!=o){var a=Z(U._week);a.dow=o,U._week=a}"ISO"!==l&&"local"!==l&&"function"!=typeof l||(U._fullCalendar_weekCalc=l),tt&&r(tt)}),j.defaultAllDayEventDuration=e.duration(j.options.defaultAllDayEventDuration),j.defaultTimedEventDuration=e.duration(j.options.defaultTimedEventDuration),j.moment=function(){var t;return"local"===j.options.timezone?(t=jt.moment.apply(null,arguments),t.hasTime()&&t.local()):t="UTC"===j.options.timezone?jt.moment.utc.apply(null,arguments):jt.moment.parseZone.apply(null,arguments),r(t),t},j.localizeMoment=r,j.getIsAmbigTimezone=function(){return"local"!==j.options.timezone&&"UTC"!==j.options.timezone},j.applyTimezone=function(t){if(!t.hasTime())return t.clone();var e,n=j.moment(t.toArray()),i=t.time()-n.time();return i&&(e=n.clone().add(i),t.time()-e.time()===0&&(n=e)),n},j.getNow=function(){var t=j.options.now;return"function"==typeof t&&(t=t()),j.moment(t).stripZone()},j.getEventEnd=function(t){return t.end?t.end.clone():j.getDefaultEventEnd(t.allDay,t.start)},j.getDefaultEventEnd=function(t,e){var n=e.clone();return t?n.stripTime().add(j.defaultAllDayEventDuration):n.add(j.defaultTimedEventDuration),j.getIsAmbigTimezone()&&n.stripZone(),n},j.humanizeDuration=function(t){return t.locale(j.options.locale).humanize()},Yt.call(j);var q,$,X,K,Q,J,tt,et=j.isFetchNeeded,nt=j.fetchEvents,it=j.fetchEventSources,st=n[0],ot={},lt=0,ut=[];tt=null!=j.options.defaultDate?j.moment(j.options.defaultDate).stripZone():j.getNow(),j.getSuggestedViewHeight=function(){return void 0===Q&&g(),Q},j.isHeightAuto=function(){return"auto"===j.options.contentHeight||"auto"===j.options.height},j.freezeContentHeight=A,j.unfreezeContentHeight=O,j.initialize()}function Vt(e){t.each(He,function(t,n){null==e[t]&&(e[t]=n(e))})}function Pt(t){return e.localeData(t)||e.localeData("en")}function _t(e){function n(){var n=e.options,s=n.header;f=n.theme?"ui":"fc",s?(h?h.empty():h=this.el=t("<div class='fc-toolbar'/>"),h.append(r("left")).append(r("right")).append(r("center")).append('<div class="fc-clear"/>')):i()}function i(){h&&(h.remove(),h=c.el=null)}function r(n){var i=t('<div class="fc-'+n+'"/>'),r=e.options,s=r.header[n];return s&&t.each(s.split(" "),function(n){var s,o=t(),l=!0;t.each(this.split(","),function(n,i){var s,a,u,d,c,h,p,v,m,y;"title"==i?(o=o.add(t("<h2>&nbsp;</h2>")),l=!1):((s=(r.customButtons||{})[i])?(u=function(t){s.click&&s.click.call(y[0],t)},d="",c=s.text):(a=e.getViewSpec(i))?(u=function(){e.changeView(i)},g.push(i),d=a.buttonTextOverride,c=a.buttonTextDefault):e[i]&&(u=function(){e[i]()},d=(e.overrides.buttonText||{})[i],c=r.buttonText[i]),u&&(h=s?s.themeIcon:r.themeButtonIcons[i],p=s?s.icon:r.buttonIcons[i],v=d?tt(d):h&&r.theme?"<span class='ui-icon ui-icon-"+h+"'></span>":p&&!r.theme?"<span class='fc-icon fc-icon-"+p+"'></span>":tt(c),m=["fc-"+i+"-button",f+"-button",f+"-state-default"],y=t('<button type="button" class="'+m.join(" ")+'">'+v+"</button>").click(function(t){y.hasClass(f+"-state-disabled")||(u(t),(y.hasClass(f+"-state-active")||y.hasClass(f+"-state-disabled"))&&y.removeClass(f+"-state-hover"))}).mousedown(function(){y.not("."+f+"-state-active").not("."+f+"-state-disabled").addClass(f+"-state-down")}).mouseup(function(){y.removeClass(f+"-state-down")}).hover(function(){y.not("."+f+"-state-active").not("."+f+"-state-disabled").addClass(f+"-state-hover")},function(){y.removeClass(f+"-state-hover").removeClass(f+"-state-down")}),o=o.add(y)))}),l&&o.first().addClass(f+"-corner-left").end().last().addClass(f+"-corner-right").end(),o.length>1?(s=t("<div/>"),l&&s.addClass("fc-button-group"),s.append(o),i.append(s)):i.append(o)}),i}function s(t){h&&h.find("h2").text(t)}function o(t){h&&h.find(".fc-"+t+"-button").addClass(f+"-state-active")}function l(t){h&&h.find(".fc-"+t+"-button").removeClass(f+"-state-active")}function a(t){h&&h.find(".fc-"+t+"-button").prop("disabled",!0).addClass(f+"-state-disabled")}function u(t){h&&h.find(".fc-"+t+"-button").prop("disabled",!1).removeClass(f+"-state-disabled")}function d(){return g}var c=this;c.render=n,c.removeElement=i,c.updateTitle=s,c.activateButton=o,c.deactivateButton=l,c.disableButton=a,c.enableButton=u,c.getViewsWithButtons=d,c.el=null;var h,f,g=[]}function Yt(){function n(t,e){return!O||t<O||e>V}function i(t,e){O=t,V=e,r(Y,"reset")}function r(t,e){var n,i;for("reset"===e?j=[]:"add"!==e&&(j=w(j,t)),n=0;n<t.length;n++)i=t[n],"pending"!==i._status&&W++,i._fetchId=(i._fetchId||0)+1,i._status="pending";for(n=0;n<t.length;n++)i=t[n],s(i,i._fetchId)}function s(e,n){a(e,function(i){var r,s,o,a=t.isArray(e.events);if(n===e._fetchId&&"rejected"!==e._status){if(e._status="resolved",i)for(r=0;r<i.length;r++)s=i[r],o=a?s:R(s,e),o&&j.push.apply(j,L(o));l()}})}function o(t){var e="pending"===t._status;t._status="rejected",e&&l()}function l(){W--,W||P(j)}function a(e,n){var i,r,s=jt.sourceFetchers;for(i=0;i<s.length;i++){if(r=s[i].call(F,e,O.clone(),V.clone(),F.options.timezone,n),r===!0)return;if("object"==typeof r)return void a(r,n)}var o=e.events;if(o)t.isFunction(o)?(F.pushLoading(),o.call(F,O.clone(),V.clone(),F.options.timezone,function(t){n(t),F.popLoading()})):t.isArray(o)?n(o):n();else{var l=e.url;if(l){var u,d=e.success,c=e.error,h=e.complete;u=t.isFunction(e.data)?e.data():e.data;var f=t.extend({},u||{}),g=J(e.startParam,F.options.startParam),p=J(e.endParam,F.options.endParam),v=J(e.timezoneParam,F.options.timezoneParam);g&&(f[g]=O.format()),p&&(f[p]=V.format()),F.options.timezone&&"local"!=F.options.timezone&&(f[v]=F.options.timezone),F.pushLoading(),t.ajax(t.extend({},Te,e,{data:f,success:function(e){e=e||[];var i=Q(d,this,arguments);t.isArray(i)&&(e=i),n(e)},error:function(){Q(c,this,arguments),n()},complete:function(){Q(h,this,arguments),F.popLoading()}}))}else n()}}function u(t){var e=d(t);e&&(Y.push(e),r([e],"add"))}function d(e){var n,i,r=jt.sourceNormalizers;if(t.isFunction(e)||t.isArray(e)?n={events:e}:"string"==typeof e?n={url:e}:"object"==typeof e&&(n=t.extend({},e)),n){for(n.className?"string"==typeof n.className&&(n.className=n.className.split(/\s+/)):n.className=[],t.isArray(n.events)&&(n.origArray=n.events,n.events=t.map(n.events,function(t){return R(t,n)})),i=0;i<r.length;i++)r[i].call(F,n);return n}}function c(t){f(m(t))}function h(t){null==t?f(Y,!0):f(v(t))}function f(e,n){var i;for(i=0;i<e.length;i++)o(e[i]);n?(Y=[],j=[]):(Y=t.grep(Y,function(t){for(i=0;i<e.length;i++)if(t===e[i])return!1;return!0}),j=w(j,e)),P(j)}function g(){return Y.slice(1)}function p(e){return t.grep(Y,function(t){return t.id&&t.id===e})[0]}function v(e){e?t.isArray(e)||(e=[e]):e=[];var n,i=[];for(n=0;n<e.length;n++)i.push.apply(i,m(e[n]));return i}function m(e){var n,i;for(n=0;n<Y.length;n++)if(i=Y[n],i===e)return[i];return i=p(e),i?[i]:t.grep(Y,function(t){return y(e,t)})}function y(t,e){return t&&e&&S(t)==S(e)}function S(t){return("object"==typeof t?t.origArray||t.googleCalendarId||t.url||t.events:null)||t}function w(e,n){return t.grep(e,function(t){for(var e=0;e<n.length;e++)if(t.source===n[e])return!1;return!0})}function E(t){t.start=F.moment(t.start),t.end?t.end=F.moment(t.end):t.end=null,B(t,D(t)),P(j)}function D(e){var n={};return t.each(e,function(t,e){b(t)&&void 0!==e&&K(e)&&(n[t]=e)}),n}function b(t){return!/^_|^(id|allDay|start|end)$/.test(t)}function C(t,e){var n,i,r,s=R(t);if(s){for(n=L(s),i=0;i<n.length;i++)r=n[i],r.source||(e&&(_.events.push(r),r.source=_),j.push(r));return P(j),n}return[]}function H(e){var n,i;for(null==e?e=function(){return!0}:t.isFunction(e)||(n=e+"",e=function(t){return t._id==n}),j=t.grep(j,e,!0),i=0;i<Y.length;i++)t.isArray(Y[i].events)&&(Y[i].events=t.grep(Y[i].events,e,!0));P(j)}function T(e){return t.isFunction(e)?t.grep(j,e):null!=e?(e+="",t.grep(j,function(t){return t._id==e})):j}function x(t){t.start=F.moment(t.start),t.end&&(t.end=F.moment(t.end)),Wt(t)}function R(n,i){var r,s,o,l={};if(F.options.eventDataTransform&&(n=F.options.eventDataTransform(n)),i&&i.eventDataTransform&&(n=i.eventDataTransform(n)),t.extend(l,n),i&&(l.source=i),l._id=n._id||(void 0===n.id?"_fc"+xe++:n.id+""),n.className?"string"==typeof n.className?l.className=n.className.split(/\s+/):l.className=n.className:l.className=[],r=n.start||n.date,s=n.end,U(r)&&(r=e.duration(r)),U(s)&&(s=e.duration(s)),n.dow||e.isDuration(r)||e.isDuration(s))l.start=r?e.duration(r):null,l.end=s?e.duration(s):null,l._recurring=!0;else{if(r&&(r=F.moment(r),!r.isValid()))return!1;s&&(s=F.moment(s),s.isValid()||(s=null)),o=n.allDay,void 0===o&&(o=J(i?i.allDayDefault:void 0,F.options.allDayDefault)),I(r,s,o,l)}return F.normalizeEvent(l),l}function I(t,e,n,i){i.start=t,i.end=e,i.allDay=n,k(i),Wt(i)}function k(t){M(t),t.end&&!t.end.isAfter(t.start)&&(t.end=null),t.end||(F.options.forceEventDuration?t.end=F.getDefaultEventEnd(t.allDay,t.start):t.end=null)}function M(t){null==t.allDay&&(t.allDay=!(t.start.hasTime()||t.end&&t.end.hasTime())),t.allDay?(t.start.stripTime(),t.end&&t.end.stripTime()):(t.start.hasTime()||(t.start=F.applyTimezone(t.start.time(0))),t.end&&!t.end.hasTime()&&(t.end=F.applyTimezone(t.end.time(0))))}function L(e,n,i){var r,s,o,l,a,u,d,c,h,f=[];if(n=n||O,i=i||V,e)if(e._recurring){if(s=e.dow)for(r={},o=0;o<s.length;o++)r[s[o]]=!0;for(l=n.clone().stripTime();l.isBefore(i);)r&&!r[l.day()]||(a=e.start,u=e.end,d=l.clone(),c=null,a&&(d=d.time(a)),u&&(c=l.clone().time(u)),h=t.extend({},e),I(d,c,!a&&!u,h),f.push(h)),l.add(1,"days")}else f.push(e);return f}function B(e,n,i){function r(t,e){return i?A(t,e,i):n.allDay?G(t,e):N(t,e)}var s,o,l,a,u,d,c={};return n=n||{},n.start||(n.start=e.start.clone()),void 0===n.end&&(n.end=e.end?e.end.clone():null),null==n.allDay&&(n.allDay=e.allDay),k(n),s={start:e._start.clone(),end:e._end?e._end.clone():F.getDefaultEventEnd(e._allDay,e._start),allDay:n.allDay},k(s),o=null!==e._end&&null===n.end,l=r(n.start,s.start),n.end?(a=r(n.end,s.end),u=a.subtract(l)):u=null,t.each(n,function(t,e){b(t)&&void 0!==e&&(c[t]=e)}),d=z(T(e._id),o,n.allDay,l,u,c),{dateDelta:l,durationDelta:u,undo:d}}function z(e,n,i,r,s,o){var l=F.getIsAmbigTimezone(),a=[];return r&&!r.valueOf()&&(r=null),s&&!s.valueOf()&&(s=null),t.each(e,function(e,u){var d,c;d={start:u.start.clone(),end:u.end?u.end.clone():null,allDay:u.allDay},t.each(o,function(t){d[t]=u[t]}),c={start:u._start,end:u._end,allDay:i},k(c),n?c.end=null:s&&!c.end&&(c.end=F.getDefaultEventEnd(c.allDay,c.start)),r&&(c.start.add(r),c.end&&c.end.add(r)),s&&c.end.add(s),l&&!c.allDay&&(r||s)&&(c.start.stripZone(),c.end&&c.end.stripZone()),t.extend(u,o,c),Wt(u),a.push(function(){t.extend(u,d),Wt(u)})}),function(){for(var t=0;t<a.length;t++)a[t]()}}var F=this;F.isFetchNeeded=n,F.fetchEvents=i,F.fetchEventSources=r,F.getEventSources=g,F.getEventSourceById=p,F.getEventSourcesByMatchArray=v,F.getEventSourcesByMatch=m,F.addEventSource=u,F.removeEventSource=c,F.removeEventSources=h,F.updateEvent=E,F.renderEvent=C,F.removeEvents=H,F.clientEvents=T,F.mutateEvent=B,F.normalizeEventDates=k,F.normalizeEventTimes=M;var O,V,P=F.reportEvents,_={events:[]},Y=[_],W=0,j=[];t.each((F.options.events?[F.options.events]:[]).concat(F.options.eventSources||[]),function(t,e){var n=d(e);n&&Y.push(n)}),F.rezoneArrayEventSources=function(){var e,n,i;for(e=0;e<Y.length;e++)if(n=Y[e].events,t.isArray(n))for(i=0;i<n.length;i++)x(n[i])},F.buildEventFromInput=R,F.expandEvent=L,F.getEventCache=function(){return j}}function Wt(t){t._allDay=t.allDay,t._start=t.start.clone(),t._end=t.end?t.end.clone():null}var jt=t.fullCalendar={version:"3.0.1",internalApiVersion:6},Ut=jt.views={};t.fn.fullCalendar=function(e){var n=Array.prototype.slice.call(arguments,1),i=this;return this.each(function(r,s){var o,l=t(s),a=l.data("fullCalendar");"string"==typeof e?a&&t.isFunction(a[e])&&(o=a[e].apply(a,n),r||(i=o),"destroy"===e&&l.removeData("fullCalendar")):a||(a=new Ee(l,e),l.data("fullCalendar",a),a.render())}),i};var qt=["header","buttonText","buttonIcons","themeButtonIcons"];jt.intersectRanges=F,jt.applyAll=Q,jt.debounce=at,jt.isInt=ot,jt.htmlEscape=tt,jt.cssToStr=nt,jt.proxy=lt,jt.capitaliseFirstLetter=rt,jt.getOuterRect=h,jt.getClientRect=f,jt.getContentRect=g,jt.getScrollbarWidths=p;var Zt=null;jt.preventDefault=C,jt.intersectRects=x,jt.parseFieldSpecs=M,jt.compareByFieldSpecs=L,jt.compareByFieldSpec=B,jt.flexibleCompare=z,jt.computeIntervalUnit=O,jt.divideRangeByDuration=P,jt.divideDurationByDuration=_,jt.multiplyDuration=Y,jt.durationHasTime=W;var $t=["sun","mon","tue","wed","thu","fri","sat"],Xt=["year","month","week","day","hour","minute","second","millisecond"];jt.log=function(){var t=window.console;if(t&&t.log)return t.log.apply(t,arguments)},jt.warn=function(){var t=window.console;return t&&t.warn?t.warn.apply(t,arguments):jt.log.apply(jt,arguments)};var Kt={}.hasOwnProperty,Qt=/^\s*\d{4}-\d\d$/,Jt=/^\s*\d{4}-(?:(\d\d-\d\d)|(W\d\d$)|(W\d\d-\d)|(\d\d\d))((T| )(\d\d(:\d\d(:\d\d(\.\d+)?)?)?)?)?$/,te=e.fn,ee=t.extend({},te),ne=e.momentProperties;ne.push("_fullCalendar"),ne.push("_ambigTime"),ne.push("_ambigZone"),jt.moment=function(){return dt(arguments)},jt.moment.utc=function(){var t=dt(arguments,!0);return t.hasTime()&&t.utc(),t},jt.moment.parseZone=function(){return dt(arguments,!0,!0)},te.week=te.weeks=function(t){var e=this._locale._fullCalendar_weekCalc;return null==t&&"function"==typeof e?e(this):"ISO"===e?ee.isoWeek.apply(this,arguments):ee.week.apply(this,arguments)},te.time=function(t){if(!this._fullCalendar)return ee.time.apply(this,arguments);if(null==t)return e.duration({hours:this.hours(),minutes:this.minutes(),seconds:this.seconds(),milliseconds:this.milliseconds()});this._ambigTime=!1,e.isDuration(t)||e.isMoment(t)||(t=e.duration(t));var n=0;return e.isDuration(t)&&(n=24*Math.floor(t.asDays())),this.hours(n+t.hours()).minutes(t.minutes()).seconds(t.seconds()).milliseconds(t.milliseconds())},te.stripTime=function(){return this._ambigTime||(this.utc(!0),this.set({hours:0,minutes:0,seconds:0,ms:0}),this._ambigTime=!0,this._ambigZone=!0),this},te.hasTime=function(){return!this._ambigTime},te.stripZone=function(){var t;return this._ambigZone||(t=this._ambigTime,this.utc(!0),this._ambigTime=t||!1,this._ambigZone=!0),this},te.hasZone=function(){return!this._ambigZone},te.local=function(t){return ee.local.call(this,this._ambigZone||t),this._ambigTime=!1,this._ambigZone=!1,this},te.utc=function(t){return ee.utc.call(this,t),this._ambigTime=!1,this._ambigZone=!1,this},te.utcOffset=function(t){return null!=t&&(this._ambigTime=!1,this._ambigZone=!1),ee.utcOffset.apply(this,arguments)},te.format=function(){return this._fullCalendar&&arguments[0]?ht(this,arguments[0]):this._ambigTime?ct(this,"YYYY-MM-DD"):this._ambigZone?ct(this,"YYYY-MM-DD[T]HH:mm:ss"):ee.format.apply(this,arguments)},te.toISOString=function(){return this._ambigTime?ct(this,"YYYY-MM-DD"):this._ambigZone?ct(this,"YYYY-MM-DD[T]HH:mm:ss"):ee.toISOString.apply(this,arguments)};var ie={t:function(t){return ct(t,"a").charAt(0)},T:function(t){return ct(t,"A").charAt(0)}};jt.formatRange=pt;var re={Y:"year",M:"month",D:"day",d:"day",A:"second",a:"second",T:"second",t:"second",H:"second",h:"second",m:"second",s:"second"},se={},oe={Y:{value:1,unit:"year"},M:{value:2,unit:"month"},W:{value:3,unit:"week"},w:{value:3,unit:"week"},D:{value:4,unit:"day"},d:{value:4,unit:"day"}};jt.queryMostGranularFormatUnit=function(t){var e,n,i,r,s=yt(t);for(e=0;e<s.length;e++)n=s[e],n.token&&(i=oe[n.token.charAt(0)],i&&(!r||i.value>r.value)&&(r=i));return r?r.unit:null},jt.Class=wt,wt.extend=function(){var t,e,n=arguments.length;for(t=0;t<n;t++)e=arguments[t],t<n-1&&Dt(this,e);return Et(this,e||{})},wt.mixin=function(t){Dt(this,t)};var le=jt.EmitterMixin={on:function(e,n){var i=function(t,e){return n.apply(e.context||this,e.args||[])};return n.guid||(n.guid=t.guid++),i.guid=n.guid,t(this).on(e,i),this},off:function(e,n){return t(this).off(e,n),this},trigger:function(e){var n=Array.prototype.slice.call(arguments,1);return t(this).triggerHandler(e,{args:n}),this},triggerWith:function(e,n,i){return t(this).triggerHandler(e,{context:n,args:i}),this}},ae=jt.ListenerMixin=function(){var e=0,n={listenerId:null,listenTo:function(e,n,i){if("object"==typeof n)for(var r in n)n.hasOwnProperty(r)&&this.listenTo(e,r,n[r]);else"string"==typeof n&&e.on(n+"."+this.getListenerNamespace(),t.proxy(i,this))},stopListeningTo:function(t,e){t.off((e||"")+"."+this.getListenerNamespace())},getListenerNamespace:function(){return null==this.listenerId&&(this.listenerId=e++),"_listener"+this.listenerId}};return n}(),ue={isIgnoringMouse:!1,delayUnignoreMouse:null,initMouseIgnoring:function(t){this.delayUnignoreMouse=at(lt(this,"unignoreMouse"),t||1e3)},tempIgnoreMouse:function(){this.isIgnoringMouse=!0,this.delayUnignoreMouse()},unignoreMouse:function(){this.isIgnoringMouse=!1}},de=wt.extend(ae,{isHidden:!0,options:null,el:null,margin:10,constructor:function(t){this.options=t||{}},show:function(){
this.isHidden&&(this.el||this.render(),this.el.show(),this.position(),this.isHidden=!1,this.trigger("show"))},hide:function(){this.isHidden||(this.el.hide(),this.isHidden=!0,this.trigger("hide"))},render:function(){var e=this,n=this.options;this.el=t('<div class="fc-popover"/>').addClass(n.className||"").css({top:0,left:0}).append(n.content).appendTo(n.parentEl),this.el.on("click",".fc-close",function(){e.hide()}),n.autoHide&&this.listenTo(t(document),"mousedown",this.documentMousedown)},documentMousedown:function(e){this.el&&!t(e.target).closest(this.el).length&&this.hide()},removeElement:function(){this.hide(),this.el&&(this.el.remove(),this.el=null),this.stopListeningTo(t(document),"mousedown")},position:function(){var e,n,i,r,s,o=this.options,l=this.el.offsetParent().offset(),a=this.el.outerWidth(),u=this.el.outerHeight(),d=t(window),h=c(this.el);r=o.top||0,s=void 0!==o.left?o.left:void 0!==o.right?o.right-a:0,h.is(window)||h.is(document)?(h=d,e=0,n=0):(i=h.offset(),e=i.top,n=i.left),e+=d.scrollTop(),n+=d.scrollLeft(),o.viewportConstrain!==!1&&(r=Math.min(r,e+h.outerHeight()-u-this.margin),r=Math.max(r,e+this.margin),s=Math.min(s,n+h.outerWidth()-a-this.margin),s=Math.max(s,n+this.margin)),this.el.css({top:r-l.top,left:s-l.left})},trigger:function(t){this.options[t]&&this.options[t].apply(this,Array.prototype.slice.call(arguments,1))}}),ce=jt.CoordCache=wt.extend({els:null,forcedOffsetParentEl:null,origin:null,boundingRect:null,isHorizontal:!1,isVertical:!1,lefts:null,rights:null,tops:null,bottoms:null,constructor:function(e){this.els=t(e.els),this.isHorizontal=e.isHorizontal,this.isVertical=e.isVertical,this.forcedOffsetParentEl=e.offsetParent?t(e.offsetParent):null},build:function(){var t=this.forcedOffsetParentEl||this.els.eq(0).offsetParent();this.origin=t.offset(),this.boundingRect=this.queryBoundingRect(),this.isHorizontal&&this.buildElHorizontals(),this.isVertical&&this.buildElVerticals()},clear:function(){this.origin=null,this.boundingRect=null,this.lefts=null,this.rights=null,this.tops=null,this.bottoms=null},ensureBuilt:function(){this.origin||this.build()},buildElHorizontals:function(){var e=[],n=[];this.els.each(function(i,r){var s=t(r),o=s.offset().left,l=s.outerWidth();e.push(o),n.push(o+l)}),this.lefts=e,this.rights=n},buildElVerticals:function(){var e=[],n=[];this.els.each(function(i,r){var s=t(r),o=s.offset().top,l=s.outerHeight();e.push(o),n.push(o+l)}),this.tops=e,this.bottoms=n},getHorizontalIndex:function(t){this.ensureBuilt();var e,n=this.lefts,i=this.rights,r=n.length;for(e=0;e<r;e++)if(t>=n[e]&&t<i[e])return e},getVerticalIndex:function(t){this.ensureBuilt();var e,n=this.tops,i=this.bottoms,r=n.length;for(e=0;e<r;e++)if(t>=n[e]&&t<i[e])return e},getLeftOffset:function(t){return this.ensureBuilt(),this.lefts[t]},getLeftPosition:function(t){return this.ensureBuilt(),this.lefts[t]-this.origin.left},getRightOffset:function(t){return this.ensureBuilt(),this.rights[t]},getRightPosition:function(t){return this.ensureBuilt(),this.rights[t]-this.origin.left},getWidth:function(t){return this.ensureBuilt(),this.rights[t]-this.lefts[t]},getTopOffset:function(t){return this.ensureBuilt(),this.tops[t]},getTopPosition:function(t){return this.ensureBuilt(),this.tops[t]-this.origin.top},getBottomOffset:function(t){return this.ensureBuilt(),this.bottoms[t]},getBottomPosition:function(t){return this.ensureBuilt(),this.bottoms[t]-this.origin.top},getHeight:function(t){return this.ensureBuilt(),this.bottoms[t]-this.tops[t]},queryBoundingRect:function(){var t=c(this.els.eq(0));if(!t.is(document))return f(t)},isPointInBounds:function(t,e){return this.isLeftInBounds(t)&&this.isTopInBounds(e)},isLeftInBounds:function(t){return!this.boundingRect||t>=this.boundingRect.left&&t<this.boundingRect.right},isTopInBounds:function(t){return!this.boundingRect||t>=this.boundingRect.top&&t<this.boundingRect.bottom}}),he=jt.DragListener=wt.extend(ae,ue,{options:null,subjectEl:null,originX:null,originY:null,scrollEl:null,isInteracting:!1,isDistanceSurpassed:!1,isDelayEnded:!1,isDragging:!1,isTouch:!1,delay:null,delayTimeoutId:null,minDistance:null,handleTouchScrollProxy:null,constructor:function(t){this.options=t||{},this.handleTouchScrollProxy=lt(this,"handleTouchScroll"),this.initMouseIgnoring(500)},startInteraction:function(e,n){var i=D(e);if("mousedown"===e.type){if(this.isIgnoringMouse)return;if(!S(e))return;e.preventDefault()}this.isInteracting||(n=n||{},this.delay=J(n.delay,this.options.delay,0),this.minDistance=J(n.distance,this.options.distance,0),this.subjectEl=this.options.subjectEl,this.isInteracting=!0,this.isTouch=i,this.isDelayEnded=!1,this.isDistanceSurpassed=!1,this.originX=w(e),this.originY=E(e),this.scrollEl=c(t(e.target)),this.bindHandlers(),this.initAutoScroll(),this.handleInteractionStart(e),this.startDelay(e),this.minDistance||this.handleDistanceSurpassed(e))},handleInteractionStart:function(t){this.trigger("interactionStart",t)},endInteraction:function(t,e){this.isInteracting&&(this.endDrag(t),this.delayTimeoutId&&(clearTimeout(this.delayTimeoutId),this.delayTimeoutId=null),this.destroyAutoScroll(),this.unbindHandlers(),this.isInteracting=!1,this.handleInteractionEnd(t,e),this.isTouch&&this.tempIgnoreMouse())},handleInteractionEnd:function(t,e){this.trigger("interactionEnd",t,e||!1)},bindHandlers:function(){var e=this,n=1;this.isTouch?(this.listenTo(t(document),{touchmove:this.handleTouchMove,touchend:this.endInteraction,touchcancel:this.endInteraction,touchstart:function(t){n?n--:e.endInteraction(t,!0)}}),!H(this.handleTouchScrollProxy)&&this.scrollEl&&this.listenTo(this.scrollEl,"scroll",this.handleTouchScroll)):this.listenTo(t(document),{mousemove:this.handleMouseMove,mouseup:this.endInteraction}),this.listenTo(t(document),{selectstart:C,contextmenu:C})},unbindHandlers:function(){this.stopListeningTo(t(document)),T(this.handleTouchScrollProxy),this.scrollEl&&this.stopListeningTo(this.scrollEl,"scroll")},startDrag:function(t,e){this.startInteraction(t,e),this.isDragging||(this.isDragging=!0,this.handleDragStart(t))},handleDragStart:function(t){this.trigger("dragStart",t)},handleMove:function(t){var e,n=w(t)-this.originX,i=E(t)-this.originY,r=this.minDistance;this.isDistanceSurpassed||(e=n*n+i*i,e>=r*r&&this.handleDistanceSurpassed(t)),this.isDragging&&this.handleDrag(n,i,t)},handleDrag:function(t,e,n){this.trigger("drag",t,e,n),this.updateAutoScroll(n)},endDrag:function(t){this.isDragging&&(this.isDragging=!1,this.handleDragEnd(t))},handleDragEnd:function(t){this.trigger("dragEnd",t)},startDelay:function(t){var e=this;this.delay?this.delayTimeoutId=setTimeout(function(){e.handleDelayEnd(t)},this.delay):this.handleDelayEnd(t)},handleDelayEnd:function(t){this.isDelayEnded=!0,this.isDistanceSurpassed&&this.startDrag(t)},handleDistanceSurpassed:function(t){this.isDistanceSurpassed=!0,this.isDelayEnded&&this.startDrag(t)},handleTouchMove:function(t){this.isDragging&&t.preventDefault(),this.handleMove(t)},handleMouseMove:function(t){this.handleMove(t)},handleTouchScroll:function(t){this.isDragging||this.endInteraction(t,!0)},trigger:function(t){this.options[t]&&this.options[t].apply(this,Array.prototype.slice.call(arguments,1)),this["_"+t]&&this["_"+t].apply(this,Array.prototype.slice.call(arguments,1))}});he.mixin({isAutoScroll:!1,scrollBounds:null,scrollTopVel:null,scrollLeftVel:null,scrollIntervalId:null,scrollSensitivity:30,scrollSpeed:200,scrollIntervalMs:50,initAutoScroll:function(){var t=this.scrollEl;this.isAutoScroll=this.options.scroll&&t&&!t.is(window)&&!t.is(document),this.isAutoScroll&&this.listenTo(t,"scroll",at(this.handleDebouncedScroll,100))},destroyAutoScroll:function(){this.endAutoScroll(),this.isAutoScroll&&this.stopListeningTo(this.scrollEl,"scroll")},computeScrollBounds:function(){this.isAutoScroll&&(this.scrollBounds=h(this.scrollEl))},updateAutoScroll:function(t){var e,n,i,r,s=this.scrollSensitivity,o=this.scrollBounds,l=0,a=0;o&&(e=(s-(E(t)-o.top))/s,n=(s-(o.bottom-E(t)))/s,i=(s-(w(t)-o.left))/s,r=(s-(o.right-w(t)))/s,e>=0&&e<=1?l=e*this.scrollSpeed*-1:n>=0&&n<=1&&(l=n*this.scrollSpeed),i>=0&&i<=1?a=i*this.scrollSpeed*-1:r>=0&&r<=1&&(a=r*this.scrollSpeed)),this.setScrollVel(l,a)},setScrollVel:function(t,e){this.scrollTopVel=t,this.scrollLeftVel=e,this.constrainScrollVel(),!this.scrollTopVel&&!this.scrollLeftVel||this.scrollIntervalId||(this.scrollIntervalId=setInterval(lt(this,"scrollIntervalFunc"),this.scrollIntervalMs))},constrainScrollVel:function(){var t=this.scrollEl;this.scrollTopVel<0?t.scrollTop()<=0&&(this.scrollTopVel=0):this.scrollTopVel>0&&t.scrollTop()+t[0].clientHeight>=t[0].scrollHeight&&(this.scrollTopVel=0),this.scrollLeftVel<0?t.scrollLeft()<=0&&(this.scrollLeftVel=0):this.scrollLeftVel>0&&t.scrollLeft()+t[0].clientWidth>=t[0].scrollWidth&&(this.scrollLeftVel=0)},scrollIntervalFunc:function(){var t=this.scrollEl,e=this.scrollIntervalMs/1e3;this.scrollTopVel&&t.scrollTop(t.scrollTop()+this.scrollTopVel*e),this.scrollLeftVel&&t.scrollLeft(t.scrollLeft()+this.scrollLeftVel*e),this.constrainScrollVel(),this.scrollTopVel||this.scrollLeftVel||this.endAutoScroll()},endAutoScroll:function(){this.scrollIntervalId&&(clearInterval(this.scrollIntervalId),this.scrollIntervalId=null,this.handleScrollEnd())},handleDebouncedScroll:function(){this.scrollIntervalId||this.handleScrollEnd()},handleScrollEnd:function(){}});var fe=he.extend({component:null,origHit:null,hit:null,coordAdjust:null,constructor:function(t,e){he.call(this,e),this.component=t},handleInteractionStart:function(t){var e,n,i,r=this.subjectEl;this.computeCoords(),t?(n={left:w(t),top:E(t)},i=n,r&&(e=h(r),i=R(i,e)),this.origHit=this.queryHit(i.left,i.top),r&&this.options.subjectCenter&&(this.origHit&&(e=x(this.origHit,e)||e),i=I(e)),this.coordAdjust=k(i,n)):(this.origHit=null,this.coordAdjust=null),he.prototype.handleInteractionStart.apply(this,arguments)},computeCoords:function(){this.component.prepareHits(),this.computeScrollBounds()},handleDragStart:function(t){var e;he.prototype.handleDragStart.apply(this,arguments),e=this.queryHit(w(t),E(t)),e&&this.handleHitOver(e)},handleDrag:function(t,e,n){var i;he.prototype.handleDrag.apply(this,arguments),i=this.queryHit(w(n),E(n)),bt(i,this.hit)||(this.hit&&this.handleHitOut(),i&&this.handleHitOver(i))},handleDragEnd:function(){this.handleHitDone(),he.prototype.handleDragEnd.apply(this,arguments)},handleHitOver:function(t){var e=bt(t,this.origHit);this.hit=t,this.trigger("hitOver",this.hit,e,this.origHit)},handleHitOut:function(){this.hit&&(this.trigger("hitOut",this.hit),this.handleHitDone(),this.hit=null)},handleHitDone:function(){this.hit&&this.trigger("hitDone",this.hit)},handleInteractionEnd:function(){he.prototype.handleInteractionEnd.apply(this,arguments),this.origHit=null,this.hit=null,this.component.releaseHits()},handleScrollEnd:function(){he.prototype.handleScrollEnd.apply(this,arguments),this.computeCoords()},queryHit:function(t,e){return this.coordAdjust&&(t+=this.coordAdjust.left,e+=this.coordAdjust.top),this.component.queryHit(t,e)}}),ge=wt.extend(ae,{options:null,sourceEl:null,el:null,parentEl:null,top0:null,left0:null,y0:null,x0:null,topDelta:null,leftDelta:null,isFollowing:!1,isHidden:!1,isAnimating:!1,constructor:function(e,n){this.options=n=n||{},this.sourceEl=e,this.parentEl=n.parentEl?t(n.parentEl):e.parent()},start:function(e){this.isFollowing||(this.isFollowing=!0,this.y0=E(e),this.x0=w(e),this.topDelta=0,this.leftDelta=0,this.isHidden||this.updatePosition(),D(e)?this.listenTo(t(document),"touchmove",this.handleMove):this.listenTo(t(document),"mousemove",this.handleMove))},stop:function(e,n){function i(){r.isAnimating=!1,r.removeElement(),r.top0=r.left0=null,n&&n()}var r=this,s=this.options.revertDuration;this.isFollowing&&!this.isAnimating&&(this.isFollowing=!1,this.stopListeningTo(t(document)),e&&s&&!this.isHidden?(this.isAnimating=!0,this.el.animate({top:this.top0,left:this.left0},{duration:s,complete:i})):i())},getEl:function(){var t=this.el;return t||(t=this.el=this.sourceEl.clone().addClass(this.options.additionalClass||"").css({position:"absolute",visibility:"",display:this.isHidden?"none":"",margin:0,right:"auto",bottom:"auto",width:this.sourceEl.width(),height:this.sourceEl.height(),opacity:this.options.opacity||"",zIndex:this.options.zIndex}),t.addClass("fc-unselectable"),t.appendTo(this.parentEl)),t},removeElement:function(){this.el&&(this.el.remove(),this.el=null)},updatePosition:function(){var t,e;this.getEl(),null===this.top0&&(t=this.sourceEl.offset(),e=this.el.offsetParent().offset(),this.top0=t.top-e.top,this.left0=t.left-e.left),this.el.css({top:this.top0+this.topDelta,left:this.left0+this.leftDelta})},handleMove:function(t){this.topDelta=E(t)-this.y0,this.leftDelta=w(t)-this.x0,this.isHidden||this.updatePosition()},hide:function(){this.isHidden||(this.isHidden=!0,this.el&&this.el.hide())},show:function(){this.isHidden&&(this.isHidden=!1,this.updatePosition(),this.getEl().show())}}),pe=jt.Grid=wt.extend(ae,ue,{hasDayInteractions:!0,view:null,isRTL:null,start:null,end:null,el:null,elsByFill:null,eventTimeFormat:null,displayEventTime:null,displayEventEnd:null,minResizeDuration:null,largeUnit:null,dayDragListener:null,segDragListener:null,segResizeListener:null,externalDragListener:null,constructor:function(t){this.view=t,this.isRTL=t.opt("isRTL"),this.elsByFill={},this.dayDragListener=this.buildDayDragListener(),this.initMouseIgnoring()},computeEventTimeFormat:function(){return this.view.opt("smallTimeFormat")},computeDisplayEventTime:function(){return!0},computeDisplayEventEnd:function(){return!0},setRange:function(t){this.start=t.start.clone(),this.end=t.end.clone(),this.rangeUpdated(),this.processRangeOptions()},rangeUpdated:function(){},processRangeOptions:function(){var t,e,n=this.view;this.eventTimeFormat=n.opt("eventTimeFormat")||n.opt("timeFormat")||this.computeEventTimeFormat(),t=n.opt("displayEventTime"),null==t&&(t=this.computeDisplayEventTime()),e=n.opt("displayEventEnd"),null==e&&(e=this.computeDisplayEventEnd()),this.displayEventTime=t,this.displayEventEnd=e},spanToSegs:function(t){},diffDates:function(t,e){return this.largeUnit?A(t,e,this.largeUnit):N(t,e)},prepareHits:function(){},releaseHits:function(){},queryHit:function(t,e){},getHitSpan:function(t){},getHitEl:function(t){},setElement:function(t){this.el=t,this.hasDayInteractions&&(b(t),this.bindDayHandler("touchstart",this.dayTouchStart),this.bindDayHandler("mousedown",this.dayMousedown)),this.bindSegHandlers(),this.bindGlobalHandlers()},bindDayHandler:function(e,n){var i=this;this.el.on(e,function(e){if(!t(e.target).is(i.segSelector+","+i.segSelector+" *,.fc-more,a[data-goto]"))return n.call(i,e)})},removeElement:function(){this.unbindGlobalHandlers(),this.clearDragListeners(),this.el.remove()},renderSkeleton:function(){},renderDates:function(){},unrenderDates:function(){},bindGlobalHandlers:function(){this.listenTo(t(document),{dragstart:this.externalDragStart,sortstart:this.externalDragStart})},unbindGlobalHandlers:function(){this.stopListeningTo(t(document))},dayMousedown:function(t){this.isIgnoringMouse||this.dayDragListener.startInteraction(t,{})},dayTouchStart:function(t){var e=this.view;(e.isSelected||e.selectedEvent)&&this.tempIgnoreMouse(),this.dayDragListener.startInteraction(t,{delay:this.view.opt("longPressDelay")})},buildDayDragListener:function(){var t,e,n=this,i=this.view,r=i.opt("selectable"),l=new fe(this,{scroll:i.opt("dragScroll"),interactionStart:function(){t=l.origHit,e=null},dragStart:function(){i.unselect()},hitOver:function(i,o,l){l&&(o||(t=null),r&&(e=n.computeSelection(n.getHitSpan(l),n.getHitSpan(i)),e?n.renderSelection(e):e===!1&&s()))},hitOut:function(){t=null,e=null,n.unrenderSelection()},hitDone:function(){o()},interactionEnd:function(r,s){s||(t&&!n.isIgnoringMouse&&i.triggerDayClick(n.getHitSpan(t),n.getHitEl(t),r),e&&i.reportSelection(e,r))}});return l},clearDragListeners:function(){this.dayDragListener.endInteraction(),this.segDragListener&&this.segDragListener.endInteraction(),this.segResizeListener&&this.segResizeListener.endInteraction(),this.externalDragListener&&this.externalDragListener.endInteraction()},renderEventLocationHelper:function(t,e){var n=this.fabricateHelperEvent(t,e);return this.renderHelper(n,e)},fabricateHelperEvent:function(t,e){var n=e?Z(e.event):{};return n.start=t.start.clone(),n.end=t.end?t.end.clone():null,n.allDay=null,this.view.calendar.normalizeEventDates(n),n.className=(n.className||[]).concat("fc-helper"),e||(n.editable=!1),n},renderHelper:function(t,e){},unrenderHelper:function(){},renderSelection:function(t){this.renderHighlight(t)},unrenderSelection:function(){this.unrenderHighlight()},computeSelection:function(t,e){var n=this.computeSelectionSpan(t,e);return!(n&&!this.view.calendar.isSelectionSpanAllowed(n))&&n},computeSelectionSpan:function(t,e){var n=[t.start,t.end,e.start,e.end];return n.sort(st),{start:n[0].clone(),end:n[3].clone()}},renderHighlight:function(t){this.renderFill("highlight",this.spanToSegs(t))},unrenderHighlight:function(){this.unrenderFill("highlight")},highlightSegClasses:function(){return["fc-highlight"]},renderBusinessHours:function(){},unrenderBusinessHours:function(){},getNowIndicatorUnit:function(){},renderNowIndicator:function(t){},unrenderNowIndicator:function(){},renderFill:function(t,e){},unrenderFill:function(t){var e=this.elsByFill[t];e&&(e.remove(),delete this.elsByFill[t])},renderFillSegEls:function(e,n){var i,r=this,s=this[e+"SegEl"],o="",l=[];if(n.length){for(i=0;i<n.length;i++)o+=this.fillSegHtml(e,n[i]);t(o).each(function(e,i){var o=n[e],a=t(i);s&&(a=s.call(r,o,a)),a&&(a=t(a),a.is(r.fillSegTag)&&(o.el=a,l.push(o)))})}return l},fillSegTag:"div",fillSegHtml:function(t,e){var n=this[t+"SegClasses"],i=this[t+"SegCss"],r=n?n.call(this,e):[],s=nt(i?i.call(this,e):{});return"<"+this.fillSegTag+(r.length?' class="'+r.join(" ")+'"':"")+(s?' style="'+s+'"':"")+" />"},getDayClasses:function(t){var e=this.view,n=e.calendar.getNow(),i=["fc-"+$t[t.day()]];return 1==e.intervalDuration.as("months")&&t.month()!=e.intervalStart.month()&&i.push("fc-other-month"),t.isSame(n,"day")?i.push("fc-today",e.highlightStateClass):t<n?i.push("fc-past"):i.push("fc-future"),i}});pe.mixin({segSelector:".fc-event-container > *",mousedOverSeg:null,isDraggingSeg:!1,isResizingSeg:!1,isDraggingExternal:!1,segs:null,renderEvents:function(t){var e,n=[],i=[];for(e=0;e<t.length;e++)(Tt(t[e])?n:i).push(t[e]);this.segs=[].concat(this.renderBgEvents(n),this.renderFgEvents(i))},renderBgEvents:function(t){var e=this.eventsToSegs(t);return this.renderBgSegs(e)||e},renderFgEvents:function(t){var e=this.eventsToSegs(t);return this.renderFgSegs(e)||e},unrenderEvents:function(){this.handleSegMouseout(),this.clearDragListeners(),this.unrenderFgSegs(),this.unrenderBgSegs(),this.segs=null},getEventSegs:function(){return this.segs||[]},renderFgSegs:function(t){},unrenderFgSegs:function(){},renderFgSegEls:function(e,n){var i,r=this.view,s="",o=[];if(e.length){for(i=0;i<e.length;i++)s+=this.fgSegHtml(e[i],n);t(s).each(function(n,i){var s=e[n],l=r.resolveEventEl(s.event,t(i));l&&(l.data("fc-seg",s),s.el=l,o.push(s))})}return o},fgSegHtml:function(t,e){},renderBgSegs:function(t){return this.renderFill("bgEvent",t)},unrenderBgSegs:function(){this.unrenderFill("bgEvent")},bgEventSegEl:function(t,e){return this.view.resolveEventEl(t.event,e)},bgEventSegClasses:function(t){var e=t.event,n=e.source||{};return["fc-bgevent"].concat(e.className,n.className||[])},bgEventSegCss:function(t){return{"background-color":this.getSegSkinCss(t)["background-color"]}},businessHoursSegClasses:function(t){return["fc-nonbusiness","fc-bgevent"]},buildBusinessHourSegs:function(e){var n=this.view.calendar.getCurrentBusinessHourEvents(e);return!n.length&&this.view.calendar.options.businessHours&&(n=[t.extend({},Re,{start:this.view.end,end:this.view.end,dow:null})]),this.eventsToSegs(n)},bindSegHandlers:function(){this.bindSegHandlersToEl(this.el)},bindSegHandlersToEl:function(t){this.bindSegHandlerToEl(t,"touchstart",this.handleSegTouchStart),this.bindSegHandlerToEl(t,"touchend",this.handleSegTouchEnd),this.bindSegHandlerToEl(t,"mouseenter",this.handleSegMouseover),this.bindSegHandlerToEl(t,"mouseleave",this.handleSegMouseout),this.bindSegHandlerToEl(t,"mousedown",this.handleSegMousedown),this.bindSegHandlerToEl(t,"click",this.handleSegClick)},bindSegHandlerToEl:function(e,n,i){var r=this;e.on(n,this.segSelector,function(e){var n=t(this).data("fc-seg");if(n&&!r.isDraggingSeg&&!r.isResizingSeg)return i.call(r,n,e)})},handleSegClick:function(t,e){var n=this.view.trigger("eventClick",t.el[0],t.event,e);n===!1&&e.preventDefault()},handleSegMouseover:function(t,e){this.isIgnoringMouse||this.mousedOverSeg||(this.mousedOverSeg=t,this.view.isEventResizable(t.event)&&t.el.addClass("fc-allow-mouse-resize"),this.view.trigger("eventMouseover",t.el[0],t.event,e))},handleSegMouseout:function(t,e){e=e||{},this.mousedOverSeg&&(t=t||this.mousedOverSeg,this.mousedOverSeg=null,this.view.isEventResizable(t.event)&&t.el.removeClass("fc-allow-mouse-resize"),this.view.trigger("eventMouseout",t.el[0],t.event,e))},handleSegMousedown:function(t,e){var n=this.startSegResize(t,e,{distance:5});!n&&this.view.isEventDraggable(t.event)&&this.buildSegDragListener(t).startInteraction(e,{distance:5})},handleSegTouchStart:function(t,e){var n,i=this.view,r=t.event,s=i.isEventSelected(r),o=i.isEventDraggable(r),l=i.isEventResizable(r),a=!1;s&&l&&(a=this.startSegResize(t,e)),a||!o&&!l||(n=o?this.buildSegDragListener(t):this.buildSegSelectListener(t),n.startInteraction(e,{delay:s?0:this.view.opt("longPressDelay")})),this.tempIgnoreMouse()},handleSegTouchEnd:function(t,e){this.tempIgnoreMouse()},startSegResize:function(e,n,i){return!!t(n.target).is(".fc-resizer")&&(this.buildSegResizeListener(e,t(n.target).is(".fc-start-resizer")).startInteraction(n,i),!0)},buildSegDragListener:function(t){var e,n,i,r=this,l=this.view,a=l.calendar,u=t.el,d=t.event;if(this.segDragListener)return this.segDragListener;var c=this.segDragListener=new fe(l,{scroll:l.opt("dragScroll"),subjectEl:u,subjectCenter:!0,interactionStart:function(i){t.component=r,e=!1,n=new ge(t.el,{additionalClass:"fc-dragging",parentEl:l.el,opacity:c.isTouch?null:l.opt("dragOpacity"),revertDuration:l.opt("dragRevertDuration"),zIndex:2}),n.hide(),n.start(i)},dragStart:function(n){c.isTouch&&!l.isEventSelected(d)&&l.selectEvent(d),e=!0,r.handleSegMouseout(t,n),r.segDragStart(t,n),l.hideEvent(d)},hitOver:function(e,o,u){var h;t.hit&&(u=t.hit),i=r.computeEventDrop(u.component.getHitSpan(u),e.component.getHitSpan(e),d),i&&!a.isEventSpanAllowed(r.eventToSpan(i),d)&&(s(),i=null),i&&(h=l.renderDrag(i,t))?(h.addClass("fc-dragging"),c.isTouch||r.applyDragOpacity(h),n.hide()):n.show(),o&&(i=null)},hitOut:function(){l.unrenderDrag(),n.show(),i=null},hitDone:function(){o()},interactionEnd:function(s){delete t.component,n.stop(!i,function(){e&&(l.unrenderDrag(),l.showEvent(d),r.segDragStop(t,s)),i&&l.reportEventDrop(d,i,this.largeUnit,u,s)}),r.segDragListener=null}});return c},buildSegSelectListener:function(t){var e=this,n=this.view,i=t.event;if(this.segDragListener)return this.segDragListener;var r=this.segDragListener=new he({dragStart:function(t){r.isTouch&&!n.isEventSelected(i)&&n.selectEvent(i)},interactionEnd:function(t){e.segDragListener=null}});return r},segDragStart:function(t,e){this.isDraggingSeg=!0,this.view.trigger("eventDragStart",t.el[0],t.event,e,{})},segDragStop:function(t,e){this.isDraggingSeg=!1,this.view.trigger("eventDragStop",t.el[0],t.event,e,{})},computeEventDrop:function(t,e,n){var i,r,s=this.view.calendar,o=t.start,l=e.start;return o.hasTime()===l.hasTime()?(i=this.diffDates(l,o),n.allDay&&W(i)?(r={start:n.start.clone(),end:s.getEventEnd(n),allDay:!1},s.normalizeEventTimes(r)):r=Ht(n),r.start.add(i),r.end&&r.end.add(i)):r={start:l.clone(),end:null,allDay:!l.hasTime()},r},applyDragOpacity:function(t){var e=this.view.opt("dragOpacity");null!=e&&t.css("opacity",e)},externalDragStart:function(e,n){var i,r,s=this.view;s.opt("droppable")&&(i=t((n?n.item:null)||e.target),r=s.opt("dropAccept"),(t.isFunction(r)?r.call(i[0],i):i.is(r))&&(this.isDraggingExternal||this.listenToExternalDrag(i,e,n)))},listenToExternalDrag:function(t,e,n){var i,r=this,l=this.view.calendar,a=Mt(t),u=r.externalDragListener=new fe(this,{interactionStart:function(){r.isDraggingExternal=!0},hitOver:function(t){i=r.computeExternalDrop(t.component.getHitSpan(t),a),i&&!l.isExternalSpanAllowed(r.eventToSpan(i),i,a.eventProps)&&(s(),i=null),i&&r.renderDrag(i)},hitOut:function(){i=null},hitDone:function(){o(),r.unrenderDrag()},interactionEnd:function(e){i&&r.view.reportExternalDrop(a,i,t,e,n),r.isDraggingExternal=!1,r.externalDragListener=null}});u.startDrag(e)},computeExternalDrop:function(t,e){var n=this.view.calendar,i={start:n.applyTimezone(t.start),end:null};return e.startTime&&!i.start.hasTime()&&i.start.time(e.startTime),e.duration&&(i.end=i.start.clone().add(e.duration)),i},renderDrag:function(t,e){},unrenderDrag:function(){},buildSegResizeListener:function(t,e){var n,i,r=this,l=this.view,a=l.calendar,u=t.el,d=t.event,c=a.getEventEnd(d),h=this.segResizeListener=new fe(this,{scroll:l.opt("dragScroll"),subjectEl:u,interactionStart:function(){n=!1},dragStart:function(e){n=!0,r.handleSegMouseout(t,e),r.segResizeStart(t,e)},hitOver:function(n,o,u){var h=r.getHitSpan(u),f=r.getHitSpan(n);i=e?r.computeEventStartResize(h,f,d):r.computeEventEndResize(h,f,d),i&&(a.isEventSpanAllowed(r.eventToSpan(i),d)?i.start.isSame(d.start.clone().stripZone())&&i.end.isSame(c.clone().stripZone())&&(i=null):(s(),i=null)),i&&(l.hideEvent(d),r.renderEventResize(i,t))},hitOut:function(){i=null},hitDone:function(){r.unrenderEventResize(),l.showEvent(d),o()},interactionEnd:function(e){n&&r.segResizeStop(t,e),i&&l.reportEventResize(d,i,this.largeUnit,u,e),r.segResizeListener=null}});return h},segResizeStart:function(t,e){this.isResizingSeg=!0,this.view.trigger("eventResizeStart",t.el[0],t.event,e,{})},segResizeStop:function(t,e){this.isResizingSeg=!1,this.view.trigger("eventResizeStop",t.el[0],t.event,e,{})},computeEventStartResize:function(t,e,n){return this.computeEventResize("start",t,e,n)},computeEventEndResize:function(t,e,n){return this.computeEventResize("end",t,e,n)},computeEventResize:function(t,e,n,i){var r,s,o=this.view.calendar,l=this.diffDates(n[t],e[t]);return r={start:i.start.clone(),end:o.getEventEnd(i),allDay:i.allDay},r.allDay&&W(l)&&(r.allDay=!1,o.normalizeEventTimes(r)),r[t].add(l),r.start.isBefore(r.end)||(s=this.minResizeDuration||(i.allDay?o.defaultAllDayEventDuration:o.defaultTimedEventDuration),"start"==t?r.start=r.end.clone().subtract(s):r.end=r.start.clone().add(s)),r},renderEventResize:function(t,e){},unrenderEventResize:function(){},getEventTimeText:function(t,e,n){return null==e&&(e=this.eventTimeFormat),null==n&&(n=this.displayEventEnd),this.displayEventTime&&t.start.hasTime()?n&&t.end?this.view.formatRange(t,e):t.start.format(e):""},getSegClasses:function(t,e,n){var i=this.view,r=["fc-event",t.isStart?"fc-start":"fc-not-start",t.isEnd?"fc-end":"fc-not-end"].concat(this.getSegCustomClasses(t));return e&&r.push("fc-draggable"),n&&r.push("fc-resizable"),i.isEventSelected(t.event)&&r.push("fc-selected"),r},getSegCustomClasses:function(t){var e=t.event;return[].concat(e.className,e.source?e.source.className:[])},getSegSkinCss:function(t){return{"background-color":this.getSegBackgroundColor(t),"border-color":this.getSegBorderColor(t),color:this.getSegTextColor(t)}},getSegBackgroundColor:function(t){return t.event.backgroundColor||t.event.color||this.getSegDefaultBackgroundColor(t)},getSegDefaultBackgroundColor:function(t){var e=t.event.source||{};return e.backgroundColor||e.color||this.view.opt("eventBackgroundColor")||this.view.opt("eventColor")},getSegBorderColor:function(t){return t.event.borderColor||t.event.color||this.getSegDefaultBorderColor(t)},getSegDefaultBorderColor:function(t){var e=t.event.source||{};return e.borderColor||e.color||this.view.opt("eventBorderColor")||this.view.opt("eventColor")},getSegTextColor:function(t){return t.event.textColor||this.getSegDefaultTextColor(t)},getSegDefaultTextColor:function(t){var e=t.event.source||{};return e.textColor||this.view.opt("eventTextColor")},eventToSegs:function(t){return this.eventsToSegs([t])},eventToSpan:function(t){return this.eventToSpans(t)[0]},eventToSpans:function(t){var e=this.eventToRange(t);return this.eventRangeToSpans(e,t)},eventsToSegs:function(e,n){var i=this,r=It(e),s=[];return t.each(r,function(t,e){var r,o=[];for(r=0;r<e.length;r++)o.push(i.eventToRange(e[r]));if(xt(e[0]))for(o=i.invertRanges(o),r=0;r<o.length;r++)s.push.apply(s,i.eventRangeToSegs(o[r],e[0],n));else for(r=0;r<o.length;r++)s.push.apply(s,i.eventRangeToSegs(o[r],e[r],n))}),s},eventToRange:function(t){var e=this.view.calendar,n=t.start.clone().stripZone(),i=(t.end?t.end.clone():e.getDefaultEventEnd(null!=t.allDay?t.allDay:!t.start.hasTime(),t.start)).stripZone();return e.localizeMoment(n),e.localizeMoment(i),{start:n,end:i}},eventRangeToSegs:function(t,e,n){var i,r=this.eventRangeToSpans(t,e),s=[];for(i=0;i<r.length;i++)s.push.apply(s,this.eventSpanToSegs(r[i],e,n));return s},eventRangeToSpans:function(e,n){return[t.extend({},e)]},eventSpanToSegs:function(t,e,n){var i,r,s=n?n(t):this.spanToSegs(t);for(i=0;i<s.length;i++)r=s[i],r.event=e,r.eventStartMS=+t.start,r.eventDurationMS=t.end-t.start;return s},invertRanges:function(t){var e,n,i=this.view,r=i.start.clone(),s=i.end.clone(),o=[],l=r;for(t.sort(kt),e=0;e<t.length;e++)n=t[e],n.start>l&&o.push({start:l,end:n.start}),l=n.end;return l<s&&o.push({start:l,end:s}),o},sortEventSegs:function(t){t.sort(lt(this,"compareEventSegs"))},compareEventSegs:function(t,e){return t.eventStartMS-e.eventStartMS||e.eventDurationMS-t.eventDurationMS||e.event.allDay-t.event.allDay||L(t.event,e.event,this.view.eventOrderSpecs)}}),jt.pluckEventDateProps=Ht,jt.isBgEvent=Tt,jt.dataAttrPrefix="";var ve=jt.DayTableMixin={breakOnWeeks:!1,dayDates:null,dayIndices:null,daysPerRow:null,rowCnt:null,colCnt:null,colHeadFormat:null,updateDayTable:function(){for(var t,e,n,i=this.view,r=this.start.clone(),s=-1,o=[],l=[];r.isBefore(this.end);)i.isHiddenDay(r)?o.push(s+.5):(s++,o.push(s),l.push(r.clone())),r.add(1,"days");if(this.breakOnWeeks){for(e=l[0].day(),t=1;t<l.length&&l[t].day()!=e;t++);n=Math.ceil(l.length/t)}else n=1,t=l.length;this.dayDates=l,this.dayIndices=o,this.daysPerRow=t,this.rowCnt=n,this.updateDayTableCols()},updateDayTableCols:function(){this.colCnt=this.computeColCnt(),this.colHeadFormat=this.view.opt("columnFormat")||this.computeColHeadFormat()},computeColCnt:function(){return this.daysPerRow},getCellDate:function(t,e){return this.dayDates[this.getCellDayIndex(t,e)].clone()},getCellRange:function(t,e){var n=this.getCellDate(t,e),i=n.clone().add(1,"days");return{start:n,end:i}},getCellDayIndex:function(t,e){return t*this.daysPerRow+this.getColDayIndex(e)},getColDayIndex:function(t){return this.isRTL?this.colCnt-1-t:t},getDateDayIndex:function(t){var e=this.dayIndices,n=t.diff(this.start,"days");return n<0?e[0]-1:n>=e.length?e[e.length-1]+1:e[n]},computeColHeadFormat:function(){return this.rowCnt>1||this.colCnt>10?"ddd":this.colCnt>1?this.view.opt("dayOfMonthFormat"):"dddd"},sliceRangeByRow:function(t){var e,n,i,r,s,o=this.daysPerRow,l=this.view.computeDayRange(t),a=this.getDateDayIndex(l.start),u=this.getDateDayIndex(l.end.clone().subtract(1,"days")),d=[];for(e=0;e<this.rowCnt;e++)n=e*o,i=n+o-1,r=Math.max(a,n),s=Math.min(u,i),r=Math.ceil(r),s=Math.floor(s),r<=s&&d.push({row:e,firstRowDayIndex:r-n,lastRowDayIndex:s-n,isStart:r===a,isEnd:s===u});return d},sliceRangeByDay:function(t){var e,n,i,r,s,o,l=this.daysPerRow,a=this.view.computeDayRange(t),u=this.getDateDayIndex(a.start),d=this.getDateDayIndex(a.end.clone().subtract(1,"days")),c=[];for(e=0;e<this.rowCnt;e++)for(n=e*l,i=n+l-1,r=n;r<=i;r++)s=Math.max(u,r),o=Math.min(d,r),s=Math.ceil(s),o=Math.floor(o),s<=o&&c.push({row:e,firstRowDayIndex:s-n,lastRowDayIndex:o-n,isStart:s===u,isEnd:o===d});return c;
},renderHeadHtml:function(){var t=this.view;return'<div class="fc-row '+t.widgetHeaderClass+'"><table><thead>'+this.renderHeadTrHtml()+"</thead></table></div>"},renderHeadIntroHtml:function(){return this.renderIntroHtml()},renderHeadTrHtml:function(){return"<tr>"+(this.isRTL?"":this.renderHeadIntroHtml())+this.renderHeadDateCellsHtml()+(this.isRTL?this.renderHeadIntroHtml():"")+"</tr>"},renderHeadDateCellsHtml:function(){var t,e,n=[];for(t=0;t<this.colCnt;t++)e=this.getCellDate(0,t),n.push(this.renderHeadDateCellHtml(e));return n.join("")},renderHeadDateCellHtml:function(t,e,n){var i=this.view;return'<th class="fc-day-header '+i.widgetHeaderClass+" fc-"+$t[t.day()]+'"'+(1===this.rowCnt?' data-date="'+t.format("YYYY-MM-DD")+'"':"")+(e>1?' colspan="'+e+'"':"")+(n?" "+n:"")+">"+i.buildGotoAnchorHtml({date:t,forceOff:this.rowCnt>1||1===this.colCnt},tt(t.format(this.colHeadFormat)))+"</th>"},renderBgTrHtml:function(t){return"<tr>"+(this.isRTL?"":this.renderBgIntroHtml(t))+this.renderBgCellsHtml(t)+(this.isRTL?this.renderBgIntroHtml(t):"")+"</tr>"},renderBgIntroHtml:function(t){return this.renderIntroHtml()},renderBgCellsHtml:function(t){var e,n,i=[];for(e=0;e<this.colCnt;e++)n=this.getCellDate(t,e),i.push(this.renderBgCellHtml(n));return i.join("")},renderBgCellHtml:function(t,e){var n=this.view,i=this.getDayClasses(t);return i.unshift("fc-day",n.widgetContentClass),'<td class="'+i.join(" ")+'" data-date="'+t.format("YYYY-MM-DD")+'"'+(e?" "+e:"")+"></td>"},renderIntroHtml:function(){},bookendCells:function(t){var e=this.renderIntroHtml();e&&(this.isRTL?t.append(e):t.prepend(e))}},me=jt.DayGrid=pe.extend(ve,{numbersVisible:!1,bottomCoordPadding:0,rowEls:null,cellEls:null,helperEls:null,rowCoordCache:null,colCoordCache:null,renderDates:function(t){var e,n,i=this.view,r=this.rowCnt,s=this.colCnt,o="";for(e=0;e<r;e++)o+=this.renderDayRowHtml(e,t);for(this.el.html(o),this.rowEls=this.el.find(".fc-row"),this.cellEls=this.el.find(".fc-day"),this.rowCoordCache=new ce({els:this.rowEls,isVertical:!0}),this.colCoordCache=new ce({els:this.cellEls.slice(0,this.colCnt),isHorizontal:!0}),e=0;e<r;e++)for(n=0;n<s;n++)i.trigger("dayRender",null,this.getCellDate(e,n),this.getCellEl(e,n))},unrenderDates:function(){this.removeSegPopover()},renderBusinessHours:function(){var t=this.buildBusinessHourSegs(!0);this.renderFill("businessHours",t,"bgevent")},unrenderBusinessHours:function(){this.unrenderFill("businessHours")},renderDayRowHtml:function(t,e){var n=this.view,i=["fc-row","fc-week",n.widgetContentClass];return e&&i.push("fc-rigid"),'<div class="'+i.join(" ")+'"><div class="fc-bg"><table>'+this.renderBgTrHtml(t)+'</table></div><div class="fc-content-skeleton"><table>'+(this.numbersVisible?"<thead>"+this.renderNumberTrHtml(t)+"</thead>":"")+"</table></div></div>"},renderNumberTrHtml:function(t){return"<tr>"+(this.isRTL?"":this.renderNumberIntroHtml(t))+this.renderNumberCellsHtml(t)+(this.isRTL?this.renderNumberIntroHtml(t):"")+"</tr>"},renderNumberIntroHtml:function(t){return this.renderIntroHtml()},renderNumberCellsHtml:function(t){var e,n,i=[];for(e=0;e<this.colCnt;e++)n=this.getCellDate(t,e),i.push(this.renderNumberCellHtml(n));return i.join("")},renderNumberCellHtml:function(t){var e,n,i="";return this.view.dayNumbersVisible||this.view.cellWeekNumbersVisible?(e=this.getDayClasses(t),e.unshift("fc-day-top"),this.view.cellWeekNumbersVisible&&(n="ISO"===t._locale._fullCalendar_weekCalc?1:t._locale.firstDayOfWeek()),i+='<td class="'+e.join(" ")+'" data-date="'+t.format()+'">',this.view.cellWeekNumbersVisible&&t.day()==n&&(i+=this.view.buildGotoAnchorHtml({date:t,type:"week"},{class:"fc-week-number"},t.format("w"))),this.view.dayNumbersVisible&&(i+=this.view.buildGotoAnchorHtml(t,{class:"fc-day-number"},t.date())),i+="</td>"):"<td/>"},computeEventTimeFormat:function(){return this.view.opt("extraSmallTimeFormat")},computeDisplayEventEnd:function(){return 1==this.colCnt},rangeUpdated:function(){this.updateDayTable()},spanToSegs:function(t){var e,n,i=this.sliceRangeByRow(t);for(e=0;e<i.length;e++)n=i[e],this.isRTL?(n.leftCol=this.daysPerRow-1-n.lastRowDayIndex,n.rightCol=this.daysPerRow-1-n.firstRowDayIndex):(n.leftCol=n.firstRowDayIndex,n.rightCol=n.lastRowDayIndex);return i},prepareHits:function(){this.colCoordCache.build(),this.rowCoordCache.build(),this.rowCoordCache.bottoms[this.rowCnt-1]+=this.bottomCoordPadding},releaseHits:function(){this.colCoordCache.clear(),this.rowCoordCache.clear()},queryHit:function(t,e){if(this.colCoordCache.isLeftInBounds(t)&&this.rowCoordCache.isTopInBounds(e)){var n=this.colCoordCache.getHorizontalIndex(t),i=this.rowCoordCache.getVerticalIndex(e);if(null!=i&&null!=n)return this.getCellHit(i,n)}},getHitSpan:function(t){return this.getCellRange(t.row,t.col)},getHitEl:function(t){return this.getCellEl(t.row,t.col)},getCellHit:function(t,e){return{row:t,col:e,component:this,left:this.colCoordCache.getLeftOffset(e),right:this.colCoordCache.getRightOffset(e),top:this.rowCoordCache.getTopOffset(t),bottom:this.rowCoordCache.getBottomOffset(t)}},getCellEl:function(t,e){return this.cellEls.eq(t*this.colCnt+e)},renderDrag:function(t,e){if(this.renderHighlight(this.eventToSpan(t)),e&&e.component!==this)return this.renderEventLocationHelper(t,e)},unrenderDrag:function(){this.unrenderHighlight(),this.unrenderHelper()},renderEventResize:function(t,e){return this.renderHighlight(this.eventToSpan(t)),this.renderEventLocationHelper(t,e)},unrenderEventResize:function(){this.unrenderHighlight(),this.unrenderHelper()},renderHelper:function(e,n){var i,r=[],s=this.eventToSegs(e);return s=this.renderFgSegEls(s),i=this.renderSegRows(s),this.rowEls.each(function(e,s){var o,l=t(s),a=t('<div class="fc-helper-skeleton"><table/></div>');o=n&&n.row===e?n.el.position().top:l.find(".fc-content-skeleton tbody").position().top,a.css("top",o).find("table").append(i[e].tbodyEl),l.append(a),r.push(a[0])}),this.helperEls=t(r)},unrenderHelper:function(){this.helperEls&&(this.helperEls.remove(),this.helperEls=null)},fillSegTag:"td",renderFill:function(e,n,i){var r,s,o,l=[];for(n=this.renderFillSegEls(e,n),r=0;r<n.length;r++)s=n[r],o=this.renderFillRow(e,s,i),this.rowEls.eq(s.row).append(o),l.push(o[0]);return this.elsByFill[e]=t(l),n},renderFillRow:function(e,n,i){var r,s,o=this.colCnt,l=n.leftCol,a=n.rightCol+1;return i=i||e.toLowerCase(),r=t('<div class="fc-'+i+'-skeleton"><table><tr/></table></div>'),s=r.find("tr"),l>0&&s.append('<td colspan="'+l+'"/>'),s.append(n.el.attr("colspan",a-l)),a<o&&s.append('<td colspan="'+(o-a)+'"/>'),this.bookendCells(s),r}});me.mixin({rowStructs:null,unrenderEvents:function(){this.removeSegPopover(),pe.prototype.unrenderEvents.apply(this,arguments)},getEventSegs:function(){return pe.prototype.getEventSegs.call(this).concat(this.popoverSegs||[])},renderBgSegs:function(e){var n=t.grep(e,function(t){return t.event.allDay});return pe.prototype.renderBgSegs.call(this,n)},renderFgSegs:function(e){var n;return e=this.renderFgSegEls(e),n=this.rowStructs=this.renderSegRows(e),this.rowEls.each(function(e,i){t(i).find(".fc-content-skeleton > table").append(n[e].tbodyEl)}),e},unrenderFgSegs:function(){for(var t,e=this.rowStructs||[];t=e.pop();)t.tbodyEl.remove();this.rowStructs=null},renderSegRows:function(t){var e,n,i=[];for(e=this.groupSegRows(t),n=0;n<e.length;n++)i.push(this.renderSegRow(n,e[n]));return i},fgSegHtml:function(t,e){var n,i,r=this.view,s=t.event,o=r.isEventDraggable(s),l=!e&&s.allDay&&t.isStart&&r.isEventResizableFromStart(s),a=!e&&s.allDay&&t.isEnd&&r.isEventResizableFromEnd(s),u=this.getSegClasses(t,o,l||a),d=nt(this.getSegSkinCss(t)),c="";return u.unshift("fc-day-grid-event","fc-h-event"),t.isStart&&(n=this.getEventTimeText(s),n&&(c='<span class="fc-time">'+tt(n)+"</span>")),i='<span class="fc-title">'+(tt(s.title||"")||"&nbsp;")+"</span>",'<a class="'+u.join(" ")+'"'+(s.url?' href="'+tt(s.url)+'"':"")+(d?' style="'+d+'"':"")+'><div class="fc-content">'+(this.isRTL?i+" "+c:c+" "+i)+"</div>"+(l?'<div class="fc-resizer fc-start-resizer" />':"")+(a?'<div class="fc-resizer fc-end-resizer" />':"")+"</a>"},renderSegRow:function(e,n){function i(e){for(;o<e;)d=(m[r-1]||[])[o],d?d.attr("rowspan",parseInt(d.attr("rowspan")||1,10)+1):(d=t("<td/>"),l.append(d)),v[r][o]=d,m[r][o]=d,o++}var r,s,o,l,a,u,d,c=this.colCnt,h=this.buildSegLevels(n),f=Math.max(1,h.length),g=t("<tbody/>"),p=[],v=[],m=[];for(r=0;r<f;r++){if(s=h[r],o=0,l=t("<tr/>"),p.push([]),v.push([]),m.push([]),s)for(a=0;a<s.length;a++){for(u=s[a],i(u.leftCol),d=t('<td class="fc-event-container"/>').append(u.el),u.leftCol!=u.rightCol?d.attr("colspan",u.rightCol-u.leftCol+1):m[r][o]=d;o<=u.rightCol;)v[r][o]=d,p[r][o]=u,o++;l.append(d)}i(c),this.bookendCells(l),g.append(l)}return{row:e,tbodyEl:g,cellMatrix:v,segMatrix:p,segLevels:h,segs:n}},buildSegLevels:function(t){var e,n,i,r=[];for(this.sortEventSegs(t),e=0;e<t.length;e++){for(n=t[e],i=0;i<r.length&&Lt(n,r[i]);i++);n.level=i,(r[i]||(r[i]=[])).push(n)}for(i=0;i<r.length;i++)r[i].sort(Bt);return r},groupSegRows:function(t){var e,n=[];for(e=0;e<this.rowCnt;e++)n.push([]);for(e=0;e<t.length;e++)n[t[e].row].push(t[e]);return n}}),me.mixin({segPopover:null,popoverSegs:null,removeSegPopover:function(){this.segPopover&&this.segPopover.hide()},limitRows:function(t){var e,n,i=this.rowStructs||[];for(e=0;e<i.length;e++)this.unlimitRow(e),n=!!t&&("number"==typeof t?t:this.computeRowLevelLimit(e)),n!==!1&&this.limitRow(e,n)},computeRowLevelLimit:function(e){function n(e,n){s=Math.max(s,t(n).outerHeight())}var i,r,s,o=this.rowEls.eq(e),l=o.height(),a=this.rowStructs[e].tbodyEl.children();for(i=0;i<a.length;i++)if(r=a.eq(i).removeClass("fc-limited"),s=0,r.find("> td > :first-child").each(n),r.position().top+s>l)return i;return!1},limitRow:function(e,n){function i(i){for(;D<i;)u=S.getCellSegs(e,D,n),u.length&&(h=s[n-1][D],y=S.renderMoreLink(e,D,u),m=t("<div/>").append(y),h.append(m),E.push(m[0])),D++}var r,s,o,l,a,u,d,c,h,f,g,p,v,m,y,S=this,w=this.rowStructs[e],E=[],D=0;if(n&&n<w.segLevels.length){for(r=w.segLevels[n-1],s=w.cellMatrix,o=w.tbodyEl.children().slice(n).addClass("fc-limited").get(),l=0;l<r.length;l++){for(a=r[l],i(a.leftCol),c=[],d=0;D<=a.rightCol;)u=this.getCellSegs(e,D,n),c.push(u),d+=u.length,D++;if(d){for(h=s[n-1][a.leftCol],f=h.attr("rowspan")||1,g=[],p=0;p<c.length;p++)v=t('<td class="fc-more-cell"/>').attr("rowspan",f),u=c[p],y=this.renderMoreLink(e,a.leftCol+p,[a].concat(u)),m=t("<div/>").append(y),v.append(m),g.push(v[0]),E.push(v[0]);h.addClass("fc-limited").after(t(g)),o.push(h[0])}}i(this.colCnt),w.moreEls=t(E),w.limitedEls=t(o)}},unlimitRow:function(t){var e=this.rowStructs[t];e.moreEls&&(e.moreEls.remove(),e.moreEls=null),e.limitedEls&&(e.limitedEls.removeClass("fc-limited"),e.limitedEls=null)},renderMoreLink:function(e,n,i){var r=this,s=this.view;return t('<a class="fc-more"/>').text(this.getMoreLinkText(i.length)).on("click",function(o){var l=s.opt("eventLimitClick"),a=r.getCellDate(e,n),u=t(this),d=r.getCellEl(e,n),c=r.getCellSegs(e,n),h=r.resliceDaySegs(c,a),f=r.resliceDaySegs(i,a);"function"==typeof l&&(l=s.trigger("eventLimitClick",null,{date:a,dayEl:d,moreEl:u,segs:h,hiddenSegs:f},o)),"popover"===l?r.showSegPopover(e,n,u,h):"string"==typeof l&&s.calendar.zoomTo(a,l)})},showSegPopover:function(t,e,n,i){var r,s,o=this,l=this.view,a=n.parent();r=1==this.rowCnt?l.el:this.rowEls.eq(t),s={className:"fc-more-popover",content:this.renderSegPopoverContent(t,e,i),parentEl:this.view.el,top:r.offset().top,autoHide:!0,viewportConstrain:l.opt("popoverViewportConstrain"),hide:function(){o.segPopover.removeElement(),o.segPopover=null,o.popoverSegs=null}},this.isRTL?s.right=a.offset().left+a.outerWidth()+1:s.left=a.offset().left-1,this.segPopover=new de(s),this.segPopover.show(),this.bindSegHandlersToEl(this.segPopover.el)},renderSegPopoverContent:function(e,n,i){var r,s=this.view,o=s.opt("theme"),l=this.getCellDate(e,n).format(s.opt("dayPopoverFormat")),a=t('<div class="fc-header '+s.widgetHeaderClass+'"><span class="fc-close '+(o?"ui-icon ui-icon-closethick":"fc-icon fc-icon-x")+'"></span><span class="fc-title">'+tt(l)+'</span><div class="fc-clear"/></div><div class="fc-body '+s.widgetContentClass+'"><div class="fc-event-container"></div></div>'),u=a.find(".fc-event-container");for(i=this.renderFgSegEls(i,!0),this.popoverSegs=i,r=0;r<i.length;r++)this.prepareHits(),i[r].hit=this.getCellHit(e,n),this.releaseHits(),u.append(i[r].el);return a},resliceDaySegs:function(e,n){var i=t.map(e,function(t){return t.event}),r=n.clone(),s=r.clone().add(1,"days"),o={start:r,end:s};return e=this.eventsToSegs(i,function(t){var e=F(t,o);return e?[e]:[]}),this.sortEventSegs(e),e},getMoreLinkText:function(t){var e=this.view.opt("eventLimitText");return"function"==typeof e?e(t):"+"+t+" "+e},getCellSegs:function(t,e,n){for(var i,r=this.rowStructs[t].segMatrix,s=n||0,o=[];s<r.length;)i=r[s][e],i&&o.push(i),s++;return o}});var ye=jt.TimeGrid=pe.extend(ve,{slotDuration:null,snapDuration:null,snapsPerSlot:null,minTime:null,maxTime:null,labelFormat:null,labelInterval:null,colEls:null,slatContainerEl:null,slatEls:null,nowIndicatorEls:null,colCoordCache:null,slatCoordCache:null,constructor:function(){pe.apply(this,arguments),this.processOptions()},renderDates:function(){this.el.html(this.renderHtml()),this.colEls=this.el.find(".fc-day"),this.slatContainerEl=this.el.find(".fc-slats"),this.slatEls=this.slatContainerEl.find("tr"),this.colCoordCache=new ce({els:this.colEls,isHorizontal:!0}),this.slatCoordCache=new ce({els:this.slatEls,isVertical:!0}),this.renderContentSkeleton()},renderHtml:function(){return'<div class="fc-bg"><table>'+this.renderBgTrHtml(0)+'</table></div><div class="fc-slats"><table>'+this.renderSlatRowHtml()+"</table></div>"},renderSlatRowHtml:function(){for(var t,n,i,r=this.view,s=this.isRTL,o="",l=e.duration(+this.minTime);l<this.maxTime;)t=this.start.clone().time(l),n=ot(_(l,this.labelInterval)),i='<td class="fc-axis fc-time '+r.widgetContentClass+'" '+r.axisStyleAttr()+">"+(n?"<span>"+tt(t.format(this.labelFormat))+"</span>":"")+"</td>",o+='<tr data-time="'+t.format("HH:mm:ss")+'"'+(n?"":' class="fc-minor"')+">"+(s?"":i)+'<td class="'+r.widgetContentClass+'"/>'+(s?i:"")+"</tr>",l.add(this.slotDuration);return o},processOptions:function(){var n,i=this.view,r=i.opt("slotDuration"),s=i.opt("snapDuration");r=e.duration(r),s=s?e.duration(s):r,this.slotDuration=r,this.snapDuration=s,this.snapsPerSlot=r/s,this.minResizeDuration=s,this.minTime=e.duration(i.opt("minTime")),this.maxTime=e.duration(i.opt("maxTime")),n=i.opt("slotLabelFormat"),t.isArray(n)&&(n=n[n.length-1]),this.labelFormat=n||i.opt("smallTimeFormat"),n=i.opt("slotLabelInterval"),this.labelInterval=n?e.duration(n):this.computeLabelInterval(r)},computeLabelInterval:function(t){var n,i,r;for(n=Ne.length-1;n>=0;n--)if(i=e.duration(Ne[n]),r=_(i,t),ot(r)&&r>1)return i;return e.duration(t)},computeEventTimeFormat:function(){return this.view.opt("noMeridiemTimeFormat")},computeDisplayEventEnd:function(){return!0},prepareHits:function(){this.colCoordCache.build(),this.slatCoordCache.build()},releaseHits:function(){this.colCoordCache.clear()},queryHit:function(t,e){var n=this.snapsPerSlot,i=this.colCoordCache,r=this.slatCoordCache;if(i.isLeftInBounds(t)&&r.isTopInBounds(e)){var s=i.getHorizontalIndex(t),o=r.getVerticalIndex(e);if(null!=s&&null!=o){var l=r.getTopOffset(o),a=r.getHeight(o),u=(e-l)/a,d=Math.floor(u*n),c=o*n+d,h=l+d/n*a,f=l+(d+1)/n*a;return{col:s,snap:c,component:this,left:i.getLeftOffset(s),right:i.getRightOffset(s),top:h,bottom:f}}}},getHitSpan:function(t){var e,n=this.getCellDate(0,t.col),i=this.computeSnapTime(t.snap);return n.time(i),e=n.clone().add(this.snapDuration),{start:n,end:e}},getHitEl:function(t){return this.colEls.eq(t.col)},rangeUpdated:function(){this.updateDayTable()},computeSnapTime:function(t){return e.duration(this.minTime+this.snapDuration*t)},spanToSegs:function(t){var e,n=this.sliceRangeByTimes(t);for(e=0;e<n.length;e++)this.isRTL?n[e].col=this.daysPerRow-1-n[e].dayIndex:n[e].col=n[e].dayIndex;return n},sliceRangeByTimes:function(t){var e,n,i,r,s=[];for(n=0;n<this.daysPerRow;n++)i=this.dayDates[n].clone(),r={start:i.clone().time(this.minTime),end:i.clone().time(this.maxTime)},e=F(t,r),e&&(e.dayIndex=n,s.push(e));return s},updateSize:function(t){this.slatCoordCache.build(),t&&this.updateSegVerticals([].concat(this.fgSegs||[],this.bgSegs||[],this.businessSegs||[]))},getTotalSlatHeight:function(){return this.slatContainerEl.outerHeight()},computeDateTop:function(t,n){return this.computeTimeTop(e.duration(t-n.clone().stripTime()))},computeTimeTop:function(t){var e,n,i=this.slatEls.length,r=(t-this.minTime)/this.slotDuration;return r=Math.max(0,r),r=Math.min(i,r),e=Math.floor(r),e=Math.min(e,i-1),n=r-e,this.slatCoordCache.getTopPosition(e)+this.slatCoordCache.getHeight(e)*n},renderDrag:function(t,e){return e?this.renderEventLocationHelper(t,e):void this.renderHighlight(this.eventToSpan(t))},unrenderDrag:function(){this.unrenderHelper(),this.unrenderHighlight()},renderEventResize:function(t,e){return this.renderEventLocationHelper(t,e)},unrenderEventResize:function(){this.unrenderHelper()},renderHelper:function(t,e){return this.renderHelperSegs(this.eventToSegs(t),e)},unrenderHelper:function(){this.unrenderHelperSegs()},renderBusinessHours:function(){this.renderBusinessSegs(this.buildBusinessHourSegs())},unrenderBusinessHours:function(){this.unrenderBusinessSegs()},getNowIndicatorUnit:function(){return"minute"},renderNowIndicator:function(e){var n,i=this.spanToSegs({start:e,end:e}),r=this.computeDateTop(e,e),s=[];for(n=0;n<i.length;n++)s.push(t('<div class="fc-now-indicator fc-now-indicator-line"></div>').css("top",r).appendTo(this.colContainerEls.eq(i[n].col))[0]);i.length>0&&s.push(t('<div class="fc-now-indicator fc-now-indicator-arrow"></div>').css("top",r).appendTo(this.el.find(".fc-content-skeleton"))[0]),this.nowIndicatorEls=t(s)},unrenderNowIndicator:function(){this.nowIndicatorEls&&(this.nowIndicatorEls.remove(),this.nowIndicatorEls=null)},renderSelection:function(t){this.view.opt("selectHelper")?this.renderEventLocationHelper(t):this.renderHighlight(t)},unrenderSelection:function(){this.unrenderHelper(),this.unrenderHighlight()},renderHighlight:function(t){this.renderHighlightSegs(this.spanToSegs(t))},unrenderHighlight:function(){this.unrenderHighlightSegs()}});ye.mixin({colContainerEls:null,fgContainerEls:null,bgContainerEls:null,helperContainerEls:null,highlightContainerEls:null,businessContainerEls:null,fgSegs:null,bgSegs:null,helperSegs:null,highlightSegs:null,businessSegs:null,renderContentSkeleton:function(){var e,n,i="";for(e=0;e<this.colCnt;e++)i+='<td><div class="fc-content-col"><div class="fc-event-container fc-helper-container"></div><div class="fc-event-container"></div><div class="fc-highlight-container"></div><div class="fc-bgevent-container"></div><div class="fc-business-container"></div></div></td>';n=t('<div class="fc-content-skeleton"><table><tr>'+i+"</tr></table></div>"),this.colContainerEls=n.find(".fc-content-col"),this.helperContainerEls=n.find(".fc-helper-container"),this.fgContainerEls=n.find(".fc-event-container:not(.fc-helper-container)"),this.bgContainerEls=n.find(".fc-bgevent-container"),this.highlightContainerEls=n.find(".fc-highlight-container"),this.businessContainerEls=n.find(".fc-business-container"),this.bookendCells(n.find("tr")),this.el.append(n)},renderFgSegs:function(t){return t=this.renderFgSegsIntoContainers(t,this.fgContainerEls),this.fgSegs=t,t},unrenderFgSegs:function(){this.unrenderNamedSegs("fgSegs")},renderHelperSegs:function(e,n){var i,r,s,o=[];for(e=this.renderFgSegsIntoContainers(e,this.helperContainerEls),i=0;i<e.length;i++)r=e[i],n&&n.col===r.col&&(s=n.el,r.el.css({left:s.css("left"),right:s.css("right"),"margin-left":s.css("margin-left"),"margin-right":s.css("margin-right")})),o.push(r.el[0]);return this.helperSegs=e,t(o)},unrenderHelperSegs:function(){this.unrenderNamedSegs("helperSegs")},renderBgSegs:function(t){return t=this.renderFillSegEls("bgEvent",t),this.updateSegVerticals(t),this.attachSegsByCol(this.groupSegsByCol(t),this.bgContainerEls),this.bgSegs=t,t},unrenderBgSegs:function(){this.unrenderNamedSegs("bgSegs")},renderHighlightSegs:function(t){t=this.renderFillSegEls("highlight",t),this.updateSegVerticals(t),this.attachSegsByCol(this.groupSegsByCol(t),this.highlightContainerEls),this.highlightSegs=t},unrenderHighlightSegs:function(){this.unrenderNamedSegs("highlightSegs")},renderBusinessSegs:function(t){t=this.renderFillSegEls("businessHours",t),this.updateSegVerticals(t),this.attachSegsByCol(this.groupSegsByCol(t),this.businessContainerEls),this.businessSegs=t},unrenderBusinessSegs:function(){this.unrenderNamedSegs("businessSegs")},groupSegsByCol:function(t){var e,n=[];for(e=0;e<this.colCnt;e++)n.push([]);for(e=0;e<t.length;e++)n[t[e].col].push(t[e]);return n},attachSegsByCol:function(t,e){var n,i,r;for(n=0;n<this.colCnt;n++)for(i=t[n],r=0;r<i.length;r++)e.eq(n).append(i[r].el)},unrenderNamedSegs:function(t){var e,n=this[t];if(n){for(e=0;e<n.length;e++)n[e].el.remove();this[t]=null}},renderFgSegsIntoContainers:function(t,e){var n,i;for(t=this.renderFgSegEls(t),n=this.groupSegsByCol(t),i=0;i<this.colCnt;i++)this.updateFgSegCoords(n[i]);return this.attachSegsByCol(n,e),t},fgSegHtml:function(t,e){var n,i,r,s=this.view,o=t.event,l=s.isEventDraggable(o),a=!e&&t.isStart&&s.isEventResizableFromStart(o),u=!e&&t.isEnd&&s.isEventResizableFromEnd(o),d=this.getSegClasses(t,l,a||u),c=nt(this.getSegSkinCss(t));return d.unshift("fc-time-grid-event","fc-v-event"),s.isMultiDayEvent(o)?(t.isStart||t.isEnd)&&(n=this.getEventTimeText(t),i=this.getEventTimeText(t,"LT"),r=this.getEventTimeText(t,null,!1)):(n=this.getEventTimeText(o),i=this.getEventTimeText(o,"LT"),r=this.getEventTimeText(o,null,!1)),'<a class="'+d.join(" ")+'"'+(o.url?' href="'+tt(o.url)+'"':"")+(c?' style="'+c+'"':"")+'><div class="fc-content">'+(n?'<div class="fc-time" data-start="'+tt(r)+'" data-full="'+tt(i)+'"><span>'+tt(n)+"</span></div>":"")+(o.title?'<div class="fc-title">'+tt(o.title)+"</div>":"")+'</div><div class="fc-bg"/>'+(u?'<div class="fc-resizer fc-end-resizer" />':"")+"</a>"},updateSegVerticals:function(t){this.computeSegVerticals(t),this.assignSegVerticals(t)},computeSegVerticals:function(t){var e,n;for(e=0;e<t.length;e++)n=t[e],n.top=this.computeDateTop(n.start,n.start),n.bottom=this.computeDateTop(n.end,n.start)},assignSegVerticals:function(t){var e,n;for(e=0;e<t.length;e++)n=t[e],n.el.css(this.generateSegVerticalCss(n))},generateSegVerticalCss:function(t){return{top:t.top,bottom:-t.bottom}},updateFgSegCoords:function(t){this.computeSegVerticals(t),this.computeFgSegHorizontals(t),this.assignSegVerticals(t),this.assignFgSegHorizontals(t)},computeFgSegHorizontals:function(t){var e,n,i;if(this.sortEventSegs(t),e=zt(t),Ft(e),n=e[0]){for(i=0;i<n.length;i++)Nt(n[i]);for(i=0;i<n.length;i++)this.computeFgSegForwardBack(n[i],0,0)}},computeFgSegForwardBack:function(t,e,n){var i,r=t.forwardSegs;if(void 0===t.forwardCoord)for(r.length?(this.sortForwardSegs(r),this.computeFgSegForwardBack(r[0],e+1,n),t.forwardCoord=r[0].backwardCoord):t.forwardCoord=1,t.backwardCoord=t.forwardCoord-(t.forwardCoord-n)/(e+1),i=0;i<r.length;i++)this.computeFgSegForwardBack(r[i],0,t.forwardCoord)},sortForwardSegs:function(t){t.sort(lt(this,"compareForwardSegs"))},compareForwardSegs:function(t,e){return e.forwardPressure-t.forwardPressure||(t.backwardCoord||0)-(e.backwardCoord||0)||this.compareEventSegs(t,e)},assignFgSegHorizontals:function(t){var e,n;for(e=0;e<t.length;e++)n=t[e],n.el.css(this.generateFgSegHorizontalCss(n)),n.bottom-n.top<30&&n.el.addClass("fc-short")},generateFgSegHorizontalCss:function(t){var e,n,i=this.view.opt("slotEventOverlap"),r=t.backwardCoord,s=t.forwardCoord,o=this.generateSegVerticalCss(t);return i&&(s=Math.min(1,r+2*(s-r))),this.isRTL?(e=1-s,n=r):(e=r,n=1-s),o.zIndex=t.level+1,o.left=100*e+"%",o.right=100*n+"%",i&&t.forwardPressure&&(o[this.isRTL?"marginLeft":"marginRight"]=20),o}});var Se=jt.View=wt.extend(le,ae,{type:null,name:null,title:null,calendar:null,options:null,el:null,displaying:null,isSkeletonRendered:!1,isEventsRendered:!1,start:null,end:null,intervalStart:null,intervalEnd:null,intervalDuration:null,intervalUnit:null,isRTL:!1,isSelected:!1,selectedEvent:null,eventOrderSpecs:null,widgetHeaderClass:null,widgetContentClass:null,highlightStateClass:null,nextDayThreshold:null,isHiddenDayHash:null,isNowIndicatorRendered:null,initialNowDate:null,initialNowQueriedMs:null,nowIndicatorTimeoutID:null,nowIndicatorIntervalID:null,constructor:function(t,n,i,r){this.calendar=t,this.type=this.name=n,this.options=i,this.intervalDuration=r||e.duration(1,"day"),this.nextDayThreshold=e.duration(this.opt("nextDayThreshold")),this.initThemingProps(),this.initHiddenDays(),this.isRTL=this.opt("isRTL"),this.eventOrderSpecs=M(this.opt("eventOrder")),this.initialize()},initialize:function(){},opt:function(t){return this.options[t]},trigger:function(t,e){var n=this.calendar;return n.trigger.apply(n,[t,e||this].concat(Array.prototype.slice.call(arguments,2),[this]))},setDate:function(t){this.setRange(this.computeRange(t))},setRange:function(e){t.extend(this,e),this.updateTitle()},computeRange:function(t){var e,n,i=O(this.intervalDuration),r=t.clone().startOf(i),s=r.clone().add(this.intervalDuration);return/year|month|week|day/.test(i)?(r.stripTime(),s.stripTime()):(r.hasTime()||(r=this.calendar.time(0)),s.hasTime()||(s=this.calendar.time(0))),e=r.clone(),e=this.skipHiddenDays(e),n=s.clone(),n=this.skipHiddenDays(n,-1,!0),{intervalUnit:i,intervalStart:r,intervalEnd:s,start:e,end:n}},computePrevDate:function(t){return this.massageCurrentDate(t.clone().startOf(this.intervalUnit).subtract(this.intervalDuration),-1)},computeNextDate:function(t){return this.massageCurrentDate(t.clone().startOf(this.intervalUnit).add(this.intervalDuration))},massageCurrentDate:function(t,e){return this.intervalDuration.as("days")<=1&&this.isHiddenDay(t)&&(t=this.skipHiddenDays(t,e),t.startOf("day")),t},updateTitle:function(){this.title=this.computeTitle()},computeTitle:function(){return this.formatRange({start:this.calendar.applyTimezone(this.intervalStart),end:this.calendar.applyTimezone(this.intervalEnd)},this.opt("titleFormat")||this.computeTitleFormat(),this.opt("titleRangeSeparator"))},computeTitleFormat:function(){return"year"==this.intervalUnit?"YYYY":"month"==this.intervalUnit?this.opt("monthYearFormat"):this.intervalDuration.as("days")>1?"ll":"LL"},formatRange:function(t,e,n){var i=t.end;return i.hasTime()||(i=i.clone().subtract(1)),pt(t.start,i,e,n,this.opt("isRTL"))},getAllDayHtml:function(){return this.opt("allDayHtml")||tt(this.opt("allDayText"))},buildGotoAnchorHtml:function(e,n,i){var r,s,o,l;return t.isPlainObject(e)?(r=e.date,s=e.type,o=e.forceOff):r=e,r=jt.moment(r),l={date:r.format("YYYY-MM-DD"),type:s||"day"},"string"==typeof n&&(i=n,n=null),n=n?" "+it(n):"",i=i||"",!o&&this.opt("navLinks")?"<a"+n+' data-goto="'+tt(JSON.stringify(l))+'">'+i+"</a>":"<span"+n+">"+i+"</span>"},setElement:function(t){this.el=t,this.bindGlobalHandlers()},removeElement:function(){this.clear(),this.isSkeletonRendered&&(this.unrenderSkeleton(),this.isSkeletonRendered=!1),this.unbindGlobalHandlers(),this.el.remove()},display:function(t,e){var n=this,i=null;return null!=e&&this.displaying&&(i=this.queryScroll()),this.calendar.freezeContentHeight(),ut(this.clear(),function(){return n.displaying=ut(n.displayView(t),function(){null!=e?n.setScroll(e):n.forceScroll(n.computeInitialScroll(i)),n.calendar.unfreezeContentHeight(),n.triggerRender()})})},clear:function(){var e=this,n=this.displaying;return n?ut(n,function(){return e.displaying=null,e.clearEvents(),e.clearView()}):t.when()},displayView:function(t){this.isSkeletonRendered||(this.renderSkeleton(),this.isSkeletonRendered=!0),t&&this.setDate(t),this.render&&this.render(),this.renderDates(),this.updateSize(),this.renderBusinessHours(),this.startNowIndicator()},clearView:function(){this.unselect(),this.stopNowIndicator(),this.triggerUnrender(),this.unrenderBusinessHours(),this.unrenderDates(),this.destroy&&this.destroy()},renderSkeleton:function(){},unrenderSkeleton:function(){},renderDates:function(){},unrenderDates:function(){},triggerRender:function(){this.trigger("viewRender",this,this,this.el)},triggerUnrender:function(){this.trigger("viewDestroy",this,this,this.el)},bindGlobalHandlers:function(){this.listenTo(t(document),"mousedown",this.handleDocumentMousedown),this.listenTo(t(document),"touchstart",this.processUnselect)},unbindGlobalHandlers:function(){this.stopListeningTo(t(document))},initThemingProps:function(){var t=this.opt("theme")?"ui":"fc";this.widgetHeaderClass=t+"-widget-header",this.widgetContentClass=t+"-widget-content",this.highlightStateClass=t+"-state-highlight"},renderBusinessHours:function(){},unrenderBusinessHours:function(){},startNowIndicator:function(){var t,n,i,r=this;this.opt("nowIndicator")&&(t=this.getNowIndicatorUnit(),t&&(n=lt(this,"updateNowIndicator"),this.initialNowDate=this.calendar.getNow(),this.initialNowQueriedMs=+new Date,this.renderNowIndicator(this.initialNowDate),this.isNowIndicatorRendered=!0,i=this.initialNowDate.clone().startOf(t).add(1,t)-this.initialNowDate,this.nowIndicatorTimeoutID=setTimeout(function(){r.nowIndicatorTimeoutID=null,n(),i=+e.duration(1,t),i=Math.max(100,i),r.nowIndicatorIntervalID=setInterval(n,i)},i)))},updateNowIndicator:function(){this.isNowIndicatorRendered&&(this.unrenderNowIndicator(),this.renderNowIndicator(this.initialNowDate.clone().add(new Date-this.initialNowQueriedMs)))},stopNowIndicator:function(){this.isNowIndicatorRendered&&(this.nowIndicatorTimeoutID&&(clearTimeout(this.nowIndicatorTimeoutID),this.nowIndicatorTimeoutID=null),this.nowIndicatorIntervalID&&(clearTimeout(this.nowIndicatorIntervalID),this.nowIndicatorIntervalID=null),this.unrenderNowIndicator(),this.isNowIndicatorRendered=!1)},getNowIndicatorUnit:function(){},renderNowIndicator:function(t){},unrenderNowIndicator:function(){},updateSize:function(t){var e;t&&(e=this.queryScroll()),this.updateHeight(t),this.updateWidth(t),this.updateNowIndicator(),t&&this.setScroll(e)},updateWidth:function(t){},updateHeight:function(t){var e=this.calendar;this.setHeight(e.getSuggestedViewHeight(),e.isHeightAuto())},setHeight:function(t,e){},computeInitialScroll:function(t){return 0},queryScroll:function(){},setScroll:function(t){},forceScroll:function(t){var e=this;this.setScroll(t),setTimeout(function(){e.setScroll(t)},0)},displayEvents:function(t){var e=this.queryScroll();this.clearEvents(),this.renderEvents(t),this.isEventsRendered=!0,this.setScroll(e),this.triggerEventRender()},clearEvents:function(){var t;this.isEventsRendered&&(t=this.queryScroll(),this.triggerEventUnrender(),this.destroyEvents&&this.destroyEvents(),this.unrenderEvents(),this.setScroll(t),this.isEventsRendered=!1)},renderEvents:function(t){},unrenderEvents:function(){},triggerEventRender:function(){this.renderedEventSegEach(function(t){this.trigger("eventAfterRender",t.event,t.event,t.el)}),this.trigger("eventAfterAllRender")},triggerEventUnrender:function(){this.renderedEventSegEach(function(t){this.trigger("eventDestroy",t.event,t.event,t.el)})},resolveEventEl:function(e,n){var i=this.trigger("eventRender",e,e,n);return i===!1?n=null:i&&i!==!0&&(n=t(i)),n},showEvent:function(t){this.renderedEventSegEach(function(t){t.el.css("visibility","")},t)},hideEvent:function(t){this.renderedEventSegEach(function(t){t.el.css("visibility","hidden")},t)},renderedEventSegEach:function(t,e){var n,i=this.getEventSegs();for(n=0;n<i.length;n++)e&&i[n].event._id!==e._id||i[n].el&&t.call(this,i[n])},getEventSegs:function(){return[]},isEventDraggable:function(t){return this.isEventStartEditable(t)},isEventStartEditable:function(t){return J(t.startEditable,(t.source||{}).startEditable,this.opt("eventStartEditable"),this.isEventGenerallyEditable(t))},isEventGenerallyEditable:function(t){return J(t.editable,(t.source||{}).editable,this.opt("editable"))},reportEventDrop:function(t,e,n,i,r){
var s=this.calendar,o=s.mutateEvent(t,e,n),l=function(){o.undo(),s.reportEventChange()};this.triggerEventDrop(t,o.dateDelta,l,i,r),s.reportEventChange()},triggerEventDrop:function(t,e,n,i,r){this.trigger("eventDrop",i[0],t,e,n,r,{})},reportExternalDrop:function(e,n,i,r,s){var o,l,a=e.eventProps;a&&(o=t.extend({},a,n),l=this.calendar.renderEvent(o,e.stick)[0]),this.triggerExternalDrop(l,n,i,r,s)},triggerExternalDrop:function(t,e,n,i,r){this.trigger("drop",n[0],e.start,i,r),t&&this.trigger("eventReceive",null,t)},renderDrag:function(t,e){},unrenderDrag:function(){},isEventResizableFromStart:function(t){return this.opt("eventResizableFromStart")&&this.isEventResizable(t)},isEventResizableFromEnd:function(t){return this.isEventResizable(t)},isEventResizable:function(t){var e=t.source||{};return J(t.durationEditable,e.durationEditable,this.opt("eventDurationEditable"),t.editable,e.editable,this.opt("editable"))},reportEventResize:function(t,e,n,i,r){var s=this.calendar,o=s.mutateEvent(t,e,n),l=function(){o.undo(),s.reportEventChange()};this.triggerEventResize(t,o.durationDelta,l,i,r),s.reportEventChange()},triggerEventResize:function(t,e,n,i,r){this.trigger("eventResize",i[0],t,e,n,r,{})},select:function(t,e){this.unselect(e),this.renderSelection(t),this.reportSelection(t,e)},renderSelection:function(t){},reportSelection:function(t,e){this.isSelected=!0,this.triggerSelect(t,e)},triggerSelect:function(t,e){this.trigger("select",null,this.calendar.applyTimezone(t.start),this.calendar.applyTimezone(t.end),e)},unselect:function(t){this.isSelected&&(this.isSelected=!1,this.destroySelection&&this.destroySelection(),this.unrenderSelection(),this.trigger("unselect",null,t))},unrenderSelection:function(){},selectEvent:function(t){this.selectedEvent&&this.selectedEvent===t||(this.unselectEvent(),this.renderedEventSegEach(function(t){t.el.addClass("fc-selected")},t),this.selectedEvent=t)},unselectEvent:function(){this.selectedEvent&&(this.renderedEventSegEach(function(t){t.el.removeClass("fc-selected")},this.selectedEvent),this.selectedEvent=null)},isEventSelected:function(t){return this.selectedEvent&&this.selectedEvent._id===t._id},handleDocumentMousedown:function(t){S(t)&&this.processUnselect(t)},processUnselect:function(t){this.processRangeUnselect(t),this.processEventUnselect(t)},processRangeUnselect:function(e){var n;this.isSelected&&this.opt("unselectAuto")&&(n=this.opt("unselectCancel"),n&&t(e.target).closest(n).length||this.unselect(e))},processEventUnselect:function(e){this.selectedEvent&&(t(e.target).closest(".fc-selected").length||this.unselectEvent())},triggerDayClick:function(t,e,n){this.trigger("dayClick",e,this.calendar.applyTimezone(t.start),n)},initHiddenDays:function(){var e,n=this.opt("hiddenDays")||[],i=[],r=0;for(this.opt("weekends")===!1&&n.push(0,6),e=0;e<7;e++)(i[e]=t.inArray(e,n)!==-1)||r++;if(!r)throw"invalid hiddenDays";this.isHiddenDayHash=i},isHiddenDay:function(t){return e.isMoment(t)&&(t=t.day()),this.isHiddenDayHash[t]},skipHiddenDays:function(t,e,n){var i=t.clone();for(e=e||1;this.isHiddenDayHash[(i.day()+(n?e:0)+7)%7];)i.add(e,"days");return i},computeDayRange:function(t){var e,n=t.start.clone().stripTime(),i=t.end,r=null;return i&&(r=i.clone().stripTime(),e=+i.time(),e&&e>=this.nextDayThreshold&&r.add(1,"days")),(!i||r<=n)&&(r=n.clone().add(1,"days")),{start:n,end:r}},isMultiDayEvent:function(t){var e=this.computeDayRange(t);return e.end.diff(e.start,"days")>1}}),we=jt.Scroller=wt.extend({el:null,scrollEl:null,overflowX:null,overflowY:null,constructor:function(t){t=t||{},this.overflowX=t.overflowX||t.overflow||"auto",this.overflowY=t.overflowY||t.overflow||"auto"},render:function(){this.el=this.renderEl(),this.applyOverflow()},renderEl:function(){return this.scrollEl=t('<div class="fc-scroller"></div>')},clear:function(){this.setHeight("auto"),this.applyOverflow()},destroy:function(){this.el.remove()},applyOverflow:function(){this.scrollEl.css({"overflow-x":this.overflowX,"overflow-y":this.overflowY})},lockOverflow:function(t){var e=this.overflowX,n=this.overflowY;t=t||this.getScrollbarWidths(),"auto"===e&&(e=t.top||t.bottom||this.scrollEl[0].scrollWidth-1>this.scrollEl[0].clientWidth?"scroll":"hidden"),"auto"===n&&(n=t.left||t.right||this.scrollEl[0].scrollHeight-1>this.scrollEl[0].clientHeight?"scroll":"hidden"),this.scrollEl.css({"overflow-x":e,"overflow-y":n})},setHeight:function(t){this.scrollEl.height(t)},getScrollTop:function(){return this.scrollEl.scrollTop()},setScrollTop:function(t){this.scrollEl.scrollTop(t)},getClientWidth:function(){return this.scrollEl[0].clientWidth},getClientHeight:function(){return this.scrollEl[0].clientHeight},getScrollbarWidths:function(){return p(this.scrollEl)}}),Ee=jt.Calendar=wt.extend({dirDefaults:null,localeDefaults:null,overrides:null,dynamicOverrides:null,options:null,viewSpecCache:null,view:null,header:null,loadingLevel:0,constructor:Ot,initialize:function(){},populateOptionsHash:function(){var t,e,i,r;t=J(this.dynamicOverrides.locale,this.overrides.locale),e=De[t],e||(t=Ee.defaults.locale,e=De[t]||{}),i=J(this.dynamicOverrides.isRTL,this.overrides.isRTL,e.isRTL,Ee.defaults.isRTL),r=i?Ee.rtlDefaults:{},this.dirDefaults=r,this.localeDefaults=e,this.options=n([Ee.defaults,r,e,this.overrides,this.dynamicOverrides]),Vt(this.options)},getViewSpec:function(t){var e=this.viewSpecCache;return e[t]||(e[t]=this.buildViewSpec(t))},getUnitViewSpec:function(e){var n,i,r;if(t.inArray(e,Xt)!=-1)for(n=this.header.getViewsWithButtons(),t.each(jt.views,function(t){n.push(t)}),i=0;i<n.length;i++)if(r=this.getViewSpec(n[i]),r&&r.singleUnit==e)return r},buildViewSpec:function(t){for(var i,r,s,o,l=this.overrides.views||{},a=[],u=[],d=[],c=t;c;)i=Ut[c],r=l[c],c=null,"function"==typeof i&&(i={class:i}),i&&(a.unshift(i),u.unshift(i.defaults||{}),s=s||i.duration,c=c||i.type),r&&(d.unshift(r),s=s||r.duration,c=c||r.type);return i=q(a),i.type=t,!!i.class&&(s&&(s=e.duration(s),s.valueOf()&&(i.duration=s,o=O(s),1===s.as(o)&&(i.singleUnit=o,d.unshift(l[o]||{})))),i.defaults=n(u),i.overrides=n(d),this.buildViewSpecOptions(i),this.buildViewSpecButtonText(i,t),i)},buildViewSpecOptions:function(t){t.options=n([Ee.defaults,t.defaults,this.dirDefaults,this.localeDefaults,this.overrides,t.overrides,this.dynamicOverrides]),Vt(t.options)},buildViewSpecButtonText:function(t,e){function n(n){var i=n.buttonText||{};return i[e]||(t.buttonTextKey?i[t.buttonTextKey]:null)||(t.singleUnit?i[t.singleUnit]:null)}t.buttonTextOverride=n(this.dynamicOverrides)||n(this.overrides)||t.overrides.buttonText,t.buttonTextDefault=n(this.localeDefaults)||n(this.dirDefaults)||t.defaults.buttonText||n(Ee.defaults)||(t.duration?this.humanizeDuration(t.duration):null)||e},instantiateView:function(t){var e=this.getViewSpec(t);return new e.class(this,t,e.options,e.duration)},isValidViewType:function(t){return Boolean(this.getViewSpec(t))},pushLoading:function(){this.loadingLevel++||this.trigger("loading",null,!0,this.view)},popLoading:function(){--this.loadingLevel||this.trigger("loading",null,!1,this.view)},buildSelectSpan:function(t,e){var n,i=this.moment(t).stripZone();return n=e?this.moment(e).stripZone():i.hasTime()?i.clone().add(this.defaultTimedEventDuration):i.clone().add(this.defaultAllDayEventDuration),{start:i,end:n}}});Ee.mixin(le),Ee.mixin({optionHandlers:null,bindOption:function(t,e){this.bindOptions([t],e)},bindOptions:function(t,e){var n,i={func:e,names:t};for(n=0;n<t.length;n++)this.registerOptionHandlerObj(t[n],i);this.triggerOptionHandlerObj(i)},registerOptionHandlerObj:function(t,e){(this.optionHandlers[t]||(this.optionHandlers[t]=[])).push(e)},triggerOptionHandlers:function(t){var e,n=this.optionHandlers[t]||[];for(e=0;e<n.length;e++)this.triggerOptionHandlerObj(n[e])},triggerOptionHandlerObj:function(t){var e,n=t.names,i=[];for(e=0;e<n.length;e++)i.push(this.options[n[e]]);t.func.apply(this,i)}}),Ee.defaults={titleRangeSeparator:" – ",monthYearFormat:"MMMM YYYY",defaultTimedEventDuration:"02:00:00",defaultAllDayEventDuration:{days:1},forceEventDuration:!1,nextDayThreshold:"09:00:00",defaultView:"month",aspectRatio:1.35,header:{left:"title",center:"",right:"today prev,next"},weekends:!0,weekNumbers:!1,weekNumberTitle:"W",weekNumberCalculation:"local",scrollTime:"06:00:00",lazyFetching:!0,startParam:"start",endParam:"end",timezoneParam:"timezone",timezone:!1,isRTL:!1,buttonText:{prev:"prev",next:"next",prevYear:"prev year",nextYear:"next year",year:"year",today:"today",month:"month",week:"week",day:"day"},buttonIcons:{prev:"left-single-arrow",next:"right-single-arrow",prevYear:"left-double-arrow",nextYear:"right-double-arrow"},allDayText:"all-day",theme:!1,themeButtonIcons:{prev:"circle-triangle-w",next:"circle-triangle-e",prevYear:"seek-prev",nextYear:"seek-next"},dragOpacity:.75,dragRevertDuration:500,dragScroll:!0,unselectAuto:!0,dropAccept:"*",eventOrder:"title",eventLimit:!1,eventLimitText:"more",eventLimitClick:"popover",dayPopoverFormat:"LL",handleWindowResize:!0,windowResizeDelay:100,longPressDelay:1e3},Ee.englishDefaults={dayPopoverFormat:"dddd, MMMM D"},Ee.rtlDefaults={header:{left:"next,prev today",center:"",right:"title"},buttonIcons:{prev:"right-single-arrow",next:"left-single-arrow",prevYear:"right-double-arrow",nextYear:"left-double-arrow"},themeButtonIcons:{prev:"circle-triangle-e",next:"circle-triangle-w",nextYear:"seek-prev",prevYear:"seek-next"}};var De=jt.locales={};jt.datepickerLocale=function(e,n,i){var r=De[e]||(De[e]={});r.isRTL=i.isRTL,r.weekNumberTitle=i.weekHeader,t.each(be,function(t,e){r[t]=e(i)}),t.datepicker&&(t.datepicker.regional[n]=t.datepicker.regional[e]=i,t.datepicker.regional.en=t.datepicker.regional[""],t.datepicker.setDefaults(i))},jt.locale=function(e,i){var r,s;r=De[e]||(De[e]={}),i&&(r=De[e]=n([r,i])),s=Pt(e),t.each(Ce,function(t,e){null==r[t]&&(r[t]=e(s,r))}),Ee.defaults.locale=e};var be={buttonText:function(t){return{prev:et(t.prevText),next:et(t.nextText),today:et(t.currentText)}},monthYearFormat:function(t){return t.showMonthAfterYear?"YYYY["+t.yearSuffix+"] MMMM":"MMMM YYYY["+t.yearSuffix+"]"}},Ce={dayOfMonthFormat:function(t,e){var n=t.longDateFormat("l");return n=n.replace(/^Y+[^\w\s]*|[^\w\s]*Y+$/g,""),e.isRTL?n+=" ddd":n="ddd "+n,n},mediumTimeFormat:function(t){return t.longDateFormat("LT").replace(/\s*a$/i,"a")},smallTimeFormat:function(t){return t.longDateFormat("LT").replace(":mm","(:mm)").replace(/(\Wmm)$/,"($1)").replace(/\s*a$/i,"a")},extraSmallTimeFormat:function(t){return t.longDateFormat("LT").replace(":mm","(:mm)").replace(/(\Wmm)$/,"($1)").replace(/\s*a$/i,"t")},hourFormat:function(t){return t.longDateFormat("LT").replace(":mm","").replace(/(\Wmm)$/,"").replace(/\s*a$/i,"a")},noMeridiemTimeFormat:function(t){return t.longDateFormat("LT").replace(/\s*a$/i,"")}},He={smallDayDateFormat:function(t){return t.isRTL?"D dd":"dd D"},weekFormat:function(t){return t.isRTL?"w[ "+t.weekNumberTitle+"]":"["+t.weekNumberTitle+" ]w"},smallWeekFormat:function(t){return t.isRTL?"w["+t.weekNumberTitle+"]":"["+t.weekNumberTitle+"]w"}};jt.locale("en",Ee.englishDefaults),jt.sourceNormalizers=[],jt.sourceFetchers=[];var Te={dataType:"json",cache:!1},xe=1;Ee.prototype.normalizeEvent=function(t){},Ee.prototype.spanContainsSpan=function(t,e){var n=t.start.clone().stripZone(),i=this.getEventEnd(t).stripZone();return e.start>=n&&e.end<=i},Ee.prototype.getPeerEvents=function(t,e){var n,i,r=this.getEventCache(),s=[];for(n=0;n<r.length;n++)i=r[n],e&&e._id===i._id||s.push(i);return s},Ee.prototype.isEventSpanAllowed=function(t,e){var n=e.source||{},i=J(e.constraint,n.constraint,this.options.eventConstraint),r=J(e.overlap,n.overlap,this.options.eventOverlap);return this.isSpanAllowed(t,i,r,e)&&(!this.options.eventAllow||this.options.eventAllow(t,e)!==!1)},Ee.prototype.isExternalSpanAllowed=function(e,n,i){var r,s;return i&&(r=t.extend({},i,n),s=this.expandEvent(this.buildEventFromInput(r))[0]),s?this.isEventSpanAllowed(e,s):this.isSelectionSpanAllowed(e)},Ee.prototype.isSelectionSpanAllowed=function(t){return this.isSpanAllowed(t,this.options.selectConstraint,this.options.selectOverlap)&&(!this.options.selectAllow||this.options.selectAllow(t)!==!1)},Ee.prototype.isSpanAllowed=function(t,e,n,i){var r,s,o,l,a,u;if(null!=e&&(r=this.constraintToEvents(e))){for(s=!1,l=0;l<r.length;l++)if(this.spanContainsSpan(r[l],t)){s=!0;break}if(!s)return!1}for(o=this.getPeerEvents(t,i),l=0;l<o.length;l++)if(a=o[l],this.eventIntersectsRange(a,t)){if(n===!1)return!1;if("function"==typeof n&&!n(a,i))return!1;if(i){if(u=J(a.overlap,(a.source||{}).overlap),u===!1)return!1;if("function"==typeof u&&!u(i,a))return!1}}return!0},Ee.prototype.constraintToEvents=function(t){return"businessHours"===t?this.getCurrentBusinessHourEvents():"object"==typeof t?null!=t.start?this.expandEvent(this.buildEventFromInput(t)):null:this.clientEvents(t)},Ee.prototype.eventIntersectsRange=function(t,e){var n=t.start.clone().stripZone(),i=this.getEventEnd(t).stripZone();return e.start<i&&e.end>n};var Re={id:"_fcBusinessHours",start:"09:00",end:"17:00",dow:[1,2,3,4,5],rendering:"inverse-background"};Ee.prototype.getCurrentBusinessHourEvents=function(t){return this.computeBusinessHourEvents(t,this.options.businessHours)},Ee.prototype.computeBusinessHourEvents=function(e,n){return n===!0?this.expandBusinessHourEvents(e,[{}]):t.isPlainObject(n)?this.expandBusinessHourEvents(e,[n]):t.isArray(n)?this.expandBusinessHourEvents(e,n,!0):[]},Ee.prototype.expandBusinessHourEvents=function(e,n,i){var r,s,o=this.getView(),l=[];for(r=0;r<n.length;r++)s=n[r],i&&!s.dow||(s=t.extend({},Re,s),e&&(s.start=null,s.end=null),l.push.apply(l,this.expandEvent(this.buildEventFromInput(s),o.start,o.end)));return l};var Ie=jt.BasicView=Se.extend({scroller:null,dayGridClass:me,dayGrid:null,dayNumbersVisible:!1,colWeekNumbersVisible:!1,cellWeekNumbersVisible:!1,weekNumberWidth:null,headContainerEl:null,headRowEl:null,initialize:function(){this.dayGrid=this.instantiateDayGrid(),this.scroller=new we({overflowX:"hidden",overflowY:"auto"})},instantiateDayGrid:function(){var t=this.dayGridClass.extend(ke);return new t(this)},setRange:function(t){Se.prototype.setRange.call(this,t),this.dayGrid.breakOnWeeks=/year|month|week/.test(this.intervalUnit),this.dayGrid.setRange(t)},computeRange:function(t){var e=Se.prototype.computeRange.call(this,t);return/year|month/.test(e.intervalUnit)&&(e.start.startOf("week"),e.start=this.skipHiddenDays(e.start),e.end.weekday()&&(e.end.add(1,"week").startOf("week"),e.end=this.skipHiddenDays(e.end,-1,!0))),e},renderDates:function(){this.dayNumbersVisible=this.dayGrid.rowCnt>1,this.opt("weekNumbers")&&(this.opt("weekNumbersWithinDays")?(this.cellWeekNumbersVisible=!0,this.colWeekNumbersVisible=!1):(this.cellWeekNumbersVisible=!1,this.colWeekNumbersVisible=!0)),this.dayGrid.numbersVisible=this.dayNumbersVisible||this.cellWeekNumbersVisible||this.colWeekNumbersVisible,this.el.addClass("fc-basic-view").html(this.renderSkeletonHtml()),this.renderHead(),this.scroller.render();var e=this.scroller.el.addClass("fc-day-grid-container"),n=t('<div class="fc-day-grid" />').appendTo(e);this.el.find(".fc-body > tr > td").append(e),this.dayGrid.setElement(n),this.dayGrid.renderDates(this.hasRigidRows())},renderHead:function(){this.headContainerEl=this.el.find(".fc-head-container").html(this.dayGrid.renderHeadHtml()),this.headRowEl=this.headContainerEl.find(".fc-row")},unrenderDates:function(){this.dayGrid.unrenderDates(),this.dayGrid.removeElement(),this.scroller.destroy()},renderBusinessHours:function(){this.dayGrid.renderBusinessHours()},unrenderBusinessHours:function(){this.dayGrid.unrenderBusinessHours()},renderSkeletonHtml:function(){return'<table><thead class="fc-head"><tr><td class="fc-head-container '+this.widgetHeaderClass+'"></td></tr></thead><tbody class="fc-body"><tr><td class="'+this.widgetContentClass+'"></td></tr></tbody></table>'},weekNumberStyleAttr:function(){return null!==this.weekNumberWidth?'style="width:'+this.weekNumberWidth+'px"':""},hasRigidRows:function(){var t=this.opt("eventLimit");return t&&"number"!=typeof t},updateWidth:function(){this.colWeekNumbersVisible&&(this.weekNumberWidth=u(this.el.find(".fc-week-number")))},setHeight:function(t,e){var n,s,o=this.opt("eventLimit");this.scroller.clear(),r(this.headRowEl),this.dayGrid.removeSegPopover(),o&&"number"==typeof o&&this.dayGrid.limitRows(o),n=this.computeScrollerHeight(t),this.setGridHeight(n,e),o&&"number"!=typeof o&&this.dayGrid.limitRows(o),e||(this.scroller.setHeight(n),s=this.scroller.getScrollbarWidths(),(s.left||s.right)&&(i(this.headRowEl,s),n=this.computeScrollerHeight(t),this.scroller.setHeight(n)),this.scroller.lockOverflow(s))},computeScrollerHeight:function(t){return t-d(this.el,this.scroller.el)},setGridHeight:function(t,e){e?a(this.dayGrid.rowEls):l(this.dayGrid.rowEls,t,!0)},queryScroll:function(){return this.scroller.getScrollTop()},setScroll:function(t){this.scroller.setScrollTop(t)},prepareHits:function(){this.dayGrid.prepareHits()},releaseHits:function(){this.dayGrid.releaseHits()},queryHit:function(t,e){return this.dayGrid.queryHit(t,e)},getHitSpan:function(t){return this.dayGrid.getHitSpan(t)},getHitEl:function(t){return this.dayGrid.getHitEl(t)},renderEvents:function(t){this.dayGrid.renderEvents(t),this.updateHeight()},getEventSegs:function(){return this.dayGrid.getEventSegs()},unrenderEvents:function(){this.dayGrid.unrenderEvents()},renderDrag:function(t,e){return this.dayGrid.renderDrag(t,e)},unrenderDrag:function(){this.dayGrid.unrenderDrag()},renderSelection:function(t){this.dayGrid.renderSelection(t)},unrenderSelection:function(){this.dayGrid.unrenderSelection()}}),ke={renderHeadIntroHtml:function(){var t=this.view;return t.colWeekNumbersVisible?'<th class="fc-week-number '+t.widgetHeaderClass+'" '+t.weekNumberStyleAttr()+"><span>"+tt(t.opt("weekNumberTitle"))+"</span></th>":""},renderNumberIntroHtml:function(t){var e=this.view,n=this.getCellDate(t,0);return e.colWeekNumbersVisible?'<td class="fc-week-number" '+e.weekNumberStyleAttr()+">"+e.buildGotoAnchorHtml({date:n,type:"week",forceOff:1===this.colCnt},n.format("w"))+"</td>":""},renderBgIntroHtml:function(){var t=this.view;return t.colWeekNumbersVisible?'<td class="fc-week-number '+t.widgetContentClass+'" '+t.weekNumberStyleAttr()+"></td>":""},renderIntroHtml:function(){var t=this.view;return t.colWeekNumbersVisible?'<td class="fc-week-number" '+t.weekNumberStyleAttr()+"></td>":""}},Me=jt.MonthView=Ie.extend({computeRange:function(t){var e,n=Ie.prototype.computeRange.call(this,t);return this.isFixedWeeks()&&(e=Math.ceil(n.end.diff(n.start,"weeks",!0)),n.end.add(6-e,"weeks")),n},setGridHeight:function(t,e){e&&(t*=this.rowCnt/6),l(this.dayGrid.rowEls,t,!e)},isFixedWeeks:function(){return this.opt("fixedWeekCount")}});Ut.basic={class:Ie},Ut.basicDay={type:"basic",duration:{days:1}},Ut.basicWeek={type:"basic",duration:{weeks:1}},Ut.month={class:Me,duration:{months:1},defaults:{fixedWeekCount:!0}};var Le=jt.AgendaView=Se.extend({scroller:null,timeGridClass:ye,timeGrid:null,dayGridClass:me,dayGrid:null,axisWidth:null,headContainerEl:null,noScrollRowEls:null,bottomRuleEl:null,initialize:function(){this.timeGrid=this.instantiateTimeGrid(),this.opt("allDaySlot")&&(this.dayGrid=this.instantiateDayGrid()),this.scroller=new we({overflowX:"hidden",overflowY:"auto"})},instantiateTimeGrid:function(){var t=this.timeGridClass.extend(Be);return new t(this)},instantiateDayGrid:function(){var t=this.dayGridClass.extend(ze);return new t(this)},setRange:function(t){Se.prototype.setRange.call(this,t),this.timeGrid.setRange(t),this.dayGrid&&this.dayGrid.setRange(t)},renderDates:function(){this.el.addClass("fc-agenda-view").html(this.renderSkeletonHtml()),this.renderHead(),this.scroller.render();var e=this.scroller.el.addClass("fc-time-grid-container"),n=t('<div class="fc-time-grid" />').appendTo(e);this.el.find(".fc-body > tr > td").append(e),this.timeGrid.setElement(n),this.timeGrid.renderDates(),this.bottomRuleEl=t('<hr class="fc-divider '+this.widgetHeaderClass+'"/>').appendTo(this.timeGrid.el),this.dayGrid&&(this.dayGrid.setElement(this.el.find(".fc-day-grid")),this.dayGrid.renderDates(),this.dayGrid.bottomCoordPadding=this.dayGrid.el.next("hr").outerHeight()),this.noScrollRowEls=this.el.find(".fc-row:not(.fc-scroller *)")},renderHead:function(){this.headContainerEl=this.el.find(".fc-head-container").html(this.timeGrid.renderHeadHtml())},unrenderDates:function(){this.timeGrid.unrenderDates(),this.timeGrid.removeElement(),this.dayGrid&&(this.dayGrid.unrenderDates(),this.dayGrid.removeElement()),this.scroller.destroy()},renderSkeletonHtml:function(){return'<table><thead class="fc-head"><tr><td class="fc-head-container '+this.widgetHeaderClass+'"></td></tr></thead><tbody class="fc-body"><tr><td class="'+this.widgetContentClass+'">'+(this.dayGrid?'<div class="fc-day-grid"/><hr class="fc-divider '+this.widgetHeaderClass+'"/>':"")+"</td></tr></tbody></table>"},axisStyleAttr:function(){return null!==this.axisWidth?'style="width:'+this.axisWidth+'px"':""},renderBusinessHours:function(){this.timeGrid.renderBusinessHours(),this.dayGrid&&this.dayGrid.renderBusinessHours()},unrenderBusinessHours:function(){this.timeGrid.unrenderBusinessHours(),this.dayGrid&&this.dayGrid.unrenderBusinessHours()},getNowIndicatorUnit:function(){return this.timeGrid.getNowIndicatorUnit()},renderNowIndicator:function(t){this.timeGrid.renderNowIndicator(t)},unrenderNowIndicator:function(){this.timeGrid.unrenderNowIndicator()},updateSize:function(t){this.timeGrid.updateSize(t),Se.prototype.updateSize.call(this,t)},updateWidth:function(){this.axisWidth=u(this.el.find(".fc-axis"))},setHeight:function(t,e){var n,s,o;this.bottomRuleEl.hide(),this.scroller.clear(),r(this.noScrollRowEls),this.dayGrid&&(this.dayGrid.removeSegPopover(),n=this.opt("eventLimit"),n&&"number"!=typeof n&&(n=Fe),n&&this.dayGrid.limitRows(n)),e||(s=this.computeScrollerHeight(t),this.scroller.setHeight(s),o=this.scroller.getScrollbarWidths(),(o.left||o.right)&&(i(this.noScrollRowEls,o),s=this.computeScrollerHeight(t),this.scroller.setHeight(s)),this.scroller.lockOverflow(o),this.timeGrid.getTotalSlatHeight()<s&&this.bottomRuleEl.show())},computeScrollerHeight:function(t){return t-d(this.el,this.scroller.el)},computeInitialScroll:function(){var t=e.duration(this.opt("scrollTime")),n=this.timeGrid.computeTimeTop(t);return n=Math.ceil(n),n&&n++,n},queryScroll:function(){return this.scroller.getScrollTop()},setScroll:function(t){this.scroller.setScrollTop(t)},prepareHits:function(){this.timeGrid.prepareHits(),this.dayGrid&&this.dayGrid.prepareHits()},releaseHits:function(){this.timeGrid.releaseHits(),this.dayGrid&&this.dayGrid.releaseHits()},queryHit:function(t,e){var n=this.timeGrid.queryHit(t,e);return!n&&this.dayGrid&&(n=this.dayGrid.queryHit(t,e)),n},getHitSpan:function(t){return t.component.getHitSpan(t)},getHitEl:function(t){return t.component.getHitEl(t)},renderEvents:function(t){var e,n,i=[],r=[],s=[];for(n=0;n<t.length;n++)t[n].allDay?i.push(t[n]):r.push(t[n]);e=this.timeGrid.renderEvents(r),this.dayGrid&&(s=this.dayGrid.renderEvents(i)),this.updateHeight()},getEventSegs:function(){return this.timeGrid.getEventSegs().concat(this.dayGrid?this.dayGrid.getEventSegs():[])},unrenderEvents:function(){this.timeGrid.unrenderEvents(),this.dayGrid&&this.dayGrid.unrenderEvents()},renderDrag:function(t,e){return t.start.hasTime()?this.timeGrid.renderDrag(t,e):this.dayGrid?this.dayGrid.renderDrag(t,e):void 0},unrenderDrag:function(){this.timeGrid.unrenderDrag(),this.dayGrid&&this.dayGrid.unrenderDrag()},renderSelection:function(t){t.start.hasTime()||t.end.hasTime()?this.timeGrid.renderSelection(t):this.dayGrid&&this.dayGrid.renderSelection(t)},unrenderSelection:function(){this.timeGrid.unrenderSelection(),this.dayGrid&&this.dayGrid.unrenderSelection()}}),Be={renderHeadIntroHtml:function(){var t,e=this.view;return e.opt("weekNumbers")?(t=this.start.format(e.opt("smallWeekFormat")),'<th class="fc-axis fc-week-number '+e.widgetHeaderClass+'" '+e.axisStyleAttr()+">"+e.buildGotoAnchorHtml({date:this.start,type:"week",forceOff:this.colCnt>1},tt(t))+"</th>"):'<th class="fc-axis '+e.widgetHeaderClass+'" '+e.axisStyleAttr()+"></th>"},renderBgIntroHtml:function(){var t=this.view;return'<td class="fc-axis '+t.widgetContentClass+'" '+t.axisStyleAttr()+"></td>"},renderIntroHtml:function(){var t=this.view;return'<td class="fc-axis" '+t.axisStyleAttr()+"></td>"}},ze={renderBgIntroHtml:function(){var t=this.view;return'<td class="fc-axis '+t.widgetContentClass+'" '+t.axisStyleAttr()+"><span>"+t.getAllDayHtml()+"</span></td>"},renderIntroHtml:function(){var t=this.view;return'<td class="fc-axis" '+t.axisStyleAttr()+"></td>"}},Fe=5,Ne=[{hours:1},{minutes:30},{minutes:15},{seconds:30},{seconds:15}];Ut.agenda={class:Le,defaults:{allDaySlot:!0,slotDuration:"00:30:00",minTime:"00:00:00",maxTime:"24:00:00",slotEventOverlap:!0}},Ut.agendaDay={type:"agenda",duration:{days:1}},Ut.agendaWeek={type:"agenda",duration:{weeks:1}};var Ge=Se.extend({grid:null,scroller:null,initialize:function(){this.grid=new Ae(this),this.scroller=new we({overflowX:"hidden",overflowY:"auto"})},setRange:function(t){Se.prototype.setRange.call(this,t),this.grid.setRange(t)},renderSkeleton:function(){this.el.addClass("fc-list-view "+this.widgetContentClass),this.scroller.render(),this.scroller.el.appendTo(this.el),this.grid.setElement(this.scroller.scrollEl)},unrenderSkeleton:function(){this.scroller.destroy()},setHeight:function(t,e){this.scroller.setHeight(this.computeScrollerHeight(t))},computeScrollerHeight:function(t){return t-d(this.el,this.scroller.el)},renderEvents:function(t){this.grid.renderEvents(t)},unrenderEvents:function(){this.grid.unrenderEvents()},isEventResizable:function(t){return!1},isEventDraggable:function(t){return!1}}),Ae=pe.extend({segSelector:".fc-list-item",hasDayInteractions:!1,spanToSegs:function(t){for(var e,n=this.view,i=n.start.clone().time(0),r=0,s=[];i<n.end;)if(e=F(t,{start:i,end:i.clone().add(1,"day")}),e&&(e.dayIndex=r,s.push(e)),i.add(1,"day"),r++,e&&!e.isEnd&&t.end.hasTime()&&t.end<i.clone().add(this.view.nextDayThreshold)){e.end=t.end.clone(),e.isEnd=!0;break}return s},computeEventTimeFormat:function(){return this.view.opt("mediumTimeFormat")},handleSegClick:function(e,n){var i;pe.prototype.handleSegClick.apply(this,arguments),t(n.target).closest("a[href]").length||(i=e.event.url,i&&!n.isDefaultPrevented()&&(window.location.href=i))},renderFgSegs:function(t){return t=this.renderFgSegEls(t),t.length?this.renderSegList(t):this.renderEmptyMessage(),t},renderEmptyMessage:function(){this.el.html('<div class="fc-list-empty-wrap2"><div class="fc-list-empty-wrap1"><div class="fc-list-empty">'+tt(this.view.opt("noEventsMessage"))+"</div></div></div>")},renderSegList:function(e){var n,i,r,s=this.groupSegsByDay(e),o=t('<table class="fc-list-table"><tbody/></table>'),l=o.find("tbody");for(n=0;n<s.length;n++)if(i=s[n])for(l.append(this.dayHeaderHtml(this.view.start.clone().add(n,"days"))),this.sortEventSegs(i),r=0;r<i.length;r++)l.append(i[r].el);this.el.empty().append(o)},groupSegsByDay:function(t){var e,n,i=[];for(e=0;e<t.length;e++)n=t[e],(i[n.dayIndex]||(i[n.dayIndex]=[])).push(n);return i},dayHeaderHtml:function(t){var e=this.view,n=e.opt("listDayFormat"),i=e.opt("listDayAltFormat");return'<tr class="fc-list-heading" data-date="'+t.format("YYYY-MM-DD")+'"><td class="'+e.widgetHeaderClass+'" colspan="3">'+(n?e.buildGotoAnchorHtml(t,{class:"fc-list-heading-main"},tt(t.format(n))):"")+(i?e.buildGotoAnchorHtml(t,{class:"fc-list-heading-alt"},tt(t.format(i))):"")+"</td></tr>"},fgSegHtml:function(t){var e,n=this.view,i=["fc-list-item"].concat(this.getSegCustomClasses(t)),r=this.getSegBackgroundColor(t),s=t.event,o=s.url;return e=s.allDay?n.getAllDayHtml():n.isMultiDayEvent(s)?t.isStart||t.isEnd?tt(this.getEventTimeText(t)):n.getAllDayHtml():tt(this.getEventTimeText(s)),o&&i.push("fc-has-url"),'<tr class="'+i.join(" ")+'">'+(this.displayEventTime?'<td class="fc-list-item-time '+n.widgetContentClass+'">'+(e||"")+"</td>":"")+'<td class="fc-list-item-marker '+n.widgetContentClass+'"><span class="fc-event-dot"'+(r?' style="background-color:'+r+'"':"")+'></span></td><td class="fc-list-item-title '+n.widgetContentClass+'"><a'+(o?' href="'+tt(o)+'"':"")+">"+tt(t.event.title||"")+"</a></td></tr>"}});return Ut.list={class:Ge,buttonTextKey:"list",defaults:{buttonText:"list",listDayFormat:"LL",noEventsMessage:"No events to display"}},Ut.listDay={type:"list",duration:{days:1},defaults:{listDayFormat:"dddd"}},Ut.listWeek={type:"list",duration:{weeks:1},defaults:{listDayFormat:"dddd",listDayAltFormat:"LL"}},Ut.listMonth={type:"list",duration:{month:1},defaults:{listDayAltFormat:"dddd"}},Ut.listYear={type:"list",duration:{year:1},defaults:{listDayAltFormat:"dddd"}},jt});