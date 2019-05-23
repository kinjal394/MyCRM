(function () {

    "use strict";
    angular.module("CRMApp.Services")
            .service("BillofLoadingService", ["$http",
            function ($http) {
                var BillofLoadingModel = {};

                var _CreateUpdateBillofLoading = function (objaccount) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Operation/BillofLoading/CreateUpdate",
                        data: objaccount
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                var _PrintBillofLoading = function (AccountId) {
                    return $http({
                        method: "POST",
                        url: "/Operation/BillofLoading/PrintById",
                        data: '{id:"' + AccountId + '"}'
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })

                }

                var _DeleteBillofLoading = function (AccountId) {
                    return $http({
                        method: "POST",
                        url: "/Operation/BillofLoading/DeleteById",
                        data: '{id:"' + AccountId + '"}'
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })

                }
               
                var _GetBillofLoadingInfoById = function (id) {
                    return $http({
                        method: "GET",
                        url: "/Operation/BillofLoading/GetBuyerAddressInfoById?id=" + id
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                var _GetAllBillofLoadingInfoById = function (id) {
                    return $http({
                        method: "GET",
                        url: "/Operation/BillofLoading/GetAllBillofLoadingInfoById?id=" + id
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                BillofLoadingModel.CreateUpdateBillofLoading = _CreateUpdateBillofLoading;
                BillofLoadingModel.DeleteBillofLoading = _DeleteBillofLoading;
                BillofLoadingModel.GetBillofLoadingInfoById = _GetBillofLoadingInfoById;
                BillofLoadingModel.GetAllBillofLoadingInfoById = _GetAllBillofLoadingInfoById;
                BillofLoadingModel.PrintBillofLoading = _PrintBillofLoading;

                return BillofLoadingModel


            }])
})()
