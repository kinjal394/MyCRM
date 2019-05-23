(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("HolidayController", [
               "$scope", "HolidayService", "ExhibitionService", "$filter", "$uibModal",
                function HolidayController($scope, HolidayService, ExhibitionService, $filter, $uibModal) {
                    $scope.storage = {};
                    $scope.AddHoliday = function () {
                        var _isdisable = 0;
                        $scope.HolidayObj = {
                            HolidayId: 0,
                            HolidayNameId: '',
                            CountryId: '',
                            OnDate: '',
                            //CountryIds: '',
                            StateIds: '',
                            ReligionsIds: '',
                        }
                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'holidayModalContent.html',
                            controller: 'HolidayModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                HolidayObj: function () {
                                    return $scope.HolidayObj;
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

                    $scope.setDirectiveRefresh = function (refreshGrid) {
                        $scope.refreshGrid = refreshGrid;
                    };

                    $scope.gridObj = {
                        columnsInfo: [
                           //{ "title": "HolidayId", "data": "HolidayId", filter: false, visible: false },
                           { "title": "Sr.", "field": "RowNumber", filter: false, show: true },
                           //{ "title": "HolidayNameId", "data": "HolidayNameId", filter: false, visible: false },
                           //{ "title": "Countrys", "field": "CountryId", filter: false, visible: false },
                           //{ "title": "States", "field": "StateIds", filter: false, visible: false },
                           //{ "title": "Religions", "data": "ReligionsIds", filter: false, visible: false },
                           { "title": "Country", "field": "CountryName", filter: { CountryName: "text" }, show: true, sortable: "CountryName" },
                           { "title": "State", "field": "StateName", filter: { StateName: "text" }, show: true, sortable: "StateName" },
                           {
                               "title": "Holiday Date", "field": "OnDate", sortable: "OnDate", filter: { OnDate: "date" }, show: true,
                               'cellTemplte': function ($scope, row) {
                                   var element = '<p ng-bind="ConvertDate(row.OnDate,\'dd/mm/yyyy\')">'
                                   return $scope.getHtml(element);
                               }
                           },
                           { "title": "Holiday Name", "field": "HolidayName", sortable: "HolidayName", filter: { HolidayName: "text" }, show: true, },
                             { "title": "Religion Name", "field": "ReligionName", sortable: "ReligionName", filter: { ReligionName: "text" }, show: true, },

                           {
                               "title": "Action", filter: false, show: true,
                               'cellTemplte': function ($scope, row) {
                                   var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                              //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.$parent.$parent.$parent.Delete(row.HolidayId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                              '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                                   return $scope.getHtml(element);
                               }
                           }
                        ],
                        Sort: { 'HolidayId': 'asc' }
                    }

                    $scope.Edit = function (data) {
                        var _isdisable = 0;
                        $scope.HolidayObj = {
                            HolidayId: data.HolidayId,
                            HolidayName: data.HolidayName,
                            CountryId: data.CountryId,
                            OnDate: $filter('mydate')(data.OnDate),
                            HolidayNameId: data.HolidayNameId,
                            StateIds: data.StateIds,
                            //CountryIds: data.CountryId,
                            ReligionIds: data.ReligionIds
                        }
                        $scope.storage = angular.copy($scope.HolidayObj);
                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'holidayModalContent.html',
                            controller: 'HolidayModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                HolidayObj: function () {
                                    return $scope.HolidayObj;
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

                    $scope.View = function (data) {
                        var _isdisable = 1;
                        $scope.HolidayObj = {
                            HolidayId: data.HolidayId,
                            HolidayName: data.HolidayName,
                            CountryId: data.CountryId,
                            OnDate: $filter('mydate')(data.OnDate),
                            HolidayNameId: data.HolidayNameId,
                            StateIds: data.StateIds,
                            //CountryIds: data.CountryId,
                            ReligionIds: data.ReligionIds
                        }
                        $scope.storage = angular.copy($scope.HolidayObj);
                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'holidayModalContent.html',
                            controller: 'HolidayModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                HolidayObj: function () {
                                    return $scope.HolidayObj;
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
                    $scope.Delete = function (id) {
                        HolidayService.DeleteHoliday(id).then(function (result) {
                            if (result.data.ResponseType == 1) {
                                toastr.success(result.data.Message)
                            }
                            else {
                                toastr.error(result.data.Message)
                            }
                            $scope.refreshGrid()
                        }, function (errorMsg) {
                            toastr.error(errorMsg, 'Opps, Something went wrong');
                        })
                    }
                }]);

    angular.module('CRMApp.Controllers')
        .controller('HolidayModalInstanceCtrl', [
            '$scope', '$uibModalInstance', 'HolidayObj', 'HolidayService', 'ExhibitionService', 'storage', 'isdisable',
            function ($scope, $uibModalInstance, HolidayObj, HolidayService, ExhibitionService, storage, isdisable) {
                $scope.isClicked = false;
                if (isdisable == 1) {
                    $scope.isClicked = true;
                }
                var $ctrl = this;

                $ctrl.ok = function () {
                    $uibModalInstance.close();
                };

                $ctrl.HolidayObj = HolidayObj;
                $ctrl.storage = storage;
                $ctrl.cancel = function () {
                    $uibModalInstance.dismiss('cancel');
                };


                gatdata();
                function gatdata() {
                    $ctrl.outputStates = [];
                    $ctrl.religionArray = [];
                    $ctrl.holidayArray = [];
                    $ctrl.stateArray = [];
                    $ctrl.outputCountry = [];
                    $ctrl.CountryArray = [];
                    $ctrl.outputReligions = [];

                    HolidayService.GetAllHolidayName().then(function (result) {
                        var res;
                        if (HolidayObj.HolidayNameId != undefined)
                            res = HolidayObj.HolidayNameId;
                        _.each(result.data, function (value, key, list) {
                            if (res != undefined && res == value.HolidayId) {
                                $ctrl.holidayArray.push({
                                    id: value.HolidayId,
                                    name: value.HolidayName,
                                    ticked: true
                                })
                            }
                            else {
                                $ctrl.holidayArray.push({
                                    id: value.HolidayId,
                                    name: value.HolidayName,
                                    ticked: false
                                })
                            }
                        })
                    }, function (errorMsg) {
                        toastr.error(errorMsg, 'Opps, Something went wrong');
                    })

                    HolidayService.GetAllReligion().then(function (result) {
                        var res;
                        if (HolidayObj.ReligionIds != undefined)
                            res = HolidayObj.ReligionIds.split("|");
                        _.each(result.data, function (value, key, list) {
                            if (res != undefined && res.includes(value.ReligionId.toString())) {
                                $ctrl.religionArray.push({
                                    name: value.ReligionId,
                                    maker: value.ReligionName,
                                    ticked: true
                                })
                            }
                            else {
                                $ctrl.religionArray.push({
                                    name: value.ReligionId,
                                    maker: value.ReligionName,
                                    ticked: false
                                })
                            }
                        })
                    }, function (errorMsg) {
                        toastr.error(errorMsg, 'Opps, Something went wrong');
                    })

                    ExhibitionService.getAllCountries().then(function (result) {
                        //$ctrl.countries = data.data;
                        var rescountry;
                        if (HolidayObj.CountryId != undefined)
                            rescountry = HolidayObj.CountryId;
                        _.each(result.data, function (value, key, list) {
                            if (rescountry != undefined && rescountry == value.CountryId) {
                                $ctrl.CountryArray.push({
                                    name: value.CountryId,
                                    maker: value.CountryName,
                                    ticked: true
                                })
                            }
                            else {
                                $ctrl.CountryArray.push({
                                    name: value.CountryId,
                                    maker: value.CountryName,
                                    ticked: false
                                })
                            }


                        })

                    }, function (errorMsg) {
                        toastr.error(errorMsg, 'Opps, Something went wrong');
                    })
                    //if (HolidayObj.CountryId) {
                    //    GetStates(HolidayObj.CountryId);
                    //}
                }

                $scope.$watch(function () { return $ctrl.outputCountry[0].name }, function (newValue, oldValue) { if (newValue && newValue > 0) { GetStates(newValue); } });
                function GetStates(countryId) {
                    $ctrl.stateArray = [];
                    ExhibitionService.getStateByID(countryId).then(function (result) {
                        var res;
                        if (HolidayObj.StateIds != undefined)
                            res = HolidayObj.StateIds.split("|");
                        _.each(result.data, function (value, key, list) {
                            if (res != undefined && res.includes(value.StateId.toString())) {
                                $ctrl.stateArray.push({
                                    name: value.StateId,
                                    maker: value.StateName,
                                    ticked: true
                                })
                            }
                            else {
                                $ctrl.stateArray.push({
                                    name: value.StateId,
                                    maker: value.StateName,
                                    ticked: false
                                })
                            }


                        })

                    }, function (errorMsg) {
                        toastr.error(errorMsg, 'Opps, Something went wrong');
                    })

                }

                $ctrl.SaveHoliday = function () {
                    var countryIds = '';
                    var stateIds = '';
                    var religionsIds = '';
                    var countrylength = $ctrl.outputCountry.length;
                    var statelength = $ctrl.outputStates.length;
                    var religionsLength = $ctrl.outputReligions.length;
                    _.each($ctrl.outputCountry, function (value, key, list) {
                        if (key < statelength - 1) {
                            countryIds += value.name + '|'
                        }
                        else if (key == statelength - 1) {
                            countryIds += value.name;
                        }

                    })
                    _.each($ctrl.outputStates, function (value, key, list) {
                        if (key < statelength - 1) {
                            stateIds += value.name + '|'
                        }
                        else if (key == statelength - 1) {
                            stateIds += value.name;
                        }

                    })
                    _.each($ctrl.outputReligions, function (value, key, list) {
                        if (key < religionsLength - 1) {
                            religionsIds += value.name + '|'
                        }
                        else if (key == religionsLength - 1) {
                            religionsIds += value.name;
                        }

                    })
                    $ctrl.HolidayObj.HolidayNameId = $ctrl.HolidayObj.HolidayName[0].id
                    $ctrl.HolidayObj.CountryId = countryIds.replace("|", "");
                    $ctrl.HolidayObj.StateIds = stateIds;
                    $ctrl.HolidayObj.ReligionIds = religionsIds;
                    HolidayService.AddHoliday($ctrl.HolidayObj).then(function (result) {
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


                $ctrl.dateOptions = {
                    formatYear: 'yy',
                    //minDate: new Date(2000, 8, 5),
                    //maxDate: new Date(2017, 5, 22),
                    startingDay: 1
                };

                $ctrl.UpdateHoliday = function () {
                    var countryIds = '';
                    var stateIds = '';
                    var religionsIds = '';
                    var countrylength = $ctrl.outputCountry.length;
                    var statelength = $ctrl.outputStates.length;
                    var length = $ctrl.outputStates.length;
                    var religionsLength = $ctrl.outputReligions.length;
                    _.each($ctrl.outputCountry, function (value, key, list) {
                        if (key < statelength - 1) {
                            countryIds += value.name + '|'
                        }
                        else if (key == statelength - 1) {
                            countryIds += value.name;
                        }

                    })
                    _.each($ctrl.outputStates, function (value, key, list) {
                        if (key < length - 1) {
                            stateIds += value.name + '|'
                        }
                        else if (key == length - 1) {
                            stateIds += value.name;
                        }

                    })
                    _.each($ctrl.outputReligions, function (value, key, list) {
                        if (key < religionsLength - 1) {
                            religionsIds += value.name + '|'
                        }
                        else if (key == religionsLength - 1) {
                            religionsIds += value.name;
                        }

                    })
                    $ctrl.HolidayObj.HolidayNameId = $ctrl.HolidayObj.HolidayName[0].id
                    $ctrl.HolidayObj.CountryId = countryIds.replace("|", "");
                    $ctrl.HolidayObj.StateIds = stateIds;
                    $ctrl.HolidayObj.ReligionIds = religionsIds;
                    HolidayService.UpdateHoliday($ctrl.HolidayObj).then(function (result) {
                        if (result.data.ResponseType == 1) {
                            $uibModalInstance.close();
                            toastr.success(result.data.Message)
                        }
                        else {
                            toastr.error(result.data.Message)
                        }
                        $uibModalInstance.close();
                    }, function (errorMsg) {
                        toastr.error(errorMsg, 'Opps, Something went wrong');
                    })
                }
                //Reporting();




                $ctrl.Reset = function () {

                    if ($ctrl.HolidayObj.HolidayId > 0) {
                        gatdata();
                        $ctrl.HolidayObj = angular.copy($ctrl.storage);
                        //Reporting();
                    } else {
                        ResetForm();
                    }
                }


                function ResetForm() {

                    $ctrl.HolidayObj = {
                        HolidayId: 0,
                        HolidayName: '',
                        ReligionId: 0,
                        ReligionName: { Display: '', Value: '' },
                        CountryId: 0,
                        CountryName: { Display: '', Value: '' },
                        TaskTypeId: 0,
                        TaskTypeName: { Display: '', Value: '' },
                        StateId: 0,
                        StateName: { Display: '', Value: '' },
                        HolidayNameId: 0,
                        OnDate: '',
                    }
                    gatdata();
                    $ctrl.storage = {};
                    $ctrl.addMode = true;
                    $ctrl.saveText = "Save";
                    if ($ctrl.$parent.HolidayFormInfo)
                        $ctrl.$parent.HolidayFormInfo.$setPristine();
                }



            }]);
})();