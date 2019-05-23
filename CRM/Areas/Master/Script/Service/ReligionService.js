

(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("ReligionService", ["$http",
            function ($http) {
                var list = {};
                list.GetAllReligion = function () {
                    return $http({
                        method: "POST",
                        url: "/Master/Religion"
                    });
                }

                list.AddReligion = function (data) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/Religion/SaveReligion",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.DeleteReligion = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/Religion/DeleteReligion?ReligionId=" + data
                    })
                }
                list.GetReligionById = function (id) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/Religion/GetReligionById?ReligionId=" + id
                    });
                }
                return list;
            }])
})()