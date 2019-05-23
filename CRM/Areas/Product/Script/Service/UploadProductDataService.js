(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("UploadProductDataService", ["$http",
    function ($http) {
        var list = {};


        list.CreateUpdate = function (data) {
            return $http({
                method: "POST",
                url: "/Product/UploadProductData/CreateUpdate",
                data: data,
            });
        }

        list.DeleteById = function (id) {
            return $http({
                method: "POST",
                url: "/Product/UploadProductData/DeleteById?id=" + id
            })
        }

        list.GetProductDetailById = function (id) {
            return $http({
                method: "GET",
                url: "/Product/UploadProductData/GetProductDetailById?ProductId=" + id,
            });
        }

        list.SendFile = function (data) {
            return $http({
                method: "Post",
                url: "/Product/UploadProductData/SendFile",
                data: data,
                contentType: "application/json"
            });
        }

        list.GetProductDetailByCId = function (id,cid) {
            return $http({
                method: "GET",
                url: "/Product/UploadProductData/GetProductDetailByCId?ProductId=" + id + "&CatalogId=" + cid,
            });
        }
        list.GetContactById = function (id) {
            return $http({
                method: "GET",
                url: "/Buyer/GetContactBuyerById?id=" + id,
            });
        }
        list.GetAllUploadProductDataInfoById = function (code) {
            return $http({
                method: "GET",
                url: "/Product/UploadProductData/GetAllUploadProductDataInfoById?code=" + code
            });
        }

        return list;

    }]);
})()