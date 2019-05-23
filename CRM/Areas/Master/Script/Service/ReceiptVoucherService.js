


(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("ReceiptVoucherService", ["$http",
            function ($http) {
                var list = {};
                list.GetAllReceiptVoucher = function () {
                    return $http({
                        method: "POST",
                        url: "/Master/ReceiptVoucher"
                    });
                }
                //list.CountryBind = function () {
                //    return $http({
                //        method: "Get",
                //        url: "/Master/ReceiptVoucher/CountryBind"
                //    });
                //}
                list.AddReceiptVoucher = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/ReceiptVoucher/SaveReceiptVoucher",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.DeleteReceiptVoucher = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/ReceiptVoucher/DeleteReceiptVoucher?VoucherId=" + data
                    })
                }

                // GETById DATA
                list.GetReceiptVoucherById = function (id) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/ReceiptVoucher/GetReceiptVoucherById?VoucherId=" + id
                    });
                }

                return list;
            }])
})()