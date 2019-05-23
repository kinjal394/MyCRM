(function () {
    "use strict";
    angular.module("CRMApp.Controllers", [])
     .controller("RolePermissionCtrl", [
      "$scope", "$rootScope", "$timeout", "$filter", "RolePermissionService", "$uibModal",
      RolePermissionCtrl
     ]);

    function RolePermissionCtrl($scope, $rootScope, $timeout, $filter, RolePermissionService, $uibModal) {
        GetAllRole();
        $scope.RolePId = 0;
        $scope.IsAddSelectAll = true;
        $scope.IsEditSelectAll = true;
        $scope.IsViewSelectAll = true;
        $scope.IsDeleteSelectAll = true;
        function GetAllRole() {
            RolePermissionService.GetAllRole().then(function (result) {
                $scope.RoleList = result.data;
            })
        }
        $scope.SavePermission = function () {
            RolePermissionService.SavePermission($scope.RolePermissionList,$scope.RolePId.RoleId).then(function (result) {
                //$scope.RoleList = result.data;
            })
        }
        $scope.ViewSelectAll = function () {
            if (!$scope.IsViewSelectAll) {
                _.each($scope.RolePermissionList, function (item) { item.IsView = false });
            }
            else {
                _.each($scope.RolePermissionList, function (item) { item.IsView = true });
            }
        }
        $scope.AddSelectAll = function () {
            if (!$scope.IsAddSelectAll) {
                _.each($scope.RolePermissionList, function (item) { item.IsAdd = false });
            }
            else {
                _.each($scope.RolePermissionList, function (item) { item.IsAdd = true });
            }
        }

        $scope.EditSelectAll = function () {
            if (!$scope.IsEditSelectAll) {
                _.each($scope.RolePermissionList, function (item) { item.IsEdit = false });
            }
            else {
                _.each($scope.RolePermissionList, function (item) { item.IsEdit = true });
            }
        }

        $scope.DeleteSelectAll = function () {
            if (!$scope.IsDeleteSelectAll) {
                _.each($scope.RolePermissionList, function (item) { item.IsDelete = false });
            }
            else {
                _.each($scope.RolePermissionList, function (item) { item.IsDelete = true });
            }
        }

        $scope.ChangeRole = function () {
            $scope.IsAddSelectAll = true;
            $scope.IsEditSelectAll = true;
            $scope.IsViewSelectAll = true;
            $scope.IsDeleteSelectAll = true;
            RolePermissionService.ChangeRole($scope.RolePId.RoleId).then(function (result) {
                $scope.RolePermissionList = result.data;
                _.each($scope.RolePermissionList, function (item) {
                    if (item.IsView == false) {
                        $scope.IsViewSelectAll = false;
                    }
                    if (item.IsAdd == false) {
                        $scope.IsAddSelectAll = false;
                    }
                    if (item.IsEdit == false) {
                        $scope.IsEditSelectAll = false;
                    }
                    if (item.IsDelete == false) {
                        $scope.IsDeleteSelectAll = false;
                    }
                })
            })
        }
    }
})()
