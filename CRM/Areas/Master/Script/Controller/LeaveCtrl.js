(function () {
    "use strict"
    angular.module("CRMApp.Controllers")
            .controller("LeaveCtrl", [
            "$scope", "$rootScope", "$timeout", "$filter", "LeaveService", "$uibModal", LeaveCtrl
            ]);

    function LeaveCtrl($scope, $rootScope, $timeout, $filter, LeaveService, $uibModal) {

        $scope.Add = function (id, _isdisable) {
            if (_isdisable === undefined) _isdisable = 0;
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    LeaveService: function () { return LeaveService; },
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
               //{ "title": "ID", "data": "LeaveId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               {
                   "title": "From Date", "field": "FromDate", sortable: "FromDate", filter: { FromDate: "date" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<p ng-bind="ConvertDate(row.FromDate,\'dd/mm/yyyy\')">'
                       return $scope.getHtml(element);
                   }
               },
               {
                   "title": "To Date", "field": "ToDate", sortable: "ToDate", filter: { ToDate: "date" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<p ng-bind="ConvertDate(row.ToDate,\'dd/mm/yyyy\')">'
                       return $scope.getHtml(element);
                   }
               },
               { "title": "Total Days", "field": "TotalDays", sortable: "TotalDays", filter: { TotalDays: "text" }, show: true, },
               //{ "title": "Reason", "data": "Reason" },
               {
                   "title": "Reason", "field": "Reason", sortable: "Reason", filter: { Reason: "text" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<p data-uib-tooltip="{{row.Reason}}" ng-bind="LimitString(row.Reason,100)">'
                       return $scope.getHtml(element);
                   }
               },
               //{ "title": "Status", "field": "Status", sortable: "Status", filter: { Status: "text" }, show: false, },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.LeaveId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                            //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.LeaveId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                            '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.LeaveId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { "LeaveId": "asc" }
        }

        $scope.Edit = function (id) {
            $scope.Add(id, 0);
        }
        $scope.View = function (id) {
            $scope.Add(id, 1);
        }
        $scope.Delete = function (id) {
            LeaveService.DeleteLeave(id).then(function (result) {
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

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, $filter, LeaveService, id, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.objLeave = $scope.objLeave || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.id = id;
        $scope.storage = {};

        if (id && id > 0) {
            LeaveService.GetByIdLeave(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objLeave = {
                        LeaveId: result.data.DataList.LeaveId,
                        FromDate: $filter('mydate')(result.data.DataList.FromDate),
                        ToDate: $filter('mydate')(result.data.DataList.ToDate),
                        IsHalf: result.data.DataList.IsHalf,
                        TotalDays: result.data.DataList.TotalDays,
                        Reason: result.data.DataList.Reason,
                        Status: result.data.DataList.Status
                    }
                    $scope.storage = angular.copy($scope.objLeave);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            })
        } else {
            ResetForm();
        }

        function ResetForm() {
            $scope.objLeave = {
                LeaveId: 0,
                FromDate: new Date(),
                ToDate: new Date(),
                IsHalf: '',
                TotalDays: 0,
                Reason: '',
                Status: 0
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormLeaveInfo)
                $scope.$parent.FormLeaveInfo.$setPristine();
        }

        $scope.CreateUpdate = function (data) {
            LeaveService.SaveLeave(data).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    ResetForm();
                    $uibModalInstance.close();
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.Reset = function () {
            if ($scope.objLeave.LeaveId > 0) {
                $scope.objLeave = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

        $scope.dateOptions = {
            formatYear: 'yy',
            minDate: new Date(),
            startingDay: 1
        };

        $scope.$watch('objLeave.FromDate', function (val) {
            if (val && val > 0 && $scope.objLeave.ToDate == $scope.objLeave.FromDate) {

                $scope.objLeave.ToDate = val;
            }
            $scope.dateOptions1 = {
                formatYear: 'yy',
                minDate: val,
                startingDay: 1
            };
        })

        $scope.$watch('objLeave.ToDate', function (val) {
            if (val && val > 0) {
                var first = $scope.objLeave.FromDate;
                var second = val;
                var totaldays = dayDiff(first, second);
                if ($scope.objLeave.IsHalf) {
                    totaldays -= 0.5;
                }
                $scope.objLeave.TotalDays = totaldays;
            }
        })

        $scope.checkSelection = function ($event) {
            var checkbox = $event.target;
            if (checkbox.checked) {
                $scope.objLeave.TotalDays -= 0.5;
            } else {
                $scope.objLeave.TotalDays += 0.5;
            }
        };

        function convert(str) {
            var date = new Date(str),
                mnth = ("0" + (date.getMonth() + 1)).slice(-2),
                day = ("0" + date.getDate()).slice(-2);
            return [date.getFullYear(), mnth, day].join("-");
        }

        function dayDiff(firstDate, secondDate) {
            var date2 = new Date(convert(secondDate));
            var date1 = new Date(convert(firstDate));
            var timeDiff = Math.abs(date2.getTime() - date1.getTime());
            var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24)) + 1;
            return diffDays;
        }

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', '$filter', 'LeaveService', 'id', 'isdisable']

    angular.module('CRMApp.Controllers').filter("mydate", function () {
        var re = /\/Date\(([0-9]*)\)\//;
        return function (x) {
            var m = x.match(re);
            if (m) return new Date(parseInt(x.substr(6)));
            else return null;
        };
    });

})()



