(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("OfferletterService", ["$http",
            function ($http) {

                var list = {};

                list.SetLetter = function (data) {

                    return $http({
                        method: "POST",
                        url: "/Employee/Offerletter/SetOfferLetter",
                        data: data,
                        contentType: "application/json"
                    });
                }
                list.GetCompanydataById = function (id) {
                    return $http({
                        method: "POST",
                        url: "/Master/companyLetter/GetCompanydata?id=" + id
                    });
                }
              

                list.FetchAllInfoById = function (id) {
                    return $http({
                        method: "GET",
                        url: "/Employee/IntervieweeCandidate/FetchAllInfoById?Id=" + id
                    });
                }

                return list;
            }])
})()