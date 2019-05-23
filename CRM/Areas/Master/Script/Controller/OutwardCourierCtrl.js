(function () {

    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("OutwardCourierCtrl", [
         "$scope", "$rootScope", "$timeout", "$filter", "OutwardCourierService", "Upload", "CityService",
         OutwardCourierCtrl
        ]);

    function OutwardCourierCtrl($scope, $rootScope, $timeout, $filter, OutwardCourierService, Upload, CityService) {

        $scope.objOutwardCourier = $scope.objOutwardCourier || {};
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};

        $scope.objOutwardCourier = {
            CourierId: 0,
            CourierDate: new Date(),
            CourierTime: new Date(),
            CountryData: { Display: '', Value: '' },
            StateData: { Display: '', Value: '' },
            CityData: { Display: '', Value: '' },
            AreaData: { Display: '', Value: '' },
            VendorData: { Display: '', Value: '' },
            ReceiverData: { Display: '', Value: '' },
            SenderData: { Display: '', Value: '' },
            ReceiverAddressData: { Display: '', Value: '' },
            ReceiverId: 0,
            ReceiverType: "",
            ShipmentRefNo: "",
            Amount: "",
            PaymentBy: "",
            Remark: "",
            POD: "",
            ShipmentPhoto: "",
            ReceiverAddressId: 0,
            ReceiverAddress: '',
            ReceiverCity: '',
            ReceiverCountry: '',
            ReceiverState: '',
            CourierReffNo: '',
            CourierTypeData: {
                Display: "",
                Value: ""
            },
            CurrencyData: {
                Display: "",
                Value: ""
            }
        };

        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };

        $scope.gridObj = {
            columnsInfo: [
               //{ "title": "Courier Id", "data": "CourierId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "Courier Ref. No", "field": "CourierReffNo", sortable: "CourierReffNo", filter: { CourierReffNo: "text" }, show: true, },
               { "title": "Month", "field": "MonthYear", sortable: "MonthYear", filter: { MonthYear: "text" }, show: true, },
               {
                   "title": "Courier Sent Date & Time", "field": "CourierDate", sortable: "CourierDate", filter: { CourierDate: "date" }, show: true,
                   //"render": '<p ng-bind="ConvertDate(row.CourierDate,\'dd/mm/yyyy\')">'
                   'cellTemplte': function ($scope, row) {
                       var element = '<span >{{ConvertDate(row.CourierDate,\'dd-mm-yyyy\') +","+ ConvertTime(row.CourierTime)}}</span>'
                       return $scope.getHtml(element);
                   }
               },
               { "title": "Receiver", "field": "Receiver", sortable: "Receiver", filter: { Receiver: "text" }, show: true, },//Receiver
               { "title": "Courier Company Name", "field": "Vendor", sortable: "Vendor", filter: { Vendor: "text" }, show: true, },//Vendor
               { "title": "Courier Type", "field": "CourierType", ssortable: "CourierType", filter: { CourierType: "text" }, show: true, },
               { "title": "POD No", "field": "ShipmentRefNo", sortable: "ShipmentRefNo", filter: { ShipmentRefNo: "text" }, show: true, },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a   class="btn btn-primary btn-xs" data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.CourierId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                             //'<a class="btn btn-danger btn-xs"  data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.CourierId)" data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                             '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.CourierId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }
               }

               //{
               //    "title": "Courier Date", "data": "CourierDate", sort: true, filter: true, dataType: "ng-table/filters/datefilter1.html",
               //    "render": '<p ng-bind="ConvertDate(row.CourierDate,\'dd/mm/yyyy\')">'
               //},
                //{ "title": "Area Id", "data": "AreaId", filter: false, visible: false },
               //{ "title": "Area Name", "data": "AreaName", sort: true, filter: true },
               //{
               //    "title": "Courier Time", "field": "CourierTime", sortable: "CourierTime", filter: { CourierTime: "text" }, show: true,
               //    'cellTemplte': function ($scope, row) {
               //        var element = '<p ng-bind="ConvertTime(row.CourierTime)">'
               //        return $scope.getHtml(element);
               //    }
               //},
               //{ "title": "Receiver Id", "data": "ReceiverId", sortable: "PaymentMode", filter: { PaymentMode: "text" }, show: false, },

               //{ "title": "Vendor Id", "data": "VendorId", sortable: "PaymentMode", filter: { PaymentMode: "text" }, show: false, },

            //{ "title": "Sender", "field": "Sender", sortable: "Sender", filter: { Sender: "text" }, show: false, },
            //{ "title": "Amount", "field": "Amount", sortable: "Amount", filter: { Amount: "text" }, show: false, },
            //   { "title": "PaymentBy", "field": "PaymentBy", sortable: "PaymentBy", filter: { PaymentBy: "text" }, show: false, },
            //   //{ "title": "Remark", "data": "Remark", sort: true, filter: true },

            //   {
            //       "title": "Shipment Remark", "field": "Remark", sortable: "Remark", filter: { Remark: "text" }, show: false,
            //       'cellTemplte': function ($scope, row) {
            //           var element = '<p data-uib-tooltip="{{row.Remark}}" ng-bind="LimitString(row.Remark,100)">'
            //           return $scope.getHtml(element);
            //       }
            //   },

            ],
            Sort: { 'BuyerId': 'asc' }
        }

        $scope.SetOutwardCourierId = function (id, isdisable) {
            if (id > 0) {
                $scope.SrNo = id;
                $scope.addMode = false;
                $scope.saveText = "Update";
                $scope.GetAllOutwardCourierById(id);
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

        $scope.GetInvoice = function () {
            OutwardCourierService.OutwardCourierInfo().then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objOutwardCourier.CourierReffNo = result.data.DataList.CourierReffNo;
                } else if (result.ResponseType == 3) {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (error) {
                $rootScope.errorHandler(error)
            })
        }

        $scope.GetAllOutwardCourierById = function (id) {
            OutwardCourierService.FetchAllInfoById(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    var objOutwardCourierMaster = result.data.DataList.objOutwardCourierMaster;
                    var d = new Date();
                    d.setHours($filter('date')(objOutwardCourierMaster.CourierTime, "HH:mm").Hours);
                    d.setMinutes($filter('date')(objOutwardCourierMaster.CourierTime, "HH:mm").Minutes);
                    var mytime = d;
                    $scope.objOutwardCourier = {
                        CourierId: objOutwardCourierMaster.CourierId,
                        CourierDate: $filter('mydate')(objOutwardCourierMaster.CourierDate),
                        CourierTime: mytime,
                        CityId: objOutwardCourierMaster.CityId,
                        CityName: objOutwardCourierMaster.CityName,
                        CountryId: objOutwardCourierMaster.CountryId,
                        CountryName: objOutwardCourierMaster.CountryName,
                        StateId: objOutwardCourierMaster.StateId,
                        StateName: objOutwardCourierMaster.StateName,
                        CountryData: {
                            Display: objOutwardCourierMaster.CountryName,
                            Value: objOutwardCourierMaster.CountryId
                        },
                        StateData: {
                            Display: objOutwardCourierMaster.StateName,
                            Value: objOutwardCourierMaster.StateId
                        },
                        CityData: {
                            Display: objOutwardCourierMaster.CityName,
                            Value: objOutwardCourierMaster.CityId
                        },
                        AreaData: {
                            Display: objOutwardCourierMaster.AreaName,
                            Value: objOutwardCourierMaster.AreaId
                        },
                        VendorData: {
                            Display: objOutwardCourierMaster.Vendor,
                            Value: objOutwardCourierMaster.VendorId
                        },
                        ReceiverData: {
                            Display: objOutwardCourierMaster.Receiver,
                            Value: objOutwardCourierMaster.ReceiverId
                        },
                        SenderData: {
                            Display: objOutwardCourierMaster.Sender,
                            Value: objOutwardCourierMaster.SenderBy
                        },
                        ReceiverAddressData: {
                            Display: objOutwardCourierMaster.ReceiverAddress,
                            Value: objOutwardCourierMaster.ReceiverAddressId
                        },
                        CourierReffNo: objOutwardCourierMaster.CourierReffNo,
                        CourierTypeData: {
                            Display: objOutwardCourierMaster.CourierType,
                            Value: objOutwardCourierMaster.CourierTypeId
                        },
                        CurrencyData: {
                            Display: objOutwardCourierMaster.CurrencyName,
                            Value: objOutwardCourierMaster.CurrencyId
                        },
                        ReceiverAddressId: objOutwardCourierMaster.ReceiverAddressId,
                        ReceiverId: objOutwardCourierMaster.ReceiverId,
                        ReceiverAddress: objOutwardCourierMaster.ReceiverAddress,
                        ReceiverType: objOutwardCourierMaster.ReceiverType,
                        ShipmentRefNo: objOutwardCourierMaster.ShipmentRefNo,
                        Amount: objOutwardCourierMaster.Amount,
                        PaymentBy: objOutwardCourierMaster.PaymentBy,
                        Remark: objOutwardCourierMaster.Remark,
                        POD: "/UploadImages/OutwardPOD/" + objOutwardCourierMaster.POD,
                        ShipmentPhoto: "/UploadImages/OutwardPOD/" + objOutwardCourierMaster.ShipmentPhoto
                    };

                    $scope.tempImagePath = "/UploadImages/OutwardPOD/" + objOutwardCourierMaster.POD;
                    $scope.tempImagePathShipment = "/UploadImages/OutwardPOD/" + objOutwardCourierMaster.ShipmentPhoto;

                    $scope.storage = angular.copy($scope.objOutwardCourier);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            })
        }

        $scope.Add = function () {
            window.location.href = "/master/OutwardCourier/AddCourier";
        }

        $scope.SetValue = function (value, type) {
            $scope.ReceiverMode = value;
            $scope.objOutwardCourier.ReceiverType = type;
            $scope.objOutwardCourier.ReceiverData = {
                Display: "",
                Value: ""
            };
        }

        function ResetForm() {
            $scope.objOutwardCourier = {
                CourierId: 0,
                CourierDate: new Date(),
                CourierTime: new Date(),
                CountryData: { Display: '', Value: '' },
                StateData: { Display: '', Value: '' },
                CityData: { Display: '', Value: '' },
                AreaData: { Display: '', Value: '' },
                VendorData: { Display: '', Value: '' },
                ReceiverData: { Display: '', Value: '' },
                SenderData: { Display: '', Value: '' },
                ReceiverAddressData: { Display: '', Value: '' },
                ReceiverId: 0,
                ReceiverType: "",
                ShipmentRefNo: "",
                Amount: "",
                PaymentBy: "",
                Remark: "",
                CountryId: 0,
                StateId: 0,
                ReceiverAddressId: 0,
                ReceiverAddress: '',
                ReceiverCity: '',
                ReceiverCountry: '',
                ReceiverState: '',
                CourierReffNo: '',
                CourierTypeData: {
                    Display: "",
                    Value: ""
                },
                CurrencyData: {
                    Display: "",
                    Value: ""
                },
                POD: '',
                ShipmentPhoto: ''
            };
            if ($scope.FormOutwardCourierInfo)
                $scope.FormOutwardCourierInfo.$setPristine();
            $scope.addMode = true;
            $scope.isFirstFocus = false;
            $scope.storage = {};
            $timeout(function () {
                $scope.isFirstFocus = true;
            });
        }
        ResetForm();

        $scope.Reset = function () {
            if ($scope.addMode == false) {
                $scope.objOutwardCourier = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.$watch('objOutwardCourier.ReceiverData', function (data) {
            if (data) {
                if (data.Value != $scope.objOutwardCourier.ReceiverId.toString()) {
                    $scope.objOutwardCourier.ReceiverAddressData.Display = '';
                    $scope.objOutwardCourier.ReceiverAddressData.Value = '';
                }
            }
        }, true)

        //$scope.$watch('objOutwardCourier.CountryData', function (data) {
        //    //if (data.Value != '') {
        //    if (data.Value != '') {
        //        if (data.Value != $scope.objOutwardCourier.CountryId.toString()) {
        //            $scope.objOutwardCourier.StateData.Display = '';
        //            $scope.objOutwardCourier.StateData.Value = '';
        //            $scope.objOutwardCourier.CityData.Display = '';
        //            $scope.objOutwardCourier.CityData.Value = '';
        //        }
        //    }
        //}, true)

        //$scope.$watch('objOutwardCourier.StateData', function (data) {
        //    if ($scope.objOutwardCourier.StateId != undefined) {
        //        if (data.Value != $scope.objOutwardCourier.StateId.toString()) {
        //            $scope.objOutwardCourier.CityData.Display = '';
        //            $scope.objOutwardCourier.CityData.Value = '';
        //        }
        //    }
        //}, true)
        $scope.$watch('objOutwardCourier.ReceiverAddressData', function (data) {
            if (data) {
                if (data.Value != $scope.objOutwardCourier.ReceiverAddressId.toString()) {
                    $scope.objOutwardCourier.CountryData.Display = '';
                    $scope.objOutwardCourier.CountryData.Value = '';
                    $scope.objOutwardCourier.StateData.Display = '';
                    $scope.objOutwardCourier.StateData.Value = '';
                    $scope.objOutwardCourier.CityData.Display = '';
                    $scope.objOutwardCourier.CityData.Value = '';
                }
                OutwardCourierService.FetchAddressById($scope.objOutwardCourier.ReceiverData.Value, $scope.objOutwardCourier.ReceiverAddressData.Value).then(function (result) {
                    if (result) {
                        if (result.data.ResponseType == 1) {
                            var list = result.data.DataList;
                            $scope.objOutwardCourier.CountryData.Display = list.CountryName;
                            $scope.objOutwardCourier.CountryData.Value = list.CountryId;
                            $scope.objOutwardCourier.StateData.Display = list.StateName;
                            $scope.objOutwardCourier.StateData.Value = list.StateId;
                            $scope.objOutwardCourier.CityData.Display = list.CityName;
                            $scope.objOutwardCourier.CityData.Value = list.CityId;
                        }
                        else if (result.data.ResponseType == 3) {
                            toastr.error(result.data.Message, 'Opps, Something went wrong');
                        }
                    }
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            } else {

                $scope.objOutwardCourier.CountryData.Display = '';
                $scope.objOutwardCourier.CountryData.Value = '';
                $scope.objOutwardCourier.StateData.Display = '';
                $scope.objOutwardCourier.StateData.Value = '';
                $scope.objOutwardCourier.CityData.Display = '';
                $scope.objOutwardCourier.CityData.Value = '';
            }
        }, true)

        $scope.CountryBind = function (data) {
            CityService.CountryBind().then(function (result) {
                if (result.data.ResponseType == 1) {
                    _.each(result.data.DataList, function (value, key, list) {
                        if (value.CountryName.toString().toLowerCase() == data.toString().toLowerCase() && $scope.objOutwardCourier.CountryData.Value == '') {
                            $scope.objOutwardCourier.CountryData = {
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

        //id  : 1 = POD, 2= Shipment Photo
        $scope.uploadImgFile = function (file, id) {
            if (id == 1) {
                $scope.tempImagePath = '';
                $scope.objOutwardCourier.POD = '';
                Upload.upload({
                    url: "/Handler/FileUpload.ashx",
                    method: 'POST',
                    file: file,
                }).then(function (result) {
                    if (result.config.file[0].type.match('application/pdf')) {

                        if (result.status == 200) {
                            if (result.data.length > 0) {
                                $scope.objOutwardCourier.POD = result.data[0].imageName;
                                $scope.tempImagePath = result.data[0].imagePath;
                            }
                        }
                        else {
                            $scope.objOutwardCourier.POD = '';
                        }
                    } else {
                        toastr.error("Only PDF File Allowed.", "Error");
                    }

                });
            } else {
                $scope.tempImagePathShipment = '';
                $scope.objOutwardCourier.ShipmentPhoto = '';
                Upload.upload({
                    url: "/Handler/FileUpload.ashx",
                    method: 'POST',
                    file: file,
                }).then(function (result) {
                    if (result.config.file[0].type.match('image.*')) {
                        if (result.status == 200) {
                            if (result.data.length > 0) {
                                $scope.objOutwardCourier.ShipmentPhoto = result.data[0].imageName;
                                $scope.tempImagePathShipment = result.data[0].imagePath;
                            }
                        }
                        else {
                            $scope.objOutwardCourier.ShipmentPhoto = '';
                        }
                    } else {
                        toastr.error("Only Image File Allowed.", "Error");
                    }
                });
            }
        }

        $scope.CreateUpdate = function (data) {
            var ObjData = {
                CourierId: data.CourierId,
                CourierDate: data.CourierDate,
                CourierTime: $filter('date')(data.CourierTime, "HH:mm"),
                AreaId: (data.AreaData.Value == "") ? 0 : data.AreaData.Value,
                AreaName: data.AreaData.Display,
                ReceiverAddressId: data.ReceiverAddressData.Value,
                CityId: data.CityData.Value,
                ReceiverCity: data.CityData.Display,
                StateId: data.StateData.Value,
                ReceiverState: data.StateData.Display,
                CountryId: data.CountryData.Value,
                ReceiverCountry: data.CountryData.Display,
                VendorId: data.VendorData.Value,
                Vendor: data.VendorData.Display,
                ReceiverCompanyName: data.VendorData.Display,
                ReceiverId: data.ReceiverData.Value,
                Receiver: data.ReceiverData.Display,
                ReceiverType: data.ReceiverType,
                SenderBy: data.SenderData.Value,
                Sender: data.SenderData.Display,
                ShipmentRefNo: data.ShipmentRefNo,
                Amount: data.Amount,
                PaymentBy: data.PaymentBy,
                Remark: data.Remark,
                POD: data.POD,
                ShipmentPhoto: data.ShipmentPhoto,
                CourierReffNo: data.CourierReffNo,
                ReceiverAddress: data.ReceiverAddressData.Display,
                ReceiverCity: data.CityData.Display,
                ReceiverCountry: data.CountryData.Display,
                ReceiverState: data.StateData.Display,
                CourierType: data.CourierTypeData.Display,
                CourierTypeId: data.CourierTypeData.Value,
                CurrencyName: data.CurrencyData.Display,
                CurrencyId: data.CurrencyData.Value,
            }

            if (ObjData.POD) {
                var dataPOD = ObjData.POD.split('/');
                ObjData.POD = dataPOD[dataPOD.length - 1];
                //data.ProductPhotoMasters[index].Photo = value.Photo.substring((value.Photo.length - 19), (value.Photo.length));
            }
            if (ObjData.ShipmentPhoto) {
                var dataShipmentPhoto = ObjData.ShipmentPhoto.split('/');
                ObjData.ShipmentPhoto = dataShipmentPhoto[dataShipmentPhoto.length - 1];
            }

            OutwardCourierService.CreateUpdate(ObjData).then(function (result) {
                if (result.data.ResponseType == 1) {
                    ResetForm();
                    toastr.success(result.data.Message);
                    //if (data.CourierId > 0) {
                    window.location.href = "/master/OutwardCourier";
                    //}
                    $scope.refreshGrid();
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })

        }

        $scope.Edit = function (id) {
            window.location.href = "/Master/OutwardCourier/AddCourier/" + id + "/" + 0;
        }

        $scope.View = function (id) {
            window.location.href = "/Master/OutwardCourier/AddCourier/" + id + "/" + 1;
        }

        $scope.Delete = function (id) {
            OutwardCourierService.Delete(id).then(function (result) {
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

        $scope.dateOptions = {
            formatYear: 'dd-MM-yyyy',
            //minDate: new Date(2016, 8, 5),
            //maxDate: new Date(),
            startingDay: 1
        };
    }
})()