(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("TaskGroupController", [
         "$scope", "TaskGroupService", "$uibModal",
         TaskGroupController]);

    function TaskGroupController($scope, TaskGroupService, $uibModal) {
        $scope.id = 0;

        $scope.Add = function (data) {
            var _isdisable = 0;
            var objTaskGroupdata = {
                TaskGroupId: 0,
                TaskGroupName: '',

            };
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    TaskGroupService: function () { return TaskGroupService; },
                    TaskGroupController: function () { return TaskGroupController; },
                    objTaskGroupdata: function () { return objTaskGroupdata; },
                    isdisable: function () { return _isdisable; }
                }
            });
            modalInstance.result.then(function () {
                $scope.refreshGrid();
            }, function () {
            });
        }

        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };

        $scope.gridObj = {
            columnsInfo: [
               //{ "title": "TaskGroup Id", "data": "TaskGroupId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "TaskGroup Name", "field": "TaskGroupName", sortable: "TaskGroupName", filter: { TaskGroupName: "text" }, show: true, },
                    {
                        "title": "Action", show: true,
                        'cellTemplte': function ($scope, row) {
                            var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                                //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.TaskGroupId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                               '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                            return $scope.getHtml(element);
                        }
                    }
            ],
            Sort: { 'TaskGroupId': 'asc' }

        }

        $scope.Edit = function (data) {
            var _isdisable = 0;
            var objTaskGroupdata = {
                TaskGroupId: data.TaskGroupId,
                TaskGroupName: data.TaskGroupName,

            };

            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    TaskGroupService: function () { return TaskGroupService; },
                    TaskGroupController: function () { return $scope; },
                    objTaskGroupdata: function () { return objTaskGroupdata; },
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
            var objTaskGroupdata = {
                TaskGroupId: data.TaskGroupId,
                TaskGroupName: data.TaskGroupName,

            };

            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    TaskGroupService: function () { return TaskGroupService; },
                    TaskGroupController: function () { return $scope; },
                    objTaskGroupdata: function () { return objTaskGroupdata; },
                    isdisable: function () { return _isdisable; }
                }
            });

            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
            });
        }
        $scope.Delete = function (data) {
            TaskGroupService.DeleteTaskGroup(data).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    $scope.refreshGrid()
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

    }

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, TaskGroupService, TaskGroupController, objTaskGroupdata, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }

        $scope.objTaskGroup = $scope.objTaskGroup || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.storage = {};

        if (objTaskGroupdata.TaskGroupId && objTaskGroupdata.TaskGroupId > 0) {
            $scope.objTaskGroup = {
                TaskGroupId: objTaskGroupdata.TaskGroupId,
                TaskGroupName: objTaskGroupdata.TaskGroupName
            }
            $scope.storage = angular.copy($scope.objTaskGroup);
        } else {
            ResetForm();
        }

        function ResetForm() {
            $scope.objTaskGroup = {
                TaskGroupId: 0,
                TaskGroupName: ''

            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormTaskGroupInfo)
                $scope.$parent.FormTaskGroupInfo.$setPristine();

        }
        //$scope.ITRBind = function () {
        //    $('#mydiv').show();
        //    LegerHeadService.ITRBind().then(function (result) {
        //        if (result.data.ResponseType == 1) {
        //            $scope.ITRList = result.data.DataList;
        //            $('#mydiv').hide();
        //        }
        //        else if (result.data.ResponseType == 3) {
        //            toastr.error(result.data.Message, 'Opps, Something went wrong');
        //        }
        //    }, function (errorMsg) {
        //        toastr.error(errorMsg, 'Opps, Something went wrong');
        //    })
        //}

        $scope.Create = function (data) {
            var TaskGroup = {
                TaskGroupName: data.TaskGroupName
            }
            TaskGroupService.AddTaskGroup(TaskGroup).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $uibModalInstance.close();
                    toastr.success(result.data.Message);
                    ResetForm();
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.Update = function (data) {
            var TaskGroup = {
                TaskGroupId: data.TaskGroupId,
                TaskGroupName: data.TaskGroupName
            }
            TaskGroupService.AddTaskGroup(TaskGroup).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $uibModalInstance.close();
                    toastr.success(result.data.Message);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.Reset = function () {
            if ($scope.objLeger.LegerId > 0) {
                $scope.objLeger = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
            //if ($scope.id > 0) {
            //    $scope.objCategory = angular.copy($scope.storage);
            //} else {
            //    ResetForm();
            //}
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'TaskGroupService', 'TaskGroupController', 'objTaskGroupdata', 'isdisable']
})()
