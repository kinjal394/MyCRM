(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("UserController", ["$scope", "UserService", "$timeout", "$filter", "$uibModal", "CountryService", "RelationService", "Upload", "$q", "CityService", "SourceService",
    function UserController($scope, UserService, $timeout, $filter, $uibModal, CountryService, RelationService, Upload, $q, CityService, SourceService) {
        $scope.objUser = {
            SourceId: 0,
            SourceName: '',
            UserId: 0,
            DepartmentId: 0,
            DepartmentName: '',
            DesignationId: 0,
            DesignationName: '',
            RoleId: 0,
            RoleName: '',
            UserName: '',
            UserCode: '',
            Name: '',
            FatherName: '',
            Surname: '',
            BirthDate: '',
            Gender: '',
            MaritalStatus: '',
            BirthPlaceCityId: '',
            BirthPlaceStateId: '',
            BirthPlaceCountryId: '',
            BirthPlaceArea: '',
            HomeTownStateId: '',
            HomeTownCountryId: '',
            HomeTownCityId: '',
            HomeTownArea: '',
            HomeTownPinCode: '',
            BloodGroupId: '',
            Email: '',
            //MobCode: '',
            MobNo: '',
            ResidentNo: '',
            //ResidentNo: '',
            PresentPropType: 0,
            PresentArea: '',
            PresentCityId: '',
            PresentPinCode: '',
            PresentAddress: '',
            PermanentPropType: 0,
            PermanentArea: '',
            PermanentCityId: '',
            PermanentPinCode: '',
            PermanentAddress: '',
            Photo: '',
            FamilyRefName: '',
            FamilyResidentAddress: '',
            FamilyRefContactNo: '',
            FamilyRefEmail: '',
            RelativeRefName: '',
            RelativeRefSurname: '',
            RelativeResidentAddress: '',
            RelativeRefCityId: '',
            RelativeRefArea: '',
            RelativeRefContactCode: '',
            RelativeRefContactNo: '',
            RelativeRefEmail: '',
            FriendRefName: '',
            FriendRefSurname: '',
            FriendResidentAddress: '',
            FriendRefCityId: '',
            FriendRefArea: '',
            FriendRefContactCode: '',
            FriendRefContactNo: '',
            FriendRefEmail: '',
            WorkMaterRefName: '',
            WorkMaterRefSurname: '',
            WorkMaterResidentAddress: '',
            WorkMaterRefCityId: '',
            WorkMaterRefArea: '',
            WorkMaterRefContactCode: '',
            WorkMaterRefContactNo: '',
            WorkMaterRefEmail: '',
            DrivingLicNo: '',
            VoterIdNumber: '',
            PassportNo: '',
            PANNo: '',
            AadharNo: '',
            FullNameAsPerBank: '',
            BranchName: '',
            BankNameId: '',
            BankName: '',
            AccountNo: '',
            AccountTypeId: '',
            IFSC: '',
            BranchAddress: '',
            MICRCode: '',
            ReportingId: 0,
            JoiningDate: '',
            CommunicationDate: new Date(),
            CountryId: 0,//
            ReferanceMode: '',
            ReferenceTypeId: '',
            ReferenceSubType: '',
            ReferenceId: '',
            ReferenceMannualEntry: '',
            MyProfile: false,
            ChatDetail: '',
            QualificationId: 0,
            QualificationName: 0,
            TotalWorkExperience: '',
            TotalExpeYear: '',
            TotalExpeMonth: '',
            //Skype: '',
            ShiftStartTime: '',
            ShiftEndTime: '',
            LunchStartTime: '',
            LunchEndTime: '',
            BankNameData: {
                Display: "",
                Value: ""
            },
            DesignationData: {
                Display: "",
                Value: ""
            },
            DepartmentData: {
                Display: "",
                Value: ""
            },
            RoleData: {
                Display: "",
                Value: ""
            },
            AccountTypeData: {
                Display: "",
                Value: ""
            },
            AgencyTypeReferanceData: {
                Display: "",
                Value: ""
            },
            ReferanceData: {
                Display: "",
                Value: ""
            },
            ReportingData: {
                Display: "",
                Value: ""
            },
            BloodGroupData: {
                Display: "",
                Value: ""
            },
            BirthCountryData: {
                Display: "",
                Value: ""
            },
            BirthStateData: {
                Display: "",
                Value: ""
            },
            BirthCityData: {
                Display: "",
                Value: ""
            },
            HomeCountryData: {
                Display: "",
                Value: ""
            },
            HomeStateData: {
                Display: "",
                Value: ""
            },
            HomeCityData: {
                Display: "",
                Value: ""
            },
            PresentAddrCountryData: {
                Display: "",
                Value: ""
            },
            PresentAddrStateData: {
                Display: "",
                Value: ""
            },
            PresentAddrCityData: {
                Display: "",
                Value: ""
            },
            PermanentAddrCountryData: {
                Display: "",
                Value: ""
            },
            PermanentAddrStateData: {
                Display: "",
                Value: ""
            },
            PermanentAddrCityData: {
                Display: "",
                Value: ""
            },
            RelativesAddrCountryData: {
                Display: "",
                Value: ""
            },
            RelativesAddrStateData: {
                Display: "",
                Value: ""
            },
            RelativesAddrCityData: {
                Display: "",
                Value: ""
            },
            //RelativeRefMobNoData: { Display: '', Value: '' },
            FriendAddrCountryData: {
                Display: "",
                Value: ""
            },
            FriendAddrStateData: {
                Display: "",
                Value: ""
            },
            FriendAddrCityData: {
                Display: "",
                Value: ""
            },
            //FriendRefMobNoData: { Display: '', Value: '' },
            WorkmateAddrCountryData: {
                Display: "",
                Value: ""
            },
            WorkmateAddrStateData: {
                Display: "",
                Value: ""
            },
            WorkmateAddrCityData: {
                Display: "",
                Value: ""
            },
            SourceData: {
                Display: "",
                Value: ""
            },
            QualificationData: {
                Display: '',
                Value: ''
            },

            UserContactDetails: [],
            UserReferanceDetails: [],
            UserSalDetails: [],
            UserDocumentDetails: [],
            UserExperDetails: [],
            UserEduDetails: []
        };

        $scope.outputTel1 = [];
        $scope.outputTel2 = [];
        $scope.telArray1 = [];
        $scope.telArray2 = [];
        $scope.MobCodeData = [];
        $scope.emailCodeData = [];
        $scope.ContactMobCodeData = [];
        $scope.contactemailCodeData = [];
        $scope.ReferenceMobCodeData = [];
        $scope.ReferenceemailCodeData = [];
        $scope.chatCodeData = [];

        $scope.EmpContactMobCode = [];
        $scope.EmpContactEmail = [];
        $scope.EmpContactchat = [];
        $scope.EmpContactMobCodeData = [];
        $scope.EmpContactchatCodeData = [];

        $scope.MobCodeArray = [];
        $scope.RelativeRefMobNoData = [];
        $scope.RelativeRefMobNoArray = [];
        $scope.WorkmateRefMobNoData = [];
        $scope.WorkmateRefMobNoArray = [];
        $scope.FriendRefMobNoData = [];
        $scope.FriendRefMobNoArray = [];
        $scope.error = false;
        $scope.isClicked = false;
        $scope.objUserContact = {
            EmpRelationId: 0,
            Relation: 0,
            RelationId: 0,
            UserId: 0,
            Name: '',
            Email: '',
            ContactNo: '',
            ContactCode: '',
            UserContactcode: '',
            Status: 0,//1 : Insert , 2:Update ,3 :Delete
            ContactCodeData: { Display: '', Value: '' },
            RelationData: { Display: '', Value: '' }
        };
        $scope.objUserRef = {
            ReffId: 0,
            UserId: 0,
            ReffType: 0,
            ReffTypeName: '',
            ReffName: '',
            Email: '',
            MobileNoId: '',
            MobileNoCode: '',
            MobileNo: '',
            Address: '',
            CountryId: 0,
            CountryName: '',
            StateId: 0,
            StateName: '',
            CityId: 0,
            CityName: '',
            Pincode: '',
            Status: 0,//1 : Insert , 2:Update ,3 :Delete
            ReffTypeData: { Display: '', Value: '' },
            RelativesAddrCountryData: {
                Display: "",
                Value: ""
            },
            RelativesAddrStateData: {
                Display: "",
                Value: ""
            },
            RelativesAddrCityData: {
                Display: "",
                Value: ""
            },
        };
        $scope.objUserSalary = {
            EmpSalId: 0,
            UserId: 0,
            SalaryHeadId: 0,
            SalaryHead: '',
            CurrencyId: 0,
            Currency: '',
            INRAmount: '',
            ExchangeRate: '',
            Amount: '',
            Status: 0,//1 : Insert , 2:Update ,3 :Delete
            SalaryHeadData: { Display: '', Value: '' },
            CurrencyData: { Display: '', Value: '' }
        };
        $scope.objUserDocument = {
            EmpDocId: 0,
            UserId: 0,
            DocId: 0,
            Documents: '',
            DocValue: '',
            DocUpload: '',
            DocType: '',
            Status: 0,//1 : Insert , 2:Update ,3 :Delete
            DocumentsTypeData: { Display: '', Value: '' }
        };
        $scope.objUserEducation = {
            EduId: 0,
            UserId: 0,
            OrganizationName: '',
            CityId: 0,
            CityName: '',
            StateId: 0,
            StateName: '',
            CountryId: 0,
            CountryName: '',
            Address: '',
            FromDate: '',
            ToDate: '',
            PinCode: '',
            TotalExpeYear: '',
            TotalExpeMonth: '',
            TotalWorkExperience: '',
            DisplayFromDate: '',
            DisplayToDate: '',
            Designation: 0,
            DesignationName: '',
            Description: '',
            Status: 0,//1 : Insert , 2:Update ,3 :Delete
            EduCountryData: { Display: '', Value: '' },
            EduStateData: { Display: '', Value: '' },
            EduCityData: { Display: '', Value: '' },
            EduDesignationData: { Display: '', Value: '' }
        }
        $scope.objUserExpEducation = {
            EducationId: 0,
            UserId: 0,
            InstituteName: '',
            QualificationId: 0,
            QualificationName: '',
            CityId: 0,
            CityName: '',
            StateId: 0,
            StateName: '',
            CountryId: 0,
            CountryName: '',
            Address: '',
            FromDate: '',
            ToDate: '',
            PinCode: '',
            DisplayFromDate: '',
            DisplayToDate: '',
            EduDescription: '',
            Status: 0,//1 : Insert , 2:Update ,3 :Delete
            EduCountryData: { Display: '', Value: '' },
            EduStateData: { Display: '', Value: '' },
            EduCityData: { Display: '', Value: '' },
            EducationData: { Display: '', Value: '' }
        }

        $scope.id = 0;
        $scope.objUser = $scope.objUser || {};
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};
        function ResetForm() {
            $scope.objUser = {
                UserId: 0,
                DepartmentId: 0,
                DepartmentName: '',
                DesignationId: 0,
                DesignationName: '',
                RoleId: 0,
                RoleName: '',
                UserName: '',
                UserCode: '',
                Name: '',
                FatherName: '',
                Surname: '',
                Gender: '',
                MaritalStatus: '',
                BirthDate: '',
                BirthPlaceCityId: '',
                BirthPlaceArea: '',
                BirthPlacePinCode: '',
                HomeTownCityId: '',
                HomeTownArea: '',
                HomeTownPinCode: '',
                BloodGroupId: '',
                Email: '',
                SourceId: 0,
                SourceName: '',
                QualificationId: 0,
                QualificationName: 0,
                TotalWorkExperience: '',
                TotalExpeYear: '',
                TotalExpeMonth: '',
                //MobCode: '',
                MobNo: '',
                ResidentNo: '',
                //ResidentNo: '',
                PresentPropType: 0,
                PresentArea: '',
                PresentCityId: '',
                PresentPinCode: '',
                PresentAddress: '',
                PermanentPropType: 0,
                PermanentArea: '',
                PermanentCityId: '',
                PermanentPinCode: '',
                PermanentAddress: '',
                Photo: '',
                FamilyRefName: '',
                FamilyResidentAddress: '',
                FamilyRefContactNo: '',
                FamilyRefEmail: '',
                RelativeRefName: '',
                RelativeRefSurname: '',
                RelativeResidentAddress: '',
                RelativeRefCityId: '',
                RelativeRefArea: '',
                RelativeRefContactCode: '',
                RelativeRefContactNo: '',
                RelativeRefEmail: '',
                FriendRefName: '',
                FriendRefSurname: '',
                FriendResidentAddress: '',
                FriendRefCityId: '',
                FriendRefArea: '',
                FriendRefContactCode: '',
                FriendRefContactNo: '',
                FriendRefEmail: '',
                WorkMaterRefName: '',
                WorkMaterRefSurname: '',
                WorkMaterResidentAddress: '',
                WorkMaterRefCityId: '',
                WorkMaterRefArea: '',
                WorkMaterRefContactCode: '',
                WorkMaterRefContactNo: '',
                WorkMaterRefEmail: '',
                DrivingLicNo: '',
                VoterIdNumber: '',
                PassportNo: '',
                PANNo: '',
                AadharNo: '',
                FullNameAsPerBank: '',
                BranchName: '',
                BankNameId: '',
                BankName: '',
                AccountNo: '',
                AccountTypeId: '',
                IFSC: '',
                BranchAddress: '',
                MICRCode: '',
                ReportingId: 0,
                JoiningDate: '',
                CommunicationDate: new Date(),
                CountryId: 0,
                ReferanceMode: '',
                ReferenceTypeId: '',
                ReferenceSubType: '',
                ReferenceId: '',
                ReferenceMannualEntry: '',
                ChatDetail: '',
                //Skype: '',
                ShiftStartTime: '',
                ShiftEndTime: '',
                MyProfile: false,
                BankNameData: {
                    Display: "",
                    Value: ""
                },
                DesignationData: {
                    Display: "",
                    Value: ""
                },
                DepartmentData: {
                    Display: "",
                    Value: ""
                },
                RoleData: {
                    Display: "",
                    Value: ""
                },
                AccountTypeData: {
                    Display: "",
                    Value: ""
                },
                AgencyTypeReferanceData: {
                    Display: "",
                    Value: ""
                },
                ReferanceData: {
                    Display: "",
                    Value: ""
                },
                ReportingData: {
                    Display: "",
                    Value: ""
                },
                BloodGroupData: {
                    Display: "",
                    Value: ""
                },
                BirthCountryData: {
                    Display: "",
                    Value: ""
                },
                BirthStateData: {
                    Display: "",
                    Value: ""
                },
                BirthCityData: {
                    Display: "",
                    Value: ""
                },
                HomeCountryData: {
                    Display: "",
                    Value: ""
                },
                HomeStateData: {
                    Display: "",
                    Value: ""
                },
                HomeCityData: {
                    Display: "",
                    Value: ""
                },
                PresentAddrCountryData: {
                    Display: "",
                    Value: ""
                },
                PresentAddrStateData: {
                    Display: "",
                    Value: ""
                },
                PresentAddrCityData: {
                    Display: "",
                    Value: ""
                },
                PermanentAddrCountryData: {
                    Display: "",
                    Value: ""
                },
                PermanentAddrStateData: {
                    Display: "",
                    Value: ""
                },
                PermanentAddrCityData: {
                    Display: "",
                    Value: ""
                },
                RelativesAddrCountryData: {
                    Display: "",
                    Value: ""
                },
                RelativesAddrStateData: {
                    Display: "",
                    Value: ""
                },
                RelativesAddrCityData: {
                    Display: "",
                    Value: ""
                },
                //RelativeRefMobNoData: { Display: '', Value: '' },
                FriendAddrCountryData: {
                    Display: "",
                    Value: ""
                },
                FriendAddrStateData: {
                    Display: "",
                    Value: ""
                },
                FriendAddrCityData: {
                    Display: "",
                    Value: ""
                },
                //FriendRefMobNoData: { Display: '', Value: '' },
                WorkmateAddrCountryData: {
                    Display: "",
                    Value: ""
                },
                WorkmateAddrStateData: {
                    Display: "",
                    Value: ""
                },
                WorkmateAddrCityData: {
                    Display: "",
                    Value: ""
                },
                //WorkmateRefMobNoData: { Display: '', Value: '' },
                //MobCodeData: { Display: '', Value: '' },
                QualificationData: { Display: '', Value: '' },
                SourceData: { Display: '', Value: '' }

            };
            $scope.outputTel1 = [];
            $scope.outputTel2 = [];
            $scope.telArray1 = [];
            $scope.telArray2 = [];
            $scope.MobCodeData = [];
            $scope.emailCodeData = [];
            $scope.ContactMobCodeData = [];
            $scope.contactemailCodeData = [];
            $scope.ReferenceMobCodeData = [];
            $scope.ReferenceemailCodeData = [];
            $scope.chatCodeData = [];

            $scope.MobCodeArray = [];
            $scope.RelativeRefMobNoData = [];
            $scope.RelativeRefMobNoArray = [];
            $scope.WorkmateRefMobNoData = [];
            $scope.WorkmateRefMobNoArray = [];
            $scope.FriendRefMobNoData = [];
            $scope.FriendRefMobNoArray = [];
            $scope.objUserEducation = {
                EduId: 0,
                UserId: 0,
                OrganizationName: '',
                CityId: 0,
                CityName: '',
                StateId: 0,
                StateName: '',
                CountryId: 0,
                CountryName: '',
                Address: '',
                FromDate: '',
                ToDate: '',
                PinCode: '',
                TotalExpeYear: '',
                TotalExpeMonth: '',
                TotalWorkExperience: '',
                DisplayFromDate: '',
                DisplayToDate: '',
                Designation: 0,
                DesignationName: '',
                Description: '',
                Status: 0,//1 : Insert , 2:Update ,3 :Delete
                EduCountryData: { Display: '', Value: '' },
                EduStateData: { Display: '', Value: '' },
                EduCityData: { Display: '', Value: '' },
                EduDesignationData: { Display: '', Value: '' }
            }
            $scope.objUserExpEducation = {
                EducationId: 0,
                UserId: 0,
                InstituteName: '',
                QualificationId: 0,
                QualificationName: '',
                CityId: 0,
                CityName: '',
                StateId: 0,
                StateName: '',
                CountryId: 0,
                CountryName: '',
                Address: '',
                FromDate: '',
                ToDate: '',
                PinCode: '',
                DisplayFromDate: '',
                DisplayToDate: '',
                EduDescription: '',
                Status: 0,//1 : Insert , 2:Update ,3 :Delete
                EduCountryData: { Display: '', Value: '' },
                EduStateData: { Display: '', Value: '' },
                EduCityData: { Display: '', Value: '' },
                EducationData: { Display: '', Value: '' }
            }
            if ($scope.FormUserInfo)
                $scope.FormUserInfo.$setPristine();
            $scope.addMode = true;
            $scope.isFirstFocus = false;
            $scope.storage = {};
            $timeout(function () {
                $scope.isFirstFocus = true;
            });
            $scope.EditUserContactIndex = -1;
            $scope.EditUserRefIndex = -1;
            $scope.EditUserSalaryIndex = -1;
        };

        $scope.newValue = function (value) {
            //alert(value)
            $scope.ReferanceMode = value;
            $scope.objUser.ReferanceData = {
                Display: "",
                Value: ""
            };

        }
        ////New Function
        $scope.showHide = function (value) {
            if (value == "BuyerMaster") {
                $scope.showHideReferance = true;
                $scope.showHideAgency = false;
                $scope.objUser.ReferenceMannualEntry = " ";
                $scope.objUser.ReferanceData.Display = "";
            }
            else if (value == "UserMaster") {
                $scope.showHideReferance = true;
                $scope.showHideAgency = false;
                $scope.objUser.ReferenceMannualEntry = " ";
                $scope.objUser.ReferanceData.Display = "";
            }
            else if (value == "Other") {
                $scope.showHideReferance = false;
                $scope.showHideAgency = true;
                $scope.objUser.ReferanceData.Display = " ";
                $scope.objUser.ReferenceMannualEntry = "";
            }
            $scope.ReferanceMode = value;
        }

        $scope.Range = function (start, end) {
            var result = [];
            for (var i = start; i <= end; i++) {
                result.push(i);
            }
            return result;
        };
        ////Old Function
        //$scope.showHide = function (value) {
        //    //alert(value)
        //    if (value == "1") {
        //        $scope.showHideReferance = true;
        //        $scope.showHideAgency = false;
        //        $scope.objUser.AgencyTypeReferanceData = {
        //            Display: "",
        //            Value: ""
        //        };
        //    }
        //    else {
        //        $scope.showHideReferance = false;
        //        $scope.showHideAgency = true;
        //        $scope.objUser.ReferanceData = {
        //            Display: "",
        //            Value: ""
        //        };

        //    }
        //}

        $scope.openTab = function (evt, tabName, data, id) {
            // Declare all variables
            var bln = true;
            var dataerror = true;
            //if (id > 0) {
            $scope.tabusershow = $scope.usertype > 1 ? true : false;
            $scope.tabadminshow = $scope.usertype == 1 ? true : false;
            //}

            if ($scope.isClicked == false) {
                if (data != undefined) {
                    //EmployeeReference, BasicDetails, ContactDetails, WorkEducationDetails, BirthDetails, HomeTownDetails, AdressDetails, FamilyMemberDetails,
                    //ReferenceDetails, DocumentsDetails, BankDetails, JoiningDetails, AllotmentDetails, SalaryDetails
                    if (tabName == 'BasicDetails') {
                        dataerror = false;
                    }
                    if (tabName == 'AllotmentDetails' || tabName == 'SalaryDetails' || tabName == 'BasicDetails' || tabName == 'ContactDetails' || tabName == 'AdressDetails' || tabName == 'BirthDetails' || tabName == 'HomeTownDetails' || tabName == 'WorkEducationDetails' || tabName == 'WorkExperienceDetails' || tabName == 'FamilyMemberDetails' || tabName == 'ReferenceDetails' || tabName == 'DocumentsDetails' || tabName == 'BankDetails') {
                        if (data.CommunicationDate == '' || data.CommunicationDate == null) {
                            toastr.error("Please Select Communication Date.");
                            dataerror = false;
                            return false;
                        }
                        if (data.JoiningDate == '' || data.JoiningDate == null) {
                            toastr.error("Please Select Joining Date.");
                            dataerror = false;
                            return false;
                        }
                        if (data.DesignationData.Display == '' || data.DesignationData.Display == null) {
                            toastr.error("Please Select Designation.");
                            dataerror = false;
                            return false;
                        }
                        if (data.DepartmentData.Display == '' || data.DepartmentData.Display == null) {
                            toastr.error("Please Select Department.");
                            dataerror = false;
                            return false;
                        }
                        if (data.RoleData.Display == '' || data.RoleData.Display == null) {
                            toastr.error("Please Select Role.");
                            dataerror = false;
                            return false;
                        }
                        if (data.ShiftStartTime == '' || data.ShiftStartTime == null) {
                            toastr.error("Please Select Shift Start Time.");
                            dataerror = false;
                            return false;
                        }
                        if (data.ShiftEndTime == '' || data.ShiftEndTime == null) {
                            toastr.error("Please Select Shift End Time.");
                            dataerror = false;
                            return false;
                        }
                        if (data.LunchStartTime == '' || data.LunchStartTime == null) {
                            toastr.error("Please Select Lunch Start Time.");
                            dataerror = false;
                            return false;
                        }
                        if (data.LunchEndTime == '' || data.LunchEndTime == null) {
                            toastr.error("Please Select Lunch End Time.");
                            dataerror = false;
                            return false;
                        }
                    }
                    if (tabName == 'ContactDetails' || tabName == 'AdressDetails' || tabName == 'BirthDetails' || tabName == 'HomeTownDetails' || tabName == 'WorkEducationDetails' || tabName == 'WorkExperienceDetails' || tabName == 'FamilyMemberDetails' || tabName == 'ReferenceDetails' || tabName == 'DocumentsDetails' || tabName == 'BankDetails') {
                        if (data.Name == '' || data.Name == null) {
                            toastr.error("Please Enter First Name.");
                            dataerror = false;
                            return false;
                        }
                        if (data.FatherName == '' || data.FatherName == null) {
                            toastr.error("Please Enter Middle Name.");
                            dataerror = false;
                            return false;
                        }
                        if (data.Surname == '' || data.Surname == null) {
                            toastr.error("Please Enter Last Name.");
                            dataerror = false;
                            return false;
                        }
                        if (data.BloodGroupData.Display == '' || data.BloodGroupData.Display == null) {
                            toastr.error("Please Select Blood Group.");
                            dataerror = false;
                            return false;
                        }
                        if (data.Photo == 'user.png') {
                            toastr.error("Please Select Photo.");
                            dataerror = false;
                            return false;
                        }
                        //if (data.Gender == '') {
                        //    toastr.error("Please Select Gender.");
                        //    dataerror = false;
                        //    return false;
                        //}
                        //if (data.MaritalStatus == '') {
                        //    toastr.error("Please Select MaritalStatus.");
                        //    dataerror = false;
                        //    return false;
                        //}
                    }
                    if (tabName == 'AdressDetails' || tabName == 'BirthDetails' || tabName == 'HomeTownDetails' || tabName == 'WorkEducationDetails' || tabName == 'WorkExperienceDetails' || tabName == 'FamilyMemberDetails' || tabName == 'ReferenceDetails' || tabName == 'DocumentsDetails' || tabName == 'BankDetails') {
                        if ($scope.EmpContactMobCode == '' || $scope.EmpContactMobCode == null) {
                            toastr.error("Please Enter Mobile No.");
                            dataerror = false;
                            return false;
                        }
                        if ($scope.EmpContactEmail == '' || $scope.EmpContactEmail == null) {
                            toastr.error("Please Enter Email.");
                            dataerror = false;
                            return false;
                        }
                    }
                    if (tabName == 'BirthDetails' || tabName == 'HomeTownDetails' || tabName == 'WorkEducationDetails' || tabName == 'WorkExperienceDetails' || tabName == 'FamilyMemberDetails' || tabName == 'ReferenceDetails' || tabName == 'DocumentsDetails' || tabName == 'BankDetails') {
                        if (data.PresentPropType == '' || data.PresentPropType == null) {
                            toastr.error("Please Select Present Property Type.");
                            dataerror = false;
                            return false;
                        }
                        if (data.PresentAddrCountryData.Display == '' || data.PresentAddrCountryData.Display == null) {
                            toastr.error("Please Select Present Country.");
                            dataerror = false;
                            return false;
                        }
                        if (data.PresentAddrStateData.Display == '' || data.PresentAddrStateData.Display == null) {
                            toastr.error("Please Select Present State.");
                            dataerror = false;
                            return false;
                        }
                        if (data.PresentAddrCityData.Display == '' || data.PresentAddrCityData.Display == null) {
                            toastr.error("Please Select Present City.");
                            dataerror = false;
                            return false;
                        }
                        if (data.PermanentPropType == '' || data.PermanentPropType == null) {
                            toastr.error("Please Select Permanent Property Type.");
                            dataerror = false;
                            return false;
                        }
                        if (data.PermanentAddrCountryData.Display == '' || data.PermanentAddrCountryData.Display == null) {
                            toastr.error("Please Select Permanent Country.");
                            dataerror = false;
                            return false;
                        }
                        if (data.PermanentAddrStateData.Display == '' || data.PermanentAddrStateData.Display == null) {
                            toastr.error("Please Select Permanent State.");
                            dataerror = false;
                            return false;
                        }
                        if (data.PermanentAddrCityData.Display == '' || data.PermanentAddrCityData.Display == null) {
                            toastr.error("Please Select Permanent City.");
                            dataerror = false;
                            return false;
                        }
                    }
                    if (tabName == 'HomeTownDetails' || tabName == 'WorkEducationDetails' || tabName == 'WorkExperienceDetails' || tabName == 'FamilyMemberDetails' || tabName == 'ReferenceDetails' || tabName == 'DocumentsDetails' || tabName == 'BankDetails') {
                        if (data.BirthDate == '' || data.BirthDate == null) {
                            toastr.error("Please Select Birth Date.");
                            dataerror = false;
                            return false;
                        }
                        if (data.BirthCountryData.Display == '' || data.BirthCountryData.Display == null) {
                            toastr.error("Please Select Country.");
                            dataerror = false;
                            return false;
                        }
                        if (data.BirthStateData.Display == '' || data.BirthStateData.Display == null) {
                            toastr.error("Please Select State.");
                            dataerror = false;
                            return false;
                        }
                        if (data.BirthCityData.Display == '' || data.BirthCityData.Display == null) {
                            toastr.error("Please Select City.");
                            dataerror = false;
                            return false;
                        }
                        if (data.BirthPlacePinCode == '' || data.BirthPlacePinCode == null) {
                            toastr.error("Please Enter Pincode.");
                            dataerror = false;
                            return false;
                        }
                        if (data.BirthPlaceArea == '' || data.BirthPlaceArea == null) {
                            toastr.error("Please Enter Address.");
                            dataerror = false;
                            return false;
                        }
                    }
                    if (tabName == 'WorkEducationDetails' || tabName == 'WorkExperienceDetails' || tabName == 'FamilyMemberDetails' || tabName == 'ReferenceDetails' || tabName == 'DocumentsDetails' || tabName == 'BankDetails') {
                        if (data.HomeCountryData.Display == '' || data.HomeCountryData.Display == null) {
                            toastr.error("Please Select Country.");
                            dataerror = false;
                            return false;
                        }
                        if (data.HomeStateData.Display == '' || data.HomeStateData.Display == null) {
                            toastr.error("Please Select State.");
                            dataerror = false;
                            return false;
                        }
                        if (data.HomeCityData.Display == '' || data.HomeCityData.Display == null) {
                            toastr.error("Please Select City.");
                            dataerror = false;
                            return false;
                        }
                        if (data.HomeTownPinCode == '' || data.HomeTownPinCode == null) {
                            toastr.error("Please Enter Pincode.");
                            dataerror = false;
                            return false;
                        }
                        if (data.HomeTownArea == '' || data.HomeTownArea == null) {
                            toastr.error("Please Enter Address.");
                            dataerror = false;
                            return false;
                        }
                    }
                   
                    if (tabName == 'ReferenceDetails' || tabName == 'DocumentsDetails' || tabName == 'BankDetails') {
                        if ($scope.objUser.UserContactDetails.length < 4) {
                            toastr.error("Enter Minmum 4 Family Contact Details.");
                            bln = false;
                        };
                    }
                    if (tabName == 'DocumentsDetails' || tabName == 'BankDetails') {
                        if ($scope.objUser.UserReferanceDetails.length < 4) {
                            toastr.error("Enter Minmum 4 Reference Details.");
                            dataerror = false;
                            return false;
                        }
                    }
                    if (tabName == 'BankDetails') {
                        if ($scope.objUser.UserDocumentDetails.length < 2) {
                            toastr.error("Enter Minmum 2 Document Details.");
                            dataerror = false;
                            return false;
                        };
                    }
                   
                    
                    if (dataerror == true) {
                        $scope.CreateUpdate(data);
                        $scope.editUser($scope.objUser.UserId);
                    }
                }
                else if (tabName == 'ReferenceDetails') {
                    if ($scope.objUser.UserContactDetails.length < 4) {
                        toastr.error("Enter Minmum 4 Family Contact Details.");
                        bln = false;
                    };
                }
                else if (tabName == 'DocumentsDetails') {
                    if ($scope.objUser.UserContactDetails.length < 4) {
                        toastr.error("Enter Minmum 4 Family Contact Details.");
                        bln = false;
                    } else if ($scope.objUser.UserReferanceDetails.length < 4) {
                        toastr.error("Enter Minmum 4 Reference Details.");
                        bln = false;
                    };
                }
                else if (tabName == 'BankDetails') {
                    if ($scope.objUser.UserContactDetails.length < 4) {
                        toastr.error("Enter Minmum 4 Family Contact Details.");
                        bln = false;
                    } else if ($scope.objUser.UserReferanceDetails.length < 4) {
                        toastr.error("Enter Minmum 4 Reference Details.");
                        bln = false;
                    } else if ($scope.objUser.UserDocumentDetails.length < 2) {
                        toastr.error("Enter Minmum 2 Document Details.");
                        bln = false;
                    };
                }
            }
            if (bln == true && $scope.issuccess == true) {
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
                //$scope.objProduct.EditProImgId = 0;
                //$scope.objProduct.EditProCatalogueId = 0;
            }
            if ($scope.issuccess == false && $scope.msg != '') {
                toastr.error($scope.msg);
            }
            //$scope.tabname = "active";
            //evt.currentTarget.className += "active";
        }

        function getAge(fromdate, todate) {
            if (todate) todate = new Date(todate);
            else todate = new Date();

            var age = [], fromdate = new Date(fromdate),
            y = [todate.getFullYear(), fromdate.getFullYear()],
            ydiff = y[0] - y[1],
            m = [todate.getMonth(), fromdate.getMonth()],
            mdiff = m[0] - m[1],
            d = [todate.getDate(), fromdate.getDate()],
            ddiff = d[0] - d[1];

            if (mdiff < 0 || (mdiff === 0 && ddiff < 0))--ydiff;
            if (mdiff < 0) mdiff += 12;
            if (ddiff < 0) {
                fromdate.setMonth(m[1] + 1, 0);
                ddiff = fromdate.getDate() - d[1] + d[0];
                --mdiff;
            }
            if (ydiff > 0) age.push(ydiff + ' year' + (ydiff > 1 ? 's ' : ' '));
            if (mdiff > 0) age.push(mdiff + ' month' + (mdiff > 1 ? 's' : ''));
            //if (ddiff > 0) age.push(ddiff + ' day' + (ddiff > 1 ? 's' : ''));
            if (age.length > 1) age.splice(age.length - 1, 0, ' and ');
            return age.join('');
        }

        $scope.$watch('objUserEducation.FromDate', function (val) {
            var fromdate = $scope.objUserEducation.FromDate;
            var todate = $scope.objUserEducation.ToDate;
            if (fromdate != undefined && fromdate != null && todate != undefined && todate != null) {
                var data = getAge(fromdate, todate);
                $scope.Duration = data;
            }
        });
        $scope.$watch('objUserEducation.ToDate', function (val) {
            var fromdate = $scope.objUserEducation.FromDate;
            var todate = $scope.objUserEducation.ToDate;
            if (fromdate != undefined && fromdate != null && todate != undefined && todate != null) {
                var data = getAge(fromdate, todate);
                $scope.Duration = data;
            }
        });

        $scope.$watch('objUserExpEducation.FromDate', function (val) {
            var fromdate = $scope.objUserExpEducation.FromDate;
            var todate = $scope.objUserExpEducation.ToDate;
            if (fromdate != undefined && fromdate != null && todate != undefined && todate != null) {
                var data = getAge(fromdate, todate);
                $scope.ExpDuration = data;
            }
        });
        $scope.$watch('objUserExpEducation.ToDate', function (val) {
            var fromdate = $scope.objUserExpEducation.FromDate;
            var todate = $scope.objUserExpEducation.ToDate;
            if (fromdate != undefined && fromdate != null && todate != undefined && todate != null) {
                var data = getAge(fromdate, todate);
                $scope.ExpDuration = data;
            }
        });

        $scope.SetUserId = function (id, usertype, isdisable) {
            $scope.usertype = usertype;
            $scope.issuccess = true;
            $scope.openTab("click", "EmployeeReference", undefined, id);
            CountryService.GetCountryFlag().then(function (result) {
                $scope.MobCodeData = angular.copy(result);
                $scope.ContactMobCodeData = angular.copy(result);
                $scope.ReferenceMobCodeData = angular.copy(result);

                $scope.EmpContactMobCodeData = angular.copy(result);

                SourceService.GetSourceData().then(function (result1) {
                    $scope.chatCodeData = angular.copy(result1);
                    $scope.EmpContactchatCodeData = angular.copy(result1);
                    if (id > 0) {
                        //edit
                        $scope.SrNo = id;
                        $scope.addMode = false;
                        $scope.saveText = "Update";
                        $scope.editUser(id);
                        if ($scope.objUser.Photo == null || $scope.objUser.Photo == '') {
                            $scope.objUser.Photo = "user.png";
                        }
                        if (isdisable == 1) {
                            $scope.isClicked = true;
                        }
                    } else {
                        //add
                        //MobileCodeBind();
                        $scope.SrNo = 0;
                        $scope.addMode = true;
                        $scope.saveText = "Save";
                        $scope.objUser.Photo = "user.png";
                        $scope.GetInvoice();
                        $scope.isClicked = false;
                    }
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }
        $scope.GetMasterInformation = function () {
            //$scope.DepartmentBind = function () {
            UserService.DepartmentBind().then(function (result) {
                $scope.DepartmentList = result.data;
                //MobCodesBind();
                RoleBind();
                ReportingBind();
            })

        }

        $scope.GetInvoice = function () {
            UserService.UserInfo().then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objUser.UserCode = result.data.DataList.UserCode;
                } else if (result.ResponseType == 3) {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (error) {
                $rootScope.errorHandler(error)
            })
        }

        function RoleBind() {
            UserService.RoleBind().then(function (result) {
                $scope.RoleList = result.data;
            })
        }
        function ReportingBind() {
            UserService.ReportingBind().then(function (result) {
                $scope.ReportingList = result.data;
            })
        }
        $scope.Add = function () {

            window.location.href = "/master/User/UserPopup";

        }
        $scope.dateOptions = {
            formatYear: 'yy',
            minDate: new Date(1950, 1, 1),
            startingDay: 1
        }
        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };
        $scope.Setbtnadd = function (id) {
            if (id == 1) {
                $scope.isadd = true;
                $scope.isview = true;
            }
            else {
                $scope.isadd = false;
                $scope.isview = true;
            }
            if (id == 13) {
                $scope.isview = false;
            }
        };
        $scope.gridObj = {
            columnsInfo: [
               //{ "title": "User Id", "data": "UserId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true },
               { "title": "Employee Name", "field": "Employee_Name", sortable: "Employee_Name", filter: { Employee_Name: "text" }, show: true },
               { "title": "Father Name", "field": "FatherName", sortable: "FatherName", filter: { FatherName: "text" }, show: false },
               { "title": "Department", "field": "DepartmentName", sortable: "DepartmentName", filter: { DepartmentName: "text" }, show: true },
               { "title": "Mobile", "field": "MobNo", sortable: "MobNo", filter: { MobNo: "text" }, show: true },
               { "title": "Email", "field": "Email", sortable: "Email", filter: { Email: "text" }, show: true },
               //{ "title": "QQ", "data": "QQCode", sort: true, filter: true },
               //{ "title": "Skype", "data": "Skype", sort: true, filter: true },
               { "title": "Status", "field": "Status", sortable: "Status", filter: { Status: "dropdown" }, show: true },

               { "title": "UserName", "field": "UserName", sortable: "UserName", filter: { UserName: "text" }, show: false },
               { "title": "ChatDetail", "field": "ChatDetail", sortable: "ChatDetail", filter: { ChatDetail: "text" }, show: false },
               {
                   "title": "BirthDate", "field": "BirthDate", sortable: "BirthDate", filter: { BirthDate: "date" }, show: false,
                   'cellTemplte': function ($scope, row) {
                       var element = '<span >{{ConvertDate(row.BirthDate,\'dd/mm/yyyy\')}}</span>'
                       return $scope.getHtml(element);
                   }
               },
               { "title": "BirthPlaceArea", "field": "BirthPlaceArea", sortable: "BirthPlaceArea", filter: { BirthPlaceArea: "text" }, show: false },
               { "title": "BirthPlacePincode", "field": "BirthPlacePincode", sortable: "BirthPlacePincode", filter: { BirthPlacePincode: "text" }, show: false },
               { "title": "HomeTownArea", "field": "HomeTownArea", sortable: "HomeTownArea", filter: { HomeTownArea: "text" }, show: false },
               { "title": "HomeTownPincode", "field": "HomeTownPincode", sortable: "HomeTownPincode", filter: { HomeTownPincode: "text" }, show: false },
               { "title": "BloodGroup", "field": "BloodGroup", sortable: "BloodGroup", filter: { BloodGroup: "text" }, show: false },
               { "title": "ResidentNo", "field": "ResidentNo", sortable: "ResidentNo", filter: { ResidentNo: "text" }, show: false },
               { "title": "PresentArea", "field": "PresentArea", sortable: "PresentArea", filter: { PresentArea: "text" }, show: false },
               { "title": "PresentPinCode", "field": "PresentPinCode", sortable: "PresentPinCode", filter: { PresentPinCode: "text" }, show: false },
               { "title": "PresentAddress", "field": "PresentAddress", sortable: "PresentAddress", filter: { PresentAddress: "text" }, show: false },
               { "title": "PermanentArea", "field": "PermanentArea", sortable: "PermanentArea", filter: { PermanentArea: "text" }, show: false },
               { "title": "PermanentPinCode", "field": "PermanentPinCode", sortable: "PermanentPinCode", filter: { PermanentPinCode: "text" }, show: false },
               { "title": "PermanentAddress", "field": "PermanentAddress", sortable: "PermanentAddress", filter: { PermanentAddress: "text" }, show: false },
               { "title": "DrivingLicNo", "field": "DrivingLicNo", sortable: "DrivingLicNo", filter: { DrivingLicNo: "text" }, show: false },
               { "title": "VoterIdNumber", "field": "VoterIdNumber", sortable: "VoterIdNumber", filter: { VoterIdNumber: "text" }, show: false },
               { "title": "PassportNo", "field": "PassportNo", sortable: "PassportNo", filter: { PassportNo: "text" }, show: false },
               { "title": "PANNo", "field": "PANNo", sortable: "PANNo", filter: { PANNo: "text" }, show: false },
               { "title": "AadharNo", "field": "AadharNo", sortable: "AadharNo", filter: { AadharNo: "text" }, show: false },
               { "title": "FullNameAsPerBank", "field": "FullNameAsPerBank", sortable: "FullNameAsPerBank", filter: { FullNameAsPerBank: "text" }, show: false },
               { "title": "BranchName", "field": "BranchName", sortable: "BranchName", filter: { BranchName: "text" }, show: false },
               { "title": "BankName", "field": "BankName", sortable: "BankName", filter: { BankName: "text" }, show: false },
               { "title": "AccountNo", "field": "AccountNo", sortable: "AccountNo", filter: { AccountNo: "text" }, show: false },
               { "title": "AccountType", "field": "AccountType", sortable: "AccountType", filter: { AccountType: "text" }, show: false },
               { "title": "IFSC", "field": "IFSC", sortable: "IFSC", filter: { IFSC: "text" }, show: false },
               { "title": "ReportingName", "field": "ReportingName", sortable: "ReportingName", filter: { ReportingName: "text" }, show: false },
               { "title": "ReferanceName", "field": "ReferanceName", sortable: "ReferanceName", filter: { ReferanceName: "text" }, show: false },
               { "title": "ReferenceMannualEntry", "field": "ReferenceMannualEntry", sortable: "ReferenceMannualEntry", filter: { ReferenceMannualEntry: "text" }, show: false },
               {

                   "title": "JoiningDate", "field": "JoiningDate", sortable: "JoiningDate", filter: { JoiningDate: "date" }, show: false,
                   'cellTemplte': function ($scope, row) {
                       var element = '<span >{{ConvertDate(row.JoiningDate,\'dd/mm/yyyy\')}}</span>'
                       return $scope.getHtml(element);
                   }
               },
               { "title": "DesignationName", "field": "DesignationName", sortable: "DesignationName", filter: { DesignationName: "text" }, show: false },
               { "title": "RoleName", "field": "RoleName", sortable: "RoleName", filter: { RoleName: "text" }, show: false },
               { "title": "MICRCode", "field": "MICRCode", sortable: "MICRCode", filter: { MICRCode: "text" }, show: false },
               { "title": "ShiftStartTime", "field": "ShiftStartTime", sortable: "ShiftStartTime", filter: { ShiftStartTime: "text" }, show: false },
               { "title": "ShiftEndTime", "field": "ShiftEndTime", sortable: "ShiftEndTime", filter: { ShiftEndTime: "text" }, show: false },
               { "title": "LunchStartTime", "field": "LunchStartTime", sortable: "LunchStartTime", filter: { LunchStartTime: "text" }, show: false },
               { "title": "LunchEndTime", "field": "LunchEndTime", sortable: "LunchEndTime", filter: { LunchEndTime: "text" }, show: false },
               {
                   "title": "CommunicationDate", "field": "CommunicationDate", sortable: "CommunicationDate", filter: { CommunicationDate: "date" }, show: false,
                   'cellTemplte': function ($scope, row) {
                       var element = '<span >{{ConvertDate(row.CommunicationDate,\'dd/mm/yyyy\')}}</span>'
                       return $scope.getHtml(element);
                   }
               },
               //{ "title": "QualificationName", "field": "QualificationName", sortable: "QualificationName", filter: { QualificationName: "text" }, show: false },
               //{ "title": "TotalWorkExperience", "field": "TotalWorkExperience", sortable: "TotalWorkExperience", filter: { TotalWorkExperience: "text" }, show: false },
               { "title": "ContactMobile", "field": "ContactMobile", sortable: "ContactMobile", filter: { ContactMobile: "text" }, show: false },
               { "title": "ContactEmail", "field": "ContactEmail", sortable: "ContactEmail", filter: { ContactEmail: "text" }, show: false },
               { "title": "ContactChat", "field": "ContactChat", sortable: "ContactChat", filter: { ContactChat: "text" }, show: false },
               { "title": "Sourcename", "field": "Sourcename", sortable: "Sourcename", filter: { Sourcename: "text" }, show: false },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a ng-show="row.Status == \' Active\' && $parent.$parent.$parent.$parent.$parent.$parent.isview" class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.UserId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                       '<a ng-show="row.Status == \' Active\' && $parent.$parent.$parent.$parent.$parent.$parent.isview" class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.ActiveInActive(row.UserId)" data-uib-tooltip="InActive"><i class="fa fa-user-times" ></i></a>' +
                   //'<a data-ng-if="row.Status == \'Active\'" class="btn btn-info btn-xs" data-ng-click="$parent.$parent.$parent.ActiveInActive(row.UserId)" data-uib-tooltip="Active"><i class="fa fa-check"></i></a> ' +
                   '<a ng-show="row.Status == \'InActive\' && $parent.$parent.$parent.$parent.$parent.$parent.isview" class="btn btn-info btn-xs" data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.ActiveInActive(row.UserId)" data-uib-tooltip="Active"><i class="fa fa-user"></i></a>' +
                   '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.UserId)" data-uib-tooltip="View"><i class="fa fa-eye"></i></a>'
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'UserId': 'asc' }
            //modeType: "StateMaster",
            //Title: "State List"
            //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.UserId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
        }


        //EDIT 
        $scope.Edit = function (id) {
            window.location.href = "/master/User/UserPopup/" + id + "/" + 0;

        }
        $scope.View = function (id) {
            window.location.href = "/master/User/UserPopup/" + id + "/" + 1;

        }
        $scope.ActiveInActive = function (id) {
            UserService.ActiveInActiveStatus(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    $scope.refreshGrid()
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
                $scope.refreshGrid()

            })
        }

        $scope.Delete = function (data) {
            UserService.DeleteUser(data).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    $scope.refreshGrid()

                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
                $scope.refreshGrid()

            })
        }
        $scope.IsActive = function (data) {
        }

        $scope.settime = function () {
            //var cid = $scope.objcity.ShiftEndTime;
            var duration = moment.duration(moment($scope.objUser.ShiftEndTime).diff(moment($scope.objUser.ShiftStartTime)));
            $scope.objUser.hours = moment.utc(duration._milliseconds).format('HH:mm:ss');
            var Lunchduration = moment.duration(moment($scope.objUser.LunchEndTime).diff(moment($scope.objUser.LunchStartTime)));
            $scope.objUser.Lunchhours = moment.utc(Lunchduration._milliseconds).format('HH:mm:ss');
        }
        //editUser();
        $scope.editUser = function (id) {
            if (id && id > 0) {
                UserService.GetUserById(id).then(function (result) {

                    if (result.data.ResponseType == 1) {
                        //alert(result.data.DataList[0].CountryId);
                        var objUserMaster = result.data.DataList.UserDetail;
                        var d = new Date();
                        var de = new Date();
                        var gettime = '';
                        var getendtime = '';
                        var mytime = d;
                        var myendtime = de;
                        var dat = new Date(), date = new Date(), getlunchtime = '', getlunchend = '', mylunch = dat, mylunchend = date, BirthDate = '', JoiningDate = '', CommunicationDate = '';
                        if (objUserMaster[0].ShiftStartTime != null) {
                            d.setHours($filter('date')(objUserMaster[0].ShiftStartTime, "HH:mm").Hours);
                            d.setMinutes($filter('date')(objUserMaster[0].ShiftStartTime, "HH:mm").Minutes);
                            gettime = objUserMaster[0].ShiftStartTime.Hours == 0 && objUserMaster[0].ShiftStartTime.Minutes == 0 ? new Date() : mytime;

                        }
                        if (objUserMaster[0].ShiftEndTime != null) {
                            de.setHours($filter('date')(objUserMaster[0].ShiftEndTime, "HH:mm").Hours);
                            de.setMinutes($filter('date')(objUserMaster[0].ShiftEndTime, "HH:mm").Minutes);
                            getendtime = objUserMaster[0].ShiftEndTime.Hours == 0 && objUserMaster[0].ShiftEndTime.Minutes == 0 ? new Date() : myendtime;

                        }
                        if (objUserMaster[0].LunchStartTime != null) {
                            dat.setHours($filter('date')(objUserMaster[0].LunchStartTime, "HH:mm").Hours);
                            dat.setMinutes($filter('date')(objUserMaster[0].LunchStartTime, "HH:mm").Minutes);
                            getlunchtime = objUserMaster[0].LunchStartTime.Hours == 0 && objUserMaster[0].LunchStartTime.Minutes == 0 ? new Date() : mylunch;

                        }
                        if (objUserMaster[0].LunchEndTime != null) {
                            date.setHours($filter('date')(objUserMaster[0].LunchEndTime, "HH:mm").Hours);
                            date.setMinutes($filter('date')(objUserMaster[0].LunchEndTime, "HH:mm").Minutes);
                            getlunchend = objUserMaster[0].LunchEndTime.Hours == 0 && objUserMaster[0].LunchEndTime.Minutes == 0 ? new Date() : mylunchend;

                        }
                        if (objUserMaster[0].BirthDate != null) {
                            BirthDate = $filter('mydate')(objUserMaster[0].BirthDate);
                        }
                        if (objUserMaster[0].JoiningDate != null) {
                            JoiningDate = $filter('mydate')(objUserMaster[0].JoiningDate);
                        }
                        if (objUserMaster[0].CommunicationDate != null) {
                            CommunicationDate = $filter('mydate')(objUserMaster[0].CommunicationDate);
                        }
                        $scope.EmpContactMobCode = (objUserMaster[0].ContactMobile != '' && objUserMaster[0].ContactMobile != null) ? objUserMaster[0].ContactMobile.split(",") : [];
                        $scope.EmpContactEmail = (objUserMaster[0].ContactEmail != '' && objUserMaster[0].ContactEmail != null) ? objUserMaster[0].ContactEmail.split(",") : [];
                        $scope.EmpContactchat = (objUserMaster[0].ContactChat != '' && objUserMaster[0].ContactChat != null) ? objUserMaster[0].ContactChat.split(",") : [];

                        $scope.MobCode = (objUserMaster[0].MobNo != '' && objUserMaster[0].MobNo != null) ? objUserMaster[0].MobNo.split(",") : [];
                        $scope.Email = (objUserMaster[0].Email != '' && objUserMaster[0].Email != null) ? objUserMaster[0].Email.split(",") : [];
                        $scope.chat = (objUserMaster[0].ChatDetail != '' && objUserMaster[0].ChatDetail != null) ? objUserMaster[0].ChatDetail.split(",") : [];

                        $scope.objUser = {
                            Photo: objUserMaster[0].Photo == null ? "user.png" : objUserMaster[0].Photo,
                            UserId: objUserMaster[0].UserId,
                            DepartmentId: objUserMaster[0].DepartmentId,
                            DepartmentName: objUserMaster[0].DepartmentName,
                            RoleId: objUserMaster[0].RoleId,
                            RoleName: objUserMaster[0].RoleName,
                            UserName: objUserMaster[0].UserName,
                            UserCode: objUserMaster[0].UserCode,
                            Name: objUserMaster[0].Name == null ? "" : objUserMaster[0].Name,
                            FatherName: objUserMaster[0].FatherName == null ? "" : objUserMaster[0].FatherName,
                            Surname: objUserMaster[0].Surname == null ? "" : objUserMaster[0].Surname,
                            BirthDate: BirthDate,
                            Gender: objUserMaster[0].Gender == null ? "" : objUserMaster[0].Gender,
                            MaritalStatus: objUserMaster[0].MaritalStatus = null ? "" : objUserMaster[0].MaritalStatus,
                            BirthPlaceArea: objUserMaster[0].BirthPlaceArea,
                            BirthPlacePinCode: objUserMaster[0].BirthPlacePincode,
                            BirthPlaceStateId: objUserMaster[0].BirthPlaceStateId,
                            BirthPlaceCountryId: objUserMaster[0].BirthPlaceCountryId,
                            HomeTownStateId: objUserMaster[0].HomeTownStateId,
                            HomeTownCountryId: objUserMaster[0].HomeTownCountryId,
                            HomeTownArea: objUserMaster[0].HomeTownArea,
                            HomeTownPinCode: objUserMaster[0].HomeTownPincode,
                            BloodGroupId: objUserMaster[0].BloodGroupId,
                            Email: objUserMaster[0].Email,
                            //MobCode: result.data.DataList.MobCode,
                            //MobCodeData: { Display: objUserMaster[0].MobCode1, Value: objUserMaster[0].MobCode },
                            //MobCode: objUserMaster[0].MobCode,
                            MobNo: objUserMaster[0].MobNo == null ? "" : objUserMaster[0].MobNo,
                            ResidentNo: objUserMaster[0].ResidentNo,
                            PresentArea: objUserMaster[0].PresentArea,
                            PresentPinCode: objUserMaster[0].PresentPinCode,
                            PresentPropType: objUserMaster[0].PresentPropType,
                            PresentAddress: objUserMaster[0].PresentAddress,
                            PresentResiCountryId: objUserMaster[0].PresentResiCountryId,
                            PresentResiStateId: objUserMaster[0].PresentResiStateId,
                            PermenantResiCountryId: objUserMaster[0].PermenantResiCountryId,
                            PermenantResiStateId: objUserMaster[0].PermenantResiStateId,
                            PermanentArea: objUserMaster[0].PermanentArea,
                            PermanentPinCode: objUserMaster[0].PermanentPinCode,
                            PermanentPropType: objUserMaster[0].PermanentPropType,
                            PermanentAddress: objUserMaster[0].PermanentAddress,

                            FamilyRefName: objUserMaster[0].FamilyRefName,
                            //FamilyRefName: objUserMaster[0].FamilyRefName,
                            FamilyResidentAddress: objUserMaster[0].FamilyResidentAddress,
                            FamilyRefContactNo: objUserMaster[0].FamilyRefContactNo,
                            FamilyRefEmail: objUserMaster[0].FamilyRefEmail,
                            RelativeRefName: objUserMaster[0].RelativeRefName,
                            RelativeRefSurname: objUserMaster[0].RelativeRefSurname,
                            RelativeRefArea: objUserMaster[0].RelativeRefArea,
                            RelativeResidentAddress: objUserMaster[0].RelativeResidentAddress,
                            RelativeRefContactNo: objUserMaster[0].RelativeRefContactNo,
                            RelativeRefEmail: objUserMaster[0].RelativeRefEmail,
                            FriendRefName: objUserMaster[0].FriendRefName,
                            FriendRefSurname: objUserMaster[0].FriendRefSurname,
                            FriendRefArea: objUserMaster[0].FriendRefArea,
                            FriendResidentAddress: objUserMaster[0].FriendResidentAddress,
                            FriendRefContactNo: objUserMaster[0].FriendRefContactNo,
                            FriendRefEmail: objUserMaster[0].FriendRefEmail,
                            WorkMaterRefName: objUserMaster[0].WorkMaterRefName,
                            WorkMaterRefSurname: objUserMaster[0].WorkMaterRefSurname,
                            WorkMaterRefArea: objUserMaster[0].WorkMaterRefArea,
                            WorkMaterResidentAddress: objUserMaster[0].WorkMaterResidentAddress,
                            WorkMaterRefContactNo: objUserMaster[0].WorkMaterRefContactNo,
                            WorkMaterRefEmail: objUserMaster[0].WorkMaterRefEmail,
                            DrivingLicNo: objUserMaster[0].DrivingLicNo,
                            VoterIdNumber: objUserMaster[0].VoterIdNumber,
                            PassportNo: objUserMaster[0].PassportNo,
                            PANNo: objUserMaster[0].PANNo,
                            AadharNo: objUserMaster[0].AadharNo,
                            FullNameAsPerBank: objUserMaster[0].FullNameAsPerBank,
                            BranchName: objUserMaster[0].BranchName,
                            BankNameId: objUserMaster[0].BankName,
                            BankName: objUserMaster[0].Bank,
                            AccountNo: objUserMaster[0].AccountNo,
                            //AccountType: objUserMaster[0].AccountType,
                            IFSC: objUserMaster[0].IFSC,
                            BranchAddress: objUserMaster[0].BranchAddress,
                            MICRCode: objUserMaster[0].MICRCode,
                            ReportingId: objUserMaster[0].ReportingId,
                            JoiningDate: JoiningDate,
                            CommunicationDate: CommunicationDate,
                            ReferenceMannualEntry: objUserMaster[0].ReferenceMannualEntry,
                            ReferenceSubType: objUserMaster[0].ReferenceSubType,
                            ReferenceTypeId: objUserMaster[0].ReferenceTypeId,
                            RelativeRefContactCode: objUserMaster[0].RelativeRefContactCode,
                            WorkMaterRefContactCode: objUserMaster[0].WorkMaterRefContactCode,
                            FriendRefContactCode: objUserMaster[0].FriendRefContactCode,
                            ChatDetail: objUserMaster[0].ChatDetail,
                            //Skype: objUserMaster[0].Skype,
                            ShiftStartTime: gettime,
                            ShiftEndTime: getendtime,
                            LunchStartTime: getlunchtime,
                            LunchEndTime: getlunchend,
                            BankNameData: {
                                Display: objUserMaster[0].Bank,
                                Value: objUserMaster[0].BankName
                            },
                            DesignationData: {
                                Display: objUserMaster[0].DesignationName,
                                Value: objUserMaster[0].DesignationId
                            },
                            DepartmentData: {
                                Display: objUserMaster[0].DepartmentName,
                                Value: objUserMaster[0].DepartmentId
                            },
                            RoleData: {
                                Display: objUserMaster[0].RoleName,
                                Value: objUserMaster[0].RoleId
                            },
                            AccountTypeData: {
                                Display: objUserMaster[0].AccountType,
                                Value: objUserMaster[0].AccountTypeId
                            },
                            AgencyTypeReferanceData: {
                                Display: objUserMaster[0].AgencyTypeReferanceName,
                                Value: objUserMaster[0].ReferenceId,
                            },
                            ReferanceData: {
                                Display: objUserMaster[0].ReferanceName,
                                Value: objUserMaster[0].ReferenceId,
                            },
                            ReportingData: {
                                Display: objUserMaster[0].ReportingName,
                                Value: objUserMaster[0].ReportingId
                            },
                            BloodGroupData: {
                                Display: objUserMaster[0].BloodGroup,
                                Value: objUserMaster[0].BloodGroupId
                            },
                            BirthCountryData: {
                                Display: objUserMaster[0].BirthPlaceCountryName,
                                Value: objUserMaster[0].BirthPlaceCountryId
                            },
                            BirthStateData: {
                                Display: objUserMaster[0].BirthPlaceStateName,
                                Value: objUserMaster[0].BirthPlaceStateId
                            },
                            BirthCityData: {
                                Display: objUserMaster[0].BirthPlaceCityName,
                                Value: objUserMaster[0].BirthPlaceCityId
                            },
                            HomeCountryData: {
                                Display: objUserMaster[0].HomeTownCountryName,
                                Value: objUserMaster[0].HomeTownCountryId
                            },
                            HomeStateData: {
                                Display: objUserMaster[0].HomeTownStateName,
                                Value: objUserMaster[0].HomeTownStateId
                            },
                            HomeCityData: {
                                Display: objUserMaster[0].HomeTownCityName,
                                Value: objUserMaster[0].HomeTownCityId
                            },
                            PresentAddrCountryData: {
                                Display: objUserMaster[0].PresentResiCountryName,
                                Value: objUserMaster[0].PresentResiCountryId
                            },
                            PresentAddrStateData: {
                                Display: objUserMaster[0].PresentResiStateName,
                                Value: objUserMaster[0].PresentResiStateId
                            },
                            PresentAddrCityData: {
                                Display: objUserMaster[0].PresentResiCityName,
                                Value: objUserMaster[0].PresentCityId
                            },
                            PermanentAddrCountryData: {
                                Display: objUserMaster[0].PermenantResiCountryName,
                                Value: objUserMaster[0].PermenantResiCountryId
                            },
                            PermanentAddrStateData: {
                                Display: objUserMaster[0].PermenantResiStateName,
                                Value: objUserMaster[0].PermenantResiStateId
                            },
                            PermanentAddrCityData: {
                                Display: objUserMaster[0].PermenantResiCityName,
                                Value: objUserMaster[0].PermanentCityId
                            },
                            RelativesAddrCountryData: {
                                Display: objUserMaster[0].RelativeCountryName,
                                Value: objUserMaster[0].RelativeCountryId
                            },
                            RelativesAddrStateData: {
                                Display: objUserMaster[0].RelativeStateName,
                                Value: objUserMaster[0].RelativeStateId
                            },
                            RelativesAddrCityData: {
                                Display: objUserMaster[0].RelativeCityName,
                                Value: objUserMaster[0].RelativeRefCityId
                            },
                            //RelativeRefMobNoData: {
                            //    Display: objUserMaster[0].RelativeCountryCode,
                            //    Value: objUserMaster[0].RelativeRefContactCode
                            //},
                            FriendAddrCountryData: {
                                Display: objUserMaster[0].FriendCountryName,
                                Value: objUserMaster[0].FriendCountryId
                            },
                            FriendAddrStateData: {
                                Display: objUserMaster[0].FriendStateName,
                                Value: objUserMaster[0].FriendStateId
                            },
                            FriendAddrCityData: {
                                Display: objUserMaster[0].FriendCityName,
                                Value: objUserMaster[0].FriendRefCityId
                            },
                            //FriendRefMobNoData: {
                            //    Display: objUserMaster[0].FriendCountryCode,
                            //    Value: objUserMaster[0].FriendRefContactCode
                            //},
                            WorkmateAddrCountryData: {
                                Display: objUserMaster[0].WorkMateCountryName,
                                Value: objUserMaster[0].WorkMateCountryId
                            },
                            WorkmateAddrStateData: {
                                Display: objUserMaster[0].WorkMateStateName,
                                Value: objUserMaster[0].WorkMateStateId
                            },
                            WorkmateAddrCityData: {
                                Display: objUserMaster[0].WorkMateCityName,
                                Value: objUserMaster[0].WorkMaterRefCityId
                            },
                            //WorkmateRefMobNoData: {
                            //    Display: objUserMaster[0].WorkMateCountryCode,
                            //    Value: objUserMaster[0].WorkMaterRefContactCode
                            //},
                            SourceId: objUserMaster[0].SourceId,
                            SourceName: objUserMaster[0].SourceName,
                            SourceData: {
                                Display: objUserMaster[0].SourceName,
                                Value: objUserMaster[0].SourceId
                            },
                            QualificationId: objUserMaster[0].QualificationId,
                            QualificationName: objUserMaster[0].QualificationName,
                            QualificationData: {
                                Display: objUserMaster[0].QualificationName,
                                Value: objUserMaster[0].QualificationId
                            },
                            TotalWorkExperience: objUserMaster[0].TotalWorkExperience,
                            TotalExpeYear: (objUserMaster[0].TotalWorkExperience != null) ? objUserMaster[0].TotalWorkExperience.split('.')[0] : '',
                            TotalExpeMonth: (objUserMaster[0].TotalWorkExperience != null) ? objUserMaster[0].TotalWorkExperience.split('.')[1] : ''
                        }
                        if ($scope.objUser.ReferenceTypeId == 1) {
                            $scope.showHideReferance = true;
                            $scope.showHideAgency = false;
                        }
                        else {
                            $scope.showHideReferance = false;
                            $scope.showHideAgency = true;
                        }
                        //MobileCodeBind();
                        $scope.objUser.UserContactDetails = [];
                        angular.forEach(result.data.DataList.UserContactDetail, function (value) {
                            var objContactDetail = {
                                EmpRelationId: value.EmpRelationId,
                                RelationId: value.RelationId,
                                Relation: value.Relation,
                                UserId: value.UserId,
                                Name: value.Name,
                                Email: value.Email,
                                ContactNo: value.ContactNo,
                                ContactCode: value.ContactCode,
                                UserContactcode: value.UserContactcode,
                                Status: 2,//1 : Insert , 2:Update ,3 :Delete
                            }


                            $scope.objUser.UserContactDetails.push(objContactDetail);
                        }, true);
                        $scope.objUser.UserReferanceDetails = [];
                        angular.forEach(result.data.DataList.UserReferanceDetail, function (value) {
                            var objRefDetail = {
                                ReffId: value.ReffId,
                                UserId: value.UserId,
                                ReffType: value.ReffType,
                                ReffTypeName: value.ReffTypeName,
                                ReffName: value.ReffName,
                                Email: value.Email,
                                MobileNoId: value.MobileNoId,
                                MobileNoCode: value.MobileNoCode,
                                MobileNo: value.MobileNo,
                                Address: value.Address,
                                CountryId: value.CountryId,
                                CountryName: value.CountryName,
                                StateId: value.StateId,
                                StateName: value.StateName,
                                CityId: value.CityId,
                                CityName: value.CityName,
                                Pincode: value.Pincode,
                                Status: 2,//1 : Insert , 2:Update ,3 :Delete
                            }
                            $scope.objUser.UserReferanceDetails.push(objRefDetail);
                        }, true);
                        $scope.objUser.UserSalDetails = [];
                        angular.forEach(result.data.DataList.UserSalaryDetail, function (value) {
                            var objSalDetail = {
                                EmpSalId: value.EmpSalId,
                                UserId: value.UserId,
                                SalaryHeadId: value.SalaryHeadId,
                                SalaryHead: value.SalaryHead,
                                CurrencyId: value.CurrencyId,
                                Currency: value.Currency,
                                INRAmount: value.INRAmount,
                                ExchangeRate: value.ExchangeRate,
                                Amount: value.Amount,
                                Status: 2,//1 : Insert , 2:Update ,3 :Delete
                            }
                            $scope.objUser.UserSalDetails.push(objSalDetail);
                        }, true);
                        $scope.objUser.UserDocumentDetails = [];
                        angular.forEach(result.data.DataList.UserDocumentDetail, function (value) {
                            var DocTypeVal, DocuploadVal;
                            if (value.DocUpload != null) {
                                DocTypeVal = value.DocUpload.split('.')[1].toString();
                                DocuploadVal = '/UploadImages/EmpDocumentImage/' + value.DocUpload;
                            }
                            var objDocDetail = {
                                EmpDocId: value.EmpDocId,
                                UserId: value.UserId,
                                DocId: value.DocId,
                                Documents: value.Documents,
                                DocUpload: DocuploadVal,
                                DocValue: value.DocValue,
                                DocType: DocTypeVal,
                                Status: 2
                            }
                            $scope.objUser.UserDocumentDetails.push(objDocDetail);
                        }, true);

                        $scope.objUser.UserExperDetails = [];
                        angular.forEach(result.data.DataList.UserExperDetails, function (value) {
                            var objExpDetail = {
                                EduId: value.EduId,
                                UserId: value.UserId,
                                OrganizationName: value.OrganizationName,
                                CityId: value.CityId,
                                CityName: value.CityName,
                                StateId: value.StateId,
                                StateName: value.StateName,
                                CountryId: value.CountryId,
                                CountryName: value.CountryName,
                                Address: value.Address,
                                PinCode: value.PinCode,
                                TotalExpeYear: parseInt(value.TotalWorkExperience.split('.')[0]),
                                TotalExpeMonth: parseInt(value.TotalWorkExperience.split('.')[1]),
                                TotalWorkExperience: value.TotalWorkExperience,
                                FromDate: $filter('mydate')(value.FromDate),
                                ToDate: $filter('mydate')(value.ToDate),
                                DisplayFromDate: ConvertDate(value.FromDate, "dd/mm/yyyy"),
                                DisplayToDate: ConvertDate(value.ToDate, "dd/mm/yyyy"),
                                //FromDate: ConvertDate(value.FromDate, "dd/mm/yyyy"),
                                //ToDate: ConvertDate(value.ToDate, "dd/mm/yyyy"),
                                Designation: value.Designation,
                                DesignationName: value.DesignationName,
                                Description: value.Description,
                                Status: 2,//1 : Insert , 2:Update ,3 :Delete
                                EduCountryData: { Display: value.CountryName, Value: value.CountryId },
                                EduStateData: { Display: value.StateName, Value: value.StateId },
                                EduCityData: { Display: value.CityName, Value: value.CityId },
                                EduDesignationData: { Display: value.DesignationName, Value: value.Designation }
                            }
                            $scope.objUser.UserExperDetails.push(objExpDetail);
                        }, true);

                        $scope.objUser.UserEduDetails = [];
                        angular.forEach(result.data.DataList.UserEducationDetails, function (value) {
                            var objEduDetail = {
                                EducationId: value.EducationId,
                                UserId: value.UserId,
                                InstituteName: value.InstituteName,
                                QualificationId: value.QualificationId,
                                QualificationName: value.QualificationName,
                                CityId: value.CityId,
                                CityName: value.CityName,
                                StateId: value.StateId,
                                StateName: value.StateName,
                                CountryId: value.CountryId,
                                CountryName: value.CountryName,
                                Address: value.Address,
                                PinCode: value.PinCode,
                                FromDate: $filter('mydate')(value.FromDate),
                                ToDate: $filter('mydate')(value.Todate),
                                DisplayFromDate: ConvertDate(value.FromDate, "dd/mm/yyyy"),
                                DisplayToDate: ConvertDate(value.Todate, "dd/mm/yyyy"),
                                EduDescription: value.EduDescription,
                                Status: 2,//1 : Insert , 2:Update ,3 :Delete
                                EduCountryData: { Display: value.CountryName, Value: value.CountryId },
                                EduStateData: { Display: value.StateName, Value: value.StateId },
                                EduCityData: { Display: value.CityName, Value: value.CityId },
                                EducationData: { Display: value.QualificationName, Value: value.QualificationId }
                            }
                            $scope.objUser.UserEduDetails.push(objEduDetail);
                        }, true);

                        var val = '';
                        if ($scope.objUser.ReferenceTypeId == 1) { val = "BuyerMaster"; }
                        else if ($scope.objUser.ReferenceTypeId == 2) { val = "UserMaster" }
                        else if ($scope.objUser.ReferenceTypeId == 3) { val = "Other" }

                        $scope.showHide(val);

                        $scope.objUser.ReferanceData.Display = objUserMaster[0].ReferenceMannualEntry;
                        $scope.objUser.ReferenceMannualEntry = objUserMaster[0].ReferenceMannualEntry;

                        $scope.storage = angular.copy($scope.objUser);
                    }
                    else {
                        toastr.error(result.data.Message);
                    }
                })
            }
        }
        function ConvertDate(data, format) {
            if (data == null) return '';
            var r = /\/Date\(([0-9]+)\)\//gi
            var matches = data.match(r);
            if (matches == null) return '';
            var result = matches.toString().substring(6, 19);
            var epochMilliseconds = result.replace(
            /^\/Date\(([0-9]+)([+-][0-9]{4})?\)\/$/,
            '$1');
            var b = new Date(parseInt(epochMilliseconds));
            var c = new Date(b.toString());
            var curr_date = c.getDate();
            var curr_month = c.getMonth() + 1;
            var curr_year = c.getFullYear();
            var curr_h = c.getHours();
            var curr_m = c.getMinutes();
            var curr_s = c.getSeconds();
            var curr_offset = c.getTimezoneOffset() / 60
            //var d = curr_month.toString() + '/' + curr_date + '/' + curr_year;
            //return d;
            return format.replace('mm', curr_month).replace('dd', curr_date).replace('yyyy', curr_year);
        }

        $scope.$watch("objUser.ShiftEndTime", function (newValue, oldValue) { if (newValue) { $scope.settime(); } });
        $scope.$watch("objUser.ShiftStartTime", function (newValue, oldValue) { if (newValue) { $scope.settime(); } });
        $scope.$watch("objUser.LunchEndTime", function (newValue, oldValue) { if (newValue) { $scope.settime(); } });
        $scope.$watch("objUser.LunchStartTime", function (newValue, oldValue) { if (newValue) { $scope.settime(); } });
        //$scope.$watch("objUser.Email", function (newValue, oldValue) {
        $scope.CheckUser = function () {
            UserService.CheckUser($scope.objUser).then(function (result) {
                if (result.data.ResponseType != 1 && result.data.Message != null) {
                    $scope.issuccess = false;
                    $scope.error = true;
                    $scope.msg = result.data.Message;
                    //toastr.error(result.data.Message);
                }
                else {
                    $scope.msg = '';
                    $scope.issuccess = true;
                }
            });
        }
        //});
        $scope.$watch('objUser.BirthCountryData', function (data) {
            if (data.Value != '' && $scope.objUser.BirthPlaceCountryId != null && $scope.objUser.BirthPlaceCountryId != '') {
                if (data.Value != $scope.objUser.BirthPlaceCountryId.toString()) {
                    $scope.objUser.BirthStateData.Display = '';
                    $scope.objUser.BirthStateData.Value = '';
                    $scope.objUser.BirthCityData.Display = '';
                    $scope.objUser.BirthCityData.Value = '';
                }
            } else {
                $scope.CountryBind('India');
            }
        }, true);

        $scope.$watch('objUser.BirthStateData', function (data) {
            if (data.Value != '' && $scope.objUser.BirthPlaceStateId != null && $scope.objUser.BirthPlaceStateId.toString() != '') {
                if (data.Value != $scope.objUser.BirthPlaceStateId.toString()) {
                    $scope.objUser.BirthCityData.Display = '';
                    $scope.objUser.BirthCityData.Value = '';
                }
            }
        }, true);
        $scope.$watch('objUser.HomeCountryData', function (data) {
            if (data.Value != '' && $scope.objUser.HomeTownCountryId != null && $scope.objUser.HomeTownCountryId.toString() != '') {
                if (data.Value != $scope.objUser.HomeTownCountryId.toString()) {
                    $scope.objUser.HomeStateData.Display = '';
                    $scope.objUser.HomeStateData.Value = '';
                    $scope.objUser.HomeCityData.Display = '';
                    $scope.objUser.HomeCityData.Value = '';
                }
            } else {
                $scope.CountryBind('India');
            }

        }, true);
        $scope.$watch('objUser.HomeStateData', function (data) {
            if (data.Value != '' && $scope.objUser.HomeTownStateId != null && $scope.objUser.HomeTownStateId.toString() != '') {
                if (data.Value != $scope.objUser.HomeTownStateId.toString()) {
                    $scope.objUser.HomeCityData.Display = '';
                    $scope.objUser.HomeCityData.Value = '';
                }
            }
        }, true);
        $scope.$watch('objUser.PresentAddrCountryData', function (data) {
            if (data.Value != '' && $scope.objUser.PresentResiCountryId != null && $scope.objUser.PresentResiCountryId.toString() != '') {
                if (data.Value != $scope.objUser.PresentResiCountryId.toString()) {
                    $scope.objUser.PresentAddrStateData.Display = '';
                    $scope.objUser.PresentAddrStateData.Value = '';
                    $scope.objUser.PresentAddrCityData.Display = '';
                    $scope.objUser.PresentAddrCityData.Value = '';
                }
            } else {
                $scope.CountryBind('India');
            }

        }, true);
        $scope.$watch('objUser.PresentAddrStateData', function (data) {
            if (data.Value != '' && $scope.objUser.PresentResiStateId != null && $scope.objUser.PresentResiStateId.toString() != '') {
                if (data.Value != $scope.objUser.PresentResiStateId.toString()) {
                    $scope.objUser.PresentAddrCityData.Display = '';
                    $scope.objUser.PresentAddrCityData.Value = '';
                }
            }
        }, true);
        $scope.$watch('objUser.PermanentAddrCountryData', function (data) {
            if (data.Value != '' && $scope.objUser.PermenantResiCountryId != null && $scope.objUser.PermenantResiCountryId.toString() != '') {
                if (data.Value != $scope.objUser.PermenantResiCountryId.toString()) {
                    $scope.objUser.PermanentAddrStateData.Display = '';
                    $scope.objUser.PermanentAddrStateData.Value = '';
                    $scope.objUser.PermanentAddrCityData.Display = '';
                    $scope.objUser.PermanentAddrCityData.Value = '';
                }
            }
        }, true);
        $scope.$watch('objUser.PermanentAddrStateData', function (data) {
            if (data.Value != '' && $scope.objUser.PermenantResiStateId != null && $scope.objUser.PermenantResiStateId.toString() != '') {
                if (data.Value != $scope.objUser.PermenantResiStateId.toString()) {
                    $scope.objUser.PermanentAddrCityData.Display = '';
                    $scope.objUser.PermanentAddrCityData.Value = '';
                }
            }
        }, true);
        $scope.$watch('objUserRef.RelativesAddrCountryData', function (data) {
            if (data.Value != '' && $scope.objUserRef.CountryId != null && $scope.objUserRef.CountryId.toString() != '') {
                if (data.Value != $scope.objUserRef.CountryId.toString()) {
                    $scope.objUserRef.RelativesAddrStateData.Display = '';
                    $scope.objUserRef.RelativesAddrStateData.Value = '';
                    $scope.objUserRef.RelativesAddrCityData.Display = '';
                    $scope.objUserRef.RelativesAddrCityData.Value = '';
                }
            } else {
                $scope.CountryBind('India');
            }

        }, true);
        $scope.$watch('objUserRef.RelativesAddrStateData', function (data) {
            if (data.Value != '' && $scope.objUserRef.StateId != null && $scope.objUserRef.StateId.toString() != '') {
                if (data.Value != $scope.objUserRef.StateId.toString()) {
                    $scope.objUserRef.RelativesAddrCityData.Display = '';
                    $scope.objUserRef.RelativesAddrCityData.Value = '';
                }
            }
        }, true);

        $scope.CountryBind = function (data) {
            CityService.CountryBind().then(function (result) {
                if (result.data.ResponseType == 1) {
                    _.each(result.data.DataList, function (value, key, list) {
                        if (value.CountryName.toString().toLowerCase() == data.toString().toLowerCase()) {
                            if ($scope.objUser.BirthCountryData.Display == '' || $scope.objUser.BirthCountryData.Display == null) {
                                $scope.objUser.BirthCountryData = {
                                    Display: value.CountryName,
                                    Value: value.CountryId
                                };
                            }
                            if ($scope.objUser.HomeCountryData.Display == '' || $scope.objUser.HomeCountryData.Display == null) {
                                $scope.objUser.HomeCountryData = {
                                    Display: value.CountryName,
                                    Value: value.CountryId
                                };
                            }
                            if ($scope.objUser.PresentAddrCountryData.Display == '' || $scope.objUser.PresentAddrCountryData.Display == null) {
                                $scope.objUser.PresentAddrCountryData = {
                                    Display: value.CountryName,
                                    Value: value.CountryId
                                };
                            }
                            if ($scope.objUser.PermanentAddrCountryData.Display == '' || $scope.objUser.PermanentAddrCountryData.Display == null) {
                                $scope.objUser.PermanentAddrCountryData = {
                                    Display: value.CountryName,
                                    Value: value.CountryId
                                };
                            }
                            if ($scope.objUser.RelativesAddrCountryData.Display == '' || $scope.objUser.RelativesAddrCountryData.Display == null) {
                                $scope.objUser.RelativesAddrCountryData = {
                                    Display: value.CountryName,
                                    Value: value.CountryId
                                };
                            }
                            return true;
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


        function MobileCodeBind() {
            $scope.telArray1 = [];
            $scope.RelativeRefMobNoArray = [];
            CountryService.GetCountryFlag().then(function (result) {
                //$scope.telArray2 = angular.copy(result);
                //$scope.telArray1 = angular.copy(result);
                //$scope.telArray3 = angular.copy(result);
                var res;
                var refres;
                var rescont;
                _.each(result, function (value, key, list) {

                    if ($scope.objUser.MobCode != undefined) {
                        res = $scope.objUser.MobCode;
                        if (res != undefined && res == value.id) {
                            var setcode = 0;
                            _.each($scope.MobCodeArray, function (value, key, list) {
                                if (value.ticked) {
                                    setcode = 1;
                                    return false;
                                }
                            });
                            if (setcode == 0) {
                                $scope.MobCodeArray.push({
                                    code: value.code,
                                    icon: value.icon,
                                    id: value.id,
                                    name: value.name,
                                    ticked: true
                                })
                            }
                        }
                        else {
                            var val;
                            if (res != "") {
                                val = false;
                            }
                            else {
                                val = value.ticked;
                            }
                            $scope.MobCodeArray.push({
                                code: value.code,
                                icon: value.icon,
                                id: value.id,
                                name: value.name,
                                ticked: val
                            })
                        }
                    };
                    if ($scope.objUserRef.MobileNoId == null || $scope.objUserRef.MobileNoId != undefined) {
                        refres = $scope.objUserRef.MobileNoId;
                        if (refres != undefined && refres == value.id) {
                            var setcode = 0;
                            _.each($scope.RelativeRefMobNoArray, function (value, key, list) {
                                if (value.ticked) {
                                    setcode = 1;
                                    return false;
                                }
                            });
                            if (setcode == 0) {
                                $scope.RelativeRefMobNoArray.push({
                                    code: value.code,
                                    icon: value.icon,
                                    id: value.id,
                                    name: value.name,
                                    ticked: true
                                })
                            }
                        }
                        else {
                            var rval;
                            if (refres != "") {
                                rval = false;
                            } else {
                                rval = value.ticked;
                            }
                            $scope.RelativeRefMobNoArray.push({
                                code: value.code,
                                icon: value.icon,
                                id: value.id,
                                name: value.name,
                                ticked: rval
                            })
                        }
                    };
                    if ($scope.objUserContact.ContactCode == null || $scope.objUserContact.ContactCode != undefined) {
                        rescont = $scope.objUserContact.ContactCode;
                        if (rescont != undefined && rescont == value.id) {
                            var setcode = 0;
                            _.each($scope.telArray1, function (value, key, list) {
                                if (value.ticked) {
                                    setcode = 1;
                                    return false;
                                }
                            });
                            if (setcode == 0) {
                                $scope.telArray1.push({
                                    code: value.code,
                                    icon: value.icon,
                                    id: value.id,
                                    name: value.name,
                                    ticked: true
                                })
                            }
                        }
                        else {
                            var val;
                            if (rescont != "") {
                                val = false;
                            } else {
                                val = value.ticked;
                            }
                            $scope.telArray1.push({
                                code: value.code,
                                icon: value.icon,
                                id: value.id,
                                name: value.name,
                                ticked: val
                            })
                        }
                    };
                    //if ($scope.objUser.FriendRefContactCode == null || $scope.objUser.FriendRefContactCode != undefined) {
                    //    refres = $scope.objUser.FriendRefContactCode;
                    //    if (refres != undefined && refres == value.id) {
                    //        $scope.FriendRefMobNoArray.push({
                    //            code: value.code,
                    //            icon: value.icon,
                    //            id: value.id,
                    //            name: value.name,
                    //            ticked: true
                    //        })
                    //    }
                    //    else {
                    //        var fval;
                    //        if (refres != "") {
                    //            fval = false;
                    //        } else {
                    //            fval = value.ticked;
                    //        }
                    //        $scope.FriendRefMobNoArray.push({
                    //            code: value.code,
                    //            icon: value.icon,
                    //            id: value.id,
                    //            name: value.name,
                    //            ticked: fval
                    //        })
                    //    }
                    //}
                })
            })
            RelationService.GetRelation().then(function (result) {

                var res;
                if ($scope.objUserContact.RelationId != undefined)
                    res = $scope.objUserContact.RelationId;
                _.each(result, function (value, key, list) {
                    if (res != undefined && res == value.id) {
                        $scope.telArray2.push({
                            id: value.id,
                            name: value.name,
                            ticked: true
                        })
                    }
                    else {
                        $scope.telArray2.push({
                            //code: value.code,
                            //icon: value.icon,
                            id: value.id,
                            name: value.name,
                            ticked: false
                        })
                    }
                })

            })
        };

        //CREATE
        $scope.CreateUpdate = function (data, tag) {
            var dataerror = true;
            if (tag == 'final') {
                if (data.FullNameAsPerBank == '' || data.FullNameAsPerBank == null) {
                    toastr.error("Please Enter Full Name As Per Bank.");
                    dataerror = false;
                    return false;
                } else if (data.BankNameData.Display == '' || data.BankNameData.Display == null) {
                    toastr.error("Please Select Bank Name.");
                    dataerror = false;
                    return false;
                } else if (data.BranchName == '' || data.BranchName == null) {
                    toastr.error("Please Enter Branch Name.");
                    dataerror = false;
                    return false;
                } else if (data.AccountTypeData.Display == '' || data.AccountTypeData.Display == null) {
                    toastr.error("Please Select Account Type.");
                    dataerror = false;
                    return false;
                } else if (data.AccountNo == '' || data.AccountNo == null) {
                    toastr.error("Please Enter Account No.");
                    dataerror = false;
                    return false;
                } else if (data.IFSC == '' || data.IFSC == null) {
                    toastr.error("Please Enter IFSC.");
                    dataerror = false;
                    return false;
                }
            }
            //if (data.Photo == '' && data.Photo == null) {
            //    toastr.error("Please Select Photo.")
            //}
            //else {
            data.ContactMobile = $scope.EmpContactMobCode != undefined ? $scope.EmpContactMobCode.toString() : "";
            data.ContactEmail = $scope.EmpContactEmail != undefined ? $scope.EmpContactEmail.toString() : "";
            data.ContactChat = $scope.EmpContactchat != undefined ? $scope.EmpContactchat.toString() : "";

            data.MobNo = $scope.MobCode != undefined ? $scope.MobCode.toString() : "";
            data.Email = $scope.Email != undefined ? $scope.Email.toString() : "";
            data.ChatDetail = $scope.chat != undefined ? $scope.chat.toString() : "";

            data.BirthPlaceCityId = data.BirthCityData.Value
            data.HomeTownCityId = data.HomeCityData.Value
            data.PresentCityId = data.PresentAddrCityData.Value
            data.PermanentCityId = data.PermanentAddrCityData.Value
            data.RelativeRefCityId = data.RelativesAddrCityData.Value

            if (data.RelativeRefMobNoData != undefined) {
                data.RelativeRefContactCode = data.RelativeRefMobNoData.length > 0 ? data.RelativeRefMobNoData[0].id : ''
                data.RelativeCountryCode = data.RelativeRefMobNoData.length > 0 ? data.RelativeRefMobNoData[0].code : ''
            }
            if (data.BankNameData != undefined && data.BankNameData.Value != '') {
                data.BankName = data.BankNameData.Value
            }


            data.FriendRefCityId = data.FriendAddrCityData.Value
            data.WorkMaterRefCityId = data.WorkmateAddrCityData.Value
            //data.MobCode = data.MobCodeData.Value

            data.BloodGroupId = data.BloodGroupData.Value
            data.SourceId = data.SourceData.Value
            data.QualificationId = data.QualificationData.Value
            data.TotalWorkExperience = parseFloat(data.TotalExpeYear + '.' + data.TotalExpeMonth);
            //data.EmergancyMobCode = data.EmergancyMobNoData.Value
            data.ReportingId = data.ReportingData.Value
            data.AccountTypeId = data.AccountTypeData.Value
            data.RoleId = data.RoleData.Value
            data.DepartmentId = data.DepartmentData.Value
            data.DesignationId = data.DesignationData.Value
            data.ShiftStartTime = data.ShiftStartTime == "" ? " " : $filter('date')(data.ShiftStartTime, "HH:mm")
            data.ShiftEndTime = data.ShiftEndTime == "" ? " " : $filter('date')(data.ShiftEndTime, "HH:mm")
            data.LunchStartTime = data.LunchStartTime == "" ? " " : $filter('date')(data.LunchStartTime, "HH:mm")
            data.LunchEndTime = data.LunchEndTime == "" ? " " : $filter('date')(data.LunchEndTime, "HH:mm")

            data.Gender = data.Gender == "" ? null : data.Gender;
            data.MaritalStatus = data.MaritalStatus == "" ? null : data.MaritalStatus;
            //if (data.ReferenceTypeId == 1) {
            //    data.ReferenceId = data.ReferanceData.Value
            //    data.ReferenceMannualEntry = ""
            //}
            //else {
            //    data.ReferenceId = data.AgencyTypeReferanceData.Value
            //    data.ReferenceSubType = ""
            //}

            if (data.ReferenceTypeId == 1 || data.ReferenceTypeId == 2) {
                data.ReferenceId = data.ReferanceData.Value
                data.ReferenceMannualEntry = data.ReferanceData.Display
            }
            else if (data.ReferenceTypeId == 3) {
                //data.ReferenceId = data.AgencyTypeReferanceData.Value
                data.ReferenceMannualEntry = data.ReferenceMannualEntry
            }

            if (data.UserDocumentDetails.length > 0) {
                angular.forEach(data.UserDocumentDetails, function (value, index) {
                    if (value.DocUpload != undefined) {
                        var docval = value.DocUpload.split('/');
                        value.DocUpload = docval[docval.length - 1].toString()
                    }
                }, true);
            }

            // UserData(data);

            if (tag != undefined) {
                $scope.objUserSalary.CurrencyData.Display = ' ';
            }
            if ($scope.issuccess) {
                UserService.AddUser(data).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        $scope.issuccess = true;
                        $scope.objUser.UserId = result.data.DataList;
                        if (tag != undefined) {
                            toastr.success(result.data.Message);
                            ResetForm();
                            window.location.href = "/master/User";
                        }
                        //    //$uibModalInstance.close();
                        //    //toastr.success(result.data.Message)
                        //    //$scope.refreshGrid();
                        //    //window.location.href = "/master/User/";
                        //} else {
                        //toastr.success(result.data.Message);
                    }
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                });
            }
            //}
        }
        $scope.profile = function (id) {
            $scope.objUser.MyProfile = true;
            window.location.href = "/master/User/UserPopup/" + id;
        }
        $scope.Range = function (start, end) {
            var result = [];
            for (var i = start; i <= end; i++) {
                result.push(i);
            }
            return result;
        };
        //UPDATE
        $scope.Update = function (data) {
            if (data.Photo == '' && data.Photo == null) {
                toastr.error("Please Select Photo.")
            }
            else {
                data.BirthPlaceCityId = data.BirthCityData.Value
                data.HomeTownCityId = data.HomeCityData.Value
                data.PresentCityId = data.PresentAddrCityData.Value
                data.PermanentCityId = data.PermanentAddrCityData.Value
                data.RelativeRefCityId = data.RelativesAddrCityData.Value
                if (data.RelativeRefMobNoData != undefined) {
                    data.RelativeRefContactCode = data.RelativeRefMobNoData[0].id
                    data.RelativeCountryCode = data.RelativeRefMobNoData[0].code
                }
                data.FriendRefCityId = data.FriendAddrCityData.Value
                if (data.FriendRefMobNoData != undefined) {
                    data.FriendRefContactCode = data.FriendRefMobNoData[0].id
                    data.FriendCountryCode = data.FriendRefMobNoData[0].code
                }
                data.WorkMaterRefCityId = data.WorkmateAddrCityData.Value
                if (data.WorkmateRefMobNoData != undefined) {
                    data.WorkMateCountryCode = data.WorkmateRefMobNoData[0].code
                    data.WorkMaterRefContactCode = data.WorkmateRefMobNoData[0].id
                }
                //data.MobCode = data.MobCodeData.Value
                if (data.MobCodeData != undefined) {
                    data.MobCode1 = data.MobCodeData[0].code
                    data.MobCode = data.MobCodeData[0].id;
                }
                data.BloodGroupId = data.BloodGroupData.Value
                //data.EmergancyMobCode = data.EmergancyMobNoData.Value
                data.ReportingId = data.ReportingData.Value
                data.AccountTypeId = data.AccountTypeData.Value
                data.RoleId = data.RoleData.Value
                data.DepartmentId = data.DepartmentData.Value
                data.DesignationId = data.DesignationData.Value
                data.ShiftStartTime = $filter('date')(data.ShiftStartTime, "HH:mm")
                data.ShiftEndTime = $filter('date')(data.ShiftEndTime, "HH:mm")
                data.Gender = data.Gender == "" ? null : data.Gender;
                data.MaritalStatus = data.MaritalStatus == "" ? null : data.MaritalStatus;

                if (data.ReferanceData.Value != "" && data.ReferanceData.Value != null) {
                    data.ReferenceId = data.ReferanceData.Value
                    data.ReferenceMannualEntry = ""
                }
                else {
                    data.ReferenceId = data.AgencyTypeReferanceData.Value
                    data.ReferenceSubType = ""
                }

                UserService.AddUser(data).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        //$uibModalInstance.close();
                        toastr.success(result.data.Message)
                        //$scope.refreshGrid();
                        window.location.href = "/master/User/";
                    } else {
                        toastr.error(result.data.Message, 'Opps, Something went wrong');
                    }
                }).error(function (e) {
                    toastr.error("Error Found")
                })

            }
        }

        //$scope.AddUserContactDetail = function (data) {
        //    var modalInstance = $uibModal.open({
        //        backdrop: 'static',
        //        templateUrl: 'UserContactDetail.html',
        //        controller: ModalInstanceCtrl,
        //        resolve: {
        //            UserController: function () { return $scope; },
        //            UserContactData: function () { return data; }
        //        }
        //    });
        //    modalInstance.result.then(function () {
        //        $scope.EditUserContactIndex = -1;
        //    }, function () {
        //        $scope.EditUserContactIndex = -1;
        //    })
        //}

        //DELETE BUYER CONTACT DETAIL
        $scope.uploadDocumentFile = function (file) {
            $scope.tempDoc = '';
            $scope.tempType = '';
            $scope.objUserDocument.DocUpload = '';
            $scope.objUserDocument.DocType = '';
            Upload.upload({
                url: "/Handler/FileUpload.ashx",
                method: 'POST',
                file: file,
            }).then(function (result) {
                var ext = result.config.file[0].name.split('.').pop();
                if (ext == "docx" || ext == "doc" || ext == "pdf" || ext == "jpg" || ext == "png") {
                    if (result.status == 200) {
                        if (result.data.length > 0) {
                            $scope.objUserDocument.DocUpload = result.data[0].imageName;
                            $scope.objUserDocument.DocType = ext;
                            $scope.tempDoc = result.data[0].imagePath;
                            $scope.tempType = ext;
                        }
                    }
                    else {
                        $scope.objUserDocument.DocUpload = '';
                        $scope.objUserDocument.DocType = '';
                    }
                } else {
                    toastr.error("Only Word, Pdf, Jpg, Png File Allowed.", "Error");
                }
            });
        }

        $scope.uploadImgFile = function (file) {
            $scope.objUser.Photo = '';
            Upload.upload({
                url: "/Handler/FileUpload.ashx",
                method: 'POST',
                file: file,
            }).then(function (result) {
                if (result.status == 200) {
                    if (result.data.length > 0) {
                        $scope.objUser.Photo = result.data[0].imageName;
                        $scope.tempImagePath = result.data[0].imagePath;
                    }
                }
                else {
                    $scope.objUser.Photo = '';
                }
            });
        }

        //EDIT BUYER CONTACT DETAIL

        //DONE
        $scope.statusFilter = function (val) {
            if (val.Status != 3)
                return true;
        }
        //RESET
        $scope.Reset = function () {
            ResetForm();
        };

        $scope.close = function () {
            window.location.href = "/master/User/";
        };
        //REFRESH TABLE
        $scope.RefreshTable = function () {
            $scope.tableParams.reload();
        };

        //var ModalInstanceCtrl = function ($scope, $uibModalInstance, UserController, UserContactData, CountryService, RelationService) {
        //$scope.EditContactDetail = function (data) {
        //    $scope.objUserContact = {
        //        EmpRelationId: data.EmpRelationId,
        //        RelationId: data.RelationId,
        //        UserId: data.UserId,
        //        Name: data.Name,
        //        Email: data.Email,
        //        ContactNo: data.ContactNo,
        //        ContactCode: data.ContactCode,
        //        Status: data.Status, //1 : Insert , 2:Update ,3 :Delete
        //        ContactCodeData: {
        //            Display: data.CountryCallCode,
        //            Value: data.ContactCode
        //        },
        //        outputTel1: [],
        //        outputTel2: [],
        //    };
        //}


        //BEGIN MANAGE User Relation
        //Add Relation DETAIL
        $scope.CreateUpdateRelation = function (data) {
            var rlsubmitt = true;
            data.ContactNo = $scope.ContactMobileNo != undefined ? $scope.ContactMobileNo.toString() : "";
            data.Email = $scope.ContactEmail != undefined ? $scope.ContactEmail.toString() : "";

            if (data.RelationData.Display == '' || data.RelationData.Display == null) {
                toastr.error("Please Select Relation.");
                rlsubmitt = false;
            } else if (data.Name == '' || data.Name == null) {
                toastr.error("Please Enter Name.");
                rlsubmitt = false;
            } else if (data.ContactNo == '' || data.ContactNo == null) {
                toastr.error("Please Enter Mobile No.");
                rlsubmitt = false;
            }
            //else if (data.Email == '' || data.Email == null) {
            //    toastr.error("Please Enter Email.");
            //    rlsubmitt = false;
            //}
            if (data != undefined && rlsubmitt == true) {
                var res;
                res = data.RelationData.Display;
                //data.ContactCode = data.outputTel1[0].id;
                //data.UserContactcode = data.outputTel1[0].code;
                var indx = $scope.EditUserContactIndex;
                var isExist = false;
                if ($scope.objUser.UserContactDetails.length == 0) {
                    isExist = false;
                    toastr.success("Insert successfully.")
                }
                else {
                    var getrelation = $.grep($scope.objUser.UserContactDetails, function (e) { return e.Relation == res; })
                    if (getrelation.length > 0) {
                        if (getrelation[0].Relation == "Father" || getrelation[0].Relation == "Mother" || getrelation[0].Relation == "Father-in-law" || getrelation[0].Relation == "Mother-in-law") {
                            isExist = true;
                            toastr.error("Already Exists.")
                        }
                    }

                    //_.each($scope.objUser.UserContactDetails, function (value, key, list) {
                    //    //add
                    //    if (data.Status == 0) {
                    //        //if ((value.Relation.indexOf("Father") != -1) || (value.Relation.indexOf("Mother") != -1)) {
                    //            if (res == value.Relation) {
                    //                isExist = true;
                    //                toastr.error("Already Exists.")
                    //            }
                    //        //}

                    //    }
                    //    else {
                    //        //if ((value.Relation.indexOf("Father") != -1) || (value.Relation.indexOf("Mother") != -1)) {
                    //            if (data.Status == 2 && indx != key && res == value.Relation && value.Status != 3) {
                    //                isExist = true;
                    //                toastr.error("Already Exists.")
                    //            }
                    //        //}
                    //    }

                    //})
                }
                if (isExist == false) {
                    var UserContact = {
                        EmpRelationId: data.EmpRelationId,
                        RelationId: data.RelationData.Value,
                        Relation: data.RelationData.Display,
                        UserId: data.UserId,
                        Name: data.Name,
                        Email: data.Email,
                        ContactNo: data.ContactNo,
                        //ContactCode: data.ContactCode,
                        //UserContactcode: data.UserContactcode,
                        Status: data.Status, //1 : Insert , 2:Update ,3 :Delete

                    };
                    if ($scope.EditUserContactIndex > -1) {
                        if ($scope.objUser.UserContactDetails[$scope.EditUserContactIndex].Status == 2) {
                            UserContact.Status = 2;
                        } else if ($scope.objUser.UserContactDetails[$scope.EditUserContactIndex].Status == 1 ||
                                   $scope.objUser.UserContactDetails[$scope.EditUserContactIndex].Status == undefined) {
                            UserContact.Status = 1;
                        }
                        $scope.objUser.UserContactDetails[$scope.EditUserContactIndex] = UserContact;
                        $scope.EditUserContactIndex = -1;
                    } else {
                        UserContact.Status = 1;
                        $scope.objUser.UserContactDetails.push(UserContact);
                    }
                    $scope.CreateUpdate($scope.objUser);
                    $scope.editUser($scope.objUser.UserId);

                    //$scope.UpdateProductFrom($scope.objProduct);
                    //$scope.VideoUrlDetails = {
                    //    AdId: 0,
                    //    ProductId: 0,
                    //    AdSourceId: 0,
                    //    VUrl: '',
                    //    VRemark: '',
                    //    Status: 0
                    //};
                    $scope.objUserContact = {
                        RelationId: 0,
                        UserId: 0,
                        Name: '',
                        ContactNo: '',
                        ContactCode: '',
                        Status: 0,
                        ContactCodeData: { Display: '', Value: '' },
                        RelationData: { Display: '', Value: '' }
                    };
                    $scope.outputTel1 = [];
                    $scope.ContactMobileNo = [];
                    $scope.ContactEmail = [];
                    //MobileCodeBind();
                }

            }
        }

        //EDIT Relation DETAIL
        $scope.EditUserDetail = function (data, index) {
            $scope.EditUserContactIndex = index;
            $scope.objUserContact = {
                EmpRelationId: data.EmpRelationId,
                RelationId: data.RelationId,
                UserId: data.UserId,
                Name: data.Name,
                //Email: data.Email,
                //ContactNo: data.ContactNo,
                //ContactCode: data.ContactCode,
                Status: data.Status, //1 : Insert , 2:Update ,3 :Delete
                ContactCodeData: {
                    Display: data.CountryCallCode,
                    Value: data.ContactCode
                },
                RelationData: {
                    Display: data.Relation,
                    Value: data.RelationId
                },
            };
            $scope.ContactMobileNo = (data.ContactNo != '' && data.ContactNo != null) ? data.ContactNo.split(",") : [];
            $scope.ContactEmail = (data.Email != '' && data.Email != null) ? data.Email.split(",") : [];
            //MobileCodeBind();
        }

        //DELETE Relation DETAIL
        $scope.DeleteUserDetail = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objUser.UserContactDetails[index] = data;
                } else {
                    $scope.objUser.UserContactDetails.splice(index, 1);
                }
                $scope.CreateUpdate($scope.objUser);
                $scope.editUser($scope.objUser.UserId);
                toastr.success("User contact Detail Deleted", "Success");
            })
        }
        //END MANAGE User Relation

        //BEGIN MANAGE User Referance
        //Add Referance DETAIL
        $scope.CreateUpdateRefDetail = function (data) {
            var refsubmitt = true;
            data.MobileNo = $scope.ReferenceMobileNo != undefined ? $scope.ReferenceMobileNo.toString() : "";
            data.Email = $scope.ReferenceEmail != undefined ? $scope.ReferenceEmail.toString() : "";


            if (data.ReffTypeData.Display == '' || data.ReffTypeData.Display == null) {
                toastr.error("Please Select Referance Type.");
                refsubmitt = false;
            } else if (data.ReffName == '' || data.ReffName == null) {
                toastr.error("Please Enter Referance Name.");
                refsubmitt = false;
            } else if (data.MobileNo == '' || data.MobileNo == null) {
                toastr.error("Please Enter Mobile No.");
                refsubmitt = false;
            } else if (data.Email == '' || data.Email == null) {
                toastr.error("Please Enter Email.");
                refsubmitt = false;
            } else if (data.RelativesAddrCountryData.Display == '' || data.RelativesAddrCountryData.Display == null) {
                toastr.error("Please Select Country.");
                refsubmitt = false;
            } else if (data.RelativesAddrStateData.Display == '' || data.RelativesAddrStateData.Display == null) {
                toastr.error("Please Select State.");
                refsubmitt = false;
            } else if (data.RelativesAddrCityData.Display == '' || data.RelativesAddrCityData.Display == null) {
                toastr.error("Please Select City.");
                refsubmitt = false;
            } else if (data.Pincode == '' || data.Pincode == null) {
                toastr.error("Please Enter Pincode.");
                refsubmitt = false;
            }
            if (data != undefined && refsubmitt == true) {
                //data.MobileNoId = data.RelativeRefMobNoData[0].id;
                //data.MobileNoCode = data.RelativeRefMobNoData[0].code;
                var UserRef = {
                    ReffId: data.ReffId,
                    UserId: data.UserId,
                    ReffType: data.ReffTypeData.Value,
                    ReffTypeName: data.ReffTypeData.Display,
                    ReffName: data.ReffName,
                    Email: data.Email,
                    //MobileNoId: data.MobileNoId,
                    //MobileNoCode: data.MobileNoCode,
                    MobileNo: data.MobileNo,
                    Address: data.Address,
                    CountryId: data.RelativesAddrCountryData.Value,
                    CountryName: data.RelativesAddrCountryData.Display,
                    StateId: data.RelativesAddrStateData.Value,
                    StateName: data.RelativesAddrStateData.Display,
                    CityId: data.RelativesAddrCityData.Value,
                    CityName: data.RelativesAddrCityData.Display,
                    Pincode: data.Pincode,
                    Status: data.Status,//1 : Insert , 2:Update ,3 :Delete
                };
                if ($scope.EditUserRefIndex > -1) {
                    if ($scope.objUser.UserReferanceDetails[$scope.EditUserRefIndex].Status == 2) {
                        UserRef.Status = 2;
                    } else if ($scope.objUser.UserReferanceDetails[$scope.EditUserRefIndex].Status == 1 ||
                               $scope.objUser.UserReferanceDetails[$scope.EditUserRefIndex].Status == undefined) {
                        UserRef.Status = 1;
                    }
                    $scope.objUser.UserReferanceDetails[$scope.EditUserRefIndex] = UserRef;
                    $scope.EditUserRefIndex = -1;
                } else {
                    UserRef.Status = 1;
                    $scope.objUser.UserReferanceDetails.push(UserRef);
                }
                $scope.CreateUpdate($scope.objUser);
                $scope.editUser($scope.objUser.UserId);

                //$scope.UpdateProductFrom($scope.objProduct);
                //$scope.VideoUrlDetails = {
                //    AdId: 0,
                //    ProductId: 0,
                //    AdSourceId: 0,
                //    VUrl: '',
                //    VRemark: '',
                //    Status: 0
                //};
                $scope.objUserRef = {
                    ReffId: 0,
                    UserId: 0,
                    ReffType: 0,
                    ReffTypeName: '',
                    ReffName: '',
                    Email: '',
                    MobileNoCode: '',
                    MobileNoId: '',
                    MobileNo: '',
                    Address: '',
                    CountryId: 0,
                    CountryName: '',
                    StateId: 0,
                    StateName: '',
                    CityId: 0,
                    CityName: '',
                    Pincode: '',
                    Status: 0,//1 : Insert , 2:Update ,3 :Delete
                    ReffTypeData: { Display: '', Value: '' },
                    RelativesAddrCountryData: {
                        Display: "",
                        Value: ""
                    },
                    RelativesAddrStateData: {
                        Display: "",
                        Value: ""
                    },
                    RelativesAddrCityData: {
                        Display: "",
                        Value: ""
                    },
                };
                $scope.RelativeRefMobNoData = [];
                $scope.ReferenceEmail = [];
                $scope.ReferenceMobileNo = [];
                //MobileCodeBind();
            }
        }

        //EDIT Referance DETAIL
        $scope.EditUserRefDetail = function (data, index) {
            $scope.EditUserRefIndex = index;
            var mob = data.MobileNo.split(' ');
            $scope.objUserRef = {
                ReffId: data.ReffId,
                UserId: data.UserId,
                ReffType: data.ReffType,
                ReffTypeName: data.ReffTypeName,
                ReffName: data.ReffName,
                Email: data.Email,
                //MobileNoId: data.MobileNoId,
                //MobileNoCode: data.MobileNoCode == null ? mob[0] : data.MobileNoCode,
                MobileNo: data.MobileNo,
                Address: data.Address,
                CountryId: data.CountryId,
                CountryName: data.CountryName,
                StateId: data.StateId,
                StateName: data.StateName,
                CityId: data.CityId,
                CityName: data.CityName,
                Pincode: data.Pincode,
                Status: data.Status,//1 : Insert , 2:Update ,3 :Delete
                ReffTypeData: {
                    Display: data.ReffTypeName,
                    Value: data.ReffType
                },
                RelativesAddrCountryData: {
                    Display: data.CountryName,
                    Value: data.CountryId
                },
                RelativesAddrStateData: {
                    Display: data.StateName,
                    Value: data.StateId
                },
                RelativesAddrCityData: {
                    Display: data.CityName,
                    Value: data.CityId
                },
            };
            $scope.ReferenceMobileNo = (data.MobileNo != '' && data.MobileNo != null) ? data.MobileNo.split(",") : [];
            $scope.ReferenceEmail = (data.Email != '' && data.Email != null) ? data.Email.split(",") : [];
            //MobileCodeBind();
        }

        //DELETE Referance DETAIL
        $scope.DeleteUserRefDetail = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objUser.UserReferanceDetails[index] = data;
                } else {
                    $scope.objUser.UserReferanceDetails.splice(index, 1);
                }
                $scope.CreateUpdate($scope.objUser);
                $scope.editUser($scope.objUser.UserId);
                toastr.success("User contact Detail Deleted", "Success");
            })
        }
        //END MANAGE User Referance

        //BEGIN MANAGE User Relation
        //Add Relation DETAIL
        $scope.CreateUpdateSalary = function (data, isdisable) {
            //EmpSalId: 0,
            //UserId: 0,
            //SalaryHeadId: 0,
            //SalaryHead: '',
            //CurrencyId: 0,
            //Currency: '',
            //INRAmount: '',
            //ExchangeRate:'',
            //Amount: '',
            //Status: 0,//1 : Insert , 2:Update ,3 :Delete
            //SalaryHeadData: { Display: '', Value: '' },
            //CurrencyData: { Display: '', Value: '' }

            var rlsubmitt = true;
            if (data.SalaryHeadData.Display == '' || data.SalaryHeadData.Display == null) {
                toastr.error("Please Select Salary Head.");
                rlsubmitt = false;
            } else if (data.CurrencyData.Display == '' || data.CurrencyData.Display == null) {
                toastr.error("Please Select Currency.");
                rlsubmitt = false;
            }
            else if (isdisable > 1 && (data.Amount == '' || data.Amount == null)) {
                toastr.error("Please Enter Amount.");
                rlsubmitt = false;
            }
            if (data != undefined && rlsubmitt == true) {
                var res;
                res = data.SalaryHeadData.Display;
                var indx = $scope.EditUserSalaryIndex;
                var isExist = false;
                if ($scope.objUser.UserSalDetails.length == 0) {
                    isExist = false;
                    toastr.success("Insert successfully.")
                }
                else {
                    _.each($scope.objUser.UserSalDetails, function (value, key, list) {
                        //add
                        if (data.Status == 0) {
                            if (res == value.SalaryHead) {
                                isExist = true;
                                toastr.error("Already Exists.")
                            }

                        }
                        else {

                            if (data.Status == 2 && indx != key && res == value.SalaryHead && value.Status != 3) {
                                isExist = true;
                                toastr.error("Already Exists.")
                            }

                        }

                    })
                }
                if (isExist == false) {
                    var UserSalary = {
                        EmpSalId: data.EmpSalId,
                        UserId: data.UserId,
                        SalaryHeadId: data.SalaryHeadData.Value,
                        SalaryHead: data.SalaryHeadData.Display,
                        CurrencyId: data.CurrencyData.Value,
                        Currency: data.CurrencyData.Display,
                        INRAmount: data.INRAmount,
                        ExchangeRate: data.ExchangeRate,
                        Amount: data.Amount,
                        Status: data.Status,//1 : Insert , 2:Update ,3 :Delete
                    };
                    if ($scope.EditUserSalaryIndex > -1) {
                        if ($scope.objUser.UserSalDetails[$scope.EditUserSalaryIndex].Status == 2) {
                            UserSalary.Status = 2;
                        } else if ($scope.objUser.UserSalDetails[$scope.EditUserSalaryIndex].Status == 1 ||
                                   $scope.objUser.UserSalDetails[$scope.EditUserSalaryIndex].Status == undefined) {
                            UserSalary.Status = 1;
                        }
                        $scope.objUser.UserSalDetails[$scope.EditUserSalaryIndex] = UserSalary;
                        $scope.EditUserSalaryIndex = -1;
                    } else {
                        UserSalary.Status = 1;
                        $scope.objUser.UserSalDetails.push(UserSalary);
                    }
                    $scope.CreateUpdate($scope.objUser);
                    $scope.editUser($scope.objUser.UserId);

                    $scope.objUserSalary = {
                        EmpSalId: 0,
                        UserId: 0,
                        SalaryHeadId: 0,
                        SalaryHead: '',
                        CurrencyId: 0,
                        Currency: '',
                        INRAmount: '',
                        ExchangeRate: '',
                        Amount: '',
                        Status: 0,//1 : Insert , 2:Update ,3 :Delete
                        SalaryHeadData: { Display: '', Value: '' },
                        CurrencyData: { Display: '', Value: '' }
                    };
                }

            }
        }

        $scope.setPrice = function () {
            var Amount = parseFloat(isNaN($scope.objUserSalary.Amount) ? 0 : $scope.objUserSalary.Amount);
            var ExchangeRate = parseFloat(isNaN($scope.objUserSalary.ExchangeRate) ? 0 : $scope.objUserSalary.ExchangeRate);
            $scope.objUserSalary.INRAmount = (Amount * ExchangeRate)
        }
        //EDIT Relation DETAIL
        $scope.EditSalary = function (data, index) {
            $scope.EditUserSalaryIndex = index;
            $scope.objUserSalary = {
                EmpSalId: data.EmpSalId,
                UserId: data.UserId,
                SalaryHeadId: data.SalaryHeadId,
                SalaryHead: data.SalaryHead,
                CurrencyData: { Display: data.Currency, Value: data.CurrencyId },
                INRAmount: data.INRAmount,
                ExchangeRate: data.ExchangeRate,
                Amount: data.Amount,
                SalaryHeadData: { Display: data.SalaryHead, Value: data.SalaryHeadId },
                Status: data.Status,//1 : Insert , 2:Update ,3 :Delete
            };
        }

        //DELETE Relation DETAIL
        $scope.DeleteSalary = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objUser.UserSalDetails[index] = data;
                } else {
                    $scope.objUser.UserSalDetails.splice(index, 1);
                }
                $scope.CreateUpdate($scope.objUser);
                $scope.editUser($scope.objUser.UserId);
                toastr.success("User Salary Detail Deleted", "Success");
            })
        }

        $scope.CreateUpdateDocument = function (data) {

            var rlsubmitt = true;
            if (data.DocumentsTypeData.Display == '' || data.DocumentsTypeData.Display == null) {
                toastr.error("Please Select Documents Type.");
                rlsubmitt = false;
            } else if (data.DocValue == '' || data.DocValue == null) {
                toastr.error("Please Enter Documents Value.");
                rlsubmitt = false;
            }
            if (data != undefined && rlsubmitt == true) {
                var res;
                res = data.DocumentsTypeData.Display;
                var indx = $scope.EditUserDocumentIndex;
                var isExist = false;
                if ($scope.objUser.UserDocumentDetails.length == 0) {
                    isExist = false;
                    toastr.success("Insert successfully.")
                }
                else {
                    _.each($scope.objUser.UserDocumentDetails, function (value, key, list) {
                        //add
                        if (data.Status == 0) {
                            if (res == value.Documents) {
                                isExist = true;
                                toastr.error("Already Exists.")
                            }

                        }
                        else {

                            if (data.Status == 2 && indx != key && res == value.Documents && value.Status != 3) {
                                isExist = true;
                                toastr.error("Already Exists.")
                            }

                        }

                    })
                }
                if (isExist == false) {
                    var UserDocument = {
                        EmpDocId: data.EmpDocId,
                        UserId: data.UserId,
                        DocId: data.DocumentsTypeData.Value,
                        Documents: data.DocumentsTypeData.Display,
                        DocUpload: data.DocUpload,
                        DocValue: data.DocValue,
                        DocType: data.DocType,
                        Status: data.Status//1 : Insert , 2:Update ,3 :Delete
                    };
                    if ($scope.EditUserDocumentIndex > -1) {
                        if ($scope.objUser.UserDocumentDetails[$scope.EditUserDocumentIndex].Status == 2) {
                            UserDocument.Status = 2;
                        } else if ($scope.objUser.UserDocumentDetails[$scope.EditUserDocumentIndex].Status == 1 ||
                                   $scope.objUser.UserDocumentDetails[$scope.EditUserDocumentIndex].Status == undefined) {
                            UserDocument.Status = 1;
                        }
                        $scope.objUser.UserDocumentDetails[$scope.EditUserDocumentIndex] = UserDocument;
                        $scope.EditUserDocumentIndex = -1;
                    } else {
                        UserDocument.Status = 1;
                        $scope.objUser.UserDocumentDetails.push(UserDocument);
                    }
                    $scope.CreateUpdate($scope.objUser);
                    $scope.objUserDocument = {
                        EmpDocId: 0,
                        UserId: 0,
                        DocId: 0,
                        Documents: '',
                        DocValue: '',
                        DocUpload: '',
                        DocType: '',
                        Status: 0,//1 : Insert , 2:Update ,3 :Delete
                        DocumentsTypeData: { Display: '', Value: '' }
                    };
                    //angular.forEach($scope.objUser.UserDocumentDetails, function (value, index) {
                    //    if (value.DocUpload.indexOf('/UploadImages/') == -1) {
                    //        if (value.Status == 1) {
                    //            value.DocUpload = '/UploadImages/TempImg/' + value.DocUpload;
                    //        } else if (value.Status == 2) {
                    //            value.DocUpload = '/UploadImages/EmpDocumentImage/' + value.DocUpload;
                    //        }
                    //    }
                    //}, true);
                    $scope.objUserDocument.DocUpload = '';
                    $scope.objUserDocument.DocType = '';
                    $scope.tempDoc = '';
                    $scope.tempType = '';
                    $scope.editUser($scope.objUser.UserId);
                }

            }
        }

        //EDIT Relation DETAIL
        $scope.EditDocument = function (data, index) {
            $scope.EditUserDocumentIndex = index;
            $scope.objUserDocument = {
                EmpDocId: data.EmpDocId,
                UserId: data.UserId,
                DocId: data.DocId,
                Documents: data.Documents,
                DocValue: data.DocValue,
                DocUpload: data.DocUpload,
                DocType: data.DocType,
                Status: data.Status,//1 : Insert , 2:Update ,3 :Delete
                DocumentsTypeData: { Display: data.Documents, Value: data.DocId }
            };
            var ext = data.DocUpload.split('.').pop();
            $scope.tempType = ext;
            $scope.tempDoc = data.DocUpload;
        }

        //DELETE Relation DETAIL
        $scope.DeleteDocument = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objUser.UserDocumentDetails[index] = data;
                } else {
                    $scope.objUser.UserDocumentDetails.splice(index, 1);
                }
                $scope.CreateUpdate($scope.objUser);
                $scope.editUser($scope.objUser.UserId);
                toastr.success("User Document Detail Deleted", "Success");
            })
        }


        //DELETE Relation DETAIL
        $scope.DeleteSalary = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objUser.UserSalDetails[index] = data;
                } else {
                    $scope.objUser.UserSalDetails.splice(index, 1);
                }
                $scope.CreateUpdate($scope.objUser);
                $scope.editUser($scope.objUser.UserId);
                toastr.success("User Salary Detail Deleted", "Success");
            })
        }

        //ADD Experience DETAIL
        $scope.CreateExperienceDocument = function (data) {
            var exsubmitt = true;
            if (data.OrganizationName == '' || data.OrganizationName == null) {
                toastr.error("Please Enter Organization Name.");
                exsubmitt = false;
            } else if (data.EduCountryData.Display == '' || data.EduCountryData.Display == null) {
                toastr.error("Please Select Country.");
                exsubmitt = false;
            } else if (data.EduStateData.Display == '' || data.EduStateData.Display == null) {
                toastr.error("Please Select State.");
                exsubmitt = false;
            } else if (data.EduCityData.Display == '' || data.EduCityData.Display == null) {
                toastr.error("Please Select City.");
                exsubmitt = false;
            } else if (data.FromDate == '' || data.FromDate == null) {
                toastr.error("Please Select From Date");
                exsubmitt = false;
            } else if (data.ToDate == '' || data.ToDate == null) {
                toastr.error("Please Select To Date");
                exsubmitt = false;
            } else if (data.EduDesignationData.Display == '' || data.EduDesignationData.Display == null) {
                toastr.error("Please Select Designation.");
                exsubmitt = false;
            }

            if (data != undefined && exsubmitt == true) {
                var UserEducation = {
                    EduId: data.EduId,
                    UserId: data.UserId,
                    OrganizationName: data.OrganizationName,
                    CityId: data.EduCityData.Value,
                    CityName: data.EduCityData.Display,
                    StateId: data.EduStateData.Value,
                    StateName: data.EduStateData.Display,
                    CountryId: data.EduCountryData.Value,
                    CountryName: data.EduCountryData.Display,
                    Address: data.Address,
                    PinCode: data.PinCode,
                    TotalExpeYear: data.TotalExpeYear,
                    TotalExpeMonth: data.TotalExpeMonth,
                    TotalWorkExperience: data.TotalExpeYear.toString() + '.' + data.TotalExpeMonth.toString(),
                    FromDate: data.FromDate,
                    ToDate: data.ToDate,
                    Designation: data.EduDesignationData.Value,
                    DesignationName: data.EduDesignationData.Display,
                    Description: data.Description,
                    Status: data.Status,//1 : Insert , 2:Update ,3 :Delete
                    EduCountryData: { Display: data.CountryName, Value: data.CountryId },
                    EduStateData: { Display: data.StateName, Value: data.StateId },
                    EduCityData: { Display: data.CityName, Value: data.CityId },
                    EduDesignationData: { Display: data.DesignationName, Value: data.Designation }
                };
                if ($scope.EditUserExperienceIndex > -1) {
                    if ($scope.objUser.UserExperDetails[$scope.EditUserExperienceIndex].Status == 2) {
                        UserEducation.Status = 2;
                    } else if ($scope.objUser.UserExperDetails[$scope.EditUserExperienceIndex].Status == 1 ||
                               $scope.objUser.UserExperDetails[$scope.EditUserExperienceIndex].Status == undefined) {
                        UserEducation.Status = 1;
                    }
                    $scope.objUser.UserExperDetails[$scope.EditUserExperienceIndex] = UserEducation;
                    $scope.EditUserExperienceIndex = -1;
                } else {
                    UserEducation.Status = 1;
                    $scope.objUser.UserExperDetails.push(UserEducation);
                }
                $scope.CreateUpdate($scope.objUser);
                $scope.editUser($scope.objUser.UserId);
                $scope.objUserEducation = {
                    EduId: 0,
                    UserId: 0,
                    OrganizationName: '',
                    CityId: 0,
                    CityName: '',
                    StateId: 0,
                    StateName: '',
                    CountryId: 0,
                    CountryName: '',
                    Address: '',
                    FromDate: '',
                    ToDate: '',
                    PinCode: '',
                    TotalExpeYear: '',
                    TotalExpeMonth: '',
                    TotalWorkExperience: '',
                    DisplayFromDate: '',
                    DisplayToDate: '',
                    Designation: 0,
                    DesignationName: '',
                    Description: '',
                    Status: 0,//1 : Insert , 2:Update ,3 :Delete
                    EduCountryData: { Display: '', Value: '' },
                    EduStateData: { Display: '', Value: '' },
                    EduCityData: { Display: '', Value: '' },
                    EduDesignationData: { Display: '', Value: '' }
                }
            }
        }

        //EDIT Experience DETAIL
        $scope.EditExperience = function (data, index) {
            $scope.EditUserExperienceIndex = index;
            $scope.objUserEducation = {
                EduId: data.EduId,
                UserId: data.UserId,
                OrganizationName: data.OrganizationName,
                CityId: data.CityId,
                CityName: data.CityName,
                StateId: data.StateId,
                StateName: data.StateName,
                CountryId: data.CountryId,
                CountryName: data.CountryName,
                Address: data.Address,
                FromDate: data.FromDate,
                ToDate: data.ToDate,
                PinCode: data.PinCode,
                TotalExpeYear: data.TotalExpeYear,
                TotalExpeMonth: data.TotalExpeMonth,
                TotalWorkExperience: data.TotalExpeYear.toString() + '.' + data.TotalExpeMonth.toString(),
                Designation: data.Designation,
                DesignationName: data.DesignationName,
                Description: data.Description,
                Status: data.Status,//1 : Insert , 2:Update ,3 :Delete
                EduCountryData: { Display: data.CountryName, Value: data.CountryId },
                EduStateData: { Display: data.StateName, Value: data.StateId },
                EduCityData: { Display: data.CityName, Value: data.CityId },
                EduDesignationData: { Display: data.DesignationName, Value: data.Designation }
            };
        }

        //DELETE Experience DETAIL
        $scope.DeleteExperience = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objUser.UserExperDetails[index] = data;
                } else {
                    $scope.objUser.UserExperDetails.splice(index, 1);
                }
                $scope.CreateUpdate($scope.objUser);
                $scope.editUser($scope.objUser.UserId);
                toastr.success("User Education Detail Deleted", "Success");
            })
        }

        //ADD Experience DETAIL
        $scope.CreateEducation = function (data) {
            var exsubmitt = true;
            if (data.EducationData.Display == '' || data.EducationData.Display == null) {
                toastr.error("Please Select Education.");
                exsubmitt = false;
            } else if (data.InstituteName == '' || data.InstituteName == null) {
                toastr.error("Please Enter InstituteName.");
                exsubmitt = false;
            } else if (data.EduCountryData.Display == '' || data.EduCountryData.Display == null) {
                toastr.error("Please Select Country.");
                exsubmitt = false;
            } else if (data.EduStateData.Display == '' || data.EduStateData.Display == null) {
                toastr.error("Please Select State.");
                exsubmitt = false;
            } else if (data.EduCityData.Display == '' || data.EduCityData.Display == null) {
                toastr.error("Please Select City.");
                exsubmitt = false;
            } else if (data.FromDate == '' || data.FromDate == null) {
                toastr.error("Please Select From Date");
                exsubmitt = false;
            } else if (data.ToDate == '' || data.ToDate == null) {
                toastr.error("Please Select To Date");
                exsubmitt = false;
            }

            if (data != undefined && exsubmitt == true) {
                var UserEducation = {
                    EducationId: data.EducationId,
                    UserId: data.UserId,
                    InstituteName: data.InstituteName,
                    QualificationId: data.EducationData.Value,
                    QualificationName: data.EducationData.Display,
                    CityId: data.EduCityData.Value,
                    CityName: data.EduCityData.Display,
                    StateId: data.EduStateData.Value,
                    StateName: data.EduStateData.Display,
                    CountryId: data.EduCountryData.Value,
                    CountryName: data.EduCountryData.Display,
                    Address: data.Address,
                    PinCode: data.PinCode,
                    FromDate: data.FromDate,
                    ToDate: data.ToDate,
                    EduDescription: data.EduDescription,
                    Status: data.Status,//1 : Insert , 2:Update ,3 :Delete
                    EduCountryData: { Display: data.CountryName, Value: data.CountryId },
                    EduStateData: { Display: data.StateName, Value: data.StateId },
                    EduCityData: { Display: data.CityName, Value: data.CityId },
                    EduDesignationData: { Display: data.DesignationName, Value: data.Designation },
                    EducationData: { Display: data.QualificationName, Value: data.QualificationId }
                };
                if ($scope.EditUserEducationIndex > -1) {
                    if ($scope.objUser.UserEduDetails[$scope.EditUserEducationIndex].Status == 2) {
                        UserEducation.Status = 2;
                    } else if ($scope.objUser.UserEduDetails[$scope.EditUserEducationIndex].Status == 1 ||
                               $scope.objUser.UserEduDetails[$scope.EditUserEducationIndex].Status == undefined) {
                        UserEducation.Status = 1;
                    }
                    $scope.objUser.UserEduDetails[$scope.EditUserEducationIndex] = UserEducation;
                    $scope.EditUserEducationIndex = -1;
                } else {
                    UserEducation.Status = 1;
                    $scope.objUser.UserEduDetails.push(UserEducation);
                }
                $scope.CreateUpdate($scope.objUser);
                $scope.editUser($scope.objUser.UserId);
                $scope.objUserExpEducation = {
                    EducationId: 0,
                    UserId: 0,
                    InstituteName: '',
                    QualificationId: 0,
                    QualificationName: '',
                    CityId: 0,
                    CityName: '',
                    StateId: 0,
                    StateName: '',
                    CountryId: 0,
                    CountryName: '',
                    Address: '',
                    FromDate: '',
                    ToDate: '',
                    PinCode: '',
                    DisplayFromDate: '',
                    DisplayToDate: '',
                    EduDescription: '',
                    Status: 0,//1 : Insert , 2:Update ,3 :Delete
                    EduCountryData: { Display: '', Value: '' },
                    EduStateData: { Display: '', Value: '' },
                    EduCityData: { Display: '', Value: '' },
                    EducationData: { Display: '', Value: '' }
                }
            }
        }

        //EDIT Experience DETAIL
        $scope.EditEducation = function (data, index) {
            $scope.EditUserEducationIndex = index;
            $scope.objUserExpEducation = {
                EducationId: data.EducationId,
                UserId: data.UserId,
                InstituteName: data.InstituteName,
                QualificationId: data.EducationData.Value,
                QualificationName: data.EducationData.Display,
                CityId: data.EduCityData.Value,
                CityName: data.EduCityData.Display,
                StateId: data.EduStateData.Value,
                StateName: data.EduStateData.Display,
                CountryId: data.EduCountryData.Value,
                CountryName: data.EduCountryData.Display,
                Address: data.Address,
                PinCode: data.PinCode,
                FromDate: data.FromDate,
                ToDate: data.ToDate,
                EduDescription: data.EduDescription,
                Status: data.Status,//1 : Insert , 2:Update ,3 :Delete
                EduCountryData: { Display: data.CountryName, Value: data.CountryId },
                EduStateData: { Display: data.StateName, Value: data.StateId },
                EduCityData: { Display: data.CityName, Value: data.CityId },
                EduDesignationData: { Display: data.DesignationName, Value: data.Designation },
                EducationData: { Display: data.QualificationName, Value: data.QualificationId }
            };
        }

        //DELETE Experience DETAIL
        $scope.DeleteEducation = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objUser.UserEduDetails[index] = data;
                } else {
                    $scope.objUser.UserEduDetails.splice(index, 1);
                }
                $scope.CreateUpdate($scope.objUser);
                $scope.editUser($scope.objUser.UserId);
                toastr.success("User Education Detail Deleted", "Success");
            })
        }
    }]);

})()
