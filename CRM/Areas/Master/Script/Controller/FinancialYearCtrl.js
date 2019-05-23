(function () {
    "use strict"
    angular.module("CRMApp.Controllers")
            .controller("FinancialYearCtrl", [
            "$scope", "$rootScope", "$timeout", "$filter", "FinancialYearService", "NgTableParams", "$uibModal",
            function FinancialYearCtrl($scope, $rootScope, $timeout, $filter, FinancialYearService, NgTableParams, $uibModal) {

                $scope.Add = function (id, _isdisable) {
                    if (_isdisable === undefined) _isdisable = 0;

                    var modalInstance = $uibModal.open({
                        backdrop: 'static',
                        templateUrl: 'myModalContent.html',
                        controller: ModalInstanceCtrl,
                        resolve: {
                            FinancialYearService: function () { return FinancialYearService; },
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
                       //{ "title": "Financial Year Id", "data": "FinancialYearId", filter: false, visible: false },
                       { "title": "Sr.", "field": "RowNumber", show: true, },
                       { "title": "Financial Year", "field": "FinancialYear", sortable: "FinancialYear", filter: { FinancialYear: "text" }, show: true, },
                       {
                           "title": "Action", show: true,
                           'cellTemplte': function ($scope, row) {
                               var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.FinancialYearId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                                    //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.FinancialYearId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                                    '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.FinancialYearId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                               return $scope.getHtml(element);
                           }
                       }
                    ],
                    Sort: { "FinancialYearId": "asc" }
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

                    FinancialYearService.DeleteFinancialYear(id).then(function (result) {
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

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, FinancialYearService, id, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.objFinancialYear = $scope.objFinancialYear || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.id = id;
        $scope.storage = {};

        if (id && id > 0) {
            FinancialYearService.GetByIdFinancialYear(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objFinancialYear = {
                        FinancialYearId: result.data.DataList.FinancialYearId,
                        FinancialYear: result.data.DataList.FinancialYear
                    }
                    $scope.storage = angular.copy($scope.objFinancialYear);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        function ResetForm() {

            $scope.objFinancialYear = {
                FinancialYearId: 0,
                FinancialYear: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormFinancialYearInfo)
                $scope.$parent.FormFinancialYearInfo.$setPristine();
        }

        $scope.CreateUpdate = function (data) {

            FinancialYearService.CreateUpdateFinancialYear(data).then(function (result) {
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

            if ($scope.objFinancialYear.FinancialYearId > 0) {
                $scope.objFinancialYear = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'FinancialYearService', 'id', 'isdisable']

})()