(function () {

    "use strict";
    angular.module("CRMApp.Controllers")
      .controller("RelationCtrl", [
       "$scope", "$rootScope", "$timeout", "$filter", "RelationService", "$uibModal",
       RelationCtrl
      ]);

    function RelationCtrl($scope, $rootScope, $timeout, $filter, RelationService, $uibModal) {

        $scope.objRelation = $scope.objRelation || {};

        $scope.objRelation = {
            RelationId: 0,
            RelationName: ''
        }

        $scope.Add = function (data, _isdisable) {
            if (_isdisable === undefined) _isdisable = 0;
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'RelationModal.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    data: function () { return data; },
                    RelationService: function () { return RelationService; },
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
               //{ "title": "RelationId", "data": "RelationId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "Relation Name", "field": "RelationName", sortable: "RelationName", filter: { RelationName: "text" }, show: true, },
               {
                   "title": "Action", sort: false, filter: false, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                            //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.RelationId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                            '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'RelationId': 'asc' },
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
            RelationService.Delete(id).then(function (result) {
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

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, data, RelationService, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }

        $scope.objRelation = $scope.objRelation || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};

        if (data.RelationId <= 0) {
            ResetForm()
        } else {
            $scope.objRelation = {
                RelationId: data.RelationId,
                RelationName: data.RelationName
            }
            $scope.storage = angular.copy($scope.objRelation);
        }

        function ResetForm() {
            $scope.objRelation = {
                RelationId: 0,
                RelationName: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.FormRelationInfo)
                $scope.FormRelationInfo.$setPristine();
        }

        $scope.CreateUpdate = function (data) {
            RelationService.CreateUpdate(data).then(function (result) {
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
            if ($scope.objRelation.RelationId > 0) {
                $scope.objRelation = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };
    }

    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'data', 'RelationService', 'isdisable']

})()