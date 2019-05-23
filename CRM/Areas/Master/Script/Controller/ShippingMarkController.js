(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("ShippingMarkController", [
               "$scope", "ShippingMarkService", "$filter", "$uibModal",
                function ShippingMarkController($scope, ShippingMarkService, $filter, $uibModal) {
                    $scope.storage = {};
                    $scope.AddShippingMark = function () {
                        var _isdisable = 0;
                        $scope.ShippingMarkObj = {
                            ShipmentMarkId: 0,
                            BuyerId: 0,
                            BuyerName: '',
                            ShipperId: 0,
                            ShipperName: '',
                            ConsigneeId: 0,
                            ConsigneeName: '',
                            POLName: '',
                            POL: 0,
                            PODName: '',
                            POD: 0,
                            BuyerData: { Display: '', Value: 0 },
                            ShipperData: { Display: '', Value: 0 },
                            ConsigneeData: { Display: '', Value: 0 },
                            POLData: { Display: '', Value: 0 },
                            PODData: { Display: '', Value: 0 }
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
                                ShippingMarkObj: function () {
                                    return $scope.ShippingMarkObj;
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
                //{ "title": "ShippingMark Id", "data": "ShipmentMarkId", filter: false, visible: false },
                           { "title": "Sr.", "field": "RowNumber", show: true, },
                           { "title": "Buyer Name", "field": "BuyerName", sortable: "BuyerName", filter: { BuyerName: "text" }, show: true, },
                           { "title": "Shipper Name", "field": "ShipperName", sortable: "ShipperName", filter: { ShipperName: "text" }, show: true, },
                           { "title": "Consignee Name", "field": "ConsigneeName", sortable: "ConsigneeName", filter: { ConsigneeName: "text" }, show: true, },
                           //{ "title": "POL", "field": "POL", sortable: "POL", filter: { POL: "text" }, show: false, },
                           //{ "title": "POD", "field": "POD", sortable: "POD", filter: { POD: "text" }, show: false, },
                           { "title": "POL Name", "field": "POLName", sortable: "POLName", filter: { POLName: "text" }, show: true, },
                           { "title": "POD Name", "field": "PODName", sortable: "PODName", filter: { PODName: "text" }, show: true, },
                           {
                               "title": "Action", show: true,
                               'cellTemplte': function ($scope, row) {
                                   var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                              //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.ShipmentMarkId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                              '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                                   return $scope.getHtml(element);
                               }
                           }
                        ],
                        Sort: { 'ShipmentMarkId': 'asc' }
                    }

                    $scope.Edit = function (data) {
                        var _isdisable = 0;
                        $scope.ShippingMarkObj = {
                            ShipmentMarkId: data.ShipmentMarkId,
                            BuyerId: (data.BuyerId == undefined) ? 0 : data.BuyerId,
                            BuyerName: data.BuyerName,
                            ShipperId: (data.ShipperId == undefined) ? 0 : data.ShipperId,
                            ShipperName: data.ShipperName,
                            ConsigneeId: (data.ConsigneeId == undefined) ? 0 : data.ConsigneeId,
                            ConsigneeName: data.ConsigneeName,
                            POLName: data.PODName,
                            POL: data.POL,
                            PODName: data.PODName,
                            POD: data.POD,
                            BuyerData: { Display: data.BuyerName, Value: (data.BuyerId == undefined) ? 0 : data.BuyerId },
                            SupllierData: { Display: data.ShipperName, Value: (data.ShipperId == undefined) ? 0 : data.ShipperId },
                            ConsigneeData: { Display: data.ConsigneeName, Value: (data.ConsigneeId == undefined) ? 0 : data.ConsigneeId },
                            POLData: { Display: data.POLName, Value: data.POL },
                            PODData: { Display: data.PODName, Value: data.POD }
                        }
                        $scope.storage = angular.copy($scope.ShippingMarkObj);
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
                                ShippingMarkObj: function () {
                                    return $scope.ShippingMarkObj;
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
                        $scope.ShippingMarkObj = {
                            ShipmentMarkId: data.ShipmentMarkId,
                            BuyerId: (data.BuyerId == undefined) ? 0 : data.BuyerId,
                            BuyerName: data.BuyerName,
                            ShipperId: (data.ShipperId == undefined) ? 0 : data.ShipperId,
                            ShipperName: data.ShipperName,
                            ConsigneeId: (data.ConsigneeId == undefined) ? 0 : data.ConsigneeId,
                            ConsigneeName: data.ConsigneeName,
                            POLName: data.PODName,
                            POL: data.POL,
                            PODName: data.PODName,
                            POD: data.POD,
                            BuyerData: { Display: data.BuyerName, Value: (data.BuyerId == undefined) ? 0 : data.BuyerId },
                            SupllierData: { Display: data.ShipperName, Value: (data.ShipperId == undefined) ? 0 : data.ShipperId },
                            ConsigneeData: { Display: data.ConsigneeName, Value: (data.ConsigneeId == undefined) ? 0 : data.ConsigneeId },
                            POLData: { Display: data.POLName, Value: data.POL },
                            PODData: { Display: data.PODName, Value: data.POD }
                        }
                        $scope.storage = angular.copy($scope.ShippingMarkObj);
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
                                ShippingMarkObj: function () {
                                    return $scope.ShippingMarkObj;
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
                    $scope.Delete = function (ShipmentMarkId) {
                        ShippingMarkService.DeleteShippingMark(ShipmentMarkId).then(function (result) {
                            if (result.data.ResponseType == 1) {
                                $scope.refreshGrid()
                                toastr.success(result.data.Message)
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
        '$uibModalInstance', 'ShippingMarkObj', 'ShippingMarkService', 'storage', 'isdisable',
        function ($scope, $uibModalInstance, ShippingMarkObj, ShippingMarkService, storage, isdisable) {
            $scope.isClicked = false;
            if (isdisable == 1) {
                $scope.isClicked = true;
            }

            var $ctrl = this;

            $ctrl.ok = function () {
                $uibModalInstance.close();
            };

            $ctrl.ShippingMarkObj = ShippingMarkObj;
            $ctrl.storage = storage;
            $ctrl.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };

            $ctrl.SaveShippingMark = function () {

                $ctrl.ShippingMarkObj.BuyerId = $ctrl.ShippingMarkObj.BuyerData.Value;
                $ctrl.ShippingMarkObj.BuyerName = $ctrl.ShippingMarkObj.BuyerData.Display;
                $ctrl.ShippingMarkObj.ShipperId = $ctrl.ShippingMarkObj.SupllierData.Value;
                $ctrl.ShippingMarkObj.ShipperName = $ctrl.ShippingMarkObj.SupllierData.Display;
                $ctrl.ShippingMarkObj.ConsigneeId = $ctrl.ShippingMarkObj.ConsigneeData.Value;
                $ctrl.ShippingMarkObj.ConsigneeName = $ctrl.ShippingMarkObj.ConsigneeData.Display;
                $ctrl.ShippingMarkObj.POLName = $ctrl.ShippingMarkObj.POLData.Display;
                $ctrl.ShippingMarkObj.POL = $ctrl.ShippingMarkObj.POLData.Value;
                $ctrl.ShippingMarkObj.PODName = $ctrl.ShippingMarkObj.PODData.Display;
                $ctrl.ShippingMarkObj.POD = $ctrl.ShippingMarkObj.PODData.Value;

                ShippingMarkService.AddShippingMark($ctrl.ShippingMarkObj).then(function (result) {
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

            $ctrl.UpdateShippingMark = function () {

                $ctrl.ShippingMarkObj.BuyerId = $ctrl.ShippingMarkObj.BuyerData.Value;
                $ctrl.ShippingMarkObj.BuyerName = $ctrl.ShippingMarkObj.BuyerData.Display;
                $ctrl.ShippingMarkObj.ShipperId = $ctrl.ShippingMarkObj.SupllierData.Value;
                $ctrl.ShippingMarkObj.ShipperName = $ctrl.ShippingMarkObj.SupllierData.Display;
                $ctrl.ShippingMarkObj.ConsigneeId = $ctrl.ShippingMarkObj.ConsigneeData.Value;
                $ctrl.ShippingMarkObj.ConsigneeName = $ctrl.ShippingMarkObj.ConsigneeData.Display;
                $ctrl.ShippingMarkObj.POLName = $ctrl.ShippingMarkObj.POLData.Display;
                $ctrl.ShippingMarkObj.POL = $ctrl.ShippingMarkObj.POLData.Value;
                $ctrl.ShippingMarkObj.PODName = $ctrl.ShippingMarkObj.PODData.Display;
                $ctrl.ShippingMarkObj.POD = $ctrl.ShippingMarkObj.PODData.Value;

                ShippingMarkService.UpdateShippingMark($ctrl.ShippingMarkObj).then(function (result) {
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
                if ($ctrl.ShippingMarkObj.ShipmentMarkId > 0) {
                    $ctrl.ShippingMarkObj = angular.copy($ctrl.storage);
                } else {
                    ResetForm();
                }
            }

            function ResetForm() {
                $ctrl.ShippingMarkObj = {
                    ShipmentMarkId: 0,
                    BuyerId: 0,
                    BuyerName: '',
                    ShipperId: 0,
                    ShipperName: '',
                    ConsigneeId: 0,
                    ConsigneeName: '',
                    POLName: '',
                    POL: 0,
                    PODName: '',
                    POD: 0,
                    BuyerData: { Display: '', Value: 0 },
                    ShipperData: { Display: '', Value: 0 },
                    ConsigneeData: { Display: '', Value: 0 },
                    POLData: { Display: '', Value: 0 },
                    PODData: { Display: '', Value: 0 }
                }
                $ctrl.storage = {};
                $ctrl.addMode = true;
                $ctrl.saveText = "Save";
                if ($ctrl.$parent.FormShippingMark)
                    $ctrl.$parent.FormShippingMark.$setPristine();
            }

        }]);
})();