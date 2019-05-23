(function () {
    angular.module("CRMApp.Services", [])
            .service("SalaryHeadService", ["$http",
                function ($http) {
                    var list = {};

                    // GETById DATA
                    list.GetByIdSalaryHead = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/SalaryHead/GetByIdSalaryHead?SalaryHeadId=" + id
                        });
                    }

                    //CREATE/UPDATE RECORD
                    list.CreateUpdateSalaryHead = function (data) {

                        return $http({
                            method: "POST",
                            url: "/Master/SalaryHead/SaveSalaryHead",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    // DELETE RECORD
                    list.DeleteSalaryHead = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/SalaryHead/DeleteSalaryHead?SalaryHeadId=" + id
                        })
                    }


                    return list;
                }]);
})()