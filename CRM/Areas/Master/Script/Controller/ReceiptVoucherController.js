(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("ReceiptVoucherController", [
               "$scope", "ReceiptVoucherService", "$uibModal", "$filter",
               ReceiptVoucherController]);

    function ReceiptVoucherController($scope, ReceiptVoucherService, $uibModal, $filter) {
        $scope.id = 0;

        $scope.Add = function () {
            var _isdisable = 0;
            var objvoucherdata = {
                VoucherId: 0,
                Type: '',
                VoucherDate: '',
                Amount: '',
                Naration: '',
                BuyerId: 0,
                BuyerNameData: {
                    Display: "",
                    Value: ""
                }
            };
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    ReceiptVoucherService: function () { return ReceiptVoucherService; },
                    ReceiptVoucherController: function () { return ReceiptVoucherController; },
                    objvoucherdata: function () { return objvoucherdata; },
                    isdisable: function () { return _isdisable; }
                }
            });
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () { });
        }

        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };

        $scope.gridObj = {
            columnsInfo: [
               //{ "title": "Voucher Id", "data": "VoucherId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               //{ "title": "BuyerId Id", "data": "BuyerId", filter: false, visible: false },
               { "title": "Buyer Name", "field": "BuyerName", sortable: "BuyerName", filter: { BuyerName: "text" }, show: true, },
               { "title": "Voucher Type", "field": "Type", sortable: "Type", filter: { Type: "text" }, show: true, },
               {
                   "title": "Voucher Date", "field": "VoucherDate", sortable: "VoucherDate", filter: { VoucherDate: "text" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<p ng-bind="ConvertDate(row.VoucherDate,\'dd/mm/yyyy\')">'
                       return $scope.getHtml(element);
                   }
               },
               { "title": "Amount", "field": "Amount", sortable: "Amount", filter: { Amount: "text" }, show: false, },
               { "title": "Naration", "field": "Naration", sortable: "Naration", filter: { Naration: "text" }, show: false, },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                           //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.VoucherId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                   '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       //'render': '<button  data-ng-click="$parent.$parent.$parent.Edit(row.SourceId)">Edit</button><button data-ng-click="$parent.$parent.$parent.Delete(row.SourceId)">Delete</button> '
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'VoucherId': 'asc' }

        }

        $scope.Edit = function (data) {
            var _isdisable = 0;
            var objvoucherdata = {
                VoucherId: data.VoucherId,
                Type: data.Type,
                VoucherDate: data.VoucherDate,
                Amount: data.Amount,
                Naration: data.Naration,
                BuyerId: data.BuyerId,
                BuyerNameData: {
                    Display: data.BuyerName,
                    Value: data.BuyerId
                }
            };
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    ReceiptVoucherService: function () { return ReceiptVoucherService; },
                    ReceiptVoucherController: function () { return $scope; },
                    objvoucherdata: function () { return objvoucherdata; },
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
            var objvoucherdata = {
                VoucherId: data.VoucherId,
                Type: data.Type,
                VoucherDate: data.VoucherDate,
                Amount: data.Amount,
                Naration: data.Naration,
                BuyerId: data.BuyerId,
                BuyerNameData: {
                    Display: data.BuyerName,
                    Value: data.BuyerId
                }
            };
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    ReceiptVoucherService: function () { return ReceiptVoucherService; },
                    ReceiptVoucherController: function () { return $scope; },
                    objvoucherdata: function () { return objvoucherdata; },
                    isdisable: function () { return _isdisable; }
                }
            });
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
                // $log.info('Modal dismissed at: ' + new Date());
            });
        }
        $scope.Delete = function (data) {
            ReceiptVoucherService.DeleteReceiptVoucher(data).then(function (result) {
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

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, ReceiptVoucherService, ReceiptVoucherController, objvoucherdata, $filter, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }

        $scope.objvoucher = $scope.objvoucher || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.storage = {};

        if (objvoucherdata.VoucherId && objvoucherdata.VoucherId > 0) {
            $scope.objvoucher = {
                VoucherId: objvoucherdata.VoucherId,
                Type: objvoucherdata.Type,
                VoucherDate: $filter('mydate')(objvoucherdata.VoucherDate),
                Amount: objvoucherdata.Amount,
                Naration: objvoucherdata.Naration,
                BuyerId: objvoucherdata.BuyerId,
                BuyerNameData: {
                    Display: objvoucherdata.BuyerNameData.Display,
                    Value: objvoucherdata.BuyerId
                }
            }
            $scope.storage = angular.copy($scope.objvoucher);
            //$scope.objCategory = result.data.DataList.CategoryName;
        } else {
            ResetForm();
        }

        function ResetForm() {
            $scope.objvoucher = {
                VoucherId: 0,
                Type: '',
                VoucherDate: '',
                Amount: '',
                Naration: '',
                BuyerId: 0,
                BuyerNameData: {
                    Display: "",
                    Value: ""
                }
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormvoucherInfo)
                $scope.$parent.FormvoucherInfo.$setPristine();

        }

        $scope.dateOptions = {
            formatYear: 'yy',
            minDate: new Date(1950, 1, 1),
            startingDay: 1
        }

        $scope.Create = function (data) {
            var voucher = {
                //VoucherId: data.VoucherId,
                Type: data.Type,
                VoucherDate: data.VoucherDate,
                Amount: data.Amount,
                Naration: data.Naration,
                BuyerId: data.BuyerNameData.Value
            }
            ReceiptVoucherService.AddReceiptVoucher(voucher).then(function (result) {
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
            var voucher = {
                VoucherId: data.VoucherId,
                Type: data.Type,
                VoucherDate: data.VoucherDate,
                Amount: data.Amount,
                Naration: data.Naration,
                BuyerId: data.BuyerNameData.Value
            }
            ReceiptVoucherService.AddReceiptVoucher(voucher).then(function (result) {
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
            if ($scope.objvoucher.VoucherId > 0) {
                $scope.objvoucher = angular.copy($scope.storage);
            } else {
                ResetForm();
            }

        }

        $scope.close = function () {
            $uibModalInstance.close();
        };
    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'ReceiptVoucherService', 'ReceiptVoucherController', 'objvoucherdata', '$filter', 'isdisable']
})()

