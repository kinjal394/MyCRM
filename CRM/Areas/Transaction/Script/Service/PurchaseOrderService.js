(function () {

    "use strict";
    angular.module("CRMApp.Services")
        .service("PurchaseOrderService", ["$http",
            function ($http) {
                var list = {};

                list.GetDropDownInformation = function () {
                    return $http({
                        method: "GET",
                        url: "/PurchaseOrder/GetDropDownInformation"
                    });
                }

                list.GetProductBySubcategoryId = function (id) {
                    return $http({
                        method: "GET",
                        url: "/PurchaseOrder/GetProductBySubcategoryId?id=" + id
                    });
                }

                list.GetAllPurchaseOrderInfoById = function (id) {
                    return $http({
                        method: "GET",
                        url: "/PurchaseOrder/GetAllPurchaseOrderInfoById?id=" + id
                    });
                }

                list.CreateUpdate = function (data) {
                    return $http({
                        method: "POST",
                        url: "/PurchaseOrder/CreateUpdate",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.DeleteById = function (id) {
                    return $http({
                        method: "POST",
                        url: "/PurchaseOrder/DeleteById?id=" + id
                    });
                }

               

                list.GetInvoice = function () {
                    return $http({
                        method: "POST",
                        url: "/PurchaseOrder/GetInvoiceInfo"
                    });
                }

                list.GetSupplierDetail = function (id) {
                    return $http({
                        method: "POST",
                        url: "/PurchaseOrder/GetSupplierDetail?Id=" + id
                    });
                }

                list.GetSupplierContactDetail = function (id) {
                    return $http({
                        method: "POST",
                        url: "/PurchaseOrder/GetSupplierContactDetail?Id=" + id
                    });
                }
                list.GetOrderNo = function (id) {
                    return $http({
                        method: "POST",
                        url: "/PurchaseOrder/GetOrderNo?id=" + id
                    });
                }

                return list;
            }])
})()