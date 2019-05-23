(function () {

    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("ReferenceSourceCtrl", [
         "$scope", "ReferenceSourceService", "$uibModal",
         ReferenceSourceCtrl]);

    function ReferenceSourceCtrl($scope, ReferenceSourceService, $uibModal) {

        $scope.Add = function () {
            var _isdisable = 0;
            var objRefernceSourceData = {
                SourceId: 0,
                SourceName: ''
            };

            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    ReferenceSourceService: function () { return ReferenceSourceService; },
                    ReferenceSourceCtrl: function () { return ReferenceSourceCtrl; },
                    objRefernceSourceData: function () { return objRefernceSourceData; },
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
                { "title": "Sr.", "field": "RowNumber", show: true },
                { "title": "Source Name", "field": "SourceName", sortable: "SourceName", filter: { SourceName: "text" }, show: false },
               { "title": "Source Name", "field": "SourceName", sortable: "SourceName", filter: { SourceName: "text" }, show: true },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                      //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.SourceId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                      '<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.$parent.$parent.$parent.Delete(row.SourceId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                   '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'CityId': 'asc' }
        }

        $scope.Edit = function (data) {
            var _isdisable = 0;
            var objRefernceSourceData = {
                SourceId: data.SourceId,
                SourceName: data.SourceName
            }
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    ReferenceSourceService: function () { return ReferenceSourceService; },
                    ReferenceSourceCtrl: function () { return ReferenceSourceCtrl; },
                    objRefernceSourceData: function () { return objRefernceSourceData; },
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
            var objRefernceSourceData = {
                SourceId: data.SourceId,
                SourceName: data.SourceName
            }
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    ReferenceSourceService: function () { return ReferenceSourceService; },
                    ReferenceSourceCtrl: function () { return ReferenceSourceCtrl; },
                    objRefernceSourceData: function () { return objRefernceSourceData; },
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
            console.log(data);
            ReferenceSourceService.DeleteReferenceSource(data).then(function (result) {
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

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, ReferenceSourceService, ReferenceSourceCtrl, objRefernceSourceData, isdisable) {

        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.objRefSource = $scope.objRefSource || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};
        $scope.callback = function (data) {
        }


        if (objRefernceSourceData.SourceId && objRefernceSourceData.SourceId > 0) {
            $scope.objRefSource = {
                SourceId: objRefernceSourceData.SourceId,
                SourceName: objRefernceSourceData.SourceName
            }
            $scope.storage = angular.copy($scope.objRefSource);

        } else {
            ResetForm();
        }

        function ResetForm() {
            $scope.objRefSource = {
                SourceId: 0,
                SourceName: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormReferenceSource)
                $scope.$parent.FormReferenceSource.$setPristine();
        }

        $scope.Create = function (data) {
            var objSource = {
                SourceId: data.SourceId,
                SourceName: data.SourceName
            }
            ReferenceSourceService.CreateUpdateReferenceSource(objSource).then(function (result) {
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
            var objSource = {
                SourceId: data.SourceId,
                SourceName: data.SourceName
            }
            ReferenceSourceService.CreateUpdateReferenceSource(objSource).then(function (result) {
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
            if ($scope.objRefSource.SourceId > 0) {
                $scope.objRefSource = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'ReferenceSourceService', 'ReferenceSourceCtrl', 'objRefernceSourceData', 'isdisable']

})()