/// <reference path="D:\Raju_Data\CRM\CRM_Proj\CRM\Content/lib/angular/angular.js" />

(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("CountryOriginService", ["$http",
            function ($http) {
                var list = {};
                list.GetAllCountryOrigin = function () {
                    return $http({
                        method: "POST",
                        url: "/Master/CountryOrigin"
                    });
                }
                list.CountryBind = function () {
                    return $http({
                        method: "Get",
                        url: "/Master/Country/CountryBind"
                    });
                }
                list.AddCountryOrigin = function (data) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/CountryOrigin/SaveCountryOrigin",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.DeleteCountryOrigin = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/CountryOrigin/DeleteCountryOrigin?OriginId=" + data
                    })
                }

                // GETById DATA
                list.GetByIdCountryOrigin = function (id) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/CountryOrigin/GetByIdCountryOrigin?OriginId=" + id
                    });
                }

                return list;
            }])
})()