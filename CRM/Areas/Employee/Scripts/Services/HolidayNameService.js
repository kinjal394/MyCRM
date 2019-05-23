(function () {
    "use strict";
    angular.module("CRMApp.Services")
        .service("HolidayNameService", ["$http",
        function ($http) {

            var list = {};

            list.CreateUpdate = function (data) {
                return $http({
                    method: "POST",
                    url: "/Employee/HolidayName/CreateUpdate",
                    data: data,
                    contentType: "application/json"
                })
            }

            list.Delete = function (id) {
                return $http({
                    method: "POST",
                    url: "/Employee/HolidayName/Delete?Id=" + id
                })
            }

            list.GetById = function (id) {
                return $http({
                    method: "Get",
                    url: "/Employee/HolidayName/GetById?Id=" + id
                });
            }

            return list;
        }])
})()