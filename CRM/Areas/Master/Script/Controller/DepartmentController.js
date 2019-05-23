(function () {
    "use strict";
    debugger;
    angular.module("CRMApp.Controllers")
           .controller("DepartmentController", [
               "$scope", "DepartmentService", "$uibModal",
               DepartmentController]);

    function DepartmentController($scope, DepartmentService, $uibModal) {
        $scope.storage = {};
        $scope.id = 0;

        $scope.Add = function (data) {
           var _isdisable = 0;
            var objdepartdata = {
                DepartmentId: 0,
                DepartmentName: '',
            };
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    DepartmentService: function () { return DepartmentService; },
                    DepartmentController: function () { return DepartmentController; },
                    objdepartdata: function () { return objdepartdata; },
                    isdisable: function () { return _isdisable; }
                },
                storage: function () {
                    return $scope.storage;
                }
            });
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            });
        }

        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };

        $scope.gridObj = {
            columnsInfo: [
               //{ "title": "DepartmentId", "data": "DepartmentId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "Department Name", "field": "DepartmentName", sortable: "DepartmentName", filter: { DepartmentName: "text" }, show: true, },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                           //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.DepartmentId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                            '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       //'render': '<button  data-ng-click="$parent.$parent.$parent.Edit(row.SourceId)">Edit</button><button data-ng-click="$parent.$parent.$parent.Delete(row.SourceId)">Delete</button> '
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'DepartmentId': 'asc' }

        }
       
        $scope.Edit = function (data) {
            var _isdisable = 0;
            var objdepartdata = {
                DepartmentId: data.DepartmentId,
                DepartmentName: data.DepartmentName,
            };
            $scope.storage = angular.copy($scope.objdepartdata);
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    DepartmentService: function () { return DepartmentService; },
                    DepartmentController: function () { return $scope; },
                    objdepartdata: function () { return objdepartdata; },
                    isdisable: function () { return _isdisable; }
                },
                storage: function () {
                    return $scope.storage;
                }

            });
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
            });
        }
        $scope.View = function (data) {
            var _isdisable = 1;
            var objdepartdata = {
                DepartmentId: data.DepartmentId,
                DepartmentName: data.DepartmentName,
            };
            $scope.storage = angular.copy($scope.objdepartdata);
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    DepartmentService: function () { return DepartmentService; },
                    DepartmentController: function () { return $scope; },
                    objdepartdata: function () { return objdepartdata; },
                    isdisable: function () { return _isdisable; }
                },
                storage: function () {
                    return $scope.storage;
                }

            });
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
            });
        }
        $scope.Delete = function (data) {
            DepartmentService.DeleteDepartment(data).then(function (result) {
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

        $scope.RefreshTable = function () {
            $scope.tableParams.reload();
        };
    }


    var ModalInstanceCtrl = function ($scope, $uibModalInstance, DepartmentService, DepartmentController, objdepartdata, isdisable, storage) {

        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }

        $scope.objdepart = $scope.objdepart || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};

        if (objdepartdata.DepartmentId && objdepartdata.DepartmentId > 0) {
            $scope.objdepart = {
                DepartmentId: objdepartdata.DepartmentId,
                DepartmentName: objdepartdata.DepartmentName
            }
            $scope.storage = angular.copy($scope.objdepart);
        } else {
            ResetForm();
        }

        function ResetForm() {
            $scope.objdepart = {
                DepartmentId: 0,
                DepartmentName: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormDepartInfo)
                $scope.$parent.FormDepartInfo.$setPristine();
        }

        $scope.Create = function (data) {
            var Depart = {
                DepartmentName: data.DepartmentName
            }
            DepartmentService.AddDepartment(Depart).then(function (result) {
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

        $scope.Update = function (data) {
            var Depart = {
                DepartmentId: data.DepartmentId,
                DepartmentName: data.DepartmentName
            }
            DepartmentService.AddDepartment(Depart).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    $uibModalInstance.close();
                    ResetForm();
                } else {
                    toastr.error(result.data.Message);
                }
               
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.Reset = function () {
            if ($scope.objdepart.DepartmentId > 0) {
                $scope.objdepart = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'DepartmentService', 'DepartmentController', 'objdepartdata', 'isdisable']
})()

