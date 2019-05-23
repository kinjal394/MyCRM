(function () {
    angular.module("CRMApp.Services", [])
            .service("AgencyTypeService", ["$http",
                function ($http) {
                    var list = {};

                    // GETById DATA
                    list.GetByIdAgencyType = function (id) {
                        
                        return $http({
                            method: "POST",
                            url: "/Master/AgencyType/GetByIdAgencyType?AgencyTypeId=" + id
                        });
                    }

                    //CREATE/UPDATE RECORD
                    list.CreateUpdateAgencyType = function (data) {
                        
                        return $http({
                            method: "POST",
                            url: "/Master/AgencyType/SaveAgencyType",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    // DELETE RECORD
                    list.DeleteAgencyType = function (id) {
                       
                        return $http({
                            method: "POST",
                            url: "/Master/AgencyType/DeleteAgencyType?AgencyTypeId=" + id
                        })
                    }


                    return list;
                }]);
})()