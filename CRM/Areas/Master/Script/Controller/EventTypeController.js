(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("EventTypeController", [
               "$scope", "EventTypeService", "$filter", "$uibModal",
                function EventTypeController($scope, EventTypeService, $filter, $uibModal) {
                    $scope.AddEventType = function () {
                        var _isdisable = 0;
                        $scope.EventTypeObj = {
                            EventTypeId: 0,
                            EventTypeName: ''
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
                                EventTypeObj: function () {
                                    return $scope.EventTypeObj;
                                },
                                isdisable: function () { return _isdisable; }
                            }
                        });

                        modalInstance.result.then(function () {
                            $scope.refreshGrid();
                            $ctrl.selected = selectedItem;

                        }, function () {
                            // $log.info('Modal dismissed at: ' + new Date());
                        });
                    }

                    $scope.setDirectiveRefresh = function (refreshGrid) {
                        $scope.refreshGrid = refreshGrid;
                    };

                    $scope.gridObj = {
                        columnsInfo: [
                           //{ "title": "Event Type Id", "data": "EventTypeId", filter: false, visible: false },
                           { "title": "Sr.", "field": "RowNumber", show: true, },
                           { "title": "Event Type Name", "field": "EventTypeName", sortable: "EventTypeName", filter: { EventTypeName: "text" }, show: true, },

                           {
                               "title": "Action", show: true,
                               //'render': '<button class="btn btn-success" data-ng-click="$parent.$parent.$parent.Edit(row)">Edit</button><button class="btn btn-success" data-ng-click="$parent.$parent.$parent.Delete(row.EventTypeId)">Delete</button> '
                               'cellTemplte': function ($scope, row) {
                                   var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                              //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.EventTypeId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                               '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                                   return $scope.getHtml(element);
                               }
                           }
                        ],
                        Sort: { 'EventTypeId': 'asc' }

                    }

                    $scope.Edit = function (data) {
                        var _isdisable = 0;
                        $scope.EventTypeObj = {
                            EventTypeId: data.EventTypeId,
                            EventTypeName: data.EventTypeName
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
                                EventTypeObj: function () {
                                    return $scope.EventTypeObj;
                                },
                                isdisable: function () { return _isdisable; }
                            }
                        });

                        modalInstance.result.then(function (selectedItem) {
                            //$scope.tableParams.reload();
                            $scope.refreshGrid();
                        }, function () {
                            // $log.info('Modal dismissed at: ' + new Date());
                        });
                    }


                    $scope.View = function (data) {
                        var _isdisable = 1;
                        $scope.EventTypeObj = {
                            EventTypeId: data.EventTypeId,
                            EventTypeName: data.EventTypeName
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
                                EventTypeObj: function () {
                                    return $scope.EventTypeObj;
                                },
                                isdisable: function () { return _isdisable; }
                            }
                        });

                        modalInstance.result.then(function (selectedItem) {
                            //$scope.tableParams.reload();
                            $scope.refreshGrid();
                        }, function () {
                            // $log.info('Modal dismissed at: ' + new Date());
                        });
                    }


                    $scope.Delete = function (EventTypeId) {
                        EventTypeService.DeleteEventType(EventTypeId).then(function (result) {
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
        '$uibModalInstance', 'EventTypeObj', 'EventTypeService', 'isdisable',
    function ($scope, $uibModalInstance, EventTypeObj, EventTypeService, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        var $ctrl = this;

        $ctrl.ok = function () {
            $uibModalInstance.close();
        };

        $ctrl.EventTypeObj = EventTypeObj;

        $ctrl.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

        $ctrl.saveEventType = function (EventTypeData) {
            EventTypeService.AddEventType(EventTypeData).then(function (result) {
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

        $ctrl.UpdateEventType = function (EventType) {
            EventTypeService.Update(EventType).then(function (result) {
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
    }]);
})();