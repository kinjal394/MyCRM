(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("ChatNameService", ["$http",
            function ($http) {
                var list = {};
                list.getAllChatName = function () {
                    return $http({
                        method: "POST",
                        url: "/Master/ChatName"
                    });
                }

                list.AddChatName = function (data) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/ChatName/SaveChatName",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.DeleteChatName = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/ChatName/DeleteChatName?ChatId=" + data
                    })
                }
                list.GetChatNameById = function (id) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/ChatName/GetChatNameById?ChatId=" + id
                    });
                }
                return list;
            }])
})()