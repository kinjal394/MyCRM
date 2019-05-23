

(function () {
    "use strict";
    angular.module("CRMApp.Controllers", [])
        .controller("LoginController", [
         "$scope", "LoginService",
         LoginController]);

    function LoginController($scope, CityService) {
        $scope.setDirectiveRefresh = function (refreshGrid) {

            $scope.refreshGrid = refreshGrid;
        };

        $scope.gridObj = {
            columnsInfo: [
               //{ "title": "Login Id", "data": "Id", filter: false, visible: false },
                { "title": "Sr.", "field": "RowNumber",  show: true, },
               //{ "title": "User Id", "field": "UserId", filter: false, visible: false },
               { "title": "User Name", "field": "UserName", sortable: "UserName", filter: { UserName: "text" }, show: true, },
               {
                   "title": "Month", "field": "MonthYear", sortable: "MonthYear", filter: { MonthYear: "text" }, show: true,
               },
               {
                   "title": "Login Date", "field": "LoginTime", sortable: "LoginTime", filter: { LoginTime: "date" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element =  '<p ng-bind="ConvertDate(row.LoginTime,\'dd/mm/yyyy\')">'
                       return $scope.getHtml(element);
                   }
               },
                {
                    "title": "Login Time", "field": "LogTime", sortable: "LogTime", filter: { LogTime: "text" }, show: true,
                    'cellTemplte': function ($scope, row) {
                        var element =  '<p ng-bind="ConvertTime(row.LogTime)">'
                        return $scope.getHtml(element);
                    }
                },
               { "title": "Device Type", "field": "DeviceTypeName", sortable: "DeviceTypeName", filter: { DeviceTypeName: "text" }, show: true, },
               { "title": "Browser", "field": "Browser", sortable: "Browser", filter: { Browser: "text" }, show: true, },
               { "title": "IP Address", "field": "IP", sortable: "IP", filter: { IP: "text" }, show: true, }
               //{
               //    "title": "Action", sort: false, filter: false,
               //    'render': '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
               //          '<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.CityId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '

               //    //'render': '<button  data-ng-click="$parent.$parent.$parent.Edit(row.CityId)">Edit</button><button data-ng-click="$parent.$parent.$parent.Delete(row.CityId)">Delete</button> '
               //}
            ],
            Sort: { 'Id': 'asc' }
            //modeType: "StateMaster",
            //Title: "State List"
        }
        $scope.gridatndObj = {
            columnsInfo: [
               //{ "title": "Attendance Id", "field": "AtId", filter: false, visible: false },
                { "title": "Sr.", "field": "RowNumber", show: true, },
               //{ "title": "User Id", "field": "UserId", filter: false, visible: false },
               { "title": "User", "field": "UserName", sortable: "UserName", filter: { UserName: "text" }, show: true, },
               { "title": "Month", "field": "Month", sortable: "Month", filter: { Month: "text" }, show: true, },
               {
                   "title": "Date", "field": "OnDate", sortable: "OnDate", filter: { OnDate: "date" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<p ng-bind="ConvertDate(row.OnDate,\'dd/mm/yyyy\')">'
                       return $scope.getHtml(element);
                   }
               },
               {
                   "title": "Work Start Time", "field": "WorkStartTime", sortable: "WorkStartTime", filter: { WorkStartTime: "text" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<p ng-bind="ConvertTime(row.WorkStartTime)">'
                       return $scope.getHtml(element);
                   }
               },
               { "title": "Work Start IP", "field": "WorkStartIP", sortable: "WorkStartIP", filter: { WorkStartIP: "text" }, show: true, },
               {
                   "title": "Work End Time", "field": "WorkEndTime", sortable: "WorkEndTime", filter: { WorkEndTime: "text" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element =  '<p ng-bind="ConvertTime(row.WorkEndTime)">'
                       return $scope.getHtml(element);
                   }
               },
               { "title": "Work End IP", "field": "WorkEndIP", sortable: "WorkEndIP", filter: { WorkEndIP: "text" }, show: true, },
               {
                   "title": "Break Start Time", "field": "LunchStartTime", sortable: "LunchStartTime", filter: { LunchStartTime: "text" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element =  '<p ng-bind="ConvertTime(row.LunchStartTime)">'
                       return $scope.getHtml(element);
                   }
               },
               { "title": "Break Start IP", "field": "LunchStartIP", sortable: "LunchStartIP", filter: { LunchStartIP: "text" }, show: true, },
               {
                   "title": "Break End Time", "field": "LunchEndTime", sortable: "LunchEndTime", filter: { LunchEndTime: "text" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<p ng-bind="ConvertTime(row.LunchEndTime)">'
                       return $scope.getHtml(element);
                   }
               },
               { "title": "Break End IP", "field": "LunchEndIP", sortable: "LunchEndIP", filter: { LunchEndIP: "text" }, show: true, },
               {
                   "title": "Total Work Time", "field": "TotalWork", sortable: "TotalWork", filter: { TotalWork: "text" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element =  '<p ng-bind="ConvertTime(row.TotalWork)">'
                       return $scope.getHtml(element);
                   }
               },
               {
                   "title": "Total Break Time", "field": "TotalLunch", sortable: "TotalLunch", filter: { TotalLunch: "text" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element =  '<p ng-bind="ConvertTime(row.TotalLunch)">'
                       return $scope.getHtml(element);
                   }
               },
               //{
               //    "title": "Action", sort: false, filter: false,
               //    'render': '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
               //          '<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.CityId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '

               //    //'render': '<button  data-ng-click="$parent.$parent.$parent.Edit(row.CityId)">Edit</button><button data-ng-click="$parent.$parent.$parent.Delete(row.CityId)">Delete</button> '
               //}
            ],
            Sort: { 'AtId': 'asc' }
            //modeType: "StateMaster",
            //Title: "State List"
        }
        $scope.gridworktaskObj = {
            columnsInfo: [
               //{ "title": "DailyWork Id", "data": "DailyWorkId", filter: false, visible: false },
                { "title": "Sr.", "field": "RowNumber", show: true, },
               //{ "title": "User Id", "field": "UserId", filter: false, visible: false },
               { "title": "User", "field": "Name", sortable: "Name", filter: { Name: "text" }, show: true, },
               {
                   "title": "Date", "field": "Date", sortable: "Date", filter: { Date: "date" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element =  '<p ng-bind="ConvertDate(row.Date,\'dd/mm/yyyy\')">'
                       return $scope.getHtml(element);
                   }
               },
               { "title": "Task", "field": "Task", sortable: "Task", filter: { Task: "text" }, show: true, },
               //{ "title": "Attandance", "field": "Attandance", sort: true, filter: true, },
               { "title": "Status", "field": "TaskStatus", sortable: "TaskStatus", filter: { TaskStatus: "text" }, show: true, },
               //{
               //    "title": "Work Start Time", "data": "WorkStartTime", sort: true, filter: true,
               //    "render": '<p ng-bind="ConvertTime(row.WorkStartTime)">'
               //},
               //{
               //    "title": "Work End Time", "data": "WorkEndTime", sort: true, filter: true,
               //    "render": '<p ng-bind="ConvertTime(row.WorkEndTime)">'
               //},
               //{
               //    "title": "Lunch Start Time", "data": "LunchStartTime", sort: true, filter: true,
               //    "render": '<p ng-bind="ConvertTime(row.LunchStartTime)">'
               //},
               //{
               //    "title": "Lunch End Time", "data": "LunchEndTime", sort: true, filter: true,
               //    "render": '<p ng-bind="ConvertTime(row.LunchEndTime)">'
               //},
               //{
               //    "title": "Action", sort: false, filter: false,
               //    'render': '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
               //          '<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.CityId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '

               //    //'render': '<button  data-ng-click="$parent.$parent.$parent.Edit(row.CityId)">Edit</button><button data-ng-click="$parent.$parent.$parent.Delete(row.CityId)">Delete</button> '
               //}
            ],
            Sort: { 'DailyWorkId': 'asc' }
            //modeType: "StateMaster",
            //Title: "State List"
        }
    }
})()