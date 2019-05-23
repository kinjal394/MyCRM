(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("InquiryController", [
            "$scope", "InquiryService", "$uibModal", "$filter", "CountryService", "NgTableParams", "$http",
            InquiryController]);

    function InquiryController($scope, InquiryService, $uibModal, $filter, CountryService, NgTableParams, $http) {

        $scope.expandRowFun = function (flag, InqId) {
            _.filter($scope.ListInqInfor, function (objInq) {
                if (objInq.InqId == InqId)
                    objInq.expandedGrid = !objInq.expandedGrid;
                else
                    objInq.expandedGrid = false;
            })
        }


        $scope.getInquiryInfo = function () {
            //InquiryService.GetInquiryGridData().then(function (result) {
            //    if (result.data.ResponseType == 1) {
            //        debugger;
            $scope.tableParams = new NgTableParams({
                page: 1,            // show first page
                count: 5       // count per page
            }, {
                    total: 50, // length of data
                    filterDelay: 750,
                    getData: function ($defer, params) {
                        // START
                        var setSearch = {};
                        $.each(params.filter(), function (index, value) {
                            var getSearch = {};
                            if (value != null) {
                                if (typeof (value) === 'object') {
                                    if (isDate(value)) {
                                        getSearch = ({ [index]: value });
                                    }
                                    else {
                                        getSearch = ({ [index]: value.Display });
                                    }
                                } else {
                                    getSearch = ({ [index]: value });
                                }
                            }
                            $.extend(setSearch, getSearch);
                        });
                        // END
                        var customPara = {
                            Sort: params.sorting(),
                            Filter: JSON.stringify(setSearch),
                            FixClause: ($scope.fixedclause === undefined || $scope.fixedclause == null || $scope.fixedclause == '') ? '' : $scope.fixedclause,
                            PageNumber: params.page(),
                            RecordPerPage: params.count(),
                            Mode: scope.modeType
                        }
                        $http({
                            method: 'post',
                            url: '/Home/getData',
                            contentType: 'application/json; charset=utf-8',
                            data: customPara,
                            responseType: 'json',
                        }).then(function (result) {
                            // //console.log(result);
                            //$defer.resolve(scope.documents = result.data.data);
                            //params.total(result.data.recordsTotal);
                            //if (result.data.ResponseType == 1) {
                            ////$scope.ListInqInfor = result.data.DataList.objInquiryMaster.slice((params.page() - 1) * params.count(), params.page() * params.count());
                            ////_.forEach($scope.ListInqInfor, function (data123) {
                            ////    data123.expandedGrid = false;
                            ////});
                            ////$defer.resolve($scope.ListInqInfor);
                            $scope.ListInqInfor = result.data.data.slice((params.page() - 1) * params.count(), params.page() * params.count());
                            _.forEach($scope.ListInqInfor, function (objdata) {
                                objdata.expandedGrid = false;
                            });
                            $defer.resolve($scope.ListInqInfor);
                            params.total(result.data.recordsTotal);
                            //}
                        }, function (response) {
                            //  //console.log("Some thing wrong");
                        });
                    }
                });
            //} else {
            //    toastr.error(result.data.Message, 'Opps, Something went wrong');
            //}
            //});
        }
        $scope.getInquiryInfo();

        var nesteddata = [{ name: "Moroni", age: 50, id: 1 },
        { name: "Tiancum", age: 43, id: 1 },
        { name: "Jacob", age: 27, id: 2 },
        { name: "Nephi", age: 29, id: 3 },
        { name: "Enos", age: 34, id: 2 },
        { name: "Tiancum", age: 43, id: 3 },
        { name: "Jacob", age: 27, id: 3 },
        { name: "Nephi", age: 29, id: 4 },
        { name: "Enos", age: 34, id: 5 },
        { name: "Tiancum", age: 43, id: 4 },
        { name: "Jacob", age: 27, id: 4 },
        { name: "Nephi", age: 29, id: 5 },
        { name: "Enos", age: 34, id: 4 },
        { name: "Tiancum", age: 43, id: 5 },
        { name: "Jacob", age: 27, id: 5 },
        { name: "Nephi", age: 29, id: 3 },
        { name: "Enos", age: 34, id: 1 }];
        var data = [{
            name: "Kid Moroni", age: 50, id: 1,
            details: [
                { name: "Tiancum", age: 43, id: 5 },
                { name: "Jacob", age: 27, id: 5 },
                { name: "Nephi", age: 29, id: 3 }
            ]
        },
        {
            name: "Kid Tiancum", age: 43, id: 2,
            details: [
                { name: "Tiancum", age: 43, id: 5 },
                { name: "Jacob", age: 27, id: 5 },
                { name: "Nephi", age: 29, id: 3 }
            ]
        },
        {
            name: "Kid Jacob", age: 27, id: 3,
            details: [
                { name: "Tiancum", age: 43, id: 5 },
                { name: "Jacob", age: 27, id: 5 },
                { name: "Nephi", age: 29, id: 3 }
            ]
        },
        {
            name: "Kid Nephi", age: 29, id: 4,
            details: [
                { name: "Tiancum", age: 43, id: 5 },
                { name: "Jacob", age: 27, id: 5 },
                { name: "Nephi", age: 29, id: 3 }
            ]
        },
        {
            name: "Kid Enos", age: 34, id: 5,
            details: [
                { name: "Tiancum", age: 43, id: 5 },
                { name: "Jacob", age: 27, id: 5 },
                { name: "Nephi", age: 29, id: 3 }
            ]
        },
        {
            name: "Kid Moroni", age: 50, id: 1,
            details: [
                { name: "Tiancum", age: 43, id: 5 },
                { name: "Jacob", age: 27, id: 5 },
                { name: "Nephi", age: 29, id: 3 }
            ]
        },
        {
            name: "Kid Tiancum", age: 43, id: 2,
            details: [
                { name: "Tiancum", age: 43, id: 5 },
                { name: "Jacob", age: 27, id: 5 },
                { name: "Nephi", age: 29, id: 3 }
            ]
        },
        {
            name: "Kid Jacob", age: 27, id: 3,
            details: [
                { name: "Tiancum", age: 43, id: 5 },
                { name: "Jacob", age: 27, id: 5 },
                { name: "Nephi", age: 29, id: 3 }
            ]
        },
        {
            name: "Kid Nephi", age: 29, id: 4,
            details: [
                { name: "Tiancum", age: 43, id: 5 },
                { name: "Jacob", age: 27, id: 5 },
                { name: "Nephi", age: 29, id: 3 }
            ]
        },
        {
            name: "Kid Enos", age: 34, id: 5,
            details: [
                { name: "Tiancum", age: 43, id: 5 },
                { name: "Jacob", age: 27, id: 5 },
                { name: "Nephi", age: 29, id: 3 }
            ]
        },
        {
            name: "Kid Moroni", age: 50, id: 1,
            details: [
                { name: "Tiancum", age: 43, id: 5 },
                { name: "Jacob", age: 27, id: 5 },
                { name: "Nephi", age: 29, id: 3 }
            ]
        },
        {
            name: "Kid Tiancum", age: 43, id: 2,
            details: [
                { name: "Tiancum", age: 43, id: 5 },
                { name: "Jacob", age: 27, id: 5 },
                { name: "Nephi", age: 29, id: 3 }
            ]
        },
        {
            name: "Kid Jacob", age: 27, id: 3,
            details: [
                { name: "Tiancum", age: 43, id: 5 },
                { name: "Jacob", age: 27, id: 5 },
                { name: "Nephi", age: 29, id: 3 }
            ]
        },
        {
            name: "Kid Nephi", age: 29, id: 4,
            details: [
                { name: "Tiancum", age: 43, id: 5 },
                { name: "Jacob", age: 27, id: 5 },
                { name: "Nephi", age: 29, id: 3 }
            ]
        },
        {
            name: "Kid Enos", age: 34, id: 5,
            details: [
                { name: "Tiancum", age: 43, id: 5 },
                { name: "Jacob", age: 27, id: 5 },
                { name: "Nephi", age: 29, id: 3 }
            ]
        },
        {
            name: "Kid Moroni", age: 50, id: 1,
            details: [
                { name: "Tiancum", age: 43, id: 5 },
                { name: "Jacob", age: 27, id: 5 },
                { name: "Nephi", age: 29, id: 3 }
            ]
        },
        {
            name: "Kid Tiancum", age: 43, id: 2,
            details: [
                { name: "Tiancum", age: 43, id: 5 },
                { name: "Jacob", age: 27, id: 5 },
                { name: "Nephi", age: 29, id: 3 }
            ]
        },
        {
            name: "Kid Jacob", age: 27, id: 3,
            details: [
                { name: "Tiancum", age: 43, id: 5 },
                { name: "Jacob", age: 27, id: 5 },
                { name: "Nephi", age: 29, id: 3 }
            ]
        },
        {
            name: "Kid Nephi", age: 29, id: 4,
            details: [
                { name: "Tiancum", age: 43, id: 5 },
                { name: "Jacob", age: 27, id: 5 },
                { name: "Nephi", age: 29, id: 3 }
            ]
        },
        {
            name: "Kid Enos", age: 34, id: 5,
            details: [
                { name: "Tiancum", age: 43, id: 5 },
                { name: "Jacob", age: 27, id: 5 },
                { name: "Nephi", age: 29, id: 3 }
            ]
        },
        {
            name: "Kid Moroni", age: 50, id: 1,
            details: [
                { name: "Tiancum", age: 43, id: 5 },
                { name: "Jacob", age: 27, id: 5 },
                { name: "Nephi", age: 29, id: 3 }
            ]
        },
        {
            name: "Kid Tiancum", age: 43, id: 2,
            details: [
                { name: "Tiancum", age: 43, id: 5 },
                { name: "Jacob", age: 27, id: 5 },
                { name: "Nephi", age: 29, id: 3 }
            ]
        },
        {
            name: "Kid Jacob", age: 27, id: 3,
            details: [
                { name: "Tiancum", age: 43, id: 5 },
                { name: "Jacob", age: 27, id: 5 },
                { name: "Nephi", age: 29, id: 3 }
            ]
        },
        {
            name: "Kid Nephi", age: 29, id: 4,
            details: [
                { name: "Tiancum", age: 43, id: 5 },
                { name: "Jacob", age: 27, id: 5 },
                { name: "Nephi", age: 29, id: 3 }
            ]
        },
        {
            name: "Kid Enos", age: 34, id: 5,
            details: [
                { name: "Tiancum", age: 43, id: 5 },
                { name: "Jacob", age: 27, id: 5 },
                { name: "Nephi", age: 29, id: 3 }
            ]
        }];
        $scope.nestedtableParams = new NgTableParams({
            page: 1,            // show first page
            count: 10           // count per page
        }, {
                //total: nesteddata.length, // length of data
                getData: function ($defer, params) {
                    $defer.resolve(nesteddata);
                }
            });
    }
})()

