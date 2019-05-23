

(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("AdvertisementSourceService", ["$http",
            function ($http) {
                var list = {};
                list.GetAllAdvertSource = function () {
                    return $http({
                        method: "POST",
                        url: "/Master/AdvertisementSource"
                    });
                }

                list.AddAdvertSource = function (data) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/AdvertisementSource/SaveAdvertSource",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.DeleteAdvertSource = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/AdvertisementSource/DeleteAdvertSource?SiteId=" + data
                    })
                }
                list.GetAdvertSourceById = function (id) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/AdvertisementSource/GetAdvertSourceById?SiteId=" + id
                    });
                }
                return list;
            }])
})()