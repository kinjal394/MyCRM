(function () {

    "use strict";
    angular.module("CRMApp.Controllers")
      .controller("IntervieweeCandidateCtrl", [
       "$scope", "$rootScope", "$timeout", "$filter", "IntervieweeCandidateService", "CountryService", "SourceService", "Upload",
       IntervieweeCandidateCtrl
      ]);

    function IntervieweeCandidateCtrl($scope, $rootScope, $timeout, $filter, IntervieweeCandidateService, CountryService, SourceService, Upload) {

        $scope.objIntervieweeCandidate = $scope.objIntervieweeCandidate || {};
        //IntCandId, ReferenceTypeId, ReferenceId, ReferenceSubType, ReferenceMannualEntry, CityId, Address, Pincode, MobileNo, Email, CommunicateDate, FirstName, SurName
        //Gender, MaritalStatus, Birthdate, TotalWorkExperience, CurrentCTC, CurrentExpected, NoticePeriod, UploadResume, IsActive
        $scope.objIntervieweeCandidate = {
            IntCandId: 0,
            ReferenceTypeId: 0,
            ReferenceId: 0,
            ReferenceSubType: 0,
            ReferenceMannualEntry: '',
            CityId: 0,
            CityName: '',
            StateId: 0,
            StateName: '',
            CountryId: 0,
            CountryName: '',
            QualificationId: 0,
            QualificationName: '',
            Address: '',
            Pincode: '',
            MobileNo: '',
            Email: '',
            CommunicateDate: new Date(),
            FirstName: '',
            Surname: '',
            Gender: '',
            MaritalStatus: '',
            Birthdate: '',
            TotalWorkExperience: '',
            CurrentCTC: 0,
            CurrentExpected: 0,
            NoticePeriod: '',
            UploadResume: '',
            CountryData: { Display: '', Value: '' },
            StateData: { Display: '', Value: '' },
            CityData: { Display: '', Value: '' },
            EducationData: { Display: '', Value: '' },
            ReferanceData: { Display: '', Value: '' },
            TotalExpeYear: '',
            TotalExpeMonth: '',
            SourceData: { Display: '', Value: '' },
            SourceId: '',
            Source: '',
            CandidateRefno: '',
            Chat: '',
        };
        $scope.telCodeData = [];
        $scope.chatCodeData = [];

        $scope.showHide = function (value) {
            //alert(value)
            if (value == "BuyerMaster") {
                $scope.showHideReferance = true;
                $scope.showHideAgency = false;
                $scope.objIntervieweeCandidate.ReferenceMannualEntry = " ";
                $scope.objIntervieweeCandidate.ReferanceData.Display = "";
            }
            else if (value == "UserMaster") {
                $scope.showHideReferance = true;
                $scope.showHideAgency = false;
                $scope.objIntervieweeCandidate.ReferenceMannualEntry = " ";
                $scope.objIntervieweeCandidate.ReferanceData.Display = "";
            }
            else if (value == "Other") {
                $scope.showHideReferance = false;
                $scope.showHideAgency = true;
                $scope.objIntervieweeCandidate.ReferanceData.Display = " ";
                $scope.objIntervieweeCandidate.ReferenceMannualEntry = "";
            }
            $scope.ReferanceMode = value;

            //if (value == "1") {
            //    $scope.showHideReferance = true;
            //    $scope.showHideAgency = false;
            //    $scope.objIntervieweeCandidate.AgencyTypeReferanceData = {
            //        Display: "",
            //        Value: ""
            //    };
            //}
            //else {
            //    $scope.showHideReferance = false;
            //    $scope.showHideAgency = true;
            //    $scope.objIntervieweeCandidate.ReferanceData = {
            //        Display: "",
            //        Value: ""
            //    };

            //}
        }

        $scope.newValue = function (value) {
            $scope.ReferanceMode = value;

        }

        $scope.Range = function (start, end) {
            var result = [];
            for (var i = start; i <= end; i++) {
                result.push(i);
            }
            return result;
        };

        $scope.Add = function (data) {
            window.location.href = "/Employee/IntervieweeCandidate/AddIntervieweeCandidate";
        }

        $scope.SetValue = function (value, type) {
            $scope.SenderMode = value;
            $scope.objIntervieweeCandidate.SenderType = type;
            $scope.objIntervieweeCandidate.SenderData = {
                Display: "",
                Value: ""
            };
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
            if (ddiff > 0) age.push(ddiff + ' day' + (ddiff > 1 ? 's' : ''));
            if (age.length > 1) age.splice(age.length - 1, 0, ' and ');
            return age.join('');
        }

        $scope.$watch('objIntervieweeCandidate.Birthdate', function (birthdate) {

            var today = new Date();
            var data = getAge(birthdate, today);
            $scope.age = data;
        });



        $scope.gridObj = {
            columnsInfo: [
                //IntCandId,FirstName +' '+ SurName [Name],MobileNo,Email,IsActive
               //{ "title": "IntCandId", "data": "IntCandId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "Name", "field": "Name", sortable: "Name", filter: { Name: "text" }, show: true, },
               { "title": "Location", "field": "Location", sortable: "Location", filter: { Location: "text" }, show: true, },
               { "title": "Mobile No", "field": "MobileNo", sortable: "MobileNo", filter: { MobileNo: "text" }, show: true, },
               { "title": "Email", "field": "Email", sortable: "Email", filter: { Email: "text" }, show: true, },
               { "title": "Gender", "field": "Gender", sortable: "Gender", filter: { Gender: "text" }, show: true, },
               { "title": "Age (Years)", "field": "Age", sortable: "Age", filter: { Age: "text" }, show: true, },
               { "title": "Education", "field": "Education", sortable: "Education", filter: { Education: "text" }, show: true, },
               { "title": "Experience (in Years)", "field": "Experience", sortable: "Experience", filter: { Experience: "text" }, show: true, },
               { "title": "Reference Mannual Entry", "field": "ReferenceMannualEntry", sortable: "ReferenceMannualEntry", filter: { ReferenceMannualEntry: "text" }, show: false, },
               {
                   "title": "Date of 1st Communication", "field": "CommunicateDate", sortable: "CommunicateDate", filter: { CommunicateDate: "date" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<p ng-bind="ConvertDate(row.CommunicateDate,\'dd-mm-yyyy\')">'
                       return $scope.getHtml(element);
                   }
               },
               { "title": "Address", "field": "Address", sortable: "Address", filter: { Address: "text" }, show: false, },
               { "title": "Pincode", "field": "Pincode", sortable: "Pincode", filter: { Pincode: "text" }, show: false, },
               //{ "title": "Marital Status", "field": "MaritalStatus", sortable: "MaritalStatus", filter: { MaritalStatus: "text" }, show: false, },
               { "title": "Current CTC", "field": "CurrentCTC", sortable: "CurrentCTC", filter: { CurrentCTC: "text" }, show: false, },
               { "title": "Current Expected", "field": "CurrentExpected", sortable: "CurrentExpected", filter: { CurrentExpected: "text" }, show: false, },
               { "title": "Notice Period", "field": "NoticePeriod", sortable: "NoticePeriod", filter: { NoticePeriod: "text" }, show: false, },
               { "title": "Chat", "field": "Chat", sortable: "Chat", filter: { Chat: "text" }, show: false, },
               { "title": "Candidate Reff No", "field": "CandidateRefNo", sortable: "CandidateRefNo", filter: { CandidateRefNo: "text" }, show: false, },
               { "title": "Source Name", "field": "SourceName", sortable: "SourceName", filter: { SourceName: "text" }, show: false, },
               {
                   "title": "Upload Resume", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a href="http://localhost:50300/UploadImages/ResumeFile/' + row.UploadResume + '" download="' + row.Name + '.PDF" data-uib-tooltip="Resume Download">Download</a>'
                       return $scope.getHtml(element);
                   }
               },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.IntCandId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                            //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.IntCandId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                            '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.IntCandId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { "IntCandId": "asc" }
        }

        $scope.SetIntervieweeCandidateId = function (id, isdisable) {
            CountryService.GetCountryFlag().then(function (result) {
                $scope.telCodeData = angular.copy(result);
                SourceService.GetSourceData().then(function (result) {
                    $scope.chatCodeData = angular.copy(result);
                    if (id > 0) {
                        $scope.SrNo = id;
                        $scope.addMode = false;
                        $scope.saveText = "Update";
                        $scope.GetAllIntervieweeCandidateById(id);
                        $scope.isClicked = false;
                        if (isdisable == 1) {
                            $scope.isClicked = true;
                        }
                    } else {
                        $scope.SrNo = 0;
                        $scope.addMode = true;
                        $scope.saveText = "Save";
                        //$scope.GetInvoice();
                        $scope.isClicked = false;
                    }
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }
        //$scope.GetInvoice = function () {
        //    IntervieweeCandidateService.InterviewCanInfo().then(function (result) {
        //        if (result.data.ResponseType == 1) {
        //            $scope.objIntervieweeCandidate.CandidateRefno = result.data.DataList.CandidateRefno;
        //        } else if (result.ResponseType == 3) {
        //            toastr.error(result.data.Message, 'Opps, Something went wrong');
        //        }
        //    }, function (error) {
        //        $rootScope.errorHandler(error)
        //    })
        //}
        $scope.GetAllIntervieweeCandidateById = function (id) {
            IntervieweeCandidateService.FetchAllInfoById(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    var objIntervieweeCandidateMaster = result.data.DataList.objInterviweeCandidate;

                    $scope.teliphone = (objIntervieweeCandidateMaster.MobileNo != '') ? objIntervieweeCandidateMaster.MobileNo.split(",") : [];
                    $scope.mail = (objIntervieweeCandidateMaster.Email != '') ? objIntervieweeCandidateMaster.Email.split(",") : [];
                    $scope.chat = (objIntervieweeCandidateMaster.Chat != '' && objIntervieweeCandidateMaster.Chat != null) ? objIntervieweeCandidateMaster.Chat.split(",") : [];

                    $scope.objIntervieweeCandidate = {
                        IntCandId: objIntervieweeCandidateMaster.IntCandId,
                        ReferenceTypeId: objIntervieweeCandidateMaster.ReferenceTypeId,
                        ReferenceId: objIntervieweeCandidateMaster.ReferenceId,
                        ReferenceSubType: objIntervieweeCandidateMaster.ReferenceSubType,
                        ReferenceMannualEntry: objIntervieweeCandidateMaster.ReferenceMannualEntry,
                        ReferanceData: { Display: objIntervieweeCandidateMaster.ReferenceMannualEntry, Value: '' },
                        AgencyTypeReferanceData: { Display: objIntervieweeCandidateMaster.AgencyTypeReferanceName, Value: objIntervieweeCandidateMaster.ReferenceId },
                        CityId: objIntervieweeCandidateMaster.CityId,
                        CityName: objIntervieweeCandidateMaster.CityName,
                        StateId: objIntervieweeCandidateMaster.StateId,
                        StateName: objIntervieweeCandidateMaster.StateName,
                        CountryId: objIntervieweeCandidateMaster.CountryId,
                        CountryName: objIntervieweeCandidateMaster.CountryName,
                        QualificationId: objIntervieweeCandidateMaster.QualificationId,
                        QualificationName: objIntervieweeCandidateMaster.QualificationName,
                        Address: objIntervieweeCandidateMaster.Address,
                        Pincode: objIntervieweeCandidateMaster.Pincode,
                        MobileNo: objIntervieweeCandidateMaster.MobileNo,
                        Email: objIntervieweeCandidateMaster.Email,
                        CommunicateDate: $filter('mydate')(objIntervieweeCandidateMaster.CommunicateDate),
                        FirstName: objIntervieweeCandidateMaster.FirstName,
                        Surname: objIntervieweeCandidateMaster.SurName,
                        Gender: objIntervieweeCandidateMaster.Gender.toString(),
                        MaritalStatus: objIntervieweeCandidateMaster.MaritalStatus.toString(),
                        Birthdate: $filter('mydate')(objIntervieweeCandidateMaster.Birthdate),
                        TotalWorkExperience: objIntervieweeCandidateMaster.TotalWorkExperience,
                        CurrentCTC: objIntervieweeCandidateMaster.CurrentCTC,
                        CurrentExpected: objIntervieweeCandidateMaster.CurrentExpected,
                        NoticePeriod: objIntervieweeCandidateMaster.NoticePeriod,
                        UploadResume: objIntervieweeCandidateMaster.UploadResume,
                        CandidateRefno: objIntervieweeCandidateMaster.CandidateRefno,
                        SourceData: { Display: objIntervieweeCandidateMaster.Source, Value: objIntervieweeCandidateMaster.SourceId },
                        CountryData: { Display: objIntervieweeCandidateMaster.CountryName, Value: objIntervieweeCandidateMaster.CountryId },
                        StateData: { Display: objIntervieweeCandidateMaster.StateName, Value: objIntervieweeCandidateMaster.StateId },
                        CityData: { Display: objIntervieweeCandidateMaster.CityName, Value: objIntervieweeCandidateMaster.CityId },
                        EducationData: { Display: objIntervieweeCandidateMaster.QualificationName, Value: objIntervieweeCandidateMaster.QualificationId },
                        TotalExpeYear: (objIntervieweeCandidateMaster.TotalWorkExperience != null) ? objIntervieweeCandidateMaster.TotalWorkExperience.split('.')[0] : '',
                        TotalExpeMonth: (objIntervieweeCandidateMaster.TotalWorkExperience != null) ? objIntervieweeCandidateMaster.TotalWorkExperience.split('.')[1] : ''
                    };
                    var val = '';
                    if (objIntervieweeCandidateMaster.ReferenceTypeId == 1) { val = "BuyerMaster"; }
                    else if (objIntervieweeCandidateMaster.ReferenceTypeId == 2) { val = "UserMaster" }
                    else if (objIntervieweeCandidateMaster.ReferenceTypeId == 3) { val = "Other" }
                    $scope.showHide(val);

                    $scope.objIntervieweeCandidate.ReferanceData.Display = objIntervieweeCandidateMaster.ReferenceMannualEntry;
                    $scope.objIntervieweeCandidate.ReferenceMannualEntry = objIntervieweeCandidateMaster.ReferenceMannualEntry;
                    $scope.storage = angular.copy($scope.objIntervieweeCandidate);
                    $scope.tempImagePath = "/UploadImages/ResumeFile/" + objIntervieweeCandidateMaster.UploadResume;

                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            })
        }

        $scope.Edit = function (id) {
            window.location.href = "/Employee/IntervieweeCandidate/AddIntervieweeCandidate/" + id + "/" + 0;
        }
        $scope.View = function (id) {
            window.location.href = "/Employee/IntervieweeCandidate/AddIntervieweeCandidate/" + id + "/" + 1;
        }
        $scope.Delete = function (id) {
            IntervieweeCandidateService.Delete(id).then(function (result) {
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

        function ResetForm() {
            $scope.objIntervieweeCandidate = {
                IntCandId: 0,
                ReferenceTypeId: 0,
                ReferenceId: 0,
                ReferenceSubType: 0,
                ReferenceMannualEntry: '',
                CityId: 0,
                CityName: '',
                StateId: 0,
                StateName: '',
                CountryId: 0,
                CountryName: '',
                QualificationId: 0,
                QualificationName: '',
                Address: '',
                Pincode: '',
                MobileNo: '',
                Email: '',
                CommunicateDate: new Date(),
                FirstName: '',
                Surname: '',
                Gender: '',
                MaritalStatus: '',
                Birthdate: '',
                TotalWorkExperience: '',
                CurrentCTC: 0,
                CurrentExpected: 0,
                NoticePeriod: '',
                UploadResume: '',
                CountryData: { Display: '', Value: '' },
                StateData: { Display: '', Value: '' },
                CityData: { Display: '', Value: '' },
                EducationData: { Display: '', Value: '' },
                ReferanceData: { Display: '', Value: '' },
                SourceData: { Display: '', Value: '' },
                TotalExpeYear: 0,
                TotalExpeMonth: 0,
                SourceId: '',
                Source: '',
                CandidateRefno: '',
                Chat: '',
            };
            $scope.chat = '';
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.FormIntervieweeCandidate)
                $scope.FormIntervieweeCandidate.$setPristine();
        }

        $scope.CreateUpdate = function (data) {
            if (data.UploadResume) {
                data.CityId = data.CityData.Value;
                data.CityName = data.CityData.Display;
                data.StateId = data.StateData.Value;
                data.StateName = data.StateData.Display;
                data.CountryId = data.CountryData.Value;
                data.CountryName = data.CountryData.Display;
                data.QualificationId = data.EducationData.Value;
                data.QualificationName = data.EducationData.Display;
                data.SourceId = data.SourceData.Value;
                data.Source = data.SourceData.Display;
                data.Chat = this.chat.toString();
                // data.TotalWorkExperience = parseFloat(data.TotalExpeYear + '.' + data.TotalExpeMonth);
                data.TotalWorkExperience = data.TotalExpeYear.toString() + '.' + data.TotalExpeMonth.toString();

                data.MobileNo = this.teliphone.toString();
                data.Email = this.mail.toString();

                if (data.ReferenceTypeId == 1 || data.ReferenceTypeId == 2) {
                    //data.ReferenceId = data.ReferanceData.Value
                    data.ReferenceMannualEntry = data.ReferanceData.Display
                }
                else if (data.ReferenceTypeId == 3) {
                    //data.ReferenceId = data.AgencyTypeReferanceData.Value
                    data.ReferenceMannualEntry = data.ReferenceMannualEntry
                }

                if (data.UploadResume != undefined) {
                    var dataPOD = data.UploadResume.split('/');
                    //data.UploadResume = dataPOD[data.UploadResume.length - 1];
                }

                IntervieweeCandidateService.CreateUpdate(data).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        //$uibModalInstance.close();
                        toastr.success(result.data.Message);
                        window.location.href = "/Employee/IntervieweeCandidate";
                        //IntervieweeCandidateCtrl.refreshGrid();
                    } else {
                        toastr.error(result.data.Message, 'Opps, Something went wrong');
                    }
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            } else {
                toastr.error('Please Upload Resume.');
            }
        }

        $scope.uploadImgFile = function (file) {
            $scope.tempImagePath = '';
            $scope.objIntervieweeCandidate.POD = '';
            Upload.upload({
                url: "/Handler/FileUpload.ashx",
                method: 'POST',
                file: file,
            }).then(function (result) {
                var ext = result.config.file[0].name.split('.').pop();
                if (ext == "docx" || ext == "doc" || ext == "pdf" || ext == "jpg" || ext == "png") {
                    //if (result.config.file[0].type.match('application/pdf')) {
                    if (result.status == 200) {
                        if (result.data.length > 0) {
                            $scope.objIntervieweeCandidate.UploadResume = result.data[0].imageName;
                            $scope.tempImagePath = result.data[0].imagePath;
                        }
                    }
                    else {
                        $scope.objIntervieweeCandidate.POD = '';
                    }
                } else {
                    toastr.error("Only Word, Pdf, Jpg, Png File Allowed.", "Error");
                }
            });


        }
        $scope.Reset = function () {
            if ($scope.objIntervieweeCandidate.CourierId > 0) {
                $scope.objIntervieweeCandidate = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

        $scope.dateOptions = {
            formatYear: 'dd-MM-yyyy',
            minDate: new Date(1950, 8, 5),
            //maxDate: new Date(),
            startingDay: 1
        };

    }
})()



