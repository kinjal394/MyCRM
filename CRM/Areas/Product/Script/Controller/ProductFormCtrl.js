(function () {

    "use strict";
    angular.module("CRMApp.Controllers")
    .controller("ProductFormCtrl", [
                "$scope", "$rootScope", "$timeout", "$filter", "ProductService", "$uibModal", "Upload",
                ProductFormCtrl
    ]);

    function ProductFormCtrl($scope, $rootScope, $timeout, $filter, ProductService, $uibModal, Upload) {

        $scope.objProduct = $scope.objProduct || {};
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};
        $scope.isClicked = false;

        $scope.SetcatlogId = 0;
        $scope.objProduct = {
            ProductId: 0,
            ProductName: '',
            ProductFunctionality: '',
            MainProductId: 0,
            MainProductName: "",
            SubCategoryId: 0,
            SubCategoryName: "",
            CategoryId: 0,
            CategoryName: "",
            ProductCode: '',
            HSCode: '',
            FbLink: '',
            GPlusLink: '',
            OurCatalogImg: '',
            SupplierCatalogimg: '',
            Price: '',
            OursModelNo: '',
            ModelNo: '',
            Height: '',
            CBM: '',
            Dimension: '',
            Width: '',
            Length: '',
            GrossWeight: '',
            NetWeight: '',
            Description: '',
            Keywords: [],
            CategoryData: { Display: '', Value: '' },
            SubCategoryData: { Display: '', Value: '' },
            MainProductData: { Display: '', Value: '' },
            ProductData: { Display: '', Value: '' },
            ProductCodeData: { Display: '', Value: '' },
            ProductParameterMasters: [],
            ProductPhotoMasters: [],
            ProductSocialMasters: [],
            ProductSuppDocumentDetail: [],
            ProductVideoMasters: [],
            ProductPackingDetails: [],
            ProductDocumentDetails: [],
            EditProImgId: 0,
            EditProCatalogueId: 0,
            EditProSocialId: 0,
            EditProVideoId: 0,
            Isphotonext: false,
            Iscatlognext: false,
            share: {},
            ContactData: { Display: '', Value: '' },
            mode: '',
            ProductApplicableCharges: [],
            getAppChargeData: [],
            ProductPrices: [],
            TotalDealerPrice: 0,
            SupplierCatalogimg: '',
            OurCatalogImg: ''
        };
        //ProductPrices
        //ProductPriceId, ProductId, CurrencyId, BaseAmount, TotalCharge, TotalAmount, SupplierId
        //ProductApplicableCharges
        //ApplicableId, ProductPriceId, AppChargeId, AppCharge, Percentage, Amount, ApplicableAmount
        $scope.Objcharges = {
            ApplicableId: 0,
            ProductPriceId: 0,
            AppChargeId: '',
            AppCharge: '',
            AppliChargeData: { Display: '', Value: '' },
            Percentage: '',
            Amount: '',
            ApplicableAmount: '',
            Status: 1
        }
        $scope.ObjPrice = {
            ProductPriceId: 0,
            ProductId: 0,
            SupplierId: 0,
            CurrencyId: '',
            CurrencyName: '',
            BaseAmount: '',
            TotalCharge: '',
            TotalAmount: '',
            Status: 1,
        };

        //$scope.ObjCurrencyData = {
        //    SrId: 0,
        //    ProductId: 0,
        //    catelogId: 0,
        //    CurAppChargeId: 0,
        //    CurrencyDataId: 0,
        //    CurrencyDataName: '',
        //    DealerAmt: 0,
        //    TotalTax: 0,
        //    TotalCurAmt: 0,
        //    Status: 0
        //};
        $scope.ProductPhoto = {
            PhotoId: 0,
            ProductId: 0,
            CatalogId: 0,
            Photo: '',
            IsDefault: false,
            share: false,
            ContactPerson: 0,
            ContactPersonType: '',
            Status: 0 //1 : Insert , 2:Update ,3 :Delete
        }
        $scope.ProductCatalogue = {
            CatalogId: 0,
            ProductId: 0,
            CatalogPath: '',
            CatalogPathName: '',
            CatalogMSO: '',
            CatalogMSOName: '',
            CatalogPDF: '',
            CatalogPDFName: '',
            IsDefault: false,
            share: false,
            ContactPerson: 0,
            ContactPersonType: '',
            SupplierId: 0,
            SupplierModelNo: '',
            ProductModelNo: '',
            SupplierName: '',
            SupplierData: { Display: '', Value: '' },
            CountryOriginData: { Display: '', Value: '' },
            Status: 0,//1 : Insert , 2:Update ,3 :Delete
            Description: '',
            NetWeight: '',
            GrossWeight: '',
            CBM: '',
            Dimension: '',
            DealerPrice: '',
            AppliChargeDetail: '',
            CurrencyId: 0,
            CurrencyName: '',
            CurrencyData: { Display: '', Value: '' },
            TaxId: 0,
            TaxName: '',
            TaxData: { Display: '', Value: '' }
        }
        $scope.ProductSocial = {
            AdId: 0,
            ProductId: 0,
            CatalogId: 0,
            AdSourceId: 0,
            SourceData: { Display: '', Value: '' },
            SourceName: '',
            Url: '',
            share: false,
            ContactPerson: 0,
            ContactPersonType: '',
            Status: 0 //1 : Insert , 2:Update ,3 :Delete
        }
        $scope.ProductVideo = {
            VideoId: 0,
            ProductId: 0,
            CatalogId: 0,
            IsDefault: false,
            URL: '',
            share: false,
            ContactPerson: 0,
            ContactPersonType: '',
            Status: 0 //1 : Insert , 2:Update ,3 :Delete
        }
        $scope.ProductPacking = {
            PackingId: 0,
            CatalogId: 0,
            ProductId: 0,
            Description: '',
            NetWeight: '',
            GrossWeight: '',
            Length: '',
            Width: '',
            Height: '',
            CBM: '',
            Dimension: '',
            DealerPrice: '',
            AppliChargeDetail: '',
            CurrencyId: 0,
            CurrencyName: '',
            CurrencyData: { Display: '', Value: '' },
            TaxId: 0,
            TaxName: '',
            TaxData: { Display: '', Value: '' },
            PackingTypeId: 0,
            PackingType: '',
            PackingTypeData: { Display: '', Value: '' },
            PlugShapeId: 0,
            PlugShape: '',
            PlugShapeData: { Display: '', Value: '' },
            PhaseId: 0,
            Phase: '',
            PhaseData: { Display: '', Value: '' },
            VoltageId: 0,
            Voltage: '',
            VoltageData: { Display: '', Value: '' },
            FrequencyId: 0,
            Frequency: '',
            FrequencyData: { Display: '', Value: '' },
            Power: ''
        }
        $scope.objProductDocument = {
            DocID: 0,
            ProductId: 0,
            CatalogId: 0,
            DocumentId: '',
            DocumentName: '',
            DocPath: '',
            DocumentType: '',
            Status: 0,
            DocumentsData: { Display: '', Value: '' }
        };



        $scope.gridObj = {
            columnsInfo: [
               { "title": "ProductId", "data": "ProductId", filter: false, visible: false },
               { "title": "Sr.", "data": "RowNumber", filter: false, sort: false },
                { "title": "Product Name", "data": "ProductName", sort: true, filter: true, datatype: Text },
                { "title": "Product Code no", "data": "ProductCode", sort: true, filter: true, datatype: Text },
               { "title": "Product HS Code", "data": "HSCode", sort: true, filter: true, },

               //{ "title": "Main Product", "data": "MainProductName", sort: true, filter: true, },

               //{ "title": "HSCode", "data": "HSCode", sort: true, filter: true, },
               {
                   "title": "Action", sort: false, filter: false,
                   'render': '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                            //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.ProductId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                            '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
               }
            ],
            Sort: { 'ProductId': 'asc' },
        }
        $scope.EditProductIndex = -1;
        $scope.dateOptions = {
            formatYear: 'yy',
            //minDate: new Date(2016, 5, 22),
            //maxDate: new Date(2017, 5, 22),
            startingDay: 1
        };
        $scope.openTab = function (evt, tabName, data, id) {
            // Declare all variables
            var bln = true;
            var dataerror = false;
            if (id > 0) {
                $scope.tabshow = true;
                $scope.tabpriceshow = true;
            }
            if ($scope.isClicked == false) {
                if (data != undefined) {
                    if (tabName == "Catalogue") {
                        if (data.CategoryData.Display == "" || data.CategoryData.Display == null) {
                            toastr.error("Please Select Category.");
                            $scope.tabshow = false;
                            dataerror = true;
                            return false;
                        } else if (data.SubCategoryData.Display == "" || data.SubCategoryData.Display == null) {
                            toastr.error("Please Select Sub Category.");
                            $scope.tabshow = false;
                            dataerror = true;
                            return false;
                        } else if (data.ProductData.Display == "" || data.ProductData.Display == null) {
                            toastr.error("Please Select Product.");
                            $scope.tabshow = false;
                            dataerror = true;
                            return false;
                        } else if (data.ProductCode == "" || data.ProductCode == null) {
                            toastr.error("Please Enter product code.");
                            $scope.tabshow = false;
                            dataerror = true;
                            return false;
                        } else if (data.Keywords.length < 4 || data.Keywords.length < 4) {
                            toastr.error("Please Enter Minimum 4 keyword.");
                            $scope.tabshow = false;
                            dataerror = true;
                            return false;
                        } else {
                            $scope.tabshow = true;
                        }
                    }
                    if (dataerror == false) {
                        $scope.UpdateProductFrom(data);
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
                $scope.objProduct.EditProImgId = 0;
                $scope.objProduct.EditProCatalogueId = 0;
            }
            //$scope.tabname = "active";
            //evt.currentTarget.className += "active";
        }

        $scope.openSubTab = function (evt, tabName, data, id) {
            // Declare all variables
            var bln = true;

            var dataerror = false;
            if (id > 0) {
                $scope.tabshow = true;
                $scope.tabpriceshow = true;
            }
            if ($scope.isClicked == false) {
                if (data != undefined) {
                    //if (tabName == 'Photos' || tabName == 'VideoInformation' || tabName == 'SocialMediaInformation' || tabName == 'ProductDescription' || tabName == 'Technicalspecification' || tabName == 'PackingDetail' || tabName == 'PriceInformation') {
                    //// Document Validation
                    //}
                    //toastr.error("hope no supplier detail on photos.", { timeOut: 10000 });
                    if (tabName == 'SupplierDoc' || tabName == 'Photos' || tabName == 'ProductDescription' || tabName == 'PowerDescription' || tabName == 'Technicalspecification' || tabName == 'PackingDetail' || tabName == 'PriceInformation') {
                        if ($scope.objProduct.SupplierCatalogimg == null && $scope.objProduct.OurCatalogImg == null) {
                            //toastr.error("All Photo Upload is Required.");
                            toastr.error("hope no supplier detail on photos", { timeOut: 10000 });
                            dataerror = true;
                            $scope.tabshow = false;
                            return false;
                        }
                    }

                    if (tabName == 'VideoInformation' || tabName == 'ProductDescription' || tabName == 'PowerDescription' || tabName == 'Technicalspecification' || tabName == 'PackingDetail' || tabName == 'PriceInformation') {
                        var cnt = 0;
                        angular.forEach($scope.objProduct.ProductPhotoMasters, function (value, index) {
                            if (value.IsDefault == true) {
                                cnt++;
                            }
                        }, true);
                        if ($scope.objProduct.ProductPhotoMasters.length < 6) {
                            toastr.error("All Photo Upload is Required.");
                            dataerror = true;
                            $scope.tabshow = false;
                            return false;
                        }
                    }

                    if (tabName == "PriceInformation") {
                        if (data.NetWeight == "" || data.NetWeight == null) {
                            toastr.error("Please Enter NetWeight.");
                            $scope.tabpriceshow = false;
                            dataerror = true;
                            return false;
                        }
                        else if (data.GrossWeight == "" || data.GrossWeight == null) {
                            toastr.error("Please Enter GrossWeight.");
                            dataerror = true;
                            return false;
                        }
                        else if (data.Length == "" || data.Length == null) {
                            toastr.error("Please Enter Length.");
                            $scope.tabpriceshow = false;
                            dataerror = true;
                            return false;
                        }
                        else if (data.Width == "" || data.Width == null) {
                            toastr.error("Please Enter Width.");
                            $scope.tabpriceshow = false;
                            dataerror = true;
                            return false;
                        }
                        else if (data.Height == "" || data.Height == null) {
                            toastr.error("Please Enter Height.");
                            $scope.tabpriceshow = false;
                            dataerror = true;
                            return false;
                        }
                        else if (data.CBM == "" || data.CBM == null) {
                            toastr.error("Please Enter CBM.");
                            $scope.tabpriceshow = false;
                            dataerror = true;
                            return false;
                        }
                            //else if (data.Dimension == "" || data.Dimension == null) {
                            //    toastr.error("Please Enter Dimension.");
                            //    $scope.tabpriceshow = false;
                            //    dataerror = true;
                            //    return false;
                            //}
                        else {
                            $scope.tabpriceshow = true;
                        }
                    }

                    if (tabName != 'Photos' && tabName != 'MainInformation') {
                        $scope.objProduct.Isphotonext = false;
                        $scope.objProduct.Iscatlognext = false;
                    }
                    if (tabName == 'Technicalspecification' || tabName == 'PowerDescription' || tabName == "PriceInformation" || tabName == 'MainInformation') {
                        var ProductPacking = {
                            PackingId: 0,
                            CatalogId: $scope.SetcatlogId,
                            ProductId: data.ProductData.Value,
                            Description: data.Description,
                            NetWeight: data.NetWeight,
                            GrossWeight: data.GrossWeight,
                            Length: data.Length,
                            Width: data.Width,
                            Height: data.Height,
                            CBM: data.CBM,
                            Dimension: data.Dimension,
                            DealerPrice: data.DealerPrice,
                            AppliChargeDetail: data.AppliChargeDetail,
                            CurrencyId: (data.CurrencyData != undefined) ? data.CurrencyData.Value : undefined,
                            CurrencyName: (data.CurrencyData != undefined) ? data.CurrencyData.Display : undefined,
                            TaxId: (data.TaxData != undefined) ? data.TaxData.Value : undefined,
                            TaxName: (data.TaxData != undefined) ? data.TaxData.Display : undefined,
                            PackingTypeId: (data.PackingTypeData != undefined) ? data.PackingTypeData.Value : undefined,
                            PackingType: (data.PackingTypeData != undefined) ? data.PackingTypeData.Display : undefined,
                            PlugShapeId: (data.PlugShapeData != undefined) ? data.PlugShapeData.Value : undefined,
                            PlugShape: (data.PlugShapeData != undefined) ? data.PlugShapeData.Display : undefined,
                            PhaseId: (data.PhaseData != undefined) ? data.PhaseData.Value : undefined,
                            Phase: (data.PhaseData != undefined) ? data.PhaseData.Display : undefined,
                            VoltageId: (data.VoltageData != undefined) ? data.VoltageData.Value : undefined,
                            Voltage: (data.VoltageData != undefined) ? data.VoltageData.Display : undefined,
                            FrequencyId: (data.FrequencyData != undefined) ? data.FrequencyData.Value : undefined,
                            Frequency: (data.FrequencyData != undefined) ? data.FrequencyData.Display : undefined,
                            Power: data.Power
                        }
                        $scope.objProduct.ProductPackingDetails.push(ProductPacking);
                    }
                    if (dataerror == false && tabName != 'ProductDescription') {
                        $scope.UpdateProductFrom(data);
                        $scope.BindDataByProductCatelogID(data.ProductId, $scope.SetcatlogId);
                    }
                }


                if (tabName == 'MainInformation') {
                    tabName = 'Close';
                }
            }

            if (tabName == 'Close') {
                bln = false;
                $scope.setsubpopup = 0;
                $scope.SetcatlogId = 0;
                SubResetForm();
                // Catelog Clear
                $scope.ProductCatalogue = {
                    CatalogId: 0,
                    ProductId: 0,
                    CatalogPath: '',
                    PhotoPath: '',
                    CatalogPathName: '',
                    CatalogMSO: '',
                    CatalogMSOName: '',
                    CatalogPDF: '',
                    CatalogPDFName: '',
                    share: false,
                    ContactPerson: 0,
                    ContactPersonType: '',
                    SupplierId: 0,
                    SupplierName: '',
                    SupplierModelNo: '',
                    ProductModelNo: '',
                    SupplierData: { Display: '', Value: '' },
                    CountryOriginData: { Display: '', Value: '' },
                    Status: 0, //1 : Insert , 2:Update ,3 :Delete
                    Capacity: ''
                }
                $scope.objProduct.ProductDocumentDetails = [];
                $scope.tempCataloguePhotoPath = "";
                $scope.tempCataloguePath = "";
                $scope.tempCatalogueMSO = "";
                $scope.tempCataloguePDF = "";
                $scope.objProduct.EditProCatalogueId = 0;
            }

            if (bln == true) {
                var i, tabcontent, tablinks;

                // Get all elements with class="tabcontent" and hide them
                tabcontent = document.getElementsByClassName("subtabcontent");
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
            //$scope.tabname = "active";
            //evt.currentTarget.className += "active";
        }

        $scope.Add = function (id) {
            window.location.href = "/Product/ProductForm/AddProductForm"
        }

        $scope.Edit = function (data) {
            window.location.href = "/Product/ProductForm/AddProductForm/" + data.ProductId + "/" + 0;
        }
        $scope.View = function (data) {
            window.location.href = "/Product/ProductForm/AddProductForm/" + data.ProductId + "/" + 1;
        }
        $scope.Delete = function (id) {
            ProductService.DeleteProduct(id).then(function (result) {
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
        $scope.printKeyword = function (Keyword) {
            var str = '';
            angular.forEach(Keyword, function (value) {
                var obj = {
                    KeyVal: value.text
                }
                str += value.text + ',';
                // $scope.Keyworddetails.push(obj);
            })
            window.location.href = "/Product/ProductForm/PrintKeyword?Key=" + str;
        }

        $scope.SetProductId = function (Id, isdisable) {
            $scope.openTab("click", "Catalogue", undefined, Id);
            $scope.setsubpopup = 0;
            //$scope.GetTechnicalSpecMaster()
            if (Id > 0) {
                $scope.ProductId = Id;
                $scope.addMode = false;
                $scope.saveText = "Update";
                $scope.objProduct.EditProImgId = 0;
                $scope.objProduct.EditProCatalogueId = 0;
                $scope.objProduct.EditProSocialId = 0;
                $scope.objProduct.EditProVideoId = 0;
                $scope.BindDataByProductID(Id)
                if (isdisable == 1) {
                    $scope.isClicked = true;
                }
            } else {
                $scope.ProductId = 0;
                $scope.addMode = true;
                $scope.saveText = "Save";
                $scope.objProduct.EditProImgId = 0;
                $scope.objProduct.EditProCatalogueId = 0;
                $scope.objProduct.EditProSocialId = 0;
                $scope.objProduct.EditProVideoId = 0;
                $scope.isClicked = false;
            }
        }

        function ResetForm() {
            $scope.objProduct = {
                ProductId: 0,
                ProductName: '',
                ProductFunctionality: '',
                MainProductId: 0,
                MainProductName: "",
                SubCategoryId: 0,
                SubCategoryName: "",
                CategoryId: 0,
                CategoryName: "",
                ProductCode: '',
                HSCode: '',
                FbLink: '',
                GPlusLink: '',
                SupplierCatalogimg: '',
                OurCatalogImg: '',
                ModelNo: '',
                Height: '',
                Width: '',
                Length: '',
                CBM: '',
                Dimension: '',
                GrossWeight: '',
                NetWeight: '',
                Price: '',
                OursModelNo: '',
                Description: '',
                Keywords: [],
                CategoryData: { Display: '', Value: '' },
                SubCategoryData: { Display: '', Value: '' },
                MainProductData: { Display: '', Value: '' },
                ProductData: { Display: '', Value: '' },
                ProductCodeData: { Display: '', Value: '' },
                ProductParameterMasters: [],
                ProductPhotoMasters: [],
                ProductCatalogueMasters: [],
                ProductSocialMasters: [],
                ProductSuppDocumentDetail: [],
                ProductVideoMasters: [],
                ProductPackingDetails: [],
                ProductDocumentDetails: [],
                ProductApplicableCharges: [],
                getAppChargeData: [],
                ProductPrices: [],
                Isphotonext: false,
                Iscatlognext: false,
                share: {},
                ContactData: { Display: '', Value: '' },
                mode: '',
            };
            $scope.ProductParameter = {
                TechDetailId: 0,
                ProductId: 0,
                CatalogId: 0,
                TechParaId: 0,
                TechSpec: "",
                Value: "",
                TechnicalHead: { Display: '', Value: '' },
                QueData: { Display: '', Value: '' },
                Status: 0 //1 : Insert , 2:Update ,3 :Delete
            };
            $scope.ProductPhoto = {
                PhotoId: 0,
                ProductId: 0,
                CatalogId: 0,
                Photo: '',
                IsDefault: false,
                share: false,
                ContactPerson: 0,
                ContactPersonType: '',
                Status: 0 //1 : Insert , 2:Update ,3 :Delete
            }
            $scope.ProductCatalogue = {
                CatalogId: 0,
                ProductId: 0,
                CatalogPath: '',
                CatalogPathName: '',
                CatalogMSO: '',
                CatalogMSOName: '',
                CatalogPDF: '',
                CatalogPDFName: '',
                IsDefault: false,
                share: false,
                ContactPerson: 0,
                ContactPersonType: '',
                SupplierId: 0,
                SupplierModelNo: '',
                ProductModelNo: '',
                SupplierName: '',
                SupplierData: { Display: '', Value: '' },
                CountryOriginData: { Display: '', Value: '' },
                Status: 0,//1 : Insert , 2:Update ,3 :Delete
                Description: '',
                NetWeight: '',
                GrossWeight: '',
                CBM: '',
                Dimension: '',
                DealerPrice: '',
                AppliChargeDetail: '',
                CurrencyId: 0,
                CurrencyName: '',
                CurrencyData: { Display: '', Value: '' },
                TaxId: 0,
                TaxName: '',
                TaxData: { Display: '', Value: '' },
                PackingTypeId: 0,
                PackingType: '',
                PackingTypeData: { Display: '', Value: '' }
            }
            $scope.ProductSocial = {
                AdId: 0,
                ProductId: 0,
                CatalogId: 0,
                AdSourceId: 0,
                SourceData: { Display: '', Value: '' },
                SourceName: '',
                Url: '',
                share: false,
                ContactPerson: 0,
                ContactPersonType: '',
                Status: 0 //1 : Insert , 2:Update ,3 :Delete
            }
            $scope.ProductVideo = {
                VideoId: 0,
                ProductId: 0,
                CatalogId: 0,
                IsDefault: false,
                URL: '',
                share: false,
                ContactPerson: 0,
                ContactPersonType: '',
                Status: 0 //1 : Insert , 2:Update ,3 :Delete
            }
            $scope.ProductPacking = {
                PackingId: 0,
                CatalogId: 0,
                ProductId: 0,
                Description: '',
                NetWeight: '',
                GrossWeight: '',
                Length: '',
                Width: '',
                Height: '',
                CBM: '',
                Dimension: '',
                DealerPrice: '',
                AppliChargeDetail: '',
                CurrencyId: 0,
                CurrencyName: '',
                CurrencyData: { Display: '', Value: '' },
                TaxId: 0,
                TaxName: '',
                TaxData: { Display: '', Value: '' },
                PackingTypeId: 0,
                PackingType: '',
                PackingTypeData: { Display: '', Value: '' },
                PlugShapeId: 0,
                PlugShape: '',
                PlugShapeData: { Display: '', Value: '' },
                PhaseId: 0,
                Phase: '',
                PhaseData: { Display: '', Value: '' },
                VoltageId: 0,
                Voltage: '',
                VoltageData: { Display: '', Value: '' },
                FrequencyId: 0,
                Frequency: '',
                FrequencyData: { Display: '', Value: '' },
                Power: ''
            }
            $scope.objProductDocument = {
                PrdSupDocId: 0,
                ProductId: 0,
                CatalogId: 0,
                PrdDocId: '',
                DocumentName: '',
                DocPath: '',
                DocumentType: '',
                Status: 0,
                Remark: '',
                Date: '',
                DocumentsData: { Display: '', Value: '' }
            };
            if ($scope.FormProductFromInfo)
                $scope.FormProductFromInfo.$setPristine();
            $scope.addMode = true;
            $scope.isFirstFocus = false;
            $scope.storage = {};
            $timeout(function () {
                $scope.isFirstFocus = true;
            });
            $scope.EditProductPhotoIndex = -1;
            $scope.EditProductCatalogueIndex = -1;
            $scope.EditProductSocialIndex = -1;
            $scope.EditProductVideoIndex = -1;
            $scope.EditProductParameterIndex = -1;
            $scope.EditProductDocumentIndex = -1;
            $scope.EditProductApplichargesIndex = -1
        }

        ResetForm();

        function SubResetForm() {
            $scope.ProductParameter = {
                TechDetailId: 0,
                ProductId: 0,
                CatalogId: 0,
                TechParaId: 0,
                TechSpec: "",
                Value: "",
                TechnicalHead: { Display: '', Value: '' },
                QueData: { Display: '', Value: '' },
                Status: 0 //1 : Insert , 2:Update ,3 :Delete
            };
            $scope.ProductPhoto = {
                PhotoId: 0,
                ProductId: 0,
                CatalogId: 0,
                Photo: '',
                IsDefault: false,
                share: false,
                ContactPerson: 0,
                ContactPersonType: '',
                Status: 0 //1 : Insert , 2:Update ,3 :Delete
            }
            $scope.ProductSocial = {
                AdId: 0,
                ProductId: 0,
                CatalogId: 0,
                AdSourceId: 0,
                SourceData: { Display: '', Value: '' },
                SourceName: '',
                Url: '',
                share: false,
                ContactPerson: 0,
                ContactPersonType: '',
                Status: 0 //1 : Insert , 2:Update ,3 :Delete
            }
            $scope.ProductVideo = {
                VideoId: 0,
                ProductId: 0,
                CatalogId: 0,
                IsDefault: false,
                URL: '',
                share: false,
                ContactPerson: 0,
                ContactPersonType: '',
                Status: 0 //1 : Insert , 2:Update ,3 :Delete
            }
            $scope.ProductPacking = {
                PackingId: 0,
                CatalogId: 0,
                ProductId: 0,
                Description: '',
                NetWeight: '',
                GrossWeight: '',
                Length: '',
                Width: '',
                Height: '',
                CBM: '',
                Dimension: '',
                DealerPrice: '',
                AppliChargeDetail: '',
                CurrencyId: 0,
                CurrencyName: '',
                CurrencyData: { Display: '', Value: '' },
                TaxId: 0,
                TaxName: '',
                TaxData: { Display: '', Value: '' },
                PackingTypeId: 0,
                PackingType: '',
                PackingTypeData: { Display: '', Value: '' },
                PlugShapeId: 0,
                PlugShape: '',
                PlugShapeData: { Display: '', Value: '' },
                PhaseId: 0,
                Phase: '',
                PhaseData: { Display: '', Value: '' },
                VoltageId: 0,
                Voltage: '',
                VoltageData: { Display: '', Value: '' },
                FrequencyId: 0,
                Frequency: '',
                FrequencyData: { Display: '', Value: '' },
                Power: ''
            }
            $scope.objProductDocument = {
                PrdSupDocId: 0,
                ProductId: 0,
                CatalogId: 0,
                PrdDocId: '',
                DocumentName: '',
                DocPath: '',
                DocumentType: '',
                Status: 0,
                Date: '',
                Remark: '',
                DocumentsData: { Display: '', Value: '' }
            };
            $scope.EditProductPhotoIndex = -1;
            $scope.EditProductSocialIndex = -1;
            $scope.EditProductVideoIndex = -1;
            $scope.EditProductParameterIndex = -1;
        }

        $scope.setCBM = function () {
            var length = isNaN($scope.objProduct.Length) ? 0 : $scope.objProduct.Length;
            var width = isNaN($scope.objProduct.Width) ? 0 : $scope.objProduct.Width;
            var height = isNaN($scope.objProduct.Height) ? 0 : $scope.objProduct.Height;
            $scope.objProduct.CBM = (length * width * height);
        }

        $scope.Reset = function () {
            if ($scope.objProduct.ProductId > 0) {
                $scope.objProduct = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };

        $scope.BindDataByProductID = function (Id) {
            ProductService.GetAllProductFormInfoById(Id).then(function (result) {
                var tempProduct = result.data.DataList.objProductFormModel;
                if (tempProduct) {
                    var ArrKeywords = [];
                    if (tempProduct.Keywords) {
                        _.forEach(tempProduct.Keywords.split(","), function (val) { ArrKeywords.push({ 'text': val }) });
                    }

                    // Product
                    $scope.objProduct = {
                        ProductId: tempProduct.ProductId,
                        ProductName: tempProduct.ProductName,
                        ProductFunctionality: tempProduct.Functionality,
                        MainProductId: tempProduct.MainProductId,
                        MainProductName: tempProduct.MainProductName,
                        SubCategoryId: tempProduct.SubCategoryId,
                        SubCategoryName: tempProduct.SubCategoryName,
                        CategoryId: tempProduct.CategoryId,
                        CategoryName: tempProduct.CategoryName,
                        ProductCode: tempProduct.ProductCode,
                        HSCode: tempProduct.HSCode,
                        FbLink: tempProduct.FbLink,
                        GPlusLink: tempProduct.GPlusLink,
                        OurCatalogImg: tempProduct.OurCatalogImg,
                        SupplierCatalogimg: tempProduct.SupplierCatalogimg,
                        ModelNo: tempProduct.ModelNo,
                        Height: tempProduct.Height,
                        Width: tempProduct.Width,
                        Length: tempProduct.Length,
                        //CBM: tempProduct.CBM,
                        //Dimension: tempProduct.Dimension,
                        //GrossWeight: tempProduct.GrossWeight,
                        //NetWeight: tempProduct.NetWeight,
                        //Price: tempProduct.Price,
                        //OursModelNo: tempProduct.OursModelNo,
                        //Description: tempProduct.Description,
                        Keywords: ArrKeywords,
                        CategoryData: { Display: tempProduct.CategoryName, Value: tempProduct.CategoryId },
                        SubCategoryData: { Display: tempProduct.SubCategoryName, Value: tempProduct.SubCategoryId },
                        MainProductData: { Display: tempProduct.MainProductName, Value: tempProduct.MainProductId },
                        ProductData: { Display: tempProduct.ProductName, Value: tempProduct.ProductId },
                    };

                    $scope.$watch('objProduct.CategoryData', function (data) {
                        if (data.Value != $scope.objProduct.CategoryId.toString()) {
                            $scope.objProduct.SubCategoryData.Display = '';
                            $scope.objProduct.SubCategoryData.Value = '';
                            $scope.objProduct.ProductData.Display = '';
                            $scope.objProduct.ProductData.Value = '';
                        }
                    }, true)

                    $scope.$watch('objProduct.SubCategoryData', function (data) {
                        if (data.Value != $scope.objProduct.SubCategoryId.toString()) {
                            $scope.objProduct.ProductData.Display = '';
                            $scope.objProduct.ProductData.Value = '';
                        }
                    }, true)

                    // Product Catalog
                    $scope.objProduct.ProductCatalogueMasters = [];


                    if (result.data.DataList.objProductCatalogMaster.length > 0) {
                        angular.forEach(result.data.DataList.objProductCatalogMaster, function (value) {
                            var ProductCatalogue = {
                                CatalogId: value.CatalogId,
                                ProductId: value.ProductId,
                                CatalogPathName: value.CatalogPath,
                                CatalogPath: value.CatalogPath,
                                PhotoPath: "/UploadImages/ProductCatalogue/" + value.PhotoPath,
                                CatalogMSOName: value.QuotationMFO,
                                CatalogMSO: "/UploadImages/ProductCatalogue/" + value.QuotationMFO,
                                CatalogPDFName: value.QuotationPDF,
                                CatalogPDF: "/UploadImages/ProductCatalogue/" + value.QuotationPDF,
                                IsDefault: value.IsDefault,
                                SupplierId: value.SupplierId,
                                SupplierModelNo: value.SupplierModelNo,
                                ProductModelNo: value.ProductModelNo,
                                SupplierName: value.SupplierName,
                                SupplierData: { Display: value.SupplierName, Value: value.SupplierId },
                                CountryOriginData: { Display: value.CountryOfOriginName, Value: value.CountryOfOriginId },
                                Status: 2,//1 : Insert , 2:Update ,3 :Delete
                                NoofDoc: value.NoofDoc,
                                Capacity: value.Capacity
                            }
                            $scope.objProduct.ProductCatalogueMasters.push(ProductCatalogue);

                        }, true);
                    }

                    $scope.objProduct.EditProImgId = 0;
                    $scope.objProduct.EditProCatalogueId = 0;
                    $scope.storage = angular.copy($scope.objProduct);
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.BindDataByProductCatelogID = function (Id, catelogId) {
            ProductService.GetAllProductSupplierInfoById(Id, catelogId).then(function (result) {

                ////// Product
                //$scope.objProduct = {
                //    ProductId: tempProduct.ProductId,
                //    ProductName: tempProduct.ProductName,
                //    MainProductId: tempProduct.MainProductId,
                //    MainProductName: tempProduct.MainProductName,
                //    SubCategoryId: tempProduct.SubCategoryId,
                //    SubCategoryName: tempProduct.SubCategoryName,
                //    CategoryId: tempProduct.CategoryId,
                //    CategoryName: tempProduct.CategoryName,
                //    ProductCode: tempProduct.ProductCode,
                //    HSCode: tempProduct.HSCode,
                //    FbLink: tempProduct.FbLink,
                //    GPlusLink: tempProduct.GPlusLink,
                //    ModelNo: tempProduct.ModelNo,
                //    Height: tempProduct.Height,
                //    Width: tempProduct.Width,
                //    Length: tempProduct.Length,
                //    CBM: tempProduct.CBM,
                //    Dimension: tempProduct.Dimension,
                //    GrossWeight: tempProduct.GrossWeight,
                //    NetWeight: tempProduct.NetWeight,
                //    Price: tempProduct.Price,
                //    OursModelNo: tempProduct.OursModelNo,
                //    Description: tempProduct.Description,
                //    Keywords: ArrKeywords,
                //    CategoryData: { Display: tempProduct.CategoryName, Value: tempProduct.CategoryId },
                //    SubCategoryData: { Display: tempProduct.SubCategoryName, Value: tempProduct.SubCategoryId },
                //    MainProductData: { Display: tempProduct.MainProductName, Value: tempProduct.MainProductId },
                //    ProductData: { Display: tempProduct.ProductName, Value: tempProduct.ProductId },
                //};

                //$scope.$watch('objProduct.CategoryData', function (data) {
                //    if (data.Value != $scope.objProduct.CategoryId.toString()) {
                //        $scope.objProduct.SubCategoryData.Display = '';
                //        $scope.objProduct.SubCategoryData.Value = '';
                //        $scope.objProduct.ProductData.Display = '';
                //        $scope.objProduct.ProductData.Value = '';
                //    }
                //}, true)

                //$scope.$watch('objProduct.SubCategoryData', function (data) {
                //    if (data.Value != $scope.objProduct.SubCategoryId.toString()) {
                //        $scope.objProduct.ProductData.Display = '';
                //        $scope.objProduct.ProductData.Value = '';
                //    }
                //}, true)

                //// Product Catalog
                //$scope.objProduct.ProductCatalogueMasters = [];
                $scope.objProduct.ProductDocumentDetails = [];
                $scope.objProduct.ProductApplicableCharges = [];
                //Priceappli
                //if ($scope.objProduct.AppliChargeDetail != null && $scope.objProduct.AppliChargeDetail != '') {
                //    var pricelist = $scope.objProduct.AppliChargeDetail.split('|');
                //    angular.forEach(pricelist, function (value) {
                //        var AppChargeId = value.split('^')[0];
                //        var AppChargeName = value.split('^')[1];
                //        var OffPrice = value.split('^')[2];
                //        var Percentage = value.split('^')[3];
                //        var Price = value.split('^')[4];
                //        var CurrencyId = value.split('^')[5];
                //        var CurrencyName = value.split('^')[6];
                //        var objprice = {
                //            catelogId: $scope.SetcatlogId,
                //            ProductId: $scope.objProduct.ProductId,
                //            CurrencyId: CurrencyId,
                //            CurrencyName: CurrencyName,
                //            ApplicableChargeId: AppChargeId,
                //            ApplicableChargeName: AppChargeName,
                //            Price: Price,
                //            OfferPrice: OffPrice,
                //            Percentage: Percentage,
                //            Status: 2,
                //            AppliChargeData: { Display: AppChargeName, Value: AppChargeId },
                //            DealerPrice: $scope.objProduct.DealerPrice
                //        }
                //        $scope.objProduct.ProductApplicableCharges.push(objprice);
                //    }, true);
                //}
                //$scope.objProduct.ProductPrices = [];
                //if ($scope.objProduct.CurrencyDetail != null && $scope.objProduct.CurrencyDetail != '') {
                //    var curlist = $scope.objProduct.CurrencyDetail.split('|');
                //    angular.forEach(curlist, function (value) {
                //        var SrId = value.split('^')[0];
                //        var CurAppChargeId = value.split('^')[1];
                //        var CurrencyDataId = value.split('^')[2];
                //        var CurrencyDataName = value.split('^')[3];
                //        var DealerAmt = value.split('^')[4];
                //        var TotalTax = value.split('^')[5];
                //        var TotalCurAmt = value.split('^')[6];
                //        var objcur = {
                //            catelogId: $scope.SetcatlogId,
                //            ProductId: $scope.objProduct.ProductId,
                //            SrId: SrId,
                //            CurAppChargeId: CurAppChargeId,
                //            CurrencyDataId: CurrencyDataId,
                //            CurrencyDataName: CurrencyDataName,
                //            DealerAmt: DealerAmt,
                //            TotalTax: TotalTax,
                //            TotalCurAmt: TotalCurAmt,
                //            Status: 2
                //        }
                //        $scope.objProduct.ProductPrices.push(objcur);
                //    }, true);
                //    $scope.SetDealerTotalPrice();
                //}

                //product price--kinjal
                if (result.data.DataList.objProductApplicableCharge != null) {
                    var prolist = result.data.DataList.objProductApplicableCharge;
                    angular.forEach(prolist, function (value) {
                        var obj = {
                            ApplicableId: value.ApplicableId,
                            ProductId: value.ProductId,
                            SupplierId: value.SupplierId,
                            ProductPriceId: value.ProductPriceId,
                            AppChargeId: value.AppChargeId,
                            AppCharge: value.AppCharge,
                            Percentage: value.Percentage,
                            Amount: value.Amount,
                            ApplicableAmount: value.ApplicableAmount,
                            Status: 2
                        }
                        $scope.objProduct.ProductApplicableCharges.push(obj);
                    })
                }
                $scope.objProduct.ProductPrices = [];
                if (result.data.DataList.objProductpriceMaster != null) {
                    var proprice = result.data.DataList.objProductpriceMaster;
                    angular.forEach(proprice, function (value) {
                        var obj = {
                            ProductPriceId: value.ProductPriceId,
                            ProductId: value.ProductId,
                            CurrencyId: value.CurrencyId,
                            CurrencyName: value.CurrencyName,
                            BaseAmount: value.BaseAmount,
                            TotalCharge: value.TotalCharge,
                            TotalAmount: value.TotalAmount,
                            SupplierId: value.SupplierId,
                            CurrencyData: { Display: value.CurrencyName, Value: value.CurrencyId },
                            Status: 2

                        }
                        $scope.objProduct.ProductPrices.push(obj)
                    })
                }
                var totaldealPrice = 0;
                angular.forEach($scope.objProduct.ProductPrices, function (value, index) {
                    value.TotalAmount = (parseFloat(value.TotalCharge) + parseFloat(value.BaseAmount))
                    totaldealPrice += parseFloat(value.TotalAmount);
                });
                $scope.objProduct.TotalDealerPrice = totaldealPrice;
                // Documents
                if (result.data.DataList.objProductCatalogMaster[0] != undefined) {
                    if (result.data.DataList.objProductCatalogMaster[0].CatalogPath != null && result.data.DataList.objProductCatalogMaster[0].CatalogPath != '') {
                        var doclist = result.data.DataList.objProductCatalogMaster[0].CatalogPath.split('|');
                        if ($scope.SetcatlogId != 0) {
                            angular.forEach(doclist, function (value) {
                                if ($scope.SetcatlogId == result.data.DataList.objProductCatalogMaster[0].CatalogId) {
                                    var docId = value.split('^')[0];
                                    var docName = value.split('^')[1];
                                    var objDocumentDetail = {
                                        DocID: 0,
                                        ProductId: result.data.DataList.objProductCatalogMaster[0].ProductId,
                                        CatalogId: result.data.DataList.objProductCatalogMaster[0].CatalogId,
                                        DocumentId: docId,
                                        DocumentName: docName,
                                        DocPath: '/UploadImages/ProductDocumentImage/' + value.toString(),
                                        DocumentType: value.split('.')[1],
                                        Status: 2,
                                        DocumentsData: { Display: docName, Value: docId }
                                    }
                                    $scope.objProduct.ProductDocumentDetails.push(objDocumentDetail);
                                }
                            }, true);
                        }
                    }
                }
                //catalog Tab

                // Product Photos
                $scope.objProduct.ProductPhotoMasters = [];
                $scope.tempImagePath = [];
                if (result.data.DataList.objProductPhotoMaster.length > 0) {
                    angular.forEach(result.data.DataList.objProductPhotoMaster, function (value, index) {
                        var ProductPhoto = {
                            PhotoId: value.PhotoId,
                            ProductId: value.ProductId,
                            CatalogId: value.CatalogId,
                            Photo: "/UploadImages/Product/" + value.Photo,
                            IsDefault: value.IsDefault,
                            Status: 2 //1 : Insert , 2:Update ,3 :Delete
                        }
                        $scope.tempImagePath[index] = "/UploadImages/Product/" + value.Photo;
                        $scope.objProduct.ProductPhotoMasters.push(ProductPhoto);
                    }, true);
                }
                // Product Social
                $scope.objProduct.ProductSocialMasters = [];
                if (result.data.DataList.objProductSocialMaster.length > 0) {
                    angular.forEach(result.data.DataList.objProductSocialMaster, function (value) {
                        var ProductSocial = {
                            AdId: value.AdId,
                            ProductId: value.ProductId,
                            CatalogId: value.CatalogId,
                            AdSourceId: value.AdSourceId,
                            SourceData: { Display: value.SourceName, Value: value.AdSourceId },
                            SourceName: value.SourceName,
                            Url: value.Url,
                            Status: 2 //1 : Insert , 2:Update ,3 :Delete
                        }
                        $scope.objProduct.ProductSocialMasters.push(ProductSocial);
                    }, true);
                }
                //product1 Document
                $scope.objProduct.ProductSuppDocumentDetail = [];
                if (result.data.DataList.objProductSuppDocumentDetail.length > 0) {
                    angular.forEach(result.data.DataList.objProductSuppDocumentDetail, function (value) {
                        var ProductDoc = {
                            PrdSupDocId: value.PrdSupDocId,
                            ProductId: value.ProductId,
                            CatalogId: value.CatalogId,
                            DocPath: "/UploadImages/ProductDocumentImage/" + value.DocPath,
                            Remark: value.Remark,
                            Date: $filter('mydate')(value.Date),
                            PrdDocId: value.PrdDocId,
                            DocumentType: value.DocPath.split('.')[1],
                            DocumentName: { Display: value.prdDocName, Value: value.PrdDocId },
                            DocumentName: value.prdDocName,
                            Status: 2 //1 : Insert , 2:Update ,3 :Delete
                        }
                        $scope.objProduct.ProductSuppDocumentDetail.push(ProductDoc);
                    }, true);
                }
                // Product Video
                $scope.objProduct.ProductVideoMasters = [];
                if (result.data.DataList.objProductVideoMaster.length > 0) {
                    angular.forEach(result.data.DataList.objProductVideoMaster, function (value) {
                        var ProductVideo = {
                            VideoId: value.VideoId,
                            ProductId: value.ProductId,
                            CatalogId: value.CatalogId,
                            IsDefault: value.IsDefault,
                            URL: value.URL,
                            Status: 2 //1 : Insert , 2:Update ,3 :Delete
                        }
                        $scope.objProduct.ProductVideoMasters.push(ProductVideo);
                    }, true);
                }
                // Product Technical Para
                $scope.objProduct.ProductParameterMasters = [];
                if (result.data.DataList.objProductParameterMaster.length > 0) {
                    angular.forEach(result.data.DataList.objProductParameterMaster, function (value) {
                        var ProductPara = {
                            TechDetailId: value.TechDetailId,
                            ProductId: value.ProductId,
                            CatalogId: value.CatalogId,
                            TechParaId: value.TechParaId,
                            TechSpec: value.TechSpec,
                            Value: value.Value,
                            TechHead: value.TechHead,
                            TechHeadId : value.TechHeadId,
                            TechnicalHead: { Display: value.TechHead, Value: value.TechHeadId },
                            QueData: { Display: value.TechSpec, Value: value.TechParaId },
                            Status: 2 //1 : Insert , 2:Update ,3 :Delete
                        }
                        $scope.objProduct.ProductParameterMasters.push(ProductPara);
                    }, true);
                }
                // Product Packing
                $scope.objProduct.ProductPackingDetails = [];
                if (result.data.DataList.objProductPackingMaster.length > 0) {
                    angular.forEach(result.data.DataList.objProductPackingMaster, function (value) {
                        $scope.objProduct.PackingId = value.PackingId;
                        //$scope.objProduct.CatalogId = value.CatalogId;
                        //$scope.objProduct.ProductId = value.ProductId;
                        $scope.objProduct.Description = value.Description;
                        $scope.objProduct.NetWeight = value.NetWeight;
                        $scope.objProduct.GrossWeight = value.GrossWeight;
                        $scope.objProduct.Length = value.Length,
                        $scope.objProduct.Width = value.Width,
                        $scope.objProduct.Height = value.Height,
                        $scope.objProduct.CBM = value.CBM;
                        $scope.objProduct.Dimension = value.Dimension;
                        //$scope.objProduct.DealerPrice = value.DealerPrice;
                        $scope.objProduct.AppliChargeDetail = value.AppliChargeDetail;
                        $scope.objProduct.CurrencyDetail = value.CurrencyDetail;
                        $scope.objProduct.CurrencyId = value.CurrencyId;
                        $scope.objProduct.CurrencyName = value.CurrencyName;
                        //$scope.objProduct.CurrencyData = { Display: value.CurrencyName, Value: value.CurrencyId };
                        $scope.objProduct.TaxId = value.TaxId;
                        $scope.objProduct.TaxName = value.TaxName;
                        $scope.objProduct.TaxData = { Display: value.TaxName, Value: value.TaxId };
                        $scope.objProduct.PackingTypeId = value.PackingTypeId;
                        $scope.objProduct.PackingType = value.PackingType;
                        $scope.objProduct.PackingTypeData = { Display: value.PackingType, Value: value.PackingTypeId };
                        $scope.objProduct.PlugShapeId = value.PlugShapeId;
                        $scope.objProduct.PlugShape = value.PlugShape;
                        $scope.objProduct.PlugShapeData = { Display: value.PlugShape, Value: value.PlugShapeId },
                        $scope.objProduct.PhaseId = value.PhaseId;
                        $scope.objProduct.Phase = value.Phase;
                        $scope.objProduct.PhaseData = { Display: value.Phase, Value: value.PhaseId },
                        $scope.objProduct.VoltageId = value.VoltageId,
                        $scope.objProduct.Voltage = value.Voltage,
                        $scope.objProduct.VoltageData = { Display: value.Voltage, Value: value.VoltageId },
                        $scope.objProduct.FrequencyId = value.FrequencyId,
                        $scope.objProduct.Frequency = value.Frequency,
                        $scope.objProduct.FrequencyData = { Display: value.Frequency, Value: value.FrequencyId },
                        $scope.objProduct.Power = value.Power
                    }, true);
                }
                if (result.data.DataList.productdata.SupplierCatalogimg != null) {
                    $scope.objProduct.SupplierCatalogimg = ''; $scope.tempImagePath[7] = '';
                    $scope.objProduct.SupplierCatalogimg = '/UploadImages/SupplierCatalogimg/' + result.data.DataList.productdata.SupplierCatalogimg;
                    $scope.tempImagePath[7] = '/UploadImages/SupplierCatalogimg/' + result.data.DataList.productdata.SupplierCatalogimg;
                }
                if (result.data.DataList.productdata.SupplierCatalogimg != null) {
                    $scope.objProduct.OurCatalogImg = ''; $scope.tempImagePath[8] = '';
                    $scope.objProduct.OurCatalogImg = '/UploadImages/OurCatalogImage/' + result.data.DataList.productdata.OurCatalogImg;
                    $scope.tempImagePath[8] = '/UploadImages/OurCatalogImage/' + result.data.DataList.productdata.OurCatalogImg;
                }
                $scope.objProduct.EditProImgId = 0;
                $scope.objProduct.EditProCatalogueId = 0;
                $scope.storage = angular.copy($scope.objProduct);

            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        //$scope.$watch('objProduct.ProductData', function (data) {
        //    if (data.Value) {
        //        $scope.BindDataByProductID(data.Value)
        //        //$scope.objProduct.EditProImgId = 0;
        //        //$scope.storage = angular.copy($scope.objProduct);
        //    }
        //}, true)

        //$scope.GetTechnicalSpecMaster = function () {
        //    ProductService.GetTechnicalSpecMaster().then(function (result) {
        //        if (result.data.ResponseType == 1) {
        //            $scope.ListTechnicalSpec = [];
        //            _.forEach(result.data.DataList, function (val) {
        //                $scope.ListTechnicalSpec.push({
        //                    TechDetailId: 0,
        //                    ProductId: 0,
        //                    TechParaId: val.SpecificationId,
        //                    TechSpec: val.TechSpec,
        //                    Value: ""
        //                })
        //            })
        //            $scope.objProduct.ProductParameterMasters = $scope.ListTechnicalSpec;
        //            $scope.ListTempTechnicalSpec = $scope.ListTechnicalSpec;
        //        } else {
        //            toastr.error(result.data.Message);
        //        }
        //    }, function (errorMsg) {
        //        toastr.error(errorMsg, 'Opps, Something went wrong');
        //    })
        //}
        $scope.UpdateProductFrom = function (data, tag) {
            var bln = true;
            if (tag == 'final') {
                //if (data.ProductPhotoMasters.length < 4) {
                //    toastr.error("Enter Minmum 4 Photo Details.");
                //    bln = false;
                //} else
                if (data.ProductCatalogueMasters.length <= 0) {
                    toastr.error("Enter Minmum 1 Product Details.");
                    bln = false;
                }
                $scope.ProductSocial.SourceData.Display = ' ';
            }
            if (bln == true) {
                $scope.objProduct.ProductPhotoMasters = data.ProductPhotoMasters;
                $scope.objProduct.ProductCatalogueMasters = data.ProductCatalogueMasters;
                $scope.objProduct.ProductApplicableCharges = data.ProductApplicableCharges;
                $scope.objProduct.ProductSuppDocumentDetail = data.ProductSuppDocumentDetail;
                if ($scope.objProduct.SupplierCatalogimg != null)
                    $scope.objProduct.SupplierCatalogimg = $scope.objProduct.SupplierCatalogimg.split('/')[$scope.objProduct.SupplierCatalogimg.split('/').length - 1];
                if ($scope.objProduct.OurCatalogImg != null)
                    $scope.objProduct.OurCatalogImg = $scope.objProduct.OurCatalogImg.split('/')[$scope.objProduct.OurCatalogImg.split('/').length - 1];

                //$scope.objProduct.ProductPrices = data.ProductPrices;//Set Data
                angular.forEach(data.ProductPhotoMasters, function (value, index) {
                    if (value.Photo != undefined) {
                        var dataphoto = value.Photo.split('/');
                        data.ProductPhotoMasters[index].Photo = dataphoto[dataphoto.length - 1];
                    }
                    value.CatalogId = $scope.SetcatlogId;
                }, true);
                angular.forEach(data.ProductSuppDocumentDetail, function (value, index) {
                    if (value.DocPath != undefined) {
                        var dataphoto = value.DocPath.split('/');
                        data.ProductSuppDocumentDetail[index].DocPath = dataphoto[dataphoto.length - 1];
                    }
                    value.CatalogId = $scope.SetcatlogId;
                }, true);

                //if (data.ProductApplicableCharges != undefined) {
                //    if (data.ProductApplicableCharges.length > 0) {
                //        var PriProdId = 0, PriCateId = 0;
                //        var pricelist = data.ProductApplicableCharges.map(function (item) {
                //            PriProdId = item['ProductId'];
                //            PriCateId = item['SupplierId'];
                //            item['Percentage'] = (item['Percentage'] != undefined && item['Percentage'] != '') ? item['Percentage'] : 0;
                //            item['Price'] = (item['Price'] != undefined && item['Price'] != '') ? item['Price'] : 0;
                //            //ApplicableChargeId | ApplicableChargeName | OfferPrice | Percentage | Price | CurrencyId | CurrencyName
                //            if (item.Status != 3) {
                //                return item['AppChargeId'] + '^' + item['AppCharge'] + '^' + item['OfferPrice'] + '^' + item['Percentage'] + '^' + item['Price'] + '^' + item['CurrencyId'] + '^' + item['CurrencyName'];
                //            }
                //        });
                //        pricelist = jQuery.grep(pricelist, function (a) { return a != undefined; });
                //        if (data.ProductApplicableCharges.length > 0) {
                //            _.filter(data.ProductApplicableCharges, { ProductId: PriProdId, CatalogId: PriCateId })[0].AppliChargeDetail = pricelist.join("|");
                //        } else {
                //            //_.filter(data.ProductPackingDetails, { ProductId: PriProdId, CatalogId: PriCateId })[0].AppliChargeDetail = "";
                //        }
                //    }

                //}
                //if (data.ProductPrices != undefined) {
                //    if (data.ProductPrices.length > 0) {
                //        var PriProdId1 = 0, PriCateId1 = 0;
                //        var clist = data.ProductPrices.map(function (item) {
                //            PriProdId1 = item['ProductId'];
                //            PriCateId1 = item['CatalogId'];
                //            //SrNo | (Multi - )ApplicableChargeId |  CurrencyId | CurrencyName | DealerAmt | TotalTax | TotalAmount 
                //            if (item.Status != 3) {
                //                return item['SrId'] + '^' + item['CurAppChargeId'] + '^' + item['CurrencyDataId'] + '^' + item['CurrencyDataName'] + '^' + item['DealerAmt'] + '^' + item['TotalTax'] + '^' + item['TotalCurAmt'];
                //            }
                //        });
                //        pricelist = jQuery.grep(clist, function (a) { return a != undefined; });
                //        if (data.ProductPackingDetails.length > 0) {
                //            _.filter(data.ProductPackingDetails, { ProductId: PriProdId1, CatalogId: PriCateId1 })[0].CurrencyDetail = clist.join("|");
                //        } else {
                //            //_.filter(data.ProductPackingDetails, { ProductId: PriProdId, CatalogId: PriCateId })[0].AppliChargeDetail = "";
                //        }
                //    }

                //}

                var objData = {
                    ProductId: data.ProductData.Value,
                    ProductName: data.ProductData.Display,
                    Functionality: data.ProductFunctionality,
                    MainProductId: data.MainProductData.Value,
                    MainProductName: data.MainProductData.Display,
                    SubCategoryId: data.SubCategoryData.Value,
                    SubCategoryName: data.SubCategoryData.Display,
                    CategoryId: data.CategoryData.Value,
                    CategoryName: data.CategoryData.Display,
                    ProductCode: data.ProductCode,
                    HSCode: data.HSCode,
                    FbLink: data.FbLink,
                    GPlusLink: data.GPlusLink,
                    SupplierCatalogimg: data.SupplierCatalogimg,
                    OurCatalogImg: data.OurCatalogImg,
                    Price: data.Price,
                    OursModelNo: data.OursModelNo,
                    ModelNo: data.ModelNo,
                    Length: data.Length,
                    Width: data.Width,
                    Height: data.Height,
                    CBM: data.CBM,
                    Dimension: data.Dimension,
                    GrossWeight: data.GrossWeight,
                    NetWeight: data.NetWeight,
                    Description: data.Description,
                    Keywords: _.map(data.Keywords, 'text').join(','),
                    ProductPhotoMasters: data.ProductPhotoMasters,
                    ProductCatalogMasters: data.ProductCatalogueMasters,
                    ProductSocialMasters: data.ProductSocialMasters,
                    ProductSuppDocumentDetail: data.ProductSuppDocumentDetail,
                    ProductVideoMasters: data.ProductVideoMasters,
                    ProductPackingDetails: data.ProductPackingDetails,
                    Isphotonext: data.Isphotonext,
                    Iscatlognext: data.Iscatlognext,
                    ProductParameterMasters: data.ProductParameterMasters,
                    ProductpriceDetail: data.ProductPrices,
                    ProductApplicableChargeDetail: data.ProductApplicableCharges
                    //ProductParameterMasters: _.remove(data.ProductParameterMasters, function (val) {
                    //    return val.Value;
                    //})
                }

                ProductService.UpdateProductFrom(objData).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        //ResetForm();
                        //angular.forEach(result.data.DataList.objProductPhotoMaster, function (value, index) {
                        //    //if (value.Photo > 19) {
                        //    $scope.objProduct.ProductPhotoMasters = result.data.DataList.objProductPhotoMaster;
                        //    $scope.objProduct.ProductPhotoMasters[index].Status = 2;
                        //    $scope.objProduct.ProductPhotoMasters[index].Photo = "/UploadImages/Product/" + value.Photo;
                        //    $scope.objProduct.ProductPhotoMasters[index].IsDefault = value.IsDefault;
                        //    //}
                        //}, true);
                        $scope.objProduct.ProductDocumentDetails = [];
                        angular.forEach(result.data.DataList.objProductCatalogMaster, function (value, index) {
                            //if (value.CatalogPath.length > 19) {
                            $scope.objProduct.ProductCatalogueMasters = result.data.DataList.objProductCatalogMaster;
                            $scope.objProduct.ProductCatalogueMasters[index].Status = 2;
                            $scope.objProduct.ProductCatalogueMasters[index].SupplierId = value.SupplierId;
                            $scope.objProduct.ProductCatalogueMasters[index].SupplierName = value.SupplierName;
                            $scope.objProduct.ProductCatalogueMasters[index].SupplierData = { Display: value.SupplierName, Value: value.SupplierId };
                            $scope.objProduct.ProductCatalogueMasters[index].CountryOriginData = { Display: value.CountryOfOriginName, Value: value.CountryOfOriginId },
                            $scope.objProduct.ProductCatalogueMasters[index].PhotoPath = "/UploadImages/ProductCatalogue/" + value.PhotoPath;
                            $scope.objProduct.ProductCatalogueMasters[index].CatalogPathName = value.CatalogPath;
                            $scope.objProduct.ProductCatalogueMasters[index].CatalogPath = value.CatalogPath;
                            $scope.objProduct.ProductCatalogueMasters[index].CatalogMSOName = value.QuotationMFO;
                            $scope.objProduct.ProductCatalogueMasters[index].CatalogMSO = "/UploadImages/ProductCatalogue/" + value.QuotationMFO;
                            $scope.objProduct.ProductCatalogueMasters[index].CatalogPDFName = value.QuotationPDF;
                            $scope.objProduct.ProductCatalogueMasters[index].CatalogPDF = "/UploadImages/ProductCatalogue/" + value.QuotationPDF;
                            $scope.objProduct.ProductCatalogueMasters[index].IsDefault = value.IsDefault;
                            $scope.objProduct.ProductCatalogueMasters[index].Capacity = value.Capacity;
                            //}

                            // Documents
                            if ($scope.objProduct.ProductCatalogueMasters[index].CatalogPath != null && $scope.objProduct.ProductCatalogueMasters[index].CatalogPath != '') {
                                var doclist = $scope.objProduct.ProductCatalogueMasters[index].CatalogPath.split('|');
                                if ($scope.SetcatlogId != 0) {
                                    angular.forEach(doclist, function (value) {
                                        if ($scope.SetcatlogId == $scope.objProduct.ProductCatalogueMasters[index].CatalogId) {
                                            var docId = value.split('^')[0];
                                            var docName = value.split('^')[1];
                                            var objDocumentDetail = {
                                                DocID: 0,
                                                ProductId: $scope.objProduct.ProductCatalogueMasters[index].ProductId,
                                                CatalogId: $scope.objProduct.ProductCatalogueMasters[index].CatalogId,
                                                DocumentId: docId,
                                                DocumentName: docName,
                                                DocPath: '/UploadImages/ProductDocumentImage/' + value.toString(),
                                                DocumentType: value.split('.')[1].toString(),
                                                Status: 2,
                                                DocumentsData: { Display: docName, Value: docId }
                                            }
                                            $scope.objProduct.ProductDocumentDetails.push(objDocumentDetail);
                                        }
                                    }, true);
                                }
                            }

                        }, true);
                        if (tag != undefined) {
                            //if (objData.Price != null && objData.Price != '' && objData.OursModelNo != null && objData.OursModelNo != '') {
                            toastr.success(result.data.Message);
                            ResetForm();
                            window.location.href = "/Product/Product";
                            //}
                        }

                        //window.location.href = "/Product/ProductForm";
                    } else {
                        toastr.error(result.data.Message);
                    }
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }

        }

        //BEGIN MANAGE PRODUCT Parameter
        //Add Parameter DETAIL
        $scope.AddProductParameter = function (data) {
            $scope.parasubmitt = true;
            if (data.TechnicalHead.Display != '' && data.TechnicalHead.Display != null && data.QueData.Display != '' && data.QueData.Display != null && data.Value != '' && data.Value != null) {
                $scope.parasubmitt = false;
                var ParaDetails = {
                    TechDetailId: data.TechDetailId,
                    ProductId: data.ProductId,
                    CatalogId: $scope.SetcatlogId,
                    TechParaId: data.QueData.Value,
                    TechSpec: data.QueData.Display,
                    Value: data.Value,
                    TechHeadId: data.TechnicalHead.Value,
                    TechHead: data.TechnicalHead.Display
                };

                if ($scope.EditProductParameterIndex > -1) {
                    if ($scope.objProduct.ProductParameterMasters[$scope.EditProductParameterIndex].Status == 2) {
                        ParaDetails.Status = 2;
                    } else if ($scope.objProduct.ProductParameterMasters[$scope.EditProductParameterIndex].Status == 1 ||
                               $scope.objProduct.ProductParameterMasters[$scope.EditProductParameterIndex].Status == undefined) {
                        ParaDetails.Status = 1;
                    }
                    $scope.objProduct.ProductParameterMasters[$scope.EditProductParameterIndex] = ParaDetails;
                    $scope.EditProductParameterIndex = -1;
                } else {
                    ParaDetails.Status = 1;
                    $scope.objProduct.ProductParameterMasters.push(ParaDetails);
                }
                $scope.ProductParameter = {
                    TechDetailId: 0,
                    ProductId: 0,
                    CatalogId: 0,
                    TechParaId: 0,
                    TechSpec: "",
                    Value: "",
                    TechnicalHead: { Display: '', Value: '' },
                    QueData: { Display: '', Value: '' },
                };
            } else {
                if (data.TechnicalHead.Display == "") {
                    toastr.error("Technical Head is required.");
                } else if (data.QueData.Display == "") {
                    toastr.error("Technical Parameter is required.");
                } else if (data.Value == "") {
                    toastr.error("Technical Specification is required.");
                }
            }
        }

        //EDIT Parameter DETAIL
        $scope.EditProductParameter = function (data, index) {
            $scope.EditProductParameterIndex = index;
            $scope.ProductParameter = {
                TechDetailId: data.TechDetailId,
                ProductId: data.ProductId,
                CatalogId: data.CatalogId,
                TechParaId: data.TechParaId,
                TechSpec: data.TechSpec,
                Value: data.Value,
                TechHeadId: data.TechHeadId,
                TechHead: data.TechHead,
                TechnicalHead: { Display: data.TechHead, Value: data.TechHeadId },
                QueData: { Display: data.TechSpec, Value: data.TechParaId },
            }
        }

        //DELETE Parameter DETAIL
        $scope.DeleteProductParameter = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objProduct.ProductParameterMasters[index] = data;
                } else {
                    $scope.objProduct.ProductParameterMasters.splice(index, 1);
                }
                toastr.success("Technical Specification Delete", "Success");
            })
        }
        //END MANAGE PRODUCT Parameter

        //BEGIN MANAGE PRODUCT Video
        //Add Video DETAIL
        $scope.AddProductVideo = function (data) {
            $scope.vdsubmitt = true;
            $scope.IsexistUrl = false;
            angular.forEach($scope.objProduct.ProductVideoMasters, function (value, index) {
                if (value.URL == data.URL && data.CatalogId == 0) {
                    $scope.IsexistUrl = true;
                }
            });

            if ($scope.IsexistUrl == false) {
                if (data.URL != undefined && data.URL != "") {
                    $scope.vdsubmitt = false;
                    if (data.IsDefault == true) {
                        angular.forEach($scope.objProduct.ProductVideoMasters, function (value, index) {
                            $scope.objProduct.ProductVideoMasters[index].IsDefault = false;
                        }, true);
                    }
                    var VideoDetails = {
                        VideoId: parseInt(data.VideoId),
                        ProductId: parseInt($scope.objProduct.ProductData.Value),
                        CatalogId: parseInt($scope.SetcatlogId),
                        IsDefault: data.IsDefault,
                        URL: data.URL
                    };

                    if ($scope.EditProductVideoIndex > -1) {
                        if ($scope.objProduct.ProductVideoMasters[$scope.EditProductVideoIndex].Status == 2) {
                            VideoDetails.Status = 2;
                        } else if ($scope.objProduct.ProductVideoMasters[$scope.EditProductVideoIndex].Status == 1 ||
                                   $scope.objProduct.ProductVideoMasters[$scope.EditProductVideoIndex].Status == undefined) {
                            VideoDetails.Status = 1;
                        }
                        $scope.objProduct.ProductVideoMasters[$scope.EditProductVideoIndex] = VideoDetails;
                        $scope.EditProductVideoIndex = -1;
                    } else {
                        VideoDetails.Status = 1;
                        $scope.objProduct.ProductVideoMasters.push(VideoDetails);
                    }
                    $scope.UpdateProductFrom($scope.objProduct);
                    $scope.ProductVideo.VideoId = 0;
                    $scope.ProductVideo.ProductId = 0;
                    $scope.ProductVideo.CatalogId = 0;
                    $scope.ProductVideo.IsDefault = false;
                    $scope.ProductVideo.URL = '';
                    $scope.ProductVideo.share = false;
                    $scope.ProductVideo.ContactPerson = 0;
                    $scope.ProductVideo.ContactPersonType = '';
                } else {
                    if (data.URL != undefined) {
                        toastr.error("Video Url is required.");
                    } else {
                        toastr.error("Enter Proper Video Url.");
                    }
                }
            } else {
                toastr.error("Video Url Already Exist.");
            }
        }

        //EDIT Video DETAIL
        $scope.EditProductVideo = function (data, index) {
            $scope.EditProductVideoIndex = index;
            $scope.ProductVideo.VideoId = data.AdId;
            $scope.ProductVideo.ProductId = data.ProductId;
            $scope.ProductVideo.CatalogId = data.CatalogId;
            $scope.ProductVideo.IsDefault = data.IsDefault;
            $scope.ProductVideo.URL = data.URL;
            $scope.ProductVideo.share = false;
            $scope.ProductVideo.ContactPerson = 0;
            $scope.ProductVideo.ContactPersonType = '';
        }

        //DELETE Video DETAIL
        $scope.DeleteProductVideo = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objProduct.ProductVideoMasters[index] = data;
                } else {
                    $scope.objProduct.ProductVideoMasters.splice(index, 1);
                }
                $scope.UpdateProductFrom($scope.objProduct);
                toastr.success("Video Details Delete", "Success");
            })
        }
        //END MANAGE PRODUCT Video

        //BEGIN MANAGE PRODUCT Social
        //Add Social DETAIL
        $scope.AddProductSocial = function (data) {
            $scope.submitt = true;
            if (data.Url != undefined && data.Url != "" && data.SourceData.Display != '') {
                $scope.submitt = false;
                var SocialDetails = {
                    AdId: parseInt(data.AdId),
                    ProductId: parseInt($scope.objProduct.ProductData.Value),
                    CatalogId: parseInt($scope.SetcatlogId),
                    AdSourceId: parseInt(data.SourceData.Value), // By default 1 - Video
                    SourceName: data.SourceData.Display,
                    Url: data.Url
                };

                if ($scope.EditProductSocialIndex > -1) {
                    if ($scope.objProduct.ProductSocialMasters[$scope.EditProductSocialIndex].Status == 2) {
                        SocialDetails.Status = 2;
                    } else if ($scope.objProduct.ProductSocialMasters[$scope.EditProductSocialIndex].Status == 1 ||
                               $scope.objProduct.ProductSocialMasters[$scope.EditProductSocialIndex].Status == undefined) {
                        SocialDetails.Status = 1;
                    }
                    $scope.objProduct.ProductSocialMasters[$scope.EditProductSocialIndex] = SocialDetails;
                    $scope.EditProductSocialIndex = -1;
                } else {
                    SocialDetails.Status = 1;
                    $scope.objProduct.ProductSocialMasters.push(SocialDetails);
                }
                $scope.UpdateProductFrom($scope.objProduct);

                $scope.ProductSocial.AdId = 0;
                $scope.ProductSocial.ProductId = 0;
                $scope.ProductSocial.CatalogId = 0;
                $scope.ProductSocial.AdSourceId = 0;
                $scope.ProductSocial.Url = '';
                $scope.ProductSocial.SourceName = '';
                $scope.ProductSocial.SourceData = { Display: '', Value: '' };
                $scope.ProductSocial.share = false;
                $scope.ProductSocial.ContactPerson = 0;
                $scope.ProductSocial.ContactPersonType = '';
            } else {
                if (data.SourceData.Display == '') {
                    toastr.error("Social Media is required.");
                } else if (data.Url == "") {
                    toastr.error("Site Url is required.");
                } else if (data.Url == undefined) {
                    toastr.error("Enter Proper Site Url.");
                }
            }
        }

        //EDIT Social DETAIL
        $scope.EditProductSocial = function (data, index) {
            $scope.EditProductSocialIndex = index;
            $scope.ProductSocial.AdId = data.AdId;
            $scope.ProductSocial.ProductId = data.ProductId;
            $scope.ProductSocial.CatalogId = data.CatalogId;
            $scope.ProductSocial.AdSourceId = data.AdSourceId;
            $scope.ProductSocial.Url = data.Url;
            $scope.ProductSocial.SourceName = data.SourceName;
            $scope.ProductSocial.SourceData = { Display: data.SourceName, Value: data.AdSourceId };
            $scope.ProductSocial.share = false;
            $scope.ProductSocial.ContactPerson = 0;
            $scope.ProductSocial.ContactPersonType = '';
        }

        //DELETE Social DETAIL
        $scope.DeleteProductSocial = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objProduct.ProductSocialMasters[index] = data;
                } else {
                    $scope.objProduct.ProductSocialMasters.splice(index, 1);
                }
                toastr.success("Social Details Delete", "Success");
            })
        }
        //END MANAGE PRODUCT Social

        //BEGIN MANAGE PRODUCT IMAGES
        $scope.AddProductImage = function (data) {
            if ($scope.EditProductPhotoIndex > -1) {
                if ($scope.addMode == false) {
                    $scope.tempImagePath = "";
                    data.Photo = "/UploadImages/TempImg/" + data.Photo;
                } else {
                    if ($scope.tempImagePath != "") {
                        data.Photo = $scope.tempImagePath;
                    }
                }
                $scope.objProduct.ProductPhotoMasters[$scope.EditProductPhotoIndex] = data;
                $scope.EditProductPhotoIndex = -1;
            } else {
                $scope.tempImagePath = "";
                data.Status = 1;
                data.Photo = "/UploadImages/TempImg/" + data.Photo;
                $scope.objProduct.ProductPhotoMasters.push(data);
            }
            $scope.objProduct.Isphotonext = true;
            $scope.objProduct.Iscatlognext = false;
            $scope.UpdateProductFrom($scope.objProduct);
            toastr.success("Product Images Add", "Success");
            $scope.ProductPhoto = {
                PhotoId: 0,
                ProductId: 0,
                Photo: '',
                share: false,
                IsDefault: false,
                Status: 0 //1 : Insert , 2:Update ,3 :Delete
            }
            $scope.tempImagePath = "";
            $scope.objProduct.EditProImgId = 0;
        }

        $scope.CancelProductImage = function () {
            $scope.ProductPhoto = {
                PhotoId: 0,
                ProductId: 0,
                Photo: '',
                share: false,
                IsDefault: false,
                Status: 0 //1 : Insert , 2:Update ,3 :Delete
            }
            $scope.tempImagePath = "";
            $scope.EditProductPhotoIndex = -1;
        }

        $scope.DeleteProductImage = function (data, index) {
            if (data.Status == 2) {
                data.Status = 3;
                $scope.objProduct.ProductPhotoMasters[index] = data;
            } else {
                $scope.objProduct.ProductPhotoMasters.splice(index, 1);
            }
            $scope.objProduct.EditProImgId = 0;
            toastr.success("Product Images Delete", "Success");
        }

        $scope.EditProductImage = function (data, index) {
            $scope.objProduct.EditProImgId = 1;
            $scope.EditProductPhotoIndex = index;
            var tempProjectImages = angular.copy(data);
            $scope.ProductPhoto = tempProjectImages;
            if ($scope.addMode == true) {
                $scope.tempImagePath = tempProjectImages.Photo;
            }
            else {
                $scope.tempImagePath = tempProjectImages.Photo;
            }
        }

        $scope.uploadImgFile = function (file, fileid, photodata) {

            //$scope.ProductPhoto.Photo = '';
            Upload.upload({
                url: "/Handler/FileUpload.ashx",
                method: 'POST',
                file: file,
            }).then(function (result) {
                if (result.config.file[0].type.match('image.*')) {
                    if (result.status == 200) {
                        if (result.data.length > 0) {
                            $scope.ProductPhoto.Photo = result.data[0].imagePath;
                            $scope.tempImagePath[fileid] = result.data[0].imagePath;
                        }
                        //$scope.ProductPhoto.Photo = $scope.tempImagePath[fileid];
                        if (photodata == 0) {
                            //$scope.ProductPhoto.Status = 1;
                            //$scope.ProductPhoto.IsDefault = (fileid == 3) ? true : false;
                            var prodphoto = {
                                PhotoId: $scope.ProductPhoto.PhotoId,
                                ProductId: $scope.ProductPhoto.ProductId,
                                Photo: $scope.ProductPhoto.Photo,
                                share: false,
                                //IsDefault: (fileid == 3) ? true : false,
                                IsDefault: fileid,
                                Status: 1 //1 : Insert , 2:Update ,3 :Delete
                            }
                            $scope.objProduct.ProductPhotoMasters.push(prodphoto);
                            //$scope.objProduct.ProductPhotoMasters[fileid].Photo = $scope.tempImagePath[fileid];
                        } else {
                            //photodata.IsDefault = (fileid == 3) ? true : false;
                            photodata.IsDefault = fileid;
                            photodata.Photo = $scope.ProductPhoto.Photo;
                            $scope.objProduct.ProductPhotoMasters[fileid] = photodata;
                        }
                    }
                    else {
                        $scope.ProductPhoto.Photo = '';
                    }
                } else {
                    toastr.error("Only Image File Allowed.", "Error");
                }
            });


        }
        //END MANAGE PRODUCT IMAGES
        //Catalog
        $scope.tempImagePath = [];
        $scope.uploadcataFile = function (file, fileid) {
            //$scope.objProduct.SupplierCatalogimg = '';
            //$scope.objProduct.OurCatalogImg = '';
            Upload.upload({
                url: "/Handler/FileUpload.ashx",
                method: 'POST',
                file: file,
            }).then(function (result) {
                if (result.config.file[0].type.match('image.*')) {
                    if (result.status == 200) {
                        if (result.data.length > 0) {
                            if (fileid == '7') {
                                $scope.objProduct.SupplierCatalogimg = result.data[0].imagePath.split('/')[result.data[0].imagePath.split('/').length - 1];
                                $scope.tempImagePath[fileid] = result.data[0].imagePath;
                            } else if (fileid == '8') {
                                $scope.objProduct.OurCatalogImg = result.data[0].imagePath.split('/')[result.data[0].imagePath.split('/').length - 1];
                                $scope.tempImagePath[fileid] = result.data[0].imagePath;
                            }
                        }
                    }
                    else {
                        $scope.objProduct.SupplierCatalogimg = '';
                        $scope.objProduct.OurCatalogImg = '';
                    }
                } else {
                    toastr.error("Only Image File Allowed.", "Error");
                }
            });


        }

        //END catalog
        //BEGIN MANAGE PRODUCT Catalogue
        $scope.AddProductCatalogue = function (data) {
            $scope.setsubpopup = 0;
            $scope.SetcatlogId = 0;
            $scope.catsubmitted = true;
            var bln = true;
            if ((data.SupplierData.Display == '' || data.SupplierData.Display == null)) {
                bln = false;
                toastr.error("Supplier is required.");
            } else if (data.SupplierModelNo == '' || data.SupplierModelNo == null) {
                bln = false;
                toastr.error("Supplier Model No is required.");
            } else if (data.ProductModelNo == '' || data.ProductModelNo == null) {
                bln = false;
                toastr.error("Product Model No is required.");
            } else if ((data.CountryOriginData.Display == '' || data.CountryOriginData.Display == null)) {
                bln = false;
                toastr.error("Country of origin is required.");
            }

            if (data.SupplierData.Display != '' && data.SupplierModelNo != '' && data.ProductModelNo != '' && bln == true) {
                $scope.catsubmitted = false;
                data.SupplierId = data.SupplierData.Value;
                data.SupplierName = data.SupplierData.Display;
                data.CountryOfOriginId = data.CountryOriginData.Value;
                data.CountryOfOriginName = data.CountryOriginData.Display;
                if ($scope.EditProductCatalogueIndex > -1) {
                    if ($scope.addMode == false) {
                        $scope.tempCataloguePath = "";
                        $scope.tempCataloguePhotoPath = "";
                        $scope.tempCatalogueMSO = "";
                        $scope.tempCataloguePDF = "";
                        data.CatalogPathName = data.CatalogPath;
                        data.PhotoPath = data.PhotoPath;
                        data.CatalogMSO = data.CatalogMSO;
                        data.CatalogPDF = data.CatalogPDF;
                        //data.CatalogPath = "/UploadImages/TempImg/" + data.CatalogPath;
                    } else {
                        if ($scope.tempCataloguePath != "") {
                            data.CatalogPathName = data.CatalogPath;
                            data.CatalogPath = $scope.tempCataloguePath;
                        }
                        if ($scope.tempCatalogueMSO != "") {
                            data.CatalogMSOName = data.CatalogMSO;
                            data.CatalogMSO = $scope.tempCatalogueMSO;
                        }
                        if ($scope.tempCataloguePDF != "") {
                            data.CatalogPDFName = data.CatalogPDF;
                            data.CatalogPDF = $scope.tempCataloguePDF;
                        }
                        data.PhotoPath = data.PhotoPath;
                    }
                    $scope.objProduct.ProductCatalogueMasters[$scope.EditProductCatalogueIndex] = data;
                    $scope.EditProductCatalogueIndex = -1;
                } else {
                    $scope.tempCataloguePath = "";
                    $scope.tempCataloguePhotoPath = "";
                    $scope.tempCatalogueMSO = "";
                    $scope.tempCataloguePDF = "";
                    data.Status = 1;
                    //if ($scope.addMode == true) {
                    data.CatalogPathName = data.CatalogPath;
                    data.CatalogMSOName = data.CatalogMSO;
                    data.CatalogPDFName = data.CatalogPDF;
                    //data.CatalogPath = "/UploadImages/TempImg/" + data.CatalogPath;
                    //}
                    $scope.objProduct.ProductCatalogueMasters.push(data);

                }

                $scope.objProduct.Isphotonext = false;
                $scope.objProduct.Iscatlognext = true;
                $scope.UpdateProductFrom($scope.objProduct);
                toastr.success("Product Catalogue Add", "Success");
                $scope.ProductCatalogue = {
                    CatalogId: 0,
                    ProductId: 0,
                    CatalogPath: '',
                    PhotoPath: '',
                    CatalogPathName: '',
                    CatalogMSO: '',
                    CatalogMSOName: '',
                    CatalogPDF: '',
                    CatalogPDFName: '',
                    share: false,
                    ContactPerson: 0,
                    ContactPersonType: '',
                    SupplierId: 0,
                    SupplierName: '',
                    SupplierModelNo: '',
                    ProductModelNo: '',
                    SupplierData: { Display: '', Value: '' },
                    CountryOriginData: { Display: '', Value: '' },
                    Status: 0,//1 : Insert , 2:Update ,3 :Delete
                    Capacity: ''
                }
                $scope.objProductDocument = {
                    DocID: 0,
                    ProductId: 0,
                    CatalogId: 0,
                    DocumentId: '',
                    DocumentName: '',
                    DocPath: '',
                    DocumentType: '',
                    Status: 0,
                    DocumentsData: { Display: '', Value: '' }
                };
                $scope.tempCataloguePhotoPath = "";
                $scope.tempCataloguePath = "";
                $scope.tempCatalogueMSO = "";
                $scope.tempCataloguePDF = "";
                $scope.objProduct.EditProCatalogueId = 0;

            }
        }

        $scope.CancelProductCatalogue = function () {
            $scope.ProductCatalogue = {
                CatalogId: 0,
                ProductId: 0,
                CatalogPath: '',
                PhotoPath: '',
                CatalogPathName: '',
                CatalogMSO: '',
                CatalogMSOName: '',
                CatalogPDF: '',
                CatalogPDFName: '',
                share: false,
                ContactPerson: 0,
                ContactPersonType: '',
                SupplierId: 0,
                SupplierName: '',
                SupplierModelNo: '',
                ProductModelNo: '',
                SupplierData: { Display: '', Value: '' },
                CountryOriginData: { Display: '', Value: '' },
                Status: 0,//1 : Insert , 2:Update ,3 :Delete
                Capacity: ''
            }
            $scope.tempCataloguePhotoPath = "";
            $scope.tempCataloguePath = "";
            $scope.tempCatalogueMSO = "";
            $scope.tempCataloguePDF = "";
            $scope.EditProductCatalogueIndex = -1;
        }

        //$scope.GetSupplierDetail = function (data, index) {
        //    $scope.objProduct.EditProCatalogueId = 1;
        //    $scope.EditProductCatalogueIndex = index;
        //    var tempProjectCatalogue = angular.copy(data);
        //    $scope.ProductCatalogue = tempProjectCatalogue;
        //    if ($scope.addMode == true) {
        //        $scope.tempCataloguePhotoPath = tempProjectCatalogue.PhotoPath;
        //        $scope.tempCataloguePath = tempProjectCatalogue.CatalogPath;
        //        $scope.tempCatalogueMSO = tempProjectCatalogue.CatalogMSO;
        //        $scope.tempCataloguePDF = tempProjectCatalogue.CatalogPDF;
        //    }
        //    else {
        //        $scope.tempCataloguePhotoPath = tempProjectCatalogue.PhotoPath;
        //        $scope.tempCataloguePath = tempProjectCatalogue.CatalogPath;
        //        $scope.tempCatalogueMSO = tempProjectCatalogue.CatalogMSO;
        //        $scope.tempCataloguePDF = tempProjectCatalogue.CatalogPDF;
        //    }
        //    //GetSupplierDetail - Functionality
        //    $scope.SetcatlogId = data.CatalogId;
        //    $scope.BindDataByProductCatelogID(data.ProductId, data.CatalogId);
        //}

        $scope.DeleteProductCatalogue = function (data, index) {
            if (data.Status == 2) {
                data.Status = 3;
                $scope.objProduct.ProductCatalogueMasters[index] = data;
            } else {
                $scope.objProduct.ProductCatalogueMasters.splice(index, 1);
            }
            $scope.objProduct.EditProCatalogueId = 0;
            $scope.UpdateProductFrom($scope.objProduct);
            toastr.success("Product Catalogue Delete", "Success");
        }

        $scope.EditProductCatalogue = function (data, index) {
            $scope.objProduct.EditProCatalogueId = 1;
            $scope.EditProductCatalogueIndex = index;
            var tempProjectCatalogue = angular.copy(data);
            $scope.ProductCatalogue = tempProjectCatalogue;
            if ($scope.addMode == true) {
                $scope.tempCataloguePhotoPath = tempProjectCatalogue.PhotoPath;
                $scope.tempCataloguePath = tempProjectCatalogue.CatalogPath;
                $scope.tempCatalogueMSO = tempProjectCatalogue.CatalogMSO;
                $scope.tempCataloguePDF = tempProjectCatalogue.CatalogPDF;
            }
            else {
                $scope.tempCataloguePhotoPath = tempProjectCatalogue.PhotoPath;
                $scope.tempCataloguePath = tempProjectCatalogue.CatalogPath;
                $scope.tempCatalogueMSO = tempProjectCatalogue.CatalogMSO;
                $scope.tempCataloguePDF = tempProjectCatalogue.CatalogPDF;
            }
            $scope.setsubpopup = 1;
            $scope.SetcatlogId = data.CatalogId;
            $scope.BindDataByProductCatelogID(data.ProductId, data.CatalogId);
            $scope.openSubTab("Click", "CatalogDoc", undefined, 0);
        }

        $scope.uploadCatalogueFile = function (file) {
            $scope.ProductCatalogue.CatalogPath = '';
            $scope.tempCataloguePath = '';
            Upload.upload({
                url: "/Handler/FileUpload.ashx",
                method: 'POST',
                file: file,
            }).then(function (result) {
                if (result.config.file[0].type.match('application/pdf')) {
                    if (result.status == 200) {
                        if (result.data.length > 0) {
                            $scope.ProductCatalogue.CatalogPath = result.data[0].imageName;
                            $scope.tempCataloguePath = result.data[0].imagePath;
                        }
                    }
                    else {
                        $scope.ProductCatalogue.CatalogPath = '';
                        $scope.tempCataloguePath = '';
                    }
                } else {
                    toastr.error("Only PDF File Allowed.", "Error");
                }
            });
        }
        $scope.uploadCataloguePhotoFile = function (file) {
            $scope.ProductCatalogue.PhotoPath = '';
            $scope.tempCataloguePhotoPath = '';
            Upload.upload({
                url: "/Handler/FileUpload.ashx",
                method: 'POST',
                file: file,
            }).then(function (result) {
                if (result.config.file[0].type.match('image.*')) {
                    if (result.status == 200) {
                        if (result.data.length > 0) {
                            $scope.ProductCatalogue.PhotoPath = result.data[0].imageName;
                            $scope.tempCataloguePhotoPath = result.data[0].imagePath;
                        }
                    }
                    else {
                        $scope.ProductCatalogue.PhotoPath = '';
                        $scope.tempCataloguePhotoPath = '';
                    }
                } else {
                    toastr.error("Only Image File Allowed.", "Error");
                }
            });
        }
        $scope.uploadCatalogueMSO = function (file) {
            $scope.ProductCatalogue.CatalogMSO = '';
            $scope.tempCatalogueMSO = '';
            Upload.upload({
                url: "/Handler/FileUpload.ashx",
                method: 'POST',
                file: file,
            }).then(function (result) {
                var ext = result.config.file[0].name.split('.').pop();
                if (ext == "docx" || ext == "doc" || ext == "xlsx" || ext == "xls") {
                    if (result.status == 200) {
                        if (result.data.length > 0) {
                            $scope.ProductCatalogue.CatalogMSO = result.data[0].imageName;
                            $scope.tempCatalogueMSO = result.data[0].imagePath;
                        }
                    }
                    else {
                        $scope.tempCatalogueMSO = '';
                        $scope.ProductCatalogue.CatalogMSO = '';
                    }
                } else {
                    toastr.error("Only Word/Excel File Allowed.", "Error");
                }
            });
        }
        $scope.uploadCataloguePDF = function (file) {
            $scope.ProductCatalogue.CatalogPDF = '';
            $scope.tempCataloguePDF = '';
            Upload.upload({
                url: "/Handler/FileUpload.ashx",
                method: 'POST',
                file: file,
            }).then(function (result) {
                if (result.config.file[0].type.match('application/pdf')) {
                    if (result.status == 200) {
                        if (result.data.length > 0) {
                            $scope.ProductCatalogue.CatalogPDF = result.data[0].imageName;
                            $scope.tempCataloguePDF = result.data[0].imagePath;
                        }
                    }
                    else {
                        $scope.ProductCatalogue.CatalogPDF = '';
                        $scope.tempCataloguePDF = '';
                    }
                } else {
                    toastr.error("Only PDF File Allowed.", "Error");
                }
            });
        }

        //END MANAGE PRODUCT IMAGES

        //BEGIN MANAGE DOCUMENT PRODUCT INFORMATION
        //$scope.objProduct.pro.ProductDocumentDetails
        $scope.AddProductDocumentDetail = function (data) {
            var dataerror = true;
            if ((data.DocumentsData.Display == '' || data.DocumentsData.Display == null)) {
                toastr.error("Document Type is required.");
                dataerror = false;
                $scope.Docsubmittedd = true;
            } else if (data.DocPath == '' || data.DocPath == null) {
                toastr.error("Please Upload Document.");
                dataerror = false;
                $scope.Docsubmittedd = true;
            } else if (data.Date == '' || data.Date == null) {
                toastr.error("Please Select Date.");
                dataerror = false;
                $scope.Docsubmittedd = true;
            }
            if (dataerror == true) {
                $scope.Docsubmittedd = false;
                var objProductDocument = {
                    PrdSupDocId: data.PrdSupDocId,
                    ProductId: parseInt($scope.objProduct.ProductData.Value),
                    CatalogId: parseInt($scope.SetcatlogId),
                    PrdDocId: data.DocumentsData.Value,
                    DocumentName: data.DocumentsData.Display,
                    DocPath: data.DocPath,
                    DocumentType: data.DocumentType,
                    Status: data.Status,
                    Remark: data.Remark,
                    Date: data.Date,
                    DocumentsData: { Display: data.DocumentsData.Display, Value: data.DocumentsData.Value }
                };
                $scope.CreateUpdateDocument(objProductDocument);
            }
        }

        $scope.EditProductDocumentDetail = function (data, index) {
            $scope.EditProductDocumentIndex = index;
            $scope.objProductDocument = {
                PrdSupDocId: data.PrdSupDocId,
                ProductId: data.ProductId,
                CatalogId: data.CatalogId,
                PrdDocId: data.PrdDocId,
                DocumentName: data.DocumentName,
                DocPath: data.DocPath,
                DocumentType: data.DocumentType,
                Status: data.Status,
                Date: data.Date,
                Remark: data.Remark,
                DocumentsData: { Display: data.DocumentName, Value: data.PrdDocId }
            };
            $scope.tempDocument = data.DocPath;
            $scope.tempType = data.DocumentType;
        }

        $scope.DeleteProductDocumentDetail = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objProduct.ProductSuppDocumentDetail[index] = data;
                } else {
                    $scope.objProduct.ProductSuppDocumentDetail.splice(index, 1);
                    $scope.objProductDocument.DocPath = '';
                    $scope.objProductDocument.DocumentsData = '';
                    $scope.tempDocument = '';
                    $scope.tempType = '';
                }
                toastr.success("Document Details Delete", "Success");
            })
        }

        $scope.CreateUpdateDocument = function (data) {
            data.PrdDocId = data.DocumentsData.Value;
            data.DocumentName = data.DocumentsData.Display;

            if ($scope.EditProductDocumentIndex > -1) {
                $scope.objProduct.ProductSuppDocumentDetail[$scope.EditProductDocumentIndex] = data;
                $scope.EditProductDocumentIndex = -1;
            } else {
                data.Status = 1;
                $scope.objProduct.ProductSuppDocumentDetail.push(data);
            }
            $scope.objProductDocument = {
                PrdSupDocId: 0,
                ProductId: 0,
                CatalogId: 0,
                PrdDocId: '',
                DocumentName: '',
                DocPath: '',
                DocumentType: '',
                Status: 0,
                Remark: '',
                Date: '',
                DocumentsData: { Display: '', Value: '' }
            };
            angular.forEach($scope.objProduct.ProductSuppDocumentDetail, function (value, index) {
                if (value.DocPath.indexOf('/UploadImages/') == -1) {
                    if (value.Status == 1) {
                        value.DocPath = '/UploadImages/TempImg/' + value.DocPath;
                    } else if (value.Status == 2) {
                        value.DocPath = '/UploadImages/TempImg/' + value.DocPath;
                    }
                }
            }, true);
            $scope.objProductDocument.DocPath = '';
            $scope.objProductDocument.DocumentType = '';
            $scope.tempDocument = '';
            $scope.tempType = '';
        }

        $scope.UploadDocumentFile = function (file) {
            $scope.objProductDocument.DocPath = '';
            $scope.objProductDocument.DocumentType = '';
            Upload.upload({
                url: "/Handler/FileUpload.ashx",
                method: 'POST',
                file: file,
            }).then(function (result) {
                var ext = result.config.file[0].name.split('.').pop();
                if (ext == "docx" || ext == "doc" || ext == "xlsx" || ext == "xls" || ext == "pdf" || ext == "jpg" || ext == "png") {
                    if (result.status == 200) {
                        if (result.data.length > 0) {
                            $scope.objProductDocument.DocPath = result.data[0].imageName;
                            $scope.objProductDocument.DocumentType = ext;
                            $scope.tempDocument = result.data[0].imagePath;
                            $scope.tempType = ext;
                        }
                    }
                    else {
                        $scope.objProductDocument.DocPath = '';
                        $scope.objProductDocument.DocumentType = '';
                    }
                } else {
                    toastr.error("Only Word, Excel, PDF, JPG, PNG File Allowed.", "Error");
                }
            });
        }
        //END MANAGE TAX PRODUCT INFORMATION


        $scope.AddProductShareDetail = function (data, tag) {
            if (angular.equals({}, data.share) || data.share == undefined) {
                toastr.error('Check Any one of share in list.');
            } else {
                //if(data.share)
                var modalInstance = $uibModal.open({
                    backdrop: 'static',
                    templateUrl: 'ProductFormShare.html',
                    controller: ModalInstanceCtrl,
                    resolve: {
                        ProductFormCtrl: function () { return $scope; },
                        ProductService: function () { return ProductService; },
                        ProductShareData: function () { return data; },
                        Tag: function () { return tag; }
                    }
                });
                modalInstance.result.then(function () {
                    $scope.EditUserContactIndex = -1;
                }, function () {
                    $scope.EditUserContactIndex = -1;
                })
            }
        }
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

        $scope.chkprdModelno = function (modelNo) {
            ProductService.CheckBuyerPrdModelNo(modelNo).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.isvalid = true
                } else {
                    $scope.isvalid = false
                    $scope.ProductCatalogue.ProductModelNo = '';
                    toastr.error(result.data.Message);
                }
            });
        }
        $scope.chkprdSupModelno = function (SupmodelNo) {
            ProductService.CheckSupplierModelNo(SupmodelNo).then(function (result) {

                if (result.data.ResponseType == 1) {
                    $scope.isvalid = true
                } else {
                    $scope.isvalid = false
                    $scope.ProductCatalogue.SupplierModelNo = '';
                    toastr.error(result.data.Message);
                }
            });
        }
        $scope.addProducts = function (data, _isdisable) {
            if (data.CurrencyData == undefined || data.CurrencyData.Display == '') {
                toastr.error("Currency is required.");
                return false;
            } else if (data.DealerPrice == undefined || data.DealerPrice == "") {
                toastr.error("Dealer Price is required.");
                return false;
            }
            $scope.objProduct.getAppChargeData = $.grep(data, function (element, index) {
                return (element.CurrencyId == $scope.objProduct.CurrencyData.Value);
            });


            var returnedData = $.grep($scope.objProduct.ProductPrices, function (element, index) {
                return (element.CurrencyDataId == data.CurrencyData.Value);
            });

            if (returnedData.length != 0) {
                toastr.error(data.CurrencyData.Display + " Currency Already Exists.");
                return false;
            }
            else {
                var modalInstance = $uibModal.open({
                    backdrop: 'static',
                    animation: true,
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'AddApplicablecharges.html',
                    controller: PackingModalInstanceCtrl,
                    size: 'lg',
                    resolve: {
                        ProductFormCtrl: function () { return $scope },
                        ProductService: function () { return ProductService },
                        ProductPacking: function () {
                            return data;
                        },
                        catId: $scope.SetcatlogId,
                        isdisable: function () { return _isdisable; }
                    }
                });
                modalInstance.result.then(function () {
                    $scope.EditProductIndex = -1;
                }, function () {
                    $scope.EditProductIndex = -1;
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                });
            }
        }

        //Edit productApp
        $scope.EditApplichargesParameter = function (data, index) {
            $scope.EditProductIndex = index;
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'AddApplicablecharges.html',
                controller: PackingModalInstanceCtrl,
                size: 'lg',
                resolve: {
                    ProductFormCtrl: function () { return $scope },
                    ProductService: function () { return ProductService },
                    ProductPacking: function () {
                        return $scope.objProduct;
                    },
                    ProductPriceData: function () {
                        return data;
                    },
                    catId: $scope.SetcatlogId,
                    isdisable: function () { return 0; }
                }
            });
            modalInstance.result.then(function () {
                $scope.EditProductIndex = -1;
            }, function () {
                $scope.EditProductIndex = -1;
                toastr.error(errorMsg, 'Opps, Something went wrong');
            });
        }
        //delete
        $scope.DeleteApplichargesParameter = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    angular.forEach($scope.objProduct.ProductPrices, function (value, index) {
                        if (value.ProductPriceId == data.ProductPriceId) {
                            value.Status = 3;
                        }
                    });
                    //$scope.objProduct.ProductPrices[index] = data;
                } else {
                    $scope.objProduct.ProductPrices.splice(index, 1);
                }
                for (var j = $scope.objProduct.ProductApplicableCharges.length - 1; j >= 0 ; j--) {
                    if ($scope.objProduct.ProductApplicableCharges[j].ProductPriceId == data.ProductPriceId) {
                        if (data.Status == 3) {
                            if ($scope.objProduct.ProductApplicableCharges[j].Status == 1)
                                $scope.objProduct.ProductApplicableCharges.splice(j, 1);
                            else
                                $scope.objProduct.ProductApplicableCharges[j].Status = 3;
                        } else {
                            $scope.objProduct.ProductApplicableCharges.splice(j, 1);
                        }
                    }
                }
                toastr.success("Price information delete", "Success");
                //$scope.SetDealerTotalPrice();
                var totaldealPrice = 0;
                angular.forEach($scope.objProduct.ProductPrices, function (value, index) {
                    if (value.Status != 3) {
                        value.TotalAmount = (parseFloat(value.TotalCharge) + parseFloat(value.BaseAmount))
                        totaldealPrice += parseFloat(value.TotalAmount);
                    }
                });
                $scope.objProduct.TotalDealerPrice = totaldealPrice;
                $scope.objProduct = angular.copy($scope.objProduct);
            })
        }

        $scope.InsetrProductsCharge = function (data) {
            $scope.IsEditexistApp = false;
            if (data.CurrencyData == undefined || data.CurrencyData.Display == '') {
                toastr.error("Currency is required.");
                return false;
            } else if (data.DealerPrice == undefined || data.DealerPrice == "") {
                toastr.error("Dealer Price is required.");
                return false;
            }
            var ObjPrice = {
                ProductPriceId: 0,
                ProductId: data.ProductId,
                CatalogId: data.ProductCatalogueMasters[0].CatalogId,
                SupplierId: $scope.SetcatlogId,
                CurrencyId: data.CurrencyData.Value,
                CurrencyName: data.CurrencyData.Display,
                BaseAmount: data.DealerPrice,
                TotalCharge: 0,
                TotalAmount: data.DealerPrice
            };
            if ($scope.EditProductIndex >= 0) {
                ObjPrice.Status = 2;
                $scope.objProduct.ProductPrices[$scope.EditProductIndex] = ObjPrice;
                $scope.EditProductIndex = -1;
                $scope.clrPriceDetail();
            } else {
                angular.forEach($scope.objProduct.ProductPrices, function (value, index) {
                    if ($scope.objProduct.ProductPrices[index].CurrencyId == data.CurrencyData.Value) {
                        $scope.IsEditexistApp = true;
                    }
                });
                if ($scope.IsEditexistApp == false) {
                    // to find Max array value in  ProductPriceId + 1
                    var tempId = 1;
                    $scope.objProduct.ProductPrices.map(function (obj) {
                        if (obj.ProductPriceId >= tempId) tempId = (obj.ProductPriceId + 1);
                    });

                    //var tempId = $.grep($scope.objProduct.ProductPrices, function (e) { return e.ProductPriceId == maxid; }).ProductPriceId;
                    ObjPrice.ProductPriceId = (tempId == undefined) ? 1 : tempId;
                    ObjPrice.Status = 1;
                    $scope.objProduct.ProductPrices.push(ObjPrice);
                    $scope.clrPriceDetail();
                } else {
                    toastr.error("Currency Already Exist.");
                }
            }
            var totaldealPrice = 0;
            angular.forEach($scope.objProduct.ProductPrices, function (value, index) {
                value.TotalAmount = (parseFloat(value.TotalCharge) + parseFloat(value.BaseAmount))
                totaldealPrice += parseFloat(value.TotalAmount);
            });
            $scope.objProduct.TotalDealerPrice = totaldealPrice;
            //$scope.objProduct.ProductPrices.push(ObjPrice);
        }

        $scope.EditProductsCharge = function (data, index) {
            $scope.EditProductIndex = index;
            $scope.objProduct.CurrencyId = data.CurrencyId;
            $scope.objProduct.CurrencyName = data.CurrencyName;
            $scope.objProduct.CurrencyData = { Display: data.CurrencyName, Value: data.CurrencyId };
            $scope.objProduct.DealerPrice = data.BaseAmount;
        }

        $scope.clrPriceDetail = function () {
            $scope.objProduct.CurrencyId = '';
            $scope.objProduct.CurrencyName = '';
            $scope.objProduct.CurrencyData = { Display: '', Value: '' };
            $scope.objProduct.DealerPrice = '';
        }

        $scope.$watch('objProduct.DealerPrice', function (newValue, oldValue) {
            //$scope.SetDealerTotalPrice();
        });
        $scope.SetDealerTotalPrice = function () {
            var dealerPrice = ParseFloat($scope.objProduct.DealerPrice);
            var total = dealerPrice;
            for (var i = 0; i < $scope.objProduct.ProductApplicableCharges.length; i++) {
                if ($scope.objProduct.CurrencyData.Value == $scope.objProduct.ProductApplicableCharges[i].CurrencyId) {
                    if (ParseFloat($scope.objProduct.ProductApplicableCharges[i].Percentage) > 0) {
                        $scope.objProduct.ProductApplicableCharges[i].OfferPrice = ParseFloat($scope.objProduct.ProductApplicableCharges[i].Percentage) * dealerPrice / 100;
                        total += $scope.objProduct.ProductApplicableCharges[i].OfferPrice;
                    }
                    else if (ParseFloat($scope.objProduct.ProductApplicableCharges[i].Price) > 0) {
                        $scope.objProduct.ProductApplicableCharges[i].OfferPrice = ParseFloat($scope.objProduct.ProductApplicableCharges[i].Price);
                        total += $scope.objProduct.ProductApplicableCharges[i].OfferPrice;
                    }
                }
            }
        }
    }
    var ModalInstanceCtrl = function ($scope, $uibModalInstance, ProductFormCtrl, ProductService, ProductShareData, Tag) {
        $scope.objProductShare = ProductShareData;
        $scope.close = function () {
            $uibModalInstance.close();
        };
        $scope.newValue = function (value) {
            //alert(value)
            $scope.ContactMode = value;
            $scope.objProductShare.ContactData = {
                Display: "",
                Value: ""
            };

        }
        $scope.sharefile = function (data) {
            var contact;
            if (data.ContactType == 1) {
                contact = 'VendorMaster';
            } else if (data.ContactType == 2) {
                contact = 'SupplierMaster';
            } else if (data.ContactType == 3) {
                contact = 'ProductMaster';
            }
            if (data.ContactData.Value != '' && data.ContactData.Value != null) {

                angular.forEach($scope.objProductShare.share, function (index, sharevalue) {
                    if (index == true) {
                        if (Tag == 'Catalogue') {
                            angular.forEach($scope.objProductShare.ProductCatalogueMasters, function (value, index) {
                                if (sharevalue == value.CatalogPathName) {
                                    //data.Status = value.Status;
                                    //data.CatalogPathName = value.CatalogPathName;
                                    //data.CatalogPath = value.CatalogPath;
                                    //data.IsDefault = value.IsDefault;
                                    $scope.objProductShare.ProductCatalogueMasters[index].share = true;
                                    $scope.objProductShare.ProductCatalogueMasters[index].ContactPerson = data.ContactData.Value;
                                    $scope.objProductShare.ProductCatalogueMasters[index].ContactPersonType = contact;
                                }
                            }, true);
                            $scope.objProductShare.mode = 'Catalogue';
                        }
                        else if (Tag == 'Photo') {
                            angular.forEach($scope.objProductShare.ProductPhotoMasters, function (value, index) {
                                if (sharevalue == value.Photo) {
                                    //data.Status = value.Status;
                                    //data.CatalogPathName = value.CatalogPathName;
                                    //data.CatalogPath = value.CatalogPath;
                                    //data.IsDefault = value.IsDefault;
                                    $scope.objProductShare.ProductPhotoMasters[index].share = true;
                                    //$scope.objProductShare.ProductPhotoMasters[index].Photo = sharevalue;
                                    $scope.objProductShare.ProductPhotoMasters[index].ContactPerson = data.ContactData.Value;
                                    $scope.objProductShare.ProductPhotoMasters[index].ContactPersonType = contact;
                                    //$scope.objProductShare.ProductCatalogueMasters[index].share = false;
                                }
                            }, true);
                            $scope.objProductShare.mode = 'Photo';
                        }
                        else if (Tag == 'Video') {
                            angular.forEach($scope.objProductShare.ProductVideoMasters, function (value, index) {
                                if (sharevalue == value.URL) {
                                    $scope.objProductShare.ProductVideoMasters[index].share = true;
                                    $scope.objProductShare.ProductVideoMasters[index].ContactPerson = data.ContactData.Value;
                                    $scope.objProductShare.ProductVideoMasters[index].ContactPersonType = contact;
                                }
                            }, true);
                            $scope.objProductShare.mode = 'Video';
                        }
                        else if (Tag == 'Social') {
                            angular.forEach($scope.objProductShare.ProductSocialMasters, function (value, index) {
                                if (sharevalue == value.Url) {
                                    $scope.objProductShare.ProductSocialMasters[index].share = true;
                                    $scope.objProductShare.ProductSocialMasters[index].ContactPerson = data.ContactData.Value;
                                    $scope.objProductShare.ProductSocialMasters[index].ContactPersonType = contact;
                                }
                            }, true);
                            $scope.objProductShare.mode = 'Social';
                        }
                    }
                }, true);
                var objData = {
                    mode: $scope.objProductShare.mode,
                    ProductCatalogMasters: $scope.objProductShare.ProductCatalogueMasters,
                    ProductPhotoMasters: $scope.objProductShare.ProductPhotoMasters,
                    ProductVideoMasters: $scope.objProductShare.ProductVideoMasters,
                    ProductSocialMasters: $scope.objProductShare.ProductSocialMasters,
                }
                ProductService.SendFile(objData).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        //toastr.success(result.data.Message);
                        $scope.close();
                    } else {
                        toastr.error(result.data.Message);
                    }
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            } else {
                toastr.error('Select Contact Person to Send File.');
            }
        }
    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'ProductFormCtrl', 'ProductService', 'ProductShareData', 'Tag']

    var PackingModalInstanceCtrl = function ($scope, $uibModalInstance, ProductFormCtrl, ProductService, ProductPacking, ProductPriceData, isdisable, catId) {

        $scope.IsexistApplicha = false;
        $scope.IsEditexistApp = false;
        $scope.objProduct = ProductPacking;
        $scope.Price = true;
        $scope.Percentage = true;

        $scope.fillAppGeid = function () {
            $scope.objProduct.getAppChargeData = [];
            var getdata = $.grep($scope.objProduct.ProductApplicableCharges, function (element, index) {
                return (element.ProductPriceId == ProductPriceData.ProductPriceId);
            });
            $scope.objProduct.getAppChargeData = angular.copy(getdata);
        }

        $scope.Objcharges = {
            ApplicableId: ProductPriceData.AppChargeId,
            SupplierId: ProductPriceData.SupplierId,
            ProductId: ProductPriceData.ProductId,
            ProductPriceId: ProductPriceData.ProductPriceId,
            AppChargeId: ProductPriceData.AppChargeId,
            AppCharge: ProductPriceData.AppCharge,
            AppliChargeData: { Display: ProductPriceData.AppCharge, Value: ProductPriceData.AppChargeId },
            Percentage: ProductPriceData.Percentage,
            Amount: ProductPriceData.Amount,
            ApplicableAmount: ProductPriceData.ApplicableAmount,
            Status: ProductPriceData.Status
        }

        //$scope.Objcharges = {
        //    ProductId: ProductPacking.ProductId,
        //    catelogId: catId,
        //    CurrencyId: ProductPacking.CurrencyId,
        //    CurrencyName: ProductPacking.CurrencyName,
        //    ApplicableChargeId: ProductPacking.ApplicableChargeId,
        //    ApplicableChargeName: ProductPacking.ApplicableChargeName,
        //    AppliChargeData: { Display: ProductPacking.ApplicableChargeName, Value: ProductPacking.ApplicableChargeId },
        //    Price: ProductPacking.Price,
        //    Percentage: ProductPacking.Percentage,
        //    OfferPrice: ProductPacking.OfferPrice,
        //    DealerPrice: ProductPacking.DealerPrice
        //};
        $scope.ProductPrices = ProductPacking.ProductPrices;

        $scope.fillAppGeid();

        $scope.close = function () {
            $uibModalInstance.close();
        };

        $scope.resetPopup = function () {
            $scope.Objcharges.AppChargeId = 0;
            $scope.Objcharges.AppCharge = '';
            $scope.Objcharges.AppliChargeData = { Display: '', Value: 0 }
            $scope.Objcharges.Amount = '';
            $scope.Objcharges.Percentage = '';
        };


        //Add application charges
        $scope.AddApplicharges = function (data) {
            $scope.IsexistApplicha = false;
            if (data.AppliChargeData == undefined || data.AppliChargeData.Display == undefined || data.AppliChargeData.Display == '') {
                toastr.error("Please Select Applicable Charge.");
                return false;
            } else if ((data.Amount == '' || data.Amount == undefined) && (data.Percentage == '' || data.Percentage == undefined)) {
                toastr.error("Please Enter Price Or Percentage value.");
                return false;
            }
            $scope.fillAppGeid();

            if (ParseFloat($scope.Objcharges.Amount) > 0 || ParseFloat($scope.Objcharges.Percentage) > 0) {
                angular.forEach($scope.objProduct.ProductApplicableCharges, function (value, index) {
                    if (value.ApplicableChargeId == data.AppliChargeData.Value && value.CurrencyId == $scope.objProduct.CurrencyData.Value) {
                        $scope.IsexistApplicha = true;
                    }
                });
                if ($scope.IsexistApplicha == false) {
                    var applichargeobj = {
                        ApplicableId: data.AppChargeId,
                        ProductPriceId: data.ProductPriceId,
                        AppChargeId: data.AppChargeId,
                        AppCharge: data.AppCharge,
                        AppliChargeData: { Display: data.AppCharge, Value: data.AppChargeId },
                        Percentage: data.Percentage,
                        Amount: data.Amount,
                        ApplicableAmount: data.ApplicableAmount,
                        Status: data.Status,
                        SupplierId: data.CatalogId,
                        ProductId: data.ProductId,
                    }
                    data.Status = 1;
                    ProductFormCtrl.objProduct.ProductApplicableCharges.push(applichargeobj);
                    $scope.resetPopup();
                    ProductFormCtrl.SetDealerTotalPrice();
                } else {
                    toastr.error("Applicable Charge Already Exist.");
                    $scope.Objcharges.AppliChargeData = '';
                }
            }
            else {
                toastr.error("Please Add Charges.");
            }
            $scope.fillAppGeid();
        }

        //$scope.saveApplicharges = function (data) {
        //    //Start : Set Curruncy Grid
        //    angular.forEach($scope.objProduct.ProductApplicableCharges, function (value, index) {
        //        debugger;
        //        var delPrice = ParseFloat(value.DealerPrice);
        //        var returnedData = $.grep($scope.ObjCurrencyData, function (element, index) {
        //            return (element.CurrencyDataId == value.CurrencyId);
        //        });
        //        var cntCur = ($scope.ObjCurrencyData.length == undefined) ? 0 : $scope.ObjCurrencyData.length;
        //        var total = 0;

        //        if (returnedData.length != 0) {
        //            // update
        //            if (returnedData[0].CurAppChargeId.indexOf(value.ApplicableChargeId) === -1 && returnedData[0].CurrencyDataId == $scope.objProduct.CurrencyData.Value) {
        //                if (ParseFloat(value.Percentage) > 0) {
        //                    total = (ParseFloat(value.Percentage) * ParseFloat(value.DealerPrice)) / 100;
        //                }
        //                else if (ParseFloat(value.Price) > 0) {
        //                    total = ParseFloat(value.Price);
        //                }
        //                if (returnedData[0].CurAppChargeId != undefined || returnedData[0].CurAppChargeId != '')
        //                    returnedData[0].CurAppChargeId = returnedData[0].CurAppChargeId + '-' + value.ApplicableChargeId;

        //                returnedData[0].TotalTax = parseFloat(returnedData[0].TotalTax) + parseFloat(total);
        //                returnedData[0].TotalCurAmt = parseFloat(returnedData[0].TotalCurAmt) + parseFloat(total);
        //            }
        //        } else { // Insert
        //            var taxDataonly = 0;
        //            if (ParseFloat(value.Percentage) > 0) {
        //                var caltotal = 0
        //                caltotal = (ParseFloat(value.Percentage) * ParseFloat(value.DealerPrice)) / 100;
        //                taxDataonly = caltotal;
        //                total = caltotal + ParseFloat(value.DealerPrice);
        //            }
        //            else if (ParseFloat(value.Price) > 0) {
        //                taxDataonly = ParseFloat(value.Price);
        //                total = ParseFloat(value.Price) + ParseFloat(value.DealerPrice);
        //            }
        //            var ObjCurrencyData = {
        //                SrId: (cntCur + 1),
        //                ProductId: data.ProductId,
        //                catelogId: data.catelogId,
        //                CurAppChargeId: value.ApplicableChargeId,
        //                CurrencyDataId: value.CurrencyId,
        //                CurrencyDataName: value.CurrencyName,
        //                DealerAmt: delPrice,
        //                TotalTax: taxDataonly,
        //                TotalCurAmt: total,
        //                Status: 1,
        //            };
        //            ProductFormCtrl.objProduct.ProductPrices.push(ObjCurrencyData);
        //        }
        //    });
        //END : Set Curruncy Grid

        //    $scope.close();

        //    $scope.objProduct.CurrencyData = { Display: '', Value: '' }
        //    $scope.objProduct.DealerPrice = '';


        //    var totaldealPrice = 0;
        //    angular.forEach($scope.objProduct.ProductPrices, function (value, index) {
        //        totaldealPrice += parseFloat(value.TotalCurAmt)
        //    });
        //    $scope.objProduct.TotalDealerPrice = totaldealPrice;
        //}

        $scope.saveApplicharges = function (data) {
            $scope.close();
            $scope.objProduct.CurrencyData = { Display: '', Value: '' }
            $scope.objProduct.DealerPrice = '';
            var totaldealPrice = 0, totaltaxPrice = 0;
            angular.forEach($scope.objProduct.ProductApplicableCharges, function (value, index) {
                if (value.ProductPriceId == ProductPriceData.ProductPriceId && value.Status != 3) {
                    totaltaxPrice += parseFloat(value.ApplicableAmount)
                }
            });
            angular.forEach($scope.objProduct.ProductPrices, function (value, index) {
                if (value.ProductPriceId == data.ProductPriceId) {
                    value.TotalCharge = totaltaxPrice
                }
            });
            angular.forEach($scope.objProduct.ProductPrices, function (value, index) {
                value.TotalAmount = (parseFloat(value.TotalCharge) + parseFloat(value.BaseAmount))
                totaldealPrice += parseFloat(value.TotalAmount);
            });
            $scope.objProduct.TotalDealerPrice = totaldealPrice;
            $scope.objProduct = angular.copy($scope.objProduct);
        }
        $scope.InsertApplicharges = function (data) {
            $scope.IsexistApplicha = false;
            if (data.AppliChargeData == undefined || data.AppliChargeData.Display == undefined || data.AppliChargeData.Display == '') {
                toastr.error("Please Select Applicable Charge.");
                return false;
            } else if ((data.Amount == '' || data.Amount == undefined) && (data.Percentage == '' || data.Percentage == undefined)) {
                toastr.error("Please Enter Price Or Percentage value.");
                return false;
            }
            if (ParseFloat($scope.Objcharges.Amount) > 0 || ParseFloat($scope.Objcharges.Percentage) > 0) {
                angular.forEach($scope.objProduct.ProductApplicableCharges, function (value, index) {
                    if (value.AppChargeId == data.AppliChargeData.Value && value.ProductPriceId == data.ProductPriceId) {
                        $scope.IsexistApplicha = true;
                    }
                });
                var AppAmount = 0;
                if (data.Percentage != undefined && data.Percentage != "")
                    data.ApplicableAmount = (ParseFloat(data.Percentage) * ParseFloat(ProductPriceData.TotalAmount) / 100);
                    //data.ApplicableAmount = ParseFloat(ProductPriceData.TotalAmount) + (ParseFloat(data.Percentage) * ParseFloat(ProductPriceData.TotalAmount) / 100);
                else if (data.Amount != undefined && data.Amount != "")
                    data.ApplicableAmount = ParseFloat(data.Amount);
                //data.ApplicableAmount = ParseFloat(ProductPriceData.TotalAmount) + ParseFloat(data.Amount);


                if ($scope.IsexistApplicha == false) {
                    var applichargeobj = {
                        ApplicableId: 0,
                        ProductPriceId: ProductPriceData.ProductPriceId,
                        AppChargeId: data.AppliChargeData.Value,
                        AppCharge: data.AppliChargeData.Display,
                        Percentage: (data.Percentage == undefined || data.Percentage == null) ? 0 : data.Percentage,
                        Amount: (data.Amount == undefined || data.Amount == null) ? 0 : data.Amount,
                        ApplicableAmount: data.ApplicableAmount,
                        Status: 1,
                        ProductId: data.ProductId,
                        SupplierId: data.SupplierId
                    }
                    data.Status = 1;
                    ProductFormCtrl.objProduct.ProductApplicableCharges.push(applichargeobj);
                    $scope.resetPopup();
                    //ProductFormCtrl.SetDealerTotalPrice();
                } else {
                    toastr.error("Applicable Charge Already Exist.");
                    $scope.Objcharges.AppliChargeData = '';
                }
            }
            else {
                toastr.error("Please Add Charges.");
            }
            $scope.fillAppGeid();
        }
        //delete
        $scope.Deletecharges = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    angular.forEach($scope.objProduct.ProductApplicableCharges, function (value, index) {
                        if (value.ApplicableId == data.ApplicableId) {
                            value.Status = 3;
                        }
                    });
                    //$scope.objProduct.ProductApplicableCharges[index] = data;
                } else {
                    $scope.objProduct.ProductApplicableCharges.splice(index, 1);
                }
                toastr.success("Charge Details Delete", "Success");
                //$scope.objProduct = angular.copy($scope.objProduct);
                //ProductPacking = angular.copy($scope.objProduct);
                //ProductFormCtrl.SetDealerTotalPrice();
                $scope.fillAppGeid();


            })
        }
        $scope.statusFilter = function (val) {
            if (val.Status != 3)
                return true;
        }
    }
    PackingModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'ProductFormCtrl', 'ProductService', 'ProductPacking', 'ProductPriceData', 'isdisable', 'catId']
})()

