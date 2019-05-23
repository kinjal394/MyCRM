(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("UnitController", [
               "$scope", "UnitService", "$filter", "$uibModal",
                function UnitController($scope, UnitService, $filter, $uibModal) {
                    $scope.storage = {};

                    $scope.AddUnit = function () {
                        var _isdisable = 0;
                        $scope.UnitObj = {
                            UnitId: 0,
                            UnitName: ''
                        }
                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'unitModalContent.html',
                            controller: 'UnitModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                UnitObj: function () {
                                    return $scope.UnitObj;
                                },
                                storage: function () {
                                    return $scope.storage;
                                },
                                isdisable: function () { return _isdisable; }
                            }
                        });

                        modalInstance.result.then(function () {
                            $scope.refreshGrid()
                        }, function () {
                            // $log.info('Modal dismissed at: ' + new Date());
                        });
                    }

                    $scope.setDirectiveRefresh = function (refreshGrid) {
                        $scope.refreshGrid = refreshGrid;
                    };

                    $scope.gridObj = {
                        columnsInfo: [
                           //{ "title": "Unit Id", "data": "UnitId", filter: false, visible: false },
                           { "title": "Sr.", "field": "RowNumber", show: true, },
                           { "title": "Unit Name", "field": "UnitName", sortable: "UnitName", filter: { UnitName: "text" }, show: true, },

                           {
                               "title": "Action", show: true,
                               'cellTemplte': function ($scope, row) {
                                   var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                              //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.UnitId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                              '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                                   return $scope.getHtml(element);
                               }
                           }
                        ],
                        Sort: { 'UnitId': 'asc' }

                    }

                    $scope.Edit = function (data) {
                        var _isdisable = 0;
                        $scope.UnitObj = {
                            UnitId: data.UnitId,
                            UnitName: data.UnitName
                        }
                        $scope.storage = angular.copy($scope.UnitObj);
                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'unitModalContent.html',
                            controller: 'UnitModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                UnitObj: function () {
                                    return $scope.UnitObj;
                                },
                                storage: function () {
                                    return $scope.storage;
                                },
                                isdisable: function () { return _isdisable; }
                            }
                        });

                        modalInstance.result.then(function () {
                            $scope.refreshGrid()
                        }, function () {
                            // $log.info('Modal dismissed at: ' + new Date());
                        });
                    }
                    $scope.View = function (data) {
                        var _isdisable = 1;
                        $scope.UnitObj = {
                            UnitId: data.UnitId,
                            UnitName: data.UnitName
                        }
                        $scope.storage = angular.copy($scope.UnitObj);
                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'unitModalContent.html',
                            controller: 'UnitModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                UnitObj: function () {
                                    return $scope.UnitObj;
                                },
                                storage: function () {
                                    return $scope.storage;
                                },
                                isdisable: function () { return _isdisable; }
                            }
                        });

                        modalInstance.result.then(function () {
                            $scope.refreshGrid()
                        }, function () {
                            // $log.info('Modal dismissed at: ' + new Date());
                        });
                    }
                    $scope.Delete = function (unitID) {
                        UnitService.DeleteUnit(unitID).then(function (result) {
                            if (result.data.ResponseType == 1) {
                                toastr.success(result.data.Message)
                            }
                            else {
                                toastr.error(result.data.Message)
                            }
                            $scope.refreshGrid()
                        })

                    }
                }]);

    angular.module('CRMApp.Controllers')
        .controller('UnitModalInstanceCtrl', ['$scope',
            '$uibModalInstance', 'UnitObj', 'UnitService', 'storage', 'isdisable',
            function ($scope, $uibModalInstance, UnitObj, UnitService, storage, isdisable) {
                $scope.isClicked = false;
                if (isdisable == 1) {
                    $scope.isClicked = true;
                }
                var $ctrl = this;
                $ctrl.ok = function () {
                    $uibModalInstance.close();
                };

                $ctrl.UnitObj = UnitObj;
                $ctrl.storage = storage;
                $ctrl.cancel = function () {
                    $uibModalInstance.dismiss('cancel');
                };

                $ctrl.SaveUnit = function () {
                    UnitService.AddUnit($ctrl.UnitObj).then(function (result) {
                        if (result.data.ResponseType == 1) {
                            $uibModalInstance.close();
                            toastr.success(result.data.Message)
                        }
                        else {
                            toastr.error(result.data.Message)
                        }
                    }).error(function (e) {
                        toastr.error("Error Found")
                    })
                }

                $ctrl.UpdateUnit = function () {
                    UnitService.UpdateUnit($ctrl.UnitObj).then(function (result) {
                        if (result.data.ResponseType == 1) {
                            $uibModalInstance.close();
                            toastr.success(result.data.Message)
                        }
                        else {
                            toastr.error(result.data.Message)
                        }
                        //$uibModalInstance.close();
                    }).error(function () {
                        toastr.error("Error found")
                    })
                }

                $ctrl.Reset = function () {

                    if ($ctrl.UnitObj.UnitId > 0) {

                        $ctrl.UnitObj = angular.copy($ctrl.storage);

                    } else {
                        ResetForm();
                    }
                }


                function ResetForm() {

                    $ctrl.UnitObj = {
                        UnitId: 0,
                        UnitName: '',

                    }
                    $ctrl.storage = {};
                    $ctrl.addMode = true;
                    $ctrl.saveText = "Save";
                    if ($ctrl.$parent.FormUnit)
                        $ctrl.$parent.FormUnit.$setPristine();
                }

            }]);
})();