(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("EmployeeShiftController", [
               "$scope", "EmployeeShiftService", "$filter", "$uibModal",
                function HolidayController($scope, EmployeeShiftService, $filter, $uibModal) {
                    $scope.storage = {};
                    $scope.setDirectiveRefresh = function (refreshGrid) {
                        $scope.refreshGrid = refreshGrid;
                    };

                    $scope.AddShift = function () {
                        var _isdisable = 0;
                        $scope.shiftObj = {
                            ShiftId: 0,
                            InTime: '',
                            OutTime: '',
                            Hours: '',
                            LateEntryCalculate: '',
                            ShiftName: ''
                        }

                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'shiftModalContent.html',
                            controller: 'ShiftModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                shiftObj: function () {
                                    return $scope.shiftObj;
                                },
                                storage: function () {
                                    return $scope.storage;
                                },
                                isdisable: function () { return _isdisable; }
                            }
                        })
                        modalInstance.result.then(function (selectedItem) {
                            $scope.refreshGrid();
                            // $scope.tableParams.reload();
                        });
                    }

                    $scope.Edit = function (data) {
                        var _isdisable = 0;
                        var d = new Date();
                        d.setHours($filter('date')(data.InTime, "hh:mm").Hours);
                        d.setMinutes($filter('date')(data.InTime, "hh:mm").Minutes);
                        var intime = d;
                        var out = new Date();
                        out.setHours($filter('date')(data.OutTime, "hh:mm").Hours);
                        out.setMinutes($filter('date')(data.OutTime, "hh:mm").Minutes);
                        var outtime = out;
                        var late = new Date();
                        late.setHours($filter('date')(data.LateEntryCalculate, "hh:mm").Hours);
                        late.setMinutes($filter('date')(data.LateEntryCalculate, "hh:mm").Minutes);
                        var lateentrycalculate = late;
                        //var timeDiff = getTimeDifference(data.InTime, data.OutTime)
                        $scope.shiftObj = {
                            ShiftId: data.ShiftId,
                            InTime: intime,
                            OutTime: outtime,
                            Hours: data.Hours,
                            LateEntryCalculate: lateentrycalculate,
                            ShiftName: data.ShiftName
                        }
                        $scope.storage = angular.copy($scope.shiftObj);
                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'shiftModalContent.html',
                            controller: 'ShiftModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                shiftObj: function () {
                                    return $scope.shiftObj;
                                },
                                storage: function () {
                                    return $scope.storage;
                                },
                                isdisable: function () { return _isdisable; }
                            }
                        })
                        modalInstance.result.then(function (selectedItem) {
                            $scope.refreshGrid();
                            // $scope.tableParams.reload();
                        }, function () {
                            // $log.info('Modal dismissed at: ' + new Date());
                        });
                    }

                    $scope.View = function (data) {
                        var _isdisable = 1;
                        var d = new Date();
                        d.setHours($filter('date')(data.InTime, "hh:mm").Hours);
                        d.setMinutes($filter('date')(data.InTime, "hh:mm").Minutes);
                        var intime = d;
                        var out = new Date();
                        out.setHours($filter('date')(data.OutTime, "hh:mm").Hours);
                        out.setMinutes($filter('date')(data.OutTime, "hh:mm").Minutes);
                        var outtime = out;
                        var late = new Date();
                        late.setHours($filter('date')(data.LateEntryCalculate, "hh:mm").Hours);
                        late.setMinutes($filter('date')(data.LateEntryCalculate, "hh:mm").Minutes);
                        var lateentrycalculate = late;
                        //var timeDiff = getTimeDifference(data.InTime, data.OutTime)
                        $scope.shiftObj = {
                            ShiftId: data.ShiftId,
                            InTime: intime,
                            OutTime: outtime,
                            Hours: data.Hours,
                            LateEntryCalculate: lateentrycalculate,
                            ShiftName: data.ShiftName
                        }
                        $scope.storage = angular.copy($scope.shiftObj);
                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'shiftModalContent.html',
                            controller: 'ShiftModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                shiftObj: function () {
                                    return $scope.shiftObj;
                                },
                                storage: function () {
                                    return $scope.storage;
                                },
                                isdisable: function () { return _isdisable; }
                            }
                        })
                        modalInstance.result.then(function (selectedItem) {
                            $scope.refreshGrid();
                            // $scope.tableParams.reload();
                        }, function () {
                            // $log.info('Modal dismissed at: ' + new Date());
                        });
                    }
                    $scope.Delete = function (id) {
                        EmployeeShiftService.DeleteShift(id).then(function (result) {
                            if (result.data.ResponseType == 1) {
                                toastr.success(result.data.Message)
                            }
                            else {
                                toastr.error(result.data.Message)
                            }
                            $scope.refreshGrid()
                        }, function (errorMsg) {
                            toastr.error(errorMsg, 'Opps, Something went wrong');
                        })
                    }
                    $scope.gridObj = {
                        columnsInfo: [
                           //{ "title": "ShiftId", "data": "ShiftId", filter: false, visible: false },
                           { "title": "Sr.", "field": "RowNumber", filter: false, show: true },
                           { "title": "ShiftName", "field": "ShiftName", sortable: "ShiftName", filter: { ShiftName: "text" }, show: true },
                           {
                               "title": "InTime", "field": "InTime", sortable: "InTime", filter: { InTime: "text" }, show: true,
                               'cellTemplte': function ($scope, row) {
                                   var element = '<p ng-bind="ConvertTime(row.InTime)">';
                                   return $scope.getHtml(element);
                               }
                           },
                           {
                               "title": "OutTime", "field": "OutTime", sortable: "OutTime", filter: { OutTime: "text" }, show: true,
                               'cellTemplte': function ($scope, row) {
                                   var element = '<p ng-bind="ConvertTime(row.OutTime)">';
                                   return $scope.getHtml(element);
                               }
                           },
                           {
                               "title": "Hours", "field": "Hours", sortable: "Hours", filter: { Hours: "text" }, show: true,
                           },
                           {
                               "title": "LateEntryCalculate", "field": "LateEntryCalculate", sortable: "LateEntryCalculate", filter: { LateEntryCalculate: "text" }, show: true,
                               'cellTemplte': function ($scope, row) {
                                   var element = '<p ng-bind="ConvertTime(row.LateEntryCalculate)">';
                                   return $scope.getHtml(element);
                               }
                           },
                           {
                               "title": "Action", filter: false, show: true,
                               'cellTemplte': function ($scope, row) {
                                   var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                              //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.ShiftId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                              '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                                   return $scope.getHtml(element);
                               }
                           }
                        ],
                        Sort: { 'HolidayId': 'asc' }

                    }
                }]);

    angular.module('CRMApp.Controllers')
        .controller('ShiftModalInstanceCtrl', ['$scope', '$uibModalInstance', 'shiftObj',
            'EmployeeShiftService', '$filter', 'storage', 'isdisable',
            function ($scope, $uibModalInstance, shiftObj, EmployeeShiftService, $filter, storage, isdisable) {
                $scope.isClicked = false;
                if (isdisable == 1) {
                    $scope.isClicked = true;
                }
                var $ctrl = this;

                function getTimeDifference(inTime, outTime) {
                    var data = isNaN(((outTime - inTime) / 1000 / 60 / 60).toFixed(2)) ? 0 : ((outTime - inTime) / 1000 / 60 / 60).toFixed(2)
                    return data;
                }

                $ctrl.shiftObj = shiftObj;
                $ctrl.storage = storage;
                $ctrl.shiftObj.Hours = getTimeDifference(shiftObj.InTime, shiftObj.OutTime);

                $ctrl.close = function () {
                    $uibModalInstance.close();
                }

                var GenerateModel = function () {
                    $ctrl.shiftObj.InTime = $filter('date')($ctrl.shiftObj.InTime, "HH:mm");
                    $ctrl.shiftObj.OutTime = $filter('date')($ctrl.shiftObj.OutTime, "HH:mm");
                    $ctrl.shiftObj.LateEntryCalculate = $filter('date')($ctrl.shiftObj.LateEntryCalculate, "HH:mm");
                }

                $ctrl.Update = function () {
                    GenerateModel();
                    EmployeeShiftService.UpdateShift($ctrl.shiftObj).then(function (result) {
                        if (result.data.ResponseType == 1) {
                            toastr.success(result.data.Message);
                            $uibModalInstance.close();
                        } else {
                            toastr.error(result.data.Message);
                        }
                    }, function (errorMsg) {
                        toastr.error(errorMsg, 'Opps, Something went wrong');
                    })
                }


                $ctrl.Reset = function () {

                    if ($ctrl.shiftObj.ShiftId > 0) {

                        $ctrl.shiftObj = angular.copy($ctrl.storage);

                    } else {
                        ResetForm();
                    }
                }


                function ResetForm() {

                    $ctrl.shiftObj = {
                        ShiftId: 0,
                        ShiftName: '',
                        InTime: '',
                        OutTime: '',
                        Hours: '',
                        LateEntryCalculate: ''
                    }
                    $ctrl.storage = {};
                    $ctrl.addMode = true;
                    $ctrl.saveText = "Save";
                    if ($ctrl.$parent.FormShiftInfo)
                        $ctrl.$parent.FormShiftInfo.$setPristine();
                }


                $ctrl.Add = function () {
                    GenerateModel();
                    EmployeeShiftService.AddShift($ctrl.shiftObj).then(function (result) {
                        if (result.data.ResponseType == 1) {
                            toastr.success(result.data.Message);
                            $uibModalInstance.close();
                        } else {
                            toastr.error(result.data.Message);
                        }
                    }, function (errorMsg) {
                        toastr.error(errorMsg, 'Opps, Something went wrong');
                    })
                }

                $scope.$watch(function () { return $ctrl.shiftObj.OutTime }, function (newval, oldval) {
                    if (newval != oldval) {
                        $ctrl.shiftObj.Hours = getTimeDifference($ctrl.shiftObj.InTime, $ctrl.shiftObj.OutTime);
                    }
                })

                $scope.$watch(function () { return $ctrl.shiftObj.InTime }, function (newval, oldval) {
                    if (newval != oldval) {
                        $ctrl.shiftObj.Hours = getTimeDifference($ctrl.shiftObj.InTime, $ctrl.shiftObj.OutTime);
                    }
                })

            }]);
})();