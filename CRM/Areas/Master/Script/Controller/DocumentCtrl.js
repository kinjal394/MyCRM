(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("DocumentCtrl", [
               "$scope", "DocumentService", "$filter", "$uibModal",
                function EventTypeController($scope, DocumentService, $filter, $uibModal) {
                    $scope.storage = {};
                    $scope.AddDocument = function () {
                        var _isdisable = 0;
                        $scope.Documentobj = {
                            DocId: 0,
                            DocName: ''
                        }
                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'myModalContent.html',
                            controller: 'ModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                Documentobj: function () {
                                    return $scope.Documentobj;
                                },
                                storage: function () {
                                    return $scope.storage;
                                },
                                isdisable: function () { return _isdisable; }
                            }
                        });

                        modalInstance.result.then(function () {
                            $scope.refreshGrid();
                            $ctrl.selected = selectedItem;
                        }, function () {
                        });
                    }

                    $scope.setDirectiveRefresh = function (refreshGrid) {
                        $scope.refreshGrid = refreshGrid;
                    };

                    $scope.gridObj = {
                        columnsInfo: [
                           //{ "title": "DocId", "data": "DocId", filter: false, visible: false },
                           { "title": "Sr.", "field": "RowNumber", show: true, },
                           { "title": "Document Name", "field": "DocName", sortable: "DocName", filter: { DocName: "text" }, show: true, },
                           {
                               "title": "Action", show: true,
                               'cellTemplte': function ($scope, row) {
                                   var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                               //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.DocId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                                '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                                   return $scope.getHtml(element);
                               }
                           }
                        ],
                        Sort: { 'DocId': 'asc' }
                    }

                    $scope.Edit = function (data) {
                        var _isdisable = 0;
                        $scope.Documentobj = {
                            DocId: data.DocId,
                            DocName: data.DocName
                        }
                        $scope.storage = angular.copy($scope.Documentobj);
                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'myModalContent.html',
                            controller: 'ModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                Documentobj: function () {
                                    return $scope.Documentobj;
                                },
                                storage: function () {
                                    return $scope.storage;
                                },
                                isdisable: function () { return _isdisable; }
                            }
                        });
                        modalInstance.result.then(function (selectedItem) {
                            $scope.refreshGrid();
                        }, function () {
                        });
                    }
                    $scope.View = function (data) {
                        var _isdisable = 1;
                        $scope.Documentobj = {
                            DocId: data.DocId,
                            DocName: data.DocName
                        }
                        $scope.storage = angular.copy($scope.Documentobj);
                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'myModalContent.html',
                            controller: 'ModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                Documentobj: function () {
                                    return $scope.Documentobj;
                                },
                                storage: function () {
                                    return $scope.storage;
                                },
                                isdisable: function () { return _isdisable; }
                            }
                        });
                        modalInstance.result.then(function (selectedItem) {
                            $scope.refreshGrid();
                        }, function () {
                        });
                    }
                    $scope.Delete = function (DocId) {
                        DocumentService.DeleteDoc(DocId).then(function (result) {
                            if (result.data.ResponseType == 1) {
                                toastr.success(result.data.Message)
                                $scope.refreshGrid();
                            }
                            else {
                                toastr.error(result.data.Message)
                            }
                        }, function (errorMsg) {
                            toastr.error(errorMsg, 'Opps, Something went wrong');
                        })
                    }
                }]);

    angular.module('CRMApp.Controllers').controller('ModalInstanceCtrl', ['$scope',
        '$uibModalInstance', 'Documentobj', 'DocumentService', 'storage', 'isdisable',
        function ($scope, $uibModalInstance, Documentobj, DocumentService, storage, isdisable) {


            var $ctrl = this;
            $scope.isClicked = false;
            if (isdisable == 1) {
                $scope.isClicked = true;
            }
            $ctrl.ok = function () {
                $uibModalInstance.close();
            };

            $ctrl.Documentobj = Documentobj;
            $ctrl.storage = storage;
            $ctrl.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };

            $ctrl.saveDoc = function (Docdata) {
                DocumentService.AddDoc(Docdata).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        $uibModalInstance.close();
                        toastr.success(result.data.Message)
                    }
                    else {
                        toastr.error(result.data.Message)
                    }
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }

            $ctrl.UpdateDoc = function (Docdata) {
                DocumentService.UpdateDoc(Docdata).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        $uibModalInstance.close();
                        toastr.success(result.data.Message)
                    }
                    else {
                        toastr.error(result.data.Message)
                    }
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }

            $ctrl.Reset = function () {

                if ($ctrl.Documentobj.DocId > 0) {

                    $ctrl.Documentobj = angular.copy($ctrl.storage);

                } else {
                    ResetForm();
                }
            }


            function ResetForm() {

                $ctrl.Documentobj = {
                    DocId: 0,
                    DocName: '',

                }
                $ctrl.storage = {};
                $ctrl.addMode = true;
                $ctrl.saveText = "Save";
                if ($ctrl.$parent.FormAreaInfo)
                    $ctrl.$parent.FormAreaInfo.$setPristine();
            }

        }]);
})();