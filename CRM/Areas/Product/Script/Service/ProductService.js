(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("ProductService", ["$http",
            function ($http) {
                var list = {};

                list.CreateUpdateProduct = function (data) {
                    return $http({
                        method: "Post",
                        url: "/Product/Product/CreateUpdateProduct",
                        data: data
                    });
                }
                list.AutoProductCode = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Product/Product/SetProductCode?ProductName=" + data
                    })
                }
                list.DeleteProduct = function (id) {
                    return $http({
                        method: "POST",
                        url: "/Product/Product/DeleteProduct?ProductId=" + id
                    })
                }

                list.GetProductById = function (id) {
                    return $http({
                        method: "Get",
                        url: "/Product/Product/GetProductById?ProductId=" + id
                    });
                }
                list.getproductCatId = function (id) {                  
                    return $http({
                        method: "Get",
                        url: "/Product/Product/GetProductByCatId?Id=" + id
                    });
                }
                list.GetAllProductFormInfoById = function (Id) {
                    return $http({
                        method: "Get",
                        url: "/Product/Product/GetAllProductFormInfoById?Id=" + Id
                    });
                }

                list.GetAllProductSupplierInfoById = function (Id, catelogId) {
                    return $http({
                        method: "Get",
                        url: "/Product/Product/GetAllProductSupplierInfoById?Id=" + Id + "&catelogId=" + catelogId
                    });
                }

                list.GetTechnicalSpecMaster = function () {
                    return $http({
                        method: "Get",
                        url: "/Product/Product/GetTechnicalSpecMaster"
                    });
                }

                list.UpdateProductFrom = function (data) {
                    return $http({
                        method: "Post",
                        url: "/Product/Product/UpdateProductFrom",
                        data: data,
                        contentType: "application/json"
                    });
                }

                list.SendFile = function (data) {
                    return $http({
                        method: "Post",
                        url: "/Product/Product/SendFile",
                        data: data,
                        contentType: "application/json"
                    });
                }

                list.CheckBuyerPrdModelNo = function (modelNo) {
                    return $http({
                        method: "GET",
                        url: "/Product/Product/CheckBuyerPrdModelNo?ModelNo=" + modelNo,
                        contentType: "application/json"
                    })
                }
                list.CheckSupplierModelNo = function (SupmodelNo) {
                    return $http({
                        method: "GET",
                        url: "/Product/Product/CheckSupplierModelNo?SupmodelNo=" + SupmodelNo,
                        contentType: "application/json"
                    })
                }
                return list;
            }]);
})()