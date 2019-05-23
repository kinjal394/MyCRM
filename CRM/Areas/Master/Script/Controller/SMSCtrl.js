(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
    .controller("SMSCtrl", [
      "$scope", "$rootScope", "$timeout", "$filter", "SMSService", "CountryService", "Upload", SMSCtrl
    ]);

    function SMSCtrl($scope, $rootScope, $timeout, $filter, SMSService, CountryService, Upload) {

        $scope.MobCodeData = [];

        $scope.setvalue = function () {
            CountryService.GetCountryFlag().then(function (result) {
                $scope.MobCodeData = angular.copy(result);
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }
        $scope.objsms = {
            mobile: '',
            SMS:''
        }

        function ResetForm() {
            $scope.objsms = {
                mobile: '',
                SMS:'',
            };
            $scope.mobile = '';
        }

        $scope.CreateUpdate = function (data) {
            $scope.submitted = false;
            data.mobile = $scope.mobile != undefined ? this.mobile.toString() : "";
            //data.mobile = this.mobile.toString();
            //data.SMS = data.SMS.toString();
            SMSService.SetSMSData(data).then(function (result) {
                toastr.success(result.data.Message);
                ResetForm();
               // window.location.href = "/master/Dashboard/Dashboard";
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
            //}
        }
    }


})()















































