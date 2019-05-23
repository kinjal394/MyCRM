
(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("CompanyLetterService", ["$http",
            function ($http) {

                var list = {};
                
                list.SetLetter = function (data) {

                    return $http({
                        method: "POST",
                        url: "/Master/companyLetter/SetOfferLetter",
                        data: data,
                        contentType: "application/json"
                    })
                }
                list.GetCompanydataById = function (id) {
                    return $http({
                        method: "POST",
                        url: "/Master/companyLetter/GetCompanydata?id=" + id
                    });
                }
                list.GetUserdataById = function (id) {
                    return $http({
                        method: "POST",
                        url: "/Master/companyLetter/GetUserData?id=" + id
                    });
                }

                list.GetInterviewDetailById = function (id) {
                    return $http({
                        method: "POST",
                        url: "/Master/companyLetter/GetInterviewDetailById?id=" + id
                    });
                }
               
                return list;
            }])
})()