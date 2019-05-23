(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("EmployeeShiftService", ["$http",
            function ($http) {
                var EmployeeShiftViewModel = {};
                var _AddShift = function (shiftData) {
                    return $http({
                        method: "POST",
                        url: "/Employee/EmployeeShift/InsertShift",
                        data: shiftData
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                var _DeleteShift = function (id) {
                    return $http({
                        method: "POST",
                        url: "/Employee/EmployeeShift/DeleteShift",
                        data: '{id:"' + id + '"}'
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })

                }
                var _UpdateShift = function (shiftData) {
                    return $http({
                        method: "POST",
                        url: "/Employee/EmployeeShift/UpdateShift",
                        data: shiftData
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }
                EmployeeShiftViewModel.AddShift= _AddShift;
                EmployeeShiftViewModel.UpdateShift= _UpdateShift;
                EmployeeShiftViewModel.DeleteShift= _DeleteShift;
                return EmployeeShiftViewModel


            }])
})()
