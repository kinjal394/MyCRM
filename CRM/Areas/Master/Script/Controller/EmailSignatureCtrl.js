(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("EmailSignatureController", [
               "$scope", "EmailSignatureService", "$filter", "$uibModal", "$sce",
                function EmailSignatureController($scope, EmailSignatureService, $filter, $uibModal, $sce) {
                    $scope.storage = {};
                    $scope.AddEmailSignature = function () {
                        var _isdisable = 0;
                        $scope.emailSignatureObj = {
                            SignatureId: 0,
                            Title: '',
                            Signature: '',
                            UserId: '',
                            UserName: { Display: '', Value: '' }
                        }

                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'signatureModalContent.html',
                            controller: 'SignatureModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                emailSignatureObj: function () {
                                    return $scope.emailSignatureObj;
                                },
                                storage: function () {
                                    return $scope.storage;
                                },
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
                           //{ "title": "Speech Id", "data": "SignatureId", filter: false, visible: false },
                            { "title": "Sr.", "field": "RowNumber", show: true, },
                           { "title": "Signature Title", "field": "Title", sortable: "Title", filter: { Title: "text" }, show: true, },
                          { "title": "Belong to Department", "field": "DepartmentName", sortable: "DepartmentName", filter: { DepartmentName: "text" }, show: true, },
                           { "title": "Belong User", "field": "Name", sortable: "Name", filter: { Name: "text" }, show: true, },
                           //, render: '<a uib-tooltip-html="{{$parent.$parent.$parent.tooltipContent}}" ng-mouseover="$parent.$parent.$parent.getTooltipHtmlContent(row.Signature)" ng-bind-html="getHtml(LimitString(row.Signature,200))"></a>' 
                            {
                                "title": "Signature Word template", "field": "Signature", sortable: "Signature", filter: { Signature: "text" }, show: true,
                                'cellTemplte': function ($scope, row) {
                                    var element = '<a href="#" style="color:black;" uib-tooltip-html="$parent.$parent.$parent.$parent.$parent.$parent.tooltipContent" ng-mouseover="$parent.$parent.$parent.$parent.$parent.$parent.getTooltipHtmlContent(row.Signature)" ng-bind-html="getHtml(LimitString(row.Signature,200))">Signature!</a>'
                                    return $scope.getHtml(element);
                                }
                            },
                           //{ "title": "UserName", "data": "UserName", filter: false, visible: false },
                           {
                               "title": "Action", show: true,
                               'cellTemplte': function ($scope, row) {
                                   var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                               //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.SignatureId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                                '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                                   return $scope.getHtml(element);
                               }
                           }
                        ],
                        Sort: { 'SignatureId': 'asc' }
                    }
                    $scope.getTooltipHtmlContent = function (tooltip) {
                        $scope.tooltipContent = $sce.trustAsHtml(tooltip);
                        return $scope.tooltipContent;
                    }
                    $scope.Edit = function (data) {
                        var _isdisable = 0;
                        $scope.emailSignatureObj = {
                            SignatureId: data.SignatureId,
                            Title: data.Title,
                            Signature: data.Signature,
                            UserId: data.UserId,
                            UserName: { Display: data.UserName, Value: data.UserId }
                        }
                        $scope.storage = angular.copy($scope.emailSignatureObj);
                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'signatureModalContent.html',
                            controller: 'SignatureModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                emailSignatureObj: function () {
                                    return $scope.emailSignatureObj;
                                },
                                storage: function () {
                                    return $scope.storage;
                                },
                                isdisable: function () { return _isdisable; }
                            }
                        });

                        modalInstance.result.then(function (selectedItem) {
                            $scope.refreshGrid();
                        }, function () {
                        });
                    }
                    $scope.View = function (data) {
                        var _isdisable = 1;
                        $scope.emailSignatureObj = {
                            SignatureId: data.SignatureId,
                            Title: data.Title,
                            Signature: data.Signature,
                            UserId: data.UserId,
                            UserName: { Display: data.UserName, Value: data.UserId }
                        }
                        $scope.storage = angular.copy($scope.emailSignatureObj);
                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'signatureModalContent.html',
                            controller: 'SignatureModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                emailSignatureObj: function () {
                                    return $scope.emailSignatureObj;
                                },
                                storage: function () {
                                    return $scope.storage;
                                },
                                isdisable: function () { return _isdisable; }
                            }
                        });

                        modalInstance.result.then(function (selectedItem) {
                            $scope.refreshGrid();
                        }, function () {
                        });
                    }
                    $scope.Delete = function (id) {
                        EmailSignatureService.DeleteSignature(id).then(function (result) {
                            if (result.data.ResponseType == 1) {
                                toastr.success(result.data.Message)
                            }
                            else {
                                toastr.error(result.data.Message)
                            }
                            $scope.refreshGrid();
                        })

                    }
                }])

    angular.module('CRMApp.Controllers').controller('SignatureModalInstanceCtrl', [
        '$scope', '$uibModalInstance', 'emailSignatureObj', 'EmailSignatureService', 'storage', 'isdisable',
    function ($scope, $uibModalInstance, emailSignatureObj, EmailSignatureService, storage, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        var $ctrl = this;
        $ctrl.ok = function () {
            $uibModalInstance.close();
        };

        $ctrl.signatureData = emailSignatureObj;
        $ctrl.storage = storage;
        $ctrl.editorOptions = {
            language: 'en',
            uiColor: '#f0f2f5'
        };

        $ctrl.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

        EmailSignatureService.GetAllUser().then(function (result) {
            $ctrl.UserList = result.data;
        }, function (errorMsg) {
            toastr.error(errorMsg, 'Opps, Something went wrong');
        })

        $ctrl.SaveSignature = function () {
            $ctrl.signatureData.UserId = $ctrl.signatureData.UserName.Value;
            EmailSignatureService.AddSignature($ctrl.signatureData).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $uibModalInstance.close();
                    toastr.success(result.data.Message)
                    $ctrl.signatureData = {
                        SignatureId: 0,
                        Title: '',
                        Signature: '',
                        UserId: ''

                    }
                }
                else {
                    toastr.error(result.data.Message)
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');

            })
        }

        $ctrl.UpdateSignature = function () {
            EmailSignatureService.UpdateSignature($ctrl.signatureData).then(function (result) {
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

            if ($ctrl.signatureData.SignatureId > 0) {

                $ctrl.signatureData = angular.copy($ctrl.storage);

            } else {
                ResetForm();
            }
        }


        function ResetForm() {

            $ctrl.signatureData = {
                SignatureId: 0,
                Title: '',
                Signature: ''
            }
            $ctrl.storage = {};
            $ctrl.addMode = true;
            $ctrl.saveText = "Save";
            if ($ctrl.$parent.signatureInfo)
                $ctrl.$parent.signatureInfo.$setPristine();
        }

    }]);
})()