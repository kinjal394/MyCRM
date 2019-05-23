(function () {
    angular.module("CRMApp.Services")
            .service("WorkProfileService", [
                "$http",
                function ($http) {
                    var list = {};

                    // GETById DATA
                    list.GetByIdWorkProfile = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Employee/WorkProfile/GetByIdWorkProfile?WorkProfileId=" + id
                        });
                    }

                    //CREATE/UPDATE RECORD
                    list.CreateUpdateWorkProfile = function (data) {

                        return $http({
                            method: "POST",
                            url: "/Employee/WorkProfile/SaveWorkProfile",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    // DELETE RECORD
                    list.DeleteWorkProfile = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Employee/WorkProfile/DeleteWorkProfile?WorkProfileId=" + id
                        })
                    }
                    
                    list.GetAllWorkProfile = function () {
                        return $http({
                            method: "GET",
                            url: "/Employee/WorkProfile/GetAllWorkProfile"
                        })
                    }

                    return list;
                }]);
})()