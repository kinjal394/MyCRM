(function () {
    angular.module("CRMApp.Services", [])
            .service("ReportFormatService", ["$http",
                function ($http) {
                    var list = {};

                    // GETById DATA
                    list.GetByIdReport = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/ReportFormat/GetByIdReport?RotFormatId=" + id
                        });
                    }

                    //CREATE/UPDATE RECORD
                    list.CreateUpdateReport = function (data) {

                        return $http({
                            method: "POST",
                            url: "/Master/ReportFormat/SaveReport",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    // DELETE RECORD
                    list.DeleteReport = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/ReportFormat/DeleteReport?RotFormatId=" + id
                        })
                    }


                    return list;
                }]);
})()