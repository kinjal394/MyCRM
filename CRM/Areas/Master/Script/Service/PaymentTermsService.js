(function () {
    "use strict";
    angular.module("CRMApp.Services", [])
            .service("PaymentTermsService", ["$http",
                function ($http) {
                    var list = {};

                    // GETById DATA
                    list.GetByIdPaymentTerms = function (id) {
                        return $http({
                            method: "POST",
                            url: "/Master/PaymentTerms/GetByIdPaymentTerms?PaymentTermId=" + id
                        });
                    }

                    //CREATE/UPDATE
                    list.CreateUpdatePaymentTerms = function (data) {
                        return $http({
                            method: "POST",
                            url: "/Master/PaymentTerms/SavePaymentTerms",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    // DELETE RECORD
                    list.DeletePaymentTerms = function (id) {
                        return $http({
                            method: "POST",
                            url: "/Master/PaymentTerms/DeletePaymentTerms?PaymentTermId=" + id
                        })
                    }

                    return list;
                }]);
})()