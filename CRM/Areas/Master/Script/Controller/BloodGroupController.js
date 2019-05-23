(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("BloodGroupController", [
               "$scope", "BloodGroupService", "$filter", "$uibModal",
                function BloodGroupController($scope, BloodGroupService, $filter, $uibModal) {
                    $scope.storage = {};
                    $scope.AddBloodGroup = function () {
                        var _isdisable = 0;
                        $scope.BloodGroupObj = {
                            BloodGroupId: 0,
                            BloodGroup: ''
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
                                BloodGroupObj: function () {
                                    return $scope.BloodGroupObj;
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
                           //{ "title": "BloodGroup Id", "data": "BloodGroupId", filter: false, visible: false },
                           { "title": "Sr.", "field": "RowNumber", filter: false, show: true },
                           { "title": "BloodGroup", "field": "BloodGroup", sortable: "BloodGroup", filter: { BloodGroup: "text" }, show: true },

                           {
                               "title": "Action", show: true,
                               'cellTemplte': function ($scope, row) {
                                   var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                                //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.$parent.$parent.$parent.Delete(row.BloodGroupId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                               '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>';
                                   return $scope.getHtml(element);
                               }
                           }
                        ],
                        Sort: { 'BloodGroupId': 'asc' }

                    }

                    $scope.Edit = function (data, _isdisable) {
                        if (_isdisable === undefined) _isdisable = 0;
                        $scope.BloodGroupObj = {
                            BloodGroupId: data.BloodGroupId,
                            BloodGroup: data.BloodGroup
                        }
                        $scope.storage = angular.copy($scope.BloodGroupObj);
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
                                BloodGroupObj: function () {
                                    return $scope.BloodGroupObj;
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
                        $scope.Edit(data, 1);
                    }

                    $scope.Delete = function (bloodgroupID) {
                        BloodGroupService.DeleteBloodGroup(bloodgroupID).then(function (result) {
                            if (result.data.ResponseType == 1) {
                                toastr.success(result.data.Message)
                            }
                            else {
                                toastr.error(result.data.Message)
                            }
                            $scope.refreshGrid()
                        }, function (errorMsg) {
                            toastr.error(errorMsg, 'Opps, Something went wrong');
                        })
                    }
                }]);

    angular.module('CRMApp.Controllers').controller('UnitModalInstanceCtrl', ['$scope', '$uibModalInstance', 'BloodGroupObj', 'BloodGroupService', 'isdisable', 'storage',
        function ($scope, $uibModalInstance, BloodGroupObj, BloodGroupService, isdisable, storage) {
            $scope.isClicked = false;
            if (isdisable == 1) {
                $scope.isClicked = true;
            }
            var $ctrl = this;

            $ctrl.ok = function () {
                $uibModalInstance.close();
            };

            $ctrl.BloodGroupObj = BloodGroupObj;
            $ctrl.storage = storage;
            $ctrl.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };

            $ctrl.SaveBloodGroup = function () {
                BloodGroupService.AddBloodGroup($ctrl.BloodGroupObj).then(function (result) {
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

            $ctrl.UpdateBloodGroup = function () {
                BloodGroupService.UpdateBloodGroup($ctrl.BloodGroupObj).then(function (result) {
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

                if ($ctrl.BloodGroupObj.BloodGroupId > 0) {

                    $ctrl.BloodGroupObj = angular.copy($ctrl.storage);

                } else {
                    ResetForm();
                }
            }


            function ResetForm() {

                $ctrl.BloodGroupObj = {
                    BloodGroupId: 0,
                    BloodGroup: '',

                }
                $ctrl.storage = {};
                $ctrl.addMode = true;
                $ctrl.saveText = "Save";
                if ($ctrl.$parent.FormBloodGroupInfo)
                    $ctrl.$parent.FormBloodGroupInfo.$setPristine();
            }

        }]);
    //ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'BloodGroupObj', 'BloodGroupService', 'isdisable']
})();