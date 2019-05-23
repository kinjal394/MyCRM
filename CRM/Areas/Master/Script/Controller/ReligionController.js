(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("ReligionController", [
               "$scope", "ReligionService", "$uibModal",
               ReligionController]);

    function ReligionController($scope, ReligionService, $uibModal, _isdisable) {
        if (_isdisable === undefined) _isdisable = 0;
        $scope.Add = function (id, _isdisable) {
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    ReligionService: function () { return ReligionService; },
                    ReligionController: function () { return ReligionController; },
                    id: function () { return id; },
                    isdisable: function () { return _isdisable; }
                }
            });
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
            });
        }

        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };

        $scope.gridObj = {
            columnsInfo: [
               //{ "title": "Religion Id", "data": "ReligionId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "Religion Name", "field": "ReligionName", sortable: "ReligionName", filter: { ReligionName: "text" }, show: true, },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.ReligionId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                           //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.ReligionId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                   '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.ReligionId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'ReligionId': 'asc' }
        }

        $scope.Edit = function (id) {
            $scope.Add(id, 0);
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            });
        }
        $scope.View = function (id) {
            $scope.Add(id, 1);
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            });
        }

        $scope.Delete = function (data) {
            ReligionService.DeleteReligion(data).then(function (result) {
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

        $scope.close = function () {
            $uibModalInstance.close();
        };
    }

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, ReligionService, ReligionController, id, isdisable) {

        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }

        $scope.objReligion = $scope.objReligion || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.id = id;
        $scope.storage = {};

        if (id && id > 0) {
            ReligionService.GetReligionById(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objReligion = {
                        ReligionId: result.data.DataList.ReligionId,
                        ReligionName: result.data.DataList.ReligionName
                    }
                    $scope.storage = angular.copy($scope.objReligion);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        } else {
            ResetForm();
        }

        function ResetForm() {
            $scope.objReligion = {
                ReligionId: 0,
                ReligionName: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormReligionInfo)
                $scope.$parent.FormReligionInfo.$setPristine();

        }

        $scope.Create = function (data) {
            ReligionService.AddReligion(data).then(function (result) {
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
            ReligionService.AddReligion(data).then(function (result) {
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
            if ($scope.objReligion.ReligionId > 0) {
                $scope.objReligion = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };
    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'ReligionService', 'ReligionController', 'id', 'isdisable']
})()


