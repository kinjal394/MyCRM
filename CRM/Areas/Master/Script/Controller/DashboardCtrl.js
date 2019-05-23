(function () {
    "use strict"
    angular.module("CRMApp.Controllers")
        .controller("DashboardCtrl", [
            "$scope", "$rootScope", "$timeout", "$filter", "WorkProfileService", "DashboardService", "TaskStatusService", "NgTableParams", "$uibModal",
            function DashboardCtrl($scope, $rootScope, $timeout, $filter, WorkProfileService, DashboardService, TaskStatusService, NgTableParams, $uibModal) {

                $scope.labels = ['2006', '2007', '2008', '2009', '2010', '2011', '2012'];
                $scope.series = ['Series A', 'Series B'];

                $scope.NoOfTask = 0;
                $scope.NoOfClient = 0;
                $scope.NoOfProduct = 0;
                $scope.NoOfInquiry = 0;
                $scope.TaskStatusData = {
                    Status: '',
                    TaskStatus: '',
                    TotalTask: '',
                    Percentage: ''
                }
                $scope.InqStatusData = {
                    Status: '',
                    TaskStatus: '',
                    TotalTask: '',
                    Percentage: ''
                }
                $scope.CountryVisitorData = {
                    CountryId: '',
                    CountryName: '',
                    TotalVisitor: '',
                    Percentage: ''
                }
                $scope.data = [
                    [65, 59, 80, 81, 56, 55, 40],
                    [28, 48, 40, 19, 86, 27, 90]
                ];

                $scope.SetworkProfile = function (id) {
                    $rootScope.UserType = id;
                    DashboardService.GetDashbordData().then(function (result1) {
                        $("#ChartType").val(1);
                        //$scope.ChartType = 1;
                        if (result1.data.ResponseType == 1) {
                            $scope.NoOfClient = result1.data.DataList.NoofData[0].noofRecord;
                            $scope.NoOfInquiry = result1.data.DataList.NoofData[1].noofRecord;
                            $scope.NoOfProduct = result1.data.DataList.NoofData[2].noofRecord;
                            $scope.NoOfTask = result1.data.DataList.NoofData[3].noofRecord;

                            $scope.TaskStatusData = [];
                            angular.forEach(result1.data.DataList.TaskStatusData, function (value) {
                                var objTaskStatusData = {
                                    Status: value.Status,
                                    TaskStatus: value.TaskStatus,
                                    TotalTask: value.TotalTask,
                                    Percentage: value.Percentage
                                }
                                $scope.TaskStatusData.push(objTaskStatusData);
                            }, true);

                            $scope.InqStatusData = [];
                            angular.forEach(result1.data.DataList.InqStatusData, function (value) {
                                var objInqStatusData = {
                                    Status: value.Status,
                                    TaskStatus: value.TaskStatus,
                                    TotalTask: value.TotalTask,
                                    Percentage: value.Percentage
                                }
                                $scope.InqStatusData.push(objInqStatusData);
                            }, true);

                            $scope.CountryVisitorData = [];
                            angular.forEach(result1.data.DataList.VisitorData, function (value) {
                                var objCountryVisitorData = {
                                    CountryId: value.CountryId,
                                    CountryName: value.CountryName,
                                    TotalVisitor: value.TotalVisitor,
                                    Percentage: value.Percentage
                                }
                                $scope.CountryVisitorData.push(objCountryVisitorData);
                            }, true);


                        }
                    }, function (errorMsg) {
                        toastr.error(errorMsg, 'Opps, Something went wrong');
                    });
                    WorkProfileService.GetAllWorkProfile().then(function (result) {
                        $("#ChartType").val(1);
                        ChartInfo()
                        if (result.data.ResponseType == 1) {
                            $scope.objWorkDetail = {
                                objWP: []
                            };
                            angular.forEach(result.data.DataList, function (value) {
                                var d = new Date();
                                var gettime = '';
                                var mytime = d;
                                if (value.WorkTime != null) {
                                    d.setHours($filter('date')(value.WorkTime, "HH:mm").Hours);
                                    d.setMinutes($filter('date')(value.WorkTime, "HH:mm").Minutes);
                                    gettime = ('0' + value.WorkTime.Hours.toString()).slice(-2) + ':' + ('0' + value.WorkTime.Minutes.toString()).slice(-2);
                                } else {
                                    gettime = '00:00';
                                }
                                var objWP = {
                                    WorkProfileId: value.WorkProfileId,
                                    Title: value.Title,
                                    Description: value.Description,
                                    WorkTime: gettime
                                }
                                $scope.objWorkDetail.objWP.push(objWP);
                            }, true);

                        } else {
                            toastr.error(result.data.Message, 'Opps, Something went wrong');
                        }
                    }, function (errorMsg) {
                        toastr.error(errorMsg, 'Opps, Something went wrong');
                    })
                }

                $scope.AjaxNotGoodForRedirect = function (url) {
                    DashboardService.AjaxNotGoodForRedirect(url).then(function (result) {
                        if (result.data.ok == true) {
                            if (result.data.WorkType == 'worktime') {
                                alert(result.data.message);
                                if (result.data.isPopup == true) {
                                    return true;
                                } else { return false; }
                            }
                            else if (result.data.WorkType == 'worktimeend') {
                                alert(result.data.message);
                                if (result.data.isPopup == true) {
                                    return true;
                                } else { return false; }
                            }
                            else if (result.data.WorkType == 'lunchtime') { alert(result.data.message); return false; }
                            else if (result.data.WorkType == 'lunchtimeend') {
                                alert(result.data.message);
                                if (result.data.isPopup == true) {
                                    return true;
                                } else { return false; }
                            }
                        } else {
                            toastr.error(errorMsg, 'Opps, Something went wrong');
                            return false;
                        }
                    });
                }
                //$scope.objDailyReporting = { Remark: '' }
                $scope.WorkList = [];
                $scope.objWorkDetail = {
                    UserId: 0,
                    WorkTypeId: 0,
                    TaskStatus: '',
                    DailyWorkDetail: []
                };
                $scope.Workendhiddenfield = [];
                $scope.Workendhiddenfield[0] = false;

                function GetAllDailyWorkInfo(wid) {
                    DashboardService.GetAllDailyWorkInfo(wid).then(function (result) {
                        if (result.data.ResponseType == 1) {
                            var objAttendance = result.data.DataList.objAttendanceModel;
                            $scope.objWorkDetail = {
                                UserId: objAttendance.UserId,
                                WorkTypeId: objAttendance.WorkTypeId,
                                DailyWorkDetail: objAttendance.DailyWorkDetail
                            }
                            $scope.objDailyReporting.Remark = objAttendance.DailyWorkDetail[0].Remark;
                            $scope.storage = angular.copy($scope.objWorkDetail);
                            $scope.WorkList = objAttendance.DailyWorkDetail;
                        } else {
                            toastr.error(result.data.Message, 'Opps, Something went wrong');
                        }
                    }, function (errorMsg) {
                        toastr.error(errorMsg, 'Opps, Something went wrong');
                    })
                }
                $scope.CreateDailyWork = function (data) {
                    //var RemarkVal = data.Remark;
                    //angular.forEach($scope.WorkList, function (value) {
                    //    $scope.objWork = {
                    //        DailyWorkId: 0,
                    //        UserId: 0,
                    //        Date: '',
                    //        TaskInqId: value.TaskInqId,
                    //        TaskType: $scope.TaskTypeId,
                    //        AttandanceType: $scope.AttandanceTypeId,
                    //        Remark: RemarkVal,
                    //        Persontage: value.Persontage
                    //    };
                    //    //$scope.objWorkDetail.WorkTypeId = $scope.AttandanceTypeId;
                    //    $scope.objWorkDetail.DailyWorkDetail.push($scope.objWork);
                    //}, true);
                    //$scope.storage = angular.copy($scope.objWorkDetail);

                    DashboardService.SaveDailyWork($scope.objWorkDetail).then(function (result) {
                        if (result.data.ResponseType == 1) {
                            toastr.success(result.data.Message);
                            //ResetForm();
                            window.location.href = "/Master/Dashboard/Dashboard";
                            //$uibModalInstance.close();
                        } else {
                            toastr.error(result.data.Message);
                        }
                    }, function (errorMsg) {
                        toastr.error(errorMsg, 'Opps, Something went wrong');
                    })
                }
                $scope.GetTaskDetail = function (WorkId, obj) {
                    var results = getByScopeId(obj.$id);
                    var ischk = results[0].cells[6].childNodes[0].checked;
                    if ($scope.WorkList.indexOf(WorkId) === -1 && ischk == true) {
                        $scope.Workendhiddenfield[WorkId] = true;
                        if ($scope.WorkList.length == 0 || $scope.WorkList.length) {
                            $scope.WorkList.push(
                                {
                                    TaskInqId: WorkId,
                                    Persontage: 0,
                                    Status: 0
                                });
                        }
                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'myModalContent.html',
                            controller: ModalInstanceCtrl,
                            size: 'md',
                            resolve: {
                                WorkList: function () {
                                    return $scope.WorkList;
                                },
                                WorkId: function () {
                                    return WorkId;
                                },
                                storage: function () {
                                    return $scope.storage;
                                },
                                objWorkDetail: function () { return $scope.objWorkDetail; },
                                DashboardService: function () { return DashboardService; },
                                popupClS: function () { return results; }
                            }
                        });

                        //modalInstance.result.then(function () {
                        //    $scope.refreshGrid();
                        //}, function () {
                        //    // $log.info('Modal dismissed at: ' + new Date());
                        //});
                        //else {
                        //    var results = getByScopeId(obj.$id);
                        //    $(results.find("td:last").find('input')).prop("checked", false)
                        //    alert("Minimum 4 Work Allow.");
                        //    return false;
                        //}
                        //return true;
                    } else {
                        var ind = $scope.WorkList.indexOf(WorkId);
                        $scope.WorkList.splice(ind, 1);
                        $scope.objWorkDetail.DailyWorkDetail.splice(ind, 1);
                        $scope.Workendhiddenfield[WorkId] = false;
                        return false;
                    }
                }
                $scope.SetTaskDetail = function (WorkId, Status) {
                    var per = parseFloat(Status.$row.Persontage);
                    angular.forEach($scope.WorkList, function (value) {
                        if (value.TaskInqId == WorkId) {
                            value.Persontage = per;
                        }
                    }, true);
                }
                $scope.SetStatusDetail = function (WorkId, percentage) {
                    var per = (percentage.$row.TaskStatus);
                    angular.forEach($scope.WorkList, function (value) {
                        if (value.TaskInqId == WorkId) {
                            value.Persontage = per;
                        }
                    }, true);
                }
                function getByScopeId(id) {
                    var filterfn = function (i, el) {
                        var sc = angular.element(el).scope();

                        return sc && sc.$id == id;
                    };
                    // low hanging fruit -- actual scope containers
                    var result = $('.ng-scope').filter(filterfn);
                    if (result && result.length) return result;

                    // try again on everything...ugh
                    return $(':not(.ng-scope)').filter(filterfn);
                }
                //if (temp != result.data.DataList[i].Name) {
                //    name += result.data.DataList[i].Name;
                //}

                //$('select[name="ChartType"]').change(function(){
                //    debugger
                //    ChartInfo(data.value);       
                //});​
                $scope.selectedItemChanged = function () {
                    debugger
                    ChartInfo($('#ChartType :selected').val());
                }
                function ChartInfo(id) {
                    debugger
                    var TypeId = id == undefined ? 1 : id;
                    DashboardService.GetChartdata(TypeId).then(function (result) {
                        debugger;
                        var title = $('#ChartType :selected').text();
                        var arrcategories = result.data.DataList.categories;
                        var arrseries = result.data.DataList.chartDataList;
                        Highcharts.chart('container', {
                            chart: {
                                type: 'column'
                            },
                            title: {
                                text: title + 'Wise'
                            },
                            xAxis: {
                                categories: arrcategories//['2017', '2016', '2015']
                            },
                            yAxis: {
                                allowDecimals: false,
                                min: 0,
                                title: {
                                    text: ''
                                }
                            },
                            tooltip: {
                                formatter: function () {
                                    return '<b>' + this.x + '</b><br/>' +
                                        this.series.name + ': ' + this.y;
                                }
                            },
                            series: arrseries
                        });
                    }, function (errorMsg) {
                        toastr.error(errorMsg, 'Opps, Something went wrong');
                    })
                }
                $scope.OutWorkPopup = function (timeString) {
                    if (timeString == 'outsideime') {
                        DashboardService.AjaxNotGoodForRedirect('/Login/Attendance?time=outsideime').then(function (result) {
                            if (result.data.ok == false) {
                                if (result.data.msgerror == false) {
                                    toastr.error(result.data.message);
                                } else if (result.data.msgerror == true) {
                                    var modalInstance = $uibModal.open({
                                        backdrop: 'static',
                                        animation: true,
                                        ariaLabelledBy: 'modal-title',
                                        ariaDescribedBy: 'modal-body',
                                        templateUrl: 'myModalContent.html',
                                        controller: ModalpromptInstanceCtrl,
                                        //size: 'md',
                                        resolve: {
                                            DashboardService: function () { return DashboardService; }
                                        }
                                    });
                                }
                            } else {
                                toastr.error(result.data.message);
                                return false;
                            }
                        }, function (errorMsg) {
                            toastr.error(errorMsg, 'Opps, Something went wrong');
                        })
                    }

                }
            }]);


    function isNumber(evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }

    var ModalpromptInstanceCtrl = function ($scope, $uibModalInstance, DashboardService) {
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};

        $scope.close = function () {
            $uibModalInstance.close();
        };

        $scope.CreateReason = function (Reason) {
            DashboardService.AjaxNotGoodForRedirect('/Login/Attendance?time=outsideime&reason=' + Reason).then(function (result) {
                if (result.data.ok == false) {
                    if (result.data.msgerror == false) {
                        toastr.error(result.data.message);
                    } else if (result.data.msgerror == true) {
                        toastr.error(result.data.message);
                    }
                } else {
                    toastr.success(result.data.message);
                    return false;
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
            $uibModalInstance.close();
        }

    }
    ModalpromptInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'DashboardService']

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, WorkList, WorkId, storage, objWorkDetail, DashboardService, popupClS) {
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};

        //var TaskTypeId = $scope.business.Value; 
        $scope.TaskTypeId = 1; // Note : 1-Task , 2-Inquiry
        //$scope.AttandanceTypeId = Attid;
        $scope.objDailyReporting = { TaskStatus: '', Persontage: '', Remark: '', TaskStatusData: { Display: '', Value: '' } }
        $scope.WorkList = WorkList;
        //$scope.objWorkDetail = {
        //    UserId: 0,
        //    WorkTypeId: 0,
        //    TaskStatus: '',
        //    DailyWorkDetail: []
        //}

        $scope.GetAllDailyWorkInfo = function (wid) {
            DashboardService.GetAllDailyWorkInfo(wid).then(function (result) {
                if (result.data.ResponseType == 1) {
                    var objAttendance = result.data.DataList.objAttendanceModel;
                    $scope.objWorkDetail = {
                        UserId: objAttendance.UserId,
                        WorkTypeId: objAttendance.WorkTypeId,
                        DailyWorkDetail: objAttendance.DailyWorkDetail
                    }
                    $scope.objDailyReporting.Remark = objAttendance.DailyWorkDetail[0].Remark;
                    $scope.storage = angular.copy($scope.objWorkDetail);
                    $scope.WorkList = objAttendance.DailyWorkDetail;
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        //DailyWorkId, UserId, Date, TaskInqId, TaskType, AttandanceType, Remark
        $scope.TaskList = [];
        $scope.abc = function () {
            TaskStatusService.GetAllTaskStatus().then(function (result) {
                $scope.TaskList = result.data.DataList;
            })
        }
        $scope.GetTaskDetail = function (WorkId, obj) {
            if ($scope.WorkList.indexOf(WorkId) === -1) {
                if ($scope.WorkList.length == 0 || $scope.WorkList.length < 4) {
                    $scope.WorkList.push(WorkId);
                    return true;
                } else {
                    var results = getByScopeId(obj.$id);
                    $(results.find("td:last").find('input')).prop("checked", false)
                    alert("Minimum 4 Work Allow.");
                    return false;
                }
            } else {
                var ind = $scope.WorkList.indexOf(WorkId);
                $scope.WorkList.splice(ind, 1);
                return false;
            }
        }

        var getByScopeId = function (id) {
            var filterfn = function (i, el) {
                var sc = angular.element(el).scope();

                return sc && sc.$id == id;
            };
            // low hanging fruit -- actual scope containers
            var result = $('.ng-scope').filter(filterfn);
            if (result && result.length) return result;

            // try again on everything...ugh
            return $(':not(.ng-scope)').filter(filterfn);
        }

        $scope.SetTaskDetail = function (WorkId, percentage) {
            var per = parseFloat(percentage.$row.Persontage);
            angular.forEach($scope.WorkList, function (value) {
                if (value.TaskInqId == WorkId) {
                    value.Persontage = per;
                }
            }, true);
        }

        //function ResetForm() {
        //    $scope.WorkList = [];
        //    $scope.objWorkDetail = {
        //        UserId: 0,
        //        DailyWorkDetail: []
        //    }
        //    if ($scope.$parent.FormDashboardInfo)
        //        $scope.$parent.FormDashboardInfo.$setPristine();
        //}

        $scope.close = function () {
            //var ind = $scope.WorkList.indexOf(WorkId);
            //$scope.WorkList.splice(ind, 1);
            //objWorkDetail.DailyWorkDetail.splice(ind, 1);
            //var results = popupClS;
            $(popupClS.find("td:last").find('input')).prop("checked", false)
            $uibModalInstance.close();
        };

        //$scope.CreateDailyWork = function (data) {
        //    var RemarkVal = data.Remark;
        //    angular.forEach($scope.WorkList, function (value) {
        //        $scope.objWork = {
        //            DailyWorkId: 0,
        //            UserId: 0,
        //            Date: '',
        //            TaskInqId: value,
        //            TaskType: $scope.TaskTypeId,
        //            AttandanceType: $scope.AttandanceTypeId,
        //            Remark: RemarkVal
        //        };
        //        $scope.objWorkDetail.WorkTypeId = $scope.AttandanceTypeId;
        //        $scope.objWorkDetail.DailyWorkDetail.push($scope.objWork);
        //    }, true);
        //    $scope.storage = angular.copy($scope.objWorkDetail);
        //    DashboardService.SaveDailyWork($scope.objWorkDetail).then(function (result) {
        //        if (result.data.ResponseType == 1) {
        //            toastr.success(result.data.Message);
        //            ResetForm();
        //            $uibModalInstance.close();
        //        } else {
        //            toastr.error(result.data.Message);
        //        }
        //    }, function (errorMsg) {
        //        toastr.error(errorMsg, 'Opps, Something went wrong');
        //    })
        //}

        //$scope.UpdateDailyWork = function (data) {
        //    var RemarkVal = data.Remark;
        //    angular.forEach($scope.WorkList, function (value) {
        //        value.Remark = RemarkVal
        //    }, true);
        //    $scope.objWorkDetail.WorkTypeId = $scope.AttandanceTypeId;
        //    $scope.objWorkDetail.DailyWorkDetail = $scope.WorkList;
        //    $scope.storage = angular.copy($scope.objWorkDetail);
        //    DashboardService.SaveDailyWork($scope.objWorkDetail).then(function (result) {
        //        if (result.data.ResponseType == 1) {
        //            toastr.success(result.data.Message);
        //            ResetForm();
        //            $uibModalInstance.close();
        //        } else {
        //            toastr.error(result.data.Message);
        //        }
        //    }, function (errorMsg) {
        //        toastr.error(errorMsg, 'Opps, Something went wrong');
        //    })
        //}

        $scope.CreateDailyWork = function (data) {
            angular.forEach($scope.WorkList, function (value) {
                if (WorkId == value.TaskInqId) {
                    $scope.objWork = {
                        DailyWorkId: 0,
                        UserId: 0,
                        Date: '',
                        TaskInqId: value.TaskInqId,
                        TaskType: $scope.TaskTypeId,
                        AttandanceType: $scope.AttandanceTypeId,
                        Remark: data.Remark,
                        Persontage: data.Persontage,
                        TaskStatus: data.TaskStatusData.Value,
                    };
                    objWorkDetail.DailyWorkDetail.push($scope.objWork);
                }
                //$scope.objWorkDetail.WorkTypeId = $scope.AttandanceTypeId;
            }, true);
            $scope.storage = angular.copy(objWorkDetail);
            $uibModalInstance.close();
            //DashboardService.SaveDailyWork($scope.objWorkDetail).then(function (result) {
            //    if (result.data.ResponseType == 1) {
            //        toastr.success(result.data.Message);
            //        ResetForm();
            //        window.location.href = "/Master/Dashboard/Dashboard";
            //        //$uibModalInstance.close();
            //    } else {
            //        toastr.error(result.data.Message);
            //    }
            //}, function (errorMsg) {
            //    toastr.error(errorMsg, 'Opps, Something went wrong');
            //})
        }

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'WorkList', 'WorkId', 'storage', 'objWorkDetail', 'DashboardService', 'popupClS']

})()