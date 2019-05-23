(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("TypeOfShipmentCtrl", [
               "$scope", "TypeOfShipmentService", "$uibModal",
               TypeOfShipmentCtrl]);

    function TypeOfShipmentCtrl($scope, TypeOfShipmentService, $uibModal) {

        $scope.objTypeOfShipment = {
            ShipmentTypeId: 0,
            ShipmentType: ''
        };

        $scope.Add = function (data, _isdisable) {
            if (_isdisable === undefined) _isdisable = 0;
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    TypeOfShipmentService: function () { return TypeOfShipmentService; },
                    objTypeOfShipment: function () { return data; },
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
               //{ "title": "Shipment Type Id", "data": "ShipmentTypeId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "Shipment Type", "field": "ShipmentType", sortable: "ShipmentType", filter: { ShipmentType: "text" }, show: true, },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                            //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.ShipmentTypeId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                            '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }

               }
            ],
            Sort: { 'ShipmentTypeId': 'asc' }
        }

        $scope.Edit = function (data) {
            var objTempShipment = {
                ShipmentTypeId: data.ShipmentTypeId,
                ShipmentType: data.ShipmentType
            };
            $scope.Add(objTempShipment, 0);
        }
        $scope.View = function (data) {
            var objTempShipment = {
                ShipmentTypeId: data.ShipmentTypeId,
                ShipmentType: data.ShipmentType
            };
            $scope.Add(objTempShipment, 1);
        }
        $scope.Delete = function (id) {
            TypeOfShipmentService.DeleteTypeOfShipment(id).then(function (result) {
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

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, TypeOfShipmentService, objTypeOfShipment, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }

        $scope.objTypeOfShipment = $scope.objTypeOfShipment || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};

        if (objTypeOfShipment.ShipmentTypeId > 0) {
            $scope.objTypeOfShipment = objTypeOfShipment;
            $scope.storage = angular.copy($scope.objTypeOfShipment);
        } else {
            ResetForm();
        }


        function ResetForm() {
            $scope.objTypeOfShipment = {
                ShipmentTypeId: 0,
                ShipmentType: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormShipInfo)
                $scope.$parent.FormShipInfo.$setPristine();

        }

        $scope.Create = function (data) {
            var objTypeOfShipment = {
                ShipmentTypeId: 0,
                ShipmentType: '',
            };
            TypeOfShipmentService.SaveTypeOfShipment(data).then(function (result) {
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
            TypeOfShipmentService.UpdateTypeOfShipment(data).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    $uibModalInstance.close();
                } else {
                    toastr.error(result.data.Message);
                }

            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.Reset = function () {
            if ($scope.objTypeOfShipment.ShipmentTypeId > 0) {
                $scope.objTypeOfShipment = angular.copy($scope.storage);
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
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'TypeOfShipmentService', 'objTypeOfShipment', 'isdisable']
})()


