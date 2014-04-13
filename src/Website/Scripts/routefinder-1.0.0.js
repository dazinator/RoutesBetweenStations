// This is a simple *viewmodel* - JavaScript that defines the data and behavior of your UI
function RouteFinderViewModel() {
    var self = this;

    self.startingStation = ko.observable("");
    self.endingStation = ko.observable("");

    self.stationsList = ko.observableArray();
    self.isLoading = ko.observable(true);
    self.isRouteLoading = ko.observable(false);

    self.routeInfo = ko.observable();

    self.selectStartStationList = ko.computed(function () {
        // should be all the stations except the one selected as the end station (if one selected.)
        return ko.utils.arrayFilter(this.stationsList(), function (item) {
            return item !== self.endingStation();
        });
    }, self);

    self.selectEndStationList = ko.computed(function () {
        // should be all the stations except the one selected as the start station (if one selected.)
        return ko.utils.arrayFilter(this.stationsList(), function (item) {
            return item !== self.startingStation();
        });
    }, self);

    self.hasSelectedEndingStation = ko.computed(function () {
        var ending = self.endingStation();
        return ending !== undefined && ending !== "";
    }, self);

    self.hasSelectedStartingStation = ko.computed(function () {
        var starting = self.startingStation();
        return starting !== undefined && starting !== "";
    }, self);
    
    self.hasBothStationsSelected = ko.computed(function () {
        return self.hasSelectedStartingStation() && self.hasSelectedEndingStation();
    }, self);

    self.isStartingSelectVisible = ko.computed(function () {
        // it's visible as soon as the stations are loaded.
        return !self.isLoading();
    }, self);

    self.isEndingSelectVisible = ko.computed(function () {
        // its visible only once a starting station has been selected.
        var isStartSelected = self.hasSelectedStartingStation();
        return isStartSelected;
    }, self);

    self.isRouteInfoVisible = ko.computed(function () {
        // its visible only once a starting station has been selected.
        return self.hasBothStationsSelected();
    }, self);

    $.getJSON("/Wcf/RoutesService.svc/getstations", function (allData) {
        // var data = ko.mapping.fromJSON(allData);
        self.stationsList(allData);
        self.isLoading(false);
    });

    self.getRoute = ko.computed(function () {
        if (self.hasBothStationsSelected()) {
            // do ajax to get route.
            self.isRouteLoading(true);
            var startStation = self.startingStation();
            var endStation = self.endingStation();
            var uriTemplate = "/Wcf/RoutesService.svc/findroute?fromStation={fromStation}&toStation={toStation}";
            var uri = uriTemplate.replace("{fromStation}", startStation).replace("{toStation}", endStation);
            
            $.ajax({
                url: uri,
                dataType: 'json',
                success: function (result) {
                    var routeModel = ko.mapping.fromJS(result);
                    self.routeInfo(routeModel);
                },
                error: function (request, textStatus, errorThrown) {
                    alert(textStatus);
                },
                complete: function (request, textStatus) { //for additional info
                    self.isRouteLoading(false);
                }
            });
        }
    }, self);

}

// Activates knockout.js
ko.applyBindings(new RouteFinderViewModel());