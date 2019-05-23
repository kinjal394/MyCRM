(function () {
    angular.module("CRMApp.Services")
            .service("IntervieweeCandidateService", [
                "$http",
                function ($http) {
                    var list = {};

                    // GETById DATA
                    list.FetchAllInfoById = function (id) {
                        return $http({
                            method: "GET",
                            url: "/Employee/IntervieweeCandidate/FetchAllInfoById?Id=" + id
                        });
                    }

                    //CREATE/UPDATE RECORD
                    list.CreateUpdate = function (data) {

                        return $http({
                            method: "POST",
                            url: "/Employee/IntervieweeCandidate/CreateUpdate",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    // DELETE RECORD
                    list.DeleteIntervieweeCandidate = function (id) {
                        return $http({
                            method: "POST",
                            url: "/Employee/IntervieweeCandidate/Delete?IntCandId=" + id
                        })
                    }

                    list.GetAllIntervieweeCandidate = function () {
                        return $http({
                            method: "GET",
                            url: "/Employee/IntervieweeCandidate/GetAllIntervieweeCandidate"
                        })
                    }
                    list.InterviewCanInfo = function () {
                        return $http({
                            method: "POST",
                            url: "/Employee/IntervieweeCandidate/InterviewCanInfo"
                        });
                    }
                    return list;
                }]);
})()