(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("UnitService", ["$http",
            function ($http) {
                var UnitViewModel = {};


                var _AddUnit= function (UnitData) {
                    return $http({
                        method: "POST",
                        url: "/Master/Unit/SaveUnit",
                        data: UnitData
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                var _DeleteUnit= function (unitId) {
                    return $http({
                        method: "POST",
                        url: "/Master/Unit/DeleteUnit",
                        data: '{id:"' + unitId + '"}'
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })

                }
                var _UpdateUnit= function (UnitData) {
                    return $http({
                        method: "POST",
                        url: "/Master/Unit/UpdateUnit",
                        data: UnitData
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                UnitViewModel.AddUnit = _AddUnit;
                UnitViewModel.UpdateUnit = _UpdateUnit;
                UnitViewModel.DeleteUnit= _DeleteUnit;
                return UnitViewModel


            }])
})()
