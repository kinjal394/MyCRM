(function () {

    "use strict";
    angular.module("CRMApp.Services")
            .service("AccountEntryService", ["$http",
            function ($http) {
                var AccountEntryModel = {}; 

                var _AddAccountEntry = function (objaccount) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Transaction/AccountEntry/SaveAccountEntry",
                        data: objaccount
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                var _DeleteAccountEntry = function (AccountId) {
                    return $http({
                        method: "POST",
                        url: "/Transaction/AccountEntry/DeleteAccountEntry",
                        data: '{id:"' + AccountId + '"}'
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })

                }
                var _UpdateAccountEntry = function (objaccount) {
                    return $http({
                        method: "POST",
                        url: "/Transaction/AccountEntry/UpdateAccountEntry",
                        data: objaccount
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                //var _LegerHeadBind = function () {
                //    return $http({
                //        method: "POST",
                //        url: "/Transaction/AccountEntry/LegerHeadBind" 
                //    }).success(function (data) {
                //        return data
                //    }).error(function (e) {
                //        return e
                //    })
                //}

                //var _LegerBind = function (data) {
                //    return $http({
                //        method: "POST",
                //        url: "/Transaction/AccountEntry/LegerBind/LegerHeadId="+ data
                //    }).success(function (data) {
                //        return data
                //    }).error(function (e) {
                //        return e
                //    })
                //}

                AccountEntryModel.AddAccountEntry = _AddAccountEntry;
                AccountEntryModel.Update = _UpdateAccountEntry;
                AccountEntryModel.DeleteAccountEntry = _DeleteAccountEntry;
                //AccountEntryModel.LegerHeadBind = _LegerHeadBind;
                //AccountEntryModel.LegerBind = _LegerBind;
                return AccountEntryModel


            }])
})()
