(function () {
    angular.module("CRMApp.Services", [])
            .service("ProductDocumentService", ["$http",
                function ($http) {
                    var list = {};

                    // GETById DATA
                    list.GetByIdProductDocument = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/ProductDocument/GetByIdProductDocument?PrdDocId=" + id
                        });
                    }

                    //CREATE/UPDATE RECORD
                    list.CreateUpdateProductDocument = function (data) {

                        return $http({
                            method: "POST",
                            url: "/Master/ProductDocument/SaveProductDocument",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    // DELETE RECORD
                    list.DeleteProductDocument = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/ProductDocument/DeleteProductDocument?PrdDocId=" + id
                        })
                    }


                    return list;
                }]);
})()