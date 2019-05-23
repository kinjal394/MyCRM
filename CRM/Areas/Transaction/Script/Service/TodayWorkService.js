(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("TodayWorkService", ["$http",
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
                        url: "/Transaction/Task/ReportingUserBind?TaskId=" + id
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