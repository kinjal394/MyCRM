/// <reference path="D:\Raju_Data\CRM\CRM_Proj\CRM\Content/lib/angular/angular.js" />

(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("TaskGroupService", ["$http",
            function ($http) {
                var list = {};
                list.GetAllTaskGroup = function () {
                    return $http({
                        method: "POST",
                        url: "/Master/TaskGroup"
                    });
                }
               
                list.AddTaskGroup = function (data) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/TaskGroup/SaveTaskGroup",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.DeleteTaskGroup = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/TaskGroup/DeleteTaskGroup?TaskGroupId=" + data
                    })
                }

                // GETById DATA
                list.GetByIdTaskGroup = function (id) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/TaskGroup/GetByIdTaskGroup?TaskGroupId=" + id
                    });
                }

                return list;
            }])
})()