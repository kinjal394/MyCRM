(function () {
    angular.module("CRMApp.Services")
            .service("DailyReportingService", ["$http",
                function ($http) {
                    var list = {};

                    // GETById DATA
                    list.GetByIdDailyReporting = function (id) {
                        return $http({
                            method: "POST",
                            url: "/Master/DailyReporting/GetByIdDailyReporting?DailyReportingId=" + id
                        });
                    }

                    //SAVE RECORD
                    list.CreateUpdateDailyReporting = function (data) {
                        return $http({
                            method: "POST",
                            url: "/Master/DailyReporting/CreateUpdate",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    // DELETE RECORD
                    list.DeleteDailyReporting = function (id) {
                        return $http({
                            method: "POST",
                            url: "/Master/DailyReporting/DeleteDailyReporting?DailyReportingId=" + id
                        })
                    }

                    //SAVE RECORD
                    list.AjaxNotGoodForRedirect = function (url) {
                        return $http({
                            method: "GET",
                            url: url,
                            contentType: "application/json"
                        }).success(function (data) {
                            return data
                        }).error(function (e) {
                            return e
                        })
                    }

                    list.GetTaskInqData = function (val, id) {
                        return $http({
                            method: "GET",
                            url: "/Master/DailyReporting/GetDailyWorkreportByID?taskinqno=" + val + "&id=" + id,
                            contentType: "application/json"
                        }).success(function (data) {
                            return data
                        }).error(function (e) {
                            return e
                        })
                    }
                    list.GetDailyWorkReportingByID = function (id) {
                        return $http({
                            method: "GET",
                            url: "/Master/DailyReporting/GetDailyWorkReportingByID?id=" + id,
                            contentType: "application/json"
                        }).success(function (data) {
                            return data
                        }).error(function (e) {
                            return e
                        })
                    }

                    return list;
                }]);
})()