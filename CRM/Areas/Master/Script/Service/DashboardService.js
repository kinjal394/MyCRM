(function () {
    angular.module("CRMApp.Services")
            .service("DashboardService", ["$http",
                function ($http) {
                    var list = {};

                    // GETById DATA
                    list.GetByIdDashboard = function (id) {
                        return $http({
                            method: "POST",
                            url: "/Master/Dashboard/GetByIdDashboard?DashboardId=" + id
                        });
                    }

                    //SAVE RECORD
                    list.SaveDailyWork = function (data) {
                        return $http({
                            method: "POST",
                            url: "/Master/Dashboard/SaveDailyWork",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    // DELETE RECORD
                    list.DeleteDashboard = function (id) {
                        return $http({
                            method: "POST",
                            url: "/Master/Dashboard/DeleteDashboard?DashboardId=" + id
                        })
                    }

                    
                    list.GetDashbordData = function () {
                        return $http({
                            method: "GET",
                            url: "/Master/Dashboard/GetDashbordData"
                        })
                    }

                    //SAVE RECORD
                    list.AjaxNotGoodForRedirect = function (url) {
                        return $http({
                            method: "POST",
                            url: url,
                            contentType: "application/json"
                        }).success(function (data) {
                            return data
                        }).error(function (e) {
                            return e
                        })
                    }

                    list.GetAllDailyWorkInfo = function (id) {
                        return $http({
                            method: "GET",
                            url: "/Master/Dashboard/GetAllDailyWorkInfo?id=" + id,
                            contentType: "application/json"
                        }).success(function (data) {
                            return data
                        }).error(function (e) {
                            return e
                        })
                    }
                    

                    list.GetChartdata = function (id) {
                        
                        return $http({
                            method: "GET",
                            url: "/Master/Dashboard/GetChartDetails?id=" + id,
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