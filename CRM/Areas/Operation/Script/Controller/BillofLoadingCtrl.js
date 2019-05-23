(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
            .controller("BillofLoadingCtrl", [
             "$scope", "$rootScope", "$timeout", "$filter", "BillofLoadingService", "CountryService", "NgTableParams", "$uibModal",
             BillofLoadingCtrl
            ]);

    function BillofLoadingCtrl($scope, $rootScope, $timeout, $filter, BillofLoadingService, CountryService, NgTableParams, $uibModal) {
        $scope.objBillofLoading = $scope.objBillofLoading || {};
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};

        $scope.objBillofLoading = {
            BLId: 0,
            ShipperName: '',
            ShipperAddress: '',
            ConsigneeName: '',
            ConsigneeAddress: '',
            Freight: '',
            POL: '',
            POD: '',
            ProductDescription: '',
            ShippingMarksNumber: '',
            TotalNOPkgs: '',
            GrossWeight: '',
            NetWeight: '',
            VolMeasurement: '',
            CompanyName: '',
            ShipperNameData: { Display: '', Value: '' },
            ShipperAddressData: { Display: '', Value: '' },
            ConsigneeNameData: { Display: '', Value: '' },
            ConsigneeAddressData: { Display: '', Value: '' },
            POLData: { Display: '', Value: '' },
            PODData: { Display: '', Value: '' },
            ProductDescriptionData: { Display: '', Value: '' },
            CompanyNameData: { Display: '', Value: '' },
            ProductDetails: []
        };
        $scope.objSOProduct = {
            BLId: 0,
            ProductId: 0,
            ProductName: '',
            Discription: '',
            Status: 0
        }
       
        $scope.SetBillofLoadingId = function (id, isdisable) {
            CountryService.GetCountryFlag().then(function (result) {
                 if (id > 0) {
                    //edit
                    $scope.SrNo = id;
                    $scope.addMode = false;
                    $scope.saveText = "Update";
                    $scope.GetAllBillofLoadingInfoById(id);
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
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.GetAllBillofLoadingInfoById = function (id) {
            BillofLoadingService.GetAllBillofLoadingInfoById(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    var objBillofLoadingMaster = result.data.DataList.objBillofLoadingMaster;

                    $scope.objBillofLoading = {
                        BLId: objBillofLoadingMaster.BLId,
                        ShipperName: objBillofLoadingMaster.ShipperName,
                        ShipperAddress: objBillofLoadingMaster.ShipperAddress,
                        ConsigneeName: objBillofLoadingMaster.ConsigneeName,
                        ConsigneeAddress: objBillofLoadingMaster.ConsigneeAddress,
                        Freight: objBillofLoadingMaster.Freight,
                        POL: objBillofLoadingMaster.POL,
                        POD: objBillofLoadingMaster.POD,
                        ProductDescription: objBillofLoadingMaster.ProductDescription,
                        ShippingMarksNumber: objBillofLoadingMaster.ShippingMarksNumber,
                        TotalNOPkgs: objBillofLoadingMaster.TotalNOPkgs,
                        GrossWeight: objBillofLoadingMaster.GrossWeight,
                        NetWeight: objBillofLoadingMaster.NetWeight,
                        VolMeasurement: objBillofLoadingMaster.VolMeasurement,
                        CompanyName: objBillofLoadingMaster.CompanyName,
                        ShipperNameData: { Display: objBillofLoadingMaster.ShipperName, Value: 0 },
                        ShipperAddressData: { Display: objBillofLoadingMaster.ShipperAddress, Value: 0 },
                        ConsigneeNameData: { Display: objBillofLoadingMaster.ConsigneeName, Value: 0 },
                        ConsigneeAddressData: { Display: objBillofLoadingMaster.ConsigneeAddress, Value: 0 },
                        POLData: { Display: objBillofLoadingMaster.POL, Value: 0 },
                        PODData: { Display: objBillofLoadingMaster.POD, Value: 0 },
                        CompanyNameData: { Display: objBillofLoadingMaster.CompanyName, Value: 0 },
                        ProductDetails: []
                       
                    };
                    //$scope.objShippingOrder.;
                    var getproduct = (objBillofLoadingMaster.ProductDescription != '') ? objBillofLoadingMaster.ProductDescription.split("|") : [];
                    if (getproduct.length > 0) {
                        angular.forEach(getproduct, function (value) {
                            var objSOProduct = {
                                BLId: objBillofLoadingMaster.BLId,
                                ProductId: value.split('-')[0],
                                ProductName: value.split('-')[1],
                                Discription: '',
                                Status: 2
                            }
                            $scope.objBillofLoading.ProductDetails.push(objSOProduct);
                        }, true);
                    }
                    //$scope.objShippingOrder.ProductDetails=
                    //ProductDetails = []// Data Get
                    $scope.storage = angular.copy($scope.objBillofLoading);
                   
                } else {
                    window.location.href = "/Operation/BillofLoading";
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            })
        }

     

        $scope.AddProduct = function (data) {
            var dataerror = true;
            if (data.ProductDescriptionData.Display == "" || data.ProductDescriptionData.Display == null) {
                toastr.error("Please Select Product.");
                dataerror = false;
                return false;
            }


            if (data != undefined && dataerror == true) {
                var objSOProduct = {
                    BLId: data.BLId,
                    ProductId: data.ProductDescriptionData.Value,
                    ProductName: data.ProductDescriptionData.Display,
                    Discription: '',
                    Status: data.Status
                }

                if ($scope.EditSOIndex > -1) {
                    if ($scope.objBillofLoading.ProductDetails[$scope.EditSOIndex].Status == 2) {
                        objSOProduct.Status = 2;
                    } else if ($scope.objBillofLoading.ProductDetails[$scope.EditSOIndex].Status == 1 ||
                               $scope.objBillofLoading.ProductDetails[$scope.EditSOIndex].Status == undefined) {
                        objSOProduct.Status = 1;
                    }
                    $scope.objBillofLoading.ProductDetails[$scope.EditSOIndex] = objSOProduct;
                    $scope.EditSOIndex = -1;
                } else {
                    objSOProduct.Status = 1;
                    $scope.objBillofLoading.ProductDetails.push(objSOProduct);
                }
                //$scope.CreateUpdate($scope.objShippingOrder);
                $scope.objSOProduct = {
                    BLId: 0,
                    ProductId: '',
                    ProductName: '',
                    Discription: '',
                    Status: 0
                }
                $scope.objBillofLoading.ProductDescriptionData.Display = ''
                $scope.objBillofLoading.ProductDescriptionData.Value = ''
            }
        }
        $scope.DeleteProduct = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objBillofLoading.ProductDetails[index] = data;
                } else {
                    $scope.objBillofLoading.ProductDetails.splice(index, 1);
                }
                toastr.success("Product detail Delete", "Success");
            })
        }
        $scope.EditProduct = function (data, index) {
            $scope.EditSOIndex = index;
            $scope.objSOProduct = {
                BLId: data.BLId,
                ProductId: data.ProductId,
                ProductName: data.ProductName,
                Discription: data.Discription,
                Status: data.Status
            }
            $scope.objBillofLoading.ProductDescriptionData.Display = data.ProductName
            $scope.objBillofLoading.ProductDescriptionData.Value = data.ProductId
        }


        $scope.Add = function () {
            window.location.href = "/Operation/BillofLoading/AddBillofLoading";
        }
        function ResetForm() {
            $scope.objBillofLoading = {
                BLId: 0,
                ShipperName: '',
                ShipperAddress: '',
                ConsigneeName: '',
                ConsigneeAddress: '',
                Freight: '',
                POL: '',
                POD: '',
                ProductDescription: '',
                ShippingMarksNumber: '',
                TotalNOPkgs: '',
                GrossWeight: '',
                NetWeight: '',
                VolMeasurement: '',
                CompanyName: '',
                ShipperNameData: { Display: '', Value: '' },
                ShipperAddressData: { Display: '', Value: '' },
                ConsigneeNameData: { Display: '', Value: '' },
                ConsigneeAddressData: { Display: '', Value: '' },
                POLData: { Display: '', Value: '' },
                PODData: { Display: '', Value: '' },
                ProductDescriptionData: { Display: '', Value: '' },
                CompanyNameData: { Display: '', Value: '' },
                ProductDetails: []
            };

            if ($scope.FormBillofLoadingInfo)
                $scope.FormBillofLoadingInfo.$setPristine();
            $scope.addMode = true;
            $scope.isFirstFocus = false;
            $scope.storage = {};
            $timeout(function () {
                $scope.isFirstFocus = true;
            });
            $scope.EditSOIndex = -1;
        }
        ResetForm();

        $scope.Reset = function () {
            if ($scope.addMode == false) {
                $scope.objBillofLoading = angular.copy($scope.storage);
            } else {
                ResetForm();
                $scope.SetBillofLoadingId(0);
            }
        }

        $scope.CountryBind = function (data) {
            CityService.CountryBind().then(function (result) {
                if (result.data.ResponseType == 1) {
                    _.each(result.data.DataList, function (value, key, list) {
                        if (value.CountryName.toString().toLowerCase() == data.toString().toLowerCase() && $scope.objShippingOrder.CountryData.Value == '') {
                            $scope.objShippingOrder.CountryData = {
                                Display: value.CountryName,
                                Value: value.CountryId
                            };
                            return false;
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
        $scope.$watch('objBillofLoading.ShipperNameData', function (data) {
            if (data.Display != $scope.objBillofLoading.ShipperNameData.Display) {
                $scope.objBillofLoading.ShipperAddressData.Display = '';
                $scope.objBillofLoading.ShipperAddressData.Value = '';

            }
        }, true)
        $scope.$watch('objBillofLoading.ConsigneeNameData', function (data) {
            if (data.Display != $scope.objBillofLoading.ConsigneeNameData.Display) {
                $scope.objBillofLoading.ConsigneeAddressData.Display = '';
                $scope.objBillofLoading.ConsigneeAddressData.Value = '';
              
            }
        }, true)

        $scope.CreateUpdate = function (data) {
             var Pdetails = data.ProductDetails.map(function (item) {
                return item['ProductId'] + '-' + item['ProductName'];
            });
            data.ProductDescription = Pdetails.join("|");
          
            var objBillofLoading = {
                BLId: data.BLId,
                ShipperName: data.ShipperNameData.Display,
                ShipperAddress: data.ShipperAddressData.Display,
                ConsigneeName: data.ConsigneeNameData.Display,
                ConsigneeAddress: data.ConsigneeAddressData.Display,
                Freight: data.Freight,
                POL: data.POLData.Display,
                POD: data.PODData.Display,
                ProductDescription: data.ProductDescription,
                ShippingMarksNumber: data.ShippingMarksNumber,
                TotalNOPkgs: data.TotalNOPkgs,
                GrossWeight: data.GrossWeight,
                NetWeight: data.NetWeight,
                VolMeasurement:data.VolMeasurement,
                CompanyName: data.CompanyNameData.Display
             
            };

            BillofLoadingService.CreateUpdateBillofLoading(objBillofLoading).then(function (result) {
                if (result.data.ResponseType == 1) {
                    ResetForm();
                    window.location.href = "/Operation/BillofLoading";
                    toastr.success(result.data.Message);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };

        $scope.gridObj = {
            columnsInfo: [
               //{ "title": "BL Id", "field": "BLId", filter: false, show: false },
               { "title": "Sr.", "field": "RowNumber", filter: false, sortable: false, show: true },
               { "title": "Shipper Name", "field": "ShipperName", sortable: "ShipperName", filter: { ShipperName: "text" }, show: true },
               { "title": "Shipper Address", "field": "ShipperAddress", sortable: "ShipperAddress", filter: { ShipperAddress: "text" }, show: false },
               { "title": "Consignee Name", "field": "ConsigneeName", sortable: "ConsigneeName", filter: { ConsigneeName: "text" }, show: true },
               { "title": "Consignee Address", "field": "ConsigneeAddress", sortable: "ConsigneeAddress", filter: { ConsigneeAddress: "text" }, show: false },
               { "title": "Freight", "field": "Freight", sortable: "Freight", filter: { Freight: "text" }, show: true },
               { "title": "Port Of Loading", "field": "POL", sortable: "POL", filter: { POL: "text" }, show: false },
               { "title": "Port Of Discharge", "field": "POD", sortable: "POD", filter: { POD: "text" }, show: false },
               { "title": "Product Description", "field": "ProductDescription", sortable: "ProductDescription", filter: { ProductDescription: "text" }, show: false },
               { "title": "Shipping Marks Number", "field": "ShippingMarksNumber", sortable: "ShippingMarksNumber", filter: { ShippingMarksNumber: "text" }, show: false },
               { "title": "Total NO Pkgs", "field": "TotalNOPkgs", sortable: "TotalNOPkgs", filter: { TotalNOPkgs: "text" }, show: false },
               { "title": "Gross Weight", "field": "GrossWeight", sortable: "GrossWeight", filter: { GrossWeight: "text" }, show: false },
               { "title": "Net Weight", "field": "NetWeight", sortable: "NetWeight", filter: { NetWeight: "text" }, show: false },
               { "title": "Vol Measurement", "field": "VolMeasurement", sortable: "VolMeasurement", filter: { VolMeasurement: "text" }, show: false },
               { "title": "Company Name", "field": "CompanyName", sortable: "CompanyName", filter: { CompanyName: "text" }, show: false },

               {
                   "title": "Action", sortable: false, filter: false, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.BLId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                         //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.$parent.$parent.$parent.Delete(row.BLId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                         '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.BLId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>' +
                         '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Print(row.BLId)" data-uib-tooltip="Print"><i class="fa fa-print" ></i></a>';
                       return $scope.getHtml(element);
                   }
                   //'render': '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.Edit(row.BLId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                   //      '<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.BLId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                   //      '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.View(row.BLId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>' +
                   //      '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.Print(row.BLId)" data-uib-tooltip="Print"><i class="fa fa-print" ></i></a>'


               }
            ],
            Sort: { 'BLId': 'asc' }
        }
        $scope.FormatEmail = function (d) {
            if (d != null) {
                var mailto = '';
                var emails = d.split(',');
                for (var i = 0; i < emails.length; i++) {
                    mailto += mailto == '' ? '' : ',';
                    mailto += '<a href="mailto:' + emails[i] + '" target="_blank">' + emails[i] + '</a>';
                }
                return mailto;
            }
        }

        $scope.Edit = function (id) {
            window.location.href = "/Operation/BillofLoading/AddBillofLoading/" + id + "/" + 0;
        }
        $scope.View = function (id) {
            window.location.href = "/Operation/BillofLoading/AddBillofLoading/" + id + "/" + 1;
        }

        $scope.Delete = function (id) {
            BillofLoadingService.DeleteBillofLoading(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    $scope.refreshGrid();
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            })
        }

        $scope.Print = function (id) {
            BillofLoadingService.PrintBillofLoading(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    var htmlData = (result.data.Message);
                    var doc = new jsPDF();
                    var specialElementHandlers = {
                        '#editor': function (element, renderer) {
                            return true;
                        }
                    };
                    doc.fromHTML(htmlData, 15, 15, {
                        'width': 100,
                        'elementHandlers': specialElementHandlers
                    });
                    doc.save('BillOfLoading.pdf')
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            })
        }
        $scope.download = function (id) {
            //login to get html data
            var htmlData = '<html><head><title></title><meta charset="utf-8" /><h2 style="text-align: center;">OfferLetter Letter</h2></head>' +
                '<body><header style="margin-bottom: 20px;"><p>@@OfferDate@@</p><table><tr><td>@@OfferCompanyHeadname@@</td></tr><tr><td>@@OfferCompanyname@@</td></tr><tr><td>@@AppointmentAddress@@</td></tr>' +
            '<tr><td>@@OfferPincode@@</td></tr></table></header>' +
            '<section><p>Dear Mr./Ms. @@OfferName@@:</p><p style="text-align: justify">This has reference to the various discussions you had with us.</p><p style="text-align: justify">we are pleased to make aprovisional offer ofappoitment as<b> @@OfferDesignation@@</b>.You Annual Earnings including Salary, allowances,annual benefits and stattutory as applicable will total Rs @@OfferSalary@@/-.</b>youwill recceive a detailed appointment order after you join.</p>' +
        '<p style="text-align: justify">On repoting please bring two recent passport size photographs,photocopy of all educationalcertificates,proof of page cerfiticate,IT declartion/Form 16 From last employer,lastemplyer\'s salary certificate and relieving letter of the last employer.</p>' +
        '<p style="text-align: justify">you are required to join the company on or before<b> @@OfferDate@@</b>.Please return a signed copy of this letter as a token of your acceptance of theofficer,confirming your date of joining.</p>' +
        '<p style="text-align: justify">We look forward to a mutually rewarding relationship.</p><br/>' +
    '</section><footer><p id="footer">Yours truly,</p><p>@@OfferName@@</p></footer></body></html>';
            var doc = new jsPDF();
            var specialElementHandlers = {
                '#editor': function (element, renderer) {
                    return true;
                }
            };

            // All units are in the set measurement for the document
            // This can be changed to "pt" (points), "mm" (Default), "cm", "in"
            doc.fromHTML(htmlData, 15, 15, {
                'width': 100,
                'elementHandlers': specialElementHandlers
            });
            //var doc = new jsPDF()
            //doc.text(htmlData, 10, 10)
            doc.save('a4.pdf')
        }
               
        $scope.dateOptions = {
            formatYear: 'yy',
            //minDate: new Date(2016, 5, 22),
            //maxDate: new Date(2017, 5, 22),
            startingDay: 1
        };
               
        $scope.statusFilter = function (val) {
            if (val.Status != 3)
                return true;
        }


    }

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