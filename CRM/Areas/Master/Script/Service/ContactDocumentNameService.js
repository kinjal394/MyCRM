(function () {
    "User strict";
    angular.module("CRMApp.Services")
    .service("ContactDocumentNameService", ["$http",
    function ($http) {
        var list = {};

        list.getALLContactDocumentName = function () {
            return $http({
                method: "POST",
                url: "/Master/ContactDocumentName"
            });

        }
        list.AddContactDocumentName = function (data) {
            debugger;
            return $http({
                method: "POST",
                url: "/Master/ContactDocumentName/SaveContactDocumentName",
                data: data,
                contentType: "application/json"
            })
        }
        list.DeleteContactDocumentName = function (data) {
            return $http({
                method: "POST",
                url: "/Master/ContactDocumentName/DeleteContactDocumentName?ContactDocId=" + data
            })
        }
        list.GetContactDocumentNameById = function (id) {
            debugger;
            return $http({
                method: "POST",
                url: "/Master/ContactDocumentName/GetContactDocumentNameById?ContactDocId=" + id
            });
        }
        return list;

    }])

})()