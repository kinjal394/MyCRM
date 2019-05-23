
(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
            .controller("TOController", [
             "$scope", "$rootScope", "$timeout", "$filter", "TOService", "UploadProductDataService", "ProductService", "NgTableParams", "$uibModal",
             TOController]);

    function TOController($scope, $rootScope, $timeout, $filter, TOService, UploadProductDataService, ProductService, NgTableParams, $uibModal) {

        $scope.objTO = $scope.objTO || {};
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};
        $scope.objTO = {
            TOId: 0,
            InqId: 0,
            InqNo: '',
            ToTypeId: 0,
            TOType: '',
            Remark: '',
            BuyerName: '',
            TOBuyerData: { Display: '', Value: '' },
            TOInquiryData: { Display: '', Value: '' },
            TOTypeData: { Display: '', Value: '' },
            TOItemDetail: [],
            TOSpecification: [],
            ToPacking: []
        };
        $scope.objTOItem = {
            TOItemId: 0,
            TOId: 0,
            SpecId: '',
            ProductCode: '',
            ProductId: '',
            SubCategoryId: '',
            CategoryId: '',
            TechSpec: '',
            SpecValue: '',
            ProductName: '',
            SubCategoryName: '',
            CategoryName: '',
            Status: 0, //1 : Insert , 2:Update ,3 :Delete
            TOItemSepcData: { Display: '', Value: '' },
            TOItemProdData: { Display: '', Value: '' },
            TOItemSubCatData: { Display: '', Value: '' },
            TOItemCatData: { Display: '', Value: '' }
        };
        $scope.objTOSpecification = {
            TechDetailId: 0,
            TOItemId: 0,
            TOId: 0,
            ProductId: '',
            TechParaID: 0,
            TechParaVal: '',
            TechParaRequirment: '',
            TechSpecifType: '',
            TechSpec: '',
            SpecId: '',
            Status: 0, //1 : Insert , 2:Update ,3 :Delete
            TOSpecData: { Display: '', Value: '' },
            TechnicalHead: { Display: '', Value: '' }
        };
        $scope.ToPacking = {
            TechDetailId: 0,
            TOItemId: 0,
            TOId: 0,
            ProductId: '',
            TechParaID: 0,
            TechParaVal: '',
            TechParaRequirment: '',
            TechSpecifType: '',
            TechSpec: '',
            SpecId: '',
            Status: 0, //1 : Insert , 2:Update ,3 :Delete
            TOPackData: { Display: '', Value: '' }
        }

        $scope.SetTOId = function (id, isdisable) {
            if (id > 0) {
                $scope.SrNo = id;
                $scope.addMode = false;
                $scope.saveText = "Update";
                $scope.isClicked = false;
                if (isdisable == 1) {
                    $scope.isClicked = true;
                }
                $scope.GetAllTOInfoById(id);
            } else {
                $scope.SrNo = 0;
                $scope.addMode = true;
                $scope.isClicked = false;
                $scope.saveText = "Save";
            }
        }
        $scope.GetAllTOInfoById = function (id) {
            TOService.GetbyId(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    var objTOMaster = result.data.DataList.objTOMaster;
                    $scope.objTO = {
                        TOId: objTOMaster.TOId,
                        InqId: objTOMaster.InqId,
                        InqNo: objTOMaster.InqNo,
                        ToTypeId: objTOMaster.ToTypeId,
                        TOType: objTOMaster.TOType,
                        Remark: objTOMaster.Remark,
                        BuyerName: objTOMaster.BuyerName,
                        TOBuyerData: { Display: objTOMaster.BuyerName, Value: objTOMaster.BuyerName },
                        TOInquiryData: { Display: objTOMaster.InqNo, Value: objTOMaster.InqId },
                        TOTypeData: { Display: objTOMaster.TOType, Value: objTOMaster.ToTypeId },
                    };

                    $scope.objTO.TOItemDetail = [];

                    angular.forEach(result.data.DataList.objTOItemDetail, function (value) {
                        var objTOItemDetail = {
                            TOItemId: value.TOItemId,
                            TOId: value.TOId,
                            SpecId: value.SpecId,
                            ProductCode: value.ProductCode,
                            ProductId: value.ProductId,
                            SubCategoryId: value.SubCategoryId,
                            CategoryId: value.CategoryId,
                            TechSpec: value.TechSpec,
                            SpecValue: value.SpecValue,
                            ProductName: value.ProductName,
                            SubCategoryName: value.SubCategoryName,
                            CategoryName: value.CategoryName,
                            Status: 2,//1 : Insert , 2:Update ,3 :Delete
                            TOItemSepcData: { Display: value.TechSpec, Value: value.SpecId },
                            TOItemProdData: { Display: value.ProductId, Value: value.ProductName },
                            TOItemSubCatData: { Display: value.SubCategoryId, Value: value.SubCategoryName },
                            TOItemCatData: { Display: value.CategoryId, Value: value.CategoryName },
                        }
                        $scope.objTO.TOItemDetail.push(objTOItemDetail);
                    }, true);

                    $scope.objTO.TOSpecification = [];
                    $scope.objTO.ToPacking = [];

                    angular.forEach(result.data.DataList.objTechnicaldetail, function (value) {
                        if (value.TechSpecifType == 1) {
                            var objTOSpecification = {
                                TechDetailId: value.TechDetailId,
                                TOItemId: value.TOItemId,
                                TOId: value.TOId,
                                ProductId: value.ProductId,
                                TechParaID: value.TechParaID,
                                TechParaVal: value.TechParaVal,
                                TechParaRequirment: value.TechParaRequirment,
                                TechSpecifType: value.TechSpecifType,
                                TechSpec: value.TechSpec,
                                SpecId: value.SpecId,
                                Status: 2, //1 : Insert , 2:Update ,3 :Delete
                                TOSpecData: { Display: value.TechSpec, Value: value.SpecId },
                                TOPackData: { Display: value.TechSpec, Value: value.SpecId },
                                TechHeadId: value.TechHeadId,
                                TechHead: value.TechHead,
                                TechnicalHead: { Display: value.TechHead, Value: value.TechHeadId }
                            }
                            $scope.objTO.TOSpecification.push(objTOSpecification);
                        }
                        else {

                            var objTOPacking = {
                                TechDetailId: value.TechDetailId,
                                TOItemId: value.TOItemId,
                                TOId: value.TOId,
                                ProductId: value.ProductId,
                                TechParaID: value.TechParaID,
                                PackParaVal: value.TechParaVal,
                                PackParaRequirment: value.TechParaRequirment,
                                TechSpecifType: value.TechSpecifType,
                                TechSpec: value.TechSpec,
                                SpecId: value.SpecId,
                                Status: 2, //1 : Insert , 2:Update ,3 :Delete
                                TOSpecData: { Display: value.TechSpec, Value: value.SpecId },
                                TOPackData: { Display: value.TechSpec, Value: value.SpecId },
                                TechHeadId: value.TechHeadId,
                                TechHead: value.TechHead,
                                TechnicalHead: { Display: value.TechHead, Value: value.TechHeadId }

                            };
                            $scope.objTO.ToPacking.push(objTOPacking);
                        }
                    }, true)
                    $scope.sample = $scope.objTO.TOSpecification;
                    $scope.Packsample = $scope.objTO.ToPacking;
                    $scope.storage = angular.copy($scope.objTO);

                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }
        $scope.Add = function () {
            window.location.href = "/master/TO/AddTO";
        }
        function ResetForm() {
            $scope.objTO = {
                TOId: 0,
                InqId: 0,
                InqNo: '',
                ToTypeId: 0,
                TOType: '',
                BuyerName: '',
                Remark: '',
                TOBuyerData: { Display: '', Value: '' },
                TOInquiryData: { Display: '', Value: '' },
                TOTypeData: { Display: '', Value: '' },
                TOItemDetail: [],
                TOSpecification: [],
                ToPacking: []
            };
            $scope.objTOItem = {
                TOItemId: 0,
                TOId: 0,
                SpecId: '',
                ProductId: '',
                ProductCode: '',
                SubCategoryId: '',
                CategoryId: '',
                TechSpec: '',
                SpecValue: '',
                ProductName: '',
                SubCategoryName: '',
                CategoryName: '',
                Status: 0, //1 : Insert , 2:Update ,3 :Delete
                TOItemSepcData: { Display: '', Value: '' },
                TOItemProdData: { Display: '', Value: '' },
                TOItemSubCatData: { Display: '', Value: '' },
                TOItemCatData: { Display: '', Value: '' },
            };
            $scope.objTOSpecification = {
                TechDetailId: 0,
                TOItemId: 0,
                TOId: 0,
                ProductId: '',
                TechParaID: 0,
                TechParaVal: '',
                TechParaRequirment: '',
                TechSpecifType: '',
                TechSpec: '',
                SpecId: '',
                Status: 0, //1 : Insert , 2:Update ,3 :Delete
                TOSpecData: { Display: '', Value: '' },
                TechHeadId: 0,
                TechHead: '',
                TechnicalHead: { Display: '', Value: '' }
            };
            $scope.ToPacking = {
                TechDetailId: 0,
                TOItemId: 0,
                TOId: 0,
                ProductId: '',
                TechParaID: 0,
                TechParaVal: '',
                TechParaRequirment: '',
                TechSpecifType: '',
                TechSpec: '',
                SpecId: '',
                Status: 0, //1 : Insert , 2:Update ,3 :Delete
                TOPackData: { Display: '', Value: '' }
            }
            if ($scope.FormTOInfo)
                $scope.FormTOInfo.$setPristine();
            $scope.addMode = true;
            $scope.isFirstFocus = false;
            $scope.storage = {};
            $timeout(function () {
                $scope.isFirstFocus = true;
            });
            $scope.EditTOItemIndex = -1;
        }
        ResetForm();
        $scope.Reset = function () {
            if ($scope.addMode == false) {
                $scope.objTO = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }
        $scope.CreateUpdate = function (data) {
            $scope.objTOItem.TOItemSepcData.Display = ' ';
            $scope.objTOItem.TOItemProdData.Display = ' ';
            $scope.objTOItem.TOItemSubCatData.Display = ' ';
            $scope.objTOItem.TOItemCatData.Display = ' ';
            data.TOType = data.TOTypeData.Display;
            data.ToTypeId = data.TOTypeData.Value;
            data.InqNo = data.TOInquiryData.Display;
            data.InqId = data.TOInquiryData.Value;
            //data.BuyerName = data.TOBuyerData.Display;
            //data.BuyerId = data.TOBuyerData.Value
            TOService.CreateUpdate(data).then(function (result) {
                if (result.data.ResponseType == 1) {
                    ResetForm();
                    toastr.success(result.data.Message);
                    window.location.href = "/master/TO";
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
               //{ "title": "TO Id", "data": "TOId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "Inquiry No", "field": "InqNo", sortable: "InqNo", filter: { InqNo: "text" }, show: true, },
               { "title": "Buyer Name", "field": "BuyerName", sortable: "BuyerName", filter: { BuyerName: "text" }, show: true, },
               { "title": "Email", "field": "Email", sortable: "Email", filter: { Email: "text" }, show: true, },
               { "title": "MobileNo", "field": "MobileNo", sortable: "MobileNo", filter: { MobileNo: "text" }, show: true, },
               { "title": "Requirement", "field": "Requirement", sortable: "Requirement", filter: { Requirement: "text" }, show: true, },
               { "title": "Address", "field": "Address", sortable: "Address", filter: { Address: "text" }, show: false, },

               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a   class="btn btn-primary btn-xs" data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.TOId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                             //'<a class="btn btn-danger btn-xs"  data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.TOId)" data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                             '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.TOId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'TOId': 'asc' }
        }
        $scope.Edit = function (id) {

            window.location.href = "/master/TO/AddTO/" + id + "/" + 0;
        }
        $scope.View = function (id) {
            window.location.href = "/master/TO/AddTO/" + id + "/" + 1;
        }
        $scope.Delete = function (id) {
            TOService.DeleteById(id).then(function (result) {
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
        //BEGIN MANAGE TO SPECIFICATION INFORMATION
        $scope.AddSpecification = function (data, Index) {
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myNewModalContent.html',
                controller: ModalInstanceCtrlaaa,
                resolve: {
                    TOController: function () {
                        return $scope;
                    },
                    TOSpecificationData: function () {
                        return data;
                    },
                    TOService: function () {
                        return TOService;
                    },
                    ProductService: function () {
                        return ProductService;
                    },
                    ItemIndex: function () {
                        return Index;
                    }
                }
            });
        }
        //END MANAGE TO SPECIFICATION INFORMATION

        //BEGIN MANAGE TO ITEM INFORMATION
        $scope.AddTOItemDetail = function (data) {


            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    TOController: function () {
                        return $scope;
                    },
                    TOItemData: function () {
                        return data;
                    },
                    UploadProductDataService: function () {
                        return UploadProductDataService;
                    },
                    ProductService: function () {
                        return ProductService;
                    }
                }
            });
            modalInstance.result.then(function () {
                $scope.EditTOItemIndex = -1;
            }, function () {
                $scope.EditTOItemIndex = -1;
            })
        }
        $scope.DeleteTOItemDetail = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objTO.TOItemDetail[index] = data;
                } else {
                    $scope.objTO.TOItemDetail.splice(index, 1);
                }
                toastr.success("TO Item Detail Delete", "Success");
            })
        }
        $scope.EditTOItemDetail = function (data, index) {
            $scope.EditTOItemIndex = index;
            $scope.AddTOItemDetail(data);
        }
        //END MANAGE TO ITEM INFORMATION
        $scope.statusFilter = function (val) {
            if (val.Status != 3)
                return true;
        }

        $scope.$watch('objTO.TOInquiryData.Value', function (newVal) {
            if (newVal && newVal > 0) {
                TOService.GetAllInquiryInfoById(newVal).then(function (data) {
                    if (data.data.DataList.objInquiryMaster != null) {
                        $scope.objTO.IBuyerName = data.data.DataList.objInquiryMaster.BuyerName;
                        //$scope.objTO.ContactPersonname = data.data.DataList.objInquiryMaster.ContactPersonname;
                        $scope.objTO.MobileNo = data.data.DataList.objInquiryMaster.MobileNo;
                        $scope.objTO.IEmail = data.data.DataList.objInquiryMaster.Email;
                        $scope.objTO.BuyAddress = data.data.DataList.objInquiryMaster.Address;
                    }
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }
        });
        //$scope.$watch('objTO.TOBuyerData', function (val) {
        //    if (val.Value && val.Value > 0) {
        //        TOService.GetBuyerDetailById(val.Value).then(function (result) {
        //            if (result.data.ResponseType == 1) {
        //                angular.forEach(result.data.DataList, function (val) {
        //                    $scope.objTO.VAT = val.VAT;
        //                    $scope.objTO.WebAddress = val.WebAddress!=undefined?val.WebAddress:'';
        //                    $scope.objTO.Email = val.Email != undefined ? val.Email : '';
        //                    $scope.objTO.Tel = val.MobileNo != undefined ? val.MobileNo : '';
        //                });
        //            } else if (result.ResponseType == 3) {
        //                toastr.error(result.data.Message, 'Opps, Something went wrong');
        //            }
        //        }, function (error) {
        //            $rootScope.errorHandler(error)
        //        })
        //    }
        //})
    }
    var ModalInstanceCtrl = function ($scope, $uibModalInstance, TOController, TOItemData, UploadProductDataService, ProductService) {
        $scope.GetAllUploadProductDataInfoById = function (id) {
            UploadProductDataService.GetAllUploadProductDataInfoById(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    if (result.data.DataList.objUploadProductData.length > 0) {
                        var objProductDataMaster = result.data.DataList.objUploadProductData[0];
                        $scope.objTOItem.TOItemCatData = {
                            Display: objProductDataMaster.Category, Value: objProductDataMaster.CategoryId
                        };
                        $scope.objTOItem.TOItemSubCatData = {
                            Display: objProductDataMaster.SubCategory, Value: objProductDataMaster.SubCategoryId
                        };
                        $scope.objTOItem.TOItemProdData = {
                            Display: objProductDataMaster.ProductName, Value: objProductDataMaster.ProductId
                        };
                        $scope.objTOItem.TOProductModel = objProductDataMaster.ProductModelNo
                    } else {
                        $scope.objTOItem.ProductCode = '';
                        $scope.objTOItem.TOItemCatData = {
                            Display: '', Value: ''
                        };
                        $scope.objTOItem.TOItemSubCatData = {
                            Display: '', Value: ''
                        };
                        $scope.objTOItem.TOItemProdData = {
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
        $scope.$watch('objTOItem.TOItemProdData', function (val) {
            if (val.Value) {
                ProductService.GetProductById(val.Value).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        if (result.data.DataList != null) {
                            $scope.objTOItem.ProductCode = result.data.DataList.ProductCode;
                        }
                    } else {
                        toastr.error(result.data.Message, 'Opps, Something went wrong');
                    }
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }
        }, true)
        //$scope.statusFilter = function (val) {
        //    if (val.Status != 3)
        //        return true;
        //}
        //$scope.$watch('objTOItem.TOItemCatData', function (data) {
        //    if (TOItemData.CategoryId != undefined && TOItemData.CategoryId != '') {
        //        if (data.Value != TOItemData.CategoryId.toString()) {
        //            $scope.objTOItem.TOItemSubCatData.Display = '';
        //            $scope.objTOItem.TOItemSubCatData.Value = '';
        //            $scope.objTOItem.TOItemProdData.Display = '';
        //            $scope.objTOItem.TOItemProdData.Value = '';
        //        }
        //    }
        //}, true)

        //$scope.$watch('objTOItem.TOItemSubCatData', function (data) {
        //    if (TOItemData.SubCategoryId != undefined && TOItemData.SubCategoryId != '') {
        //        if (data.Value != TOItemData.SubCategoryId.toString()) {
        //            $scope.objTOItem.TOItemProdData.Display = '';
        //            $scope.objTOItem.TOItemProdData.Value = '';
        //        }
        //    }
        //}, true)

        $scope.objTOItem = {
            TOItemId: TOItemData.TOItemId,
            TOId: TOItemData.TOId,
            SpecId: TOItemData.SpecId,
            ProductId: TOItemData.ProductId,
            ProductCode: TOItemData.ProductCode,
            SubCategoryId: TOItemData.SubCategoryId,
            CategoryId: TOItemData.CategoryId,
            TechSpec: TOItemData.TechSpec,
            SpecValue: TOItemData.SpecValue,
            ProductName: TOItemData.ProductName,
            SubCategoryName: TOItemData.SubCategoryName,
            CategoryName: TOItemData.CategoryName,
            Status: TOItemData.Status, //1 : Insert , 2:Update ,3 :Delete
            TOItemSepcData: { Display: TOItemData.TechSpec, Value: TOItemData.SpecId },
            TOItemProdData: { Display: TOItemData.ProductName, Value: TOItemData.ProductId },
            TOItemSubCatData: { Display: TOItemData.SubCategoryName, Value: TOItemData.SubCategoryId },
            TOItemCatData: { Display: TOItemData.CategoryName, Value: TOItemData.CategoryId },
        };
        $scope.close = function () {
            $uibModalInstance.close();
        };

        $scope.CreateUpdate = function (data) {
            data.SpecId = data.TOItemSepcData.Value;
            data.TechSpec = data.TOItemSepcData.Display;
            data.ProductId = data.TOItemProdData.Value;
            data.ProductName = data.TOItemProdData.Display;
            data.SubCategoryId = data.TOItemSubCatData.Value;
            data.SubCategoryName = data.TOItemSubCatData.Display;
            data.CategoryId = data.TOItemCatData.Value;
            data.CategoryName = data.TOItemCatData.Display;
            if (TOController.EditTOItemIndex > -1) {
                TOController.objTO.TOItemDetail[TOController.EditTOItemIndex] = data;
                TOController.EditTOItemIndex = -1;
            } else {
                data.Status = 1;
                TOController.objTO.TOItemDetail.push(data);
            }
            $scope.close();
        }

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'TOController', 'TOItemData', 'UploadProductDataService', 'ProductService']





    //test
    var ModalInstanceCtrlaaa = function ($scope, $uibModalInstance, TOController, TOSpecificationData, TOService, ProductService, ItemIndex) {

        var TOItemData = [];
        $scope.prdId = TOSpecificationData.TOItemDetail[ItemIndex].ProductId;

        $scope.sample = TOSpecificationData.TOSpecification;
        var returnedData = $.grep($scope.sample, function (element, index) {
            return (element.ProductId == $scope.prdId);
        });
        $scope.ToItemdata = returnedData;


        $scope.Packsample = TOSpecificationData.ToPacking;
        var packreturnedData = $.grep($scope.Packsample, function (element, index) {
            return (element.ProductId == $scope.prdId);
        });
        $scope.TopackItemdata = packreturnedData;
        //$scope.objTOSpecification.ProductId = TOSpecificationData.ProductId;
        $scope.objTOSpecification = {
            TechDetailId: TOSpecificationData.TechDetailId,
            TOItemId: TOSpecificationData.TOItemId,
            TOId: TOSpecificationData.TOId,
            ProductId: TOSpecificationData.ProductId,
            TechParaID: TOSpecificationData.TechParaID,
            TechParaVal: TOSpecificationData.TechParaVal,
            TechParaRequirment: TOSpecificationData.TechParaRequirment,
            TechSpecifType: TOSpecificationData.TechSpecifType,
            TechSpec: TOSpecificationData.TechSpec,
            SpecId: TOSpecificationData.SpecId,
            Status: TOSpecificationData.Status, //1 : Insert , 2:Update ,3 :Delete
            TOSpecData: { Display: TOSpecificationData.TechSpec, Value: TOSpecificationData.SpecId },
            TOPackData: { Display: TOSpecificationData.TechSpec, Value: TOSpecificationData.SpecId },
            TechnicalHead: { Display: TOSpecificationData.TechHead, Value: TOSpecificationData.TechHeadId }
        };

        $scope.objTOPacking = {
            TechDetailId: TOSpecificationData.TechDetailId,
            TOItemId: TOSpecificationData.TOItemId,
            TOId: TOSpecificationData.TOId,
            ProductId: TOSpecificationData.ProductId,
            TechParaID: TOSpecificationData.TechParaID,
            TechParaVal: TOSpecificationData.TechParaVal,
            TechParaRequirment: TOSpecificationData.TechParaRequirment,
            TechSpecifType: TOSpecificationData.TechSpecifType,
            TechSpec: TOSpecificationData.TechSpec,
            SpecId: TOSpecificationData.SpecId,
            Status: TOSpecificationData.Status, //1 : Insert , 2:Update ,3 :Delete
            TOSpecData: { Display: TOSpecificationData.TechSpec, Value: TOSpecificationData.SpecId },
            TOPackData: { Display: TOSpecificationData.TechSpec, Value: TOSpecificationData.SpecId },
            TechnicalHead: { Display: TOSpecificationData.TechHead, Value: TOSpecificationData.TechHeadId }

        };


        //delete Technical Detail
        $scope.DeletetechnicalDetail = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.sample[index] = data;
                } else {
                    $scope.sample.splice(index, 1);
                }
                var returnedData = $.grep($scope.sample, function (element, index) {
                    return (element.ProductId == $scope.prdId && element.Status != 3);
                });
                $scope.ToItemdata = returnedData;
                toastr.success("TO Technical Detail Delete", "Success");
            })
        }

        $scope.DeletepackingDetail = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.Packsample[index] = data;
                } else {
                    $scope.Packsample.splice(index, 1);
                }
                var packreturnedData = $.grep($scope.Packsample, function (element, index) {
                    return (element.ProductId == $scope.prdId && element.Status != 3);
                });
                $scope.TopackItemdata = packreturnedData;
                toastr.success("TO Packing Detail Delete", "Success");
            })
        }

        $scope.AddGrid = function (data) {
            //if (data.TOSpecData.Display == undefined || data.TOSpecData.Display == "") {
            //    toastr.error("Select Specification");
            //    return false
            //} else if (data.TechnicalHead.Display == undefined || data.TechnicalHead.Display == "") {
            //    toastr.error("Select Technical Head");
            //    return false
            //}
            //else if (data.TechParaVal == undefined || data.TechParaVal == "") {
            //    toastr.error("Enter Product Parameter");
            //    return false
            //}
            //else if (data.TechParaRequirment == undefined || data.TechParaRequirment == "") {
            //    toastr.error("Enter Remark");
            //    return false
            //}
            data.ProductId = $scope.prdId;
            data.TechParaID = data.TOSpecData.Value;
            data.TechSpec = data.TOSpecData.Display;
            data.TechHeadId = data.TechnicalHead.Value;
            data.TechHead = data.TechnicalHead.Display;
            if (TOController.EditTOItemIndex > -1) {
                data.Status = 2;
                TOController.objTO.TOSpecification[TOController.EditTOItemIndex] = data;
                TOController.EditTOItemIndex = -1;
            } else {
                data.Status = 1;
                //if (data.TOSpecData.Display != undefined || && data.TechParaRequirment != undefined && data.TechParaVal != undefined) {
                //    TOController.objTO.TOSpecification.push(data);
                //}
                if (data.TechnicalHead.Display == undefined || data.TechnicalHead.Display == "") {
                    toastr.error("Select Technical Head");
                    return false
                } else if (data.TOSpecData.Display == undefined || data.TOSpecData.Display == "") {
                    toastr.error("Select Specification");
                    return false
                    }
                else if (data.TechParaVal == undefined || data.TechParaVal == "") {
                    toastr.error("Enter Product Parameter");
                    return false
                }
                else if (data.TechParaRequirment == undefined || data.TechParaRequirment == "") {
                    toastr.error("Enter Remark");
                    return false
                }

                else {
                    TOController.objTO.TOSpecification.push(data);
                }

            }

            $scope.ToItemdata = TOController.objTO.TOSpecification;
            var returnedData = $.grep($scope.ToItemdata, function (element, index) {
                return (element.ProductId == $scope.prdId);
            });
            $scope.ToItemdata = returnedData;

            $scope.objTOSpecification = {
                TechDetailId: 0,
                TOItemId: 0,
                TOId: 0,
                TechParaID: 0,
                TechParaVal: '',
                TechParaRequirment: '',
                TechSpecifType: '',
                TechSpec: '',
                SpecId: '',
                Status: 0, //1 : Insert , 2:Update ,3 :Delete
                TOSpecData: { Display: '', Value: '' },
                TechnicalHead: { Display: '', Value: '' }
            };
        }

        $scope.$watch('objTOSpecification.TOSpecData', function (val) {
            if (val.Value) {
                TOService.gettechnicalspec($scope.prdId, val.Value).then(function (result) {
                    $scope.objTOSpecification.TechParaVal = result.data.DataList.objTOMaster.Value
                })
            }
        }, true)

        $scope.$watch('objTOPacking.TOPackData', function (val) {

            if (val.Value) {
                TOService.gettechnicalspec($scope.prdId, val.Value).then(function (result) {
                    $scope.objTOPacking.PackParaVal = result.data.DataList.objTOMaster.Value
                })
            }
        }, true)


        $scope.AddPackGrid = function (data) {
            data.ProductId = $scope.prdId;
            data.TechParaID = data.TOPackData.Value;
            data.TechSpec = data.TOPackData.Display;
            if (TOController.EditTOItemIndex > -1) {
                data.Status = 2;
                TOController.objTO.ToPacking[TOController.EditTOItemIndex] = data;
                TOController.EditTOItemIndex = -1;
            } else {
                data.Status = 1;

                if (data.TOPackData.Display == undefined || data.TOPackData.Display == "") {
                    toastr.error("Select Specification", "Error");
                    return false
                }
                else if (data.PackParaVal == undefined || data.PackParaVal == "") {
                    toastr.error("Select Product Parameter", "Error");
                    return false
                }
                else if (data.PackParaRequirment == undefined || data.PackParaRequirment == "") {
                    toastr.error("Add Remark", "Error");
                    return false
                }
                else {
                    TOController.objTO.ToPacking.push(data);
                }

            }

            $scope.TopackItemdata = TOController.objTO.ToPacking;
            var packreturnedData = $.grep($scope.TopackItemdata, function (element, index) {
                return (element.ProductId == $scope.prdId);
            });
            $scope.TopackItemdata = packreturnedData;

            $scope.objTOPacking = {
                TechDetailId: 0,
                TOItemId: 0,
                TOId: 0,
                TechParaID: 0,
                TechParaVal: '',
                TechParaRequirment: '',
                TechSpecifType: '',
                TechSpec: '',
                SpecId: '',
                Status: 0, //1 : Insert , 2:Update ,3 :Delete
                TOPackData: { Display: '', Value: '' }
            };
        }
        $scope.close = function () {
            $uibModalInstance.close();
        };
    }
    ModalInstanceCtrlaaa.$inject = ['$scope', '$uibModalInstance', 'TOController', 'TOSpecificationData', 'TOService', 'ProductService', 'ItemIndex']


})()



