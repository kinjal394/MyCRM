(function () {
    "use strict";
    debugger;
    angular.module("CRMApp.Controllers")
           .controller("DailyReportingCtrl", [
               "$scope", "DailyReportingService", "$uibModal", "$filter",
               DailyReportingCtrl]);

    function DailyReportingCtrl($scope, DailyReportingService, $uibModal, $filter) {
        $scope.storage = {};
        $scope.id = 0;

        $scope.objDailyReporting = {
            DailyWorkId: 0,
            UserId: 0,
            Date: new Date(),
            TaskInqNo: '',
            TaskInqId: '',
            TaskType: 0,
            Remark: '',
            Persontage: 0,
            StartTime: new Date(),
            EndTime: new Date(),
            TotalTime: '',
            Title: '',
            Description: '',
            StatusId: 0,
            TaskStatusData: { Display: '', Value: 0 }
        };


        $scope.Add = function () {
            window.location.href = "/Master/DailyReporting/DailyReport";
        }



        $scope.SetDailyWorkId = function (id, isdisable) {
            if (id > 0) {
                $scope.SrNo = id;
                $scope.addMode = false;
                $scope.saveText = "Update";
                $scope.isClicked = false;
                if (isdisable == 1) {
                    $scope.isClicked = true;
                }
            } else {
                $scope.SrNo = 0;
                $scope.addMode = true;
                $scope.isClicked = false;
                $scope.saveText = "Save";
            }
        }

        $scope.GetDailyWorkById = function (id) {
            BuyerService.GetDailyWorkById(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    var objDailyReporting = result.data.DataList.objDailyReporting;
                    $scope.webaddress = (objDailyReporting.WebAddress != '' && objDailyReporting.WebAddress != null) ? objDailyReporting.WebAddress.split(",") : [];
                    $scope.emailid = (objDailyReporting.Email != '' && objDailyReporting.Email != null) ? objDailyReporting.Email.split(",") : [];
                    $scope.cmpnyteliphone = (objDailyReporting.Telephone != '' && objDailyReporting.Telephone != null) ? objDailyReporting.Telephone.split(",") : [];
                    $scope.objBuyer = {
                        BuyerId: objDailyReporting.BuyerId,
                        CompanyName: objDailyReporting.CompanyName,
                        Email: $scope.emailid.toString(),
                        WebAddress: $scope.webaddress.toString(),
                        Remark: objDailyReporting.Remark
                    };
                    $scope.storage = angular.copy($scope.objDailyReporting);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.Add = function (data, _isdisable) {
            if (!_isdisable) { _isdisable = 0 };
            if (data == 0) {
                $scope.objDailyReporting = {
                    DailyWorkId: 0,
                    UserId: 0,
                    Date: new Date(),
                    TaskInqNo: '',
                    TaskInqId: '',
                    TaskType: 0,
                    Remark: '',
                    Persontage: 0,
                    StartTime: new Date(),
                    EndTime: new Date(),
                    TotalTime: '',
                    Title: '',
                    Description: '',
                    StatusId: 0,
                    TaskStatusData: { Display: '', Value: 0 }
                };
            }
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    DailyReportingService: function () { return DailyReportingService; },
                    DailyReportingCtrl: function () { return DailyReportingCtrl; },
                    objDailyReporting: function () { return $scope.objDailyReporting; },
                    isdisable: function () { return _isdisable; }
                }
                //storage: function () {
                //    return $scope.storage;
                //}
            });
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            });
            //, function (errorMsg) {
            //    toastr.error(errorMsg, 'Opps, Something went wrong');
            //});
        }

        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };

        $scope.gridObj = {
            //DailyWorkId, UserId,,UserName Date, TaskInqId, TaskType,TypeName, Remark, Persontage, StartTime, EndTime, Title, Description, StatusId,StatusName
            columnsInfo: [
               //{ "title": "DailyWorkId", "data": "DailyWorkId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "User", "field": "Name", sortable: "Name", filter: { Name: "text" }, show: true, },
               {
                   "title": "Date", "field": "Date", sortable: "Date", filter: { Date: "date" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element =  '<span >{{ConvertDate(row.Date,\'dd/mm/yyyy\')}}</span>'
                       return $scope.getHtml(element);
                   }
               },
               { "title": "Task", "field": "TaskType", sortable: "TaskType", filter: { TaskType: "text" }, show: true, },
               { "title": "Title", "field": "Title", sortable: "Title", filter: { Title: "text" }, show: true, },
               { "title": "Description", "field": "Description", sortable: "Description", filter: { Description: "text" }, show: true, },
               { "title": "Persontage", "field": "Persontage", sortable: "Persontage", filter: { Persontage: "text" }, show: true, },
               { "title": "Status", "field": "TaskStatus", sortable: "TaskStatus", filter: { TaskStatus: "text" }, show: true, },
               {
                   "title": "Start Time", "field": "StartTime", sortable: "StartTime", filter: { StartTime: "text" }, show: false,
                   //'cellTemplte': function ($scope, row) {
                   //    var element = '<span >{{ConvertTime(row.StartTime)}}</span>'
                   //    return $scope.getHtml(element);
                   //}
               }, {
                   "title": "End Time", "field": "EndTime", sortable: "EndTime", filter: { EndTime: "text" }, show: false,
                   //'cellTemplte': function ($scope, row) {
                   //    var element = '<span >{{ConvertTime(row.EndTime)}}</span>'
                   //    return $scope.getHtml(element);
                   //}
               },
               //{ "title": "Remark", "field": "Remark", sortable: "Remark", filter: { Remark: "text" }, show: true, },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.DailyWorkId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                           //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.DailyWorkId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                            '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.DailyWorkId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'DailyReportingId': 'asc' }

        }

        $scope.Edit = function (id) {
            $scope.addMode = false;
            $scope.getDailyTaskData(id, 'edit');
        }
        $scope.View = function (id) {
            $scope.getDailyTaskData(id, 'view');
        }
        $scope.Delete = function (id) {
            DailyReportingService.DeleteDailyReporting(id).then(function (result) {
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
        $scope.getDailyTaskData = function (id, mode) {
            var d = new Date();
            var de = new Date();
            var gettime = '';
            var getendtime = '';
            var mytime = d;
            var myendtime = de;
            DailyReportingService.GetDailyWorkReportingByID(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    var objDailyReporting = result.data.DataList;
                    if (objDailyReporting.StartTime != null) {
                        d.setHours($filter('date')(objDailyReporting.StartTime, "HH:mm").Hours);
                        d.setMinutes($filter('date')(objDailyReporting.StartTime, "HH:mm").Minutes);
                        gettime = objDailyReporting.StartTime.Hours == 0 && objDailyReporting.StartTime.Minutes == 0 ? new Date() : mytime;

                    }
                    if (objDailyReporting.EndTime != null) {
                        de.setHours($filter('date')(objDailyReporting.EndTime, "HH:mm").Hours);
                        de.setMinutes($filter('date')(objDailyReporting.EndTime, "HH:mm").Minutes);
                        getendtime = objDailyReporting.EndTime.Hours == 0 && objDailyReporting.EndTime.Minutes == 0 ? new Date() : myendtime;

                    }
                    $scope.objDailyReporting = {
                        DailyWorkId: objDailyReporting.DailyWorkId,
                        UserId: objDailyReporting.UserId,
                        Date: $filter('mydate')(objDailyReporting.Date),
                        TaskInqId: objDailyReporting.TaskInqId,
                        TaskType: objDailyReporting.TaskType,
                        TaskInqNo: objDailyReporting.TaskInqNo,
                        Remark: objDailyReporting.Remark,
                        Persontage: objDailyReporting.Persontage,
                        StartTime: gettime,
                        EndTime: getendtime,
                        Title: objDailyReporting.Title,
                        Description: objDailyReporting.Description,
                        StatusId: objDailyReporting.StatusId,
                        StatusName: objDailyReporting.TaskStatus,
                        TaskStatusData: { Display: objDailyReporting.StatusName, Value: objDailyReporting.StatusId }
                    }
                    if (mode == 'edit') {
                        $scope.Add(id, 0);
                    } else if (mode == 'view') {
                        $scope.Add(id, 1);
                    }
                } else {
                    toastr.error(result.data.Message);
                }

            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

    }


    var ModalInstanceCtrl = function ($scope, $uibModalInstance, DailyReportingService, DailyReportingCtrl, objDailyReporting, isdisable, $filter) {

        $scope.settime = function () {
            var duration = moment.duration(moment($scope.objDailyReporting.EndTime).diff(moment($scope.objDailyReporting.StartTime)));
            $scope.objDailyReporting.TotalTime = moment.utc(duration._milliseconds).format('HH:mm:ss');
        }

        $scope.$watch("objDailyReporting.EndTime", function (newValue, oldValue) {
            var startTime = $filter('date')($scope.objDailyReporting.StartTime, "HH:mm") ;
            var endTime = $filter('date')($scope.objDailyReporting.EndTime, "HH:mm");
            //var rsl = TimeSpan.Compare(startTime, endTime);
            if (startTime > endTime) {
                toastr.error('EndTime smaller than StartTime. Plase Change EndTime.');
                $scope.chkbtn = false;
            } else {
                $scope.chkbtn = true;
            }

            if (newValue) { $scope.settime(); }
        });
        $scope.$watch("objDailyReporting.StartTime", function (newValue, oldValue) { if (newValue) { $scope.settime(); } });
        $scope.newValue = function (data) {
            if ($scope.objDailyReporting.TaskInqNo != '' || $scope.objDailyReporting.TaskInqNo != null) {
                $scope.objDailyReporting.TaskInqNo = '';
            }
            if ($scope.objDailyReporting.Title != '' || $scope.objDailyReporting.Title != null) {
                $scope.objDailyReporting.Title = '';
            }
            if ($scope.objDailyReporting.Description != '' || $scope.objDailyReporting.Description != null) {
                $scope.objDailyReporting.Description = '';
            }
        }
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.dateOptions = {
            formatYear: 'yy',
            minDate: new Date(1950, 1, 1),
            startingDay: 1
        }
        $scope.objdepart = $scope.objdepart || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};

        var duration = moment.duration(moment(objDailyReporting.EndTime).diff(moment(objDailyReporting.StartTime)));
        if (objDailyReporting.DailyWorkId > 0) {
            $scope.addMode = false;
            $scope.objDailyReporting = {
                DailyWorkId: objDailyReporting.DailyWorkId,
                UserId: objDailyReporting.UserId,
                Date: objDailyReporting.Date,
                TaskInqId: objDailyReporting.TaskInqId,
                TaskInqNo: objDailyReporting.TaskInqNo,
                TaskType: objDailyReporting.TaskType,
                Remark: objDailyReporting.Remark,
                Persontage: objDailyReporting.Persontage,
                StartTime: objDailyReporting.StartTime,
                EndTime: objDailyReporting.EndTime,
                TotalTime: moment.utc(duration._milliseconds).format('HH:mm:ss'),
                TotalTime: objDailyReporting.TotalTime,
                Title: objDailyReporting.Title,
                Description: objDailyReporting.Description,
                StatusId: objDailyReporting.StatusId,
                TaskStatusData: { Display: objDailyReporting.StatusName, Value: objDailyReporting.StatusId }
            };
        } else {
            ResetForm();
        }
        $scope.close = function () {
            $uibModalInstance.close();
        };

        $scope.CreateUpdate = function (data) {
            if ($scope.chkbtn == true) {
                data.StatusId = data.TaskStatusData.Value;
                data.StatusName = data.TaskStatusData.Display;
                data.StartTime = data.StartTime == "" ? " " : $filter('date')(data.StartTime, "HH:mm")
                data.EndTime = data.EndTime == "" ? " " : $filter('date')(data.EndTime, "HH:mm")
                DailyReportingService.CreateUpdateDailyReporting(data).then(function (result) {
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
                $scope.close();
            } else {
                toastr.error('EndTime smaller than StartTime. Plase Change EndTime.');
            }
        }

        $scope.GetTaskInqData = function (val, id) {
            DailyReportingService.GetTaskInqData(val, id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objDailyReporting.TaskInqId = result.data.DataList.TaskInqId;
                    $scope.objDailyReporting.Title = result.data.DataList.Title;
                    $scope.objDailyReporting.Description = result.data.DataList.Description;
                } else {
                    $scope.objDailyReporting.Title = '';
                    $scope.objDailyReporting.Description = '';
                    toastr.error(result.data.Message);
                }

            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }


        function ResetForm() {
            $scope.objDailyReporting = {
                DailyWorkId: 0,
                UserId: 0,
                Date: new Date(),
                TaskInqNo: '',
                TaskInqId: 0,
                TaskType: 0,
                Remark: '',
                Persontage: 0,
                StartTime: new Date(),
                EndTime: new Date(),
                TotalTime: '',
                Title: '',
                Description: '',
                StatusId: 0,
                TaskStatusData: { Display: '', Value: 0 }
            };
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormDepartInfo)
                $scope.$parent.FormDepartInfo.$setPristine();
        }
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'DailyReportingService', 'DailyReportingCtrl', 'objDailyReporting', 'isdisable', '$filter']
})()

