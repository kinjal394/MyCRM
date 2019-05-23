(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("TaskController", [
         "$scope", "TaskService", "$uibModal", "$filter",
         TaskController]);

    function TaskController($scope, TaskService, $uibModal, $filter) {

        var hdnuserrollid = false;
        $scope.id = 0;
        $scope.storage = {};
        $scope.Add = function (data, _isdisable) {
            var objtask = "";
            if (_isdisable === undefined) _isdisable = 0;
            if (data == 0) {
                objtask = {
                    TaskId: 0,
                    Task: '',
                    Priority: 0,
                    PriorityId: 0,
                    PriorityName: { Display: '', Value: '' },
                    Review: '',
                    Status: '',
                    StatusId: 0,
                    TaskStatus: { Display: '', Value: '' },
                    TaskType: '',
                    TaskTypeId: 0,
                    TaskTypeName: { Display: '', Value: '' },
                    TaskGroup: '',
                    TaskGroupId: 0,
                    TaskGroupData: { Display: '', Value: '' },
                    //GroupBy: '',
                    NextFollowTime: new Date(),
                    NextFollowDate: new Date(),
                    DeadlineDate: new Date(),
                    ToId: '',
                    Note: '',
                    CreatedBy: '',
                    FromUser: '',
                    hdnuserrollid: false
                };
            }
            else {
                objtask = data;

            }
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    TaskService: function () { return TaskService; },
                    TaskController: function () { return TaskController; },
                    objtask: function () { return objtask },
                    storage: function () { return objtask; },
                    isdisable: function () { return _isdisable; }
                }
            });
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            });
        }
        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };
        $scope.SetSessionObj = function (data) {
            if (data == 1) {
                hdnuserrollid = true;
            }

            $scope.mode = "TaskStatusMaster";
            $scope.gridObj = {
                columnsInfo: [
                   //{ "title": "TaskId", "data": "TaskId", filter: false, visible: false },
                   { "title": "Sr.", "field": "RowNumber", show: true, },
                   { "title": "Task No", "field": "TaskNo", sortable: "TaskNo", filter: { TaskNo: "text" }, show: true, },
                   { "title": "Assign From", "field": "FromUser", sortable: "FromUser", filter: { FromUser: "text" }, show: true, },
                   //{ "title": "CreatedBy", "data": "CreatedBy", filter: false, visible: false },
                   //{ "title": "Work/Task", "data": "Task", sort: true, filter: true },
                   {
                       "title": "Work/Task", "field": "Task", sortable: "Task", filter: { Task: "text" }, show: true,
                       'cellTemplte': function ($scope, row) {
                           var element = '<p data-uib-tooltip="{{row.Task}}" ng-bind="LimitString(row.Task,100)">'
                           return $scope.getHtml(element);
                       }
                   },
                   {
                       "title": "Time Duration", "field": "Duration", sortable: "Duration", filter: { followtime: "text" }, show: true,
                       'celltemplte': function ($scope, row) {
                           var element = '<span >{{converttime(row.Duration)}}</span>'
                           return $scope.gethtml(element);
                       }
                   },
                  
                    { "title": "Task Group", "field": "TaskGroupName", sortable: "TaskGroupName", filter: { TaskGroupName: "text" }, show: true, },
                   { "title": "Priority", "field": "PriorityName", sortable: "PriorityName", filter: { PriorityName: "text" }, show: true, },
                    {
                        "title": "Work add Date", "field": "CreatedDate", sortable: "CreatedDate", filter: { CreatedDate: "date" }, show: true,
                        'cellTemplte': function ($scope, row) {
                            var element = '<span >{{ConvertDate(row.CreatedDate,\'dd/mm/yyyy\')}}</span>'
                            return $scope.getHtml(element);
                        }
                    },
                   //{ "title": "Review", "field": "Review", sortable: "Review", filter: { Review: "text" }, show: false, },
                   //{ "title": "Status", "field": "TaskStatus", sortable: "TaskStatus", filter: { TaskStatus: "text" }, show: true, },

                   //dataType: "dropdown", mode: "TaskStatusMaster" 
                   //{ "title": "TaskType", "field": "TaskType", sortable: "TaskType", filter: { TaskType: "text" }, show: false, },
                   //{ "title": "Group By", "data": "GroupBy", sort: true, filter: true },
                   //{ "title": "To User", "data": "ToUser", sort: true, filter: true,visible: false },
                   //{ "title": "TaskGroupId", "data": "TaskGroupId", sort: true, filter: true, visible: false },

                   //{ "title": "Note/Remark", "data": "Note", sort: true, filter: true},
                   //{
                   //    "title": "Last Note/Remark", "field": "Note", sortable: "Note", filter: { Note: "text" }, show: true,
                   //    'cellTemplte': function ($scope, row) {
                   //        var element = '<span >{{ConvertDate(row.lastFollowdate,\'dd/mm/yyyy\') +"      "+ ConvertTime(row.lastFollowTime) + " " + row.Note }}</span>'
                   //        return $scope.getHtml(element);
                   //    }
                   //},
                   {
                       "title": " Last Follow Up", "field": "PlanDateTime", sortable: "PlanDateTime", filter: { NextFollowDate: "date" }, show: true,
                       'cellTemplte': function ($scope, row) {
                           var element = '<span >{{ConvertDate(row.PlanDateTime,\'dd/mm/yyyy\') +"      "+ ConvertTime(row.PlanTime)}}</span>';
                           return $scope.getHtml(element);
                       }
                   },
                   {
                       "title": "Last Note/Remark", "field": "Note", sortable: "Note", filter: { Note: "text" }, show: true,
                       'cellTemplte': function ($scope, row) {
                           var element = '<p data-uib-tooltip="{{row.Note}}" ng-bind="LimitString(row.Note,100)">'
                           return $scope.getHtml(element);
                       }
                   },
                   {
                       "title": "Next Follow Up", "field": "NextFollowDate", sortable: "NextFollowDate", filter: { NextFollowDate: "date" }, show: true,
                       'cellTemplte': function ($scope, row) {
                           var element = '<span >{{ConvertDate(row.NextFollowDate,\'dd/mm/yyyy\') +"      "+ ConvertTime(row.NextFollowTime)}}</span>'
                           return $scope.getHtml(element);
                       }
                   },
                   {
                       "title": "FollowUp Created Date", "field": "FollowCreatedDate", sortable: "FollowCreatedDate", filter: { FollowCreatedDate: "date" }, show: false,
                       'cellTemplte': function ($scope, row) {
                           var element = '<span >{{ConvertDate(row.FollowCreatedDate,\'dd/mm/yyyy\') +"      "+ ConvertTime(row.FollowCreatedTime)}}</span>'
                           return $scope.getHtml(element);
                       }
                   },
                   //{
                   //    "title": " Actual Follow Up", "field": "ActualDate", sortable: "ActualDate", filter: { NextFollowDate: "date" }, show: true,
                   //    'cellTemplte': function ($scope, row) {
                   //        var element = '<span >{{ConvertDate(row.ActualDate,\'dd/mm/yyyy\') +"      "+ ConvertTime(row.ActualTime)}}</span>';
                   //        return $scope.getHtml(element);
                   //    }
                   //},

                    //{
                    //    "title": "Current Follow Up", "field": "lastFollowdate", sortable: "lastFollowdate", filter: { lastFollowdate: "date" }, show: true,
                    //    'cellTemplte': function ($scope, row) {
                    //        var element = '<span >{{ConvertDate(row.lastFollowdate,\'dd/mm/yyyy\') +"      "+ ConvertTime(row.lastFollowTime)}}</span>'
                    //        return $scope.getHtml(element);
                    //    }
                    //},
                      {
                          "title": "Dead line Date", "field": "DeadlineDate", sortable: "DeadlineDate", filter: { DeadlineDate: "date" }, show: true,
                          'cellTemplte': function ($scope, row) {
                              var element = '<span >{{ConvertDate(row.DeadlineDate,\'dd/mm/yyyy\') }}</span>'
                              return $scope.getHtml(element);
                          }
                      },
                    {
                        "title": "FollowUp Date", "field": "FollowDate", sortable: "FollowDate", filter: { FollowDate: "date" }, show: false,
                        'cellTemplte': function ($scope, row) {
                            var element = '<span >{{ConvertDate(row.FollowDate,\'dd/mm/yyyy\')}}</span>'
                            return $scope.getHtml(element);
                        }
                    },
                     


                   //in use visible bcs its visiblity change as per users role ---RJ 14-02-2017
                   
                   { "title": "Follow From User", "field": "FollowFromUser", sortable: "FollowFromUser", filter: { FollowFromUser: "text" }, visible: false, },
                   { "title": "Assign To", "field": "AssignTo", sortable: "AssignTo", filter: { AssignTo: "text" }, visible: hdnuserrollid, },
                   { "title": "Current Status", "field": "FollowUpTaskStatus", sortable: "FollowUpTaskStatus", filter: { FollowUpTaskStatus: "text" }, show: true, },
                   //{
                   //    "title": "Follow Time", "data": "FollowTime", sort: true, filter: true,
                   //    "render": '<p ng-bind="ConvertTime(row.FollowTime)">'
                   //},
                   {
                       "title": "Action", show: true,
                       'cellTemplte': function ($scope, row) {
                           var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.TaskId,row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                                 //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.TaskId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                                 '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.TaskId,row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>' +
                                   '<a class="btn btn-info btn-xs" data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.ShowFollowupDetail(row.TaskId,row.FollowStatus,row)"  data-uib-tooltip="Followup"><i class="fa fa-tasks"></i></a> '
                           return $scope.getHtml(element);
                       }
                   }
                ],
                Sort: { 'TaskId': 'asc' }
            }
        }

        $scope.ShowFollowupDetail = function (id, followStatus, maindata) {
            
            var modalInstance = $uibModal.open({
                templateUrl: 'myModalTaskFollowUpList.html',
                controller: TaskFollowUpListModalInstanceCtrl,
                size: 'lg',
                resolve: {
                    TaskController: function () { return TaskController; },
                    TaskService: function () { return TaskService; },
                    id: function () { return id; },
                    followStatus: function () { return followStatus; },
                    MainData: function () { return maindata }
                }
            });
            modalInstance.result.then(function () {
                $scope.refreshGrid();
            }, function () {
            })
        }

        $scope.Edit = function (id, getdata) {
            TaskService.GetTaskInfoById(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    var data, setdate;
                    if (result.data.DataList == null) { data = getdata; data.TSTaskStatus = data.TaskStatus; } else { data = result.data.DataList; }
                    var d = new Date();
                    var gettime = '';
                    var mytime = d;
                    if (data.FollowTime != null) {
                        d.setHours($filter('date')(data.FollowTime, "HH:mm").Hours);
                        d.setMinutes($filter('date')(data.FollowTime, "HH:mm").Minutes);
                        gettime = data.FollowTime.Hours == 0 && data.FollowTime.Minutes == 0 ? new Date() : mytime;

                    }
                    if (data.FollowDate != null) {
                        if ($filter('mydate')(data.FollowDate) != null) {
                            setdate = $filter('mydate')(data.FollowDate);
                        } else {
                            setdate = d;
                        }
                    }
                    var objtempinq = {
                        TaskId: data.TaskId,
                        Task: data.Task,
                        FollowTime: gettime,
                        FollowDate: moment(data.FollowDate).format("DD-MM-YYYY") == "01-01-0001" ? new Date() : setdate,
                        Duration:data.Duration,
                        Priority: data.Priority,
                        PriorityName: { Display: data.PriorityName, Value: data.Priority },
                        Review: data.Review,
                        TaskType: data.TaskType,
                        TaskTypeId: data.TaskTypeId,
                        TaskTypeName: { Display: data.TaskType, Value: data.TaskTypeId },
                        TaskGroup: data.TaskGroupName,
                        TaskGroupId: data.TaskGroupId,
                        TaskGroupData: { Display: data.TaskGroupName, Value: data.TaskGroupId },
                        GroupBy: data.GroupBy,
                        Status: data.Status,
                        TaskStatus: { Display: data.TSTaskStatus, Value: data.Status },
                        Note: data.Note,
                        ToId: data.ToId,
                        DeadlineDate: $filter('mydate')(data.DeadlineDate),
                        CreatedBy: data.CreatedBy,
                        FromUser: data.FromUser
                    }
                    objtempinq.frmdate = ConvertDate(data.FollowDate, 'dd/mm/yyyy')
                    if (data.FollowTime != null) {
                        objtempinq.frmtime = ConvertTime(data.FollowTime)
                    }
                    $scope.storage = angular.copy(objtempinq);
                    $scope.Add(objtempinq, 0);
                } else {
                    window.location.href = "/Transaction/Task";
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            })
        }

        $scope.View = function (id, getdata) {
            TaskService.GetTaskInfoById(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    var data, setdate;
                    if (result.data.DataList == null) { data = getdata; data.TSTaskStatus = data.TaskStatus; } else { data = result.data.DataList; }
                    var d = new Date();
                    var gettime = '';
                    var mytime = d;
                    if (data.FollowTime != null) {
                        d.setHours($filter('date')(data.FollowTime, "HH:mm").Hours);
                        d.setMinutes($filter('date')(data.FollowTime, "HH:mm").Minutes);
                        gettime = data.FollowTime.Hours == 0 && data.FollowTime.Minutes == 0 ? new Date() : mytime;

                    }
                    if (data.FollowDate != null) {
                        if ($filter('mydate')(data.FollowDate) != null) {
                            setdate = $filter('mydate')(data.FollowDate);
                        } else {
                            setdate = d;
                        }
                    }
                    var objtempinq = {
                        TaskId: data.TaskId,
                        Task: data.Task,
                        FollowTime: gettime,
                        FollowDate: moment(data.FollowDate).format("DD-MM-YYYY") == "01-01-0001" ? new Date() : setdate,
                        Duration: data.Duration,
                        Priority: data.Priority,
                        PriorityName: { Display: data.PriorityName, Value: data.Priority },
                        Review: data.Review,
                        TaskType: data.TaskType,
                        TaskTypeId: data.TaskTypeId,
                        TaskTypeName: { Display: data.TaskType, Value: data.TaskTypeId },
                        TaskGroup: data.TaskGroupName,
                        TaskGroupId: data.TaskGroupId,
                        TaskGroupData: { Display: data.TaskGroupName, Value: data.TaskGroupId },
                        GroupBy: data.GroupBy,
                        Status: data.Status,
                        TaskStatus: { Display: data.TSTaskStatus, Value: data.Status },
                        Note: data.Note,
                        ToId: data.ToId,
                        CreatedBy: data.CreatedBy,
                        FromUser: data.FromUser,
                        DeadlineDate: $filter('mydate')(data.DeadlineDate)
                    }
                    objtempinq.frmdate = ConvertDate(data.FollowDate, 'dd/mm/yyyy')
                    if (data.FollowTime != null) {
                        objtempinq.frmtime = ConvertTime(data.FollowTime)
                    }
                    $scope.storage = angular.copy(objtempinq);
                    $scope.Add(objtempinq, 1);
                } else {
                    window.location.href = "/Transaction/Task";
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            })
        }
        function ConvertDate(data, format) {
            if (data == null) return '';
            var r = /\/Date\(([0-9]+)\)\//gi
            var matches = data.match(r);
            if (matches == null) return '';
            var result = matches.toString().substring(6, 19);
            var epochMilliseconds = result.replace(
            /^\/Date\(([0-9]+)([+-][0-9]{4})?\)\/$/,
            '$1');
            var b = new Date(parseInt(epochMilliseconds));
            var c = new Date(b.toString());
            var curr_date = c.getDate();
            var curr_month = c.getMonth() + 1;
            var curr_year = c.getFullYear();
            var curr_h = c.getHours();
            var curr_m = c.getMinutes();
            var curr_s = c.getSeconds();
            var curr_offset = c.getTimezoneOffset() / 60
            //var d = curr_month.toString() + '/' + curr_date + '/' + curr_year;
            //return d;
            return format.replace('mm', curr_month).replace('dd', curr_date).replace('yyyy', curr_year);
        }
        function ConvertTime(Time) {
            // //Old code
            //var d = ('0' + Time.Hours).slice(-2) + ':' + ('0' + Time.Minutes).slice(-2) + ':' + ('0' + Time.Seconds).slice(-2);
            //return d;
            // //New code
            var d = ('0' + Time.Hours).slice(-2) + ':' + ('0' + Time.Minutes).slice(-2);
            if (d == '00:00') {
                return '';
            } else {
                return d;
            }
        }

        $scope.Delete = function (data) {

            TaskService.DeleteTask(data).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    $scope.refreshGrid()
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            })
        }

        $scope.IsActive = function (data) {
        }
    }

    var TaskFollowUpModalInstanceCtrl = function ($scope, $uibModalInstance, $filter, TaskController, TaskService, TaskFollowUpDetailsData, isdisable) {
        $scope.isclose = false;
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }

        $scope.objTaskFollowUp = {
            TaskDetailId: TaskFollowUpDetailsData.TaskDetailId,
            TaskId: TaskFollowUpDetailsData.TaskId,
            Note: TaskFollowUpDetailsData.Note,
            ActualDate: (moment(TaskFollowUpDetailsData.ActualDate).format("DD-MM-YYYY") == "01-01-0001" || (TaskFollowUpDetailsData.ActualDate) == "" || (TaskFollowUpDetailsData.ActualDate) == null) ? new Date() : $filter('mydate')(TaskFollowUpDetailsData.ActualDate),
            ActualTime: (TaskFollowUpDetailsData.ActualTime == null) ? new Date() : TaskFollowUpDetailsData.ActualTime,
            PlanDateTime: (TaskFollowUpDetailsData.TaskDetailId > 0) ? (TaskFollowUpDetailsData.PlanDateTime!=null?moment(TaskFollowUpDetailsData.PlanDateTime).format("DD-MM-YYYY HH:mm"):'') : TaskFollowUpDetailsData.PlanDateTime,
            NextFollowTime: (TaskFollowUpDetailsData.NextFollowTime == null) ? '' : TaskFollowUpDetailsData.NextFollowTime,
            NextFollowDate: (moment(TaskFollowUpDetailsData.NextFollowDate).format("DD-MM-YYYY") == "01-01-0001" || (TaskFollowUpDetailsData.NextFollowDate) == "" || (TaskFollowUpDetailsData.NextFollowDate) == null) ? '' : $filter('mydate')(TaskFollowUpDetailsData.NextFollowDate),
            Status: TaskFollowUpDetailsData.Status,
            ToId: TaskFollowUpDetailsData.ToId,
            TaskStatus: { Display: TaskFollowUpDetailsData.StatusName, Value: TaskFollowUpDetailsData.Status },
            outputReportingUser: []
        }

        $scope.ReportingUserArray = [];
        $scope.outputReportingUser = [];
        //TaskFollowUpDetailsData.AssignId;
        $scope.dateOptions = {
            formatYear: 'yy',
        //    minDate: new Date(2016, 8, 5),
            startingDay: 1
        };

        TaskService.ReportingUserBind(TaskFollowUpDetailsData.TaskId).then(function (result) {
            _.each(result.data.DataList, function (value, key, list) {
                if (value.UserId == TaskFollowUpDetailsData.ToId) {
                    $scope.ReportingUserArray.push({
                        name: value.UserId,
                        maker: value.Name,
                        ticked: true
                    })
                }
                else {
                    $scope.ReportingUserArray.push({
                        name: value.UserId,
                        maker: value.Name,
                        ticked: false
                    })
                }
            })
        });

        //$scope.objTaskFollowupDetails = TaskFollowUpDetailsData;
        // $scope.objTaskFollowupDetails.NextFollowDate = moment(TaskFollowUpDetailsData.NextFollowDate).format("DD-MM-YYYY") == "01-01-0001" ? new Date() : $filter('mydate')(TaskFollowUpDetailsData.NextFollowDate);
        var mytime = new Date(), mytimeA = new Date();
        var gettime = '', gettimeA = '';
        if (TaskFollowUpDetailsData.ActualTime != null) {
            mytimeA.setHours($filter('date')(TaskFollowUpDetailsData.ActualTime, "HH:mm").Hours);
            mytimeA.setMinutes($filter('date')(TaskFollowUpDetailsData.ActualTime, "HH:mm").Minutes);
            gettimeA = TaskFollowUpDetailsData.ActualTime.Hours == 0 && TaskFollowUpDetailsData.ActualTime.Minutes == 0 ? new Date() : mytimeA;
            $scope.objTaskFollowUp.ActualTime = gettimeA;
        }
        if (TaskFollowUpDetailsData.NextFollowTime != null) {
            mytime.setHours($filter('date')(TaskFollowUpDetailsData.NextFollowTime, "HH:mm").Hours);
            mytime.setMinutes($filter('date')(TaskFollowUpDetailsData.NextFollowTime, "HH:mm").Minutes);
            gettime = TaskFollowUpDetailsData.NextFollowTime.Hours == 0 && TaskFollowUpDetailsData.NextFollowTime.Minutes == 0 ? new Date() : mytime;
            $scope.objTaskFollowUp.NextFollowTime = gettime;
        }
        $scope.close = function () {
            $scope.isclose = true;
            $uibModalInstance.close($scope.isclose);
        };

        $scope.CreateTaskFollowUp = function () {
            var dateString = $scope.objTaskFollowUp.PlanDateTime,
            dateTimeParts = dateString.split(' '),
            timeParts = dateTimeParts[1].split(':'),
            dateParts = dateTimeParts[0].split('-'),
            date;

            date = new Date(dateParts[2], parseInt(dateParts[1], 10) - 1, dateParts[0], timeParts[0], timeParts[1]);

            $scope.objTaskFollowUp.PlanDateTime = date;
            $scope.objTaskFollowUp.ActualTime = $filter('date')($scope.objTaskFollowUp.ActualTime, "HH:mm");
            $scope.objTaskFollowUp.NextFollowTime = $filter('date')($scope.objTaskFollowUp.NextFollowTime, "HH:mm");
            $scope.objTaskFollowUp.ToId = $scope.objTaskFollowUp.outputReportingUser[0].name;
            $scope.objTaskFollowUp.TaskStatus = $scope.objTaskFollowUp.TaskStatus.Value;
            var aid = $scope.objTaskFollowUp.AssignId;
            TaskService.SaveTaskFollowUp($scope.objTaskFollowUp).then(function (result)  {
                toastr.success(result.data.Message);
                $uibModalInstance.close($scope.objTaskFollowUp.ToId);
            })
        }
    }
    TaskFollowUpModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', '$filter', 'TaskController', 'TaskService', 'TaskFollowUpDetailsData', 'isdisable']


    var TaskFollowUpListModalInstanceCtrl = function ($scope, $uibModalInstance, $filter, $uibModal, TaskController, TaskService, id, followStatus, MainData) {
        //        FollowupId,InqId,CurrentUpdate,NextFollowDate,NextFollowTime,TaskStatus AS StatusName,
        //Status, A.CreatedBy, C.Name As UserName , AssignId, D.Name As AssignUserName ,A.IsActive
        $scope.setclause = function (userid) {
            $scope.FixClause = 'TaskId = ' + id;
            $scope.setStatus = (followStatus == 'true') ? true : false;
            $scope.userid = userid;
            $scope.MainDetails = MainData;
            $scope.MainDetails.CreatedDate = moment($scope.MainDetails.CreatedDate).format("DD/MM/YYYY");
            $scope.MainDetails.DeadlineDate = (moment($scope.MainDetails.DeadlineDate).format("DD/MM/YYYY") == "01/01/1900") ? "" : moment($scope.MainDetails.DeadlineDate).format("DD/MM/YYYY");

            $scope.gridFollowupObj = {
                columnsInfo: [
                   //{ "title": "TaskDetail Id","field": "TaskDetailId", sortable: "TaskDetailId", filter: { TaskDetailId: "text" }, show: false },
                   { "title": "Sr.", "field": "RowNumber", filter: false, show: true },
                   //{ "title": "Task Id", "field": "TaskId", sortable: "TaskId", filter: { TaskId: "text" }, show: false },
                   {
                       "title": "Last Follow", "field": "PlanDateTime", show: true,
                       'cellTemplte': function ($scope, row) {
                           var element = '<span >{{ConvertDate(row.PlanDateTime,\'dd/mm/yyyy\') +"      "+ ConvertTime(row.PlanTime)}}</span>';
                           return $scope.getHtml(element);
                       }
                   },
                   { "title": "Last Note/Remark", "field": "Note", sortable: "Note", filter: { Note: "text" }, show: true },
                   {
                       "title": "Next Follow", "field": "NextFollowDate", show: true,
                       'cellTemplte': function ($scope, row) {
                           var element = '<span >{{ConvertDate(row.NextFollowDate,\'dd/mm/yyyy\') +"      "+ ConvertTime(row.NextFollowTime)}}</span>';
                           return $scope.getHtml(element);
                       }
                   },
                   {
                       "title": "Follow Created Date & Time", "field": "NextFollowDate", show: false,
                       'cellTemplte': function ($scope, row) {
                           var element = '<span >{{ConvertDate(row.CreatedDate,\'dd/mm/yyyy\') +"      "+ ConvertTime(row.CreatedTime)}}</span>';
                           return $scope.getHtml(element);
                       }
                   },
                   //{
                   //    "title": " Actual Follow Date-Time", "field": "ActualDate", show: true,
                   //    'cellTemplte': function ($scope, row) {
                   //        var element = '<span >{{ConvertDate(row.ActualDate,\'dd/mm/yyyy\') +"      "+ ConvertTime(row.ActualTime)}}</span>';
                   //        return $scope.getHtml(element);
                   //    }
                   //},

                   //
                   { "title": "Current Status", "field": "StatusName", sortable: "StatusName", filter: { StatusName: "text" }, show: true },
                   //{ "title": "UserID", "field": "FromId", sortable: "FromId", filter: { FromId: "text" }, show: true },
                   { "title": "Assign From", "field": "UserName", sortable: "UserName", filter: { UserName: "text" }, show: true },
                   //{ "title": "AssignUserID", "field": "ToId", sortable: "ToId", filter: { ToId: "text" }, show: true },
                   { "title": "Assign To", "field": "AssignUserName", sortable: "AssignUserName", filter: { AssignUserName: "text" }, show: true },
                   {
                       "title": "Action", show: true,
                       'cellTemplte': function ($scope, row) {
                           var element = '<a class="btn btn-primary btn-xs" ng-show="row.FromId == $parent.$parent.$parent.$parent.$parent.$parent.userid"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.EditFollowup(row.TaskDetailId,0)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                             '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.EditFollowup(row.TaskDetailId,1)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>';
                           return $scope.getHtml(element);
                       }
                   }
                ],
                Sort: { 'TaskDetailId': 'desc' }
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };
        $scope.setdate = function () {
            if ($scope.NextFollowDate == '') {
                $scope.NextFollowDate = '31-05-2017';
            }
        }

        $scope.AddFollowup = function () {
            var isdisable = 0
            TaskService.GetTaskFollowUp(id).then(function (result) {
                var objTaskFollowUp = {
                    TaskDetailId: 0,
                    TaskId: result.data.TaskId,
                    Note: '',
                    ActualTime: null,
                    ActualDate: '',
                    PlanDateTime: moment(result.data.NextFollowDate).format("DD-MM-YYYY") + ' ' + moment(result.data.NextFollowTime).format("HH:mm"),
                    NextFollowTime: null,
                    NextFollowDate: '',
                    Status: result.data.Status,
                    StatusName: result.data.StatusName,
                    ToId: result.data.ToId,
                    TaskStatus: { Display: result.data.StatusName, Value: result.data.Status },
                    outputReportingUser: []
                }
                var modalInstance = $uibModal.open({
                    templateUrl: 'myModalTaskFollowUp.html',
                    controller: TaskFollowUpModalInstanceCtrl,
                    resolve: {
                        TaskController: function () { return $scope; },
                        TaskService: function () { return TaskService; },
                        TaskFollowUpDetailsData: function () { return objTaskFollowUp; },
                        isdisable: function () { return isdisable; }
                    }
                });
                modalInstance.result.then(function (toid) {
                    if (result.data.FromId != toid) {
                        $scope.setStatus = false;
                    }
                    $scope.refreshGrid();
                }, function () {
                })
            })
        }

        $scope.EditFollowup = function (id, isdisable) {
            TaskService.GetTaskFollowUpByID(id).then(function (result) {
                var modalInstance = $uibModal.open({
                    templateUrl: 'myModalTaskFollowUp.html',
                    controller: TaskFollowUpModalInstanceCtrl,
                    resolve: {
                        TaskController: function () { return $scope; },
                        TaskService: function () { return TaskService; },
                        TaskFollowUpDetailsData: function () { return result.data; },
                        isdisable: function () { return isdisable; }
                    }
                });
                modalInstance.result.then(function (toid) {
                    if (result.data.FromId != toid) {
                        $scope.setStatus = false;
                    }
                    $scope.refreshGrid();
                }, function () {
                })
            })
        }

        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };

    }
    TaskFollowUpListModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', '$filter', '$uibModal', 'TaskController', 'TaskService', 'id', 'followStatus', 'MainData']

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, TaskService, TaskController, objtask, storage, $filter, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.objtask = $scope.objtask || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.store = {};

        if (objtask.TaskId > 0) {
            $scope.objtask = objtask;
            $scope.store = storage;
        } else {
            ResetForm();
        }
        $scope.$watch("objtask.PriorityName", function (newValue, oldValue) {
            if (newValue.Value == '' || newValue.Value == null) {
                $scope.objtask.PriorityName = { Display: 'Normal', Value: '3' }
            }
        });
        Reporting();
        function Reporting() {
            TaskService.ReportingUserBind($scope.objtask.TaskId).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objtask.ReportingUserArray = [];
                    var Mainres, data = '', res = '', AssignMsg = '', AssigneIds = '';
                    if ($scope.objtask.ToId != undefined) {
                        Mainres = $scope.objtask.ToId.split(",");
                        var total = parseInt(Mainres.length);
                        for (var i = 0; i < total; i++) {
                            var getval = Mainres[i].split('|');
                            res += getval[0] + ',';
                            data += getval[1] + ',';
                            if (parseInt(getval[2]) == 1) {
                                AssignMsg += getval[1] + ',';
                                AssigneIds += getval[0] + ',';
                            }
                        }
                        res = res.slice(0, res.length - 1);
                        AssigneIds = AssigneIds.slice(0, AssigneIds.length - 1);
                        if (AssignMsg.length > 0) {
                            AssignMsg = "You can not remove " + AssignMsg.slice(0, AssignMsg.length - 1) + ". because they assign this task to others.";
                        }
                        if (objtask != 0) {
                            objtask.AssignMsg = AssignMsg;
                            objtask.reportlist = data;
                        }
                    }
                    _.each(result.data.DataList, function (value, key, list) {
                        if (res != undefined && res.includes(value.UserId.toString())) {
                            $scope.objtask.ReportingUserArray.push({
                                name: value.UserId,
                                maker: value.Name,
                                ticked: true,
                                disabled: (AssigneIds != undefined && AssigneIds.includes(value.UserId.toString())) ? true : false
                            })

                        }
                        else {
                            $scope.objtask.ReportingUserArray.push({
                                name: value.UserId,
                                maker: value.Name,
                                ticked: false,
                                disabled: (AssigneIds != undefined && AssigneIds.includes(value.UserId.toString())) ? true : false
                            })
                        }
                    })
                }
                else {
                    toastr.error(result.data.Message)
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }
        //$scope.ReportingUse = function () {
        //    TaskService.ReportingUserBind().then(function (result) {
        //        if (result.data.ResponseType == 1) {
        //        }
        //        else {
        //            toastr.error(result.data.Message)
        //        }
        //    }, function (errorMsg) {
        //        toastr.error(errorMsg, 'Opps, Something went wrong');
        //    })
        //}

        function ResetForm() {
            $scope.objtask = {
                TaskId: 0,
                //AssignTo: 0,
                Task: '',
                NextFollowTime: new Date(),
                NextFollowDate: new Date(),
                Duration: '',
                DeadlineDate: '',
                Priority: 0,
                PriorityId: 0,
                PriorityName: { Display: '', Value: '' },
                Status: 0,
                StatusId: 0,
                TaskStatus: { Display: '', Value: '' },
                Note: '',
                Review: '',
                TaskType: '',
                TaskTypeId: 0,
                TaskTypeName: { Display: '', Value: '' },
                TaskGroup: '',
                TaskGroupId: '',
                TaskGroupData: { Display: '', Value: '' },
                GroupBy: '',
                ToId: '',
                CreatedBy: '',
                FromUser: ''
            }
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormTaskInfo)
                $scope.$parent.FormTaskInfo.$setPristine();

        }

        $scope.Create = function (data) {
            if (data.NextFollowDate == null || data.NextFollowDate == '') {
                data.NextFollowDate = new Date();
            }
            if (data.NextFollowTime == null || data.NextFollowTime == '') {
                data.NextFollowTime = $filter('date')(data.NextFollowTime, "HH:mm");
            }
            var objinstask = {
                TaskId: data.TaskId,
                //AssignTo: data.Name.Value,
                //Name: { Display: data.Name.Display, Value: data.Name.Value },
                Task: data.Task,
                //FollowTime: $filter('date')(data.FollowTime, "hh:mm"),
                NextFollowDate: data.NextFollowDate,
                NextFollowTime: data.NextFollowTime,
                Duration: data.Duration,
                GroupBy: data.GroupBy,
                TaskGroupId: data.TaskGroupData.Value,
                TaskGroup: data.TaskGroupData.Display,
                Priority: data.PriorityName.Value,
                PriorityName: { Display: data.PriorityName.Display, Value: data.PriorityName.Value },
                DeadlineDate: data.DeadlineDate
                //Status: data.TaskStatus.Value,
                //TaskStatus: { Display: data.TaskStatus.Display, Value: data.TaskStatus.Value },
                //Note: data.Note
            }

            TaskService.CreateUpdateTask(objinstask).then(function (result) {
                toastr.success(result.data.Message);
                $uibModalInstance.close();
                //ResetForm();
            })
        }

        $scope.Update = function (data) {
            var ToIdList = '';
            //var ToLength = $scope.objtask.outputReportingUser.length;
            //_.each($scope.objtask.outputReportingUser, function (value, key, list) {
            //    if (key < ToLength - 1) {
            //        ToIdList += value.name + ','
            //    }
            //    else if (key == ToLength - 1) {
            //        ToIdList += value.name;
            //    }

            //})
            var d = new Date();
            if (data.FollowDate == null || data.FollowDate == '') {
                data.FollowDate = new Date();
            }
            var objinstask = {
                TaskId: data.TaskId,
                //AssignTo: data.Name.Value,
                //Name: { Display: data.Name.Display, Value: data.Name.Value },
                Task: data.Task,
                //FollowTime: $filter('date')(data.FollowTime, "HH:mm"),
                NextFollowTime: new Date(),
                NextFollowDate: data.FollowDate,
                Duration: data.Duration,
                Priority: data.PriorityName.Value,
                PriorityName: { Display: data.PriorityName.Display, Value: data.PriorityName.Value },
                Status: data.TaskStatus.Value,
                TaskStatus: { Display: data.TaskStatus.Display, Value: data.TaskStatus.Value },
                Note: data.Note,
                Review: data.Review,
                TaskType: data.TaskTypeName.Display,
                TaskTypeId: data.TaskTypeName.Value,
                TaskTypeName: { Display: data.TaskTypeName.Display, Value: data.TaskTypeName.Value },
                TaskGroupId: data.TaskGroupData.Value,
                TaskGroup: data.TaskGroupData.Display,
                TaskGroupData: { Display: data.TaskGroupData.Display, Value: data.TaskGroupData.Value },
                GroupBy: data.GroupBy,
                DeadlineDate: data.DeadlineDate,
                ToId: ToIdList
            }
            TaskService.CreateUpdateTask(objinstask).then(function (result) {
                toastr.success(result.data.Message);
                $uibModalInstance.close();
            })
        }

        $scope.Reset = function () {
            if ($scope.objtask.TaskId > 0) {
                Reporting();
                $scope.objtask = angular.copy($scope.store);
            } else {
                ResetForm();
            }

        }

        $scope.dateOptions = {
            formatYear: 'yy',
           // minDate: new Date(2016, 5, 22),
            //maxDate: new Date(2017, 5, 22),
            startingDay: 1
        };

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'TaskService', 'TaskController', 'objtask', 'storage', '$filter', 'isdisable']

})()
