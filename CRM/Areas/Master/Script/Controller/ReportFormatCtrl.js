(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("ReportFormatCtrl", [
         "$scope", "ReportFormatService", "$filter", "$uibModal","$sce",
         ReportFormatCtrl]);

    function ReportFormatCtrl($scope, ReportFormatService, $filter, $uibModal, $sce) {
        $scope.id = 0;
        $scope.Add = function () {
            var _isdisable = 0;
            var objReport = [];
            $scope.objReport = {
                RotFormatId: 0,
                CompanyCode: '',
                CompanyHeader: '',
                CompanyFooter: ''
            }

            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    ReportFormatService: function () { return ReportFormatService; },
                    ReportFormatCtrl: function () { return ReportFormatCtrl; },
                    objReport: function () { return objReport; },
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
               //{ "title": "Agency Type Id", "field": "AgencyTypeId", filter: false, show: false },
               { "title": "Sr.", "field": "RowNumber", filter: false, show: true },
               { "title": "Company Code", "field": "CompanyCode", sortable: "CompanyCode", filter: { CompanyCode: "text" }, show: true },
               //{ "title": "Company Header", "field": "CompanyHeader", sortable: "CompanyHeader", filter: { CompanyHeader: "text" }, show: true },
               //{ "title": "Company Footer", "field": "CompanyFooter", sortable: "CompanyFooter", filter: { CompanyFooter: "text" }, show: true },
               {
                   "title": "Company Header", "field": "CompanyHeader", sortable: "CompanyHeader", filter: { CompanyHeader: "text" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a href="#" style="color:black;" uib-tooltip-html="$parent.$parent.$parent.$parent.$parent.$parent.tooltipContent" ng-mouseover="$parent.$parent.$parent.$parent.$parent.$parent.getTooltipHtmlContent(row.CompanyHeader)" ng-bind-html="getHtml(LimitString(row.CompanyHeader,200))">CompanyHeader!</a>'
                       return $scope.getHtml(element);
                   }
               },
               {
                   "title": "Company Footer", "field": "CompanyFooter", sortable: "CompanyFooter", filter: { CompanyFooter: "text" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a href="#" style="color:black;" uib-tooltip-html="$parent.$parent.$parent.$parent.$parent.$parent.tooltipContent" ng-mouseover="$parent.$parent.$parent.$parent.$parent.$parent.getTooltipHtmlContent(row.CompanyFooter)" ng-bind-html="getHtml(LimitString(row.CompanyFooter,200))">CompanyFooter!</a>'
                       return $scope.getHtml(element);
                   }
               },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                            //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.$parent.$parent.$parent.Delete(row.AgencyTypeId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                            '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { "RotFormatId": "asc" }
        }

        $scope.getTooltipHtmlContent = function (tooltip) {
            $scope.tooltipContent = $sce.trustAsHtml(tooltip);
            return $scope.tooltipContent;
        }

        $scope.Edit = function (data) {

            var _isdisable = 0;
            var objReport = {
                RotFormatId: data.RotFormatId,
                CompanyCode: data.CompanyCode,
                CompanyHeader: data.CompanyHeader,
                CompanyFooter: data.CompanyFooter
            }
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    ReportFormatService: function () { return ReportFormatService; },
                    ReportFormatCtrl: function () { return ReportFormatCtrl; },
                    objReport: function () { return objReport; },
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
            var objReport = {
                RotFormatId: data.RotFormatId,
                CompanyCode: data.CompanyCode,
                CompanyHeader: data.CompanyHeader,
                CompanyFooter: data.CompanyFooter
            }
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    ReportFormatService: function () { return ReportFormatService; },
                    ReportFormatCtrl: function () { return ReportFormatCtrl; },
                    objReport: function () { return objReport; },
                    isdisable: function () { return _isdisable; }
                }
            });
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
            });
        }
        $scope.Delete = function (RotFormatId) {

            ReportFormatService.DeleteReport(RotFormatId).then(function (result) {
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

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, ReportFormatService, ReportFormatCtrl, $filter, objReport, Upload, isdisable) {
    
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.objReport = $scope.objReport || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};
        $scope.callback = function (data) {
            console.log(data);
        }

        if (objReport.RotFormatId && objReport.RotFormatId > 0) {
            $scope.objReport = {

                RotFormatId: objReport.RotFormatId,
                CompanyCode: objReport.CompanyCode,
                CompanyHeader: objReport.CompanyHeader,
                CompanyFooter: objReport.CompanyFooter,
            }
            $scope.storage = angular.copy($scope.objstate);
            //$scope.objCategory = result.data.DataList.CategoryName;
        } else {
            //toastr.error(objstate.data.Message, 'Opps, Something went wrong');
            ResetForm();
        }

        function ResetForm() {
            $scope.objReport = {
                RotFormatId: 0,
                CompanyCode: '',
                CompanyHeader: '',
                CompanyFooter: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormWorkRemindInfo)
                $scope.$parent.FormWorkRemindInfo.$setPristine();

        }

        $scope.CreateUpdate = function (data) {
            ReportFormatService.CreateUpdateReport(data).then(function (result) {
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
            var objReport = {
                RotFormatId: data.RotFormatId,
                CompanyCode: data.CompanyCode,
                CompanyHeader: data.CompanyHeader,
                CompanyFooter: data.CompanyFooter
            }
            ReportFormatService.CreateUpdateReport(objReport).then(function (result) {
                $uibModalInstance.close();
                toastr.success(result.data.Message);
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
            //}
        }

        $scope.Reset = function () {
            if ($scope.objReport.RotFormatId > 0) {
                $scope.objReport = angular.copy($scope.storage);
            } else {
                ResetForm();
            }

        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'ReportFormatService', 'ReportFormatCtrl', '$filter', 'objReport', 'Upload', 'isdisable']

})()