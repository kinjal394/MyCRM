(function () {
    "use strict";

    angular.module("CRMApp.Services")
        .service("RelationService", ["$http",
        function ($http) {

            var list = {};

            list.CreateUpdate = function (data) {
                return $http({
                    method: "POST",
                    url: "/Employee/Relation/CreateUpdate",
                    data: data,
                    contentType: "application/json"
                })
            }

            list.Delete = function (id) {
                return $http({
                    method: "POST",
                    url: "/Employee/Relation/Delete?Id=" + id
                })
            }

            list.GetById = function (id) {
                return $http({
                    method: "Get",
                    url: "/Employee/Relation/GetById?Id=" + id
                });
            }
            list.GetRelation = function () {
                return $http({
                    method: 'post',
                    url: '/Employee/Relation/getRelationData',
                    contentType: 'application/json; charset=utf-8',
                    data: {},
                    responseType: 'json',
                }).then(function (result) {
                    var data = [];
                    _.each(result.data, function (value, key, list) {
                        data.push({
                            name: value.RelationName,
                            id: value.RelationId,
                            ticked: false
                        })
                    })
                    return data;
                });
            }
            return list;
        }])
})()