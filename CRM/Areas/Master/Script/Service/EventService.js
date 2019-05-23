(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("EventService", ["$http",
            function ($http) {
                var EventViewModel = {};


                var _getAllEventType = function ()
                {
                    return $http({
                        method: "GET",
                        url: "/Master/Event/GetAllEvent",
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }


                var _addEvent = function (Event) {
                    return $http({
                        method: "POST",
                        url: "/Master/Event/SaveEvent",
                        data: Event
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                var _Deleteevent = function (eventid) {
                    return $http({
                        method: "POST",
                        url: "/Master/Event/DeleteEvent",
                        data: '{id:"' + eventid + '"}'
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })

                }
                var _UpdateBank = function (Eventdata) {
                    return $http({
                        method: "POST",
                        url: "/Master/Event/UpdateEvent",
                        data: Eventdata
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                EventViewModel.AddEventdata = _addEvent;
                EventViewModel.Update = _UpdateBank;
                EventViewModel.DeleteEvent = _Deleteevent;
                EventViewModel.getAllEventType = _getAllEventType;
                return EventViewModel


            }])
})()
