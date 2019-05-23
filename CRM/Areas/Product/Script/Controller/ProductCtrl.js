(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("ProductCtrl", [
                    "$scope", "$rootScope", "$timeout", "$filter", "ProductService", "$uibModal", "Upload",
                    ProductCtrl
        ]);

    function ProductCtrl($scope, $rootScope, $timeout, $filter, ProductService, $uibModal, Upload) {

        $scope.objProduct = $scope.objProduct || {};
        $scope.objProduct = {
            ProductId: 0,
            CategoryId: 0,
            CategoryData: { Display: '', Value: '' },
            SubCategoryId: 0,
            SubCategoryData: { Display: '', Value: '' },
            MainProductId: 0,
            MainProductData: { Display: '', Value: '' },
            ProductName: '',
            HSCode: '',
            Keywords: [],
            Functionality: '',
            ProductCode: '',
        }

        $scope.Add = function (data, _isdisable) {
            if (_isdisable === undefined) _isdisable = 0;
            var ProductModalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'ProductModalContent.html',
                controller: ProductModalInstanceCtrl,
                resolve: {
                    ProductCtrl: function () { return $scope },
                    ProductService: function () { return ProductService; },
                    data: function () { return data; },
                    isdisable: function () { return _isdisable; }
                }
                // size:'lg'
            });
            ProductModalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
            });
        }

        $scope.gridObj = {
            columnsInfo: [
               //{ "title": "ProductId", "data": "ProductId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", filter: false, show: true },
                { "title": "Product Code", "field": "ProductCode", sortable: "ProductCode", filter: { ProductCode: "text" }, show: true },
               { "title": "Category", "field": "CategoryName", sortable: "CategoryName", filter: { CategoryName: "text" }, show: true },
               { "title": "Sub Category", "field": "SubCategoryName", sortable: "SubCategoryName", filter: { SubCategoryName: "text" }, show: true },
               //{ "title": "Main Product", "data": "MainProductName", sort: true, filter: true, },
                { "title": "Product", "field": "ProductName", sortable: "ProductName", filter: { ProductName: "text" }, show: true },
                { "title": "HSCode", "field": "HSCode", sortable: "HSCode", filter: { HSCode: "text" }, show: false },
                { "title": "Keywords", "field": "Keywords", sortable: "Keywords", filter: { Keywords: "text" }, show: false },
                { "title": "Description", "field": "Description", sortable: "Description", filter: { Description: "text" }, show: false },
                { "title": "Net Weight", "field": "NetWeight", sortable: "NetWeight", filter: { NetWeight: "text" }, show: false },
                { "title": "Gross Weight", "field": "GrossWeight", sortable: "GrossWeight", filter: { GrossWeight: "text" }, show: false },
                { "title": "Dimension", "field": "Dimension", sortable: "Dimension", filter: { Dimension: "text" }, show: false },
                { "title": "Dealer Price", "field": "DealerPrice", sortable: "DealerPrice", filter: { DealerPrice: "text" }, show: false },
                { "title": "Functionality", "field": "Functionality", sortable: "Functionality", filter: { Functionality: "text" }, show: false },

                //{ "title": "CBM", "field": "CBM", sortable: "CBM", filter: { CBM: "text" }, show: false },
                //{ "title": "Height", "field": "Height", sortable: "Height", filter: { Height: "text" }, show: false },
                //{ "title": "Width", "field": "Width", sortable: "Width", filter: { Width: "text" }, show: false },
                //{ "title": "Length", "field": "Length", sortable: "Length", filter: { Length: "text" }, show: false },
                //{ "title": "Google+ Link", "field": "GPlusLink", sortable: "GPlusLink", filter: { GPlusLink: "text" }, show: false },
                //{ "title": "Fb Link", "field": "FbLink", sortable: "FbLink", filter: { FbLink: "text" }, show: false },
                //{ "title": "OursModelNo", "field": "OursModelNo", sortable: "OursModelNo", filter: { OursModelNo: "text" }, show: false },
                //{ "title": "Price", "field": "Price", sortable: "Price", filter: { Price: "text" }, show: false },
                //{ "title": "Model No", "field": "ModelNo", sortable: "ModelNo", filter: { ModelNo: "text" }, show: false },
               //{ "title": "Net Weight", "field": "NetWeight", sortable: "NetWeight", filter: { NetWeight: "text" }, show: false },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit Product Name"><i class="fa fa-pencil" ></i></a>' +
                       //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.ProductId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                           '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View Product Name"><i class="fa fa-eye" ></i></a>' +
                           '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Show(row)" data-uib-tooltip="Edit Product Form"><i class="fa fa-pencil-square-o" ></i></a>'
                       return $scope.getHtml(element);
                   }

               }
            ],
            Sort: { 'ProductId': 'asc' },
        }

        $scope.Edit = function (data) {
            var ArrKeywords = [];
            if (data.Keywords) {
                _.forEach(data.Keywords.split(","), function (val) { ArrKeywords.push({ 'text': val }) });
            }
            var objData = {
                ProductId: data.ProductId,
                CategoryId: data.CategoryId,
                CategoryData: { Display: data.CategoryName, Value: data.CategoryId },
                SubCategoryId: data.SubCategoryId,
                SubCategoryData: { Display: data.SubCategoryName, Value: data.SubCategoryId },
                MainProductId: data.MainProductId,
                MainProductData: { Display: data.MainProductName, Value: data.MainProductId },
                ProductName: data.ProductName,
                HSCode: data.HSCode,
                Keywords: ArrKeywords,
                ProductCode: data.ProductCode,
                ProductFunctionality: data.Functionality
            }
            $scope.Add(objData, 0);
        }
        $scope.Show = function (data) {
            window.location.href = "/Product/ProductForm/AddProductForm/" + data.ProductId + "/" + 0;
        }
        $scope.View = function (data) {
            var ArrKeywords = [];
            if (data.Keywords) {
                _.forEach(data.Keywords.split(","), function (val) { ArrKeywords.push({ 'text': val }) });
            }
            var objData = {
                ProductId: data.ProductId,
                CategoryId: data.CategoryId,
                CategoryData: { Display: data.CategoryName, Value: data.CategoryId },
                SubCategoryId: data.SubCategoryId,
                SubCategoryData: { Display: data.SubCategoryName, Value: data.SubCategoryId },
                MainProductId: data.MainProductId,
                MainProductData: { Display: data.MainProductName, Value: data.MainProductId },
                ProductName: data.ProductName,
                HSCode: data.HSCode,
                Keywords: ArrKeywords,
                ProductCode: data.ProductCode,
                ProductFunctionality: data.Functionality
            }
            $scope.Add(objData, 1);
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

        $scope.gridObjProductList = {
            columnsInfo: [
              { "title": "Sr.", "field": "RowNumber", filter: false, show: true },
               { "title": "CatalogId", "field": "CatalogId", sortable: "CatalogId", filter: { ProductCode: "text" }, show: false },
               { "title": "ProductId", "field": "ProductId", sortable: "ProductId", filter: { ProductId: "text" }, show: false },
               { "title": "Product Code", "field": "ProductCode", sortable: "ProductCode", filter: { ProductCode: "text" }, show: true },
               { "title": "Product Name", "field": "ProductName", sortable: "ProductName", filter: { ProductName: "text" }, show: true },
               { "title": "Supplier Code", "field": "SupplierModelNo", sortable: "SupplierModelNo", filter: { SupplierModelNo: "text" }, show: true },
               { "title": "Supplier Name", "field": "CompanyName", sortable: "CompanyName", filter: { SubCategoryName: "text" }, show: true },
               //{ "title": "OurCatalogImg", "field": "OurCatalogImg", sortable: "OurCatalogImg", filter: { OurCatalogImg: "text" }, show: true },
              {
                  "title": "Action", show: true,
                  'cellTemplte': function ($scope, row) {
                      var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Print(row)" data-uib-tooltip="Print"><i class="fa fa-print" ></i></a>' +
                          '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.OurProductCatelog(row)" data-uib-tooltip="Our Catelog"><i class="fa fa-picture-o" ></i></a>'
                      return $scope.getHtml(element);
                  }

              }
            ],
            Sort: { 'ProductId': 'asc' },
        }

        $scope.Print = function (data) {

            var CatalogId = data.CatalogId;
            var ProductId = data.ProductId;
            // <a href="~/Reports/Index?ReportName=ProductDetail&ID="&CatalogId class="nav-link ">
            window.open('/ReportViewer.aspx?ReportName=ProductDetail&ID=' + CatalogId, "ProductDetails Reports", "resizable,scrollbars,status,width=1200,height=700,menubar=no");
        }

        $scope.OurProductCatelog = function (d) {
            if (d.OurCatalogImg != 'null' && d.OurCatalogImg!=null) {
                var win = window.open("/UploadImages/OurCatalogImage/" + d.OurCatalogImg, '_blank');
                win.focus();
            } else {
                toastr.error('catalog Photo is not found');
            }
        }

        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };

    }

    var ProductModalInstanceCtrl = function ($scope, $uibModalInstance, ProductCtrl, ProductService, data, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }

        $scope.objProduct = $scope.objProduct || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};

        if (data.ShapeId <= 0) {
            ResetForm()
        } else {
            $scope.objProduct = angular.copy(data);
            $scope.storage = angular.copy(data);
        }

        function ResetForm() {
            $scope.objProduct = {
                ProductId: 0,
                CategoryId: 0,
                CategoryData: { Display: '', Value: '' },
                SubCategoryId: 0,
                SubCategoryData: { Display: '', Value: '' },
                MainProductId: 0,
                MainProductData: { Display: '', Value: '' },
                ProductName: '',
                HSCode: '',
                Keywords: []
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.FormProductInfo)
                $scope.FormProductInfo.$setPristine();
        }

        $scope.Reset = function () {
            if ($scope.objProduct.ProductId > 0) {
                $scope.objProduct = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

        $scope.$watch('objProduct.CategoryData', function (dataa) {
            if (dataa.Value != data.CategoryId.toString()) {
                $scope.objProduct.SubCategoryData.Display = '';
                $scope.objProduct.SubCategoryData.Value = '';

            }
        }, true)

        $scope.setproductcode = function (data) {
            if (data) {
                ProductService.AutoProductCode(data).then(function (result) {
                    $scope.objProduct.ProductCode = result.data.DataList.PrdData;
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }
        }

        $scope.CreateUpdate = function (data) {
            if (data.Keywords.length < 4) {
                toastr.error("Please Enter Minimum 4 keyword.");
            } else if (data.Keywords.length > 8) {
                toastr.error("Please Enter Maximum 8 keyword.");
            }
            else {
                var objProduct = {
                    ProductId: data.ProductId,
                    CategoryId: data.CategoryData.Value,
                    SubCategoryId: data.SubCategoryData.Value,
                    MainProductId: data.MainProductData.Value,
                    ProductName: data.ProductName,
                    HSCode: data.HSCode,
                    Keywords: _.map(data.Keywords, 'text').join(','),
                    ProductCode: data.ProductCode,
                    Functionality: data.ProductFunctionality
                }
                ProductService.CreateUpdateProduct(objProduct).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        $uibModalInstance.close();
                        toastr.success(result.data.Message);
                    } else {
                        toastr.error(result.data.Message);
                    }
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }
        }

        $scope.printKeyword = function (Keyword) {
            if (Keyword.length == 0)
                toastr.error("Please Enter keyword.");
            else {
                var str = '';
                angular.forEach(Keyword, function (value) {
                    var obj = {
                        KeyVal: value.text
                    }
                    str += value.text + ',';
                    // $scope.Keyworddetails.push(obj);
                })
                window.location.href = "/Product/Product/PrintKeyword?Key=" + str;
            }
        }


    }
    ProductModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'ProductCtrl', 'ProductService', 'data', 'isdisable']
})()