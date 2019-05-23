(function () {
    "use strict";
    angular.module("CRMApp.Controllers", [])
           .controller("AreaController", [
               "$scope", "AreaService", "$filter", "$uibModal",
                function AreaController($scope, AreaService, $filter, $uibModal) {
                    $scope.storage = {};

                    $scope.AddArea = function () {
                        $scope.AreaObj = {
                            AreaId: 0,
                            CityId: '',
                            AreaName: '',
                            CountryName: { Display: '', Value: '' },
                            StateName: { Display: '', Value: '' },
                            CityName: { Display: '', Value: '' },
                        }

                        $scope.SetAreaId = function (id) {
                            if (id > 0) {
                                $scope.SrNo = id;
                                $scope.addMode = false;
                                $scope.saveText = "Update";
                                $scope.GetProductById(id);
                            } else {
                                $scope.SrNo = 0;
                                $scope.addMode = true;
                                $scope.saveText = "Save";
                            }
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
                                AreaObj: function () {
                                    return $scope.AreaObj;
                                },
                                storage: function () {
                                    return $scope.storage;
                                }
                            }
                        });

                        modalInstance.result.then(function () {
                            $scope.refreshGrid();
                            $ctrl.selected = selectedItem;

                        }, function () {
                            // $log.info('Modal dismissed at: ' + new Date());
                        });
                    }

                    $scope.setDirectiveRefresh = function (refreshGrid) {
                        $scope.refreshGrid = refreshGrid;
                    };

                    $scope.gridObj = {
                        columnsInfo: [
                           { "title": "Area Id", "data": "AreaId", filter: false, visible: false },
               { "title": "Sr.", "data": "RowNumber", filter: false, sort: false },
                           { "title": "Area Name", "data": "AreaName", sort: true, filter: true, },
                           { "title": "City/District", "data": "CityName", sort: true, filter: true, },
                           { "title": "CityId", "data": "CityId", sort: true, filter: true, visible: false },
                           { "title": "StateName", "data": "StateName", sort: true, filter: true, visible: false },
                           { "title": "CountryName", "data": "CountryName", sort: true, filter: true, visible: false },
                           { "title": "CountryId", "data": "CountryId", sort: true, filter: true, visible: false },
                           { "title": "StateId", "data": "StateId", sort: true, filter: true, visible: false },
                           {
                               "title": "Action", sort: false, filter: false,
                               //'render': '<button class="btn btn-success" data-ng-click="$parent.$parent.$parent.Edit(row)">Edit</button><button class="btn btn-success" data-ng-click="$parent.$parent.$parent.Delete(row.EventTypeId)">Delete</button> '

                               'render': '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                              '<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.AreaId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '

                           }
                        ],
                        Sort: { 'CurrencyId': 'asc' }

                    }
                    $scope.Edit = function (data) {
                        //console.log(data);
                        $scope.AreaObj = {
                            AreaId: data.AreaId,
                            AreaName: data.AreaName,
                            CurrencySymbol: data.CurrencySymbol,
                            CountryName: { Display: data.CountryName, Value: data.CountryId },
                            StateName: { Display: data.StateName, Value: data.StateId },
                            CityName: { Display: data.CityName, Value: data.CityId },
                        }
                        $scope.storage = angular.copy($scope.AreaObj);

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
                                AreaObj: function () {
                                    return $scope.AreaObj;
                                },
                                storage: function () {
                                    return $scope.storage;
                                }
                                 
                            }
                        });

                        modalInstance.result.then(function (selectedItem) {
                            //$scope.tableParams.reload();
                            $scope.refreshGrid();
                        }, function () {
                            // $log.info('Modal dismissed at: ' + new Date());
                        });
                    }

                    $scope.Delete = function (AreaId) {
                        AreaService.DeleteArea(AreaId).then(function (result) {
                            if (result.data.ResponseType == 1) {

                                toastr.success(result.data.Message)
                            }
                            else {
                                toastr.error(result.data.Message)
                            }
                            $scope.refreshGrid();
                        }, function (errorMsg) {
                            toastr.error(errorMsg, 'Opps, Something went wrong');
                        })

                    }
                }]);

    angular.module('CRMApp.Controllers').controller('ModalInstanceCtrl', ['$scope', '$uibModalInstance', 'AreaObj', 'storage', 'AreaService',
        function ($scope, $uibModalInstance, AreaObj, storage, AreaService) {
            var $ctrl = this;

            $ctrl.ok = function () {
                $uibModalInstance.close();
            };

            $ctrl.objArea = AreaObj;
            $ctrl.storage = storage;
            $ctrl.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.$watch(function () { return $ctrl.objArea.CountryName }, function (newval, oldval) {
                if (newval.Value && newval.Value > 0) {
                    AreaService.GetStateList(newval.Value).then(function (result) {
                        $ctrl.objArea.StateName.Value = result.data;
                    }, function (errorMsg) {
                        toastr.error(errorMsg, 'Opps, Something went wrong');
                    })
                }
            })

            $ctrl.Create = function (AreaData) {
                var Area = {
                    AreaName: AreaData.AreaName,
                    CityId: AreaData.CityName.Value
                }
                AreaService.AddArea(Area).then(function (result) {
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

            $ctrl.Update = function (AreaData) {
                var Area = {
                    IsActive: true,
                    AreaId: AreaData.AreaId,
                    AreaName: AreaData.AreaName,
                    CityId: AreaData.CityName.Value
                }
                AreaService.Update(Area).then(function (result) {
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

                if ($ctrl.objArea.AreaId > 0) {

                    $ctrl.objArea = angular.copy($ctrl.storage);
                   
                    } else {
                    ResetForm();
                }
            }
          

            function ResetForm() {

                $ctrl.objArea = {
                    AreaId: 0,
                    CityName: '',
                    AreaName: '',
                    StateName: '',
                    CountryName: ''
                }
                $ctrl.storage = {};
                $ctrl.addMode = true;
                $ctrl.saveText = "Save";
                if ($ctrl.$parent.FormAreaInfo)
                    $ctrl.$parent.FormAreaInfo.$setPristine();
            }

        }]);
})();