(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("WorkReminderController", [
         "$scope", "WorkReminderService", "$filter", "$uibModal",
         WorkReminderController]);

    function WorkReminderController($scope, WorkReminderService, $filter, $uibModal) {
        $scope.id = 0;
        $scope.Add = function () {
            var _isdisable = 0;
            var objWorkRemind = [];
            $scope.objWorkRemind = {
                WorkRemindId: 0,
                DepartmentId: '',
                DepartmentData: { Display: '', Value: '' },
                Title: '',
                Description: '',
                RemindDate: '',
                RemindTime: '',
                RemindMode: '',
            }

            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    WorkReminderService: function () { return WorkReminderService; },
                    WorkReminderController: function () { return WorkReminderController; },
                    objWorkRemind: function () { return objWorkRemind; },
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

               //{ "title": "Work Remind Id", "field": "WorkRemindId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", filter: false, sort: false, show: true },
               //{ "title": "DepartmentId", "field": "DepartmentId", filter: false, visible: false },
               { "title": "Department Name", "field": "Department", sortable: "Department", filter: { Department: "text" }, show: true },
               { "title": "Work Title", "field": "Title", filter: { Title: "text" }, sortable: "Title", show: true },
               { "title": "Work Cycle", "field": "RemindMode", sort: true, sortable: "RemindMode", filter: { RemindMode: "text" }, },
              
               {
                   "title": "Remind Date", "field": "RemindDate", sortable: "RemindDate", filter: { RemindDate: "date" }, show: true,
                   'cellTemplte': function ($scope, row){
                    var element = '<span >{{ConvertDate(row.RemindDate,\'dd/mm/yyyy\')}}</span>'
                    return $scope.getHtml(element);
                   }
               },
               { "title": "Last Update & Current Status", "field": "Description", filter: { Description: "text" }, sortable: "Description", show: true },


                {
                    "title": "Remind Date", "field": "RemindTime", sort: true, filter: { RemindTime: "text" }, sortable: "RemindTime", show: false,
                    'cellTemplte': function ($scope, row){
                        var element = '<span >{{ConvertTime(row.RemindTime)}}</span>'
                        return $scope.getHtml(element);
                    }
                },
               
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row){
                   //'render': '<button class="btn btn-success" data-ng-click="$parent.$parent.$parent.Edit(row)">Edit</button><button class="btn btn-success" data-ng-click="$parent.$parent.$parent.Delete(row.AccountEntryId)">Delete</button> '
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil"></i></a>' +
                   //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.AccountId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                   '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                    return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'WorkRemindId': 'asc' }

        }

        $scope.Edit = function (data) {

            var _isdisable = 0;
            var d = new Date();
            var gettime = '';
            var mytime = d;
            if (data.RemindTime != null) {
                d.setHours($filter('date')(data.RemindTime, "HH:mm").Hours);
                d.setMinutes($filter('date')(data.RemindTime, "HH:mm").Minutes);
                gettime = data.RemindTime.Hours == 0 && data.RemindTime.Minutes == 0 ? new Date() : mytime;

            }
            var objWorkRemind = {
                WorkRemindId: data.WorkRemindId,
                DepartmentId: data.DepartmentId,
                DepartmentData: { Display: data.Department, Value: data.DepartmentId },
                Title: data.Title,
                Description: data.Description,
                RemindDate: $filter('mydate')(data.RemindDate),
                RemindTime: gettime,
                RemindMode: data.RemindMode,
            }
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    WorkReminderService: function () { return WorkReminderService; },
                    WorkReminderController: function () { return WorkReminderController; },
                    objWorkRemind: function () { return objWorkRemind; },
                    isdisable: function () { return _isdisable; }
                }
            });
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
            });
        }
        $scope.View = function (data) {
            var _isdisable = 1;
            var d = new Date();
            var gettime = '';
            var mytime = d;
            if (data.RemindTime != null) {
                d.setHours($filter('date')(data.RemindTime, "HH:mm").Hours);
                d.setMinutes($filter('date')(data.RemindTime, "HH:mm").Minutes);
                gettime = data.RemindTime.Hours == 0 && data.RemindTime.Minutes == 0 ? new Date() : mytime;

            }
            var objWorkRemind = {
                WorkRemindId: data.WorkRemindId,
                DepartmentId: data.DepartmentId,
                DepartmentData: { Display: data.Department, Value: data.DepartmentId },
                Title: data.Title,
                Description: data.Description,
                RemindDate: $filter('mydate')(data.RemindDate),
                RemindTime: gettime,
                RemindMode: data.RemindMode,
            }
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    WorkReminderService: function () { return WorkReminderService; },
                    WorkReminderController: function () { return WorkReminderController; },
                    objWorkRemind: function () { return objWorkRemind; },
                    isdisable: function () { return _isdisable; }
                }
            });
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
            });
        }
        $scope.Delete = function (WorkRemindId) {

            WorkReminderService.DeleteWorkReminder(WorkRemindId).then(function (result) {
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

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, WorkReminderService, WorkReminderController, $filter, objWorkRemind, Upload, isdisable) {
        $scope.dateOptions = {
            formatYear: 'yy',
            minDate: new Date(1950, 1, 1),
            startingDay: 1
        }
       

        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.objWorkRemind = $scope.objWorkRemind || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};
        $scope.callback = function (data) {
            console.log(data);
        }

        if (objWorkRemind.WorkRemindId && objWorkRemind.WorkRemindId > 0) {
            $scope.objWorkRemind = {

                WorkRemindId: objWorkRemind.WorkRemindId,
                DepartmentId: objWorkRemind.DepartmentId,
                DepartmentData: { Display: objWorkRemind.DepartmentData.Display, Value: objWorkRemind.DepartmentId },
                Title: objWorkRemind.Title,
                Description: objWorkRemind.Description,
                RemindDate: objWorkRemind.RemindDate,
                RemindTime:objWorkRemind.RemindTime,
                RemindMode: objWorkRemind.RemindMode
            }
            $scope.storage = angular.copy($scope.objstate);
            //$scope.objCategory = result.data.DataList.CategoryName;
        } else {
            //toastr.error(objstate.data.Message, 'Opps, Something went wrong');
            ResetForm();
        }

        function ResetForm() {
            $scope.objWorkRemind = {
                WorkRemindId: 0,              
                DepartmentId: 0,
                DepartmentData: { Display: '', Value: '' },
                Title: '',
                Description: '',
                RemindDate: '',
                RemindMode:''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormWorkRemindInfo)
                $scope.$parent.FormWorkRemindInfo.$setPristine();

        }

        $scope.CreateUpdate = function (data) {
           
            //else {
                //var objWorkRemind = {
                //    WorkRemindId: data.WorkRemindId,
                //    DepartmentId: DepartmentData.Value,
                //    DepartmentData: DepartmentData.Display,
                //    Title: data.Title,
                //    Description: data.Description,
                //    RemindDate: data.RemindDate,
                //    RemindMode: data.RemindMode,
                //}
                data.DepartmentId = data.DepartmentData.Value;
                data.Department = data.DepartmentData.Display;
                data.RemindTime = $filter('date')(data.RemindTime, "HH:mm")
                WorkReminderService.CreateUpdateWorkReminder(data).then(function (result) {
                    $uibModalInstance.close();
                    toastr.success(result.data.Message);
                    ResetForm();
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            //}
        }

        $scope.Update = function (data) {
           
            //else {
                var objWorkRemind = {
                    WorkRemindId: data.WorkRemindId,
                    DepartmentId: data.DepartmentId,
                    DepartmentData: { Display: data.DepartmentData, Value: data.DepartmentId },
                    Title: data.Title,
                    Description: data.Description,
                    RemindDate: data.RemindDate,
                    RemindTime:data.RemindTime,
                    RemindMode: data.RemindMode,
                }
                WorkReminderService.CreateUpdateWorkReminder(objWorkRemind).then(function (result) {
                    $uibModalInstance.close();
                    toastr.success(result.data.Message);
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            //}
        }

        $scope.Reset = function () {
            if ($scope.objWorkRemind.WorkRemindId > 0) {
                $scope.objWorkRemind = angular.copy($scope.storage);
            } else {
                ResetForm();
            }

        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'WorkReminderService', 'WorkReminderController', '$filter', 'objWorkRemind', 'Upload', 'isdisable']
    
})()