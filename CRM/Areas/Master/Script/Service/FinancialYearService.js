(function () {
    angular.module("CRMApp.Services", [])
            .service("FinancialYearService", ["$http",
                function ($http) {
                    var list = {};

                    // GETById DATA
                    list.GetByIdFinancialYear = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/FinancialYear/GetByIdFinancialYear?FinancialYearId=" + id
                        });
                    }

                    //CREATE/UPDATE RECORD
                    list.CreateUpdateFinancialYear = function (data) {

                        return $http({
                            method: "POST",
                            url: "/Master/FinancialYear/SaveFinancialYear",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    // DELETE RECORD
                    list.DeleteFinancialYear = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/FinancialYear/DeleteFinancialYear?FinancialYearId=" + id
                        })
                    }


                    return list;
                }]);
})()