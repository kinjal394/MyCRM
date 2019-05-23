(function () {
    "use strict"
    angular.module("CRMApp.Controllers")
            .controller("AddressTypeCtrl", [
                "$scope", "$rootScope", "$timeout", "$filter", "AddressTypeService", "NgTableParams", "$uibModal",
                AddressTypeCtrl
            ])

    function AddressTypeCtrl($scope, $rootScope, $timeout, $filter, AddressTypeService, NgTableParams, $uibModal) {
        $scope.Add = function (id, _isdisable) {
            if (_isdisable === undefined) _isdisable = 0;

            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    AddressTypeService: function () { return AddressTypeService; },
                    id: function () { return id; },
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
               //{ "title": "Address Type Id", "data": "AddressTypeId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", filter: false, show: true },
               { "title": "AddressType Name", "field": "AddressTypeName", sortable: "AddressTypeName", filter: { AddressTypeName: "text" }, show: true },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.AddressTypeId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                             //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.$parent.$parent.$parent.Delete(row.AddressTypeId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                             '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.AddressTypeId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>';
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { "AddressTypeId": "asc" }
        }

        $scope.Edit = function (id) {
            $scope.Add(id, 0);
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
            });
        }

        $scope.View = function (id) {
            $scope.Add(id, 1);
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
            });
        }

        $scope.Delete = function (id) {

            AddressTypeService.DeleteAddressType(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
                $scope.refreshGrid()
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.RefreshTable = function () {
            $scope.tableParams.reload();
        };

    }

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, AddressTypeService, id, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }

        $scope.objAddressType = $scope.objAddressType || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.id = id;
        $scope.storage = {};

        if (id && id > 0) {
            AddressTypeService.GetByIdAddressType(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objAddressType = {
                        AddressTypeId: result.data.DataList.AddressTypeId,
                        AddressTypeName: result.data.DataList.AddressTypeName
                    }
                    $scope.storage = angular.copy($scope.objAddressType);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        function ResetForm() {

            $scope.objAddressType = {
                AddressTypeId: 0,
                AddressTypeName: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormAddressTypeInfo)
                $scope.$parent.FormAddressTypeIdInfo.$setPristine();
        }

        $scope.CreateUpdate = function (data) {

            AddressTypeService.CreateUpdateAddressType(data).then(function (result) {
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

        $scope.Reset = function () {

            if ($scope.objAddressType.AddressTypeId > 0) {
                $scope.objAddressType = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };
    }
})();