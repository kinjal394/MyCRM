(function () {
    "use strict"
    angular.module("CRMApp.Controllers")
                .controller("QualificationCtrl", [
                    "$scope", "$rootScope", "$timeout", "$filter", "QualificationService", "NgTableParams", "$uibModal",
                    QualificationCtrl
                ])

    function QualificationCtrl($scope, $rootScope, $timeout, $filter, QualificationService, NgTableParams, $uibModal) {
        $scope.Add = function (id, _isdisable) {
            if (_isdisable === undefined) _isdisable = 0;

            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    QualificationService: function () { return QualificationService; },
                    id: function () { return id; },
                    isdisable: function () { return _isdisable; }
                }
            });
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
                // $log.info('Modal dismissed at: ' + new Date());
            });
        }

        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };

        $scope.gridObj = {
            columnsInfo: [
               //{ "title": "Qualification Id", "data": "QualificationId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "Qualification Name", "field": "QualificationName", sortable: "QualificationName", filter: { QualificationName: "text" }, show: true, },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.QualificationId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                           //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.QualificationId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                            '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.QualificationId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { "QualificationId": "asc" }
        }


        $scope.Edit = function (id) {
            $scope.Add(id, 0);
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
            });
        }

        $scope.View = function (id) {
            $scope.Add(id, 1);
            modalInstance.result.then(function () {
                $scope.refreshGrid()
            }, function () {
            });
        }

        $scope.Delete = function (id) {
            QualificationService.DeleteQual(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
                $scope.refreshGrid()
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }
        $scope.RefreshTable = function () {
            $scope.tableParams.reload();
        };
    }

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, QualificationService, id, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.objQual = $scope.objQual || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.id = id;
        $scope.storage = {};
        if (id && id > 0) {
            QualificationService.GetByIdQual(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objQual = {
                        QualificationId: result.data.DataList.QualificationId,
                        QualificationName: result.data.DataList.QualificationName
                    }
                    $scope.storage = angular.copy($scope.objQual);
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        function ResetForm() {
            $scope.objQual = {
                QualificationId: 0,
                QualificationName: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormQualInfo)
                $scope.$parent.FormQualInfo.$setPristine();
        }

        $scope.CreateUpdate = function (data) {
            QualificationService.CreateUpdateQual(data).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    ResetForm();
                    $uibModalInstance.close();
                } else {
                    toastr.error(result.data.Message);
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.Reset = function () {
            if ($scope.objQual.QualificationId > 0) {
                $scope.objQual = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }
        $scope.close = function () {
            $uibModalInstance.close();
        };
    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'QualificationService', 'id', 'isdisable']
})()






