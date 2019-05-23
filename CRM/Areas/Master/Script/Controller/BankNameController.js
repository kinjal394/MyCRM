(function () {
    "use strict";
    debugger;
    angular.module("CRMApp.Controllers")
           .controller("BankNameController", [
               "$scope", "BankNameService", "$uibModal",
               BankNameController]);

    function BankNameController($scope, BankNameService, $uibModal) {
        $scope.storage = {};
        $scope.id = 0;

        $scope.Add = function (data) {
            var _isdisable = 0;
            var objBankdata = {
                BankId: 0,
                BankName: '',
            };
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    BankNameService: function () { return BankNameService; },
                    BankNameController: function () { return BankNameController; },
                    objBankdata: function () { return objBankdata; },
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
               //{ "title": "BankId", "data": "BankId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", filter: false, show: true },
               { "title": "Bank Name", "field": "BankName", sortable: "BankName", filter: { BankName: "text" }, show: true },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                  //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.$parent.$parent.$parent.Delete(row.BankId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                  '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>';
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'BankId': 'asc' }

        }

        $scope.Edit = function (data, _isdisable) {
            if (_isdisable === undefined) _isdisable = 0;
            var objBankdata = {
                BankId: data.BankId,
                BankName: data.BankName,
            };
            $scope.storage = angular.copy($scope.objBankdata);
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    BankNameService: function () { return BankNameService; },
                    BankNameController: function () { return $scope; },
                    objBankdata: function () { return objBankdata; },
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
            $scope.Edit(data, 1)

        }
        $scope.Delete = function (data) {
            BankNameService.DeleteBankName(data).then(function (result) {
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


    var ModalInstanceCtrl = function ($scope, $uibModalInstance, BankNameService, BankNameController, objBankdata, isdisable, storage) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }

        $scope.objBank = $scope.objBank || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};

        if (objBankdata.BankId && objBankdata.BankId > 0) {
            $scope.objBank = {
                BankId: objBankdata.BankId,
                BankName: objBankdata.BankName
            }
            $scope.storage = angular.copy($scope.objBank);
        } else {
            ResetForm();
        }

        function ResetForm() {
            $scope.objBank = {
                BankId: 0,
                BankName: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormBankInfo)
                $scope.$parent.FormBankInfo.$setPristine();
        }

        $scope.Create = function (data) {
            var Bank = {
                BankName: data.BankName
            }
            BankNameService.AddBankName(Bank).then(function (result) {
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
            var Bank = {
                BankId: data.BankId,
                BankName: data.BankName
            }
            BankNameService.AddBankName(Bank).then(function (result) {
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
            if ($scope.objBank.BankId > 0) {
                $scope.objBank = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'BankNameService', 'BankNameController', 'objBankdata', 'isdisable']
})()