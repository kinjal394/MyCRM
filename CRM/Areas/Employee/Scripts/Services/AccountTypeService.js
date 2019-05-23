(function () {
    "use strict";

    angular.module("CRMApp.Services")
        .service("AccountTypeService", ["$http",
        function ($http) {

            var list = {};

            list.CreateUpdate = function (data) {
                return $http({
                    method: "POST",
                    url: "/Employee/AccountType/CreateUpdate",
                    data: data,
                    contentType: "application/json"
                })
            }

            list.Delete = function (id) {
                return $http({
                    method: "POST",
                    url: "/Employee/AccountType/Delete?Id=" + id
                })
            }

            list.GetById = function (id) {
                return $http({
                    method: "Get",
                    url: "/Employee/AccountType/GetById?Id=" + id
                });
            }

            return list;
        }])
})()