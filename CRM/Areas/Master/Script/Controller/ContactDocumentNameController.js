(function () {
    "User strict";
    angular.module("CRMApp.Controllers")
         .controller("ContactDocumentNameController", [
             "$scope", "ContactDocumentNameService", "$uibModal",
             ContactDocumentNameController]);

    function ContactDocumentNameController($scope, ContactDocumentNameService, $uibModal) {
        $scope.storage = {};
        $scope.id = 0;

        $scope.Add = function (data) {
            _isdisable = 0
            var objdata = {
                ContactDocId: 0,
                ContactDocName: '',
            };
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    ContactDocumentNameService: function () { return ContactDocumentNameService; },
                    ContactDocumentNameController: function () { return ContactDocumentNameController; },
                    objdata: function () { return objdata; },
                    isdisable: function () { return _isdisable; }
                },
                storage: function () {
                    return $scope.storage;
                }
            });
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            });
        }


        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };

        $scope.gridObj = {
            columnsInfo: [
               //{ "title": "ContactDocId", "data": "ContactDocId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true },
               { "title": "Contact Document Name", "field": "ContactDocName", sortable: "ContactDocName", filter: { ContactDocName: "text" }, show: true },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                           //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.TOTypeId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                  '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       //'render': '<button  data-ng-click="$parent.$parent.$parent.Edit(row.SourceId)">Edit</button><button data-ng-click="$parent.$parent.$parent.Delete(row.SourceId)">Delete</button> '
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'ContactDocId': 'asc' }

        }

        $scope.Edit = function (data) {
            _isdisable = 0;
            var objdata = {
                ContactDocId: data.ContactDocId,
                ContactDocName: data.ContactDocName,
            };
            $scope.storage = angular.copy($scope.objdata);
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    ContactDocumentNameService: function () { return ContactDocumentNameService; },
                    ContactDocumentNameController: function () { return $scope; },
                    objdata: function () { return objdata; },
                    isdisable: function () { return _isdisable; }
                },
                storage: function () {
                    return $scope.storage;
                }

            });
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
            });
        }


        $scope.View = function (data) {
            _isdisable = 1;
            var objdata = {
                ContactDocId: data.ContactDocId,
                ContactDocName: data.ContactDocName,
            };
            $scope.storage = angular.copy($scope.objdata);
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    ContactDocumentNameService: function () { return ContactDocumentNameService; },
                    ContactDocumentNameController: function () { return $scope; },
                    objdata: function () { return objdata; },
                    isdisable: function () { return _isdisable; }
                },
                storage: function () {
                    return $scope.storage;
                }

            });
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
            });
        }

        $scope.Delete = function (data) {
            ContactDocumentNameService.DeleteContactDocumentName(data).then(function (result) {
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

        $scope.RefreshTable = function () {
            $scope.tableParams.reload();
        };
    }

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, ContactDocumentNameService, ContactDocumentNameController, objdata, isdisable, storage) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.objTO = $scope.objTO || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};

        if (objdata.ContactDocId && objdata.ContactDocId > 0) {
            $scope.objTO = {
                ContactDocId: objdata.ContactDocId,
                ContactDocName: objdata.ContactDocName
            }
            $scope.storage = angular.copy($scope.objTO);
        } else {
            ResetForm();
        }


        $scope.focusMe = function (value) {
            if (value === true) {
                element[0].focus();
            }
        };


        function ResetForm() {
            $scope.objTO = {
                ContactDocId: 0,
                ContactDocName: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormTOInfo)
                $scope.$parent.FormTOInfo.$setPristine();
        }

        $scope.Create = function (data) {
            var TO = {
                ContactDocName: data.ContactDocName
            }
            ContactDocumentNameService.AddContactDocumentName(TO).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    ResetForm();
                    $uibModalInstance.close();
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.Update = function (data) {
            var TO = {
                ContactDocId: data.ContactDocId,
                ContactDocName: data.ContactDocName
            }
            ContactDocumentNameService.AddContactDocumentName(TO).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    $uibModalInstance.close();
                    ResetForm();
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
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'ContactDocumentNameService', 'ContactDocumentNameController', 'objdata', 'isdisable']


})()