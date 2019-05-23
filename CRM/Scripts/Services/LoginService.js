

(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("LoginService", ["$http",
            function ($http) {
               
                var list = {};
                list.GetAllHistory = function () { 
                    return $http({
                        method: "Get",
                        url: "/Login/LogInReport"
                    });
                }
                return list;
            }])
})()