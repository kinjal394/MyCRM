(function () {
    "use strict"
    angular.module("CRMApp.Controllers")
            .controller("VoltageCtrl", [
            "$scope", "$rootScope", "$timeout", "$filter", "VoltageService", "NgTableParams", "$uibModal",
            function VoltageCtrl($scope, $rootScope, $timeout, $filter, VoltageService, NgTableParams, $uibModal) {

                $scope.Add = function (id, _isdisable) {
                    if (_isdisable === undefined) _isdisable = 0;

                    var modalInstance = $uibModal.open({
                        backdrop: 'static',
                        templateUrl: 'myModalContent.html',
                        controller: ModalInstanceCtrl,
                        resolve: {
                            VoltageService: function () { return VoltageService; },
                            id: function () { return id; },
                            isdisable: function () { return _isdisable; }
                        }
                    });
                    modalInstance.result.then(function () {
                        $scope.refreshGrid()
                    }, function () {
                        // $log.info('Modal dismissed at: ' + new Date());
                    });
                }

                $scope.setDirectiveRefresh = function (refreshGrid) {
                    $scope.refreshGrid = refreshGrid;
                };

                $scope.gridObj = {
                    columnsInfo: [
                       //{ "title": "VoltageId", "field": "VoltageId", filter: false, show: false },
                       { "title": "Sr.", "field": "RowNumber", filter: false, show: true },
                       { "title": "Voltage", "field": "Voltage", sortable: "Voltage", filter: { Voltage: "text" }, show: true },
                       {
                           "title": "Action", show: true,
                           'cellTemplte': function ($scope, row) {
                               var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.VoltageId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                                    //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.$parent.$parent.$parent.Delete(row.VoltageId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                                    '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.VoltageId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                               return $scope.getHtml(element);
                           }
                       }
                    ],
                    Sort: { "VoltageId": "asc" }
                }

                //$scope.Edit = function (id) {

                //    $scope.Add(id,0);
                //}
                //$scope.View = function (id) {

                //    $scope.Add(id,1);
                //}

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

                    VoltageService.DeleteVoltage(id).then(function (result) {
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

            }]);

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, VoltageService, id, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.objVoltage = $scope.objVoltage || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.id = id;
        $scope.storage = {};

        if (id && id > 0) {
            VoltageService.GetByIdVoltage(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objVoltage = {
                        VoltageId: result.data.DataList.VoltageId,
                        Voltage: result.data.DataList.Voltage
                    }
                    $scope.storage = angular.copy($scope.objVoltage);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        function ResetForm() {

            $scope.objVoltage = {
                VoltageId: 0,
                Voltage: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormVoltageInfo)
                $scope.$parent.FormVoltageInfo.$setPristine();
        }

        $scope.CreateUpdate = function (data) {

            VoltageService.CreateUpdateVoltage(data).then(function (result) {
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

            if ($scope.objVoltage.VoltageId > 0) {
                $scope.objVoltage = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'VoltageService', 'id', 'isdisable']

})()