(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("AccountEntryController", [
         "$scope", "AccountEntryService", "$filter", "$uibModal",
         AccountEntryController]);

    function AccountEntryController($scope, AccountEntryService, $filter, $uibModal) {
        $scope.id = 0;
        $scope.Add = function () {
            var _isdisable = 0;
            var objaccount = [];
            $scope.objaccount = {
                AccountId: 0,
                PartyName: '',
                LegerId: 0,
                ITRHeadData: { Display: '', Value: '' },
                LegerData: { Display: '', Value: '' },
                LegerHeadId: 0,
                LegerHeadData: { Display: '', Value: '' },
                CurrencyId: 0,
                CurrencyData: { Display: '', Value: '' },
                TaxId: 0,
                TaxData: { Display: '', Value: '' },
                BillDate: '',
                BillNo: '',
                Amount: 0,
                ExchangeRate: 0,
                INRAmount: 0,
                BillPdf: '',
                TransactionSlip: '',
                Photo: '',
                AccountEntryType: '',
                Remark: ''
            }

            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    AccountEntryService: function () { return AccountEntryService; },
                    AccountEntryController: function () { return AccountEntryController; },
                    objaccount: function () { return objaccount; },
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

               //{ "title": "Account Id", "data": "AccountId", filter: false, visible: false },
               //{ "title": "Currency Id", "data": "CurrencyId", filter: false, visible: false },
               //{ "title": "Leger Id", "data": "LegerId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "Account Entry Type", "field": "Account_Entry_Type", sortable: "Account_Entry_Type", filter: { Account_Entry_Type: "text" }, show: true, },
               //{ "title": "ITRId", "field": "ITRId", filter: false, visible: false },
               { "title": "ITR Head", "field": "ITRName", sortable: "ITRName", filter: { ITRName: "text" }, show: true, },
               //{ "title": "LegerHeadId", "field": "LegerHeadId", filter: false, visible: false },
               { "title": "Leger Head", "field": "LegerHeadName", sortable: "LegerHeadName", filter: { LegerHeadName: "text" }, show: true, },
               { "title": "Leger Name", "field": "LegerName", sortable: "LegerName", filter: { LegerName: "text" }, show: true, },
               { "title": "Party Name", "field": "PartyName", sortable: "PartyName", filter: { PartyName: "text" }, show: true, },
               {
                   "title": "Bill Date", "field": "BillDate", sortable: "BillDate", filter: { BillDate: "date" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<p ng-bind="ConvertDate(row.BillDate,\'dd/mm/yyyy\')">'
                       return $scope.getHtml(element);
                   }
               },
               { "title": "Bill No", "field": "BillNo", sortable: "BillNo", filter: { BillNo: "text" }, show: true, },
               { "title": "INR Amount", "field": "INRAmount", sortable: "INRAmount", filter: { INRAmount: "text" }, show: true, },
               { "title": "Amount", "field": "Amount", sortable: "Amount", filter: { Amount: "text" }, show: false, },
               { "title": "Currency Name", "field": "CurrencyName", sortable: "CurrencyName", filter: { CurrencyName: "text" }, show: false, },
               { "title": "Exchange Rate", "field": "ExchangeRate", sortable: "ExchangeRate", filter: { ExchangeRate: "text" }, show: false, },
               //{ "title": "TaxId", "field": "TaxId", sortable: "TaxId", filter: { TaxId: "text" }, show: false, },
                { "title": "TaxValue", "field": "TaxValue", sortable: "TaxValue", filter: { TaxValue: "text" }, show: false, },
                { "title": "TaxName", "field": "TaxName", sortable: "TaxName", filter: { TaxName: "text" }, show: false, },

               //{ "title": "Remark", "data": "Remark", sort: true, filter: true, },
               {
                   "title": "Remark", "field": "Remark", sortable: "Remark", filter: { Remark: "text" }, show: false,
                   'cellTemplte': function ($scope, row) {
                       var element = '<p data-uib-tooltip="{{row.Remark}}" ng-bind="LimitString(row.Remark,100)">'
                       return $scope.getHtml(element);
                   }
               },
               {
                   "title": "Action", show: true,
                   //'render': '<button class="btn btn-success" data-ng-click="$parent.$parent.$parent.Edit(row)">Edit</button><button class="btn btn-success" data-ng-click="$parent.$parent.$parent.Delete(row.AccountEntryId)">Delete</button> '
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil"></i></a>' +
                   //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.AccountId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                   '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'AccountId': 'asc' }

        }

        $scope.Edit = function (data) {

            var _isdisable = 0;
            var objaccount = {
                AccountId: data.AccountId,
                PartyName: data.PartyName,
                LegerId: data.LegerId,
                ITRHeadData: { Display: data.ITRName, Value: data.ITRId },
                LegerData: { Display: data.LegerName, Value: data.LegerId },
                LegerHeadId: data.LegerHeadId,
                LegerHeadData: { Display: data.LegerHeadName, Value: data.LegerHeadId },
                CurrencyId: data.CurrencyId,
                CurrencyData: { Display: data.CurrencyName, Value: data.CurrencyId },
                TaxId: data.TaxId,
                TaxData: { Display: data.TaxName, Value: data.TaxId },
                TaxValue: data.TaxValue,
                BillDate: $filter('mydate')(data.BillDate),
                BillNo: data.BillNo,
                Amount: data.Amount,
                ExchangeRate: data.ExchangeRate,
                INRAmount: data.INRAmount,
                BillPdf: data.BillPdf,
                TransactionSlip: data.TransactionSlip,
                Photo: data.Photo,
                AccountEntryType: data.AccountEntryType,
                Remark: data.Remark
            }
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    AccountEntryService: function () { return AccountEntryService; },
                    AccountEntryController: function () { return AccountEntryController; },
                    objaccount: function () { return objaccount; },
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
            var objaccount = {
                AccountId: data.AccountId,
                PartyName: data.PartyName,
                LegerId: data.LegerId,
                ITRHeadData: { Display: data.ITRName, Value: data.ITRId },
                LegerData: { Display: data.LegerName, Value: data.LegerId },
                LegerHeadId: data.LegerHeadId,
                LegerHeadData: { Display: data.LegerHeadName, Value: data.LegerHeadId },
                CurrencyId: data.CurrencyId,
                CurrencyData: { Display: data.CurrencyName, Value: data.CurrencyId },
                BillDate: $filter('mydate')(data.BillDate),
                BillNo: data.BillNo,
                TaxId: data.TaxId,
                TaxData: { Display: data.TaxName, Value: data.TaxId },
                TaxValue: data.TaxValue,
                Amount: data.Amount,
                ExchangeRate: data.ExchangeRate,
                INRAmount: data.INRAmount,
                BillPdf: data.BillPdf,
                TransactionSlip: data.TransactionSlip,
                Photo: data.Photo,
                AccountEntryType: data.AccountEntryType,
                Remark: data.Remark
            }
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    AccountEntryService: function () { return AccountEntryService; },
                    AccountEntryController: function () { return AccountEntryController; },
                    objaccount: function () { return objaccount; },
                    isdisable: function () { return _isdisable; }
                }
            });
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
            });
        }
        $scope.Delete = function (AccountId) {

            AccountEntryService.DeleteAccountEntry(AccountId).then(function (result) {
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

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, AccountEntryService, AccountEntryController, $filter, objaccount, Upload, isdisable) {

        $scope.$watch('objaccount.ITRHeadData', function (data) {
            if (objaccount.ITRId != undefined) {
                if (data.Value != objaccount.ITRId.toString()) {
                    $scope.objaccount.LegerHeadData.Display = '';
                    $scope.objaccount.LegerHeadData.Value = '';
                    $scope.objaccount.LegerData.Display = '';
                    $scope.objaccount.LegerData.Value = '';
                }
            }
        }, true)
        $scope.$watch('objaccount.LegerHeadData', function (data) {
            if (objaccount.LegerHeadId != undefined) {
                if (data.Value != objaccount.LegerHeadId.toString()) {
                    $scope.objaccount.LegerData.Display = '';
                    $scope.objaccount.LegerData.Value = '';
                }
            }
        }, true)

        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.objaccount = $scope.objaccount || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};
        $scope.callback = function (data) {
            console.log(data);
        }

        //$scope.LegerHeadBind = function () { 
        //    debugger;
        //    AccountEntryService.LegerHeadBind().then(function (result) {
        //        if (result.data.ResponseType == 1) {
        //            $scope.LegerHeadList = result.data.DataList;
        //        }
        //        else if (result.data.ResponseType == 3) {
        //            toastr.error(result.data.Message, 'Opps, Something went wrong');
        //        }
        //    }, function (errorMsg) {
        //        toastr.error(errorMsg, 'Opps, Something went wrong');
        //    })
        //}

        //function LegerBind() { 
        //    debugger;
        //    var lid = $scope.objaccount.LegerHeadId;
        //    if (lid == undefined) {
        //        lid = 0;
        //    }
        //    AccountEntryService.LegerBind(lid).then(function (result) {
        //        if (result.data.ResponseType == 1) {
        //            $scope.LegerList = result.data.DataList;
        //        }
        //        else if (result.data.ResponseType == 3) {
        //            toastr.error(result.data.Message, 'Opps, Something went wrong');
        //        }
        //    }, function (errorMsg) {
        //        toastr.error(errorMsg, 'Opps, Something went wrong');
        //    })
        //}


        $scope.setPrice = function () {
            var Amount = parseFloat(isNaN($scope.objaccount.Amount) ? 0 : $scope.objaccount.Amount);
            var TaxAmount = parseFloat(isNaN($scope.objaccount.TaxValue) ? 0 : $scope.objaccount.TaxValue);
            var ExchangeRate = parseFloat(isNaN($scope.objaccount.ExchangeRate) ? 0 : $scope.objaccount.ExchangeRate);
            $scope.objaccount.INRAmount = ((Amount+TaxAmount) * ExchangeRate)
        }

        if (objaccount.AccountId && objaccount.AccountId > 0) {
            $scope.objaccount = {

                AccountId: objaccount.AccountId,
                PartyName: objaccount.PartyName,
                LegerId: objaccount.LegerId,
                ITRHeadData: { Display: objaccount.ITRHeadData.Display, Value: objaccount.ITRId },
                LegerData: { Display: objaccount.LegerData.Display, Value: objaccount.LegerId },
                LegerHeadId: objaccount.LegerHeadId,
                LegerHeadData: { Display: objaccount.LegerHeadData.Display, Value: objaccount.LegerHeadId },
                CurrencyId: objaccount.CurrencyId,
                CurrencyData: { Display: objaccount.CurrencyData.Display, Value: objaccount.CurrencyId },
                TaxId: objaccount.TaxId,
                TaxData: { Display: objaccount.TaxData.Display, Value: objaccount.TaxId },
                TaxValue: objaccount.TaxValue,
                BillDate: objaccount.BillDate,
                BillNo: objaccount.BillNo,
                Amount: objaccount.Amount,
                ExchangeRate: objaccount.ExchangeRate,
                INRAmount: objaccount.INRAmount,
                BillPdf: objaccount.BillPdf,
                TransactionSlip: objaccount.TransactionSlip,
                Photo: objaccount.Photo,
                AccountEntryType: objaccount.AccountEntryType,
                Remark: objaccount.Remark
            }
            $scope.storage = angular.copy($scope.objstate);
            //$scope.objCategory = result.data.DataList.CategoryName;
        } else {
            //toastr.error(objstate.data.Message, 'Opps, Something went wrong');
            ResetForm();
        }

        function ResetForm() {
            $scope.objaccount = {
                AccountId: 0,
                PartyName: '',
                LegerId: 0,
                LegerData: { Display: '', Value: '' },
                LegerHeadId: 0,
                LegerHeadData: { Display: '', Value: '' },
                CurrencyId: 0,
                CurrencyData: { Display: '', Value: '' },
                TaxData: { Display: '', Value: '' },
                TaxValue: 0,
                BillDate: '',
                BillNo: '',
                Amount: 0,
                ExchangeRate: 1,
                INRAmount: 0,
                BillPdf: '',
                TransactionSlip: '',
                Photo: '',
                Remark: '',
                AccountEntryType: '',
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormAccountEntryInfo)
                $scope.$parent.FormAccountEntryInfo.$setPristine();

        }

        $scope.uploadImgPhoto = function (file) {
            var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];
            var oInput = file[0];
            var sFileName = oInput.name;
            if (sFileName.length > 0) {
                var blnValid = false;
                for (var j = 0; j < _validFileExtensions.length; j++) {
                    var sCurExtension = _validFileExtensions[j];
                    if (sFileName.substr(sFileName.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
                        blnValid = true;
                        break;
                    }
                }

                if (!blnValid) {
                    lblErrorphoto.innerHTML = "Sorry, " + sFileName + " is invalid, allowed extensions are: " + _validFileExtensions.join(", ");
                    $scope.objaccount.Photo = '';
                    return false;
                }
            }
            //var cnt = 0;
            //var blnValid = false;
            //var allowedFiles1 = [".jpg"];
            //var allowedFiles2 = [".jpeg"];
            //var allowedFiles3 = [".gif"];
            //var allowedFiles4 = [".png"];
            //var regex1 = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles1.join('|') + ")$");
            //if (!regex1.test(file[0].name.toLowerCase())) {
            //    var regex2 = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles2.join('|') + ")$");
            //    if (!regex2.test(file[0].name.toLowerCase())) {
            //        var regex3 = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles3.join('|') + ")$");
            //        if (!regex3.test(file[0].name.toLowerCase())) {
            //            var regex4 = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles4.join('|') + ")$");
            //            if (!regex4.test(file[0].name.toLowerCase())) {
            //                lblErrorphoto.innerHTML = "Please upload files having extensions: <b>" + allowedFiles.join(', ') + "</b> only.";
            //                return false;
            //            }
            //            else
            //            {
            //                cnt = 1;
            //            }
            //        }
            //        else
            //        {
            //            cnt = 1;
            //        }
            //    }
            //    else
            //    {
            //        cnt = 1;
            //    }
            //}
            //else
            //{
            //    cnt = 1
            //}
            if (blnValid == true) {
                if (parseInt(file[0].size) <= 2097152) {
                    lblErrorphoto.innerHTML = "";
                    $scope.objaccount.Photo = '';
                    Upload.upload({
                        url: "/Handler/FileUpload.ashx",
                        method: 'POST',
                        file: file,
                    }).then(function (result) {
                        if (result.status == 200) {
                            if (result.data.length > 0) {
                                $scope.objaccount.Photo = result.data[0].imageName;
                                $scope.tempImagePhotoPath = result.data[0].imagePath;
                            }
                        }
                        else {
                            $scope.objaccount.Photo = '';
                        }
                    });
                }
                else {
                    lblErrorphoto.innerHTML = "Please upload files size maximum: <b> 2 MB </b> only.";
                    return false;
                }
            }
        }

        $scope.uploadImgSlip = function (file) {

            var allowedFiles = [".pdf"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            if (file[0].type != 'application/pdf') {
                lblErrorslip.innerHTML = "Please upload files having extensions: <b>" + allowedFiles.join(', ') + "</b> only.";
                return false;
            }
            else {
                if (parseInt(file[0].size) <= 10485760) {
                    $scope.objaccount.TransactionSlip = '';
                    lblErrorslip.innerHTML = "";
                    Upload.upload({
                        url: "/Handler/FileUpload.ashx",
                        method: 'POST',
                        file: file,
                    }).then(function (result) {
                        if (result.status == 200) {
                            if (result.data.length > 0) {
                                $scope.objaccount.TransactionSlip = result.data[0].imageName;
                                $scope.tempImageSlipPath = result.data[0].imagePath;
                            }
                        }
                        else {
                            $scope.objaccount.TransactionSlip = '';
                        }
                    });
                }
                else {
                    lblErrorslip.innerHTML = "Please upload files size maximum: <b> 10 MB </b> only.";
                    return false;
                }
            }
        }

        $scope.uploadImgBillPdf = function (file) {

            var allowedFiles = [".pdf"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            if (file[0].type != 'application/pdf') {
                lblErrorbill.innerHTML = "Please upload files having extensions: <b>" + allowedFiles.join(', ') + "</b> only.";
                return false;
            }
            else {
                if (parseInt(file[0].size) <= 10485760) {
                    lblErrorbill.innerHTML = "";
                    $scope.objaccount.BillPdf = '';
                    Upload.upload({
                        url: "/Handler/FileUpload.ashx",
                        method: 'POST',
                        file: file,
                    }).then(function (result) {
                        if (result.status == 200) {
                            if (result.data.length > 0) {
                                $scope.objaccount.BillPdf = result.data[0].imageName;
                                $scope.tempImageBillPdfPath = result.data[0].imagePath;
                            }
                        }
                        else {
                            $scope.objaccount.BillPdf = '';
                        }
                    });
                }
                else {
                    lblErrorbill.innerHTML = "Please upload files size maximum: <b> 10 MB </b> only.";
                    return false;
                }
            }
        }

        $scope.Create = function (data) {
            if (data.BillPdf == "" || data.BillPdf == null) {
                $scope.lblErrorbill = "Bill is required";
            }
            else {
                $scope.lblErrorbill = "";
                var objaccount = {
                    AccountId: data.AccountId,
                    PartyName: data.PartyName,
                    //ITRId: data.ITRHeadData.Value,
                    //ITRHeadData: data.ITRHeadData.Display,
                    LegerId: data.LegerData.Value,
                    LegerData: data.LegerData.Display,
                    LegerHeadId: data.LegerHeadData.Value,
                    LegerHeadData: data.LegerHeadData.Display,
                    CurrencyId: data.CurrencyData.Value,
                    CurrencyData: data.CurrencyData.Display,
                    TaxId: data.TaxData.Value,
                    TaxData: data.TaxData.Display,
                    TaxValue: data.TaxValue,
                    BillDate: data.BillDate,
                    BillNo: data.BillNo,
                    Amount: data.Amount,
                    ExchangeRate: data.ExchangeRate,
                    INRAmount: data.INRAmount,
                    BillPdf: data.BillPdf,
                    TransactionSlip: data.TransactionSlip,
                    Photo: data.Photo,
                    AccountEntryType: data.AccountEntryType,
                    Remark: data.Remark
                }
                AccountEntryService.AddAccountEntry(objaccount).then(function (result) {
                    $uibModalInstance.close();
                    toastr.success(result.data.Message);
                    ResetForm();
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }
        }

        $scope.Update = function (data) {
            if (data.BillPdf == "" || data.BillPdf == null) {
                $scope.lblErrorbill = "Bill is required";
            }
            else {
                $scope.lblErrorbill = "";
                var objaccount = {
                    AccountId: data.AccountId,
                    PartyName: data.PartyName,
                    //ITRId: data.ITRHeadData.Value,
                    //ITRHeadData: data.ITRHeadData.Display,
                    LegerId: data.LegerData.Value,
                    LegerData: data.LegerData.Display,
                    LegerHeadId: data.LegerHeadData.Value,
                    LegerHeadData: data.LegerHeadData.Display,
                    CurrencyId: data.CurrencyData.Value,
                    CurrencyData: data.CurrencyData.Display,
                    TaxId: data.TaxData.Value,
                    TaxData: data.TaxData.Display,
                    TaxValue: data.TaxValue,
                    BillDate: data.BillDate,
                    BillNo: data.BillNo,
                    Amount: data.Amount,
                    ExchangeRate: data.ExchangeRate,
                    INRAmount: data.INRAmount,
                    BillPdf: data.BillPdf,
                    TransactionSlip: data.TransactionSlip,
                    Photo: data.Photo,
                    AccountEntryType: data.AccountEntryType,
                    Remark: data.Remark
                }
                AccountEntryService.Update(objaccount).then(function (result) {
                    $uibModalInstance.close();
                    toastr.success(result.data.Message);
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }
        }

        $scope.Reset = function () {
            if ($scope.objaccount.AccountId > 0) {
                $scope.objaccount = angular.copy($scope.storage);
            } else {
                ResetForm();
            }

        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'AccountEntryService', 'AccountEntryController', '$filter', 'objaccount', 'Upload', 'isdisable']
    angular.module('CRMApp.Controllers').filter("mydate", function () {

        var re = /\/Date\(([0-9]*)\)\//;
        return function (x) {
            var m = x.match(re);
            if (m) return new Date(parseInt(x.substr(6)));
            else return null;
        };
    });

    //angular.module('CRMApp.Controllers').filter("strlimit", function() {
    //    return function (input, limit) {
    //        if (!input) return;
    //        if (input.length <= limit) {
    //            return input;
    //        } 
    //        return $filter('limitTo')(input, limit) + '...';
    //    };
    //});

    //angular.module('CRMApp.Controllers').filter('truncate', function () {

    //    return function (content, maxCharacters) {

    //        if (content == null) return "";

    //        content = "" + content;

    //        content = content.trim();

    //        if (content.length <= maxCharacters) return content;

    //        content = content.substring(0, maxCharacters);

    //        var lastSpace = content.lastIndexOf(" ");

    //        if (lastSpace > -1) content = content.substr(0, lastSpace);

    //        return content + '...';
    //    };
    //});
})()


