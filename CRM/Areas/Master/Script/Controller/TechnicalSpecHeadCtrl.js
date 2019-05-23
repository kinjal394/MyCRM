(function () {
    "use strict"
    angular.module("CRMApp.Controllers")
            .controller("TechnicalSpecHeadCtrl", [
            "$scope", "$rootScope", "$timeout", "$filter", "TechnicalSpecHeadService", "NgTableParams", "$uibModal",
            function TechnicalSpecHeadCtrl($scope, $rootScope, $timeout, $filter, TechnicalSpecHeadService, NgTableParams, $uibModal) {

                $scope.Add = function (id, _isdisable) {
                    if (_isdisable === undefined) _isdisable = 0;
                    var modalInstance = $uibModal.open({
                        backdrop: 'static',
                        templateUrl: 'myModalContent.html',
                        controller: ModalInstanceCtrl,
                        resolve: {
                            TechnicalSpecHeadService: function () { return TechnicalSpecHeadService; },
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
                       { "title": "Sr.", "field": "RowNumber", filter: false, show: true },
                       { "title": "Technical Specification Head", "field": "TechHead", sortable: "TechHead", filter: { TechHead: "text" }, show: true },
                       {
                           "title": "Action", show: true,
                           'cellTemplte': function ($scope, row) {
                               var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.TechHeadId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                                    //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.$parent.$parent.$parent.Delete(row.TechHeadId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                                    '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.TechHeadId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                               return $scope.getHtml(element);
                           }
                       }
                    ],
                    Sort: { "TechHeadId": "asc" }
                }

                $scope.Edit = function (id) {
                    $scope.Add(id, 0);
                }

                $scope.View = function (id) {
                    $scope.Add(id, 1);
                }


                $scope.Delete = function (id) {
                    TechnicalSpecHeadService.DeleteTechnicalSpecHead(id).then(function (result) {
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

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, TechnicalSpecHeadService, id, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.objTechnicalSpecHead = $scope.objTechnicalSpecHead || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.id = id;
        $scope.storage = {};

        if (id && id > 0) {
            TechnicalSpecHeadService.GetByIdTechnicalSpecHead(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objTechnicalSpecHead = {
                        TechHeadId: result.data.DataList.TechHeadId,
                        TechHead: result.data.DataList.TechHead
                    }
                    $scope.storage = angular.copy($scope.objTechnicalSpecHead);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        function ResetForm() {

            $scope.objTechnicalSpecHead = {
                TechHeadId: 0,
                TechHead: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormTechnicalSpecHeadInfo)
                $scope.$parent.FormTechnicalSpecHeadInfo.$setPristine();
        }

        $scope.CreateUpdate = function (data) {

            TechnicalSpecHeadService.CreateUpdateTechnicalSpecHead(data).then(function (result) {
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

            if ($scope.objTechnicalSpecHead.TechHeadId > 0) {
                $scope.objTechnicalSpecHead = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'TechnicalSpecHeadService', 'id', 'isdisable']

})()






