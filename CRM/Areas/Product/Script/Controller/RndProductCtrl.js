(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("RndProductCtrl", [
                    "$scope", "$filter", "RndProductService", "CountryService", "SourceService", "$uibModal", "Upload", "$sce",
                    RndProductCtrl
        ]);
    function RndProductCtrl($scope, $filter, RndProductService, CountryService, SourceService, $uibModal, Upload, $sce) {
        $scope.objRndProduct = $scope.objRndProduct || {};
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};
        //var Permission = '';
        //$scope.myinit = Function()
        //{
        //    var MenuCode = 15;//Work Task List Form ID
        //    Permission = GetRole.multiply(MenuCode)
        //}
        $scope.objRndProduct = {
            //MainProductId: 0,
            //MainProductData: { Display: '', Value: '' },
            RNDProductId: 0,
            CategoryId: 0,
            CategoryData: { Display: '', Value: '' },
            SubCategoryId: 0,
            SubCategoryData: { Display: '', Value: '' },
            ProductName: '',
            Description: '',
            EmailSpeechId: 0,
            EmailSpeechId: 0,
            EmailSpeechData: { Display: '', Value: '' },
            SMSSpeechData: { Display: '', Value: '' },
            Keyword: [],
            RMPhotos: '',
            MPPhotos: '',
            FMPhotos: '',
            Videoes: '',
            EmailSpeech: '',
            SMSSpeech: '',
            ChatSpeech: '',
            objRndSupplierList: [],
        }
        $scope.objRndSupplierDetail = {
            RNDSupplierId: 0,
            RNDProductId: 0,
            CompanyName: '',
            Website: '',
            CityId: 0,
            MobileNo: '',
            Email: '',
            ContactEmail: '',
            chat: '',
            TeliPhoneNo: '',
            Address: '',
            Attende: '',
            Price: '',
            Pincode: '',
            Remark: '',
            Quotations: '',
            Catalogue: '',
            UnitId: 0,
            Status: 0, //1 : Insert , 2:Update ,3 :Delete
            CountryData: { Display: '', Value: '' },
            StateData: { Display: '', Value: '' },
            CityData: { Display: '', Value: '' },
            CurrencyData: { Display: '', Value: '' },
            SupplierData: { Display: '', Value: '' },
            CommunicationRecord: []
        };
        $scope.telCodeData = [];
        $scope.EmailCodeData = [];
        $scope.mobileCodeData = [];
        $scope.emailAddressData = [];
        $scope.chatCodeData = [];
        $scope.files = [];
        $scope.images = [];
        $scope.img = [];
        $scope.WebAddressData = [];
        $scope.SetRNDId = function (id, isdisable) {
            if (isdisable === undefined) isdisable = 0;
            CountryService.GetCountryFlag().then(function (result) {
                $scope.telCodeData = angular.copy(result);
                $scope.mobileCodeData = angular.copy(result);

                SourceService.GetSourceData().then(function (result) {
                    $scope.chatCodeData = angular.copy(result);
                    if (id > 0) {
                        $scope.SrNo = id;
                        $scope.addMode = false;
                        $scope.saveText = "Update";
                        $scope.isClicked = false;
                        if (isdisable == 1) {
                            $scope.isClicked = true;
                        }
                        $scope.GetAllRndProductInfoById(id);
                    } else {
                        $scope.SrNo = 0;
                        $scope.addMode = true;
                        $scope.isClicked = false;
                        $scope.saveText = "Save";
                    }
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })

        }
        // SourceService
        $scope.GetAllRndProductInfoById = function (id) {
            RndProductService.GetAllRndProductInfoById(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    var objRndProductMaster = result.data.DataList.objRndProductMaster;
                    $scope.objRndProduct = {
                        RNDProductId: objRndProductMaster.RNDProductId,
                        CategoryId: objRndProductMaster.CategoryId,
                        CategoryData: { Display: objRndProductMaster.CategoryName, Value: objRndProductMaster.CategoryId },
                        SubCategoryId: objRndProductMaster.SubCategoryId,
                        SubCategoryData: { Display: objRndProductMaster.SubCategoryName, Value: objRndProductMaster.SubCategoryId },
                        ProductName: objRndProductMaster.ProductName,
                        Description: objRndProductMaster.Description,

                        EmailSpeechId: objRndProductMaster.EmailSpeechId,
                        EmailSpeechData: { Display: objRndProductMaster.Title, Value: objRndProductMaster.EmailSpeechId },
                        SMSSpeechId: objRndProductMaster.SMSSpeechId,
                        SMSSpeechData: { Display: objRndProductMaster.SMSTitle, Value: objRndProductMaster.SMSSpeechId },
                        Keyword: objRndProductMaster.Keyword,
                        RMPhotos: objRndProductMaster.RMPhotos,
                        MPPhotos: objRndProductMaster.MPPhotos,
                        FMPhotos: objRndProductMaster.FMPhotos,
                        Videoes: objRndProductMaster.Videoes,
                        EmailSpeech: objRndProductMaster.EmailSpeech,
                        SMSSpeech: objRndProductMaster.SMSSpeech,
                        ChatSpeech: objRndProductMaster.ChatSpeech,
                    };
                    $scope.Videoes = (objRndProductMaster.Videoes != '' && objRndProductMaster.Videoes != null) ? objRndProductMaster.Videoes.split("|") : [];

                    var ArrKeywords = [];
                    if (objRndProductMaster.Keyword) {
                        _.forEach(objRndProductMaster.Keyword.split("|"), function (val) { ArrKeywords.push({ 'text': val }) });
                    }
                    $scope.objRndProduct.Keyword = ArrKeywords;

                    if (objRndProductMaster.RMPhotos) {
                        _.forEach(objRndProductMaster.RMPhotos.split("|"), function (val) { $scope.files.push({ 'result': "/UploadImages/RndProduct/" + val }) });
                    }

                    if (objRndProductMaster.MPPhotos) {
                        _.forEach(objRndProductMaster.MPPhotos.split("|"), function (val) { $scope.images.push({ 'result': "/UploadImages/RndProduct/" + val }) });
                    }
                    if (objRndProductMaster.FMPhotos) {
                        _.forEach(objRndProductMaster.FMPhotos.split("|"), function (val) { $scope.img.push({ 'result': "/UploadImages/RndProduct/" + val }) });
                    }
                    $scope.objRndProduct.objRndSupplierList = [];
                    angular.forEach(result.data.DataList.objRndSupplierDetail, function (value) {
                        var datacatimg = value.Quotations.split('|');
                        var img = datacatimg.map(function (item) {
                            var dataca = "/UploadImages/RndProductSupplier/" + item;
                            return dataca;
                        });
                        datacatimg = img.join("|");
                        var datacat = value.Catalogue.split('|');
                        var imgin = datacat.map(function (item) {
                            var dataca = "/UploadImages/RndProductSupplier/" + item;
                            return dataca;
                        });
                        datacat = imgin.join("|");
                        var objRndSupplierDetail = {
                            RNDSupplierId: value.RNDSupplierId,
                            RNDProductId: value.RNDProductId,
                            CompanyName: value.CompanyName,
                            MobileNo: value.MobileNo,
                            TeliPhoneNo: value.TeliPhoneNo,
                            Email: value.Email,
                            CommunicationRecord: value.CommunicationRecord,
                            ContactEmail: value.ContactEmail,
                            chat: value.Chat,
                            Address: value.Address,
                            Attende: value.Attende,
                            Website: value.Website,
                            Price: value.Price,
                            Pincode: value.Pincode,
                            Remark: value.Remark,
                            CityId: value.CityId,
                            Quotations: datacatimg,
                            Catalogue: datacat,
                            UnitId: value.UnitId,
                            Status: 2,//1 : Insert , 2:Update ,3 :Delete
                            CountryData: { Display: value.CountryName, Value: value.CountryId },
                            StateData: { Display: value.StateName, Value: value.StateId },
                            CityData: { Display: value.CityName, Value: value.CityId },
                            CurrencyData: { Display: value.CurrencyName, Value: value.UnitId },
                            SupplierData: { Display: value.CompanyName, Value: value.SupplierId }
                        }
                        $scope.objRndProduct.objRndSupplierList.push(objRndSupplierDetail);
                    }, true);
                    //_.forEach($scope.objRndProduct.CommunicationRecord.split("|"), function (val) { $scope.objRndSupplierDetail.objRndSupplierCommunicate.push({ 'CommunicationRecord': val }) });
                    $scope.storage = angular.copy($scope.objRndProduct);

                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }
        $scope.$watch('objRndProduct.EmailSpeechData', function (val) {
            if (val.Value && val.Value > 0) {
                RndProductService.GetEmailSpeechDetailById(val.Value).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        $scope.objRndProduct.EmailSpeech = result.data.DataList.objobjEmailSpeechMaster.Description;
                        $scope.objRndProduct.Subject = result.data.DataList.objobjEmailSpeechMaster.Subject;
                    } else if (result.ResponseType == 3) {
                        toastr.error(result.data.Message, 'Opps, Something went wrong');
                    }
                }, function (error) {
                    $rootScope.errorHandler(error)
                })
            }
        })
        $scope.$watch('objRndProduct.SMSSpeechData', function (val) {
            if (val.Value && val.Value > 0) {
                RndProductService.GetSMSSpeechDetailById(val.Value).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        $scope.objRndProduct.SMSSpeech = result.data.DataList.objSMSSpeechMaster.SMS;
                    } else if (result.ResponseType == 3) {
                        toastr.error(result.data.Message, 'Opps, Something went wrong');
                    }
                }, function (error) {
                    $rootScope.errorHandler(error)
                })
            }
        })


        function ResetForm() {
            $scope.objRndProduct = {
                //MainProductId: 0,
                //MainProductData: { Display: '', Value: '' },
                RNDProductId: 0,
                CategoryId: 0,
                CategoryData: { Display: '', Value: '' },
                SubCategoryId: 0,
                SubCategoryData: { Display: '', Value: '' },
                ProductName: '',
                Description: '',
                EmailSpeechId: 0,
                EmailSpeechData: { Display: '', Value: '' },
                SMSSpeechData: { Display: '', Value: '' },
                Keyword: [],
                RMPhotos: '',
                MPPhotos: '',
                FMPhotos: '',
                Videoes: '',
                EmailSpeech: '',
                SMSSpeech: '',
                ChatSpeech: '',
                objRndSupplierList: [],
            }
            $scope.objRndSupplierDetail = {
                RNDSupplierId: 0,
                RNDProductId: 0,
                CompanyName: '',
                MobileNo: '',
                Website: '',
                TeliPhoneNo: '',
                CityId: 0,
                Email: '',
                ContactEmail: '',
                chat: '',
                Address: '',
                Attende: '',
                Price: '',
                Pincode: '',
                Remark: '',
                Quotations: '',
                Catalogue: '',
                UnitId: 0,
                Status: 0, //1 : Insert , 2:Update ,3 :Delete
                CountryData: { Display: '', Value: '' },
                StateData: { Display: '', Value: '' },
                CityData: { Display: '', Value: '' },
                CurrencyData: { Display: '', Value: '' },
                SupplierData: { Display: '', Value: '' },
                CommunicationRecord: []

            };
            if ($scope.FormRndProductInfo)
                $scope.FormRndProductInfo.$setPristine();
            $scope.addMode = true;
            $scope.isFirstFocus = false;
            $scope.storage = {};
            //$timeout(function () {
            //    $scope.isFirstFocus = true;
            //});
            $scope.EditRndProductContactIndex = -1;
        }
        ResetForm();
        $scope.statusFilter = function (val) {
            if (val.Status != 3)
                return true;
        }
        $scope.CreateUpdate = function (data) {
            var error = true;
            if (data.Keyword.length < 8) {
                toastr.error('Enter Minimum 8 Keywords.');
                error = false;
            }
            if (error == true) {
                //$scope.objRndProduct.RNDProductId = data.RNDProductId;
                data.CategoryId = data.CategoryData.Value;
                data.SubCategoryId = data.SubCategoryData.Value;
                data.EmailSpeechId = data.EmailSpeechData.Value;
                data.SMSSpeechId = data.SMSSpeechData.Value;

                // KeyWord
                var Keywords;
                if (data.Keyword.indexOf('|') == -1 && data.Keyword != '') {
                    data.Keyword = data.Keyword.map(function (item) { return item['text'] }).join('|')
                }
                // Videoes
                if (data.Videoes != '' && data.Videoes != null) {
                    if (data.Videoes.indexOf('|') == -1) {
                        data.Videoes = $scope.Videoes.join("|")
                    }
                }
                // cataloguoes
                var RMPhotos = $scope.files.map(function (item) {
                    var datacatalog = item['result'].split('/');
                    return datacatalog[datacatalog.length - 1];
                });
                data.RMPhotos = RMPhotos.join("|")
                // Photoes
                var MPPhotos = $scope.images.map(function (item) {
                    var datacatalog = item['result'].split('/');
                    return datacatalog[datacatalog.length - 1];
                });
                data.MPPhotos = MPPhotos.join("|")

                // RMPhotos
                var FMPhoto = $scope.img.map(function (item) {
                    var datacatalog = item['result'].split('/');
                    return datacatalog[datacatalog.length - 1];
                });
                data.FMPhotos = FMPhoto.join("|")
                angular.forEach(data.objRndSupplierList, function (value, index) {
                    if (value.Quotations != undefined) {
                        var datacatalog = value.Quotations.split('|');
                        var img = datacatalog.map(function (item) {
                            var dataca = item.split('/');
                            return dataca[dataca.length - 1];
                        });
                        datacatalog = img.join("|");
                        data.objRndSupplierList[index].Quotations = datacatalog
                    }
                    if (value.Catalogue != undefined) {
                        var datacatimg = value.Catalogue.split('|');
                        var img = datacatimg.map(function (item) {
                            var dataca = item.split('/');
                            return dataca[dataca.length - 1];
                        });
                        datacatimg = img.join("|");
                        data.objRndSupplierList[index].Catalogue = datacatimg
                    }
                }, true);
                RndProductService.CreateUpdate(data).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        ResetForm();
                        toastr.success(result.data.Message);
                        window.location.href = "/Product/RndProduct";
                    } else {
                        toastr.error(result.data.Message, 'Opps, Something went wrong');
                    }
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }
        }

        //CountryService.GetCountryFlag().then(function (result) {
        //    $scope.telCodeData = angular.copy(result);
        //});
        //$scope.objRndProduct = $scope.objRndProduct || {};
        //RNDProductId, SubCategoryId, ProductName, Description, Keyword, EmailSpeech, CreatedBy, CreatedDate, ModifyBy, ModifyDate, DeletedBy, DeletedDate, 
        //IsActive, Cataloges, Photoes, Videoes
        //$scope.SetModelObj = function (obj) {
        //    $scope.files = [];
        //    $scope.images = [];
        //    $scope.objRndProduct = obj.Product;
        //    $scope.objSupplierList = obj.SupplierList;
        //    $scope.objRndProduct = {
        //        RNDProductId: obj.Product.RNDProductId,
        //        MainProductId: obj.Product.MainProductId,
        //        MainProductData: { Display: obj.Product.MainProductName, Value: obj.Product.MainProductId },
        //        CategoryId: obj.Product.CategoryId,
        //        CategoryData: { Display: obj.Product.CategoryName, Value: obj.Product.CategoryId },
        //        SubCategoryId: obj.Product.SubCategoryId,
        //        SubCategoryData: { Display: obj.Product.SubCategoryName, Value: obj.Product.SubCategoryId },
        //        ProductName: obj.Product.ProductName,
        //        Description: obj.Product.Description,
        //        EmailSpeech: obj.Product.EmailSpeech,
        //        Videoes: [],//Keywords: [],
        //        Photoes: [],
        //        objRndSupplierList: obj.SupplierList
        //        //Photos: [],
        //    }
        //    $scope.video = (obj.Product.Videoes != '') ? obj.Product.Videoes.split("|") : [];
        //    var ArrKeywords = [];
        //    if (obj.Product.Keyword) {
        //        _.forEach(obj.Product.Keyword.split(","), function (val) { ArrKeywords.push({ 'text': val }) });
        //    }
        //    $scope.objRndProduct.Keywords = ArrKeywords;
        //    if (obj.Product.Cataloges) {
        //        _.forEach(obj.Product.Cataloges.split("|"), function (val) { $scope.files.push({ 'result': val }) });
        //    }
        //    if (obj.Product.Photoes) {
        //        _.forEach(obj.Product.Photoes.split("|"), function (val) { $scope.images.push({ 'result': val }) });
        //    }
        //}

        //$scope.SaveRndProduct = function () {
        //    $scope.objRndProduct.RNDProductId = $scope.objRndProduct.RNDProductId;
        //    $scope.objRndProduct.SubCategoryId = $scope.objRndProduct.SubCategoryData.Value;
        //    if ($scope.objRndProduct.Keywords.indexOf('|') == -1) {
        //        $scope.objRndProduct.Keywords = $scope.objRndProduct.Keywords.map(function (item) { return item['text'] }).join('|')
        //    }
        //    if ($scope.objRndProduct.Videos.indexOf('|') == -1) {
        //        $scope.objRndProduct.Videoes = $scope.objRndProduct.Videos//.join("|")
        //    }
        //    var RMPhotos = $scope.files.map(function (item) {
        //        return item['result'];
        //    });
        //    $scope.objRndProduct.RMPhotos = RMPhotos.join("|")
        //    var MPPhotos = $scope.images.map(function (item) {
        //        return item['result'];
        //    });
        //    $scope.objRndProduct.MPPhotos = MPPhotos.join("|")
        //    var FMPhotos = $scope.img.map(function (item) {
        //        return item['result'];
        //    });
        //    $scope.objRndProduct.FMPhotos = FMPhotos.join("|")

        //    var obj = {
        //        Product: $scope.objRndProduct,
        //        SupplierList: $scope.objRndProduct.objRndSupplierList
        //    }
        //    RndProductService.SaveProduct(obj).then(function (result) {
        //        if (result.data.status == true) {
        //            ResetForm();
        //            toastr.success(result.data.Message);
        //            window.location.href = "/Product/RndProduct";
        //        } else {
        //            toastr.error(result.data.Message, 'Opps, Something went wrong');
        //        }
        //    }, function (errorMsg) {
        //        toastr.error(errorMsg, 'Opps, Something went wrong');
        //    })
        //}

        $scope.Add = function () {
            //if (Permission.IsAdd == false) {
            //    toastr.error('Access is denied');
            //}
            //else {
            window.location.href = "/Product/RndProduct/AddProduct";
            //}
        }
        $scope.EditProduct = function (id) {
            //if (Permission.IsEdit == false) {
            //    toastr.error('Access is denied');
            //}
            //else {
            window.location.href = "/Product/RNDProduct/AddProduct/" + id + "/" + 0;
            //}
        }
        $scope.View = function (id) {
            window.location.href = "/Product/RNDProduct/AddProduct/" + id + "/" + 1;
        }
        //$scope.View = function (data) {
        //    var ArrKeywords = [];
        //    if (data.Keyword) {
        //        _.forEach(data.Keyword.split(","), function (val) { ArrKeywords.push({ 'text': val }) });
        //    }
        //    var objData = {
        //        ProductId: data.ProductId,
        //        CategoryId: data.CategoryId,
        //        CategoryData: { Display: data.CategoryName, Value: data.CategoryId },
        //        SubCategoryId: data.SubCategoryId,
        //        SubCategoryData: { Display: data.SubCategoryName, Value: data.SubCategoryId },
        //        MainProductId: data.MainProductId,
        //        MainProductData: { Display: data.MainProductName, Value: data.MainProductId },
        //        ProductName: data.ProductName,
        //        HSCode: data.HSCode,
        //        Keyword: ArrKeywords
        //    }
        //    $scope.Add(objData, 1);
        //}
        $scope.Delete = function (id) {
            RndProductService.DeleteProduct(id).then(function (result) {
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

        $scope.uploadCatloguesFile = function (files, errFiles) {
            if (files.length > 0) {
                angular.forEach(files, function (file) {
                    $scope.files.push(file);
                });
            }
            else {
                return;
            }
            $scope.errFiles = errFiles;
            angular.forEach(files, function (file) {
                file.upload = Upload.upload({
                    url: "/Handler/FileUpload.ashx",
                    method: 'POST',
                    data: { file: file },
                });
                file.upload.then(function (response) {
                    file.result = "/UploadImages/TempImg/" + response.data[0].imageName;
                }, function (response) {
                    if (response.status > 0)
                        $scope.errorMsg = response.status + ': ' + response.data;
                }, function (evt) {
                    file.progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                })
            });
        }
        $scope.deleteCatalogFile = function (idx) {
            var file = $scope.files[idx];
            $scope.files.splice(idx, 1);
        }
        $scope.uploadImgFile = function (files, errFiles) {
            if (files.length > 0) {
                angular.forEach(files, function (file) {
                    $scope.images.push(file);
                });
            }
            else {
                return;
            }
            $scope.errImageFiles = errFiles;
            angular.forEach(files, function (file) {
                file.upload = Upload.upload({
                    url: "/Handler/FileUpload.ashx",
                    method: 'POST',
                    data: { file: file },
                });

                file.upload.then(function (response) {
                    file.result = "/UploadImages/TempImg/" + response.data[0].imageName;
                }, function (response) {
                    if (response.status > 0)
                        $scope.errorMsg = response.status + ': ' + response.data;
                }, function (evt) {
                    file.progressp = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                })
            })
        }
        $scope.deleteImgFile = function (idx) {
            var file = $scope.images[idx];
            $scope.images.splice(idx, 1);
        }
        $scope.uploadFinalProductImgsFile = function (files, errFiles) {
            if (files.length > 0) {
                angular.forEach(files, function (file) {
                    $scope.img.push(file);
                });
            }
            else {
                return;
            }
            $scope.errImagesFiles = errFiles;
            angular.forEach(files, function (file) {
                file.upload = Upload.upload({
                    url: "/Handler/FileUpload.ashx",
                    method: 'POST',
                    data: { file: file },
                });

                file.upload.then(function (response) {
                    file.result = "/UploadImages/TempImg/" + response.data[0].imageName;
                }, function (response) {
                    if (response.status > 0)
                        $scope.errorMsg = response.status + ': ' + response.data;
                }, function (evt) {
                    file.progr = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                })
            })
        }

        $scope.deleteImgsFile = function (idx) {
            var file = $scope.img[idx];
            $scope.img.splice(idx, 1);
        }
        //BEGIN MANAGE QUOTATION SUPPLIER INFORMATION
        $scope.AddSupplier = function (data) {
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                size: 'lg',
                templateUrl: 'RndSupplierModalContent.html',
                controller: RndSupplierModalInstanceCtrl,
                resolve: {
                    RndProductCtrl: function () { return $scope; },
                    RndProductService: function () { return RndProductService; },
                    telCodeData: function () { return $scope.telCodeData },
                    EmailCodeData: function () { return $scope.EmailCodeData },
                    emailAddressData: function () { return $scope.emailAddressData },
                    mobileCodeData: function () { return $scope.mobileCodeData },
                    chatCodeData: function () { return $scope.chatCodeData },
                    WebAddressData: function () { return $scope.WebAddressData },
                    Upload: function () { return Upload },
                    objRndSupplierList: function () { return data }
                }
            });
            modalInstance.result.then(function () {
                $scope.EditRndProductContactIndex = -1;
            }, function () {
                $scope.EditRndProductContactIndex = -1;
            })
        }

        $scope.DeleteSupplier = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objRndProduct.objRndSupplierList[index] = data;
                } else {
                    $scope.objRndProduct.objRndSupplierList.splice(index, 1);
                }
                toastr.success("Supplier Quotation Detail Delete.", "Success");
            })
        }

        $scope.EditSupplier = function (data, index) {
            $scope.EditRndProductSupplierIndex = index;
            $scope.AddSupplier(data);
        }
        //END MANAGE QUOTATION SUPPLIER INFORMATION
        $scope.$watch('objRndProduct.CategoryData', function (data) {
            if ($scope.objRndProduct.CategoryId != undefined) {
                if (data.Value != $scope.objRndProduct.CategoryId.toString()) {
                    $scope.objRndProduct.SubCategoryData.Display = '';
                    $scope.objRndProduct.SubCategoryData.Value = '';
                }
            }
        }, true)

        //$scope.AddSupplier = function (data) {
        //    $scope.objRndSupplierObj = {
        //        RNDProductId: 0,
        //        CompanyName: '',
        //        MobileNo: [],
        //        Email: [],
        //        Address: '',
        //        Attende: '',
        //        Price: '',
        //        Remark: '',
        //        Quotations: ''
        //    }
        //    var RndSupplierModalInstance = $uibModal.open({
        //        backdrop: 'static',
        //        templateUrl: 'RndSupplierModalContent.html',
        //        controller: RndSupplierModalInstanceCtrl,
        //        resolve: {
        //            RndProductCtrl: function () { return $scope; },
        //            RndProductService: function () { return RndProductService; },
        //            telCodeData: function () { return $scope.telCodeData },
        //            Upload: function () { return Upload },
        //            objRndSupplier: function () { return data }
        //        }
        //    });
        //    RndSupplierModalInstance.result.then(function () {
        //        if (result.data.ResponseType == 1) {
        //            $uibModalInstance.close();
        //            toastr.success(result.data.Message);
        //        } else {
        //            toastr.error(result.data.Message, 'Opps, Something went wrong');
        //        }
        //    }, function (errorMsg) {
        //        toastr.error(errorMsg, 'Opps, Something went wrong');
        //    });
        //}
        //$scope.EditSupplier = function (data, index) {
        //    $scope.EditSupplierIndex = index
        //    var RndSupplierModalInstance = $uibModal.open({
        //        backdrop: 'static',
        //        templateUrl: 'RndSupplierModalContent.html',
        //        controller: RndSupplierModalInstanceCtrl,
        //        resolve: {
        //            RndProductCtrl: function () { return $scope },
        //            RndProductService: function () { return RndProductService; },
        //            telCodeData: function () { return $scope.telCodeData },
        //            Upload: function () { return Upload },
        //            objRndSupplier: function () { return data }
        //        }
        //    });
        //    RndSupplierModalInstance.result.then(function () {
        //    }, function () {
        //    });
        //}
        $scope.getTooltipHtmlContent = function (tooltip) {
            $scope.tooltipContent = $sce.trustAsHtml(tooltip);
            return $scope.tooltipContent;
        }
        $scope.gridObj = {
            columnsInfo: [
               //{ "title": "RNDProductId", "data": "RNDProductId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", filter: false, show: true },
               { "title": "Product", "field": "ProductName", sortable: "ProductName", filter: { ProductName: "text" }, show: true },
               { "title": "Category", "field": "CategoryName", sortable: "CategoryName", filter: { CategoryName: "text" }, show: true },
               { "title": "Sub Category", "field": "SubCategoryName", sortable: "SubCategoryName", filter: { SubCategoryName: "text" }, show: true },

               { "title": "Keyword", "field": "Keyword", sortable: "Keyword", filter: { Keyword: "text" }, show: true },
               {
                   "title": "Description", "field": "Description", sortable: "Description", filter: { Description: "text" }, show: false,
                   cellTemplte: function ($scope, row) {
                       var element = '<a href="#" style="color:black;" uib-tooltip-html="$parent.$parent.$parent.$parent.$parent.$parent.tooltipContent" ng-mouseover="$parent.$parent.$parent.$parent.$parent.$parent.getTooltipHtmlContent(row.Description)" ng-bind-html="getHtml(LimitString(row.Description,200))">Description!</a>'
                       return $scope.getHtml(element);
                   }
               },
               { "title": "Email Speech", "field": "EmailSpeech", sortable: "EmailSpeech", filter: { EmailSpeech: "text" }, show: false },
               { "title": "SMS Speech", "field": "SMSSpeech", sortable: "SMSSpeech", filter: { SMSSpeech: "text" }, show: false },
                //{
               //    "title": "RMPhotos", inputType: "text", show: false,
               //    "cellTemplte": function ($scope, row) {
               //        return $scope.getHtml('<img src="../Content/lib/CountryFlags/flags/' + row.RMPhotos + '" style="width:50px;height:40px;" />');
               //},
               //{ "title": "MPPhotos", "field": "MPPhotos", sortable: "MPPhotos", filter: { MPPhotos: "text" }, show: false },
               //{ "title": "FMPhotos", "field": "FMPhotos", sortable: "FMPhotos", filter: { FMPhotos: "text" }, show: false },
               {
                   "title": "Video Link", "field": "Videoes", sortable: "Videoes", filter: { Videoes: "text" }, show: false,
                   'cellTemplte': function ($scope, row) {
                       var element = "<p class='MailTo' ng-bind-html='getHtml($parent.$parent.$parent.$parent.$parent.$parent.FormatEmail(row.Videoes))'>"
                       return $scope.getHtml(element);
                   }
               },
               { "title": "ChatSpeech", "field": "ChatSpeech", sortable: "ChatSpeech", filter: { ProductName: "text" }, show: false },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.EditProduct(row.RNDProductId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                       //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.RNDProductId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                                                   '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.RNDProductId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }

               }
            ],
            Sort: { 'RNDProductId': 'asc' }
        }
        $scope.FormatEmail = function (d) {
            if (d != null) {
                var mailto = '';
                var emails = d.split('|');
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
        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };
    }

    var RndSupplierModalInstanceCtrl = function ($scope, $uibModalInstance, RndProductCtrl, RndProductService, telCodeData, EmailCodeData, emailAddressData, mobileCodeData, chatCodeData, WebAddressData, Upload, objRndSupplierList) {
        $scope.telCodeData = RndProductCtrl.telCodeData;
        $scope.EmailCodeData = RndProductCtrl.EmailCodeData;
        $scope.mobileCodeData = RndProductCtrl.mobileCodeData;
        $scope.emailAddressData = RndProductCtrl.emailAddressData;
        $scope.chatCodeData = RndProductCtrl.chatCodeData;
        $scope.WebAddressData = RndProductCtrl.WebAddressData;

        var objRndSupplier = objRndSupplierList;

        $scope.mobile = (objRndSupplier.MobileNo != '' && objRndSupplier.MobileNo != null) ? objRndSupplier.MobileNo.split(",") : [];
        $scope.Email = (objRndSupplier.Email != '' && objRndSupplier.Email != null) ? objRndSupplier.Email.split(",") : [];
        $scope.TeliPhoneNo = (objRndSupplier.TeliPhoneNo != '' && objRndSupplier.TeliPhoneNo != null) ? objRndSupplier.TeliPhoneNo.split(",") : [];
        $scope.ContactEmail = (objRndSupplier.ContactEmail != '' && objRndSupplier.ContactEmail != null) ? objRndSupplier.ContactEmail.split(",") : [];
        $scope.chat = (objRndSupplier.chat != '' && objRndSupplier.chat != null) ? objRndSupplier.chat.split(",") : [];
        $scope.Website = (objRndSupplier.Website != '' && objRndSupplier.Website != null) ? objRndSupplier.Website.split(",") : [];
        $scope.CommunicationRecords = (objRndSupplier.CommunicationRecord != '' && objRndSupplier.CommunicationRecord != null) ? objRndSupplier.CommunicationRecord.split("|") : [];
        $scope.QuotationFiles = [];
        if (objRndSupplier.Quotations) {
            //if (objRndSupplier.Status == 2) {
            //    _.forEach(objRndSupplier.Quotations.split("|"), function (val) { $scope.QuotationFiles.push({ 'result': "/UploadImages/RndProductSupplier/" + val }) });
            //} else {
            _.forEach(objRndSupplier.Quotations.split("|"), function (val) { $scope.QuotationFiles.push({ 'result': val }) });
            //}
        }

        //if (objRndSupplier.Quotations != '' && objRndSupplier.Quotations != null)
        //    $scope.QuotationFiles = objRndSupplier.Quotations.split("|")

        $scope.Catalogues = [];
        if (objRndSupplier.Catalogue) {
            //if (objRndSupplier.Status == 2) {
            //    _.forEach(objRndSupplier.Catalogue.split("|"), function (val) { $scope.Catalogues.push({ 'result': "/UploadImages/RndProductSupplier/" + val }) });
            //} else {
            _.forEach(objRndSupplier.Catalogue.split("|"), function (val) { $scope.Catalogues.push({ 'result': val }) });
            //}
        }

        //if (objRndSupplier.Catalogue != '' && objRndSupplier.Catalogue != null)
        //    $scope.Catalogues = objRndSupplier.Catalogue.split("|")



        $scope.objRndSupplier = {
            RNDSupplierId: objRndSupplier.RNDSupplierId,
            RNDProductId: objRndSupplier.RNDProductId,
            CompanyName: objRndSupplier.CompanyName,
            MobileNo: $scope.mobile.toString(),
            Email: $scope.Email.toString(),
            CommunicationRecord: $scope.CommunicationRecords.toString(),
            TeliPhoneNo: $scope.TeliPhoneNo.toString(),
            chat: $scope.chat.toString(),
            ContactEmail: $scope.ContactEmail.toString(),
            Address: objRndSupplier.Address,
            Attende: objRndSupplier.Attende,
            Website: objRndSupplier.Website,
            CityId: objRndSupplier.CityId,
            Price: objRndSupplier.Price,
            Pincode: objRndSupplier.Pincode,
            Remark: objRndSupplier.Remark,
            Quotations: $scope.QuotationFiles.toString(),
            Catalogue: $scope.Catalogues.toString(),
            Status: objRndSupplier.Status,
            UnitId: objRndSupplier.UnitId,
            CountryData: { Display: objRndSupplier.CountryData.Display, Value: objRndSupplier.CountryData.Value },
            StateData: { Display: objRndSupplier.StateData.Display, Value: objRndSupplier.StateData.Value },
            CityData: { Display: objRndSupplier.CityData.Display, Value: objRndSupplier.CityData.Value },
            CurrencyData: { Display: objRndSupplier.CurrencyData.Display, Value: objRndSupplier.CurrencyData.Value },
            SupplierData: { Display: objRndSupplier.SupplierData.Display, Value: objRndSupplier.SupplierData.Value }
        };
        $scope.CreateUpdate = function (data) {

            // Quotations
            var Quotations = $scope.QuotationFiles.map(function (item) {
                //var datacatalog = item['result'].split('/');
                //return datacatalog[datacatalog.length - 1];
                return item['result']
            });
            data.Quotations = Quotations.join("|")

            var Catalogue = $scope.Catalogues.map(function (item) {
                //var datacatalog = item['result'].split('/');
                //return datacatalog[datacatalog.length - 1];
                return item['result']
            });
            data.Catalogue = Catalogue.join("|")


            data.CityId = data.CityData.Value;
            data.CompanyName = data.SupplierData.Display;
            //data.RNDSupplierId = data.SupplierData.Value;
            data.UnitId = data.CurrencyData.Value;

            data.TeliPhoneNo = this.TeliPhoneNo.toString();
            data.MobileNo = this.mobile.toString();
            data.Email = this.Email.toString();
            data.chat = this.chat.toString();
            data.ContactEmail = this.ContactEmail.toString();
            data.Website = this.Website.toString();
            data.CommunicationRecord = this.CommunicationRecords.toString().replace(',', '|');
            if (RndProductCtrl.EditRndProductSupplierIndex > -1) {
                RndProductCtrl.objRndProduct.objRndSupplierList[RndProductCtrl.EditRndProductSupplierIndex] = data;
                RndProductCtrl.EditRndProductSupplierIndex = -1;
            } else {
                data.Status = 1;
                RndProductCtrl.objRndProduct.objRndSupplierList.push(data);
            }
            $scope.close();
        }

        $scope.$watch('objRndSupplier.SupplierData', function (val) {
            if (val.Value && val.Value > 0) {
                RndProductService.GetCompanyDetailById(val.Value).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        $scope.objRndSupplier.Address = result.data.DataList.objRndSupplierDetail.Address;
                        //$scope.objRndSupplier.Website = result.data.DataList.objRndSupplierDetail.Website;
                        $scope.Email = (result.data.DataList.objRndSupplierDetail.Email != '') ? result.data.DataList.objRndSupplierDetail.Email.split(",") : [];
                        $scope.TeliPhoneNo = (result.data.DataList.objRndSupplierDetail.MobileNo != '') ? result.data.DataList.objRndSupplierDetail.MobileNo.split(",") : [];
                        $scope.Website = (result.data.DataList.objRndSupplierDetail.Website != '' && result.data.DataList.objRndSupplierDetail.Website != null) ? result.data.DataList.objRndSupplierDetail.Website.split(",") : [];

                    } else if (result.ResponseType == 3) {
                        toastr.error(result.data.Message, 'Opps, Something went wrong');
                    }
                }, function (error) {
                    $rootScope.errorHandler(error)
                })
            }
        })


        //$scope.saveSupplier = function (isAdd) {
        //    if (objRndSupplier.RNDProductId == 0) {
        //        $scope.objRndSupplier.MobileNo = this.mobile13.toString();
        //        $scope.objRndSupplier.Email = this.mail13.toString();
        //        $scope.objRndSupplier.Quotations = $scope.QuotationFiles == undefined ? '' : $scope.QuotationFiles.map(function (item) { return item['result'] }).join("|");
        //        RndProductCtrl.objRndProduct.objRndSupplierList.push($scope.objRndSupplier);
        //    }
        //    else {
        //        $scope.objRndSupplier.MobileNo = this.mobile13.toString();
        //        $scope.objRndSupplier.Email = this.mail13.toString();
        //        $scope.objRndSupplier.Quotations = $scope.QuotationFiles == undefined ? '' : $scope.QuotationFiles.map(function (item) { return item['result'] }).join("|");
        //        RndProductCtrl.objRndProduct.objRndSupplierList[RndProductCtrl.EditSupplierIndex] = $scope.objRndSupplier;
        //    }
        //    $scope.close();
        //}
        ////BEGIN MANAGE PRODUCT Parameter
        ////Add Parameter DETAIL
        //$scope.AddProductCommunication = function (data) {
        //    $scope.parasubmitt = true;
        //    if (data.CommunicationRecord != '' && data.CommunicationRecord != null) {
        //        $scope.parasubmitt = false;

        //        if ($scope.EditProductParameterIndex > -1) {
        //            if ($scope.objRndSupplier.objRndSupplierCommunicate[$scope.EditProductParameterIndex].Status == 2) {
        //                data.Status = 2;
        //            } else if ($scope.objRndSupplier.objRndSupplierCommunicate[$scope.EditProductParameterIndex].Status == 1 ||
        //                       $scope.objRndSupplier.objRndSupplierCommunicate[$scope.EditProductParameterIndex].Status == undefined) {
        //                data.Status = 1;
        //            }
        //            $scope.objRndSupplier.objRndSupplierCommunicate[$scope.EditProductParameterIndex] = data;
        //            $scope.EditProductParameterIndex = -1;
        //        } else {
        //            data.Status = 1;
        //            $scope.objRndSupplier.objRndSupplierCommunicate.push(data);
        //        }
        //        $scope.objRndSupplier = {
        //            CommunicationRecord: "",
        //        };
        //    }
        //}

        ////EDIT Parameter DETAIL
        //$scope.EditProductParameter = function (data, index) {
        //    $scope.EditProductParameterIndex = index;
        //    $scope.ProductParameter = {
        //        TechDetailId: data.TechDetailId,
        //        ProductId: data.ProductId,
        //        TechParaId: data.TechParaId,
        //        TechSpec: data.TechSpec,
        //        Value: data.Value,
        //        QueData: { Display: data.TechSpec, Value: data.TechParaId },
        //    }
        //}

        ////DELETE Parameter DETAIL
        //$scope.DeleteProductParameter = function (data, index) {
        //    $scope.$apply(function () {
        //        if (data.Status == 2) {
        //            data.Status = 3;
        //            $scope.objProduct.ProductParameterMasters[index] = data;
        //        } else {
        //            $scope.objProduct.ProductParameterMasters.splice(index, 1);
        //        }
        //        toastr.success("Technical Specification Delete", "Success");
        //    })
        //}
        ////END MANAGE PRODUCT Parameter
        $scope.close = function () {
            $uibModalInstance.close();
        };
        $scope.ok = function () {
            $uibModalInstance.close();
        };
        $scope.deleteQuotationFile = function (idx) {
            var file = $scope.QuotationFiles[idx];
            $scope.QuotationFiles.splice(idx, 1);
        }
        $scope.uploadQuotationFile = function (files, errFiles) {
            if (files.length > 0) {
                angular.forEach(files, function (file) {
                    $scope.QuotationFiles.push(file);
                });
            }
            else {
                return;
            }

            $scope.errQuotationFiles = errFiles;
            angular.forEach(files, function (file) {
                file.upload = Upload.upload({
                    url: "/Handler/FileUpload.ashx",
                    method: 'POST',
                    data: { file: file },
                });
                file.upload.then(function (response) {
                    file.result = "/UploadImages/TempImg/" + response.data[0].imageName;
                }, function (response) {
                    if (response.status > 0)
                        $scope.errorMsg = response.status + ': ' + response.data;
                }, function (evt) {
                    file.progressq = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                })
            });
        }
        $scope.deleteCatalogueFile = function (idx) {
            var file = $scope.Catalogues[idx];
            $scope.Catalogues.splice(idx, 1);
        }
        $scope.uploadCatalogueFile = function (files, errFiles) {
            if (files.length > 0) {
                angular.forEach(files, function (file) {
                    $scope.Catalogues.push(file);
                });
            }
            else {
                return;
            }

            $scope.errCatalogueFiles = errFiles;
            angular.forEach(files, function (file) {
                file.upload = Upload.upload({
                    url: "/Handler/FileUpload.ashx",
                    method: 'POST',
                    data: { file: file },
                });
                file.upload.then(function (response) {
                    file.result = "/UploadImages/TempImg/" + response.data[0].imageName;
                }, function (response) {
                    if (response.status > 0)
                        $scope.errorMsg = response.status + ': ' + response.data;
                }, function (evt) {
                    file.progressq = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                })
            });
        }
    }
    RndSupplierModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'RndProductCtrl', 'RndProductService', 'telCodeData', 'EmailCodeData', 'emailAddressData', 'mobileCodeData', 'chatCodeData', 'WebAddressData', 'Upload', 'objRndSupplierList']

    //var RndSupplierModalInstanceCtrl = function ($scope, $uibModalInstance, RndProductCtrl, RndProductService, telCodeData, Upload, objRndSupplier) {
    //    $scope.telCodeData = telCodeData;
    //    $scope.objRndSupplier = objRndSupplier;

    //    $scope.mobile13 = ($scope.objRndSupplier.MobileNo != '') ? $scope.objRndSupplier.MobileNo.split("|") : [];
    //    $scope.mail13 = ($scope.objRndSupplier.Email != '') ? $scope.objRndSupplier.Email.split("|") : [];
    //    $scope.QuotationFiles = [];
    //    if ($scope.objRndSupplier.Quotations) {
    //        _.forEach($scope.objRndSupplier.Quotations.split("|"), function (val) { $scope.QuotationFiles.push({ 'result': val }) });
    //    }
    //    $scope.QuotationFiles = $scope.objRndSupplier.Quotations.split("|")
    //    $scope.saveSupplier = function (isAdd) {
    //        if (objRndSupplier.RNDProductId == 0) {
    //            $scope.objRndSupplier.MobileNo = this.mobile13.toString();
    //            $scope.objRndSupplier.Email = this.mail13.toString();
    //            $scope.objRndSupplier.Quotations = $scope.QuotationFiles == undefined ? '' : $scope.QuotationFiles.map(function (item) { return item['result'] }).join("|");
    //            RndProductCtrl.objRndProduct.objRndSupplierList.push($scope.objRndSupplier);
    //        }
    //        else {
    //            $scope.objRndSupplier.MobileNo = this.mobile13.toString();
    //            $scope.objRndSupplier.Email = this.mail13.toString();
    //            $scope.objRndSupplier.Quotations = $scope.QuotationFiles == undefined ? '' : $scope.QuotationFiles.map(function (item) { return item['result'] }).join("|");
    //            RndProductCtrl.objRndProduct.objRndSupplierList[RndProductCtrl.EditSupplierIndex] = $scope.objRndSupplier;
    //        }
    //        $scope.close();
    //    }
    //    $scope.close = function () {
    //        $uibModalInstance.close();
    //    };
    //    $scope.ok = function () {
    //        $uibModalInstance.close();
    //    };
    //    $scope.deleteQuotationFile = function (idx) {
    //        var file = $scope.images[idx];
    //        $scope.images.splice(idx, 1);
    //    }
    //    $scope.uploadQuotationFile = function (files, errFiles) {
    //        if (files.length > 0) {
    //            angular.forEach(files, function (file) {
    //                $scope.QuotationFiles.push(file);
    //            });
    //        }
    //        else {
    //            return;
    //        }

    //        $scope.errQuotationFiles = errFiles;
    //        angular.forEach(files, function (file) {
    //            file.upload = Upload.upload({
    //                url: "/Handler/FileUpload.ashx",
    //                method: 'POST',
    //                data: { file: file },
    //            });
    //            file.upload.then(function (response) {
    //                file.result = response.data[0].imageName;
    //            }, function (response) {
    //                if (response.status > 0)
    //                    $scope.errorMsg = response.status + ': ' + response.data;
    //            }, function (evt) {
    //                file.progressq = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
    //            })
    //        });
    //    }
    //}
    //RndSupplierModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'RndProductCtrl', 'RndProductService', 'telCodeData', 'Upload', 'objRndSupplier']
})();