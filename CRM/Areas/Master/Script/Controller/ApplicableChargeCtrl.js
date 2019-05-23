(function () {
    "use strict"
    angular.module("CRMApp.Controllers")
            .controller("ApplicableChargeCtrl", [
            "$scope", "$rootScope", "$timeout", "$filter", "ApplicableChargeService", "NgTableParams", "$uibModal",
            function ApplicableChargeCtrl($scope, $rootScope, $timeout, $filter, ApplicableChargeService, NgTableParams, $uibModal) {

                $scope.Add = function (id, _isdisable) {
                    if (_isdisable === undefined) _isdisable = 0;

                    var modalInstance = $uibModal.open({
                        backdrop: 'static',
                        templateUrl: 'myModalContent.html',
                        controller: ModalInstanceCtrl,
                        resolve: {
                            ApplicableChargeService: function () { return ApplicableChargeService; },
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
                       //{ "title": "ApplicableChargeId", "field": "ApplicableChargeId", filter: false, show: false },
                       { "title": "Sr.", "field": "RowNumber", filter: false, show: true },
                       { "title": "Applicable Charge", "field": "ApplicableChargeName", sortable: "ApplicableChargeName", filter: { ApplicableChargeName: "text" }, show: true },
                       {
                           "title": "Action", show: true,
                           'cellTemplte': function ($scope, row) {
                               var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.ApplicableChargeId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                                    '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.ApplicableChargeId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                               return $scope.getHtml(element);
                           }
                       }
                    ],
                    Sort: { "ApplicableChargeId": "asc" }
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

                    ApplicableChargeService.DeleteApplicableCharge(id).then(function (result) {
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

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, ApplicableChargeService, id, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.objApplicableCharge = $scope.objApplicableCharge || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.id = id;
        $scope.storage = {};

        if (id && id > 0) {
            ApplicableChargeService.GetByIdApplicableCharge(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objApplicableCharge = {
                        ApplicableChargeId: result.data.DataList.ApplicableChargeId,
                        ApplicableChargeName: result.data.DataList.ApplicableChargeName
                    }
                    $scope.storage = angular.copy($scope.objApplicableCharge);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        function ResetForm() {

            $scope.objApplicableCharge = {
                ApplicableChargeId: 0,
                ApplicableChargeName: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormAgencyTypeInfo)
                $scope.$parent.FormAgencyTypeInfo.$setPristine();
        }

        $scope.CreateUpdate = function (data) {

            ApplicableChargeService.CreateUpdateApplicableCharge(data).then(function (result) {
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

            if ($scope.objApplicableCharge.ApplicableChargeId > 0) {
                $scope.objApplicableCharge = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'ApplicableChargeService', 'id', 'isdisable']

})()






