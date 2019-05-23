(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("LegerController", [
         "$scope", "LegerService", "$uibModal",
         LegerController]);

    function LegerController($scope, LegerService, $uibModal) {
        $scope.id = 0;

        $scope.Add = function (data) {
            var _isdisable = 0;
            var objLegerdata = {
                LegerId: 0,
                LegerName: '',
                ITRId: 0,
                LegerData: {
                    Display: "",
                    Value: ""
                }
            };
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    LegerService: function () { return LegerService; },
                    LegerController: function () { return LegerController; },
                    objLegerdata: function () { return objLegerdata; },
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
               //{ "title": "Leger Id", "data": "LegerId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "Leger Name", "field": "LegerName", sortable: "LegerHeadName", filter: { LegerHeadName: "text" }, show: true, },
               //{ "title": "LegerHeadId", "data": "LegerHeadId", filter: false, visible: false },
               { "title": "LegerHead Name", "field": "LegerHeadName", sortable: "LegerHeadName", filter: { LegerHeadName: "text" }, show: true, },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                           //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.LegerId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                          '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }

               }
            ],
            Sort: { 'LegerHeadId': 'asc' }

        }

        $scope.Edit = function (data) {
            var _isdisable = 0;

            var objLegerdata = {
                LegerId: data.LegerId,
                LegerName: data.LegerName,
                LegerHeadId: data.LegerHeadId,
                LegerHeadData: {
                    Display: data.LegerHeadName,
                    Value: data.LegerHeadId
                }
            };
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    LegerService: function () { return LegerService; },
                    LegerController: function () { return $scope; },
                    objLegerdata: function () { return objLegerdata; },
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

            var objLegerdata = {
                LegerId: data.LegerId,
                LegerName: data.LegerName,
                LegerHeadId: data.LegerHeadId,
                LegerHeadData: {
                    Display: data.LegerHeadName,
                    Value: data.LegerHeadId
                }
            };
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    LegerService: function () { return LegerService; },
                    LegerController: function () { return $scope; },
                    objLegerdata: function () { return objLegerdata; },
                    isdisable: function () { return _isdisable; }
                }
            });

            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
            });
        }

        $scope.Delete = function (data) {
            LegerService.DeleteLeger(data).then(function (result) {
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

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, LegerService, LegerController, objLegerdata, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }

        $scope.objLeger = $scope.objLeger || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.storage = {};

        if (objLegerdata.LegerId && objLegerdata.LegerId > 0) {
            $scope.objLeger = {
                LegerId: objLegerdata.LegerId,
                LegerName: objLegerdata.LegerName,
                LegerHead: {
                    Display: objLegerdata.LegerHeadData.Display,
                    Value: objLegerdata.LegerHeadId
                }
            }
            $scope.storage = angular.copy($scope.objLeger);
        } else {
            ResetForm();
        }

        function ResetForm() {
            $scope.objLeger = {
                LegerId: 0,
                LegerHeadName: '',
                LegerHeadId: 0
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormLegerInfo)
                $scope.$parent.FormLegerInfo.$setPristine();

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
            var Leger = {
                LegerName: data.LegerName,
                LegerHeadId: data.LegerHead.Value
            }
            LegerService.AddLeger(Leger).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $uibModalInstance.close();
                    toastr.success(result.data.Message);
                    ResetForm();
                } else {
                    toastr.error(result.data.Message);
                }

            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.Update = function (data) {
            var Leger = {
                LegerId: data.LegerId,
                LegerName: data.LegerName,
                LegerHeadId: data.LegerHead.Value
            }
            LegerService.AddLeger(Leger).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $uibModalInstance.close();
                    toastr.success(result.data.Message);
                } else {
                    toastr.error(result.data.Message);
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
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'LegerService', 'LegerController', 'objLegerdata', 'isdisable']
})()
