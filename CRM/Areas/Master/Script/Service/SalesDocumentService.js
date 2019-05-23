(function () {
    angular.module("CRMApp.Services", [])
            .service("SalesDocumentService", ["$http",
                function ($http) {
                    var list = {};

                    // GETById DATA
                    list.GetByIdSalesDocument = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/SalesDocument/GetByIdSalesDocument?SalesDocId=" + id
                        });
                    }

                    //CREATE/UPDATE RECORD
                    list.CreateUpdateSalesDocument = function (data) {

                        return $http({
                            method: "POST",
                            url: "/Master/SalesDocument/SaveSalesDocument",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    // DELETE RECORD
                    list.DeleteSalesDocument = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/SalesDocument/DeleteSalesDocument?SalesDocId=" + id
                        })
                    }


                    return list;
                }]);
})()