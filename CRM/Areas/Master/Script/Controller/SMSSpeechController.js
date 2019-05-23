(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("SMSSpeechController", [
               "$scope", "SMSSpeechService", "$uibModal",
               SMSSpeechController]);

    function SMSSpeechController($scope, SMSSpeechService, $uibModal) {
        $scope.Add = function (id, _isdisable) {
            if (_isdisable === undefined) _isdisable = 0;
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    SMSSpeechService: function () { return SMSSpeechService; },
                    SMSSpeechController: function () { return SMSSpeechController; },
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
               //{ "title": "SMS Id", "data": "SMSId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "Department Name", "field": "DepartmentName", sortable: "DepartmentName", filter: { DepartmentName: "text" }, show: true, },
               { "title": "SMS Title", "field": "SMSTitle", sortable: "SMSTitle", filter: { SMSTitle: "text" }, show: true, },
               //{ "title": "SMS", "data": "SMS", sort: true, filter: true, },
               {
                   "title": "SMS", "field": "SMS", sortable: "SMS", filter: { SMS: "text" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<p data-uib-tooltip="{{row.SMS}}" ng-bind="LimitString(row.SMS,100)">'
                       return $scope.getHtml(element);
                   }
               },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.SMSId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                           //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.SMSId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                   '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.SMSId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       //'render': '<button  data-ng-click="$parent.$parent.$parent.Edit(row.SourceId)">Edit</button><button data-ng-click="$parent.$parent.$parent.Delete(row.SourceId)">Delete</button> '
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'SMSId': 'asc' }

        }

        $scope.Edit = function (id) {
            $scope.Add(id, 0);
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
                // $log.info('Modal dismissed at: ' + new Date());
            });
        }
        $scope.View = function (id) {
            $scope.Add(id, 1);
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
                // $log.info('Modal dismissed at: ' + new Date());
            });
        }
        $scope.Delete = function (data) {
            SMSSpeechService.DeleteSMSSpeech(data).then(function (result) {
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

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, SMSSpeechService, SMSSpeechController, id, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }

        $scope.objSMSSpeech = $scope.objSMSSpeech || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.id = id;
        $scope.storage = {};

        if (id && id > 0) {
            SMSSpeechService.GetSMSSpeechById(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objSMSSpeech = {
                        SMSId: result.data.DataList.SMSId,
                        SMSTitle: result.data.DataList.SMSTitle,
                        SMS: result.data.DataList.SMS,
                        DepartmentId: result.data.DataList.DepartmentId,
                        DepartmentData: { Display: result.data.DataList.DepartmentName, Value: result.data.DataList.DepartmentId },
                    }
                    $scope.storage = angular.copy($scope.objSMSSpeech);
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
            $scope.objSMSSpeech = {
                SMSId: 0,
                SMSTitle: '',
                SMS: '',
                DepartmentId: '',
                DepartmentData: { Display: '', Value: '' },
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormSMSSpeechInfo)
                $scope.$parent.FormSMSSpeechInfo.$setPristine();

        }

        $scope.Create = function (data) {
            data.DepartmentId = data.DepartmentData.Value;
            data.Department = data.DepartmentData.Display;
            SMSSpeechService.AddSMSSpeech(data).then(function (result) {
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
            data.DepartmentId = data.DepartmentData.Value;
            data.Department = data.DepartmentData.Display;
            SMSSpeechService.AddSMSSpeech(data).then(function (result) {
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
            if ($scope.objSMSSpeech.SMSId > 0) {
                $scope.objSMSSpeech = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'SMSSpeechService', 'SMSSpeechController', 'id', 'isdisable']

})()


