(function () {
    "use strict"
    angular.module("CRMApp.Controllers")
            .controller("DeliveryTermsCtrl", [
            "$scope", "$rootScope", "$timeout", "$filter", "DeliveryTermsService", "$uibModal",
            DeliveryTermsCtrl
            ]);

    function DeliveryTermsCtrl($scope, $rootScope, $timeout, $filter, DeliveryTermsService, $uibModal) {
        $scope.Add = function (id, _isdisable) {
            if (_isdisable === undefined) _isdisable = 0;
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    DeliveryTermsService: function () { return DeliveryTermsService; },
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
               //{ "title": "Term Id", "data": "TermsId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "Delivery Terms", "field": "DeliveryName", sortable: "DeliveryName", filter: { DeliveryName: "text" }, show: true, },
               //{ "title": "Description", "data": "Description" },
               {
                   "title": "Place of delivery", "field": "Description", sortable: "Description", filter: { Description: "text" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<p data-uib-tooltip="{{row.Description}}" ng-bind="LimitString(row.Description,100)">'
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
            Sort: { "TermsId": "asc" }
        }

        $scope.Edit = function (id) {
            $scope.Add(id, 0);
        }
        $scope.View = function (id) {
            $scope.Add(id, 1);
        }
        $scope.Delete = function (id) {
            DeliveryTermsService.DeleteDeliveryTerms(id).then(function (result) {
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

        $scope.refreshtable = function () {
            $scope.tableparams.reload();
        };

    }

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, DeliveryTermsService, id, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }

        $scope.objDeliveryTerms = $scope.objDeliveryTerms || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.id = id;
        $scope.storage = {};

        if (id && id > 0) {
            DeliveryTermsService.GetByIdDeliveryTerms(id).then(function (result) {
                $scope.objDeliveryTerms = result.data.DataList;
                if (result.data.ResponseType == 1) {
                    $scope.objDeliveryTerms = {
                        TermsId: result.data.DataList.TermsId,
                        DeliveryName: result.data.DataList.DeliveryName,
                        Description: result.data.DataList.Description
                    }
                    $scope.storage = angular.copy($scope.objDeliveryTerms);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        } else {
            ResetForm();
        }

        function ResetForm() {
            $scope.objDeliveryTerms = {
                TermsId: 0,
                DeliveryName: '',
                Description: '',
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormDeliveryTermsInfo)
                $scope.$parent.FormDeliveryTermsInfo.$setPristine();
        }

        $scope.CreateUpdate = function (data) {
            DeliveryTermsService.CreateUpdateDeliveryTerms(data).then(function (result) {
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
            if ($scope.objDeliveryTerms.TermsId > 0) {
                $scope.objDeliveryTerms = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };
    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'DeliveryTermsService', 'id', 'isdisable']
})()





