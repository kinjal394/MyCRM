(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
           .controller("BankController", [
               "$scope", "BankService", "$filter", "$uibModal", "CountryService",
                function BankController($scope, BankService, $filter, $uibModal, CountryService) {
                    $scope.storage = {};
                    $scope.bankObj = {
                        BankId: 0,
                        BeneficiaryName: '',
                        //BankName: '',
                        BranchName: '',
                        AccountNo: '',
                        BankType: '',
                        IFSCCode: '',
                        MICRCode: '',
                        NickNameData: { Display: '', Value: '' },
                        SwiftCode: '',
                        RegisterEmail: '',
                        RegisterMobile: '',
                        CRNNo: '',
                        BankCustomerCareNo: '',
                        StatementPassword: '',
                        BankCustomerCareEmail: '',
                        Note: '',
                        // AccountType: '',
                        AccountTypeData: { Display: '', Value: '' },
                        BankNameData: { Display: '', Value: '' },
                        AccountTypeId: 0
                    }
                    $scope.telCodeData = [];
                    $scope.AddBank = function () {
                        CountryService.GetCountryFlag().then(function (result) {
                            $scope.telCodeData = angular.copy(result);
                        })
                        var _isdisable = 0;
                        // $scope.telCodeData = angular.copy(result);
                        $scope.bankObj = {
                            BankId: 0,
                            BeneficiaryName: '',
                            //BankName: '',
                            BranchName: '',
                            AccountNo: '',
                            BankType: '',
                            IFSCCode: '',
                            MICRCode: '',
                            NickNameData: { Display: '', Value: '' },
                            SwiftCode: '',
                            RegisterEmail: '',
                            RegisterMobile: '',
                            CRNNo: '',
                            BankCustomerCareNo: '',
                            StatementPassword: '',
                            BankCustomerCareEmail: '',
                            Note: '',
                            // AccountType: '',
                            AccountTypeData: { Display: '', Value: 0 },
                            BankNameData: { Display: '', Value: 0 },
                            AccountTypeId: 0
                        }

                        var modalInstance = $uibModal.open({
                            backdrop: 'static',
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'myModalContent.html',
                            controller: 'ModalInstanceCtrl',
                            controllerAs: '$ctrl',
                            size: 'lg',
                            resolve: {
                                bankObj: function () {
                                    return $scope.bankObj;
                                },
                                storage: function () {
                                    return $scope.storage;
                                },
                                isdisable: function () { return _isdisable; }
                            }
                        });

                        modalInstance.result.then(function () {
                            // $ctrl.selected = selectedItem;
                            $scope.refreshGrid();
                        }, function () {
                            // $log.info('Modal dismissed at: ' + new Date());
                        });
                    }

                    $scope.setDirectiveRefresh = function (refreshGrid) {
                        $scope.refreshGrid = refreshGrid;
                    };

                    $scope.gridObj = {
                        columnsInfo: [
                           //{ "title": "Bank Id", "field": "BankId", filter: false, show: false },
                           { "title": "Sr.", "field": "RowNumber", filter: false, show: true },
                           { "title": "Nick name/Ac Holder code", "field": "AcNickName", sortable: "AcNickName", filter: { AcNickName: "text" }, show: true },
                           { "title": "Beneficiary Bank name", "field": "BeneficiaryName", sortable: "BeneficiaryName", filter: { BeneficiaryName: "text" }, show: true },
                           { "title": "CRN no", "field": "CRNNo", sortable: "CRNNo", filter: { CRNNo: "text" }, show: true },
                           { "title": "Account no", "field": "AccountNo", sortable: "AccountNo", filter: { AccountNo: "text" }, show: true },
                           { "title": "Branch Name", "field": "BranchName", sortable: "BranchName", filter: { BranchName: "text" }, show: false },
                           { "title": "IFSC Code", "field": "IFSCCode", sortable: "IFSCCode", filter: { IFSCCode: "text" }, show: false },
                           { "title": "Swift Code", "field": "SwiftCode", sortable: "SwiftCode", filter: { SwiftCode: "text" }, show: false },
                           {
                               "title": "Action", show: true,
                               'cellTemplte': function ($scope, row) {
                                   var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.BankId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                                                //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.$parent.$parent.$parent.Delete(row.BankId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> ' +
                                               '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.BankId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>';
                                   return $scope.getHtml(element);
                               }
                           }
                        ],
                        Sort: { 'BankId': 'asc' }
                    }

                    $scope.Edit = function (id, _isdisable) {
                        CountryService.GetCountryFlag().then(function (result) {
                            $scope.telCodeData = angular.copy(result);
                        })
                        if (_isdisable === undefined) _isdisable = 0;
                        BankService.editBank(id).then(function (result) {
                            if (result.data.ResponseType == 1) {
                                var data = result.data.DataList;
                                if (data.SwiftCode == null) {
                                    data.BankType = 1;
                                }
                                else if (data.IFSCCode == null) {
                                    data.BankType = 2;
                                }
                                $scope.teliphone = (data.BankCustomerCareNo != '' && data.BankCustomerCareNo != null) ? data.BankCustomerCareNo.split(",") : [];
                                $scope.mail = (data.BankCustomerCareEmail != '' && data.BankCustomerCareEmail != null) ? data.BankCustomerCareEmail.split(",") : [];
                                $scope.bankObj = {
                                    BankId: data.BankId,
                                    BeneficiaryName: data.BeneficiaryName,
                                    // BankName: data.BankName,
                                    BranchName: data.BranchName,
                                    AccountNo: data.AccountNo,
                                    IFSCCode: data.IFSCCode,
                                    MICRCode: data.MICRCode,
                                    //NickName: data.NickName,
                                    NickNameData: { Display: data.AcNickName, Value: data.NickName },
                                    SwiftCode: data.SwiftCode,
                                    BankType: data.BankType,
                                    RegisterEmail: data.RegisterEmail,
                                    RegisterMobile: data.RegisterMobile,
                                    CRNNo: data.CRNNo,
                                    BankCustomerCareNo: $scope.teliphone.toString(),// data.BankCustomerCareNo,
                                    StatementPassword: data.StatementPassword,
                                    BankCustomerCareEmail: $scope.mail.toString(),
                                    Note: data.Note,
                                    // AccountType: data.AccountType,
                                    AccountTypeData: { Display: data.AccountType, Value: data.AccountTypeId },
                                    BankNameData: { Display: data.BankName, Value: data.BankNameId },
                                    AccountTypeId: data.AccountTypeId,
                                    /// BankName: data.BankName,
                                    BankNameId: data.BankNameId

                                }
                                $scope.storage = angular.copy($scope.bankObj);
                                var modalInstance = $uibModal.open({
                                    backdrop: 'static',
                                    animation: true,
                                    ariaLabelledBy: 'modal-title',
                                    ariaDescribedBy: 'modal-body',
                                    templateUrl: 'myModalContent.html',
                                    controller: 'ModalInstanceCtrl',
                                    controllerAs: '$ctrl',
                                    size: 'lg',
                                    resolve: {
                                        bankObj: function () {
                                            return $scope.bankObj;
                                        }, storage: function () {
                                            return $scope.storage;
                                        },
                                        isdisable: function () { return _isdisable; }

                                    }
                                });

                                modalInstance.result.then(function (selectedItem) {
                                    // $scope.tableParams.reload();
                                    $scope.refreshGrid();
                                }, function () {
                                    // $log.info('Modal dismissed at: ' + new Date());
                                });
                            } else {
                                toastr.error(result.data.Message, 'Opps, Something went wrong');
                            }
                        }, function (errorMsg) {
                            toastr.error(errorMsg, 'Opps, Something went wrong');
                        });


                    }

                    $scope.View = function (id) {
                        $scope.Edit(id, 1);

                    }

                    $scope.Delete = function (bankid) {
                        BankService.Deletebanks(bankid).then(function (result) {
                            if (result.data.ResponseType == 1) {
                                toastr.success(result.data.Message)
                            }
                            else {
                                toastr.error(result.data.Message)
                            }
                            $scope.refreshGrid();
                        }, function (errorMsg) {
                            toastr.error(errorMsg, 'Opps, Something went wrong');
                        })

                    }

                    function blockSpecialChar(e) {
                        var k;
                        document.all ? k = e.keyCode : k = e.which;
                        return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57));
                    }

                }]);

    angular.module('CRMApp.Controllers').controller('ModalInstanceCtrl', [
        '$scope', '$uibModalInstance', 'bankObj', 'BankService', 'storage', 'isdisable',
        function ($scope, $uibModalInstance, bankObj, BankService, storage, isdisable) {
            // $scope.telCodeData = bankObj.telCodeData;



            $scope.isClicked = false;
            if (isdisable == 1) {
                $scope.isClicked = true;
            }

            var $ctrl = this;

            $ctrl.ok = function () {
                $uibModalInstance.close();
            };

            $ctrl.bankData = bankObj;
            $ctrl.teliphone = ($ctrl.bankData.BankCustomerCareNo != '' && $ctrl.bankData.BankCustomerCareNo != null) ? $ctrl.bankData.BankCustomerCareNo.split(",") : [];
            $ctrl.mail = ($ctrl.bankData.BankCustomerCareEmail != '' && $ctrl.bankData.BankCustomerCareEmail != null) ? $ctrl.bankData.BankCustomerCareEmail.split(",") : [];
            $ctrl.storage = storage;

            $ctrl.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };

            $ctrl.savebank = function (bankData) {
                //if (bankData.BankType == 2) {
                //    bankData.IFSCCode = "";
                //}
                //else if (bankData.BankType == 1) {
                //    bankData.SwiftCode = "";
                //}
                bankData.AccountTypeId = bankData.AccountTypeData.Value;
                bankData.BankNameId = bankData.BankNameData.Value;
                bankData.NickName = bankData.NickNameData.Value;
                bankData.BankCustomerCareEmail = this.mail.toString();
                bankData.BankCustomerCareNo = this.teliphone.toString();
                BankService.addBank(bankData).then(function (result) {
                    if (result.data.ResponseType == 1) {

                        $uibModalInstance.close();
                        toastr.success(result.data.Message)
                        //$ctrl.bankData = {
                        //    BankId: 0,
                        //    BeneficiaryName: '',
                        //    BankName: '',
                        //    BranchName: '',
                        //    AccountNo: '',
                        //    BankType: '',
                        //    IFSCCode: '',
                        //    NickName: '',
                        //    SwiftCode: ''
                        //}
                        $ctrl.BankInfo.$setPristine();
                        $ctrl.BankInfo.$setUntouched();

                    }
                    else {
                        toastr.error(result.data.Message)
                    }
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }

            $ctrl.Update = function (bankData) {
                //if (bankData.BankType == 2) {
                //    bankData.IFSCCode = "";
                //}
                //else if (bankData.BankType == 1) {
                //    bankData.SwiftCode = "";
                //}
                //debugger;
                bankData.AccountTypeId = $ctrl.bankData.AccountTypeData.Value;
                bankData.BankNameId = bankData.BankNameData.Value;
                bankData.NickName = bankData.NickNameData.Value;
                bankData.BankCustomerCareEmail = this.mail.toString();
                bankData.BankCustomerCareNo = this.teliphone.toString();
                //BankCustomerCareNo: this.teliphone.toString()
                BankService.Update(bankData).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        $uibModalInstance.close();
                        toastr.success(result.data.Message)
                    }
                    else {
                        toastr.error(result.data.Message)
                    }
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            }

            $ctrl.Reset = function () {
                if ($ctrl.bankData.BankId > 0) {

                    $ctrl.bankData = angular.copy($ctrl.storage);

                } else {
                    ResetForm();
                }
            }

            function ResetForm() {

                $ctrl.bankData = {
                    BankId: 0,
                    BeneficiaryName: '',
                    bankname: '',
                    branchname: '',
                    accountno: '',
                    banktype: '',
                    ifsccode: '',
                    NickNameData: { Display: '', Value: '' },
                    SwiftCode: '',
                    RegisterEmail: '',
                    MICRCode: '',
                    RegisterMobile: '',
                    CRNNo: '',
                    BankCustomerCareNo: '',
                    StatementPassword: '',
                    BankCustomerCareEmail: '',
                    Note: '',
                    //AccountType: '',
                    AccountTypeData: { Display: '', Value: '' },
                    BankNameData: { Display: '', Value: '' },
                    AccountTypeId: 0
                }
                $ctrl.storage = {};
                $ctrl.addMode = true;
                $ctrl.saveText = "Save";
                if ($ctrl.$parent.BankInfo)
                    $ctrl.$parent.BankInfo.$setPristine();
            }

        }]);



})();