(function () {
    angular.module("CRMApp.Services", [])
        .service("TransactionTypeService", ["$http",
            function ($http) {
                var list = {};

                // GETById DATA
                list.GetByIdTransactionType = function (id) {

                    return $http({
                        method: "POST",
                        url: "/Master/TransactionType/GetByIdTransactionType?TranTypeId=" + id
                    });
                }

                //CREATE/UPDATE RECORD
                list.CreateUpdateTransactionType = function (data) {

                    return $http({
                        method: "POST",
                        url: "/Master/TransactionType/SaveTransactionType",
                        data: data,
                        contentType: "application/json"
                    })
                }

                // DELETE RECORD
                list.DeleteTransactionType = function (id) {

                    return $http({
                        method: "POST",
                        url: "/Master/TransactionType/DeleteTransactionType?TranTypeId=" + id
                    })
                }


                return list;
            }]);
})()