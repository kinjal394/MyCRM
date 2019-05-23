/// <reference path="D:\Raju_Data\CRM\CRM_Proj\CRM\Content/lib/angular/angular.js" />

(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("CityService", ["$http",
            function ($http) {
               
                var list = {};
                list.GetAllCity = function () { 
                    return $http({
                        method: "POST",
                        url: "/Master/City"
                    });
                }

                list.CountryBind = function () {
                   
                    return $http({
                        method: "Get",
                        url: "/Master/Country/CountryBind"
                    });
                }

                list.StateBind = function (data) {
                  
                    return $http({
                        method: "Get",
                        url: "/Master/State/StateBind?CountryId=" + data
                    })
                }

                list.AddCity = function (data) {
                   
                    return $http({
                        method: "POST",
                        url: "/Master/City/SaveCity",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.DeleteCity = function (data) {
                   
                    return $http({
                        method: "POST",
                        url: "/Master/City/DeleteCity?CityId=" + data
                    })
                }

                // GETById DATA
                list.GetByIdCity = function (id) {
                   
                    return $http({
                        method: "POST",
                        url: "/Master/City/GetByIdCity?CityID=" + id
                    });
                }

                return list;
            }])
})()