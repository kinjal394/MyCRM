/// <reference path="E:\Urvish\CRM\Project\CRM\Content/lib/angular/angular.js" />

(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("CategoryService", ["$http",
            function ($http) {
                var list = {};

                list.CreateUpdateCategory = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Product/Category/CreateUpdate",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.DeleteCategory = function (id) {
                    return $http({
                        method: "POST",
                        url: "/Product/Category/DeleteCategory?CategoryId=" + id
                    })
                }

                list.GetCategoryById = function (id) {
                    return $http({
                        method: "Get",
                        url: "/Product/Category/GetCategoryById?CategoryId=" + id
                    });
                }

                return list;
            }])
})()





