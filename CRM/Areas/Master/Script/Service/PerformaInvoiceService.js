(function () {
    "use strict";
    angular.module("CRMApp.Services")
        .service("PerformaInvoiceService", ["$http",
            function ($http) {
                var list = {};

                list.GetMasterInformation = function () {
                    return $http({
                        method: "GET",
                        url: "/Master/PerformaInvoice/GetMasterInformation"
                    });
                }

                list.GetCompanyById = function (id) {
                    return $http({
                        method: "GET",
                        url: "/Master/Company/GetCompanyById?id=" + id
                    });
                }

                list.GetConsigneById = function (id) {
                    return $http({
                        method: "GET",
                        url: "/Master/PerformaInvoice/GetConsigneById?id=" + id
                    });
                }

                list.GetConsigneAddressById = function (id) {
                    return $http({
                        method: "GET",
                        url: "/Master/PerformaInvoice/GetConsigneAddressById?id=" + id
                    });
                }

                list.GetConsigneContactById = function (id) {
                    return $http({
                        method: "GET",
                        url: "/Master/PerformaInvoice/GetConsigneContactById?id=" + id
                    });
                }

                list.SavePerformaInvoice = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/PerformaInvoice/CreateUpdate",
                        data: data
                    });
                }

                list.UpdatePerformaInvoice = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/PerformaInvoice/UpdatePerformaInvoice",
                        data: data
                    });
                }

                list.DeletePerformaInvoice = function (id) {
                    return $http({
                        method: "POST",
                        url: "/Master/PerformaInvoice/DeletePerformaInvoice?id=" + id,
                    });
                }

                list.GetAllPerformaInvoiceById = function (id) {
                    return $http({
                        method: "GET",
                        url: "/Master/PerformaInvoice/GetAllPerformaInvoiceById?id=" + id
                    })
                }

                list.GetsupplierContactDetail = function (id) {
                    return $http({
                        method: "GET",
                        url: "/Master/PerformaInvoice/GetsupplierContactDetailByid?id=" + id
                    });
                }

                list.GetAllBuyerInfoById = function (id) {
                    return $http({
                        method: "GET",
                        url: "/Buyer/GetAllBuyerInfoById?id=" + id
                    });
                }

                list.GetInvoice = function () {
                    return $http({
                        method: "POST",
                        url: "/PerformaInvoice/GetInvoiceInfo"
                    });
                }

                list.PrintPerformaInvoice = function (Id) {
                    return $http({
                        method: "POST",
                        url: "/PerformaInvoice/PrintById",
                        data: '{id:"' + Id + '"}'
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    });
                }

                list.GetAddressByBuyerID = function (id) {
                    return $http({
                        method: "GET",
                        url: "/Buyer/GetAddressByBuyerID?id=" + id
                    });
                }

                list.SavePerformaPayment = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/PerformaInvoice/CreateUpdatePerformaPayment",
                        data: data
                    });
                }

                list.DeletePerformaPayment = function (id) {
                    console.log("Sample Application");
                    return $http({
                        method: "GET",
                        url: "/Master/PerformaInvoice/DeletePerformaPayment?id=" + id
                    });
                }

                list.GetCompanydataById = function (id) {
                    return $http({
                        method: "POST",
                        url: "/Master/companyLetter/GetCompanydata?id=" + id
                    });
                }

                return list;
            }])
})()