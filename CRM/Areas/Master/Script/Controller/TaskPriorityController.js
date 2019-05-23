(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("TaskPriorityController", [
               "$scope", "TaskPriorityService", "$filter", "$uibModal",
                function TaskPriorityController($scope, TaskPriorityService, $filter, $uibModal) {
                    $scope.storage = {};
                    $scope.AddTaskPriority = function () {
                        var _isdisable = 0;
                        $scope.TaskPriorityObj = {
                            PriorityId: 0,
                            PriorityName: ''
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
                                TaskPriorityObj: function () {
                                    return $scope.TaskPriorityObj;
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
                        });
                    }

                    $scope.setDirectiveRefresh = function (refreshGrid) {
                        $scope.refreshGrid = refreshGrid;
                    };

                    $scope.gridObj = {
                        columnsInfo: [
                           //{ "title": "Priority Id", "data": "PriorityId", filter: false, visible: false },
                           { "title": "Sr.", "field": "RowNumber", show: true, },
                           { "title": "PriorityName", "field": "PriorityName", sortable: "PriorityName", filter: { PriorityName: "text" }, show: true, },

                           {
                               "title": "Action", show: true,
                               'cellTemplte': function ($scope, row) {
                                   var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                              //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.PriorityId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                              '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                                   return $scope.getHtml(element);
                               }
                           }
                        ],
                        Sort: { 'PriorityId': 'asc' }

                    }

                    $scope.Edit = function (data) {
                        var _isdisable = 0;
                        $scope.TaskPriorityObj = {
                            PriorityId: data.PriorityId,
                            PriorityName: data.PriorityName
                        }
                        $scope.storage = angular.copy($scope.TaskPriorityObj);
                        var modalInstance = $uibModal.open({
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'unitModalContent.html',
                            controller: 'UnitModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                TaskPriorityObj: function () {
                                    return $scope.TaskPriorityObj;
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
                        });
                    }

                    $scope.View = function (data) {
                        var _isdisable = 1;
                        $scope.TaskPriorityObj = {
                            PriorityId: data.PriorityId,
                            PriorityName: data.PriorityName
                        }
                        $scope.storage = angular.copy($scope.TaskPriorityObj);
                        var modalInstance = $uibModal.open({
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'unitModalContent.html',
                            controller: 'UnitModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                TaskPriorityObj: function () {
                                    return $scope.TaskPriorityObj;
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
                        });
                    }

                    $scope.Delete = function (priorityID) {
                        TaskPriorityService.DeleteTaskPriority(priorityID).then(function (result) {
                            if (result.data.ResponseType == 1) {
                                toastr.success(result.data.Message)
                                $scope.refreshGrid()
                            }
                            else {
                                toastr.error(result.data.Message)
                            }
                        }, function (errorMsg) {
                            toastr.error(errorMsg, 'Opps, Something went wrong');
                        })
                    }
                }]);

    angular.module('CRMApp.Controllers')
        .controller('UnitModalInstanceCtrl', ['$scope',
            '$uibModalInstance', 'TaskPriorityObj', 'TaskPriorityService', 'storage', 'isdisable',
            function ($scope, $uibModalInstance, TaskPriorityObj, TaskPriorityService, storage, isdisable) {
                $scope.isClicked = false;
                if (isdisable == 1) {
                    $scope.isClicked = true;
                }
                var $ctrl = this;

                $ctrl.ok = function () {
                    $uibModalInstance.close();
                };

                $ctrl.TaskPriorityObj = TaskPriorityObj;
                $ctrl.storage = storage;
                $ctrl.cancel = function () {
                    $uibModalInstance.dismiss('cancel');
                };

                $ctrl.SaveTaskPriority = function () {
                    TaskPriorityService.AddTaskPriority($ctrl.TaskPriorityObj).then(function (result) {
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

                $ctrl.UpdateTaskPriority = function () {
                    TaskPriorityService.UpdateTaskPriority($ctrl.TaskPriorityObj).then(function (result) {
                        if (result.data.ResponseType == 1) {
                            $uibModalInstance.close();
                            toastr.success(result.data.Message)
                        }
                        else {
                            toastr.error(result.data.Message)
                        }
                        //$uibModalInstance.close();
                    }, function (errorMsg) {
                        toastr.error(errorMsg, 'Opps, Something went wrong');
                    })
                }

                $ctrl.Reset = function () {

                    if ($ctrl.TaskPriorityObj.PriorityId > 0) {

                        $ctrl.TaskPriorityObj = angular.copy($ctrl.storage);

                    } else {
                        ResetForm();
                    }
                }


                function ResetForm() {

                    $ctrl.TaskPriorityObj = {
                        PriorityId: 0,
                        PriorityName: '',

                    }
                    $ctrl.storage = {};
                    $ctrl.addMode = true;
                    $ctrl.saveText = "Save";
                    if ($ctrl.$parent.FormTaskPriority)
                        $ctrl.$parent.FormTaskPriority.$setPristine();
                }

            }]);
})();