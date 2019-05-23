(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("SalesOrderService", ["$http",
                function ($http) {
                    var list = {};

                    list.GetMasterInformation = function () {
                        return $http({
                            method: "GET",
                            url: "/SalesOrder/GetMasterInformation"
                        });
                    }


                    list.GetBuyerById = function (id) {
                        return $http({
                            method: "GET",
                            url: "/SalesOrder/GetBuyerById?id=" + id
                        });
                    }


                    list.GetSubCategoryById = function (id) {
                        return $http({
                            method: "GET",
                            url: "/SalesOrder/GetSubCategoryById?id=" + id
                        });
                    }

                    list.CreateUpdate = function (data) {
                        return $http({
                            method: "POST",
                            url: "/SalesOrder/CreateUpdate",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    list.DeleteById = function (id) {
                        return $http({
                            method: "POST",
                            url: "/SalesOrder/DeleteById?id=" + id
                        });
                    }

                    list.GetAllSalesOrderInfoById = function (id) {
                        return $http({
                            method: "GET",
                            url: "/SalesOrder/GetAllSalesOrderInfoById?id=" + id
                        });
                    }

                    list.GetInvoice = function () {
                        return $http({
                            method: "POST",
                            url: "/SalesOrder/GetInvoiceInfo"
                        });
                    }
                    

                    list.GetSpecificationList = function () {
                        return $http({
                            method: "POST",
                            url: "/SalesOrder/GetSpecificationList"
                        });
                    }
                    
                    list.GetBuyerDetailById = function (id) {
                        return $http({
                            method: "POST",
                            url: "/SalesOrder/GetBuyerDetailById?id=" + id
                        });
                    }

                    list.GetBuyerContactDetailById = function (id) {
                        return $http({
                            method: "POST",
                            url: "/SalesOrder/GetBuyerContactDetailById?id=" + id
                        });
                    }

                    list.GetCompanyDetailById = function (id) {
                        return $http({
                            method: "POST",
                            url: "/SalesOrder/GetCompanyDetailById?id=" + id
                        });
                    }
                    return list;
                }])
})()