(function () {
    "use strict";
    angular.module("CRMApp.Services")
        .service("SupplierService", ["$http",
            function ($http) {
                var list = {};

                list.GetMasterInformation = function () {
                    return $http({
                        method: "GET",
                        url: "/Supplier/GetMasterInformation"
                    });
                }

                list.CreateUpdate = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Supplier/CreateUpdate",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.DeleteById = function (id) {
                    return $http({
                        method: "POST",
                        url: "/Supplier/DeleteById?id=" + id
                    });
                }

                list.GetAllSupplierInfoById = function (id) {
                    return $http({
                        method: "GET",
                        url: "/Supplier/GetAllSupplierInfoById?id=" + id
                    });
                }

                return list;
            }])
})()