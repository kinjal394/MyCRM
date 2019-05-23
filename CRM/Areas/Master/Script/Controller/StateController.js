(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("StateController", [
         "$scope", "StateService", "$uibModal",
         StateController]);

    function StateController($scope, StateService, $uibModal) {
        $scope.id = 0;
        $scope.Add = function () {
            var _isdisable = 0;
            var objStateData = {
                StateId: 0,
                StateName: '',
                CountryId: 0,
                CountryName: { Display: '', Value: '' },

            };

            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    StateService: function () { return StateService; },
                    StateController: function () { return StateController; },
                    objStateData: function () { return objStateData; },
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
               //{ "title": "StateId", "data": "StateId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "Country Name", "field": "CountryName", sortable: "CountryName", filter: { CountryName: "text" }, show: true, },
               { "title": "State Name", "field": "StateName", sortable: "StateName", filter: { StateName: "text" }, show: true, },
               //{ "title": "CountryId", "data": "CountryId", filter: false, visible: false },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                           //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.StateId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                            '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'StateId': 'asc' }
        }

        $scope.Edit = function (data) {
            var _isdisable = 0;
            var objStateData = {
                CountryId: data.CountryId,
                StateId: data.StateId,
                StateName: data.StateName,
                CountryName: { Display: data.CountryName, Value: data.CountryId },
            }
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    StateService: function () { return StateService; },
                    StateController: function () { return StateController; },
                    objStateData: function () { return objStateData; },
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
            var objStateData = {
                CountryId: data.CountryId,
                StateId: data.StateId,
                StateName: data.StateName,
                CountryName: { Display: data.CountryName, Value: data.CountryId },
            }
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    StateService: function () { return StateService; },
                    StateController: function () { return StateController; },
                    objStateData: function () { return objStateData; },
                    isdisable: function () { return _isdisable; }
                }
            });
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
            });
        }
        $scope.Delete = function (data) {
            StateService.DeleteState(data).then(function (result) {
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

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, StateService, StateController, objStateData, isdisable, CityService) {

        $scope.$watch("objstate.CountryName", function (data) {
            //if (newValue == 0) {
            if (data == '' || data == undefined) {
                $scope.CountryBind('India');
            }
            //}
        });

        $scope.CountryBind = function (data) {
            CityService.CountryBind().then(function (result) {
                if (result.data.ResponseType == 1) {
                    _.each(result.data.DataList, function (value, key, list) {
                        if (value.CountryName.toString().toLowerCase() == data.toString().toLowerCase()) {
                            $scope.objstate.CountryName = {
                                Display: value.CountryName,
                                Value: value.CountryId
                            };
                            return false;
                        }
                    });
                }
                else if (result.data.ResponseType == 3) {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.objstate = $scope.objstate || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};
        $scope.callback = function (data) {
            console.log(data);
        }


        if (objStateData.StateId && objStateData.StateId > 0) {
            $scope.objstate = {
                StateId: objStateData.StateId,
                StateName: objStateData.StateName,
                CountryId: objStateData.CountryId,
                CountryName: { Display: objStateData.CountryName.Display, Value: objStateData.CountryId }
            }
            $scope.storage = angular.copy($scope.objstate);
            //$scope.objCategory = result.data.DataList.CategoryName;
        } else {
            //toastr.error(objstate.data.Message, 'Opps, Something went wrong');
            ResetForm();
        }

        function ResetForm() {
            $scope.objstate = {
                StateId: 0,
                StateName: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormStateInfo)
                $scope.$parent.FormStateInfo.$setPristine();

        }

        $scope.Create = function (data) {
            var objinscountry = {
                CountryId: data.CountryName.Value,
                StateName: data.StateName
            }
            StateService.AddState(objinscountry).then(function (result) {
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
            var objinscountry = {
                CountryId: data.CountryName.Value,
                StateName: data.StateName,
                StateId: data.StateId
            }
            StateService.AddState(objinscountry).then(function (result) {
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
            if ($scope.objstate.StateId > 0) {
                $scope.objstate = angular.copy($scope.storage);
            } else {
                ResetForm();
            }

        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'StateService', 'StateController', 'objStateData', 'isdisable', 'CityService']

})()


