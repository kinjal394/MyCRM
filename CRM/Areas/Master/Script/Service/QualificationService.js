(function () {
    angular.module("CRMApp.Services", [])
            .service("QualificationService", ["$http",
                function ($http) {
                    var list = {};

                    // GETById DATA
                    list.GetByIdQual = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/Qualification/GetByIdQual?QualId=" + id
                        });
                    }

                    //CREATE/UPDATE RECORD
                    list.CreateUpdateQual = function (data) {

                        return $http({
                            method: "POST",
                            url: "/Master/Qualification/SaveQual",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    // DELETE RECORD
                    list.DeleteQual = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/Qualification/DeleteQual?QualId=" + id
                        })
                    }
                    return list;
                }]);
})()