(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("ExhibitionController", [
         "$scope", "ExhibitionService", "$uibModal", "$filter", "CountryService",
            function ExhibitionController($scope, ExhibitionService, $uibModal, $filter, CountryService) {
                $scope.storage = {};
                $scope.gridObj = {
                    columnsInfo: [
                       //{ "title": "ExId", "data": "ExId", filter: false, visible: false },
                        { "title": "Sr.", "field": "RowNumber", show: true, },
                       //{ "title": "City Id", "data": "CityId", filter: false, visible: false },
                       //{ "title": "State Id", "data": "StateId", filter: false, visible: false },
                       //{ "title": "Country Id", "data": "CountryId", filter: false, visible: false },
                       //{ "title": "Area Id", "data": "AreaId", filter: false, visible: false },
                       //{ "title": "Area Name", "field": "AreaName", sortable: "AreaName", filter: { AreaName: "text" }, show: false, },
                       //{ "title": "IsActive", "data": "IsActive", filter: false, visible: false },
                       //{ "title": "Created By", "data": "CreatedBy", filter: false, visible: false },
                       { "title": "Exhibition Name", "field": "ExName", sortable: "ExName", filter: { ExName: "text" }, show: true, },
                       { "title": "Country", "field": "CountryName", sortable: "CountryName", filter: { CountryName: "text" }, show: true, },
                       { "title": "State", "field": "StateName", sortable: "StateName", filter: { StateName: "text" }, show: true, },
                       { "title": "City", "field": "CityName", sortable: "CityName", filter: { CityName: "text" }, show: true, },
                       {
                           "title": "Venue/Address", "field": "Venue", sortable: "Venue", filter: { Venue: "text" }, show: true,
                           'cellTemplte': function ($scope, row) {
                               var element = '<span data-uib-tooltip="{{row.Address}}">{{row.Venue}}</br>{{LimitString(row.Address,100)}}</span>'
                               return $scope.getHtml(element);
                           }
                       },
                       //{ "title": "No. of Years", "data": "NoofYears", sort: true, filter: true, },
                       { "title": "Exhibition Profile", "field": "ExProfile", sortable: "ExProfile", filter: { ExProfile: "text" }, show: true, },
                       { "title": "Exhibition Month", "field": "Month", sortable: "Month", filter: { Month: "text" }, show: true, },
                       //{ "title": "Organizer Detail", "data": "OrganizerDetail", sort: true, filter: true, },
                       {
                           "title": "Organizer Detail", "field": "OrganizerDetail", sortable: "OrganizerDetail", filter: { OrganizerDetail: "text" }, show: false,
                           'cellTemplte': function ($scope, row) {
                               var element = '<p data-uib-tooltip="{{row.OrganizerDetail}}" ng-bind="LimitString(row.OrganizerDetail,100)">'
                               return $scope.getHtml(element);
                           }
                       },
                       //{ "title": "Bank Detail", "data": "BankDetail", sort: true, filter: true, },
                       //{
                       //    "title": "Bank Detail", "data": "BankDetail", sort: true, filter: true,
                       //    "render": '<p data-uib-tooltip="{{row.BankDetail}}" ng-bind="LimitString(row.BankDetail,100)">'
                       //},
                       //{ "title": "Address", "data": "Address", sort: true, filter: true, },
                       {
                           "title": "Address", "field": "Address", sortable: "Address", filter: { Address: "text" }, show: false,
                           'cellTemplte': function ($scope, row) {
                               var element = '<p data-uib-tooltip="{{row.Address}}" ng-bind="LimitString(row.Address,100)">'
                               return $scope.getHtml(element);
                           }
                       },
                       //{ "title": "District", "data": "CityName", sort: true, filter: false, visible: false, },
                       { "title": "Tel", "field": "Tel", sortable: "Tel", filter: { Tel: "text" }, show: false, },
                       //{ "title": "TelId", "data": "TelId", filter: false, visible: false },
                       { "title": "MobileNo", "field": "MobileNo", sortable: "MobileNo", filter: { MobileNo: "text" }, show: false, },
                       //{ "title": "MobileId", "data": "MobileId", filter: false, visible: false },
                       { "title": "Email", "field": "Email", sortable: "Email", filter: { Email: "text" }, show: false, },
                       { "title": "Web", "field": "Web", sortable: "Web", filter: { Web: "text" }, show: false, },
                       { "title": "ContactPerson", "field": "ContactPerson", sortable: "ContactPerson", filter: { ContactPerson: "text" }, show: false, },
                       { "title": "Chat", "field": "Chat", sortable: "Chat", filter: { Chat: "text" }, show: false, },
                       //{ "title": "Created Date", "data": "CreatedDate", sort: true, filter: true, visible: false, "render": '<p ng-bind="ConvertDate(row.CreatedDate,\'dd/mm/yyyy\')">' },
                       {
                           "title": "Exhibition Date", "field": "ExDate", sortable: "ExDate", filter: { ExDate: "date" }, show: false,
                           'cellTemplte': function ($scope, row) {
                               var element = '<p ng-bind="ConvertDate(row.ExDate,\'dd/mm/yyyy\')">'
                               return $scope.getHtml(element);
                           }
                       },
                       {
                           "title": "Action", show: true,
                           'cellTemplte': function ($scope, row) {
                               var element ='<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                          //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.ExId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                           '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                               return $scope.getHtml(element);
                           }
                       }
                    ],
                    Sort: { 'ExId': 'asc' }
                }
               
                $scope.Add = function () {
                    var _isdisable = 0;
                    $scope.exhibitionObj = {
                        ExId: 0,
                        ExName: '',
                        CityId: '',
                        StateId: '',
                        CountryId: '',
                        AreaId: 0,
                       // AreaName: '',
                        Venue: '',
                        NoofYears: '',
                        ExProfile: '',
                        OrganizerDetail: '',
                        BankDetail: '',
                        Address: '',
                        ExDate: '',
                        TelId: '',
                        TelIdCode: '',
                        Tel: '',
                        MobileId: '',
                        MobileIdCode: '',
                        MobileNo: '',
                        Email: '',
                        Web: '',
                        ContactPerson: '',
                        Chat: '',
                        outputMobCode: [],
                        outputTelCode: [],
                        CountryName: { Display: '', Value: '' },
                        StateName: { Display: '', Value: '' },
                        CityName: { Display: '', Value: '' },
                        AreaName: { Display: '', Value: '' },

                    }
                    $scope.MobCodeArray = [];
                    $scope.TelCodeArray = [];
                    var modalInstance = $uibModal.open({
                        backdrop: 'static',
                        animation: true,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: 'myModalContent.html',
                        controller: 'ModalInstanceCtrl',
                        controllerAs: '$ctrl',
                        size: 'lg',
                        resolve: {
                            exhibitionObj: function () {
                                return $scope.exhibitionObj;
                            },
                            storage: function () {
                                return $scope.storage;
                            },
                            CountryService:function(){
                                return CountryService;
                            },
                            isdisable: function () { return _isdisable; }
                        }
                    })
                    modalInstance.result.then(function (selectedItem) {
                        $scope.refreshGrid();
                    }, function () {
                    });
                }

                $scope.Edit = function (row) {
                    var _isdisable = 0;
                    var mob = row.MobileNo.split(' ');
                    var tel = row.Tel.split(' ');
                    $scope.exhibitionObj = {
                        ExId: row.ExId,
                        ExName: row.ExName,
                        CityId: row.CityId,
                        StateId: row.StateId,
                        CountryId: row.CountryId,
                        AreaId: row.AreaId,
                        Venue: row.Venue,
                        NoofYears: row.NoofYears,
                        ExProfile: row.ExProfile,
                        OrganizerDetail: row.OrganizerDetail,
                        BankDetail: row.BankDetail,
                        Address: row.Address,
                        ExDate: $filter('mydate')(row.ExDate),
                        Tel: tel[1],
                        TelIdCode: tel[0],
                        TelId:row.TelId,
                        MobileId: row.MobileId,
                        MobileIdCode: mob[0],
                        MobileNo: mob[1],
                        Email: row.Email,
                        Web: row.Web,
                        ContactPerson: row.ContactPerson,
                        Chat: row.Chat,
                        CountryName: { Display: row.CountryName, Value: row.CountryId },
                        StateName: { Display: row.StateName, Value: row.StateId },
                        CityName: { Display: row.CityName, Value: row.CityId },
                        AreaName: { Display: row.AreaName, Value: row.AreaId },

                    }
                    $scope.storage = angular.copy($scope.exhibitionObj);
                    var modalInstance = $uibModal.open({
                        backdrop: 'static',
                        animation: true,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: 'myModalContent.html',
                        controller: 'ModalInstanceCtrl',
                        controllerAs: '$ctrl',
                        size: 'lg',
                        resolve: {
                            exhibitionObj: function () {
                                return $scope.exhibitionObj;
                            },
                            storage: function () {
                                return $scope.storage;
                            },
                            CountryService: function () {
                                return CountryService;
                            },
                            isdisable: function () { return _isdisable; }
                        }
                    });
                    modalInstance.result.then(function (selectedItem) {
                        $scope.refreshGrid();
                    }, function () {
                    });
                }

                $scope.View = function (row) {
                    var _isdisable = 1;
                    var mob = row.MobileNo.split(' ');
                    var tel = row.Tel.split(' ');
                    $scope.exhibitionObj = {
                        ExId: row.ExId,
                        ExName: row.ExName,
                        CityId: row.CityId,
                        StateId: row.StateId,
                        CountryId: row.CountryId,
                        AreaId: row.AreaId,
                        Venue: row.Venue,
                        NoofYears: row.NoofYears,
                        ExProfile: row.ExProfile,
                        OrganizerDetail: row.OrganizerDetail,
                        BankDetail: row.BankDetail,
                        Address: row.Address,
                        ExDate: $filter('mydate')(row.ExDate),
                        Tel: tel[1],
                        TelIdCode: tel[0],
                        TelId: row.TelId,
                        MobileId: row.MobileId,
                        MobileIdCode: mob[0],
                        MobileNo: mob[1],
                        Email: row.Email,
                        Web: row.Web,
                        ContactPerson: row.ContactPerson,
                        Chat: row.Chat,
                        CountryName: { Display: row.CountryName, Value: row.CountryId },
                        StateName: { Display: row.StateName, Value: row.StateId },
                        CityName: { Display: row.CityName, Value: row.CityId },
                        AreaName: { Display: row.AreaName, Value: row.AreaId },

                    }
                    $scope.storage = angular.copy($scope.exhibitionObj);
                    var modalInstance = $uibModal.open({
                        backdrop: 'static',
                        animation: true,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: 'myModalContent.html',
                        controller: 'ModalInstanceCtrl',
                        controllerAs: '$ctrl',
                        size: 'lg',
                        resolve: {
                            exhibitionObj: function () {
                                return $scope.exhibitionObj;
                            },
                            storage: function () {
                                return $scope.storage;
                            },
                            CountryService: function () {
                                return CountryService;
                            },
                            isdisable: function () { return _isdisable; }
                        }
                    });
                    modalInstance.result.then(function (selectedItem) {
                        $scope.refreshGrid();
                    }, function () {
                    });
                }
                $scope.setDirectiveRefresh = function (refreshGrid) {
                    $scope.refreshGrid = refreshGrid;
                };
               
                $scope.Delete = function (ExId) {
                    ExhibitionService.DeleteExhibition(ExId).then(function (result) {
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

                $scope.dateOptions = {
                    formatYear: 'yy',
                    //minDate: new Date(2016, 8, 5),
                    //maxDate: new Date(2017, 5, 22),
                    startingDay: 1
                };

            }]);

    angular.module('CRMApp.Controllers').controller('ModalInstanceCtrl', [
        '$scope', '$uibModalInstance', 'exhibitionObj', 'ExhibitionService', 'storage', 'isdisable', "CountryService","CityService",
        function ($scope, $uibModalInstance, exhibitionObj, ExhibitionService, storage, isdisable, CountryService, CityService) {

            $scope.$watch('$ctrl.exhibitionObj.CountryName', function (dataa) {
                if (dataa.Value != '') {
                    if (dataa.Value != exhibitionObj.CountryId.toString()) {
                        $ctrl.exhibitionObj.StateName.Display = '';
                        $ctrl.exhibitionObj.StateName.Value = '';
                        $ctrl.exhibitionObj.CityName.Display = '';
                        $ctrl.exhibitionObj.CityName.Value = '';

                    }
                } else {
                    $scope.CountryBind('India');
                }
                
            }, true)

            $scope.$watch('$ctrl.exhibitionObj.StateName', function (dataa) {
                if (dataa.Value != exhibitionObj.StateId.toString()) {
                    $ctrl.exhibitionObj.CityName.Display = '';
                    $ctrl.exhibitionObj.CityName.Value = '';

                }
            }, true)


            $scope.CountryBind = function (data) {
                CityService.CountryBind().then(function (result) {
                    if (result.data.ResponseType == 1) {
                        _.each(result.data.DataList, function (value, key, list) {
                            if (value.CountryName.toString().toLowerCase() == data.toString().toLowerCase()) {
                                $ctrl.exhibitionObj.CountryName = {
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


            var $ctrl = this;

            $ctrl.exhibitionObj = exhibitionObj;
            $ctrl.storage = storage;

            $ctrl.State = exhibitionObj.StateName.Value;
            $ctrl.country = exhibitionObj.CountryName.Value;
            $ctrl.city = exhibitionObj.CityName.Value;
            $ctrl.area = exhibitionObj.AreaName.Value;
            $scope.MobCodeArray = [];
            $scope.TelCodeArray = [];
            CountryService.GetCountryFlag().then(function (result) {
                //$scope.telArray2 = angular.copy(result);
                //$scope.telArray1 = angular.copy(result);
                //$scope.telArray3 = angular.copy(result);
                var res;
                var refres;
                var rescont;
                _.each(result, function (value, key, list) {
                    if ($ctrl.exhibitionObj.TelId != undefined) {
                        res = $ctrl.exhibitionObj.TelId;
                        if (res != undefined && res == value.id) {
                            $scope.TelCodeArray.push({
                                code: value.code,
                                icon: value.icon,
                                id: value.id,
                                name: value.name,
                                ticked: true
                            })
                        }
                        else {
                            var val;
                            if (res != "") {
                                val = false;
                            } else {
                                val = value.ticked;
                            }
                            $scope.TelCodeArray.push({
                                code: value.code,
                                icon: value.icon,
                                id: value.id,
                                name: value.name,
                                ticked: val
                            })
                        }
                    };
                    if ($ctrl.exhibitionObj.MobileId == null || $ctrl.exhibitionObj.MobileId != undefined) {
                        refres = $ctrl.exhibitionObj.MobileId;
                        if (refres != undefined && refres == value.id) {
                            $scope.MobCodeArray.push({
                                code: value.code,
                                icon: value.icon,
                                id: value.id,
                                name: value.name,
                                ticked: true
                            })
                        }
                        else {
                            var rval;
                            if (refres != "") {
                                rval = false;
                            } else {
                                rval = value.ticked;
                            }
                            $scope.MobCodeArray.push({
                                code: value.code,
                                icon: value.icon,
                                id: value.id,
                                name: value.name,
                                ticked: rval
                            })
                        }
                    };
                });
            });
            ExhibitionService.getAllCountries().then(function (data) {
                $ctrl.CountryName = { Display: data.CountryName, Value: data.CountryId };
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })

            $scope.$watch(function () { return $ctrl.CountryName }, function (newval, oldval) {
                if (newval.Value && newval.Value > 0) {
                    ExhibitionService.getStateByID(newval.Value).then(function (result) {
                        $ctrl.StateName.Value = result.data;
                    }, function (errorMsg) {
                        toastr.error(errorMsg, 'Opps, Something went wrong');
                    })
                }
            })
            $scope.$watch(function () { return $ctrl.StateName }, function (newval, oldval) {
                if (newval.Value && newval.Value > 0) {
                    ExhibitionService.getCityByID(newval.Value).then(function (result) {
                        $ctrl.CityName.Value = result.data;
                    }, function (errorMsg) {
                        toastr.error(errorMsg, 'Opps, Something went wrong');
                    })
                }
            })
            $scope.$watch(function () { return $ctrl.CityName }, function (newval, oldval) {
                if (newval.Value && newval.Value > 0) {
                    ExhibitionService.getAreaByID($ctrl.CityName.Value).then(function (result) {
                        $ctrl.AreaName.Value = result.data;
                    }, function (errorMsg) {
                        toastr.error(errorMsg, 'Opps, Something went wrong');
                    })
                }
            })
            function GetStates() {
                ExhibitionService.getStateByID($ctrl.CountryName.Value).then(function (data) {
                    $ctrl.StateName.Value = data.data;
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }

            $ctrl.GetStates = function () {
                GetStates();
            }
            function GetCities() {
                ExhibitionService.getCityByID($ctrl.StateName.Value).then(function (data) {
                    $ctrl.city = data.data;
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }
            $ctrl.GetCity = function () {
                GetCities();
            }
            $ctrl.getArea = function () {
                ExhibitionService.getAreaByID($ctrl.CityName.Value).then(function (data) {
                    $ctrl.AreaName.Value = data.data;
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }

            $ctrl.exhibitionSave = function (exhibitionData) {
                exhibitionData.AreaId = exhibitionData.AreaName.Value
                exhibitionData.CountryId = exhibitionData.CountryName.Value
                exhibitionData.StateId = exhibitionData.StateName.Value
                exhibitionData.CityId = exhibitionData.CityName.Value
                exhibitionData.MobileNo = exhibitionData.outputMobCode[0].code + ' ' + exhibitionData.MobileNo
                exhibitionData.Tel = exhibitionData.outputTelCode[0].code + ' ' + exhibitionData.Tel
                ExhibitionService.saveExhibition(exhibitionData).then(function (result) {
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
            $ctrl.Update = function (exhibitionData) {
                exhibitionData.AreaId = exhibitionData.AreaName.Value
                exhibitionData.CountryId = exhibitionData.CountryName.Value
                exhibitionData.StateId = exhibitionData.StateName.Value
                exhibitionData.CityId = exhibitionData.CityName.Value
                exhibitionData.MobileNo = exhibitionData.outputMobCode[0].code + ' ' + exhibitionData.MobileNo
                exhibitionData.Tel = exhibitionData.outputTelCode[0].code + ' ' + exhibitionData.Tel
                ExhibitionService.saveExhibition(exhibitionData).then(function (result) {
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

                if ($ctrl.exhibitionObj.ExId > 0) {

                    $ctrl.exhibitionObj = angular.copy($ctrl.storage);

                } else {
                    ResetForm();
                }
            }


            function ResetForm() {

                $ctrl.exhibitionObj = {
                    ExId: 0,
                    ExName: '',
                    CityId: '',
                    StateId: '',
                    CountryId: '',
                    AreaId: 0,
                    //AreaName: '',
                    Venue: '',
                    NoofYears: '',
                    ExProfile: '',
                    OrganizerDetail: '',
                    BankDetail: '',
                    Address: '',
                    ExDate: '',
                    TelId: '',
                    TelIdCode: '',
                    Tel: '',
                    MobileId: '',
                    MobileIdCode: '',
                    MobileNo: '',
                    Email: '',
                    Web: '',
                    ContactPerson: '',
                    Chat: '',
                    CountryName: '',
                    StateName: '',
                    CityName: '',
                    AreaName: '',
                }
                $ctrl.storage = {};
                $ctrl.addMode = true;
                $ctrl.saveText = "Save";
                if ($ctrl.$parent.FormAreaInfo)
                    $ctrl.$parent.FormAreaInfo.$setPristine();
            }


            $ctrl.ok = function () {
                $uibModalInstance.dismiss();
            };
            $ctrl.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };

            $ctrl.dateOptions = {
                formatYear: 'yy',
                //minDate: new Date(2016, 8, 5),
                //maxDate: new Date(2017, 5, 22),
                startingDay: 1
            };

        }])

    angular.module('CRMApp.Controllers').filter("mydate", function () {
        var re = /\/Date\(([0-9]*)\)\//;
        return function (x) {
            var m = x.match(re);
            if (m) return new Date(parseInt(x.substr(6)));
            else return null;
        };
    });
})()
