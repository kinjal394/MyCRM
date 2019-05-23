(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("PercentageController", [
               "$scope", "PercentageService", "$uibModal",
               PercentageController]);

    function PercentageController($scope, PercentageService, $uibModal) {

        $scope.Add = function (id, _isdisable) {
            if (_isdisable === undefined) _isdisable = 0;
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    PercentageService: function () { return PercentageService; },
                    PercentageController: function () { return PercentageController; },
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
               //{ "title": "Percentage Id", "data": "PercentageId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               {
                   "title": "Percentage %", "field": "Percentage", sortable: "Percentage", filter: { Percentage: "text" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = "<p class='MailTo' ng-bind-html='getHtml($parent.$parent.$parent.$parent.$parent.$parent.FormatPercentage(row.Percentage))'>"
                       return $scope.getHtml(element);
                   }
               },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.PercentageId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                           //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.PercentageId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                            '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.PercentageId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'PercentageId': 'asc' }
        }
        $scope.FormatPercentage = function (d) {

            var emails = d.toFixed(2) + ' %';
            var Str = emails.split('.');
            var con = '';
            if (Str[0].length == 1) {
                con = '0' + Str[0] + '.' + Str[1];
            }
            else {
                con = emails;
            }

            return con;
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
            PercentageService.DeletePercentage(data).then(function (result) {
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

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, PercentageService, PercentageController, id, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.objPercentage = $scope.objPercentage || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.id = id;
        $scope.storage = {};

        if (id && id > 0) {
            PercentageService.GetPercentageById(id).then(function (result) {
                if (result.data.ResponseType == 1) {

                    $scope.objPercentage = {
                        PercentageId: result.data.DataList.PercentageId,
                        Percentage: result.data.DataList.Percentage
                    }
                    $scope.storage = angular.copy($scope.objPercentage);
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
            $scope.objPercentage = {
                PercentageId: 0,
                Percentage: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormPercentageInfo)
                $scope.$parent.FormPercentageInfo.$setPristine();

        }

        $scope.Create = function (data) {
            PercentageService.AddPercentage(data).then(function (result) {
                $uibModalInstance.close();
                toastr.success(result.data.Message);
                ResetForm();
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.Update = function (data) {
            PercentageService.AddPercentage(data).then(function (result) {
                $uibModalInstance.close();
                toastr.success(result.data.Message);
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.Reset = function () {
            if ($scope.objPercentage.PercentageId > 0) {
                $scope.objPercentage = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'PercentageService', 'PercentageController', 'id', 'isdisable']

})()

