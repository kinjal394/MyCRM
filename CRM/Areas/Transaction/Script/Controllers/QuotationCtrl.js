/// <reference path="F:\CRM\CRM\ReportViewer.aspx" />
(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("QuotationController", [
            "$scope", "QuotationService", "ProductService", "PurchaseOrderService", "CompanyService", "InquiryService", "UploadProductDataService", "$filter", "$uibModal",
            function QuotationController($scope, QuotationService, ProductService, PurchaseOrderService, CompanyService, InquiryService, UploadProductDataService, $filter, $uibModal) {
                $scope.showProductMessage = false

                $scope.gridObj = {
                    columnsInfo: [
                        { "title": "Sr.", "field": "RowNumber", show: true },
                        { "title": "Inq No", "field": "InqNo", sortable: "InqNo", filter: { InqNo: "text" }, show: false, },
                        { "title": "Quotation Number", "field": "QuotationNo", sortable: "QuotationNo", filter: { QuotationNo: "text" }, show: true, },
                        { "title": "Company Name", "field": "CompanyName", sortable: "CompanyName", filter: { CompanyName: "text" }, show: true, },
                        { "title": "Delivery Name", "field": "DeliveryName", sortable: "DeliveryName", filter: { DeliveryName: "text" }, show: true, },
                        { "title": "Term Name", "field": "TermName", sortable: "TermName", filter: { TermName: "text" }, show: true, },
                        { "title": "Quotation Made By", "field": "Name", sortable: "Name", filter: { Name: "text" }, show: true, },
                        {
                            "title": "Quotation Date", "field": "QuotationDate", sortable: "QuotationDate", filter: { QuotationDate: "date" }, show: false,
                            'cellTemplte': function ($scope, row) {
                                var element = '<p ng-bind="ConvertDate(row.QuotationDate,\'dd/mm/yyyy\')">'
                                return $scope.getHtml(element);
                            }
                        },
                        {
                            "title": "Offer Validdate", "field": "OfferValiddate", sortable: "OfferValiddate", filter: { OfferValiddate: "date" }, show: false,
                            'cellTemplte': function ($scope, row) {
                                var element = '<p ng-bind="ConvertDate(row.OfferValiddate,\'dd/mm/yyyy\')">'
                                return $scope.getHtml(element);
                            }
                        },
                        { "title": "Note", "field": "Note", sortable: "Note", filter: { Note: "text" }, show: false, },
                        {
                            "title": "Action", show: true,
                            'cellTemplte': function ($scope, row) {
                                var element = '<a   class="btn btn-primary btn-xs" data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.QuotationId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                                    //'<a class="btn btn-danger btn-xs"  data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.QuotationId)" data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                                    '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.QuotationId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>' +
                                    '<a   class="btn btn-info btn-xs" data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Print(row.QuotationId)" data-uib-tooltip="Print"><i class="fa fa-print" ></i></a>'
                                return $scope.getHtml(element);
                            }
                        }
                    ],
                    Sort: { 'QuotationId': 'asc' }
                }

                $scope.EditProductIndex = -1;
                $scope.dateOptions = {
                    formatYear: 'yy',
                    minDate: new Date(1999, 8, 5),
                    maxDate: new Date(2020, 5, 22),
                    startingDay: 1
                };

                $scope.quotationData = {
                    QuotationItemMasters: [],
                    QuotationDate: new Date()
                }
                $scope.quotationData.OfferValiddate = new Date(moment($scope.quotationData.QuotationDate, "DD-MM-YYYY").add(15, 'days'));
                $scope.productObj1 = {
                    BuyerId: '',
                    ProductCode: '',
                    ProductId: '',
                    MainProductId: '',
                    ProductDescription: '',
                    CurrSymbol: '',
                    CurrencyID: '',
                    ExCurrSymbol: '',
                    ExCurrencyID: '',
                    DealerPrice: '',
                    Percentage: '',
                    QtyCode: '',
                    QtyCodeName: '',
                    Qty: '',
                    Total: '',
                    ExRate: '',
                    ExTotal: '',
                    CategorId: '',
                    Status: 0,
                    Category: '',
                    SubCategoryId: 0,
                    SubCategory: '',
                    SupplierId: '',
                    SupplierModelName: '',
                    CategoryData: { Display: '', Value: '' },
                    SubCategoryData: { Display: '', Value: '' },
                    MainProductData: { Display: '', Value: '' },
                    ProductData: { Display: '', Value: '' },
                    SupplierModelData: { Display: '', Value: '' },
                    DealerPriceModel: { Display: '', Value: '' },
                    QtyCodeValueData: { Display: '', Value: '' },
                    CurrencyData: { Display: '', Value: '' },
                    ExCurrencyData: { Display: '', Value: '' },
                    CompanyData: { Display: '', Value: '' },
                    BuyerData: { Display: '', Value: '' },
                    QuotationMadeData: { Display: '', Value: '' },
                    DeliveryTermData: { Display: '', Value: '' },
                    PaymentTermData: { Display: '', Value: '' },
                    PortTermsData: { Display: '', Value: '' },
                    InquiryData: { Display: '', Value: '' }
                }

                $scope.$watch('quotationData.QuotationDate', function (newVal) {
                    $scope.quotationData.OfferValiddate = new Date(moment($scope.quotationData.QuotationDate, "DD-MM-YYYY").add(15, 'days'));
                })

                $scope.$watch('quotationData.CompanyData.Value', function (newVal) {
                    if (newVal && newVal > 0) {
                        CompanyService.GetCompanyById($scope.quotationData.CompanyData.Value).then(function (data) {
                            $scope.headerAddress = data.data.DataList.RegOffAdd;
                            $scope.ComName = data.data.DataList.ComName;
                            $scope.TelNos = data.data.DataList.TelNos;
                            $scope.Email = data.data.DataList.Email;
                            $scope.Web = data.data.DataList.Web;
                            if (data.data.DataList.ComLogo != null)
                                $scope.ComLogo = '/UploadImages/Companylogo/' + data.data.DataList.ComLogo;
                            else
                                $scope.ComLogo = '';
                            //$scope.quotationData.QuotationNo = "QTN";
                        }, function (errorMsg) {
                            toastr.error(errorMsg, 'Opps, Something went wrong');
                        })
                    }
                })

                $scope.$watch('quotationData.InquiryData.Value', function (newVal) {
                    if (newVal && newVal > 0) {
                        InquiryService.GetAllInquiryInfoById($scope.quotationData.InquiryData.Value).then(function (data) {
                            var Inquirydata = "";
                            if (data.data.DataList.objInquiryMaster != null) {
                                $scope.headerAddress = data.data.DataList.RegOffAdd;
                                $scope.quotationData.BuyerName = data.data.DataList.objInquiryMaster.BuyerName;
                                $scope.ContactPersonname = data.data.DataList.objInquiryMaster.ContactPersonname;
                                $scope.MobileNo = data.data.DataList.objInquiryMaster.MobileNo;
                                $scope.IEmail = data.data.DataList.objInquiryMaster.Email;
                                $scope.BuyAddress = data.data.DataList.objInquiryMaster.Address;
                            }
                        }, function (errorMsg) {
                            toastr.error(errorMsg, 'Opps, Something went wrong');
                        })
                    }
                })

                $scope.GetInvoice = function () {
                    QuotationService.GetQuotationNo().then(function (result) {
                        if (result.data.ResponseType == 1) {
                            $scope.quotationData.QuotationNo = result.data.DataList.InvCode;
                        } else if (result.ResponseType == 3) {
                            toastr.error(result.data.Message, 'Opps, Something went wrong');
                        }
                    }, function (error) {
                        $rootScope.errorHandler(error)
                    })
                }

                $scope.SetQuotationObj = function (data, isdisable) {
                    debugger;
                    if (data > 0) {
                        QuotationService.GetAllQuotationInfoById(data).then(function (result) {
                            if (result.data.ResponseType == 1) {
                                var objQuotationMaster = result.data.DataList.objQuotationMaster;
                                $scope.quotationData = objQuotationMaster;
                                $scope.quotationData.CurrencyData = { Display: objQuotationMaster.CurrSymbol, Value: objQuotationMaster.CurrencyId };
                                $scope.quotationData.ExCurrencyData = { Display: objQuotationMaster.ExCurrSymbol, Value: objQuotationMaster.ExCurrencyId };
                                $scope.quotationData.QuotationDate = $filter('mydate')(objQuotationMaster.QuotationDate);
                                $scope.quotationData.OfferValiddate = $filter('mydate')(objQuotationMaster.OfferValiddate);
                                $scope.quotationData.CompanyData = { Display: objQuotationMaster.CompanyName, Value: objQuotationMaster.CompanyId };
                                $scope.quotationData.QuotationMadeData = { Display: objQuotationMaster.UserName, Value: objQuotationMaster.QuotationMadeBy };
                                $scope.quotationData.DeliveryTermData = { Display: objQuotationMaster.DeliveryName, Value: objQuotationMaster.DeliveryTermId };
                                $scope.quotationData.PortTermsData = { Display: objQuotationMaster.TermPlace, Value: objQuotationMaster.TermPlaceId };
                                $scope.quotationData.PaymentTermData = { Display: objQuotationMaster.TermName, Value: objQuotationMaster.PaymentTermId };
                                $scope.quotationData.InquiryData = { Display: objQuotationMaster.Inqno, Value: objQuotationMaster.InqId };
                                $scope.quotationData.BuyerName = objQuotationMaster.BuyerName;
                                $scope.quotationData.QuotationItemMasters = [];
                                var quoTot = 0;
                                angular.forEach(result.data.DataList.objQuotationItemDetail, function (value) {
                                    quoTot += (value.Total == null || value.Total == '') ? 0 : value.Total;

                                    var objItemDetail = {
                                        BuyerId: value.BuyerId,
                                        CategoryId: value.CategoryId,
                                        ItemId: value.ItemId,
                                        MainProductId: value.MainProductId,
                                        OfferPrice: value.OfferPrice,
                                        OfferPriceName: value.OfferPriceName,
                                        //ProductPrice: value.ProductPrice,
                                        Percentage: value.Percentage,
                                        OfferPriceCode: value.OfferPriceCode,
                                        DealerPriseName: value.DealerPriseName,
                                        ProductDescription: value.ProductDescription,
                                        ProductCode: value.ProductCode,
                                        ProductId: value.ProductId,
                                        Qty: value.Qty,
                                        QtyCode: value.QtyCode,
                                        QtyCodeName: value.QtyCodeName,
                                        QuotationId: value.QuotationId,
                                        SubCategoryId: value.SubCategoryId,
                                        SubCategory: value.SubCategory,
                                        Total: value.Total,
                                        Category: value.Category,
                                        MainProductName: value.MainProductName,
                                        ProductName: value.ProductName,
                                        Status: 2,//1 : Insert , 2:Update ,3 :Delete
                                        SupplierId: value.SupplierId,
                                        SupplierModelName: value.SupplierModelName,
                                        PriceId: value.PriceId,
                                        DealerPrice: value.DealerPrice,
                                        CurrSymbol: value.CurrSymbol,
                                        ExRate: value.ExRate,
                                        ExTotal: value.ExTotal,
                                        CategoryData: {
                                            Display: value.Category,
                                            Value: value.CategoryId
                                        },
                                        SubCategoryData: {
                                            Display: value.SubCategory,
                                            Value: value.SubCategoryId
                                        },
                                        MainProductData: {
                                            Display: value.MainProductName,
                                            Value: value.MainProductId
                                        },
                                        ProductData: {
                                            Display: value.ProductName,
                                            Value: value.ProductId
                                        },
                                        SupplierModelData: {
                                            Display: value.SupplierModelName,
                                            Value: value.SupplierId
                                        },
                                        DealerPriceModel: {
                                            Display: value.DealerPriseName + ' ' + value.DealerPrice,
                                            Value: value.OfferPriceCode
                                        },
                                        QtyCodeValueData: {
                                            Display: value.QtyCodeName,
                                            Value: value.QtyCode
                                        },
                                        CurrencyData: {
                                            Display: value.CurrSymbol,
                                            Value: value.CurrencyId
                                        },
                                        ExCurrencyData: {
                                            Display: value.ExCurrSymbol,
                                            Value: value.ExCurrencyId
                                        }
                                    };
                                    $scope.quotationData.QuotationItemMasters.push(objItemDetail);
                                }, true);
                                $scope.quotationData.Total = quoTot;
                                $scope.storage = angular.copy($scope.quotationData);
                                $scope.isClicked = false;
                                if (isdisable == 1) {
                                    $scope.isClicked = true;
                                }
                            } else {
                                window.location.href = "/Transaction/Quotation";
                                $scope.isClicked = false;
                                toastr.error(result.data.Message, 'Opps, Something went wrong');
                            }

                            $scope.GetAddress();
                        }, function (errorMsg) {
                            toastr.error(errorMsg, 'Opps, Something went wrong');
                        });
                    }
                    else {
                        $scope.GetInvoice();
                        $scope.quotationData.QuotationItemMasters = [];
                    }
                }

                $scope.Edit = function (id) {
                    window.location.href = "/Transaction/Quotation/AddQuotation/" + id + "/" + 0;
                }


                $scope.View = function (id) {
                    window.location.href = "/Transaction/Quotation/AddQuotation/" + id + "/" + 1;
                }
                $scope.Print = function (id) {
                    window.open('/ReportViewer.aspx?ReportName=Quotation&ID=' + id, "Quotation Reports", "resizable,scrollbars,status,width=1200,height=700,menubar=no");
                }

                $scope.Add = function () {
                    window.location.href = "/Transaction/Quotation/AddQuotation/";
                }
                $scope.setCalExchangeRate = function () {

                    var Total = $scope.quotationData.Total;
                    var ExchangeRat = $scope.quotationData.ExRate == undefined ? 0 : $scope.quotationData.ExRate;
                    $scope.quotationData.ExTotal = (Total * ExchangeRat);

                }

                $scope.GetAddress = function () {
                    if ($scope.quotationData.CompanyId > 0) {
                        CompanyService.GetCompanyById($scope.quotationData.CompanyId).then(function (data) {
                            $scope.headerAddress = data.data.DataList.RegOffAdd;
                            $scope.ComName = data.data.DataList.ComName;
                            $scope.TelNos = data.data.DataList.TelNos;
                            $scope.Email = data.data.DataList.Email;
                            $scope.Web = data.data.DataList.Web;
                            if (data.data.DataList.ComLogo != null)
                                $scope.ComLogo = '/UploadImages/Companylogo/' + data.data.DataList.ComLogo
                            else
                                $scope.ComLogo = '';
                        }, function (errorMsg) {
                            toastr.error(errorMsg, 'Opps, Something went wrong');
                        })
                    }
                }

                $scope.addProducts = function (data) {
                    var modalInstance = $uibModal.open({
                        backdrop: 'static',
                        animation: true,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: 'addProductsModalContent.html',
                        controller: 'AddProductInstanceCtrl',
                        controllerAs: '$ctrl',
                        size: 'md',
                        resolve: {
                            productObj: function () { return data; },
                            QuotationController: function () { return $scope; },
                            quotationData: function () { return data; },
                            ProductService: function () { return ProductService; },
                            UploadProductDataService: function () { return UploadProductDataService; }
                        }
                    });

                    modalInstance.result.then(function () {
                        $scope.EditProductIndex = -1;
                        var totalAmount = 0;
                        _.each($scope.quotationData.QuotationItemMasters, function (element, index, list) {
                            if (element.Status != 3) {
                                totalAmount += parseInt(element.OfferPrice * element.Qty)
                            }
                        });
                        $scope.quotationData.Total = totalAmount;
                        $scope.setCalExchangeRate();
                        $scope.productObj1 = {
                            BuyerId: '',
                            ProductCode: '',
                            ProductId: '',
                            MainProductId: '',
                            ProductDescription: '',
                            Currency: '',
                            OfferPriceName: '',
                            OfferPrice: '',

                            //ProductPrice: '',
                            Percentage: '',
                            QtyCode: '',
                            QtyCodeName: '',
                            Qty: '',
                            Total: '',
                            CategorId: 0,
                            Status: 0,
                            Category: '',
                            SubCategoryId: 0,
                            SubCategory: '',
                            SupplierId: '',
                            SupplierModelName: '',
                            PriceId: 0,
                            DealerPrice: '',
                            CurrSymbol: '',
                            ExRate: '',
                            ExTotal: '',
                            CategoryData: { Display: '', Value: '' },
                            SubCategoryData: { Display: '', Value: '' },
                            MainProductData: { Display: '', Value: '' },
                            ProductData: { Display: '', Value: '' },
                            SupplierModelData: { Display: '', Value: '' },
                            DealerPriceModel: { Display: '', Value: '' },
                            QtyCodeValueData: { Display: '', Value: '' },
                            OfferPriceData: { Display: '', Value: '' },
                            CompanyData: { Display: '', Value: '' },
                            BuyerData: { Display: '', Value: '' },
                            QuotationMadeData: { Display: '', Value: '' },
                            DeliveryTermData: { Display: '', Value: '' },
                            PaymentTermData: { Display: '', Value: '' },
                            PortTermsData: { Display: '', Value: '' },
                            InquiryData: { Display: '', Value: '' }
                        }
                    }, function () {
                        $scope.EditProductIndex = -1;
                        toastr.error(errorMsg, 'Opps, Something went wrong');
                    });
                }

                $scope.DeleteProductDetail = function (data, index) {
                    $scope.$apply(function () {
                        if (data.Status == 2) {
                            data.Status = 3;
                            $scope.quotationData.QuotationItemMasters[index] = data;
                        } else {
                            $scope.quotationData.QuotationItemMasters.splice(index, 1);
                        }
                        toastr.success("Product Deleted Successfully", "Success");
                    })
                }

                $scope.EditProductDetail = function (data, index) {
                    debugger;
                    $scope.EditProductIndex = index;
                    $scope.addProducts(data);
                }

                $scope.Delete = function (id) {
                    QuotationService.deleteQuotation(id).then(function (result) {
                        if (result.data.ResponseType == 1) {
                            toastr.success(result.data.Message);
                            $scope.refreshGrid();
                        } else {
                            toastr.error(result.data.Message, 'Opps, Something went wrong');
                        }
                    }, function (errorMsg) {
                        toastr.error(errorMsg, 'Opps, Something went wrong');
                    })
                }

                $scope.statusFilter = function (val) {
                    if (val.Status != 3)
                        return true;
                }

                $scope.AddQuoatation = function () {

                    if ($scope.quotationData.QuotationItemMasters.length > 0) {
                        $scope.quotationData.CurrencyId = $scope.quotationData.CurrencyData.Value;
                        $scope.quotationData.ExCurrencyId = $scope.quotationData.ExCurrencyData.Value;

                        $scope.quotationData.CompanyId = $scope.quotationData.CompanyData.Value;
                        $scope.quotationData.DeliveryTermId = $scope.quotationData.DeliveryTermData.Value;
                        $scope.quotationData.DeliveryTerm = $scope.quotationData.DeliveryTermData.Display;
                        $scope.quotationData.PaymentTermId = $scope.quotationData.PaymentTermData.Value;
                        $scope.quotationData.PaymentTerm = $scope.quotationData.PaymentTermData.Display;
                        $scope.quotationData.TermPlaceId = $scope.quotationData.PortTermsData.Value;
                        $scope.quotationData.TermPlace = $scope.quotationData.PortTermsData.Display;
                        $scope.quotationData.QuotationMadeBy = $scope.quotationData.QuotationMadeData.Value;
                        $scope.quotationData.InqId = $scope.quotationData.InquiryData.Value;
                        $scope.quotationData.IMobileNo = $scope.MobileNo;
                        $scope.quotationData.IEmail = $scope.IEmail;
                        QuotationService.addQuoatation($scope.quotationData).then(function (result) {
                            if (result.data.ResponseType == 1) {
                                toastr.success(result.data.Message);
                                window.location.href = "/Transaction/Quotation/Index";
                            } else {
                                toastr.error(result.data.Message, 'Opps, Something went wrong');
                            }
                        }, function (errorMsg) {
                            toastr.error(errorMsg, 'Opps, Something went wrong');
                        })
                    } else {
                        $scope.showProductMessage = true
                    }
                }
            }
        ])

    angular.module('CRMApp.Controllers')
        .controller('AddProductInstanceCtrl', [
            '$scope', '$uibModalInstance', 'productObj', 'QuotationController', 'QuotationService', 'ProductService', 'PurchaseOrderService', 'UploadProductDataService',
            function ($scope, $uibModalInstance, productObj, QuotationController, QuotationService, ProductService, PurchaseOrderService, UploadProductDataService) {
                debugger
                $scope.setPrice = function () {

                    if ($ctrl.productData.DealerPrice != '') {
                        var price = $ctrl.productData.DealerPrice;
                        var prdprice = parseFloat(isNaN(price) ? 0 : price);
                        var Percentage = parseFloat(isNaN($ctrl.productData.Percentage) ? 0 : $ctrl.productData.Percentage);
                        var perVal = (prdprice * (Percentage / 100));
                        $ctrl.productData.OfferPrice = (prdprice + perVal)
                        $scope.setTotal();
                    }
                    else {
                        toastr.success("Select Dealer Price.", "");
                    }
                }

                $scope.setTotal = function () {
                    var Qty = parseFloat(isNaN($ctrl.productData.Qty) ? 0 : $ctrl.productData.Qty);
                    var OfferPrice = parseFloat(isNaN($ctrl.productData.OfferPrice) ? 0 : $ctrl.productData.OfferPrice);
                    $ctrl.productData.Total = ((isNaN(Qty) ? 0 : Qty) * (isNaN(OfferPrice) ? 0 : OfferPrice))
                }


                var $ctrl = this;
                debugger
                $scope.PrdDescription = '';
                $ctrl.productData = productObj;
                $ctrl.productData.CategoryData = {
                    Display: productObj.Category, Value: productObj.CategoryId
                };
                $ctrl.productData.SubCategoryData = {
                    Display: productObj.SubCategory, Value: productObj.SubCategoryId
                };
                $ctrl.productData.ProductData = {
                    Display: productObj.ProductName, Value: productObj.ProductId
                };
                $ctrl.productData.SupplierModelData = {
                    Display: productObj.SupplierModelName, Value: productObj.SupplierId
                };
                $scope.$watch('$ctrl.productData.SupplierModelData', function (newVal) {
                    debugger;
                    if (newVal.Value && newVal.Value > 0) {
                        QuotationService.GetDelarePriceById($ctrl.productData.ProductData.Value, parseInt(newVal.Value)).then(function (data) {
                            if (data.data.DataList.DelaerPriceData != null) {
                                $ctrl.productData.DealerPrice = data.data.DataList.DelaerPriceData.TotalAmount;
                                $ctrl.productData.PriceId = data.data.DataList.DelaerPriceData.ProductPriceId;
                            } else {
                                toastr.error('Product Dealer Price not Found.');
                                $ctrl.productData.DealerPrice = '';
                                $ctrl.productData.PriceId = '';
                            }
                        }, function (errorMsg) {
                            toastr.error(errorMsg, 'Opps, Something went wrong');
                        })
                    }
                });
                //$ctrl.productData.DealerPriceModel = {
                //    Display: productObj.DealerPriseName + ' ' + productObj.DealerPrice, Value: productObj.OfferPriceCode
                //};

                // $ctrl.productData.OfferPriceData = productObj.OfferPriceData;
                //$ctrl.productData.QtyCodeValueData = productObj.QtyCodeValueData;
                //$ctrl.productData.OfferPriceData = {
                //    Display: productObj.CurrSymbol //, Value: productObj.OfferPriceCode
                //};
                $ctrl.productData.CurrSymbol = productObj.CurrSymbol;
                $ctrl.productData.QtyCodeValueData = {
                    Display: productObj.QtyCodeName, Value: productObj.QtyCode
                };
                //$scope.$watch("$ctrl.productData.OfferPriceData", function (val) {
                //    debugger;
                //});
                $scope.$watch("$ctrl.productData.QtyCodeValueData", function (val) {
                    debugger;
                });
                //$scope.$watch('$ctrl.productData.CategoryData', function (dataa) {
                //    if (productObj.CategoryId == "" || productObj.CategoryId == undefined)
                //        var catid = productObj.CategorId == "" ? 0 : productObj.CategorId;
                //    else
                //        var catid = productObj.CategoryId;
                //    if (productObj.CategoryId != null || catid != undefined) {
                //        if (dataa.Value != catid.toString()) {
                //            $ctrl.productData.SubCategoryData.Display = '';
                //            $ctrl.productData.SubCategoryData.Value = '';
                //            $ctrl.productData.ProductData.Display = '';
                //            $ctrl.productData.ProductData.Value = '';
                //            $ctrl.productData.SupplierModelData.Display = '';
                //            $ctrl.productData.SupplierModelData.Value = '';
                //            $ctrl.productData.DealerPriceModel.Display = '';
                //            $ctrl.productData.DealerPriceModel.Value = '';
                //            $ctrl.productData.ProductCode = '';
                //        }
                //    }
                //}, true)
                //$scope.$watch('$ctrl.productData.SubCategoryData', function (dataa) {
                //    var Subcatid = productObj.SubCategoryId == "" ? 0 : productObj.SubCategoryId;
                //    if (productObj.SubCategoryId != null || Subcatid != undefined) {
                //        if (dataa.Value != Subcatid.toString()) {
                //            $ctrl.productData.ProductData.Display = '';
                //            $ctrl.productData.ProductData.Value = '';
                //            $ctrl.productData.SupplierModelData.Display = '';
                //            $ctrl.productData.SupplierModelData.Value = '';
                //            $ctrl.productData.DealerPriceModel.Display = '';
                //            $ctrl.productData.DealerPriceModel.Value = '';
                //            $ctrl.productData.ProductCode = '';
                //        }
                //    }
                //}, true)


                $scope.GetAllUploadProductDataInfoById = function (id) {

                    UploadProductDataService.GetAllUploadProductDataInfoById(id).then(function (result) {
                        if (result.data.ResponseType == 1) {
                            if (result.data.DataList.objUploadProductData.length > 0) {
                                var objProductDataMaster = result.data.DataList.objUploadProductData[0];
                                $ctrl.productData.ProductDescription = objProductDataMaster.Description;
                                $ctrl.productData.CategoryData = {
                                    Display: objProductDataMaster.Category, Value: objProductDataMaster.CategoryId
                                };
                                $ctrl.productData.SubCategoryData = {
                                    Display: objProductDataMaster.SubCategory, Value: objProductDataMaster.SubCategoryId
                                };
                                $ctrl.productData.ProductData = {
                                    Display: objProductDataMaster.ProductName, Value: objProductDataMaster.ProductId
                                };
                                $ctrl.productData.SupplierModelData = {
                                    Display: productObj.SupplierModelName, Value: productObj.SupplierId
                                };
                                debugger;
                                //$ctrl.productData.DealerPriceModel = {
                                //    Display: productObj.DealerPriseName + ' ' + productObj.DealerPrice, Value: productObj.OfferPriceCode
                                //};
                                //$ctrl.productData.DealerPriceModel = {
                                //    Display: '', Value: ''
                                //};
                                //$ctrl.productData.OfferPriceData = {
                                //    Display: productObj.CurrSymbol , Value: productObj.OfferPriceCode
                                //};
                                $ctrl.productData.QtyCodeValueData = {
                                    Display: productObj.QtyCodeName, Value: productObj.QtyCode
                                };
                            } else {
                                $ctrl.productData.ProductCode = '';
                                $ctrl.productData.CategoryData = {
                                    Display: '', Value: ''
                                };
                                $ctrl.productData.SubCategoryData = {
                                    Display: '', Value: ''
                                };
                                $ctrl.productData.ProductData = {
                                    Display: '', Value: ''
                                };
                                $ctrl.productData.SupplierModelData = {
                                    Display: '', Value: ''
                                };
                                //$ctrl.productData.DealerPriceModel = {
                                //    Display: '', Value: ''
                                //};
                                $ctrl.productData.OfferPriceData = {
                                    Display: '', Value: ''
                                };
                                $ctrl.productData.QtyCodeValueData = {
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

                $scope.$watch('$ctrl.productData.ProductData', function (val) {
                    if (val.Value != '' && val.Value != undefined && val.Value != null) {
                        debugger
                        ProductService.GetProductById(val.Value).then(function (result) {
                            if (result.data.ResponseType == 1) {
                                if (result.data.DataList != null && result.data.DataList != undefined) {
                                    $ctrl.productData.ProductCode = result.data.DataList.ProductCode;
                                    $ctrl.productData.ProductDescription = result.data.DataList.Description;
                                    $scope.PrdDescription = result.data.DataList.Description;
                                }
                            } else {
                                toastr.error(result.data.Message, 'Opps, Something went wrong');
                            }
                        }, function (errorMsg) {
                            toastr.error(errorMsg, 'Opps, Something went wrong');
                        })
                    }
                }, true)

                $ctrl.ok = function () {
                    $ctrl.productData.ProductCode = '';
                    $ctrl.productData.CategoryData = {
                        Display: '', Value: ''
                    };
                    $ctrl.productData.SubCategoryData = {
                        Display: '', Value: ''
                    };
                    $ctrl.productData.ProductData = {
                        Display: '', Value: ''
                    };
                    $uibModalInstance.close();
                };

                $ctrl.cancel = function () {
                    $uibModalInstance.dismiss('cancel');
                };

                $ctrl.SubGetCategory = function () {
                    var CategorId = $ctrl.productData.CategoryId;
                    $ctrl.subcategoryDataList = _.filter($ctrl.AllSubCategory, { 'CategoryId': CategorId });
                }

                function GetProductInfo() {
                    var CategorId = $ctrl.productData.ProductId;
                    ProductService.GetProductById(CategorId).then(function (result) {
                        if (result.data.ResponseType == 1) {
                            var resultData = result.data.DataList;
                            //$ctrl.productDataList = result.data.DataList;
                            $ctrl.ProductPhoto = resultData.ProductPhoto;
                            $ctrl.MachinePhoto = resultData.MachinePhoto;
                            $ctrl.YoutubeLink = resultData.YoutubeLink;
                            $ctrl.FbLink = resultData.FbLink;
                        }
                        else if (result.data.ResponseType == 3) {
                            toastr.error(result.data.Message, 'Opps, Something went wrong');
                        }

                    })
                }

                $ctrl.AddProductItem = function (data) {
                    debugger
                    var DPrice = data.DealerPriceModel.Display.split(' ');
                    console.log(data);
                    data.CategorId = data.CategoryData.Value;
                    data.Category = data.CategoryData.Display;
                    data.SubCategoryId = data.SubCategoryData.Value;
                    data.SubCategory = data.SubCategoryData.Display;
                    data.ProductId = data.ProductData.Value;
                    data.ProductName = data.ProductData.Display;
                    data.SupplierId = data.SupplierModelData.Value;
                    data.SupplierModelName = data.SupplierModelData.Display;
                    data.Description = $scope.PrdDescription;
                    //data.PriceId = data.DealerPriceModel.Value;
                    //data.CurrSymbol = $ctrl.productData.DealerPriceModel.Display.split(' ')[0];
                    data.PriceId = data.PriceId;
                    data.DealerPrice = data.DealerPrice;
                    data.QtyCode = data.QtyCodeValueData.Value;
                    data.ExRatePrice = data.ExRatePrice;
                    //data.OfferPriceCode = data.OfferPriceData.Value;
                    //data.CurrSymbol = data.OfferPriceData.Display;
                    data.QtyCodeName = data.QtyCodeValueData.Display;
                    //data.OfferPriceName = data.OfferPriceData.Display;
                    if (QuotationController.EditProductIndex >= 0) {
                        QuotationController.quotationData.QuotationItemMasters[QuotationController.EditProductIndex] = data;
                        QuotationController.EditProductIndex = -1;
                    } else {
                        //data.OfferPriceCode = data.OfferPriceData.Value;
                        data.Status = 1;
                        QuotationController.quotationData.QuotationItemMasters.push(data);
                    }
                    $uibModalInstance.close();
                    QuotationController.productObj1 = $ctrl.productData;
                }
            }]);
})();

