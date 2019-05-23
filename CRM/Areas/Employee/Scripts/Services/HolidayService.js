(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("HolidayService", ["$http",
            function ($http) {
                var HolidayViewModel = {};


                var _AddHoliday = function (HolidayData) {
                    return $http({
                        method: "POST",
                        url: "/Employee/Holiday/SaveHoliday",
                        data: HolidayData
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                var _DeleteHoliday = function (id) {
                    return $http({
                        method: "POST",
                        url: "/Employee/Holiday/DeleteHoliday",
                        data: '{id:"' + id + '"}'
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })

                }
                var _UpdateHoliday = function (HolidayData) {
                    return $http({
                        method: "POST",
                        url: "/Employee/Holiday/UpdateHoliday",
                        data: HolidayData
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }
               
                var _GetAllReligion = function () {
                    return $http({
                        method: "POST",
                        url: "/Master/Religion/GetReligions"
                    });
                }
                var _GetAllHolidayName = function () {

                    return $http({
                        method: "POST",
                        url: "/Employee/Holiday/GetAllHolidayName"
                    }).success(function (result) {
                        return result
                    }).error(function (e) {
                        return e;})
                }

                HolidayViewModel.AddHoliday = _AddHoliday;
                HolidayViewModel.UpdateHoliday = _UpdateHoliday;
                HolidayViewModel.DeleteHoliday = _DeleteHoliday;
                HolidayViewModel.GetAllReligion = _GetAllReligion;
                HolidayViewModel.GetAllHolidayName = _GetAllHolidayName;
                return HolidayViewModel


            }])
})()
