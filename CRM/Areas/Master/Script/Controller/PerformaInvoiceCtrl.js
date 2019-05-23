(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("PerformaInvoiceCtrl", [
            "$scope", "$rootScope", "$timeout", "$filter", "PerformaInvoiceService", "UploadProductDataService", "ProductService", "$uibModal", "PurchaseOrderService",
            PerformaInvoiceCtrl
        ]);
    function PerformaInvoiceCtrl($scope, $rootScope, $timeout, $filter, PerformaInvoiceService, UploadProductDataService, ProductService, $uibModal, PurchaseOrderService) {
        $scope.isClicked = true;
        $scope.objPerformaInvoice = $scope.objPerformaInvoice || {};
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};
        var pdate = new Date();
        var pedate = new Date().setDate(pdate.getDate() + 7);
        $scope.objPerformaInvoice = {
            PerformaInvId: 0,
            PerformaInvNo: '',
            RptFormatId: '',
            ComName: '',
            ComAddress: '',
            ComEmail: '',
            ComTel: '',
            ComTax: '',
            PerformaInvDate: pdate,
            ExpirationDate: pedate,
            DeliveryTermId: 0,
            PaymentTermId: '',
            ModeOfShipmentId: '',
            LoadingPortId: '',
            LoadingPort: '',
            DischargePortId: '',
            DischargePort: '',
            ShippingMarks: '',
            ConsigneId: '',
            ConsigneName: '',
            ConsigneAddressId: '',
            ConsigneAddress: '',
            ConsigneEmail: '',
            ConsigneTel: '',
            ConsigneTaxId: '',
            ConsigneTax: '',
            ConsigneTaxValue: '',
            ContactId: '',
            ContactName: '',
            ContactTel: '',
            ContactEmail: '',
            BankNameId: '',
            BankNameName: '',
            AccountTypeId: '',
            AccountType: '',
            BeneficiaryName: '',
            BranchName: '',
            BankAddress: '',
            AccountNo: '',
            IFSCCode: '',
            SwiftCode: '',
            CompanyCodeData: { Display: '', Value: '' },
            DeliveryData: { Display: '', Value: '' },
            PaymentTermData: { Display: '', Value: '' },
            ModeOfShipmentData: { Display: '', Value: '' },
            LoadingPortData: { Display: '', Value: '' },
            DischargePortData: { Display: '', Value: '' },
            ConsigneData: { Display: '', Value: '' },
            ConsigneAddressData: { Display: '', Value: '' },
            ConsigneContactData: { Display: '', Value: '' },
            TaxData: { Display: '', Value: '' },
            BankData: { Display: '', Value: '' },
            AccountTypeData: { Display: '', Value: '' },
            PerformaProductMasters: []
        };
        $scope.objPerfomaPrd = {
            Id: 0,
            PerformaInvId: 0,
            ProductId: 0,
            ProductCode: '',
            CategoryId: 0,
            Category: '',
            SubCategoryId: 0,
            SubCategory: '',
            ProductDescription: '',
            CountryOfOriginId: 0,
            CountryOfOrigin: '',
            ProductModelNo: '',
            QtyCode: '',
            QtyCodeValue: '',
            Qty: '',
            CurrencyCode: '',
            CurrencyCodeValue: '',
            AddPerValue: '',
            DealerPrice: '',
            OfferPrice: '',
            Total: '',
            Status: 0,//1 : Insert , 2:Update ,3 :Delete
            CategoryData: { Display: '', Value: '' },
            SubCategoryData: { Display: '', Value: '' },
            ProductData: { Display: '', Value: '' },
            CountryOfOriginData: { Display: '', Value: '' },
            QtyCodeValueData: { Display: '', Value: '' },
            CurrencyData: { Display: '', Value: '' }
        };
        $scope.SetPerformaInvoiceId = function (id, isdisable) {
            if (id > 0) {
                $scope.SrNo = id;
                $scope.addMode = false;
                $scope.saveText = "Update";
                $scope.GetAllPerformaInvoiceById(id);
                $scope.isClicked = false;
                if (isdisable == 1) {
                    $scope.isClicked = true;
                }
            } else {
                $scope.SrNo = 0;
                $scope.addMode = true;
                $scope.saveText = "Save";
                $scope.GetInvoice();
                $scope.isClicked = false;
            }
        }
        $scope.Add = function () {
            window.location.href = "/master/PerformaInvoice/AddPerformaInvoice";
        }
        $scope.GetInvoice = function () {
            PerformaInvoiceService.GetInvoice().then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objPerformaInvoice.PerformaInvNo = result.data.DataList.InvCode;
                } else if (result.ResponseType == 3) {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }
        $scope.GetAllPerformaInvoiceById = function (id) {
            PerformaInvoiceService.GetAllPerformaInvoiceById(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    var objPerformaInv = result.data.DataList.objPerformaInvoice;
                    var objPerformaPrd = result.data.DataList.objPerformaProduct;
                    $scope.objPerformaInvoice = {
                        PerformaInvId: objPerformaInv.PerformaInvId,
                        PerformaInvNo: objPerformaInv.PerformaInvNo,
                        RptFormatId: objPerformaInv.RptFormatId,
                        ComName: objPerformaInv.ComName,
                        ComAddress: objPerformaInv.CorpOffAdd,
                        ComEmail: objPerformaInv.Email,
                        ComTel: objPerformaInv.TelNos,
                        ComTax: objPerformaInv.TaxDetails,
                        PerformaInvDate: $filter('mydate')(objPerformaInv.PerformaInvDate),
                        ExpirationDate: $filter('mydate')(objPerformaInv.ExpirationDate),
                        DeliveryTermId: objPerformaInv.DeliveryTermId,
                        PaymentTermId: objPerformaInv.PaymentTermId,
                        ModeOfShipmentId: objPerformaInv.ModeOfShipmentId,
                        LoadingPortId: objPerformaInv.LoadingPortId,
                        DischargePortId: objPerformaInv.DischargePortId,
                        ShippingMarks: objPerformaInv.ShippingMarks,
                        ConsigneId: objPerformaInv.ConsigneId,
                        ConsigneName: objPerformaInv.ConsigneName,
                        ConsigneAddressId: objPerformaInv.ConsigneAddressId,
                        ConsigneAddress: objPerformaInv.ConsigneAddress,
                        ConsigneEmail: objPerformaInv.ConsigneEmail,
                        ConsigneTel: objPerformaInv.ConsigneTel,
                        ConsigneTaxId: objPerformaInv.ConsigneTaxId,
                        ConsigneTax: objPerformaInv.ConsigneTax.split('|')[2],
                        ConsigneTaxValue: objPerformaInv.ConsigneTaxValue,
                        ContactId: objPerformaInv.ContactId,
                        ContactName: objPerformaInv.ContactName,
                        ContactTel: objPerformaInv.ContactTel,
                        ContactEmail: objPerformaInv.ContactEmail,
                        BankNameId: objPerformaInv.BankNameId,
                        BankName: objPerformaInv.BankName,
                        AccountTypeId: objPerformaInv.AccountTypeId,
                        AccountType: objPerformaInv.AccountType,
                        BeneficiaryName: objPerformaInv.BeneficiaryName,
                        BranchName: objPerformaInv.BranchName,
                        BankAddress: objPerformaInv.BankAddress,
                        AccountNo: objPerformaInv.AccountNo,
                        IFSCCode: objPerformaInv.IFSCCode,
                        SwiftCode: objPerformaInv.SwiftCode,
                        CompanyCodeData: { Display: objPerformaInv.RptCompany, Value: objPerformaInv.RptFormatId },
                        DeliveryData: { Display: objPerformaInv.DeliveryTerm, Value: objPerformaInv.DeliveryTermId },
                        PaymentTermData: { Display: objPerformaInv.PaymentTerm, Value: objPerformaInv.PaymentTermId },
                        ModeOfShipmentData: { Display: objPerformaInv.ModeOfShipment, Value: objPerformaInv.ModeOfShipmentId },
                        LoadingPortData: { Display: objPerformaInv.LoadingPort, Value: objPerformaInv.LoadingPortId },
                        DischargePortData: { Display: objPerformaInv.DischargePort, Value: objPerformaInv.DischargePortId },
                        ConsigneData: { Display: objPerformaInv.ConsigneName, Value: objPerformaInv.ConsigneId },
                        ConsigneAddressData: { Display: objPerformaInv.ConsigneAddress, Value: objPerformaInv.ConsigneAddressId },
                        ConsigneContactData: { Display: objPerformaInv.ContactName, Value: objPerformaInv.ContactId },
                        TaxData: { Display: objPerformaInv.ConsigneTax.split('|')[1], Value: objPerformaInv.ConsigneTax.split('|')[0] },
                        BankData: { Display: objPerformaInv.BankName, Value: objPerformaInv.BankNameId },
                        AccountTypeData: { Display: objPerformaInv.AccountType, Value: objPerformaInv.AccountTypeId },
                    };

                    $scope.objPerformaInvoice.PerformaProductMasters = [];
                    angular.forEach(result.data.DataList.objPerformaProduct, function (value) {
                        var objContactDetail = {
                            Id: value.Id,
                            PerformaInvId: value.PerformaInvId,
                            ProductId: value.ProductId,
                            ProductName: value.ProductName,
                            ProductCode: value.ProductCode,
                            CategoryId: value.CategoryId,
                            Category: value.Category,
                            SubCategoryId: value.SubCategoryId,
                            SubCategory: value.SubCategory,
                            ProductDescription: value.ProductDescription,
                            CountryOfOriginId: value.CountryOfOriginId,
                            CountryOfOrigin: value.CountryOfOrigin,
                            ProductModelNo: value.ProductModelNo,
                            QtyCode: value.QtyCode,
                            QtyCodeValue: value.QtyCodeValue,
                            Qty: value.Qty,
                            CurrencyCode: value.CurrencyCode,
                            CurrencyCodeValue: value.CurrencyCodeValue,
                            AddPerValue: value.AddPerValue,
                            DealerPrice: value.DealerPrice,
                            OfferPrice: value.OfferPrice,
                            Total: (value.OfferPrice * value.Qty),
                            Status: 2,//1 : Insert , 2:Update ,3 :Delete
                            CategoryData: { Display: value.Category, Value: value.CategoryId },
                            SubCategoryData: { Display: value.SubCategory, Value: value.SubCategoryId },
                            ProductData: { Display: value.ProductName, Value: value.ProductId },
                            CountryOfOriginData: { Display: value.CountryOfOrigin, Value: value.CountryOfOriginId },
                            QtyCodeValueData: { Display: value.QtyCodeValue, Value: value.QtyCode },
                            CurrencyData: { Display: value.CurrencyCodeValue, Value: value.CurrencyCode }
                        }

                        $scope.objPerformaInvoice.PerformaProductMasters.push(objContactDetail);
                    }, true);

                    $scope.storage = angular.copy($scope.objPerformaInvoice);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }
        $scope.dateOptions = {
            formatYear: 'yy',
            //minDate: new Date(2016, 8, 5),
            //maxDate: new Date(2017, 5, 22),
            startingDay: 1
        };
        $scope.$watch('objPerformaInvoice.CompanyCodeData', function (val) {
            if (val) {
                if (val.Value && val.Value > 0) {
                    PerformaInvoiceService.GetCompanydataById(val.Value).then(function (result) {
                        if (result.data.ResponseType == 1) {
                            $scope.objPerformaInvoice.ComName = result.data.DataList.ComName;
                            $scope.objPerformaInvoice.ComAddress = result.data.DataList.CorpOffAdd;
                            $scope.objPerformaInvoice.ComEmail = result.data.DataList.Email;
                            $scope.objPerformaInvoice.ComTel = result.data.DataList.TelNos;
                            $scope.objPerformaInvoice.ComTax = result.data.DataList.TaxDetails;
                            //$scope.objofferLetter.Web = result.data.DataList.Web;
                            //$scope.objofferLetter.RegOffAdd = result.data.DataList.RegOffAdd;
                            //$scope.objofferLetter.CorpOffAdd = result.data.DataList.CorpOffAdd;
                            //if (result.data.DataList.ComLogo != null)
                                //$scope.ComLogo = '/UploadImages/Companylogo/' + result.data.DataList.ComLogo;
                            //else
                                //$scope.ComLogo = '';
                        } else if (result.ResponseType == 3) {
                            toastr.error(result.data.Message, 'Opps, Something went wrong');
                        }
                    }, function (error) {
                        $rootScope.errorHandler(error)
                    })
                }
            }
        });
        //$scope.$watch("objPerformaInvoice.ConsigneData", function (val) {
        //    if (val.Value && val.Value > 0) {
        //        PerformaInvoiceService.GetConsigneById(val.Value).then(function (result) {
        //            if (result.data.ResponseType == 1) {
        //                $scope.objPerformaInvoice.FromCompanyAddress = result.data.DataList.RegOffAdd;
        //            } else if (result.ResponseType == 3) {
        //                toastr.error(result.data.Message, 'Opps, Something went wrong');
        //            }
        //        }, function (errorMsg) {
        //            toastr.error(errorMsg, 'Opps, Something went wrong');
        //        })
        //    }
        //})
        $scope.$watch("objPerformaInvoice.ConsigneAddressData", function (val) {
            if (val.Value && val.Value > 0) {
                PerformaInvoiceService.GetConsigneAddressById(val.Value).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        $scope.objPerformaInvoice.ConsigneTel = result.data.DataList.Telephone;
                        $scope.objPerformaInvoice.ConsigneEmail = result.data.DataList.Email;
                    } else if (result.ResponseType == 3) {
                        toastr.error(result.data.Message, 'Opps, Something went wrong');
                    }
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }
        })
        $scope.$watch("objPerformaInvoice.ConsigneContactData", function (val) {
            if (val.Value && val.Value > 0) {
                PerformaInvoiceService.GetConsigneContactById(val.Value).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        $scope.objPerformaInvoice.ContactTel = result.data.DataList.MobileNo;
                        $scope.objPerformaInvoice.ContactEmail = result.data.DataList.Email;

                    } else if (result.ResponseType == 3) {
                        toastr.error(result.data.Message, 'Opps, Something went wrong');
                    }
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }
        })

        $scope.CreateUpdate = function (data) {

            data.ConsigneTaxId = data.TaxData.Value
            data.ConsigneTaxValue = data.TaxData.Display
            data.ConsigneTax = data.ConsigneTaxId + '|' + data.ConsigneTaxValue + '|' + data.ConsigneTax

            data.RptFormatId = data.CompanyCodeData.Value
            data.DeliveryTermId = data.DeliveryData.Value
            data.PaymentTermId = data.PaymentTermData.Value
            data.ModeOfShipmentId = data.ModeOfShipmentData.Value
            data.LoadingPortId = data.LoadingPortData.Value
            data.DischargePortId = data.DischargePortData.Value
            data.ConsigneId = data.ConsigneData.Value
            data.BankNameId = data.BankData.Value
            data.AccountTypeId = data.AccountTypeData.Value

            data.ConsigneName = data.ConsigneData.Display
            data.ConsigneAddress = data.ConsigneAddressData.Display
            data.ContactName = data.ConsigneContactData.Display


            PerformaInvoiceService.SavePerformaInvoice(data).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    window.location.href = "/master/PerformaInvoice";
                }
                else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.Save = function (data) {
            data.PaymentTermId = data.PaymentTermData.Value
            data.ModeOfShipmentId = data.ModeOfShipmentData.Value
            data.LoadingPortId = data.PortData.Value
            data.DeliveryTermId = data.DeliveryData.Value
            data.DischargePortId = data.PortDischargeData.Value
            data.BeneficialyId = data.BeneficialyData.Value

            PerformaInvoiceService.SavePerformaInvoice(data).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    window.location.href = "/master/PerformaInvoice";
                }
                else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })

        }
        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };
        $scope.Update = function (data) {
            data.PaymentTermId = data.PaymentTermData.Value
            data.ModeOfShipmentId = data.ModeOfShipmentData.Value
            data.LoadingPortId = data.PortData.Value
            data.DeliveryTermId = data.DeliveryData.Value
            data.DischargePortId = data.PortDischargeData.Value
            data.BeneficialyId = data.BeneficialyData.Value
            data.PoId = data.PoData.Value
            data.CompanyId = data.CompanyData.Value
            data.ToCompanyId = data.SupplierData.Value
            data.ContactId = data.SupplierContactData.Value
            data.QtyCode = data.UnitData.Value
            data.CountryOfOriginId = data.CountryOfOriginData.Value
            data.OfferPriceCode = data.CurrencyData.Value

            PerformaInvoiceService.UpdatePerformaInvoice(data).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    window.location.href = "/master/PerformaInvoice";
                }
                else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }
        $scope.Delete = function (id) {
            PerformaInvoiceService.DeletePerformaInvoice(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                }
                else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
                $scope.refreshGrid()
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }
        $scope.gridObj = {
            columnsInfo: [
                { "title": "Performa Inv. Id", "field": "PerformaInvId", filter: false, show: false },
                { "title": "Sr.", "field": "RowNumber", filter: false },
                { "title": "Performa Inv No.", "field": "PerformaInvNo", sortable: "PerformaInvNo", filter: { PerformaInvNo: "text" }, show: true },
                {
                    "title": "Performa Date", "field": "PerformaInvDate", sortable: "PerformaInvDate", filter: { PerformaInvDate: "text" }, show: true,
                    'cellTemplte': function ($scope, row) {
                        var element = '<span >{{ConvertDate(row.PerformaInvDate,\'dd/mm/yyyy\')}}</span>'
                        return $scope.getHtml(element);
                    }
                },
                //{
                //    "title": "Expiration Date", "field": "ExpirationDate", sortable: "ExpirationDate", filter: { ExpirationDate: "text" }, show: true,
                //    'cellTemplte': function ($scope, row) {
                //        var element = '<span >{{ConvertDate(row.ExpirationDate,\'dd/mm/yyyy\')}}</span>'
                //        return $scope.getHtml(element);
                //    }
                //},
                { "title": "Company", "field": "RptCompany", sortable: "RptCompany", filter: { RptCompany: "text" }, show: true },
                { "title": "Delivery Term", "field": "DeliveryTerm", sortable: "DeliveryTerm", filter: { DeliveryTerm: "text" }, show: true },
                { "title": "Payment Term", "field": "PaymentTerm", sortable: "PaymentTerm", filter: { PaymentTerm: "text" }, show: true },
                { "title": "Mode Of Shipment", "field": "ModeOfShipment", sortable: "ModeOfShipment", filter: { ModeOfShipment: "text" }, show: false },
                { "title": "Loading of Port", "field": "LoadingPort", sortable: "LoadingPort", filter: { LoadingPort: "text" }, show: false },
                { "title": "Discharge of Port", "field": "DischargePort", sortable: "DischargePort", filter: { DischargePort: "text" }, show: false },

                { "title": "Consigne", "field": "Contact", sortable: "Contact", filter: { Contact: "text" }, show: true },
                //{ "title": "Consigne Name", "field": "ConsigneName", sortable: "ConsigneName", filter: { ConsigneName: "text" }, show: true },
                { "title": "Consigne Address", "field": "ConsigneAddress", sortable: "ConsigneAddress", filter: { ConsigneAddress: "text" }, show: false },
                { "title": "Consigne Email", "field": "ConsigneEmail", sortable: "ConsigneEmail", filter: { ConsigneEmail: "text" }, show: false },
                { "title": "Consigne Tel", "field": "ConsigneTel", sortable: "ConsigneTel", filter: { ConsigneTel: "text" }, show: false },

                { "title": "Consigne Tax", "field": "ConsigneTax", sortable: "ConsigneTax", filter: { ConsigneTax: "text" }, show: false },
                { "title": "Contact Name", "field": "ContactName", sortable: "ContactName", filter: { ContactName: "text" }, show: true },
                { "title": "Contact Tel", "field": "ContactTel", sortable: "ContactTel", filter: { ContactTel: "text" }, show: false },
                { "title": "Contact Email", "field": "Contactmail", sortable: "Contactmail", filter: { Contactmail: "text" }, show: false },

                { "title": "Bank Name", "field": "BankName", sortable: "BankName", filter: { BankName: "text" }, show: false },
                { "title": "Account Type", "field": "AccountType", sortable: "AccountType", filter: { AccountType: "text" }, show: false },
                { "title": "Beneficiary Name", "field": "BeneficiaryName", sortable: "BeneficiaryName", filter: { BeneficiaryName: "text" }, show: false },
                { "title": "Account No", "field": "AccountNo", sortable: "AccountNo", filter: { AccountNo: "text" }, show: false },
                { "title": "Branch Name", "field": "BranchName", sortable: "BranchName", filter: { BranchName: "text" }, show: false },
                { "title": "Bank Address", "field": "BankAddress", sortable: "BankAddress", filter: { BankAddress: "text" }, show: false },
                { "title": "IFSC Code", "field": "IFSCCode", sortable: "IFSCCode", filter: { IFSCCode: "text" }, show: false },
                { "title": "Swift Code", "field": "SwiftCode", sortable: "SwiftCode", filter: { SwiftCode: "text" }, show: false },
                {
                    "title": "Action", show: true,
                    'cellTemplte': function ($scope, row) {
                        var element = '<a   class="btn btn-primary btn-xs" data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.PerformaInvId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                            //'<a class="btn btn-danger btn-xs"  data-final-confirm-box=""  data-callback="$parent.$parent.$parent.$parent.$parent.$parent.Delete(row.PerformaInvId)" data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                            '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.PerformaInvId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>' +
                            '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Print(row.PerformaInvId)" data-uib-tooltip="Print"><i class="fa fa-print" ></i></a>' +
                            '<a class="btn btn-success btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.FollowReceipt(row.PerformaInvId)" data-uib-tooltip="Follow Receipt"><i class="fa fa-files-o" ></i></a>';
                        return $scope.getHtml(element);
                    }
                }
            ],
            Sort: { 'PerformaInvId': 'asc' }
        }
        $scope.Edit = function (id) {
            window.location.href = "/master/PerformaInvoice/AddPerformaInvoice/" + id + "/" + 0;
        }
        $scope.View = function (id) {
            window.location.href = "/master/PerformaInvoice/AddPerformaInvoice/" + id + "/" + 1;
        }
        $scope.Print = function (id) {
            PerformaInvoiceService.PrintPerformaInvoice(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    var htmlData = (result.data.Message);
                    var doc = new jsPDF();
                    var specialElementHandlers = {
                        '#editor': function (element, renderer) {
                            return true;
                        }
                    };
                    doc.fromHTML($('#test').html(), 15, 15, {
                        'width': 100,
                        'elementHandlers': specialElementHandlers
                    });
                    doc.fromHTML(htmlData, 10, 10, {
                        'width': 180,
                        'elementHandlers': specialElementHandlers
                    });
                    doc.save('PerformaInvoice.pdf')
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            })
        }
        $scope.FollowReceipt = function (id) {
            var modalInstance = $uibModal.open({
                templateUrl: 'myModalPerformaInvoiceList.html',
                controller: PerformaInvoiceModalInstanceCtrl,
                size: 'lg',
                resolve: {
                    PerformaInvoiceCtrl: function () { return $scope; },
                    PerformaInvoiceService: function () { return PerformaInvoiceService; },
                    id: function () { return id; },
                    PurchaseOrderService: function () { return PurchaseOrderService; }

                    //followStatus: function () { return followStatus; },
                    //MainData: function () { return maindata }
                }
            });
            modalInstance.result.then(function () {
                $scope.refreshGrid();
            }, function () {
            })
        }
        function ResetForm() {
            $scope.objPerformaInvoice = {
                PerformaInvId: 0,
                PerformaInvNo: '',
                PoId: '',
                PerformaInvDate: new Date(),
                ExpirationDate: '',
                CompanyId: 0,
                DeliveryTermId: 0,
                ToCompanyId: '',
                PaymentTermId: '',
                ModeOfShipmentId: '',
                ContactId: '',
                LoadingPortId: '',
                Description: '',
                DischargePortId: '',
                CountryOfOriginId: '',
                QtyCode: '',
                Qty: '',
                OfferPriceCode: '',
                OfferPrice: '',
                Address: '',
                BeneficialyId: '',
                FromCompanyAddress: '',
                CreatedDate: '',
                PaymentTermData: { Display: '', Value: '' },
                ModeOfShipmentData: { Display: '', Value: '' },
                PortData: { Display: '', Value: '' },
                DeliveryData: { Display: '', Value: '' },
                PortDischargeData: { Display: '', Value: '' },
                BeneficialyData: { Display: '', Value: '' },
                PoData: { Display: '', Value: '' },
                CompanyData: { Display: '', Value: '' },
                SupplierData: { Display: '', Value: '' },
                SupplierContactData: { Display: '', Value: '' },
                UnitData: { Display: '', Value: '' },
                CountryOfOriginData: { Display: '', Value: '' },
                CurrencyData: { Display: '', Value: '' },
            };
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.FormPerformaInvoice)
                $scope.FormPerformaInvoice.$setPristine();
        }
        $scope.Reset = function () {
            if ($scope.objPerformaInvoice.PerformaInvId > 0) {
                $scope.objPerformaInvoice = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        //BEGIN MANAGE SUPPLIER ADDRESS INFORMATION
        $scope.AddProductDetails = function (data) {
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'PerfomaPrdDetails.html',
                controller: ModalInstanceCtrl,
                size: 'lg',
                resolve: {
                    PerformaInvoiceCtrl: function () {
                        return $scope;
                    },
                    PerformaProductData: function () {
                        return data;
                    }
                }
            });
            modalInstance.result.then(function () {
                $scope.EditProductIndex = -1;
            }, function () {
                $scope.EditProductIndex = -1;
            })
        }
        $scope.DeleteProductDetails = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objPerformaInvoice.PerformaProductMasters[index] = data;
                } else {
                    $scope.objPerformaInvoice.PerformaProductMasters.splice(index, 1);
                }
                toastr.success("Product Detail Delete", "Success");
            })
        }
        $scope.statusFilter = function (val) {
            if (val.Status != 3)
                return true;
        }
        $scope.EditProductDetails = function (data, index) {
            $scope.EditProductIndex = index;
            $scope.AddProductDetails(data);
        }
        //END MANAGE SUPPLIER ADDRESS INFORMATION
    }

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, PerformaInvoiceCtrl, PerformaProductData, UploadProductDataService, ProductService) {

        $scope.objPerfomaPrd = {
            Id: PerformaProductData.Id,
            PerformaInvId: PerformaProductData.PerformaInvId,
            ProductId: PerformaProductData.ProductId,
            ProductCode: PerformaProductData.ProductCode,
            CategoryId: PerformaProductData.CategoryId,
            Category: PerformaProductData.Category,
            SubCategoryId: PerformaProductData.SubCategoryId,
            SubCategory: PerformaProductData.SubCategory,
            ProductDescription: PerformaProductData.ProductDescription,
            CountryOfOriginId: PerformaProductData.CountryOfOriginId,
            CountryOfOrigin: PerformaProductData.CountryOfOrigin,
            ProductModelNo: PerformaProductData.ProductModelNo,
            QtyCode: PerformaProductData.QtyCode,
            QtyCodeValue: PerformaProductData.QtyCodeValue,
            Qty: PerformaProductData.Qty,
            CurrencyCode: PerformaProductData.CurrencyCode,
            CurrencyCodeValue: PerformaProductData.CurrencyCodeValue,
            AddPerValue: PerformaProductData.AddPerValue,
            DealerPrice: PerformaProductData.DealerPrice,
            OfferPrice: PerformaProductData.OfferPrice,
            Total: PerformaProductData.Total,
            Status: PerformaProductData.Status,
            CategoryData: { Display: PerformaProductData.Category, Value: PerformaProductData.CategoryId },
            SubCategoryData: { Display: PerformaProductData.SubCategory, Value: PerformaProductData.SubCategoryId },
            ProductData: { Display: PerformaProductData.ProductName, Value: PerformaProductData.ProductId },
            CountryOfOriginData: { Display: PerformaProductData.CountryOfOrigin, Value: PerformaProductData.CountryOfOriginId },
            QtyCodeValueData: { Display: PerformaProductData.QtyCodeValue, Value: PerformaProductData.QtyCode },
            CurrencyData: { Display: PerformaProductData.CurrencyCodeValue, Value: PerformaProductData.CurrencyCode }
        };
        $scope.CountryBind = function (data) {
            CityService.CountryBind().then(function (result) {
                if (result.data.ResponseType == 1) {
                    _.each(result.data.DataList, function (value, key, list) {
                        if (value.CountryName.toString().toLowerCase() == data.toString().toLowerCase()) {
                            $scope.objSupplierAddress.CountryData = {
                                Display: value.CountryName,
                                Value: value.CountryId
                            };
                            return forEach.break();
                        }
                    });
                }
                else if (result.data.ResponseType == 3) {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }
        $scope.setPrice = function () {
            var prdprice = parseFloat(isNaN($scope.objPerfomaPrd.DealerPrice) ? 0 : $scope.objPerfomaPrd.DealerPrice);
            var Persontage = parseFloat(isNaN($scope.objPerfomaPrd.AddPerValue) ? 0 : $scope.objPerfomaPrd.AddPerValue);
            var perVal = (prdprice * (Persontage / 100));
            $scope.objPerfomaPrd.OfferPrice = (prdprice + perVal)
            $scope.setTotal();
        }
        $scope.setTotal = function () {
            var Qty = parseFloat(isNaN($scope.objPerfomaPrd.Qty) ? 0 : $scope.objPerfomaPrd.Qty);
            var OfferPrice = parseFloat(isNaN($scope.objPerfomaPrd.OfferPrice) ? 0 : $scope.objPerfomaPrd.OfferPrice);
            $scope.objPerfomaPrd.Total = ((isNaN(Qty) ? 0 : Qty) * (isNaN(OfferPrice) ? 0 : OfferPrice))
        }
        $scope.GetAllUploadProductDataInfoById = function (id) {
            UploadProductDataService.GetAllUploadProductDataInfoById(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    if (result.data.DataList.objUploadProductData.length > 0) {
                        var objProductDataMaster = result.data.DataList.objUploadProductData[0];
                        $scope.objPerfomaPrd.CategoryData = {
                            Display: objProductDataMaster.Category, Value: objProductDataMaster.CategoryId
                        };
                        $scope.objPerfomaPrd.SubCategoryData = {
                            Display: objProductDataMaster.SubCategory, Value: objProductDataMaster.SubCategoryId
                        };
                        $scope.objPerfomaPrd.ProductData = {
                            Display: objProductDataMaster.ProductName, Value: objProductDataMaster.ProductId
                        };
                    } else {
                        $scope.objPerfomaPrd.ProductCode = '';
                        $scope.objPerfomaPrd.CategoryData = {
                            Display: '', Value: ''
                        };
                        $scope.objPerfomaPrd.SubCategoryData = {
                            Display: '', Value: ''
                        };
                        $scope.objPerfomaPrd.ProductData = {
                            Display: '', Value: ''
                        };
                    }
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }
        $scope.$watch('objPerfomaPrd.ProductData', function (val) {
            if (val.Value != '' && val.Value != undefined && val.Value != null) {
                ProductService.GetProductById(val.Value).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        if (result.data.DataList != null && result.data.DataList != undefined) {
                            $scope.objPerfomaPrd.ProductCode = result.data.DataList.ProductCode;
                        }
                    } else {
                        toastr.error(result.data.Message, 'Opps, Something went wrong');
                    }
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }
        }, true)

        $scope.close = function () {
            $uibModalInstance.close();
        };
        $scope.CreateUpdate = function (data) {
            data.CurrencyCode = data.CurrencyData.Value;
            data.CurrencyCodeValue = data.CurrencyData.Display;
            data.QtyCode = data.QtyCodeValueData.Value;
            data.QtyCodeValue = data.QtyCodeValueData.Display;

            data.CountryOfOriginId = data.CountryOfOriginData.Value;
            data.CountryOfOrigin = data.CountryOfOriginData.Display;
            data.CategorId = data.CategoryData.Value;
            data.Category = data.CategoryData.Display;
            data.SubCategoryId = data.SubCategoryData.Value;
            data.SubCategory = data.SubCategoryData.Display;
            data.ProductId = data.ProductData.Value;
            data.ProductName = data.ProductData.Display;

            if (PerformaInvoiceCtrl.EditProductIndex > -1) {
                PerformaInvoiceCtrl.objPerformaInvoice.PerformaProductMasters[PerformaInvoiceCtrl.EditProductIndex] = data;
                PerformaInvoiceCtrl.EditProductIndex = -1;
            } else {
                data.Status = 1;
                PerformaInvoiceCtrl.objPerformaInvoice.PerformaProductMasters.push(data);
            }
            $scope.close();
        }
    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'PerformaInvoiceCtrl', 'PerformaProductData', 'UploadProductDataService', 'ProductService']

    var PerformaInvoiceModalInstanceCtrl = function ($scope, $uibModalInstance, $filter, $uibModal, PerformaInvoiceCtrl, PerformaInvoiceService, id, PurchaseOrderService) {

        $scope.PerfomaID = id;
        //OBJECT
        $scope.objPerfomaInvoiceModel = {
            fromCompanyData: { Display: '', Value: '' },
            toCompanyData: { Display: '', Value: '' },
            fromContactData: { Display: '', Value: '' },
            toContactData: { Display: '', Value: '' },
            fromAddressData: { Display: '', Value: '' },
            toAddressData: { Display: '', Value: '' },
            fromCustomerContact: {
                Name: '',
                Mobile: '',
                Email: '',
                Chat: ''
            },
            toCustomerContact: {
                Name: '',
                Mobile: '',
                Email: '',
                Chat: ''
            },
            fromAddressDetail: {
                Address: '',
                Tel: '',
                FAX: '',
                Email: '',
                Web: ''
            },
            toAddressDetail: {
                Address: '',
                Tel: '',
                FAX: '',
                Email: '',
                Web: ''
            }
        }

        $scope.objTransaction = {
            PerfomaPaymentId: 0,
            PerformaInvId: $scope.PerfomaID,
            DateofReceipt: new Date(),
            ReceivedAmount: 0.00,
            PaymentModeData: { Display: '', Value: '' },
            TransactionTypeModeData: { Display: '', Value: '' },
            ReceivedRemark: ''
        }

        $scope.$watch('objPerfomaInvoiceModel.fromContactData', function (data) {
            if (data) {
                if (data.Value != '') {
                    PurchaseOrderService.GetSupplierContactDetail(parseInt(data.Value)).then(function (result) {
                        if (result.data.ResponseType == 1) {
                            if (result.data.DataList.SupplierContactData[0] != undefined) {
                                var teliphone = (result.data.DataList.SupplierContactData[0].MobileNo != '') ? result.data.DataList.SupplierContactData[0].MobileNo.split(",") : [];
                                var mail = (result.data.DataList.SupplierContactData[0].Email != '') ? result.data.DataList.SupplierContactData[0].Email.split(",") : [];

                                $scope.objPerfomaInvoiceModel.fromCustomerContact = {
                                    Name: result.data.DataList.SupplierContactData[0].Surname + " " + result.data.DataList.SupplierContactData[0].ContactName,
                                    Mobile: teliphone.toString(),
                                    Email: mail.toString(),
                                    Chat: ''
                                }
                            }
                        } else if (result.ResponseType == 3) {
                            toastr.error(result.data.Message, 'Opps, Something went wrong');
                        }
                    }, function (errorMsg) {
                        toastr.error(errorMsg, 'Opps, Something went wrong');
                    })
                }
            }
        }, true)

        $scope.$watch('objPerfomaInvoiceModel.fromAddressData', function (data) {
            if (data) {
                if (data.Value != '') {
                    PerformaInvoiceService.GetAddressByBuyerID(data.Value).then(function (result) {
                        if (result.data.ResponseType == 1) {
                            if (result.data.DataList[0] != undefined) {
                                var teliphone = (result.data.DataList[0].Telephone) ? result.data.DataList[0].Telephone.split(",") : [];
                                var mail = (result.data.DataList[0].Email) ? result.data.DataList[0].Email.split(",") : [];
                                $scope.objPerfomaInvoiceModel.fromAddressDetail = {
                                    Tel: teliphone.toString(),
                                    FAX: result.data.DataList[0].Fax,
                                    Email: mail.toString(),
                                    Web: result.data.DataList[0].WebAddress
                                }
                            }
                        } else if (result.ResponseType == 3) {
                            toastr.error(result.data.Message, 'Opps, Something went wrong');
                        }
                    }, function (errorMsg) {
                        toastr.error(errorMsg, 'Opps, Something went wrong');
                    })
                }
            }
        }, true)

        $scope.$watch('objPerfomaInvoiceModel.toContactData', function (data) {
            if (data) {
                if (data.Value != '') {
                    PurchaseOrderService.GetSupplierContactDetail(parseInt(data.Value)).then(function (result) {
                        if (result.data.ResponseType == 1) {
                            if (result.data.DataList.SupplierContactData[0] != undefined) {
                                var teliphone = (result.data.DataList.SupplierContactData[0].MobileNo != '') ? result.data.DataList.SupplierContactData[0].MobileNo.split(",") : [];
                                var mail = (result.data.DataList.SupplierContactData[0].Email != '') ? result.data.DataList.SupplierContactData[0].Email.split(",") : [];

                                $scope.objPerfomaInvoiceModel.toCustomerContact = {
                                    Name: result.data.DataList.SupplierContactData[0].Surname + " " + result.data.DataList.SupplierContactData[0].ContactName,
                                    Mobile: teliphone.toString(),
                                    Email: mail.toString(),
                                    Chat: ''
                                }
                            }
                        } else if (result.ResponseType == 3) {
                            toastr.error(result.data.Message, 'Opps, Something went wrong');
                        }
                    }, function (errorMsg) {
                        toastr.error(errorMsg, 'Opps, Something went wrong');
                    })
                }
            }
        }, true)

        $scope.$watch('objPerfomaInvoiceModel.toAddressData', function (data) {
            if (data) {
                if (data.Value != '') {
                    PerformaInvoiceService.GetAddressByBuyerID(data.Value).then(function (result) {
                        if (result.data.ResponseType == 1) {
                            if (result.data.DataList[0] != undefined) {
                                var teliphone = (result.data.DataList[0].Telephone) ? result.data.DataList[0].Telephone.split(",") : [];
                                var mail = (result.data.DataList[0].Email) ? result.data.DataList[0].Email.split(",") : [];
                                $scope.objPerfomaInvoiceModel.toAddressDetail = {
                                    Tel: teliphone.toString(),
                                    FAX: result.data.DataList[0].Fax,
                                    Email: mail.toString(),
                                    Web: result.data.DataList[0].WebAddress
                                }
                            }
                        } else if (result.ResponseType == 3) {
                            toastr.error(result.data.Message, 'Opps, Something went wrong');
                        }
                    }, function (errorMsg) {
                        toastr.error(errorMsg, 'Opps, Something went wrong');
                    })
                }
            }
        }, true)

        $scope.SetPerformaId = function () {
            $scope.openTab("Click", "MainDetails", undefined, id)
        }

        $scope.GetAllPerformaInvoiceById = function () {
            PerformaInvoiceService.GetAllPerformaInvoiceById($scope.PerfomaID).then(function (result) {
                if (result) {
                    if (result.data.ResponseType == 1) {
                        $scope.performaInvoiceData = result.data.DataList.objPerformaInvoice;
                        $scope.performaProductData = result.data.DataList.objPerformaProduct;
                        $scope.performaPaymentData = result.data.DataList.objPerformaPayment;
                        $scope.performaInvoiceData.PerformaInvDate = moment(result.data.DataList.objPerformaInvoice.PerformaInvDate).format("DD-MM-YYYY");
                        $scope.performaInvoiceData.ExpirationDate = moment(result.data.DataList.objPerformaInvoice.ExpirationDate).format("DD-MM-YYYY");
                        $scope.performaInvoiceData.ConsigneTax = result.data.DataList.objPerformaInvoice.ConsigneTax ? result.data.DataList.objPerformaInvoice.ConsigneTax.split('|')[1] : null;

                        $scope.TotalAmount = result.data.DataList.TotalAmount;
                        $scope.PaidAmount = result.data.DataList.PaidAmount;

                        _.forEach($scope.performaPaymentData, function (val) {
                            val.DateofReceipt = moment(val.DateofReceipt).format("DD-MM-YYYY");
                        })
                        //$scope.AmountData = result.data.DataList.objPerformaProduct[0];

                    } else if (result.ResponseType == 3) {
                        toastr.error(result.data.Message, 'Opps, Something went wrong');
                    }
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.openTab = function (evt, tabName, data, id) {
            $scope.GetAllPerformaInvoiceById();
            var bln = true;
            var dataerror = true;
            //if (id > 0) {
            $scope.tabusershow = $scope.usertype > 1 ? true : false;
            $scope.tabadminshow = $scope.usertype == 1 ? true : false;
            //}

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
                //$scope.objProduct.EditProImgId = 0;
                //$scope.objProduct.EditProCatalogueId = 0;
            }
            if ($scope.issuccess == false && $scope.msg != '') {
                toastr.error($scope.msg);
            }
            //$scope.tabname = "active";
            //evt.currentTarget.className += "active";
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

        $scope.savePerformaPayment = function (data) {
            if (moment(data.DateofReceipt).format("DD-MM-YYYY") == "01-01-0001") {
                toastr.error("Date is Required.");
                return false;
            } else if (data.ReceivedAmount == 0) {
                toastr.error("Please enter amount.");
                return false;
            }
            else if ($scope.TotalAmount - $scope.PaidAmount - data.ReceivedAmount < 0) {
                toastr.error("Please enter valid amount.");
                return false;
            } else if (data.TransactionTypeModeData.Value == '' || !data.TransactionTypeModeData.Value) {
                toastr.error("Transection type required.");
                return false;
            }
            else if (data.PaymentModeData.Value == '' || !data.PaymentModeData.Value) {
                toastr.error("Payment mode required.");
                return false;
            }
            var objPerformaPaymentMaster = {
                PerfomaPaymentId: data.PerfomaPaymentId,
                PerformaInvId: data.PerformaInvId,
                DateofReceipt: data.DateofReceipt,
                ReceivedAmount: data.ReceivedAmount,
                PaymentModeId: data.PaymentModeData.Value,
                TransactionTypeId: data.TransactionTypeModeData.Value,
                ReceivedRemark: data.ReceivedRemark
            }
            PerformaInvoiceService.SavePerformaPayment(objPerformaPaymentMaster).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objTransaction = {
                        PerfomaPaymentId: 0,
                        PerformaInvId: $scope.PerfomaID,
                        DateofReceipt: new Date(),
                        ReceivedAmount: 0.00,
                        PaymentModeData: { Display: '', Value: '' },
                        TransactionTypeModeData: { Display: '', Value: '' },
                        ReceivedRemark: ''
                    }
                    toastr.success(result.data.Message)
                    $scope.GetAllPerformaInvoiceById();
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.EditPerformaPayment = function (data) {
            if (data.DateofReceipt) {
                var tempDate = data.DateofReceipt.split('-');
                var frinalDate = tempDate[2] + "-" + tempDate[1] + "-" + tempDate[0];
            }
            var tempData = {
                PerfomaPaymentId: data.PerfomaPaymentId,
                PerformaInvId: data.PerformaInvId,
                DateofReceipt: new Date(frinalDate),
                ReceivedAmount: data.ReceivedAmount,
                PaymentModeData: { Display: data.PaymentMode, Value: data.PaymentModeId },
                TransactionTypeModeData: { Display: data.TranType, Value: data.TransactionTypeId },
                ReceivedRemark: data.ReceivedRemark
            }
            $scope.objTransaction = angular.copy(tempData);
        }

        $scope.DeletePerformaPayment = function (id) {
            PerformaInvoiceService.DeletePerformaPayment(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    $scope.GetAllPerformaInvoiceById();
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.PerformaPaymentClear = function () {
            $scope.objTransaction = {
                PerfomaPaymentId: 0,
                PerformaInvId: $scope.PerfomaID,
                DateofReceipt: new Date(),
                ReceivedAmount: 0.00,
                PaymentModeData: { Display: '', Value: '' },
                TransactionTypeModeData: { Display: '', Value: '' },
                ReceivedRemark: ''
            }
        }
    }
    PerformaInvoiceModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', '$filter', '$uibModal', 'PerformaInvoiceCtrl', 'PerformaInvoiceService', 'id', 'PurchaseOrderService']

})()