(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("EmailSpeechController", [
               "$scope", "EmailSpeechService", "$filter", "$uibModal", "$sce",
                function EmailSpeechController($scope, EmailSpeechService, $filter, $uibModal, $sce) {
                    $scope.storage = {};
                    $scope.AddEmailSpeech = function () {
                        var _isdisable = 0;
                        $scope.emailSpeechObj = {
                            SpeechId: 0,
                            Title: '',
                            Description: '',
                            Subject:''
                        }

                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'speechModalContent.html',
                            controller: 'SpeechModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                emailSpeechObj: function () {
                                    return $scope.emailSpeechObj;
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
                            // $log.info('Modal dismissed at: ' + new Date());
                        });
                    }

                    $scope.setDirectiveRefresh = function (refreshGrid) {
                        $scope.refreshGrid = refreshGrid;
                    };

                    $scope.gridObj = {
                        columnsInfo: [
                           //{ "title": "Speech Id", "data": "SpeechId", filter: false, visible: false },
                           { "title": "Sr.", "field": "RowNumber", show: true, },
                           { "title": "Email Speech Used For", "field": "Title", sortable: "Title", filter: { Title: "text" }, show: true, },
                           { "title": "Email Subject", "field": "Subject", sortable: "Subject", filter: { Subject: "text" }, show: true, },

                           // render: '<div data-uib-tooltip="{{row.Description}}" ng-bind-html="getHtml(LimitString(row.Description,200))"></div>' 
                           {
                               "title": "Description/Matter", "field": "Description", sortable: "Description", filter: { Description: "text" }, show: true,
                               'cellTemplte': function ($scope, row) {
                                   var element = '<a href="#" style="color:black;" uib-tooltip-html="$parent.$parent.$parent.$parent.$parent.$parent.tooltipContent" ng-mouseover="$parent.$parent.$parent.$parent.$parent.$parent.getTooltipHtmlContent(row.Description)" ng-bind-html="getHtml(LimitString(row.Description,200))">Description!</a>'
                                   return $scope.getHtml(element);
                               }
                           },
                           //{
                           //    "title": "Description", "data": "Description", sort: true, filter: true,
                           //    "render": '<p data-uib-tooltip="{{row.Description}}" ng-bind="LimitString(row.Description,100)">'
                           //},
                           {
                               "title": "Action", show: true,
                               'cellTemplte': function ($scope, row) {
                                   var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                                //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.SpeechId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                                '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                                   return $scope.getHtml(element);
                               }
                           }
                        ],
                        Sort: { 'SpeechId': 'asc' }

                    }
                    $scope.getTooltipHtmlContent = function (tooltip) {
                        $scope.tooltipContent = $sce.trustAsHtml(tooltip);
                        return $scope.tooltipContent;
                    }
                    $scope.Edit = function (data) {
                        var _isdisable = 0;
                        $scope.emailSpeechObj = {
                            SpeechId: data.SpeechId,
                            Title: data.Title,
                            Subject:data.Subject,
                            Description: data.Description
                        }
                        $scope.storage = angular.copy($scope.emailSpeechObj);
                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'speechModalContent.html',
                            controller: 'SpeechModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                emailSpeechObj: function () {
                                    return $scope.emailSpeechObj;
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
                        $scope.emailSpeechObj = {
                            SpeechId: data.SpeechId,
                            Title: data.Title,
                            Subject:data.Subject,
                            Description: data.Description
                        }
                        $scope.storage = angular.copy($scope.emailSpeechObj);
                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'speechModalContent.html',
                            controller: 'SpeechModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                emailSpeechObj: function () {
                                    return $scope.emailSpeechObj;
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
                        EmailSpeechService.DeleteSpeech(id).then(function (result) {
                            if (result.data.ResponseType == 1) {
                                toastr.success(result.data.Message)
                                $scope.refreshGrid();
                            }
                            else {
                                toastr.error(result.data.Message)
                            }
                        }, function (errorMsg) {
                            toastr.error(errorMsg, 'Opps, Something went wrong');
                        })

                    }

                }])

    angular.module('CRMApp.Controllers').controller('SpeechModalInstanceCtrl', [
        '$scope', '$uibModalInstance', 'emailSpeechObj', 'EmailSpeechService', 'storage', 'isdisable',
        function ($scope, $uibModalInstance, emailSpeechObj, EmailSpeechService, storage, isdisable) {
            $scope.isClicked = false;
            if (isdisable == 1) {
                $scope.isClicked = true;
            }
            var $ctrl = this;

            $ctrl.ok = function () {
                $uibModalInstance.close();
            };

            $ctrl.speechData = emailSpeechObj;
            $ctrl.storage = storage;
            $ctrl.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };

            $ctrl.SaveSpeech = function () {
                //$scope.$apply(function () {
                EmailSpeechService.AddSpeech($ctrl.speechData).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        $uibModalInstance.close();
                        toastr.success(result.data.Message)
                        $ctrl.speechData = {
                            SpeechId: 0,
                            Title: '',
                            Description: '',
                            Subject:''
                        }
                    }
                    else {
                        toastr.error(result.data.Message)
                    }
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                    //})
                })
            }

            $ctrl.UpdateSpeech = function () {
                EmailSpeechService.UpdateSpeech($ctrl.speechData).then(function (result) {
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

                if ($ctrl.speechData.SpeechId > 0) {

                    $ctrl.speechData = angular.copy($ctrl.storage);

                } else {
                    ResetForm();
                }
            }


            function ResetForm() {

                $ctrl.speechData = {
                    SpeechId: 0,
                    Title: '',
                    Subject:'',
                    Description: ''
                }
                $ctrl.storage = {};
                $ctrl.addMode = true;
                $ctrl.saveText = "Save";
                if ($ctrl.$parent.FormAreaInfo)
                    $ctrl.$parent.FormAreaInfo.$setPristine();
            }

        }]);
})()