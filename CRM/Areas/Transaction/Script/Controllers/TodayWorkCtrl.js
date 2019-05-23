(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("TodayWorkCtrl", [
         "$scope", "TodayWorkService", "$filter", "$uibModal",
         TodayWorkCtrl]);

    function TodayWorkCtrl($scope, TodayWorkService, $filter, $uibModal) {

        $scope.setTab = function () {
            $scope.openTab('click', 'TaskDetails');
        }

        $scope.openTab = function (evt, tabName) {
            // Declare all variables
            var bln = true;
            var dataerror = true;
            if (bln == true) {
                var i, tabcontent, tablinks;
                // Get all elements with class="tabcontent" and hide them
                tabcontent = document.getElementsByClassName("tabcontent");
                for (i = 0; i < tabcontent.length; i++) {
                    tabcontent[i].style.display = "none";
                }
                // Get all elements with class="tablinks" and remove the class "active"
                tablinks = document.getElementsByClassName("tablinks");
                for (i = 0; i < tablinks.length; i++) {
                    tablinks[i].className = tablinks[i].className.replace("active", "");
                }
                // Show the current tab, and add an "active" class to the link that opened the tab
                document.getElementById(tabName).style.display = "block";
                $scope.tabname = tabName;
                $scope.objProduct.EditProImgId = 0;
                $scope.objProduct.EditProCatalogueId = 0;
            }
            if ($scope.issuccess == false && $scope.msg != '') {
                toastr.error($scope.msg);
            }

        }

        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };

        $scope.TaskgridObj = {
            columnsInfo: [
               //{ "title": "TaskId", "data": "TaskId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", filter: false, show: true },
               { "title": "Task No", "field": "TaskNo", filter: true, sortable: "TaskNo" },
               { "title": "Assign From", "field": "FromUser", sortable: "FromUser", filter: { FromUser: "text" }, show: true },
               { "title": "CreatedBy", "field": "CreatedBy", filter: { CreatedBy: "text" }, show: false },
               //{ "title": "Work/Task", "field": "Task", sortable: "Task", filter: { Task: "text" } },
               {
                   "title": "Work/Task", "field": "Task", sortable: "Task", filter: { Task: "text" }, show: true,
                   'cellTemplte': function ($scope, row){
                       var element = '<span data-uib-tooltip="{{row.Task}}" ng-bind="LimitString(row.Task,100)"></span>'
                       return $scope.getHtml(element);
                   }
               },
               { "title": "Task Group", "field": "TaskGroupName", sortable: "TaskGroupName", filter: { TaskGroupName: "text" }, show: true },
               { "title": "Priority", "field": "PriorityName", sortable: "PriorityName", show: true, filter: { PriorityName: "text" } },
               
                   {
                       "title": "Last Note/Remark", "field": "Note", sortable: "Note", filter: { Note: "text" }, show: true,
                       'cellTemplte': function ($scope, row) {
                           var element = '<p data-uib-tooltip="{{row.Note}}" ng-bind="LimitString(row.Note,100)">'
                           return $scope.getHtml(element);
                       }
                   },
                   {
                       "title": "Last Follow Up", "field": "NextFollowDate", sortable: "NextFollowDate", filter: { NextFollowDate: "date" }, show: true,
                       'cellTemplte': function ($scope, row) {
                           var element = '<span >{{ConvertDate(row.NextFollowDate,\'dd/mm/yyyy\') +"      "+ ConvertTime(row.NextFollowTime)}}</span>'
                           return $scope.getHtml(element);
                       }
                   },
                    {
                        "title": "Next Follow Up", "field": "lastFollowdate", sortable: "lastFollowdate", filter: { lastFollowdate: "date" }, show: true,
                        'cellTemplte': function ($scope, row) {
                            var element = '<span >{{ConvertDate(row.lastFollowdate,\'dd/mm/yyyy\') +"      "+ ConvertTime(row.lastFollowTime)}}</span>'
                            return $scope.getHtml(element);
                        }
                    },
               //{ "title": "Review", "field": "Review", sortable: "Review", filter: { Review: "text" }, show: false },
               { "title": "Assign To", "field": "AssignTo", sortable: "AssignTo", filter: { AssignTo: "text" }, show: true },
               { "title": "Current Status", "field": "FollowUpTaskStatus", sortable: "FollowUpTaskStatus", filter: { FollowUpTaskStatus: "text" }, show: true },
               //dataType: "dropdown", mode: "TaskStatusMaster" 
               //{ "title": "TaskType", "field": "TaskType", sortable: "TaskType", filter: { TaskType: "text" }, show: false },
               { "title": "Group By", "field": "GroupBy", sortable: "GroupBy", filter: { GroupBy: "text" }, show: false },
               { "title": "To User", "field": "AssignTo", sortable: "AssignTo", filter: { AssignTo: "text" }, show: false },
               //{ "title": "TaskGroupId", "field": "TaskGroupId", sortable: "TaskGroupId", filter: { TaskGroupId: "text" }, show: false },
               //{ "title": "Note/Remark", "field": "Note", sortable: "Note", filter: { Note: "text" }, show: true },
               //{
               //    "title": "Note/Remark", "field": "Note", sortable: "Note", filter: { Note: "text" }, show: true,
               //    'cellTemplte': function ($scope, row){
               //        var element = '<span data-uib-tooltip="{{row.Note}}" ng-bind="LimitString(row.Note,100)"></span>'
               //        return $scope.getHtml(element);
               //    }
               //},
               //{
               //    "title": "Follow DateTime", "field": "FollowDate", sortable: "FollowDate", filter: { FollowDate: "date" }, show: false,
               //    'cellTemplte': function ($scope, row){
               //        var element = '<span >{{ConvertDate(row.FollowDate,\'dd/mm/yyyy\') +"      "+ ConvertTime(row.FollowTime)}}</span>'
               //        return $scope.getHtml(element);
               //    }
               //},
            ],
            Sort: { 'TaskId': 'asc' }
        }

        $scope.InquirygridObj = {
            columnsInfo: [
               //{ "title": "Inq Id", "field": "InqId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", filter: false, show: true },
               { "title": "Inquiry Number", "field": "InqNo", sortable: "InqNo", filter: { InqNo: "text" }, show: true },
               {
                   "title": "Date", "field": "InqDate", sortable: "InqDate", filter: { InqDate: "date" }, show: true,
                   'cellTemplte': function ($scope, row){
                       var element = '<span ng-bind="ConvertDate(row.InqDate,\'dd/mm/yyyy\')"></span>'
                       return $scope.getHtml(element);
                   }
               },
               //{ "title": "Source Id", "field": "SourceId", sortable: "SourceId", filter: true, visible: false },
               { "title": "Source", "field": "SourceName", sortable: "SourceName", filter: { SourceName: "text" }, show: true },
               { "title": "User Name", "field": "AssignFromUser", sortable: "AssignFromUser", filter: { AssignFromUser: "text" }, show: false },
               {
                   "title": "Buyer Name", "field": "BuyerName", sortable: "BuyerName", filter: { BuyerName: "text" }, show: true,
                   'cellTemplte': function ($scope, row){
                       var element = '<span>{{row.BuyerName}}</br><b>{{row.CityName}}</b></span>'
                       return $scope.getHtml(element);
                   }
               },
               //{ "title": "City Id", "field": "CityId", sortable: true, filter: true, visible: false },
               { "title": "District", "field": "CityName", sortable: "CityName", filter: { CityName: "text" }, show: true },
               { "title": "State", "field": "StateName", sortable: "StateName", filter: { StateName: "text" }, show: false },
               //{ "title": "State Id", "field": "StateId", sortable: true, filter: true, visible: false },
               //{ "title": "Country Id", "field": "CountryId", sortable: true, filter: true, visible: false },
               { "title": "Country", "field": "CountryName", sortable: "CountryName", filter: { CountryName: "text" }, show: false },
               { "title": "Mobile No", "field": "MobileNo", sortable: "MobileNo", filter: { MobileNo: "text" }, show: false },
               { "title": "Email", "field": "Email", sortable: "Email", filter: { Email: "text" }, "render": "<p class='MailTo' ng-bind-html='getHtml($parent.$parent.$parent.FormatEmail(row.Email))'>", show: false },
               {
                   "title": "Requirement", "field": "Requirement", sortable: "Requirement", filter: { Requirement: "text" }, show: true,
                   'cellTemplte': function ($scope, row){
                       var element = '<span data-uib-tooltip="{{row.Requirement}}" ng-bind="LimitString(row.Requirement,100)"></span>'
                       return $scope.getHtml(element);
                   }
               },
               { "title": "Assign To", "field": "AssignToUser", sortable: "AssignToUser", filter: { AssignToUser: "text" }, show: false },
               //{ "title": "FollowStatus", "field": "FollowStatus", sortable: "FollowStatus", filter: { FollowStatus: "text" }, show: true },
               { "title": "Status", "field": "TaskStatus", sortable: "TaskStatus", filter: { TaskStatus: "text" }, show: false },
               {
                   "title": "Note/Remark", "field": "Remark", sortable: "Remark", filter: { Remark: "text" }, show: false,
                   'cellTemplte': function ($scope, row){
                       var element = '<span data-uib-tooltip="{{row.Remark}}" ng-bind="LimitString(row.Remark,100)"></span>'
                       return $scope.getHtml(element);
                   }
               },
               { "title": "Address", "field": "Address", sortable: "Address", filter: { Address: "text" }, show: true }
            ],
            Sort: { 'InqId': 'asc' }
        }

    }

    angular.module('CRMApp.Controllers').filter("mydate", function () {
        var re = /\/Date\(([0-9]*)\)\//;
        return function (x) {
            var m = x.match(re);
            if (m) return new Date(parseInt(x.substr(6)));
            else return null;
        };
    });

})()


