(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("CountryOriginController", [
         "$scope", "CountryOriginService", "$uibModal",
         CountryOriginController]);

    function CountryOriginController($scope, CountryOriginService, $uibModal) {
        $scope.id = 0;

        $scope.Add = function (data) {
            var _isdisable = 0;
            var objorigindata = {
                OriginId: 0,
                CountryOfOrigin: '',
                CountryId: 102,
                CountryData: {
                    Display: "",
                    Value: ""
                }
            };
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    CountryOriginService: function () { return CountryOriginService; },
                    CountryOriginController: function () { return CountryOriginController; },
                    objorigindata: function () { return objorigindata; },
                    isdisable: function () { return _isdisable; }
                }
            });
            modalInstance.result.then(function () {
                $scope.refreshGrid();
            }, function () {
            });
        }

        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };

        $scope.gridObj = {
            columnsInfo: [
               //{ "title": "Origin Id", "data": "OriginId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "Country Of Origin", "field": "CountryOfOrigin", sortable: "CountryOfOrigin", filter: { CountryOfOrigin: "text" }, show: true, },
               //{ "title": "CountryId", "data": "CountryId", filter: false, visible: false },
               //{ "title": "Country Name", "field": "CountryName", sortable: "CountryName", filter: { CountryName: "text" }, show: true, },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                           //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.OriginId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                   '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       //'render': '<button  data-ng-click="$parent.$parent.$parent.Edit(row.CountryId)">Edit</button><button data-ng-click="$parent.$parent.$parent.Delete(row.CountryId)">Delete</button> '
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'OriginId': 'asc' }
            //modeType: "CountryMaster",
            //Title: "Country List"
        }

        $scope.Edit = function (data) {
            var _isdisable=0;
            var objorigindata = {
                OriginId: data.OriginId,
                CountryOfOrigin: data.CountryOfOrigin,
                CountryId: data.CountryId,
                CountryData: {
                    Display: data.CountryName,
                    Value: data.CountryId
                }
            };
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    CountryOriginService: function () { return CountryOriginService; },
                    CountryOriginController: function () { return $scope; },
                    objorigindata: function () { return objorigindata; },
                    isdisable: function () { return _isdisable; }
                }
            });

            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
            });
        }
        $scope.View = function (data) {
            var _isdisable = 1;
            var objorigindata = {
                OriginId: data.OriginId,
                CountryOfOrigin: data.CountryOfOrigin,
                CountryId: data.CountryId,
                CountryData: {
                    Display: data.CountryName,
                    Value: data.CountryId
                }
            };
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    CountryOriginService: function () { return CountryOriginService; },
                    CountryOriginController: function () { return $scope; },
                    objorigindata: function () { return objorigindata; },
                    isdisable: function () { return _isdisable; }
                }
            });

            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
            });
        }
        $scope.Delete = function (data) {
            CountryOriginService.DeleteCountryOrigin(data).then(function (result) {
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

    }

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, CountryOriginService, CountryOriginController, objorigindata, isdisable, CityService) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }

        $scope.objorigin = $scope.objorigin || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.storage = {};

        if (objorigindata.OriginId && objorigindata.OriginId > 0) {
            $scope.objorigin = {
                OriginId: objorigindata.OriginId,
                CountryOfOrigin: objorigindata.CountryOfOrigin,
                CountryData: {
                    Display: objorigindata.CountryData.Display,
                    Value: objorigindata.CountryId
                }
            }
            $scope.storage = angular.copy($scope.objorigin);
        } else {
            ResetForm();
        }
        $scope.$watch("objorigin.CountryData", function (data) {
            //if (newValue == 0) {
            if (data == '' || data == undefined) {
                $scope.CountryBind('India');
            }
            //}
        });
        $scope.CountryBind = function (data) {
            CityService.CountryBind().then(function (result) {
                if (result.data.ResponseType == 1) {
                    _.each(result.data.DataList, function (value, key, list) {
                        if (value.CountryName.toString().toLowerCase() == data.toString().toLowerCase()) {
                            $scope.objorigin.CountryData = {
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
        function ResetForm() {
            $scope.objorigin = {
                OriginId: 0,
                CountryOfOrigin: '',
                CountryId: 0
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormCountryOriginInfo)
                $scope.$parent.FormCountryOriginInfo.$setPristine();

        }
        //$scope.CountryBind = function () {
        //    $('#mydiv').show();
        //    CountryOriginService.CountryBind().then(function (result) {
        //        if (result.data.ResponseType == 1) {
        //            $scope.CountryList = result.data.DataList;
        //            $('#mydiv').hide();
        //        }
        //        else if (result.data.ResponseType == 3) {
        //            toastr.error(result.data.Message, 'Opps, Something went wrong');
        //        }
        //    }, function (errorMsg) {
        //        toastr.error(errorMsg, 'Opps, Something went wrong');
        //    })
        //}

        $scope.Create = function (data) {
            var CountryOrigin = {
                //OriginId: data.OriginId,
                CountryOfOrigin: data.CountryOfOrigin,
                CountryId: data.CountryData.Value
            }
            CountryOriginService.AddCountryOrigin(CountryOrigin).then(function (result) {
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
            var CountryOrigin = {
                OriginId: data.OriginId,
                CountryOfOrigin: data.CountryOfOrigin,
                CountryId: data.CountryData.Value
            }
            CountryOriginService.AddCountryOrigin(CountryOrigin).then(function (result) {
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
            if ($scope.objorigin.OriginId > 0) {
                $scope.objorigin = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
            //if ($scope.id > 0) {
            //    $scope.objCategory = angular.copy($scope.storage);
            //} else {
            //    ResetForm();
            //}
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'CountryOriginService', 'CountryOriginController', 'objorigindata', 'isdisable', 'CityService']
})()


