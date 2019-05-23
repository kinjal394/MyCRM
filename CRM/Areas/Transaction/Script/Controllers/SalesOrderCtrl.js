(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
            .controller("SalesOrderCtrl", [
             "$scope", "$rootScope", "$timeout", "$filter", "SalesOrderService", "NgTableParams", "$uibModal",
             SalesOrderCtrl
            ]);

    function SalesOrderCtrl($scope, $rootScope, $timeout, $filter, SalesOrderService, NgTableParams, $uibModal) {

        $scope.objSalesOrder = $scope.objSalesOrder || {};
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};
        $scope.objSalesOrder = {
            SOId: 0,
            SoNo: '',
            SoDate: '',
            BuyerId: 0,
            Email: '',
            Telephone: '',
            Address: '',
            VAT: '',
            BuyerContactId: 0,
            BEmail: '',
            BAddress: '',
            MobileCode: 0,
            MobileNo: '',
            Tel: '',
            Remark: '',
            TotalAmount: '',
            TotalAmountCode: 0,
            DeliveryTermId: 0,
            PaymentTermId: 0,
            SoRefNo: '',
            CompanyId: 0,
            SalesItemDetails: [],
            CompanyData: { Display: '', Value: '' },
            BuyerData: { Display: '', Value: '' },
            BuyerContactData: { Display: '', Value: '' },
            TotalAmountCodeData: { Display: '', Value: '' },
            DeliveryTermData: { Display: '', Value: '' },
            PaymentTermData: { Display: '', Value: '' }
        };

        $scope.SetSalesOrderId = function (id, isdisable) {
            //$scope.GetMasterInformation();
            if (id > 0) {
                //edit
                $scope.SrNo = id;
                $scope.addMode = false;
                $scope.saveText = "Update";
                $scope.GetAllSalesOrderInfoById(id);
                $scope.isClicked = false;
                if (isdisable == 1) {
                    $scope.isClicked = true;
                }
            } else {
                //add
                $scope.SrNo = 0;
                $scope.addMode = true;
                $scope.saveText = "Save";
                $scope.GetInvoice();
                $scope.isClicked = false;
            }
        }

        $scope.GetAllSalesOrderInfoById = function (id) {
            SalesOrderService.GetAllSalesOrderInfoById(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    var objSalesOrderMaster = result.data.DataList.objSalesOrderMaster;

                    $scope.objSalesOrder = {
                        SOId: objSalesOrderMaster.SOId,
                        SoNo: objSalesOrderMaster.SoNo,
                        SoDate: $filter('mydate')(objSalesOrderMaster.SoDate),
                        BuyerId: objSalesOrderMaster.BuyerId,
                        BuyerName: objSalesOrderMaster.CompanyName,
                        Email: objSalesOrderMaster.Email,
                        Tel: objSalesOrderMaster.TelNo1,
                        BAddress: objSalesOrderMaster.Address,
                        VAT: objSalesOrderMaster.VAT,
                        BuyerContactId: objSalesOrderMaster.BuyerContactId,
                        ContactPerson: objSalesOrderMaster.ContactPerson,
                        BEmail: objSalesOrderMaster.BEmail,
                        MobileCode: objSalesOrderMaster.MobileCode,
                        MobileNo: objSalesOrderMaster.MobileNo,
                        Remark: objSalesOrderMaster.Remark,
                        TotalAmount: objSalesOrderMaster.TotalAmount,
                        TotalAmountCode: objSalesOrderMaster.TotalAmountCode,
                        TotalAmountCodeName: objSalesOrderMaster.CurrencyName,
                        DeliveryTermId: objSalesOrderMaster.DeliveryTermId,
                        DeliveryTermName: objSalesOrderMaster.DeliveryName,
                        PaymentTermId: objSalesOrderMaster.PaymentTermId,
                        PaymentTermName: objSalesOrderMaster.TermName,
                        SoRefNo: objSalesOrderMaster.SoRefNo,
                        CompanyId: objSalesOrderMaster.CompanyId,
                        CompanyName: objSalesOrderMaster.CompanyName,
                        Address: objSalesOrderMaster.RegOffAdd,
                        CompanyData: {
                            Display: objSalesOrderMaster.CompanyName,
                            Value: objSalesOrderMaster.CompanyId
                        },
                        BuyerData: {
                            Display: objSalesOrderMaster.CompanyName,
                            Value: objSalesOrderMaster.BuyerId
                        },
                        BuyerContactData: {
                            Display: objSalesOrderMaster.ContactPerson,
                            Value: objSalesOrderMaster.BuyerContactId
                        },
                        TotalAmountCodeData: {
                            Display: objSalesOrderMaster.CurrencyName,
                            Value: objSalesOrderMaster.TotalAmountCode
                        },
                        DeliveryTermData: {
                            Display: objSalesOrderMaster.DeliveryName,
                            Value: objSalesOrderMaster.DeliveryTermId
                        },
                        PaymentTermData: {
                            Display: objSalesOrderMaster.TermName,
                            Value: objSalesOrderMaster.PaymentTermId
                        }
                    };

                    $scope.objSalesOrder.SalesItemDetails = [];
                    angular.forEach(result.data.DataList.objSalesItemDetail, function (value) {
                        $scope.objSalesOrder.SalesTechnicalDetails = [];
                        angular.forEach(result.data.DataList.objSalesTechnicalDetail, function (val) {
                            var objTechnicalDetail = {
                                TechDetailId: val.TechDetailId,
                                ItemId: val.ItemId,
                                TechParaId: val.TechParaId,
                                Value: val.Value,
                                Status: 2 //1 : Insert , 2:Update ,3 :Delete
                            };
                            $scope.objSalesOrder.SalesTechnicalDetails.push(objTechnicalDetail);
                        }, true);
                        var objItemDetail = {
                            ItemId: value.ItemId,
                            SOId: value.SOId,
                            CategoryId: value.CategoryId,
                            Category: value.Category,
                            SubCategoryId: value.SubCategoryId,
                            SubCategory: value.SubCategory,
                            ProductId: value.ProductId,
                            ProductName: value.ProductName,
                            ProductDescription: value.ProductDescription,
                            ModelNo: value.ModelNo,
                            OriginId: value.OriginId,
                            CountryOfOrigin: value.CountryOfOrigin,
                            QtyCodeId: value.QtyCode,
                            QtyCode: value.QtyCode,
                            QtyCodeData: value.QtyCodeData,
                            Qty: value.Qty,
                            UnitPriceCodeId: value.UnitPriceCode,
                            UnitPriceCode: value.UnitPriceCode,
                            UnitPriceCodeData: value.UnitPriceCodeData,
                            UnitPrice: value.UnitPrice,
                            Status: 2,//1 : Insert , 2:Update ,3 :Delete
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
                            OriginData: {
                                Display: value.CountryOfOrigin,
                                Value: value.OriginId
                            },
                            QtyCodeValueData: {
                                Display: value.QtyCodeData,
                                Value: value.QtyCode
                            },
                            UnitPriceCodeValueData: {
                                Display: value.UnitPriceCodeData,
                                Value: value.UnitPriceCode
                            },
                            SalesTechnicalDetails: $scope.objSalesOrder.SalesTechnicalDetails
                        };
                        $scope.objSalesOrder.SalesItemDetails.push(objItemDetail);
                    }, true);
                    $scope.storage = angular.copy($scope.objSalesOrder);
                } else {
                    window.location.href = "/Transaction/SalesOrder";
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            })
        }

        $scope.GetInvoice = function () {
            SalesOrderService.GetInvoice().then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objSalesOrder.SoNo = result.data.DataList.InvCode;
                } else if (result.ResponseType == 3) {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (error) {
                $rootScope.errorHandler(error)
            })
        }

        $scope.$watch('objSalesOrder.CompanyData', function (val) {
            if (val.Value && val.Value > 0) {
                SalesOrderService.GetCompanyDetailById(val.Value).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        $scope.objSalesOrder.Address = result.data.DataList.RegOffAdd;
                    } else if (result.ResponseType == 3) {
                        toastr.error(result.data.Message, 'Opps, Something went wrong');
                    }
                }, function (error) {
                    $rootScope.errorHandler(error)
                })
            }
        })

        $scope.$watch('objSalesOrder.BuyerData', function (val) {
            if (val.Value && val.Value > 0) {
                SalesOrderService.GetBuyerDetailById(val.Value).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        $scope.objSalesOrder.BuyerName = result.data.DataList.CompanyName;
                        angular.forEach(result.data.DataList, function (val) {
                            $scope.objSalesOrder.VAT = val.VAT;
                            $scope.objSalesOrder.BAddress = val.Address;
                            if ($scope.objSalesOrder.Email != "" && $scope.objSalesOrder.Email != null) {
                                $scope.objSalesOrder.Email += "," + val.EmailAddress;
                            } else {
                                $scope.objSalesOrder.Email = val.EmailAddress;
                            }
                            if ($scope.objSalesOrder.Tel != "" && $scope.objSalesOrder.Tel != null) {
                                $scope.objSalesOrder.Tel += "," + val.MobileNo;
                            } else {
                                $scope.objSalesOrder.Tel = val.MobileNo;
                            }
                        });
                    } else if (result.ResponseType == 3) {
                        toastr.error(result.data.Message, 'Opps, Something went wrong');
                    }
                }, function (error) {
                    $rootScope.errorHandler(error)
                })
            }
        })

        $scope.$watch('objSalesOrder.BuyerContactData', function (val) {
            if (val.Value && val.Value > 0) {
                SalesOrderService.GetBuyerContactDetailById(val.Value).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        $scope.objSalesOrder.ContactPerson = result.data.DataList.ContactPerson;
                        $scope.objSalesOrder.BEmail = result.data.DataList.Email;
                        $scope.objSalesOrder.MobileCode = result.data.DataList.MobileCode;
                        $scope.objSalesOrder.MobileNo = result.data.DataList.MobileNo;
                    } else if (result.ResponseType == 3) {
                        toastr.error(result.data.Message, 'Opps, Something went wrong');
                    }
                }, function (error) {
                    $rootScope.errorHandler(error)
                })

                //$scope.objSalesOrder.ContactPerson = parseInt(val.Value) == 0 ? '-' : _.find($scope.BuyerContactList, { ContactId: parseInt(val.Value) }).ContactPerson;
                //$scope.objSalesOrder.BEmail = parseInt(val.Value) == 0 ? '-' : _.find($scope.BuyerContactList, { ContactId: parseInt(val.Value) }).Email;
                //$scope.objSalesOrder.MobileCode = parseInt(val.Value) == 0 ? '-' : _.find($scope.BuyerContactList, { ContactId: parseInt(val.Value) }).MobileCode;
                //$scope.objSalesOrder.MobileNo = parseInt(val.Value) == 0 ? '-' : _.find($scope.BuyerContactList, { ContactId: parseInt(val.Value) }).MobileNo;
            }
        })

        $scope.Add = function () {
            window.location.href = "/Transaction/SalesOrder/AddSalesOrder";
        }

        function ResetForm() {
            $scope.objSalesOrder = {
                SOId: ($scope.SrNo && $scope.SrNo > 0) ? $scope.SrNo : 0,
                SoNo: '',
                SoDate: '',
                BuyerId: 0,
                Email: '',
                Telephone: '',
                Address: '',
                VAT: '',
                BuyerContactId: 0,
                BEmail: '',
                BAddress: '',
                MobileCode: 0,
                MobileNo: '',
                Tel: '',
                Remark: '',
                TotalAmount: '',
                TotalAmountCode: 0,
                DeliveryTermId: 0,
                PaymentTermId: 0,
                SoRefNo: '',
                CompanyId: 0,
                SalesItemDetails: [],
                CompanyData: { Display: '', Value: '' },
                BuyerData: { Display: '', Value: '' },
                BuyerContactData: { Display: '', Value: '' },
                TotalAmountCodeData: { Display: '', Value: '' },
                DeliveryTermData: { Display: '', Value: '' },
                PaymentTermData: { Display: '', Value: '' }
            };

            $scope.SalesItemDetails = {
                ItemId: 0,
                SOId: 0,
                CategoryId: 0,
                Category: '',
                SubCategoryId: 0,
                SubCategory: '',
                ProductId: 0,
                ProductDescription: '',
                ModelNo: '',
                OriginId: 0,
                QtyCode: 0,
                UnitPriceCode: 0,
                UnitPrice: '',
                Status: 0,//1 : Insert , 2:Update ,3 :Delete
                SalesTechnicalDetails: [],
                CategoryData: { Display: '', Value: '' },
                SubCategoryData: { Display: '', Value: '' },
                MainProductData: { Display: '', Value: '' },
                ProductData: { Display: '', Value: '' },
                OriginData: { Display: '', Value: '' },
                QtyCodeValueData: { Display: '', Value: '' },
                UnitPriceCodeValueData: { Display: '', Value: '' }
            };

            $scope.SalesTechnicalDetails = {
                TechDetailId: 0,
                ItemId: 0,
                TechParaId: 0,
                Value: '',
                Status: 0 //1 : Insert , 2:Update ,3 :Delete
            };

            if ($scope.FormSalesOrderInfo)
                $scope.FormSalesOrderInfo.$setPristine();
            $scope.addMode = true;
            $scope.isFirstFocus = false;
            $scope.storage = {};
            $timeout(function () {
                $scope.isFirstFocus = true;
            });
            $scope.EditSalesItemDetailsIndex = -1;
        }
        ResetForm();

        $scope.Reset = function () {
            if ($scope.addMode == false) {
                $scope.objSalesOrder = angular.copy($scope.storage);
            } else {
                ResetForm();
                $scope.SetSalesOrderId(0);
            }
        }

        $scope.RefreshTable = function () {
            $scope.tableParams.reload();
        };

        $scope.CreateUpdate = function (data) {

            // SET DDL ID 
            data.CompanyId = data.CompanyData.Value;
            data.BuyerId = data.BuyerData.Value;
            data.BuyerContactId = data.BuyerContactData.Value;
            data.TotalAmountCode = data.TotalAmountCodeData.Value;
            data.DeliveryTermId = data.DeliveryTermData.Value;
            data.PaymentTermId = data.PaymentTermData.Value;

            //Delete Techno Record
            if (data.SalesItemDetails) {
                angular.forEach(data.SalesItemDetails, function (val, ind) {
                    if (val.SalesTechnicalDetails) {
                        angular.forEach(val.SalesTechnicalDetails, function (v, i) {
                            if (v.Status == 0) {
                                val.SalesTechnicalDetails.splice(i, 1);
                            }
                        })
                    }
                })
            }

            SalesOrderService.CreateUpdate(data).then(function (result) {
                if (result.data.ResponseType == 1) {
                    ResetForm();
                    toastr.success(result.data.Message);
                    //if (data.SOId > 0) {
                    window.location.href = "/Transaction/SalesOrder";
                    //toastr.success(result.data.Message);
                    //}
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            })
        }

        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };

        $scope.gridObj = {
            columnsInfo: [
               //{ "title": "Sales Order Id", "data": "SOId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "Sales Order Number", "field": "SoNo", sortable: "SoNo", filter: { SoNo: "text" }, show: true, },
               { "title": "Buyer Name", "field": "CompanyName", sortable: "CompanyName", filter: { CompanyName: "text" }, show: true, },
               { "title": "Contact Person", "field": "ContactPerson", sortable: "ContactPerson", filter: { ContactPerson: "text" }, show: true, },
               { "title": "Delivery Name", "field": "DeliveryName", sortable: "DeliveryName", filter: { DeliveryName: "text" }, show: true, },
               { "title": "Term Name", "field": "TermName", sortable: "TermName", filter: { TermName: "text" }, show: true, },
               {
                   "title": "SoDate", "field": "SoDate", sortable: "SoDate", filter: { SoDate: "date" }, show: false,
                   'cellTemplte': function ($scope, row) {
                       var element = '<span ng-bind="ConvertDate(row.SoDate,\'dd/mm/yyyy\')"></span>'
                       return $scope.getHtml(element);
                   }
               },
               { "title": "SoRefNo", "field": "SoRefNo", sortable: "SoRefNo", filter: { SoRefNo: "text" }, show: false, },
               { "title": "Company Name", "field": "ComName", sortable: "ComName", filter: { ComName: "text" }, show: false, },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a   class="btn btn-primary btn-xs" data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.SOId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                             //'<a class="btn btn-danger btn-xs"  data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.SOId)" data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                             '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.SOId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'SOId': 'asc' }
        }

        $scope.Edit = function (id) {
            window.location.href = "/Transaction/SalesOrder/AddSalesOrder/" + id + "/" + 0;
        }
        $scope.View = function (id) {
            window.location.href = "/Transaction/SalesOrder/AddSalesOrder/" + id + "/" + 1;
        }
        $scope.Delete = function (id) {
            SalesOrderService.DeleteById(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    $scope.refreshGrid();
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            })
        }

        $scope.AddSalesItemDetails = function (data) {
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'SalesItemDetails.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    SalesOrderCtrl: function () { return $scope; },
                    SalesOrderService: function () { return SalesOrderService; },
                    SalesItemDetailsData: function () { return data; }
                }
            });
        }

        $scope.DeleteSalesOrderDetail = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objSalesOrder.SalesItemDetails[index] = data;
                } else {
                    $scope.objSalesOrder.SalesItemDetails.splice(index, 1);
                }
                toastr.success("SalesOrder contact detail Delete", "Success");
            })
        }

        $scope.EditSalesOrderDetail = function (data, index) {
            $scope.EditSalesItemDetailsIndex = index;
            $scope.AddSalesItemDetails(data);
        }

        $scope.statusFilter = function (val) {
            if (val.Status != 3)
                return true;
        }

        $scope.dateOptions = {
            formatYear: 'yy',
            //minDate: new Date(2016, 8, 5),
            //maxDate: new Date(2017, 5, 22),
            startingDay: 1
        };
    }

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, SalesOrderCtrl, SalesOrderService, SalesItemDetailsData) {

        var SalesTechList;
        $scope.SalesTechDetails = function (data) {
            SalesOrderService.GetSpecificationList().then(function (result) {
                if (result.data.ResponseType == 1) {
                    SalesTechList = result.data.DataList;


                    $scope.objSalesItemDetails = {
                        ItemId: SalesItemDetailsData.ItemId,
                        SOId: SalesItemDetailsData.SOId,
                        CategoryId: SalesItemDetailsData.CategoryId,
                        //Category: SalesItemDetailsData.Category,
                        SubCategoryId: SalesItemDetailsData.SubCategoryId,
                        //SubCategory: SalesItemDetailsData.SubCategory,
                        ProductId: SalesItemDetailsData.ProductId,
                        ProductDescription: SalesItemDetailsData.ProductDescription,
                        ModelNo: SalesItemDetailsData.ModelNo,
                        OriginId: SalesItemDetailsData.OriginId,
                        QtyCodeData: SalesItemDetailsData.QtyCodeData,
                        QtyCode: SalesItemDetailsData.QtyCodeId,
                        Qty: SalesItemDetailsData.Qty,
                        // UnitPriceCodeData: SalesItemDetailsData.UnitPriceCodeData,
                        UnitPriceCode: SalesItemDetailsData.UnitPriceCodeId,
                        UnitPrice: SalesItemDetailsData.UnitPrice,
                        Status: SalesItemDetailsData.Status,//1 : Insert , 2:Update ,3 :Delete
                        Category: SalesItemDetailsData.CategoryName,
                        SubCategory: SalesItemDetailsData.SubCategoryName,
                        ProductName: SalesItemDetailsData.ProductName,
                        UnitPriceCodeData: SalesItemDetailsData.UnitPriceCodeData,
                        CountryOfOrigin: SalesItemDetailsData.CountryOfOrigin,
                        CategoryData: SalesItemDetailsData.CategoryData,
                        SubCategoryData: SalesItemDetailsData.SubCategoryData,
                        MainProductData: SalesItemDetailsData.MainProductData,
                        ProductData: SalesItemDetailsData.ProductData,
                        OriginData: SalesItemDetailsData.OriginData,
                        QtyCodeValueData: SalesItemDetailsData.QtyCodeValueData,
                        UnitPriceCodeValueData: SalesItemDetailsData.UnitPriceCodeValueData,
                        SalesTechnicalDetails: SalesItemDetailsData.SalesTechnicalDetails

                    };


                    if ($scope.objSalesItemDetails.ItemId > 0 && $scope.objSalesItemDetails.SOId > 0) {
                        angular.forEach($scope.objSalesItemDetails.SalesTechnicalDetails, function (val, key) {
                            angular.forEach(SalesTechList, function (val1, key1) {
                                if ($scope.objSalesItemDetails.ItemId == val.ItemId) {
                                    if (val1.SpecificationId == val.TechParaId) {
                                        val1.Value = val.Value;
                                        val1.TechParaId = val1.SpecificationId;
                                        if (val.Status == 1) {
                                            val1.Status = 1;
                                        } else {
                                            val1.Status = 2;
                                        }
                                        val1.ItemId = val.ItemId;
                                        val1.TechDetailId = val.TechDetailId;
                                    }
                                }
                            });
                        });
                        $scope.objSalesItemDetails.SalesTechnicalDetails = SalesTechList;
                    } else {
                        angular.forEach($scope.objSalesItemDetails.SalesTechnicalDetails, function (val, key) {
                            angular.forEach(SalesTechList, function (val1, key1) {
                                if ($scope.objSalesItemDetails.ItemId == val.ItemId) {
                                    if (val1.SpecificationId == val.TechParaId) {
                                        val1.Value = val.Value;
                                        val1.TechParaId = val1.SpecificationId;
                                        val1.Status = 1;
                                        val1.ItemId = val.ItemId;
                                        val1.TechDetailId = val.TechDetailId;
                                    }
                                }
                            });
                        });
                        $scope.objSalesItemDetails.SalesTechnicalDetails = SalesTechList;
                    }

                } else if (result.ResponseType == 3) {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (error) {
                $rootScope.errorHandler(error)
            })
        }

        $scope.SalesTechDetails();

        $scope.close = function () {
            $uibModalInstance.close();
        };

        $scope.CreateUpdate = function (data) {

            // SET DDL ID 
            data.CategoryId = data.CategoryData.Value;
            data.SubCategoryId = data.SubCategoryData.Value;
            data.MainProductId = data.MainProductData.Value;
            data.ProductId = data.ProductData.Value;
            data.OriginId = data.OriginData.Value;
            data.QtyCode = data.QtyCodeValueData.Value;
            data.UnitPriceCode = data.UnitPriceCodeValueData.Value;

            data.Category = data.CategoryId.Display;
            data.SubCategory = data.SubCategoryData.Display;
            data.MainProductName = data.MainProductData.Display;
            data.ProductName = data.ProductData.Display;
            data.CountryOfOrigin = data.OriginData.Display;
            data.QtyCodeData = data.QtyCodeValueData.Display;
            data.UnitPriceCodeData = data.UnitPriceCodeValueData.Display;

            //data.Category = parseInt(data.CategoryId) == 0 ? '-' : _.find($scope.CategoryList, { CategoryId: parseInt(data.CategoryId) }).CategoryName;
            //data.SubCategory = parseInt(data.SubCategoryId) == 0 ? '-' : _.find($scope.ALLSubCategoryList, { SubCategoryId: parseInt(data.SubCategoryId) }).SubCategoryName;
            //data.CountryOfOrigin = parseInt(data.OriginId) == 0 ? '-' : _.find($scope.CountryOfOriginList, { OriginId: parseInt(data.OriginId) }).CountryOfOrigin;
            //data.ProductName = parseInt(data.ProductId) == 0 ? '-' : _.find($scope.ALLProductList, { ProductId: parseInt(data.ProductId) }).ProductName;
            //data.UnitPriceCodeData = parseInt(data.UnitPriceCode) == 0 ? '-' : _.find($scope.CurrencyList, { CurrencyId: parseInt(data.UnitPriceCode) }).CurrencyName;
            //data.QtyCodeData = parseInt(data.QtyCode) == 0 ? '-' : _.find($scope.UnitList, { UnitId: parseInt(data.QtyCode) }).UnitName;


            if (data.SalesTechnicalDetails) {
                angular.forEach(data.SalesTechnicalDetails, function (v, i) {
                    if (data.ItemId == v.ItemId) {
                        if (v.Value == '' || v.Value == undefined) {
                            if (v.Status == undefined) {
                                data.SalesTechnicalDetails.splice(i, 1);
                            } else {
                                v.Status = 3;
                                //v.TechParaId = v.TechParaId;
                            }
                        } else {
                            if (v.TechParaId == undefined) {
                                v.Status = 1;
                                v.TechParaId = v.SpecificationId;
                            } else {
                                if (v.Status == undefined) { v.Status = 1; } else {
                                    if (v.Status == 1) { v.Status = 1; }
                                    else if (v.Status == 2) { v.Status = 2; }
                                }
                            }
                        }
                    }
                    else {
                        if (v.Value == '' || v.Value == undefined) {
                            v.Status = 0;
                        } else {
                            v.Status = 1;
                            if (data.Status == 2)
                                v.ItemId = data.ItemId;
                            else if (data.Status == 1)
                                v.ItemId = 0;
                        }
                        v.TechParaId = v.SpecificationId;
                        //v.TechParaId = 0;
                    }
                })
            }

            if (SalesOrderCtrl.EditSalesItemDetailsIndex > -1) {
                SalesOrderCtrl.objSalesOrder.SalesItemDetails[SalesOrderCtrl.EditSalesItemDetailsIndex] = data;
                SalesOrderCtrl.EditSalesItemDetailsIndex = -1;
            } else {
                data.Status = 1;
                SalesOrderCtrl.objSalesOrder.SalesItemDetails.push(data);
            }
            //$scope.objSalesItemDetails = {
            //    ItemId: 0,
            //    SOId: 0,
            //    ProductId: 0,
            //    ProductDescription: '',
            //    ModelNo: '',
            //    OriginId: 0,
            //    QtyCode: 0,
            //    UnitPriceCode: 0,
            //    UnitPrice: 0,
            //    Status: 0
            //};
            $scope.close();
        }

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'SalesOrderCtrl', 'SalesOrderService', 'SalesItemDetailsData']

    angular.module('CRMApp.Controllers')
        .filter("mydate", function () {
            var re = /\/Date\(([0-9]*)\)\//;
            return function (x) {
                var m = x.match(re);
                if (m) return new Date(parseInt(x.substr(6)));
                else return null;
            };
        });
})()