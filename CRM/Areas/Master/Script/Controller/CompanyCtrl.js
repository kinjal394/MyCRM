(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("CompanyCtrl", [
               "$scope", "CompanyService", "$filter", "$uibModal",
                function CompanyCtrl($scope, CompanyService, $filter, $uibModal) {
                    $scope.storage = {};
                    $scope.MobCodeData = [];
                    $scope.AddCompany = function () {
                        var _isdisable = 0;
                        $scope.Companyobj = {
                            ComId: 0,
                            ComName: "",
                            RegOffAdd: "",
                            CorpOffAdd: "",
                            TelNos: "",
                            Email: "",
                            Web: "",
                            ComCode: "",
                            TaxDetails: ""
                        }
                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'companyModalContent.html',
                            controller: 'CompanyModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                Companyinfo: function () {
                                    return $scope.Companyobj;
                                },
                                storage: function () {
                                    return $scope.storage;
                                },
                                isdisable: function () { return _isdisable; }
                            }
                        });

                        modalInstance.result.then(function () {
                            // $ctrl.selected = selectedItem;
                            $scope.refreshGrid();
                        }, function () {
                            // $log.info('Modal dismissed at: ' + new Date());
                        });
                    }

                    $scope.setDirectiveRefresh = function (refreshGrid) {
                        $scope.refreshGrid = refreshGrid;
                    };

                    $scope.gridObj = {
                        columnsInfo: [
                           //{ "title": "Company Id", "data": "ComId", filter: false, visible: false },
                           { "title": "Sr.", "field": "RowNumber", show: true, },
                           { "title": "Company Code", "field": "ComCode", sortable: "ComCode", filter: { ComCode: "text" }, show: true, },
                           { "title": "Company Name", "field": "ComName", sortable: "ComName", filter: { ComName: "text" }, show: true, },
                           //{ "title": "Register Office Address", "data": "RegOffAdd", sort: true, filter: true, },
                           {
                               "title": "Register Office", "field": "RegOffAdd", sortable: "RegOffAdd", filter: { RegOffAdd: "text" }, show: true,
                               'cellTemplte': function ($scope, row) {
                                   var element = '<p data-uib-tooltip="{{row.RegOffAdd}}" ng-bind="LimitString(row.RegOffAdd,100)">'
                                   return $scope.getHtml(element);
                               }
                           },
                           //{ "title": "Corporate Office Address", "data": "CorpOffAdd", sort: true, filter: true, },
                           {
                               "title": "Corporate Office", "field": "CorpOffAdd", sortable: "CorpOffAdd", filter: { CorpOffAdd: "text" }, show: true,
                               'cellTemplte': function ($scope, row) {
                                   var element = '<p data-uib-tooltip="{{row.CorpOffAdd}}" ng-bind="LimitString(row.CorpOffAdd,100)">'
                                   return $scope.getHtml(element);
                               }
                           },
                           { "title": "Tel", "field": "TelNos", sortable: "TelNos", filter: { TelNos: "text" }, show: true, },
                           { "title": "Email", "field": "Email", sortable: "Email", filter: { Email: "text" }, show: true, },
                           { "title": "Web", "field": "Web", sortable: "Web", filter: { Web: "text" }, show: true, },
                           {
                               "title": "Company Logo", "field": "ComLogo", show: true, inputType: "text",
                               "cellTemplte": function ($scope, row) {
                                   return $scope.getHtml('<img src="/UploadImages/Companylogo/' + row.ComLogo + '" style="width:50px;height:40px;" />');
                               }
                           },
                           {
                               "title": "Action", show: true,
                               'cellTemplte': function ($scope, row) {
                                   var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                               //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.ComId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                               '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                                   return $scope.getHtml(element);
                               }
                           }
                        ],
                        Sort: { 'ComId': 'asc' }
                    }

                    $scope.Edit = function (data) {
                        debugger
                        var _isdisable = 0;
                        $scope.Companyobj = {
                            ComId: data.ComId,
                            ComCode: data.ComCode,
                            ComName: data.ComName,
                            RegOffAdd: data.RegOffAdd,
                            CorpOffAdd: data.CorpOffAdd,
                            TelNos: data.TelNos,
                            Email: data.Email,
                            Web: data.Web != null ? data.Web : '',
                            TaxDetails: data.TaxDetails ? data.TaxDetails : '',
                            Setlogo: "/UploadImages/Companylogo/" + data.ComLogo
                        }
                        $scope.storage = angular.copy($scope.Companyobj);
                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'companyModalContent.html',
                            controller: 'CompanyModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                Companyinfo: function () {
                                    return $scope.Companyobj;
                                },
                                storage: function () {
                                    return $scope.storage;
                                },
                                isdisable: function () { return _isdisable; }
                            }
                        });
                        modalInstance.result.then(function (selectedItem) {
                            $scope.refreshGrid();
                            $scope.tableParams.reload();
                        }, function () {
                            // $log.info('Modal dismissed at: ' + new Date());
                        });
                    }

                    $scope.View = function (data) {
                        var _isdisable = 1;
                        $scope.Companyobj = {
                            ComId: data.ComId,
                            ComCode: data.ComCode,
                            ComName: data.ComName,
                            RegOffAdd: data.RegOffAdd,
                            CorpOffAdd: data.CorpOffAdd,
                            TelNos: data.TelNos,
                            Email: data.Email,
                            Web: data.Web != null ? data.Web : '',
                            TaxDetails: data.TaxDetails ? data.TaxDetails : "",
                            Setlogo: "/UploadImages/Companylogo/" + data.ComLogo
                        }
                        $scope.storage = angular.copy($scope.Companyobj);
                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'companyModalContent.html',
                            controller: 'CompanyModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                Companyinfo: function () {
                                    return $scope.Companyobj;
                                },
                                storage: function () {
                                    return $scope.storage;
                                },
                                isdisable: function () { return _isdisable; }
                            }
                        });
                        modalInstance.result.then(function (selectedItem) {
                            $scope.refreshGrid();
                            $scope.tableParams.reload();
                        }, function () {
                            // $log.info('Modal dismissed at: ' + new Date());
                        });
                    }

                    $scope.Delete = function (comid) {
                        CompanyService.DeleteCompany(comid).then(function (result) {
                            if (result.data.ResponseType == 1) {
                                toastr.success(result.data.Message)
                            }
                            else {
                                toastr.error(result.data.Message)
                            }
                            $scope.refreshGrid();
                        }, function (errorMsg) {
                            toastr.error(errorMsg, 'Opps, Something went wrong');
                        })
                    }
                }]);

    angular.module('CRMApp.Controllers').controller('CompanyModalInstanceCtrl', ['$scope',
        '$uibModalInstance', 'CompanyService', 'Companyinfo', 'storage', 'isdisable', 'Upload', 'CountryService',
        function ($scope, $uibModalInstance, CompanyService, Companyinfo, storage, isdisable, Upload, CountryService) {
            $scope.MobCodeData = [];
            Companyinfo.TelNos = (Companyinfo.TelNos != '' && Companyinfo.TelNos != null) ? Companyinfo.TelNos.split(",") : [];
            Companyinfo.Email = (Companyinfo.Email != '' && Companyinfo.Email != null) ? Companyinfo.Email.split(",") : [];
            Companyinfo.Web = (Companyinfo.Web != '' && Companyinfo.Web != null) ? Companyinfo.Web.split(",") : [];

            $scope.setvalue = function () {
                CountryService.GetCountryFlag().then(function (result) {
                    $scope.MobCodeData = angular.copy(result);
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }

            $scope.isClicked = false;
            if (isdisable == 1) {
                $scope.isClicked = true;
            }

            var $ctrl = this;

            $ctrl.ok = function () {
                $uibModalInstance.close();
            };

            $ctrl.CompanyInfo = Companyinfo;
            $ctrl.storage = storage;

            $ctrl.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.ChechFileValid = function (file) {

                var isValid = false;
                // if ($scope.SelectedFileForUpload != null) {
                if ((file.type == 'image/png' || file.type == 'image/jpeg' || file.type == 'image/gif')) {
                    $scope.FileInvalidMessage = "";
                    isValid = true;
                }
                else {
                    $scope.FileInvalidMessage = "Selected file is Invalid. (only file type png, jpeg and gif and 512 kb size allowed)";
                }
                // }
                //else {
                //    $scope.FileInvalidMessage = "Image required!";
                //}
                $scope.IsFileValid = isValid;
            };

            $scope.selectFileforUpload = function (file) {
                $ctrl.CompanyInfo.Setlogo = '';
                Upload.upload({
                    url: "/Handler/FileUpload.ashx",
                    method: 'POST',
                    file: file,
                }).then(function (result) {
                    if (result.status == 200) {
                        if (result.data.length > 0) {
                            $ctrl.CompanyInfo.Setlogo = result.data[0].imageName;
                            $scope.tempImagePath = result.data[0].imagePath;
                            $scope.SelectedFileForUpload = file[0];
                        }
                    }
                    else {
                        $ctrl.CompanyInfo.Setlogo = '';
                    }
                });
                //$scope.objcountry.Setlogo = file[0].name;
            }

            $ctrl.saveComapny = function (company) {
                $scope.Message = "";
                if ($scope.SelectedFileForUpload == undefined) {
                    $scope.SelectedFileForUpload = null;
                }
                company.TelNos = $ctrl.CompanyInfo.TelNos != undefined ? $ctrl.CompanyInfo.TelNos.toString() : "";
                CompanyService.addCompany(company, $scope.SelectedFileForUpload).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        $uibModalInstance.close();
                        toastr.success(result.data.Message)
                        $scope.Companyobj = {
                            ComId: 0,
                            ComName: "",
                            RegOffAdd: "",
                            CropOffAdd: "",
                            TelNos: "",
                            Email: "",
                            IsActive: "",
                            Web: "",
                            TaxDetails: "",
                            ComCode: "",
                            Setlogo: "",
                        }
                        $ctrl.CompanyInfo.$setPristine();
                        $ctrl.CompanyInfo.$setUntouched();
                    }
                    else {
                        toastr.error(result.data.Message)
                    }
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }

            $ctrl.Update = function (Comapny) {
                $scope.Message = "";
                // $scope.ChechFileValid($scope.SelectedFileForUpload);
                CompanyService.Update(Comapny, $scope.SelectedFileForUpload).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        $uibModalInstance.close();
                        toastr.success(result.data.Message)
                    }
                    else {
                        toastr.error(result.data.Message)
                    }
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }

            $ctrl.Reset = function () {

                if ($ctrl.CompanyInfo.ComId > 0) {

                    $ctrl.CompanyInfo = angular.copy($ctrl.storage);

                } else {
                    ResetForm();
                }
            }

            function ResetForm() {

                $ctrl.CompanyInfo = {
                    ComId: 0,
                    ComName: "",
                    RegOffAdd: "",
                    CropOffAdd: "",
                    TelNos: "",
                    Email: "",
                    Web: "",
                    TaxDetails: "",
                    ComCode: ""
                }
                $ctrl.storage = {};
                $ctrl.addMode = true;
                $ctrl.saveText = "Save";
                if ($ctrl.$parent.ComapnyInfoId)
                    $ctrl.$parent.ComapnyInfoId.$setPristine();
            }

        }]);
})();