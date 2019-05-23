(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("EmailSignatureService", ["$http",
            function ($http) {
                var EmailSignatureServiceViewModel = {};
                var _AddSignature = function (speechData) {
                    return $http({
                        method: "POST",
                        url: "/Master/EmailSignature/SaveSignature",
                        data: speechData
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                var _GetSignatureById = function (id) {
                    return $http({
                        method: "GET",
                        url: "/Master/EmailSignature/GetSignatureById/" + id,
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                var _DeleteSignature = function (id) {
                    return $http({
                        method: "POST",
                        url: "/Master/EmailSignature/DeleteSignature",
                        data: '{id:"' + id + '"}'
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })

                }
                var _UpdateSignature = function (signaturedata) {
                    debugger    
                    return $http({
                        method: "POST",
                        url: "/Master/EmailSignature/UpdateSignature",
                        data: signaturedata
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }
                var _GetAllUser = function () {
                        return $http({
                            method: "Get",
                            url: "/Master/EmailSignature/GetAllUsers"
                        });
                    }
                EmailSignatureServiceViewModel.AddSignature = _AddSignature;
                EmailSignatureServiceViewModel.GetSignatureById = _GetSignatureById;
                EmailSignatureServiceViewModel.UpdateSignature = _UpdateSignature;
                EmailSignatureServiceViewModel.DeleteSignature = _DeleteSignature;
                EmailSignatureServiceViewModel.GetAllUser = _GetAllUser;
                return EmailSignatureServiceViewModel


            }])
})()
