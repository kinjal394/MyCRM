(function () {

    "use strict";
    angular.module("CRMApp.Services")
            .service("DocumentService", ["$http",
            function ($http) {
                var EventTypeModel = {};


                var _AddDoc = function (docdata) {
                    return $http({
                        method: "POST",
                        url: "/Master/Document/SaveDoc",
                        data: docdata
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                var _DeleteDoc = function (docid) {
                    return $http({
                        method: "POST",
                        url: "/Master/Document/DeleteDoc",
                        data: '{id:"' + docid + '"}'
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })

                }
                var _UpdateDoc = function (docobj) {
                    return $http({
                        method: "POST",
                        url: "/Master/Document/UpdateDoc",
                        data: docobj
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                EventTypeModel.AddDoc = _AddDoc;
                EventTypeModel.UpdateDoc = _UpdateDoc;
                EventTypeModel.DeleteDoc = _DeleteDoc;
                return EventTypeModel


            }])
})()
