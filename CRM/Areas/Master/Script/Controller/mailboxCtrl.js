(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
    .controller("mailboxCtrl", [
      "$scope", "$rootScope", "$timeout", "$filter", "EmailSpeechService", "EmailSignatureService", "BuyerService", "mailboxService", "Upload", mailboxCtrl
    ]);

    function mailboxCtrl($scope, $rootScope, $timeout, $filter, EmailSpeechService, EmailSignatureService, BuyerService, mailboxService, Upload) {

        $scope.objEmail = $scope.objEmail || {};
        $scope.CompanyArray = [];

        $scope.objEmail = {
            BuyerId: 0,
            companyName: '',
            Subject: '',
            Emailspeechdata: { Display: '', Value: '' },
            description: '',
            Signaturedata: { Display: '', Value: '' },
            Signature: '',
            EmailId: '',
            SenderType: '',
            Body: '',
            Sign: '',
            SenderEmail: '',
            BOdescription: '',
            OTSignature: '',

        };
        getdata();
        function getdata() {
            BuyerService.getallbuyer().then(function (result) {
                var res;
                //if (objEmail.BuyerId != undefined)
                res = $scope.objEmail.BuyerId;
                _.each(result.data.DataList, function (value, key, list) {
                    if (value.BuyerId == res) {
                        $scope.CompanyArray.push({
                            id: value.BuyerId,
                            name: value.CompanyName,
                            ticked: true
                        })
                    }
                    else {
                        $scope.CompanyArray.push({
                            id: value.BuyerId,
                            name: value.CompanyName,
                            ticked: false
                        })
                    }
                })
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.SetValue = function (value, type) {

            //$scope.IsVisible = false;
            if (type == 'B') {
                $scope.IsVisible1 = true;
            }
            $scope.SenderMode = value;
            $scope.objEmail.SenderType = type;
            $scope.objEmail.SenderData = {
                Display: "",
                Value: ""
            };
        }

        $scope.set = function (value, type) {
            if (type == 'T') {
                $scope.EmailspeechMode = value;
                $scope.objEmail.Body = type;
                $scope.objEmail.Emailspeechdata = {
                    Display: "",
                    Value: ""
                }
            } else if (type == 'BO') {
                $scope.description = '';
            }
        }
        $scope.setsigna = function (value, type) {
            if (type == 'SI') {
                $scope.SignatureMode = value;
                $scope.objEmail.Sign = type;
                $scope.objEmail.Signaturedata = {
                    Display: "",
                    Value: ""
                }
            } else if (type == 'OT') {
                $scope.Signature = '';
            }

        }

        $scope.multiSelectClick = function (data) {
            var newVal = data;
            BuyerService.getallbuyerEmail(newVal.id).then(function (result) {
                if (result.data.DataList[0].Email != null) {
                    if ($scope.objEmail.EmailId != "") {
                        $scope.objEmail.EmailId += "," + result.data.DataList[0].Email;
                    } else {
                        $scope.objEmail.EmailId = result.data.DataList[0].Email;
                    }
                    return false;
                }
            })
        }

        $scope.multiSelectSelectAll = function () {
            var data = $scope.CompanyArray;
            for (var i = 0; i < data.length; i++) {
                BuyerService.getallbuyerEmail(data[i].id).then(function (result) {
                    if (result.data.DataList[0].Email != null) {
                        if ($scope.objEmail.EmailId != "") {
                            $scope.objEmail.EmailId += "," + result.data.DataList[0].Email;
                        } else {
                            $scope.objEmail.EmailId = result.data.DataList[0].Email;
                        }
                        return false;
                    }
                })
            }
        }

        $scope.multiSelectSelectAllNone = function () {
            $scope.objEmail.EmailId = "";
        }

        $scope.$watch('objEmail.Emailspeechdata.Value', function (newVal) {
            if (newVal && newVal > 0) {
                EmailSpeechService.GetSpeechById($scope.objEmail.Emailspeechdata.Value).then(function (data) {
                    $scope.objEmail.description = data.data.DataList.Description;
                    $scope.objEmail.Subject = data.data.DataList.Subject;
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }
        });

        $scope.$watch('objEmail.Signaturedata.Value', function (newVal) {
            if (newVal && newVal > 0) {
                EmailSignatureService.GetSignatureById($scope.objEmail.Signaturedata.Value).then(function (data) {
                    $scope.objEmail.Signature = data.data.DataList.Signature;
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }

        });
        //$scope.$watch('objEmail.CompanyName', function (newVal) {
        //    //if (newVal && newVal == 0) {
        //    //$scope.objEmail.EmailId = '';
        //    //$.each(newVal, function (index, element) {
        //    for (var i = 0; i < newVal.length; i++) {
        //        BuyerService.getallbuyerEmail(newVal[i].id).then(function (result) {
        //            if (result.data.DataList[0].Email != null) {
        //                $scope.objEmail.EmailId += result.data.DataList[0].Email + ",";
        //                return false;
        //            }
        //        })
        //    }
        //    //});
        //});

        $scope.CreateUpdate = function (data) {
            var dataerror = true;
            if (data.SenderType == '' || data.SenderType == null) {
                toastr.error("Contact/Other is required.");
                dataerror = false;
                return false;
            }
            else if (data.CompanyName == '' && data.SenderType == 'B') {
                toastr.error("Sender is required.");
                dataerror = false;
                return false;
            }
            else if (data.SenderEmail == '' && data.SenderType == 'O') {
                toastr.error("Sender is required.");
                dataerror = false;
                return false;
            }
            else if (data.Subject == '' || data.Subject == null) {
                toastr.error("Subject is required.");
                dataerror = false;
                return false;
            }
            else if (data.Body == '' || data.Body == null) {
                toastr.error("List/New is required.");
                dataerror = false;
                return false;
            }
            else if (data.Emailspeechdata.Display == '' && data.Body == 'T') {
                toastr.error("Source Name is required.");
                dataerror = false;
                return false;
            }
            else if (data.description == '' && data.Body == 'T') {
                toastr.error("Body Text is required.");
                dataerror = false;
                return false;
            }
            else if (data.BOdescription == '' && data.Body == 'BO') {
                toastr.error("Body Text is required.");
                dataerror = false;
                return false;
            }
            else if (data.Sign == '' || data.Sign == null) {
                toastr.error("List/New is required.");
                dataerror = false;
                return false;
            }
            else if (data.Signaturedata.Display == '' && data.Sign == 'SI') {
                toastr.error("Source Name is required.");
                dataerror = false;
                return false;
            }
            else if (data.Signature == '' && data.Sign == 'SI') {
                toastr.error("Signature is required.");
                dataerror = false;
                return false;
            }
            else if (data.OTSignature == '' && data.Sign == 'OT') {
                toastr.error("Signature is required.");
                dataerror = false;
                return false;
            }
            data.Emailspeech = data.Emailspeechdata.Display;
            data.EmailSignature = data.Signaturedata.Display;
            var CompanyNamelength = data.CompanyName.length;
            data.CompanyIds = '';
            _.each(data.CompanyName, function (value, key, list) {
                if (key < CompanyNamelength - 1) {
                    if (data.CompanyIds == '') {
                        data.CompanyIds = value.name + '|';
                    } else {
                        data.CompanyIds += value.name + '|';
                    }
                }
                else if (key == CompanyNamelength - 1) {
                    data.CompanyIds = value.name;
                }

            })
            mailboxService.CreateUpdate(data).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    ResetForm();
                } else {
                    toastr.error(result.data.Message);
                }

            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            });
        }
        function ResetForm() {
            $scope.objEmail = {
                BuyerId: 0,
                companyName: '',
                Subject: '',
                Emailspeechdata: { Display: '', Value: '' },
                description: '',
                Signaturedata: { Display: '', Value: '' },
                Signature: '',
                EmailId: '',
                SenderType: '',
                Body: '',
                Sign: '',
                SenderEmail: '',
                BOdescription: '',
                OTSignature: '',
            };
        }
    }


})()