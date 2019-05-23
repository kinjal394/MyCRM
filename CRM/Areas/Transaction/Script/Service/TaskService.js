/// <reference path="D:\Raju_Data\CRM\CRM_Proj\CRM\Content/lib/angular/angular.js" />

(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("TaskService", ["$http",
            function ($http) {
                var list = {};
                list.GetTaskInfromation = function () {
                    return $http({
                        method: "GET",
                        url: "/Transaction/Task/GetTaskInfromation"
                    });
                }

                list.ReportingUserBind = function (id) {
                    return $http({
                        method: "Get",
                        //data:data,
                        url: "/Transaction/Task/ReportingUserBindTask?TaskId=" + id
                    });
                }

                list.AddTask = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Transaction/Task/CreateUpdateTask",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.GetTaskDatabyId = function (id) {
                    return $http({
                        method: "POST",
                        url: "/Transaction/Task/GetTaskDatabyId?TaskId=" + id
                    })
                }

                list.SaveTaskFollowUp = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Transaction/Task/SaveTaskFollowUp",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.GetTaskInfoById = function (id) {
                    return $http({
                        method: "POST",
                        url: "/Transaction/Task/GetTaskInfoById?TaskId=" + id
                    })
                }

                list.DeleteTask = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Transaction/Task/DeleteTask?TaskId=" + data
                    })
                }

                list.GetTaskFollowUp = function (id) {
                    return $http({
                        method: "GET",
                        url: "/Transaction/Task/GetTaskFollowUp/" + id
                    });
                }

                list.GetTaskFollowUpByID = function (id) {
                    return $http({
                        method: "GET",
                        url: "/Transaction/Task/GetTaskFollowUpByID/" + id
                    });
                }

                list.CompleteTaskStatus = function (TaskId) {
                    return $http({
                        method: "GET",
                        url: "/Transaction/Task/CompleteTaskStatus?TaskId=" + TaskId
                    })
                }

                list.AssignNewUser = function (TaskId, UserId) {
                    return $http({
                        method: "GET",
                        url: "/Transaction/Task/AssignNewUser?TaskId=" + TaskId + "&UserId=" + UserId
                    })
                }

                list.EditDashboardTask = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Transaction/Task/EditDashboardTask",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.CreateUpdateTask = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Transaction/Task/CreateUpdateTask",
                        data: data,
                        contentType: "application/json"
                    })
                }



                return list;
            }])
})()