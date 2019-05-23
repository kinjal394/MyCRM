(function () {
    "use strict"
    angular.module("CRMApp.Controllers")
                .controller("ITRCtrl", [
                    "$scope", "$rootScope", "$timeout", "$filter", "ITRService", "NgTableParams", "$uibModal",
                    ITRCtrl
                ])

    function ITRCtrl($scope, $rootScope, $timeout, $filter, ITRService, NgTableParams, $uibModal) {
        $scope.Add = function (id, _isdisable) {
            if (_isdisable === undefined) _isdisable = 0;

            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    ITRService: function () { return ITRService; },
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
               //{ "title": "ITR Type Id", "data": "ITRId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "ITR Name", "field": "ITRName", sortable: "ITRName", filter: { ITRName: "text" }, show: true, },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.ITRId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                            //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.ITRId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                            '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.ITRId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { "ITRId": "asc" }
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

            ITRService.DeleteITR(id).then(function (result) {
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


    var ModalInstanceCtrl = function ($scope, $uibModalInstance, ITRService, id, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.objITR = $scope.objITR || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.id = id;
        $scope.storage = {};

        if (id && id > 0) {
            ITRService.GetByIdITR(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objITR = {
                        ITRId: result.data.DataList.ITRId,
                        ITRName: result.data.DataList.ITRName
                    }
                    $scope.storage = angular.copy($scope.objITR);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        function ResetForm() {

            $scope.objITR = {
                ITRId: 0,
                ITRName: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormITRInfo)
                $scope.$parent.FormITRInfo.$setPristine();
        }

        $scope.CreateUpdate = function (data) {

            ITRService.CreateUpdateITR(data).then(function (result) {
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

            if ($scope.objITR.ITRId > 0) {
                $scope.objITR = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'ITRService', 'id', 'isdisable']

})()






