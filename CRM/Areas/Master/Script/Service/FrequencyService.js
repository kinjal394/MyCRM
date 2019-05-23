(function () {
    angular.module("CRMApp.Services", [])
            .service("FrequencyService", ["$http",
                function ($http) {
                    var list = {};

                    // GETById DATA
                    list.GetByIdFrequency = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/Frequency/GetByIdFrequency?FrequencyId=" + id
                        });
                    }

                    //CREATE/UPDATE RECORD
                    list.CreateUpdateFrequency = function (data) {

                        return $http({
                            method: "POST",
                            url: "/Master/Frequency/SaveFrequency",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    // DELETE RECORD
                    list.DeleteFrequency = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/Frequency/DeleteFrequency?FrequencyId=" + id
                        })
                    }


                    return list;
                }]);
})()