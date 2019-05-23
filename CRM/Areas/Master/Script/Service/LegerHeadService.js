/// <reference path="D:\Raju_Data\CRM\CRM_Proj\CRM\Content/lib/angular/angular.js" />

(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("LegerHeadService", ["$http",
            function ($http) {
                var list = {};
                list.GetAllLegerHead = function () {
                    return $http({
                        method: "POST",
                        url: "/Master/LegerHead"
                    });
                }
                //list.ITRBind = function () {
                //    return $http({
                //        method: "Get",
                //        url: "/Master/ITR/ITRBind"
                //    });
                //}
                list.AddLegerHead = function (data) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/LegerHead/SaveLegerHead",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.DeleteLegerHead = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/LegerHead/DeleteLegerHead?LegerHeadId=" + data
                    })
                }

                // GETById DATA
                list.GetByIdLegerHead = function (id) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/LegerHead/GetByIdLegerHead?LegerHeadId=" + id
                    });
                }

                return list;
            }])
})()