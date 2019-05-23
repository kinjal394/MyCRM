(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("SourceService", ["$http",
            function ($http) {
                var list = {};
                list.getAllSource = function () {
                    return $http({
                        method: "POST",
                        url: "/Master/Source"
                    });
                }

                list.AddSource = function (data) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/Source/SaveSource",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.DeleteSource = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/Source/DeleteSource?SourceId=" + data
                    })
                }
                list.GetSourceById = function (id) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/Source/GetSourceById?SourceId=" + id
                    });
                }

                list.GetSourceData = function () {
                    return $http({
                        method: "GET",
                        url: "/Master/Source/SourceBind",
                        contentType: 'application/json; charset=utf-8',
                        data: {},
                        responseType: 'json',
                    }).then(function (result) {
                        var data = [];
                        _.each(result.data.DataList, function (value, key, list) {
                            data.push({
                                icon: value.SourceName,
                                id: value.SourceId,
                                ticked: false
                            })
                        })
                        return data;
                    });
                }


                return list;
            }])
})()