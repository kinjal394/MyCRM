
(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("QuotationService", ["$http",
            function ($http) {
                var QuotationViewModel = {};
                var _getAllDropDownList = function (countryId) {
                    return $http({
                        method: 'Get',
                        url: '/Transaction/Quotation/GetAllDropDownList'
                    }).success(function (data) {
                        return data;
                    }).error(function (data, status, headers, config) {
                        return 'Unexpected Error';
                    });
                }
                var _GetAllQuotationInfoById = function (id) {
                    return $http({
                        method: "GET",
                        url: "/Quotation/GetAllQuotationInfoById?id=" + id
                    });
                }
                var _addQuoatation = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Quotation/CreateUpdate",
                        data: data,
                        contentType: "application/json"
                    });
                }
                var _deleteQuotation = function (id) {
                    return $http({
                        method: "GET",
                        url: "/Quotation/DeleteById?id=" + id
                    });
                }
                var _GetQuotationNo = function () {
                    return $http({
                        method: "POST",
                        url: "/Transaction/Quotation/GetQuotationInfo"
                    });
                }
                var _GetDelarePriceById = function (ProductId, SupplierId) {
                    debugger;
                    return $http({
                        method: "GET",
                        url: "/Quotation/GetDelarePriceById?ProductId=" + ProductId + "&SupplierId=" + SupplierId
                    });
                }

                QuotationViewModel.deleteQuotation = _deleteQuotation;
                QuotationViewModel.addQuoatation = _addQuoatation;
                QuotationViewModel.getAllDropDownList = _getAllDropDownList;
                QuotationViewModel.GetAllQuotationInfoById = _GetAllQuotationInfoById;
                QuotationViewModel.GetQuotationNo = _GetQuotationNo;
                QuotationViewModel.GetDelarePriceById = _GetDelarePriceById;
                return QuotationViewModel;

            }])
})()





