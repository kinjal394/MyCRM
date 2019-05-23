(function () {
    angular.module("CRMApp.Services", [])
            .service("TechnicalSpecHeadService", ["$http",
                function ($http) {
                    var list = {};

                    // GETById DATA
                    list.GetByIdTechnicalSpecHead = function (id) {
                        return $http({
                            method: "POST",
                            url: "/Master/TechnicalSpecHead/GetByIdTechnicalSpecHead?TechHeadId=" + id
                        });
                    }

                    // CREATE/UPDATE RECORD
                    list.CreateUpdateTechnicalSpecHead = function (data) {
                        return $http({
                            method: "POST",
                            url: "/Master/TechnicalSpecHead/SaveTechnicalSpecHead",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    // DELETE RECORD
                    list.DeleteTechnicalSpecHead = function (id) {
                        return $http({
                            method: "POST",
                            url: "/Master/TechnicalSpecHead/DeleteTechnicalSpecHead?TechHeadId=" + id
                        })
                    }

                    return list;
                }]);
})()