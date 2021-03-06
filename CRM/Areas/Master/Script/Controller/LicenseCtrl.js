﻿(function () {
    "use strict"
    angular.module("CRMApp.Controllers")
        .controller("LicenseCtrl", [
        "$scope", "$rootScope", "$timeout", "$filter", "LicenseService", "NgTableParams", "$uibModal",
        function LicenseCtrl($scope, $rootScope, $timeout, $filter, LicenseService, NgTableParams, $uibModal) {

            $scope.Add = function (id, _isdisable) {
                if (_isdisable === undefined) _isdisable = 0;

                var modalInstance = $uibModal.open({
                    backdrop: 'static',
                    templateUrl: 'myModalContent.html',
                    controller: ModalInstanceCtrl,
                    resolve: {
                        LicenseService: function () { return LicenseService; },
                        id: function () { return id; },
                        isdisable: function () { return _isdisable; }
                    }
                });
                modalInstance.result.then(function () {
                    $scope.refreshGrid()
                }, function () {

                });
            }
             
            $scope.setDirectiveRefresh = function (refreshGrid) {
                $scope.refreshGrid = refreshGrid;
            };

            $scope.gridObj = {
                columnsInfo: [
                    //{ "title": "License Id", "data": "LicenseId", filter: false, visible: false },
                    { "title": "Sr.", "field": "RowNumber", show: true, },
                    { "title": "License Name", "field": "LicenseName", sortable: "LicenseName", filter: { LicenseName: "text" }, show: true, },
                    {
                        "title": "Action", show: true,
                        'cellTemplte': function ($scope, row) {
                            var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.LicenseId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                                 //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.LicenseId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                                 '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.LicenseId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                            return $scope.getHtml(element);
                        }
                    }
                ],
                Sort: { "LicenseId": "asc" }
            }

            $scope.Edit = function (id) {
                $scope.Add(id, 0);
                modalInstance.result.then(function () {
                    $scope.refreshGrid()
                }, function () {
                });
            }

            $scope.View = function (id) {
                $scope.Add(id, 1);
                modalInstance.result.then(function () {
                    $scope.refreshGrid()
                }, function () {
                });
            }

            $scope.Delete = function (id) {
                LicenseService.DeleteLicense(id).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        toastr.success(result.data.Message);
                    } else {
                        toastr.error(result.data.Message, 'Opps, Something went wrong');
                    }
                    $scope.refreshGrid()
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }

            $scope.RefreshTable = function () {
                $scope.tableParams.reload();
            };
        }]);

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, LicenseService, id, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.objLicense = $scope.objLicense || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.id = id;
        $scope.storage = {};

        if (id && id > 0) {
            LicenseService.GetByIdLicense(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objLicense = {
                        LicenseId: result.data.DataList.LicenseId,
                        LicenseName: result.data.DataList.LicenseName
                    }
                    $scope.storage = angular.copy($scope.objLicense);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        function ResetForm() {
            $scope.objLicense = {
                LicenseId:'0',
                LicenseName:''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormAgencyTypeInfo)
                $scope.$parent.FormAgencyTypeInfo.$setPristine();
        }

        $scope.CreateUpdate = function (data) {
            LicenseService.CreateUpdateLicense(data).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    ResetForm();
                    $uibModalInstance.close();
                } else {
                    toastr.error(result.data.Message);
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.Reset = function () {
            if ($scope.objLicense.LicenseId > 0) {
                $scope.objLicense = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'LicenseService', 'id', 'isdisable']
})()