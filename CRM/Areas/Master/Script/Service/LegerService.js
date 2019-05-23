/// <reference path="D:\Raju_Data\CRM\CRM_Proj\CRM\Content/lib/angular/angular.js" />

(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("LegerService", ["$http",
            function ($http) {
                var list = {};
                list.GetAllLeger = function () {
                    return $http({
                        method: "POST",
                        url: "/Master/Leger"
                    });
                }
                //list.ITRBind = function () {
                //    return $http({
                //        method: "Get",
                //        url: "/Master/ITR/ITRBind"
                //    });
                //}
                list.AddLeger = function (data) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/Leger/SaveLeger",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.DeleteLeger = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/Leger/DeleteLeger?LegerId=" + data
                    })
                }

                // GETById DATA
                list.GetByIdLegerd = function (id) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/Leger/GetByIdLeger?LegerId=" + id
                    });
                }

                return list;
            }])
})()