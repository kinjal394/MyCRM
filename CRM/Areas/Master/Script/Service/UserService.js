
(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("UserService", ["$http",
            function ($http) {
                var list = {};
                list.getAllUser = function () {
                    return $http({
                        method: "POST",
                        url: "/Master/User"
                    });
                }

                list.DepartmentBind = function () {
                    return $http({
                        method: "Get",
                        url: "/Master/User/DepartmentBind"
                    });
                }

                list.MobCodesBind = function () {
                    return $http({
                        method: "Get",
                        url: "/Master/User/MobCodesBind"
                    });
                }
                list.RoleBind = function () {
                    return $http({
                        method: "Get",
                        url: "/Master/User/RoleBind"
                    })
                }
                list.UserInfo = function () {
                    return $http({
                        method: "POST",
                        url: "/Master/User/UserInfo"
                    });
                }

                list.ReportingBind = function () {
                    return $http({
                        method: "Get",
                        url: "/Master/User/ReportingBind"
                    })
                }
                list.AddUser = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/User/SaveUser",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.GetUserById = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/User/GetUserById?UserId=" + data
                    })
                }
                list.DeleteUser = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/User/DeleteUser?UserId=" + data
                    })
                }
                // GETById DATA
                list.ActiveInActiveStatus = function (id) {
                    return $http({
                        method: "POST",
                        url: "/Master/User/ActiveInActiveStatus?UserId=" + id
                    });
                }
                list.CheckUser = function (data) {
                    return $http({
                        method: "POST",
                        data: data,
                        url: "/Master/User/CheckUser"
                    });
                }
                return list;
            }])
})()