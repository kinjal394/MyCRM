(function () {
    "use strict"
    angular.module("CRMApp.Controllers")
            .controller("AcHolderCtrl", [
            "$scope", "$rootScope", "$timeout", "$filter", "AcHolderService", "NgTableParams", "$uibModal",
            function AcHolderCtrl($scope, $rootScope, $timeout, $filter, AcHolderService, NgTableParams, $uibModal) {

                $scope.Add = function (id, _isdisable) {
                    if (_isdisable === undefined) _isdisable = 0;

                    var modalInstance = $uibModal.open({
                        backdrop: 'static',
                        templateUrl: 'myModalContent.html',
                        controller: ModalInstanceCtrl,
                        resolve: {
                            AcHolderService: function () { return AcHolderService; },
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
                       //{ "title": "AcHolder Code", "data": "AcHolderCode", filter: false, visible: false },
                       { "title": "Sr.", "field": "RowNumber", filter: false, show: true },
                       { "title": "AcHolder Name", "field": "AcHolderName", sortable: "AcHolderName", filter: { AcHolderName: "text" }, show: true },
                       {
                           "title": "Action", show: true,
                           'cellTemplte': function ($scope, row) {
                               var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.AcHolderCode)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                                            //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.$parent.$parent.$parent.Delete(row.AgencyTypeId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                                            '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.AcHolderCode)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>';
                               return $scope.getHtml(element);
                           }
                       }
                    ],
                    Sort: { "AcHolderCode": "asc" }
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
                    AcHolderService.DeleteAcHolder(id).then(function (result) {
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

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, AcHolderService, id, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.objAcHolder = $scope.objAcHolder || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.id = id;
        $scope.storage = {};

        if (id && id > 0) {
            AcHolderService.GetByIdAcHolder(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objAcHolder = {
                        AcHolderCode: result.data.DataList.AcHolderCode,
                        AcHolderName: result.data.DataList.AcHolderName
                    }
                    $scope.storage = angular.copy($scope.objAcHolder);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        function ResetForm() {

            $scope.objAcHolder = {
                AcHolderCode: 0,
                AcHolderName: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormAcHolderInfo)
                $scope.$parent.FormAcHolderInfo.$setPristine();
        }

        $scope.CreateUpdate = function (data) {

            AcHolderService.CreateUpdateAcHolder(data).then(function (result) {
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

            if ($scope.objAcHolder.AcHolderCode > 0) {
                $scope.objAcHolder = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'AcHolderService', 'id', 'isdisable']

})()