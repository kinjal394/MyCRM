
(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("TOService", ["$http",
            function ($http) {
                var TOViewModel = {};


                var _addupdateTO = function (TO) {
                    return $http({
                        method: "POST",
                        url: "/Master/TO/CreateUpdate",
                        data: TO
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                var _DeleteTO = function (TOId) {
                    return $http({
                        method: "POST",
                        url: "/Master/TO/DeleteById",
                        data: '{TOId:"' + TOId + '"}'
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })

                }

                var _getTO = function (TOId) {

                    return $http({
                        method: "GET",
                        url: "/Master/TO/GetAllTOInfoById?TOId=" + TOId,
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                var _gettechnicalspec = function (pid,techparaid)
                {
                    return $http({
                        method: "GET",
                        url: "/Master/TO/GetTecgnicalspec?pid=" + pid + "&Techparaid=" + techparaid,

                    }).success(function (data) {
                        return data;
                    }).error(function (e) {
                        return e;
                    })
                }

                var _GetAllInquiryInfoById = function (id) {
                    return $http({
                        method: "GET",
                        url: "/Transaction/Inquiry/GetAllInquiryInfoById?id=" + id,
                    }).success(function (data) {
                        return data;
                    }).error(function (e) {
                        return e;
                    })
                }

                var _GetBuyerDetailById = function (id) {
                    return $http({
                        method: "GET",
                        url: "/Transaction/SalesOrder/GetBuyerDetailById?id=" + id,
                    }).success(function (data) {
                        return data;
                    }).error(function (e) {
                        return e;
                    })
                }

                TOViewModel.CreateUpdate = _addupdateTO;
                TOViewModel.GetbyId = _getTO;
                TOViewModel.DeleteById = _DeleteTO;
                TOViewModel.gettechnicalspec = _gettechnicalspec;
                TOViewModel.GetAllInquiryInfoById = _GetAllInquiryInfoById;
                TOViewModel.GetBuyerDetailById = _GetBuyerDetailById;

                return TOViewModel


            }])
})()