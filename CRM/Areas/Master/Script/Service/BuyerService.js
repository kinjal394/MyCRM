(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("BuyerService", ["$http",
                function ($http) {
                    var list = {};

                    list.GetMasterInformation = function () {
                        return $http({
                            method: "GET",
                            url: "/Buyer/GetMasterInformation"
                        });
                    }
                    list.getallbuyer=function()
                    {
                        return $http({
                            method: "GET",
                            url: "/Buyer/getallbuyercom"
                        });
                    }
                    list.GetCityById = function (id) {
                        return $http({
                            method: "GET",
                            url: "/Buyer/GetCityById?id=" + id
                        });
                    }

                    list.CreateUpdate = function (data) {
                        return $http({
                            method: "POST",
                            url: "/Buyer/CreateUpdate",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    list.DeleteById = function (id) {
                        return $http({
                            method: "POST",
                            url: "/Buyer/DeleteById?id=" + id
                        });
                    }

                    list.GetAllBuyerInfoById = function (id)
                    {
                        return $http({
                            method: "GET",
                            url: "/Buyer/GetAllBuyerInfoById?id=" + id
                        });
                    }
                    list.CheckBuyerDuplicate = function (data) {
                        return $http({
                            method: "POST",
                            url: "/Buyer/CheckBuyerDuplicate",
                            data: data,
                            contentType: "application/json"
                        })
                    }
                    list.GetInvitationData = function () {
                        return $http({
                            method: "GET",
                            url: "/ContactInvitation/GetAllConInv",
                            contentType: "application/json"
                        })
                    }
                    list.getallbuyerEmail = function (id) {
                        return $http({
                            method: "GET",
                            url: "/Buyer/getbuyerEmail?id=" + id
                        });
                    }
                    return list;
                }])
})()