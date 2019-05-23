(function () {

    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("AccountTypeCtrl", [
            "$scope", "$rootScope", "$timeout", "$filter", "AccountTypeService", "$uibModal",
            AccountTypeCtrl
           ]);

    function AccountTypeCtrl($scope, $rootScope, $timeout, $filter, AccountTypeService, $uibModal) {

        $scope.objAccountType = $scope.objAccountType || {};

        $scope.objAccountType = {
            AccountTypeId: 0,
            AccountType: ''
        }

        $scope.Add = function (data, _isdisable) {
            if (_isdisable === undefined) _isdisable = 0;
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'AccountTypeModal.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    data: function () { return data; },
                    AccountTypeService: function () { return AccountTypeService; },
                    isdisable: function () { return _isdisable; }
                }
            });
            modalInstance.result.then(function () {
                $scope.refreshGrid();
            }, function () {
            });
        }

        $scope.gridObj = {
            columnsInfo: [
               //{ "title": "AccountTypeId", "data": "AccountTypeId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", filter: false, show: true },
               { "title": "Account Type", "field": "AccountType", sortable: "AccountType", filter: { AccountType: "text" }, show: true },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                            //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.AccountTypeId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                            '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>';
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'AccountTypeId': 'asc' },
        }

        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };

        $scope.Edit = function (data) {
            $scope.Add(data, 0);
        }
        $scope.View = function (id) {
            $scope.Add(id, 1);
        }

        $scope.Delete = function (id) {
            AccountTypeService.Delete(id).then(function (result) {
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

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, data, AccountTypeService, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }

        $scope.objAccountType = $scope.objAccountType || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};
        if (data.AccountTypeId <= 0) {
            ResetForm()
        } else {
            $scope.objAccountType = {
                AccountTypeId: data.AccountTypeId,
                AccountType: data.AccountType
            }
            $scope.storage = angular.copy($scope.objAccountType);
        }

        function ResetForm() {
            $scope.objAccountType = {
                AccountTypeId: 0,
                AccountType: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.FormAccountTypeInfo)
                $scope.FormAccountTypeInfo.$setPristine();
        }

        $scope.CreateUpdate = function (data) {
            AccountTypeService.CreateUpdate(data).then(function (result) {
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
            if ($scope.objAccountType.AccountTypeId > 0) {
                $scope.objAccountType = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }

    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'data', 'AccountTypeService', 'isdisable']

})()