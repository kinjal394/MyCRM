(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
            .controller("InquiryCtrl", [
             "$scope", "$rootScope", "$timeout", "$filter", "InquiryService", "CountryService", "SalesOrderService", "TaskService", "NgTableParams", "$uibModal", "CityService",
             InquiryCtrl
            ]);

    function InquiryCtrl($scope, $rootScope, $timeout, $filter, InquiryService, CountryService, SalesOrderService, TaskService, NgTableParams, $uibModal, CityService) {
        $scope.objInquiry = $scope.objInquiry || {};
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};
        //InqId, InqNo, InqDate, SourceId, BuyerName, MobileNo, Email, CityId, Requirement, Address, CreatedBy, Status, Remark, IsActive
        $scope.objInquiry = {
            InqId: 0,
            InqNo: '',
            InqDate: new Date(),
            SourceId: 0,
            SourceName: '',
            BuyerName: '',
            //ContactId: '',
            //ContactPersonData:'',
            CompanyName: '',
            ContactPersonData: { Display: '', Value: '' },
            ContactPersonname: '',
            MobileNo: '',
            Email: '',
            CityId: 0,
            CityName: '',
            StateId: 0,
            StateName: '',
            CountryId: 0,
            CountryName: '',
            Requirement: '',
            Address: '',
            StatusId: 0,
            StatusName: '',
            Remark: '',
            AssignTo: 0,
            SourceData: { Display: '', Value: '' },
            CountryData: { Display: '', Value: '' },
            StateData: { Display: '', Value: '' },
            CityData: { Display: '', Value: '' },
            StatusData: { Display: '', Value: '' },
            BuyerType: 1,
            SubjectType: 1,
            Subjectlist: '',
            Subject: '',
            BuyerData: { Display: '', Value: '' },
        };
        $scope.telCodeData = [];
        $scope.SetInquiryId = function (id, isdisable) {
            CountryService.GetCountryFlag().then(function (result) {
                $scope.telCodeData = angular.copy(result);
                if (id > 0) {
                    //edit
                    $scope.SrNo = id;
                    $scope.addMode = false;
                    $scope.saveText = "Update";
                    $scope.GetAllInquiryInfoById(id);
                    $scope.isClicked = false;
                    if (isdisable == 1) {
                        $scope.isClicked = true;
                    }
                } else {
                    //add
                    $scope.SrNo = 0;
                    $scope.addMode = true;
                    $scope.saveText = "Save";
                    $scope.GetInvoice();
                    $scope.isClicked = false;
                    $scope.objInquiry.BuyerType = 1;
                    $scope.objInquiry.SubjectType = 1;
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
            //$scope.GetCountryFlag();
        }

        $scope.GetAllInquiryInfoById = function (id) {
            InquiryService.GetAllInquiryInfoById(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    var objInquiryMaster = result.data.DataList.objInquiryMaster;
                    debugger;
                    $scope.teliphone = (objInquiryMaster.MobileNo != '') ? objInquiryMaster.MobileNo.split(",") : [];
                    $scope.mail = (objInquiryMaster.Email != '') ? objInquiryMaster.Email.split(",") : [];
                    $scope.objInquiry = {
                        InqId: objInquiryMaster.InqId,
                        InqNo: objInquiryMaster.InqNo,
                        InqDate: $filter('mydate')(objInquiryMaster.InqDate),
                        //InqDate: new Date(),
                        SourceId: objInquiryMaster.SourceId,
                        SourceName: objInquiryMaster.SourceName,
                        BuyerName: objInquiryMaster.BuyerName,
                        //ContactId:objInquiryMaster.ContactId,
                        BuyerData: { Display: objInquiryMaster.BuyerName, Value: '' },
                        CompanyName: objInquiryMaster.CompanyName,
                        ContactPersonname: objInquiryMaster.ContactPersonname,
                        ContactPersonData: { Display: objInquiryMaster.ContactPersonname, Value: '' },
                        MobileNo: $scope.teliphone.toString(),
                        Email: $scope.mail.toString(),
                        CityId: objInquiryMaster.CityId,
                        CityName: objInquiryMaster.CityName,
                        CountryId: objInquiryMaster.CountryId,
                        CountryName: objInquiryMaster.CountryName,
                        StateId: objInquiryMaster.StateId,
                        StateName: objInquiryMaster.StateName,
                        Requirement: objInquiryMaster.Requirement,
                        Address: objInquiryMaster.Address,
                        StatusId: objInquiryMaster.StatusId,
                        Status: objInquiryMaster.StatusName,
                        Remark: objInquiryMaster.Remark,
                        AssignTo: objInquiryMaster.AssignTo,
                        //ContactPersonData: { Display: objInquiryMaster.ContactPerson, Value: objInquiryMaster.ContactId },
                        SourceData: { Display: objInquiryMaster.SourceName, Value: objInquiryMaster.SourceId },
                        CountryData: { Display: objInquiryMaster.CountryName, Value: objInquiryMaster.CountryId },
                        StateData: { Display: objInquiryMaster.StateName, Value: objInquiryMaster.StateId },
                        CityData: { Display: objInquiryMaster.CityName, Value: objInquiryMaster.CityId },
                        StatusData: { Display: objInquiryMaster.StatusName, Value: objInquiryMaster.StatusId },
                        BuyerType: objInquiryMaster.BuyerType,
                        SubjectType: objInquiryMaster.SubjectType,
                        Subject: objInquiryMaster.Subject,
                        Subjectlist: { Display: objInquiryMaster.Subject, Value: '' }
                    };
                    
                    $scope.storage = angular.copy($scope.objInquiry);
                    //showHide($scope.objInquiry.BuyerType);
                    //showHideSub($scope.objInquiry.SubjectType);
                    Reporting();
                } else {
                    window.location.href = "/Transaction/Inquiry";
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            })
        }

        function Reporting() {
            TaskService.ReportingUserBind(0).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objInquiry.ReportingUserArray = [];
                    var Mainres, data = '', res = '', AssignMsg = '', AssigneIds = '';
                    _.each(result.data.DataList, function (value, key, list) {
                        if (value.UserId == $scope.objInquiry.AssignTo) {
                            $scope.objInquiry.ReportingUserArray.push({
                                name: value.UserId,
                                maker: value.Name,
                                ticked: true,
                                disabled: (AssigneIds != undefined && AssigneIds.includes(value.UserId.toString())) ? true : false
                            })

                        }
                        else {
                            $scope.objInquiry.ReportingUserArray.push({
                                name: value.UserId,
                                maker: value.Name,
                                ticked: false,
                                disabled: (AssigneIds != undefined && AssigneIds.includes(value.UserId.toString())) ? true : false
                            })
                        }
                    })
                }
                else {
                    toastr.error(result.data.Message)
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.GetInvoice = function () {
            InquiryService.GetInvoice().then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objInquiry.InqNo = result.data.DataList.InvCode;
                } else if (result.ResponseType == 3) {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (error) {
                $rootScope.errorHandler(error)
            })
        }

        $scope.Add = function () {
            window.location.href = "/Transaction/Inquiry/AddInquiry";
        }
        function ResetForm() {
            $scope.objInquiry = {
                InqNo: '',
                InqDate: new Date(),
                SourceId: 0,
                SourceName: '',
                BuyerName: '',
                ContactId: '',
                CompanyName:'',
                ContactPersonname: '',
                ContactPersonData: { Display: '', Value: '' },
                MobileNo: '',
                Email: '',
                CityId: 0,
                CityName: '',
                StateId: 0,
                StateName: '',
                CountryId: 0,
                CountryName: '',
                Requirement: '',
                Address: '',
                Subject: '',
                StatusId: 0,
                StatusName: '',
                Remark: '',
                AssignTo: 0,
                SourceData: { Display: '', Value: '' },
                CountryData: { Display: '', Value: '' },
                StateData: { Display: '', Value: '' },
                CityData: { Display: '', Value: '' },
                StatusData: { Display: '', Value: '' },
                BuyerType: 1,
                SubjectType: 1,
                Subjectlist: '',
                Subject: '',
                BuyerData: { Display: '', Value: '' },
            };

            if ($scope.FormInquiryInfo)
                $scope.FormInquiryInfo.$setPristine();
            $scope.addMode = true;
            $scope.isFirstFocus = false;
            $scope.storage = {};
            $timeout(function () {
                $scope.isFirstFocus = true;
            });
            $scope.EditInquiryItemDetailsIndex = -1;
        }
        ResetForm();

        $scope.Reset = function () {
            if ($scope.addMode == false) {
                $scope.objInquiry = angular.copy($scope.storage);
            } else {
                ResetForm();
                $scope.SetInquiryId(0);
            }
        }
        $scope.CountryBind = function (data) {
            CityService.CountryBind().then(function (result) {
                if (result.data.ResponseType == 1) {
                    _.each(result.data.DataList, function (value, key, list) {
                        if (value.CountryName.toString().toLowerCase() == data.toString().toLowerCase() && $scope.objInquiry.CountryData.Value == '') {
                            $scope.objInquiry.CountryData = {
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
        $scope.$watch('objInquiry.CountryData', function (data) {
            //if ($scope.objInquiry.BuyerData.Display == '' || $scope.objInquiry.BuyerData.Display == null) {
            if (data.Value != "") {
                if (data.Value != $scope.objInquiry.CountryId.toString()) {
                    $scope.objInquiry.StateData.Display = '';
                    $scope.objInquiry.StateData.Value = '';
                    $scope.objInquiry.CityData.Display = '';
                    $scope.objInquiry.CityData.Value = '';
                }
            } else {
                $scope.CountryBind('India');
            }
            //}
        }, true)

        $scope.$watch('objInquiry.StateData', function (data) {
            //if ($scope.objInquiry.BuyerData.Display == '' || $scope.objInquiry.BuyerData.Display == null) {
            if (data.Value != $scope.objInquiry.StateId.toString()) {
                $scope.objInquiry.CityData.Display = '';
                $scope.objInquiry.CityData.Value = '';
            }
            //}
        }, true)
        $scope.$watch('objInquiry.BuyerData', function (val) {
            $scope.objInquiry.ContactPerson = {
                Display: "",
                Value: ""
            };
            if (val != undefined) {
                if (val.Value != '' && val.Value > 0) {
                    InquiryService.GetAddressByBuyerId(val.Value).then(function (result) {
                        if (result.data.ResponseType == 1) {
                            var addressdata = result.data.DataList.buyeraddress;
                            $scope.objInquiry.CompanyName = result.data.DataList.buyerdata.CompanyName;
                            $scope.objInquiry.StateId = addressdata.StateId;
                            $scope.objInquiry.CountryId = addressdata.CountryId;
                            $scope.objInquiry.CountryData = { Display: addressdata.CountryName, Value: addressdata.CountryId };
                            $scope.objInquiry.StateData = { Display: addressdata.StateName, Value: addressdata.StateId };
                            $scope.objInquiry.CityData = { Display: addressdata.CityName, Value: addressdata.CityId };
                            $scope.objInquiry.Address = addressdata.Address;
                            angular.forEach(result.data.DataList.buyercontact, function (val) {
                                $scope.objInquiry.ContactId = val.ContactId;
                                $scope.objInquiry.ContactPersonData = { Display: val.ContactPerson, Value: val.ContactId };
                                $scope.mail = (val.Email != '' && val.Email != null) ? val.Email.split(",") : [];
                                $scope.teliphone = (val.MobileNo != '' && val.MobileNo != null) ? val.MobileNo.split(",") : [];
                            });
                        } else if (result.ResponseType == 3) {
                            toastr.error(result.data.Message, 'Opps, Something went wrong');
                        }
                    }, function (error) {
                        $rootScope.errorHandler(error)
                    })
                }
            }
        }, true)

        $scope.CreateUpdate = function (data) {
            var AssignToList = '';

            //$scope.teliphone = (data.MobileNo != '') ? data.MobileNo.split(",") : [];
            //$scope.mail = (data.Email != '') ? data.Email.split(",") : [];
            debugger;
            if (data.BuyerType == 2) {
                data.BuyerName = data.BuyerData.Display;
                data.ContactPersonname = data.ContactPersonData.Display;
            } else {
                data.BuyerName = data.BuyerName;
                data.ContactPersonname = data.ContactPersonname
            }

            if (data.SubjectType == 1) {
                data.Subject = data.Subjectlist.Display;
            } else {
                data.Subject = data.Subject;
            }
            var objinsinquiry = {
                InqId: data.InqId,
                InqNo: data.InqNo,
                InqDate: data.InqDate,
                BuyerType: data.BuyerType,
                BuyerName: data.BuyerName,
                CompanyName: data.CompanyName,
                ContactPersonname: data.ContactPersonname,
                MobileNo: $scope.teliphone.toString(),
                Email: $scope.mail.toString(),
                CityId: data.CityData.Value,
                CityName: data.CityData.Display,
                StateId: data.StateData.Value,
                StateName: data.StateData.Display,
                CountryId: data.CountryData.Value,
                CountryName: data.CountryData.Display,
                Requirement: data.Requirement,
                Address: data.Address,
                Remark: data.Remark,
                SubjectType: data.SubjectType,
                Subject: data.Subject,
                SourceId: data.SourceData.Value,
                SourceName: data.SourceData.Display,
                StatusId: data.StatusData.Value,
                StatusName: data.StatusData.Display,
                SourceData: { Display: data.SourceData.Display, Value: data.SourceData.Value },
                CountryData: { Display: data.CountryData.Display, Value: data.CountryData.Value },
                StateData: { Display: data.StateData.Display, Value: data.StateData.Value },
                CityData: { Display: data.CityData.Display, Value: data.CityData.Value },
                StatusData: { Display: data.StatusData.Display, Value: data.StatusData.Value },
            }
            debugger;
            //if (data.InqId > 0) {
            //    var ToLength = $scope.objInquiry.ReportingUserArray.length;
            //    if (data.outputReportingUser.length > 0) {
            //        //_.each($scope.objInquiry.ReportingUserArray, function (value, key, list) {
            //        //    if (key < ToLength - 1) {
            //        //        AssignToList += value.name + ','
            //        //    }
            //        //    else if (key == ToLength - 1) {
            //        //        AssignToList += value.name;
            //        //    }
            //        //});
            //        objinsinquiry.AssignTo = data.outputReportingUser[0].name;
            //    }

            //}
            InquiryService.CreateUpdateInquiry(objinsinquiry).then(function (result) {
                if (result.data.ResponseType == 1) {
                    ResetForm();
                    window.location.href = "/Transaction/Inquiry";
                    toastr.success(result.data.Message);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };

        $scope.gridObj = {
            columnsInfo: [
               //{ "title": "Inq Id", "data": "InqId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", filter: false, show: true },
               { "title": "Inquiry Number", "field": "InqNo", sortable: "InqNo", filter: { InqNo: "text" }, show: true, },
               {
                   "title": "Inquiry Date", "field": "InqDate", sortable: "InqDate", filter: { InqDate: "date" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<p ng-bind="ConvertDate(row.InqDate,\'dd/mm/yyyy\')">'
                       return $scope.getHtml(element);
                   }
               },
               //{ "title": "Source Id", "data": "SourceId", sort: true, filter: true, visible: false },
               { "title": "Source", "field": "SourceName", sortable: "SourceName", filter: { SourceName: "text" }, show: true, },
               { "title": "Assign from", "field": "AssignFromUser", sortable: "AssignFromUser", filter: { AssignFromUser: "text" }, show: true, },
               { "title": "Assign To", "field": "AssignToUser", sortable: "AssignToUser", filter: { AssignToUser: "text" }, show: true, },
               {
                   "title": "Buyer Company Name", "field": "BuyerName", sortable: "BuyerName", filter: { BuyerName: "text" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<span>{{row.BuyerName}}</br><b>{{row.CityName}}</b></span>'
                       return $scope.getHtml(element);
                   }
               },
               { "title": "Contact Person", "field": "ContactPersonname", sortable: "ContactPersonname", filter: { ContactPersonname: "text" }, show: true, },
               //{ "title": "City Id", "data": "CityId", sort: true, filter: true, visible: false },
               { "title": "Address", "field": "Address", sortable: "Address", filter: { Address: "text" }, show: false, },
               { "title": "City/District", "field": "CityName", sortable: "CityName", filter: { CityName: "text" }, show: true, },
               { "title": "State", "field": "StateName", sortable: "StateName", filter: { StateName: "text" }, show: true, },
               //{ "title": "State Id", "data": "StateId", sort: true, filter: true, visible: false },
               //{ "title": "Country Id", "data": "CountryId", sort: true, filter: true, visible: false },
               { "title": "Country", "field": "CountryName", sortable: "CountryName", filter: { CountryName: "text" }, show: true, },
               { "title": "Mobile", "field": "MobileNo", sortable: "MobileNo", filter: { MobileNo: "text" }, show: true, },
               {
                   "title": "Email", "field": "Email", sortable: "Email", filter: { Email: "text" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = "<p class='MailTo' ng-bind-html='getHtml($parent.$parent.$parent.$parent.$parent.$parent.FormatEmail(row.Email))'>"
                       return $scope.getHtml(element);
                   }
               },
               //{ "title": "Requirement", "field": "Requirement", sort: true, filter: true },
               {
                   "title": "Subject", "field": "Subject", sortable: "Subject", filter: { Subject: "text" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<p data-uib-tooltip="{{row.Subject}}" ng-bind="LimitString(row.Subject,100)">'
                       return $scope.getHtml(element);
                   }
               },
               { "title": "FollowStatus", "field": "FollowStatus", sortable: "FollowStatus", filter: { FollowStatus: "text" }, show: false, },
               {
                   "title": "Last Note/Remark", "field": "Remark", sortable: "Remark", filter: { Remark: "text" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<p data-uib-tooltip="{{row.Remark}}" ng-bind="LimitString(row.Remark,100)">'
                       return $scope.getHtml(element);
                   }
               },
               //{ "title": "Requirement subject", "field": "Subject", sortable: "Subject", filter: { Subject: "text" }, show: false, },
               //{ "title": "Requirement Note", "field": "Requirement", sortable: "Requirement", filter: { Requirement: "text" }, show: false, },
               //{ "title": "Last Note/Remark", "field": "Remark", sortable: "Remark", filter: { Remark: "text" }, show: false, },
                {
                    "title": "Last Follow date & time ", "field": "LastFollowDate", sortable: "LastFollowDate", filter: { LastFollowDate: "date" }, show: true,
                    'cellTemplte': function ($scope, row) {
                        var element = '<span >{{ConvertDate(row.LastFollowDate,\'dd/mm/yyyy\') +"      "+ ConvertTime(row.LastFollowTime)}}</span>'
                        return $scope.getHtml(element);
                    }
                },
               { "title": "Status", "field": "TaskStatus", sortable: "TaskStatus", filter: { TaskStatus: "text" }, show: true },

               // {
               //     "title": "Follow Date Time", "field": "FollowDate", sortable: "FollowDate", filter: { FollowDate: "date" }, show: true,
               //     'cellTemplte': function ($scope, row) {
               //         var element = '<span >{{ConvertDate(row.FollowDate,\'dd/mm/yyyy\') +"      "+ ConvertTime(row.FollowTime)}}</span>'
               //         return $scope.getHtml(element);
               //     }
               // },
               
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.InqId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                         //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.InqId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                         '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.InqId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>' +
                         '<a class="btn btn-info btn-xs" data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.ShowFollowupDetail(row.InqId,row.FollowStatus,row)"  data-uib-tooltip="Followup"><i class="fa fa-tasks"></i></a> ' +
                         '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.VcCard(row.InqId)" data-uib-tooltip="VCard"><i class="fa fa-download" ></i></a>'

                       return $scope.getHtml(element);
                   }
               }
            ],
            //ng-show="row.FollowStatus==\'true\'" 
            Sort: { 'InqId': 'asc' }
        }
        $scope.FormatEmail = function (d) {
            if (d != null) {
                var mailto = '';
                var emails = d.split(',');
                for (var i = 0; i < emails.length; i++) {
                    mailto += mailto == '' ? '' : ',';
                    mailto += '<a href="mailto:' + emails[i] + '" target="_blank">' + emails[i] + '</a>';
                }
                return mailto;
            }
        }

        $scope.Edit = function (id) {
            window.location.href = "/Transaction/Inquiry/AddInquiry/" + id + "/" + 0;
        }
        $scope.View = function (id) {
            window.location.href = "/Transaction/Inquiry/AddInquiry/" + id + "/" + 1;
        }
        $scope.VcCard = function (id) {
            window.location.href = "/Transaction/Inquiry/GetVCCard/" + id;

        }

        $scope.Delete = function (id) {
            InquiryService.DeleteInquiry(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    $scope.refreshGrid();
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            })
        }

        $scope.AssignUser = function (id) {

            alert('Assign User.');
            //InquiryService.DeleteInquiry(id).then(function (result) {
            //    if (result.data.ResponseType == 1) {
            //        toastr.success(result.data.Message);
            //        $scope.refreshGrid();
            //    } else {
            //        toastr.error(result.data.Message, 'Opps, Something went wrong');
            //    }
            //})
        }

        $scope.AddAssignItemDetails = function (data) {
            var modalInstance = $uibModal.open({
                templateUrl: 'InquiryAssignDetails.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    InquiryCtrl: function () { return $scope; },
                    InquiryService: function () { return InquiryService; },
                    InquiryItemDetailsData: function () { return data; }
                }
            });
        }
        $scope.dateOptions = {
            formatYear: 'yy',
            minDate: new Date(2010, 0, 1),
            startingDay: 1
        };

        $scope.DeleteAssignDetail = function (data, index) {
            $scope.$apply(function () {
                if (data.Status == 2) {
                    data.Status = 3;
                    $scope.objInquiry.InquiryItemDetails[index] = data;
                } else {
                    $scope.objInquiry.InquiryItemDetails.splice(index, 1);
                }
                toastr.success("Inquiry contact detail Delete", "Success");
            })
        }

        $scope.ShowFollowupDetail = function (id, followStatus, maindata) {
            var modalInstance = $uibModal.open({
                templateUrl: 'myModalInquiryFollowUpList.html',
                controller: InqFollowUpListModalInstanceCtrl,
                size: 'lg',
                resolve: {
                    InquiryCtrl: function () { return $scope; },
                    InquiryService: function () { return InquiryService; },
                    id: function () { return id; },
                    followStatus: function () { return followStatus; },
                    MainData: function () { return maindata }
                }
            });
            modalInstance.result.then(function () {
                $scope.refreshGrid();
            }, function () {
            })
        }

        $scope.EditAssignDetail = function (id) {
            $scope.EditInquiryItemDetailsIndex = id;
            InquiryService.GetInquiryFollowUp(id).then(function (result) {
                var modalInstance = $uibModal.open({
                    templateUrl: 'myModalInquiryFollowUp.html',
                    controller: InquiryFollowUpModalInstanceCtrl,
                    resolve: {
                        InquiryCtrl: function () { return $scope; },
                        InquiryService: function () { return InquiryService; },
                        InquiryFollowUpDetailsData: function () { return result.data; }
                    }
                });
            })
        }

        $scope.statusFilter = function (val) {
            if (val.Status != 3)
                return true;
        }

        $scope.showHide = function (value) {
            $scope.objInquiry.CompanyName = '';
            $scope.teliphone = [];
            $scope.mail = [];
            if (value == "1") {
                $scope.objInquiry.ContactPersonname = '';
                $scope.objInquiry.BuyerName = '';
            }
            else {
                $scope.objInquiry.ContactPerson = {
                    Display: "",
                    Value: ""
                };
            }
        }

        $scope.showHideSub = function (value) {
            if (value == "1") {
                $scope.objInquiry.Subjectlist = {
                    Display: "",
                    Value: ""
                };
            }
            else {
                $scope.objInquiry.Subject = '';
            }
        }
        
    }

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, InquiryCtrl, InquiryService, InquiryItemDetailsData) {

        $scope.objInquiryItemDetails = {
            InqDetailId: InquiryItemDetailsData.InqDetailId,
            InqId: InquiryItemDetailsData.InqId,
            CategoryId: InquiryItemDetailsData.CategoryId,
            Category: InquiryItemDetailsData.Category,
            SubCategoryId: InquiryItemDetailsData.SubCategoryId,
            SubCategory: InquiryItemDetailsData.SubCategory,
            MainProductId: InquiryItemDetailsData.MainProductId,
            MainProductName: InquiryItemDetailsData.MainProductName,
            ProductId: InquiryItemDetailsData.ProductId,
            ProductName: InquiryItemDetailsData.ProductName,
            ProductDescription: InquiryItemDetailsData.ProductDescription,
            QtyCodeData: InquiryItemDetailsData.QtyCodeData,
            QtyCode: InquiryItemDetailsData.QtyCode,
            Qty: InquiryItemDetailsData.Qty,
            Status: InquiryItemDetailsData.Status,
            CategoryData: { Display: InquiryItemDetailsData.Category, Value: InquiryItemDetailsData.CategoryId },
            SubCategoryData: { Display: InquiryItemDetailsData.SubCategory, Value: InquiryItemDetailsData.SubCategoryId },
            MainProductData: { Display: InquiryItemDetailsData.MainProductName, Value: InquiryItemDetailsData.MainProductId },
            ProductData: { Display: InquiryItemDetailsData.ProductName, Value: InquiryItemDetailsData.ProductId },
            QtyCodeValueData: { Display: InquiryItemDetailsData.QtyCodeData, Value: InquiryItemDetailsData.QtyCode }
        };

        $scope.close = function () {
            $uibModalInstance.close();
        };

        $scope.CreateUpdate = function (data) {
            // SET DDL ID 
            data.CategoryId = data.CategoryData.Value;
            data.SubCategoryId = data.SubCategoryData.Value;
            data.MainProductId = data.MainProductData.Value;
            data.ProductId = data.ProductData.Value;
            data.QtyCode = data.QtyCodeValueData.Value;

            data.Category = data.CategoryData.Display;
            data.SubCategory = data.SubCategoryData.Display;
            data.MainProductName = data.MainProductData.Display;
            data.ProductName = data.ProductData.Display;
            data.QtyCodeData = data.QtyCodeValueData.Display;

            if (InquiryCtrl.EditInquiryItemDetailsIndex > -1) {
                InquiryCtrl.objInquiry.InquiryItemDetails[InquiryCtrl.EditInquiryItemDetailsIndex] = data;
                InquiryCtrl.EditInquiryItemDetailsIndex = -1;
            } else {
                data.Status = 1;
                InquiryCtrl.objInquiry.InquiryItemDetails.push(data);
            }
            $scope.close();
        }

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'InquiryCtrl', 'InquiryService', 'InquiryItemDetailsData']


    var InquiryFollowUpModalInstanceCtrl = function ($scope, $uibModalInstance, $filter, InquiryCtrl, TaskService, InquiryService, InquiryFollowUpDetailsData, isdisable) {

        $scope.isclose = false;
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }

        $scope.objInquiryFollowUp = {
            FollowupId: InquiryFollowUpDetailsData.FollowupId,
            InqId: InquiryFollowUpDetailsData.InqId,
            CurrentUpdate: InquiryFollowUpDetailsData.CurrentUpdate,
            NextFollowTime: (InquiryFollowUpDetailsData.NextFollowTime == null) ? new Date() : InquiryFollowUpDetailsData.NextFollowTime,
            NextFollowDate: (moment(InquiryFollowUpDetailsData.NextFollowDate).format("DD-MM-YYYY") == "01-01-0001" || (InquiryFollowUpDetailsData.NextFollowDate) == "") ? new Date() : $filter('mydate')(InquiryFollowUpDetailsData.NextFollowDate),
            Status: InquiryFollowUpDetailsData.Status,
            AssignId: InquiryFollowUpDetailsData.AssignId,
            InquiryStatus: { Display: InquiryFollowUpDetailsData.StatusName, Value: InquiryFollowUpDetailsData.Status },
            outputReportingUser: []
        }

        $scope.ReportingUserArray = [];
        $scope.outputReportingUser = [];
        //InquiryFollowUpDetailsData.AssignId;
        $scope.dateOptions = {
            formatYear: 'yy',
            //minDate: new Date(2016, 8, 5),
            //maxDate: new Date(2017, 5, 22),
            startingDay: 1
        };

        //$scope.InquiryStatus = { Display: InquiryFollowUpDetailsData.StatusName, Value: InquiryFollowUpDetailsData.Status },

        InquiryService.ReportingInquiryUserBind(InquiryFollowUpDetailsData.InqId).then(function (result) {
            debugger
            _.each(result.data.DataList, function (value, key, list) {
                if (value.UserId == InquiryFollowUpDetailsData.AssignId) {
                    $scope.ReportingUserArray.push({
                        name: value.UserId,
                        maker: value.Name,
                        ticked: true,
                        //disabled: true
                    })

                }
                else {
                    $scope.ReportingUserArray.push({
                        name: value.UserId,
                        maker: value.Name,
                        ticked: false,
                        //disabled: (AssigneIds != undefined && AssigneIds.includes(value.UserId.toString())) ? true : false
                    })
                }
            })
        });

        $scope.objInquiryFollowupDetails = InquiryFollowUpDetailsData;
        // $scope.objInquiryFollowupDetails.NextFollowDate = moment(InquiryFollowUpDetailsData.NextFollowDate).format("DD-MM-YYYY") == "01-01-0001" ? new Date() : $filter('mydate')(InquiryFollowUpDetailsData.NextFollowDate);
        var mytime = new Date();
        var gettime = '';
        if (InquiryFollowUpDetailsData.NextFollowTime != null) {
            mytime.setHours($filter('date')(InquiryFollowUpDetailsData.NextFollowTime, "HH:mm").Hours);
            mytime.setMinutes($filter('date')(InquiryFollowUpDetailsData.NextFollowTime, "HH:mm").Minutes);
            gettime = InquiryFollowUpDetailsData.NextFollowTime.Hours == 0 && InquiryFollowUpDetailsData.NextFollowTime.Minutes == 0 ? new Date() : mytime;
            $scope.objInquiryFollowUp.NextFollowTime = gettime;
        }
        $scope.close = function () {
            $scope.isclose = true;
            $uibModalInstance.close($scope.isclose);
        };

        $scope.CreateInquiryFollowUp = function () {
            $scope.objInquiryFollowUp.NextFollowTime = $filter('date')($scope.objInquiryFollowUp.NextFollowTime, "HH:mm");
            $scope.objInquiryFollowUp.AssignId = $scope.objInquiryFollowUp.outputReportingUser[0].name;
            $scope.objInquiryFollowUp.Status = $scope.objInquiryFollowUp.InquiryStatus.Value;
            var aid = $scope.objInquiryFollowUp.AssignId;
            InquiryService.SaveInquiryFollowUp($scope.objInquiryFollowUp).then(function (result) {
                toastr.success(result.data.Message);
                $uibModalInstance.close($scope.isclose);
            })
        }
    }
    InquiryFollowUpModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', '$filter', 'InquiryCtrl', 'TaskService', 'InquiryService', 'InquiryFollowUpDetailsData', 'isdisable']

    var InqFollowUpListModalInstanceCtrl = function ($scope, $uibModalInstance, $filter, $uibModal, InquiryCtrl, TaskService, InquiryService, id, followStatus, MainData) {
        //        FollowupId,InqId,CurrentUpdate,NextFollowDate,NextFollowTime,TaskStatus AS StatusName,
        //Status, A.CreatedBy, C.Name As UserName , AssignId, D.Name As AssignUserName ,A.IsActive
        $scope.setclause = function (userid) {
            $scope.FixClause = 'InqId = ' + id;
            $scope.setStatus = (followStatus == 'true') ? true : false;
            $scope.userid = userid;
            $scope.MainDetails = MainData;
            $scope.iDate = moment(MainData.InqDate).format("DD-MM-YYYY");

            $scope.gridFollowupObj = {
                columnsInfo: [
                   //{ "title": "Followup Id", "field": "FollowupId", filter: false, visible: false },
                   { "title": "Sr.", "field": "RowNumber", show: true, },
                   //{ "title": "Inquiry Id", "field": "InqId", sort: true, filter: true, visible: false },
                   { "title": "Current Update", "field": "CurrentUpdate", sortable: "CurrentUpdate", filter: { CurrentUpdate: "text" }, show: true, },
                   {
                       "title": "Next Follow DateTime", sortable: "NextFollowDate", filter: { NextFollowDate: "date" }, show: true,
                       'cellTemplte': function ($scope, row) {
                           var element = '<span >{{ConvertDate(row.NextFollowDate,\'dd/mm/yyyy\') +"      "+ ConvertTime(row.NextFollowTime)}}</span>'
                           return $scope.getHtml(element);
                       }
                   },
                   { "title": "Status", "field": "StatusName", sortable: "StatusName", filter: { StatusName: "text" }, show: true, },
                   //{ "title": "UserID", "field": "CreatedBy", sortable: "CreatedBy", filter: { CreatedBy: "text" }, show: false },
                   { "title": "User", "field": "UserName", sortable: "UserName", filter: { UserName: "text" }, show: true, },
                   //{ "title": "AssignUserID", "field": "AssignId", sortable: "AssignId", filter: { AssignId: "text" }, show: false },
                   { "title": "Assign User", "field": "AssignUserName", sortable: "AssignUserName", filter: { AssignUserName: "text" }, show: true, },
                   {
                       "title": "Current Followup date time", sortable: "CurrentDate", filter: { CurrentDate: "date" }, show: true,
                       'cellTemplte': function ($scope, row) {
                           var element = '<span >{{ConvertDate(row.CurrentDate,\'dd/mm/yyyy\') +"      "+ ConvertTime(row.CurrentTime)}}</span>'
                           return $scope.getHtml(element);
                       }
                   },
                   {
                       "title": "Action", show: true,
                       'cellTemplte': function ($scope, row) {
                           var element = '<a class="btn btn-primary btn-xs" ng-show="row.CreatedBy == $parent.$parent.$parent.$parent.$parent.$parent.userid"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.EditFollowup(row.FollowupId,0)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                             '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.EditFollowup(row.FollowupId,1)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                           return $scope.getHtml(element);
                       }
                   }
                ],
                Sort: { 'FollowupId': 'asc' }
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

        $scope.AddFollowup = function () {
            var isdisable = 0
            InquiryService.GetInquiryFollowUp(id).then(function (result) {

                var objInquiryFollowUp = {
                    FollowupId: 0,
                    InqId: result.data.InqId,
                    CurrentUpdate: '',
                    NextFollowTime: null,
                    NextFollowDate: '',
                    Status: result.data.Status,
                    StatusName: result.data.StatusName,
                    AssignId: result.data.AssignId,
                    InquiryStatus: { Display: result.data.StatusName, Value: result.data.Status },
                    outputReportingUser: []
                }

                var modalInstance = $uibModal.open({
                    templateUrl: 'myModalInquiryFollowUp.html',
                    controller: InquiryFollowUpModalInstanceCtrl,
                    resolve: {
                        InquiryCtrl: function () { return $scope; },
                        InquiryService: function () { return InquiryService; },
                        InquiryFollowUpDetailsData: function () { return objInquiryFollowUp; },
                        isdisable: function () { return isdisable; }
                    }
                });
                modalInstance.result.then(function (close) {
                    if (result.data.AssignId != id && close == false) {
                        $scope.setStatus = false;
                    }
                    $scope.refreshGrid();
                }, function () {
                })
            })
        }

        $scope.EditFollowup = function (id, isdisable) {
            InquiryService.GetInquiryFollowUpByID(id).then(function (result) {
                var modalInstance = $uibModal.open({
                    templateUrl: 'myModalInquiryFollowUp.html',
                    controller: InquiryFollowUpModalInstanceCtrl,
                    resolve: {
                        InquiryCtrl: function () { return $scope; },
                        InquiryService: function () { return InquiryService; },
                        InquiryFollowUpDetailsData: function () { return result.data; },
                        isdisable: function () { return isdisable; }
                    }
                });
                modalInstance.result.then(function (close) {
                    if (result.data.AssignId != id && close == false) {
                        $scope.setStatus = false;
                    }
                    $scope.refreshGrid();
                }, function () {
                })
            })
        }

        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };

    }
    InqFollowUpListModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', '$filter', '$uibModal', 'InquiryCtrl', 'TaskService', 'InquiryService', 'id', 'followStatus', 'MainData']

    angular.module('CRMApp.Controllers')
        .filter("mydate", function () {
            var re = /\/Date\(([0-9]*)\)\//;
            return function (x) {
                var m = x.match(re);
                if (m) return new Date(parseInt(x.substr(6)));
                else return null;
            };
        });
})()