(function () {
    angular.module("CRMApp.Services", [])
            .service("ApplicableChargeService", ["$http",
                function ($http) {
                    var list = {};

                    // GETById DATA
                    list.GetByIdApplicableCharge = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/ApplicableCharge/GetByIdApplicableCharge?ApplicableChargeId=" + id
                        });
                    }

                    //CREATE/UPDATE RECORD
                    list.CreateUpdateApplicableCharge = function (data) {

                        return $http({
                            method: "POST",
                            url: "/Master/ApplicableCharge/SaveApplicableChar",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    // DELETE RECORD
                    list.DeleteApplicableCharge = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/ApplicableCharge/DeleteApplicableChar?ApplicableChargeId=" + id
                        })
                    }


                    return list;
                }]);
})()