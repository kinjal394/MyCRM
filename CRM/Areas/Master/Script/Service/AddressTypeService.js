(function () {
    angular.module("CRMApp.Services", [])
            .service("AddressTypeService", ["$http",
                function ($http) {
                    var list = {};

                    // GETById DATA
                    list.GetByIdAddressType = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/AddressType/GetByIdAddressType?AddressTypeId=" + id
                        });
                    }

                    //CREATE/UPDATE RECORD
                    list.CreateUpdateAddressType = function (data) {

                        return $http({
                            method: "POST",
                            url: "/Master/AddressType/SaveAddressType",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    // DELETE RECORD
                    list.DeleteAddressType = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/AddressType/DeleteAddressType?AddressTypeId=" + id
                        })
                    }
                    return list;
                }]);
})()