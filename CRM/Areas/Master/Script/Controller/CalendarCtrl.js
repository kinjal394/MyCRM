(function () {

    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("CalendarCtrl", [
            "$scope", "$compile", "$filter", "$uibModal", "TaskService", "InquiryService", "$rootScope",
            CalendarCtrl]);

    function CalendarCtrl($scope, $compile, $filter, $uibModal, TaskService, InquiryService, $rootScope) {
        $scope.objTaskArray = [];
        $scope.objInquiryArray = [];
        $scope.object = [];
        var date = new Date();
        var d = date.getDate();
        var m = date.getMonth();
        var y = date.getFullYear();
        var typeOfMaster = {
            Task: 1,
            Event: 2,
            Holiday: 3,
            Leave: 4,
            BirthDay: 5
        }
        $scope.dateOptionsFilter = {
            formatYear: 'yy',
            //minDate: new Date(2016, 8, 5),
            //maxDate: new Date(2017, 5, 22),
            startingDay: 1
        };
        $scope.abcd = function () {
            var date = new Date();
            calendar.fullCalendar('changeView', 'Week');
        }
        $scope.id = 0;

        $scope.AddTask = function () {
            var objtask = {
                TaskId: 0,
                AssignTo: 0,
                Task: '',
                FollowTime: new Date(),
                FollowDate: '',
                Priority: 0,
                PriorityId: 0,
                PriorityName: { Display: '', Value: '' },
                Status: 0,
                StatusId: 0,
                TaskStatus: { Display: '', Value: '' },
                Note: ''
            };
            var AddtaskModalInstance = $uibModal.open({
                templateUrl: 'myModalContent.html',
                controller: AddTaskModalInstanceCtrl,
                resolve: {
                    TaskService: function () { return TaskService; },
                    objtask: function () { return objtask; }
                }
            });

            AddtaskModalInstance.result.then(function () {
                $scope.GetTaskInfromation()
            }, function () { });
        }

        $scope.dayClickevent = function (date, allDay, jsEvent, view) {
            console.log("Day click event");
        }

        var TaskStatus = {
            Hold: "#d6b215",
            OnProgress: "#1468ad",
            Complete: "#0b5b0f",
            Cancel: "#ce4c1c"
        }

        $scope.getColorInfo = function (id) {
            switch (id) {
                case 1:
                    return TaskStatus.Hold
                    break;
                case 2:
                    return TaskStatus.OnProgress
                    break;
                case 3:
                    return TaskStatus.Complete
                    break;
                case 4:
                    return TaskStatus.Cancel
                    break;
            }
        }

        $scope.GetTaskInfromation = function () {
            InquiryService.GetAllInquiryInfo().then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objTaskArray.length = 0;
                    _.forEach(result.data.DataList, function (value, key) {
                        if (value.Date) {
                            $scope.objTaskArray.push({
                                Id: value.Id,
                                title: value.title,
                                Status: value.Status,
                                color: $scope.getColorInfo(value.Status),
                                start: value.Time ? moment(value.Date).format("YYYY-MM-DD") + 'T' + moment(value.Time).format("HH:mm") : moment(value.Date).format("YYYY-MM-DD"),
                                //start: (value.FollowTime) ? moment(value.FollowDate).format("YYYY-MM-DD ") + moment(value.FollowTime).format("HH:mm") : moment(value.FollowDate).format("YYYY-MM-DD"),
                                typeOfEventTitle: value.typeOfEventTitle,
                                objTask: value
                            })
                        }
                    });
                    //InquiryService.GetAllInquiryInfo().then(function (resultdata) {
                    //    if (resultdata.data.ResponseType == 1) {
                    //        $scope.objInquiryArray.length = 0;
                    //        _.forEach(resultdata.data.DataList, function (value, key) {
                    //            if (value.NextFollowDate) {
                    //                $scope.objInquiryArray.push({
                    //                    Id: value.FollowupId,
                    //                    title: value.CurrentUpdate,
                    //                    Status: value.Status,
                    //                    color: $scope.getColorInfo(value.Status),
                    //                    start: value.NextFollowTime ? moment(value.NextFollowDate).format("YYYY-MM-DD") + 'T' + moment(value.NextFollowTime).format("HH:mm") : moment(value.NextFollowDate).format("YYYY-MM-DD"),
                    //                    //start: (value.FollowTime) ? moment(value.FollowDate).format("YYYY-MM-DD ") + moment(value.FollowTime).format("HH:mm") : moment(value.FollowDate).format("YYYY-MM-DD"),
                    //                    typeOfEventTitle: "InquiryFollowUp",
                    //                    //objTask: value
                    //                })
                    //            }
                    //        })

                    //    }
                    //});

                    /* config object */
                    $scope.uiConfig = {
                        calendar: {
                            height: 600,
                            editable: false,
                            customButtons: {
                                //AddTask: {
                                //    text: 'Add Task',
                                //    click: function () {
                                //        $scope.AddTask();
                                //    }
                                //},
                                //AllTask: {
                                //    text: 'All Task',
                                //    click: function () {
                                //        window.location.href = "/transaction/task";
                                //    }
                                //}
                            },
                            header: {
                                left: 'agendaDay basicWeek month ',
                                //left: 'agendaDay basicWeek month AddTask AllTask',
                                //left: 'month basicWeek basicDay agendaWeek agendaDay',
                                center: 'title',
                                right: 'prev,next'
                            },
                            eventClick: $scope.alertOnEventClick,
                            eventDrop: $scope.alertOnDrop,
                            eventResize: $scope.alertOnResize,
                            eventRender: $scope.eventRender,
                            dayClick: $scope.dayClickevent,
                            //timeFormat: {
                            //    '': 'H:mm{ - H:mm }'
                            //},
                            eventAfterAllRender: function () {

                                //  $compile($('.fc-month-button').after('<button data-ng-click="AddTask()"> Add Task </button>'))($scope);
                                //  $compile(element)($scope);


                                //$rootScope.$safeApply($scope, function () {
                                //    $compile($(".fc-event-container"))($scope);
                                //});
                            },
                            eventRender: function (event, eventElement, monthView) {
                                eventElement.attr({
                                    'uib-tooltip': event.typeOfEventTitle,
                                    //'tooltip-append-to-body': true
                                });

                                $compile(eventElement)($scope);

                                //$('.fc-agendaDay-button').text('Day')
                                //$('.fc-basicWeek-button').text('Week')
                                //$('.fc-month-button').text('Month')

                                //Change current date color
                                $("td[data-date='" + $filter('date')(new Date(), "yyyy-MM-dd") + "']").css({ "background-color": "rgba(83, 123, 101, 0.51)", "border": "1px solid #FFFFFF" });
                            }
                        }
                    };

                    $('.fc-agendaDay-button').text('Day')
                    $('.fc-basicWeek-button').text('Week')
                    $('.fc-month-button').text('Month')
                    // $compile($('.fc-month-button').after('<button data-ng-click="AddTask()"> Add Task </button>'))($scope);

                    // $scope.eventSources = [$scope.calEventsExt, $scope.events, $scope.eventsF];
                    //$scope.object = $.extend([], $scope.objTaskArray, $scope.objInquiryArray);
                    $scope.eventSources = [$scope.objTaskArray];
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }
        $scope.GetTaskInfromation()

        //$scope.object = $scope.objTaskArray.concat($scope.objInquiryArray);
        $scope.eventSources = [$scope.objTaskArray];

        $scope.alertOnEventClick = function (data, jsEvent, view) {
            if (data.typeOfEventTitle == "Task") {
                var taskModalInstance = $uibModal.open({
                    templateUrl: 'taskModalContent.html',
                    controller: TaskModalInstanceCtrl,
                    resolve: {
                        objTaskdata: function () { return data; },
                        CalendarCtrl: function () { return $scope; },
                        TaskService: function () { return TaskService; },
                    }
                });
                taskModalInstance.result.then(function () {
                    $scope.GetTaskInfromation()
                }, function () {
                });
            } else if (data.typeOfEventTitle == "InquiryFollowUp") {
                var taskModalInstance = $uibModal.open({
                    templateUrl: 'inquiryfollowupModalContent.html',
                    controller: InquiryFollowUpModalInstanceCtrl,
                    resolve: {
                        InquiryFollowUpDetailsData: function () { return data.objTask; },
                        CalendarCtrl: function () { return $scope; },
                        TaskService: function () { return TaskService; },
                        InquiryService: function () { return InquiryService; },
                    }
                });
                taskModalInstance.result.then(function () {
                    $scope.GetTaskInfromation()
                }, function () {
                });
            }
        };

        $scope.alertOnDrop = function (event, delta, revertFunc, jsEvent, ui, view) {
            $scope.alertMessage = ('Event Droped to make dayDelta ' + delta);
        };

        $scope.alertOnResize = function (event, delta, revertFunc, jsEvent, ui, view) {
            $scope.alertMessage = ('Event Resized to make dayDelta ' + delta);
        };

        $scope.changeView = function (view, calendar) {
            uiCalendarConfig.calendars[calendar].fullCalendar('changeView', view);
        };

        $scope.renderCalender = function (calendar) {
            if (uiCalendarConfig.calendars[calendar]) {
                uiCalendarConfig.calendars[calendar].fullCalendar('render');
            }
        };

    }

    var TaskModalInstanceCtrl = function ($scope, $uibModalInstance, $filter, objTaskdata, TaskService, TaskStatusService) {
        var objtempinq = {};
        $scope.objtask = $scope.objtask || {}
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};
        $scope.isClicked = true;
        function ResetForm() {
            $scope.objtask = {
                TaskId: 0,
                Task: '',
                FollowTime: new Date(),
                FollowDate: '',
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
                GroupBy: '',
                ToId: '',
                CreatedBy: '',
                FromUser: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormTaskInfo)
                $scope.$parent.FormTaskInfo.$setPristine();

        }

        if (objTaskdata.objTask.Id > 0) {
            TaskService.GetTaskInfoById(objTaskdata.objTask.Id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    var data;
                    var d = new Date();
                    var gettime = '';
                    var mytime = d;

                    if (result.data.DataList == null) {
                        TaskService.GetTaskDatabyId(objTaskdata.objTask.Id).then(function (getresult) {
                            if (getresult.data.ResponseType == 1) {
                                data = getresult.data.DataList;
                                if (data.FollowTime != null) {
                                    d.setHours($filter('date')(data.FollowTime, "HH:mm").Hours);
                                    d.setMinutes($filter('date')(data.FollowTime, "HH:mm").Minutes);
                                    gettime = data.FollowTime.Hours == 0 && data.FollowTime.Minutes == 0 ? new Date() : mytime;
                                }

                                objtempinq = {
                                    TaskId: data.TaskId,
                                    Task: data.Task,
                                    FollowTime: gettime,
                                    FollowDate: data.FollowDate ? $filter('mydate')(data.FollowDate) : new Date(),
                                    Priority: data.Priority,
                                    PriorityName: { Display: data.PriorityName, Value: data.Priority },
                                    Review: data.Review,
                                    TaskType: data.TaskType,
                                    TaskTypeId: data.TaskTypeId,
                                    TaskTypeName: { Display: data.TaskType, Value: data.TaskTypeId },
                                    GroupBy: data.GroupBy,
                                    Status: data.Status,
                                    TaskStatus: { Display: data.TSTaskStatus, Value: data.Status },
                                    Note: data.Note,
                                    ToId: data.ToId,
                                    CreatedBy: data.CreatedBy,
                                    FromUser: data.FromUser,
                                    ReportingUserArray: []
                                }
                                objtempinq.frmdate = ConvertDate(data.FollowDate, 'dd/mm/yyyy')
                                objtempinq.frmtime = ConvertTime(data.FollowTime)
                                if (objtempinq.TaskId > 0) {
                                    $scope.objtask = objtempinq;

                                    TaskService.ReportingUserBind(objTaskdata.objTask.Id).then(function (result) {
                                        if (result.data.ResponseType == 1) {
                                            var Mainres, res = '', AssignMsg = '', AssigneIds = '';
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
                                                $scope.objtask.AssignMsg = AssignMsg;
                                                $scope.objtask.reportlist = data;
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

                                } else {
                                    ResetForm();
                                }

                            } else {
                                window.location.href = "/Transaction/SalesOrder";
                                toastr.error(result.data.Message, 'Opps, Something went wrong');
                            }
                        })
                    } else {
                        data = result.data.DataList;

                        if (data.FollowTime != null) {
                            d.setHours($filter('date')(data.FollowTime, "HH:mm").Hours);
                            d.setMinutes($filter('date')(data.FollowTime, "HH:mm").Minutes);
                            gettime = data.FollowTime.Hours == 0 && data.FollowTime.Minutes == 0 ? new Date() : mytime;
                        }

                        objtempinq = {
                            TaskId: data.TaskId,
                            Task: data.Task,
                            FollowTime: gettime,
                            FollowDate: moment(data.FollowDate).format("DD-MM-YYYY") == "01-01-0001" ? new Date() : $filter('mydate')(data.FollowDate),
                            Priority: data.Priority,
                            PriorityName: { Display: data.PriorityName, Value: data.Priority },
                            Review: data.Review,
                            TaskType: data.TaskType,
                            TaskTypeId: data.TaskTypeId,
                            TaskTypeName: { Display: data.TaskType, Value: data.TaskTypeId },
                            GroupBy: data.GroupBy,
                            Status: data.Status,
                            TaskStatus: { Display: data.TSTaskStatus, Value: data.Status },
                            Note: data.Note,
                            ToId: data.ToId,
                            CreatedBy: data.CreatedBy,
                            FromUser: data.FromUser,
                            ReportingUserArray: []
                        }
                        objtempinq.frmdate = ConvertDate(data.FollowDate, 'dd/mm/yyyy')
                        objtempinq.frmtime = ConvertTime(data.FollowTime)
                        //$scope.Add(objtempinq);
                        if (objtempinq.TaskId > 0) {
                            $scope.objtask = objtempinq;

                            TaskService.ReportingUserBind(objTaskdata.objTask.Id).then(function (result) {
                                if (result.data.ResponseType == 1) {
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
                                        $scope.objtask.AssignMsg = AssignMsg;
                                        $scope.objtask.reportlist = data;
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

                        } else {
                            ResetForm();
                        }
                    }
                } else {
                    window.location.href = "/Transaction/SalesOrder";
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            })
        } else {
            ResetForm()
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

        $scope.Update = function (data) {
            var ToIdList = '';
            var ToLength = $scope.objtask.outputReportingUser.length;
            _.each($scope.objtask.outputReportingUser, function (value, key, list) {
                if (key < ToLength - 1) {
                    ToIdList += value.name + ','
                }
                else if (key == ToLength - 1) {
                    ToIdList += value.name;
                }

            })
            var objinstask = {
                TaskId: data.TaskId,
                Task: data.Task,
                FollowTime: $filter('date')(data.FollowTime, "HH:mm"),
                FollowDate: data.FollowDate,
                Priority: data.PriorityName.Value,
                PriorityName: { Display: data.PriorityName.Display, Value: data.PriorityName.Value },
                Status: data.TaskStatus.Value,
                TaskStatus: { Display: data.TaskStatus.Display, Value: data.TaskStatus.Value },
                Note: data.Note,
                Review: data.Review,
                TaskType: data.TaskTypeName.Display,
                TaskTypeId: data.TaskTypeName.Value,
                TaskTypeName: { Display: data.TaskTypeName.Display, Value: data.TaskTypeName.Value },
                GroupBy: data.GroupBy,
                ToId: ToIdList
            }
            TaskService.CreateUpdateTask(objinstask).then(function (result) {
                toastr.success(result.data.Message);
                $uibModalInstance.close();
            })
        }

        $scope.Reset = function () {
            if ($scope.objtask.InqId > 0) {
                $scope.objtask = angular.copy($scope.storage);
            } else {
                ResetForm();
            }

        }

        $scope.dateOptions = {
            formatYear: 'yy',
            //minDate: new Date(2016, 8, 5),
            //maxDate: new Date(2017, 5, 22),
            startingDay: 1
        };

        $scope.close = function () {
            $uibModalInstance.dismiss();
        };
    }
    TaskModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', '$filter', 'objTaskdata', 'TaskService', 'TaskStatusService']

    var InquiryFollowUpModalInstanceCtrl = function ($scope, $uibModalInstance, $filter,InquiryFollowUpDetailsData, TaskService, InquiryService ) {

        $scope.isClicked = true;
        //if (isdisable == 1) {
        //    $scope.isClicked = true;
        //}
        InquiryService.GetInquiryFollowUpByID(InquiryFollowUpDetailsData.Id).then(function (result) {
            $scope.objInquiryFollowUp = {
                FollowupId: result.data.FollowupId,
                InqId: result.data.InqId,
                CurrentUpdate: result.data.CurrentUpdate,
                NextFollowTime: (result.data.NextFollowTime == null) ? new Date() : result.data.NextFollowTime,
                NextFollowDate: (moment(result.data.NextFollowDate).format("DD-MM-YYYY") == "01-01-0001" || (result.data.NextFollowDate) == "") ? new Date() : $filter('mydate')(result.data.NextFollowDate),
                Status: result.data.Status,
                AssignId: result.data.AssignId,
                InquiryStatus: { Display: result.data.StatusName, Value: result.data.Status },
                outputReportingUser: []
            }
        });

        $scope.ReportingUserArray = [];
        $scope.outputReportingUser = [];
        //InquiryFollowUpDetailsData.AssignId;
        $scope.dateOptions = {
            formatYear: 'yy',
            //minDate: new Date(2016, 8, 5),
            //maxDate: new Date(2017, 5, 22),
            startingDay: 1
        };

        //$scope.InquiryStatus = { Display: InquiryFollowUpDetailsData.StatusName, Value: InquiryFollowUpDetailsData.Status },

        InquiryService.ReportingInquiryUserBind(InquiryFollowUpDetailsData.InqId).then(function (result) {
            _.each(result.data.DataList, function (value, key, list) {
                if (value.UserId == InquiryFollowUpDetailsData.AssignId) {
                    $scope.ReportingUserArray.push({
                        name: value.UserId,
                        maker: value.Name,
                        ticked: true,
                        //disabled: true
                    })

                }
                else {
                    $scope.ReportingUserArray.push({
                        name: value.UserId,
                        maker: value.Name,
                        ticked: false,
                        //disabled: (AssigneIds != undefined && AssigneIds.includes(value.UserId.toString())) ? true : false
                    })
                }
            })
        });

        $scope.objInquiryFollowUp = InquiryFollowUpDetailsData;
        // $scope.objInquiryFollowupDetails.NextFollowDate = moment(InquiryFollowUpDetailsData.NextFollowDate).format("DD-MM-YYYY") == "01-01-0001" ? new Date() : $filter('mydate')(InquiryFollowUpDetailsData.NextFollowDate);
        var mytime = new Date();
        var gettime = '';
        if (InquiryFollowUpDetailsData.Time != null) {
            mytime.setHours($filter('date')(InquiryFollowUpDetailsData.Time, "HH:mm").Hours);
            mytime.setMinutes($filter('date')(InquiryFollowUpDetailsData.Time, "HH:mm").Minutes);
            gettime = InquiryFollowUpDetailsData.Time.Hours == 0 && InquiryFollowUpDetailsData.Time.Minutes == 0 ? new Date() : mytime;
            $scope.objInquiryFollowUp.NextFollowTime = gettime;
        }
        $scope.close = function () {
            $uibModalInstance.dismiss();
        };

        $scope.CreateInquiryFollowUp = function () {
            $scope.objInquiryFollowUp.NextFollowTime = $filter('date')($scope.objInquiryFollowUp.NextFollowTime, "HH:mm");
            $scope.objInquiryFollowUp.AssignId = $scope.objInquiryFollowUp.outputReportingUser[0].name;
            $scope.objInquiryFollowUp.Status = $scope.objInquiryFollowUp.InquiryStatus.Value;
            var aid = $scope.objInquiryFollowUp.AssignId;
            InquiryService.SaveInquiryFollowUp($scope.objInquiryFollowUp).then(function (result) {
                toastr.success(result.data.Message);
                $uibModalInstance.close(aid);
            })
        }
    }
    InquiryFollowUpModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', '$filter', 'InquiryFollowUpDetailsData', 'TaskService', 'InquiryService']

    var AddTaskModalInstanceCtrl = function ($scope, $uibModalInstance, TaskService, objtask, $filter) {

        $scope.objtask = $scope.objtask || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};

        if (objtask.TaskId > 0) {
            $scope.objtask = objtask;

        } else {
            ResetForm();
        }

        function ResetForm() {
            $scope.objtask = {
                TaskId: 0,
                AssignTo: 0,
                Task: '',
                FollowTime: new Date(),
                FollowDate: '',
                Priority: 0,
                PriorityId: 0,
                PriorityName: { Display: '', Value: '' },
                Status: 0,
                StatusId: 0,
                TaskStatus: { Display: '', Value: '' },
                Note: '',
                FromUser: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormTaskInfo)
                $scope.$parent.FormTaskInfo.$setPristine();

        }

        $scope.Create = function (data) {
            var objinstask = {
                TaskId: data.TaskId,
                //AssignTo: data.Name.Value,
                //Name: { Display: data.Name.Display, Value: data.Name.Value },
                Task: data.Task,
                //FollowTime: $filter('date')(data.FollowTime, "hh:mm"),
                //FollowDate: data.FollowDate,
                Priority: data.PriorityName.Value,
                // PriorityName: { Display: data.PriorityName.Display, Value: data.PriorityName.Value },
                //Status: data.TaskStatus.Value,
                //TaskStatus: { Display: data.TaskStatus.Display, Value: data.TaskStatus.Value },
                //Note: data.Note
            }
            TaskService.AddTask(objinstask).then(function (result) {
                toastr.success(result.data.Message);
                $uibModalInstance.close();
                ResetForm();
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.Update = function (data) {

            var objinstask = {
                TaskId: data.TaskId,
                AssignTo: data.Name.Value,
                Name: { Display: data.Name.Display, Value: data.Name.Value },
                Task: data.Task,
                FollowTime: $filter('date')(data.FollowTime, "HH:mm"),
                FollowDate: data.FollowDate,
                Priority: data.PriorityName.Value,
                PriorityName: { Display: data.PriorityName.Display, Value: data.PriorityName.Value },
                Status: data.TaskStatus.Value,
                TaskStatus: { Display: data.TaskStatus.Display, Value: data.TaskStatus.Value },
                Note: data.Note
            }
            TaskService.AddTask(objinstask).then(function (result) {
                toastr.success(result.data.Message);
                $uibModalInstance.close();
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.Reset = function () {
            if ($scope.objtask.InqId > 0) {
                $scope.objtask = angular.copy($scope.storage);
            } else {
                ResetForm();
            }

        }

        $scope.dateOptions = {
            formatYear: 'yy',
            //minDate: new Date(2016, 8, 5),
            //maxDate: new Date(2017, 5, 22),
            startingDay: 1
        };

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    AddTaskModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'TaskService', 'objtask', '$filter']

})()

