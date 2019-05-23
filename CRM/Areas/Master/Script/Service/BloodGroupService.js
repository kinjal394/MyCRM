(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("BloodGroupService", ["$http",
            function ($http) {
                var BloodGroupViewModel = {};


                var _AddBloodGroup = function (BloodGroupData) {
                    return $http({
                        method: "POST",
                        url: "/Master/BloodGroup/SaveBloodGroup",
                        data: BloodGroupData
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                var _DeleteBloodGroup = function (bloodgroupId) {
                    return $http({
                        method: "POST",
                        url: "/Master/BloodGroup/DeleteBloodGroup",
                        data: '{id:"' + bloodgroupId + '"}'
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })

                }
                var _UpdateBloodGroup = function (BloodGroupData) {
                    return $http({
                        method: "POST",
                        url: "/Master/BloodGroup/UpdateBloodGroup",
                        data: BloodGroupData
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                BloodGroupViewModel.AddBloodGroup = _AddBloodGroup;
                BloodGroupViewModel.UpdateBloodGroup = _UpdateBloodGroup;
                BloodGroupViewModel.DeleteBloodGroup = _DeleteBloodGroup;
                return BloodGroupViewModel


            }])
})()
