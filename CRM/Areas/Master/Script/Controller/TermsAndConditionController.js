(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("TermsAndConditionController", [
               "$scope", "TermsAndConditionService", "$uibModal",
               TermsAndConditionController]);

    function TermsAndConditionController($scope, TermsAndConditionService, $uibModal) {

        $scope.Add = function (id, _isdisable) {
            if (_isdisable === undefined) _isdisable = 0;
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    TermsAndConditionService: function () { return TermsAndConditionService; },
                    TermsAndConditionController: function () { return TermsAndConditionController; },
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
               //{ "title": "Terms Id", "data": "TermsId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "Title", "field": "Title", sortable: "Title", filter: { Title: "text" }, show: true, },
               {
                   "title": "Description", "field": "Description", sortable: "Description", filter: { Description: "text" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<div ng-bind-html="getHtml(row.Description)"></div>'
                       return $scope.getHtml(element);
                   }
               },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.TermsId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                           //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.TermsId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                           '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.TermsId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'TermsId': 'asc' }
        }

        $scope.Edit = function (id) {
            $scope.Add(id, 0);
        }
        $scope.View = function (id) {
            $scope.Add(id, 1);

        }

        $scope.Delete = function (data) {
            TermsAndConditionService.DeleteTermsAndCondition(data).then(function (result) {
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

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, TermsAndConditionService, TermsAndConditionController, id, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }

        $scope.objterms = $scope.objterms || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.id = id;
        $scope.storage = {};
        $scope.editorOptions = {
            language: 'en',
            uiColor: '#f0f2f5'
        };

        if (id && id > 0) {
            TermsAndConditionService.GettermsAndConditionById(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objterms = {
                        TermsId: result.data.DataList.TermsId,
                        Description: result.data.DataList.Description,
                        Title: result.data.DataList.Title,
                    }
                    $scope.storage = angular.copy($scope.objterms);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        function ResetForm() {
            $scope.objterms = {
                TermsId: 0,
                Description: '',
                Title: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormTermsInfo)
                $scope.$parent.FormTermsInfo.$setPristine();
        }

        $scope.Create = function (data) {
            TermsAndConditionService.AddTermsAndCondition(data).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $uibModalInstance.close();
                    toastr.success(result.data.Message);
                    ResetForm();
                } else {
                    toastr.error(result.data.Message);
                }

            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.Update = function (data) {
            TermsAndConditionService.AddTermsAndCondition(data).then(function (result) {
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
            if ($scope.objterms.TermsId > 0) {
                $scope.objterms = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'TermsAndConditionService', 'TermsAndConditionController', 'id', 'isdisable']

})()


