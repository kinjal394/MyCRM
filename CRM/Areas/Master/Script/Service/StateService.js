/// <reference path="D:\Raju_Data\CRM\CRM_Proj\CRM\Content/lib/angular/angular.js" />

(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("StateService", ["$http",
            function ($http) {
                var list = {};
                list.GetAllState = function () {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/State"
                    });
                }

                list.CountryBind = function () {
                    debugger;
                    return $http({
                        method: "Get",
                        url: "/Master/Country/CountryBind"
                    });
                }

                list.StateBind = function (countryId) {
                    debugger;
                    return $http({
                        method: "Get",
                        url: "/Master/State/StateBind?CountryId=" + countryId
                    });
                }

                list.AddState = function (data) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/State/SaveState",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.DeleteState = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/State/DeleteState?StateId=" + data
                    })
                }

                // GETById DATA
                list.GetByIdState = function (id) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/State/GetByIdState?StateID=" + id
                    });
                }

                return list;
            }])
})()