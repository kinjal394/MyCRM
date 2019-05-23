/// <reference path="D:\Raju_Data\CRM\CRM_Proj\CRM\Content/lib/angular/angular.js" />

(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("InquiryService", ["$http",
            function ($http) {
                var list = {};
                list.GetAllInquiry = function () {
                    return $http({
                        method: "POST",
                        url: "/Transaction/Inquiry"
                    });
                }

                list.CountryBind = function () {
                    return $http({
                        method: "Get",
                        url: "/Master/Country/CountryBind"
                    });
                }

                list.GetInvoice = function () {
                    return $http({
                        method: "POST",
                        url: "/Transaction/Inquiry/GetInvoiceInfo"
                    });
                }

                list.StateBind = function (data) {
                    return $http({
                        method: "Get",
                        url: "/Master/State/StateBind?CountryId=" + data
                    })
                }

                list.CityBind = function (data) {
                    return $http({
                        method: "Get",
                        url: "/Master/City/CityBind?StateId=" + data
                    })
                }

                list.SourceBind = function (data) {
                    return $http({
                        method: "Get",
                        url: "/Master/Source/SourceBind"
                    })
                }

                list.CountryCodeBind = function (data) {
                    return $http({
                        method: "Get",
                        url: "/Master/Country/CountryBind"
                    })
                }

                list.CreateUpdateInquiry = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Transaction/Inquiry/CreateUpdate",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.DeleteInquiry = function (id) {
                    return $http({
                        method: "POST",
                        url: "/Transaction/Inquiry/DeleteInquiry?InqId=" + id
                    })
                }

                list.InquiryNoAuto = function () {
                    return $http({
                        method: "Get",
                        url: "/Transaction/Inquiry/InquiryNoAuto"
                    })
                }

                list.GetAllInquiryInfoById = function (id) {
                    return $http({
                        method: "GET",
                        url: "/Transaction/Inquiry/GetAllInquiryInfoById?id=" + id
                    });
                }
                list.GetAllInquiryInfo = function (id) {
                    return $http({
                        method: "GET",
                        url: "/Transaction/Inquiry/GetAllInquiryInfo"
                    });
                }
                
                list.GetInquiryFollowUp = function (id) {
                    return $http({
                        method: "GET",
                        url: "/Transaction/Inquiry/GetInquiryFollowUp/" + id
                    });
                }

                list.GetInquiryFollowUpByID = function (id) {
                    return $http({
                        method: "GET",
                        url: "/Transaction/Inquiry/GetInquiryFollowUpByID/" + id
                    });
                }

                list.ReportingInquiryUserBind = function (id) {
                    return $http({
                        method: "Get",
                        //data:data,
                        url: "/Transaction/Inquiry/GetInquiryReportingUser?inqId=" + id
                    });
                }
                list.SaveInquiryFollowUp = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Transaction/Inquiry/SaveInquiryFollowUp",
                        data: data,
                        contentType: "application/json"
                    })
                }
                list.GetAddressByBuyerId = function (id) {
                    return $http({
                        method: "POST",
                        url: "/Transaction/Inquiry/GetBuyerContactDetailById?id=" + id
                    });
                }

                list.GetInquiryGridData = function () {
                    return $http({
                        method: "GET",
                        url: "/Transaction/Inquiry/GetGridInquiryData"
                    });
                }

                return list;
            }])
})()