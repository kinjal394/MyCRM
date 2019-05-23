
(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("SalesPurchaseEntryController", [
               "$scope", "SalesPurchaseEntryService", "$timeout", "$filter", "$uibModal", "Upload",
               SalesPurchaseEntryController]);

    function SalesPurchaseEntryController($scope, SalesPurchaseEntryService, $timeout, $filter, $uibModal, Upload) {
        $scope.objSalesPurchase = $scope.objSalesPurchase || {};
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};
        $scope.isClicked = false;
        $scope.objSalesPurchase = {
            SalesPurchaseId: 0,
            FinicialYearId: 0,
            FinicialYear: 0,
            InvoiceNo: '',
            InvoiceDate: new Date(),
            PartyType: '',
            PartyId: 0,
            PartyName: '',
            PartyData: { Display: '', Value: '' },
            YearData: { Display: '', Value: '' },
            SalesPurchaseDocMasters: [],
        }
        $scope.gridObj = {
            columnsInfo: [
               //{ "title": "SalesPurchaseId", "data": "SalesPurchaseId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               //{ "title": "FinicialYearId", "data": "FinicialYearId", sortable: "FinicialYearId", filter: { FinicialYearId: "text" }, show: false, },
               { "title": "Finicial Year", "field": "FinancialYear", sortable: "FinancialYear", filter: { FinancialYear: "text" }, show: true, },
               { "title": "Invoice No", "field": "InvoiceNo", sortable: "InvoiceNo", filter: { InvoiceNo: "text" }, show: true, },
               {
                   "title": "Invoice Date", "field": "InvoiceDate", sortable: "InvoiceDate", filter: { InvoiceDate: "date" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<span >{{ConvertDate(row.InvoiceDate,\'dd/mm/yyyy\')}}</span>'
                       return $scope.getHtml(element);
                   }
               },
               { "title": "Party Type", "field": "PartyTypeName", sortable: "PartyTypeName", filter: { PartyTypeName: "text" }, show: false, },
               { "title": "Party Name", "field": "PartyName", sortable: "PartyName", filter: { PartyName: "text" }, show: true, },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.SalesPurchaseId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                    //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.SalesPurchaseId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                    '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.SalesPurchaseId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'ProductId': 'asc' },
        }
        $scope.Add = function (id) {
            window.location.href = "/Transaction/SalesPurchaseEntry/SalesPurchaseEntryDetail"
        }

        $scope.Edit = function (id) {
            window.location.href = "/Transaction/SalesPurchaseEntry/SalesPurchaseEntryDetail/" + id + "/" + 0;
        }
        $scope.View = function (id) {
            window.location.href = "/Transaction/SalesPurchaseEntry/SalesPurchaseEntryDetail/" + id + "/" + 1;
        }
        $scope.Delete = function (id) {
            SalesPurchaseEntryService.DeleteSalePurchaseEntry(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    $scope.refreshGrid()
                } else {
                    toastr.error(result.data.Message);
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }
        $scope.SetSalePurchaseId = function (Id, isdisable) {
            if (Id > 0) {
                $scope.ProductId = Id;
                $scope.addMode = false;
                $scope.saveText = "Update";
                $scope.objSalesPurchase.EditSalePurchaseImgId = 0;
                $scope.BindDataBySalePurchaseID(Id)
                if (isdisable == 1) {
                    $scope.isClicked = true;
                }
            } else {
                $scope.ProductId = 0;
                $scope.addMode = true;
                $scope.saveText = "Save";
                $scope.objSalesPurchase.EditSalePurchaseImgId = 0;
                $scope.isClicked = false;
            }
        }
        function ResetForm() {
            $scope.objSalesPurchase = {
                SalesPurchaseId: 0,
                FinicialYearId: 0,
                FinicialYear: 0,
                InvoiceNo: '',
                InvoiceDate: new Date(),
                PartyType: '',
                PartyId: 0,
                PartyName: '',
                PartyData: { Display: '', Value: '' },
                YearData: { Display: '', Value: '' },
                SalesPurchaseDocMasters: [],
            }
            $scope.objSalesPurchaseDetail = {
                SalesPurchaseDocId: 0,
                SalesPurchaseId: 0,
                DocId: 0,
                DocPath: '',
                DocName: '',
                DocData: { Display: '', Value: '' },
                Status: 0 //1 : Insert , 2:Update ,3 :Delete
            }
            if ($scope.FormSalesPurchaseFromInfo)
                $scope.FormSalesPurchaseFromInfo.$setPristine();
            $scope.addMode = true;
            $scope.isFirstFocus = false;
            $scope.storage = {};
            $timeout(function () {
                $scope.isFirstFocus = true;
            });
            $scope.EditSalesPurchaseDocIndex = -1;
        }
        ResetForm();
        $scope.Reset = function () {
            if ($scope.objSalesPurchase.SalesPurchaseId > 0) {
                $scope.objSalesPurchase = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }
        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };
        $scope.SetValue = function (value, type) {
            $scope.PartyMode = value;
            $scope.objSalesPurchase.PartyType = type;
            $scope.objSalesPurchase.PartyData = {
                Display: "",
                Value: ""
            };
        }
        $scope.dateOptions = {
            formatYear: 'dd-MM-yyyy',
            //minDate: new Date(2016, 8, 5),
            //maxDate: new Date(),
            startingDay: 1
        };
        $scope.BindDataBySalePurchaseID = function (Id) {
            SalesPurchaseEntryService.GetAllSalesPurchaseEntryById(Id).then(function (result) {
                var objSalesPurchase = result.data.DataList.objSalesPurchaseModel;
                $scope.objSalesPurchase = {
                    SalesPurchaseId: objSalesPurchase.SalesPurchaseId,
                    FinicialYearId: objSalesPurchase.FinicialYearId,
                    FinicialYear: objSalesPurchase.FinancialYear,
                    InvoiceNo: objSalesPurchase.InvoiceNo,
                    InvoiceDate: $filter('mydate')(objSalesPurchase.InvoiceDate),
                    PartyType: objSalesPurchase.PartyType,
                    PartyId: objSalesPurchase.PartyId,
                    PartyName: objSalesPurchase.PartyName,
                    PartyData: { Display: objSalesPurchase.PartyName, Value: objSalesPurchase.PartyId },
                    YearData: { Display: objSalesPurchase.FinancialYear, Value: objSalesPurchase.FinicialYearId },
                }
                $scope.objSalesPurchase.SalesPurchaseDocMasters = [];
                if (result.data.DataList.objSalesPurchaseDocMaster.length > 0) {
                    angular.forEach(result.data.DataList.objSalesPurchaseDocMaster, function (value) {
                        var SalesPurchaseDoc = {
                            SalesPurchaseDocId: value.SalesPurchaseDocId,
                            SalesPurchaseId: value.SalesPurchaseId,
                            DocId: value.DocId,
                            DocPath: "/UploadImages/SalesPurchaseEntry/" + value.DocPath,
                            DocName: value.DocName,
                            DocData: { Display: value.DocName, Value: value.DocId },
                            Status: 2 //1 : Insert , 2:Update ,3 :Delete
                        }
                        $scope.objSalesPurchase.SalesPurchaseDocMasters.push(SalesPurchaseDoc);
                    }, true);
                }
                $scope.objSalesPurchase.EditSalePurchaseImgId = 0;
                $scope.storage = angular.copy($scope.objSalesPurchase);
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.CreateUpdate = function (data) {
            angular.forEach(data.SalesPurchaseDocMasters, function (value, index) {
                if (value.DocPath != undefined) {
                    var dataphoto = value.DocPath.split('/');
                    data.SalesPurchaseDocMasters[index].DocPath = dataphoto[dataphoto.length - 1];
                    //data.ProductPhotoMasters[index].Photo = value.Photo.substring((value.Photo.length - 19), (value.Photo.length));
                }
            }, true);
            data.FinicialYearId = data.YearData.Value;
            data.PartyId = data.PartyData.Value;
            data.PartyName = data.PartyData.Display;
            $scope.objSalesPurchaseDetail.DocData.Display = ' ';
            SalesPurchaseEntryService.CreateUpdateSalePurchaseEntry(data).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    ResetForm();
                    window.location.href = "/Transaction/SalesPurchaseEntry";
                } else {
                    toastr.error(result.data.Message);
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }
        //BEGIN MANAGE Sales/Purchase DOC
        $scope.AddSalesPurchaseDoc = function (data) {
            $scope.docsubmitted = true;
            var bln = true;
            if (data.DocData.Display != '' && (data.DocPath == '' || data.DocPath == null)) {
                bln = false;
                toastr.error("Document Upload is Required.", "Error");
            }
            if (data.DocData.Display != '' && bln == true) {
                $scope.docsubmitted = false;
                data.DocId = data.DocData.Value;
                data.DocName = data.DocData.Display;
                if ($scope.EditSalesPurchaseDocIndex > -1) {
                    if ($scope.addMode == false) {
                        $scope.tempDocPath = "";
                        data.DocPath = data.DocPath;
                    } else {
                        if ($scope.tempDocPath != "") {
                            data.tempDocPath = $scope.tempDocPath;
                            data.DocPath = data.tempDocPath;
                        }
                    }
                    $scope.objSalesPurchase.SalesPurchaseDocMasters[$scope.EditSalesPurchaseDocIndex] = data;
                    $scope.EditSalesPurchaseDocIndex = -1;
                } else {
                    data.Status = 1;
                    data.DocPath = $scope.tempDocPath;
                    $scope.objSalesPurchase.SalesPurchaseDocMasters.push(data);

                }
                toastr.success("Document Add", "Success");
                $scope.objSalesPurchaseDetail = {
                    SalesPurchaseDocId: 0,
                    SalesPurchaseId: 0,
                    DocId: 0,
                    DocPath: '',
                    DocName: '',
                    DocData: { Display: '', Value: '' },
                    Status: 0 //1 : Insert , 2:Update ,3 :Delete
                }
                $scope.tempDocPath = "";
                $scope.objSalesPurchase.EditSalePurchaseImgId = 0;

            }
        }

        $scope.CancelSalesPurchaseDoc = function () {
            $scope.objSalesPurchaseDetail = {
                SalesPurchaseDocId: 0,
                SalesPurchaseId: 0,
                DocId: 0,
                DocPath: '',
                DocName: '',
                DocData: { Display: '', Value: '' },
                Status: 0 //1 : Insert , 2:Update ,3 :Delete
            }
            $scope.tempDocPath = "";
            $scope.EditSalesPurchaseDocIndex = -1;
        }

        $scope.DeleteSalesPurchaseDoc = function (data, index) {
            if (data.Status == 2) {
                data.Status = 3;
                $scope.objSalesPurchase.SalesPurchaseDocMasters[index] = data;
            } else {
                $scope.objSalesPurchase.SalesPurchaseDocMasters.splice(index, 1);
            }
            $scope.objSalesPurchase.EditSalePurchaseImgId = 0;
            toastr.success("Document Delete", "Success");
        }

        $scope.EditSalesPurchaseDoc = function (data, index) {
            $scope.objSalesPurchase.EditSalePurchaseImgId = 1;
            $scope.EditSalesPurchaseDocIndex = index;
            var tempProjectDoc = angular.copy(data);
            $scope.objSalesPurchaseDetail = tempProjectDoc;
            $scope.tempDocPath = tempProjectDoc.DocPath;
        }

        $scope.uploadDocFile = function (file) {
            $scope.objSalesPurchaseDetail.DocPath = '';
            $scope.tempDocPath = '';
            Upload.upload({
                url: "/Handler/FileUpload.ashx",
                method: 'POST',
                file: file,
            }).then(function (result) {
                if (result.config.file[0].type.match('application/pdf')) {
                    if (result.status == 200) {
                        if (result.data.length > 0) {
                            $scope.objSalesPurchaseDetail.DocPath = result.data[0].imagePath;
                            $scope.tempDocPath = result.data[0].imagePath;
                        }
                    }
                    else {
                        $scope.tempDocPath = '';
                        $scope.objSalesPurchaseDetail.DocPath = '';
                    }
                } else {
                    toastr.error("Only PDF File Allowed.", "Error");
                }
            });
        }
        //END MANAGE Sales/Purchase DOC

        $scope.statusFilter = function (val) {
            if (val.Status != 3)
                return true;
        }
        $scope.cntImage = function (data) {
            var count = 0;
            _.forEach(data, function (val) {
                if (val.Status != 3)
                    count++;
            })
            return count;
        }
    }
})()