(function () {

    "use strict";
    angular.module("CRMApp.Services")
          .service("PlugShapeService", ["$http",
          function ($http) {

              var list = {};

              list.CreateUpdate = function (data) {
                  return $http({
                      method: "POST",
                      url: "/Master/PlugShape/CreateUpdate",
                      data: data,
                      contentType: "application/json"
                  })
              }

              list.Delete = function (id) {
                  return $http({
                      method: "POST",
                      url: "/Master/PlugShape/Delete?Id=" + id
                  })
              }

              list.GetById = function (id) {
                  return $http({
                      method: "Get",
                      url: "/Master/PlugShape/GetById?Id=" + id
                  });
              }

              return list;
          }])
})()