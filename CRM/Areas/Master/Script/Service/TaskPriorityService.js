(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("TaskPriorityService", ["$http",
            function ($http) {
                var TaskPriorityViewModel = {};


                var _AddTaskPriority = function (TaskPriorityData) {
                    return $http({
                        method: "POST",
                        url: "/Master/TaskPriority/SaveTaskPriority",
                        data: TaskPriorityData
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                var _DeleteTaskPriority = function (taskpriorityId) {
                    return $http({
                        method: "POST",
                        url: "/Master/TaskPriority/DeleteTaskPriority",
                        data: '{id:"' + taskpriorityId + '"}'
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })

                }
                var _UpdateTaskPriority = function (TaskPriorityData) {
                    return $http({
                        method: "POST",
                        url: "/Master/TaskPriority/UpdateTaskPriority",
                        data: TaskPriorityData
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                TaskPriorityViewModel.AddTaskPriority = _AddTaskPriority;
                TaskPriorityViewModel.UpdateTaskPriority = _UpdateTaskPriority;
                TaskPriorityViewModel.DeleteTaskPriority = _DeleteTaskPriority;
                return TaskPriorityViewModel


            }])
})()
