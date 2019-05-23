(function () {
    "use strict"
    angular.module("CRMApp.Controllers")
            .controller("AgencyTypeCtrl", [
            "$scope", "$rootScope", "$timeout", "$filter", "AgencyTypeService", "NgTableParams", "$uibModal",
            function AgencyTypeCtrl($scope, $rootScope, $timeout, $filter, AgencyTypeService, NgTableParams, $uibModal) {

                $scope.Add = function (id, _isdisable) {
                    if (_isdisable === undefined) _isdisable = 0;
                   
                    var modalInstance = $uibModal.open({
                        backdrop: 'static',
                        templateUrl: 'myModalContent.html',
                        controller: ModalInstanceCtrl,
                        resolve: {
                            AgencyTypeService: function () { return AgencyTypeService; },
                            id: function () { return id; },
                            isdisable: function () { return _isdisable;}
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
                       //{ "title": "Agency Type Id", "field": "AgencyTypeId", filter: false, show: false },
                       { "title": "Sr.", "field": "RowNumber", filter: false, show: true },
                       { "title": "Agency Type", "field": "AgencyType", sortable: "AgencyType", filter: { AgencyType: "text" }, show: true },
                       {
                           "title": "Action", show: true,
                           'cellTemplte': function ($scope, row) {
                               var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.AgencyTypeId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                                    //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.$parent.$parent.$parent.Delete(row.AgencyTypeId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                                    '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.AgencyTypeId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                               return $scope.getHtml(element);
                           }
                       }
                    ],
                    Sort: { "AgencyTypeId": "asc" }
                }

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
                  
                    AgencyTypeService.DeleteAgencyType(id).then(function (result) {
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

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, AgencyTypeService, id, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.objAgencyType = $scope.objAgencyType || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.id = id;
        $scope.storage = {};

        if (id && id > 0) {
            AgencyTypeService.GetByIdAgencyType(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objAgencyType = {
                        AgencyTypeId: result.data.DataList.AgencyTypeId,
                        AgencyType: result.data.DataList.AgencyType
                    }
                    $scope.storage = angular.copy($scope.objAgencyType);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        function ResetForm() {
           
            $scope.objAgencyType = {
                AgencyTypeId: 0,
                AgencyType: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormAgencyTypeInfo)
                $scope.$parent.FormAgencyTypeInfo.$setPristine();
        }

        $scope.CreateUpdate = function (data) {
          
            AgencyTypeService.CreateUpdateAgencyType(data).then(function (result) {
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
           
            if ($scope.objAgencyType.AgencyTypeId > 0) {
                $scope.objAgencyType = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'AgencyTypeService', 'id','isdisable']

})()






