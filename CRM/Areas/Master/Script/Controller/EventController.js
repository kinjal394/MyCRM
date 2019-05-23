(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("EventController", [
               "$scope", "EventService", "$filter", "$uibModal",
                function EventController($scope, EventService, $filter, $uibModal) {
                    $scope.storage = {};
                    $scope.AddEvent = function () {
                        var _isdisable = 0;
                        $scope.EventObj = {
                            EventId: 0,
                            EventTypeId: '',
                            EventName: '',
                            EventDate: '',
                            EventTypeName: { Display: '', Value: '' }
                        }
                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'eventModalContent.html',
                            controller: 'EventModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                EventObj: function () {
                                    return $scope.EventObj;
                                },
                                storage: function () {
                                    return $scope.storage;
                                },
                                isdisable: function () { return _isdisable; }

                            }
                        });
                        modalInstance.result.then(function () {
                            $scope.refreshGrid();
                            // $ctrl.selected = selectedItem;
                        }, function () {
                        });
                    }

                    $scope.setDirectiveRefresh = function (refreshGrid) {
                        $scope.refreshGrid = refreshGrid;
                    };

                    $scope.gridObj = {
                        columnsInfo: [
                           //{ "title": "EventId", "data": "EventId", filter: false, visible: false },
                           { "title": "Sr.", "field": "RowNumber", show: true, },
                           //{ "title": "EventTypeId", "data": "EventTypeId", filter: false, visible: false },
                           { "title": "EventType Name", "field": "EventTypeName", sortable: "EventTypeName", filter: { EventTypeName: "text" }, show: true, },
                           { "title": "Event Name", "field": "EventName", sortable: "EventName", filter: { EventName: "text" }, show: true, },
                           {
                               "title": "Event Date", "field": "EventDate", sortable: "EventDate", filter: { EventDate: "date" }, show: true,
                               'cellTemplte': function ($scope, row) {
                                   var element = '<p ng-bind="ConvertDate(row.EventDate,\'dd/mm/yyyy\')">'
                                   return $scope.getHtml(element);
                               }
                           },
                           {
                               "title": "Action", show: true,
                               //'render': '<button class="btn btn-success" data-ng-click="$parent.$parent.$parent.Edit(row)">Edit</button><button class="btn btn-success" data-ng-click="$parent.$parent.$parent.Delete(row.EventId)">Delete</button> '
                               'cellTemplte': function ($scope, row) {
                                   var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                                 //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.EventId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                               '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                                   return $scope.getHtml(element);
                               }
                           }
                        ],
                        Sort: { 'EventId': 'asc' },
                        modeType: "EventMaster",
                        Title: "Event List"
                    }

                    $scope.Edit = function (data) {
                        var _isdisable = 0;
                        $scope.EventObj = {
                            EventId: data.EventId,
                            EventTypeId: data.EventTypeId,
                            EventName: data.EventName,
                            EventDate: $filter('mydate')(data.EventDate),
                            EventTypeName: { Display: data.EventTypeName, Value: data.EventTypeId }

                        }
                        $scope.storage = angular.copy($scope.EventObj);
                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'eventModalContent.html',
                            controller: 'EventModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                EventObj: function () {
                                    return $scope.EventObj;
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
                        $scope.EventObj = {
                            EventId: data.EventId,
                            EventTypeId: data.EventTypeId,
                            EventName: data.EventName,
                            EventDate: $filter('mydate')(data.EventDate),
                            EventTypeName: { Display: data.EventTypeName, Value: data.EventTypeId }

                        }
                        $scope.storage = angular.copy($scope.EventObj);
                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'eventModalContent.html',
                            controller: 'EventModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                EventObj: function () {
                                    return $scope.EventObj;
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
                    $scope.Delete = function (eventid) {
                        EventService.DeleteEvent(eventid).then(function (result) {
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

    angular.module('CRMApp.Controllers').controller('EventModalInstanceCtrl', [
        '$scope', '$uibModalInstance', 'EventObj', 'EventService', 'storage', 'isdisable',
        function ($scope, $uibModalInstance, EventObj, EventService, storage, isdisable) {
            $scope.isClicked = false;
            if (isdisable == 1) {
                $scope.isClicked = true;
            }

            var $ctrl = this;

            EventService.getAllEventType().then(function (data) {
                $ctrl.Eventtype = data.data;
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })

            $ctrl.EventData = EventObj;
            $ctrl.storage = storage;

            $ctrl.ok = function () {
                $uibModalInstance.close();
            };
            $ctrl.saveEvent = function (EventData) {
                EventData.EventTypeId = $ctrl.EventData.EventTypeName.Value
                EventService.AddEventdata(EventData).then(function (result) {
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
            $ctrl.Update = function (EventData) {
                EventData.EventTypeId = $ctrl.EventData.EventTypeName.Value
                EventService.Update(EventData).then(function (result) {
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

                if ($ctrl.EventData.EventId > 0) {

                    $ctrl.EventData = angular.copy($ctrl.storage);

                } else {
                    ResetForm();
                }
            }


            function ResetForm() {

                $ctrl.EventData = {
                    EventId: 0,
                    EventTypeId: '',
                    EventTypeName: '',
                    EventName: '',
                    EventDate: ''
                }
                $ctrl.storage = {};
                $ctrl.addMode = true;
                $ctrl.saveText = "Save";
                if ($ctrl.$parent.EventInfo)
                    $ctrl.$parent.EventInfo.$setPristine();
            }

            $ctrl.inlineOptions = {
                // customClass: getDayClass,
                minDate: new Date(),
                showWeeks: true
            };

            //$ctrl.dateOptions = {
            //    //dateDisabled: disabled,
            //    formatYear: 'yy',
            //    maxDate: new Date(2020, 5, 22),
            //    minDate: new Date(),
            //    startingDay: 1
            //};
            $scope.dateOptions = {
                formatYear: 'dd-MM-yyyy',
                //minDate: new Date(2016, 8, 5),
                //maxDate: new Date(),
                startingDay: 1
            };

            $ctrl.open1 = function () {
                $ctrl.popup1.opened = true;
            };
            $ctrl.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate', 'dd-MM-yyyy'];
            $ctrl.format = $ctrl.formats[4];
            $ctrl.altInputFormats = ['M!/d!/yyyy'];

            $ctrl.popup1 = {
                opened: false
            };
        }]);

    angular.module('CRMApp.Controllers').filter("mydate", function () {
        var re = /\/Date\(([0-9]*)\)\//;
        return function (x) {
            var m = x.match(re);
            if (m) return new Date(parseInt(x.substr(6)));
            else return null;
        };
    });

})();