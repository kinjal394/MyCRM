using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.DTOModel
{
    public class vmSourceUrlDetail
    {
        public int AdId { get; set; }
        public int ProductId { get; set; }
        public int CatalogId { get; set; }
        public int AdSourceId { get; set; }
        public string AdSource { get; set; }
        public string Url { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }
    }
    public class vmVideoUrlDetail
    {
        public int AdId { get; set; }
        public int ProductId { get; set; }
        public int CatalogId { get; set; }
        public int AdSourceId { get; set; }
        public int VideoId { get; set; }
        public string URL { get; set; }
        public string VRemark { get; set; }
        public int Status { get; set; }
        public bool IsDefault { get; set; }
    }
    public class UploadProductDataModel
    {
        public List<vmSourceUrlDetail> SourceUrlDetails { get; set; }
        public List<vmVideoUrlDetail> VideoUrlDetails { get; set; }
        public int AdId { get; set; }
        public int AdSourceId { get; set; }
        public string AdSource { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategory { get; set; }
        public int MainProductId { get; set; }
        public string MainProductName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public string FbUrl { get; set; }
        public string GooglePlusUrl { get; set; }
        public string DropboxLink { get; set; }
        public string GDriveLink { get; set; }
        public string HSCode { get; set; }
        public string Keywords { get; set; }
        public string Url { get; set; }
        public string Remark { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public int DeletedBy { get; set; }
        public string ProductModelNo { get; set; }
    }
}
