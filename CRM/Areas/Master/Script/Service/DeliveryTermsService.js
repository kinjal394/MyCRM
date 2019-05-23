(function () {
    "use strict";
    angular.module("CRMApp.Services", [])
            .service("DeliveryTermsService", ["$http",
                function ($http) {
                    var list = {};

                    // GETById DATA
                    list.GetByIdDeliveryTerms = function (id) {
                        return $http({
                            method: "POST",
                            url: "/Master/DeliveryTerms/GetByIdDeliveryTerms?DeliveryTermsId=" + id
                        });
                    }

                    //CREATE/UPDATE RECORD
                    list.CreateUpdateDeliveryTerms = function (data) {
                        return $http({
                            method: "POST",
                            url: "/Master/DeliveryTerms/SaveDeliveryTerms",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    // DELETE RECORD
                    list.DeleteDeliveryTerms = function (id) {
                        return $http({
                            method: "POST",
                            url: "/Master/DeliveryTerms/DeleteDeliveryTerms?DeliveryTermsId=" + id
                        })
                    }

                    return list;
                }]);
})()