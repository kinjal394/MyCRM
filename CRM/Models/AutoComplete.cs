using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CRM_Repository.DataServices;
using CRM_Repository;

namespace CRM.Models
{
    public class AutoComplete
    {
        public AutoComplete(string type, string keyword, string RelatedValue, Boolean IsPopup = false, string Type = "LIST")
        {

            this.Count = 10;
            List<ColumnInfo> lstColumns = new List<ColumnInfo>();
            this.Type = Type;
            if (type == "Test")
            {
                this.TableName = "CategoryMaster";
                this.DisplayColumnName = "CategoryName";
                this.Keyword = keyword;
                this.ValueColumnName = "CategoryId";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Category From List";
                    lstColumns.Add(new ColumnInfo() { title = "Category Name", field = "CategoryName", show = true, sortable = "CategoryName", filter = new Dictionary<string, string>() { { "CategoryName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "CategoryMaster")
            {
                this.TableName = "CategoryMaster";
                this.DisplayColumnName = "CategoryName";
                this.Keyword = keyword;
                this.ValueColumnName = "CategoryId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Category From List";
                    lstColumns.Add(new ColumnInfo() { title = "Category Name", field = "CategoryName", show = true, sortable = "CategoryName", filter = new Dictionary<string, string>() { { "CategoryName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "InterviweeCandidateMaster")
            {
                this.TableName = "InterviweeCandidateMaster";
                this.DisplayColumnName = "CandidateRefno";
                this.Keyword = keyword;
                this.ValueColumnName = "IntCandId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select InterviweeCandidate From List";
                    lstColumns.Add(new ColumnInfo() { title = "CandidateRef Number", field = "CandidateRefno", show = true, sortable = "CandidateRefno", filter = new Dictionary<string, string>() { { "CandidateRefno", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "InterviweeCandidate")
            {
                this.TableName = "InterviweeCandidateMaster";
                this.DisplayColumnName = "FirstName + ' ' + SurName";
                this.Keyword = keyword;
                this.ValueColumnName = "IntCandId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select InterviweeCandidate From List";
                    lstColumns.Add(new ColumnInfo() { title = "User Name", field = "FirstName + ' ' + SurName", show = true, sortable = "FirstName + ' ' + SurName", filter = new Dictionary<string, string>() { { "FirstName + ' ' + SurName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "LegerMaster")
            {
                this.TableName = "LegerMaster ";
                this.DisplayColumnName = "LegerName";
                this.Keyword = keyword;
                this.ValueColumnName = "LegerId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND LegerHeadId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Leger From List";
                    lstColumns.Add(new ColumnInfo() { title = "Leger Name", field = "LegerName", show = true, sortable = "LegerName", filter = new Dictionary<string, string>() { { "LegerName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "TaxMaster")
            {
                this.TableName = "TaxMaster";
                this.DisplayColumnName = "TaxName";
                this.Keyword = keyword;
                this.ValueColumnName = "TaxId";
                this.WhereCalue = "ISNULL(IsActive,1)=1";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Tax Name From List";
                    lstColumns.Add(new ColumnInfo() { title = "Tax Name", field = "TaxName", show = true, sortable = "TaxName", filter = new Dictionary<string, string>() { { "TaxName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "LegerHeadMaster")
            {
                this.TableName = "LegerHeadMaster ";
                this.DisplayColumnName = "LegerHeadName";
                this.Keyword = keyword;
                this.ValueColumnName = "LegerHeadId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND LegerHeadId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                this.PopupTitle = "Select Leger From List";
                lstColumns.Add(new ColumnInfo() { title = "LegerHead Name", field = "LegerHeadName", show = true, sortable = "LegerHeadName", filter = new Dictionary<string, string>() { { "LegerHeadName", "text" } } });
                this.PopupColumns = lstColumns;
            }
            else if (type == "AddressTypeMaster")
            {
                this.TableName = "AddressTypeMaster ";
                this.DisplayColumnName = "AddressTypeName";
                this.Keyword = keyword;
                this.ValueColumnName = "AddressTypeId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND AddressTypeId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                this.PopupTitle = "Select Leger From List";
                lstColumns.Add(new ColumnInfo() { title = "AddressTypeName", field = "AddressTypeName", show = true, sortable = "AddressTypeName", filter = new Dictionary<string, string>() { { "AddressTypeName", "text" } } });
                this.PopupColumns = lstColumns;
            }
            else if (type == "SubCategoryMaster")
            {
                this.TableName = "CategoryMaster as C Join SubCategoryMaster as S on  C.CategoryId = S.CategoryId ";
                this.DisplayColumnName = "SubCategoryName";
                this.Keyword = keyword;
                this.ValueColumnName = "SubCategoryId";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND C.CategoryId=" + RelatedValue;
                this.WhereCalue = " ISNULL(S.IsActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select SubCategory From List";
                    lstColumns.Add(new ColumnInfo() { title = "Category", field = "CategoryName", show = true, sortable = "CategoryName", filter = new Dictionary<string, string>() { { "CategoryName", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "Sub Category", field = "SubCategoryName", show = true, sortable = "SubCategoryName", filter = new Dictionary<string, string>() { { "SubCategoryName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "ProductMaster")
            {
                this.TableName = "ProductMaster as P Join SubCategoryMaster as S on  P.SubCategoryId = S.SubCategoryId ";
                this.DisplayColumnName = "ProductName";
                this.Keyword = keyword;
                this.ValueColumnName = "ProductId";
                this.WhereCalue = " ISNULL(P.IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND P.SubCategoryId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Main Product From List";
                    lstColumns.Add(new ColumnInfo() { title = "Category Name", field = "CategoryName", show = true, sortable = "CategoryName", filter = new Dictionary<string, string>() { { "CategoryName", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "Sub Category Name", field = "SubCategoryName", show = true, sortable = "SubCategoryName", filter = new Dictionary<string, string>() { { "SubCategoryName", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "Product Name", field = "ProductName", show = true, sortable = "ProductName", filter = new Dictionary<string, string>() { { "ProductName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "ProductSupllierDetail")
            {
                this.TableName = "ProductCatalogMaster as C  inner Join ProductMaster as P on  C.ProductId = P.ProductId inner Join BuyerMaster as B on  B.BuyerId = C.SupplierId";
                this.DisplayColumnName = "CompanyName";
                this.Keyword = keyword;
                this.ValueColumnName = "CatalogId";
                this.WhereCalue = " ISNULL(C.IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND C.ProductId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Supplier From List";
                    lstColumns.Add(new ColumnInfo() { title = "Supplier Id", field = "CatalogId", show = false, sortable = "CatalogId", filter = new Dictionary<string, string>() { { "CatalogId", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "Product Name", field = "ProductName", show = true, sortable = "ProductName", filter = new Dictionary<string, string>() { { "ProductName", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "Supplier Name", field = "CompanyName", show = true, sortable = "CompanyName", filter = new Dictionary<string, string>() { { "CompanyName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "ProductSupllierModelNoDetail")
            {
                this.TableName = "ProductCatalogMaster as C  inner Join ProductMaster as P on  C.ProductId = P.ProductId inner Join BuyerMaster as B on  B.BuyerId = C.SupplierId";
                this.DisplayColumnName = "SupplierModelNo";
                this.Keyword = keyword;
                this.ValueColumnName = "CatalogId";
                this.WhereCalue = " ISNULL(C.IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND C.ProductId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Supplier Model No From List";
                    lstColumns.Add(new ColumnInfo() { title = "Supplier Id", field = "CatalogId", show = false, sortable = "CatalogId", filter = new Dictionary<string, string>() { { "CatalogId", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "Product Name", field = "ProductName", show = true, sortable = "ProductName", filter = new Dictionary<string, string>() { { "ProductName", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "Supplier Model No", field = "SupplierModelNo", show = true, sortable = "SupplierModelNo", filter = new Dictionary<string, string>() { { "SupplierModelNo", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            //else if (type == "MainProductMaster")
            //{
            //    this.TableName = "MainProductMaster as P Join SubCategoryMaster as S on  P.SubCategoryId = S.SubCategoryId ";
            //    this.DisplayColumnName = "MainProductName";
            //    this.Keyword = keyword;
            //    this.ValueColumnName = "MainProductId";
            //    this.WhereCalue = " ISNULL(P.IsActive,1)=1 ";
            //    if (RelatedValue.Trim() != "")
            //        this.WhereCalue += " AND P.SubCategoryId=" + RelatedValue;
            //    if (!IsPopup)
            //        this.Result = this.GetData();
            //    else
            //    {
            //        this.PopupTitle = "Select MainProduct From List";
            //        lstColumns.Add(new ColumnInfo() { title = "Sub Category", field = "SubCategoryName" });
            //        lstColumns.Add(new ColumnInfo() { title = "Main Product Name", field = "MainProductName" });
            //        lstColumns.Add(new ColumnInfo() { title = "MainProductId", field = "MainProductId", show = false });
            //        this.PopupColumns = lstColumns;
            //    }
            //}
            else if (type == "CompanyMaster")
            {
                this.TableName = "CompanyMaster ";
                this.DisplayColumnName = "ComCode";
                this.Keyword = keyword;
                this.ValueColumnName = "ComId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND ComId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Company From List";
                    lstColumns.Add(new ColumnInfo() { title = "Company Code", field = "ComCode", show = true, sortable = "ComCode", filter = new Dictionary<string, string>() { { "ComCode", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "Company Name", field = "ComName", show = true, sortable = "ComName", filter = new Dictionary<string, string>() { { "ComName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "BuyerMaster")
            {
                this.TableName = "BuyerMaster ";
                this.DisplayColumnName = "CompanyName";
                this.Keyword = keyword;
                this.ValueColumnName = "BuyerId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND BuyerId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Buyer From List";
                    lstColumns.Add(new ColumnInfo() { title = "Company Name", field = "CompanyName", show = true, sortable = "CompanyName", filter = new Dictionary<string, string>() { { "CompanyName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "BuyerContactMaster")
            {
                this.TableName = "BuyerMaster as BM Join BuyerContactDetail as BC on BC.BuyerId = BM.BuyerId ";
                this.DisplayColumnName = "ContactPerson";
                this.Keyword = keyword;
                this.ValueColumnName = "ContactId";
                this.WhereCalue = " ISNULL(BC.IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND BM.BuyerId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Buyer Contact From List";
                    lstColumns.Add(new ColumnInfo() { title = "Company Name", field = "CompanyName", show = true, sortable = "CompanyName", filter = new Dictionary<string, string>() { { "CompanyName", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "Contact Person", field = "ContactPerson", show = true, sortable = "ContactPerson", filter = new Dictionary<string, string>() { { "ContactPerson", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "CurrencyMaster")
            {
                this.TableName = "CurrencyMaster ";
                this.DisplayColumnName = "CurrencyName";
                this.Keyword = keyword;
                this.ValueColumnName = "CurrencyId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND CurrencyId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Currency From List";
                    lstColumns.Add(new ColumnInfo() { title = "Currency Name", field = "CurrencyName", show = true, sortable = "CurrencyName", filter = new Dictionary<string, string>() { { "CurrencyName", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "CurrencyId", field = "CurrencyId", show = false, sortable = "CurrencyId", filter = new Dictionary<string, string>() { { "CurrencyId", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "PaymentTermsMaster")
            {
                this.TableName = "PaymentTermsMaster ";
                this.DisplayColumnName = "TermName";
                this.Keyword = keyword;
                this.ValueColumnName = "PaymentTermId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND PaymentTermId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Payment terms From List";
                    lstColumns.Add(new ColumnInfo() { title = "Term Name", field = "TermName", show = true, sortable = "TermName", filter = new Dictionary<string, string>() { { "TermName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "DeliveryTermsMaster")
            {
                this.TableName = "DeliveryTermsMaster ";
                this.DisplayColumnName = "DeliveryName";
                this.Keyword = keyword;
                this.ValueColumnName = "TermsId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND TermsId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Delivery terms From List";
                    lstColumns.Add(new ColumnInfo() { title = "Delivery Name", field = "DeliveryName", show = true, sortable = "DeliveryName", filter = new Dictionary<string, string>() { { "DeliveryName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "CountryOfOriginMaster")
            {
                this.TableName = "CountryOfOriginMaster ";
                this.DisplayColumnName = "CountryOfOrigin";
                this.Keyword = keyword;
                this.ValueColumnName = "OriginId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND OriginId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Country Of Origin From List";
                    lstColumns.Add(new ColumnInfo() { title = "Country Of Origin", field = "CountryOfOrigin", show = true, sortable = "CountryOfOrigin", filter = new Dictionary<string, string>() { { "CountryOfOrigin", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "OriginId", field = "OriginId", show = false, sortable = "OriginId", filter = new Dictionary<string, string>() { { "OriginId", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "UnitMaster")
            {
                this.TableName = "UnitMaster ";
                this.DisplayColumnName = "UnitName";
                this.Keyword = keyword;
                this.ValueColumnName = "UnitId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND UnitId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Unit From List";
                    lstColumns.Add(new ColumnInfo() { title = "Unit Name", field = "UnitName", show = true, sortable = "UnitName", filter = new Dictionary<string, string>() { { "UnitName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "DesignationMaster")
            {
                this.TableName = "DesignationMaster ";
                this.DisplayColumnName = "DesignationName";
                this.Keyword = keyword;
                this.ValueColumnName = "DesignationId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND DesignationId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Designation From List";
                    lstColumns.Add(new ColumnInfo() { title = "Designation Name", field = "DesignationName", show = true, sortable = "DesignationName", filter = new Dictionary<string, string>() { { "DesignationName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "CountryMaster" || type == "CountryCodeMaster")
            {
                this.TableName = "CountryMaster ";
                if (type == "CountryMaster")
                    this.DisplayColumnName = "CountryName";
                else
                    this.DisplayColumnName = "CountryCallCode";

                this.Keyword = keyword;
                this.ValueColumnName = "CountryId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND CountryId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Country From List";
                    lstColumns.Add(new ColumnInfo() { title = "Country Name", field = "CountryName", show = true, sortable = "CountryName", filter = new Dictionary<string, string>() { { "CountryName", "text" } } });
                    if (type == "CountryCodeMaster")
                        lstColumns.Add(new ColumnInfo() { title = "Country Code", field = "CountryCallCode", show = true, sortable = "CountryCallCode", filter = new Dictionary<string, string>() { { "CountryCallCode", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "StateMaster")
            {
                this.TableName = "CountryMaster as C join StateMaster as S on C.CountryId = S.CountryId ";
                this.DisplayColumnName = "StateName";
                this.Keyword = keyword;
                this.ValueColumnName = "StateId";
                this.WhereCalue = " ISNULL(S.IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND C.CountryId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select State From List";
                    lstColumns.Add(new ColumnInfo() { title = "Country Name", field = "CountryName", show = true, sortable = "CountryName", filter = new Dictionary<string, string>() { { "CountryName", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "State Name", field = "StateName", show = true, sortable = "StateName", filter = new Dictionary<string, string>() { { "StateName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "ExpenseTypeMaster")
            {
                this.TableName = "ExpenseTypeMaster";
                this.DisplayColumnName = "ExTypeName";
                this.Keyword = keyword;
                this.ValueColumnName = "ExTypeId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Expence Type From List";
                    lstColumns.Add(new ColumnInfo() { title = "Expense Type Name", field = "ExTypeName", show = true, sortable = "ExTypeName", filter = new Dictionary<string, string>() { { "ExTypeName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "CityMaster")
            {
                this.TableName = "CountryMaster as C join StateMaster as S on C.CountryId = S.CountryId join CityMaster as CI on S.StateId = CI.StateId ";
                this.DisplayColumnName = "CityName";
                this.Keyword = keyword;
                this.ValueColumnName = "CityId";
                this.WhereCalue = " ISNULL(S.IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND S.StateId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select City From List";
                    lstColumns.Add(new ColumnInfo() { title = "Country Name", field = "CountryName", show = true, sortable = "CountryName", filter = new Dictionary<string, string>() { { "CountryName", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "State Name", field = "StateName", show = true, sortable = "StateName", filter = new Dictionary<string, string>() { { "StateName", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "City Name", field = "CityName", show = true, sortable = "CityName", filter = new Dictionary<string, string>() { { "CityName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "AreaMaster")
            {
                this.TableName = "AreaMaster as A join CityMaster As C on A.CityId = C.CityId";
                this.DisplayColumnName = "AreaName";
                this.Keyword = keyword;
                this.ValueColumnName = "AreaId";
                this.WhereCalue = " ISNULL(A.IsActive,1)=1 AND ISNULL(A.IsDefault,0)=0 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += "  AND C.CityId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Area From List";
                    lstColumns.Add(new ColumnInfo() { title = "City Name", field = "CityName", show = true, sortable = "CityName", filter = new Dictionary<string, string>() { { "CityName", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "Area Name", field = "AreaName", show = true, sortable = "AreaName", filter = new Dictionary<string, string>() { { "AreaName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "UserMaster")
            {
                this.TableName = "UserMaster";
                this.DisplayColumnName = "Name + ' ' + Surname";
                this.Keyword = keyword;
                this.ValueColumnName = "UserId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select User From List";
                    lstColumns.Add(new ColumnInfo() { title = "User Name", field = "Name + ' ' + Surname", show = true, sortable = "Name + ' ' + Surname", filter = new Dictionary<string, string>() { { "Name + ' ' + Surname", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "SupplierMaster")
            {
                this.TableName = "SupplierMaster ";
                this.DisplayColumnName = "CompanyName";
                this.Keyword = keyword;
                this.ValueColumnName = "SupplierId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND SupplierId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Supplier From List";
                    lstColumns.Add(new ColumnInfo() { title = "Supplier Company Name", field = "CompanyName", show = true, sortable = "CompanyName", filter = new Dictionary<string, string>() { { "CompanyName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "SupplierContactDetail")
            {
                this.TableName = "BuyerContactDetail as C Join BuyerMaster as S on  C.BuyerId = S.BuyerId  ";
                this.DisplayColumnName = "ContactPerson";
                this.Keyword = keyword;
                this.ValueColumnName = "ContactId";
                this.WhereCalue = " ISNULL(C.IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND C.BuyerId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Contact List";
                    lstColumns.Add(new ColumnInfo() { title = "Supplier Company Name", field = "CompanyName", show = true, sortable = "CompanyName", filter = new Dictionary<string, string>() { { "CompanyName", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "Contact Name", field = "ContactPerson", show = true, sortable = "ContactPerson", filter = new Dictionary<string, string>() { { "ContactPerson", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "Mobile No", field = "MobileNo", show = true, sortable = "MobileNo", filter = new Dictionary<string, string>() { { "MobileNo", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "Email", field = "Email", show = true, sortable = "Email", filter = new Dictionary<string, string>() { { "Email", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "SupplierAddressDetail")
            {
                this.TableName = "BuyerAddressDetail as C Join BuyerMaster as S on  C.BuyerId = S.BuyerId  ";
                this.DisplayColumnName = "Address";
                this.Keyword = keyword;
                this.ValueColumnName = "AddressId";
                this.WhereCalue = "";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " C.BuyerId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Address List";
                    lstColumns.Add(new ColumnInfo() { title = "Supplier Company Name", field = "CompanyName", show = true, sortable = "CompanyName", filter = new Dictionary<string, string>() { { "CompanyName", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "Address", field = "Address", show = true, sortable = "Address", filter = new Dictionary<string, string>() { { "Address", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "PinCode", field = "PinCode", show = true, sortable = "PinCode", filter = new Dictionary<string, string>() { { "PinCode", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "Fax", field = "Fax", show = true, sortable = "Fax", filter = new Dictionary<string, string>() { { "Fax", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "VendorMaster")
            {
                this.TableName = "VendorMaster ";
                this.DisplayColumnName = "CompanyName";
                this.Keyword = keyword;
                this.ValueColumnName = "VendorId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND VendorId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Vendor From List";
                    lstColumns.Add(new ColumnInfo() { title = "Vendor Company Name", field = "CompanyName", show = true, sortable = "CompanyName", filter = new Dictionary<string, string>() { { "CompanyName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "EmailSpeechMaster")
            {
                this.TableName = "EmailSpeechMaster";
                this.DisplayColumnName = "Title";
                this.Keyword = keyword;
                this.ValueColumnName = "SpeechId";
                this.WhereCalue = "ISNULL(IsActive,1)=1";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND SpeechId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select EmailSpeech Title From List";
                    lstColumns.Add(new ColumnInfo() { title = "Title", field = "Title", show = true, sortable = "Title", filter = new Dictionary<string, string>() { { "Title", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "ContactTypeMaster")
            {
                this.TableName = "ContactTypeMaster";
                this.DisplayColumnName = "ContactTypeName";
                this.Keyword = keyword;
                this.ValueColumnName = "ContactTypeId";
                this.WhereCalue = "ISNULL(IsActive,1)=1";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND ContactTypeId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select ContactType Name From List";
                    lstColumns.Add(new ColumnInfo() { title = "ContactType Name", field = "ContactTypeName", show = true, sortable = "ContactTypeName", filter = new Dictionary<string, string>() { { "ContactTypeName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "SignatureMaster")
            {
                this.TableName = "SignatureMaster";
                this.DisplayColumnName = "Title";
                this.Keyword = keyword;
                this.ValueColumnName = "SignatureId";
                this.WhereCalue = "ISNULL(IsActive,1)=1";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += "AND SignatureId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Signature Title From List";
                    lstColumns.Add(new ColumnInfo() { title = "Title", field = "Title", show = true, sortable = "Title", filter = new Dictionary<string, string>() { { "Title", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "CourierClients")
            {
                this.TableName = @"(select CompanyName, CONVERT(nvarchar,CONVERT(nvarchar, BuyerId)+'|B') As ID from buyermaster WHERE IsActive = 1
                                union all SELECT CompanyName, CONVERT(nvarchar,CONVERT(nvarchar, SupplierId) + '|S') As ID from suppliermaster WHERE IsActive = 1
                                UNION ALL Select CompanyName, CONVERT(nvarchar,CONVERT(nvarchar, VendorId) + '|V') As ID from vendormaster WHERE IsActive = 1)T";
                this.DisplayColumnName = "CompanyName";
                this.Keyword = keyword;
                this.ValueColumnName = "ID";
                this.WhereCalue = "";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Sender From List";
                    lstColumns.Add(new ColumnInfo() { title = "Company Name", field = "CompanyName", show = true, sortable = "CompanyName", filter = new Dictionary<string, string>() { { "CompanyName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "DocumentNameMaster")
            {
                this.TableName = "DocumentNameMaster";
                this.DisplayColumnName = "DocName";
                this.Keyword = keyword;
                this.ValueColumnName = "DocId";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Doc From List";
                    lstColumns.Add(new ColumnInfo() { title = "Doc Name", field = "DocName", show = true, sortable = "DocName", filter = new Dictionary<string, string>() { { "DocName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "ApplicableChargeMaster")
            {
                this.TableName = "ApplicableChargeMaster";
                this.DisplayColumnName = "ApplicableChargeName";
                this.Keyword = keyword;
                this.ValueColumnName = "ApplicableChargeId";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Applicable Charge Name List";
                    lstColumns.Add(new ColumnInfo() { title = "ApplicableCharge Name", field = "ApplicableChargeName", show = true, sortable = "ApplicableChargeName", filter = new Dictionary<string, string>() { { "ApplicableChargeName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "eventtypemaster")
            {
                this.TableName = "eventtypemaster";
                this.DisplayColumnName = "EventTypeName";
                this.Keyword = keyword;
                this.ValueColumnName = "EventTypeId";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select EventType From List";
                    lstColumns.Add(new ColumnInfo() { title = "Event Type Name", field = "EventTypeName", show = true, sortable = "EventTypeName", filter = new Dictionary<string, string>() { { "EventTypeName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "AgencyTypeMaster")
            {
                this.TableName = "AgencyTypeMaster ";
                this.DisplayColumnName = "AgencyType";
                this.Keyword = keyword;
                this.ValueColumnName = "AgencyTypeId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Agency Type From List";
                    lstColumns.Add(new ColumnInfo() { title = "Agency Type", field = "AgencyType", show = true, sortable = "AgencyType", filter = new Dictionary<string, string>() { { "AgencyType", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }

            else if (type == "TechnicalSpecHeadMaster")
            {
                this.TableName = "TechnicalSpecHeadMaster ";
                this.DisplayColumnName = "TechHead";
                this.Keyword = keyword;
                this.ValueColumnName = "TechHeadId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Agency Type From List";
                    lstColumns.Add(new ColumnInfo() { title = "Tech Head", field = "TechHead", show = true, sortable = "TechHead", filter = new Dictionary<string, string>() { { "TechHead", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "ShipmentMaster")
            {
                this.TableName = "ShipmentMaster ";
                this.DisplayColumnName = "ModeOfShipment";
                this.Keyword = keyword;
                this.ValueColumnName = "ShipmentId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND ShipmentId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Mode Of Shipment From List";
                    lstColumns.Add(new ColumnInfo() { title = "Mode Of Shipment", field = "ModeOfShipment", show = true, sortable = "ModeOfShipment", filter = new Dictionary<string, string>() { { "ModeOfShipment", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "PortMaster")
            {
                this.TableName = "PortMaster ";
                this.DisplayColumnName = "PortName";
                this.Keyword = keyword;
                this.ValueColumnName = "PortId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND PortId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Port From List";
                    lstColumns.Add(new ColumnInfo() { title = "Port Name", field = "PortName", show = true, sortable = "PortName", filter = new Dictionary<string, string>() { { "PortName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "BankMaster")
            {
                this.TableName = "BankMaster ";
                this.DisplayColumnName = "BeneficiaryName";
                this.Keyword = keyword;
                this.ValueColumnName = "BankId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND BankId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Bank From List";
                    lstColumns.Add(new ColumnInfo() { title = "Beneficiary Name", field = "BeneficiaryName", show = true, sortable = "BeneficiaryName", filter = new Dictionary<string, string>() { { "BeneficiaryName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "PurchaseOrderMaster")
            {
                this.TableName = "PurchaseOrderMaster ";
                this.DisplayColumnName = "PoNo";
                this.Keyword = keyword;
                this.ValueColumnName = "POId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND POId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Purchase Order From List";
                    lstColumns.Add(new ColumnInfo() { title = "Purchase Order No", field = "PoNo", show = true, sortable = "PoNo", filter = new Dictionary<string, string>() { { "PoNo", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "TermsAndConditionMaster")
            {
                this.TableName = "TermsAndConditionMaster ";
                this.DisplayColumnName = "Title";
                this.Keyword = keyword;
                this.ValueColumnName = "TermsId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND TermsId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Terms And Condition From List";
                    lstColumns.Add(new ColumnInfo() { title = "Title", field = "Title", show = true, sortable = "Title", filter = new Dictionary<string, string>() { { "Title", "text" } } });
                    lstColumns.Add(new ColumnInfo()
                    {
                        title = "Description",
                        cellTemplte = "<div ng-bind-html ='getHtml(row.Description)'></div>",
                        show = true
                    });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "TaskStatusMaster")
            {
                this.TableName = "TaskStatusMaster";
                this.DisplayColumnName = "TaskStatus";
                this.Keyword = keyword;
                this.ValueColumnName = "StatusId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND TermsId=" + RelatedValue;
                if (sessionUtils.UserRollType > 1)
                {
                    this.WhereCalue += " And TaskStatus != 'Completed'";
                }
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Task Status From List";
                    lstColumns.Add(new ColumnInfo() { title = "Task Status", field = "TaskStatus", show = true, sortable = "TaskStatus", filter = new Dictionary<string, string>() { { "TaskStatus", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "TaskPriorityMaster")
            {
                this.TableName = "TaskPriorityMaster";
                this.DisplayColumnName = "PriorityName";
                this.Keyword = keyword;
                this.ValueColumnName = "PriorityId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Priority From List";
                    lstColumns.Add(new ColumnInfo() { title = "Priority Name", field = "PriorityName", show = true, sortable = "PriorityName", filter = new Dictionary<string, string>() { { "PriorityName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "TaskTypeMaster")
            {
                this.TableName = "TaskTypeMaster";
                this.DisplayColumnName = "TaskType";
                this.Keyword = keyword;
                this.ValueColumnName = "TaskTypeId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Task Type From List";
                    lstColumns.Add(new ColumnInfo() { title = "Task Type", field = "TaskType", show = true, sortable = "TaskType", filter = new Dictionary<string, string>() { { "TaskType", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "ReportingUserMaster")
            {
                this.TableName = "UserMaster";
                this.DisplayColumnName = "Name";
                this.Keyword = keyword;
                this.ValueColumnName = "UserId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 AND ReportingId=" + sessionUtils.UserId + "";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select User From List";
                    lstColumns.Add(new ColumnInfo() { title = "Name", field = "Name", show = true, sortable = "Name", filter = new Dictionary<string, string>() { { "Name", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "BloodGroupMaster")
            {
                this.TableName = "BloodGroupMaster ";
                this.DisplayColumnName = "BloodGroup";
                this.Keyword = keyword;
                this.ValueColumnName = "BloodGroupId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND BloodGroupId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                this.PopupTitle = "Select BloodGroup From List";
                lstColumns.Add(new ColumnInfo() { title = "BloodGroup", field = "BloodGroup", show = true, sortable = "BloodGroup", filter = new Dictionary<string, string>() { { "BloodGroup", "text" } } });
                this.PopupColumns = lstColumns;
            }
            else if (type == "AccountTypeMaster")
            {
                this.TableName = "AccountTypeMaster ";
                this.DisplayColumnName = "AccountType";
                this.Keyword = keyword;
                this.ValueColumnName = "AccountTypeId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND AccountTypeId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                this.PopupTitle = "Select Account Type From List";
                lstColumns.Add(new ColumnInfo() { title = "AccountType", field = "AccountType", show = true, sortable = "AccountType", filter = new Dictionary<string, string>() { { "AccountType", "text" } } });
                this.PopupColumns = lstColumns;
            }
            else if (type == "AdvertisementSourceMaster")
            {
                this.TableName = "AdvertisementSourceMaster ";
                this.DisplayColumnName = "SiteName";
                this.Keyword = keyword;
                this.ValueColumnName = "SiteId";
                this.WhereCalue = " ISNULL(IsActive,1)=1";
                if (!IsPopup)
                    this.Result = this.GetData();
                this.PopupTitle = "Select Account Type From List";
                lstColumns.Add(new ColumnInfo() { title = "Source Name", field = "SiteName", show = true, sortable = "SiteName", filter = new Dictionary<string, string>() { { "SiteName", "text" } } });
                lstColumns.Add(new ColumnInfo() { title = "Source Url", field = "SiteUrl", show = true, sortable = "SiteUrl", filter = new Dictionary<string, string>() { { "SiteUrl", "text" } } });
                this.PopupColumns = lstColumns;
            }
            else if (type == "SourceMaster")
            {
                this.TableName = "SourceMaster ";
                this.DisplayColumnName = "SourceName";
                this.Keyword = keyword;
                this.ValueColumnName = "SourceId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND SourceId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                this.PopupTitle = "Select Source From List";
                lstColumns.Add(new ColumnInfo() { title = "Source Name", field = "SourceName", show = true, sortable = "SourceName", filter = new Dictionary<string, string>() { { "SourceName", "text" } } });
                this.PopupColumns = lstColumns;
            }
            else if (type == "RoleMaster")
            {
                this.TableName = "RoleMaster";
                this.DisplayColumnName = "RoleName";
                this.Keyword = keyword;
                this.ValueColumnName = "RoleId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND RoleId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                this.PopupTitle = "Select Role From List";
                lstColumns.Add(new ColumnInfo() { title = "Role Name", field = "RoleName", show = true, sortable = "RoleName", filter = new Dictionary<string, string>() { { "RoleName", "text" } } });
                this.PopupColumns = lstColumns;
            }
            else if (type == "DepartmentMaster")
            {
                this.TableName = "DepartmentMaster";
                this.DisplayColumnName = "DepartmentName";
                this.Keyword = keyword;
                this.ValueColumnName = "DepartmentId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND DepartmentId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                this.PopupTitle = "Select Department From List";
                lstColumns.Add(new ColumnInfo() { title = "Department Name", field = "DepartmentName", show = true, sortable = "DepartmentName", filter = new Dictionary<string, string>() { { "DepartmentName", "text" } } });
                this.PopupColumns = lstColumns;
            }
            else if (type == "ChatNameMaster")
            {
                this.TableName = "ChatNameMaster";
                this.DisplayColumnName = "ChatName";
                this.Keyword = keyword;
                this.ValueColumnName = "ChatId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND ChatId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                this.PopupTitle = "Select Chat From List";
                lstColumns.Add(new ColumnInfo() { title = "Chat Name", field = "ChatName", show = true, sortable = "ChatName", filter = new Dictionary<string, string>() { { "ChatName", "text" } } });
                this.PopupColumns = lstColumns;
            }
            else if (type == "BankNameMaster")
            {
                this.TableName = "BankNameMaster";
                this.DisplayColumnName = "BankName";
                this.Keyword = keyword;
                this.ValueColumnName = "BankId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND BankId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                this.PopupTitle = "Select BankName From List";
                lstColumns.Add(new ColumnInfo() { title = "Bank Name", field = "BankName", show = true, sortable = "BankName", filter = new Dictionary<string, string>() { { "BankName", "text" } } });
                this.PopupColumns = lstColumns;
            }
            else if (type == "ITRMaster")
            {
                this.TableName = "ITRMaster";
                this.DisplayColumnName = "ITRName";
                this.Keyword = keyword;
                this.ValueColumnName = "ITRId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND ITRId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                this.PopupTitle = "Select ITR List";
                lstColumns.Add(new ColumnInfo() { title = "ITR Name", field = "ITRName", show = true, sortable = "ITRName", filter = new Dictionary<string, string>() { { "ITRName", "text" } } });
                this.PopupColumns = lstColumns;
            }
            else if (type == "LegerHeadMaster")
            {
                this.TableName = "LegerHeadMaster";
                this.DisplayColumnName = "LegerHeadName";
                this.Keyword = keyword;
                this.ValueColumnName = "LegerHeadId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND LegerHeadId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                this.PopupTitle = "Select LegerHead List";
                lstColumns.Add(new ColumnInfo() { title = "LegerHead Name", field = "LegerHeadName", show = true, sortable = "LegerHeadName", filter = new Dictionary<string, string>() { { "LegerHeadName", "text" } } });
                this.PopupColumns = lstColumns;
            }
            else if (type == "InquiryMaster")
            {
                this.TableName = "InquiryMaster";
                this.DisplayColumnName = "InqNo";
                this.Keyword = keyword;
                this.ValueColumnName = "InqId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                this.PopupTitle = "Select Inquiry List";
                lstColumns.Add(new ColumnInfo() { title = "Inquiry No", field = "InqNo", show = true, sortable = "InqNo", filter = new Dictionary<string, string>() { { "InqNo", "text" } } });
                this.PopupColumns = lstColumns;
            }
            else if (type == "TaskGroupMaster")
            {
                this.TableName = "TaskGroupMaster";
                this.DisplayColumnName = "TaskGroupName";
                this.Keyword = keyword;
                this.ValueColumnName = "TaskGroupId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Task Group List";
                    lstColumns.Add(new ColumnInfo() { title = "Task Group Name", field = "TaskGroupName", show = true, sortable = "TaskGroupName", filter = new Dictionary<string, string>() { { "TaskGroupName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "InquiryBuyer")
            {
                this.TableName = "InquiryMaster";
                this.DisplayColumnName = "BuyerName";
                this.Keyword = keyword;
                this.ValueColumnName = "BuyerName";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Buyer List";
                    lstColumns.Add(new ColumnInfo() { title = "Buyer Name", field = "BuyerName", show = true, sortable = "BuyerName", filter = new Dictionary<string, string>() { { "BuyerName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "TOTypeMaster")
            {
                this.TableName = "TOTypeMaster";
                this.DisplayColumnName = "TOType";
                this.Keyword = keyword;
                this.ValueColumnName = "ToTypeId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Technical Order Type List";
                    lstColumns.Add(new ColumnInfo() { title = "Technical Order Type", field = "TOType", show = true, sortable = "TOType", filter = new Dictionary<string, string>() { { "TOType", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "SpecificationMaster")
            {
                this.TableName = "TechnicalSpecHeadMaster as C Join TechnicalSpecMaster as S on  C.TechHeadId = S.TechHeadId";
                this.DisplayColumnName = "TechSpec";
                this.Keyword = keyword;
                this.ValueColumnName = "SpecificationId";
                this.WhereCalue = " ISNULL(S.IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND C.TechHeadId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Technical Specification List";
                    lstColumns.Add(new ColumnInfo() { title = "Technical Head", field = "TechHead", show = true, sortable = "TechHead", filter = new Dictionary<string, string>() { { "TechHead", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "Technical Specification", field = "TechSpec", show = true, sortable = "TechSpec", filter = new Dictionary<string, string>() { { "TechSpec", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "RelationMaster")
            {
                this.TableName = "RelationMaster";
                this.DisplayColumnName = "RelationName";
                this.Keyword = keyword;
                this.ValueColumnName = "RelationId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Relation List";
                    lstColumns.Add(new ColumnInfo() { title = "Relation Name", field = "RelationName", show = true, sortable = "RelationName", filter = new Dictionary<string, string>() { { "RelationName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "EmpReffTypeMaster")
            {
                this.TableName = "EmpReffTypeMaster";
                this.DisplayColumnName = "ReffTypeName";
                this.Keyword = keyword;
                this.ValueColumnName = "ReffTypeId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Reff Type Name List";
                    lstColumns.Add(new ColumnInfo() { title = "Reference Type Name", field = "ReffTypeName", show = true, sortable = "ReffTypeName", filter = new Dictionary<string, string>() { { "ReffTypeName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "LicenseMaster")
            {
                this.TableName = "LicenseMaster";
                this.DisplayColumnName = "LicenseName";
                this.Keyword = keyword;
                this.ValueColumnName = "LicenseId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select License From List";
                    lstColumns.Add(new ColumnInfo() { title = "License Name", field = "LicenseName", show = true, sortable = "LicenseName", filter = new Dictionary<string, string>() { { "LicenseName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "BuyerAddressDetail")
            {
                this.TableName = "BuyerAddressDetail";
                this.DisplayColumnName = "Address";
                this.Keyword = keyword;
                this.ValueColumnName = "AddressId";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue = " (BuyerId = '" + RelatedValue + "')";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Address From List";
                    lstColumns.Add(new ColumnInfo() { title = "Address", field = "Address", show = true, sortable = "Address", filter = new Dictionary<string, string>() { { "Address", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "VendorAddressDetail")
            {
                this.TableName = "VendorAddressDetail";
                this.DisplayColumnName = "Address";
                this.Keyword = keyword;
                this.ValueColumnName = "AddressId";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue = " (VendorId = '" + RelatedValue + "')";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Address From List";
                    lstColumns.Add(new ColumnInfo() { title = "Address", field = "Address", show = true, sortable = "Address", filter = new Dictionary<string, string>() { { "Address", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "SupplierAddressMaster")
            {
                this.TableName = "SupplierAddressMaster";
                this.DisplayColumnName = "Address";
                this.Keyword = keyword;
                this.ValueColumnName = "AddressId";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue = " (SupplierId = '" + RelatedValue + "')";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Address From List";
                    lstColumns.Add(new ColumnInfo() { title = "Address", field = "Address", show = true, sortable = "Address", filter = new Dictionary<string, string>() { { "Address", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "SalesDocumentNameMaster")
            {
                this.TableName = "SalesDocumentNameMaster";
                this.DisplayColumnName = "SalesDocument";
                this.Keyword = keyword;
                this.ValueColumnName = "SalesDocId";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Sales Document From List";
                    lstColumns.Add(new ColumnInfo() { title = "Sales Document", field = "SalesDocument", show = true, sortable = "SalesDocument", filter = new Dictionary<string, string>() { { "SalesDocument", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "SalesOrderMaster")
            {
                this.TableName = "SalesOrderMaster";
                this.DisplayColumnName = "SoNo";
                this.Keyword = keyword;
                this.ValueColumnName = "SOId";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Sales Order From List";
                    lstColumns.Add(new ColumnInfo() { title = "Sales Order Reference", field = "SoNo", show = true, sortable = "SoNo", filter = new Dictionary<string, string>() { { "SoNo", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "FinancialYearMaster")
            {
                this.TableName = "FinancialYearMaster";
                this.DisplayColumnName = "FinancialYear";
                this.Keyword = keyword;
                this.ValueColumnName = "FinancialYearId";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Financial Year From List";
                    lstColumns.Add(new ColumnInfo() { title = "Financial Year", field = "FinancialYear", show = true, sortable = "FinancialYear", filter = new Dictionary<string, string>() { { "FinancialYear", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "SalaryHeadMaster")
            {
                this.TableName = "SalaryHeadMaster";
                this.DisplayColumnName = "SalaryHeadName";
                this.Keyword = keyword;
                this.ValueColumnName = "SalaryHeadId";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Salary Head From List";
                    lstColumns.Add(new ColumnInfo() { title = "Salary Head Name", field = "SalaryHeadName", show = true, sortable = "SalaryHeadName", filter = new Dictionary<string, string>() { { "SalaryHeadName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "Status")
            {
                this.TableName = "UserMaster";
                this.DisplayColumnName = "case when ISNULL(IsActive,1)=1 then ' Active' else  'InActive' end ";
                this.Keyword = keyword;
                this.ValueColumnName = "IsActive";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select IsActive From List";
                    lstColumns.Add(new ColumnInfo() { title = "IsActive", field = "Name", show = true, sortable = "Name", filter = new Dictionary<string, string>() { { "Name", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "AcHolderMaster")
            {
                this.TableName = "AcHolderMaster";
                this.DisplayColumnName = "AcHolderName";
                this.Keyword = keyword;
                this.ValueColumnName = "AcHolderCode";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select A/c Holder From List";
                    lstColumns.Add(new ColumnInfo() { title = "A/c Holder Name", field = "AcHolderName", show = true, sortable = "AcHolderName", filter = new Dictionary<string, string>() { { "AcHolderName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "SMSSpeechMaster")
            {
                this.TableName = "SMSSpeechMaster";
                this.DisplayColumnName = "SMSTitle";
                this.Keyword = keyword;
                this.ValueColumnName = "SMSId";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue = " (SMSId = '" + RelatedValue + "')";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select SMSSpeech List";
                    lstColumns.Add(new ColumnInfo() { title = "SMSTitle", field = "SMSTitle", show = true, sortable = "SMSTitle", filter = new Dictionary<string, string>() { { "SMSTitle", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "QualificationsMaster")
            {
                this.TableName = "QualificationsMaster";
                this.DisplayColumnName = "QualificationName";
                this.Keyword = keyword;
                this.ValueColumnName = "QualificationId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Qualification From List";
                    lstColumns.Add(new ColumnInfo() { title = "Qualification Name", field = "QualificationName", show = true, sortable = "QualificationName", filter = new Dictionary<string, string>() { { "QualificationName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "ContactTypeMaster")
            {
                this.TableName = "ContactTypeMaster";
                this.DisplayColumnName = "ContactTypeName";
                this.Keyword = keyword;
                this.ValueColumnName = "ContactTypeId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select ContactType From List";
                    lstColumns.Add(new ColumnInfo() { title = "ContactType Name", field = "ContactTypeName", show = true, sortable = "ContactTypeName", filter = new Dictionary<string, string>() { { "ContactTypeName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "ContactDocumentNameMaster")
            {
                this.TableName = "ContactDocumentNameMaster";
                this.DisplayColumnName = "ContactDocName";
                this.Keyword = keyword;
                this.ValueColumnName = "ContactDocId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Contact Document From List";
                    lstColumns.Add(new ColumnInfo() { title = "Contact Document Name", field = "ContactDocName", show = true, sortable = "ContactDocName", filter = new Dictionary<string, string>() { { "ContactDocName", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "CourierTypeMaster")
            {
                this.TableName = "CourierTypeMaster";
                this.DisplayColumnName = "CourierType";
                this.Keyword = keyword;
                this.ValueColumnName = "CourierTypeId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select CourierType From List";
                    lstColumns.Add(new ColumnInfo() { title = "Courier Type", field = "CourierType", show = true, sortable = "CourierType", filter = new Dictionary<string, string>() { { "CourierType", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "ProductDocumentMaster")
            {
                this.TableName = "ProductDocumentMaster";
                this.DisplayColumnName = "PrdDocName";
                this.Keyword = keyword;
                this.ValueColumnName = "PrdDocId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Product Document From List";
                    lstColumns.Add(new ColumnInfo() { title = "Product Document Name", field = "PrdDocName", show = true, sortable = "PrdDocName", filter = new Dictionary<string, string>() { { "PrdDocName", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "PrdDocId", field = "PrdDocId", show = false, sortable = "PrdDocId", filter = new Dictionary<string, string>() { { "PrdDocId", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "PackingTypeMaster")
            {
                this.TableName = "PackingTypeMaster";
                this.DisplayColumnName = "PackingType";
                this.Keyword = keyword;
                this.ValueColumnName = "PackingTypeId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Packing Type From List";
                    lstColumns.Add(new ColumnInfo() { title = "Packing Type", field = "PackingType", show = true, sortable = "PackingType", filter = new Dictionary<string, string>() { { "PackingType", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "PackingTypeId", field = "PackingTypeId", show = false, sortable = "PackingTypeId", filter = new Dictionary<string, string>() { { "PackingTypeId", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "ReportFormatMaster")
            {
                this.TableName = "ReportFormatMaster";
                this.DisplayColumnName = "CompanyCode";
                this.Keyword = keyword;
                this.ValueColumnName = "RotFormatId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Report Fromat From List";
                    lstColumns.Add(new ColumnInfo() { title = "Company Code", field = "CompanyCode", show = true, sortable = "CompanyCode", filter = new Dictionary<string, string>() { { "CompanyCode", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "Company Header", field = "CompanyHeader", show = true, sortable = "CompanyHeader", filter = new Dictionary<string, string>() { { "CompanyHeader", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "Company Footer", field = "CompanyFooter", show = true, sortable = "CompanyFooter", filter = new Dictionary<string, string>() { { "CompanyFooter", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "RotFormatId", field = "RotFormatId", show = false, sortable = "RotFormatId", filter = new Dictionary<string, string>() { { "RotFormatId", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "Technicalspecification")
            {
                this.TableName = "ProductParameterMaster P inner join TechnicalSpecMaster T on P.Techparaid=T.SpecificationId inner join TechnicalSpecHeadMaster H on H.TechHeadId=T.TechHeadId";
                this.DisplayColumnName = "TechSpec";
                this.Keyword = keyword;
                this.ValueColumnName = "SpecificationId";
                this.WhereCalue = "ISNULL(T.isActive,1)=1 and T.TechHeadId=" + RelatedValue + "";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Report Fromat From List";
                    lstColumns.Add(new ColumnInfo() { title = "TechSpec", field = "TechSpec", show = true, sortable = "TechSpec", filter = new Dictionary<string, string>() { { "TechSpec", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "SpecificationId", field = "SpecificationId", show = false, sortable = "SpecificationId", filter = new Dictionary<string, string>() { { "SpecificationId", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "TechnicalPeraHeadMaster")
            {
                this.TableName = "ProductParameterMaster P inner join TechnicalSpecMaster T on P.Techparaid=T.SpecificationId inner join TechnicalSpecHeadMaster H on H.TechHeadId=T.TechHeadId";
                this.DisplayColumnName = "TechHead";
                this.Keyword = keyword;
                this.ValueColumnName = "T.TechHeadId";
                this.WhereCalue = "ISNULL(T.isActive,1)=1 and P.ProductId=" + RelatedValue + "";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Report Fromat From List";
                    lstColumns.Add(new ColumnInfo() { title = "TechHead", field = "TechHead", show = true, sortable = "TechHead", filter = new Dictionary<string, string>() { { "TechHead", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "TechHeadId", field = "TechHeadId", show = false, sortable = "TechHeadId", filter = new Dictionary<string, string>() { { "TechHeadId", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "PlugShapeMaster")
            {
                this.TableName = "plugshapeMaster";
                this.DisplayColumnName = "Title";
                this.Keyword = keyword;
                this.ValueColumnName = "PlugShapeId";
                this.WhereCalue = "ISNULL(isActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select PlugShape From List";
                    lstColumns.Add(new ColumnInfo() { title = "Plug Shape", field = "Title", show = true, sortable = "Title", filter = new Dictionary<string, string>() { { "Title", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "PlugShapeId", field = "PlugShapeId", show = false, sortable = "PlugShapeId", filter = new Dictionary<string, string>() { { "PlugShapeId", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "VoltageMaster")
            {
                this.TableName = "VoltageMaster";
                this.DisplayColumnName = "Voltage";
                this.Keyword = keyword;
                this.ValueColumnName = "VoltageId";
                this.WhereCalue = "ISNULL(isActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Voltage From List";
                    lstColumns.Add(new ColumnInfo() { title = "Voltage", field = "Voltage", show = true, sortable = "Voltage", filter = new Dictionary<string, string>() { { "Voltage", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "VoltageId", field = "VoltageId", show = false, sortable = "VoltageId", filter = new Dictionary<string, string>() { { "VoltageId", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "Phasemaster")
            {
                this.TableName = "PhaseMaster";
                this.DisplayColumnName = "Phase";
                this.Keyword = keyword;
                this.ValueColumnName = "PhaseId";
                this.WhereCalue = "ISNULL(isActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Phase From List";
                    lstColumns.Add(new ColumnInfo() { title = "Phase", field = "Phase", show = true, sortable = "Phase", filter = new Dictionary<string, string>() { { "Phase", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "PhaseId", field = "PhaseId", show = false, sortable = "PhaseId", filter = new Dictionary<string, string>() { { "PhaseId", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "FrequencyMaster")
            {
                this.TableName = "FrequencyMaster";
                this.DisplayColumnName = "Frequency";
                this.Keyword = keyword;
                this.ValueColumnName = "FrequencyId";
                this.WhereCalue = "ISNULL(isActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select Frequency From List";
                    lstColumns.Add(new ColumnInfo() { title = "Frequency", field = "Frequency", show = true, sortable = "Frequency", filter = new Dictionary<string, string>() { { "Frequency", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "FrequencyId", field = "FrequencyId", show = false, sortable = "FrequencyId", filter = new Dictionary<string, string>() { { "FrequencyId", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }


            else if (type == "ReferenceSourceMaster")
            {
                this.TableName = "ReferenceSourceMaster ";
                this.DisplayColumnName = "SourceName";
                this.Keyword = keyword;
                this.ValueColumnName = "SourceId";
                this.WhereCalue = " ISNULL(IsActive,1)=1 ";
                if (RelatedValue.Trim() != "")
                    this.WhereCalue += " AND SourceId=" + RelatedValue;
                if (!IsPopup)
                    this.Result = this.GetData();
                this.PopupTitle = "Select Source From List";
                lstColumns.Add(new ColumnInfo() { title = "Source Name", field = "SourceName", show = true, sortable = "SourceName", filter = new Dictionary<string, string>() { { "SourceName", "text" } } });
                this.PopupColumns = lstColumns;
            }
            else if (type == "DealerPrice")
            {
                this.TableName = " ProductPrices AS A INNER JOIN CurrencyMaster AS B ON A.CurrencyId = B.CurrencyId";
                this.DisplayColumnName = "(CurrencyName + ' '+ CAST(TotalAmount AS nvarchar)) ";
                this.Keyword = keyword;
                this.ValueColumnName = "ProductPriceId";
                string[] words = RelatedValue.Split('|');
                int PId = words[0] == "" ? 0 : Convert.ToInt32(words[0]);
                int SId = words[1] == "" ? 0 : Convert.ToInt32(words[1]);
                this.WhereCalue = "1=1 AND A.ProductId=" + PId + " AND A.SupplierId=" + SId;
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select DealerPrice List";
                    lstColumns.Add(new ColumnInfo() { title = "DealerPrice", field = "DealerPrice", show = true, sortable = "DealerPrice", filter = new Dictionary<string, string>() { { "DealerPrice", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "PaymentModeMaster")
            {
                this.TableName = "PaymentModeMaster";
                this.DisplayColumnName = "PaymentMode";
                this.Keyword = keyword;
                this.ValueColumnName = "PaymentModeId";
                this.WhereCalue = "ISNULL(isActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select PaymentMode From List";
                    lstColumns.Add(new ColumnInfo() { title = "PaymentMode", field = "PaymentMode", show = true, sortable = "PaymentMode", filter = new Dictionary<string, string>() { { "PaymentMode", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "PaymentModeId", field = "PaymentModeId", show = false, sortable = "PaymentModeId", filter = new Dictionary<string, string>() { { "PaymentModeId", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
            else if (type == "TransactionTypeMaster")
            {
                this.TableName = "TransactionTypeMaster";
                this.DisplayColumnName = "TranType";
                this.Keyword = keyword;
                this.ValueColumnName = "TranTypeId";
                this.WhereCalue = "ISNULL(isActive,1)=1 ";
                if (!IsPopup)
                    this.Result = this.GetData();
                else
                {
                    this.PopupTitle = "Select TranType From List";
                    lstColumns.Add(new ColumnInfo() { title = "TranType", field = "TranType", show = true, sortable = "TranType", filter = new Dictionary<string, string>() { { "TranType", "text" } } });
                    lstColumns.Add(new ColumnInfo() { title = "TranTypeId", field = "TranTypeId", show = false, sortable = "TranTypeId", filter = new Dictionary<string, string>() { { "TranTypeId", "text" } } });
                    this.PopupColumns = lstColumns;
                }
            }
        }

        public List<AutoCompleteData> GetData()
        {
            SqlParameter[] para = new SqlParameter[7];
            para[0] = new SqlParameter().CreateParameter("@TableName", this.TableName, 500);
            para[1] = new SqlParameter().CreateParameter("@DisplayColumnName", this.DisplayColumnName, 1000);
            para[2] = new SqlParameter().CreateParameter("@Keyword", this.Keyword);
            para[3] = new SqlParameter().CreateParameter("@ValueColumnName", this.ValueColumnName);
            para[4] = new SqlParameter().CreateParameter("@Count", this.Count);
            para[5] = new SqlParameter().CreateParameter("@WhereClause", this.WhereCalue, 500);
            para[6] = new SqlParameter().CreateParameter("@Type", this.Type);
            return new dalc().GetDataTable("GetAutoCompleteData", para).ConvertToList<AutoCompleteData>();
        }

        public string TableName { get; set; }
        public string Keyword { get; set; }
        public string DisplayColumnName { get; set; }
        public string ValueColumnName { get; set; }
        public int Count { get; set; }
        public List<AutoCompleteData> Result { get; set; }
        public List<ColumnInfo> PopupColumns { get; set; }
        public string PopupTitle { get; set; }
        public string WhereCalue { get; set; }
        public string Type { get; set; }
    }

    public class AutoCompleteData
    {
        public string Name { get; set; }
        public string ID { get; set; }
    }

    public class ColumnInfo
    {
        public string title { get; set; }
        public string field { get; set; }
        public string sortable { get; set; }
        public Boolean show { get; set; }
        public Dictionary<string, string> filter { get; set; }
        public string cellTemplte { get; set; }
    }

}