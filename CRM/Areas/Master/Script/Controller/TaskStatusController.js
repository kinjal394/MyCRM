(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("TaskStatusController", [
               "$scope", "TaskStatusService", "$filter", "$uibModal",
                function TaskStatusController($scope, TaskStatusService, $filter, $uibModal) {
                    $scope.storage = {};
                    $scope.AddTaskStatus = function () {
                        var _isdisable = 0;
                        $scope.TaskStatusObj = {
                            StatusId: 0,
                            TaskStatus: ''
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
                                TaskStatusObj: function () {
                                    return $scope.TaskStatusObj;
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
                           //{ "title": "Priority Id", "data": "StatusId", filter: false, visible: false },
                           { "title": "Sr.", "field": "RowNumber", show: true, },
                           { "title": "TaskStatus", "field": "TaskStatus", sortable: "TaskStatus", filter: { TaskStatus: "text" }, show: true, },

                           {
                               "title": "Action", show: true,
                               'cellTemplte': function ($scope, row) {
                                   var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                              //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.StatusId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                               '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                                   return $scope.getHtml(element);
                               }
                           }
                        ],
                        Sort: { 'StatusId': 'asc' }

                    }

                    $scope.Edit = function (data) {
                        var _isdisable = 0;
                        $scope.TaskStatusObj = {
                            StatusId: data.StatusId,
                            TaskStatus: data.TaskStatus
                        }
                        $scope.storage = angular.copy($scope.TaskStatusObj);
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
                                TaskStatusObj: function () {
                                    return $scope.TaskStatusObj;
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
                        $scope.TaskStatusObj = {
                            StatusId: data.StatusId,
                            TaskStatus: data.TaskStatus
                        }
                        $scope.storage = angular.copy($scope.TaskStatusObj);
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
                                TaskStatusObj: function () {
                                    return $scope.TaskStatusObj;
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

                    $scope.Delete = function (statusID) {
                        TaskStatusService.DeleteTaskStatus(statusID).then(function (result) {
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
            '$uibModalInstance', 'TaskStatusObj', 'TaskStatusService', 'storage', 'isdisable',
            function ($scope, $uibModalInstance, TaskStatusObj, TaskStatusService, storage, isdisable) {
                $scope.isClicked = false;
                if (isdisable == 1) {
                    $scope.isClicked = true;
                }
                var $ctrl = this;

                $ctrl.ok = function () {
                    $uibModalInstance.close();
                };

                $ctrl.TaskStatusObj = TaskStatusObj;
                $ctrl.storage = storage;
                $ctrl.cancel = function () {
                    $uibModalInstance.dismiss('cancel');
                };

                $ctrl.SaveTaskStatus = function () {
                    TaskStatusService.AddTaskStatus($ctrl.TaskStatusObj).then(function (result) {
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

                $ctrl.UpdateTaskStatus = function () {
                    TaskStatusService.UpdateTaskStatus($ctrl.TaskStatusObj).then(function (result) {
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

                    if ($ctrl.TaskStatusObj.StatusId > 0) {

                        $ctrl.TaskStatusObj = angular.copy($ctrl.storage);

                    } else {
                        ResetForm();
                    }
                }


                function ResetForm() {

                    $ctrl.TaskStatusObj = {
                        StatusId: 0,
                        TaskStatus: '',

                    }
                    $ctrl.storage = {};
                    $ctrl.addMode = true;
                    $ctrl.saveText = "Save";
                    if ($ctrl.$parent.FormTaskStatus)
                        $ctrl.$parent.FormTaskStatus.$setPristine();
                }

            }]);
})();