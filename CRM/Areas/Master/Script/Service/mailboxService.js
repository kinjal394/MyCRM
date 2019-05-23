(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("mailboxService", ["$http",
                function ($http) {
                    var list = {};

                    list.CreateUpdate = function (data) {
                        return $http({
                            method: "POST",
                            url: "/EmailBox/SetEmail",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                   
                    return list;
                }])
})()