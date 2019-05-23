(function () {

    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("InwardCourierCtrl", [
       "$scope", "$rootScope", "$timeout", "$filter", "InwardCourierService", "Upload",
       InwardCourierCtrl
      ]);
    function InwardCourierCtrl($scope, $rootScope, $timeout, $filter, InwardCourierService, Upload) {

        $scope.objInwardCourier = $scope.objInwardCourier || {};

        $scope.objInwardCourier = {
            CourierId: 0,
            CourierDate: new Date(),
            CourierTime: new Date(),
            CountryData: { Display: '', Value: '' },
            StateData: { Display: '', Value: '' },
            CityData: { Display: '', Value: '' },
            AreaData: { Display: '', Value: '' },
            ShipmentRefNo: '',
            POD: '',
            ShipmentPhoto: '',
            VendorName: '',
            SenderData: {
                Display: "",
                Value: ""
            },
            SenderType: "",
            VendorData: {
                Display: "",
                Value: ""
            },
            ShipmentRemark: '',
            ReceiveData: {
                Display: "",
                Value: ""
            },
            CourierReffNo: '',
            CourierTypeData: {
                Display: "",
                Value: ""
            }
        };

        $scope.Add = function (data) {
            //var modalInstance = $uibModal.open({
            //    templateUrl: 'InwardCourierModal.html',
            //    controller: ModalInstanceCtrl,
            //    resolve: {
            //        InwardCourierCtrl: function () { return $scope },
            //        InwardCourierService: function () { return InwardCourierService; },
            //        data: function () { return data; }
            //    }
            //});
            //modalInstance.result.then(function () {
            //    $scope.refreshGrid();
            //}, function () {
            //});
            window.location.href = "/master/InwardCourier/AddCourier";
        }

        $scope.SetValue = function (value, type) {
            $scope.SenderMode = value;
            $scope.objInwardCourier.SenderType = type;
            $scope.objInwardCourier.SenderData = {
                Display: "",
                Value: ""
            };
        }

        $scope.gridObj = {
            columnsInfo: [
                  //{ "title": "CourierId", "data": "CourierId", filter: false, visible: false },
                  { "title": "Sr.", "field": "RowNumber", show: true, },
                  { "title": "Inward Courier Ref. No", "field": "CourierReffNo", sortable: "CourierReffNo", filter: { CourierReffNo: "text" }, show: true, },
                  { "title": "Month", "field": "MonthYear", sortable: "MonthYear", filter: { MonthYear: "text" }, show: true, },
                  {
                      "title": "Courier Received Date & Time", "field": "CourierDate", sortable: "CourierDate", filter: { CourierDate: "date" }, show: true,
                      //"render": '<p ng-bind="ConvertDate(row.CourierDate,\'dd/mm/yyyy\')">'
                      'cellTemplte': function ($scope, row) {
                          var element = '<span >{{ConvertDate(row.CourierDate,\'dd-mm-yyyy\') +","+ ConvertTime(row.CourierTime)}}</span>'
                          return $scope.getHtml(element);
                      }
                  },
                     //{ "title": "Area Id", "data": "AreaId", filter: false, visible: false },
                     //{ "title": "Area Name", "data": "AreaName", sort: true, filter: true },
                     //{ "title": "Courier Time", "data": "CourierTime", sort: true, filter: true, "render": '<p ng-bind="ConvertTime(row.CourierTime)">' },
                 { "title": "Sender", "field": "Sender", sortable: "Sender", filter: { Sender: "text" }, show: true, },
                 //{ "title": "VendorId", "data": "VendorId", sort: false, filter: false },
                 { "title": "Courier Company Name", "field": "Vendor", sortable: "Vendor", filter: { Vendor: "text" }, show: true, },

                 { "title": "Courier Type", "field": "CourierType", sortable: "CourierType", filter: { CourierType: "text" }, show: true, },
                 { "title": "Received by", "field": "Receive", sortable: "Receive", filter: { Receive: "text" }, show: true},
                 //{ "title": "Remark", "data": "Remark", sort: true, filter: true },
                 {
                     "title": "Shipment Remark", "field": "ShipmentRemark", sortable: "ShipmentRemark", filter: { ShipmentRemark: "text" }, show: false,
                     'cellTemplte': function ($scope, row) {
                         var element = '<p data-uib-tooltip="{{row.ShipmentRemark}}" ng-bind="LimitString(row.ShipmentRemark,100)">'
                         return $scope.getHtml(element);
                     }
                 },
                 {
                     "title": "Action", show: true,
                     'cellTemplte': function ($scope, row) {
                         var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.CourierId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                              //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.CourierId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                              '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.CourierId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                         return $scope.getHtml(element);
                     }
                 }
            ],
            Sort: { 'CourierId': 'asc' },
        }

        $scope.SetInwardCourierId = function (id, isdisable) {
            if (id > 0) {
                $scope.SrNo = id;
                $scope.addMode = false;
                $scope.saveText = "Update";
                $scope.GetAllInwardCourierById(id);
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
            InwardCourierService.InwardCourierInfo().then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objInwardCourier.CourierReffNo = result.data.DataList.CourierReffNo;
                } else if (result.ResponseType == 3) {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (error) {
                $rootScope.errorHandler(error)
            })
        }

        $scope.GetAllInwardCourierById = function (id) {
            InwardCourierService.FetchAllInfoById(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    var objInwardCourierMaster = result.data.DataList.objInwardCourierMaster;
                    var d = new Date();
                    d.setHours($filter('date')(objInwardCourierMaster.CourierTime, "HH:mm").Hours);
                    d.setMinutes($filter('date')(objInwardCourierMaster.CourierTime, "HH:mm").Minutes);
                    var mytime = d;
                    $scope.objInwardCourier = {
                        CourierId: objInwardCourierMaster.CourierId,
                        CourierDate: $filter('mydate')(objInwardCourierMaster.CourierDate),
                        CourierTime: mytime,
                        ShipmentRefNo: objInwardCourierMaster.ShipmentRefNo,
                        POD: "/UploadImages/InwardPOD/" + objInwardCourierMaster.POD,
                        ShipmentPhoto: "/UploadImages/InwardPOD/" + objInwardCourierMaster.ShipmentPhoto,
                        VendorName: objInwardCourierMaster.Vendor,
                        CountryData: {
                            Display: objInwardCourierMaster.CountryName,
                            Value: objInwardCourierMaster.CountryId
                        },
                        StateData: {
                            Display: objInwardCourierMaster.StateName,
                            Value: objInwardCourierMaster.StateId
                        },
                        CityData: {
                            Display: objInwardCourierMaster.CityName,
                            Value: objInwardCourierMaster.CityId
                        },
                        AreaData: {
                            Display: objInwardCourierMaster.AreaName,
                            Value: objInwardCourierMaster.AreaId
                        },
                        VendorData: {
                            Display: objInwardCourierMaster.Vendor,
                            Value: objInwardCourierMaster.VendorId
                        },
                        ReceiveData: {
                            Display: objInwardCourierMaster.Receiver,
                            Value: objInwardCourierMaster.ReceivedBy
                        },
                        SenderData: {
                            Display: objInwardCourierMaster.Sender,
                            Value: objInwardCourierMaster.SenderId
                        },
                        CourierReffNo: objInwardCourierMaster.CourierReffNo,
                        CourierTypeData: {
                            Display: objInwardCourierMaster.CourierType,
                            Value: objInwardCourierMaster.CourierTypeId
                        },
                        SenderType: objInwardCourierMaster.SenderType,
                        ShipmentRefNo: objInwardCourierMaster.ShipmentRefNo,
                        Amount: objInwardCourierMaster.Amount,
                        PaymentBy: objInwardCourierMaster.PaymentBy,
                        ShipmentRemark: objInwardCourierMaster.ShipmentRemark
                    };
                    $scope.tempImagePath = "/UploadImages/InwardPOD/" + objInwardCourierMaster.POD;
                    $scope.tempImagePathShipment = "/UploadImages/InwardPOD/" + objInwardCourierMaster.ShipmentPhoto;

                    $scope.storage = angular.copy($scope.objInwardCourier);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            })
        }

        $scope.Edit = function (id) {
            //var d = new Date();
            //d.setHours($filter('date')(data.CourierTime, "hh:mm").Hours);
            //d.setMinutes($filter('date')(data.CourierTime, "hh:mm").Minutes);
            //var mytime = d;
            //var objFinal = {
            //    CourierId: data.CourierId,
            //    CourierDate: $filter('mydate')(data.CourierDate),
            //    CourierTime: mytime,
            //    CountryData: {
            //        Display: data.CountryName,
            //        Value: data.CountryId
            //    },
            //    StateData: {
            //        Display: data.StateName,
            //        Value: data.StateId
            //    },
            //    CityData: {
            //        Display: data.CityName,
            //        Value: data.CityId
            //    },
            //    AreaData: {
            //        Display: data.AreaName,
            //        Value: data.AreaId
            //    },
            //    SenderData: {
            //        Display: data.Sender,
            //        Value: data.SenderId + "|" + data.SenderType
            //    },
            //    SenderType: data.SenderType,
            //    VendorData: {
            //        Display: data.Vendor,
            //        Value: data.VendorId
            //    },
            //    Remark: data.Remark,
            //    ReceiveData: {
            //        Display: data.Receive,
            //        Value: data.ReceivedBy
            //    }
            //};
            //$scope.Add(objFinal);
            window.location.href = "/Master/InwardCourier/AddCourier/" + id + "/" + 0;
        }

        $scope.View = function (id) {

            window.location.href = "/Master/InwardCourier/AddCourier/" + id + "/" + 1;
        }

        $scope.Delete = function (id) {
            InwardCourierService.Delete(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    $scope.refreshGrid()
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

        //var ModalInstanceCtrl = function ($scope, $uibModalInstance, InwardCourierCtrl, InwardCourierService, data, $filter) {
        //$scope.objInwardCourier = $scope.objInwardCourier || {};
        //$scope.addMode = true;
        //$scope.saveText = "Save";
        //$scope.timeZone = new Date().getTimezoneOffset().toString();
        //$scope.storage = {};

        //if (objInwardCourier.CourierId <= 0) {
        //    ResetForm()
        //} else {
        //    $scope.objInwardCourier = objInwardCourier;
        //    $scope.storage = angular.copy($scope.objInwardCourier);
        //}

        function ResetForm() {
            $scope.objInwardCourier = {
                CourierId: 0,
                CourierDate: new Date(),
                CourierTime: new Date(),
                CountryData: { Display: '', Value: '' },
                StateData: { Display: '', Value: '' },
                CityData: { Display: '', Value: '' },
                AreaData: { Display: '', Value: '' },
                ShipmentRefNo: '',
                POD: '',
                ShipmentPhoto: '',
                VendorName: '',
                SenderData: {
                    Display: "",
                    Value: ""
                },
                SenderType: "",
                VendorData: {
                    Display: "",
                    Value: ""
                },
                ShipmentRemark: '',
                ReceiveData: {
                    Display: "",
                    Value: ""
                },
                CourierReffNo: '',
                CourierTypeData: {
                    Display: "",
                    Value: ""
                }
            };
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.FormInwardCourier)
                $scope.FormInwardCourier.$setPristine();
        }

        $scope.CreateUpdate = function (data) {
            data.AreaId = (data.AreaData.Value == "") ? 0 : data.AreaData.Value;
            data.AreaName = data.AreaData.Display;
            data.CityId = data.CityData.Value;
            data.CityName = data.CityData.Display;
            data.StateId = data.StateData.Value;
            data.StateName = data.StateData.Display;
            data.CountryId = data.CountryData.Value;
            data.CountryName = data.CountryData.Display;
            data.SenderId = _.split(data.SenderData.Value, '|')[0];
            data.VendorId = data.VendorData.Value;
            data.SenderType = data.SenderType;
            data.CourierTime = $filter('date')(data.CourierTime, "HH:mm");
            data.ReceivedBy = data.ReceiveData.Value;
            data.ShipmentRefNo = data.ShipmentRefNo;
            data.CourierType = data.CourierTypeData.Display;
            data.CourierTypeId = data.CourierTypeData.Value;
            if (data.POD != undefined) {
                var dataPOD = data.POD.split('/');
                data.POD = dataPOD[dataPOD.length - 1];
                //data.ProductPhotoMasters[index].Photo = value.Photo.substring((value.Photo.length - 19), (value.Photo.length));
            }
            if (data.ShipmentPhoto) {
                var dataShipmentPhoto = data.ShipmentPhoto.split('/');
                data.ShipmentPhoto = dataShipmentPhoto[dataShipmentPhoto.length - 1];
            }


            //data.POD = data.POD;
            data.VendorName = data.VendorData.Display;
            InwardCourierService.CreateUpdate(data).then(function (result) {
                if (result.data.ResponseType == 1) {
                    //$uibModalInstance.close();
                    toastr.success(result.data.Message);
                    window.location.href = "/Master/InwardCourier";
                    //InwardCourierCtrl.refreshGrid();
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        //id : 1 :POD,
        //id : 2 :ShipmentPhoto
        $scope.uploadImgFile = function (file, id) {

            if (id == 1) {
                $scope.tempImagePath = '';
                $scope.objInwardCourier.POD = '';
                Upload.upload({
                    url: "/Handler/FileUpload.ashx",
                    method: 'POST',
                    file: file,
                }).then(function (result) {
                    if (result.config.file[0].type.match('application/pdf')) {
                        if (result.status == 200) {
                            if (result.data.length > 0) {
                                $scope.objInwardCourier.POD = result.data[0].imageName;
                                $scope.tempImagePath = result.data[0].imagePath;
                            }
                        }
                        else {
                            $scope.objInwardCourier.POD = '';
                        }
                    } else {
                        toastr.error("Only PDF File Allowed.", "Error");
                    }
                });
            } else if (id == 2) {

                $scope.tempImagePathShipment = '';
                $scope.objInwardCourier.ShipmentPhoto = '';
                Upload.upload({
                    url: "/Handler/FileUpload.ashx",
                    method: 'POST',
                    file: file,
                }).then(function (result) {
                    if (result.config.file[0].type.match('image.*')) {
                        if (result.status == 200) {
                            if (result.data.length > 0) {
                                $scope.objInwardCourier.ShipmentPhoto = result.data[0].imageName;
                                $scope.tempImagePathShipment = result.data[0].imagePath;
                            }
                        }
                        else {
                            $scope.objInwardCourier.ShipmentPhoto = '';
                        }
                    } else {
                        toastr.error("Only Image File Allowed.", "Error");
                    }
                });
            }
        }

        $scope.Reset = function () {
            if ($scope.objInwardCourier.CourierId > 0) {
                $scope.objInwardCourier = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

        $scope.dateOptions = {
            formatYear: 'dd-MM-yyyy',
            //minDate: new Date(2016, 8, 5),
            //maxDate: new Date(),
            startingDay: 1
        };

        //}
        //ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'InwardCourierCtrl', 'InwardCourierService', 'data', '$filter']
    }
})()


