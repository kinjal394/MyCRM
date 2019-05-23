(function () {
    angular.module("CRMApp.Services", [])
            .service("PaymentModeService", ["$http",
                function ($http) {
                    var list = {};

                    // GETById DATA
                    list.GetByIdPaymentMode = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/PaymentMode/GetByIdPaymentMode?PaymentModeId=" + id
                        });
                    }

                    //CREATE/UPDATE RECORD
                    list.CreateUpdatePaymentMode = function (data) {

                        return $http({
                            method: "POST",
                            url: "/Master/PaymentMode/SavePaymentMode",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    // DELETE RECORD
                    list.DeletePaymentMode = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/PaymentMode/DeletePaymentMode?PaymentModeId=" + id
                        })
                    }


                    return list;
                }]);
})()