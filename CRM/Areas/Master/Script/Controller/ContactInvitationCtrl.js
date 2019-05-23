(function () {
    "use strict"
    angular.module("CRMApp.Controllers")
            .controller("ContactInvitationCtrl", [
            "$scope", "$rootScope", "$timeout", "$filter", "ContactInvitationService", "NgTableParams", "$uibModal",
            function ContactInvitationCtrl($scope, $rootScope, $timeout, $filter, ContactInvitationService, NgTableParams, $uibModal) {

                $scope.Add = function (id, _isdisable) {
                    if (_isdisable === undefined) _isdisable = 0;

                    var modalInstance = $uibModal.open({
                        backdrop: 'static',
                        templateUrl: 'myModalContent.html',
                        controller: ModalInstanceCtrl,
                        resolve: {
                            ContactInvitationService: function () { return ContactInvitationService; },
                            id: function () { return id; },
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
                       //{ "title": "Agency Type Id", "field": "AgencyTypeId", filter: false, show: false },
                       { "title": "Sr.", "field": "RowNumber", filter: false, show: true },
                       { "title": "Contact Invitation", "field": "ContactInvitation", sortable: "ContactInvitation", filter: { ContactInvitation: "text" }, show: true },
                       {
                           "title": "Action", show: true,
                           'cellTemplte': function ($scope, row) {
                               var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.ContactInvitationId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                                    //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.$parent.$parent.$parent.Delete(row.AgencyTypeId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                                    '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.ContactInvitationId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                               return $scope.getHtml(element);
                           }
                       }
                    ],
                    Sort: { "ContactInvitationId": "asc" }
                }

                //$scope.Edit = function (id) {

                //    $scope.Add(id,0);
                //}
                //$scope.View = function (id) {

                //    $scope.Add(id,1);
                //}

                $scope.Edit = function (id) {
                    $scope.Add(id, 0);
                    modalInstance.result.then(function () {
                        $scope.refreshGrid()
                    }, function () {
                    });
                }

                $scope.View = function (id) {
                    $scope.Add(id, 1);
                    modalInstance.result.then(function () {
                        $scope.refreshGrid()
                    }, function () {
                    });
                }


                $scope.Delete = function (id) {

                    ContactInvitationService.DeleteConInv(id).then(function (result) {
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

                $scope.RefreshTable = function () {
                    $scope.tableParams.reload();
                };

            }]);

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, ContactInvitationService, id, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.objConInv = $scope.objConInv || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.id = id;
        $scope.storage = {};

        if (id && id > 0) {
            ContactInvitationService.GetByIdConInv(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objConInv = {
                        ContactInvitationId: result.data.DataList.ContactInvitationId,
                        ContactInvitation: result.data.DataList.ContactInvitation
                    }
                    $scope.storage = angular.copy($scope.objConInv);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        function ResetForm() {

            $scope.objConInv = {
                ContactInvitationId: 0,
                ContactInvitation: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormAgencyTypeInfo)
                $scope.$parent.FormAgencyTypeInfo.$setPristine();
        }

        $scope.CreateUpdate = function (data) {

            ContactInvitationService.CreateUpdateConInv(data).then(function (result) {
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

            if ($scope.objConInv.ContactInvitationId > 0) {
                $scope.objConInv = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'ContactInvitationService', 'id', 'isdisable']

})()
