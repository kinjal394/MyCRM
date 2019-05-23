(function () {
    "use strict"
    angular.module("CRMApp.Controllers")
            .controller("WorkProfileCtrl", [
            "$scope", "$rootScope", "$timeout", "$filter", "WorkProfileService", "NgTableParams", "$uibModal", "$compile",
            function WorkProfileCtrl($scope, $rootScope, $timeout, $filter, WorkProfileService, NgTableParams, $uibModal,$compile) {


                $scope.Add = function (id, _isdisable) {
                    if (_isdisable === undefined) _isdisable = 0;

                    var modalInstance = $uibModal.open({
                        backdrop: 'static',
                        templateUrl: 'myModalContent.html',
                        controller: ModalInstanceCtrl,
                        resolve: {
                            WorkProfileService: function () { return WorkProfileService; },
                            id: function () { return id; },
                            isdisable: function () { return _isdisable; }
                        }
                    });
                    modalInstance.result.then(function () {
                        $scope.refreshGrid()
                    }, function () {
                        // $log.info('Modal dismissed at: ' + new Date());
                    });
                }

                $scope.setDirectiveRefresh = function (refreshGrid) {
                    $scope.refreshGrid = refreshGrid;
                };

                $scope.gridObj = {
                    columnsInfo: [
                        //--WorkProfileId,DepartmentId,DepartmentName,Title,Description,WorkTime,IsActive
                       //{ "title": "Work Profile Id", "data": "WorkProfileId", filter: false, visible: false },
                       { "title": "Sr.", "field": "RowNumber", show: true, },
                       { "title": "Department", "field": "DepartmentName", sortable: "DepartmentName", filter: { DepartmentName: "text" }, show: true, },
                       { "title": "Work Title", "field": "Title", sortable: "Title", filter: { Title: "text" }, show: true, },
                       { "title": "Work Description", "field": "Description", sortable: "Description", filter: { Description: "text" }, show: true, },
                      {
                          "title": "Work Date", "field": "WorkDate", sortable: "WorkDate", filter: { WorkDate: "date" }, show: true,
                          'cellTemplte': function ($scope, row) {
                              var element = '<span >{{ConvertDate(row.WorkDate,\'dd/mm/yyyy\')}}</span>'
                              return $scope.getHtml(element);
                          }
                      },
                       
                       {
                           "title": "WorkTime", "field": "WorkTime", sortable: "WorkTime", filter: { WorkTime: "text" }, show: true,
                           'cellTemplte': function ($scope, row) {
                               var element = '<p ng-bind="ConvertTime(row.WorkTime)">'
                               return $scope.getHtml(element);
                           }
                       },
                       { "title": "Work Cycle", "field": "WorkCycle", sortable: "WorkCycle", filter: { WorkCycle: "text" },show:true },
                       {
                           "title": "Action", show: true,
                           'cellTemplte': function ($scope, row) {
                               var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.WorkProfileId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                                    //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.WorkProfileId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                                    '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.WorkProfileId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                               return $scope.getHtml(element);
                           }
                       }
                    ],
                    Sort: { "WorkProfileId": "asc" }
                }

                $scope.Edit = function (id) {
                    $scope.Add(id, 0);
                }

                $scope.View = function (id) {
                    $scope.Add(id, 1);
                }

                $scope.Delete = function (id) {
                    WorkProfileService.DeleteWorkProfile(id).then(function (result) {
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

                $scope.RefreshTable = function () {
                    $scope.tableParams.reload();
                };

            }]);

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, WorkProfileService, id, isdisable, $filter) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.objWorkProfile = $scope.objWorkProfile || {};
        $scope.objWorkProfile = {
            WorkProfileId: 0,
            Title: '',
            Description: '',
            WorkDate:new Date(),
            WorkTime: new Date(),
            DepartmentId: 0,
            DepartmentName: '',
            WorkDay: new Date(),
            DepartmentData: { Display: '', Value: '' },
            WorkCycle:''
            
        }

        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.id = id;
        $scope.storage = {};

        if (id && id > 0) {
            WorkProfileService.GetByIdWorkProfile(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    var d = new Date();
                    var gettime = '';
                    var mytime = d;
                   
                    
                    if (result.data.DataList.WorkTime != null) {
                        d.setHours($filter('date')(result.data.DataList.WorkTime, "HH:mm").Hours);
                        d.setMinutes($filter('date')(result.data.DataList.WorkTime, "HH:mm").Minutes);
                        gettime = result.data.DataList.WorkTime.Hours == 0 && result.data.DataList.WorkTime.Minutes == 0 ? new Date() : mytime;
                    } else {
                        gettime = new Date();
                    }
                    $scope.objWorkProfile = {
                        WorkProfileId: result.data.DataList.WorkProfileId,
                        Title: result.data.DataList.Title,
                        Description: result.data.DataList.Description,
                        WorkDate: $filter('mydate')(result.data.DataList.WorkDate),
                        WorkTime: gettime,
                        DepartmentId: result.data.DataList.DepartmentId,
                        DepartmentName: result.data.DataList.DepartmentName,
                        DepartmentData: { Display: result.data.DataList.DepartmentName, Value: result.data.DataList.DepartmentId },
                        WorkCycle: result.data.DataList.WorkCycle

                    }
                    $scope.storage = angular.copy($scope.objWorkProfile);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        function ResetForm() {
            //WorkProfileId,DepartmentId,DepartmentName,Title,Description,WorkTime,IsActive
            $scope.objWorkProfile = {
                WorkProfileId: 0,
                Title: '',
                Description: '',
                WorkTime: new Date(),
                DepartmentId: 0,
                DepartmentName: '',
                WorkDay: '',
                DepartmentData: { Display: '', Value: '' },
                WorkDate: new Date(),
                
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormWorkProfileInfo)
                $scope.$parent.FormWorkProfileInfo.$setPristine();
        }

        $scope.CreateUpdate = function (data) {
            data.DepartmentId = data.DepartmentData.Value;
            data.DepartmentName = data.DepartmentData.Display;
            data.WorkTime = $filter('date')(data.WorkTime, "HH:mm");
            WorkProfileService.CreateUpdateWorkProfile(data).then(function (result) {
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

            if ($scope.objWorkProfile.WorkProfileId > 0) {
                $scope.objWorkProfile = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.dateOptions = {
            formatYear: 'yy',
            //minDate: new Date(2016, 8, 5),
            //maxDate: new Date(2099, 5, 22),
            startingDay: 1
        };
        $scope.close = function () {
            $uibModalInstance.close();
        };

        

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'WorkProfileService', 'id', 'isdisable', '$filter']
    angular.module('CRMApp.Controllers').filter("mydate", function () {
        var re = /\/Date\(([0-9]*)\)\//;
        return function (x) {
            var m = x.match(re);
            if (m) return new Date(parseInt(x.substr(6)));
            else return null;
        };
    });
})()
