(function () {
    angular.module("CRMApp.Services", [])
            .service("AcHolderService", ["$http",
                function ($http) {
                    var list = {};

                    // GETById DATA
                    list.GetByIdAcHolder = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/AcHolder/GetByIdAcHolder?AcHolderCode=" + id
                        });
                    }

                    //CREATE/UPDATE RECORD
                    list.CreateUpdateAcHolder = function (data) {

                        return $http({
                            method: "POST",
                            url: "/Master/AcHolder/SaveAcHolder",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    // DELETE RECORD
                    list.DeleteAcHolder = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/AcHolder/DeleteAcHolder?AcHolderCode=" + id
                        })
                    }


                    return list;
                }]);
})()