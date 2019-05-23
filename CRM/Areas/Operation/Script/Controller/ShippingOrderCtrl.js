(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
            .controller("ShippingOrderCtrl", [
             "$scope", "$rootScope", "$timeout", "$filter", "ShippingOrderService", "CountryService", "NgTableParams", "$uibModal",
             ShippingOrderCtrl
            ]);

    function ShippingOrderCtrl($scope, $rootScope, $timeout, $filter, ShippingOrderService, CountryService, NgTableParams, $uibModal) {
        $scope.objShippingOrder = $scope.objShippingOrder || {};
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};

        $scope.objShippingOrder = {
            ShippingOrdId: 0,
            TypeofShipment: '',
            Commodity: '',
            Nooftotal: '',
            NoofBL: '',
            CPBuyerName: '',
            CPBuyerAddress: '',
            CPBuyerTelephone: '',
            CPBuyerFax: '',
            CPBuyerContactPerson: '',
            EDBuyerName: '',
            EDBuyerAddress: '',
            EDBuyerTelephone: '',
            EDBuyerContactPerson: '',
            Freight: '',
            POL: '',
            POD: '',
            ProductDescription: '',
            ShippingMarksNumber: '',
            TotalNOPkgs: '',
            TotalGross: '',
            Measurement: '',
            Shipmentterms: '',
            CompanyName: '',
            CPBuyerNameData: { Display: '', Value: '' },
            CPBuyerAddressData: { Display: '', Value: '' },
            CPBuyerTelephoneData: { Display: '', Value: '' },
            CPBuyerFaxData: { Display: '', Value: '' },
            CPBuyerContactPersonData: { Display: '', Value: '' },
            EDBuyerNameData: { Display: '', Value: '' },
            EDBuyerAddressData: { Display: '', Value: '' },
            EDBuyerTelephoneData: { Display: '', Value: '' },
            EDBuyerContactPersonData: { Display: '', Value: '' },
            POLData: { Display: '', Value: '' },
            PODData: { Display: '', Value: '' },
            ProductDescriptionData: { Display: '', Value: '' },
            ShipmenttermsData: { Display: '', Value: '' },
            ProductDetails: []
        };
        $scope.objSOProduct = {
            ShippingOrdId: 0,
            ProductId: 0,
            ProductName: '',
            Discription: '',
            Status: 0
        }
        $scope.CPtelCodeData = [];
        $scope.CPfaxCodeData = [];
        $scope.EDtelCodeData = [];

        $scope.SetShippingOrderId = function (id, isdisable) {
            CountryService.GetCountryFlag().then(function (result) {
                $scope.CPtelCodeData = angular.copy(result);
                $scope.CPfaxCodeData = angular.copy(result);
                $scope.EDtelCodeData = angular.copy(result);
                if (id > 0) {
                    //edit
                    $scope.SrNo = id;
                    $scope.addMode = false;
                    $scope.saveText = "Update";
                    $scope.GetAllShippingOrderInfoById(id);
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

        $scope.GetAllShippingOrderInfoById = function (id) {
            ShippingOrderService.GetAllShippingOrderInfoById(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    var objShippingOrderMaster = result.data.DataList.objShippingOrderMaster;

                    $scope.CPBuyerTelephone = (objShippingOrderMaster.CPBuyerTelephone != '' || objShippingOrderMaster.CPBuyerTelephone != null) ? objShippingOrderMaster.CPBuyerTelephone.split(",") : [];
                    $scope.CPBuyerFax = (objShippingOrderMaster.CPBuyerFax != '' || objShippingOrderMaster.CPBuyerFax != null) ? objShippingOrderMaster.CPBuyerFax.split(",") : [];
                    $scope.EDBuyerTelephone = (objShippingOrderMaster.EDBuyerTelephone != '' || objShippingOrderMaster.EDBuyerTelephone != null) ? objShippingOrderMaster.EDBuyerTelephone.split(",") : [];

                    $scope.objShippingOrder = {
                        ShippingOrdId: objShippingOrderMaster.ShippingOrdId,
                        TypeofShipment: objShippingOrderMaster.TypeofShipment,
                        Commodity: objShippingOrderMaster.Commodity,
                        Nooftotal: objShippingOrderMaster.Nooftotal,
                        NoofBL: objShippingOrderMaster.NoofBL,
                        CPBuyerName: objShippingOrderMaster.CPBuyerName,
                        CPBuyerAddress: objShippingOrderMaster.CPBuyerAddress,
                        CPBuyerTelephone: objShippingOrderMaster.CPBuyerTelephone,
                        CPBuyerFax: objShippingOrderMaster.CPBuyerFax,
                        CPBuyerContactPerson: objShippingOrderMaster.CPBuyerContactPerson,
                        EDBuyerName: objShippingOrderMaster.EDBuyerName,
                        EDBuyerAddress: objShippingOrderMaster.EDBuyerAddress,
                        EDBuyerTelephone: objShippingOrderMaster.EDBuyerTelephone,
                        EDBuyerContactPerson: objShippingOrderMaster.EDBuyerContactPerson,
                        Freight: objShippingOrderMaster.Freight,
                        POL: objShippingOrderMaster.POL,
                        POD: objShippingOrderMaster.POD,
                        ProductDescription: objShippingOrderMaster.ProductDescription,
                        ShippingMarksNumber: objShippingOrderMaster.ShippingMarksNumber,
                        TotalNOPkgs: objShippingOrderMaster.TotalNOPkgs,
                        TotalGross: objShippingOrderMaster.TotalGross,
                        Measurement: objShippingOrderMaster.Measurement,
                        Shipmentterms: objShippingOrderMaster.Shipmentterms,
                        CompanyName: objShippingOrderMaster.CompanyName,
                        CPBuyerNameData: { Display: objShippingOrderMaster.CPBuyerName, Value: 0 },
                        CPBuyerAddressData: { Display: objShippingOrderMaster.CPBuyerAddress, Value: 0 },
                        CPBuyerTelephoneData: { Display: objShippingOrderMaster.CPBuyerTelephone, Value: 0 },
                        EDBuyerContactPersonData: { Display: objShippingOrderMaster.EDBuyerContactPerson, Value: 0 },
                        CPBuyerContactPersonData: { Display: objShippingOrderMaster.CPBuyerContactPerson, Value: 0 },
                        CPBuyerFaxData: { Display: objShippingOrderMaster.CPBuyerFax, Value: 0 },
                        EDBuyerNameData: { Display: objShippingOrderMaster.EDBuyerName, Value: 0 },
                        EDBuyerAddressData: { Display: objShippingOrderMaster.EDBuyerAddress, Value: 0 },
                        EDBuyerTelephoneData: { Display: objShippingOrderMaster.EDBuyerTelephone, Value: 0 },
                        POLData: { Display: objShippingOrderMaster.POL, Value: 0 },
                        PODData: { Display: objShippingOrderMaster.POD, Value: 0 },
                        //ProductDescriptionData: { Display: objShippingOrderMaster.ProductDescription, Value: 0 },
                        ShipmenttermsData: { Display: objShippingOrderMaster.Shipmentterms, Value: 0 },
                        CompanyNameData: { Display: objShippingOrderMaster.CompanyName, Value: 0 },
                        ProductDetails: []
                    };
                    //$scope.objShippingOrder.;
                    var getproduct = (objShippingOrderMaster.ProductDescription != '') ? objShippingOrderMaster.ProductDescription.split("|") : [];
                    if (getproduct.length > 0) {
                        angular.forEach(getproduct, function (value) {
                            var objSOProduct = {
                                ShippingOrdId: objShippingOrderMaster.ShippingOrdId,
                                ProductId: value.split('-')[0],
                                ProductName: value.split('-')[1],
                                Discription: '',
                                Status: 2
                            }
                            $scope.objShippingOrder.ProductDetails.push(objSOProduct);
                        }, true);
                    }
                    //$scope.objShippingOrder.ProductDetails=
                    //ProductDetails = []// Data Get
                    $scope.storage = angular.copy($scope.objShippingOrder);
                    Reporting();
                } else {
                    window.location.href = "/Transaction/ShippingOrder";
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            })
        }

        function Reporting() {
            TaskService.ReportingUserBind(0).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objShippingOrder.ReportingUserArray = [];
                    var Mainres, data = '', res = '', AssignMsg = '', AssigneIds = '';
                    _.each(result.data.DataList, function (value, key, list) {
                        if (value.UserId == $scope.objShippingOrder.AssignTo) {
                            $scope.objShippingOrder.ReportingUserArray.push({
                                name: value.UserId,
                                maker: value.Name,
                                ticked: true,
                                disabled: (AssigneIds != undefined && AssigneIds.includes(value.UserId.toString())) ? true : false
                            })

                        }
                        else {
                            $scope.objShippingOrder.ReportingUserArray.push({
                                name: value.UserId,
                                maker: value.Name,
                                ticked: false,
                                disabled: (AssigneIds != undefined && AssigneIds.includes(value.UserId.toString())) ? true : false
                            })
                        }
                    })
                }
                else {
                    toastr.error(result.data.Message)
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
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
                    ShippingOrdId: data.ShippingOrdId,
                    ProductId: data.ProductDescriptionData.Value,
                    ProductName: data.ProductDescriptionData.Display,
                    Discription: '',
                    Status: data.Status
                }

                if ($scope.EditSOIndex > -1) {
                    if ($scope.objShippingOrder.ProductDetails[$scope.EditSOIndex].Status == 2) {
                        objSOProduct.Status = 2;
                    } else if ($scope.objShippingOrder.ProductDetails[$scope.EditSOIndex].Status == 1 ||
                               $scope.objShippingOrder.ProductDetails[$scope.EditSOIndex].Status == undefined) {
                        objSOProduct.Status = 1;
                    }
                    $scope.objShippingOrder.ProductDetails[$scope.EditSOIndex] = objSOProduct;
                    $scope.EditSOIndex = -1;
                } else {
                    objSOProduct.Status = 1;
                    $scope.objShippingOrder.ProductDetails.push(objSOProduct);
                }
                //$scope.CreateUpdate($scope.objShippingOrder);
                $scope.objSOProduct = {
                    ShippingOrdId: 0,
                    ProductId: '',
                    ProductName: '',
                    Discription: '',
                    Status: 0
                }
                $scope.objShippingOrder.ProductDescriptionData.Display = ''
                $scope.objShippingOrder.ProductDescriptionData.Value = ''
            }
        }
        $scope.DeleteProduct = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objShippingOrder.ProductDetails[index] = data;
                } else {
                    $scope.objShippingOrder.ProductDetails.splice(index, 1);
                }
                toastr.success("Product detail Delete", "Success");
            })
        }
        $scope.EditProduct = function (data, index) {
            $scope.EditSOIndex = index;
            $scope.objSOProduct = {
                ShippingOrdId: data.ShippingOrdId,
                ProductId: data.ProductId,
                ProductName: data.ProductName,
                Discription: data.Discription,
                Status: data.Status
            }
            $scope.objShippingOrder.ProductDescriptionData.Display = data.ProductName
            $scope.objShippingOrder.ProductDescriptionData.Value = data.ProductId
        }


        $scope.Add = function () {
            window.location.href = "/Operation/ShippingOrder/AddShippingOrder";
        }
        function ResetForm() {
            $scope.objShippingOrder = {
                ShippingOrdId: 0,
                TypeofShipment: '',
                Commodity: '',
                Nooftotal: '',
                NoofBL: '',
                CPBuyerName: '',
                CPBuyerAddress: '',
                CPBuyerTelephone: '',
                CPBuyerFax: '',
                CPBuyerContactPerson: '',
                EDBuyerName: '',
                EDBuyerAddress: '',
                EDBuyerTelephone: '',
                EDBuyerContactPerson: '',
                Freight: '',
                POL: '',
                POD: '',
                ProductDescription: '',
                ShippingMarksNumber: '',
                TotalNOPkgs: '',
                TotalGross: '',
                Measurement: '',
                Shipmentterms: '',
                CompanyName: '',
                CPBuyerNameData: { Display: '', Value: '' },
                CPBuyerAddressData: { Display: '', Value: '' },
                CPBuyerTelephoneData: { Display: '', Value: '' },
                CPBuyerFaxData: { Display: '', Value: '' },
                CPBuyerContactPersonData: { Display: '', Value: '' },
                EDBuyerNameData: { Display: '', Value: '' },
                EDBuyerAddressData: { Display: '', Value: '' },
                EDBuyerTelephoneData: { Display: '', Value: '' },
                EDBuyerContactPersonData: { Display: '', Value: '' },
                POLData: { Display: '', Value: '' },
                PODData: { Display: '', Value: '' },
                ProductDescriptionData: { Display: '', Value: '' },
                ShipmenttermsData: { Display: '', Value: '' },
                ProductDetails: []
            };

            if ($scope.FormShippingOrderInfo)
                $scope.FormShippingOrderInfo.$setPristine();
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
                $scope.objShippingOrder = angular.copy($scope.storage);
            } else {
                ResetForm();
                $scope.SetShippingOrderId(0);
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
        $scope.$watch('objShippingOrder.CPBuyerNameData', function (data) {
            if (data.Display != $scope.objShippingOrder.CPBuyerName.toString()) {
                $scope.objShippingOrder.CPBuyerAddressData.Display = '';
                $scope.objShippingOrder.CPBuyerAddressData.Value = '';
                $scope.objShippingOrder.CPBuyerContactPersonData.Display = '';
                $scope.objShippingOrder.CPBuyerContactPersonData.Value = '';
                $scope.CPBuyerTelephone = [];
                $scope.CPBuyerFax = [];
            }
        }, true)
        $scope.$watch('objShippingOrder.EDBuyerNameData', function (data) {
            if (data.Display != $scope.objShippingOrder.EDBuyerName.toString()) {
                $scope.objShippingOrder.EDBuyerAddressData.Display = '';
                $scope.objShippingOrder.EDBuyerAddressData.Value = '';
                $scope.objShippingOrder.EDBuyerContactPersonData.Display = '';
                $scope.objShippingOrder.EDBuyerContactPersonData.Value = '';
                $scope.EDBuyerTelephone = [];
            }
        }, true)

        $scope.$watch('objShippingOrder.CPBuyerAddressData', function (data) {
            if (data != undefined) {
                if (data.Value != '') {
                    ShippingOrderService.GetBuyerAddressInfoById(parseInt(data.Value)).then(function (result) {
                        if (result.data.ResponseType == 1) {
                            $scope.CPBuyerTelephone = (result.data.DataList.objBuyerAddressDetail.Telephone != '') ? result.data.DataList.objBuyerAddressDetail.Telephone.split(",") : [];
                            $scope.CPBuyerFax = '';

                            //$scope.objPurchaseOrder.CPBuyerTelephone = $scope.CPBuyerTelephone.toString();
                            //$scope.objPurchaseOrder.CPBuyerFax = $scope.CPBuyerFax.toString();
                        } else if (result.ResponseType == 3) {
                            toastr.error(result.data.Message, 'Opps, Something went wrong');
                        }
                    }, function (errorMsg) {
                        toastr.error(errorMsg, 'Opps, Something went wrong');
                    })
                }
            }
        }, true)

        $scope.$watch('objShippingOrder.EDBuyerAddressData', function (data) {
            if (data != undefined) {
                if (data.Value != '') {
                    ShippingOrderService.GetBuyerAddressInfoById(parseInt(data.Value)).then(function (result) {
                        if (result.data.ResponseType == 1) {
                            $scope.EDBuyerTelephone = (result.data.DataList.objBuyerAddressDetail.Telephone != '') ? result.data.DataList.objBuyerAddressDetail.Telephone.split(",") : [];
                            //$scope.objPurchaseOrder.EDBuyerTelephone = $scope.EDBuyerTelephone.toString();
                        } else if (result.ResponseType == 3) {
                            toastr.error(result.data.Message, 'Opps, Something went wrong');
                        }
                    }, function (errorMsg) {
                        toastr.error(errorMsg, 'Opps, Something went wrong');
                    })
                }
            }
        }, true)

        $scope.CreateUpdate = function (data) {
            //$scope.CPBuyerTelephone = ($scope.CPBuyerTelephone != '') ? $scope.CPBuyerTelephone.split(",") : [];
            //$scope.CPBuyerFax = ($scope.CPBuyerFax != '') ? $scope.CPBuyerFax.split(",") : [];
            //$scope.EDBuyerTelephone = ($scope.EDBuyerTelephone != '') ? $scope.EDBuyerTelephone.split(",") : [];

            var Pdetails = data.ProductDetails.map(function (item) {
                return item['ProductId'] + '-' + item['ProductName'];
            });
            data.ProductDescription = Pdetails.join("|");
            //var Pdetails = '';
            //if (data.ProductDetails.length > 0) {
            //    angular.forEach(data.ProductDetails, function (value) {
            //        Pdetails += value.ProductId + '-' + value.ProductName;
            //    }, true);
            //}

            var objShippingOrder = {
                ShippingOrdId: data.ShippingOrdId,
                TypeofShipment: data.TypeofShipment,
                Commodity: data.Commodity,
                Nooftotal: data.Nooftotal,
                NoofBL: data.NoofBL,
                CPBuyerName: data.CPBuyerNameData.Display,
                CPBuyerAddress: data.CPBuyerAddressData.Display,
                CPBuyerTelephone: $scope.CPBuyerTelephone.toString(),
                CPBuyerFax: $scope.CPBuyerFax.toString(),
                CPBuyerContactPerson: data.CPBuyerContactPersonData.Display,
                EDBuyerName: data.EDBuyerNameData.Display,
                EDBuyerAddress: data.EDBuyerAddressData.Display,
                EDBuyerTelephone: $scope.EDBuyerTelephone.toString(),
                EDBuyerContactPerson: data.EDBuyerContactPersonData.Display,
                Freight: data.Freight,
                POL: data.POLData.Display,
                POD: data.PODData.Display,
                ProductDescription: data.ProductDescription,
                ShippingMarksNumber: data.ShippingMarksNumber,
                TotalNOPkgs: data.TotalNOPkgs,
                TotalGross: data.TotalGross,
                Measurement: data.Measurement,
                Shipmentterms: data.ShipmenttermsData.Display,
                CompanyName: data.CompanyNameData.Display
                //CPBuyerNameData: { Display: data.CPBuyerName, Value: 0 },
                //CPBuyerAddressData: { Display: data.CPBuyerAddress, Value: 0 },
                //CPBuyerTelephoneData: { Display: data.CPBuyerTelephone, Value: 0 },
                //CPBuyerFaxData: { Display: data.CPBuyerFax, Value: 0 },
                //EDBuyerNameData: { Display: data.EDBuyerName, Value: 0 },
                //EDBuyerAddressData: { Display: data.EDBuyerAddress, Value: 0 },
                //EDBuyerTelephoneData: { Display: data.EDBuyerTelephone, Value: 0 },
                //POLData: { Display: data.POL, Value: 0 },
                //PODData: { Display: data.POD, Value: 0 },
                //ProductDescriptionData: { Display: data.ProductDescription, Value: 0 },
                //ShipmenttermsData: { Display: data.Shipmentterms, Value: 0 }
            };

            ShippingOrderService.CreateUpdateShippingOrder(objShippingOrder).then(function (result) {
                if (result.data.ResponseType == 1) {
                    ResetForm();
                    window.location.href = "/Operation/ShippingOrder";
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
        //ShippingOrdId, TypeofShipment, Commodity, Nooftotal, NoofBL, CPBuyerName, CPBuyerAddress, CPBuyerTelephone, CPBuyerFax, CPBuyerContactPerson, EDBuyerName,
        //EDBuyerAddress, EDBuyerTelephone, EDBuyerContactPerson, Freight, POL, POD, ProductDescription, ShippingMarksNumber, TotalNOPkgs, TotalGross, Measurement, Shipmentterms, CompanyName
        $scope.gridObj = {
            columnsInfo: [
               //{ "title": "Shipping Order Id", "data": "ShippingOrdId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", filter: false, show: true },
               { "title": "Type of Shipment", "field": "TypeofShipment", sortable: "TypeofShipment", filter: { TypeofShipment: "text" }, show: true },
               { "title": "Commodity", "field": "Commodity", sortable: "Commodity", filter: { Commodity: "text" }, show: true },
               { "title": "No of total", "field": "Nooftotal", sortable: "Nooftotal", filter: { Nooftotal: "text" }, show: true },
               { "title": "No of BL", "field": "NoofBL", sortable: "NoofBL", filter: { NoofBL: "text" }, show: false },
               { "title": "Cargo Pickup Name", "field": "CPBuyerName", sortable: "CPBuyerName", filter: { CPBuyerName: "text" }, show: false },
               { "title": "Cargo Pickup Address", "field": "CPBuyerAddress", sortable: "CPBuyerAddress", filter: { CPBuyerAddress: "text" }, show: false },
               { "title": "Cargo Pickup Telephone", "field": "CPBuyerTelephone", sortable: "CPBuyerTelephone", filter: { CPBuyerTelephone: "text" }, show: false },
               { "title": "Cargo Pickup Fax", "field": "CPBuyerFax", sortable: "CPBuyerFax", filter: { CPBuyerFax: "text" }, show: false },
               { "title": "Cargo Pickup ContactPerson", "field": "CPBuyerContactPerson", sortable: "CPBuyerContactPerson", filter: { CPBuyerContactPerson: "text" }, show: false },
               { "title": "Export Document Name", "field": "EDBuyerName", sortable: "EDBuyerName", filter: { EDBuyerName: "text" }, show: false },
               { "title": "Export Document Address", "field": "EDBuyerAddress", sortable: "EDBuyerAddress", filter: { EDBuyerAddress: "text" }, show: false },
               { "title": "Export Document Telephone", "field": "EDBuyerTelephone", sortable: "EDBuyerTelephone", filter: { EDBuyerTelephone: "text" }, show: false },
               { "title": "Export Document ContactPerson", "field": "EDBuyerContactPerson", sortable: "EDBuyerContactPerson", filter: { EDBuyerContactPerson: "text" }, show: false },
               { "title": "Freight", "field": "Freight", sortable: "Freight", filter: { Freight: "text" }, show: false },
               { "title": "Port Of Loading", "field": "POL", sortable: "POL", filter: { POL: "text" }, show: false },
               { "title": "Port Of Discharge", "field": "POD", sortable: "POD", filter: { POD: "text" }, show: false },
               { "title": "ProductDescription", "field": "ProductDescription", sortable: "ProductDescription", filter: { ProductDescription: "text" }, show: false },
               { "title": "Shipping Marks Number", "field": "ShippingMarksNumber", sortable: "ShippingMarksNumber", filter: { ShippingMarksNumber: "text" }, show: false },
               { "title": "Total NO Pkgs", "field": "TotalNOPkgs", sortable: "TotalNOPkgs", filter: { TotalNOPkgs: "text" }, show: false },
               { "title": "Total Gross", "field": "TotalGross", sortable: "TotalGross", filter: { TotalGross: "text" }, show: false },
               { "title": "Measurement", "field": "Measurement", sortable: "Measurement", filter: { Measurement: "text" }, show: false },
               { "title": "Company Name", "field": "CompanyName", sortable: "CompanyName", filter: { CompanyName: "text" }, show: false },

               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row){
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.ShippingOrdId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                         //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.$parent.$parent.$parent.Delete(row.ShippingOrdId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                         '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.ShippingOrdId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>' +
                         '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Print(row.ShippingOrdId)" data-uib-tooltip="Print"><i class="fa fa-print" ></i></a>'
                       return $scope.getHtml(element);
                   }
                   }
            ],
            Sort: { 'ShippingOrdId': 'asc' }
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
            window.location.href = "/Operation/ShippingOrder/AddShippingOrder/" + id + "/" + 0;
        }
        $scope.View = function (id) {
            window.location.href = "/Operation/ShippingOrder/AddShippingOrder/" + id + "/" + 1;
        }

        $scope.Delete = function (id) {
            ShippingOrderService.DeleteShippingOrder(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    $scope.refreshGrid();
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            })
        }
        $scope.Print = function (id) {
            ShippingOrderService.PrintShippingOrder(id).then(function (result) {
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
                    doc.save('ShippingOrder.pdf')
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