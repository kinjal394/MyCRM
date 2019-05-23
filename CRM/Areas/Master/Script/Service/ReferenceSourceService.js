/// <reference path="D:\Raju_Data\CRM\CRM_Proj\CRM\Content/lib/angular/angular.js" />

(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("ReferenceSourceService", ["$http",
            function ($http) {
                var list = {};

                list.CreateUpdateReferenceSource = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/ReferenceSource/CreateUpdateReferenceSource",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.DeleteReferenceSource = function (id) {
                    console.log(id);
                    return $http({
                        method: "POST",
                        url: "/Master/ReferenceSource/DeleteReferenceSource?SourceId=" + id
                    })
                }

                // GETById DATA
                list.GetReferenceSourceById = function (id) {
                    return $http({
                        method: "POST",
                        url: "/Master/ReferenceSource/GetReferenceSourceById?SourceId=" + id
                    });
                }

                return list;
            }])


})()