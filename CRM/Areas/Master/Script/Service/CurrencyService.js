(function () {

    "use strict";
    angular.module("CRMApp.Services")
            .service("CurrencyService", ["$http",
            function ($http) {
                var CurrencyModel = {};


                var _AddCurrency = function (CurrencyData) {
                    return $http({
                        method: "POST",
                        url: "/Master/Currency/SaveCurrency",
                        data: CurrencyData
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                var _DeleteCurrency = function (CurrencyId) {
                    return $http({
                        method: "POST",
                        url: "/Master/Currency/DeleteCurrency",
                        data: '{id:"' + CurrencyId + '"}'
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })

                }
                var _UpdateCurrency = function (CurrencyData) {
                    return $http({
                        method: "POST",
                        url: "/Master/Currency/UpdateCurrency",
                        data: CurrencyData
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                var _AutoCurrencyCode = function (CurrencyData) {
                    return $http({
                        method: "POST",
                        url: "/Master/Currency/SetCurrencyCode?CurrencyName=" + CurrencyData,
                        //data: CurrencyData
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                
                CurrencyModel.AddCurrency = _AddCurrency;
                CurrencyModel.Update = _UpdateCurrency;
                CurrencyModel.DeleteCurrency = _DeleteCurrency;
                CurrencyModel.AutoCurrencyCode = _AutoCurrencyCode;
                return CurrencyModel


            }])
})()
