(function () {
    angular.module("CRMApp.Services", [])
            .service("DesignationService", ["$http",
                function ($http) {
                    var list = {};

                    // GETById DATA
                    list.GetByIdDesignation = function (id) {
                        return $http({
                            method: "POST",
                            url: "/Master/Designation/GetByIdDesignation?DesignationId=" + id
                        });
                    }

                    //CREATE/UPDATE RECORD
                    list.CreateUpdateDesignation = function (data) {
                        return $http({
                            method: "POST",
                            url: "/Master/Designation/SaveDesignation",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    // DELETE RECORD
                    list.DeleteDesignation = function (id) {
                        return $http({
                            method: "POST",
                            url: "/Master/Designation/DeleteDesignation?DesignationId=" + id
                        })
                    }
                    

                    return list;
                }]);
})()