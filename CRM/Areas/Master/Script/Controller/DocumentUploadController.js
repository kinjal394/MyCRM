(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("DocumentUploadController", [
         "$scope", "DocumentUploadService",
         DocumentUploadController]);

    function DocumentUploadController($scope, DocumentUploadService) {
        $scope.empdocobj = $scope.empdocobj || {};
        $scope.addMode = true;
        $scope.saveText = "Upload";
        $scope.UserName = { Display: '', Value: '' };
        $scope.DocName = { Display: '', Value: '' };
        $scope.objDoc = function () { return objDoc; };
        $scope.empdocobj = {
            EmpDocId: 0,
            EmpId: 0,
            DocId: 0,
            UserName: { Display: '', Value: '' },
            DocName: { Display: '', Value: '' },
        }
        $scope.storage = {};

        $scope.callback = function (data) {
            if ($scope.empdocobj.UserName.Value > 0) {
                $scope.bindgrid();
            }
        }
        $scope.bindgrid = function () {
            if ($scope.empdocobj.UserName.Value > 0) {
                var EmpId = $scope.empdocobj.UserName.Value;
                DocumentUploadService.bindgrid(EmpId).then(function (result) {
                    $scope.DocGridList = result.data;
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }
        }


        $scope.$watch("empdocobj.UserName", function (newval, oldval) {
            if (newval && newval != "") {
                $scope.bindgrid();

            }
        })
        function ResetForm() {
            $scope.empdocobj = {
                EmpDocId: 0,
                EmpId: 0,
                DocId: 0,
            }
            $('#docId').val('');

            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Upload";
            if ($scope.$parent.FormEmpDocumentUploadInfo)
                $scope.$parent.FormEmpDocumentUploadInfo.$setPristine();
            //$scope.FormCountryInfo.$setPristine();
            //$timeout(function () {
            //    $scope.isFirstFocus = true;
            //});
        }

        $scope.Create = function (data) {
            $scope.Message = "";
            $scope.ChechFileValid($scope.SelectedFileForUpload);
            if ($scope.IsFileValid) {
                data.EmpId = data.UserName.Value;
                data.DocId = data.DocName.Value;
                DocumentUploadService.AddDocumentUpload(data, $scope.SelectedFileForUpload).then(function (result) {
                    $scope.submitted = false;
                    toastr.success(result.data.Message);
                    ResetForm();
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }
            else {
                //$scope.Message = "Please Select File";
                toastr.error($scope.FileInvalidMessage);
                //ResetForm();
            }
        }

        $scope.ChechFileValid = function (file) {

            var isValid = false;
            if ($scope.SelectedFileForUpload != null) {
                if ((file.type == 'application/pdf')) {
                    if (file.size > "2048") {
                        $scope.FileInvalidMessage ="File Size is More Than 2MB.(only File Size Less then 2Mb allowed)";
                    } else {
                        $scope.FileInvalidMessage = "";
                        isValid = true;
                    }
                }
                else {
                    $scope.FileInvalidMessage = "Selected file is Invalid. (only file type .pdf allowed)";
                }
            }
            else {
                $scope.FileInvalidMessage = "File Upload required!";
            }
            $scope.IsFileValid = isValid;
        };

        $scope.selectFileforUpload = function (file) {
                $scope.SelectedFileForUpload = file[0];
        }

        $scope.Reset = function () {
            if ($scope.empdocobj.EmpDocId > 0) {
                $scope.empdocobj = angular.copy($scope.storage);
            } else {
                ResetForm();
            }

        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

        $scope.IsActive = function (data) {
        }

        $scope.RefreshTable = function () {
            $scope.tableParams.reload();
        };
    }
})()

