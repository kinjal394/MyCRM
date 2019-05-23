


(function () {
    "use strict";

    debugger;
    angular.module("CRMApp.Services")
            .service("DepartmentService", ["$http",
            function ($http) {
                var list = {};
                list.getAllDepartment = function () {
                    return $http({
                        method: "POST",
                        url: "/Master/Department"
                    });
                }

                list.AddDepartment = function (data) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/Department/SaveDepartment",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.DeleteDepartment = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/Department/DeleteDepartment?DepartmentId=" + data
                    })
                }
                list.GetDepartmentById = function (id) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/Department/GetDepartmentById?DepartmentId=" + id
                    });
                }
                return list;
            }])
})()