(function () {
    "use strict"
    angular.module("CRMApp.Controllers")
            .controller("DesignationCtrl", [
            "$scope", "$rootScope", "$timeout", "$filter", "DesignationService", "$uibModal",
            DesignationCtrl
            ]);

    function DesignationCtrl($scope, $rootScope, $timeout, $filter, DesignationService, $uibModal) {

        $scope.Add = function (id, _isdisable) {
            if (_isdisable === undefined) _isdisable = 0;
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    DesignationService: function () { return DesignationService; },
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
               //{ "title": "Designation Id", "data": "DesignationId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "Designation Name", "field": "DesignationName", sortable: "DesignationName", filter: { DesignationName: "text" }, show: true, },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.DesignationId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                            //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.DesignationId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                             '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.DesignationId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { "DesignationId": "asc" }
        }

        $scope.Edit = function (id) {
            $scope.Add(id, 0);

        }
        $scope.View = function (id) {
            $scope.Add(id, 1);

        }

        $scope.Delete = function (id) {
            DesignationService.DeleteDesignation(id).then(function (result) {
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

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, DesignationService, id, isdisable) {

        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }


        $scope.objDesignation = $scope.objDesignation || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.id = id;
        $scope.storage = {};

        if (id && id > 0) {
            DesignationService.GetByIdDesignation(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objDesignation = {
                        DesignationId: result.data.DataList.DesignationId,
                        DesignationName: result.data.DataList.DesignationName
                    }
                    $scope.storage = angular.copy($scope.objDesignation);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        function ResetForm() {
            $scope.objDesignation = {
                DesignationId: 0,
                DesignationName: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormDesignationInfo)
                $scope.$parent.FormDesignationInfo.$setPristine();
        }

        $scope.CreateUpdate = function (data) {
            DesignationService.CreateUpdateDesignation(data).then(function (result) {
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
            if ($scope.objDesignation.DesignationId > 0) {
                $scope.objDesignation = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'DesignationService', 'id', 'isdisable']
})()





