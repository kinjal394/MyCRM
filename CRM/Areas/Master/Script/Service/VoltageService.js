(function () {
    angular.module("CRMApp.Services", [])
            .service("VoltageService", ["$http",
                function ($http) {
                    var list = {};

                    // GETById DATA
                    list.GetByIdVoltage = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/Voltage/GetByIdVoltage?VoltageId=" + id
                        });
                    }

                    //CREATE/UPDATE RECORD
                    list.CreateUpdateVoltage = function (data) {

                        return $http({
                            method: "POST",
                            url: "/Master/Voltage/SaveVoltage",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    // DELETE RECORD
                    list.DeleteVoltage = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/Voltage/DeleteVoltage?VoltageId=" + id
                        })
                    }


                    return list;
                }]);
})()