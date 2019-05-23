(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
            .controller("UploadProductDataCtrl", [
             "$scope", "$rootScope", "$timeout", "$filter", "UploadProductDataService", "ProductService", "$uibModal",
             UploadProductDataCtrl
            ]);

    function UploadProductDataCtrl($scope, $rootScope, $timeout, $filter, UploadProductDataService, ProductService, $uibModal) {

        $scope.objUploadProductData = $scope.objUploadProductData || {};
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};
        $scope.objUploadProductData = {
            AdId: 0,
            ProductCode: '',
            CategoryId: 0,
            Category: '',
            SubCategoryId: 0,
            SubCategory: '',
            MainProductId: 0,
            MainProductName: '',
            KeyWords: [],
            ProductId: 0,
            ProductName: '',
            CatalogId: 0,
            GooglePlusUrl: '',
            FbUrl: '',
            GDriveLink: '',
            DropboxLink: '',
            VideoUrl: '',
            VideoUrlDetails: [],
            SourceUrlDetails: [],
            UploadProductDataContactDetails: [],
            CategoryData: { Display: '', Value: '' },
            SubCategoryData: { Display: '', Value: '' },
            MainProductData: { Display: '', Value: '' },
            ProductData: { Display: '', Value: '' },
            SourceData: { Display: '', Value: '' },
            share: {},
        };
        $scope.objVideoUrlDetails = {
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
        $scope.objSourceUrlDetails = {
            AdId: 0,
            ProductId: 0,
            CatalogId: 0,
            AdSourceId: 0,
            SourceData: { Display: '', Value: '' },
            AdSource: '',
            Url: '',
            Remark: '',
            share: false,
            ContactPerson: 0,
            ContactPersonType: '',
            Status: 0 //1 : Insert , 2:Update ,3 :Delete
        }

        $scope.CatalogId = 0;

        $scope.openTab = function (evt, tabName, data, id) {
            // Declare all variables
            var bln = true;
            var dataerror = true;
            if (data != undefined) {
                if (tabName == 'OnlineFolders' || tabName == 'VideoDetails' || tabName == 'OnlinePortals') {
                    if (data.ProductCode == '' || data.ProductCode == null) {
                        toastr.error("Please Enter Product Code.");
                        dataerror = false;
                        return false;
                    } else if (data.keywords.length <= 0) {
                        toastr.error("Please Enter Keyword.");
                        dataerror = false;
                        return false;
                    }
                }
                if (dataerror == true) {
                    $scope.CreateUpdate(data);
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
            }
            if ($scope.issuccess == false && $scope.msg != '') {
                toastr.error($scope.msg);
            }
        }

        //SET DEFAULT SOId
        $scope.SetUploadProductDataId = function (id, isdisable, cid) {
            $scope.CatalogId = cid;
            $scope.openTab("Click", "ProductDetails", undefined, id);
            if (id > 0) {
                //edit
                $scope.SrNo = id;
                $scope.addMode = false;
                $scope.saveText = "Update";
                $scope.GetAllUploadProductDataById(id, cid);
                $scope.isClicked = false;
                if (isdisable == 1) {
                    $scope.isClicked = true;
                }
            } else {
                //add
                $scope.SrNo = 0;
                $scope.addMode = true;
                $scope.saveText = "Save";
                $scope.isClicked = false;
            }
        }

        $scope.GetAllUploadProductDataById = function (id, cid) {
            UploadProductDataService.GetProductDetailByCId(id, cid).then(function (result) {
                if (result.data.ResponseType == 1) {
                    if (result.data.DataList.objUploadProductData.length > 0) {
                        var objUploadProductDataMaster = result.data.DataList.objUploadProductData;
                        var ArrKeywords = [];
                        if (objUploadProductDataMaster[0].Keywords) {
                            _.forEach(objUploadProductDataMaster[0].Keywords.split(","), function (val) { ArrKeywords.push({ 'text': val }) });
                        }
                        $scope.objUploadProductData = {
                            AdId: objUploadProductDataMaster[0].AdId,
                            CategoryId: objUploadProductDataMaster[0].CategoryId,
                            Category: objUploadProductDataMaster[0].Category,
                            SubCategoryId: objUploadProductDataMaster[0].SubCategoryId,
                            SubCategory: objUploadProductDataMaster[0].SubCategory,
                            MainProductId: objUploadProductDataMaster[0].MainProductId,
                            MainProductName: objUploadProductDataMaster[0].MainProductName,
                            ProductId: objUploadProductDataMaster[0].ProductId,
                            ProductCode: objUploadProductDataMaster[0].ProductCode,
                            ProductName: objUploadProductDataMaster[0].ProductName,
                            GooglePlusUrl: objUploadProductDataMaster[0].GooglePlusUrl,
                            FbUrl: objUploadProductDataMaster[0].FbUrl,
                            GDriveLink: objUploadProductDataMaster[0].GDriveLink,
                            DropboxLink: objUploadProductDataMaster[0].DropboxLink,
                            HSCode: objUploadProductDataMaster[0].HSCode,
                            keywords: ArrKeywords,
                            //KeyWords: objUploadProductDataMaster[0].KeyWords,
                            CategoryData: { Display: objUploadProductDataMaster[0].Category, Value: objUploadProductDataMaster[0].CategoryId },
                            SubCategoryData: { Display: objUploadProductDataMaster[0].SubCategory, Value: objUploadProductDataMaster[0].SubCategoryId },
                            MainProductData: { Display: objUploadProductDataMaster[0].MainProductName, Value: objUploadProductDataMaster[0].MainProductId },
                            ProductData: { Display: objUploadProductDataMaster[0].ProductName, Value: objUploadProductDataMaster[0].ProductId },
                            SourceData: { Display: '', Value: '' }
                        };
                        // Product Video
                        $scope.objUploadProductData.VideoUrlDetails = [];
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
                                $scope.objUploadProductData.VideoUrlDetails.push(ProductVideo);
                            }, true);
                        }

                        // Product Social
                        $scope.objUploadProductData.SourceUrlDetails = [];
                        if (result.data.DataList.objProductSocialMaster.length > 0) {
                            angular.forEach(result.data.DataList.objProductSocialMaster, function (value) {
                                var ProductSocial = {
                                    AdId: value.AdId,
                                    ProductId: value.ProductId,
                                    CatalogId: value.CatalogId,
                                    AdSourceId: value.AdSourceId,
                                    AdSource: value.SourceName,
                                    SourceData: { Display: value.SourceName, Value: value.AdSourceId },
                                    SourceName: value.SourceName,
                                    Remark: value.Remark,
                                    Url: value.Url,
                                    Status: 2 //1 : Insert , 2:Update ,3 :Delete
                                }
                                $scope.objUploadProductData.SourceUrlDetails.push(ProductSocial);
                            }, true);
                        }

                        $scope.storage = angular.copy($scope.objUploadProductData);
                    } else {
                        ResetForm();
                    }
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }


        $scope.GetAllUploadProductDataInfoById = function (id) {
            UploadProductDataService.GetAllUploadProductDataInfoById(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    if (result.data.DataList.objUploadProductData.length > 0) {
                        var objUploadProductDataMaster = result.data.DataList.objUploadProductData;
                        var ArrKeywords = [];
                        if (objUploadProductDataMaster[0].Keywords) {
                            _.forEach(objUploadProductDataMaster[0].Keywords.split(","), function (val) { ArrKeywords.push({ 'text': val }) });
                        }
                        $scope.objUploadProductData = {
                            AdId: objUploadProductDataMaster[0].AdId,
                            CategoryId: objUploadProductDataMaster[0].CategoryId,
                            Category: objUploadProductDataMaster[0].Category,
                            SubCategoryId: objUploadProductDataMaster[0].SubCategoryId,
                            SubCategory: objUploadProductDataMaster[0].SubCategory,
                            MainProductId: objUploadProductDataMaster[0].MainProductId,
                            MainProductName: objUploadProductDataMaster[0].MainProductName,
                            ProductId: objUploadProductDataMaster[0].ProductId,
                            ProductCode: objUploadProductDataMaster[0].ProductCode,
                            ProductName: objUploadProductDataMaster[0].ProductName,
                            GooglePlusUrl: objUploadProductDataMaster[0].GooglePlusUrl,
                            FbUrl: objUploadProductDataMaster[0].FbUrl,
                            GDriveLink: objUploadProductDataMaster[0].GDriveLink,
                            DropboxLink: objUploadProductDataMaster[0].DropboxLink,
                            HSCode: objUploadProductDataMaster[0].HSCode,
                            keywords: ArrKeywords,
                            //KeyWords: objUploadProductDataMaster[0].KeyWords,
                            CategoryData: { Display: objUploadProductDataMaster[0].Category, Value: objUploadProductDataMaster[0].CategoryId },
                            SubCategoryData: { Display: objUploadProductDataMaster[0].SubCategory, Value: objUploadProductDataMaster[0].SubCategoryId },
                            MainProductData: { Display: objUploadProductDataMaster[0].MainProductName, Value: objUploadProductDataMaster[0].MainProductId },
                            ProductData: { Display: objUploadProductDataMaster[0].ProductName, Value: objUploadProductDataMaster[0].ProductId },
                            SourceData: { Display: '', Value: '' }
                        };

                        // Product Video
                        //$scope.objUploadProductData.VideoUrlDetails = [];
                        //if (result.data.DataList.objProductVideoMaster.length > 0) {
                        //    angular.forEach(result.data.DataList.objProductVideoMaster, function (value) {
                        //        var ProductVideo = {
                        //            VideoId: value.VideoId,
                        //            ProductId: value.ProductId,
                        //            CatalogId: value.CatalogId,
                        //            IsDefault: value.IsDefault,
                        //            URL: value.URL,
                        //            Status: 2 //1 : Insert , 2:Update ,3 :Delete
                        //        }
                        //        $scope.objUploadProductData.VideoUrlDetails.push(ProductVideo);
                        //    }, true);
                        //}

                        //// Product Social
                        //$scope.objUploadProductData.SourceUrlDetails = [];
                        //if (result.data.DataList.objProductSocialMaster.length > 0) {
                        //    angular.forEach(result.data.DataList.objProductSocialMaster, function (value) {
                        //        var ProductSocial = {
                        //            AdId: value.AdId,
                        //            ProductId: value.ProductId,
                        //            CatalogId: value.CatalogId,
                        //            AdSourceId: value.AdSourceId,
                        //            SourceData: { Display: value.SourceName, Value: value.AdSourceId },
                        //            SourceName: value.SourceName,
                        //            Url: value.Url,
                        //            Status: 2 //1 : Insert , 2:Update ,3 :Delete
                        //        }
                        //        $scope.objUploadProductData.SourceUrlDetails.push(ProductSocial);
                        //    }, true);
                        //}

                        //$scope.storage = angular.copy($scope.objUploadProductData);
                    } else {
                        ResetForm();
                    }
                } else {
                    window.location.href = "/Product/UploadProductData";
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.Add = function () {
            window.location.href = "/Product/UploadProductData/ManageUploadProductData";
        }

        //RESET
        function ResetForm() {
            $scope.objUploadProductData = {
                AdId: ($scope.SrNo && $scope.SrNo > 0) ? $scope.SrNo : 0,
                AdSourceId: 0,
                ProductCode: '',
                CategoryId: 0,
                Category: '',
                SubCategoryId: 0,
                SubCategory: '',
                MainProductId: 0,
                MainProductName: '',
                ProductId: 0,
                ProductName: '',
                GooglePlusUrl: '',
                FbUrl: '',
                VideoUrl: '',
                VideoUrlDetails: [],
                SourceUrlDetails: [],
                CategoryData: { Display: '', Value: '' },
                SubCategoryData: { Display: '', Value: '' },
                MainProductData: { Display: '', Value: '' },
                ProductData: { Display: '', Value: '' },
                SourceData: { Display: '', Value: '' },
                share: {},
            };

            $scope.objVideoUrlDetails = {
                VideoId: 0,
                ProductId: 0,
                CatalogId: 0,
                IsDefault: false,
                URL: '',
                share: false,
                ContactPerson: 0,
                ContactPersonType: '',
                Status: 0 //1 : Insert , 2:Update ,3 :Delete
            };

            $scope.objUploadProductDataDetails = {
                AdId: 0,
                ProductId: 0,
                AdSourceId: 0,
                AdSource: '',
                Url: '',
                Remark: '',
                Status: 0 //1 : Insert , 2:Update ,3 :Delete
            };

            if ($scope.FormUploadProductDataInfo)
                $scope.FormUploadProductDataInfo.$setPristine();
            $scope.addMode = true;
            $scope.isFirstFocus = false;
            $scope.storage = {};
            $timeout(function () {
                $scope.isFirstFocus = true;
            });
            $scope.EditSalesItemDetailsIndex = -1;
            $scope.EditVideoUrlDetailIndex = -1;
        }
        ResetForm();

        //RESET
        $scope.Reset = function () {
            if ($scope.addMode == false) {
                $scope.objUploadProductData = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        //DISPLAY GRID --done
        $scope.gridObj = {
            columnsInfo: [
               //{ "title": "Product Id", "field": "ProductId", sortable: "ProductId", filter: { ProductId: "text" }, show: true },
               { "title": "Sr.", "field": "RowNumber", filter: false, show: true },
               { "title": "Product Name", "field": "ProductName", sortable: "ProductName", filter: { ProductName: "text" }, show: true },
               { "title": "Product Code", "field": "ProductCode", sortable: "ProductCode", filter: { ProductCode: "text" }, show: true },
               { "title": "Supplier Name", "field": "SupplierName", sortable: "SupplierName", filter: { SupplierName: "text" }, show: true },
               {
                   "title": "Video Link", "field": "VideoLink", show: true, sortable: "VideoLink", filter: { VideoLink: "text" },
                   'cellTemplte': function ($scope, row) {
                       var element = "<p class='MailTo' ng-bind-html='getHtml($parent.$parent.$parent.$parent.$parent.$parent.FormatEmail(row.VideoLink))'>";
                       return $scope.getHtml(element);
                   }
               },
               {
                   "title": "Google+ Link", "field": "GplusLink", show: true, sortable: "GplusLink", filter: { GplusLink: "text" },
                   'cellTemplte': function ($scope, row) {
                       var element = "<p class='MailTo' ng-bind-html='getHtml($parent.$parent.$parent.$parent.$parent.$parent.FormatEmail(row.GplusLink))'>";
                       return $scope.getHtml(element);
                   }
               },
               {
                   "title": "Facebook Link", "field": "FbLink", show: true, sortable: "FbLink", filter: { FbLink: "text" },
                   'cellTemplte': function ($scope, row) {
                       var element = "<p class='MailTo' ng-bind-html='getHtml($parent.$parent.$parent.$parent.$parent.$parent.FormatEmail(row.FbLink))'>";
                       return $scope.getHtml(element);
                   }
               },
               {
                   "title": "WebSite", "field": "SourceLink", show: true, sortable: "SourceLink", filter: { SourceLink: "text" },
                   'cellTemplte': function ($scope, row) {
                       var element = "<p class='MailTo' ng-bind-html='getHtml($parent.$parent.$parent.$parent.$parent.$parent.FormatEmail(row.SourceLink))'>";
                       return $scope.getHtml(element);
                   }
               },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs" data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.ProductId,row.CatalogId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                       //'<a class="btn btn-danger btn-xs"  data-final-confirm-box=""  data-callback="$parent.$parent.$parent.$parent.$parent.$parent.Delete(row.ProductId,row.CatalogI)" data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                       '<a class="btn btn-info btn-xs" data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Share(row.ProductId,row.CatalogId)" data-uib-tooltip="Share"><i class="fa fa-share-alt" ></i></a>' +
                       '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.ProductId,row.CatalogId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>';
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'AdId': 'asc' }
        }

        $scope.FormatEmail = function (d) {
            if (d != null) {
                var mailto = '';
                var emails = d.split(',');
                for (var i = 0; i < emails.length; i++) {
                    mailto += mailto == '' ? '' : ',';
                    if (emails[i].indexOf('http://') > -1) {
                        mailto += '<a href="' + emails[i] + '" target="_blank">' + emails[i] + '</a>';
                    } else {
                        mailto += '<a href="http://' + emails[i] + '" target="_blank">' + emails[i] + '</a>';
                    }
                }
                return mailto;
            }
        }
        //REFRESH GRID
        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };

        $scope.$watch('objUploadProductData.CategoryData', function (data) {
            if (data.Value != $scope.objUploadProductData.CategoryId.toString()) {
                $scope.objUploadProductData.SubCategoryData.Display = '';
                $scope.objUploadProductData.SubCategoryData.Value = '';
                $scope.objUploadProductData.ProductData.Display = '';
                $scope.objUploadProductData.ProductData.Value = '';
            }
        }, true)

        $scope.$watch('objUploadProductData.SubCategoryData', function (data) {
            if (data.Value != $scope.objUploadProductData.SubCategoryId.toString()) {
                $scope.objUploadProductData.ProductData.Display = '';
                $scope.objUploadProductData.ProductData.Value = '';
            }
        }, true)



        //CREATE Main
        $scope.CreateUpdate = function (data, tag) {
            data.CategoryId = data.CategoryData.Value;
            data.SubCategoryId = data.SubCategoryData.Value;
            data.MainProductId = data.MainProductData.Value;
            data.ProductId = data.ProductData.Value;
            data.Category = data.CategoryData.Display;
            data.SubCategory = data.SubCategoryData.Display;
            data.MainProductName = data.MainProductData.Display;
            data.ProductName = data.ProductData.Display;
            data.KeyWords = _.map(data.keywords, 'text').join(',');
            if ($scope.objUploadProductData.SourceData != undefined && tag == 'final') {
                $scope.objUploadProductData.SourceData.Display = ' ';
            }
            UploadProductDataService.CreateUpdate(data).then(function (result) {
                if (result.data.ResponseType == 1) {
                    //ResetForm();
                    //toastr.success(result.data.Message);
                    //if (data.SOId > 0) {
                    if (tag == 'final') {
                        window.location.href = "/Product/UploadProductData";
                        toastr.success(result.data.Message);
                    }
                    //}
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            })
        }

        //EDIT Main
        $scope.Edit = function (id, cid) {
            window.location.href = "/Product/UploadProductData/ManageUploadProductData/" + id + "/" + 0 + "/" + cid;
        }
        $scope.View = function (id, cid) {
            window.location.href = "/Product/UploadProductData/ManageUploadProductData/" + id + "/" + 1 + "/" + cid;
        }
        $scope.Share = function (id, cid) {
            $scope.GetAllUploadProductDataById(id, cid);
            $scope.AddProductShareDetail($scope.objUploadProductData, '');
        }
        $scope.AddProductShareDetail = function (data, tag) {
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'ProductFormShare.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    UploadDataCtrl: function () { return $scope; },
                    UploadProductDataService: function () { return UploadProductDataService; },
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
        //DELETE Main
        $scope.Delete = function (id) {
            UploadProductDataService.DeleteById(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    $scope.refreshGrid();
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            })
        }

        //ADD VIDEO URL DETAIL

        $scope.AddVideoUrlDetail = function (data) {
            $scope.vdsubmitt = true;
            $scope.IsexistUrl = false;
            angular.forEach($scope.objUploadProductData.VideoUrlDetails, function (value, index) {
                if (value.URL == data.URL && data.CatalogId == 0) {
                    $scope.IsexistUrl = true;
                }
            });

            if ($scope.IsexistUrl == false) {
                if (data.URL != undefined && data.URL != "") {
                    $scope.vdsubmitt = false;
                    //if (data.IsDefault == true) {
                    //    angular.forEach($scope.objUploadProductData.VideoUrlDetails, function (value, index) {
                    //        $scope.objProduct.ProductVideoMasters[index].IsDefault = false;
                    //    }, true);
                    //}
                    var VideoDetails = {
                        VideoId: parseInt(data.VideoId),
                        ProductId: parseInt($scope.objUploadProductData.ProductId),
                        CatalogId: parseInt($scope.CatalogId),
                        IsDefault: data.IsDefault,
                        URL: data.URL
                    };

                    if ($scope.EditVideoUrlDetailIndex > -1) {
                        if ($scope.objUploadProductData.VideoUrlDetails[$scope.EditVideoUrlDetailIndex].Status == 2) {
                            VideoDetails.Status = 2;
                        } else if ($scope.objUploadProductData.VideoUrlDetails[$scope.EditVideoUrlDetailIndex].Status == 1 ||
                                   $scope.objUploadProductData.VideoUrlDetails[$scope.EditVideoUrlDetailIndex].Status == undefined) {
                            VideoDetails.Status = 1;
                        }
                        $scope.objUploadProductData.VideoUrlDetails[$scope.EditVideoUrlDetailIndex] = VideoDetails;
                        $scope.EditVideoUrlDetailIndex = -1;
                    } else {
                        VideoDetails.Status = 1;
                        $scope.objUploadProductData.VideoUrlDetails.push(VideoDetails);
                    }

                    data.VideoId = 0;
                    data.ProductId = 0;
                    data.CatalogId = 0;
                    data.IsDefault = false;
                    data.URL = '';
                    data.share = false;
                    data.ContactPerson = 0;
                    data.ContactPersonType = '';

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

        //EDIT VIDEO URL DETAIL
        $scope.EditVideoUrlDetail = function (data, index) {
            $scope.EditVideoUrlDetailIndex = index;
            $scope.objVideoUrlDetails = {
                VideoId: data.VideoId,
                ProductId: data.ProductId,
                CatalogId: $scope.CatalogId,
                IsDefault: data.IsDefault,
                URL: data.URL,
                share: false,
                ContactPerson: 0,
                ContactPersonType: ''
            }

        }

        //DELETE VIDEO URL DETAIL
        $scope.DeleteVideoUrlDetail = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objUploadProductData.VideoUrlDetails[index] = data;
                } else {
                    $scope.objUploadProductData.VideoUrlDetails.splice(index, 1);
                }
                toastr.success("Video Url Details Delete", "Success");
            })
        }

        //ADD SOURCE URL DETAIL
        $scope.AddOnlineProductUploadDetail = function (data) {
            $scope.datasubmitted = true;

            if ((data.SourceData.Display != '' && data.SourceData.Display != null) && (data.SourceUrl != '' && data.SourceUrl != null)) {
                var SUrlDetails = {
                    AdId: parseInt(data.AdId),
                    ProductId: parseInt(data.ProductData.Value),
                    CatalogId: $scope.CatalogId,
                    AdSourceId: parseInt(data.SourceData.Value),
                    AdSource: data.SourceData.Display,
                    Url: data.SourceUrl,
                    Remark: data.Remark
                }
                $scope.datasubmitted = false;
                if ($scope.EditOnlineProductUploadDetailIndex > -1) {
                    if ($scope.objUploadProductData.SourceUrlDetails[$scope.EditOnlineProductUploadDetailIndex].Status == 2) {
                        SUrlDetails.Status = 2;
                    } else if ($scope.objUploadProductData.SourceUrlDetails[$scope.EditOnlineProductUploadDetailIndex].Status == 1 ||
                               $scope.objUploadProductData.SourceUrlDetails[$scope.EditOnlineProductUploadDetailIndex].Status == undefined) {
                        SUrlDetails.Status = 1;
                    }
                    $scope.objUploadProductData.SourceUrlDetails[$scope.EditOnlineProductUploadDetailIndex] = SUrlDetails;
                    $scope.EditOnlineProductUploadDetailIndex = -1;
                } else {
                    SUrlDetails.Status = 1;
                    $scope.objUploadProductData.SourceUrlDetails.push(SUrlDetails);
                }
                //$scope.SourceUrlDetails = {
                //    AdId: 0,
                //    ProductId: 0,
                //    AdSourceId: 0,
                //    AdSource: '',
                //    UPDUrl: '',
                //    Remark: '',
                //    Status: 0
                //};
                $scope.objUploadProductData.SourceData.Value = '';
                $scope.objUploadProductData.SourceData.Display = '';
                $scope.objUploadProductData.SourceUrl = '';
                $scope.objUploadProductData.Remark = '';
            }
        }
        //EDIT SOURCE URL DETAIL
        $scope.EditOnlineProductUploadDetail = function (data, index) {
            $scope.EditOnlineProductUploadDetailIndex = index;
            $scope.objUploadProductData.SourceData.Value = data.AdSourceId;
            $scope.objUploadProductData.SourceData.Display = data.AdSource;
            $scope.objUploadProductData.SourceUrl = data.Url;
            $scope.objUploadProductData.Remark = data.Remark;

        }

        //DELETE SOURCE URL DETAIL
        $scope.DeleteOnlineProductUploadDetail = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objUploadProductData.SourceUrlDetails[index] = data;
                } else {
                    $scope.objUploadProductData.SourceUrlDetails.splice(index, 1);
                }
                toastr.success("Source Url Details Delete", "Success");
            })
        }

        //DONE
        $scope.statusFilter = function (val) {
            if (val.Status != 3)
                return true;
        }

    }

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, UploadDataCtrl, UploadProductDataService, ProductShareData, Tag) {
        $scope.objProductShare = ProductShareData;
        $scope.close = function () {
            $uibModalInstance.close();
        };
        $scope.newValue = function (value) {
            $scope.ContactMode = value;
            $scope.objProductShare.ContactData = {
                Display: "",
                Value: ""
            };
            $scope.objProductShare.ContactPersonData = {
                Display: "",
                Value: ""
            };
        }
        $scope.sharefile = function (data) {
            if (data.ContactData == undefined || data.ContactPersonData == undefined) {
                return false;
            }
            if (data.ContactData.Display == '' || data.ContactData.Display == null) {
                toastr.error("Contact Person is required.");
                dataerror = false;
                return false;
            } else if (data.ContactPersonData.Display == '' || data.ContactPersonData.Display == null) {
                toastr.error("Contact is required.");
                dataerror = false;
                return false;
            }

            var objData = {
                mode: data.ContactData.Value,
                ProductCatalogMasters: [],
                ProductPhotoMasters: [],
                ProductVideoMasters: $scope.objProductShare.VideoUrlDetails,
                ProductSocialMasters: $scope.objProductShare.SourceUrlDetails,
                EmailId: data.EmailId
            }
            UploadProductDataService.SendFile(objData).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.close();
                } else {
                    toastr.error(result.data.Message);
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })

        }
        $scope.$watch('objProductShare.ContactPersonData', function (newVal) {
            if (newVal) {
                UploadProductDataService.GetContactById(newVal.Value).then(function (result) {
                    if (result.data.DataList[0].Email != null) {
                        $scope.objProductShare.EmailId = result.data.DataList[0].Email;
                        return false;
                    }
                })
            }
        });
    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'UploadDataCtrl', 'UploadProductDataService', 'ProductShareData', 'Tag']
})()