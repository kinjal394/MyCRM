(function () {
    "User strict";
    angular.module("CRMApp.Services")
    .service("TOTypeService", ["$http",
    function ($http) {
    var list = {};

    list.getALLTOTypeName = function () {
        return $http({
            method: "POST",
            url: "/Master/TOType"
        });

    }
    list.AddTOType = function (data) {
        debugger;
        return $http({
            method: "POST",
            url: "/Master/TOType/SaveTOType",
            data: data,
            contentType: "application/json"
        })
    }
    list.DeleteTOType = function (data) {
        return $http({
            method: "POST",
            url: "/Master/TOType/DeleteTOType?TOTypeId=" + data
        })
    }
    list.GetTOTypeById = function (id) {
        debugger;
        return $http({
            method: "POST",
            url: "/Master/TOType/GetTOTypeById?TOTypeId=" + id
        });
    }
    return list;

}])

})()