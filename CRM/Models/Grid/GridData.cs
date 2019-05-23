using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.Grid
{
    public class GridData
    {
        public string TableName { get; set; }
        public string ColumnsName { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public int PageNumber { get; set; }
        public int RecordPerPage { get; set; }
        public string WhereClause { get; set; }
        public string MultiOrder { get; set; }
        public string JsonData { get; set; }
        public string ExportedColumns { get; set; }
        public string ExportedFileName { get; set; }

        public GridData(GridReqData obj, Boolean IsExport = false)
        {


            if (obj.Mode == "CategoryMaster")
            {
                this.ColumnsName = "CategoryId,CategoryName,IsActive,CreatedBy";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "CategoryId";
                this.SortOrder = "desc";
                this.TableName = "CategoryMaster";
                this.WhereClause = "IsActive=1";
                this.ExportedFileName = "CategoryList";
                this.ExportedColumns = "CategoryId[Hidden],CategoryName,IsActive[Hidden],CreatedBy[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                {
                    this.JsonData = oGrid.GetJson<Category>();
                }
                else
                {
                    oGrid.Export(obj);
                }
            }
            else if (obj.Mode == "WorkReminderMaster")
            {
                this.ColumnsName = "WM.WorkRemindId,WM.DepartmentId,DM.DepartmentName as Department,WM.Title,WM.Description,convert(date, WM.RemindDate)RemindDate,WM.RemindTime,WM.RemindMode,WM.IsActive,WM.CreatedBy";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "WorkRemindId";
                this.SortOrder = "desc";
                this.TableName = "WorkReminderMaster as WM WITH(NOLOCK) Inner join departmentMaster as DM WITH(NOLOCK)  on DM.DepartmentId=WM.DepartmentId";
                this.WhereClause = "IsActive=1";
                this.ExportedFileName = "WorkReminderList";
                this.ExportedColumns = "WM.WorkRemindId[Hidden],DM.DepartmentName as Department,WM.Title,WM.Description,convert(date, WM.RemindDate)RemindDate,WM.RemindTime,WM.RemindMode,WM.IsActive[Hidden],WM.CreatedBy[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<WorkReminderMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "ApplicableChargeMaster")
            {
                this.ColumnsName = "ApplicableChargeId,ApplicableChargeName,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "ApplicableChargeId";
                this.SortOrder = "desc";
                this.TableName = "ApplicableChargeMaster";
                this.WhereClause = "IsActive=1";
                this.ExportedFileName = "ApplicableChargeList";
                this.ExportedColumns = "ApplicableChargeId[Hidden],ApplicableChargeName,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<ApplicableChargeMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "AskcustomerDetails")
            {
                this.ColumnsName = "UM.Name As UserName,AD.AskCustId,AD.SourceId,SM.SourceName,convert(date, AD.Date)Date,AD.Createddate,convert(time, AD.Createddate)Createdtime,AD.Requirement,AD.Name,AD.Mobileno,AD.Email,AD.IsActive,AD.CreatedBy";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "AskCustId";
                this.SortOrder = "desc";
                this.TableName = "AskcustomerDetails As AD WITH(NOLOCK) Inner join SourceMaster as SM WITH(NOLOCK)  on SM.SourceId=AD.SourceId inner join UserMaster as UM WITH(NOLOCK) on UM.userid=AD.CreatedBy";
                this.WhereClause = "IsActive=1";
                this.ExportedFileName = "AskcustomerDetailsList";
                this.ExportedColumns = "AD.AskCustId[Hidden],AD.SourceId[Hidden],SM.SourceName,convert(date, AD.Date)Date,AD.Requirement,AD.Name,AD.Mobileno,AD.Email,AD.IsActive[Hidden],AD.CreatedBy[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<AskcustomerDetails>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "BankMaster") //Change Condition block
            {
                this.ColumnsName = "BM.BankId,BM.BeneficiaryName,BM.BranchName,BM.AccountNo,BM.IFSCCode,BM.NickName,BM.SwiftCode,BM.CRNNo,BM.AccountTypeId,BM.RegisterEmail ,BM.RegisterMobile,BM.StatementPassword,BM.BankCustomerCareNo,BM.BankCustomerCareEmail,BM.Note,BM.BankNameId,BM.MICRCode ,BM.CreatedBy,BM.CreatedDate,AC.AcHolderName As AcNickName,BN.BankName,BM.IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "BankId";
                this.SortOrder = "desc";
                this.TableName = "BankMaster BM WITH(NOLOCK) Inner join AcHolderMaster AC WITH(NOLOCK)  ON BM.NickName = AC.AcHolderCode Inner join BankNameMaster BN WITH(NOLOCK)  ON BN.BankId = BM.BankNameId";
                this.WhereClause = "IsActive=1";
                this.ExportedFileName = "BankList";
                this.ExportedColumns = "BM.BankId[Hidden],BM.BeneficiaryName,BM.BranchName,BM.AccountNo,BM.IFSCCode,BM.NickName,BM.SwiftCode,BM.CRNNo,BM.AccountTypeId[Hidden],BM.RegisterEmail ,BM.RegisterMobile,BM.StatementPassword,BM.BankCustomerCareNo,BM.BankCustomerCareEmail,BM.Note,BM.BankNameId[Hidden],BM.MICRCode ,BM.CreatedBy[Hidden],BM.CreatedDate[Hidden],AC.AcHolderName As AcNickName,BN.BankName,BM.IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<Bank>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "SubCategoryMaster")
            {
                this.ColumnsName = "S.SubCategoryId,S.SubCategoryName,C.CategoryName,S.IsActive,S.CategoryId,S.CreatedBy";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "SubCategoryId";
                this.SortOrder = "desc";
                this.TableName = "CategoryMaster as C Join SubCategoryMaster as S on  C.CategoryId = S.CategoryId WHERE ISNULL(C.IsActive,1)=1";
                if (obj.FixClause == null)
                    this.WhereClause = "ISNULL(IsActive,1)=1 ";
                else
                    this.WhereClause = "ISNULL(IsActive,1)=1 AND " + obj.FixClause;

                this.ExportedFileName = "SubCategoryList";
                this.ExportedColumns = "S.SubCategoryId[Hidden],S.SubCategoryName,C.CategoryName,S.IsActive[Hidden],S.CreatedBy[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<SubCategory>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "CityMaster")
            {
                this.ColumnsName = "CI.CityId,C.CountryId,C.CountryName,S.StateName,S.StateId,CI.CityName,CI.IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "CityId";
                this.SortOrder = "desc";
                this.TableName = "CountryMaster as C join StateMaster as S on C.CountryId = S.CountryId join CityMaster as CI on S.StateId = CI.StateId ";
                if (obj.FixClause == null)
                    this.WhereClause = "ISNULL(IsActive,1)=1";
                else
                    this.WhereClause = "ISNULL(IsActive,1)=1 AND " + obj.FixClause;
                this.ExportedFileName = "CityList";
                this.ExportedColumns = "CI.CityId[Hidden],C.CountryId[Hidden],C.CountryName,S.StateName,CI.CityName,CI.IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<City>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "RNDProductMaster")
            {
                this.ColumnsName = @"P.RNDProductId,P.ProductName,P.Description,REPLACE (P.Keyword, '|', ',') AS Keyword,P.EmailSpeechId,
                                    Title AS EmailSpeech,P.SMSSpeechId,SMSTitle AS SMSSpeech,P.RMPhotos,P.MPPhotos,P.FMPhotos,P.ChatSpeech,P.Videoes,P.IsActive,S.SubCategoryId,S.SubCategoryName,
                                    C.CategoryId,C.CategoryName,P.CreatedBy";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "RNDProductId";
                this.SortOrder = "desc";
                this.TableName = @"RNDProductMaster as P LEFT JOIN SubCategoryMaster as S on S.SubCategoryId = P.SubCategoryId 
                                    LEFT JOIN EmailSpeechMaster as ES on ES.SpeechId = P.EmailSpeechId
                                    LEFT JOIN SMSSpeechMaster as SP on SP.SMSId = P.SMSSpeechId
                                    LEFT JOIN CategoryMaster as C on C.CategoryId = S.CategoryId 
                                    WHERE ISNULL(P.IsActive, 1)= 1 AND ISNULL(C.IsActive, 1)= 1 AND ISNULL(S.IsActive, 1)= 1";
                if (obj.FixClause == null)
                    this.WhereClause = "ISNULL(IsActive,1)=1";
                else
                    this.WhereClause = "ISNULL(IsActive,1)=1 AND " + obj.FixClause;
                this.ExportedFileName = "ProductList";
                this.ExportedColumns = @"P.RNDProductId[Hidden],P.ProductName[Hidden],REPLACE (P.Keyword, '|', ',') AS Keyword,P.Description,P.EmailSpeechId[Hidden],
                                        Title AS EmailSpeech,P.SMSSpeechId[Hidden],SMSTitle AS SMSSpeech,P.RMPhotos[Hidden],P.FMPhotos,P.MPPhotos[Hidden],P.ChatSpeech,P.Videoes[Hidden],P.IsActive[Hidden]
                                        ,S.SubCategoryId[Hidden],S.SubCategoryName[Hidden],
                                        C.CategoryId[Hidden],C.CategoryName[Hidden],P.CreatedBy[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<RNDProduct>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "ProductMaster")
            {
                this.ColumnsName = @" Distinct P.ProductId,P.ProductName,P.ProductCode,P.HSCode,P.Keywords,P.Description,P.Functionality,
                                    PP.GrossWeight,PP.NetWeight,PP.CBM,PP.Dimension,PP.DealerPrice,
                                    P.IsActive,S.SubCategoryId,S.SubCategoryName,C.CategoryId,C.CategoryName,P.CreatedBy";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "ProductId";
                this.SortOrder = "desc";
                this.TableName = "ProductMaster as P Left join ProductPackingDetail as PP On PP.ProductId = P.ProductId Join SubCategoryMaster as S on S.SubCategoryId = P.SubCategoryId join CategoryMaster as C on C.CategoryId = S.CategoryId WHERE ISNULL(C.IsActive, 1)= 1 AND ISNULL(S.IsActive, 1)= 1 ";
                if (obj.FixClause == null)
                    this.WhereClause = "ISNULL(IsActive,1)=1";
                else
                    this.WhereClause = "ISNULL(IsActive,1)=1 AND " + obj.FixClause;

                this.ExportedFileName = "ProductList";
                this.ExportedColumns = @" Distinct P.ProductId[Hidden],P.ProductName,P.ProductCode,P.HSCode,P.Keywords,P.Description,
                                    PP.GrossWeight,PP.NetWeight,PP.CBM,PP.Dimension,PP.DealerPrice,
                                    P.IsActive[Hidden],S.SubCategoryId[Hidden],S.SubCategoryName,C.CategoryId[Hidden],C.CategoryName,P.CreatedBy[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<Product>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "ProductSupllierDetail")
            {
                this.ColumnsName = "C.CatalogId,B.CompanyName,C.ProductId,P.ProductName,C.IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "ProductId";
                this.SortOrder = "desc";
                this.TableName = "ProductCatalogMaster as C  inner Join ProductMaster as P on  C.ProductId = P.ProductId inner Join BuyerMaster as B on  B.BuyerId = C.SupplierId ";
                if (obj.FixClause == null)
                    this.WhereClause = "ISNULL(IsActive,1)=1";
                else
                    this.WhereClause = "ISNULL(IsActive,1)=1 AND " + obj.FixClause;

                this.ExportedFileName = "ProductList";
                this.ExportedColumns = "C.CatalogId[Hidden],B.CompanyName[Hidden],C.ProductId[Hidden],P.ProductName,C.IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<ProductSupllier>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "ProductSupllierModelNoDetail")
            {
                this.ColumnsName = "C.CatalogId,C.SupplierModelNo,C.ProductId,P.ProductName,C.IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "ProductId";
                this.SortOrder = "desc";
                this.TableName = "ProductCatalogMaster as C  inner Join ProductMaster as P on  C.ProductId = P.ProductId inner Join BuyerMaster as B on  B.BuyerId = C.SupplierId ";
                if (obj.FixClause == null)
                    this.WhereClause = "ISNULL(IsActive,1)=1";
                else
                    this.WhereClause = "ISNULL(IsActive,1)=1 AND " + obj.FixClause;

                this.ExportedFileName = "ProductList";
                this.ExportedColumns = "C.CatalogId[Hidden],C.SupplierModelNo,C.ProductId[Hidden],P.ProductName,C.IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<ProductSupllier>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "ProductMasterFrom")
            {
                this.ColumnsName = @"P.ProductId,P.ProductName,P.HSCode,P.ProductCode,P.Price,P.ModelNo,P.Height,P.CBM,P.Width,P.Length,
                                        P.GrossWeight,P.NetWeight,P.Description,P.Keywords,P.IsActive,S.SubCategoryId,
                                        S.SubCategoryName,C.CategoryId,C.CategoryName,P.CreatedBy";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "ProductId";
                this.SortOrder = "desc";
                this.TableName = "ProductMaster as P Join SubCategoryMaster as S on S.SubCategoryId=P.SubCategoryId join CategoryMaster as C on C.CategoryId=S.CategoryId WHERE ISNULL(C.IsActive,1)=1 AND ISNULL(S.IsActive,1)=1";
                if (obj.FixClause == null)
                    this.WhereClause = "ISNULL(IsActive,1)=1";
                else
                    this.WhereClause = "ISNULL(IsActive,1)=1 AND " + obj.FixClause;
                this.ExportedFileName = "ProductList";
                this.ExportedColumns = @"P.ProductId[Hidden],P.ProductName,P.HSCode[Hidden],P.ProductCode[Hidden],P.Price[Hidden],P.ModelNo[Hidden],P.Height[Hidden],
                                            P.CBM[Hidden],P.Width[Hidden],P.Length[Hidden],P.GrossWeight[Hidden],P.NetWeight[Hidden],
                                            P.Description[Hidden],P.Keywords,P.IsActive[Hidden],S.SubCategoryId[Hidden],S.SubCategoryName,
                                            C.CategoryId[Hidden],C.CategoryName,P.CreatedBy[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<Product>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "StateMaster")
            {
                this.ColumnsName = "S.StateId,C.CountryId,C.CountryName,S.StateName,S.IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "StateId";
                this.SortOrder = "desc";
                this.TableName = "CountryMaster as C join StateMaster as S on C.CountryId = S.CountryId ";
                if (obj.FixClause == null)
                    this.WhereClause = "ISNULL(IsActive,1)=1";
                else
                    this.WhereClause = "ISNULL(IsActive,1)=1 AND " + obj.FixClause;
                this.ExportedFileName = "StateList";
                this.ExportedColumns = "S.StateId[Hidden],C.CountryId[Hidden],C.CountryName,S.StateName,S.IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<State>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "ExpenseMaster")
            {
                this.ColumnsName = @"E.ExTypeName,EX.ExId,EX.ExTypeId,EX.Description,EX.Amount,EX.ContactPerson,EX.CreatedDate,
                                        EX.IsActive,EX.CreatedBy";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "ExId";
                this.SortOrder = "desc";
                this.TableName = "ExpenseTypeMaster as E inner join ExpenseMaster as EX on E.ExTypeId = EX.ExTypeId ";
                if (obj.FixClause == null)
                    this.WhereClause = "ISNULL(IsActive,1)=1";
                else
                    this.WhereClause = "ISNULL(IsActive,1)=1 AND " + obj.FixClause;
                this.ExportedFileName = "ExpenseList";
                this.ExportedColumns = @"E.ExTypeName,EX.ExId[Hidden],EX.ExTypeId[Hidden],EX.Description,EX.Amount,EX.ContactPerson,
                                            CONVERT(VARCHAR(20),EX.CreatedDate,105)CreatedDate,EX.IsActive[Hidden],EX.CreatedBy[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<Expense>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "CountryMaster" || obj.Mode == "CountryCodeMaster")
            {
                this.ColumnsName = "CountryId,CountryCallCode,CountryName,CountryFlag,CountryAlphaCode,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "CountryId";
                this.SortOrder = "desc";
                this.TableName = "CountryMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "CountryList";
                this.ExportedColumns = "CountryId[Hidden],CountryCallCode,CountryName,CountryFlag[Hidden],CountryAlphaCode,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<Country>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "CountryOfOriginMaster")
            {
                this.ColumnsName = "C.OriginId,C.CountryOfOrigin,C.CountryId,S.CountryName,C.IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "OriginId";
                this.SortOrder = "desc";
                this.TableName = "CountryOfOriginMaster as C join countrymaster as S on C.CountryId = S.CountryId ";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "CountryOriginList";
                this.ExportedColumns = "C.OriginId[Hidden],C.CountryOfOrigin,S.CountryName,C.IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<CountryOrigin>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "DesignationMaster")
            {
                this.ColumnsName = "DesignationId,DesignationName,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "DesignationId";
                this.SortOrder = "desc";
                this.TableName = "DesignationMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "DesignationList";
                this.ExportedColumns = "DesignationId[Hidden],DesignationName,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<Designation>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "PortMaster")
            {
                this.ColumnsName = "C.PortId,C.PortName,C.CountryId,S.CountryName,C.IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "PortId";
                this.SortOrder = "desc";
                this.TableName = "PortMaster as C join countrymaster as S on C.CountryId = S.CountryId ";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "PortList";
                this.ExportedColumns = "C.PortId[Hidden],C.PortName,S.CountryName,C.IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<PortMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "SourceMaster")
            {
                this.ColumnsName = "SourceId,SourceName,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "SourceId";
                this.SortOrder = "desc";
                this.TableName = "SourceMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "Source List";
                this.ExportedColumns = "SourceId[Hidden],SourceName,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<SourceMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "TermsAndConditionMaster")
            {
                this.ColumnsName = "TermsId,Description,Title,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "TermsId";
                this.SortOrder = "desc";
                this.TableName = "TermsAndConditionMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "TermsAndCondition List";
                this.ExportedColumns = "TermsId[Hidden],Title,Description,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<TermsAndConditionMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "ShipmentMaster")
            {
                this.ColumnsName = "ShipmentId,ModeOfShipment,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "ShipmentId";
                this.SortOrder = "desc";
                this.TableName = "ShipmentMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "ShipmentMaster List";
                this.ExportedColumns = "ShipmentId[Hidden],ModeOfShipment,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<ShipmentMaster>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "BuyerMaster") // Change Condition block
            {
                this.ColumnsName = @" Distinct BM.BuyerId,BM.CompanyName,BM.WebAddress,BM.Remark,BM.Email,BM.Telephone,BM.ContactType,BM.Fax,
                                    BM.DocumentsData,BM.AgencyTypeId,AT.AgencyType,BM.IsActive,BM.CreatedBy,
                                    (SELECT STUFF((
                                    SELECT ', ' + CCT.CountryName 
                                    FROM BuyerAddressDetail AS AC 
                                    INNER JOIN CityMaster AS CT ON CT.cityid=AC.cityid
                                    INNER JOIN StateMaster AS ST ON CT.stateid=ST.stateid
                                    INNER JOIN CountryMaster AS CCT ON ST.countryid=CCT.countryid
                                    Where BC.BuyerId = AC.BuyerId And CT.IsActive = 1
                                    FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As Country,
                                    (SELECT STUFF((
                                    SELECT ', ' + ST.StateName 
                                    FROM BuyerAddressDetail AS AC 
                                    INNER JOIN CityMaster AS CT ON CT.cityid=AC.cityid
                                    INNER JOIN StateMaster AS ST ON CT.stateid=ST.stateid
                                    INNER JOIN CountryMaster AS CCT ON ST.countryid=CCT.countryid
                                    Where BC.BuyerId = AC.BuyerId And CT.IsActive = 1
                                    FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As StateName,
                                    (SELECT STUFF((
                                    SELECT ', ' + CT.CityName +' - '+ CCT.CountryAlphaCode
                                    FROM BuyerAddressDetail AS AC 
                                    INNER JOIN CityMaster AS CT ON CT.cityid=AC.cityid
                                    INNER JOIN StateMaster AS ST ON CT.stateid=ST.stateid
                                    INNER JOIN CountryMaster AS CCT ON ST.countryid=CCT.countryid
                                    Where BC.BuyerId = AC.BuyerId And CT.IsActive = 1
                                    FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As CityName,
		                            (SELECT STUFF((
                                    SELECT ', ' + Ac.Address
                                    FROM BuyerAddressDetail AS AC 
                                    Where BC.BuyerId = AC.BuyerId 
                                    FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As Addres,
                                   REPLACE (  (SELECT STUFF((
                                    SELECT ', ' + ISNULL(AC.Telephone,'')+'|'+ ISNULL(AC.Email,'') +'|'+ ISNULL(AC.Fax,'') +'|'+ ISNULL(AC.WebAddress,'')+''
                                    FROM BuyerAddressDetail AS AC 
                                    Where BM.BuyerId = AC.BuyerId 
                                    FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')),'|||' , '' ) As ContectDetails,

                                    (SELECT STUFF((
                                    SELECT ', ' + ContactPerson + ' ' + Surname
                                    FROM BuyerContactDetail WITH(NOLOCK) Where BuyerId = BM.BuyerId And IsActive = 1
                                    FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As Person,
                                    (SELECT STUFF((
                                    SELECT ', ' + MobileNo
                                    FROM BuyerContactDetail WITH(NOLOCK) Where BuyerId = BM.BuyerId And IsActive = 1
                                    FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As MobileNo,
                                    (SELECT STUFF((
                                    SELECT ', ' + ChatDetails
                                    FROM BuyerContactDetail WITH(NOLOCK) Where BuyerId = BM.BuyerId And IsActive = 1
                                    FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As ChatDetails,
                                    (SELECT STUFF((
                                    SELECT ', ' + Email
                                    FROM BuyerContactDetail WITH(NOLOCK) Where BuyerId = BM.BuyerId And IsActive = 1
                                    FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As ContactEmail,
                                    (SELECT STUFF((
                                    SELECT ' ' + ContactPerson +'|'+ MobileNo +'|'+ Email +'<br/>'
                                    FROM BuyerContactDetail WITH(NOLOCK) Where BuyerId = BM.BuyerId And IsActive = 1
                                    FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As ContactPersonDetail ";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "CompanyName";
                this.SortOrder = "desc";
                this.TableName = @" BuyerMaster as BM WITH(NOLOCK)
                                    left join AgencyTypeMaster AT WITH(NOLOCK) on AT.AgencyTypeId = BM.AgencyTypeId
                                    left join BuyerContactDetail BC WITH(NOLOCK) on BC.BuyerId = BM.BuyerId ";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "Buyer List";
                this.ExportedColumns = @" Distinct BM.BuyerId[Hidden],BM.CompanyName,BM.WebAddress,BM.Remark,BM.Email,BM.Telephone,BM.ContactType,BM.Fax,
                                            BM.DocumentsData,BM.AgencyTypeId[Hidden],AT.AgencyType,BM.IsActive[Hidden],BM.CreatedBy[Hidden],
                                            (SELECT STUFF((
                                            SELECT ', ' + CCT.CountryName 
                                            FROM BuyerAddressDetail AS AC 
                                            INNER JOIN CityMaster AS CT ON CT.cityid=AC.cityid
                                            INNER JOIN StateMaster AS ST ON CT.stateid=ST.stateid
                                            INNER JOIN CountryMaster AS CCT ON ST.countryid=CCT.countryid
                                            Where BC.BuyerId = AC.BuyerId And CT.IsActive = 1
                                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As Country,
                                            (SELECT STUFF((
                                            SELECT ', ' + ST.StateName 
                                            FROM BuyerAddressDetail AS AC 
                                            INNER JOIN CityMaster AS CT ON CT.cityid=AC.cityid
                                            INNER JOIN StateMaster AS ST ON CT.stateid=ST.stateid
                                            INNER JOIN CountryMaster AS CCT ON ST.countryid=CCT.countryid
                                            Where BC.BuyerId = AC.BuyerId And CT.IsActive = 1
                                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As StateName,                                            
                                            (SELECT STUFF((
                                            SELECT ', ' + CityName 
                                            FROM BuyerAddressDetail AS AC 
                                            INNER JOIN CityMaster AS CT ON CT.cityid=AC.cityid
                                            Where BC.BuyerId = AC.BuyerId And IsActive = 1
                                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As [CityName],
		                                    (SELECT STUFF((
                                            SELECT ', ' + Ac.Address
                                            FROM BuyerAddressDetail AS AC 
                                            Where BC.BuyerId = AC.BuyerId 
                                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As Addres,
                                            REPLACE (  (SELECT STUFF((
                                    SELECT ', ' + ISNULL(AC.Telephone,'')+'|'+ ISNULL(AC.Email,'') +'|'+ ISNULL(AC.Fax,'') +'|'+ ISNULL(AC.WebAddress,'')+''
                                    FROM BuyerAddressDetail AS AC 
                                    Where BM.BuyerId = AC.BuyerId 
                                    FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')),'|||<br/>' , '' ) As ContectDetails,
                                            (SELECT STUFF((
                                            SELECT ', ' + ContactPerson + ' ' + Surname
                                            FROM BuyerContactDetail WITH(NOLOCK) Where BuyerId = BM.BuyerId And IsActive = 1
                                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As [Person],
                                            (SELECT STUFF((
                                            SELECT ', ' + MobileNo
                                            FROM BuyerContactDetail WITH(NOLOCK) Where BuyerId = BM.BuyerId And IsActive = 1
                                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As [MobileNo],
                                            (SELECT STUFF((
                                            SELECT ', ' + ChatDetails
                                            FROM BuyerContactDetail WITH(NOLOCK) Where BuyerId = BM.BuyerId And IsActive = 1
                                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As [ChatDetails],
                                            (SELECT STUFF((
                                            SELECT ', ' + Email
                                            FROM BuyerContactDetail WITH(NOLOCK) Where BuyerId = BM.BuyerId And IsActive = 1
                                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As [ContactEmail]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<BuyerMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "BuyerContactMaster")
            {
                this.ColumnsName = "BM.BuyerId,BM.CompanyName,BC.ContactId,BC.ContactPerson,BM.IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "ContactId";
                this.SortOrder = "desc";
                this.TableName = "BuyerContactDetail as BC Join BuyerMaster as BM On BC.BuyerId = BM.BuyerId";
                if (obj.FixClause == null)
                    this.WhereClause = "ISNULL(IsActive,1)=1";
                else
                    this.WhereClause = "ISNULL(IsActive,1)=1 AND " + obj.FixClause;
                this.ExportedFileName = "Buyer Contact List";
                this.ExportedColumns = "BM.BuyerId[Hidden],BM.CompanyName,BC.ContactId[Hidden],BC.ContactPerson,BM.IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<BuyerContactDetail>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "PaymentTermsMaster")
            {
                this.ColumnsName = "PaymentTermId,TermName,Terms,Description,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "PaymentTermId";
                this.SortOrder = "desc";
                this.TableName = "PaymentTermsMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "PaymentTermList";
                this.ExportedColumns = "PaymentTermId[Hidden],TermName,Terms,Description,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<PaymentTerms>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "ExhibitionMaster")
            {
                this.ColumnsName = @"Em.ExId,Em.ExName,Em.ExDate,Em.Venue,Em.ExProfile,Em.OrganizerDetail,Em.Address,
                                    DATENAME(MONTH,ExDate)[Month],Em.Tel,Em.MobileNo,Em.Email,Em.Web,Em.ContactPerson,
                                    Em.Chat,Em.CreatedDate,Em.CreatedBy,Em.IsActive,C.CityId,C.CityName,S.StateId,
                                    S.StateName,Co.CountryId,CO.CountryName,ccm.CountryId MobileId,cm.CountryId TelId";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "ExId";
                this.SortOrder = "desc";
                this.TableName = "ExhibitionMaster Em with(nolock)  inner join CityMaster C with(nolock) on Em.CityId = C.CityId  inner join Statemaster S with(nolock) on C.StateId = S.StateId inner join CountryMaster Co with(nolock) on Co.CountryId = S.CountryId  left join CountryMaster ccm with(nolock) on ccm.CountryCallCode=LEFT(Em.MobileNo,CHARINDEX(' ',Em.MobileNo)-1) left join CountryMaster cm with(nolock) on cm.CountryCallCode=LEFT(Em.Tel,CHARINDEX(' ',Em.Tel)-1)";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "Exhibition List";
                this.ExportedColumns = @"Em.ExId[Hidden],Em.ExName,CO.CountryName,S.StateName,C.CityName,
                                        Em.Venue,Em.Address[Hidden],Em.ExProfile,DATENAME(MONTH,ExDate)[Month],
                                        Em.OrganizerDetail[Hidden],Em.CreatedDate[Hidden],Em.IsActive[Hidden],S.StateId[Hidden],
                                        Co.CountryId[Hidden],Em.CreatedBy[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<ExhibitionMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "EventMaster")
            {
                this.ColumnsName = "ET.EventTypeName,E.EventTypeId,E.EventId,E.EventName,E.EventDate,E.IsActive,E.CreatedBy";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "EventId";
                this.SortOrder = "desc";
                this.TableName = "EventMaster E inner join EventTypeMaster ET on E.EventTypeId=ET.EventTypeId";
                this.WhereClause = "ISNULL(IsActive,0)=1";//ISNULL(IsActive,0)=1
                this.ExportedFileName = "EventList";
                this.ExportedColumns = "ET.EventTypeName,E.EventTypeId[Hidden],E.EventId[Hidden],E.EventName,CONVERT(VARCHAR(20),E.EventDate,105)EventDate,E.IsActive[Hidden],E.CreatedBy[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<Event>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "EventTypeMaster")
            {
                this.ColumnsName = "EventTypeId,EventTypeName,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "EventTypeId";
                this.SortOrder = "desc";
                this.TableName = "EventTypeMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";//ISNULL(IsActive,0)=1
                this.ExportedFileName = "EventTypeList";
                this.ExportedColumns = "EventTypeId[Hidden],EventTypeName,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<Event>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "AgencyTypeMaster")
            {
                this.ColumnsName = "AgencyTypeId,AgencyType,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "AgencyType";
                this.SortOrder = "desc";
                this.TableName = "AgencyTypeMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "AgencyTypeList";
                this.ExportedColumns = "AgencyTypeId[Hidden],AgencyType,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<AgencyTypeMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "Phasemaster")
            {
                this.ColumnsName = "PhaseId,Phase,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "Phase";
                this.SortOrder = "desc";
                this.TableName = "Phasemaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "PhaseList";
                this.ExportedColumns = "PhaseId[Hidden],Phase,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<Phasemaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "FrequencyMaster")
            {
                this.ColumnsName = "FrequencyId,Frequency,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "Frequency";
                this.SortOrder = "desc";
                this.TableName = "FrequencyMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "FrequencyList";
                this.ExportedColumns = "FrequencyId[Hidden],Frequency,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<FrequencyMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "VoltageMaster")
            {
                this.ColumnsName = "VoltageId,Voltage,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "Voltage";
                this.SortOrder = "desc";
                this.TableName = "VoltageMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "VoltageList";
                this.ExportedColumns = "VoltageId[Hidden],Voltage,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<VoltageMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "ReportFormatMaster")
            {
                this.ColumnsName = "RotFormatId,CompanyCode,CompanyHeader,CompanyFooter,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "RotFormatId";
                this.SortOrder = "desc";
                this.TableName = "ReportFormatMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "ReportFormatList";
                this.ExportedColumns = "RotFormatId[Hidden],CompanyCode,CompanyHeader,CompanyFooter,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<ReportFormatMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "ContactInvitationMaster")
            {
                this.ColumnsName = "ContactInvitationId,ContactInvitation,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "ContactInvitation";
                this.SortOrder = "desc";
                this.TableName = "ContactInvitationMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "Contact Invitation List";
                this.ExportedColumns = "ContactInvitationId[Hidden],ContactInvitation,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<ContactInvitationMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "ProductDocumentMaster")
            {
                this.ColumnsName = "PrdDocId,PrdDocName,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "PrdDocName";
                this.SortOrder = "desc";
                this.TableName = "ProductDocumentMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "ProductDocumentList";
                this.ExportedColumns = "PrdDocId[Hidden],PrdDocName,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<ProductDocumentMaster>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "AcHolderMaster")
            {
                this.ColumnsName = "AcHolderCode,AcHolderName,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "AcHolderName";
                this.SortOrder = "desc";
                this.TableName = "AcHolderMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "AcHolderList";
                this.ExportedColumns = "AcHolderCode[Hidden],AcHolderName,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<AcHolderMaster>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "SalaryHeadMaster")
            {
                this.ColumnsName = "SalaryHeadId,SalaryHeadName,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "SalaryHeadName";
                this.SortOrder = "desc";
                this.TableName = "SalaryHeadMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "SalaryHeadList";
                this.ExportedColumns = "SalaryHeadId[Hidden],SalaryHeadName,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<SalaryHeadMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "SalesDocumentNameMaster")
            {
                this.ColumnsName = "SalesDocId,SalesDocument,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "SalesDocument";
                this.SortOrder = "desc";
                this.TableName = "SalesDocumentNameMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "SalesDocumentList";
                this.ExportedColumns = "SalesDocId[Hidden],SalesDocument,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<SalesDocumentNameMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "PaymentModeMaster")
            {
                this.ColumnsName = "PaymentModeId,PaymentMode,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "PaymentMode";
                this.SortOrder = "desc";
                this.TableName = "PaymentModeMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "PaymentModeList";
                this.ExportedColumns = "PaymentModeId[Hidden],PaymentMode,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<PaymentModeMaster>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "FinancialYearMaster")
            {
                this.ColumnsName = "FinancialYearId,FinancialYear,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "FinancialYear";
                this.SortOrder = "desc";
                this.TableName = "FinancialYearMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "FinancialYearList";
                this.ExportedColumns = "FinancialYearId[Hidden],FinancialYear,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<FinancialYearMaster>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "ITRMaster")
            {
                this.ColumnsName = "ITRId,ITRName,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "ITRId";
                this.SortOrder = "desc";
                this.TableName = "ITRMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "ITRMaster";
                this.ExportedColumns = "ITRId[Hidden],ITRName,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<ITRMaster>();
                else
                    oGrid.Export(obj);
            }


            else if (obj.Mode == "DeliveryTermsMaster")
            {
                this.ColumnsName = "TermsId,DeliveryName,Description,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "TermsId";
                this.SortOrder = "desc";
                this.TableName = "DeliveryTermsMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "DeliveryTermList";
                this.ExportedColumns = "TermsId[Hidden],DeliveryName,Description,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<DeliveryTerms>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "UnitMaster")
            {
                this.ColumnsName = "UnitId,UnitName,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "UnitId";
                this.SortOrder = "desc";
                this.TableName = "UnitMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";//ISNULL(IsActive,0)=1
                this.ExportedFileName = "UnitList";
                this.ExportedColumns = "UnitId[Hidden],UnitName,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<Unit>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "CurrencyMaster")
            {
                this.ColumnsName = "CurrencyId,CurrencyName,CurrencySymbol,IsActive,CurrencyCode";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "CurrencyId";
                this.SortOrder = "desc";
                this.TableName = "CurrencyMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";//ISNULL(IsActive,0)=1
                this.ExportedFileName = "CurrencyList";
                this.ExportedColumns = "CurrencyId[Hidden],CurrencyName,CurrencySymbol,IsActive[Hidden],CurrencyCode";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<Currency>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "AreaMaster")
            {
                this.ColumnsName = @"AM.AreaId,AM.CityId,AM.AreaName,AM.IsActive,AM.IsDefault,CM.CityName,
                                        St.StateName,St.StateId,COM.CountryId,COM.CountryName";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "AreaId";
                this.SortOrder = "desc";
                this.TableName = "AreaMaster AM Inner Join CityMaster CM On  CM.CityId = AM.CityId Inner Join StateMaster St On St.StateId = CM.StateId Left Join CountryMaster COM On COM.CountryId=St.CountryId";
                if (obj.FixClause == null)
                    this.WhereClause = "ISNULL(IsActive,1)=1";
                else
                    this.WhereClause = "ISNULL(IsActive,1)=1 AND " + obj.FixClause;
                this.ExportedFileName = "AreaList";
                this.ExportedColumns = @"AM.AreaId[Hidden],AM.CityId[Hidden],AM.AreaName,AM.IsActive[Hidden],
                                            AM.IsDefault[Hidden],CM.CityName,St.StateName,St.StateId[Hidden],
                                            COM.CountryId[Hidden],COM.CountryName";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<AreaMaster>();
                else
                    oGrid.Export(obj);
            }


            else if (obj.Mode == "SpecificationMaster")
            {
                this.ColumnsName = "TM.SpecificationId,TM.TechSpec,TSM.TechHead,TM.IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "SpecificationId";
                this.SortOrder = "desc";
                this.TableName = "TechnicalSpecMaster as TM join TechnicalSpecHeadMaster as TSM on TSM.TechHeadId=TM.TechHeadId";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "SpecificationList";
                this.ExportedColumns = "SpecificationId[Hidden],TechSpec,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<Specification>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "VendorMaster")
            {
                this.ColumnsName = @"  Distinct BM.VendorId,BM.CompanyName,
						                BM.PAN,BM.[TAN],BM.Remark,
										(SELECT STUFF((
						                SELECT ', ' + Address 
						                FROM VendorAddressDetail AS AC 
						                Where BM.VendorId = AC.VendorId 
						                FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As [Address],
						                (SELECT STUFF((
						                SELECT ', ' + CityName 
						                FROM VendorAddressDetail AS AC 
						                LEFT JOIN CityMaster AS CT ON CT.cityid=AC.cityid
						                Where BM.VendorId = AC.VendorId 
						                FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As CityName,
						                (SELECT STUFF((
						                SELECT ', ' + ContactName
						                FROM VendorContactDetail WITH(NOLOCK) Where VendorId = BM.VendorId And IsActive = 1
						                FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As Person,
						                (SELECT STUFF((
						                SELECT ', ' + MobileNo
						                FROM VendorContactDetail WITH(NOLOCK) Where VendorId = BM.VendorId And IsActive = 1
						                FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As MobileNo,
						                (SELECT STUFF((
						                SELECT ', ' + Email
						                FROM VendorContactDetail WITH(NOLOCK) Where VendorId = BM.VendorId And IsActive = 1
						                FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As Email ,BM.IsActive  ";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "VendorId";
                this.SortOrder = "desc";
                this.TableName = @" VendorMaster as BM 
				                        LEFT JOIN VendorContactDetail BC WITH(NOLOCK) on BC.VendorId = BM.VendorId ";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "Buyer List";
                this.ExportedColumns = @"  Distinct BM.VendorId[Hidden],BM.CompanyName,
						                BM.PAN[Hidden],BM.Remark[Hidden],
										(SELECT STUFF((
						                SELECT ', ' + Address 
						                FROM VendorAddressDetail AS AC 
						                Where BM.VendorId = AC.VendorId 
						                FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As [Address] [Hidden],
						                (SELECT STUFF((
						                SELECT ', ' + CityName 
						                FROM VendorAddressDetail AS AC 
						                LEFT JOIN CityMaster AS CT ON CT.cityid=AC.cityid
						                Where BM.VendorId = AC.VendorId And IsActive = 1
						                FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As [District],
						                (SELECT STUFF((
						                SELECT ', ' + BC.ContactName
						                FROM VendorContactDetail WITH(NOLOCK) Where VendorId = BM.VendorId And IsActive = 1
						                FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As [Contect Person],
						                (SELECT STUFF((
						                SELECT ', ' + MobileNo
						                FROM VendorContactDetail WITH(NOLOCK) Where VendorId = BM.VendorId And IsActive = 1
						                FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As [Mobile No],
						                (SELECT STUFF((
						                SELECT ', ' + Email
						                FROM VendorContactDetail WITH(NOLOCK) Where VendorId = BM.VendorId And IsActive = 1
						                FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As Email ,BM.IsActive[Hidden]  ";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<VendorMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "CompanyMaster")
            {
                this.ColumnsName = "ComId,ComCode,ComName,RegOffAdd,CorpOffAdd,TelNos,Email,Web,ComLogo,TaxDetails,IsActive,CreatedBy";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "ComId";
                this.SortOrder = "desc";
                this.TableName = "CompanyMaster";
                this.WhereClause = "IsActive=1";
                this.ExportedFileName = "Company List";
                this.ExportedColumns = "ComId[Hidden],ComCode,ComName,RegOffAdd,CorpOffAdd,TelNos,Email,Web,ComLogo,TaxDetails,IsActive[Hidden],CreatedBy[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<Company>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "DepartmentMaster")
            {
                this.ColumnsName = "DepartmentId,DepartmentName,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "DepartmentName";
                this.SortOrder = "desc";
                this.TableName = "DepartmentMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "DepartmentList";
                this.ExportedColumns = "DepartmentId[Hidden],DepartmentName,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<DepartmentMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "RoleMaster")
            {
                this.ColumnsName = "RoleId,RoleName,IsActive,CreatedBy";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "RoleId";
                this.SortOrder = "desc";
                this.TableName = "RoleMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";//ISNULL(IsActive,0)=1

                this.ExportedFileName = "RoleList";
                this.ExportedColumns = "RoleId[Hidden],RoleName,IsActive[Hidden],CreatedBy[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<Role>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "SupplierMaster")
            {
                this.ColumnsName = @"  Distinct BM.SupplierId,BM.CompanyName,
								(SELECT STUFF((
									SELECT ', ' + Address 
									FROM SupplierAddressMaster AS AC 
									Where BM.SupplierId = AC.SupplierId And IsActive = 1
									FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As Address,
									 (SELECT STUFF((
									SELECT ', ' + CityName 
									FROM SupplierAddressMaster AS AC 
									INNER JOIN CityMaster AS CT ON CT.cityid=AC.cityid
									 Where BM.SupplierId = AC.SupplierId And IsActive = 1
									FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As CityName,
                                    (SELECT STUFF((
                                        SELECT ', ' + ContactName
                                        FROM SupplierContactDetail WITH(NOLOCK) Where SupplierId = BM.SupplierId 
										And IsActive = 1
                                        FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As Person,
                                    (SELECT STUFF((
                                        SELECT ', ' + MobileNo
                                        FROM SupplierContactDetail WITH(NOLOCK) Where SupplierId = BM.SupplierId 
										And IsActive = 1
                                        FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As MobileNo,
                                    (SELECT STUFF((
                                        SELECT ', ' + Email
                                        FROM SupplierContactDetail WITH(NOLOCK) Where SupplierId = BM.SupplierId 
										And IsActive = 1
                                        FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As Email,
                                    BM.IsActive    ";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = " SupplierId";
                this.SortOrder = "desc";
                this.TableName = @" SupplierMaster as BM ";
                this.WhereClause = " ISNULL(IsActive,0)=1";
                this.ExportedFileName = " Supplier List";
                this.ExportedColumns = @" Distinct BM.SupplierId[Hidden],BM.CompanyName,
								(SELECT STUFF((
									SELECT ', ' + Address 
									FROM SupplierAddressMaster AS AC 
									Where BM.SupplierId = AC.SupplierId And IsActive = 1
									FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As Address,
									 (SELECT STUFF((
									SELECT ', ' + CityName 
									FROM SupplierAddressMaster AS AC 
									INNER JOIN CityMaster AS CT ON CT.cityid=AC.cityid
									 Where BM.SupplierId = AC.SupplierId And IsActive = 1
									FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As CityName,
                                    (SELECT STUFF((
                                        SELECT ', ' + ContactName
                                        FROM SupplierContactDetail WITH(NOLOCK) Where SupplierId = BM.SupplierId 
										And IsActive = 1
                                        FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As Person,
                                    (SELECT STUFF((
                                        SELECT ', ' + MobileNo
                                        FROM SupplierContactDetail WITH(NOLOCK) Where SupplierId = BM.SupplierId 
										And IsActive = 1
                                        FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As MobileNo,
                                    (SELECT STUFF((
                                        SELECT ', ' + Email
                                        FROM SupplierContactDetail WITH(NOLOCK) Where SupplierId = BM.SupplierId 
										And IsActive = 1
                                        FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As Email,
                                    BM.IsActive[Hidden]   ";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<Supplier>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "SupplierContactDetail")
            {
                this.ColumnsName = "S.SupplierId,C.ContactId,S.CompanyName,C.ContactName,S.IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "ContactId";
                this.SortOrder = "desc";
                this.TableName = "SupplierContactDetail as C Join SupplierMaster as S on  C.SupplierId = S.SupplierId ";
                if (obj.FixClause == null)
                    this.WhereClause = "ISNULL(IsActive,1)=1";
                else
                    this.WhereClause = "ISNULL(IsActive,1)=1 AND " + obj.FixClause;
                this.ExportedFileName = "Supplier Contact List";
                this.ExportedColumns = "S.SupplierId[Hidden],C.ContactId[Hidden],S.CompanyName,C.ContactName,S.IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<SupplierContactDetail>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "SalesOrderMaster")
            {
                this.ColumnsName = @"SM.SOId,SM.SoNo,BM.CompanyName,BC.ContactPerson,DT.DeliveryName,PT.TermName,
                                        SM.IsActive,SM.CreatedBy,SM.SoDate,SM.SoRefNo,CM.ComName";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "SOId";
                this.SortOrder = "desc";
                this.TableName = @"SalesOrderMaster As SM 
                                   inner join BuyerMaster As BM on SM.BuyerId = BM.BuyerId 
                                   inner join BuyerContactDetail As BC on SM.BuyerContactId = BC.ContactId 
                                   inner join DeliveryTermsMaster As DT on SM.DeliveryTermId = DT.TermsId 
                                   inner join PaymentTermsMaster As PT on SM.PaymentTermId = PT.PaymentTermId 
                                   inner join CompanyMaster As CM on CM.ComId = SM.CompanyId";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                if (sessionUtils.UserRollType > 1)
                {
                    this.WhereClause += " AND CreatedBy=" + sessionUtils.UserId + "";
                }
                this.ExportedFileName = "SalesOrderList";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<SalesOrderMaster>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "TypeOfShipmentMaster")
            {
                this.ColumnsName = "ShipmentTypeId,ShipmentType,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "ShipmentTypeId";
                this.SortOrder = "desc";
                this.TableName = "TypeOfShipmentMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "TypeOfShipmentMasterList";
                this.ExportedColumns = "ShipmentTypeId[Hidden],ShipmentType,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<TypeOfShipmentMaster>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "InvoiceTypeMaster")
            {
                this.ColumnsName = "InvoiceTypeId,InvoiceTypeName,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "InvoiceTypeId";
                this.SortOrder = "desc";
                this.TableName = "InvoiceTypeMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "InvoiceTypeMasterList";
                this.ExportedColumns = "InvoiceTypeId[Hidden],InvoiceTypeName,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<InvoiceTypeMaster>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "QuotationMaster")
            {
                this.ColumnsName = @"Q.InqId,Q.QuotationId,Q.QuotationNo,DT.DeliveryName,PT.TermName,Q.DeliveryTermId,
                                    BuyerName as CompanyName,Q.PaymentTermId,Q.QuotationMadeBy,Q.QuotationDate,UM.Name,Q.IsActive,Q.CreatedBy
                                    ,IM.InqNo,Q.Total,Q.OfferValiddate,Q.Note";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "QuotationId";
                this.SortOrder = "desc";
                this.TableName = @"QuotationMaster as Q 
                                    INNER JOIN DeliveryTermsMaster AS DT ON Q.DeliveryTermId = DT.TermsId
                                    INNER JOIN PaymentTermsMaster AS PT ON Q.PaymentTermId = PT.PaymentTermId
                                    INNER JOIN UserMaster AS UM ON Q.QuotationMadeBy = UM.UserId
                                    INNER JOIN InquiryMaster AS IM ON IM.InqId = Q.InqId";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                if (sessionUtils.UserRollType > 1)
                {
                    this.WhereClause += " AND CreatedBy=" + sessionUtils.UserId + "";
                }
                this.ExportedFileName = "QuotationMasterList";
                this.ExportedColumns = @"Q.InqId[Hidden],Q.QuotationId[Hidden],Q.QuotationNo,BuyerName as CompanyName,DT.DeliveryName,PT.TermName,Q.DeliveryTermId[Hidden],
                                    Q.PaymentTermId[Hidden],Q.QuotationMadeBy,Q.QuotationDate[Hidden],UM.Name[Hidden],Q.IsActive[Hidden],Q.CreatedBy[Hidden]";

                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<QuotationMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "InquiryMaster")
            {
                this.ColumnsName = @" Distinct I.ContactPersonname, I.InqId,I.CreatedBy,I.Subject,C.CityId,I.InqDate,I.InqNo,I.BuyerName,replace(I.MobileNo,',',', ')  As MobileNo,replace(I.Email,',',', ') As Email,I.Requirement,I.Address,
                                    C.CityName,I.SourceId,S.SourceName,ST.StateId,ST.StateName,I.Status,(select top 1  TS1.TaskStatus From InquiryFollowupMaster T1 Inner JOIN TaskStatusMaster AS TS1 ON T1.Status = TS1.StatusId 
                                        WHERE T1.inqid =I.InqId ORDER BY T1.FollowupId DESC) AS TaskStatus,
                                    ST.CountryId,CM.CountryName,I.IsActive,ISNULL(UI.Name,'') As AssignFromUser,
                                    ISNULL(I.AssignTo,0) As AssignTo,ISNULL(U.Name,'') As AssignToUser,
                                    ISNULL((select top 1 currentupdate From InquiryFollowupMaster where InqId=I.InqId ORDER BY FollowupId DESC),' ') As Remark ,
                                    (select top 1 case when NextFollowDate is NULL then '23:59:59' else NextFollowDate end  From InquiryFollowupMaster WHERE InqId=I.InqId ORDER BY FollowupId DESC) As FollowDate,
	                                (select top 1 case when NextFollowTime is NULL then '1001/01/01' else NextFollowTime end From InquiryFollowupMaster WHERE  InqId=I.InqId ORDER BY FollowupId DESC) As FollowTime,
                                    (SELECT TOP 1 * From
	                                (select TOP 2 case when NextFollowDate is NULL then '23:59:59' else NextFollowDate end AS NextFollowDate  
	                                From InquiryFollowupMaster WHERE InqId=551 ORDER BY FollowupId DESC) x order by NextFollowDate) AS LastFollowDate,
                                    (SELECT TOP 1 * From
	                                (select  TOP 2 case when NextFollowTime is NULL then '1001/01/01' else NextFollowTime end AS NextFollowTime  
	                                From InquiryFollowupMaster WHERE InqId=551 ORDER BY FollowupId DESC ) x order by NextFollowTime) AS LastFollowTime,
                                    (select top 1 AssignId From InquiryFollowupMaster WHERE InqId=I.InqId ORDER BY FollowupId DESC) AS FollowUserId,
                                    case when (select top 1 AssignId From InquiryFollowupMaster WHERE InqId=I.InqId ORDER BY FollowupId DESC)=" + sessionUtils.UserId + " then 'true' else 'false' end FollowStatus,I.CreatedDate";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "InqId";
                this.SortOrder = "desc";
                this.TableName = @" InquiryMaster AS I
                                    inner Join InquiryFollowupMaster As F On I.InqId = F.InqId 
                                    inner join CityMaster as C on C.CityId = I.CityId         
                                    Inner join StateMaster as ST on ST.StateId = C.StateId 
                                    inner join CountryMaster as CM on CM.CountryId = ST.CountryId 
                                    inner join SourceMaster as S on S.SourceId = I.SourceId 
                                    inner join TaskStatusMaster  as ts on ts.StatusId = I.Status 
                                    Left join UserMaster  as U on U.UserId = I.AssignTo 
                                    Left join UserMaster  as UI on UI.UserId = I.CreatedBy GROUP BY
                                    I.ContactPersonname,I.InqId,I.Subject,C.CityId,I.InqDate,I.InqNo,I.BuyerName,replace(I.MobileNo,',',', '),replace(I.Email,',',', '),I.Requirement,I.Address,
                                    C.CityName,I.SourceId,S.SourceName,I.Remark,ST.StateId,ST.StateName,I.Status,TS.TaskStatus,
                                    ST.CountryId,CM.CountryName,I.IsActive,I.CreatedBy,I.CreatedDate ,ISNULL(UI.Name,''),
                                    ISNULL(I.AssignTo,0),ISNULL(U.Name,'') ,F.FollowupId";
                if (obj.FixClause == null)
                    this.WhereClause = "ISNULL(IsActive,1)=1";
                else
                    this.WhereClause = "ISNULL(IsActive,1)=1 AND " + obj.FixClause;
                if (sessionUtils.UserRollType > 1)
                {
                    this.WhereClause += " And (CreatedBy= " + sessionUtils.UserId + " OR AssignTo = " + sessionUtils.UserId + " OR FollowUserId=" + sessionUtils.UserId + ") ";
                }
                this.ExportedFileName = "Inquiry List";
                this.ExportedColumns = @" Distinct I.InqId[Hidden],I.Subject,I.CreatedBy[Hidden],C.CityId[Hidden],S.SourceName[Hidden],I.InqNo,I.InqDate,I.BuyerName,replace(I.MobileNo,',',', ')  As MobileNo,replace(I.Email,',',', ') As Email,I.Requirement,I.Address[Hidden],
                                    C.CityName[Hidden],I.SourceId[Hidden],I.Remark,ST.StateId[Hidden],ST.StateName[Hidden],I.Status[Hidden],TS.TaskStatus[Hidden],
                                    ST.CountryId[Hidden],CM.CountryName[Hidden],I.IsActive[Hidden],ISNULL(UI.Name,'') As AssignFromUser[Hidden],
                                    ISNULL(I.AssignTo,0) As AssignTo,ISNULL(U.Name,'') As AssignToUser,
                                    (select top 1 AssignId From InquiryFollowupMaster WHERE InqId=I.InqId ORDER BY FollowupId DESC) AS FollowUserId[Hidden],
                                    case when (select top 1 AssignId From InquiryFollowupMaster WHERE InqId=I.InqId ORDER BY FollowupId DESC)=" + sessionUtils.UserId + " then 'true' else 'false' end FollowStatus[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<Inquiry>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "TodayInquiryMaster")
            {
                this.ColumnsName = @" Distinct I.InqId,I.CreatedBy,C.CityId,I.InqDate,I.InqNo,I.BuyerName,replace(I.MobileNo,',',', ')  As MobileNo,replace(I.Email,',',', ') As Email,I.Requirement,I.Address,
                                    C.CityName,I.SourceId,S.SourceName,I.Remark,ST.StateId,ST.StateName,I.Status,TS.TaskStatus,I.CreatedDate,
                                    ST.CountryId,CM.CountryName,I.IsActive,ISNULL(UI.Name,'') As AssignFromUser,
                                    ISNULL(I.AssignTo,0) As AssignTo,ISNULL(U.Name,'') As AssignToUse,
                                    (select top 1 AssignId From InquiryFollowupMaster WHERE InqId=I.InqId ORDER BY FollowupId DESC) AS FollowUserId, ";
                if (sessionUtils.UserRollType == 1)
                {
                    this.ColumnsName += @" 'true' As FollowStatus ";
                }
                else if (sessionUtils.UserRollType > 1)
                {
                    this.ColumnsName += @" case when (select top 1 AssignId From InquiryFollowupMaster WHERE InqId=I.InqId ORDER BY FollowupId DESC)=" + sessionUtils.UserId + " then 'true' else 'false' end FollowStatus";
                }
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "InqId";
                this.SortOrder = "desc";
                this.TableName = @" InquiryMaster AS I
                                    inner Join InquiryFollowupMaster As F On I.InqId = F.InqId 
                                    inner join CityMaster as C on C.CityId = I.CityId         
                                    Inner join StateMaster as ST on ST.StateId = C.StateId 
                                    inner join CountryMaster as CM on CM.CountryId = ST.CountryId 
                                    inner join SourceMaster as S on S.SourceId = I.SourceId 
                                    inner join TaskStatusMaster  as ts on ts.StatusId = I.Status 
                                    Left join UserMaster  as U on U.UserId = I.AssignTo 
                                    Left join UserMaster  as UI on UI.UserId = I.CreatedBy GROUP BY
                                    I.InqId,C.CityId,I.InqDate,I.InqNo,I.BuyerName,replace(I.MobileNo,',',', '),replace(I.Email,',',', '),I.Requirement,I.Address,
                                    C.CityName,I.SourceId,S.SourceName,I.Remark,ST.StateId,ST.StateName,I.Status,TS.TaskStatus,
                                    ST.CountryId,CM.CountryName,I.IsActive,I.CreatedBy,I.CreatedDate ,ISNULL(UI.Name,''),
                                    ISNULL(I.AssignTo,0),ISNULL(U.Name,'') ,F.FollowupId";
                if (obj.FixClause == null)
                    this.WhereClause = "ISNULL(IsActive,1)=1  ";
                else
                    this.WhereClause = "ISNULL(IsActive,1)=1 AND " + obj.FixClause;
                if (sessionUtils.UserRollType > 1)
                {
                    this.WhereClause += " And (CreatedBy= " + sessionUtils.UserId + " OR AssignTo = " + sessionUtils.UserId + " OR FollowUserId=" + sessionUtils.UserId + ") ";
                }
                this.WhereClause += " And CONVERT(VARCHAR, CreatedDate, 112) = CONVERT(VARCHAR, GETDATE(), 112)";
                this.ExportedFileName = "Inquiry List";
                this.ExportedColumns = @" Distinct I.InqId[Hidden],I.CreatedBy[Hidden],C.CityId[Hidden],I.InqDate,I.InqNo,I.BuyerName,replace(I.MobileNo,',',', ')  As MobileNo,replace(I.Email,',',', ') As Email,I.Requirement,I.Address,
                                    C.CityName,I.SourceId[Hidden],S.SourceName,I.Remark,ST.StateId[Hidden],ST.StateName,I.Status[Hidden],TS.TaskStatus,I.CreatedDate,
                                    ST.CountryId[Hidden],CM.CountryName,I.IsActive[Hidden],ISNULL(UI.Name,'') As AssignFromUser,
                                    ISNULL(I.AssignTo,0) As AssignTo,ISNULL(U.Name,'') As AssignToUse,
                                    (select top 1 AssignId From InquiryFollowupMaster WHERE InqId=I.InqId ORDER BY FollowupId DESC) AS FollowUserId[Hidden],
                                    case when (select top 1 AssignId From InquiryFollowupMaster WHERE InqId=I.InqId ORDER BY FollowupId DESC)=" + sessionUtils.UserId + " then 'true' else 'false' end FollowStatus[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<Inquiry>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "DocumentNameMaster")
            {
                this.ColumnsName = "DocId,DocName,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "DocId";
                this.SortOrder = "desc";
                this.TableName = "DocumentNameMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "Document List";
                this.ExportedColumns = "DocId[Hidden],DocName,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<Document>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "UserMaster")
            {
                this.ColumnsName = @"u.UserId,u.Name +' '+ u.surname [Employee_Name],u.FatherName,dm.DepartmentName,u.MobNo [MobNo],u.Email
                                    ,u.UserName,u.ChatDetail,case when u.IsActive = 0 then 'InActive' else ' Active' end [Status],u.IsActive,u.CreatedBy
                                    ,u.BirthDate,u.BirthPlaceArea,u.BirthPlacePincode,u.HomeTownArea,u.HomeTownPincode,bl.BloodGroup,u.ResidentNo,u.PresentArea
                                    ,u.PresentPinCode,u.PresentAddress,u.PermanentArea,u.PermanentPinCode,u.PermanentAddress,u.DrivingLicNo,u.VoterIdNumber
                                    ,u.PassportNo,u.PANNo,u.AadharNo,u.FullNameAsPerBank,u.BranchName,bnk.BankName,u.AccountNo,ac.AccountType,u.IFSC
                                    ,u.Name +' '+ u.surname [ReportingName]
                                    ,(case when u.ReferenceTypeId=2 then ag.AgencyType end)AgencyTypeReferanceName,
                                    (case when u.ReferenceTypeId=1 and u.ReferenceSubType=1 then vsg.CompanyName 
                                    when u.ReferenceTypeId=1 and u.ReferenceSubType=2 then ssg.CompanyName 
                                    when u.ReferenceTypeId=1 and u.ReferenceSubType=3 then usg.UserName
                                    when u.ReferenceTypeId=1 and u.ReferenceSubType=4 then bsg.CompanyName
                                    end)ReferanceName,u.ReferenceMannualEntry,u.JoiningDate,dsgm.DesignationName,rl.RoleName,u.MICRCode,u.ShiftStartTime,u.ShiftEndTime
                                    ,u.LunchStartTime,u.LunchEndTime,u.CommunicationDate,u.ContactMobile,u.ContactEmail,u.ContactChat
                                    ,ss.Sourcename";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = " UserId";
                this.SortOrder = " desc";
                this.TableName = @"UserMaster As u 
                                   left join DepartmentMaster As dm on u.DepartmentId = dm.DepartmentId 
                                   left join BloodGroupMaster As bl on u.BloodGroupId = bl.BloodGroupId 
                                   left join BankNameMaster As bnk on u.BankName = bnk.BankId 
                                   left join AccountTypeMaster As ac on u.AccountTypeId = ac.AccountTypeId 
                                   left join UserMaster As rp on u.userid = rp.userid 
                                   left join DesignationMaster as dsgm with(nolock) on  u.DesignationId = dsgm.DesignationId
                                   left join RoleMaster as rl with(nolock) on  u.RoleId = rl.RoleId
                                   left join AgencyTypeMaster as ag with(nolock) on  u.ReferenceId = ag.AgencyTypeId
                                   left join UserMaster as usg with(nolock) on  u.ReferenceId = usg.UserId
                                   left join BuyerMaster as bsg with(nolock) on  u.ReferenceId = bsg.BuyerId
                                   left join SupplierMaster as ssg with(nolock) on  u.ReferenceId = ssg.SupplierId
                                   left join VendorMaster as vsg with(nolock) on  u.ReferenceId = vsg.VendorId 
                                   left join SourceMaster as ss with(nolock) on  u.SourceId = ss.SourceId ";
                this.MultiOrder = " IsActive desc , UserId desc";
                if (sessionUtils.UserRollType > 1 && sessionUtils.UserRollType != 13)
                {
                    this.WhereClause += " UserId=" + sessionUtils.UserId + "";
                }
                else
                {
                    this.WhereClause = " 1=1 ";
                }
                this.ExportedFileName = "User List";
                this.ExportedColumns = "u.UserId[Hidden],u.Name +' '+ u.surname [Employee_Name],u.Name[Hidden],dm.DepartmentName[Hidden],u.MobNo[Hidden],dm.DepartmentName [Department],u.MobNo[Mobile],u.Email,u.ChatDetail,case when u.IsActive = 0 then 'InActive' else ' Active' end [Status],u.IsActive[Hidden],u.CreatedBy[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<UserMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "TaskMaster")
            {
                this.ColumnsName = @" Distinct
                                        T.TaskId,T.TaskNo,T.Task,ISNULL(T.DeadlineDate,'') AS DeadlineDate,T.Priority,TP.PriorityName,T.Review,T.Status,TS.TaskStatus,
                                        T.TaskTypeId,TTM.TaskType,T.GroupBy,T.IsActive,T.TaskGroupId,TG.TaskGroupName,TDM.FromId,T.CreatedBy,T.CreatedDate,
                                        FromUM.Name as FromUser,T.Duration,
                                        (SELECT STUFF((
                                        SELECT Distinct ', ' + InUM.Name
                                        FROM TaskDetailMaster As InTD
                                        INNER JOIN UserMaster As InUM  on InUM.UserId = InTD.ToId 
                                        Where InTD.TaskId = T.TaskId And InTD.ToId <> InTD.FromId 
                                        FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, ''))
                                        As AssignTo,                                    
                                        ISNULL((select top 1 Note From TaskDetailMaster WHERE TaskId=T.TaskId ORDER BY TaskDetailId DESC),' ') As Note ,

                                        (select top 1 case when NextFollowDate is NULL then '23:59:59' else NextFollowDate end  From TaskDetailMaster WHERE TaskId=T.TaskId ORDER BY TaskDetailId DESC) As NextFollowDate,
                                        (select top 1 case when NextFollowTime is NULL then '1001/01/01' else NextFollowTime end From TaskDetailMaster WHERE TaskId=T.TaskId ORDER BY TaskDetailId DESC) As NextFollowTime,

                                        (select top 1 case when ActualDate is NULL then '23:59:59' else ActualDate end  From TaskDetailMaster WHERE TaskId=T.TaskId ORDER BY TaskDetailId DESC) As ActualDate,
                                        (select top 1 case when ActualTime is NULL then '1001/01/01' else ActualTime end From TaskDetailMaster WHERE TaskId=T.TaskId ORDER BY TaskDetailId DESC) As ActualTime,

                                        (select top 1 case when Convert(Date, PlanDateTime,101) is NULL then '23:59:59' else Convert(Date, PlanDateTime,101) end  From TaskDetailMaster WHERE TaskId=T.TaskId ORDER BY TaskDetailId DESC) As PlanDateTime,
                                        (select top 1 case when cast(PlanDateTime as time) is NULL then '1001/01/01' else cast(PlanDateTime as time) end From TaskDetailMaster WHERE TaskId=T.TaskId ORDER BY TaskDetailId DESC) As PlanTime,

                                        (select top 1 case when Convert(Date, CreatedDate,101) is NULL then '23:59:59' else Convert(Date, CreatedDate,101) end  From TaskDetailMaster WHERE TaskId=T.TaskId ORDER BY TaskDetailId DESC) As FollowCreatedDate,
                                        (select top 1 case when cast(CreatedDate as time) is NULL then '1001/01/01' else cast(CreatedDate as time) end From TaskDetailMaster WHERE TaskId=T.TaskId ORDER BY TaskDetailId DESC) As FollowCreatedTime,
                                        
                                        (select top 1 InUM.Name From TaskDetailMaster As InTD INNER JOIN UserMaster As InUM  on InUM.UserId = InTD.FromId Where InTD.TaskId = T.TaskId ORDER BY InTD.TaskDetailId DESC) AS FollowFromUser,
                                        
                                        (SELECT TOP 1 * From
                                        (select TOP 2 case when NextFollowDate is NULL then '23:59:59' else NextFollowDate end AS NextFollowDate  
                                        From TaskDetailMaster WHERE TaskId=T.TaskId ORDER BY TaskDetailId DESC) x order by NextFollowDate)as lastFollowdate,
                                        (SELECT TOP 1 * From
                                        (select  TOP 2 case when NextFollowTime is NULL then '1001/01/01' else NextFollowTime end AS NextFollowTime  
                                        From TaskDetailMaster WHERE TaskId=T.TaskId ORDER BY TaskDetailId DESC ) x order by NextFollowTime desc)as lastFollowTime,
                                        (select top 1 ToId From TaskDetailMaster WHERE TaskId=T.TaskId ORDER BY TaskDetailId DESC) AS FollowUserId,
                                        case when (select top 1 ToId From TaskDetailMaster WHERE TaskId=T.TaskId ORDER BY TaskDetailId DESC)=" + sessionUtils.UserId;
                this.ColumnsName += " then 'true' else 'false' end FollowStatus";
                this.ColumnsName += @", (select top 1 TS1.TaskStatus From TaskDetailMaster T1 LEFT JOIN TaskStatusMaster AS TS1 ON T1.TaskStatus = TS1.StatusId 
                                        WHERE T1.TaskId = T.TaskId ORDER BY T1.TaskDetailId DESC) AS FollowUpTaskStatus";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = " lastFollowdate";
                this.SortOrder = "DESC ";
                this.TableName = @" TaskMaster AS T 
                                    LEFT JOIN gurjari_crmuser.TaskGroupMaster AS TG ON T.TaskGroupId = TG.TaskGroupId 
                                    LEFT JOIN gurjari_crmuser.TaskPriorityMaster AS TP ON T.Priority = TP.PriorityId  
                                    LEFT JOIN gurjari_crmuser.TaskStatusMaster AS TS ON T.Status = TS.StatusId 
                                    LEFT JOIN gurjari_crmuser.TaskTypeMaster AS TTM ON T.TaskTypeId = TTM.TaskTypeId 
                                    LEFT JOIN gurjari_crmuser.TaskDetailMaster as TDM  on TDM.TaskId = T.TaskId  AND TDM.ToId = T.CreatedBy
                                    LEFT JOIN gurjari_crmuser.UserMaster as FromUM  on FromUM.UserId = TDM.FromId 
                                    LEFT JOIN gurjari_crmuser.UserMaster as ToUM  on ToUM.UserId = TDM.ToId";
                if (obj.FixClause == null)
                    this.WhereClause = "ISNULL(IsActive,1)=1  ";
                else
                    this.WhereClause = "ISNULL(IsActive,1)=1 AND " + obj.FixClause;
                if (sessionUtils.UserRollType > 1)
                {
                    this.WhereClause += " And (CreatedBy= " + sessionUtils.UserId + " OR FollowUserId=" + sessionUtils.UserId + ") ";
                }
                else if (sessionUtils.UserRollType == 1)
                {
                    this.WhereClause += " AND TaskStatus != 'Completed'";
                }
                this.ExportedFileName = "Task List";
                this.ExportedColumns = @" Distinct
                                        T.TaskId[Hidden],T.TaskNo,case when T.DeadlineDate is NULL then '' else CONVERT(VARCHAR(20),T.DeadlineDate,105) end AS DeadlineDate,
                                        FromUM.Name as AssignFrom,T.Task,TP.PriorityName AS Priority,TP.PriorityName[Hidden],T.Review[Hidden],TS.TaskStatus,
                                        T.TaskTypeId[Hidden],TTM.TaskType[Hidden],T.GroupBy[Hidden],T.IsActive[Hidden],T.TaskGroupId[Hidden],TG.TaskGroupName,TDM.FromId[Hidden],FromUM.Name as FromUser,(SELECT STUFF((
                                        SELECT Distinct ', ' + InUM.Name
                                        FROM TaskDetailMaster As InTD
                                        INNER JOIN UserMaster As InUM  on InUM.UserId = InTD.ToId 
                                        Where InTD.TaskId = T.TaskId And InTD.ToId <> InTD.FromId 
                                        FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As AssignTo,TDM.NextFollowDate[Hidden],
                                        case when TDM.NextFollowDate is NULL then '' else CONVERT(VARCHAR(20),TDM.NextFollowDate,105) end +' '+
                                        case when TDM.NextFollowTime is NULL then '' else CONVERT(varchar(15),TDM.NextFollowTime,108) end As [Follow DateTime],
                                        TDM.Note,T.CreatedBy[Hidden],
                                        (select top 1 ToId From TaskDetailMaster WHERE TaskId=T.TaskId ORDER BY TaskDetailId DESC) AS FollowUserId,
                                        case when (select top 1 ToId From TaskDetailMaster WHERE TaskId=T.TaskId ORDER BY TaskDetailId DESC)=" + sessionUtils.UserId;
                this.ExportedColumns += " then 'true' else 'false' end FollowStatus";
                this.ExportedColumns += @", (select top 1 TS1.TaskStatus From TaskDetailMaster T1 LEFT JOIN TaskStatusMaster AS TS1 ON T1.TaskStatus = TS1.StatusId 
                                        WHERE T1.TaskId = T.TaskId ORDER BY T1.TaskDetailId DESC) AS FollowUpTaskStatus";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<TaskMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "TodayTaskMaster")
            {
                this.ColumnsName = @" Distinct
                                        T.TaskId,T.TaskNo,T.Task,T.Priority,TP.PriorityName,T.Review,T.Status,TS.TaskStatus,
                                        T.TaskTypeId,TTM.TaskType,T.GroupBy,T.IsActive,T.TaskGroupId,TG.TaskGroupName,TDM.FromId,
                                        FromUM.Name as FromUser,TDM.ToId As AssignToId,ToUM.UserName As AssignTo,
                                        --case when TDM.FollowDate is NULL then '23:59:59' else TDM.FollowDate end As FollowDate,
                                        --case when TDM.FollowTime is NULL then '1001/01/01' else TDM.FollowTime end As FollowTime,
                                        --TDM.FollowDate,TDM.FollowTime,
                                        T.CreatedBy,T.CreatedDate,ISNULL((select top 1 Note From TaskDetailMaster WHERE TaskId=T.TaskId ORDER BY TaskDetailId DESC),' ') As Note ,
	                                    (select top 1 case when FollowDate is NULL then '23:59:59' else FollowDate end  From TaskDetailMaster WHERE TaskId=T.TaskId ORDER BY TaskDetailId DESC) As NextFollowDate,
	                                    (select top 1 case when FollowTime is NULL then '1001/01/01' else FollowTime end From TaskDetailMaster WHERE TaskId=T.TaskId ORDER BY TaskDetailId DESC) As NextFollowTime,(SELECT TOP 1 * From
	                                    (select TOP 2 case when FollowDate is NULL then '23:59:59' else FollowDate end AS FollowDate  
	                                    From TaskDetailMaster WHERE TaskId=640 ORDER BY TaskDetailId DESC) x order by FollowDate)as lastFollowdate,
	                                    (SELECT TOP 1 * From
	                                    (select  TOP 2 case when FollowTime is NULL then '1001/01/01' else FollowTime end AS FollowTime  
	                                    From TaskDetailMaster WHERE TaskId=640 ORDER BY TaskDetailId DESC ) x order by FollowTime)as lastFollowTime";
                this.ColumnsName += @", (select top 1 TS1.TaskStatus From TaskDetailMaster T1 LEFT JOIN TaskStatusMaster AS TS1 ON T1.TaskStatus = TS1.StatusId 
                                        WHERE T1.TaskId = T.TaskId ORDER BY T1.TaskDetailId DESC) AS FollowUpTaskStatus";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "lastFollowdate";
                this.SortOrder = "DESC ";
                this.TableName = @" TaskMaster AS T 
                                    LEFT JOIN gurjari_crmuser.TaskGroupMaster AS TG ON T.TaskGroupId = TG.TaskGroupId 
                                    LEFT JOIN gurjari_crmuser.TaskPriorityMaster AS TP ON T.Priority = TP.PriorityId  
                                    LEFT JOIN gurjari_crmuser.TaskStatusMaster AS TS ON T.Status = TS.StatusId 
                                    LEFT JOIN gurjari_crmuser.TaskTypeMaster AS TTM ON T.TaskTypeId = TTM.TaskTypeId ";
                if (sessionUtils.UserRollType == 1)
                {
                    this.TableName += @" LEFT JOIN gurjari_crmuser.TaskDetailMaster as TDM  on TDM.TaskId = T.TaskId  AND TDM.ToId = T.CreatedBy ";
                }
                else if (sessionUtils.UserRollType > 1)
                {
                    this.TableName += @" LEFT JOIN gurjari_crmuser.TaskDetailMaster as TDM  on TDM.TaskId = T.TaskId  AND TDM.IsActive <> 0";
                }
                this.TableName += @" LEFT JOIN gurjari_crmuser.UserMaster as FromUM  on FromUM.UserId = TDM.FromId 
                                    LEFT JOIN gurjari_crmuser.UserMaster as ToUM  on ToUM.UserId = TDM.ToId";
                this.WhereClause = " ISNULL(IsActive,0)=1 AND CONVERT(VARCHAR, CreatedDate, 112) = CONVERT(VARCHAR, GETDATE(), 112)";
                if (sessionUtils.UserRollType > 1)
                {
                    this.WhereClause += " AND AssignToId=" + sessionUtils.UserId + "";
                }
                else if (sessionUtils.UserRollType == 1)
                {
                    this.WhereClause += " AND TaskStatus != 'Completed'";
                }
                this.ExportedFileName = "Task List";
                this.ExportedColumns = @" Distinct
                                        T.TaskId[Hidden],T.TaskNo,
                                        FromUM.Name as AssignFrom,T.Task,TP.PriorityName[Hidden],T.Review[Hidden],TS.TaskStatus,
                                        T.TaskTypeId[Hidden],TTM.TaskType[Hidden],T.GroupBy[Hidden],T.IsActive[Hidden],T.TaskGroupId[Hidden],TG.TaskGroupName,TDM.FromId[Hidden],FromUM.Name as FromUser,
                                        TDM.ToId As AssignToId,ToUM.UserName As AssignTo,TDM.FollowDate[Hidden],
                                        case when TDM.FollowDate is NULL then '' else CONVERT(VARCHAR(20),TDM.FollowDate,105) end +' '+
                                        case when TDM.FollowTime is NULL then '' else CONVERT(varchar(15),TDM.FollowTime,108) end As [Follow DateTime],
                                        TDM.Note,T.CreatedBy[Hidden],T.CreatedDate";
                this.ExportedColumns += @", (select top 1 TS1.TaskStatus From TaskDetailMaster T1 LEFT JOIN TaskStatusMaster AS TS1 ON T1.TaskStatus = TS1.StatusId 
                                        WHERE T1.TaskId = T.TaskId ORDER BY T1.TaskDetailId DESC) AS FollowUpTaskStatus";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<TaskMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "PerformaInvoiceMaster")
            {
                this.ColumnsName = @"PO.PerformaInvId,PO.PerformaInvNo,PO.RptFormatId,RM.CompanyCode As RptCompany,PO.PerformaInvDate,
                                    PO.DeliveryTermId,DT.DeliveryName AS DeliveryTerm,PO.PaymentTermId,PM.TermName As PaymentTerm,PO.ShippingMarks,
                                    PO.ModeOfShipmentId,SP.ModeOfShipment,PO.ConsigneId,BB.CompanyName As Contact,PO.ContactTel,PO.Contactmail,
                                    PO.ConsigneName,PO.ConsigneAddress,PO.ConsigneEmail,PO.ConsigneTel,PO.ConsigneTax,PO.ContactName,
                                    PO.LoadingPortId,PR.PortName As LoadingPort,PO.DischargePortId,PR1.PortName As DischargePort,
                                    PO.BankNameId,BM.BankName,PO.AccountTypeId,AT.AccountType,PO.BeneficiaryName,PO.BranchName,
                                    PO.BankAddress,PO.AccountNo,PO.IFSCCode,PO.SwiftCode,PO.IsActive,PO.CreatedBy";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "PerformaInvId";
                this.SortOrder = "desc";
                this.TableName = @"PerformaInvoiceMaster As PO With(NOLOCK)
                                    Inner join ReportFOrmatMaster As RM With(NOLOCK)On RM.RotFormatId = PO.RptFormatId
                                    Inner join DeliveryTermsMaster As DT With(NOLOCK) On DT.TermsId = PO.DeliveryTermId
                                    Inner join PaymentTermsMaster As PM With(NOLOCK) On PM.PaymentTermId = PO.PaymentTermId
                                    Inner join ShipmentMaster As SP With(NOLOCK) On SP.ShipmentId = PO.ModeOfShipmentId
                                    Inner join BuyerMaster As BB With(NOLOCK) On BB.BuyerId = PO.ConsigneId
                                    Inner join PortMaster As PR With(NOLOCK) On PR.PortId = PO.LoadingPortId
                                    Inner join PortMaster As PR1 With(NOLOCK) On PR1.PortId = PO.DischargePortId
                                    Inner join BankNameMaster  As BM With(NOLOCK) On BM.BankId = PO.BankNameId
                                    Inner join AccountTypeMaster  As AT With(NOLOCK) On AT.AccountTypeId = PO.AccountTypeId";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                if (sessionUtils.UserRollType > 1)
                {
                    this.WhereClause += " AND CreatedBy=" + sessionUtils.UserId + "";
                }
                this.ExportedFileName = "PerformaInvoice List";
                this.ExportedColumns = @"PO.PerformaInvId[Hidden],PO.PerformaInvNo,PO.RptFormatId[Hidden],RM.CompanyCode As RptCompany,PO.PerformaInvDate,
                                        PO.DeliveryTermId[Hidden],DT.DeliveryName AS DeliveryTerm,PO.PaymentTermId[Hidden],PM.TermName As PaymentTerm,PO.ShippingMarks,
                                        PO.ModeOfShipmentId[Hidden],SP.ModeOfShipment,PO.ConsigneId[Hidden],BB.CompanyName As Contact,PO.ContactTel,PO.Contactmail,
                                        PO.ConsigneName,PO.ConsigneAddress,PO.ConsigneEmail,PO.ConsigneTel,PO.ConsigneTax,PO.ContactName,
                                        PO.LoadingPortId[Hidden],PR.PortName As LoadingPort,PO.DischargePortId[Hidden],PR1.PortName As DischargePort,
                                        PO.BankNameId[Hidden],BM.BankName,PO.AccountTypeId[Hidden],AT.AccountType,PO.BeneficiaryName,PO.BranchName,
                                        PO.BankAddress,PO.AccountNo,PO.IFSCCode,PO.SwiftCode,PO.IsActive[Hidden],PO.CreatedBy[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<PerformaInvoice>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "PurchaseOrderMaster")
            {
                this.ColumnsName = @"PoId,PoNo,PoRefNo,PoDate,P.Remark,P.TermsConditionId,T.DeliveryName As TermsCondition,TotalAmount,
                                    TotalTax,PayableAmount,P.ModeOfShipmentId,S.ModeOfShipment,PriceCode ,
                                    P.LandingPort,LP.PortName As LandingPortName, P.DischargePort,DP.PortName As DischargePortName, P.SupplierId,SP.CompanyName As SupplierName, Address, Tel, P.Fax, P.Email, Website, Attn, AttnMobile,P.SupplierTax, 
                                    AttnEmail, P.ComId,C.ComName, P.BuyerId,B.CompanyName As BuyerName, BuyerAddress, BuyerTel, BuyerFax, BuyerWebsite, BuyerEmail, BuyerAttn, BuyerAttnMobile,  BuyerTax,
                                    BuyerAttnEmail,P.IsActive,P.CreatedBy,U.Name As UserName,P.CreatedDate";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "PoId";
                this.SortOrder = "desc";
                this.TableName = @"PurchaseOrderMaster P 
                                    Left Join BuyerMaster B On P.BuyerId = B.BuyerId
                                    Left Join BuyerMaster SP On P.SupplierId = SP.BuyerId
                                    Left Join CompanyMaster C On P.ComId = C.ComId
                                    Left Join DeliveryTermsMaster T On P.TermsConditionId = T.TermsId
                                    Left Join ShipmentMaster S On P.ModeOfShipmentId = S.ShipmentId
                                    Left Join PortMaster LP On P.LandingPort = LP.PortId
                                    Left Join PortMaster DP On P.DischargePort = DP.PortId
                                    Left Join UserMaster U On P.CreatedBy = U.UserId";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                if (sessionUtils.UserRollType > 1)
                {
                    this.WhereClause += " AND CreatedBy=" + sessionUtils.UserId + "";
                }
                this.ExportedFileName = "Purchase Order List";
                this.ExportedColumns = @"PO.PoId[Hidden],PO.PoNo,PO.PoRefNo,CONVERT(VARCHAR(20),PO.PoDate,105)PoDate[Hidden],PO.SupplierId[Hidden],
                                            SM.CompanyName,PO.Remark[Hidden],PO.TermsConditionId[Hidden],TC.Description[Hidden],PO.TotalAmount[Hidden],PO.TotalTax[Hidden],
                                            PO.PayableAmount[Hidden],PO.ModeOfShipmentId[Hidden],SP.ModeOfShipment[Hidden], PO.PriceCode[Hidden],CM.CurrencyName[Hidden],
                                            PO.IsActive[Hidden],PO.CreatedBy[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<PurchaseOrder>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "HolidayMaster")
            {
                this.ColumnsName = @" HM.HolidayId,HM.HolidayNameId,HNM.HolidayName,HM.OnDate,CM.CountryName,
                                        CM.CountryId,HM.IsActive,HM.StateIds,HM.ReligionIds, HM.CreatedBy ,
                                            (SELECT STUFF((SELECT ', ' + StateName                      
                                            FROM stateMaster where ISNULL(IsActive,0)=1 AND Stateid in (SELECT * FROM gurjari_crmuser.SplitString(HM.StateIds,'|'))  
                                            FOR XML PATH(''),type).value('.','nvarchar(max)'),1,1,'')) AS StateName,
                                                (SELECT STUFF((SELECT ', ' + ReligionName                      
                                                FROM ReligionMaster where ReligionId in (SELECT * FROM gurjari_crmuser.SplitString(HM.ReligionIds,'|'))  
                                                FOR XML PATH(''),type).value('.','nvarchar(max)'),1,1,'')) AS ReligionName";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "HolidayId";
                this.SortOrder = "desc";
                this.TableName = "HolidayMaster as HM inner join HolidayNameMaster as HNM on HM.HolidayNameId =HNM.HolidayId inner join CountryMaster as CM on HM.CountryId =CM.CountryId";
                this.WhereClause = "ISNULL(IsActive,0)=1";

                this.ExportedFileName = "Holiday List";
                this.ExportedColumns = @"HM.HolidayId[Hidden],CM.CountryName,CONVERT(VARCHAR(20),HM.OnDate,105)[Hidden],
                                        HM.IsActive[Hidden],HM.StateIds[Hidden],HM.ReligionIds[Hidden],HM.CreatedBy[Hidden],
                                                (SELECT STUFF((SELECT ', ' + StateName
                                                FROM stateMaster where ISNULL(IsActive,0)=1 AND Stateid in (SELECT * FROM gurjari_crmuser.SplitString(HM.StateIds, '|'))
                                                FOR XML PATH(''), type).value('.', 'nvarchar(max)'),1,1,'')) AS StateName,
                                                        HNM.HolidayName,
                                                        (SELECT STUFF((SELECT ', ' + ReligionName                      
                                                        FROM ReligionMaster where ReligionId in (SELECT * FROM gurjari_crmuser.SplitString(HM.ReligionIds,'|'))  
                                                        FOR XML PATH(''),type).value('.','nvarchar(max)'),1,1,'')) AS ReligionName";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<HolidayMaster>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "LicenseMaster")
            {
                this.ColumnsName = "LicenseId,LicenseName,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "LicenseId";
                this.SortOrder = "desc";
                this.TableName = "LicenseMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "License List ";
                this.ExportedColumns = "LicenseId[Hidden],LicenseName,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<LicenseMaster>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "LeaveMaster")
            {
                this.ColumnsName = "LeaveId,UserId,FromDate,ToDate,IsHalf,TotalDays,Reason,Status,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "LeaveId";
                this.SortOrder = "desc";
                this.TableName = "LeaveMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "Leave List";
                this.ExportedColumns = "LeaveId[Hidden],UserId[Hidden],CONVERT(VARCHAR(20),FromDate,105)FromDate,CONVERT(VARCHAR(20),ToDate,105)ToDate,IsHalf[Hidden],TotalDays,Reason,Status[Hidden],IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<LeaveMaster>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "EmailSignature")
            {
                this.ColumnsName = "S.SignatureId,S.UserId,S.Title,D.DepartmentName,S.Signature,U.Name,U.UserName,S.IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "SignatureId";
                this.SortOrder = "desc";
                this.TableName = "SignatureMaster S inner join UserMaster U on S.UserId =U.UserId INNER JOIN DepartmentMaster D ON D.DepartmentId=U.DepartmentId";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "Signature List";
                this.ExportedColumns = "S.SignatureId[Hidden],S.UserId[Hidden],S.Title,D.DepartmentName,U.Name,S.Signature,S.IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<EmailSignature>();
                else
                {
                    oGrid.Export(obj);
                }
            }



            else if (obj.Mode == "EmailSpeech")
            {
                this.ColumnsName = "SpeechId,Title,Subject,Description,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "SpeechId";
                this.SortOrder = "desc";
                this.TableName = "EmailSpeechMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "Email Speech List";
                this.ExportedColumns = "SpeechId[Hidden],Title,Subject,Description,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<EmailSpeech>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "ReligionMaster")
            {
                this.ColumnsName = "ReligionId,ReligionName,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "ReligionId";
                this.SortOrder = "desc";
                this.TableName = "ReligionMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "Religion List";
                this.ExportedColumns = "ReligionId[Hidden],ReligionName,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<Religion>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "SMSSpeechMaster")
            {
                this.ColumnsName = "SM.SMSId,DM.DepartmentName,SM.SMSTitle,SM.SMS,SM.IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "SMSId";
                this.SortOrder = "desc";
                this.TableName = "SMSSpeechMaster As SM inner join departmentmaster as DM on DM.DepartmentId=SM.DepartmentId";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "SMS Speech List";
                this.ExportedColumns = "SMSId[Hidden],SMSTitle,SMS,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<SMSSpeech>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "ShapeMaster")
            {
                this.ColumnsName = "ShapeId,ShapeName,Description,IsActive,Photo";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "ShapeId";
                this.SortOrder = "desc";
                this.TableName = "ShapeMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "Shape List";
                this.ExportedColumns = "ShapeId[Hidden],ShapeName,Description,IsActive[Hidden],Photo[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<ShapeInfo>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "PlugShapeMaster")
            {
                this.ColumnsName = " PlugShapeId,Title,Description,Photo,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "PlugShapeId";
                this.SortOrder = "desc";
                this.TableName = "plugshapeMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "Plug Shape List";
                this.ExportedColumns = "PlugShapeId[Hidden],Title,Description,IsActive[Hidden],Photo[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<PlugShapeInfo>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "InwardCourierMaster")
            {
                //this.ColumnsName = @"datename(m,IC.CourierDate)+'-'+cast(datepart(yyyy,IC.CourierDate) as varchar) as MonthYear,
                //                    IC.CourierId,IC.CourierDate,IC.CourierTime,IC.SenderId,IC.ReceivedBy,UM.UserName As Receive,
                //                    Case  IC.SenderType WHEN 'v'  THEN VM.CompanyName WHEN 'S' THEN  SM.CompanyName WHEN 'B' 
                //                    THEN  BM.CompanyName ELSE null END as Sender,IC.CourierReffNo,CSM.CourierType,
                //                    IC.VendorId,VMM.CompanyName as Vendor,IC.ShipmentRemark,IC.SenderType,IC.IsActive,IC.CreatedBy";
                this.ColumnsName = @"datename(m,IC.CourierDate)+'-'+cast(datepart(yyyy,IC.CourierDate) as varchar) as MonthYear,
                                    IC.CourierId,IC.CourierDate,IC.CourierTime,IC.SenderId,IC.ReceivedBy,UM.UserName As Receive, 
                                    BM.CompanyName as Sender, IC.CourierReffNo,CSM.CourierType, IC.VendorId,VMM.CompanyName as Vendor,
                                    IC.ShipmentRemark,IC.SenderType,IC.IsActive,IC.CreatedBy";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "CourierDate";
                this.SortOrder = "desc";
                //this.TableName = "InwardCourierMaster IC Inner join UserMaster UM on IC.ReceivedBy = UM.UserId Inner join VendorMaster VMM on VMM.VendorId= IC.VendorId left join SupplierMaster SM on SM.SupplierId = IC.SenderId AND IC.SenderType='S' left join VendorMaster VM on VM.VendorId = IC.SenderId AND IC.SenderType='V' left join BuyerMaster BM on BM.BuyerId = IC.SenderId AND IC.SenderType='B' left join CourierTypeMaster CSM with(nolock) on CSM.CourierTypeId = IC.CourierTypeId";
                this.TableName = "InwardCourierMaster IC Inner join UserMaster UM on IC.ReceivedBy = UM.UserId Inner join VendorMaster VMM on VMM.VendorId = IC.VendorId Inner join BuyerMaster BM on BM.BuyerId = IC.SenderId left join CourierTypeMaster CSM with(nolock) on CSM.CourierTypeId = IC.CourierTypeId";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                if (sessionUtils.UserRollType > 1)
                {
                    this.WhereClause += " AND CreatedBy=" + sessionUtils.UserId + "";
                }
                this.ExportedFileName = "Inward Courier List";
                //this.ExportedColumns = @"datename(m,IC.CourierDate)+'-'+cast(datepart(yyyy,IC.CourierDate) as varchar) as MonthYear,
                //                        IC.CourierId[Hidden],CONVERT(VARCHAR(20),IC.CourierDate,105)CourierDate,IC.SenderId[Hidden],
                //                        Case  IC.SenderType WHEN 'v'  THEN VM.CompanyName WHEN 'S' THEN  SM.CompanyName WHEN 'B' 
                //                        THEN  BM.CompanyName ELSE null END as Sender,IC.VendorId[Hidden],VMM.CompanyName as [Vendor],
                //                        IC.CourierReffNo[Courier Ref. No],IC.CourierReffNo[Hidden],CSM.CourierType,IC.ShipmentRemark,IC.SenderType[Hidden],IC.IsActive[Hidden],IC.CreatedBy[Hidden]";
                this.ExportedColumns = @"datename(m,IC.CourierDate)+'-'+cast(datepart(yyyy,IC.CourierDate) as varchar) as MonthYear,
                                        IC.CourierId[Hidden],CONVERT(VARCHAR(20),IC.CourierDate,105)CourierDate,IC.SenderId[Hidden],
                                        BM.CompanyName as Sender,IC.VendorId[Hidden],VMM.CompanyName as [Vendor],
                                        IC.CourierReffNo[Courier Ref. No],IC.CourierReffNo[Hidden],CSM.CourierType,IC.ShipmentRemark,IC.SenderType[Hidden],IC.IsActive[Hidden],IC.CreatedBy[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<InwardCourierInfo>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "ShippingMarkMaster")
            {
                this.ColumnsName = "S.ShipmentMarkId,S.BuyerName,S.ShipperName,S.ConsigneeName,S.POL,S.POD,S.IsActive,P.PortName As POLName,PP.PortName As PODName";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "ShipmentMarkId";
                this.SortOrder = "desc";
                this.TableName = " ShipmentMarkMaster As S Left join Portmaster P On P.PortId = S.POL Left join Portmaster PP On PP.PortId = S.POD";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "ShippingMark List";
                this.ExportedColumns = "S.ShipmentMarkId[Hidden],S.BuyerName,S.ShipperName,S.ConsigneeName,S.POL[Hidden],P.PortName As POLName,S.POD[Hidden],PP.PortName As PODName,S.IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<ShippingMarkInfo>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "TaxMaster")
            {
                this.ColumnsName = "TaxId,TaxName,Percentage,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "TaxId";
                this.SortOrder = "desc";
                this.TableName = "TaxMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";//ISNULL(IsActive,0)=1
                this.ExportedFileName = "TaxList";
                this.ExportedColumns = "TaxId[Hidden],TaxName,Percentage,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<TaxInfo>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "CourierClients")
            {
                this.ColumnsName = "*";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "CompanyName";
                this.SortOrder = "desc";
                this.TableName = "(select CompanyName,  CONVERT(nvarchar, BuyerId) + '|B' As ID from buyermaster WHERE IsActive = 1 union all SELECT CompanyName,  CONVERT(nvarchar, SupplierId) + '|S' As ID from suppliermaster WHERE IsActive = 1 UNION ALL Select CompanyName, CONVERT(nvarchar, VendorId) + '|V' As ID from vendormaster WHERE IsActive = 1)T";
                this.WhereClause = "";
                this.ExportedFileName = "";
                this.ExportedColumns = "";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<SenderInformation>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "OutwardCourierMaster")
            {
                //this.ColumnsName = @"datename(m,OCM.CourierDate)+'-'+cast(datepart(yyyy,OCM.CourierDate) as varchar) as MonthYear,
                //                    OCM.CourierId,OCM.CourierDate,OCM.CourierTime,OCM.SenderBy,UM.UserName As Sender,
                //                    OCM.VendorId,VM.CompanyName as Vendor,OCM.ReceiverId,Case OCM.ReceiverType WHEN 'v'  
                //                    THEN VMM.CompanyName WHEN 'S' THEN SM.CompanyName WHEN 'B' 
                //                    THEN BM.CompanyName ELSE null END as Receiver,OCM.ReceiverType,OCM.CourierReffNo,CSM.CourierType,
                //                    OCM.ShipmentRefNo, OCM.Amount,OCM.PaymentBy, OCM.Remark, OCM.IsActive,OCM.CreatedBy";
                this.ColumnsName = @"datename(m,OCM.CourierDate)+'-'+cast(datepart(yyyy,OCM.CourierDate) as varchar) as MonthYear,
                                     OCM.CourierId,OCM.CourierDate,OCM.CourierTime,OCM.SenderBy,UM.UserName As Sender,
                                     OCM.VendorId,VM.CompanyName as Vendor,OCM.ReceiverId,BM.CompanyName as Receiver,OCM.ReceiverType,
	                                 OCM.CourierReffNo,CSM.CourierType,
                                     OCM.ShipmentRefNo, OCM.Amount,OCM.PaymentBy, OCM.Remark, OCM.IsActive,OCM.CreatedBy";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "CourierDate";
                this.SortOrder = "desc";
                this.TableName = "OutwardCourierMaster as OCM left join UserMaster UM on OCM.SenderBy = UM.UserId Inner join VendorMaster as VM on VM.VendorId = OCM.VendorId Inner join BuyerMaster BM on BM.BuyerId = OCM.ReceiverId left join CourierTypeMaster CSM with(nolock) on CSM.CourierTypeId = OCM.CourierTypeId";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                if (sessionUtils.UserRollType > 1)
                {
                    this.WhereClause += " AND CreatedBy=" + sessionUtils.UserId + "";
                }
                this.ExportedFileName = "Outward List";
                //this.ExportedColumns = @"datename(m,OCM.CourierDate)+'-'+cast(datepart(yyyy,OCM.CourierDate) as varchar) as MonthYear,
                //                        OCM.CourierId[Hidden],CONVERT(VARCHAR(20),OCM.CourierDate,103) AS CourierDate,OCM.VendorId[Hidden],
                //                        Case OCM.ReceiverType WHEN 'v'  THEN VMM.CompanyName WHEN 'S' THEN SM.CompanyName WHEN 'B' 
                //                        THEN BM.CompanyName ELSE null END as Receiver,VM.CompanyName as Vendor,OCM.ReceiverId[Hidden],
                //                        OCM.ReceiverType[Hidden], OCM.ShipmentRefNo, OCM.Amount[Hidden],OCM.PaymentBy[Hidden], OCM.Remark, OCM.IsActive[Hidden],
                //                        OCM.CreatedBy[Hidden],OCM.CourierReffNo[Courier Ref. No],OCM.CourierReffNo[Hidden],CSM.CourierType";
                this.ExportedColumns = @"datename(m,OCM.CourierDate)+'-'+cast(datepart(yyyy,OCM.CourierDate) as varchar) as MonthYear,
                                     OCM.CourierId[Hidden],CONVERT(VARCHAR(20),OCM.CourierDate,103) AS CourierDate,OCM.CourierTime,OCM.SenderBy,UM.UserName As Sender,
                                     OCM.VendorId[Hidden],VM.CompanyName as Vendor,OCM.ReceiverId[Hidden],BM.CompanyName as Receiver,OCM.ReceiverType[Hidden],
	                                 OCM.CourierReffNo[Courier Ref. No],CSM.CourierType,OCM.ShipmentRefNo, OCM.Amount[Hidden],OCM.PaymentBy[Hidden], OCM.Remark, OCM.IsActive[Hidden],OCM.CreatedBy[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<OutwardCourierInfo>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "ReceiptVoucherMaster")
            {
                this.ColumnsName = "R.VoucherId,R.BuyerId,R.[Type],R.Amount,R.Naration,R.VoucherDate,B.companyname[BuyerName],R.IsActive,R.CreatedBy";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "VoucherId";
                this.SortOrder = "desc";
                this.TableName = "ReceiptVoucherMaster R left join BuyerMaster B on R.BuyerId = B.BuyerId and B.IsActive = 'True'";
                this.WhereClause = "ISNULL(IsActive,0)=1";

                this.ExportedFileName = "Receipt Voucher List";
                this.ExportedColumns = "VoucherId[Hidden],BuyerId[Hidden],Amount,Naration,Name,CONVERT(VARCHAR(20),VoucherDate,105)VoucherDate,IsActive[Hidden],R.CreatedBy[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<ReceiptVoucher>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "ExpenseTypeMaster")
            {
                this.ColumnsName = "ExTypeId,ExTypeName,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "ExTypeId";
                this.SortOrder = "desc";
                this.TableName = "ExpenseTypeMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";//ISNULL(IsActive,0)=1
                this.ExportedFileName = "ExpenseTypeList";
                this.ExportedColumns = "ExTypeId[Hidden],ExTypeName,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<ExpenseTypeInfo>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "BloodGroupMaster")
            {
                this.ColumnsName = "BloodGroupId,BloodGroup,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "BloodGroupId";
                this.SortOrder = "desc";
                this.TableName = "BloodGroupMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";//ISNULL(IsActive,0)=1
                this.ExportedFileName = "BloodGroupList";
                this.ExportedColumns = "BloodGroupId[Hidden],BloodGroup,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<BloodGroupInfo>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "TaskStatusMaster")
            {
                this.ColumnsName = "StatusId,TaskStatus,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "StatusId";
                this.SortOrder = "desc";
                this.TableName = "TaskStatusMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";//ISNULL(IsActive,0)=1
                this.ExportedFileName = "TaskStatusList";
                this.ExportedColumns = "StatusId[Hidden],TaskStatus,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<TaskStatusInfo>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "TaskPriorityMaster")
            {
                this.ColumnsName = "PriorityId,PriorityName,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "PriorityId";
                this.SortOrder = "desc";
                this.TableName = "TaskPriorityMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";//ISNULL(IsActive,0)=1
                this.ExportedFileName = "TaskPriorityList";
                this.ExportedColumns = "PriorityId[Hidden],PriorityName,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<TaskPriorityInfo>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "ReportingUserMaster")
            {
                this.ColumnsName = "UserId,Name,IsActive,ReportingId";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "UserId";
                this.SortOrder = "desc";
                this.TableName = "UserMaster";
                this.WhereClause = " ISNULL(IsActive,0)=1 and ReportingId=" + sessionUtils.UserId + "";//ISNULL(IsActive,0)=1
                this.ExportedFileName = "ReportingUserList";
                this.ExportedColumns = "UserId[Hidden],Name,IsActive[Hidden],ReportingId[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<ReportingUser>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "RelationMaster")
            {
                this.ColumnsName = "RelationId,RelationName,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "RelationName";
                this.SortOrder = "desc";
                this.TableName = "RelationMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";//ISNULL(IsActive,0)=1
                this.ExportedFileName = "Relation List";
                this.ExportedColumns = "RelationId[Hidden],RelationName,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<Relation>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "AccountTypeMaster")
            {
                this.ColumnsName = "AccountTypeId,AccountType,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "AccountTypeId";
                this.SortOrder = "desc";
                this.TableName = "AccountTypeMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";//ISNULL(IsActive,0)=1
                this.ExportedFileName = "Account Type List";
                this.ExportedColumns = "AccountTypeId[Hidden],AccountType,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<AccountTypee>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "HolidayNameMaster")
            {
                this.ColumnsName = "HolidayId,HolidayName,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "HolidayId";
                this.SortOrder = "desc";
                this.TableName = "HolidayNameMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "Holiday Name List";
                this.ExportedColumns = "HolidayId[Hidden],HolidayName,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<HolidayNamee>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "EmployeeShiftMaster")
            {
                this.ColumnsName = "ShiftId,InTime,OutTime,Hours,IsActive,LateEntryCalculate,ShiftName";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "ShiftId";
                this.SortOrder = "desc";
                this.TableName = "EmployeeShitfMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "Employee Shift List";
                this.ExportedColumns = "ShiftId[Hidden],CONVERT(varchar(15),InTime,108)InTime,CONVERT(varchar(15),OutTime,108)OutTime,Hours,IsActive[Hidden],LateEntryCalculate,ShiftName";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<EmployeeShitf>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "PercentageMaster")
            {
                this.ColumnsName = "PercentageId, Percentage,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "Percentage";
                this.SortOrder = "asc";
                this.TableName = "PercentageMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "Percentage List";
                this.ExportedColumns = "PercentageId[Hidden],Percentage,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<Percentages>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "AdvertisementSourceMaster")
            {
                this.ColumnsName = "SiteId,SiteName,SiteUrl,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "SiteId";
                this.SortOrder = "desc";
                this.TableName = "AdvertisementSourceMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "Advertisement Source List";
                this.ExportedColumns = "SiteId[Hidden],SiteName,SiteUrl,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<AdvertisementSource>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "UploadProductDataMaster")
            {
                this.ColumnsName = @"DISTINCT PM.ProductId,PM.ProductName,PC.CatalogId,PC.SupplierId,BM.CompanyName As SupplierName,PM.ProductCode,PM.GplusLink,PM.FbLink,
                                    (SELECT STUFF((
                                        SELECT ', ' + Url
                                        FROM ProductLinkMaster Where ProductId = PM.ProductId And AdSourceId = 1 And IsActive = 1
                                        FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As VideoLink,
                                    (SELECT STUFF((
                                        SELECT ', ' + Url
                                        FROM ProductLinkMaster Where ProductId = PM.ProductId And AdSourceId <> 1 And IsActive = 1
                                        FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As SourceLink,AL.IsActive ";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "ProductId";
                this.SortOrder = "desc";
                this.TableName = "ProductMaster As PM Left join ProductCatalogMaster As PC On PC.ProductId = PM.ProductId  Left join ProductLinkMaster As AL On AL.ProductId = PM.ProductId Left join BuyerMaster As BM On BM.BuyerId = PC.SupplierId ";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "Upload Product Data List";
                this.ExportedColumns = @"DISTINCT PM.ProductId[Hidden],PM.ProductName,PC.CatalogId[Hidden],PC.SupplierId[Hidden],BM.CompanyName As SupplierName,PM.ProductCode,PM.GplusLink[Hidden],PM.FbLink[Hidden],
                                        (SELECT STUFF((
                                            SELECT ', ' + Url
                                            FROM ProductLinkMaster Where ProductId = PM.ProductId And AdSourceId = 1 And IsActive = 1
                                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As VideoLink,
                                        (SELECT STUFF((
                                            SELECT ', ' + Url
                                            FROM ProductLinkMaster Where ProductId = PM.ProductId And AdSourceId <> 1 And IsActive = 1
                                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As SourceLink,PM.IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<UploadProductDataMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "TaskTypeMaster")
            {
                this.ColumnsName = "TaskTypeId,TaskType,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "TaskTypeId";
                this.SortOrder = "desc";
                this.TableName = "TaskTypeMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "Task Type List";
                this.ExportedColumns = "TaskTypeId[Hidden],TaskType,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<TaskTypeMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "LoginHistory")
            {
                this.ColumnsName = "datename(m,lh.LoginTime)+'-'+cast(datepart(yyyy,lh.LoginTime) as varchar) as MonthYear,lh.Id,lh.UserId,um.UserName,lh.IP,lh.Browser,convert(date, lh.LoginTime)LoginTime,convert(time,lh.logintime) LogTime,case when lh.DeviceType = '1' then 'Mobile' else 'Desktop' end DeviceTypeName";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "Id";
                this.SortOrder = "desc";
                this.TableName = "LoginHistory lh inner join UserMaster um on lh.userId = um.UserId";
                this.WhereClause = "";
                if (sessionUtils.UserRollType > 1)
                {
                    this.WhereClause += " UserId=" + sessionUtils.UserId + "";
                }
                this.ExportedFileName = "Login History List";
                this.ExportedColumns = "lh.Id[Hidden],lh.UserId[Hidden],um.UserName,datename(m,lh.LoginTime)+'-'+cast(datepart(yyyy,lh.LoginTime) as varchar) as MonthYear,CONVERT(varchar(20),lh.logintime,105) LoginTime,CONVERT(VARCHAR(20),lh.logintime,108) LogTime,lh.Browser,lh.IP,case when lh.DeviceType = '1' then 'Mobile' else 'Desktop' end DeviceTypeName";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<LoginHistoryMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "AttendanceMaster")
            {
                this.ColumnsName = @"lh.AtId,lh.UserId,ISNULL(um.Name,'') +' '+ ISNULL(um.Surname,'') [UserName],datename(m,lh.OnDate)+'-'+cast(datepart(yyyy,lh.OnDate) as varchar) as Month,lh.OnDate,lh.WorkStartTime,lh.WorkEndTime,lh.LunchStartTime,lh.LunchEndTime
                                    ,lh.WorkStartIP,lh.WorkEndIP,lh.LunchStartIP,lh.LunchEndIP,convert(time,DATEADD(SECOND, - DATEDIFF(SECOND, lh.WorkEndTime, lh.WorkStartTime), '00:00:00')) [TotalWork]
                                    ,convert(time, DATEADD(SECOND, -DATEDIFF(SECOND, lh.LunchEndTime, lh.LunchStartTime), '00:00:00'))[TotalLunch]
                                    ,convert(time, (DATEADD(SECOND, -DATEDIFF(SECOND, lh.WorkEndTime, lh.WorkStartTime), '00:00:00') - DATEADD(SECOND, -DATEDIFF(SECOND, lh.LunchEndTime, lh.LunchStartTime), '00:00:00')))[TotalWorking]";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "OnDate";
                this.SortOrder = "desc";
                this.TableName = "AttendanceMaster lh inner join UserMaster um on lh.userId = um.UserId";
                this.WhereClause = "";
                if (sessionUtils.UserRollType > 1)
                {
                    this.WhereClause += " UserId=" + sessionUtils.UserId + "";
                }
                this.ExportedFileName = "AttendanceMaster List";
                this.ExportedColumns = @"lh.AtId[Hidden],lh.UserId[Hidden],um.Name +' '+ um.Surname [UserName],datename(m,lh.OnDate)+'-'+cast(datepart(yyyy,lh.OnDate) as varchar) as Month,CONVERT(varchar(20),lh.OnDate,105) [OnDate],CONVERT(VARCHAR(20),lh.WorkStartTime,108) [WorkStartTime],CONVERT(VARCHAR(20),lh.WorkEndTime,108) [WorkEndTime],CONVERT(VARCHAR(20),lh.LunchStartTime,108) [LunchStartTime]
                                        ,CONVERT(VARCHAR(20),lh.LunchEndTime,108) [LunchEndTime],lh.WorkStartIP[WorkStartIP],lh.WorkEndIP[WorkEndIP],lh.LunchStartIP[LunchStartIP],lh.LunchEndIP[LunchEndIP]
                                        ,convert(time,DATEADD(SECOND, - DATEDIFF(SECOND, lh.WorkEndTime, lh.WorkStartTime), '00:00:00')) [TotalWork]
                                        ,convert(time, DATEADD(SECOND, -DATEDIFF(SECOND, lh.LunchEndTime, lh.LunchStartTime), '00:00:00'))[TotalLunch]
                                        ,convert(time, (DATEADD(SECOND, -DATEDIFF(SECOND, lh.WorkEndTime, lh.WorkStartTime), '00:00:00') - DATEADD(SECOND, -DATEDIFF(SECOND, lh.LunchEndTime, lh.LunchStartTime), '00:00:00')))[TotalWorking]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<Attendance>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "ChatNameMaster")
            {
                this.ColumnsName = "ChatId,ChatName,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "ChatId";
                this.SortOrder = "desc";
                this.TableName = "ChatNameMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "ChatNameList";
                this.ExportedColumns = "ChatId[Hidden],ChatName,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<ChatNameMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "BankNameMaster")
            {
                this.ColumnsName = "BankId,BankName,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "BankId";
                this.SortOrder = "desc";
                this.TableName = "BankNameMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "BankNameList";
                this.ExportedColumns = "BankId[Hidden],BankName,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<BankNameMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "TOTypeMaster")
            {
                this.ColumnsName = "TOTypeId,TOType,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "TOTypeId";
                this.SortOrder = "desc";
                this.TableName = "TOTypeMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "TOTypeNameList";
                this.ExportedColumns = "TOTypeId[Hidden],TOType,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<TOTypeMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "LegerHeadMaster")
            {
                this.ColumnsName = "L.LegerHeadId,L.LegerHeadName,L.ITRId,I.ITRName,L.IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "LegerHeadId";
                this.SortOrder = "desc";
                this.TableName = "LegerHeadMaster as L join ITRMaster as I on L.ITRId = I.ITRId ";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "LegerHeadList";
                this.ExportedColumns = "L.LegerHeadId[Hidden],L.LegerHeadName,I.ITRName,L.IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<LegerHead>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "LegerMaster")
            {
                this.ColumnsName = "L.LegerId,L.LegerName,L.LegerHeadId,I.LegerHeadName,L.IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "LegerId";
                this.SortOrder = "desc";
                this.TableName = "LegerMaster as L join LegerHeadMaster as I on L.LegerHeadId = I.LegerHeadId ";
                if (obj.FixClause == null)
                    this.WhereClause = "ISNULL(IsActive,0)=1";
                else
                    this.WhereClause = "ISNULL(IsActive,1)=1 AND " + obj.FixClause;

                this.ExportedFileName = "LegerList";
                this.ExportedColumns = "L.LegerId[Hidden],L.LegerName,I.LegerHeadName,L.IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<Leger>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "AccountEntryMaster")
            {
                this.ColumnsName = @"A.AccountId,T.TaxName,A.TaxId,A.TaxValue,A.CurrencyId,A.LegerId,A.PartyName,A.Amount,A.INRAmount,A.ExchangeRate,
                                    A.BillDate,A.BillNo,A.TransactionSlip,A.Photo,A.BillPdf,A.Remark,A.IsActive,
                                    L.LegerName,C.CurrencyName,LH.LegerHeadName,LH.LegerHeadId,LH.ITRId,ITR.ITRName,A.AccountEntryType,case when A.AccountEntryType='1' then 'Assets' when A.AccountEntryType='2' then 'Expense' else '' end [Account_Entry_Type]";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "AccountId";
                this.SortOrder = "desc";
                this.TableName = @"AssetsExpenseMaster AS A 
                                    INNER JOIN LegerMaster AS L ON L.LegerId = A.LegerId 
                                    LEFT JOIN CurrencyMaster AS C ON C.CurrencyId = A.CurrencyId 
                                    inner join LegerHeadMaster as LH on LH.LegerHeadId = L.LegerHeadId 
                                    inner join ITRMaster as ITR on LH.ITRId = ITR.ITRId 
                                    LEFT JOIN TaxMaster AS T ON T.TaxId = A.TaxId";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "Account Entry List";
                this.ExportedColumns = @"A.AccountId[Hidden],T.TaxName[Hidden],case when A.AccountEntryType='1' then 'Assets' else 'Expense' end [Account_Entry_Type],L.LegerName AS [LegerName],A.PartyName AS [PartyName],Convert(nvarchar,A.BillDate,103) As BillDate,A.BillNo AS [BillNo],
                                        A.CurrencyId[Hidden],A.Amount[Hidden],A.INRAmount,
                                        A.ExchangeRate[Hidden],A.TransactionSlip[Hidden],A.Photo[Hidden],A.Remark[Hidden],
                                        A.IsActive[Hidden],C.CurrencyName[Hidden],LH.LegerHeadName[Hidden],LH.LegerHeadId[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<AccountEntry>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "TaskGroupMaster")
            {
                this.ColumnsName = "TaskGroupId,TaskGroupName,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "TaskGroupId";
                this.SortOrder = "desc";
                this.TableName = "TaskGroupMaster";
                this.WhereClause = "IsActive=1";
                this.ExportedFileName = "TaskGroup List";
                this.ExportedColumns = "TaskGroupId[Hidden],TaskGroupName,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<TaskGroup>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "InquiryBuyer")
            {
                this.ColumnsName = "BuyerName,InqNo,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "InqId";
                this.SortOrder = "desc";
                this.TableName = "InquiryMaster";
                this.WhereClause = "IsActive=1";
                this.ExportedFileName = "Buyer List";
                this.ExportedColumns = "InqNo[Hidden],BuyerName,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<Inquiry>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "TOMaster")
            {
                this.ColumnsName = "tom.TOId,tom.InqId,inq.InqNo,inq.BuyerName,inq.Email,inq.MobileNo,inq.Requirement,inq.Address,tom.IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "TOId";
                this.SortOrder = "desc";
                this.TableName = "TOMaster tom WITH(nolock) INNER JOIN gurjari_crmuser.InquiryMaster inq WITH(nolock) ON inq.InqId=tom.InqId";
                this.WhereClause = "IsActive=1";
                this.ExportedFileName = "Technical Order List";
                this.ExportedColumns = "tom.TOId[Hidden],tom.InqId[Hidden],inq.InqNo,tom.IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<TO>();
                else
                    oGrid.Export(obj);
            }

            // DailyWorkTask Grid Data
            else if (obj.Mode == "DailyWorkStartMaster" || obj.Mode == "DailyWorkEndMaster" || obj.Mode == "DailyLunchStartMaster" || obj.Mode == "DailyLunchEndMaster")
            {
                this.ColumnsName = @" Distinct
                                        T.TaskId,T.Task,T.Priority,TP.PriorityName,T.Review,T.Status,TS.TaskStatus,TS.TaskStatus,
                                        T.TaskTypeId,TTM.TaskType,T.GroupBy,T.IsActive,T.TaskGroupId,TG.TaskGroupName,TDM.FromId,
                                        FromUM.Name as FromUser,(SELECT STUFF((
                                        SELECT Distinct ', ' + InUM.Name
                                        FROM TaskDetailMaster As InTD
                                        INNER JOIN UserMaster As InUM  on InUM.UserId = InTD.ToId 
                                        Where InTD.TaskId = T.TaskId And InTD.ToId <> InTD.FromId 
                                        FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As AssignTo,
                                        case when TDM.FollowDate is NULL then '23:59:59' else TDM.FollowDate end As FollowDate,
                                        case when TDM.FollowTime is NULL then '1001/01/01' else TDM.FollowTime end As FollowTime,
                                        --TDM.FollowDate,TDM.FollowTime,
                                        TDM.Note,T.CreatedBy
                                        ,DW.Persontage";//,DW.DailyWorkId,DW.TaskType,DW.AttandanceType
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = " FollowDate";
                this.SortOrder = "DESC";
                this.TableName = @" TaskMaster AS T 
                                    LEFT JOIN gurjari_crmuser.TaskGroupMaster AS TG ON T.TaskGroupId = TG.TaskGroupId 
                                    LEFT JOIN gurjari_crmuser.TaskPriorityMaster AS TP ON T.Priority = TP.PriorityId  
                                    LEFT JOIN gurjari_crmuser.TaskStatusMaster AS TS ON T.Status = TS.StatusId 
                                    LEFT JOIN gurjari_crmuser.TaskTypeMaster AS TTM ON T.TaskTypeId = TTM.TaskTypeId 
                                    LEFT JOIN gurjari_crmuser.TaskDetailMaster as TDM  on TDM.TaskId = T.TaskId  AND TDM.ToId = T.CreatedBy
                                    LEFT JOIN gurjari_crmuser.UserMaster as FromUM  on FromUM.UserId = TDM.FromId 
                                    LEFT JOIN gurjari_crmuser.UserMaster as ToUM  on ToUM.UserId = TDM.ToId
                                    LEFT JOIN gurjari_crmuser.DailyWorkReport as DW  on DW.TaskInqId = T.TaskId";
                this.WhereClause = " ISNULL(IsActive,0)=1 ";
                if (sessionUtils.UserRollType > 1)
                {
                    if (obj.Mode == "DailyWorkStartMaster" || obj.Mode == "DailyLunchStartMaster")
                        this.WhereClause += " AND CreatedBy=" + sessionUtils.UserId + " AND TaskId NOT IN (Select  ISNULL(TaskInqId,0) from DailyWorkReport Where  UserId= " + sessionUtils.UserId + ")";
                    if (obj.Mode == "DailyWorkEndMaster")
                        this.WhereClause += " AND CreatedBy=" + sessionUtils.UserId + " AND TaskId IN (Select  ISNULL(TaskInqId,0) from DailyWorkReport Where  UserId= " + sessionUtils.UserId + " AND AttandanceType = 1 AND [Date] = Convert(Date,getdate()))";
                    if (obj.Mode == "DailyLunchEndMaster")
                        this.WhereClause += " AND CreatedBy=" + sessionUtils.UserId + " AND TaskId IN (Select  ISNULL(TaskInqId,0) from DailyWorkReport Where  UserId= " + sessionUtils.UserId + " AND AttandanceType = 3 AND [Date] = Convert(Date,getdate()))";
                }
                else if (sessionUtils.UserRollType == 1)
                {
                    this.WhereClause += " AND TaskStatus != 'Completed'";
                }
                this.ExportedFileName = "Task List";
                this.ExportedColumns = @" Distinct
                                        T.TaskId[Hidden],
                                        FromUM.Name as AssignFrom,T.Task as [Work/Task],TP.PriorityName[Hidden],T.Review[Hidden],TS.TaskStatus,
                                        T.TaskTypeId[Hidden],TTM.TaskType[Hidden],T.GroupBy[Hidden],T.IsActive[Hidden],T.TaskGroupId[Hidden],TG.TaskGroupName,TDM.FromId[Hidden],(SELECT STUFF((
                                        SELECT Distinct ', ' + InUM.Name
                                        FROM TaskDetailMaster As InTD
                                        INNER JOIN UserMaster As InUM  on InUM.UserId = InTD.ToId 
                                        Where InTD.TaskId = T.TaskId And InTD.ToId <> InTD.FromId 
                                        FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As AssignTo,TDM.FollowDate[Hidden],
                                        case when TDM.FollowDate is NULL then '' else CONVERT(VARCHAR(20),TDM.FollowDate,105) end +' '+
                                        case when TDM.FollowTime is NULL then '' else CONVERT(varchar(15),TDM.FollowTime,108) end As [Follow DateTime],
                                        TDM.Note,T.CreatedBy[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<TaskMaster>();

                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "WorkTaskMaster")
            {
                this.ColumnsName = @" dw.DailyWorkId,dw.Date,dw.UserId,dw.Remark,dw.Persontage,tm.Task,ur.Name+' '+ur.Surname [Name] 
                                     ,ts.TaskStatus";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "DailyWorkId";
                this.SortOrder = "desc";
                this.TableName = @" DailyWorkReport dw WITH(nolock) 
                                    LEFT JOIN UserMaster ur ON ur.UserId=dw.UserId
                                    LEFT JOIN TaskMaster tm ON	tm.TaskId=dw.TaskInqId and dw.TaskType=1
                                    LEFT JOIN InquiryMaster inq ON	inq.InqId=dw.TaskInqId and dw.TaskType=2
                                    LEFT JOIN TaskStatusMaster ts ON ts.StatusId=tm.Status
                                    LEFT JOIN AttendanceMaster atnd ON atnd.UserId = dw.UserId AND ISNULL(atnd.IsActive,0)=1
                                    AND Convert(nvarchar(10), OnDate, 112)= Convert(nvarchar(10),[Date], 112) AND 1 = 1";
                if (sessionUtils.UserRollType > 1)
                {
                    this.WhereClause = " UserId=" + sessionUtils.UserId + "";
                }
                else
                {
                    this.WhereClause = " 1=1";
                }
                this.ExportedFileName = "Daily Work List";
                this.ExportedColumns = @" dw.DailyWorkId[Hidden],dw.Date,dw.Remark,dw.Persontage,tm.Task,ur.Name+' '+ur.Surname [Name] 
                                          ,ts.TaskStatus";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<WorkTask>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "BuyerAddressDetail")
            {
                this.ColumnsName = "*";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "AddressId";
                this.SortOrder = "desc";
                this.TableName = "BuyerAddressDetail";
                this.WhereClause = "1=1";
                this.ExportedFileName = "Buyer Address List";
                this.ExportedColumns = "*";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<BuyerAddressDetail>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "VendorAddressDetail")
            {
                this.ColumnsName = "*";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "AddressId";
                this.SortOrder = "desc";
                this.TableName = "VendorAddressDetail";
                this.WhereClause = "1=1";
                this.ExportedFileName = "Vendor Address List";
                this.ExportedColumns = "*";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<VendorAddressDetail>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "SupplierAddressMaster")
            {
                this.ColumnsName = "*";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "AddressId";
                this.SortOrder = "desc";
                this.TableName = "SupplierAddressMaster";
                this.WhereClause = "1=1";
                this.ExportedFileName = "Supplier Address List";
                this.ExportedColumns = "*";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<SupplierAddressMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "WorkProfileMaster")
            {
                this.ColumnsName = "WorkProfileId,D.DepartmentId,DepartmentName,Title,Description,WorkTime,WorkDay,W.IsActive,WorkCycle,WorkDate";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "WorkProfileId";
                this.SortOrder = "desc";
                this.TableName = "WorkProfileMaster As W WITH(nolock) Inner Join DepartmentMaster As D WITH(nolock) On W.DepartmentId = D.DepartmentId";
                this.WhereClause = "IsActive=1";
                this.ExportedFileName = "Work Profile List";
                this.ExportedColumns = "WorkProfileId[Hidden],D.DepartmentId[Hidden],DepartmentName,W.Title,WorkDate,Description,WorkCycle,WorkTime,WorkDay,W.IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<WorkProfile>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "SalesPurchaseEntryMaster")
            {
                this.ColumnsName = @"spe.SalesPurchaseId,spe.FinicialYearId,fy.FinancialYear,spe.InvoiceNo,spe.InvoiceDate,spe.PartyType,
                                    case when spe.PartyType = '1' then 'Buyer' when spe.PartyType = '2' then 'Supplier' end [PartyTypeName],spe.PartyId,spe.PartyName,spe.IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "SalesPurchaseId";
                this.SortOrder = "desc";
                this.TableName = "SalesPurchaseEntryMaster As spe WITH(nolock) Inner Join FinancialYearMaster fy WITH(nolock) on spe.FinicialYearId=fy.FinancialYearId";
                this.WhereClause = "IsActive=1";
                this.ExportedFileName = "SalesPurchase Entry List";
                this.ExportedColumns = @"spe.SalesPurchaseId[Hidden],spe.FinicialYearId[Hidden],fy.FinancialYear,spe.InvoiceNo,spe.InvoiceDate,spe.PartyType[Hidden],
                                        case when spe.PartyType = '1'then 'Buyer' when spe.PartyType = '2' then 'Supplier' end[PartyTypeName],spe.PartyId[Hidden],spe.PartyName,spe.IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<SalesPurchaseEntry>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "IntervieweeCandidateMaster")
            {
                this.ColumnsName = @"IM.IntCandId,IM.ReferenceTypeId,IM.ReferenceId,IM.ReferenceSubType,IM.ReferenceMannualEntry,IM.Address,convert(date,IM.CommunicateDate)CommunicateDate
                                    ,IM.MaritalStatus,IM.CurrentCTC,IM.CurrentExpected,IM.NoticePeriod,IM.UploadResume,IM.Chat,IM.CandidateRefNo,IM.SourceId,sc.SourceName
                                    ,IM.Pincode,IM.FirstName +' '+ IM.SurName [Name],DATEDIFF(yy,CONVERT(DATETIME,IM.Birthdate),GETDATE()) AS Age
                                    ,IM.TotalWorkExperience As Experience,QM.QualificationName As Education,CM.cityname as Location
                                    ,case when IM.Gender=1 then 'Male' else 'Female' end[Gender],replace(IM.MobileNo,',',', ') As MobileNo,replace(IM.Email,',',', ') As Email
                                    ,IM.IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "IntCandId";
                this.SortOrder = "desc";
                this.TableName = "InterviweeCandidateMaster as IM WITH(nolock) Inner join citymaster as CM on CM.cityId=IM.cityId left join SourceMaster sc on sc.SourceId=IM.SourceId left join QualificationsMaster as QM on QM.QualificationId=IM.QualificationId ";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "New Candidate List";
                this.ExportedColumns = "IM.IntCandId[Hidden],IM.FirstName +' '+ IM.SurName [Name],DATEDIFF(yy,CONVERT(DATETIME,IM.Birthdate),GETDATE()) AS Age,IM.TotalWorkExperience As Experience,QM.QualificationName As Education,CM.cityname as Location,case when IM.Gender=1 then 'Male' else 'Female' end[Gender],replace(IM.MobileNo,',',', ') As MobileNo,replace(IM.Email,',',', ') As Email,IM.IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<IntervieweeCandidate>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "Status")
            {
                this.ColumnsName = @"Distinct case when ISNULL(IsActive,1)=1 then ' Active' else  'InActive' end [Name],IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "IsActive";
                this.SortOrder = "desc";
                this.TableName = "UserMaster";
                this.WhereClause = "1=1";
                this.ExportedFileName = "IsActive List";
                this.ExportedColumns = @"Distinct case when ISNULL(IsActive,1)=1 then ' Active' else  'InActive' end [Hidden],IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<UserMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "AcHolderMaster")
            {
                this.ColumnsName = "AcHolderCode,AcHolderName,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "AcHolderCode";
                this.SortOrder = "desc";
                this.TableName = "AcHolderMaster";
                this.WhereClause = "IsActive=1";
                this.ExportedFileName = "A/C Holder List";
                this.ExportedColumns = "AcHolderCode[Hidden],AcHolderName,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<AHolderCodeMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "DailyReportingMaster")
            {
                this.ColumnsName = @" D.DepartmentName,DW.DailyWorkId,DW.UserId,U.Name,DW.[Date],DW.TaskInqId,DW.Title,DW.Description,
                                        DW.StatusId,TS.TaskStatus,DW.Remark,DW.Persontage,DW.StartTime,DW.EndTime,
                                        Case When DW.TaskType > 1 Then  Case When DW.TaskType = 2 Then 'Inquiry' Else 'Other' end else 'Inquiry' end As  TaskType,
                                        convert(varchar(5),DateDiff(s, StartTime, EndTime)/3600)+':'+convert(varchar(5),DateDiff(s, StartTime, EndTime)%3600/60) as Totaltime ";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "DailyWorkId";
                this.SortOrder = "desc";
                this.TableName = @" DailyWorkReport As DW WITH(nolock) 
                                    Left Join UserMaster As U WITH(nolock)On DW.UserId = U.UserId 
                                    Left Join TaskStatusMaster As TS WITH(nolock) On TS.StatusId = DW.StatusId 
                                    Left Join DepartmentMaster As D WITH(nolock)On D.DepartmentId = U.UserId ";
                this.WhereClause = "";
                this.ExportedFileName = "Daily Reporting List";
                this.ExportedColumns = @" DW.DailyWorkId[Hidden],DW.UserId[Hidden],D.DepartmentName,U.Name,DW.[Date],DW.TaskInqId[Hidden],
                                        DW.Title,DW.Description,DW.StatusId[Hidden],TS.TaskStatus,DW.Remark,DW.Persontage,DW.StartTime[Hidden],
                                        DW.EndTime[Hidden],Case When DW.TaskType > 1 Then  Case When DW.TaskType = 2 Then 'Inquiry' Else 'Other' end else 'Inquiry' end As  TaskType,
                                        convert(varchar(5),DateDiff(s, StartTime, EndTime)/3600)+':'+convert(varchar(5),DateDiff(s, StartTime, EndTime)%3600/60) as Totaltime";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<DailyReporting>();
                else
                    oGrid.Export(obj);
            }
            if (obj.Mode == "QualificationsMaster")
            {
                this.ColumnsName = "QualificationId,QualificationName,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "QualificationId";
                this.SortOrder = "desc";
                this.TableName = "QualificationsMaster";
                this.WhereClause = "IsActive=1";
                this.ExportedFileName = "Qualifications List";
                this.ExportedColumns = "QualificationId[Hidden],QualificationName,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<Qualifications>();
                else
                    oGrid.Export(obj);
            }
            if (obj.Mode == "ContactTypeMaster")
            {
                this.ColumnsName = "ContactTypeId,ContactTypeName,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "ContactTypeId";
                this.SortOrder = "desc";
                this.TableName = "ContactTypeMaster";
                this.WhereClause = "IsActive=1";
                this.ExportedFileName = "ContactType List";
                this.ExportedColumns = "ContactTypeId[Hidden],ContactTypeName,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<ContactType>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "AddressTypeMaster")
            {
                this.ColumnsName = "AddressTypeId,AddressTypeName,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "AddressTypeId";
                this.SortOrder = "desc";
                this.TableName = "AddressTypeMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "AddressTypeMaster";
                this.ExportedColumns = "AddressTypeId[Hidden],AddressTypeName,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<AddressTypeMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "InquiryFollowupMaster")
            {
                this.ColumnsName = "FollowupId,InqId,CurrentUpdate,NextFollowDate,NextFollowTime,TaskStatus AS StatusName,Status,convert(date, A.CreatedDate)CurrentDate,convert(Time, A.CreatedDate)CurrentTime, A.CreatedBy, C.Name As UserName , AssignId, D.Name As AssignUserName ,A.IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "FollowupId";
                this.SortOrder = "desc";
                this.TableName = "InquiryFollowupMaster AS A LEFT JOIN TaskStatusMaster AS B ON A.Status = B.StatusId LEFT JOIN UserMaster AS C ON A.CreatedBy = C.UserId LEFT JOIN UserMaster AS D ON A.AssignId = D.UserId ";
                if (obj.FixClause == null)
                    this.WhereClause = " ";
                else
                    this.WhereClause = obj.FixClause;
                this.ExportedFileName = "Inquiry Followup List";
                this.ExportedColumns = "FollowupId[Hidden],A.CreatedBy[Hidden], C.Name As UserName ,InqId[Hidden],CurrentUpdate,NextFollowDate,NextFollowTime,TaskStatus AS StatusName,Status[Hidden],  AssignId[Hidden], D.Name As AssignUserName ,A.IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<InquiryFollowup>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "TaskFollowupMaster")
            {
                this.ColumnsName = "TaskDetailId,TaskId,A.Note,A.NextFollowDate,A.NextFollowTime,A.ActualDate,A.ActualTime,A.PlanDateTime,cast(A.PlanDateTime as time) As PlanTime,B.TaskStatus AS StatusName,A.TaskStatus As Status,A.FromId, C.Name As UserName , A.ToId, D.Name As AssignUserName ,A.IsActive,A.CreatedDate,cast(A.CreatedDate as time) As CreatedTime";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "TaskDetailId";
                this.SortOrder = "asc";
                this.TableName = "TaskDetailMaster AS A LEFT JOIN TaskStatusMaster AS B ON A.TaskStatus = B.StatusId LEFT JOIN UserMaster AS C ON A.FromId = C.UserId LEFT JOIN UserMaster AS D ON A.ToId = D.UserId";
                if (obj.FixClause == null)
                    this.WhereClause = " ";
                else
                    this.WhereClause = obj.FixClause;
                this.ExportedFileName = "Task Followup List";
                this.ExportedColumns = "TaskDetailId[Hidden],TaskId[Hidden],A.Note,A.NextFollowDate,A.NextFollowTime,A.ActualDate,A.ActualTime,A.PlanDateTime,cast(A.PlanDateTime as time) As PlanTime,B.TaskStatus AS StatusName,A.TaskStatus As Status[Hidden], A.FromId[Hidden], C.Name As UserName , A.ToId[Hidden], D.Name As AssignUserName ,A.IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<TaskFollowup>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "ShippingOrderMaster")
            {
                this.ColumnsName = "ShippingOrdId,TypeofShipment,Commodity,Nooftotal,NoofBL,CPBuyerName,CPBuyerAddress,CPBuyerTelephone,CPBuyerFax,CPBuyerContactPerson,EDBuyerName,EDBuyerAddress,EDBuyerTelephone,EDBuyerContactPerson,Freight,POL,POD,ProductDescription,ShippingMarksNumber,TotalNOPkgs,TotalGross,Measurement,Shipmentterms,CompanyName,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "ShippingOrdId";
                this.SortOrder = "desc";
                this.TableName = "ShippingOrderMaster";
                this.WhereClause = "IsActive=1 ";
                this.ExportedFileName = "Shipping Order List";
                this.ExportedColumns = "ShippingOrdId[Hidden],TypeofShipment,Commodity,Nooftotal,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<ShippingOrder>();
                else
                    oGrid.Export(obj);
            }

            else if (obj.Mode == "BillofLoadingMaster")
            {
                this.ColumnsName = "BLId,ShipperName,ShipperAddress,ConsigneeName,ConsigneeAddress,Freight,POL,POD,ProductDescription,ShippingMarksNumber,TotalNOPkgs,GrossWeight,NetWeight,VolMeasurement,CompanyName,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "BLId";
                this.SortOrder = "desc";
                this.TableName = "BillofLoadingMaster";
                this.WhereClause = "IsActive=1 ";
                this.ExportedFileName = "Billof Loading List";
                this.ExportedColumns = "BLId[Hidden],ShipperName,ConsigneeName,Freight,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<BillofLoading>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "ContactDocumentNameMaster")
            {
                this.ColumnsName = "ContactDocId,ContactDocName,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "ContactDocId";
                this.SortOrder = "desc";
                this.TableName = "ContactDocumentNameMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "ContactDocumentNameList";
                this.ExportedColumns = "ContactDocId[Hidden],ContactDocName,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<ContactDocumentName>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "CourierTypeMaster")
            {
                this.ColumnsName = "CourierTypeId,CourierType,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "CourierTypeId";
                this.SortOrder = "desc";
                this.TableName = "CourierTypeMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "CourierTypeList";
                this.ExportedColumns = "CourierTypeId[Hidden],CourierType,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<CourierTypeName>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "PackingTypeMaster")
            {
                this.ColumnsName = "PackingTypeId,PackingType,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "PackingTypeId";
                this.SortOrder = "desc";
                this.TableName = "PackingTypeMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "PackingTypeList";
                this.ExportedColumns = "PackingTypeId[Hidden],PackingType,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<PackingTypeName>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "ProductList")
            {
                this.ColumnsName = "CatalogId,B.ProductId AS ProductId,ProductCode,ProductName,CompanyName,SupplierModelNo,B.OurCatalogImg as OurCatalogImg";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "CatalogId";
                this.SortOrder = "desc";
                this.TableName = @"ProductCatalogMaster AS A With(Nolock) 
                                INNER JOIN ProductMaster AS B WITH(NOLOCK)ON A.ProductId = B.ProductId
                                INNER JOIN BuyerMaster AS S WITH(NOLOCK)  ON S.BuyerID = A.SupplierId";
                this.WhereClause = "";
                this.ExportedFileName = "PackingTypeList";
                this.ExportedColumns = "CatalogId[Hidden],B.ProductId AS ProductId[Hidden],ProductCode,ProductName,CompanyName,SupplierModelNo,B.OurCatalogImg as OurCatalogImg[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<ProductList>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "EmpReffTypeMaster")
            {
                this.ColumnsName = "ReffTypeName,ReffTypeId,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "ReffTypeId";
                this.SortOrder = "desc";
                this.TableName = @"EmpReffTypeMaster";
                this.WhereClause = "";
                this.ExportedFileName = "Reference Type List";
                this.ExportedColumns = "ReffTypeName,ReffTypeId[Hidden],IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<EmpReffTypeMaster>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "ReferenceSourceMaster")
            {
                this.ColumnsName = "SourceId,SourceName,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "SourceId";
                this.SortOrder = "desc";
                this.TableName = @"ReferenceSourceMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "Reference Source Type List";
                this.ExportedColumns = "SourceId,SourceName[Hidden],IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<ReferenceSourceMasterGrid>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "DealerPrice")
            {
                this.ColumnsName = "ProductPriceId,(CurrencyName + ' '+ CAST(TotalAmount AS nvarchar)) AS DealerPrice,ProductId,SupplierId";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "ProductPriceId";
                this.SortOrder = "desc";
                this.TableName = @" ProductPrices AS A INNER JOIN CurrencyMaster AS B ON A.CurrencyId = B.CurrencyId Where " + obj.FixClause;
                this.WhereClause = "1=1";
                this.ExportedFileName = "Dealer Price List";
                this.ExportedColumns = "ProductPriceId[Hidden],(CurrencyName + ' '+ CAST(TotalAmount AS nvarchar)) AS DealerPrice,ProductId[Hidden],SupplierId[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<tblDealerPrice>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "TechnicalSpecHeadMaster")
            {
                this.ColumnsName = "TechHeadId,TechHead,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "TechHeadId";
                this.SortOrder = "desc";
                this.TableName = "TechnicalSpecHeadMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "TechnicalSpecHeadList";
                this.ExportedColumns = "TechHeadId[Hidden],TechHead,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<TechnicalSpecHead>();
                else
                    oGrid.Export(obj);
            }
            else if (obj.Mode == "TransactionTypeMaster")
            {
                this.ColumnsName = "TranTypeId,TranType,IsActive";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "TranTypeId";
                this.SortOrder = "desc";
                this.TableName = "TransactionTypeMaster";
                this.WhereClause = "ISNULL(IsActive,0)=1";
                this.ExportedFileName = "TransactionType";
                this.ExportedColumns = "TranTypeId[Hidden],TranType,IsActive[Hidden]";
                GridFunctions oGrid = new GridFunctions(this, obj);
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<TransactionTypeMaster>();
                else
                    oGrid.Export(obj);
            }
        }
    }
}
