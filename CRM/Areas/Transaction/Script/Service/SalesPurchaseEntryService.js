(function () {

    "use strict";
    angular.module("CRMApp.Services")
            .service("SalesPurchaseEntryService", ["$http",
            function ($http) {
                var list = {};

                list.CreateUpdateSalePurchaseEntry = function (objsalespurchase) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Transaction/SalesPurchaseEntry/CreateUpdate",
                        data: objsalespurchase
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                list.DeleteSalePurchaseEntry = function (SalesPurchaseId) {
                    return $http({
                        method: "POST",
                        url: "/Transaction/SalesPurchaseEntry/DeleteSalePurchaseEntry",
                        data: '{id:"' + SalesPurchaseId + '"}'
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })

                }
                list.GetAllSalesPurchaseEntryById = function (SalesPurchaseId) {
                    return $http({
                        method: "GET",
                        url: "/Transaction/SalesPurchaseEntry/GetAllSalesPurchaseEntryById?id=" + SalesPurchaseId
                    });
                }
                return list;
            }])
})()
