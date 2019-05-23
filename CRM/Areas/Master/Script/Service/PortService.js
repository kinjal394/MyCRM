


(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("PortService", ["$http",
            function ($http) {
                var list = {};
                list.getAllPort = function () {
                    return $http({
                        method: "POST",
                        url: "/Master/Port"
                    });
                }
                list.CountryBind = function () {
                    return $http({
                        method: "Get",
                        url: "/Master/Country/CountryBind"
                    });
                }
                list.AddPort = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/Port/SavePort",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.DeletePort = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/Port/DeletePort?PortId=" + data
                    })
                }

                // GETById DATA
                list.GetByIdPort = function (id) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/Port/GetByIdPort?PortId=" + id
                    });
                }

                return list;
            }])
})()