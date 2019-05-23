



(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("TermsAndConditionService", ["$http",
            function ($http) {
                var list = {};
                list.getAllTermsAndCondition = function () {
                    return $http({
                        method: "POST",
                        url: "/Master/TermsAndCondition"
                    });
                }

                list.AddTermsAndCondition = function (data) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/TermsAndCondition/SaveTermsAndCondition",
                        data: data,
                        contentType: "application/json"
                    })
                    //return $http({
                    //    method: "POST",
                    //    //url: "/Master/TermsAndCondition/SaveTermsAndCondition",
                    //    url: "/Master/TermsAndCondition/SaveTermsAndCondition?Description=" + data
                    //    //contentType: "application/json"
                    //    //data: data
                    //})
                }

                list.DeleteTermsAndCondition = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/TermsAndCondition/DeleteTermsAndCondition?TermsId=" + data
                    })
                }
                // GETById DATA
                list.GettermsAndConditionById = function (id) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/TermsAndCondition/GettermsAndConditionById?TermsId=" + id
                    });
                }
                return list;
            }])
})()