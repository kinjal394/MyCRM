(function () {
    "use strict"
    angular.module("CRMApp.Controllers")
            .controller("PaymentTermsCtrl", [
            "$scope", "$rootScope", "$timeout", "$filter", "PaymentTermsService", "NgTableParams", "$uibModal",
            PaymentTermsCtrl
            ]);

    function PaymentTermsCtrl($scope, $rootScope, $timeout, $filter, PaymentTermsService, NgTableParams, $uibModal) {

        $scope.Add = function (id, _isdisable) {
            if (_isdisable === undefined) _isdisable = 0;
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    PaymentTermsService: function () { return PaymentTermsService; },
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
               //{ "title": "Payment Term Id", "data": "PaymentTermId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "Term Name", "field": "TermName", sortable: "TermName", filter: { TermName: "text" }, show: true, },
               {
                   "title": "Terms %", "field": "Terms", sortable: "Terms", filter: { Terms: "text" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = "<p class='MailTo' ng-bind-html='getHtml($parent.$parent.$parent.$parent.$parent.$parent.FormatPercentage(row.Terms))'>"
                       return $scope.getHtml(element);
                   }
               },

               //{ "title": "Description", "data": "Description" },
               {
                   "title": "Description", "field": "Description", sortable: "Description", filter: { Description: "text" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<p data-uib-tooltip="{{row.Description}}" ng-bind="LimitString(row.Description,100)">'
                       return $scope.getHtml(element);
                   }
               },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.PaymentTermId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                            //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.PaymentTermId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                            '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.PaymentTermId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { "PaymentTermId": "asc" }
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
        }
        $scope.View = function (id) {
            $scope.Add(id, 1);
        }
        $scope.Delete = function (id) {
            PaymentTermsService.DeletePaymentTerms(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
                $scope.refreshGrid()
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.refreshtable = function () {
            $scope.tableparams.reload();
        };

    }

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, PaymentTermsService, id, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.objPaymentTerms = $scope.objPaymentTerms || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.id = id;
        $scope.storage = {};

        if (id && id > 0) {
            PaymentTermsService.GetByIdPaymentTerms(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objPaymentTerms = {
                        PaymentTermId: result.data.DataList.PaymentTermId,
                        TermName: result.data.DataList.TermName,
                        Terms : result.data.DataList.Terms,
                        Description: result.data.DataList.Description
                    }
                    $scope.storage = angular.copy($scope.objPaymentTerms);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        function ResetForm() {
            $scope.objPaymentTerms = {
                PaymentTermId: 0,
                TermName: '',
                Terms:'',
                Description: '',
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormPaymentTermsInfo)
                $scope.$parent.FormPaymentTermsInfo.$setPristine();
        }

        $scope.CreateUpdate = function (data) {
            PaymentTermsService.CreateUpdatePaymentTerms(data).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    ResetForm();
                    $uibModalInstance.close();
                } else {
                    toastr.error(result.data.Message);
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.Reset = function () {
            if ($scope.objPaymentTerms.PaymentTermId > 0) {
                $scope.objPaymentTerms = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };
    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'PaymentTermsService', 'id', 'isdisable']

})()





