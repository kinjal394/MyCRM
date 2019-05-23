(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("PortController", [
               "$scope", "PortService", "$uibModal",
               PortController]);

    function PortController($scope, PortService, $uibModal) {

        $scope.id = 0;
        $scope.Add = function (data) {
            var _isdisable = 0;
            var objportdata = {
                PortId: 0,
                PortName: '',
                CountryId: 0,
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
                    PortService: function () { return PortService; },
                    PortController: function () { return PortController; },
                    objportdata: function () { return objportdata; },
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
               //{ "title": "Port Id", "data": "PortId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "Port Name", "field": "PortName", sortable: "PortName", filter: { PortName: "text" }, show: true, },
                //{ "title": "CountryId", "data": "CountryId", filter: false, visible: false },
               { "title": "CountryName", "field": "CountryName", sortable: "CountryName", filter: { CountryName: "text" }, show: true, },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                           //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.PortId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                           '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'

                       //'render': '<button  data-ng-click="$parent.$parent.$parent.Edit(row.PortId)">Edit</button><button data-ng-click="$parent.$parent.$parent.Delete(row.PortId)">Delete</button> '
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'PortId': 'asc' }
        }

        $scope.Edit = function (data) {
            var _isdisable = 0;
            var objportdata = {
                PortId: data.PortId,
                PortName: data.PortName,
                CountryId: data.CountryId,
                CountryData: {
                    Display: data.CountryName,
                    Value: data.CountryId
                }
            };
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    PortService: function () { return PortService; },
                    PortController: function () { return $scope; },
                    objportdata: function () { return objportdata; },
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
            var objportdata = {
                PortId: data.PortId,
                PortName: data.PortName,
                CountryId: data.CountryId,
                CountryData: {
                    Display: data.CountryName,
                    Value: data.CountryId
                }
            };
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    PortService: function () { return PortService; },
                    PortController: function () { return $scope; },
                    objportdata: function () { return objportdata; },
                    isdisable: function () { return _isdisable; }
                }
            });
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
            });
        }
        $scope.Delete = function (data) {
            PortService.DeletePort(data).then(function (result) {
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

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, PortService, PortController, objportdata, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.objport = $scope.objport || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.storage = {};

        if (objportdata.PortId && objportdata.PortId > 0) {
            $scope.objport = {
                PortId: objportdata.PortId,
                PortName: objportdata.PortName,
                CountryData: {
                    Display: objportdata.CountryData.Display,
                    Value: objportdata.CountryId
                }
            }
            $scope.storage = angular.copy($scope.objport);
        } else {
            ResetForm();
        }

        function ResetForm() {
            $scope.objport = {
                PortId: 0,
                PortName: '',
                CountryId: 0
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormPortInfo)
                $scope.$parent.FormPortInfo.$setPristine();

        }
        $scope.CountryBind = function () {
            $('#mydiv').show();
            PortService.CountryBind().then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.CountryList = result.data.DataList;
                    $('#mydiv').hide();
                }
                else if (result.data.ResponseType == 3) {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.Create = function (data) {
            var Port = {
                //PortId: data.PortId,
                PortName: data.PortName,
                CountryId: data.CountryData.Value
            }
            PortService.AddPort(Port).then(function (result) {
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
            var Port = {
                PortId: data.PortId,
                PortName: data.PortName,
                CountryId: data.CountryData.Value
            }
            PortService.AddPort(Port).then(function (result) {
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

        $scope.Reset = function () {
            if ($scope.objport.PortId > 0) {
                $scope.objport = angular.copy($scope.storage);
            } else {
                ResetForm();
            }

        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'PortService', 'PortController', 'objportdata', 'isdisable']

})()