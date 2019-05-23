(function () {

    "use strict";
    angular.module("CRMApp.Services")
            .service("RoleService", ["$http",
            function ($http) {
                var RoleModel = {};


                var _AddRole = function (RoleData) {
                    return $http({
                        method: "POST",
                        url: "/Master/Role/SaveRole",
                        data: RoleData
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                var _DeleteRole = function (RoleId) {
                    return $http({
                        method: "POST",
                        url: "/Master/Role/DeleteRole",
                        data: '{id:"' + RoleId + '"}'
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })

                }
                var _UpdateRole = function (RoleData) {
                    return $http({
                        method: "POST",
                        url: "/Master/Role/UpdateRole",
                        data: RoleData
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                RoleModel.AddRole = _AddRole;
                RoleModel.Update = _UpdateRole;
                RoleModel.DeleteRole = _DeleteRole;
                return RoleModel


            }])
})()
