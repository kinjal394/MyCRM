
(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("SubCategoryService", ["$http",
            function ($http) {
                var list = {};

                list.CreateUpdate = function (data)
                {
                    return $http({
                        method: "POST",
                        url: "/Product/SubCategory/CreateUpdate",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.DeleteSubCategory = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Product/SubCategory/DeleteSubCategory?SubCategoryId=" + data
                    })
                }

                //list.GetCategory=function()
                //{
                //    return $http({
                //        method: "Get",
                //        url: "/Product/Category/GetCategory"
                //    });
                //}

                return list;
                
            }])
})()
