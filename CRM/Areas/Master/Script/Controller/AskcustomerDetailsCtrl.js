(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("AskcustomerDetailsCtrl", [
         "$scope", "AskcustomerDetailsService", "$filter", "$uibModal", "CountryService",
         AskcustomerDetailsCtrl]);

    function AskcustomerDetailsCtrl($scope, AskcustomerDetailsService, $filter, $uibModal, CountryService) {
        $scope.id = 0;
        $scope.MobCodeData = [];
        $scope.Add = function () {
            var _isdisable = 0;
            var objAskCustomer = [];
            $scope.objAskCustomer = {
                AskCustId: 0,
                SourceId: '',
                SourceData: { Display: '', Value: '' },
                Name: '',
                Mobileno: '',
                Email: '',
                //Mobileno: '',
                //Email: '',
                Requirement:''
            }

            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    AskcustomerDetailsService: function () { return AskcustomerDetailsService; },
                    AskcustomerDetailsCtrl: function () { return AskcustomerDetailsCtrl; },
                    objAskCustomer: function () { return objAskCustomer; },
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
               { "title": "Sr.", "field": "RowNumber", filter: false, sort: false, show: true },
               //{ "title": "SourceId", "field": "SourceId", filter: false, visible: false },
               {
                   "title": "Inquiry Date", "field": "Date", sortable: "Date", filter: { Date: "date" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<span >{{ConvertDate(row.Date,\'dd/mm/yyyy\')}}</span>'
                       return $scope.getHtml(element);
                   }
               },
               { "title": "Source", "field": "SourceName", sortable: "SourceName", filter: { SourceName: "text" }, show: true },
               { "title": "Buyer Name/Remark", "field": "Name", filter: { Name: "text" }, sortable: "Name", show: true },
               { "title": "Requirement", "field": "Requirement", filter: { Requirement: "text" }, sortable: "Requirement", show: true },
               { "title": "Mobile no", "field": "Mobileno", sort: true, sortable: "Mobileno", filter: { Mobileno: "text" }, show: true },
               { "title": "Email", "field": "Email", filter: { Email: "text" }, sortable: "Email", show: true },
               { "title": "Add By", "field": "UserName", filter: { UserName: "text" }, sortable: "UserName", show: true },
               {
                   "title": "Add on", "field": "Createddate", sortable: "Createddate", filter: { Createddate: "text" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<span >{{ConvertDate(row.Createddate,\'dd/mm/yyyy\')  +"      "+ ConvertTime(row.Createdtime) }}</span>'
                       return $scope.getHtml(element);
                   }
               },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       //'render': '<button class="btn btn-success" data-ng-click="$parent.$parent.$parent.Edit(row)">Edit</button><button class="btn btn-success" data-ng-click="$parent.$parent.$parent.Delete(row.AccountEntryId)">Delete</button> '
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil"></i></a>' +
                   //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.AccountId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                   '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'+
                   '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.VcCard(row.AskCustId)" data-uib-tooltip="VCard"><i class="fa fa-download" ></i></a>'
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'AskCustId': 'asc' }

        }

        $scope.Edit = function (data) {

            var _isdisable = 0;
            var objAskCustomer = {
                AskCustId: data.AskCustId,
                SourceId: data.SourceId,
                SourceData: { Display: data.SourceName, Value: data.SourceId },
                Name: data.Name,
                Mobileno: data.Mobileno,
                Date: $filter('mydate')(data.Date),
                Email: data.Email,
                Requirement:data.Requirement
            }
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    AskcustomerDetailsService: function () { return AskcustomerDetailsService; },
                    AskcustomerDetailsCtrl: function () { return AskcustomerDetailsCtrl; },
                    objAskCustomer: function () { return objAskCustomer; },
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
            var objAskCustomer = {
                AskCustId: data.AskCustId,
                SourceId: data.SourceId,
                SourceData: { Display: data.SourceName, Value: data.SourceId },
                Name: data.Name,
                Mobileno: data.Mobileno,
                Date: $filter('mydate')(data.Date),
                Email: data.Email,
                Requirement:data.Requirement
            }
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    AskcustomerDetailsService: function () { return AskcustomerDetailsService; },
                    AskcustomerDetailsCtrl: function () { return AskcustomerDetailsCtrl; },
                    objAskCustomer: function () { return objAskCustomer; },
                    isdisable: function () { return _isdisable; }
                }
            });
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
            });
        }
        $scope.VcCard = function (id) {
            window.location.href = "/Master/AskcustomerDetails/GetVCCard/" + id;

        }
        //$scope.Delete = function (WorkRemindId) {

        //    AskcustomerDetailsService.DeleteWorkReminder(WorkRemindId).then(function (result) {
        //        if (result.data.ResponseType == 1) {
        //            toastr.success(result.data.Message);
        //            $scope.refreshGrid()
        //        } else {
        //            toastr.error(result.data.Message, 'Opps, Something went wrong');
        //        }
        //    }, function (errorMsg) {
        //        toastr.error(errorMsg, 'Opps, Something went wrong');
        //    })
        //}
    }

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, AskcustomerDetailsService, AskcustomerDetailsCtrl, $filter, objAskCustomer, Upload, isdisable, CountryService) {
        $scope.MobCodeData = [];


        $scope.dateOptions = {
            formatYear: 'yy',
            minDate: new Date(1950, 1, 1),
            startingDay: 1
        }
        $scope.setvalue = function () {
            CountryService.GetCountryFlag().then(function (result) {
                $scope.MobCodeData = angular.copy(result);
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.objAskCustomer = $scope.objAskCustomer || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};
        $scope.callback = function (data) {
            console.log(data);
        }

        if (objAskCustomer.AskCustId && objAskCustomer.AskCustId > 0) {
            $scope.objAskCustomer = {
                AskCustId: objAskCustomer.AskCustId,
                SourceId: objAskCustomer.SourceId,
                SourceData: { Display: objAskCustomer.SourceData.Display, Value: objAskCustomer.SourceId },
                Name: objAskCustomer.Name,
                Mobileno: objAskCustomer.Mobileno,
                Date: objAskCustomer.Date,
                Email: objAskCustomer.Email,
                Requirement:objAskCustomer.Requirement
            }
            $scope.storage = angular.copy($scope.objstate);
            //$scope.objCategory = result.data.DataList.CategoryName;
            $scope.mobile = (objAskCustomer.Mobileno != '' && objAskCustomer.Mobileno != null) ? objAskCustomer.Mobileno.split(",") : [];
            $scope.emailid = (objAskCustomer.Email != '' && objAskCustomer.Email != null) ? objAskCustomer.Email.split(",") : [];
        } else {
            //toastr.error(objstate.data.Message, 'Opps, Something went wrong');
            ResetForm();
        }

        function ResetForm() {
            $scope.objAskCustomer = {
                AskCustId: 0,
                SourceId: 0,
                SourceData: { Display: '', Value: '' },
                Name: '',
                Mobileno: '',
                Date: '',
                Email: '',
                Requirement:''
            }
            $scope.Email = '';
            $scope.Mobileno = '';
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormAskCustomerInfo)
                $scope.$parent.FormAskCustomerInfo.$setPristine();

        }

        $scope.CreateUpdate = function (data) {
            data.Mobileno = this.mobile != undefined ? this.mobile.toString() : "";
            data.Email = this.emailid != undefined ? this.emailid.toString() : "";
            //data.Mobileno = this.mobile.toString();
            //data.Email = this.emailid.toString();
            data.SourceId = data.SourceData.Value;
            data.SourceName = data.SourceData.Display;
            AskcustomerDetailsService.CreateUpdateAskcustomerDetails(data).then(function (result) {
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
            var objAskCustomer = {
                AskCustId: data.AskCustId,
                SourceId: data.SourceId,
                SourceData: { Display: data.SourceData, Value: data.SourceId },
                Name: data.Name,
                Mobileno: data.Mobileno,
                Date: data.Date,
                Email: data.Email,
                Requirement:data.Requirement
            }
            AskcustomerDetailsService.CreateUpdateAskcustomerDetails(objAskCustomer).then(function (result) {
                $uibModalInstance.close();
                toastr.success(result.data.Message);
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
            //}
        }

        $scope.Reset = function () {
            if ($scope.objAskCustomer.AskCustId > 0) {
                $scope.objAskCustomer = angular.copy($scope.storage);
            } else {
                ResetForm();
            }

        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'AskcustomerDetailsService', 'AskcustomerDetailsCtrl', '$filter', 'objAskCustomer', 'Upload', 'isdisable', 'CountryService']

})()