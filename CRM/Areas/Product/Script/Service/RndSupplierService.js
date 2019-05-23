(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("RndSupplierService", ["$http",
            function ($http) {
                var list = {};

                list.CreateUpdateProduct = function (data) {
                    return $http({
                        method: "Post",
                        url: "/Product/Product/CreateUpdateProduct",
                        data: data
                    });
                }

                list.GetTechnicalSpecMaster = function () {
                    return $http({
                        method: "Get",
                        url: "/Product/Product/GetTechnicalSpecMaster"
                    });
                }
                return list;
            }]);
})()