(function () {
    angular.module("CRMApp.Services", [])
            .service("SpecificationService", ["$http",
                function ($http) {
                    var list = {};

                    // GETById DATA
                    list.GetByIdSpecification = function (id) {
                        return $http({
                            method: "POST",
                            url: "/Master/Specification/GetByIdSpecification?SpecificationId=" + id
                        });
                    }

                    //CREATE/UPDATE RECORD
                    list.CreateUpdateSpecification = function (data) {
                        return $http({
                            method: "POST",
                            url: "/Master/Specification/SaveSpecification",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    // DELETE RECORD
                    list.DeleteSpecification = function (id) {
                        return $http({
                            method: "POST",
                            url: "/Master/Specification/DeleteSpecification?SpecificationId=" + id
                        })
                    }


                    return list;
                }]);
})()