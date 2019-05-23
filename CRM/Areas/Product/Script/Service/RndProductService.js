(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("RndProductService", ["$http",
            function ($http) {
                var list = {};

                list.CreateUpdateProduct = function (data) {
                    return $http({
                        method: "Post",
                        url: "/Product/Product/CreateUpdateProduct",
                        data: data
                    });
                }

                list.CreateUpdate = function (data) {
                    return $http({
                        method: "Post",
                        url: "/Product/RNDProduct/CreateUpdate",
                        data: data
                    });
                }

                list.GetAllRndProductInfoById = function (Id) {
                    return $http({
                        method: "Get",
                        url: "/Product/RNDProduct/GetAllRndProductInfoById?Id=" + Id
                    });
                }

                list.SaveProduct = function (data) {
                    return $http({
                        method: "Post",
                        url: "/Product/RNDProduct/SaveProduct",
                        data: data
                    });
                }
                

                list.GetTechnicalSpecMaster = function () {
                    return $http({
                        method: "Get",
                        url: "/Product/Product/GetTechnicalSpecMaster"
                    });
                }

                list.DeleteProduct = function (Id) {
                    return $http({
                        method: "POST",
                        url: "/Product/RNDProduct/DeleteById?id=" + Id
                    });
                }
                list.GetCompanyDetailById = function (Id) {
                    return $http({
                        method: "Get",
                        url: "/Product/RNDProduct/GetCompanyDetailById?Id=" + Id
                    });
                }
                list.GetEmailSpeechDetailById = function (Id) {
                    return $http({
                        method: "Get",
                        url: "/Product/RNDProduct/GetEmailSpeechDetailById?Id=" + Id
                    });
                }
                list.GetSMSSpeechDetailById = function (Id) {
                    return $http({
                        method: "Get",
                        url: "/Product/RNDProduct/GetSMSSpeechDetailById?Id=" + Id
                    });
                }
                return list;
            }]);
})()