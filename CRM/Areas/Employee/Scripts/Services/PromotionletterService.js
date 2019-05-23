(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("PromotionletterService", ["$http",
            function ($http) {

                var list = {};

               
                list.GetCompanydataById = function (id) {
                    return $http({
                        method: "POST",
                        url: "/Master/companyLetter/GetCompanydata?id=" + id
                    });
                }

                list.GetUserById = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/User/GetUserById?UserId=" + data
                    })
                }

                list.SetLetter = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Employee/Promotionletter/PrintPromotionletter",
                        data: data,
                        contentType: "application/json"
                    });
                }

                return list;
            }])
})()