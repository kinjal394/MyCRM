(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("LegerHeadController", [
         "$scope", "LegerHeadService", "$uibModal",
         LegerHeadController]);

    function LegerHeadController($scope, LegerHeadService, $uibModal) {
        $scope.id = 0;

        $scope.Add = function (data) {
            var _isdisable = 0;
            var objLegerHeaddata = {
                LegerHeadId: 0,
                LegerHeadName: '',
                ITRId: 0,
                CountryData: {
                    Display: "",
                    Value: ""
                }
            };
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    LegerHeadService: function () { return LegerHeadService; },
                    LegerHeadController: function () { return LegerHeadController; },
                    objLegerHeaddata: function () { return objLegerHeaddata; },
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
               //{ "title": "LegerHead Id", "data": "LegerHeadId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "LegerHead Name", "field": "LegerHeadName", sortable: "LegerHeadName", filter: { LegerHeadName: "text" }, show: true, },
               //{ "title": "ITRId", "data": "ITRId", filter: false, visible: false },
               { "title": "ITR Name", "field": "ITRName", sortable: "ITRName", filter: { ITRName: "text" }, show: true, },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                           //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.LegerHeadId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                          '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'LegerHeadId': 'asc' }
            
        }

        $scope.Edit = function (data) {
            var _isdisable = 0;
          
            var objLegerHeaddata = {
                LegerHeadId: data.LegerHeadId,
                LegerHeadName: data.LegerHeadName,
                ITRId: data.ITRId,
                ITRData: {
                    Display: data.ITRName,
                    Value: data.ITRId
                }
            };
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    LegerHeadService: function () { return LegerHeadService; },
                    LegerHeadController: function () { return $scope; },
                    objLegerHeaddata: function () { return objLegerHeaddata; },
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

            var objLegerHeaddata = {
                LegerHeadId: data.LegerHeadId,
                LegerHeadName: data.LegerHeadName,
                ITRId: data.ITRId,
                ITRData: {
                    Display: data.ITRName,
                    Value: data.ITRId
                }
            };
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    LegerHeadService: function () { return LegerHeadService; },
                    LegerHeadController: function () { return $scope; },
                    objLegerHeaddata: function () { return objLegerHeaddata; },
                    isdisable: function () { return _isdisable; }
                }
            });

            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
            });
        }
        $scope.Delete = function (data) {
            LegerHeadService.DeleteLegerHead(data).then(function (result) {
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

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, LegerHeadService, LegerHeadController, objLegerHeaddata, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }

        $scope.objLHead = $scope.objLHead || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.storage = {};

        if (objLegerHeaddata.LegerHeadId && objLegerHeaddata.LegerHeadId > 0) {
            $scope.objLHead = {
                LegerHeadId: objLegerHeaddata.LegerHeadId,
                LegerHeadName: objLegerHeaddata.LegerHeadName,
                ITRData: {
                    Display: objLegerHeaddata.ITRData.Display,
                    Value: objLegerHeaddata.ITRId
                }
            }
            $scope.storage = angular.copy($scope.objLHead);
        } else {
            ResetForm();
        }

        function ResetForm() {
            $scope.objLHead = {
                LegerHeadId: 0,
                LegerHeadName: '',
                ITRId: 0
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormLegerHeadInfo)
                $scope.$parent.FormLegerHeadInfo.$setPristine();

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
            var LegerHead = {
                LegerHeadName: data.LegerHeadName,
                ITRId: data.ITRData.Value
            }
            LegerHeadService.AddLegerHead(LegerHead).then(function (result) {
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
            var LegerHead = {
                LegerHeadId: data.LegerHeadId,
                LegerHeadName: data.LegerHeadName,
                ITRId: data.ITRData.Value
            }
            LegerHeadService.AddLegerHead(LegerHead).then(function (result) {
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
            if ($scope.objLHead.LegerHeadId > 0) {
                $scope.objLHead = angular.copy($scope.storage);
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
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'LegerHeadService', 'LegerHeadController', 'objLegerHeaddata', 'isdisable']
})()
