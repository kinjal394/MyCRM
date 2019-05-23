(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
            .controller("VendorCtrl", [
             "$scope", "$rootScope", "$timeout", "$filter", "VendorService", "$uibModal", "CountryService",
             VendorCtrl
            ]);

    function VendorCtrl($scope, $rootScope, $timeout, $filter, VendorService, $uibModal, CountryService) {
        $scope.dfg = [];
        $scope.objVendor = $scope.objVendor || {};
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};
        $scope.objVendor = {
            VendorId: 0,
            AgencyTypeId: 0,
            AgencyType: '',
            CompanyName: '',
            Website: '',
            Remark: '',
            GST: '',
            PAN: '',
            TAN: '',
            ServiceTaxNo: '',
            AgencyTypeData: { Display: '', Value: '' },
            VendorContactDetails: [],
            VendorBankDetails: [],
            VendorAddressDetails: [],
            VendorChatDetails: []
        };
        $scope.objVendorAddress = {
            AddressId: 0,
            VendorId: 0,
            CountryId: 0,
            StateId: 0,
            CityId: 0,
            CountryName: '',
            StateName: '',
            CityName: '',
            Address: '',
            Pincode: '',
            Fax: '',
            Status: 0, //1 : Insert , 2:Update ,3 :Delete
            CountryData: { Display: '', Value: '' },
            StateData: { Display: '', Value: '' },
            CityData: { Display: '', Value: '' }
        };
        $scope.objVendorChatDetail = {
            VendorChatId: 0,
            VendorId: 0,
            ChatId: 0,
            ChatName: '',
            ChatValue: '',
            Status: 0, //1 : Insert , 2:Update ,3 :Delete
            ChatData: { Display: '', Value: '' },
        };

        $scope.MobileData = [];

        $scope.SetVendorId = function (id, isdisable) {
            CountryService.GetCountryFlag().then(function (result) {
                $scope.MobileData = angular.copy(result);
                if (id > 0) {
                    $scope.SrNo = id;
                    $scope.addMode = false;
                    $scope.saveText = "Update";
                    $scope.GetAllVendorInfoById(id);
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

        $scope.GetAllVendorInfoById = function (id) {
            VendorService.GetAllVendorInfoById(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    var objVendorMaster = result.data.DataList.objVendorMaster;
                    $scope.objVendor = {
                        VendorId: objVendorMaster.VendorId,
                        AgencyTypeId: objVendorMaster.AgencyTypeId,
                        CompanyName: objVendorMaster.CompanyName,
                        //Address: objVendorMaster.Address,
                        //CountryId: objVendorMaster.CountryId,
                        //StateId: objVendorMaster.StateId,
                        //CityId: objVendorMaster.CityId,
                        //AreaId: objVendorMaster.AreaId,
                        //Fax: objVendorMaster.Fax,
                        Website: objVendorMaster.Website,
                        Remark: objVendorMaster.Remark,
                        //VAT: objVendorMaster.VAT,
                        GST: objVendorMaster.GST,
                        PAN: objVendorMaster.PAN,
                        TAN: objVendorMaster.TAN,
                        ServiceTaxNo: objVendorMaster.ServiceTaxNo,
                        //CountryData: {
                        //    Display: objVendorMaster.CountryName,
                        //    Value: objVendorMaster.CountryId
                        //},
                        //StateData: {
                        //    Display: objVendorMaster.StateName,
                        //    Value: objVendorMaster.StateId
                        //},
                        //CityData: {
                        //    Display: objVendorMaster.CityName,
                        //    Value: objVendorMaster.CityId
                        //},
                        //AreaData: {
                        //    Display: objVendorMaster.AreaName,
                        //    Value: objVendorMaster.AreaId
                        //},
                        AgencyTypeData: {
                            Display: objVendorMaster.AgencyType,
                            Value: objVendorMaster.AgencyTypeId
                        }
                    };
                    $scope.objVendor.VendorContactDetails = [];
                    angular.forEach(result.data.DataList.objVendorContactDetail, function (value) {
                        var objContactDetail = {
                            ContactId: value.ContactId,
                            VendorId: value.VendorId,
                            ContactName: value.ContactName,
                            DesignationId: value.DesignationId,
                            DesignationName: value.DesignationName,
                            MobileNo: value.MobileNo,
                            Email: value.Email,
                            Surname: value.Surname,
                            //QQ: value.QQ,
                            //Skype: value.Skype,
                            Status: 2,//1 : Insert , 2:Update ,3 :Delete
                            DesignationData: {
                                Display: value.DesignationName,
                                Value: value.DesignationId
                            }
                        }
                        $scope.objVendor.VendorContactDetails.push(objContactDetail);
                    }, true);

                    // Get Vendor Bank Detail
                    $scope.objVendor.VendorBankDetails = [];
                    angular.forEach(result.data.DataList.objVendorBankDetail, function (value) {
                        var objBankDetail = {
                            BankDetailId: value.BankDetailId,
                            VendorId: value.VendorId,
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
                            Status: 2, //1 : Insert , 2:Update ,3 :Delete
                            BankData: { Display: value.BankName, Value: value.BankNameId },
                            AccountTypeData: { Display: value.AccountType, Value: value.AccountTypeId }
                        }
                        $scope.objVendor.VendorBankDetails.push(objBankDetail);
                    }, true);

                    $scope.objVendor.VendorAddressDetails = [];
                    angular.forEach(result.data.DataList.objVendorAddressDetail, function (value) {
                        var objVendorAddress = {
                            AddressId: value.AddressId,
                            VendorId: value.VendorId,
                            Address: value.Address,
                            CountryId: value.CountryId,
                            StateId: value.StateId,
                            CityId: value.CityId,
                            CountryName: value.CountryName,
                            StateName: value.StateName,
                            CityName: value.CityName,
                            AreaId: value.AreaId,
                            Pincode: value.Pincode,
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
                        $scope.objVendor.VendorAddressDetails.push(objVendorAddress);
                    }, true);

                    $scope.objVendor.VendorChatDetails = [];
                    angular.forEach(result.data.DataList.objVendorChatDetail, function (value) {
                        var objChatDetail = {
                            VendorChatId: value.VendorChatId,
                            VendorId: value.VendorId,
                            ChatId: value.ChatId,
                            ChatName: value.ChatName,
                            ChatValue: value.ChatValue,
                            Status: 2, //1 : Insert , 2:Update ,3 :Delete
                            ChatData: { Display: value.ChatName, Value: value.ChatId },
                        }
                        $scope.objVendor.VendorChatDetails.push(objChatDetail);
                    }, true);

                    $scope.storage = angular.copy($scope.objVendor);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        function ResetForm() {
            $scope.objVendor = {
                VendorId: ($scope.SrNo && $scope.SrNo > 0) ? $scope.SrNo : 0,
                AgencyTypeId: 0,
                AgencyType: '',
                CompanyName: '',
                Website: '',
                Remark: '',
                GST: '',
                PAN: '',
                TAN: '',
                ServiceTaxNo: '',
                AgencyTypeData: { Display: '', Value: '' },
                VendorContactDetails: [],
                VendorBankDetails: [],
                VendorAddressDetails: [],
                VendorChatDetails: []
            };
            $scope.VendorContact = {
                ContactId: 0,
                VendorId: 0,
                ContactName: '',
                MobileNo: '',
                DesignationId: 0,
                DesignationName: '',
                Email: '',
                Surname: '',
                Status: 0,
                DesignationData: { Display: '', Value: '' }
            };
            $scope.VendorBank = {
                BankDetailId: 0,
                VendorId: 0,
                BeneficiaryName: '',
                NickName: '',
                BankNameId: 0,
                BankName: '',
                BranchName: '',
                AccountNo: '',
                IFSCCode: '',
                SwiftCode: '',
                AccountTypeId: 0,
                AccountType: '',
                Status: 2, //1 : Insert , 2:Update ,3 :Delete
                BankData: { Display: '', Value: '' },
                AccountTypeData: { Display: '', Value: '' }
            };
            $scope.VendorAddress = {
                AddressId: 0,
                VendorId: 0,
                CountryId: 0,
                StateId: 0,
                CityId: 0,
                CountryName: '',
                StateName: '',
                CityName: '',
                Address: '',
                Pincode: '',
                WeeklyOff: '',
                Status: 0, //1 : Insert , 2:Update ,3 :Delete
                CountryData: {
                    Display: '', Value: ''
                },
                StateData: {
                    Display: '', Value: ''
                },
                CityData: {
                    Display: '', Value: ''
                }
            };
            $scope.VendorChat = {
                VendorChatId: 0,
                VendorId: 0,
                ChatId: 0,
                ChatName: '',
                ChatValue: '',
                Status: 0, //1 : Insert , 2:Update ,3 :Delete
                ChatData: { Display: '', Value: '' },
            };

            if ($scope.FormVendorInfo)
                $scope.FormVendorInfo.$setPristine();
            $scope.addMode = true;
            $scope.isFirstFocus = false;
            $timeout(function () {
                $scope.isFirstFocus = true;
            });
            $scope.EditVendorContactIndex = -1;
            $scope.AddTextVendorContact = "Save";
        }
        ResetForm();

        $scope.Add = function () {
            window.location.href = "/Master/Vendor/AddVendor";
        };

        $scope.Reset = function () {
            if ($scope.addMode == false) {
                $scope.objVendor = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

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
            $scope.objVendorChatDetail.ChatData.Display = ' ';
            $scope.objVendorChatDetail.ChatValue = ' ';
            data.AgencyTypeId = data.AgencyTypeData.Value;
            data.AgencyType = data.AgencyTypeData.Display;
            VendorService.CreateUpdate(data).then(function (result) {
                if (result.data.ResponseType == 1) {
                    ResetForm();
                    toastr.success(result.data.Message);
                    //$scope.refreshGrid();
                    window.location.href = "/Master/Vendor";
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
               { "title": "Vendor Id", "data": "VendorId", filter: false, visible: false },
               { "title": "Sr.", "data": "RowNumber", filter: false, sort: false },
               { "title": "Company Name", "data": "CompanyName" },
               { "title": "District", "data": "CityName" },
               { "title": "Contact Person", "data": "Person" },
               { "title": "Mobile No", "data": "MobileNo" },
               { "title": "Email Id", "data": "Email", sort: true, filter: true, "render": "<p class='MailTo' ng-bind-html='getHtml($parent.$parent.$parent.FormatEmail(row.Email))'>" },
               {
                   "title": "Action", sort: false, filter: false,
                   'render': '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.Edit(row.VendorId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                            //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.VendorId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                            '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.View(row.VendorId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
               }
            ],
            Sort: { 'VendorId': 'asc' }
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
            window.location.href = "/Master/Vendor/AddVendor/" + id + "/" + 0;
        }
        $scope.View = function (id) {
            window.location.href = "/Master/Vendor/AddVendor/" + id + "/" + 1;
        }
        $scope.Delete = function (id) {
            VendorService.DeleteById(id).then(function (result) {
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

        //BEGIN MANAGE VENDOR CONTACT DETAIL
        $scope.AddVendorContactDetail = function (data) {
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'VendorContactDetail.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    VendorCtrl: function () { return $scope; },
                    VendorContactData: function () { return data; },
                }
            });
        }

        $scope.DeleteVendorDetail = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objVendor.VendorContactDetails[index] = data;
                } else {
                    $scope.objVendor.VendorContactDetails.splice(index, 1);
                }
                toastr.success("Vendor contact detail Delete", "Success");
            })
        }

        $scope.EditVendorDetail = function (data, index) {
            $scope.EditVendorContactIndex = index;
            $scope.AddVendorContactDetail(data);
        }
        //END MANAGE VENDOR CONTACT DETAIL

        //BEGIN MANAGE VENDOR BANK DETAIL
        $scope.AddVendorBankDetail = function (data) {
            //if(){
            //    $scope.objVendorBank = {
            //        BankDetailId: 0,
            //        VendorId: 0,
            //        BankName: '',
            //        IFSCCode: '',
            //        AccountNo: '',
            //        BeneficiaryName: '',
            //        BranchName: '',
            //        Status: 0
            //    };

            //}
            var BankmodalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'VendorBankDetail.html',
                controller: BankModalInstanceCtrl,
                resolve: {
                    VendorCtrl: function () { return $scope; },
                    VendorBankData: function () { return data; }
                }
            });
            BankmodalInstance.result.then(function () {
                $scope.EditVendorBankIndex = -1;
            }, function () {
                $scope.EditVendorBankIndex = -1;
            })
        }

        $scope.DeleteVendorBankDetail = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objVendor.VendorBankDetails[index] = data;
                } else {
                    $scope.objVendor.VendorBankDetails.splice(index, 1);
                }
                toastr.success("Vendor Bank Detail Delete", "Success");
            })
        }

        $scope.EditVendorBankDetail = function (data, index) {
            $scope.EditVendorBankIndex = index;
            $scope.AddVendorBankDetail(data);
        }
        //END MANAGE VENDOR BANK DETAIL

        //BEGIN MANAGE VENDOR ADDRESS INFORMATION
        $scope.AddVendorAddressDetail = function (data) {
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'VendorAddressDetail.html',
                controller: AddressModalInstanceCtrl,
                resolve: {
                    VendorCtrl: function () {
                        return $scope;
                    },
                    VendorAddressData: function () {
                        return data;
                    }
                }
            });
            modalInstance.result.then(function () {
                $scope.EditVendorAddressIndex = -1;
            }, function () {
                $scope.EditVendorAddressIndex = -1;
            })
        }

        $scope.DeleteVendorAddressDetail = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objVendor.VendorAddressDetails[index] = data;
                } else {
                    $scope.objVendor.VendorAddressDetails.splice(index, 1);
                }
                toastr.success("Vendor Address Detail Delete", "Success");
            })
        }

        $scope.EditVendorAddressDetail = function (data, index) {
            $scope.EditVendorAddressIndex = index;
            $scope.AddVendorAddressDetail(data);
        }
        //END MANAGE BUYER ADDRESS INFORMATION

        //ADD Chat DETAIL
        $scope.AddVendorChatDetail = function (data) {
            $scope.submittedd = true;
            if (data.ChatValue != undefined && data.ChatValue != "") {
                $scope.submittedd = false;
                var ChatDetails = {
                    VendorChatId: parseInt(data.VendorChatId),
                    ChatId: data.ChatData.Value,
                    ChatName: data.ChatData.Display,
                    ChatValue: data.ChatValue
                };

                if ($scope.EditVendorChatDetailIndex > -1) {
                    if ($scope.objVendor.VendorChatDetails[$scope.EditVendorChatDetailIndex].Status == 2) {
                        ChatDetails.Status = 2;
                    } else if ($scope.objVendor.VendorChatDetails[$scope.EditVendorChatDetailIndex].Status == 1 ||
                               $scope.objVendor.VendorChatDetails[$scope.EditVideoUrlDetailIndex].Status == undefined) {
                        ChatDetails.Status = 1;
                    }
                    $scope.objVendor.VendorChatDetails[$scope.EditVendorChatDetailIndex] = ChatDetails;
                    $scope.EditVendorChatDetailIndex = -1;
                } else {
                    ChatDetails.Status = 1;
                    $scope.objVendor.VendorChatDetails.push(ChatDetails);
                }
                $scope.objVendorChatDetail.ChatId = '';
                $scope.objVendorChatDetail.ChatValue = '';
                $scope.objVendorChatDetail.ChatName = '';
                $scope.objVendorChatDetail.ChatData = {
                    Display: '', Value: ''
                };
            }
        }

        //EDIT Chat DETAIL
        $scope.EditVendorChatDetail = function (data, index) {
            $scope.EditVendorChatDetailIndex = index;
            $scope.objVendorChatDetail.ChatId = data.ChatId;
            $scope.objVendorChatDetail.ChatValue = data.ChatValue;
            $scope.objVendorChatDetail.ChatName = data.ChatName;
            $scope.objVendorChatDetail.VendorChatId = data.VendorChatId;
            $scope.objVendorChatDetail.ChatData = { Display: data.ChatName, Value: data.ChatId };
        }

        //DELETE Chat DETAIL
        $scope.DeleteVendorChatDetail = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objVendor.VendorChatDetails[index] = data;
                } else {
                    $scope.objVendor.VendorChatDetails.splice(index, 1);
                }
                toastr.success("Chat Details Delete", "Success");
            })
        }

        $scope.statusFilter = function (val) {
            if (val.Status != 3)
                return true;
        }

    }
    var AddressModalInstanceCtrl = function ($scope, $uibModalInstance, VendorCtrl, VendorAddressData, CityService) {


        $scope.$watch('objVendorAddress.CountryData', function (data) {
            if (data.Value != '') {
                if (data.Value != VendorAddressData.CountryId.toString()) {
                    $scope.objVendorAddress.StateData.Display = '';
                    $scope.objVendorAddress.StateData.Value = '';
                    $scope.objVendorAddress.CityData.Display = '';
                    $scope.objVendorAddress.CityData.Value = '';
                }
            } else {
                $scope.CountryBind('India');
            }
            
        }, true)

        $scope.$watch('objVendorAddress.StateData', function (data) {
            if (data.Value != VendorAddressData.StateId.toString()) {
                $scope.objVendorAddress.CityData.Display = '';
                $scope.objVendorAddress.CityData.Value = '';
            }
        }, true)

        $scope.CountryBind = function (data) {
            CityService.CountryBind().then(function (result) {
                if (result.data.ResponseType == 1) {
                    _.each(result.data.DataList, function (value, key, list) {
                        debugger;
                        if (value.CountryName.toString().toLowerCase() == data.toString().toLowerCase()) {
                            $scope.objVendorAddress.CountryData = {
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

        $scope.objVendorAddress = {
            AddressId: VendorAddressData.AddressId,
            VendorId: VendorAddressData.VendorId,
            CountryId: VendorAddressData.CountryId,
            StateId: VendorAddressData.StateId,
            CityId: VendorAddressData.CityId,
            CountryName: VendorAddressData.CountryName,
            StateName: VendorAddressData.StateName,
            CityName: VendorAddressData.CityName,
            Address: VendorAddressData.Address,
            Pincode: VendorAddressData.Pincode,
            WeeklyOffId: VendorAddressData.WeeklyOffId,
            WeeklyOff: VendorAddressData.WeeklyOff,
            Status: VendorAddressData.Status,
            Fax: VendorAddressData.Fax,
            CountryData: { Display: VendorAddressData.CountryName, Value: VendorAddressData.CountryId },
            StateData: { Display: VendorAddressData.StateName, Value: VendorAddressData.StateId },
            CityData: { Display: VendorAddressData.CityName, Value: VendorAddressData.CityId }
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

            if (VendorCtrl.EditVendorAddressIndex > -1) {
                VendorCtrl.objVendor.VendorAddressDetails[VendorCtrl.EditVendorAddressIndex] = data;
                VendorCtrl.EditVendorAddressIndex = -1;
            } else {
                data.Status = 1;
                VendorCtrl.objVendor.VendorAddressDetails.push(data);
            }
            $scope.close();
        }

    }
    AddressModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'VendorCtrl', 'VendorAddressData', 'CityService']

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, VendorCtrl, VendorContactData) {
        $scope.MobileData = VendorCtrl.MobileData;
        $scope.MobileNo = VendorContactData.MobileNo != "" ? VendorContactData.MobileNo.split(",") : [];
        $scope.Email = VendorContactData.Email != "" ? VendorContactData.Email.split(",") : [];

        $scope.objVendorContact = {
            ContactId: VendorContactData.ContactId,
            VendorId: VendorContactData.VendorId,
            ContactName: VendorContactData.ContactName,
            MobileNo: $scope.MobileNo.toString(),
            DesignationId: VendorContactData.DesignationId,
            Email: $scope.Email.toString(),
            Surname: VendorContactData.Surname,
            Status: VendorContactData.Status, //1 : Insert , 2:Update ,3 :Delete
            DesignationData: {
                Display: VendorContactData.DesignationName,
                Value: VendorContactData.DesignationId
            }
        };

        $scope.close = function () {
            $uibModalInstance.close();
        };

        $scope.CreateUpdate = function (data) {

            data.DesignationId = data.DesignationData.Value;
            data.DesignationName = data.DesignationData.Display;
            data.MobileNo = this.MobileNo.toString();
            data.Email = this.Email.toString();
            if (VendorCtrl.EditVendorContactIndex > -1) {
                VendorCtrl.objVendor.VendorContactDetails[VendorCtrl.EditVendorContactIndex] = data;
                VendorCtrl.EditVendorContactIndex = -1;
            } else {
                data.Status = 1;
                VendorCtrl.objVendor.VendorContactDetails.push(data);
            }
            //$scope.objVendorContact = {
            //    ContactId: 0,
            //    VendorId: 0,
            //    ContactName: '',
            //    MobileNo: '',
            //    DesignationId: 0,
            //    DesignationName: '',
            //    Email: '',
            //    Surname: '',
            //    QQ: '',
            //    Skype: '',
            //    Status: 0,
            //    DesignationData: { Display: '', Value: '' }
            //};
            $scope.close();
        }
    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'VendorCtrl', 'VendorContactData']

    var BankModalInstanceCtrl = function ($scope, $uibModalInstance, VendorCtrl, VendorBankData) {
        $scope.objVendorBank = {
            BankDetailId: VendorBankData.BankDetailId,
            VendorId: VendorBankData.VendorId,
            NickName: VendorBankData.NickName,
            BankNameId: VendorBankData.BankNameId,
            BeneficiaryName: VendorBankData.BeneficiaryName,
            BankName: VendorBankData.BankName,
            BranchName: VendorBankData.BranchName,
            AccountNo: VendorBankData.AccountNo,
            AccountTypeId: VendorBankData.AccountTypeId,
            IFSCCode: VendorBankData.IFSCCode,
            SwiftCode: VendorBankData.SwiftCode,
            Status: VendorBankData.Status, //1 : Insert , 2:Update ,3 :Delete
            BankData: {
                Display: VendorBankData.BankName, Value: VendorBankData.BankNameId
            },
            AccountTypeData: {
                Display: VendorBankData.AccountType, Value: VendorBankData.AccountTypeId
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

            if (VendorCtrl.EditVendorBankIndex > -1) {
                VendorCtrl.objVendor.VendorBankDetails[VendorCtrl.EditVendorBankIndex] = data;
                VendorCtrl.EditVendorBankIndex = -1;
            } else {
                data.Status = 1;
                VendorCtrl.objVendor.VendorBankDetails.push(data);
            }
            //$scope.objVendorBank = {
            //    BankDetailId: 0,
            //    VendorId: 0,
            //    BankName: '',
            //    IFSCCode: '',
            //    AccountNo: '',
            //    BeneficiaryName: '',
            //    BranchName: '',
            //    Status: 0
            //};
            $scope.close();
        }
    }
    BankModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'VendorCtrl', 'VendorBankData']

})()

