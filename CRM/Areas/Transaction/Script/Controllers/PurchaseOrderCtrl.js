(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
            .controller("PurchaseOrderCtrl", [
                "$scope", "$rootScope", "$timeout", "$filter", "PurchaseOrderService", "ProductService", "CountryService", "UploadProductDataService", "$uibModal", "Upload",
             PurchaseOrderCtrl
            ]);

    function PurchaseOrderCtrl($scope, $rootScope, $timeout, $filter, PurchaseOrderService, ProductService, CountryService, UploadProductDataService, $uibModal, Upload) {

        $scope.objPurchaseOrder = $scope.objPurchaseOrder || {};
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};
        $scope.objPurchaseOrder = {
            PoId: 0,
            PoNo: '',
            PoRefNo: '',
            PoDate: new Date(),
            SupplierId: 0,
            Remark: '',
            TermsConditionId: 0,
            TotalAmount: '',
            TotalTax: '',
            PayableAmount: '',
            ModeOfShipmentId: 0,
            PriceCode: 0,
            Address: '',
            Tel: '',
            Email: '',
            Website: '',
            Attn: '',
            ProductCode: '',
            LandingPortName: 0,
            LandingPort: 0,
            DischargePortName: 0,
            DischargePort: 0,
            DeliveryTermId: 0,
            AddressId: 0,
            ContactId: 0,
            ContactName: '',
            PurchaseOrderDetails: [],
            SupplierModelData: { Display: '', Value: '' },
            CompanyData: { Display: '', Value: '' },
            SupplierData: { Display: '', Value: '' },
            BuyerData: { Display: '', Value: '' },
            ContactPersondata: { Display: '', Value: '' },
            BuyerAddressData: { Display: '', Value: '' },
            BuyerTel: '',
            BuyerEmail: '',
            ShipmentData: { Display: '', Value: '' },
            PriceCodeData: { Display: '', Value: '' },
            TermsAndConditionData: { Display: '', Value: '' },
            LandingPortData: { Display: '', Value: '' },
            DischargePortData: { Display: '', Value: '' },
            DeliveryTermData: { Display: '', Value: '' },
            SupplierContactData: { Display: '', Value: '' },
            SupplierAddressData: { Display: '', Value: '' },
        };
        $scope.telCodeData = [];
        $scope.openTab = function (evt, tabName, data, id) {
            if (data != undefined) {
                data.BuyerTel = this.Buyerteliphone != undefined ? this.Buyerteliphone.toString() : "";
                data.Tel = this.teliphone != undefined ? this.teliphone.toString() : "";
                data.BuyerEmail = this.Buyermail != undefined ? this.Buyermail.toString() : "";
                data.Email = this.mail != undefined ? this.mail.toString() : "";
            }
            //data.BuyerTel = this.Buyerteliphone != undefined ? this.Buyerteliphone.toString() : "";
            // Declare all variables
            debugger
            var bln = true;
            var dataerror = true;
            //if (id > 0) {
            $scope.tabusershow = $scope.usertype > 1 ? true : false;
            $scope.tabadminshow = $scope.usertype == 1 ? true : false;
            //}
            if ($scope.isClicked == false) {
                var dataerror = true;
                if (data != undefined) {
                    if (tabName == 'ProductDetails' || tabName == 'ShipmentDetails' || tabName == 'TermsDetails') {
                        if (data.CompanyData.Display == "" || data.CompanyData.Display == null) {
                            toastr.error("Please Select Company");
                            dataerror = false;
                            return false;
                        }
                        else if (data.PoNo == "" || data.PoNo == null) {
                            toastr.error("Please Enter Purchase Order No.");
                            dataerror = false;
                            return false;
                        }
                        else if (data.PoDate == "" || data.PoDate == null) {
                            toastr.error("Please Enter Purchase Order Date.");
                            dataerror = false;
                            return false;
                        }
                        else if (data.BuyerData.Display == "" || data.BuyerData.Display == null) {
                            toastr.error("Please Select Buyer.");
                            dataerror = false;
                            return false;
                        }
                        else if (data.SupplierData.Display == "" || data.SupplierData.Display == null) {
                            toastr.error("Please Select Supplier.");
                            dataerror = false;
                            return false;
                        }
                        else if (data.ContactPersondata.Display == "" || data.ContactPersondata.Display == null) {
                            toastr.error("Please Select Buyer Contact Person.");
                            dataerror = false;
                            return false;
                        }
                        else if (data.SupplierContactData.Display == "" || data.SupplierContactData.Display == null) {
                            toastr.error("Please Select Supplier Contact.");
                            dataerror = false;
                            return false;
                        }
                        else if (data.BuyerAddressData.Display == "" || data.BuyerAddressData.Display == null) {
                            toastr.error("Please Select Buyer Address.");
                            dataerror = false;
                            return false;
                        }
                        else if (data.SupplierAddressData.Display == "" || data.SupplierAddressData.Display == null) {
                            toastr.error("Please Select Supplier Address.");
                            dataerror = false;
                            return false;
                        }
                        else if (data.BuyerTel.toString() == "" || data.BuyerTel.toString() == null) {
                            toastr.error("Please Enter Mobile No.");
                            dataerror = false;
                            return false;
                        }
                        else if (data.Tel.toString() == "" || data.Tel.toString() == null) {
                            toastr.error("Please Enter Mobile No.");
                            dataerror = false;
                            return false;
                        }
                        else if (data.BuyerEmail.toString() == "" || data.BuyerEmail.toString() == null) {
                            toastr.error("Please Enter Email");
                            dataerror = false;
                            return false;
                        }
                        else if (data.Email.toString() == "" || data.Email.toString() == null) {
                            toastr.error("Please Enter Email.");
                            dataerror = false;
                            return false;
                        } else {
                            dataerror = true;
                        }
                    }
                    //if (tabName == 'PhotosDetails' || tabName == 'TechnicalspecificationDetail' || tabName == 'ShipmentDetails' || tabName == 'TermsDetails') {
                    //    if (data.CategoryData.Display == "" || data.CategoryData.Display == null) {
                    //        toastr.error("Please Select Category.");
                    //        dataerror = false;
                    //        return false;
                    //    }
                    //    else if (data.SubCategoryData.Display == "" || data.SubCategoryData.Display == null) {
                    //        toastr.error("Please Select SubCategory.");
                    //        dataerror = false;
                    //        return false;
                    //    }
                    //    else if (data.ProductData.Display == "" || data.ProductData.Display == null) {
                    //        toastr.error("Please Select Product.");
                    //        dataerror = false;
                    //        return false;
                    //    }
                    //    else if (data.Qty == "" || data.Qty == null) {
                    //        toastr.error("Please Enter Qty.");
                    //        dataerror = false;
                    //        return false;
                    //    }
                    //    else if (data.QtyCodeValueData.Display == "" || data.QtyCodeValueData.Display == null) {
                    //        toastr.error("Please Select Qty Code.");
                    //        dataerror = false;
                    //        return false;
                    //    }
                    //    else if (data.UnitPrice == "" || data.UnitPrice == null) {
                    //        toastr.error("Please Enter UnitPrice.");
                    //        dataerror = false;
                    //        return false;
                    //    }
                    //    else if (data.UnitPriceCodeValueData.Display == "" || data.UnitPriceCodeValueData.Display == null) {
                    //        toastr.error("Please Select Unit Price Code.");
                    //        dataerror = false;
                    //        return false;
                    //    }
                    //    else if (data.ModelNo == "" || data.ModelNo == null) {
                    //        toastr.error("Please Enter ModelNo.");
                    //        return false;
                    //    }
                    //    else {
                    //        dataerror = true;
                    //    }
                    //}
                    if (tabName == 'TermsDetails') {
                        if (data.LandingPortData.Display == "" || data.LandingPortData.Display == null) {
                            toastr.error("Please Select Port Of Loading.");
                            dataerror = false;
                            return false;
                        }
                        else if (data.DischargePortData.Display == "" || data.DischargePortData.Display == null) {
                            toastr.error("Please Select Port Of Discharge.");
                            return false;
                        }
                        else {
                            dataerror = true;
                        }
                    }
                    if (dataerror == true) {
                        $scope.CreateUpdate(data);
                        $scope.GetAllPurchaseOrderInfoById(data.PoId);
                    }
                }
            }
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

        //SET DEFAULT BUYER ID
        $scope.SetPurchaseOrderId = function (id, isdisable) {
            $scope.openTab("Click", "BasicDetails", undefined, id)
            CountryService.GetCountryFlag().then(function (result) {
                $scope.telCodeData = angular.copy(result);
                if (id > 0) {
                    //edit
                    $scope.SrNo = id;
                    $scope.addMode = false;
                    $scope.saveText = "Update";
                    $scope.GetAllPurchaseOrderInfoById(id);
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
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.GetAllPurchaseOrderInfoById = function (id) {
            PurchaseOrderService.GetAllPurchaseOrderInfoById(id).then(function (result) {
                if (result.data.ResponseType == 1 && result != undefined) {
                    debugger;
                    var objPurchaseOrderMaster = result.data.DataList.objPurchaseOrderMaster;
                    //123
                    objPurchaseOrderMaster.CompanyData = { Display: objPurchaseOrderMaster.CompanyName, Value: objPurchaseOrderMaster.ComId },
                    objPurchaseOrderMaster.BuyerData = { Display: objPurchaseOrderMaster.BuyerComName, Value: objPurchaseOrderMaster.BuyerId },
                    objPurchaseOrderMaster.SupplierData = { Display: objPurchaseOrderMaster.SupplierComName, Value: objPurchaseOrderMaster.SupplierId },
                    objPurchaseOrderMaster.ContactPersondata = { Display: objPurchaseOrderMaster.BuyerContactperson, Value: objPurchaseOrderMaster.BuyerContactId },
                    objPurchaseOrderMaster.SupplierContactData = { Display: objPurchaseOrderMaster.SupplierContactperson, Value: objPurchaseOrderMaster.SupplierContactId },
                    objPurchaseOrderMaster.BuyerAddressData = { Display: objPurchaseOrderMaster.BuyerAddress, Value: objPurchaseOrderMaster.AddressId },
                    objPurchaseOrderMaster.SupplierAddressData = { Display: objPurchaseOrderMaster.Address, Value: objPurchaseOrderMaster.AddressId }
                    //objPurchaseOrder.BuyerWebsite
                    $scope.objPurchaseOrder.BuyerWebsite = objPurchaseOrderMaster.BuyerWebsite;
                    $scope.objPurchaseOrder.Website = objPurchaseOrderMaster.Website;

                    objPurchaseOrderMaster.ShipmentData = { Display: objPurchaseOrderMaster.ModeOfShipment, Value: objPurchaseOrderMaster.ModeOfShipmentId },
                    objPurchaseOrderMaster.PriceCodeData = { Display: objPurchaseOrderMaster.PriceCodeName, Value: objPurchaseOrderMaster.PriceCode },
                    objPurchaseOrderMaster.DeliveryTermData = { Display: objPurchaseOrderMaster.TermsCondition, Value: objPurchaseOrderMaster.DeliveryTermId }
                    objPurchaseOrderMaster.LandingPortData = { Display: objPurchaseOrderMaster.LandingPortName, Value: objPurchaseOrderMaster.LandingPort }
                    objPurchaseOrderMaster.DischargePortData = { Display: objPurchaseOrderMaster.DischargePortName, Value: objPurchaseOrderMaster.DischargePort }
                    //objPurchaseOrderMaster.SupplierContactData = { Display: objPurchaseOrderMaster.ContactName, Value: objPurchaseOrderMaster.ContactId }

                    objPurchaseOrderMaster.PoDate = $filter('mydate')(objPurchaseOrderMaster.PoDate);
                    $scope.objPurchaseOrder = objPurchaseOrderMaster;
                    $scope.objPurchaseOrder.PurchaseOrderDetails = [];
                    $scope.objPurchaseOrder.TechSpecParameterMasters = [];
                    //$scope.objPurchaseOrder.PurchaseOrderDetails = result.data.DataList.objPurchaseOrderDetailMaster;
                    angular.forEach(result.data.DataList.objPurchaseOrderDetailMaster, function (value, index) {
                        var objItemDetail = {
                            PoDetailId: value.PoDetailId,
                            PoDetailIndex: index,
                            PoId: value.PoId,
                            CategoryId: value.CategoryId,
                            Category: value.Category,
                            SubCategoryId: value.SubCategoryId,
                            SubCategory: value.SubCategory,
                            MianProductId: value.MainProductId,
                            MainProduct: value.MainProduct,
                            ProductId: value.ProductId,
                            Product: value.Product,
                            Description: value.Description,
                            QtyCode: value.QtyCode,
                            QtyCodeData: value.QtyCodeData,
                            Qty: value.Qty,
                            PriceCode: value.PriceCode,
                            PriceCodeData: value.PriceCodeData,
                            UnitPrice: value.UnitPrice,
                            Amount: value.Amount,
                            ModelNo: value.ModelNo,
                            SupplierModelData: { Display: value.ModelNo, Value: '0' },
                            ProductPhotoes: "/UploadImages/PurchaseOrder/" + value.ProductPhotoes,
                            MachinaryPhotoes: "/UploadImages/PurchaseOrder/" + value.MachinaryPhotoes,
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
                                Display: value.MainProduct,
                                Value: value.MainProductId
                            },
                            ProductData: {
                                Display: value.Product,
                                Value: value.ProductId
                            },
                            QtyCodeValueData: {
                                Display: value.QtyCodeData,
                                Value: value.QtyCode
                            },
                            UnitPriceCodeValueData: {
                                Display: value.PriceCodeData,
                                Value: value.PriceCode
                            }
                        }
                        //$scope.tempProductsImagePath = "/UploadImages/PurchaseOrder/" + value.ProductPhotoes;
                        //$scope.tempMachineryImagePath = "/UploadImages/PurchaseOrder/" + value.MachinaryPhotoes;
                        $scope.objPurchaseOrder.PurchaseOrderDetails.push(objItemDetail);

                        if (result.data.DataList.objPurchaseOrderTechnicalDetail.length > 0) {
                            angular.forEach(result.data.DataList.objPurchaseOrderTechnicalDetail, function (value) {
                                if (value.PoDetailId == objItemDetail.PoDetailId) {
                                    var ProductPara = {
                                        POSpecId: value.POSpecId,
                                        PoDetailIndex: objItemDetail.PoDetailIndex,
                                        PoDetailId: value.PoDetailId,
                                        SpecId: value.SpecId,
                                        SpecName: value.SpecName,
                                        SpecVal: value.SpecVal,
                                        Status: 2, //1 : Insert , 2:Update ,3 :Delete
                                        SpecHeadId: value.SpecHeadId,
                                        SpecHead: value.SpecHead
                                    }
                                    $scope.objPurchaseOrder.TechSpecParameterMasters.push(ProductPara);
                                }
                            }, true);
                        }
                    }, true);

                    //var dataSample = _.forEach($scope.ListTempTechnicalSpec, function (val) {
                    //    var tempData = _.filter(result.data.DataList.TechSpecParameterMasters, { 'TechParaId': val.TechParaId })[0];
                    //    if (tempData) {
                    //        val.TechDetailId = tempData.TechDetailId;
                    //        val.ProductId = tempData.ProductId;
                    //        val.TechParaId = tempData.TechParaId;
                    //        val.TechSpec = tempData.TechSpec;
                    //        val.Value = tempData.Value;
                    //    }
                    //})
                    //$scope.objPurchaseOrder.TechSpecParameterMasters = dataSample;
                    $scope.storage = angular.copy($scope.objPurchaseOrder);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.GetInvoice = function () {
            //PurchaseOrderService.GetInvoice().then(function (result) {
            //    if (result.data.ResponseType == 1) {
            //        $scope.objPurchaseOrder.PoNo = result.data.DataList.InvCode;
            //        //$scope.objPurchaseOrder.TechSpecParameterMasters = result.data.DataList.ListTechnicalSpecMaster;
            //    } else if (result.ResponseType == 3) {
            //        toastr.error(result.data.Message, 'Opps, Something went wrong');
            //    }
            //}, function (errorMsg) {
            //    toastr.error(errorMsg, 'Opps, Something went wrong');
            //})
            $scope.$watch('objPurchaseOrder.CompanyData', function (data) {
                debugger
                if (data != undefined) {
                    if (data.Value != '') {
                        PurchaseOrderService.GetOrderNo(parseInt(data.Value)).then(function (result) {
                            if (result.data.DataList != undefined)
                                $scope.objPurchaseOrder.PoNo = result.data.DataList.PRNo;
                            // $scope.objPurchaseOrderDetail.BuyerAddress = result.data.DataList.companydata.RegOffAdd;
                            //$scope.objPurchaseOrderDetail.ContactPerson = result.data.DataList.companydata.ContactPerson;
                            // $scope.Buyerteliphone = (result.data.DataList.companydata.TelNos != '') ? result.data.DataList.companydata.TelNos.split(",") : [];
                            // $scope.Buyermail = (result.data.DataList.companydata.Email != '') ? result.data.DataList.companydata.Email.split(",") : [];

                            // $scope.objPurchaseOrder.BuyerTel = $scope.Buyerteliphone.toString();
                            // $scope.objPurchaseOrder.BuyerEmail = $scope.Buyermail.toString();

                        }, function (errorMsg) {
                            toastr.error(errorMsg, 'Opps, Something went wrong');
                        })
                    }
                }
            }, true)
        }



        $scope.Add = function () {
            window.location.href = "/Transaction/PurchaseOrder/AddPurchaseOrder";
        }

        function ResetForm() {
            $scope.objPurchaseOrder = {
                PoId: ($scope.SrNo && $scope.SrNo > 0) ? $scope.SrNo : 0,
                PoNo: '',
                PoRefNo: '',
                PoDate: new Date(),
                SupplierId: 0,
                Remark: '',
                TermsConditionId: 0,
                TotalAmount: '',
                TotalTax: '',
                PayableAmount: '',
                ModeOfShipmentId: 0,
                PriceCode: 0,
                Address: '',
                Tel: '',
                Email: '',
                BuyerTel: '',
                BuyerEmail: '',
                Website: '',
                BuyerWebsite: '',
                Attn: '',
                ProductCode: '',
                LandingPortName: 0,
                LandingPort: 0,
                DischargePortName: 0,
                DischargePort: 0,
                DeliveryTermId: 0,
                AddressId: 0,
                ContactId: 0,
                ContactName: '',
                PurchaseOrderDetails: [],
                TechSpecParameterMasters: [],
                CompanyData: { Display: '', Value: '' },
                SupplierData: { Display: '', Value: '' },
                BuyerData: { Display: '', Value: '' },
                ContactPersondata: { Display: '', Value: '' },
                BuyerAddressData: { Display: '', Value: '' },
                ShipmentData: { Display: '', Value: '' },
                PriceCodeData: { Display: '', Value: '' },
                TermsAndConditionData: { Display: '', Value: '' },
                LandingPortData: { Display: '', Value: '' },
                DischargePortData: { Display: '', Value: '' },
                DeliveryTermData: { Display: '', Value: '' },
                SupplierContactData: { Display: '', Value: '' },
                SupplierAddressData: { Display: '', Value: '' },
            };
            //--PoId, PoNo, PoRefNoPoDate, SupplierId, Remark, TermsConditionId, TotalAmount, TotalTax, PayableAmount, ModeOfShipmentId, PriceCode, 
            //CreatedBy, CreatedDate, ModifyBy, ModifyDate, DeletedBy, DeletedDate, IsActive, Address, Tel, Email, Website, Attn, LandingPort, DischargePort, DeliveryTermId
            //--PoDetailId, PoId, ProductId, Description, QtyCode, Qty, PriceCode, UnitPrice, Amount, IsActive, ModelNom, ProductPhotoes, MachinaryPhotoes
            //--POSpecId, SpecId, SpecVal, PoDetailId,
            $scope.objPurchaseOrderDetail = {
                PoDetailId: 0,
                PoId: 0,
                PoDetailIndex: '',
                CategoryId: 0,
                Category: '',
                SubCategoryId: 0,
                SubCategory: '',
                ProductId: 0,
                Product: '',
                Description: '',
                QtyCode: 0,
                QtyCodeData: '',
                Qty: '',
                PriceCode: 0,
                PriceCodeData: '',
                UnitPrice: '',
                Amount: '',
                //ModelNo: '',
                ProductPhotoes: '',
                MachinaryPhotoes: '',
                Status: 0,//1 : Insert , 2:Update ,3 :Delete
                SupplierModelData: { Display: '', Value: '' },
                CategoryData: { Display: '', Value: '' },
                SubCategoryData: { Display: '', Value: '' },
                MainProductData: { Display: '', Value: '' },
                ProductData: { Display: '', Value: '' },
                QtyCodeValueData: { Display: '', Value: '' },
                UnitPriceCodeValueData: { Display: '', Value: '' },
                TechSpecParameterMasters: [],
            }
            $scope.TechSpecParameterMasters = {
                POSpecId: 0,
                SpecId: 0,
                SpecName: '',
                SpecVal: '',
                PoDetailIndex: '',
                PoDetailId: 0,
                SpecificationData: { Display: '', Value: '' },
                Status: 0,//1 : Insert , 2:Update ,3 :Delete
            }

            if ($scope.FormPurchaseOrder)
                $scope.FormPurchaseOrder.$setPristine();
            $scope.addMode = true;
            $scope.isFirstFocus = false;
            $scope.storage = {};
            $timeout(function () {
                $scope.isFirstFocus = true;
            });
            $scope.EditPurchaseOrderDetailIndex = -1;
            $scope.EditPurchaseParameterIndex = -1;
        }
        ResetForm();

        $scope.$watch('objPurchaseOrder.SupplierData', function (data) {
            if (data != undefined) {
                if (data.Value != '') {
                    PurchaseOrderService.GetSupplierDetail(parseInt(data.Value)).then(function (result) {
                        if (result.data.ResponseType == 1) {
                            // $scope.objPurchaseOrder.Website = result.data.DataList.SupplierData.Website;
                            if ($scope.objPurchaseOrder.PoRefNo == null || $scope.objPurchaseOrder.PoRefNo == '') {
                                $scope.objPurchaseOrder.PoRefNo = result.data.DataList.PoRefNo;
                                // $scope.objPurchaseOrder.SupplierAddressData = result.data.DataList.SupplierData.Address;
                                //$scope.objPurchaseOrder.SupplierContactData = result.data.DataList.SupplierData.ContactPerson;
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

        $scope.$watch('objPurchaseOrder.SupplierContactData', function (data) {
            if (data != undefined || data != null) {
                if (data.Value != '') {
                    PurchaseOrderService.GetSupplierContactDetail(parseInt(data.Value)).then(function (result) {
                        if (result.data.ResponseType == 1) {
                            if (result.data.DataList.SupplierContactData[0] != undefined) {
                                $scope.teliphone = (result.data.DataList.SupplierContactData[0].MobileNo != '') ? result.data.DataList.SupplierContactData[0].MobileNo.split(",") : [];
                                $scope.mail = (result.data.DataList.SupplierContactData[0].Email != '') ? result.data.DataList.SupplierContactData[0].Email.split(",") : [];
                                $scope.objPurchaseOrder.Tel = $scope.teliphone.toString();
                                $scope.objPurchaseOrder.Email = $scope.mail.toString();
                                $scope.objPurchaseOrder.SupplierTax = result.data.DataList.SupplierContactData[0].TaxDetais;
                                $scope.SupplierTaxDetails = result.data.DataList.SupplierContactData[0].TaxDetais.split(',');
                            }
                            //$scope.objPurchaseOrder.Website = result.data.DataList.SupplierContactData[0].webaddress;
                            // $scope.objPurchaseOrder.supplierTaxDetails = result.data.DataList.SupplierContactData[0].TaxDetais;

                        } else if (result.ResponseType == 3) {
                            toastr.error(result.data.Message, 'Opps, Something went wrong');
                        }
                    }, function (errorMsg) {
                        toastr.error(errorMsg, 'Opps, Something went wrong');
                    })
                }
            }
        }, true)
        $scope.$watch('objPurchaseOrder.ContactPersondata', function (data) {
            if (data != undefined || data != null) {
                if (data.Value != '') {
                    PurchaseOrderService.GetSupplierContactDetail(parseInt(data.Value)).then(function (result) {
                        if (result.data.ResponseType == 1) {
                            if (result.data.DataList.SupplierContactData[0] != undefined) {
                                $scope.Buyerteliphone = (result.data.DataList.SupplierContactData[0].MobileNo != '') ? result.data.DataList.SupplierContactData[0].MobileNo.split(",") : [];
                                $scope.objPurchaseOrder.BuyerTel = $scope.Buyerteliphone.toString();
                                $scope.Buyermail = (result.data.DataList.SupplierContactData[0].Email != '') ? result.data.DataList.SupplierContactData[0].Email.split(",") : [];
                                $scope.objPurchaseOrder.BuyerEmail = $scope.Buyermail.toString();
                                //$scope.objPurchaseOrder.BuyerWebsite = result.data.DataList.SupplierContactData[0].webaddress;
                                $scope.objPurchaseOrder.BuyerTax = result.data.DataList.SupplierContactData[0].TaxDetais;
                                $scope.BuyerTaxDetails = result.data.DataList.SupplierContactData[0].TaxDetais.split(',');
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
        $scope.GetAllUploadProductDataInfoById = function (id) {
            UploadProductDataService.GetAllUploadProductDataInfoById(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    if (result.data.DataList.objUploadProductData.length > 0) {
                        var objProductDataMaster = result.data.DataList.objUploadProductData[0];
                        $scope.objPurchaseOrderDetail.CategoryData = {
                            Display: objProductDataMaster.Category, Value: objProductDataMaster.CategoryId
                        };
                        $scope.objPurchaseOrderDetail.SubCategoryData = {
                            Display: objProductDataMaster.SubCategory, Value: objProductDataMaster.SubCategoryId
                        };
                        $scope.objPurchaseOrderDetail.ProductData = {
                            Display: objProductDataMaster.ProductName, Value: objProductDataMaster.ProductId
                        };
                    } else {
                        $scope.objPurchaseOrderDetail.ProductCode = '';
                        $scope.objPurchaseOrderDetail.CategoryData = {
                            Display: '', Value: ''
                        };
                        $scope.objPurchaseOrderDetail.SubCategoryData = {
                            Display: '', Value: ''
                        };
                        $scope.objPurchaseOrderDetail.ProductData = {
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

        $scope.$watch('objPurchaseOrderDetail.ProductData', function (val) {
            if (val.Value != '' && val.Value != undefined && val.Value != null) {
                ProductService.GetProductById(val.Value).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        $scope.objPurchaseOrderDetail.ProductCode = result.data.DataList.ProductCode;
                    } else {
                        toastr.error(result.data.Message, 'Opps, Something went wrong');
                    }
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }
        }, true)

        $scope.Reset = function () {
            if ($scope.addMode == false) {
                $scope.objPurchaseOrder = angular.copy($scope.storage);
            } else {
                ResetForm();
                $scope.SetPurchaseOrderId(0);
            }
        }

        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };

        $scope.CreateUpdate = function (data, tag) {
            debugger;
            data.BuyerTel = this.Buyerteliphone != undefined ? this.Buyerteliphone.toString() : "";
            data.BuyerEmail = this.Buyermail != undefined ? this.Buyermail.toString() : "";
            data.Tel = this.teliphone != undefined ? this.teliphone.toString() : "";
            data.Email = this.mail != undefined ? this.mail.toString() : "";
            data.ComId = data.CompanyData.Value;
            data.BuyerId = data.BuyerData.Value;
            data.SupplierId = data.SupplierData.Value;
            data.BuyerContactperson = data.ContactPersondata.Display;
            data.SupplierContactperson = data.SupplierContactData.Display;
            data.BuyerAddress = data.BuyerAddressData.Display;
            data.Address = data.SupplierAddressData.Display;

            data.ModeOfShipmentId = (data.ShipmentData.Value == 0) ? null : data.ShipmentData.Value;
            data.PriceCode = (data.PriceCodeData.Value == 0) ? null : data.PriceCodeData.Value;
            //data.DeliveryTermId = (data.TermsAndConditionData.Value == 0) ? null : data.TermsAndConditionData.Value;
            if (data.DeliveryTermData != undefined) {
                data.DeliveryTermId = (data.DeliveryTermData.Value == 0) ? null : data.DeliveryTermData.Value;
            }
            if (data.LandingPortData != undefined) {
                data.LandingPort = (data.LandingPortData.Value == 0) ? null : data.LandingPortData.Value;
            }
            if (data.DischargePortData != undefined) {
                data.DischargePort = (data.DischargePortData.Value == 0) ? null : data.DischargePortData.Value;
            }

            angular.forEach(data.PurchaseOrderDetails, function (value, index) {
                if (value.MachinaryPhotoes != undefined) {
                    var dataMachinary = value.MachinaryPhotoes.split('/');
                    data.PurchaseOrderDetails[index].MachinaryPhotoes = dataMachinary[dataMachinary.length - 1];
                    //data.ProductPhotoMasters[index].Photo = value.Photo.substring((value.Photo.length - 19), (value.Photo.length));
                }
                if (value.ProductPhotoes != undefined) {
                    var dataphoto = value.ProductPhotoes.split('/');
                    data.PurchaseOrderDetails[index].ProductPhotoes = dataphoto[dataphoto.length - 1];
                    //data.ProductPhotoMasters[index].Photo = value.Photo.substring((value.Photo.length - 19), (value.Photo.length));
                }
            }, true);
            data.Address = data.SupplierAddressData.Display;
            data.PurchaseOrderDetails = data.PurchaseOrderDetails;
            data.TechSpecParameterMasters = data.TechSpecParameterMasters;
            PurchaseOrderService.CreateUpdate(data).then(function (result) {
                if (result.data.ResponseType == 1) {
                    if (tag == 'final') {
                        ResetForm();
                        toastr.success(result.data.Message);
                        window.location.href = "/Transaction/PurchaseOrder";
                    } else {
                        $scope.objPurchaseOrder.PoId = result.data.DataList.valueData;
                    }
                    angular.forEach(result.data.DataList.objPurchaseOrderDetailMaster, function (value, index) {
                        if (value.MachinaryPhotoes != undefined) {
                            data.PurchaseOrderDetails[index].MachinaryPhotoes = "/UploadImages/PurchaseOrder/" + value.MachinaryPhotoes;
                            //data.ProductPhotoMasters[index].Photo = value.Photo.substring((value.Photo.length - 19), (value.Photo.length));
                        }
                        if (value.ProductPhotoes != undefined) {
                            data.PurchaseOrderDetails[index].ProductPhotoes = "/UploadImages/PurchaseOrder/" + value.ProductPhotoes;
                            //data.ProductPhotoMasters[index].Photo = value.Photo.substring((value.Photo.length - 19), (value.Photo.length));
                        }
                    }, true);

                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.gridObj = {
            columnsInfo: [
               //{ "title": "PoId", "data": "PoId", filter: false, show: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "Product Number", "field": "PoNo", sortable: "PoNo", filter: { PoNo: "text" }, show: true, },
               { "title": "Reference Number", "field": "PoRefNo", sortable: "PoRefNo", filter: { PoRefNo: "text" }, show: true, },
               {
                   "title": "Product Date", "field": "PoDate", sortable: "PoDate", filter: { PoDate: "date" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<p ng-bind="ConvertDate(row.PoDate,\'dd/mm/yyyy\')">'
                       return $scope.getHtml(element);
                   }
               },
               { "title": "Remark", "field": "Remark", sortable: "Remark", filter: { Remark: "text" }, show: false, },
               { "title": "TermsCondition", "field": "TermsCondition", sortable: "TermsCondition", filter: { TermsCondition: "text" }, show: false, },
               { "title": "Mode Of Shipment", "field": "ModeOfShipment", sortable: "ModeOfShipment", filter: { ModeOfShipment: "text" }, show: false, },
               { "title": "Landing Port", "field": "LandingPortName", sortable: "LandingPortName", filter: { LandingPortName: "text" }, show: false, },
               { "title": "Discharge Port", "field": "DischargePortName", sortable: "DischargePortName", filter: { DischargePortName: "text" }, show: false, },

               { "title": "Total Amount", "field": "TotalAmount", sortable: "TotalAmount", filter: { TotalAmount: "text" }, show: false, },
               { "title": "Total Tax", "field": "TotalTax", sortable: "TotalTax", filter: { TotalTax: "text" }, show: false, },
               { "title": "Payable Amount", "field": "PayableAmount", sortable: "PayableAmount", filter: { PayableAmount: "text" }, show: false, },
               { "title": "Price Code", "field": "PriceCode", sortable: "PriceCode", filter: { PriceCode: "text" }, show: false, },

               { "title": "Supplier Name", "field": "SupplierName", sortable: "SupplierName", filter: { SupplierName: "text" }, show: true, },
               { "title": "Address", "field": "Address", sortable: "Address", filter: { Address: "text" }, show: false, },
               { "title": "Telephone", "field": "Tel", sortable: "Tel", filter: { Tel: "text" }, show: false, },
               { "title": "Fax", "field": "Fax", sortable: "Fax", filter: { Fax: "text" }, show: false, },
               { "title": "Email", "field": "Email", sortable: "Email", filter: { Email: "text" }, show: false, },
               { "title": "Website", "field": "Website", sortable: "Website", filter: { Website: "text" }, show: false, },
               { "title": "Attn", "field": "Attn", sortable: "Attn", filter: { Attn: "text" }, show: false, },
               { "title": "Attn Mobile", "field": "AttnMobile", sortable: "AttnMobile", filter: { AttnMobile: "text" }, show: false, },
               { "title": "Attn Email", "field": "AttnEmail", sortable: "AttnEmail", filter: { AttnEmail: "text" }, show: false, },
               { "title": "Supplier Tax", "field": "SupplierTax", sortable: "SupplierTax", filter: { SupplierTax: "text" }, show: false, },

               { "title": "Buyer Name", "field": "BuyerName", sortable: "BuyerName", filter: { BuyerName: "text" }, show: true, },
               { "title": "Buyer Address", "field": "BuyerAddress", sortable: "BuyerAddress", filter: { BuyerAddress: "text" }, show: false, },
               { "title": "Buyer Tel", "field": "BuyerTel", sortable: "BuyerTel", filter: { BuyerTel: "text" }, show: false, },
               { "title": "Buyer Fax", "field": "BuyerFax", sortable: "BuyerFax", filter: { BuyerFax: "text" }, show: false, },
               { "title": "Buyer Website", "field": "BuyerWebsite", sortable: "BuyerWebsite", filter: { BuyerWebsite: "text" }, show: false, },
               { "title": "Buyer Email", "field": "BuyerEmail", sortable: "BuyerEmail", filter: { BuyerEmail: "text" }, show: false, },
               { "title": "Buyer Attn", "field": "BuyerAttn", sortable: "BuyerAttn", filter: { BuyerAttn: "text" }, show: false, },
               { "title": "Buyer Attn Mobile", "field": "BuyerAttnMobile", sortable: "BuyerAttnMobile", filter: { BuyerAttnMobile: "text" }, show: false, },
               { "title": "Buyer Attn Email", "field": "BuyerAttnEmail", sortable: "BuyerAttnEmail", filter: { BuyerAttnEmail: "text" }, show: false, },
               { "title": "Buyer Tax", "field": "BuyerTax", sortable: "BuyerTax", filter: { BuyerTax: "text" }, show: false, },
               { "title": "Created User", "field": "UserName", sortable: "UserName", filter: { UserName: "text" }, show: false, },
               {
                   "title": "Created Date", "field": "CreatedDate", sortable: "CreatedDate", filter: { CreatedDate: "date" }, show: false,
                   'cellTemplte': function ($scope, row) {
                       var element = '<p ng-bind="ConvertDate(row.PoDate,\'dd/mm/yyyy\')">'
                       return $scope.getHtml(element);
                   }
               },
               //{ "title": "Delivery Date", "field": "DeliveryDate", sortable: "DeliveryDate", filter: { DeliveryDate: "date" }, show: false, },
               //{ "title": "Port Name", "field": "portName", sortable: "portName", filter: { portName: "text" }, show: false, },
               //{ "title": "Delivery Name", "field": "DeliveryName", sortable: "DeliveryName", filter: { BuyerEmail: "text" }, show: false, },
               //{ "title": "Delivery Date", "field": "DeliveryDate", sortable: "DeliveryDate", filter: { DeliveryDate: "date" }, show: false, },
               //{ "title": "Port Name", "field": "portName", sortable: "portName", filter: { portName: "text" }, show: false, },
               //{ "title": "License Name", "field": "LicenseName", sortable: "LicenseName", filter: { LicenseName: "text" }, show: false, },
               //{ "title": "BusTerms License Value", "field": "BusTermsLicenseValue", sortable: "BusTermsLicenseValue", filter: { BusTermsLicenseValue: "text" }, show: false, },
               //{ "title": "BusTerms Desc", "field": "BusTermsDesc", sortable: "BusTermsDesc", filter: { BusTermsDesc: "text" }, show: false, },
               //{ "title": "Total NoOf pckg", "field": "TotalNoOfpckg", sortable: "TotalNoOfpckg", filter: { TotalNoOfpckg: "text" }, show: false, },
               //{ "title": "Diameter Of Cartoon", "field": "DiameterOfCartoon", sortable: "DiameterOfCartoon", filter: { DiameterOfCartoon: "text" }, show: false, },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a   class="btn btn-primary btn-xs" data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.PoId)" data-uib-tooltip="Edit"><i class="fa fa-pencil"></i></a>' +
                             //'<a class="btn btn-danger btn-xs"  data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.PoId)" data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                   '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.PoId)" data-uib-tooltip="View"><i class="fa fa-eye"></i></a>'
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'PoId': 'asc' }
        }

        $scope.Edit = function (id) {
            window.location.href = "/Transaction/PurchaseOrder/AddPurchaseOrder/" + id + "/" + 0;
        }
        $scope.View = function (id) {
            window.location.href = "/Transaction/PurchaseOrder/AddPurchaseOrder/" + id + "/" + 1;
        }
        $scope.Delete = function (id) {
            PurchaseOrderService.DeleteById(id).then(function (result) {
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

        //BEGIN PURCHASE ORDER CONTACT DETAIL
        $scope.AddUpdatePurchaseOrderDetail = function (data) {
            debugger;
            var dataerror = true;
            if (data.CategoryData.Display == "" || data.CategoryData.Display == null) {
                toastr.error("Please Select Category.");
                dataerror = false;
                return false;
            }
            else if (data.SubCategoryData.Display == "" || data.SubCategoryData.Display == null) {
                toastr.error("Please Select SubCategory.");
                dataerror = false;
                return false;
            }
            else if (data.ProductData.Display == "" || data.ProductData.Display == null) {
                toastr.error("Please Select Product.");
                dataerror = false;
                return false;
            }
            else if (data.Qty == "" || data.Qty == null) {
                toastr.error("Please Enter Qty.");
                dataerror = false;
                return false;
            }
            else if (data.QtyCodeValueData.Display == "" || data.QtyCodeValueData.Display == null) {
                toastr.error("Please Select Qty Code.");
                dataerror = false;
                return false;
            }
            else if (data.UnitPrice == "" || data.UnitPrice == null) {
                toastr.error("Please Enter UnitPrice.");
                dataerror = false;
                return false;
            }
            else if (data.UnitPriceCodeValueData.Display == "" || data.UnitPriceCodeValueData.Display == null) {
                toastr.error("Please Select Unit Price Code.");
                dataerror = false;
                return false;
            }
            else if (data.SupplierModelData.Display == "" || data.SupplierModelData.Display == null) {
                toastr.error("Please Select Supllier Model No.");
                dataerror = false;
                return false;
            }
                //else if (data.ModelNo == "" || data.ModelNo == null) {
                //    toastr.error("Please Enter ModelNo.");
                //    return false;
                //}
            else if (data.MachinaryPhotoes == "" || data.MachinaryPhotoes == null) {
                toastr.error("Please Select Machinary Photoes.");
                dataerror = false;
                return false;
            }
            else if (data.ProductPhotoes == "" || data.ProductPhotoes == null) {
                toastr.error("Please Select Product Photoes.");
                dataerror = false;
                return false;
            }
            //else if (data.pr == "" || data.ModelNo == null) {
            //    toastr.error("Please Enter ModelNo.");
            //    return false;
            //}
            if (data != undefined && dataerror == true) {
                if ($scope.objPurchaseOrder.PurchaseOrderDetails.length <= 0) {
                    data.PoDetailIndex = 0;
                } else {
                    data.PoDetailIndex = $scope.objPurchaseOrder.PurchaseOrderDetails.length;
                }
                var PurchaseOrderDetail = {
                    PoDetailId: data.PoDetailId,
                    PoId: data.PoId,
                    PoDetailIndex: data.PoDetailIndex,
                    CategoryId: data.CategoryData.Value,
                    Category: data.CategoryData.Display,
                    SubCategoryId: data.SubCategoryData.Value,
                    SubCategory: data.SubCategoryData.Display,
                    ProductId: data.ProductData.Value,
                    Product: data.ProductData.Display,
                    Description: data.Description,
                    QtyCode: data.QtyCodeValueData.Value,
                    QtyCodeData: data.QtyCodeValueData.Display,
                    Qty: data.Qty,
                    PriceCode: data.UnitPriceCodeValueData.Value,
                    PriceCodeData: data.UnitPriceCodeValueData.Display,
                    UnitPrice: data.UnitPrice,
                    Amount: (data.Qty * data.UnitPrice),
                    ModelNo: data.SupplierModelData.Display,
                    ProductPhotoes: data.ProductPhotoes,
                    MachinaryPhotoes: data.MachinaryPhotoes,
                    Status: data.Status,//1 : Insert , 2:Update ,3 :Delete
                    //CategoryData: { Display: data.PoDetailId, Value: data.PoDetailId },
                    //SubCategoryData: { Display: data.PoDetailId, Value: data.PoDetailId },
                    //MainProductData: { Display: data.PoDetailId, Value: data.PoDetailId },
                    //ProductData: { Display: data.PoDetailId, Value: data.PoDetailId },
                    //QtyCodeValueData: { Display: data.PoDetailId, Value: data.PoDetailId },
                    //UnitPriceCodeValueData: { Display: data.PoDetailId, Value: data.PoDetailId }
                }
                if ($scope.EditPurchaseOrderDetailIndex > -1) {
                    if ($scope.objPurchaseOrder.PurchaseOrderDetails[$scope.EditPurchaseOrderDetailIndex].Status == 2) {
                        PurchaseOrderDetail.Status = 2;
                    } else if ($scope.objPurchaseOrder.PurchaseOrderDetails[$scope.EditPurchaseOrderDetailIndex].Status == 1 ||
                               $scope.objPurchaseOrder.PurchaseOrderDetails[$scope.EditPurchaseOrderDetailIndex].Status == undefined) {
                        PurchaseOrderDetail.Status = 1;
                    }
                    $scope.objPurchaseOrder.PurchaseOrderDetails[$scope.EditPurchaseOrderDetailIndex] = PurchaseOrderDetail;
                    $scope.EditPurchaseOrderDetailIndex = -1;
                } else {
                    PurchaseOrderDetail.Status = 1;
                    $scope.objPurchaseOrder.PurchaseOrderDetails.push(PurchaseOrderDetail);
                }
                //$scope.CreateUpdate($scope.objPurchaseOrder);
                $scope.objPurchaseOrderDetail = {
                    PoDetailId: 0,
                    PoId: 0,
                    PoDetailIndex: '',
                    CategoryId: 0,
                    Category: '',
                    SubCategoryId: 0,
                    SubCategory: '',
                    ProductId: 0,
                    Product: '',
                    Description: '',
                    QtyCode: 0,
                    QtyCodeData: '',
                    Qty: '',
                    PriceCode: 0,
                    PriceCodeData: '',
                    UnitPrice: '',
                    Amount: '',
                    //ModelNo: '',
                    ProductPhotoes: '',
                    MachinaryPhotoes: '',
                    SupplierModelData: { Display: '', Value: '' },
                    Status: 0,//1 : Insert , 2:Update ,3 :Delete
                    CategoryData: { Display: '', Value: '' },
                    SubCategoryData: { Display: '', Value: '' },
                    ProductData: { Display: '', Value: '' },
                    QtyCodeValueData: { Display: '', Value: '' },
                    UnitPriceCodeValueData: { Display: '', Value: '' }
                }
            }
        }
        $scope.DeletePurchaseOrderDetail = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objPurchaseOrder.PurchaseOrderDetails[index] = data;
                } else {
                    $scope.objPurchaseOrder.PurchaseOrderDetails.splice(index, 1);
                }
                toastr.success("Product detail Delete", "Success");
            })
        }
        $scope.EditPurchaseOrderDetail = function (data, index) {
            $scope.EditPurchaseOrderDetailIndex = index;
            $scope.tempMachineryImagePath = data.MachinaryPhotoes;
            $scope.tempProductsImagePath = data.ProductPhotoes;
            $scope.objPurchaseOrderDetail = {
                PoDetailId: data.PoDetailId,
                PoId: data.PoId,
                PoDetailIndex: data.PoDetailIndex,
                CategoryId: data.CategoryId,
                Category: data.Category,
                SubCategoryId: data.SubCategoryId,
                SubCategory: data.SubCategory,
                ProductId: data.ProductId,
                Product: data.Product,
                Description: data.Description,
                QtyCode: data.QtyCode,
                QtyCodeData: data.QtyCodeData,
                Qty: data.Qty,
                PriceCode: data.PriceCode,
                PriceCodeData: data.PriceCodeData,
                UnitPrice: data.UnitPrice,
                Amount: data.Amount,
                //ModelNo: data.ModelNo,
                SupplierModelData: { Display: data.ModelNo, Value: data.ModelNoId },
                ProductPhotoes: data.ProductPhotoes,
                MachinaryPhotoes: data.MachinaryPhotoes,
                Status: data.Status,//1 : Insert , 2:Update ,3 :Delete
                CategoryData: { Display: data.Category, Value: data.CategoryId },
                SubCategoryData: { Display: data.SubCategory, Value: data.SubCategoryId },
                ProductData: { Display: data.Product, Value: data.ProductId },
                QtyCodeValueData: { Display: data.QtyCodeData, Value: data.QtyCode },
                UnitPriceCodeValueData: { Display: data.PriceCodeData, Value: data.PriceCode }
            }
            //$scope.AddUpdatePurchaseOrderDetail(data);
        }
        //END PURCHASE ORDER CONTACT DETAIL

        ////BEGIN MANAGE PURCHASE ORDER Parameter
        ////Add Parameter DETAIL
        //$scope.AddPurchaseParameter = function (data) {
        //    $scope.parasubmitt = true;
        //    if (data.SpecificationData.Display != '' && data.SpecificationData.Display != null && data.SpecVal != '' && data.SpecVal != null) {
        //        $scope.parasubmitt = false;
        //        var ParaDetails = {
        //            POSpecId: data.POSpecId,
        //            PoDetailId: data.PoDetailId,
        //            SpecId: data.SpecificationData.Value,
        //            SpecName: data.SpecificationData.Display,
        //            SpecVal: data.SpecVal,
        //            Status: data.Status,
        //        };

        //        if ($scope.EditPurchaseParameterIndex > -1) {
        //            if ($scope.objPurchaseOrder.TechSpecParameterMasters[$scope.EditPurchaseParameterIndex].Status == 2) {
        //                ParaDetails.Status = 2;
        //            } else if ($scope.objPurchaseOrder.TechSpecParameterMasters[$scope.EditPurchaseParameterIndex].Status == 1 ||
        //                       $scope.objPurchaseOrder.TechSpecParameterMasters[$scope.EditPurchaseParameterIndex].Status == undefined) {
        //                ParaDetails.Status = 1;
        //            }
        //            $scope.objPurchaseOrder.TechSpecParameterMasters[$scope.EditPurchaseParameterIndex] = ParaDetails;
        //            $scope.EditPurchaseParameterIndex = -1;
        //        } else {
        //            ParaDetails.Status = 1;
        //            $scope.objPurchaseOrder.TechSpecParameterMasters.push(ParaDetails);
        //        }
        //        $scope.TechSpecParameterMasters = {
        //            POSpecId: 0,
        //            SpecId: 0,
        //            SpecName: '',
        //            SpecVal: '',
        //            PoDetailId: 0,
        //            SpecificationData: { Display: '', Value: '' },
        //        };
        //    }
        //}

        ////EDIT Parameter DETAIL
        //$scope.EditPurchaseParameter = function (data, index) {
        //    $scope.EditPurchaseParameterIndex = index;
        //    $scope.TechSpecParameterMasters = {
        //        POSpecId: data.POSpecId,
        //        PoDetailId: data.PoDetailId,
        //        SpecId: data.SpecId,
        //        SpecName: data.SpecName,
        //        SpecVal: data.SpecVal,
        //        SpecificationData: { Display: data.SpecName, Value: data.SpecId },
        //    }
        //}

        ////DELETE Parameter DETAIL
        //$scope.DeletePurchaseParameter = function (data, index) {
        //    $scope.$apply(function () {
        //        if (data.Status == 2) {
        //            data.Status = 3;
        //            $scope.objPurchaseOrder.TechSpecParameterMasters[index] = data;
        //        } else {
        //            $scope.objPurchaseOrder.TechSpecParameterMasters.splice(index, 1);
        //        }
        //        toastr.success("Technical Specification Delete", "Success");
        //    })
        //}
        ////END MANAGE PURCHASE ORDER Parameter

        $scope.uploadMachineryImgFile = function (file) {
            $scope.objPurchaseOrderDetail.MachinaryPhotoes = '';
            Upload.upload({
                url: "/Handler/FileUpload.ashx",
                method: 'POST',
                file: file,
            }).then(function (result) {
                if (result.status == 200) {
                    if (result.data.length > 0) {
                        $scope.objPurchaseOrderDetail.MachinaryPhotoes = result.data[0].imagePath;
                        $scope.tempMachineryImagePath = result.data[0].imagePath;
                    }
                }
                else {
                    $scope.objPurchaseOrderDetail.MachinaryPhotoes = '';
                }
            });
        }
        $scope.uploadProductsImgFile = function (file) {
            $scope.objPurchaseOrderDetail.ProductPhotoes = '';
            Upload.upload({
                url: "/Handler/FileUpload.ashx",
                method: 'POST',
                file: file,
            }).then(function (result) {
                if (result.status == 200) {
                    if (result.data.length > 0) {
                        $scope.objPurchaseOrderDetail.ProductPhotoes = result.data[0].imagePath;
                        $scope.tempProductsImagePath = result.data[0].imagePath;
                    }
                }
                else {
                    $scope.objPurchaseOrderDetail.ProductPhotoes = '';
                }
            });
        }
        $scope.AddPurchaseOrderTechDetail = function (data) {
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'PurchaseOrderDetail.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    PurchaseOrderCtrl: function () { return $scope; },
                    PurchaseOrderDetailData: function () { return data; },
                    PurchaseOrderService: function () { return PurchaseOrderService; }
                }
            });
            modalInstance.result.then(function () {
                $scope.EditPurchaseParameterIndex = -1;
            }, function () {
                $scope.EditPurchaseParameterIndex = -1;
            })
        }
        $scope.statusFilter = function (val) {
            if (val.Status != 3)
                return true;
        }

        $scope.dateOptions = {
            formatYear: 'dd-MM-yyyy',
            //minDate: new Date(2016, 8, 5),
            maxDate: new Date(),
            startingDay: 1
        };
    }

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, $timeout, PurchaseOrderCtrl, PurchaseOrderDetailData, PurchaseOrderService) {

        $scope.objPurchaseOrder = PurchaseOrderCtrl.objPurchaseOrder;
        $scope.EditPurchaseParameterIndex = PurchaseOrderCtrl.EditPurchaseParameterIndex;
        //$scope.TechSpecParameterMasters = PurchaseOrderCtrl.TechSpecParameterMasters
        $scope.TechSpecParameterMasters = {
            POSpecId: 0,
            SpecId: 0,
            SpecName: '',
            SpecVal: '',
            PoDetailIndex: PurchaseOrderDetailData,
            PoDetailId: 0,
            SpecificationData: { Display: '', Value: '' },
            Status: 0,//1 : Insert , 2:Update ,3 :Delete
        }
        $scope.IndexFilter = function (val) {
            if (val.PoDetailIndex == PurchaseOrderDetailData) {
                return true;
            } else {
                return false;
            }
        }
        $scope.statusFilter = function (val) {
            if (val.Status != 3)
                return true;
        }
        //BEGIN MANAGE PURCHASE ORDER Parameter
        //Add Parameter DETAIL
        $scope.AddPurchaseParameter = function (data) {
            $scope.parasubmitt = true;
            if (data.TechnicalHead.Display != '' && data.TechnicalHead.Display != null && data.SpecificationData.Display != '' && data.SpecificationData.Display != null && data.SpecVal != '' && data.SpecVal != null) {
                $scope.parasubmitt = false;
                var ParaDetails = {
                    POSpecId: data.POSpecId,
                    PoDetailId: data.PoDetailId,
                    PoDetailIndex: PurchaseOrderDetailData,
                    SpecId: data.SpecificationData.Value,
                    SpecName: data.SpecificationData.Display,
                    SpecVal: data.SpecVal,
                    Status: data.Status,
                    SpecHeadId: data.TechnicalHead.Value,
                    SpecHead: data.TechnicalHead.Display
                };
                $scope.TechSpecParameterMasters.PoDetailIndex = PurchaseOrderDetailData;
                if ($scope.EditPurchaseParameterIndex > -1) {
                    if ($scope.objPurchaseOrder.TechSpecParameterMasters[$scope.EditPurchaseParameterIndex].Status == 2) {
                        ParaDetails.Status = 2;
                    } else if ($scope.objPurchaseOrder.TechSpecParameterMasters[$scope.EditPurchaseParameterIndex].Status == 1 ||
                               $scope.objPurchaseOrder.TechSpecParameterMasters[$scope.EditPurchaseParameterIndex].Status == undefined) {
                        ParaDetails.Status = 1;
                    }
                    $scope.objPurchaseOrder.TechSpecParameterMasters[$scope.EditPurchaseParameterIndex] = ParaDetails;
                    $scope.EditPurchaseParameterIndex = -1;
                } else {
                    ParaDetails.Status = 1;
                    $scope.objPurchaseOrder.TechSpecParameterMasters.push(ParaDetails);
                }
                resetform();
            }
        }

        //EDIT Parameter DETAIL
        $scope.EditPurchaseParameter = function (data, index) {
            if (data.PoDetailIndex == PurchaseOrderDetailData) {
                $scope.EditPurchaseParameterIndex = index;
                $scope.TechSpecParameterMasters = {
                    POSpecId: data.POSpecId,
                    PoDetailId: data.PoDetailId,
                    PoDetailIndex: PurchaseOrderDetailData,
                    SpecId: data.SpecId,
                    SpecName: data.SpecName,
                    SpecVal: data.SpecVal,
                    SpecificationData: { Display: data.SpecName, Value: data.SpecId },
                    SpecHeadId: data.SpecHeadId,
                    SpecHead: data.SpecHead,
                    TechnicalHead: { Display: data.SpecHead, Value: data.SpecHeadId }
                }
            }
        }

        //DELETE Parameter DETAIL
        $scope.DeletePurchaseParameter = function (data, index) {
            if (data.PoDetailIndex == PurchaseOrderDetailData) {
                $scope.$apply(function () {
                    if (data.Status == 2) {
                        data.Status = 3;
                        $scope.objPurchaseOrder.TechSpecParameterMasters[index] = data;
                    } else {
                        $scope.objPurchaseOrder.TechSpecParameterMasters.splice(index, 1);
                    }
                    toastr.success("Technical Specification Delete", "Success");
                })
            }
        }
        //END MANAGE PURCHASE ORDER Parameter


        //    $scope.$watch('objPurchaseOrderDetail.CategoryData', function (data) {
        //        if (data.Value != PurchaseOrderDetailData.CategoryId.toString()) {
        //            $scope.objPurchaseOrderDetail.SubCategoryData.Display = '';
        //            $scope.objPurchaseOrderDetail.SubCategoryData.Value = '';
        //            $scope.objPurchaseOrderDetail.ProductData.Display = '';
        //            $scope.objPurchaseOrderDetail.ProductData.Value = '';
        //        }
        //    }, true)

        //    $scope.$watch('objPurchaseOrderDetail.SubCategoryData', function (data) {
        //        if (data.Value != PurchaseOrderDetailData.SubCategoryId.toString()) {
        //            $scope.objPurchaseOrderDetail.ProductData.Display = '';
        //            $scope.objPurchaseOrderDetail.ProductData.Value = '';
        //        }
        //    }, true)

        //    $scope.setData = function () {
        //        $scope.objPurchaseOrderDetail = {
        //            PoDetailId: PurchaseOrderDetailData.PoDetailId,
        //            PoId: PurchaseOrderDetailData.PoId,
        //            CategoryId: PurchaseOrderDetailData.CategoryId,
        //            Category: PurchaseOrderDetailData.Category,
        //            SubCategoryId: PurchaseOrderDetailData.SubCategoryId,
        //            SubCategory: PurchaseOrderDetailData.SubCategory,
        //            MainProductId: PurchaseOrderDetailData.MainProductId,
        //            MainProduct: PurchaseOrderDetailData.MainProduct,
        //            ProductId: PurchaseOrderDetailData.ProductId,
        //            Product: PurchaseOrderDetailData.Product,
        //            Description: PurchaseOrderDetailData.Description,
        //            QtyCode: PurchaseOrderDetailData.QtyCode,
        //            QtyCodeData: PurchaseOrderDetailData.QtyCodeData,
        //            Qty: PurchaseOrderDetailData.Qty,
        //            PriceCode: PurchaseOrderDetailData.PriceCode,
        //            PriceCodeData: PurchaseOrderDetailData.PriceCodeData,
        //            UnitPrice: PurchaseOrderDetailData.UnitPrice,
        //            Amount: PurchaseOrderDetailData.Amount,
        //            ModelNo: PurchaseOrderDetailData.ModelNo,
        //            Status: PurchaseOrderDetailData.Status,//1 : Insert , 2:Update ,3 :Delete
        //            CategoryData: {
        //                Display: PurchaseOrderDetailData.Category,
        //                Value: PurchaseOrderDetailData.CategoryId
        //            },
        //            SubCategoryData: {
        //                Display: PurchaseOrderDetailData.SubCategory,
        //                Value: PurchaseOrderDetailData.SubCategoryId
        //            },
        //            MainProductData: {
        //                Display: PurchaseOrderDetailData.MainProduct,
        //                Value: PurchaseOrderDetailData.MainProductId
        //            },
        //            ProductData: {
        //                Display: PurchaseOrderDetailData.Product,
        //                Value: PurchaseOrderDetailData.ProductId
        //            },
        //            QtyCodeValueData: {
        //                Display: PurchaseOrderDetailData.QtyCodeData,
        //                Value: PurchaseOrderDetailData.QtyCode
        //            },
        //            UnitPriceCodeValueData: {
        //                Display: PurchaseOrderDetailData.PriceCodeData,
        //                Value: PurchaseOrderDetailData.PriceCode
        //            }
        //        }
        //    }
        //    $scope.setData();
        function resetform() {
            $scope.TechSpecParameterMasters = {
                POSpecId: 0,
                SpecId: 0,
                SpecName: '',
                SpecVal: '',
                PoDetailIndex: PurchaseOrderDetailData,
                PoDetailId: 0,
                SpecificationData: { Display: '', Value: '' },
                Status: 0,//1 : Insert , 2:Update ,3 :Delete
                SpecHeadId: '',
                SpecHead: '',
                TechnicalHead: { Display: '', Value: '' }
            }

            if ($scope.FormPurchaseOrderDetailForm)
                $scope.FormPurchaseOrderDetailForm.$setPristine();
            $scope.addMode = true;
            $scope.isFirstFocus = false;
            $scope.storage = {};
            $timeout(function () {
                $scope.isFirstFocus = true;
            });
            $scope.EditPurchaseParameterIndex = -1;
        }
        $scope.close = function () {
            $uibModalInstance.close();
        };


        //    $scope.CreateUpdate = function (data) {

        //        data.CategoryId = data.CategoryData.Value;
        //        data.SubCategoryId = data.SubCategoryData.Value;
        //        data.MainProductId = data.MainProductData.Value;
        //        data.ProductId = data.ProductData.Value;
        //        data.PriceCode = data.UnitPriceCodeValueData.Value;
        //        data.QtyCode = data.QtyCodeValueData.Value;

        //        data.Category = data.CategoryData.Display;
        //        data.SubCategory = data.SubCategoryData.Display;
        //        data.MainProduct = data.MainProductData.Display;
        //        data.Product = data.ProductData.Display;
        //        data.PriceCodeData = data.UnitPriceCodeValueData.Display;
        //        data.QtyCodeData = data.QtyCodeValueData.Display;
        //        data.Amount = data.UnitPrice * data.Qty;
        //        if (PurchaseOrderCtrl.EditPurchaseOrderDetailIndex > -1) {
        //            PurchaseOrderCtrl.objPurchaseOrder.PurchaseOrderDetails[PurchaseOrderCtrl.EditPurchaseOrderDetailIndex] = data;
        //            PurchaseOrderCtrl.EditPurchaseOrderDetailIndex = -1;
        //        } else {
        //            data.Status = 1;
        //            PurchaseOrderCtrl.objPurchaseOrder.PurchaseOrderDetails.push(data);
        //        }
        //        $scope.close();
        //    }
    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', '$timeout', 'PurchaseOrderCtrl', 'PurchaseOrderDetailData', 'PurchaseOrderService']

    angular.module('CRMApp.Controllers').filter("mydate", function () {
        var re = /\/Date\(([0-9]*)\)\//;
        return function (x) {
            var m = x.match(re);
            if (m) return new Date(parseInt(x.substr(6)));
            else return null;
        };
    });

})();