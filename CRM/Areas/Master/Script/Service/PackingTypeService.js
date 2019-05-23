(function () {
    angular.module("CRMApp.Services", [])
            .service("PackingTypeService", ["$http",
                function ($http) {
                    var list = {};

                    // GETById DATA
                    list.GetByIdPackingType = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/PackingType/GetByIdPackingType?PackingTypeId=" + id
                        });
                    }

                    //CREATE/UPDATE RECORD
                    list.CreateUpdatePackingType = function (data) {

                        return $http({
                            method: "POST",
                            url: "/Master/PackingType/SavePackingType",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    // DELETE RECORD
                    list.DeletePackingType = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/PackingType/DeletePackingType?PackingTypeId=" + id
                        })
                    }


                    return list;
                }]);
})()