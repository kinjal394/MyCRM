(function () {
    angular.module("CRMApp.Services", [])
            .service("WorkReminderService", ["$http",
                function ($http) {
                    var list = {};

                    // GETById DATA
                    list.GetByIdWorkReminder = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/WorkReminder/GetByIdWorkReminder?WorkRemindId=" + id
                        });
                    }

                    //CREATE/UPDATE RECORD
                    list.CreateUpdateWorkReminder = function (data) {

                        return $http({
                            method: "POST",
                            url: "/Master/WorkReminder/SaveWorkReminder",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    // DELETE RECORD
                    list.DeleteWorkReminder = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/WorkReminder/DeleteWorkReminder?WorkRemindId=" + id
                        })
                    }


                    return list;
                }]);
})()