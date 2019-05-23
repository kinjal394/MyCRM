(function () {
    "use strict"
    angular.module("CRMApp.Controllers")
            .controller("SalaryHeadCtrl", [
            "$scope", "$rootScope", "$timeout", "$filter", "SalaryHeadService", "NgTableParams", "$uibModal",
            function SalaryHeadCtrl($scope, $rootScope, $timeout, $filter, SalaryHeadService, NgTableParams, $uibModal) {

                $scope.Add = function (id, _isdisable) {
                    if (_isdisable === undefined) _isdisable = 0;

                    var modalInstance = $uibModal.open({
                        backdrop: 'static',
                        templateUrl: 'myModalContent.html',
                        controller: ModalInstanceCtrl,
                        resolve: {
                            SalaryHeadService: function () { return SalaryHeadService; },
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
                       //{ "title": "Salary Head Id", "data": "SalaryHeadId", filter: false, visible: false },
                       { "title": "Sr.", "field": "RowNumber", show: true, },
                       { "title": "SalaryHead Name", "field": "SalaryHeadName", sortable: "SalesDocument", filter: { SalesDocument: "text" }, show: true, },
                       {
                           "title": "Action", show: true,
                           'cellTemplte': function ($scope, row) {
                               var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.SalaryHeadId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                                    //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.SalaryHeadId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                                    '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.SalaryHeadId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                               return $scope.getHtml(element);
                           }
                       }
                    ],
                    Sort: { "SalaryHeadId": "asc" }
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

                    SalaryHeadService.DeleteSalaryHead(id).then(function (result) {
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

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, SalaryHeadService, id, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.objSalaryHead = $scope.objSalaryHead || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.id = id;
        $scope.storage = {};

        if (id && id > 0) {
            SalaryHeadService.GetByIdSalaryHead(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objSalaryHead = {
                        SalaryHeadId: result.data.DataList.SalaryHeadId,
                        SalaryHeadName: result.data.DataList.SalaryHeadName
                    }
                    $scope.storage = angular.copy($scope.objSalaryHead);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        function ResetForm() {

            $scope.objSalaryHead = {
                SalaryHeadId: 0,
                SalaryHeadName: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormSalaryHeadInfo)
                $scope.$parent.FormSalaryHeadInfo.$setPristine();
        }

        $scope.CreateUpdate = function (data) {

            SalaryHeadService.CreateUpdateSalaryHead(data).then(function (result) {
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

            if ($scope.objSalaryHead.AgencyTypeId > 0) {
                $scope.objSalaryHead = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'SalaryHeadService', 'id', 'isdisable']

})()