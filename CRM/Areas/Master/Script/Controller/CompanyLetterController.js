(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("CompanyLetterController", [
         "$scope", "CompanyLetterService", "$uibModal", "$filter", "CountryService",
         CompanyLetterController]);

    function CompanyLetterController($scope, CompanyLetterService, $uibModal, $filter, CountryService) {
        templateUrl: 'speechModalContent.html';
        $scope.telCodeData = [];
        $scope.MobCodeData = [];
        $scope.id = 0;
        $scope.objLetter = {
            LetterType: '',
            referenceno: '',
            joiningDate: '',
            offerDate: '',
            companyName: '',
            companyAddress: '',
            Email: '',
            TelePhoneNO: '',
            Web: '',
            UserName: '',
            Employeename: '',
            MobileNo: '',
            Mail: '',
            Candidatename: '',
            Name: '',
            Address: '',
            Pincode: '',
            State: '',
            Country: '',
            MobNo: '',
            EmailCode: '',
            TelNos: '',
            Designation: '',
            Currency: '',
            Salary: '',
            SalaryHead: '',
            description:'',
            //ShiftStartTime: '',
            //ShiftEndTime: '',
            //LunchStartTime: '',
            //LunchEndTime: ''
        }
        $scope.dateOptions = {
            formatYear: 'yy',
            minDate: new Date(1950, 1, 1),
            startingDay: 1
        }
        $scope.setvalue = function () {
            $scope.openTab("Click", "OfferLetter", undefined, 1);
            CountryService.GetCountryFlag().then(function (result) {
                $scope.telCodeData = angular.copy(result);
                $scope.MobCodeData = angular.copy(result);
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }
        $scope.$watch('objLetter.companydata', function (val) {
            if (val){
            if (val.Value && val.Value > 0) {
                CompanyLetterService.GetCompanydataById(val.Value).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        $scope.objLetter.companyAddress = result.data.DataList.CorpOffAdd;
                        $scope.objLetter.TelePhoneNO = result.data.DataList.TelNos;
                        $scope.objLetter.Emaildata = result.data.DataList.Email;
                        $scope.objLetter.Web = result.data.DataList.Web;
                        //$scope.objLetter.Country = result.data.DataList.Country;
                    } else if (result.ResponseType == 3) {
                        toastr.error(result.data.Message, 'Opps, Something went wrong');
                    }
                }, function (error) {
                    $rootScope.errorHandler(error)
                })
            }
            }
        });
        $scope.$watch('objLetter.employeedata', function (val) {
            if (val) {
                if (val.Value && val.Value > 0) {
                    CompanyLetterService.GetUserdataById(val.Value).then(function (result) {
                        if (result.data.ResponseType == 1) {
                            var objUserMaster = result.data.DataList.UserDetail;
                            $scope.MobCode = (objUserMaster.MobNo != '' && objUserMaster.MobNo != null) ? objUserMaster.MobNo.split(",") : [];
                            $scope.Email = (objUserMaster.Email != '' && objUserMaster.Email != null) ? objUserMaster.Email.split(",") : [];
                            //$scope.objLetter.Country = result.data.DataList.Country;
                        } else if (result.ResponseType == 3) {
                            toastr.error(result.data.Message, 'Opps, Something went wrong');
                        }
                    }, function (error) {
                        $rootScope.errorHandler(error)
                    })
                }
            }
        });

        //$scope.$watch('objLetter.Candidatedata', function (val) {
        $scope.getcandata = function (val) {
            if (val) {
                CompanyLetterService.GetInterviewDetailById(val).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        if (result.data.DataList == null) {
                            $scope.objLetter.Candidatename = '';
                            $scope.objLetter.Name = '';
                            $scope.objLetter.Address = '';
                            $scope.objLetter.Pincode = '';
                            $scope.objLetter.State = '';
                            $scope.objLetter.Country = '';
                            $scope.teliphone = '';
                            $scope.mail = '';
                        }
                        else{
                        $scope.objLetter.Name = result.data.DataList.FirstName;
                        $scope.objLetter.Address = result.data.DataList.Address;
                        $scope.objLetter.Pincode = result.data.DataList.Pincode;
                        $scope.objLetter.State = result.data.DataList.StateName;
                        $scope.objLetter.Country = result.data.DataList.CountryName;
                        $scope.teliphone = (result.data.DataList.MobileNo != '' && result.data.DataList.MobileNo != null) ? result.data.DataList.MobileNo.split(",") : [];
                        $scope.mail = (result.data.DataList.Email != '' && result.data.DataList.Email != null) ? result.data.DataList.Email.split(",") : [];
                        }
                        } else if (result.ResponseType == 3) {
                        toastr.error(result.data.Message, 'Opps, Something went wrong');
                    }
                }, function (error) {
                    $rootScope.errorHandler(error)
                })
            }
        }
        //})

        $scope.SetOfferLetter = function (data) {
            data.companyName = data.companydata.Display;
            data.Employeename = data.employeedata.Display;
            data.Currency = data.CurrencyData.Display;
            data.SalaryHead = data.SalaryHeadData.Display;
            data.MobNo = $scope.MobCode.toString();
            data.Mail = $scope.Email.toString();
            data.MobileNo = $scope.teliphone.toString();
            data.Email = $scope.mail.toString();
            data.description = $scope.descriptiondata.toString();
            $scope.Print(data);
            //ResetFrom();
        }
        function ResetFrom()
        {
            $scope.objLetter = {
                LetterType: '',
                referenceno: '',
                joiningDate: '',
                offerDate: '',
                companyName: { Display: '', Value: '' },
                companyAddress: '',
                Email: '',
                TelePhoneNO: '',
                Web: '',
                UserName: '',
                Employeename: { Display: '', Value: '' },
                MobileNo: '',
                Mail: '',
                Candidatename: '',
                Name: '',
                Address: '',
                Pincode: '',
                CountryId: 0,
                StateId: 0,
                State: '',
                Country: '',
                MobNo: '',
                EmailCode: '',
                TelNos: '',
                Designation:'',
                Currency: { Display: '', Value: '' },
                Salary: '',
                SalaryHead: { Display: '', Value: '' },
                description: ''
            }
            $scope.teliphone = '';
            $scope.mail = '';
            $scope.Email = '';
            $scope.MobCode = '';
            $scope.descriptiondata='';
        }

        $scope.Print = function (data) {
            CompanyLetterService.SetLetter(data).then(function (result) {

                if (result.data.ResponseType == 1) {
                    var htmlData = (result.data.Message);
                    var doc = new jsPDF();
                    var specialElementHandlers = {
                        '#editor': function (element, renderer) {
                            return true;
                        }
                    };
                    doc.fromHTML(htmlData, 10, 10, {
                        'width': 180,
                        'elementHandlers': specialElementHandlers
                    });
                    doc.save('OfferLetter.pdf')
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            })
        }
        $scope.setLetter = function () {
        }

        $scope.openTab = function (evt, tabName, data, id) {
            var bln = true;
            var dataerror = true;
            if (bln == true) {
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

    }
})();