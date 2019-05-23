(function () {
    "use strict";
    debugger;
    angular.module("CRMApp.Controllers")
           .controller("ChatNameController", [
               "$scope", "ChatNameService", "$uibModal",
               ChatNameController]);

    function ChatNameController($scope, ChatNameService, $uibModal) {
        $scope.storage = {};
        $scope.id = 0;

        $scope.Add = function (data) {
            var _isdisable = 0;
            var objdepartdata = {
                ChatId: 0,
                ChatName: '',
            };
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    ChatNameService: function () { return ChatNameService; },
                    ChatNameController: function () { return ChatNameController; },
                    objdepartdata: function () { return objdepartdata; },
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
               //{ "title": "ChatId", "field": "ChatId", filter: false, show: true },
               { "title": "Sr.", "field": "RowNumber", filter: false, show: true },
               { "title": "Chat Name", "field": "ChatName", sortable: "ChatName", filter: { ChatName: "text" }, show: true },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                                    //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.$parent.$parent.$parent.Delete(row.ChatId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                                   '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>';
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'ChatId': 'asc' }
        }

        $scope.Edit = function (data, _isdisable) {
            if (_isdisable===undefined) _isdisable = 0;
            var objdepartdata = {
                ChatId: data.ChatId,
                ChatName: data.ChatName,
            };
            $scope.storage = angular.copy($scope.objdepartdata);
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    ChatNameService: function () { return ChatNameService; },
                    ChatNameController: function () { return $scope; },
                    objdepartdata: function () { return objdepartdata; },
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
            $scope.Edit(data, 1)
           
        }

        $scope.Delete = function (data) {
            ChatNameService.DeleteChatName(data).then(function (result) {
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


    var ModalInstanceCtrl = function ($scope, $uibModalInstance, ChatNameService, ChatNameController, objdepartdata, isdisable, storage) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.objdepart = $scope.objdepart || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};

        if (objdepartdata.ChatId && objdepartdata.ChatId > 0) {
            $scope.objchat = {
                ChatId: objdepartdata.ChatId,
                ChatName: objdepartdata.ChatName
            }
            $scope.storage = angular.copy($scope.objdepart);
        } else {
            ResetForm();
        }

        function ResetForm() {
            $scope.objchat = {
                ChatId: 0,
                ChatName: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormChatInfo)
                $scope.$parent.FormChatInfo.$setPristine();
        }

        $scope.Create = function (data) {
            var chat = {
                ChatName: data.ChatName
            }
            ChatNameService.AddChatName(chat).then(function (result) {
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

        $scope.Update = function (data) {
            var chat = {
                ChatId: data.ChatId,
                ChatName: data.ChatName
            }
            ChatNameService.AddChatName(chat).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    $uibModalInstance.close();
                    ResetForm();
                } else {
                    toastr.error(result.data.Message);
                }
               
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.Reset = function () {
            if ($scope.objchat.ChatId > 0) {
                $scope.objchat = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'ChatNameService', 'ChatNameController', 'objdepartdata', 'isdisable']
})()