(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("RoleController", [
               "$scope", "RoleService", "$filter", "$uibModal",
                function RoleController($scope, RoleService, $filter, $uibModal) {
                    $scope.storage = {};
                    $scope.AddRole = function () {
                        var _isdisable = 0;
                        $scope.RoleObj = {
                            RoleId: 0,
                            RoleName: ''
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
                                RoleObj: function () {
                                    return $scope.RoleObj;
                                },
                                storage: function () {
                                    return $scope.storage;
                                },
                                isdisable: function () { return _isdisable; }
                            }
                        });

                        modalInstance.result.then(function () {
                            $scope.refreshGrid();
                            $ctrl.selected = selectedItem;
                        }, function () {
                        });
                    }

                    $scope.setDirectiveRefresh = function (refreshGrid) {
                        $scope.refreshGrid = refreshGrid;
                    };

                    $scope.gridObj = {
                        columnsInfo: [
                           //{ "title": "Role Id", "data": "RoleId", filter: false, visible: false },
                           { "title": "Sr.", "field": "RowNumber", show: true, },
                           { "title": "Role Name", "field": "RoleName", sortable: "RoleName", filter: { RoleName: "text" }, show: true, },
                           {
                               "title": "Action", show: true,
                               'cellTemplte': function ($scope, row) {
                                   var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                              //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.RoleId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                              '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                                   return $scope.getHtml(element);
                               }
                           }
                        ],
                        Sort: { 'RoleId': 'asc' }
                    }

                    $scope.Edit = function (data) {
                        var _isdisable = 0;
                        $scope.RoleObj = {
                            RoleId: data.RoleId,
                            RoleName: data.RoleName
                        }
                        $scope.storage = angular.copy($scope.RoleObj);
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
                                RoleObj: function () {
                                    return $scope.RoleObj;
                                },
                                storage: function () {
                                    return $scope.storage;
                                },
                                isdisable: function () { return _isdisable; }
                            }
                        });

                        modalInstance.result.then(function (selectedItem) {
                            $scope.refreshGrid();
                        }, function () {
                        });
                    }

                    $scope.View = function (data) {
                        var _isdisable = 1;
                        $scope.RoleObj = {
                            RoleId: data.RoleId,
                            RoleName: data.RoleName
                        }
                        $scope.storage = angular.copy($scope.RoleObj);
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
                                RoleObj: function () {
                                    return $scope.RoleObj;
                                },
                                storage: function () {
                                    return $scope.storage;
                                },
                                isdisable: function () { return _isdisable; }
                            }
                        });

                        modalInstance.result.then(function (selectedItem) {
                            $scope.refreshGrid();
                        }, function () {
                        });
                    }


                    $scope.Delete = function (RoleId) {
                        RoleService.DeleteRole(RoleId).then(function (result) {
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

    angular.module('CRMApp.Controllers').controller('ModalInstanceCtrl', [
        '$scope', '$uibModalInstance', 'RoleObj', 'RoleService', 'storage', 'isdisable',
        function ($scope, $uibModalInstance, RoleObj, RoleService, storage, isdisable) {

            $scope.isClicked = false;
            if (isdisable == 1) {
                $scope.isClicked = true;
            }

            var $ctrl = this;

            $ctrl.ok = function () {
                $uibModalInstance.close();
            };

            $ctrl.RoleObj = RoleObj;
            $ctrl.storage = storage;
            $ctrl.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };

            $ctrl.saveRole = function (RoleData) {
                RoleService.AddRole(RoleData).then(function (result) {
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

            $ctrl.UpdateRole = function (Role) {
                RoleService.Update(Role).then(function (result) {
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

                if ($ctrl.RoleObj.RoleId > 0) {

                    $ctrl.RoleObj = angular.copy($ctrl.storage);

                } else {
                    ResetForm();
                }
            }


            function ResetForm() {

                $ctrl.RoleObj = {
                    RoleId: 0,
                    RoleName: '',

                }
                $ctrl.storage = {};
                $ctrl.addMode = true;
                $ctrl.saveText = "Save";
                if ($ctrl.$parent.FormRole)
                    $ctrl.$parent.FormRole.$setPristine();
            }
        }]);
})();