(function () {
    angular.module("CRMApp.Services", [])
            .service("ITRService", ["$http",
                function ($http) {
                    var list = {};

                    // GETById DATA
                    list.GetByIdITR = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/ITR/GetByIdITR?ITRId=" + id
                        });
                    }

                    //CREATE/UPDATE RECORD
                    list.CreateUpdateITR = function (data) {

                        return $http({
                            method: "POST",
                            url: "/Master/ITR/SaveITR",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    // DELETE RECORD
                    list.DeleteITR = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/ITR/DeleteITR?ITRId=" + id
                        })
                    }
                    return list;
                }]);
})()