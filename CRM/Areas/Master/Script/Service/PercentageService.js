


(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("PercentageService", ["$http",
            function ($http) {
                var list = {};
                list.getAllPercentage = function () {
                    return $http({
                        method: "POST",
                        url: "/Master/Percentage"
                    });
                }

                list.AddPercentage = function (data) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/Percentage/SavePercentage",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.DeletePercentage = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/Percentage/DeletePercentage?PercentageId=" + data
                    })
                }
                list.GetPercentageById = function (id) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/Percentage/GetPercentageById?PercentageId=" + id
                    });
                }
                return list;
            }])
})()