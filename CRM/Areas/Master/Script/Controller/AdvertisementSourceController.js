(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("AdvertisementSourceController", [
               "$scope", "AdvertisementSourceService", "$uibModal",
               AdvertisementSourceController]);

    function AdvertisementSourceController($scope, AdvertisementSourceService, $uibModal) {
        $scope.Add = function (id, _isdisable) {
            if (_isdisable === undefined) _isdisable = 0;
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    AdvertisementSourceService: function () { return AdvertisementSourceService; },
                    AdvertisementSourceController: function () { return AdvertisementSourceController; },
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

        $scope.gridObj = {
            columnsInfo: [
               //{ "title": "Site Id", "data": "SiteId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", filter: false, show: true },
               { "title": "Site Name", "field": "SiteName", sortable: "SiteName", filter: { SiteName: "text" }, show: true },
               { "title": "Site Url", "field": "SiteUrl", sortable: "SiteUrl", filter: { SiteUrl: "text" }, show: true },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.SiteId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                            //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.$parent.$parent.$parent.Delete(row.SiteId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                           '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.SiteId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'SiteId': 'asc' }
        }

        $scope.Edit = function (id) {
            $scope.Add(id,0);
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
            });
        }

        $scope.View = function (id) {
            $scope.Add(id,1);
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
            });
        }

        $scope.Delete = function (data) {
            AdvertisementSourceService.DeleteAdvertSource(data).then(function (result) {
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

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, AdvertisementSourceService, AdvertisementSourceController, id, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }

        $scope.objAdvertSource = $scope.objAdvertSource || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.id = id;
        $scope.storage = {};

        if (id && id > 0) {
            AdvertisementSourceService.GetAdvertSourceById(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objAdvertSource = {
                        SiteId: result.data.DataList.SiteId,
                        SiteName: result.data.DataList.SiteName,
                        SiteUrl: result.data.DataList.SiteUrl
                    }
                    $scope.storage = angular.copy($scope.objAdvertSource);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        } else {
            ResetForm();
        }

        $scope.focusMe = function (value) {
            if (value === true) {
                element[0].focus();
            }
        };
        function ResetForm() {
            $scope.objAdvertSource = {
                SiteId: 0,
                SiteName: '',
                SiteUrl: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormAdvertSourceInfo)
                $scope.$parent.FormAdvertSourceInfo.$setPristine();

        }

        //CREATE
        $scope.Create = function (data) {
            AdvertisementSourceService.AddAdvertSource(data).then(function (result) {
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

        //UPDATE
        $scope.Update = function (data) {
            AdvertisementSourceService.AddAdvertSource(data).then(function (result) {
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

        $scope.Reset = function () {
            if ($scope.objAdvertSource.SiteId > 0) {
                $scope.objAdvertSource = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'AdvertisementSourceService', 'AdvertisementSourceController', 'id', 'isdisable']

})()

