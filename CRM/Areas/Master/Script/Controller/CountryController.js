(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("CountryController", [
         "$scope", "CountryService", "$uibModal", "$compile",
         CountryController]);

    function CountryController($scope, CountryService, $uibModal, $compile) {

        $scope.Add = function (id, _isdisable) {
            if (_isdisable === undefined) _isdisable = 0;
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    CountryService: function () { return CountryService; },
                    CountryController: function () { return CountryController; },
                    id: function () { return id; },
                    isdisable: function () { return _isdisable; }
                }
            });

            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () { });
        }
        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };
        $scope.setpermission = function (add, edit, deletee, view) {
            $scope.isadd = add == "True" ? true : false;
            $scope.editt = edit == "True" ? true : false;
            $scope.vieww = view == "True" ? true : false;
            $scope.gridObj = {
                columnsInfo: [
                    //{ "title": "Country Id", "field": "CountryId", filter: false, show: false },
                    { "title": "Sr.", "field": "RowNumber", filter: false, show: true },
                    { "title": "Country Name", "field": "CountryName", sortable: "CountryName", filter: { CountryName: "text" }, show: true },
                    {
                        "title": "Flag", "field": "CountryFlag", show: true, inputType: "text",
                        "cellTemplte": function ($scope, row) {
                            return $scope.getHtml('<img src="/Content/lib/CountryFlags/flags/' + row.CountryFlag + '" style="width:50px;height:40px;" />');
                        }
                    },
                    { "title": "Country Code(Alpha)", "field": "CountryAlphaCode", sortable: "CountryAlphaCode", filter: { CountryAlphaCode: "text" }, show: true },
                    { "title": "Country Code(Call)", "field": "CountryCallCode", sortable: "CountryCallCode", filter: { CountryCallCode: "text" }, show: true },
                    {
                        "title": "Action", show: true,
                        'cellTemplte': function ($scope, row) {
                            //return $scope.getHtml('<button class="btn btn-primary btn-xs"  onclick="console.log(angular.element(this.parentNode).scope())" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></button>' +
                            //'<a class="btn btn-info btn-xs"  data-ng-click="$scope.$parent.$parent.$parent.$parent.$parent.$parent.View(' + row.CountryId + ')" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>');
                            //function ($scope, row) {
                            //    var data =
                            //        '<button class="btn btn-primary btn-xs"  onclick="angular.element(document.getElementsByClassName(\'row\')).scope().Edit(' + row.CountryId + ')" title="Edit"><i class="fa fa-pencil"></i></button>' +
                            //    //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.CountryId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '
                            //    '<button class="btn btn-info btn-xs"  onclick="angular.element(document.getElementsByClassName(\'row\')).scope().View(' + row.CountryId + ')" data-uib-tooltip="View"><i class="fa fa-eye" ></i></button>'
                            //    return $scope.getHtml(data);
                            //},
                            var element = '<button class="btn btn-primary btn-xs" ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.CountryId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></button>' +
                            '<a class="btn btn-info btn-xs" ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(' + row.CountryId + ')" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>';
                            return $scope.getHtml(element);
                            //return $scope.getHtml($compile(element)($scope));
                        }
                    }
                ],
                Sort: { 'CountryId': 'asc' }
            }
        }
        $scope.Edit = function (id) {

            //$scope.id = id;
            $scope.Add(id, 0);

            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
                // $log.info('Modal dismissed at: ' + new Date());
            });
        }
        $scope.View = function (id) {

            //$scope.id = id;
            $scope.Add(id, 1);

            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
                // $log.info('Modal dismissed at: ' + new Date());
            });
        }
        $scope.Delete = function (data) {
            CountryService.DeleteCountry(data).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.refreshGrid()
                    toastr.success(result.data.Message);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.IsActive = function (data) {
        }

        $scope.RefreshTable = function () {
            $scope.tableParams.reload();
        };
    }
    var ModalInstanceCtrl = function ($scope, $uibModalInstance, CountryService, CountryController, id, Upload, isdisable) {

        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }

        $scope.objcountry = $scope.objcountry || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.id = id;
        $scope.storage = {};

        if (id && id > 0) {
            CountryService.GetByIdCountry(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objcountry = {
                        CountryId: result.data.DataList.CountryId,
                        CountryName: result.data.DataList.CountryName,
                        CountryAlphaCode: result.data.DataList.CountryAlphaCode,
                        CountryCallCode: result.data.DataList.CountryCallCode,
                        CountryFlag: result.data.DataList.CountryFlag,
                        SetFlag: "../Content/lib/CountryFlags/flags/" + result.data.DataList.CountryFlag
                    }
                    $scope.storage = angular.copy($scope.objcountry);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }
        else {
            ResetForm();
        }

        function ResetForm() {
            $scope.objcountry = {
                CountryId: 0,
                CountryName: '',
                CountryAlphaCode: '',
                CountryCallCode: '',
                SetFlag: '',
            }
            $scope.tempImagePath = "";
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormCountryInfo)
                $scope.$parent.FormCountryInfo.$setPristine();
        }

        $scope.Create = function (data) {

            $scope.Message = "";
            $scope.ChechFileValid($scope.SelectedFileForUpload);
            if ($scope.IsFileValid) {
                CountryService.AddCountry(data, $scope.SelectedFileForUpload).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        $uibModalInstance.close();
                        toastr.success(result.data.Message);
                        ResetForm();
                    } else {
                        toastr.error(result.data.Message);
                    }

                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }
            else {
                $scope.Message = "Please Select Image";
                toastr.error($scope.Message);
                //ResetForm();
            }
        }

        $scope.Update = function (data) {
            CountryService.AddCountry(data, $scope.SelectedFileForUpload).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $uibModalInstance.close();
                    toastr.success(result.data.Message);
                } else {
                    toastr.error(result.data.Message);
                }

            })
        }

        $scope.ChechFileValid = function (file) {

            var isValid = false;
            if ($scope.SelectedFileForUpload != null) {
                if ((file.type == 'image/png' || file.type == 'image/jpeg' || file.type == 'image/gif')) {
                    $scope.FileInvalidMessage = "";
                    isValid = true;
                }
                else {
                    $scope.FileInvalidMessage = "Selected file is Invalid. (only file type png, jpeg and gif and 512 kb size allowed)";
                }
            }
            else {
                $scope.FileInvalidMessage = "Image required!";
            }
            $scope.IsFileValid = isValid;
        };

        $scope.selectFileforUpload = function (file) {
            $scope.objcountry.SetFlag = '';
            Upload.upload({
                url: "/Handler/FileUpload.ashx",
                method: 'POST',
                file: file,
            }).then(function (result) {
                if (result.status == 200) {
                    if (result.data.length > 0) {
                        $scope.objcountry.SetFlag = result.data[0].imageName;
                        $scope.tempImagePath = result.data[0].imagePath;
                        $scope.SelectedFileForUpload = file[0];
                    }
                }
                else {
                    $scope.objcountry.SetFlag = '';
                }
            });
            //$scope.objcountry.SetFlag = file[0].name;
        }

        $scope.Reset = function () {
            if ($scope.objcountry.CountryId > 0) {
                $scope.tempImagePath = "";
                $scope.SelectedFileForUpload = null;
                $scope.objcountry = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };
    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'CountryService', 'CountryController', 'id', 'Upload', 'isdisable']
})()

