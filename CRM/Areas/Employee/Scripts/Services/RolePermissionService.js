(function () {
    "use strict";
    angular.module("CRMApp.Services")
        .service("RolePermissionService", ["$http",
        function ($http) {
            var list = {};
            list.ChangeRole = function (id) {
                return $http({
                    method: "GET",
                    url: "/Employee/RolePermission/ChangeRole?groupId=" + id
                });
            }

            list.GetAllRole = function (id) {
                return $http({
                    method: "GET",
                    url: "/Employee/RolePermission/GetAllRole"
                });
            };

            list.SavePermission = function (permissionData, groupId) {
                var data = JSON.stringify(permissionData);
                return $http({
                    method: "POST",
                    url: "/Employee/RolePermission/SavePermission?groupId=" + groupId,
                    data: { rolePermissionModel: data },
                    contentType: "application/json",
                    dataType: "json"
                })
            }

            return list;
        }])
})()