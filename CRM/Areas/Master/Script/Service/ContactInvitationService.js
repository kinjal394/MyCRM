(function () {
    angular.module("CRMApp.Services", [])
            .service("ContactInvitationService", ["$http",
                function ($http) {
                    var list = {};

                    // GETById DATA
                    list.GetByIdConInv = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/ContactInvitation/GetByIdConInv?ContactInvitationId=" + id
                        });
                    }

                    //CREATE/UPDATE RECORD
                    list.CreateUpdateConInv = function (data) {

                        return $http({
                            method: "POST",
                            url: "/Master/ContactInvitation/SaveConInv",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    // DELETE RECORD
                    list.DeleteConInv = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/ContactInvitation/DeleteConInv?ContactInvitationId=" + id
                        })
                    }


                    return list;
                }]);
})()