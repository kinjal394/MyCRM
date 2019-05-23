(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("TaxService", ["$http",
            function ($http) {
                var TaxViewModel = {};


                var _AddTax = function (TaxData) {
                    return $http({
                        method: "POST",
                        url: "/Master/Tax/SaveTax",
                        data: TaxData
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                var _DeleteTax = function (taxId) {
                    return $http({
                        method: "POST",
                        url: "/Master/Tax/DeleteTax",
                        data: '{id:"' + taxId + '"}'
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })

                }
                var _UpdateTax = function (TaxData) {
                    return $http({
                        method: "POST",
                        url: "/Master/Tax/UpdateTax",
                        data: TaxData
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                TaxViewModel.AddTax = _AddTax;
                TaxViewModel.UpdateTax = _UpdateTax;
                TaxViewModel.DeleteTax = _DeleteTax;
                return TaxViewModel


            }])
})()
