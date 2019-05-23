(function () {
    "User strict";
    angular.module("CRMApp.Controllers")
         .controller("TOTypeController", [
             "$scope", "TOTypeService", "$uibModal",
             TOTypeController]);

    function TOTypeController($scope, TOTypeService, $uibModal) {
        $scope.storage = {};
        $scope.id = 0;

        $scope.Add = function (data) {
            _isdisable = 0
            var objTOdata = {
                TOTypeId: 0,
                TOType: '',
            };
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    TOTypeService: function () { return TOTypeService; },
                    TOTypeController: function () { return TOTypeController; },
                    objTOdata: function () { return objTOdata; },
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
               //{ "title": "TOTypeId", "data": "TOTypeId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "TOType Name", "field": "TOType", sortable: "ShipmentType", filter: { ShipmentType: "text" }, show: true, },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                           //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.TOTypeId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                  '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       //'render': '<button  data-ng-click="$parent.$parent.$parent.Edit(row.SourceId)">Edit</button><button data-ng-click="$parent.$parent.$parent.Delete(row.SourceId)">Delete</button> '
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'TOTypeId': 'asc' }

        }

        $scope.Edit = function (data) {
            _isdisable = 0;
            var objTOdata = {
                TOTypeId: data.TOTypeId,
                TOType: data.TOType,
            };
            $scope.storage = angular.copy($scope.objTOdata);
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    TOTypeService: function () { return TOTypeService; },
                    TOTypeController: function () { return $scope; },
                    objTOdata: function () { return objTOdata; },
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
            _isdisable = 1;
            var objTOdata = {
                TOTypeId: data.TOTypeId,
                TOType: data.TOType,
            };
            $scope.storage = angular.copy($scope.objTOdata);
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    TOTypeService: function () { return TOTypeService; },
                    TOTypeController: function () { return $scope; },
                    objTOdata: function () { return objTOdata; },
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
            TOTypeService.DeleteTOType(data).then(function (result) {
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

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, TOTypeService, TOTypeController, objTOdata, isdisable, storage) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.objTO = $scope.objTO || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};

        if (objTOdata.TOTypeId && objTOdata.TOTypeId > 0) {
            $scope.objTO = {
                TOTypeId: objTOdata.TOTypeId,
                TOType: objTOdata.TOType
            }
            $scope.storage = angular.copy($scope.objTO);
        } else {
            ResetForm();
        }


        $scope.focusMe = function (value) {
            if (value === true) {
                element[0].focus();
            }
        };


        function ResetForm() {
            $scope.objTO = {
                TOTypeId: 0,
                TOType: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormTOInfo)
                $scope.$parent.FormTOInfo.$setPristine();
        }

        $scope.Create = function (data) {
            var TO = {
                TOType: data.TOType
            }
            TOTypeService.AddTOType(TO).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    ResetForm();
                    $uibModalInstance.close();
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.Update = function (data) {
            var TO = {
                TOTypeId: data.TOTypeId,
                TOType: data.TOType
            }
            TOTypeService.AddTOType(TO).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    $uibModalInstance.close();
                    ResetForm();
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }


        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'TOTypeService', 'TOTypeController', 'objTOdata', 'isdisable']


})()