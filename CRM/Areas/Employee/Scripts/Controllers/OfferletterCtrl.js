(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("OfferletterCtrl", [
         "$scope", "OfferletterService", "$uibModal", "$filter", "CountryService",
         OfferletterCtrl]);

    function OfferletterCtrl($scope, OfferletterService, $uibModal, $filter, CountryService) {
       // templateUrl: 'speechModalContent.html';
        $scope.telCodeData = [];
        $scope.MobCodeData = [];
        $scope.id = 0;
        $scope.objofferLetter = {
            Currentoffer: '',
            mail: '',
            Address: '',
            RefNo: '',
            offerDate: '',
            joiningDate: '',
            Web: '',
            CompanyEmail: '',
            Comteliphone: '',
            CorpOffAdd: '',
            RegOffAdd: '',
            ComLogo:'',
            DesignationData: { Display: '', Value: '' },
            companydata: { Display: '', Value: '' },
            Candidatedata: { Display: '', Value: '' },
            BuyerData: { Display: '', Value: '' },
            ContactPersondata: { Display: '', Value: '' },
            BuyerAddressData: { Display: '', Value: '' },
        }
        $scope.dateOptions = {
            formatYear: 'yy',
            minDate: new Date(1950, 1, 1),
            startingDay: 1
        }
        $scope.setvalue = function () {
            CountryService.GetCountryFlag().then(function (result) {
                $scope.telCodeData = angular.copy(result);
                $scope.MobCodeData = angular.copy(result);
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }
        $scope.$watch('objofferLetter.Candidatedata', function (val) {
            if (val) {
                if (val.Value && val.Value > 0) {
                    OfferletterService.FetchAllInfoById(val.Value).then(function (result) {
                        debugger
                        if (result.data.ResponseType == 1) {
                            $scope.objofferLetter.RefNo = result.data.DataList.objInterviweeCandidate.CandidateRefNo;
                            $scope.objofferLetter.Address = result.data.DataList.objInterviweeCandidate.Address;
                            $scope.mail = (result.data.DataList.objInterviweeCandidate.Email != '') ? result.data.DataList.objInterviweeCandidate.Email.split(",") : [];
                            $scope.objofferLetter.mail = $scope.mail.toString();

                        } else if (result.ResponseType == 3) {
                            toastr.error(result.data.Message, 'Opps, Something went wrong');
                        }
                    }, function (error) {
                        $rootScope.errorHandler(error)
                    })
                }
            }
        });

        $scope.$watch('objofferLetter.companydata', function (val) {
            if (val) {
                if (val.Value && val.Value > 0) {
                    OfferletterService.GetCompanydataById(val.Value).then(function (result) {
                        if (result.data.ResponseType == 1) {
                            //$scope.objofferLetter.companyAddress = result.data.DataList.CorpOffAdd;
                            // $scope.objofferLetter.TelePhoneNO = result.data.DataList.TelNos;
                            $scope.Comteliphone = (result.data.DataList.TelNos != '') ? result.data.DataList.TelNos.split(",") : [];
                            $scope.objofferLetter.Comteliphone = $scope.Comteliphone.toString();
                            $scope.CompanyEmail = (result.data.DataList.Email != '') ? result.data.DataList.Email.split(",") : [];
                            $scope.objofferLetter.CompanyEmail = $scope.CompanyEmail.toString();
                            $scope.objofferLetter.Web = result.data.DataList.Web;
                            $scope.objofferLetter.RegOffAdd = result.data.DataList.RegOffAdd;
                            $scope.objofferLetter.CorpOffAdd = result.data.DataList.CorpOffAdd;
                            if (result.data.DataList.ComLogo != null)
                                $scope.ComLogo = '/UploadImages/Companylogo/' + result.data.DataList.ComLogo;
                            else
                                $scope.ComLogo = '';
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


        $scope.SetOfferLetter = function (data) {
           // data.MobileNo = this.Comteliphone != undefined ? this.Comteliphone.toString() : "";
            //data.Email = this.CompanyEmail != undefined ? this.CompanyEmail.toString() : "";
            var obj = {
                companyName: data.companydata.Display,
                CorpOffAdd: data.CorpOffAdd,
                RegOffAdd: data.RegOffAdd,
                ComLogo: $scope.ComLogo,
                MobileNo: this.Comteliphone != undefined ? this.Comteliphone.toString() : "",//company
                Email: this.CompanyEmail != undefined ? this.CompanyEmail.toString() : "",//company
                Web: data.Web,
                Designation: data.DesignationData.Display,
                Candidatename: data.Candidatedata.Display,
                RefNo: data.RefNo,
                Address: data.Address,
                Salary: data.Currentoffer,
                Mail: this.mail != undefined ? this.mail.toString() : "",
                joiningDate : moment(data.joiningDate).format("DD-MM-YYYY"),
                offerDate : moment(data.offerDate).format("DD-MM-YYYY"),
            }
            $scope.Print(obj);
            ResetFrom();
        }
        function ResetFrom() {
            $scope.objofferLetter = {
                Currentoffer: '',
                mail: '',
                Address: '',
                RefNo: '',
                offerDate: '',
                joiningDate: '',
                Web: '',
                CompanyEmail: '',
                Comteliphone: '',
                CorpOffAdd: '',
                RegOffAdd: '',
                ComLogo: '',
                DesignationData: { Display: '', Value: '' },
                companydata: { Display: '', Value: '' },
                Candidatedata: { Display: '', Value: '' },
                BuyerData: { Display: '', Value: '' },
                ContactPersondata: { Display: '', Value: '' },
                BuyerAddressData: { Display: '', Value: '' },
            }
            $scope.teliphone = '';
            $scope.mail = '';
            $scope.Email = '';
            $scope.MobCode = '';
            $scope.descriptiondata = '';
            $scope.Comteliphone = '';
            $scope.CompanyEmail = '';
        }

        $scope.Print = function (data) {
            OfferletterService.SetLetter(data).then(function (result) {

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



    }
})();