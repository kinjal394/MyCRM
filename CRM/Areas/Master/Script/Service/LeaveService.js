(function () {
    angular.module("CRMApp.Services", [])
            .service("LeaveService", ["$http",
                function ($http) {
                    var list = {};
                    // GET ALL DATA
                    list.GetAllLeave = function () {
                        return $http({
                            method: "POST",
                            url: "/Master/Leave"
                        });
                    }

                    // GETById DATA
                    list.GetByIdLeave = function (id) {
                        return $http({
                            method: "POST",
                            url: "/Master/Leave/GetByIdLeave?LeaveId=" + id
                        });
                    }

                    //CREATE/Update RECORD
                    list.SaveLeave = function (data) {
                        return $http({
                            method: "POST",
                            url: "/Master/Leave/SaveLeave",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    //UPDATE RECORD :// Use IN Aproval site
                    list.UpdateLeave = function (data) {
                        return $http({
                            method: "POST",
                            url: "/Master/Leave/UpdateLeave",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    // DELETE RECORD
                    list.DeleteLeave = function (id) {
                        return $http({
                            method: "POST",
                            url: "/Master/Leave/DeleteLeave?LeaveId=" + id
                        })
                    }
                    
                    return list;
                }]);
})()