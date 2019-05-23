(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("VendorService", ["$http",
                function ($http) {
                    var list = {};

                    //list.GetMasterInformation = function () {
                    //    return $http({
                    //        method: "POST",
                    //        url: "/Vendor/GetMasterInformation"
                    //    });
                    //}

                    //list.GetCityMasterInformation = function (id) {
                    //    return $http({
                    //        method: "POST",
                    //        url: "/Vendor/GetCityMasterInformation?cityId=" + id
                    //    });
                    //}

                    list.CreateUpdate = function (data) {
                        return $http({
                            method: "POST",
                            url: "/Vendor/CreateUpdate",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    list.DeleteById = function (id) {
                        return $http({
                            method: "POST",
                            url: "/Vendor/DeleteById?id=" + id
                        });
                    }

                    list.GetAllVendorInfoById = function (id) {
                        return $http({
                            method: "GET",
                            url: "/Vendor/GetAllVendorInfoById?id=" + id
                        });
                    }

                    return list;
                }])
})()