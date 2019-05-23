(function () {
    
    "use strict";
    angular.module("CRMApp.Services")
            .service("EventTypeService", ["$http",
            function ($http) {
                var EventTypeModel = {};


                var _AddEventType = function (EventTypeData) {
                    return $http({
                        method: "POST",
                        url: "/Master/EventType/SaveEventType",
                        data: EventTypeData
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                var _DeleteEventType = function (EventTypeId) {
                    return $http({
                        method: "POST",
                        url: "/Master/EventType/DeleteEventType",
                        data: '{id:"' + EventTypeId + '"}'
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })

                }
                var _UpdateEventType = function (EventTypeData) {
                    return $http({
                        method: "POST",
                        url: "/Master/EventType/UpdateEventType",
                        data: EventTypeData
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                EventTypeModel.AddEventType = _AddEventType;
                EventTypeModel.Update = _UpdateEventType;
                EventTypeModel.DeleteEventType = _DeleteEventType;
                return EventTypeModel


            }])
})()
