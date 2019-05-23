(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("PromotionletterCtrl", [
         "$scope", "PromotionletterService", "$uibModal", "$filter", "CountryService",
         PromotionletterCtrl]);

    function PromotionletterCtrl($scope, PromotionletterService, $uibModal, $filter, CountryService) {
        templateUrl: 'speechModalContent.html';
        $scope.telCodeData = [];
        $scope.MobCodeData = [];
        $scope.id = 0;
        $scope.objPromotion = {
            Salary: '',
            offerDate: '',
            joiningDate: '',
            Web: '',
            CompanyEmail: '',
            Comteliphone: '',
            CorpOffAdd: '',
            RegOffAdd: '',
            ComLogo: '',
            CurrentDesignationData: { Display: '', Value: '' },
            NextDesignationData: { Display: '', Value: '' },
            Departmentdata: { Display: '', Value: '' },
            companydata: { Display: '', Value: '' },
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

        $scope.$watch('objPromotion.Userdata', function (val) {
            if (val) {
                if (val.Value && val.Value > 0) {
                    PromotionletterService.GetUserById(val.Value).then(function (result) {
                        debugger
                        if (result.data.ResponseType == 1) {
                            $scope.objPromotion.Departmentdata = { Display: result.data.DataList.UserDetail[0].DepartmentName, Value: result.data.DataList.UserDetail[0].DepartmentId };
                            $scope.objPromotion.CurrentDesignationData = { Display: result.data.DataList.UserDetail[0].DesignationName, Value: result.data.DataList.UserDetail[0].DesignationId };
                        } else if (result.ResponseType == 3) {
                            toastr.error(result.data.Message, 'Opps, Something went wrong');
                        }
                    }, function (error) {
                        $rootScope.errorHandler(error)
                    })
                }
            }
        });

        $scope.$watch('objPromotion.companydata', function (val) {
            if (val) {
                if (val.Value && val.Value > 0) {
                    PromotionletterService.GetCompanydataById(val.Value).then(function (result) {
                        if (result.data.ResponseType == 1) {

                            $scope.Comteliphone = (result.data.DataList.TelNos != '') ? result.data.DataList.TelNos.split(",") : [];
                            $scope.objPromotion.Comteliphone = $scope.Comteliphone.toString();
                            $scope.CompanyEmail = (result.data.DataList.Email != '') ? result.data.DataList.Email.split(",") : [];
                            $scope.objPromotion.CompanyEmail = $scope.CompanyEmail.toString();
                            $scope.objPromotion.Web = result.data.DataList.Web;
                            $scope.objPromotion.RegOffAdd = result.data.DataList.RegOffAdd;
                            $scope.objPromotion.CorpOffAdd = result.data.DataList.CorpOffAdd;
                            if (result.data.DataList.ComLogo != null)
                                $scope.ComLogo = '/UploadImages/Companylogo/' + result.data.DataList.ComLogo;
                            else
                                $scope.ComLogo = '';

                        } else if (result.ResponseType == 3) {
                            toastr.error(result.data.Message, 'Opps, Something went wrong');
                        }
                    }, function (error) {
                        $rootScope.errorHandler(error)
                    })
                }
            }
        });


        $scope.SetPromotionLetter = function (data) {
            var obj = {
                companyName: data.companydata.Display,
                CorpOffAdd: data.CorpOffAdd,
                RegOffAdd: data.RegOffAdd,
                ComLogo: $scope.ComLogo,
                MobileNo: this.Comteliphone != undefined ? this.Comteliphone.toString() : "",//company
                Email: this.CompanyEmail != undefined ? this.CompanyEmail.toString() : "",//company
                Web: data.Web,
                NextDesignation: data.NextDesignationData.Display,
                CurrentDesignation: data.CurrentDesignationData.Display,
                Username: data.Userdata.Display,
                DepartmentName: data.Departmentdata.Display,
                joiningDate: moment(data.joiningDate).format("DD-MM-YYYY"),
                offerDate: moment(data.offerDate).format("DD-MM-YYYY"),
                Salary: data.Salary,

            }
            $scope.Print(obj);
            ResetFrom();
        }
        function ResetFrom() {
            $scope.objPromotion = {
                Salary: '',
                offerDate: '',
                joiningDate: '',
                Web: '',
                CompanyEmail: '',
                Comteliphone: '',
                CorpOffAdd: '',
                RegOffAdd: '',
                ComLogo: '',
                CurrentDesignationData: { Display: '', Value: '' },
                NextDesignationData: { Display: '', Value: '' },
                Departmentdata: { Display: '', Value: '' },
                companydata: { Display: '', Value: '' },
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
            PromotionletterService.SetLetter(data).then(function (result) {

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
                    doc.save('Promotionletter.pdf')
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            })
        }



    }
})();