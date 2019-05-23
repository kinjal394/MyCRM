using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.Data
{
    public partial class ProductPackingDetail
    {
        public Nullable<int> Status { get; set; }
        public string CurrencyName { get; set; }
        public string TaxName { get; set; }
        public string PackingType { get; set; }

        public string PlugShape { get; set; }
        public string Phase { get; set; }
        public string Voltage { get; set; }
        public string Frequency { get; set; }
    }
    public partial class ProductSuppDocumentDetail
    {
        public Nullable<int> Status { get; set; }
        public string prdDocName { get; set; }
    }
    public partial class ProductPhotoMaster
    {
        public Nullable<int> Status { get; set; }
        public bool share { get; set; }
        public int ContactPerson { get; set; }
        public string ContactPersonType { get; set; }
    }
    public partial class ProductCatalogMaster
    {
        public Nullable<int> Status { get; set; }
        public bool share { get; set; }
        public int ContactPerson { get; set; }
        public string ContactPersonType { get; set; }
        public string SupplierName { get; set; }
        public string CountryOfOriginName { get; set; }
        public string CatalogMSO { get; set; }
        public string CatalogPDF { get; set; }
        public string NoofDoc { get; set; }
    }
    public partial class ProductLinkMaster
    {
        public Nullable<int> Status { get; set; }
        public string SourceName { get; set; }
        public bool share { get; set; }
        public int ContactPerson { get; set; }
        public string ContactPersonType { get; set; }
    }
    public partial class ProductVideoMaster
    {
        public Nullable<int> Status { get; set; }
        public bool share { get; set; }
        public int ContactPerson { get; set; }
        public string ContactPersonType { get; set; }
    }
    public class vmProductParameterMaster
    {
        public Nullable<int> Status { get; set; }
        public int CatalogId { get; set; }
        public int TechDetailId { get; set; }
        public int ProductId { get; set; }
        public int TechParaId { get; set; }
        public String TechSpec { get; set; }
        public string Value { get; set; }
        public int TechHeadId { get; set; }
        public String TechHead { get; set; }
    }

    public class ProductFormModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductFunctionality { get; set; }
        public string Functionality { get; set; }
        public int MainProductId { get; set; }
        public string MainProductName { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string HSCode { get; set; }
        public string ProductCode { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string OursModelNo { get; set; }
        public string ModelNo { get; set; }
        public string Height { get; set; }
        public string CBM { get; set; }
        public string Dimension { get; set; }
        public string Width { get; set; }
        public string Length { get; set; }
        public string GrossWeight { get; set; }
        public string NetWeight { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string Keywords { get; set; }
        public string GPlusLink { get; set; }
        public string SupplierCatalogimg { get; set; }
        public string OurCatalogImg { get; set; }
        public string FbLink { get; set; }
        public int CreatedBy { get; set; }
        public bool Isphotonext { get; set; }
        public bool Iscatlognext { get; set; }
        public string mode { get; set; }
        public string EmailId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public List<ProductPhotoMaster> ProductPhotoMasters { get; set; }
        public List<vmProductParameterMaster> ProductParameterMasters { get; set; }
        public List<ProductCatalogMaster> ProductCatalogMasters { get; set; }
        public List<ProductLinkMaster> ProductSocialMasters { get; set; }
        public List<ProductVideoMaster> ProductVideoMasters { get; set; }
        public List<ProductPackingDetail> ProductPackingDetails { get; set; }
        public List<ProductSuppDocumentDetail> ProductSuppDocumentDetail { get; set; }
        public List<ProductPrice> ProductpriceDetail { get; set; }
        public List<ProductApplicableCharge> ProductApplicableChargeDetail { get; set; }
    }

    public partial class TechnicalSpecMaster
    {
        public string TechHead { get; set; }

    }
}
