(function () {

    "use strict";
    angular.module("CRMApp.Services")
            .service("ShippingOrderService", ["$http",
            function ($http) {
                var ShippingOrderModel = {};

                var _CreateUpdateShippingOrder = function (objaccount) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Operation/ShippingOrder/CreateUpdate",
                        data: objaccount
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                var _DeleteShippingOrder = function (AccountId) {
                    return $http({
                        method: "POST",
                        url: "/Operation/ShippingOrder/DeleteById",
                        data: '{id:"' + AccountId + '"}'
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })

                }

                var _PrintShippingOrder = function (AccountId) {
                    return $http({
                        method: "POST",
                        url: "/Operation/ShippingOrder/PrintById",
                        data: '{id:"' + AccountId + '"}'
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })

                }

                //var _LegerHeadBind = function () {
                //    return $http({
                //        method: "POST",
                //        url: "/Transaction/ShippingOrder/LegerHeadBind" 
                //    }).success(function (data) {
                //        return data
                //    }).error(function (e) {
                //        return e
                //    })
                //}

                var _GetBuyerAddressInfoById = function (id) {
                    return $http({
                        method: "GET",
                        url: "/Operation/ShippingOrder/GetBuyerAddressInfoById?id=" + id
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                var _GetAllShippingOrderInfoById = function (id) {
                    return $http({
                        method: "GET",
                        url: "/Operation/ShippingOrder/GetAllShippingOrderInfoById?id=" + id
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }
                
                ShippingOrderModel.CreateUpdateShippingOrder = _CreateUpdateShippingOrder;
                ShippingOrderModel.DeleteShippingOrder = _DeleteShippingOrder;
                //ShippingOrderModel.LegerHeadBind = _LegerHeadBind;
                ShippingOrderModel.GetBuyerAddressInfoById = _GetBuyerAddressInfoById;
                ShippingOrderModel.GetAllShippingOrderInfoById = _GetAllShippingOrderInfoById;
                ShippingOrderModel.PrintShippingOrder = _PrintShippingOrder;
                
                return ShippingOrderModel


            }])
})()
