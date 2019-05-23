
(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("TaskTypeService", ["$http",
            function ($http) {
                var list = {};
                list.GetAllTaskType = function () {
                    return $http({
                        method: "POST",
                        url: "/Master/TaskType"
                    });
                }

                list.AddTaskType = function (data) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/TaskType/SaveTaskType",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.DeleteTaskType = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/TaskType/DeleteTaskType?TaskTypeId=" + data
                    })
                }
                list.GetTaskTypeById = function (id) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/TaskType/GetTaskTypeById?TaskTypeId=" + id
                    });
                }
                return list;
            }])
})()