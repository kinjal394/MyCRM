(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("TaskStatusService", ["$http",
            function ($http) {
                var TaskStatusViewModel = {};


                var _AddTaskStatus = function (TaskStatusData) {
                    return $http({
                        method: "POST",
                        url: "/Master/TaskStatus/SaveTaskStatus",
                        data: TaskStatusData
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }
                var _DeleteTaskStatus = function (taskstatusId) {
                    return $http({
                        method: "POST",
                        url: "/Master/TaskStatus/DeleteTaskStatus",
                        data: '{id:"' + taskstatusId + '"}'
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })

                }
                var _UpdateTaskStatus = function (TaskStatusData) {
                    return $http({
                        method: "POST",
                        url: "/Master/TaskStatus/UpdateTaskStatus",
                        data: TaskStatusData
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                var _GetAllTaskStatus = function () {
                    return $http({
                        method: "GET",
                        url: "/Master/TaskStatus/GetAllTaskStatus"
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }


                TaskStatusViewModel.AddTaskStatus = _AddTaskStatus;
                TaskStatusViewModel.UpdateTaskStatus = _UpdateTaskStatus;
                TaskStatusViewModel.DeleteTaskStatus = _DeleteTaskStatus;
                TaskStatusViewModel.GetAllTaskStatus = _GetAllTaskStatus;
                return TaskStatusViewModel

            }])
})()
