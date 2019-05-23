/// <reference path="E:\Urvish\CRM\Project\CRM\Content/lib/angular/angular.js" />

(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("MainProductService", ["$http",
            function ($http) {
                var list = {};

                list.SaveMainProduct = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Product/MainProduct/CreateMainProduct",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.UpdateMainProduct = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Product/MainProduct/UpdateMainProduct",
                        data: data,
                    })
                }

                list.DeleteMainProduct= function (data) {
                    return $http({
                        method: "POST",
                        url: "/Product/MainProduct/DeleteMainProduct?ProductId=" + data
                    })
                }
                list.GetProductBySubcategoryId = function (id) {
                    debugger
                    return $http({
                        method: "Get",
                        url: "/Product/MainProduct/GetProductBySubcategoryId?id=" + id
                    })
                }
                return list;
            }])
})()





