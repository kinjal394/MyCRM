(function () {
    "use strict"
    angular.module("CRMApp.Controllers")
            .controller("SpecificationCtrl", [
            "$scope", "$rootScope", "$timeout", "$filter", "SpecificationService", "$uibModal",
            SpecificationCtrl]);

    function SpecificationCtrl($scope, $rootScope, $timeout, $filter, SpecificationService, $uibModal) {

        $scope.Add = function (id, _isdisable) {
            if (_isdisable === undefined) _isdisable = 0;
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    SpecificationService: function () { return SpecificationService; },
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
               //{ "title": "SpecificationId", "data": "SpecificationId", filter: false, visible: false },TechHead
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "Technical Specification Head", "field": "TechHead", sortable: "TechHead", filter: { TechHead: "text" }, show: true, },
               { "title": "Technical Specification", "field": "TechSpec", sortable: "TechSpec", filter: { TechSpec: "text" }, show: true, },

               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.SpecificationId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                            //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.SpecificationId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                            '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.SpecificationId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { "SpecificationId": "asc" }
        }

        $scope.Edit = function (id) {
            $scope.Add(id, 0);
        }
        $scope.View = function (id) {
            $scope.Add(id, 1);
        }
        $scope.Delete = function (id) {
            SpecificationService.DeleteSpecification(id).then(function (result) {
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

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, SpecificationService, id, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }

        $scope.objSpecification = $scope.objSpecification || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.id = id;
        $scope.storage = {};

        if (id && id > 0) {
            SpecificationService.GetByIdSpecification(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objSpecification = {
                        SpecificationId: result.data.DataList.SpecificationId,
                        TechSpec: result.data.DataList.TechSpec,
                        TechHeadId: result.data.DataList.TechHeadId,
                        TechHeaddata: { Display: result.data.DataList.TechHead, Value: result.data.DataList.TechHeadId },
                    }
                    $scope.storage = angular.copy($scope.objSpecification);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        function ResetForm() {
            $scope.objSpecification = {
                SpecificationId: 0,
                TechSpec: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormSpecificationInfo)
                $scope.$parent.FormSpecificationInfo.$setPristine();
        }

        $scope.CreateUpdate = function (data) {
            data.TechHeadId = data.TechHeaddata.Value;
            data.TechHead = data.TechHeaddata.Display;
            SpecificationService.CreateUpdateSpecification(data).then(function (result) {
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
            if ($scope.objSpecification.SpecificationId > 0) {
                $scope.objSpecification = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'SpecificationService', 'id', 'isdisable']
})()





