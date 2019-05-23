(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("CityController", [
         "$scope", "CityService", "$uibModal",
         CityController]);

    function CityController($scope, CityService, $uibModal) {

        $scope.id = 0;
        $scope.Add = function () {
            var _isdisable = 0;
            var objCityData = {
                CityId: 0,
                CityName: '',
                StateId: 0,
                //StateName: '',
                CountryId: 0,
                //CountryName: ''
                CountryName: { Display: '', Value: '' },
                StateName: { Display: '', Value: '' },
                //CityName: { Display: '', Value: '' },
            };

            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    CityService: function () { return CityService; },
                    CityController: function () { return CityController; },
                    objCityData: function () { return objCityData; },
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

        $scope.gridObj = {
            columnsInfo: [
               //{ "title": "City Id", "data": "CityId", filter: false, visible: false },
                { "title": "Sr.", "field": "RowNumber", show: true, },
               //{ "title": "Country Id", "data": "CountryId", filter: false, visible: false },
               //{ "title": "State Id", "data": "StateId", filter: false, visible: false },
               { "title": "Country Name", "field": "CountryName", sortable: "CountryName", filter: { CountryName: "text" }, show: true, },
               { "title": "State Name", "field": "StateName", sortable: "StateName", filter: { StateName: "text" }, show: true, },
               { "title": "District", "field": "CityName", sortable: "CityName", filter: { CityName: "text" }, show: true, },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                         //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.CityId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                   '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       //'render': '<button  data-ng-click="$parent.$parent.$parent.Edit(row.CityId)">Edit</button><button data-ng-click="$parent.$parent.$parent.Delete(row.CityId)">Delete</button> '
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'CityId': 'asc' }
            //modeType: "StateMaster",
            //Title: "State List"
        }

        $scope.Edit = function (data) {
            var _isdisable = 0;
            //$scope.id = id;
            //$scope.Add(id);
            var objCityData = {
                CityId: data.CityId,
                CityName: data.CityName,
                CountryId: data.CountryId,
                StateId: data.StateId,
                CountryName: { Display: data.CountryName, Value: data.CountryId },
                StateName: { Display: data.StateName, Value: data.StateId },
            }
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    CityService: function () { return CityService; },
                    CityController: function () { return $scope; },
                    objCityData: function () { return objCityData; },
                    isdisable: function () { return _isdisable; }
                }
            });
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
                // $log.info('Modal dismissed at: ' + new Date());
            });
        }

        $scope.View = function (data) {
            var _isdisable = 1;
            //$scope.id = id;
            //$scope.Add(id);
            var objCityData = {
                CityId: data.CityId,
                CityName: data.CityName,
                CountryId: data.CountryId,
                StateId: data.StateId,
                CountryName: { Display: data.CountryName, Value: data.CountryId },
                StateName: { Display: data.StateName, Value: data.StateId },
            }
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    CityService: function () { return CityService; },
                    CityController: function () { return $scope; },
                    objCityData: function () { return objCityData; },
                    isdisable: function () { return _isdisable; }
                }
            });
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
                // $log.info('Modal dismissed at: ' + new Date());
            });
        }

        $scope.Delete = function (data) {

            CityService.DeleteCity(data).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    $scope.refreshGrid()
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
                $scope.refreshGrid()
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }
    }

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, CityService, CityController, objCityData, isdisable) {

        $scope.$watch('objcity.CountryName', function (dataa) {
            if (dataa.Value != '') {
                if (dataa.Value != objCityData.CountryId.toString()) {
                    $scope.objcity.StateName.Display = '';
                    $scope.objcity.StateName.Value = '';
                }
            } else {
                $scope.CountryBind('India');
            }
        }, true)

        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.objcity = $scope.objcity || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};
        $scope.callback = function (data) {
        }

        if (objCityData.CityId && objCityData.CityId > 0) {
            $scope.objcity = {

                CityId: objCityData.CityId,
                CityName: objCityData.CityName,
                CountryName: {
                    Display: objCityData.CountryName.Display,
                    Value: objCityData.CountryId
                },
                StateName: {
                    Display: objCityData.StateName.Display,
                    Value: objCityData.StateId
                }
                //CountryId: objCityData.CountryId,
                //StateId: objCityData.StateId
            }
            $scope.storage = angular.copy($scope.objcity);

        } else {
            ResetForm();
        }

        $scope.CountryBind = function (data) {
            CityService.CountryBind().then(function (result) {
                if (result.data.ResponseType == 1) {
                    _.each(result.data.DataList, function (value, key, list) {
                        if (value.CountryName.toString().toLowerCase() == data.toString().toLowerCase()) {
                            $scope.objcity.CountryName = {
                                Display: value.CountryName,
                                Value: value.CountryId
                            };
                            return false;
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

        //$scope.$watch("objcity.CountryId", function (newValue, oldValue) {
        //    if (newValue) {
        //        StateBind();
        //    }
        //    if (newValue == 0) {
        //        $scope.CountryBind('India');
        //    }
        //});

        function ResetForm() {

            $scope.objcity = {
                CityId: 0,
                CityName: '',
                CountryId: 0,
                StateId: 0,
                CountryName: { Display: '', Value: '' },
                StateName: { Display: '', Value: '' }
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormCityInfo)
                $scope.$parent.FormCityInfo.$setPristine();
            //$scope.FormCityInfo.$setPristine();
            //$timeout(function () {
            //    $scope.isFirstFocus = true;
            //});
        }

        function StateBind() {

            var cid = $scope.objcity.CountryId;
            if (cid == undefined) {
                cid = 0;
            }
            CityService.StateBind(cid).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.StateList = result.data.DataList;
                }
                else if (result.data.ResponseType == 3) {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.StateBind = function () {
            StateBind();
        }

        $scope.Create = function (data) {

            var objinscity = {
                CountryId: data.CountryName.Value,
                StateId: data.StateName.Value,
                CityName: data.CityName
            }
            CityService.AddCity(objinscity).then(function (result) {
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

        $scope.Update = function (data) {
            var objinscity = {
                CountryId: data.CountryName.Value,
                CityName: data.CityName,
                CityId: data.CityId,
                StateId: data.StateName.Value
            }
            CityService.AddCity(objinscity).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $uibModalInstance.close();
                    toastr.success(result.data.Message);
                } else {
                    toastr.error(result.data.Message);
                }

            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.Reset = function () {
            if ($scope.objcity.CityId > 0) {
                $scope.objcity = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'CityService', 'CityController', 'objCityData', 'isdisable']
})()

