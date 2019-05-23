(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("TaxController", [
               "$scope", "TaxService", "$filter", "$uibModal",
                function TaxController($scope, TaxService, $filter, $uibModal) {
                    $scope.storage = {};
                    $scope.AddTax = function () {
                        var _isdisable = 0;
                        $scope.TaxObj = {
                            TaxId: 0,
                            TaxName: ''
                        }
                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'unitModalContent.html',
                            controller: 'TaxModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                TaxObj: function () {
                                    return $scope.TaxObj;
                                },
                                storage: function () {
                                    return $scope.storage;
                                },
                                isdisable: function () { return _isdisable; }
                            }
                        });

                        modalInstance.result.then(function () {
                            $scope.refreshGrid()
                        }, function () {
                            // $log.info('Modal dismissed at: ' + new Date());
                        });
                    }

                    $scope.setDirectiveRefresh = function (refreshGrid) {
                        $scope.refreshGrid = refreshGrid;
                    };

                    $scope.gridObj = {
                        columnsInfo: [
                           //{ "title": "Tax Id", "data": "TaxId", filter: false, visible: false },
                           { "title": "Sr.", "field": "RowNumber", show: true, },
                           { "title": "Tax Name", "field": "TaxName", sortable: "TaxName", filter: { TaxName: "text" }, show: true, },
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
                                   var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                              //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.TaxId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                              '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                                   return $scope.getHtml(element);
                               }
                           }
                        ],
                        Sort: { 'TaxId': 'asc' }
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
                    $scope.Edit = function (data) {
                        var _isdisable = 0;
                        $scope.TaxObj = {
                            TaxId: data.TaxId,
                            TaxName: data.TaxName,
                            Percentage: data.Percentage
                        }
                        $scope.storage = angular.copy($scope.TaxObj);
                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'unitModalContent.html',
                            controller: 'TaxModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                TaxObj: function () {
                                    return $scope.TaxObj;
                                },
                                storage: function () {
                                    return $scope.storage;
                                },
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
                        $scope.TaxObj = {
                            TaxId: data.TaxId,
                            TaxName: data.TaxName,
                            Percentage: data.Percentage
                        }
                        $scope.storage = angular.copy($scope.TaxObj);
                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'unitModalContent.html',
                            controller: 'TaxModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                TaxObj: function () {
                                    return $scope.TaxObj;
                                },
                                storage: function () {
                                    return $scope.storage;
                                },
                                isdisable: function () { return _isdisable; }
                            }
                        });
                        modalInstance.result.then(function () {
                            $scope.refreshGrid()
                        }, function () {
                        });
                    }
                    $scope.Delete = function (taxID) {
                        TaxService.DeleteTax(taxID).then(function (result) {
                            if (result.data.ResponseType == 1) {
                                toastr.success(result.data.Message)
                                $scope.refreshGrid()
                            }
                            else {
                                toastr.error(result.data.Message)
                            }
                        }, function (errorMsg) {
                            toastr.error(errorMsg, 'Opps, Something went wrong');
                        })
                    }
                }]);

    angular.module('CRMApp.Controllers')
        .controller('TaxModalInstanceCtrl', ['$scope',
            '$uibModalInstance', 'TaxObj', 'TaxService', 'storage', 'isdisable',
            function ($scope, $uibModalInstance, TaxObj, TaxService, storage, isdisable) {
                $scope.isClicked = false;
                if (isdisable == 1) {
                    $scope.isClicked = true;
                }

                var $ctrl = this;

                $ctrl.ok = function () {
                    $uibModalInstance.close();
                };

                $ctrl.TaxObj = TaxObj;
                $ctrl.storage = storage;
                $ctrl.cancel = function () {
                    $uibModalInstance.dismiss('cancel');
                };

                $ctrl.SaveTax = function () {
                    TaxService.AddTax($ctrl.TaxObj).then(function (result) {
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

                $ctrl.UpdateTax = function () {
                    TaxService.UpdateTax($ctrl.TaxObj).then(function (result) {
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

                    if ($ctrl.TaxObj.TaxId > 0) {

                        $ctrl.TaxObj = angular.copy($ctrl.storage);

                    } else {
                        ResetForm();
                    }
                }


                function ResetForm() {

                    $ctrl.TaxObj = {
                        TaxId: 0,
                        TaxName: '',
                        Percentage: '',

                    }
                    $ctrl.storage = {};
                    $ctrl.addMode = true;
                    $ctrl.saveText = "Save";
                    if ($ctrl.$parent.FormTax)
                        $ctrl.$parent.FormTax.$setPristine();
                }

            }]);
})();