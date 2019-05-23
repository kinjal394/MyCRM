
(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("BankService", ["$http",
            function ($http) {
                var BankViewModel = {};


                var _addBank = function (bank) {
                    return $http({
                        method: "POST",
                        url: "/Master/Bank/SaveBank",
                        data: bank
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }
                var _editBank = function (bankid) {
                    return $http({
                        method: "POST",
                        url: "/Master/Bank/EditBank",
                        data: '{id:"' + bankid + '"}'
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }
                var _Deletebanks = function (bankid)
                {
                    return $http({
                        method: "POST",
                        url: "/Master/Bank/DeleteBank",
                        data: '{id:"'+bankid+'"}'
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })

                }
                var _UpdateBank = function (bankdata)
                {
                    return $http({
                        method: "POST",
                        url: "/Master/Bank/UpdateBank",
                        data: bankdata
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }
                
                BankViewModel.addBank = _addBank;
                BankViewModel.editBank = _editBank;
                BankViewModel.Update = _UpdateBank;
                BankViewModel.Deletebanks = _Deletebanks;
                return BankViewModel


            }])
})()
