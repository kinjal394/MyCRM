(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("InvoiceTypeCtrl", [
               "$scope", "InvoiceTypeService", "$uibModal",
               InvoiceTypeCtrl]);

    function InvoiceTypeCtrl($scope, InvoiceTypeService, $uibModal) {

        $scope.objInvoiceType = {
            InvoiceTypeId: 0,
            InvoiceTypeName: ''
        };

        $scope.Add = function (data, _isdisable) {
            if (_isdisable === undefined) _isdisable = 0;
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    InvoiceTypeService: function () { return InvoiceTypeService; },
                    objInvoiceType: function () { return data; },
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
               //{ "title": "Invoice Type Id", "data": "InvoiceTypeId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "Invoice Type Name", "field": "InvoiceTypeName", sortable: "InvoiceTypeName", filter: { InvoiceTypeName: "text" }, show: true, },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                            //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.InvoiceTypeId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                            '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'InvoiceTypeId': 'asc' }
        }

        $scope.Edit = function (data) {
            var objTempShipment = {
                InvoiceTypeId: data.InvoiceTypeId,
                InvoiceTypeName: data.InvoiceTypeName
            };
            $scope.Add(objTempShipment, 0);
        }
        $scope.View = function (data) {
            var objTempShipment = {
                InvoiceTypeId: data.InvoiceTypeId,
                InvoiceTypeName: data.InvoiceTypeName
            };
            $scope.Add(objTempShipment, 1);
        }
        $scope.Delete = function (id) {
            InvoiceTypeService.DeleteInvoiceType(id).then(function (result) {
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

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, InvoiceTypeService, objInvoiceType, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }

        $scope.objInvoiceType = $scope.objInvoiceType || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};

        if (objInvoiceType.InvoiceTypeId > 0) {
            $scope.objInvoiceType = objInvoiceType;
            $scope.storage = angular.copy($scope.objInvoiceType);
        } else {
            ResetForm();
        }


        function ResetForm() {
            $scope.objInvoiceType = {
                InvoiceTypeId: 0,
                InvoiceTypeName: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.InvoiceTypeInfo)
                $scope.$parent.InvoiceTypeInfo.$setPristine();

        }


        $scope.Create = function (data) {
            var objinvoicetype = {
                invoicetypeid: 0,
                invoicetypename: '',
            };
            InvoiceTypeService.SaveInvoiceType(data).then(function (result) {
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
            InvoiceTypeService.UpdateInvoiceType(data).then(function (result) {
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
            if ($scope.objInvoiceType.InvoiceTypeId > 0) {
                $scope.objInvoiceType = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'InvoiceTypeService', 'objInvoiceType', 'isdisable']
})()


