(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("ShippingMarkService", ["$http",
            function ($http) {
                var ShippingMarkViewModel = {};


                var _AddShippingMark = function (ShippingMarkData) {
                    return $http({
                        method: "POST",
                        url: "/Master/ShippingMark/SaveShippingMark",
                        data: ShippingMarkData
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                var _DeleteShippingMark = function (shippingmarkId) {
                    return $http({
                        method: "POST",
                        url: "/Master/ShippingMark/DeleteShippingMark",
                        data: '{id:"' + shippingmarkId + '"}'
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })

                }
                var _UpdateShippingMark = function (ShippingMarkData) {
                    return $http({
                        method: "POST",
                        url: "/Master/ShippingMark/UpdateShippingMark",
                        data: ShippingMarkData
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                ShippingMarkViewModel.AddShippingMark = _AddShippingMark;
                ShippingMarkViewModel.UpdateShippingMark = _UpdateShippingMark;
                ShippingMarkViewModel.DeleteShippingMark = _DeleteShippingMark;
                return ShippingMarkViewModel


            }])
})()
