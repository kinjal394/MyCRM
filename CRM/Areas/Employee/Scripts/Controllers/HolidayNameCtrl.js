(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
      .controller("HolidayNameCtrl", [
       "$scope", "$rootScope", "$timeout", "$filter", "HolidayNameService", "$uibModal",
       HolidayNameCtrl
      ]);

    function HolidayNameCtrl($scope, $rootScope, $timeout, $filter, HolidayNameService, $uibModal) {

        $scope.objHolidayName = $scope.objHolidayName || {};

        $scope.objHolidayName = {
            HolidayId: 0,
            HolidayName: ''
        }

        $scope.Add = function (data, _isdisable) {
            if (_isdisable === undefined) _isdisable = 0;
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'HolidayNameModal.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    data: function () { return data; },
                    HolidayNameService: function () { return HolidayNameService; },
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
               //{ "title": "HolidayId", "data": "HolidayId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "Holiday Name", "field": "HolidayName", sortable: "HolidayName", filter: { HolidayName: "text" }, show: true, },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                            //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.HolidayId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                            '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'HolidayId': 'asc' },
        }

        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };

        $scope.Edit = function (data) {
            $scope.Add(data, 0);
        }
        $scope.View = function (data) {
            $scope.Add(data, 1);
        }

        $scope.Delete = function (id) {
            HolidayNameService.Delete(id).then(function (result) {
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

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, data, HolidayNameService, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }

        $scope.objHolidayName = $scope.objHolidayName || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};

        if (data.HolidayId <= 0) {
            ResetForm()
        } else {
            $scope.objHolidayName = {
                HolidayId: data.HolidayId,
                HolidayName: data.HolidayName
            }
            $scope.storage = angular.copy($scope.objHolidayName);
        }

        function ResetForm() {
            $scope.objHolidayName = {
                HolidayId: 0,
                HolidayName: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.FormHolidayNameInfo)
                $scope.FormHolidayNameInfo.$setPristine();
        }

        $scope.CreateUpdate = function (data) {
            HolidayNameService.CreateUpdate(data).then(function (result) {
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
            if ($scope.objHolidayName.HolidayId > 0) {
                $scope.objHolidayName = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'data', 'HolidayNameService', 'isdisable']

})()