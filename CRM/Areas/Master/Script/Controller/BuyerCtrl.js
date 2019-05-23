(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("BuyerCtrl", [
            "$scope", "$rootScope", "$timeout", "$filter", "BuyerService", "NgTableParams", "$uibModal", "CityService", "CountryService", "SourceService", "Upload", 
            BuyerCtrl]);

    function BuyerCtrl($scope, $rootScope, $timeout, $filter, BuyerService, NgTableParams, $uibModal, CityService, CountryService, SourceService, Upload) {
        $scope.objBuyer = $scope.objBuyer || {};
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};
        //var Permission = '';
        //$scope.myinit = Function()
        //{
        //    var MenuCode = 17;//Work Task List Form ID
        //    Permission = GetRole.multiply(MenuCode)
        //}

        $scope.objBuyer = {
            BuyerId: 0,
            CompanyName: '',
            Email: '',
            Fax: '',
            WebAddress: '',
            //GST: '',
            //VAT: '',
            //CST: '',
            //PAN: '',
            //TAN: '',
            Remark: '',
            ContactType: '',
            AgencyTypeId: 0,
            ContactData: { Display: '', Value: '' },
            IsBuyerSelect: 0,
            IsSupplierSelect: 0,
            IsVendorSelect: 0,
            DocumentsData: '',
            BuyerContactDetails: [],
            BuyerAddressDetails: [],
            BuyerBankDetails: [],
            BuyerLicenseDetails: [],
            BuyerDocumentDetails: [],
            ConInvId: ''
        };
        $scope.objBuyerAddress = {
            AddressId: 0,
            BuyerId: 0,
            CountryId: 0,
            StateId: 0,
            CityId: 0,
            CountryName: '',
            StateName: '',
            CityName: '',
            Address: '',
            PinCode: '',
            WeeklyOff: '',
            Telephone: '',
            Email: '',
            Fax: '',
            WebAddress: '',
            addressData: { Display: '', Value: '' },
            Status: 0, //1 : Insert , 2:Update ,3 :Delete
            CountryData: { Display: '', Value: '' },
            StateData: { Display: '', Value: '' },
            CityData: { Display: '', Value: '' }
        };
        $scope.objBuyerTax = {
            BuyerLicenseId: 0,
            LicenseId: 0,
            LicenseName: 0,
            BuyerId: 0,
            LicenseNo: '',
            Status: 0, //1 : Insert , 2:Update ,3 :Delete
            LicenseData: { Display: '', Value: '' },
        };
        $scope.objBuyerBank = {
            BankDetailID: 0,
            BuyerId: 0,
            BeneficiaryName: '',
            NickName: '',
            BankName: '',
            BankNameId: 0,
            BranchName: '',
            AccountNo: '',
            AccountTypeId: 0,
            AccountType: '',
            IFSCCode: '',
            SwiftCode: '',
            Status: 0,
            BankData: { Display: '', Value: '' },
            AccountTypeData: { Display: '', Value: '' }
        };
        $scope.objBuyerDocument = {
            DocID: 0,
            BuyerId: 0,
            DocumentId: '',
            DocumentName: '',
            DocumentValue: '',
            DocumentType: '',
            Status: 0,
            DocumentsData: { Display: '', Value: '' }
        };
        $scope.objBuyerContact = {
            ContactId: 0,
            BuyerId: 0,
            ContactPerson: '',
            Surname: '',
            DesignationId: 0,
            DesignationName: '',
            MobileNo: '',
            Chat: '',
            Email: '',
            SkypeId: '',
            QQCode: '',
            Status: 0,//1 : Insert , 2:Update ,3 :Delete
            DesignationData: { Display: '', Value: '' }
        };
        $scope.telCodeData = [];
        $scope.chatCodeData = [];
        $scope.WebAddressData = [];
        $scope.cmpnyemailCodeData = [];
        $scope.cmpnytelCodeData = [];
        $scope.cmpnyFaxCodeData = [];
        $scope.addreteliphoneData = [];
        $scope.addreFaxCodeData = [];
        $scope.addressemailiddata = [];
        $scope.ContactInvitation = [];
        $scope.OutputInvitation = [];
        var forloop = function (data) {
            if (data != null && data != '') {
                angular.forEach(data, function (value) {
                    value.Status = 2;
                });
            }
        }
        $scope.openTab = function (evt, tabName, data, tag) {
            // Declare all variables
            var bln = true;
            var dataerror = true;

            if ($scope.isClicked == false) {
                if (data != undefined) {
                    if (tabName == 'AddressDetails' || tabName == 'ContactDetails' || tabName == 'TaxDetails' || tabName == 'BankDetails' || tabName == 'DocumentsUploadDetails') {
                        if (data.IsBuyerSelect == 0 && data.IsSupplierSelect == 0 && data.IsVendorSelect == 0) {
                            toastr.error("Please Contact Register.");
                            dataerror = false;
                            return false;
                        } else if (data.CompanyName == '' || data.CompanyName == null) {
                            toastr.error("Company Name is required.");
                            dataerror = false;
                            return false;
                        } else if (data.ContactData.Display == '' || data.ContactData.Display == null) {
                            toastr.error("Please Select Type Of Contact.");
                            dataerror = false;
                            return false;
                        }
                        //if (data.WebAddress == '' || data.WebAddress == null) {
                        //    toastr.error("Please Enter Company Website.");
                        //    dataerror = false;
                        //    return false;
                        //}
                    }
                    //if (tabName != 'ContactDetails' && dataerror == true) {
                    //    forloop(data.BuyerAddressDetails);
                    //}
                    //if (tabName != 'TaxDetails' && dataerror == true) {
                    //    forloop(data.BuyerContactDetails);
                    //}
                    //if (tabName != 'BankDetails' && dataerror == true) {
                    //    forloop(data.BuyerLicenseDetails);
                    //}
                    if (dataerror == true) {
                        $scope.CreateUpdate(data, tag);
                        $scope.GetAllBuyerInfoById(data.BuyerId);
                    }
                }
            }
            if (bln == true && dataerror == true && $scope.isvalid == true) {
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

        $scope.SetBuyerId = function (id, isdisable) {
            $scope.isvalid = true;
            $scope.openTab("Click", "MainDetails", undefined);
            CountryService.GetCountryFlag().then(function (result) {
                $scope.telCodeData = angular.copy(result);
                $scope.cmpnytelCodeData = angular.copy(result);
                $scope.cmpnyFaxCodeData = angular.copy(result);
                $scope.addreteliphoneData = angular.copy(result);
                $scope.addreFaxCodeData = angular.copy(result);
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
                        $scope.GetAllBuyerInfoById(id);
                    } else {
                        $scope.SrNo = 0;
                        $scope.addMode = true;
                        $scope.isClicked = false;
                        $scope.saveText = "Save";
                    }
                    //GetInvitationData();
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        //function GetInvitationData() {
        //    $scope.ContactInvitation = [];
        //    BuyerService.GetInvitationData().then(function (result) {
        //        var res;
        //        if ($scope.objBuyer.ConInvId != undefined && $scope.objBuyer.ConInvId != null && $scope.objBuyer.ConInvId != '')
        //            res = $scope.objBuyer.ConInvId.split("|");
        //        _.each(result.data.DataList, function (value, key, list) {
        //            if (res != undefined && res.includes(value.ContactInvitationId.toString())) {
        //                $scope.ContactInvitation.push({
        //                    name: value.ContactInvitationId,
        //                    maker: value.ContactInvitation,
        //                    ticked: true
        //                })
        //            }
        //            else {
        //                $scope.ContactInvitation.push({
        //                    name: value.ContactInvitationId,
        //                    maker: value.ContactInvitation,
        //                    ticked: false
        //                })
        //            }
        //        })
        //    }, function (errorMsg) {
        //        toastr.error(errorMsg, 'Opps, Something went wrong');
        //    })
        //}

        $scope.GetAllBuyerInfoById = function (id) {
            BuyerService.GetAllBuyerInfoById(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    var objBuyerMaster = result.data.DataList.objBuyerMaster;
                    $scope.webaddress = (objBuyerMaster.WebAddress != '' && objBuyerMaster.WebAddress != null) ? objBuyerMaster.WebAddress.split(",") : [];
                    $scope.emailid = (objBuyerMaster.Email != '' && objBuyerMaster.Email != null) ? objBuyerMaster.Email.split(",") : [];
                    $scope.cmpnyteliphone = (objBuyerMaster.Telephone != '' && objBuyerMaster.Telephone != null) ? objBuyerMaster.Telephone.split(",") : [];
                    $scope.cmpnyFaxNo = (objBuyerMaster.Fax != '' && objBuyerMaster.Fax != null) ? objBuyerMaster.Fax.split(",") : [];
                    objBuyerMaster.ContactType = (objBuyerMaster.ContactType != '' && objBuyerMaster.ContactType != null) ? objBuyerMaster.ContactType.split("|") : [];


                    $scope.objBuyer = {
                        BuyerId: objBuyerMaster.BuyerId,
                        CompanyName: objBuyerMaster.CompanyName,
                        Email: $scope.emailid.toString(),
                        WebAddress: $scope.webaddress.toString(),
                        ContactType: objBuyerMaster.ContactType,
                        AgencyTypeId: objBuyerMaster.AgencyTypeId,
                        Fax: objBuyerMaster.Fax,
                        ContactData: { Display: objBuyerMaster.AgencyType, Value: objBuyerMaster.AgencyTypeId },
                        //GST: objBuyerMaster.GST,
                        //VAT: objBuyerMaster.VAT,
                        //CST: objBuyerMaster.CST,
                        //PAN: objBuyerMaster.PAN,
                        //TAN: objBuyerMaster.TAN,
                        Remark: objBuyerMaster.Remark,
                        DocumentsData: objBuyerMaster.DocumentsData,
                        ConInvId: objBuyerMaster.ConInvId
                    };
                    angular.forEach(objBuyerMaster.ContactType, function (value) {
                        if (value == 'Buyer') {
                            $scope.objBuyer.IsBuyerSelect = true
                        }
                        if (value == 'Vendor') {
                            $scope.objBuyer.IsVendorSelect = true
                        }
                        if (value == 'Supplier') {
                            $scope.objBuyer.IsSupplierSelect = true
                        }
                    });
                    $scope.objBuyer.BuyerContactDetails = [];
                    angular.forEach(result.data.DataList.objBuyerContactDetail, function (value) {
                        var objContactDetail = {
                            ContactId: value.ContactId,
                            BuyerId: value.BuyerId,
                            ContactPerson: value.ContactPerson,
                            Surname: value.Surname,
                            DesignationId: value.DesignationId,
                            DesignationName: value.DesignationName,
                            MobileNo: value.MobileNo,
                            Email: value.Email,
                            Chat: value.ChatDetails,
                            SkypeId: value.SkypeId,
                            QQCode: value.QQCode,
                            Status: 2,//1 : Insert , 2:Update ,3 :Delete
                            DesignationData: {
                                Display: value.DesignationName,
                                Value: value.DesignationId
                            }
                        }

                        $scope.objBuyer.BuyerContactDetails.push(objContactDetail);
                    }, true);

                    $scope.objBuyer.BuyerAddressDetails = [];
                    angular.forEach(result.data.DataList.objBuyerAddressDetail, function (value) {
                        var objBuyerAddress = {
                            AddressId: value.AddressId,
                            BuyerId: value.BuyerId,
                            Address: value.Address,
                            CountryId: value.CountryId,
                            StateId: value.StateId,
                            CityId: value.CityId,
                            CountryName: value.CountryName,
                            StateName: value.StateName,
                            CityName: value.CityName,
                            AreaId: value.AreaId,
                            PinCode: value.PinCode,
                            WeeklyOff: value.WeeklyOff,
                            Telephone: value.Telephone,
                            Email: value.Email,
                            Fax: value.Fax,
                            WebAddress: value.WebAddress,
                            AddressTypeId: value.AddressTypeId,
                            AddressType: value.AddressType,
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
                        $scope.objBuyer.BuyerAddressDetails.push(objBuyerAddress);
                    }, true);

                    $scope.objBuyer.BuyerBankDetails = [];
                    angular.forEach(result.data.DataList.objBuyerBankDetail, function (value) {
                        var objBankDetail = {
                            BankDetailID: value.BankDetailID,
                            BuyerId: value.BuyerId,
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
                        $scope.objBuyer.BuyerBankDetails.push(objBankDetail);
                    }, true);

                    $scope.objBuyer.BuyerLicenseDetails = [];
                    angular.forEach(result.data.DataList.objBuyerLicenseDetail, function (value) {
                        var objTaxDetail = {
                            BuyerLicenseId: value.BuyerLicenseId,
                            LicenseId: value.LicenseId,
                            LicenseName: value.LicenseName,
                            BuyerId: value.BuyerId,
                            LicenseNo: value.LicenseNo,
                            Status: 2, //1 : Insert , 2:Update ,3 :Delete
                            LicenseData: { Display: value.LicenseName, Value: value.LicenseName }
                        }
                        $scope.objBuyer.BuyerLicenseDetails.push(objTaxDetail);
                    }, true);
                    $scope.objBuyer.BuyerDocumentDetails = [];
                    if (objBuyerMaster.DocumentsData != undefined || objBuyerMaster.DocumentsData != null) {
                        var doclist = objBuyerMaster.DocumentsData.split('|');
                        angular.forEach(doclist, function (value) {
                            var docId = value.split('-')[0];
                            var docName = value.split('-')[1];
                            var objDocumentDetail = {
                                DocID: 0,
                                BuyerId: value.BuyerId,
                                DocumentId: docId,
                                DocumentName: docName,
                                DocumentValue: '/UploadImages/ContactDocumentImage/' + value.split('-')[2].toString(),
                                DocumentType: value.split('-')[2].toString().split('.')[1],
                                Status: 2,
                                DocumentsData: { Display: docName, Value: docId }
                            }
                            $scope.objBuyer.BuyerDocumentDetails.push(objDocumentDetail);
                        }, true);
                    }
                    $scope.storage = angular.copy($scope.objBuyer);

                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.Add = function () {
            //if (Permission.IsAdd == false) {
            //    toastr.error('Access is denied');
            //}
            //else {
            window.location.href = "/master/Buyer/AddBuyer";
            //}
        }

        $scope.GetDuplicationDataInfoByName = function (data) {
            BuyerService.CheckBuyerDuplicate(data).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.isvalid = true
                } else {
                    $scope.isvalid = false
                    toastr.error(result.data.Message);
                }
            });
        }

        function ResetForm() {
            $scope.objBuyer = {
                BuyerId: ($scope.SrNo && $scope.SrNo > 0) ? $scope.SrNo : 0,
                CompanyName: '',
                Address: '',
                CountryId: 0,
                StateId: 0,
                CityId: 0,
                PinCode: '',
                Email: '',
                Fax: '',
                WebAddress: '',
                WeeklyOff: '',
                GST: '',
                VAT: '',
                CST: '',
                PAN: '',
                TAN: '',
                Remark: '',
                ContactType: '',
                AgencyTypeId: 0,
                ContactData: { Display: '', Value: '' },
                IsBuyerSelect: 0,
                IsSupplierSelect: 0,
                IsVendorSelect: 0,
                CountryData: { Display: '', Value: '' },
                StateData: { Display: '', Value: '' },
                CityData: { Display: '', Value: '' },
                AreaData: { Display: '', Value: '' },
                BuyerAddressDetails: [],
                BuyerContactDetails: [],
                BuyerBankDetails: [],
                BuyerLicenseDetails: [],
                ConInvId: ''
            };
            $scope.objBuyerAddress = {
                AddressId: 0,
                BuyerId: 0,
                CountryId: 0,
                StateId: 0,
                CityId: 0,
                CountryName: '',
                StateName: '',
                CityName: '',
                Address: '',
                PinCode: '',
                WeeklyOff: '',
                Telephone: '',
                Email: '',
                Fax: '',
                WebAddress: '',
                addressData: { Display: '', Value: '' },
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
            $scope.objBuyerContact = {
                ContactId: 0,
                BuyerId: 0,
                ContactPerson: '',
                Surname: '',
                DesignationId: 0,
                DesignationName: '',
                MobileNo: '',
                Chat: '',
                Email: '',
                SkypeId: '',
                QQCode: '',
                Status: 0,//1 : Insert , 2:Update ,3 :Delete
                DesignationData: { Display: '', Value: '' }
            };
            $scope.objBuyerBank = {
                BankDetailID: 0,
                BuyerId: 0,
                BeneficiaryName: '',
                NickName: '',
                BankNameId: '',
                BankName: '',
                BranchName: '',
                AccountNo: '',
                AccountTypeId: 0,
                AccountType: '',
                IFSCCode: '',
                SwiftCode: '',
                Status: 0, //1 : Insert , 2:Update ,3 :Delete
                BankData: { Display: '', Value: '' },
                AccountTypeData: { Display: '', Value: '' }
            };
            $scope.objBuyerTax = {
                BuyerLicenseId: 0,
                LicenseId: 0,
                LicenseName: 0,
                BuyerId: 0,
                LicenseNo: '',
                Status: 0, //1 : Insert , 2:Update ,3 :Delete
                LicenseData: { Display: '', Value: '' },
            };
            if ($scope.FormBuyerInfo)
                $scope.FormBuyerInfo.$setPristine();
            $scope.addMode = true;
            $scope.isFirstFocus = false;
            $scope.storage = {};
            $timeout(function () {
                $scope.isFirstFocus = true;
            });
            $scope.EditBuyerContactIndex = -1;
            $scope.EditBuyerAddressIndex = -1;
            $scope.EditBuyerTaxIndex = -1;
            $scope.EditBuyerBankIndex = -1;
            $scope.EditBuyerDocumentIndex = -1;
        }
        ResetForm();

        $scope.Reset = function () {
            if ($scope.addMode == false) {
                $scope.objBuyer = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }
        $scope.CheckValue = function (value) {
            var alphanumers = /^[~\-!@#\$%\^\*]+$/;
            if (alphanumers.indexOf(value) > -1) {
                toastr.error("Company Name (~,-,!,@,#,$,%,^,*) special character not allow.");
            }
        }
        $scope.CreateUpdate = function (data, tag) {
            var type = ""; var ConInvitersIds = '';
            if (data.IsBuyerSelect) {
                if (type == '') {
                    type = "Buyer";
                } else {
                    type += "|Buyer";
                }
            }
            if (data.IsVendorSelect) {
                if (type == '') {
                    type = "Vendor";
                } else {
                    type += "|Vendor";
                }
            }
            if (data.IsSupplierSelect) {
                if (type == '') {
                    type = "Supplier";
                } else {
                    type += "|Supplier";
                }
            }
            if (data.BuyerDocumentDetails != undefined) {
                if (data.BuyerDocumentDetails.length > 0) {
                    var prdDocVal = data.BuyerDocumentDetails.map(function (item) {
                        var docval = item['DocumentValue'].split('/');
                        return item['DocumentId'] + '-' + item['DocumentName'] + '-' + docval[docval.length - 1].toString();
                    });
                    data.DocumentsData = prdDocVal.join("|");
                }
            }
            _.each($scope.OutputInvitation, function (value, key, list) {
                if (key < $scope.OutputInvitation.length - 1) {
                    ConInvitersIds += value.name + '|'
                }
                else if (key == $scope.OutputInvitation.length - 1) {
                    ConInvitersIds += value.name;
                }
            })

            data.ContactType = type;
            if (tag != undefined) {
                $scope.objBuyerContact.DesignationData.Display = ' ';
                $scope.objBuyerTax.LicenseData.Display = ' ';
                $scope.objBuyerBank.BankData.Display = ' ';
                $scope.objBuyerBank.AccountTypeData.Display = ' ';
                $scope.objBuyerAddress.addressData.Display = ' ';
                $scope.objBuyerAddress.StateData.Display = ' ';
                $scope.objBuyerAddress.CityData.Display = ' ';
                $scope.objBuyerDocument.DocumentsData.Display = ' ';
                $scope.objBuyerBank.AccountTypeData.Display = ' ';
                $scope.teliphone = ' ';
                $scope.mail = ' ';
                $scope.chat = ' ';
            }
            data.ConInvId = ConInvitersIds;
            data.AgencyTypeId = data.ContactData.Value;
            data.Telephone = $scope.cmpnyteliphone != undefined ? $scope.cmpnyteliphone.toString() : "";
            data.Fax = $scope.cmpnyFaxNo != undefined ? $scope.cmpnyFaxNo.toString() : "";
            data.WebAddress = $scope.webaddress != undefined ? $scope.webaddress.toString() : "";
            data.Email = $scope.emailid != undefined ? $scope.emailid.toString() : "";


            BuyerService.CreateUpdate(data).then(function (result) {
                if (result.data.ResponseType == 1) {
                    if (tag != undefined) {
                        ResetForm();
                        toastr.success(result.data.Message);
                        window.location.href = "/master/buyer";
                    } else {
                        $scope.objBuyer.BuyerId = result.data.DataList.valueData;
                    }
                } else {
                    toastr.error(result.data.Message);
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
                //{ "title": "Buyer Id", "field": "BuyerId", filter: false, show: false },
                { "title": "Sr.", "field": "RowNumber", filter: false, show: true },
                { "title": "Contact Head", "field": "ContactType", sortable: "ContactType", filter: { AgencyType: "text" }, show: true },
                { "title": "Contact Type", "field": "AgencyType", sortable: "AgencyType", filter: { AgencyType: "text" }, show: true },
                { "title": "Company Name", "field": "CompanyName", sortable: "CompanyName", filter: { CompanyName: "text" }, show: true },
                { "title": "Web Address", "field": "WebAddress", sortable: "WebAddress", filter: { WebAddress: "text" }, show: false },
                //{ "title": "Contact Email", "field": "ContactEmail", sortable: "ContactEmail", filter: { ContactEmail: "text" }, show: false },
                { "title": "Telephone", "field": "Telephone", sortable: "Telephone", filter: { Telephone: "text" }, show: false },
                { "title": "Fax", "field": "Fax", sortable: "Fax", filter: { Fax: "text" }, show: false },
                { "title": "Remark", "field": "Remark", sortable: "Remark", filter: { Remark: "text" }, show: false },
                { "title": "Country", "field": "Country", sortable: "Country", filter: { Remark: "text" }, show: true },
                { "title": "State", "field": "StateName", sortable: "StateName", filter: { Remark: "text" }, show: true },
                { "title": "District", "field": "CityName", sortable: "CityName", filter: { CityName: "text" }, show: true },
                { "title": "Address", "field": "Addres", sortable: "Addres", filter: { CityName: "text" }, show: true },
                { "title": "Company Contact Details", "field": "ContectDetails", sortable: "ContectDetails", filter: { CityName: "text" }, show: true },
              {
                    "title": "Contact Person", "field": "ContactPersonDetail", sortable: "Person", filter: { Person: "text" }, show: true,
                    'cellTemplte': function ($scope, row) {
                        var element = "<span ng-bind-html='getHtml($parent.$parent.$parent.$parent.$parent.$parent.SetContactData(row.ContactPersonDetail))'></span>";
                        return $scope.getHtml(element);
                    }
                },
                //{ "title": "Mobile No", "field": "MobileNo", sortable: "MobileNo", filter: { MobileNo: "text" }, show: true },
                { "title": "Chat", "field": "ChatDetails", sortable: "ChatDetails", filter: { ChatDetails: "text" }, show: true },
                {
                    "title": "Email Id", "field": "Email", show: false, filter: { Email: "text" },
                    'cellTemplte': function ($scope, row) {
                        var element = "<p class='MailTo' ng-bind-html='getHtml($parent.$parent.$parent.$parent.$parent.$parent.FormatEmail(row.Email))'>";
                        return $scope.getHtml(element);
                    }
                },
                {
                    "title": "Action", show: true,
                    'cellTemplte': function ($scope, row) {
                        var element = '<a   class="btn btn-primary btn-xs" data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.BuyerId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                            //'<a class="btn btn-danger btn-xs"  data-final-confirm-box=""  data-callback="$parent.$parent.$parent.$parent.$parent.$parent.Delete(row.BuyerId)" data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                            '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.BuyerId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>' +
                            '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.VcCard(row.BuyerId)" data-uib-tooltip="VCard"><i class="fa fa-download" ></i></a>';
                        return $scope.getHtml(element);
                    }
                }
            ],
            Sort: { 'BuyerId': 'asc' }
        }

        $scope.SetContactData = function (d) {
            if (d != null) {
                var str = '';
                var Record = d.split('<br/>');
                for (var j = 0; j < Record.length - 1; j++) {
                    var conDetail = Record[j].split('|');
                    var mailto = '';
                    var emails = conDetail[2].split(',');
                    for (var i = 0; i < emails.length; i++) {
                        mailto += mailto == '' ? '' : ', ';
                        mailto += '<a href="mailto:' + emails[i] + '" target="_blank">' + emails[i] + '</a>';
                    }
                    str += conDetail[0] + ', ' + conDetail[1] + ', ' + mailto + '<br/>';
                }
                return str;
            }
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

        $scope.SendMail = function (emailId) {
            $window.open("mailto:" + emailId, "_self");
        }

        $scope.Edit = function (id) {
            //if (Permission.IsEdit == false) {
            //    toastr.error('Access is denied');
            //}
            //else {
            window.location.href = "/master/buyer/AddBuyer/" + id + "/" + 0;
            //}
        }
        $scope.View = function (id) {
            //if (Permission.IsView == false) {
            //    toastr.error('Access is denied');
            //}
            //else {
            window.location.href = "/master/buyer/AddBuyer/" + id + "/" + 1;
            //}
        }
        $scope.VcCard = function (id) {
            window.location.href = "/Master/buyer/GetVCCard/" + id;
        }
        $scope.Delete = function (id) {
            //if (Permission.IsDelete == false) {
            //    toastr.error('Access is denied');
            //}
            //else {
            BuyerService.DeleteById(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    $scope.refreshGrid();
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
            //}
        }

        $scope.$watch('objBuyerAddress.CountryData', function (data) {
            if (data.Value != '') {
                if (data.Value != $scope.objBuyerAddress.CountryId.toString()) {
                    $scope.objBuyerAddress.StateData.Display = '';
                    $scope.objBuyerAddress.StateData.Value = '';
                    $scope.objBuyerAddress.CityData.Display = '';
                    $scope.objBuyerAddress.CityData.Value = '';
                }
            } else {
                $scope.CountryBind('India');
            }

        }, true)

        $scope.$watch('objBuyerAddress.StateData', function (data) {
            if (data.Value != $scope.objBuyerAddress.StateId.toString()) {
                $scope.objBuyerAddress.CityData.Display = '';
                $scope.objBuyerAddress.CityData.Value = '';
            }
        }, true)

        $scope.CountryBind = function (data) {
            CityService.CountryBind().then(function (result) {
                if (result.data.ResponseType == 1) {
                    _.each(result.data.DataList, function (value, key, list) {
                        if (value.CountryName.toString().toLowerCase() == data.toString().toLowerCase() && $scope.objBuyerAddress.CountryData.Value == '') {
                            $scope.objBuyerAddress.CountryData = {
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

        //BEGIN MANAGE BUYER ADDRESS INFORMATION
        $scope.AddBuyerAddressDetail = function (data) {
            var dataerror = true;

            if ((data.addressData.Display == '' || data.addressData.Display == null) || (data.CountryData.Display == '' || data.CountryData.Display == null) || (data.StateData.Display == '' || data.StateData.Display == null) || (data.CityData.Display == '' || data.CityData.Display == null) || (data.Address == '' || data.Address == null) || (data.PinCode == '' || data.PinCode == null)) {
                if ((data.addressData.Display == '' || data.addressData.Display == null)) { toastr.error("Please select AddressType."); }
                else if ((data.CountryData.Display == '' || data.CountryData.Display == null)) { toastr.error("Please select Country."); }
                else if ((data.StateData.Display == '' || data.StateData.Display == null)) { toastr.error("Please select State."); }
                else if ((data.CityData.Display == '' || data.CityData.Display == null)) { toastr.error("Please select District."); }
                else if ((data.PinCode == '' || data.PinCode == null)) { toastr.error("PinCode is required."); }
                else if ((data.Address == '' || data.Address == null)) { toastr.error("Address is required."); }
                dataerror = false;
                $scope.Addresssubmitted = true;
            }
            if (dataerror == true) {

                data.addreteliphone = $scope.addreteliphone != undefined ? $scope.addreteliphone.toString() : "";
                data.addressemailid = $scope.addressemailid != undefined ? $scope.addressemailid.toString() : "";
                data.addreFaxNo = $scope.addreFaxNo != undefined ? $scope.addreFaxNo.toString() : "";
                data.addreFaxNo = $scope.addreFaxNo != undefined ? $scope.addreFaxNo.toString() : "";
                data.adderwebaddress = $scope.adderwebaddress != undefined ? $scope.adderwebaddress.toString() : "";

                $scope.Addresssubmitted = false;
                var objBuyerAddress = {
                    AddressId: data.AddressId,
                    BuyerId: data.BuyerId,
                    CountryId: data.CountryData.Value,
                    StateId: data.StateData.Value,
                    CityId: data.CityData.Value,
                    CountryName: data.CountryData.Display,
                    StateName: data.StateData.Display,
                    CityName: data.CityData.Display,
                    Address: data.Address,
                    PinCode: data.PinCode,
                    Telephone: data.addreteliphone,
                    Email: data.addressemailid,
                    Fax: data.addreFaxNo,
                    WebAddress: data.adderwebaddress,
                    AddressTypeId: data.addressData.Value,
                    AddressType: data.addressData.Display,
                    Status: data.Status,
                    CountryData: { Display: data.CountryData.Display, Value: data.CountryData.Value },
                    StateData: { Display: data.StateData.Display, Value: data.StateData.Value },
                    CityData: { Display: data.CityData.Display, Value: data.CityData.Value },
                    WeeklyOff: data.WeeklyOff
                };
                $scope.CreateUpdateAddress(objBuyerAddress);
            }
        }

        $scope.DeleteBuyerAddressDetail = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objBuyer.BuyerAddressDetails[index] = data;
                } else {
                    $scope.objBuyer.BuyerAddressDetails.splice(index, 1);
                }
                toastr.success("Buyer Address Detail Delete", "Success");
            })
        }

        $scope.EditBuyerAddressDetail = function (data, index) {
            $scope.EditBuyerAddressIndex = index;
            $scope.objBuyerAddress = {
                AddressId: data.AddressId,
                BuyerId: data.BuyerId,
                CountryId: data.CountryData.Value,
                StateId: data.StateData.Value,
                CityId: data.CityData.Value,
                CountryName: data.CountryData.Display,
                StateName: data.StateData.Display,
                CityName: data.CityData.Display,
                Address: data.Address,
                PinCode: data.PinCode,
                addressData: { Display: data.AddressType, Value: data.AddressTypeId },
                //AddressTypeId: data.addressData.Value,
                //AddressType: data.addressData.Display,
                WebAddress: data.WebAddress,
                WeeklyOff: data.WeeklyOff,
                Status: data.Status,
                CountryData: { Display: data.CountryData.Display, Value: data.CountryData.Value },
                StateData: { Display: data.StateData.Display, Value: data.StateData.Value },
                CityData: { Display: data.CityData.Display, Value: data.CityData.Value }
            };
            $scope.addreteliphone = (data.Telephone != '' && data.Telephone != null) ? data.Telephone.split(",") : [];
            $scope.addressemailid = (data.Email != '' && data.Email != null) ? data.Email.split(",") : [];
            $scope.addreFaxNo = (data.Fax != '' && data.Fax != null) ? data.Fax.split(",") : [];
            $scope.adderwebaddress = (data.WebAddress != '' && data.WebAddress != null) ? data.WebAddress.split(",") : [];
        }

        $scope.CreateUpdateAddress = function (data) {
            data.CountryId = data.CountryData.Value;
            data.CountryName = data.CountryData.Display;
            data.StateId = data.StateData.Value;
            data.StateName = data.StateData.Display;
            data.CityId = data.CityData.Value;
            data.CityName = data.CityData.Display;

            if ($scope.EditBuyerAddressIndex > -1) {
                $scope.objBuyer.BuyerAddressDetails[$scope.EditBuyerAddressIndex] = data;
                $scope.EditBuyerAddressIndex = -1;
            } else {
                data.Status = 1;
                $scope.objBuyer.BuyerAddressDetails.push(data);
            }
            $scope.objBuyerAddress = {
                AddressId: 0,
                BuyerId: 0,
                CountryId: 0,
                StateId: 0,
                CityId: 0,
                CountryName: '',
                StateName: '',
                CityName: '',
                Address: '',
                PinCode: '',
                WeeklyOff: '',
                Status: 0,
                Telephone: '',
                Email: '',
                Fax: '',
                WebAddress: '',
                addressData: { Display: '', Value: '' },
                CountryData: { Display: '', Value: 0 },
                StateData: { Display: '', Value: 0 },
                CityData: { Display: '', Value: 0 }
            };
            $scope.addressemailid = [];
            $scope.addreteliphone = [];
            $scope.addreFaxNo = [];
            $scope.adderwebaddress = [];
        }
        //END MANAGE BUYER ADDRESS INFORMATION

        //BEGIN MANAGE CONTACT BUYER INFORMATION
        $scope.AddBuyerContactDetail = function (data) {
            var dataerror = true;
            data.teliphone = $scope.teliphone != undefined ? $scope.teliphone.toString() : "";
            data.mail = $scope.mail != undefined ? $scope.mail.toString() : "";
            data.chat = $scope.chat != undefined ? $scope.chat.toString() : "";

            if ((data.ContactPerson == '' || data.ContactPerson == null) || (data.teliphone.toString() == '' || data.teliphone.toString() == null) || (data.mail.toString() == '' || data.mail.toString() == null)) {
                dataerror = false;
                if ((data.ContactPerson == '' || data.ContactPerson == null)) { toastr.error("Contact Person is Required."); }
                else if ((data.teliphone.toString() == '' || data.teliphone.toString() == null)) { toastr.error("Mobile No. is Required."); }
                else if ((data.mail.toString() == '' || data.mail.toString() == null)) { toastr.error("Email Id is Required."); }
                //else if ((data.chat.toString() == '' || data.chat.toString() == null)) { toastr.error("Chat is Required."); }
                //toastr.error("Please fill whatever matter.", "Error");
                $scope.Contactsubmitted = true;
            }
            if (dataerror == true) {
                $scope.Contactsubmitted = false;
                var objBuyerContact = {
                    ContactId: data.ContactId,
                    BuyerId: data.BuyerId,
                    ContactPerson: data.ContactPerson,
                    Surname: data.Surname,
                    DesignationId: data.DesignationData.Value,
                    DesignationName: data.DesignationData.Display,
                    MobileNo: $scope.teliphone.toString(),
                    Email: $scope.mail.toString(),
                    Chat: $scope.chat != undefined ? $scope.chat.toString() : "",
                    Status: data.Status, //1 : Insert , 2:Update ,3 :Delete
                    DesignationData: {
                        Display: data.DesignationData.Display,
                        Value: data.DesignationData.Value
                    }
                };
                $scope.CreateUpdateContact(objBuyerContact);
            }
        }

        $scope.DeleteBuyerDetail = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objBuyer.BuyerContactDetails[index] = data;
                } else {
                    $scope.objBuyer.BuyerContactDetails.splice(index, 1);
                }
                toastr.success("Buyer contact detail Delete", "Success");
            })
        }

        $scope.EditBuyerDetail = function (data, index) {
            $scope.EditBuyerContactIndex = index;
            if (data.MobileNo == null) { data.MobileNo = ''; }
            if (data.Email == null) { data.Email = ''; }
            if (data.Chat == null) { data.Chat = ''; }
            $scope.teliphone = (data.MobileNo != '') ? data.MobileNo.split(",") : [];
            $scope.mail = (data.Email != '') ? data.Email.split(",") : [];
            $scope.chat = (data.Chat != '') ? data.Chat.split(",") : [];
            $scope.objBuyerContact = {
                ContactId: data.ContactId,
                BuyerId: data.BuyerId,
                ContactPerson: data.ContactPerson,
                Surname: data.Surname,
                DesignationId: data.DesignationData.Value,
                DesignationName: data.DesignationData.Display,
                MobileNo: $scope.teliphone.toString(),
                Email: $scope.mail.toString(),
                Chat: $scope.chat != undefined ? $scope.chat.toString() : "",
                Status: data.Status, //1 : Insert , 2:Update ,3 :Delete
                DesignationData: {
                    Display: data.DesignationData.Display,
                    Value: data.DesignationData.Value
                }
            };
        }

        $scope.CreateUpdateContact = function (data) {
            data.DesignationId = data.DesignationData.Value == 0 ? null : data.DesignationData.Value;
            data.DesignationName = data.DesignationData.Display;
            data.MobileNo = this.teliphone.toString();
            data.Email = this.mail.toString();
            data.Chat = this.chat != undefined ? this.chat.toString() : "";

            if ($scope.EditBuyerContactIndex > -1) {
                $scope.objBuyer.BuyerContactDetails[$scope.EditBuyerContactIndex] = data;
                $scope.EditBuyerContactIndex = -1;
            } else {
                data.Status = 1;
                $scope.objBuyer.BuyerContactDetails.push(data);
            }
            $scope.objBuyerContact = {
                ContactId: 0,
                BuyerId: 0,
                ContactPerson: '',
                Surname: '',
                DesignationId: 0,
                DesignationName: '',
                MobileNo: '',
                Chat: '',
                Email: '',
                Status: 0,
                DesignationData: { Display: '', Value: '' }
            };
            $scope.teliphone = [];
            $scope.mail = [];
            $scope.chat = [];
        }
        //END MANAGE CONTACT BUYER INFORMATION


        //BEGIN MANAGE BANK BUYER INFORMATION
        $scope.AddBuyerBankDetail = function (data) {
            var dataerror = true;
            // (data.BranchName == '' || data.BranchName == null) || 
            if ((data.BeneficiaryName == '' || data.BeneficiaryName == null) || (data.BankData.Display == '' || data.BankData.Display == null) || (data.AccountTypeData.Display == '' || data.AccountTypeData.Display == null) || (data.AccountNo == '' || data.AccountNo == null) || (data.IFSCCode == '' || data.IFSCCode == null)) {
                dataerror = false;
                if ((data.BeneficiaryName == '' || data.BeneficiaryName == null)) { toastr.error("Beneficiary Name is required."); }
                else if ((data.BankData.Display == '' || data.BankData.Display == null)) { toastr.error("Bank Name is required."); }
                //else if ((data.BranchName == '' || data.BranchName == null)) { toastr.error("Branch Name is required"); }
                else if ((data.AccountTypeData.Display == '' || data.AccountTypeData.Display == null)) { toastr.error(" Account Type is required."); }
                else if ((data.AccountNo == '' || data.AccountNo == null)) { toastr.error("AccountNo is required."); }
                else if ((data.IFSCCode == '' || data.IFSCCode == null)) { toastr.error("IFSCCode is required."); }
                $scope.Banksubmitted = true;
            }
            if (dataerror == true) {
                $scope.Banksubmitted = false;
                var objBuyerBank = {
                    BankDetailID: data.BankDetailID,
                    BuyerId: data.BuyerId,
                    BeneficiaryName: data.BeneficiaryName,
                    NickName: data.NickName,
                    BankNameId: data.BankData.Value,
                    BankName: data.BankData.Display,
                    BranchName: data.BranchName,
                    AccountNo: data.AccountNo,
                    AccountTypeId: data.AccountTypeData.Value,
                    AccountType: data.AccountTypeData.Display,
                    IFSCCode: data.IFSCCode,
                    SwiftCode: data.SwiftCode,
                    Status: data.Status, //1 : Insert , 2:Update ,3 :Delete
                    BankData: {
                        Display: data.BankData.Display, Value: data.BankData.Value
                    },
                    AccountTypeData: {
                        Display: data.AccountTypeData.Display, Value: data.AccountTypeData.Value
                    }
                };
                $scope.CreateUpdateBank(objBuyerBank);
            }
        }

        $scope.DeleteBuyerBankDetail = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objBuyer.BuyerBankDetails[index] = data;
                } else {
                    $scope.objBuyer.BuyerBankDetails.splice(index, 1);
                }
                toastr.success("Buyer Bank Detail Delete", "Success");
            })
        }

        $scope.EditBuyerBankDetail = function (data, index) {
            $scope.EditBuyerBankIndex = index;
            $scope.objBuyerBank = {
                BankDetailID: data.BankDetailID,
                BuyerId: data.BuyerId,
                BeneficiaryName: data.BeneficiaryName,
                NickName: data.NickName,
                BankNameId: data.BankData.Value,
                BankName: data.BankData.Display,
                BranchName: data.BranchName,
                AccountNo: data.AccountNo,
                AccountTypeId: data.AccountTypeData.Value,
                AccountType: data.AccountTypeData.Display,
                IFSCCode: data.IFSCCode,
                SwiftCode: data.SwiftCode,
                Status: data.Status, //1 : Insert , 2:Update ,3 :Delete
                BankData: {
                    Display: data.BankData.Display, Value: data.BankData.Value
                },
                AccountTypeData: {
                    Display: data.AccountTypeData.Display, Value: data.AccountTypeData.Value
                }
            };
        }

        $scope.CreateUpdateBank = function (data) {
            data.BankNameId = data.BankData.Value;
            data.BankName = data.BankData.Display;
            data.AccountTypeId = data.AccountTypeData.Value;
            data.AccountType = data.AccountTypeData.Display;

            if ($scope.EditBuyerBankIndex > -1) {
                $scope.objBuyer.BuyerBankDetails[$scope.EditBuyerBankIndex] = data;
                $scope.EditBuyerBankIndex = -1;
            } else {
                data.Status = 1;
                $scope.objBuyer.BuyerBankDetails.push(data);
            }

            $scope.objBuyerBank = {
                BankDetailID: 0,
                BuyerId: 0,
                BeneficiaryName: '',
                NickName: '',
                BankName: '',
                BankNameId: 0,
                BranchName: '',
                AccountNo: '',
                AccountTypeId: 0,
                AccountType: '',
                IFSCCode: '',
                SwiftCode: '',
                Status: 0,
                BankData: { Display: '', Value: '' },
                AccountTypeData: { Display: '', Value: '' }
            };
        }
        //END MANAGE BANK BUYER INFORMATION

        //BEGIN MANAGE TAX BUYER INFORMATION
        $scope.AddBuyerTaxDetail = function (data) {
            var dataerror = true;
            if ((data.LicenseData.Display == '' || data.LicenseData.Display == null)) {
                toastr.error("Tax is required.");
                dataerror = false;
                $scope.Taxsubmittedd = true;
            } else if (data.LicenseNo == '' || data.LicenseNo == null) {
                toastr.error("Tax Value is required.");
                dataerror = false;
                $scope.Taxsubmittedd = true;
            }
            if (dataerror == true) {
                $scope.Taxsubmittedd = false;
                var objBuyerTax = {
                    BuyerLicenseId: data.BuyerLicenseId,
                    LicenseId: data.LicenseData.Value,
                    LicenseName: data.LicenseData.Display,
                    BuyerId: data.BuyerId,
                    LicenseNo: data.LicenseNo,
                    Status: data.Status, //1 : Insert , 2:Update ,3 :Delete
                    LicenseData: { Display: data.LicenseData.Display, Value: data.LicenseData.Value },
                };
                $scope.CreateUpdateTax(objBuyerTax);
            }
        }

        $scope.EditBuyerTaxDetail = function (data, index) {
            $scope.EditBuyerTaxIndex = index;
            $scope.objBuyerTax = {
                BuyerLicenseId: data.BuyerLicenseId,
                LicenseId: data.LicenseData.Value,
                LicenseName: data.LicenseData.Display,
                BuyerId: data.BuyerId,
                LicenseNo: data.LicenseNo,
                Status: data.Status, //1 : Insert , 2:Update ,3 :Delete
                LicenseData: { Display: data.LicenseData.Display, Value: data.LicenseData.Value },
            };
        }

        $scope.DeleteBuyerTaxDetail = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objBuyer.BuyerLicenseDetails[index] = data;
                } else {
                    $scope.objBuyer.BuyerLicenseDetails.splice(index, 1);
                }
                toastr.success("Tax Details Delete", "Success");
            })
        }

        $scope.CreateUpdateTax = function (data) {
            data.LicenseId = data.LicenseData.Value;
            data.LicenseName = data.LicenseData.Display;

            if ($scope.EditBuyerTaxIndex > -1) {
                $scope.objBuyer.BuyerLicenseDetails[$scope.EditBuyerTaxIndex] = data;
                $scope.EditBuyerTaxIndex = -1;
            } else {
                data.Status = 1;
                $scope.objBuyer.BuyerLicenseDetails.push(data);
            }
            $scope.objBuyerTax = {
                BuyerLicenseId: 0,
                LicenseId: 0,
                LicenseName: 0,
                BuyerId: 0,
                LicenseNo: '',
                Status: 0, //1 : Insert , 2:Update ,3 :Delete
                LicenseData: { Display: '', Value: '' },
            };
        }
        //END MANAGE TAX BUYER INFORMATION

        //BEGIN MANAGE DOCUMENT BUYER INFORMATION
        $scope.AddBuyerDocumentDetail = function (data) {
            var dataerror = true;
            if ((data.DocumentsData.Display == '' || data.DocumentsData.Display == null)) {
                toastr.error("Document Type is required.");
                dataerror = false;
                $scope.Docsubmittedd = true;
            } else if (data.DocumentValue == '' || data.DocumentValue == null) {
                toastr.error("Please Upload Document.");
                dataerror = false;
                $scope.Docsubmittedd = true;
            }
            if (dataerror == true) {
                $scope.Docsubmittedd = false;
                var objBuyerDocument = {
                    DocID: data.DocID,
                    BuyerId: data.BuyerId,
                    DocumentId: data.DocumentsData.Value,
                    DocumentName: data.DocumentsData.Display,
                    DocumentValue: data.DocumentValue,
                    DocumentType: data.DocumentType,
                    Status: data.Status,
                    DocumentsData: { Display: data.DocumentsData.Display, Value: data.DocumentsData.Value }
                };
                $scope.CreateUpdateDocument(objBuyerDocument);
            }
        }

        $scope.EditBuyerDocumentDetail = function (data, index) {
            $scope.EditBuyerDocumentIndex = index;
            $scope.objBuyerDocument = {
                DocID: data.DocID,
                BuyerId: data.BuyerId,
                DocumentId: data.DocumentsData.Value,
                DocumentName: data.DocumentsData.Display,
                DocumentValue: data.DocumentValue,
                DocumentType: data.DocumentType,
                Status: data.Status,
                DocumentsData: { Display: data.DocumentsData.Display, Value: data.DocumentsData.Value }
            };
            $scope.tempDocument = data.DocumentValue;
            $scope.tempType = data.DocumentType;
        }

        $scope.DeleteBuyerDocumentDetail = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objBuyer.BuyerDocumentDetails[index] = data;
                } else {
                    $scope.objBuyer.BuyerDocumentDetails.splice(index, 1);
                    $scope.objBuyerDocument.DocumentValue = '';
                    $scope.objBuyerDocument.DocumentsData = '';
                    $scope.tempDocument = '';
                    $scope.tempType = '';
                }
                toastr.success("Document Details Delete", "Success");
            })
        }

        $scope.CreateUpdateDocument = function (data) {
            data.DocumentId = data.DocumentsData.Value;
            data.DocumentName = data.DocumentsData.Display;

            if ($scope.EditBuyerDocumentIndex > -1) {
                $scope.objBuyer.BuyerDocumentDetails[$scope.EditBuyerDocumentIndex] = data;
                $scope.EditBuyerDocumentIndex = -1;
            } else {
                data.Status = 1;
                $scope.objBuyer.BuyerDocumentDetails.push(data);
            }
            $scope.objBuyerDocument = {
                DocID: 0,
                BuyerId: 0,
                DocumentId: '',
                DocumentName: '',
                DocumentValue: '',
                DocumentType: '',
                Status: 0,
                DocumentsData: { Display: '', Value: '' }
            };
            angular.forEach($scope.objBuyer.BuyerDocumentDetails, function (value, index) {
                if (value.DocumentValue.indexOf('/UploadImages/') == -1) {
                    if (value.Status == 1) {
                        value.DocumentValue = '/UploadImages/TempImg/' + value.DocumentValue;
                    } else if (value.Status == 2) {
                        value.DocumentValue = '/UploadImages/ContactDocumentImage/' + value.DocumentValue;
                    }
                }
            }, true);
            $scope.objBuyerDocument.DocumentValue = '';
            $scope.objBuyerDocument.DocumentType = '';
            $scope.tempDocument = '';
            $scope.tempType = '';
        }

        $scope.UploadDocumentFile = function (file) {
            $scope.objBuyerDocument.DocumentValue = '';
            $scope.objBuyerDocument.DocumentType = '';
            Upload.upload({
                url: "/Handler/FileUpload.ashx",
                method: 'POST',
                file: file,
            }).then(function (result) {
                var ext = result.config.file[0].name.split('.').pop();
                if (ext == "docx" || ext == "doc" || ext == "xlsx" || ext == "xls" || ext == "pdf" || ext == "jpg" || ext == "png") {
                    if (result.status == 200) {
                        if (result.data.length > 0) {
                            $scope.objBuyerDocument.DocumentValue = result.data[0].imageName;
                            $scope.objBuyerDocument.DocumentType = ext;
                            $scope.tempDocument = result.data[0].imagePath;
                            $scope.tempType = ext;
                        }
                    }
                    else {
                        $scope.objBuyerDocument.DocumentValue = '';
                        $scope.objBuyerDocument.DocumentType = '';
                    }
                } else {
                    toastr.error("Only Word, Excel, PDF, JPG, PNG File Allowed.", "Error");
                }
            });
        }
        //END MANAGE TAX BUYER INFORMATION

        $scope.statusFilter = function (val) {
            if (val.Status != 3)
                return true;
        }
    }
})()

function check(e) {
    var keynum
    var keychar
    var numcheck
    // For Internet Explorer
    if (window.event) {
        keynum = e.keyCode;
    }
    // For Netscape/Firefox/Opera
    else if (e.which) {
        keynum = e.which;
    }
    keychar = String.fromCharCode(keynum);
    //List of special characters you want to restrict
    if (keychar == "'" || keychar == "`" || keychar == "!" || keychar == "@" || keychar == "#" || keychar == "$" || keychar == "%" || keychar == "^" || keychar == "*" || keychar == "-" || keychar == "_" || keychar == "+" || keychar == "=" || keychar == "/" || keychar == "~" || keychar == "<" || keychar == ">" || keychar == "," || keychar == ";" || keychar == ":" || keychar == "|" || keychar == "?" || keychar == "{" || keychar == "}" || keychar == "[" || keychar == "]" || keychar == "¬" || keychar == "£" || keychar == '"' || keychar == "\\") {
        return false;
    } else {
        return true;
    }
}