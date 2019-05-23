(function () {
    angular.module("CRMApp.Services", [])
            .service("PhaseService", ["$http",
                function ($http) {
                    var list = {};

                    // GETById DATA
                    list.GetByIdPhase = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/Phase/GetByIdPhase?PhaseId=" + id
                        });
                    }

                    //CREATE/UPDATE RECORD
                    list.CreateUpdatePhase = function (data) {

                        return $http({
                            method: "POST",
                            url: "/Master/Phase/SavePhase",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    // DELETE RECORD
                    list.DeletePhase = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/Phase/DeletePhase?PhaseId=" + id
                        })
                    }
                    return list;
                }]);
})()