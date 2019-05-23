(function () {

    "use strict";
    angular.module("CRMApp.Services")
            .service("ShapeService", ["$http",
            function ($http) {

                var list = {};

                list.CreateUpdate = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/Shape/CreateUpdate",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.Delete = function (id) {
                    return $http({
                        method: "POST",
                        url: "/Master/Shape/Delete?Id=" + id
                    })
                }

                list.GetById = function (id) {
                    return $http({
                        method: "Get",
                        url: "/Master/Shape/GetById?Id=" + id
                    });
                }

                return list;

            }])
})()