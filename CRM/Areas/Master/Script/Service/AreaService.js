/// <reference path="E:\Urvish\CRM\Project\CRM\Content/lib/angular/angular.js" />

(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("AreaService", ["$http",
            function ($http) {
                var list = {};

                list.GetStateList = function (countryId) {
                    return $http({
                        method: 'Get',
                        url: '/Master/Exhibition/GetStates?CountryID=' + countryId,
                        contentType: "application/json"
                    })
                }

                list.AddArea = function (data)
                {
                    return $http({
                        method: 'POST',
                        url: '/Master/Area/SaveArea',
                        data:data,
                        contentType: "application/json"
                    })
                }

                list.Update=function(data)
                {
                    return $http({
                        method: 'POST',
                        url: '/Master/Area/UpdateArea',
                        data: data,
                        contentType: "application/json"
                    })
                }

                //var _getStateByID = function (countryId) {
                //    return $http({
                //        method: 'Get',
                //        url: '/Master/Exhibition/GetStates?CountryID=' + countryId
                //    }).success(function (data) {
                //        return data;
                //    }).error(function (data, status, headers, config) {
                //        return 'Unexpected Error';
                //    });
                //}


                list.DeleteArea = function (id) {
                    return $http({
                        method: "POST",
                        url: "/Master/Area/DeleteArea?id=" + id
                    })
                }

                list.GetCategoryById = function (id) {
                    return $http({
                        method: "Get",
                        url: "/Product/Category/GetCategoryById?CategoryId=" + id
                    });
                }

                return list;
            }])
})()





