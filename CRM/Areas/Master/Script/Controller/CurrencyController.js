(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("CurrencyController", [
               "$scope", "CurrencyService", "$filter", "$uibModal",
                function CurrencyController($scope, CurrencyService, $filter, $uibModal) {
                    $scope.storage = {};
                    $scope.AddCurrency = function () {
                        var _isdisable = 0;
                        $scope.CurrencyObj = {
                            CurrencyId: 0,
                            CurrencyName: '',
                            CurrencySymbol: '',
                            CurrencyCode: ''
                        }
                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'myModalContent.html',
                            controller: 'ModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                CurrencyObj: function () {
                                    return $scope.CurrencyObj;
                                },
                                storage: function () {
                                    return $scope.storage;
                                },
                                isdisable: function () { return _isdisable; }
                            }
                        });
                        modalInstance.result.then(function () {
                            $scope.refreshGrid();
                            //$ctrl.selected = selectedItem;
                        }, function () {
                        });
                    }

                    $scope.setDirectiveRefresh = function (refreshGrid) {
                        $scope.refreshGrid = refreshGrid;
                    };

                    $scope.gridObj = {
                        columnsInfo: [
                          // { "title": "Currency Id", "data": "CurrencyId", filter: false, visible: false },
                            { "title": "Sr.", "field": "RowNumber", show: true, },
                            { "title": "Currency Code", "field": "CurrencyCode", sortable: "CurrencyCode", filter: { CurrencyCode: "text" }, show: true, },
                           { "title": "Currency Name", "field": "CurrencyName", sortable: "CurrencyName", filter: { CurrencyName: "text" }, show: true, },
                            { "title": "Currency Symbol", "field": "CurrencySymbol", sortable: "CurrencySymbol", filter: { CurrencySymbol: "text" }, show: true, },

                           {
                               "title": "Action", show: true,
                               //'render': '<button class="btn btn-success" data-ng-click="$parent.$parent.$parent.Edit(row)">Edit</button><button class="btn btn-success" data-ng-click="$parent.$parent.$parent.Delete(row.EventTypeId)">Delete</button> '

                               'cellTemplte': function ($scope, row) {
                                   var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                              //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.CurrencyId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                              '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                                   return $scope.getHtml(element);
                               }
                           }
                        ],
                        Sort: { 'CurrencyId': 'asc' }

                    }

                    $scope.Edit = function (data) {
                        var _isdisable = 0;
                        $scope.CurrencyObj = {
                            CurrencyId: data.CurrencyId,
                            CurrencyName: data.CurrencyName,
                            CurrencySymbol: data.CurrencySymbol,
                            CurrencyCode: data.CurrencyCode
                        }
                        $scope.storage = angular.copy($scope.CurrencyObj);
                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'myModalContent.html',
                            controller: 'ModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                CurrencyObj: function () {
                                    return $scope.CurrencyObj;
                                },
                                storage: function () {
                                    return $scope.storage;
                                },
                                isdisable: function () { return _isdisable; }
                            }
                        });

                        modalInstance.result.then(function (selectedItem) {
                            //$scope.tableParams.reload();
                            $scope.refreshGrid();
                        }, function () {
                            // $log.info('Modal dismissed at: ' + new Date());
                        });
                    }
                    $scope.View = function (data) {
                        var _isdisable = 1;
                        $scope.CurrencyObj = {
                            CurrencyId: data.CurrencyId,
                            CurrencyName: data.CurrencyName,
                            CurrencySymbol: data.CurrencySymbol,
                            CurrencyCode: data.CurrencyCode
                        }
                        $scope.storage = angular.copy($scope.CurrencyObj);
                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'myModalContent.html',
                            controller: 'ModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                CurrencyObj: function () {
                                    return $scope.CurrencyObj;
                                },
                                storage: function () {
                                    return $scope.storage;
                                },
                                isdisable: function () { return _isdisable; }
                            }
                        });

                        modalInstance.result.then(function (selectedItem) {
                            //$scope.tableParams.reload();
                            $scope.refreshGrid();
                        }, function () {
                            // $log.info('Modal dismissed at: ' + new Date());
                        });
                    }
                    $scope.Delete = function (CurrencyId) {
                        CurrencyService.DeleteCurrency(CurrencyId).then(function (result) {
                            if (result.data.ResponseType == 1) {
                                toastr.success(result.data.Message)
                                $scope.refreshGrid();
                            }
                            else {
                                toastr.error(result.data.Message)
                            }
                        }, function (errorMsg) {
                            toastr.error(errorMsg, 'Opps, Something went wrong');
                        })
                    }
                }]);

    angular.module('CRMApp.Controllers').controller('ModalInstanceCtrl', ['$scope',
        '$uibModalInstance', 'CurrencyObj', 'CurrencyService', 'storage', 'isdisable',
        function ($scope, $uibModalInstance, CurrencyObj, CurrencyService, storage, isdisable) {
            $scope.isClicked = false;
            if (isdisable == 1) {
                $scope.isClicked = true;
            }
            var $ctrl = this;

            $ctrl.ok = function () {
                $uibModalInstance.close();
            };

            $ctrl.CurrencyObj = CurrencyObj;
            $ctrl.storage = storage;
            $ctrl.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.setCurrencycode = function (data) {
                if (data) {
                    CurrencyService.AutoCurrencyCode(data).then(function (result) {
                        $ctrl.CurrencyObj.CurrencyCode = result.data.DataList.CurrencyData;
                    }, function (errorMsg) {
                        toastr.error(errorMsg, 'Opps, Something went wrong');
                    })
                }
            }

            $ctrl.saveCurrency = function (CurrencyData) {
                CurrencyService.AddCurrency(CurrencyData).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        $uibModalInstance.close();
                        toastr.success(result.data.Message)
                    }
                    else {
                        toastr.error(result.data.Message)
                    }
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }

            $ctrl.UpdateCurrency = function (Currency) {
                CurrencyService.Update(Currency).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        $uibModalInstance.close();
                        toastr.success(result.data.Message)
                    }
                    else {
                        toastr.error(result.data.Message)
                    }
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }

            $ctrl.Reset = function () {

                if ($ctrl.CurrencyObj.CurrencyId > 0) {

                    $ctrl.CurrencyObj = angular.copy($ctrl.storage);

                } else {
                    ResetForm();
                }
            }


            function ResetForm() {

                $ctrl.CurrencyObj = {
                    CurrencyId: 0,
                    CurrencyName: '',
                    CurrencySymbol: '',
                }
                $ctrl.storage = {};
                $ctrl.addMode = true;
                $ctrl.saveText = "Save";
                if ($ctrl.$parent.FormCurrecy)
                    $ctrl.$parent.FormCurrecy.$setPristine();
            }


        }]);
})();