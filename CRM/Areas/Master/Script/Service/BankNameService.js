(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("BankNameService", ["$http",
            function ($http) {
                var list = {};
                list.getAllBankName = function () {
                    return $http({
                        method: "POST",
                        url: "/Master/BankName"
                    });
                }

                list.AddBankName = function (data) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/BankName/SaveBankName",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.DeleteBankName = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/BankName/DeleteBankName?BankId=" + data
                    })
                }
                list.GetBankNameById = function (id) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/BankName/GetBankNameById?BankId=" + id
                    });
                }
                return list;
            }])
})()