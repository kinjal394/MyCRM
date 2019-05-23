(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("SupplierCtrl", [
         "$scope", "$rootScope", "$timeout", "$filter", "SupplierService", "$uibModal", "CountryService",
         SupplierCtrl
        ]);

    function SupplierCtrl($scope, $rootScope, $timeout, $filter, SupplierService, $uibModal, CountryService) {

        $scope.objSupplier = $scope.objSupplier || {};
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};
        $scope.objSupplier = {
            SupplierId: 0,
            CompanyName: '',
            Address: '',
            CountryId: 0,
            StateId: 0,
            CityId: 0,
            AreaId: 0,
            PinCode: '',
            Fax: '',
            Website: '',
            CountryData: { Display: '', Value: '' },
            StateData: { Display: '', Value: '' },
            CityData: { Display: '', Value: '' },
            AreaData: { Display: '', Value: '' },
            SupplierAddressDetails: [],
            SupplierContactDetails: [],
            SupplierChatDetails: [],
            SupplierBankDetails: []
        };
        $scope.objSupplierAddress = {
            AddressId: 0,
            SupplierId: 0,
            CountryId: 0,
            StateId: 0,
            CityId: 0,
            CountryName: '',
            StateName: '',
            CityName: '',
            Address: '',
            PinCode: '',
            Fax: '',
            Status: 0, //1 : Insert , 2:Update ,3 :Delete
            CountryData: { Display: '', Value: '' },
            StateData: { Display: '', Value: '' },
            CityData: { Display: '', Value: '' }
        };
        $scope.objSupplierChatDetail = {
            SupplierChatId: 0,
            SupplierId: 0,
            ChatId: 0,
            ChatName: '',
            ChatValue: '',
            Status: 0, //1 : Insert , 2:Update ,3 :Delete
            ChatData: { Display: '', Value: '' },
        };
        $scope.telCodeData = [];

        $scope.SetSupplierId = function (id, isdisable) {
            CountryService.GetCountryFlag().then(function (result) {
                $scope.telCodeData = angular.copy(result);
                if (id > 0) {
                    $scope.SrNo = id;
                    $scope.addMode = false;
                    $scope.saveText = "Update";
                    $scope.GetAllSupplierInfoById(id);
                    $scope.isClicked = false;
                    if (isdisable == 1) {
                        $scope.isClicked = true;
                    }
                } else {
                    $scope.SrNo = 0;
                    $scope.addMode = true;
                    $scope.saveText = "Save";
                    $scope.isClicked = false;
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.GetAllSupplierInfoById = function (id) {
            SupplierService.GetAllSupplierInfoById(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    var objSupplierMaster = result.data.DataList.objSupplierMaster;
                    $scope.objSupplier = {
                        SupplierId: objSupplierMaster.SupplierId,
                        CompanyName: objSupplierMaster.CompanyName,
                        Website: objSupplierMaster.Website
                    };

                    $scope.objSupplier.SupplierAddressDetails = [];
                    angular.forEach(result.data.DataList.objSupplierAddressDetail, function (value) {
                        var objSupplierAddress = {
                            AddressId: value.AddressId,
                            SupplierId: value.SupplierId,
                            Address: value.Address,
                            CountryId: value.CountryId,
                            StateId: value.StateId,
                            CityId: value.CityId,
                            CountryName: value.CountryName,
                            StateName: value.StateName,
                            CityName: value.CityName,
                            AreaId: value.AreaId,
                            PinCode: value.PinCode,
                            Fax: value.Fax,
                            Status: 2,//1 : Insert , 2:Update ,3 :Delete
                            CountryData: {
                                Display: value.CountryName,
                                Value: value.CountryId
                            },
                            StateData: {
                                Display: value.StateName,
                                Value: value.StateId
                            },
                            CityData: {
                                Display: value.CityName,
                                Value: value.CityId
                            }
                        }
                        $scope.objSupplier.SupplierAddressDetails.push(objSupplierAddress);
                    }, true);

                    $scope.objSupplier.SupplierContactDetails = [];
                    angular.forEach(result.data.DataList.objSupplierContactDetail, function (value) {
                        var objContactDetail = {
                            ContactId: value.ContactId,
                            SupplierId: value.SupplierId,
                            ContactName: value.ContactName,
                            Surname: value.Surname,
                            MobileNo: value.MobileNo,
                            Email: value.Email,
                            Status: 2
                        }
                        $scope.objSupplier.SupplierContactDetails.push(objContactDetail);
                    }, true);

                    $scope.objSupplier.SupplierBankDetails = [];
                    angular.forEach(result.data.DataList.objSupplierBankDetail, function (value) {
                        var objBankDetail = {
                            BankDetailId: value.BankDetailId,
                            SupplierId: value.SupplierId,
                            BeneficiaryName: value.BeneficiaryName,
                            NickName: value.NickName,
                            BankNameId: value.BankNameId,
                            BankName: value.BankName,
                            BranchName: value.BranchName,
                            AccountNo: value.AccountNo,
                            IFSCCode: value.IFSCCode,
                            SwiftCode: value.SwiftCode,
                            AccountTypeId: value.AccountTypeId,
                            AccountType: value.AccountType,
                            Status: 2,//1 : Insert , 2:Update ,3 :Delete
                            BankData: { Display: value.BankName, Value: value.BankNameId },
                            AccountTypeData: { Display: value.AccountType, Value: value.AccountTypeId }
                        }
                        $scope.objSupplier.SupplierBankDetails.push(objBankDetail);
                    }, true);

                    $scope.objSupplier.SupplierChatDetails = [];
                    angular.forEach(result.data.DataList.objSupplierChatDetail, function (value) {
                        var objChatDetail = {
                            SupplierChatId: value.SupplierChatId,
                            SupplierId: value.SupplierId,
                            ChatId: value.ChatId,
                            ChatName: value.ChatName,
                            ChatValue: value.ChatValue,
                            Status: 2, //1 : Insert , 2:Update ,3 :Delete
                            ChatData: { Display: value.ChatName, Value: value.ChatId },
                        }
                        $scope.objSupplier.SupplierChatDetails.push(objChatDetail);
                    }, true);

                    $scope.storage = angular.copy($scope.objSupplier);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }

            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.Add = function () {
            window.location.href = "/master/Supplier/AddSupplier";
        }

        function ResetForm() {
            $scope.objSupplier = {
                SupplierId: ($scope.SrNo && $scope.SrNo > 0) ? $scope.SrNo : 0,
                CompanyName: '',
                Address: '',
                CountryId: 0,
                StateId: 0,
                CityId: 0,
                AreaId: 0,
                PinCode: '',
                Fax: '',
                Website: '',
                SupplierAddressDetails: [],
                SupplierContactDetails: [],
                SupplierBankDetails: [],
                SupplierChatDetails: [],
                CountryData: { Display: '', Value: '' },
                StateData: { Display: '', Value: '' },
                CityData: { Display: '', Value: '' },
                AreaData: { Display: '', Value: '' }
            };
            //ContactId,SupplierId,ContactName,MobileNo,QQCode,Skype,IsActive,Surname,Email
            $scope.objSupplierAddress = {
                AddressId: 0,
                SupplierId: 0,
                CountryId: 0,
                StateId: 0,
                CityId: 0,
                CountryName: '',
                StateName: '',
                CityName: '',
                Address: '',
                PinCode: '',
                Fax: '',
                Status: 0, //1 : Insert , 2:Update ,3 :Delete
                CountryData: { Display: '', Value: '' },
                StateData: { Display: '', Value: '' },
                CityData: { Display: '', Value: '' }
            };
            $scope.SupplierContact = {
                ContactId: 0,
                SupplierId: 0,
                ContactName: '',
                Surname: '',
                MobileNo: '',
                Email: '',
                //QQCode: '',
                //Skype: '',
                Status: 0 //1 : Insert , 2:Update ,3 :Delete
            };
            $scope.SupplierBank = {
                BankDetailId: 0,
                SupplierId: 0,
                BeneficiaryName: '',
                NickName: '',
                BankName: '',
                BankNameId: '',
                BranchName: '',
                AccountNo: '',
                AccountTypeId: 0,
                // AccountNo: '',
                IFSCCode: '',
                SwiftCode: '',
                Status: 0, //1 : Insert , 2:Update ,3 :Delete
                BankData: { Display: '', Value: '' },
                AccountTypeData: { Display: '', Value: '' }
            };
            $scope.objSupplierChatDetail = {
                SupplierChatId: 0,
                SupplierId: 0,
                ChatId: 0,
                ChatName: '',
                ChatValue: '',
                Status: 0, //1 : Insert , 2:Update ,3 :Delete
                ChatData: { Display: '', Value: '' },
            };

            if ($scope.FormSupplierInfo)
                $scope.FormSupplierInfo.$setPristine();
            $scope.addMode = true;
            $scope.isFirstFocus = false;
            $scope.storage = {};
            $timeout(function () {
                $scope.isFirstFocus = true;
            });
            $scope.EditSupplierContactIndex = -1;
        }
        ResetForm();

        $scope.Reset = function () {
            if ($scope.addMode == false) {
                $scope.objSupplier = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };

        $scope.RefreshTable = function () {
            $scope.tableParams.reload();
        };

        $scope.CreateUpdate = function (data) {
            //data.CountryId = data.CountryData.Value;
            //data.CountryName = data.CountryData.Display;
            //data.StateId = data.StateData.Value;
            //data.StateName = data.StateData.Display;
            //data.CityId = data.CityData.Value;
            //data.CityName = data.CityData.Display;
            //data.AreaId = (data.AreaData.Value == "") ? 0 : data.AreaData.Value;
            //data.AreaName = data.AreaData.Display;
            $scope.objSupplierChatDetail.ChatData.Display = ' ';
            SupplierService.CreateUpdate(data).then(function (result) {
                if (result.data.ResponseType == 1) {
                    ResetForm();
                    toastr.success(result.data.Message);
                    if (data.SupplierId == 0) {
                        window.location.href = "/master/Supplier";
                    }
                    if (data.SupplierId > 0)
                        window.location.href = "/master/Supplier";
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.gridObj = {
            columnsInfo: [
               { "title": "Supplier Id", "data": "SupplierId", filter: false, visible: false },
               { "title": "Sr.", "data": "RowNumber", filter: false, sort: false },
               { "title": "Company Name", "data": "CompanyName" },
               { "title": "District", "data": "CityName" },
               { "title": "Contact Person", "data": "Person" },
               { "title": "Mobile No", "data": "MobileNo" },
               { "title": "Email Id", "data": "Email", sort: true, filter: true, "render": "<p class='MailTo' ng-bind-html='getHtml($parent.$parent.$parent.FormatEmail(row.Email))'>" },
               {
                   "title": "Action", filter: false, sort: false,
                   'render': '<a   class="btn btn-primary btn-xs" data-ng-click="$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                             //'<a class="btn btn-danger btn-xs"  data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.SupplierId)" data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                             '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
               }
            ],
            Sort: { 'SupplierId': 'asc' }
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

        $scope.Edit = function (data) {
            window.location.href = "/master/Supplier/AddSupplier/" + data.SupplierId + "/" + 0;
        }
        $scope.View = function (data) {
            window.location.href = "/master/Supplier/AddSupplier/" + data.SupplierId + "/" + 1;
        }

        $scope.Delete = function (id) {
            SupplierService.DeleteById(id).then(function (result) {
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

        //BEGIN MANAGE SUPPLIER ADDRESS INFORMATION
        $scope.AddSupplierAddressDetail = function (data) {
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'SupplierAddressDetail.html',
                controller: AddressModalInstanceCtrl,
                resolve: {
                    SupplierCtrl: function () {
                        return $scope;
                    },
                    SupplierAddressData: function () {
                        return data;
                    }
                }
            });
            modalInstance.result.then(function () {
                $scope.EditSupplierAddressIndex = -1;
            }, function () {
                $scope.EditSupplierAddressIndex = -1;
            })
        }

        $scope.DeleteSupplierAddressDetail = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objSupplier.SupplierAddressDetails[index] = data;
                } else {
                    $scope.objSupplier.SupplierAddressDetails.splice(index, 1);
                }
                toastr.success("Supplier Address Detail Delete", "Success");
            })
        }

        $scope.EditSupplierAddressDetail = function (data, index) {
            $scope.EditSupplierAddressIndex = index;
            $scope.AddSupplierAddressDetail(data);
        }
        //END MANAGE SUPPLIER ADDRESS INFORMATION

        //BEGIN MANAGE SUPPLIER CONTACT DETAIL
        $scope.AddSupplierContactDetail = function (data) {
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'SupplierContactDetail.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    SupplierCtrl: function () { return $scope; },
                    SupplierContactData: function () { return data; }
                }
            });
            modalInstance.result.then(function () {
                $scope.EditSupplierContactIndex = -1;
            }, function () {
                $scope.EditSupplierContactIndex = -1;
            })
        }

        $scope.DeleteSupplierDetail = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objSupplier.SupplierContactDetails[index] = data;
                } else {
                    $scope.objSupplier.SupplierContactDetails.splice(index, 1);
                }
                toastr.success("Supplier contact detail Delete", "Success");
            })
        }

        $scope.EditSupplierDetail = function (data, index) {

            $scope.EditSupplierContactIndex = index;
            $scope.AddSupplierContactDetail(data);
        }
        //END MANAGE SUPPLIER CONTACT DETAIL

        $scope.statusFilter = function (val) {
            if (val.Status != 3)
                return true;
        }

        //BEGIN MANAGE SUPPLIER BANK DETAIL
        $scope.AddSupplierBankDetail = function (data) {

            var BankmodalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'SupplierBankDetail.html',
                controller: BankModalInstanceCtrl,
                resolve: {
                    SupplierCtrl: function () { return $scope; },
                    SupplierBankData: function () { return data; }
                }
            });
            BankmodalInstance.result.then(function () {
                $scope.EditSupplierBankIndex = -1;
            }, function () {
                $scope.EditSupplierBankIndex = -1;
            })
        }

        $scope.DeleteSupplierBankDetail = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objSupplier.SupplierBankDetails[index] = data;
                } else {
                    $scope.objSupplier.SupplierBankDetails.splice(index, 1);
                }
                toastr.success("Supplier Bank Detail Delete", "Success");
            })
        }

        $scope.EditSupplierBankDetail = function (data, index) {
            $scope.EditSupplierBankIndex = index;
            $scope.AddSupplierBankDetail(data);
        }
        //END MANAGE SUPPLIER BANK DETAIL

        //ADD Chat DETAIL
        $scope.AddSupplierChatDetail = function (data) {
            $scope.submittedd = true;
            if (data.ChatValue != undefined && data.ChatValue != "") {
                $scope.submittedd = false;
                var ChatDetails = {
                    SupplierChatId: parseInt(data.SupplierChatId),
                    //ProductId: parseInt(data.ProductData.Value),
                    //AdSourceId: 1, // By default 1 - Video
                    ChatId: data.ChatData.Value,
                    ChatName: data.ChatData.Display,
                    ChatValue: data.ChatValue
                };

                if ($scope.EditSupplierChatDetailIndex > -1) {
                    if ($scope.objSupplier.SupplierChatDetails[$scope.EditSupplierChatDetailIndex].Status == 2) {
                        ChatDetails.Status = 2;
                    } else if ($scope.objSupplier.SupplierChatDetails[$scope.EditSupplierChatDetailIndex].Status == 1 ||
                               $scope.objSupplier.SupplierChatDetails[$scope.EditVideoUrlDetailIndex].Status == undefined) {
                        ChatDetails.Status = 1;
                    }
                    $scope.objSupplier.SupplierChatDetails[$scope.EditSupplierChatDetailIndex] = ChatDetails;
                    $scope.EditSupplierChatDetailIndex = -1;
                } else {
                    ChatDetails.Status = 1;
                    $scope.objSupplier.SupplierChatDetails.push(ChatDetails);
                }
                //$scope.VideoUrlDetails = {
                //    AdId: 0,
                //    ProductId: 0,
                //    AdSourceId: 0,
                //    VUrl: '',
                //    VRemark: '',
                //    Status: 0
                //};
                $scope.objSupplierChatDetail.ChatId = '';
                $scope.objSupplierChatDetail.ChatValue = '';
                $scope.objSupplierChatDetail.ChatName = '';
                $scope.objSupplierChatDetail.ChatData = {
                    Display: '', Value: ''
                };
            }
        }

        //EDIT Chat DETAIL
        $scope.EditSupplierChatDetail = function (data, index) {
            $scope.EditSupplierChatDetailIndex = index;
            $scope.objSupplierChatDetail.ChatId = data.ChatId;
            $scope.objSupplierChatDetail.ChatValue = data.ChatValue;
            $scope.objSupplierChatDetail.ChatName = data.ChatName;
            $scope.objSupplierChatDetail.SupplierChatId = data.SupplierChatId;
            $scope.objSupplierChatDetail.ChatData = { Display: data.ChatName, Value: data.ChatId };
        }

        //DELETE Chat DETAIL
        $scope.DeleteSupplierChatDetail = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objSupplier.SupplierChatDetails[index] = data;
                } else {
                    $scope.objSupplier.SupplierChatDetails.splice(index, 1);
                }
                toastr.success("Chat Details Delete", "Success");
            })
        }
    }

    var AddressModalInstanceCtrl = function ($scope, $uibModalInstance, SupplierCtrl, SupplierAddressData, CityService) {

        $scope.$watch('objSupplierAddress.CountryData', function (data) {
            if (data) {
                if (data.Value != SupplierAddressData.CountryId.toString()) {
                    $scope.objSupplierAddress.StateData.Display = '';
                    $scope.objSupplierAddress.StateData.Value = '';
                    $scope.objSupplierAddress.CityData.Display = '';
                    $scope.objSupplierAddress.CityData.Value = '';
                }
            } else {
                $scope.CountryBind('India');
            }
            
        }, true)

        $scope.$watch('objSupplierAddress.StateData', function (data) {
            if (data.Value != SupplierAddressData.StateId.toString()) {
                $scope.objSupplierAddress.CityData.Display = '';
                $scope.objSupplierAddress.CityData.Value = '';
            }
        }, true)

        $scope.CountryBind = function (data) {
            CityService.CountryBind().then(function (result) {
                if (result.data.ResponseType == 1) {
                    _.each(result.data.DataList, function (value, key, list) {
                        if (value.CountryName.toString().toLowerCase() == data.toString().toLowerCase()) {
                            $scope.objSupplierAddress.CountryData = {
                                Display: value.CountryName,
                                Value: value.CountryId
                            };
                            return forEach.break();
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

        $scope.objSupplierAddress = {
            AddressId: SupplierAddressData.AddressId,
            SupplierId: SupplierAddressData.SupplierId,
            CountryId: SupplierAddressData.CountryId,
            StateId: SupplierAddressData.StateId,
            CityId: SupplierAddressData.CityId,
            CountryName: SupplierAddressData.CountryName,
            StateName: SupplierAddressData.StateName,
            CityName: SupplierAddressData.CityName,
            Address: SupplierAddressData.Address,
            PinCode: SupplierAddressData.PinCode,
            Fax: SupplierAddressData.Fax,
            Status: SupplierAddressData.Status,
            CountryData: { Display: SupplierAddressData.CountryName, Value: SupplierAddressData.CountryId },
            StateData: { Display: SupplierAddressData.StateName, Value: SupplierAddressData.StateId },
            CityData: { Display: SupplierAddressData.CityName, Value: SupplierAddressData.CityId }
        };


        $scope.close = function () {
            $uibModalInstance.close();
        };

        $scope.CreateUpdate = function (data) {
            data.CountryId = data.CountryData.Value;
            data.CountryName = data.CountryData.Display;
            data.StateId = data.StateData.Value;
            data.StateName = data.StateData.Display;
            data.CityId = data.CityData.Value;
            data.CityName = data.CityData.Display;

            if (SupplierCtrl.EditSupplierAddressIndex > -1) {
                SupplierCtrl.objSupplier.SupplierAddressDetails[SupplierCtrl.EditSupplierAddressIndex] = data;
                SupplierCtrl.EditSupplierAddressIndex = -1;
            } else {
                data.Status = 1;
                SupplierCtrl.objSupplier.SupplierAddressDetails.push(data);
            }
            $scope.close();
        }

    }
    AddressModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'SupplierCtrl', 'SupplierAddressData', 'CityService']

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, SupplierCtrl, SupplierContactData, CountryService) {

        $scope.telCodeData = SupplierCtrl.telCodeData;
        $scope.teliphone = (SupplierContactData.MobileNo != '') ? SupplierContactData.MobileNo.split(",") : [];
        $scope.mail = (SupplierContactData.Email != '') ? SupplierContactData.Email.split(",") : [];

        $scope.objSupplierContact = {
            ContactId: SupplierContactData.ContactId,
            SupplierId: SupplierContactData.SupplierId,
            Surname: SupplierContactData.Surname,
            ContactName: SupplierContactData.ContactName,
            Email: $scope.mail.toString(),
            MobileNo: $scope.teliphone.toString(),
            AccountTypeId: SupplierContactData.AccountTypeId,
            QQCode: SupplierContactData.QQCode,
            Skype: SupplierContactData.Skype,
            Status: SupplierContactData.Status //1 : Insert , 2:Update ,3 :Delete
        };

        $scope.close = function () {
            $uibModalInstance.close();
        };

        $scope.CreateUpdate = function (data) {
            data.MobileNo = this.teliphone.toString();
            data.Email = this.mail.toString();
            if (SupplierCtrl.EditSupplierContactIndex > -1) {
                SupplierCtrl.objSupplier.SupplierContactDetails[SupplierCtrl.EditSupplierContactIndex] = data;
                SupplierCtrl.EditSupplierContactIndex = -1;
            } else {
                data.Status = 1;
                SupplierCtrl.objSupplier.SupplierContactDetails.push(data);
            }
            //$scope.objSupplierContact = {
            //    ContactId: 0,
            //    SupplierId: 0,
            //    ContactName: '',
            //    Surname: '',
            //    MobileNo: '',
            //    Email: '',
            //    QQCode: '',
            //    Skype: '',
            //    Status: 0
            //};
            $scope.close();
        }
    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'SupplierCtrl', 'SupplierContactData', 'CountryService']

    var BankModalInstanceCtrl = function ($scope, $uibModalInstance, SupplierCtrl, SupplierBankData) {

        $scope.objSupplierBank = {
            BankDetailId: SupplierBankData.BankDetailId,
            SupplierId: SupplierBankData.SupplierId,
            BeneficiaryName: SupplierBankData.BeneficiaryName,
            NickName: SupplierBankData.NickName,
            BankNameId: SupplierBankData.BankNameId,
            BankName: SupplierBankData.BankName,
            BranchName: SupplierBankData.BranchName,
            AccountNo: SupplierBankData.AccountNo,
            AccountTypeId: SupplierBankData.AccountTypeId,
            IFSCCode: SupplierBankData.IFSCCode,
            SwiftCode: SupplierBankData.SwiftCode,
            Status: SupplierBankData.Status, //1 : Insert , 2:Update ,3 :Delete
            BankData: {
                Display: SupplierBankData.BankName, Value: SupplierBankData.BankNameId
            },
            AccountTypeData: {
                Display: SupplierBankData.AccountType, Value: SupplierBankData.AccountTypeId
            }
        };

        $scope.close = function () {
            $uibModalInstance.close();
        };

        $scope.CreateUpdate = function (data) {
            data.BankNameId = data.BankData.Value;
            data.BankName = data.BankData.Display;
            data.AccountTypeId = data.AccountTypeData.Value;
            data.AccountType = data.AccountTypeData.Display;

            if (SupplierCtrl.EditSupplierBankIndex > -1) {
                SupplierCtrl.objSupplier.SupplierBankDetails[SupplierCtrl.EditSupplierBankIndex] = data;
                SupplierCtrl.EditSupplierBankIndex = -1;
            } else {
                data.Status = 1;
                SupplierCtrl.objSupplier.SupplierBankDetails.push(data);
            }
            //$scope.objSupplierBank = {
            //    BankDetailId: 0,
            //    SupplierId: 0,
            //    BeneficiaryName: '',
            //    NickName: '',
            //    BankName: '',
            //    BranchName: '',
            //    AccountNo: '',
            //    IFSCCode: '',
            //    SwiftCode: '',
            //    Status: 0
            //};
            $scope.close();
        }
    }
    BankModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'SupplierCtrl', 'SupplierBankData']
})()


