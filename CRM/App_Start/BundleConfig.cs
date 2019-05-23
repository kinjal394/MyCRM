using System.Web;
using System.Web.Optimization;

namespace CRM
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region For Main Page
            bundles.Add(new ScriptBundle("~/bundles/MainPageRequired").Include(
                "~/Content/lib/jquery/jquery.min.js",
                "~/Content/lib/bootstrap/bootstrap.min.js",
                "~/Content/lib/bootstrap/bootstrap-hover-dropdown.min.js",
                "~/Content/lib/jquery/jquery.slimscroll.min.js",
                "~/Content/lib/jquery/jquery.blockui.min.js",
                "~/Content/lib/bootstrap/bootstrap-switch.min.js",
                "~/Content/lib/jquery/jquery.flot.min.js",
                "~/Content/lib/Template/js/app.min.js",
                "~/Content/lib/Template/js/layout.min.js",
                "~/Content/lib/angular/angular.js",
                "~/Content/lib/angular/angular-touch.min.js",
                "~/Content/lib/moment.js",
                "~/Content/lib/bootbox.js",
                "~/Content/lib/ng-table/ng-table.js",
                "~/Content/lib/ui-bootstrap-tpls-2.1.3.js",
                "~/Content/lib/toastr/toastr.min.js",
                "~/Content/lib/fileupload/ng-file-upload.js",
                "~/Content/lib/fileupload/ng-file-upload-shim.js",
                "~/Content/lib/ng-tags-input/ng-tags-input.js",
                "~/Content/lib/Calender/interact.min.js",
                "~/Content/lib/Calender/calendar.js",
                "~/Content/lib/Calender/fullcalendar.js",
                "~/Content/lib/Calender/gcal.js",
                "~/Content/lib/lodash.core.js",
                "~/Scripts/ckeditor/ckeditor.js",
                "~/Scripts/ckeditor/ckeditor_basic.js",
                "~/Content/lib/ng-ckeditor.min.js",
                "~/Content/lib/chart/Chart.js",
                "~/Content/lib/chart/angular-chart.js",
                "~/Scripts/Module/App.js",
                "~/Scripts/Directive/Loader.js",
                "~/Scripts/Filters/JsonToDateFilter.js",
                "~/Content/lib/isteven-multi-select.js",
                "~/Scripts/Controller/ExportModalInstanceCtrl.js",
                "~/Scripts/Controller/timer.js",
                "~/Scripts/Common.js"
                ));

            bundles.Add(new StyleBundle("~/Content/MainPageCss").Include(
                "~/Content/lib/Template/css/simple-line-icons.min.css",
                "~/Content/lib/bootstrap/bootstrap.min.css",
                "~/Content/lib/Template/css/components.min.css",
                "~/Content/lib/Template/css/layout.min.css",
                "~/Content/lib/Template/css/light.min.css",
                "~/Content/lib/Template/css/font-awesome.css",
                "~/Content/lib/ng-table/ng-table.min.css",
                "~/Content/lib/toastr/toastr.css",
                "~/Content/CSS/Custom.css",
                "~/Content/CSS/helper.css",
                "~/Content/CSS/autocomplete.css",
                "~/Content/CSS/ng-ckeditor.css",
                "~/Content/CSS/isteven-multi-select.css",
                "~/Content/lib/ng-tags-input/ng-tags-input.css",
                "~/Content/lib/Calender/fullcalendar.css"
                ));

            #endregion

            #region For Elaunch Directives
            bundles.Add(new ScriptBundle("~/bundles/ElaunchControls").Include(
               "~/Scripts/Directive/Loader.js",
               "~/Scripts/Directive/confirmbox.js",
              "~/Scripts/Directive/deleteConfirm.js",
              "~/Scripts/Directive/ElaunchCompiler.js",
              "~/Scripts/Directive/ElaunchAutoComplete.js",
              "~/Scripts/Directive/ElaunchCountryFlag.js",
              "~/Scripts/Directive/ElaunchGrid.js",
              "~/Scripts/Directive/ElaunchDatePicker.js",
              "~/Scripts/Directive/ElaunchTimePicker.js",
              "~/Scripts/Directive/ElaunchAttrfocus.js",
              "~/Scripts/Directive/ElaunchEmail.js",
              "~/Scripts/Directive/ElaunchMobile.js",
              "~/Scripts/Directive/ElaunchTextBox.js",
              "~/Scripts/Directive/ElaunchCommon.js",
              "~/Scripts/Filters/JsonToDateFilter.js",
              "~/Scripts/ng-file-upload-shim.min.js",
              "~/Scripts/ng-file-upload.min.js"
               ));
            #endregion

            #region For PDF Convert
            bundles.Add(new ScriptBundle("~/bundles/PdfConvert").Include(
                "~/Content/lib/jspdf.debug.js"
                ));
            #endregion

            #region For HighChart
            bundles.Add(new ScriptBundle("~/bundles/Highchart").Include(
                "~/Content/lib/highChart/highstock.js",
                "~/Content/lib/highChart/exporting.js"
                ));
            #endregion

            #region For DashboardHelper
            bundles.Add(new ScriptBundle("~/bundles/DashboardHelper").Include(
                "~/Content/lib/dashbord/nprogress/nprogress.js",
                "~/Content/lib/dashbord/Chart.js/dist/Chart.min.js",
                "~/Content/lib/dashbord/bootstrap-progressbar/bootstrap-progressbar.min.js",
                "~/Content/lib/dashbord/DateJS/build/date.js",
                "~/Content/lib/dashbord/jqvmap/dist/jquery.vmap.js",
                "~/Content/lib/dashbord/jqvmap/dist/maps/jquery.vmap.world.js",
                "~/Content/lib/dashbord/jqvmap/examples/js/jquery.vmap.sampledata.js",
                "~/Content/lib/dashbord/build/js/custom.min.js"
                ));
            #endregion

            #region For DashboardHelperCss
            bundles.Add(new StyleBundle("~/Content/DashboardHelperCss").Include(
                "~/Content/lib/dashbord/jqvmap/dist/jqvmap.css",
                "~/Content/lib/dashbord/build/css/custom.min.css"
                ));
            #endregion

            #region For TodayWork
            bundles.Add(new ScriptBundle("~/bundles/TodayWork").Include(
                "~/Areas/Transaction/Script/Controllers/TodayWorkCtrl.js",
                "~/Areas/Transaction/Script/Service/TodayWorkService.js"
                ));
            #endregion

            #region For EmployeeShift
            bundles.Add(new ScriptBundle("~/bundles/EmployeeShift").Include(
                "~/Areas/Employee/Scripts/Services/EmployeeShiftService.js",
                "~/Areas/Employee/Scripts/Controllers/EmployeeShiftCtrl.js"
                ));
            #endregion

            #region For RolePermission
            bundles.Add(new ScriptBundle("~/bundles/RolePermission").Include(
                "~/Areas/Employee/Scripts/Controllers/RolePermissionCtrl.js",
                "~/Areas/Employee/Scripts/Services/RolePermissionService.js"
                ));
            #endregion

            #region For ReferenceSource
            bundles.Add(new ScriptBundle("~/bundles/ReferenceSource").Include(
                "~/Areas/Master/Script/Controller/ReferenceSourceCtrl.js",
                "~/Areas/Master/Script/Service/ReferenceSourceService.js"
                ));
            #endregion

            #region For AccountEntry
            bundles.Add(new ScriptBundle("~/bundles/AccountEntry").Include(
                "~/Areas/Transaction/Script/Service/AccountEntryService.js",
                "~/Areas/Transaction/Script/Controllers/AccountEntryController.js"
                ));
            #endregion

            #region For TechnicalSpecHead
            bundles.Add(new ScriptBundle("~/bundles/TechnicalSpecHead").Include(
                "~/Areas/Master/Script/Service/TechnicalSpecHeadService.js",
                "~/Areas/Master/Script/Controller/TechnicalSpecHeadCtrl.js"
                ));
            #endregion

            #region For Holiday
            bundles.Add(new ScriptBundle("~/bundles/Holiday").Include(
                "~/Areas/Employee/Scripts/Services/HolidayService.js",
                "~/Areas/Employee/Scripts/Controllers/HolidayCtrl.js",
                "~/Areas/Master/Script/Service/ExhibitionService.js"
                ));
            #endregion

            #region For DailyReporting
            bundles.Add(new ScriptBundle("~/bundles/DailyReporting").Include(
                "~/Areas/Master/Script/Controller/DailyReportingCtrl.js",
                "~/Areas/Master/Script/Service/DailyReportingService.js"
                ));
            #endregion

            #region For AgencyType
            bundles.Add(new ScriptBundle("~/bundles/AgencyType").Include(
                "~/Areas/Master/Script/Controller/AgencyTypeCtrl.js",
                "~/Areas/Master/Script/Service/AgencyTypeService.js"
                ));
            #endregion

            #region For Voltage
            bundles.Add(new ScriptBundle("~/bundles/Voltage").Include(
                "~/Areas/Master/Script/Controller/VoltageCtrl.js",
                "~/Areas/Master/Script/Service/VoltageService.js"
                ));
            #endregion

            #region For Frequency
            bundles.Add(new ScriptBundle("~/bundles/Frequency").Include(
                "~/Areas/Master/Script/Controller/FrequencyCtrl.js",
                "~/Areas/Master/Script/Service/FrequencyService.js"
                ));
            #endregion

            #region For Phase
            bundles.Add(new ScriptBundle("~/bundles/Phase").Include(
                "~/Areas/Master/Script/Controller/PhaseCtrl.js",
                "~/Areas/Master/Script/Service/PhaseService.js"
                ));
            #endregion

            #region For ApplicableCharge
            bundles.Add(new ScriptBundle("~/bundles/ApplicableCharge").Include(
                "~/Areas/Master/Script/Controller/ApplicableChargeCtrl.js",
                "~/Areas/Master/Script/Service/ApplicableChargeService.js"
                ));
            #endregion

            #region For ReportFormat
            bundles.Add(new ScriptBundle("~/bundles/ReportFormat").Include(
                "~/Areas/Master/Script/Controller/ReportFormatCtrl.js",
                "~/Areas/Master/Script/Service/ReportFormatService.js"
                ));
            #endregion

            #region For ContactInvitation
            bundles.Add(new ScriptBundle("~/bundles/ContactInvitation").Include(
                "~/Areas/Master/Script/Controller/ContactInvitationCtrl.js",
                "~/Areas/Master/Script/Service/ContactInvitationService.js"
                ));
            #endregion

            #region For SMS
            bundles.Add(new ScriptBundle("~/bundles/SMS").Include(
                "~/Areas/Master/Script/Controller/SMSCtrl.js",
                "~/Areas/Master/Script/Service/SMSService.js",
                "~/Areas/Master/Script/Service/CountryService.js"
                ));
            #endregion

            #region For AgencyType
            bundles.Add(new ScriptBundle("~/bundles/PackingType").Include(
                "~/Areas/Master/Script/Controller/PackingTypeCtrl.js",
                "~/Areas/Master/Script/Service/PackingTypeService.js"
                ));
            #endregion

            #region For AgencyType
            bundles.Add(new ScriptBundle("~/bundles/ProductDocument").Include(
                "~/Areas/Master/Script/Controller/ProductDocumentCtrl.js",
                "~/Areas/Master/Script/Service/ProductDocumentService.js"
                ));
            #endregion

            #region For WorkReminder
            bundles.Add(new ScriptBundle("~/bundles/WorkReminder").Include(
                "~/Areas/Master/Script/Controller/WorkReminderController.js",
                "~/Areas/Master/Script/Service/WorkReminderService.js",
                "~/Areas/Master/Script/Service/CountryService.js"
                ));
            #endregion

            #region For AskcustomerDetails
            bundles.Add(new ScriptBundle("~/bundles/AskcustomerDetails").Include(
                "~/Areas/Master/Script/Controller/AskcustomerDetailsCtrl.js",
                "~/Areas/Master/Script/Service/AskcustomerDetailsService.js",
                 "~/Areas/Master/Script/Service/CountryService.js"
                ));
            #endregion

            #region For AcHolder
            bundles.Add(new ScriptBundle("~/bundles/AcHolder").Include(
                "~/Areas/Master/Script/Controller/AcHolderCtrl.js",
                "~/Areas/Master/Script/Service/AcHolderService.js"
                ));
            #endregion

            #region For SalaryHead
            bundles.Add(new ScriptBundle("~/bundles/SalaryHead").Include(
                "~/Areas/Master/Script/Controller/SalaryHeadCtrl.js",
                "~/Areas/Master/Script/Service/SalaryHeadService.js"
                ));
            #endregion

            #region For IntervieweeCandidate
            bundles.Add(new ScriptBundle("~/bundles/IntervieweeCandidate").Include(
                "~/Areas/Employee/Scripts/Controllers/IntervieweeCandidateCtrl.js",
                "~/Areas/Employee/Scripts/Services/IntervieweeCandidateService.js",
                "~/Areas/Master/Script/Service/CountryService.js",
                 "~/Areas/Master/Script/Service/SourceService.js",
                "~/Scripts/Directive/TotalInWords.js"
                ));
            #endregion

            #region For mailbox
            bundles.Add(new ScriptBundle("~/bundles/mailbox").Include(
                "~/Areas/Master/Script/Service/EmailSpeechService.js",
                "~/Areas/Master/Script/Controller/mailboxCtrl.js",
                "~/Areas/Master/Script/Service/mailboxService.js",
                "~/Areas/Master/Script/Service/EmailSignatureService.js",
                "~/Areas/Master/Script/Service/BuyerService.js"
                ));
            #endregion

            #region For WorkProfile
            bundles.Add(new ScriptBundle("~/bundles/WorkProfile").Include(
                "~/Areas/Employee/Scripts/Controllers/WorkProfileCtrl.js",
                "~/Areas/Employee/Scripts/Services/WorkProfileService.js"
                ));
            #endregion

            #region For FinancialYear
            bundles.Add(new ScriptBundle("~/bundles/FinancialYear").Include(
                "~/Areas/Master/Script/Controller/FinancialYearCtrl.js",
                "~/Areas/Master/Script/Service/FinancialYearService.js"
                ));
            #endregion

            #region For PaymentMode
            bundles.Add(new ScriptBundle("~/bundles/PaymentMode").Include(
                "~/Areas/Master/Script/Controller/PaymentModeCtrl.js",
                "~/Areas/Master/Script/Service/PaymentModeService.js"
                ));
            #endregion

            #region For SalesDocumentName
            bundles.Add(new ScriptBundle("~/bundles/SalesDocumentName").Include(
                "~/Areas/Master/Script/Controller/SalesDocumentCtrl.js",
                "~/Areas/Master/Script/Service/SalesDocumentService.js"
                ));
            #endregion

            #region For Bank
            bundles.Add(new ScriptBundle("~/bundles/Bank").Include(
                "~/Areas/Master/Script/Controller/BankController.js",
                "~/Areas/Master/Script/Service/BankService.js",
                "~/Areas/Master/Script/Service/CountryService.js"
                ));
            #endregion

            #region For Buyer
            bundles.Add(new ScriptBundle("~/bundles/Buyer").Include(
                "~/Areas/Master/Script/Service/BuyerService.js",
                "~/Areas/Master/Script/Controller/BuyerCtrl.js",
                "~/Areas/Master/Script/Service/CountryService.js",
                "~/Areas/Master/Script/Service/CityService.js",
                "~/Areas/Master/Script/Service/SourceService.js"
                ));
            #endregion

            #region For City
            bundles.Add(new ScriptBundle("~/bundles/City").Include(
                "~/Areas/Master/Script/Controller/CityController.js",
                "~/Areas/Master/Script/Service/CityService.js"
                ));
            #endregion

            #region For Company
            bundles.Add(new ScriptBundle("~/bundles/Company").Include(
                "~/Areas/Master/Script/Controller/CompanyCtrl.js",
                "~/Areas/Master/Script/Service/CompanyService.js",
                 "~/Areas/Master/Script/Service/CountryService.js"
                ));
            #endregion

            #region For Country
            bundles.Add(new ScriptBundle("~/bundles/Country").Include(
                "~/Areas/Master/Script/Controller/CountryController.js",
                "~/Areas/Master/Script/Service/CountryService.js"
                ));
            #endregion

            #region For CountryOrigin
            bundles.Add(new ScriptBundle("~/bundles/CountryOrigin").Include(
                "~/Areas/Master/Script/Controller/CountryOriginController.js",
                "~/Areas/Master/Script/Service/CountryOriginService.js",
                "~/Areas/Master/Script/Service/CityService.js"

                ));
            #endregion

            #region For Currency
            bundles.Add(new ScriptBundle("~/bundles/Currency").Include(
                "~/Areas/Master/Script/Controller/CurrencyController.js",
                "~/Areas/Master/Script/Service/CurrencyService.js"
                ));
            #endregion

            #region For DeliveryTerms
            bundles.Add(new ScriptBundle("~/bundles/DeliveryTerms").Include(
                "~/Areas/Master/Script/Controller/DeliveryTermsCtrl.js",
                "~/Areas/Master/Script/Service/DeliveryTermsService.js"
                ));
            #endregion

            #region For LicenseMaster
            bundles.Add(new ScriptBundle("~/bundles/License").Include(
                "~/Areas/Master/Script/Controller/LicenseCtrl.js",
                "~/Areas/Master/Script/Service/LicenseService.js"
                ));
            #endregion

            #region For Department
            bundles.Add(new ScriptBundle("~/bundles/Department").Include(
                "~/Areas/Master/Script/Controller/DepartmentController.js",
                "~/Areas/Master/Script/Service/DepartmentService.js"
                ));
            #endregion

            #region For Designation
            bundles.Add(new ScriptBundle("~/bundles/Designation").Include(
                "~/Areas/Master/Script/Controller/DesignationCtrl.js",
                "~/Areas/Master/Script/Service/DesignationService.js"
                ));
            #endregion

            #region For Dashboard
            bundles.Add(new ScriptBundle("~/bundles/Dashboard").Include(
                "~/Areas/Employee/Scripts/Services/WorkProfileService.js",
                "~/Areas/Master/Script/Controller/DashboardCtrl.js",
                "~/Areas/Master/Script/Service/DashboardService.js",
                "~/Areas/Master/Script/Service/TaskStatusService.js"
                ));
            #endregion

            #region For Document
            bundles.Add(new ScriptBundle("~/bundles/Document").Include(
                "~/Areas/Master/Script/Controller/DocumentCtrl.js",
                "~/Areas/Master/Script/Service/DocumentService.js"
                ));
            #endregion

            #region For DocumentUpload
            bundles.Add(new ScriptBundle("~/bundles/DocumentUpload").Include(
                "~/Areas/Master/Script/Controller/DocumentUploadController.js",
                "~/Areas/Master/Script/Service/DocumentUploadService.js"
                ));
            #endregion

            #region For EmailSignature
            bundles.Add(new ScriptBundle("~/bundles/EmailSignature").Include(
                "~/Areas/Master/Script/Controller/EmailSignatureCtrl.js",
                "~/Areas/Master/Script/Service/EmailSignatureService.js"
                ));
            #endregion

            #region For EmailSpeech
            bundles.Add(new ScriptBundle("~/bundles/EmailSpeech").Include(
                "~/Areas/Master/Script/Controller/EmailSpeechCtrl.js",
                "~/Areas/Master/Script/Service/EmailSpeechService.js"
                ));
            #endregion

            #region For Event
            bundles.Add(new ScriptBundle("~/bundles/Event").Include(
                "~/Areas/Master/Script/Controller/EventController.js",
                "~/Areas/Master/Script/Service/EventService.js"
                ));
            #endregion

            #region For EventType
            bundles.Add(new ScriptBundle("~/bundles/EventType").Include(
                "~/Areas/Master/Script/Controller/EventTypeController.js",
                "~/Areas/Master/Script/Service/EventTypeService.js"
                ));
            #endregion

            #region For Exhibition
            bundles.Add(new ScriptBundle("~/bundles/Exhibition").Include(
                "~/Areas/Master/Script/Controller/ExhibitionController.js",
                "~/Areas/Master/Script/Service/ExhibitionService.js",
                "~/Areas/Master/Script/Service/CountryService.js",
                "~/Areas/Master/Script/Service/CityService.js"

                ));
            #endregion

            #region For InvoiceType
            bundles.Add(new ScriptBundle("~/bundles/InvoiceType").Include(
                "~/Areas/Master/Script/Controller/InvoiceTypeCtrl.js",
                "~/Areas/Master/Script/Service/InvoiceTypeService.js"
                ));
            #endregion

            #region For ITR
            bundles.Add(new ScriptBundle("~/bundles/ITR").Include(
                "~/Areas/Master/Script/Controller/ITRCtrl.js",
                "~/Areas/Master/Script/Service/ITRService.js"
                ));
            #endregion

            #region For Qualification
            bundles.Add(new ScriptBundle("~/bundles/Qualification").Include(
                "~/Areas/Master/Script/Controller/QualificationCtrl.js",
                "~/Areas/Master/Script/Service/QualificationService.js"
                ));
            #endregion

            #region For Leave
            bundles.Add(new ScriptBundle("~/bundles/Leave").Include(
                "~/Areas/Master/Script/Controller/LeaveCtrl.js",
                "~/Areas/Master/Script/Service/LeaveService.js"
                ));
            #endregion

            #region For ModeofShipment
            bundles.Add(new ScriptBundle("~/bundles/ModeofShipment").Include(
                "~/Areas/Master/Script/Controller/ModeShipmentController.js",
                "~/Areas/Master/Script/Service/ModeShipmentService.js"
                ));
            #endregion

            #region For PaymentTerms
            bundles.Add(new ScriptBundle("~/bundles/PaymentTerms").Include(
                "~/Areas/Master/Script/Controller/PaymentTermsCtrl.js",
                "~/Areas/Master/Script/Service/PaymentTermsService.js"
                ));
            #endregion

            #region For PerformaInvoice
            bundles.Add(new ScriptBundle("~/bundles/PerformaInvoice").Include(
                "~/Areas/Master/Script/Controller/PerformaInvoiceCtrl.js",
                "~/Areas/Master/Script/Service/PerformaInvoiceService.js",
                "~/Areas/Product/Script/Service/UploadProductDataService.js",
                "~/Areas/Product/Script/Service/ProductService.js",
                "~/Areas/Transaction/Script/Service/PurchaseOrderService.js"
                ));
            #endregion

            #region For Port
            bundles.Add(new ScriptBundle("~/bundles/Port").Include(
                "~/Areas/Master/Script/Controller/PortController.js",
                "~/Areas/Master/Script/Service/PortService.js"
                ));
            #endregion

            #region For Religion
            bundles.Add(new ScriptBundle("~/bundles/Religion").Include(
                "~/Areas/Master/Script/Controller/ReligionController.js",
                "~/Areas/Master/Script/Service/ReligionService.js"
                ));
            #endregion

            #region For Role
            bundles.Add(new ScriptBundle("~/bundles/Role").Include(
                "~/Areas/Master/Script/Controller/RoleController.js",
                "~/Areas/Master/Script/Service/RoleService.js"
                ));
            #endregion

            #region For SMSSpeech
            bundles.Add(new ScriptBundle("~/bundles/SMSSpeech").Include(
                "~/Areas/Master/Script/Controller/SMSSpeechController.js",
                "~/Areas/Master/Script/Service/SMSSpeechService.js"
                ));
            #endregion

            #region For Source
            bundles.Add(new ScriptBundle("~/bundles/Source").Include(
                "~/Areas/Master/Script/Controller/SourceController.js",
                "~/Areas/Master/Script/Service/SourceService.js"
                ));
            #endregion

            #region For Specification
            bundles.Add(new ScriptBundle("~/bundles/Specification").Include(
                "~/Areas/Master/Script/Controller/SpecificationCtrl.js",
                "~/Areas/Master/Script/Service/SpecificationService.js"
                ));
            #endregion

            #region For State
            bundles.Add(new ScriptBundle("~/bundles/State").Include(
                "~/Areas/Master/Script/Controller/StateController.js",
                "~/Areas/Master/Script/Service/StateService.js",
                "~/Areas/Master/Script/Service/CityService.js"

                ));
            #endregion

            #region For Area
            bundles.Add(new ScriptBundle("~/bundles/Area").Include(
               "~/Areas/Master/Script/Controller/AreaController.js",
               "~/Areas/Master/Script/Service/AreaService.js"
                ));
            #endregion

            #region For Expense
            bundles.Add(new ScriptBundle("~/bundles/Expense").Include(
                "~/Areas/Master/Script/Controller/ExpenseController.js",
                "~/Areas/Master/Script/Service/ExpenseService.js"
                ));
            #endregion

            #region For ShippingMarkMaster
            bundles.Add(new ScriptBundle("~/bundles/ShippingMark").Include(
                "~/Areas/Master/Script/Controller/ShippingMarkController.js",
                "~/Areas/Master/Script/Service/ShippingMarkService.js"
                ));
            #endregion

            #region For TaxMaster
            bundles.Add(new ScriptBundle("~/bundles/Tax").Include(
                "~/Areas/Master/Script/Controller/TaxController.js",
                "~/Areas/Master/Script/Service/TaxService.js"
                ));
            #endregion

            #region For Supplier
            bundles.Add(new ScriptBundle("~/bundles/Supplier").Include(
                "~/Areas/Master/Script/Controller/SupplierCtrl.js",
                "~/Areas/Master/Script/Service/SupplierService.js",
                "~/Areas/Master/Script/Service/CountryService.js",
                "~/Areas/Master/Script/Service/CityService.js"
                ));
            #endregion

            #region For ReceiptVoucher
            bundles.Add(new ScriptBundle("~/bundles/ReceiptVoucher").Include(
                "~/Areas/Master/Script/Controller/ReceiptVoucherController.js",
                "~/Areas/Master/Script/Service/ReceiptVoucherService.js"
                ));
            #endregion

            #region For TermsAndCondition
            bundles.Add(new ScriptBundle("~/bundles/TermsAndCondition").Include(
                "~/Areas/Master/Script/Controller/TermsAndConditionController.js",
                "~/Areas/Master/Script/Service/TermsAndConditionService.js"
                ));
            #endregion

            #region For CkEditor
            bundles.Add(new ScriptBundle("~/bundles/CkEditor").Include(
              "~/Scripts/ckeditor/ckeditor.js",
              "~/Scripts/ckeditor/ckeditor_basic.js",
              "~/Content/lib/ng-ckeditor.min.js",
               "~/Scripts/Directive/CkEditor.js"
              ));
            #region For TypeofShipment
            bundles.Add(new ScriptBundle("~/bundles/TypeofShipment").Include(
                "~/Areas/Master/Script/Controller/TypeOfShipmentCtrl.js",
                "~/Areas/Master/Script/Service/TypeOfShipmentService.js"
                ));
            #endregion

            #region For Unit
            bundles.Add(new ScriptBundle("~/bundles/Unit").Include(
                "~/Areas/Master/Script/Controller/UnitController.js",
                "~/Areas/Master/Script/Service/UnitService.js"
                ));
            #endregion

            #region For BloodGroup
            bundles.Add(new ScriptBundle("~/bundles/BloodGroup").Include(
                "~/Areas/Master/Script/Controller/BloodGroupController.js",
                "~/Areas/Master/Script/Service/BloodGroupService.js"
                ));
            #endregion

            #region For TaskPriority
            bundles.Add(new ScriptBundle("~/bundles/TaskPriority").Include(
                "~/Areas/Master/Script/Controller/TaskPriorityController.js",
                "~/Areas/Master/Script/Service/TaskPriorityService.js"
                ));
            #endregion

            #region For TaskStatus
            bundles.Add(new ScriptBundle("~/bundles/TaskStatus").Include(
                "~/Areas/Master/Script/Controller/TaskStatusController.js",
                "~/Areas/Master/Script/Service/TaskStatusService.js"
                ));
            #endregion

            #region For ExpenseType
            bundles.Add(new ScriptBundle("~/bundles/ExpenseType").Include(
                "~/Areas/Master/Script/Controller/ExpenseTypeController.js",
                "~/Areas/Master/Script/Service/ExpenseTypeService.js"
                ));
            #endregion

            #region For User
            bundles.Add(new ScriptBundle("~/bundles/User").Include(
              "~/Areas/Master/Script/Controller/UserController.js",
              "~/Areas/Master/Script/Service/UserService.js",
               "~/Areas/Master/Script/Service/CountryService.js",
               "~/Areas/Employee/Scripts/Services/RelationService.js",
               "~/Areas/Master/Script/Service/CityService.js",
               "~/Areas/Master/Script/Service/SourceService.js"
              ));
            #endregion

            #region For Vendor
            bundles.Add(new ScriptBundle("~/bundles/Vendor").Include(
                "~/Areas/Master/Script/Controller/VendorCtrl.js",
                "~/Areas/Master/Script/Service/VendorService.js",
                "~/Areas/Master/Script/Service/CountryService.js",
                "~/Areas/Master/Script/Service/CityService.js"

                ));
            #endregion

            #region For TaskType
            bundles.Add(new ScriptBundle("~/bundles/TaskType").Include(
                "~/Areas/Master/Script/Controller/TaskTypeController.js",
                "~/Areas/Master/Script/Service/TaskTypeService.js"
                ));
            #endregion
            #endregion

            #region For Product
            #region For ProductCategory
            bundles.Add(new ScriptBundle("~/bundles/ProductCategory").Include(
                "~/Areas/Product/Script/Controller/CategoryCtrl.js",
                "~/Areas/Product/Script/Service/CategoryService.js"
                ));
            #endregion

            #region For MainProduct
            bundles.Add(new ScriptBundle("~/bundles/MainProduct").Include(
                "~/Areas/Product/Script/Controller/MainProductCtrl.js",
                "~/Areas/Product/Script/Service/MainProductService.js"
                ));
            #endregion

            #region For Product
            bundles.Add(new ScriptBundle("~/bundles/Product").Include(
                "~/Areas/Product/Script/Controller/ProductCtrl.js",
                "~/Areas/Product/Script/Service/ProductService.js"
                ));
            #endregion

            #region For SubProductCategory
            bundles.Add(new ScriptBundle("~/bundles/SubProductCategory").Include(
                "~/Areas/Product/Script/Controller/SubCategoryCtrl.js",
                "~/Areas/Product/Script/Service/SubCategoryService.js"
                ));
            #endregion

            #region For ProductForm
            bundles.Add(new ScriptBundle("~/bundles/ProductForm").Include(
                "~/Areas/Product/Script/Controller/ProductFormCtrl.js",
                "~/Areas/Product/Script/Service/ProductService.js"
                ));
            #endregion

            #region For RNDProduct
            bundles.Add(new ScriptBundle("~/bundles/RNDProduct").Include(
                "~/Areas/Product/Script/Controller/RndProductCtrl.js",
                "~/Areas/Product/Script/Controller/RndSupplierCtrl.js",
                "~/Areas/Product/Script/Service/RndProductService.js",
                "~/Areas/Product/Script/Service/RndSupplierService.js",
                "~/Areas/Master/Script/Service/CountryService.js",
                 "~/Areas/Master/Script/Service/SourceService.js"
                ));
            #endregion
            #endregion

            #region For Transaction

            #region For Inquiry
            bundles.Add(new ScriptBundle("~/bundles/Inquiry").Include(
                "~/Areas/Master/Script/Service/CountryService.js",
                 "~/Areas/Master/Script/Service/CityService.js",
                "~/Areas/Transaction/Script/Controllers/InquiryCtrl.js",
                "~/Areas/Transaction/Script/Service/InquiryService.js",
                "~/Areas/Transaction/Script/Service/TaskService.js",
                "~/Areas/Transaction/Script/Service/SalesOrderService.js"
                ));
            #endregion

            #region For PurchaseOrder
            bundles.Add(new ScriptBundle("~/bundles/PurchaseOrder").Include(
                "~/Areas/Transaction/Script/Controllers/PurchaseOrderCtrl.js",
                "~/Areas/Transaction/Script/Service/PurchaseOrderService.js",
                "~/Areas/Product/Script/Service/ProductService.js",
                "~/Areas/Master/Script/Service/CountryService.js",
                "~/Areas/Product/Script/Service/UploadProductDataService.js",
                "~/Scripts/Directive/TotalInWords.js"
                ));
            #endregion

            #region For Quotation
            bundles.Add(new ScriptBundle("~/bundles/Quotation").Include(
                "~/Areas/Master/Script/Service/CompanyService.js",
                "~/Areas/Product/Script/Service/ProductService.js",
                "~/Areas/Transaction/Script/Service/PurchaseOrderService.js",
                "~/Areas/Transaction/Script/Service/InquiryService.js",
                "~/Areas/Product/Script/Service/UploadProductDataService.js",
                "~/Areas/Transaction/Script/Service/QuotationService.js",
                "~/Areas/Transaction/Script/Controllers/QuotationCtrl.js",
                 "~/Scripts/Directive/TotalInWords.js"
                ));
            #endregion

            #region For SalesOrder
            bundles.Add(new ScriptBundle("~/bundles/SalesOrder").Include(
                "~/Areas/Transaction/Script/Service/SalesOrderService.js",
                "~/Areas/Transaction/Script/Controllers/SalesOrderCtrl.js"
                ));
            #endregion

            #region For Task
            bundles.Add(new ScriptBundle("~/bundles/Task").Include(
                "~/Areas/Transaction/Script/Service/TaskService.js",
                "~/Areas/Transaction/Script/Controllers/TaskController.js"
                ));
            #endregion

            #endregion

            #region For InwardCourier
            bundles.Add(new ScriptBundle("~/bundles/InwardCourier").Include(
                        "~/Areas/Master/Script/Controller/InwardCourierCtrl.js",
                        "~/Areas/Master/Script/Service/InwardCourierService.js"
                        ));
            #endregion

            #region For OutwardCourier
            bundles.Add(new ScriptBundle("~/bundles/OutwardCourier").Include(
                        "~/Areas/Master/Script/Controller/OutwardCourierCtrl.js",
                        "~/Areas/Master/Script/Service/OutwardCourierService.js",
                        "~/Areas/Master/Script/Service/CityService.js"

                        ));
            #endregion

            #region For SalePurchase
            bundles.Add(new ScriptBundle("~/bundles/SalePurchase").Include(
                "~/Areas/Transaction/Script/Controllers/SalesPurchaseEntryController.js",
                "~/Areas/Transaction/Script/Service/SalesPurchaseEntryService.js"
                ));
            #endregion

            #region For Calendar
            bundles.Add(new ScriptBundle("~/bundles/Calendar").Include(
                        "~/Areas/Master/Script/Controller/CalendarCtrl.js",
                        "~/Areas/Transaction/Script/Service/TaskService.js",
                        "~/Areas/Master/Script/Service/TaskStatusService.js",
                "~/Areas/Transaction/Script/Service/InquiryService.js"
                        ));
            #endregion

            #region For Shape
            bundles.Add(new ScriptBundle("~/bundles/Shape").Include(
                        "~/Areas/Master/Script/Controller/ShapeCtrl.js",
                        "~/Areas/Master/Script/Service/ShapeService.js"
                        ));
            #endregion

            #region For PlugShape
            bundles.Add(new ScriptBundle("~/bundles/PlugShape").Include(
                        "~/Areas/Master/Script/Controller/PlugShapeCtrl.js",
                        "~/Areas/Master/Script/Service/PlugShapeService.js"
                        ));
            #endregion

            #region For AccountType
            bundles.Add(new ScriptBundle("~/bundles/AccountType").Include(
                        "~/Areas/Employee/Scripts/Controllers/AccountTypeCtrl.js",
                        "~/Areas/Employee/Scripts/Services/AccountTypeService.js"
                        ));
            #endregion

            #region For HolidayName
            bundles.Add(new ScriptBundle("~/bundles/HolidayName").Include(
                        "~/Areas/Employee/Scripts/Controllers/HolidayNameCtrl.js",
                        "~/Areas/Employee/Scripts/Services/HolidayNameService.js"
                        ));
            #endregion

            #region For Relation
            bundles.Add(new ScriptBundle("~/bundles/Relation").Include(
                        "~/Areas/Employee/Scripts/Controllers/RelationCtrl.js",
                        "~/Areas/Employee/Scripts/Services/RelationService.js"
                        ));
            #endregion

            #region For Percentage
            bundles.Add(new ScriptBundle("~/bundles/Percentage").Include(
                       "~/Areas/Master/Script/Controller/PercentageController.js",
                        "~/Areas/Master/Script/Service/PercentageService.js"
                        ));
            #endregion

            #region For AdvertisementSource
            bundles.Add(new ScriptBundle("~/bundles/AdvertisementSource").Include(
                       "~/Areas/Master/Script/Controller/AdvertisementSourceController.js",
                        "~/Areas/Master/Script/Service/AdvertisementSourceService.js"
                        ));
            #endregion

            #region For UploadProductData
            bundles.Add(new ScriptBundle("~/bundles/UploadProductData").Include(
                  "~/Areas/Product/Script/Service/UploadProductDataService.js",
                  "~/Areas/Product/Script/Controller/UploadProductDataCtrl.js",
                  "~/Areas/Product/Script/Service/ProductService.js"
                  ));
            #endregion

            #region For ChatName
            bundles.Add(new ScriptBundle("~/bundles/ChatName").Include(
                "~/Areas/Master/Script/Controller/ChatNameController.js",
                "~/Areas/Master/Script/Service/ChatNameService.js"
                ));
            #endregion

            #region For BankName
            bundles.Add(new ScriptBundle("~/bundles/BankName").Include(
                "~/Areas/Master/Script/Controller/BankNameController.js",
                "~/Areas/Master/Script/Service/BankNameService.js"
               ));
            #endregion

            #region For TOType
            bundles.Add(new ScriptBundle("~/bundles/TOType").Include(
                "~/Areas/Master/Script/Controller/TOTypeController.js",
                "~/Areas/Master/Script/Service/TOTypeService.js"

                ));
            #endregion

            #region For TOMaster
            bundles.Add(new ScriptBundle("~/bundles/TOMaster").Include(
                "~/Areas/Master/Script/Controller/TOController.js",
                "~/Areas/Master/Script/Service/TOService.js",
                "~/Areas/Product/Script/Service/ProductService.js",
                "~/Areas/Product/Script/Service/UploadProductDataService.js"
                ));
            #endregion

            #region For LegerHead

            bundles.Add(new ScriptBundle("~/bundles/LegerHead").Include(
                "~/Areas/Master/Script/Controller/LegerHeadControllert.js",
                "~/Areas/Master/Script/Service/LegerHeadService.js"

                ));
            #endregion

            #region For Leger

            bundles.Add(new ScriptBundle("~/bundles/Leger").Include(
                "~/Areas/Master/Script/Controller/LegerController.js",
                "~/Areas/Master/Script/Service/LegerService.js"
             ));
            #endregion

            #region For Login
            bundles.Add(new ScriptBundle("~/bundles/Login").Include(
                "~/Scripts/Controller/LoginController.js",
                "~/Scripts/Services/LoginService.js"
                ));
            #endregion

            #region For ReportPage
            bundles.Add(new ScriptBundle("~/bundles/ReportPage").Include(
                 "~/Content/lib/bootstrap/bootstrap-datepicker.js",
                "~/Scripts/ReportPage.js"
                ));
            #endregion

            #region For AddressType
            bundles.Add(new ScriptBundle("~/bundles/AddressType").Include(
                "~/Areas/Master/Script/Controller/AddressTypeCtrl.js",
                "~/Areas/Master/Script/Service/AddressTypeService.js"
                ));
            #endregion

            #region For Leger

            bundles.Add(new ScriptBundle("~/bundles/TaskGroup").Include(
                "~/Areas/Master/Script/Controller/TaskGroupController.js",
                "~/Areas/Master/Script/Service/TaskGroupService.js"
             ));
            #endregion

            #region For Letter

            bundles.Add(new ScriptBundle("~/bundles/Letter").Include(
                "~/Areas/Master/Script/Controller/CompanyLetterController.js",
                "~/Areas/Master/Script/Service/CompanyLetterService.js",
                "~/Areas/Master/Script/Service/CountryService.js"
             ));
            #endregion

            #region For OfferLetter

            bundles.Add(new ScriptBundle("~/bundles/OfferLetter").Include(
                "~/Areas/Employee/Scripts/Controllers/OfferletterCtrl.js",
                "~/Areas/Employee/Scripts/Services/OfferletterService.js", 
                "~/Areas/Master/Script/Service/CountryService.js"
             ));
            #endregion

            #region For PromotionLetter

            bundles.Add(new ScriptBundle("~/bundles/PromotionLetter").Include(
                "~/Areas/Employee/Scripts/Controllers/PromotionletterCtrl.js",
                "~/Areas/Employee/Scripts/Services/PromotionletterService.js",
                "~/Areas/Master/Script/Service/CountryService.js"

             ));
            #endregion

            #region For ShippingOrder
            bundles.Add(new ScriptBundle("~/bundles/ShippingOrder").Include(
                "~/Areas/Operation/Script/Controller/ShippingOrderCtrl.js",
                "~/Areas/Operation/Script/Service/ShippingOrderService.js",
                "~/Areas/Master/Script/Service/CountryService.js"
                ));
            #endregion

            #region For BillofLoading
            bundles.Add(new ScriptBundle("~/bundles/BillofLoading").Include(
                "~/Areas/Operation/Script/Controller/BillofLoadingCtrl.js",
                "~/Areas/Operation/Script/Service/BillofLoadingService.js",
                "~/Areas/Master/Script/Service/CountryService.js"
                ));
            #endregion

            #region For ContactDocumentName
            bundles.Add(new ScriptBundle("~/bundles/ContactDocumentName").Include(
                "~/Areas/Master/Script/Controller/ContactDocumentNameController.js",
                "~/Areas/Master/Script/Service/ContactDocumentNameService.js"

                ));
            #endregion

            #region For TransactionType
            bundles.Add(new ScriptBundle("~/bundles/TransactionType").Include(
                "~/Areas/Master/Script/Controller/TransactionTypeCtrl.js",
                "~/Areas/Master/Script/Service/TransactionTypeService.js"
                ));
            #endregion

            if (HttpContext.Current.IsDebuggingEnabled)
                BundleTable.EnableOptimizations = false;
            else
                BundleTable.EnableOptimizations = true;

        }
    }
}
