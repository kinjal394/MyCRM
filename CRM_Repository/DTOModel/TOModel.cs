using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.DTOModel
{
    public class TOItemModel
    {
        public int TOItemId { get; set; }
        public int TOId { get; set; }
        public int ProductId { get; set; }
        public int SpecId { get; set; }
        public string SpecValue { get; set; }
        public int Status { get; set; }
        public string TechSpec { get; set; }
        public string ProductName { get; set; }
        public string SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int TechParaId { get; set; }
        public string TechParaVal { get; set; }
        public string TechParaRequirment { get; set; }
        public int TechSpecifType { get; set; }    
    }

    public class Techniclspec
    {
        public int TechDetailId { get; set; }
        public int TechParaID { get; set; }
        public int TechSpecifType { get; set; }
        public string TechParaVal { get; set; }
        public int Status { get; set; }
        public int ProductId { get; set; }
        public string TechParaRequirment { get; set; }
    }


    public class Packingspec
    {
        public int TechDetailId { get; set; }
        public int TechParaID { get; set; }
        public string PackParaVal { get; set; }
        public string PackParaRequirment { get; set; }
        public int Status { get; set; }
        public int ProductId { get; set; }
        public int TechSpecifType { get; set; }
    }

    public class TechnicleDetail
    {
        public int TechDetailId { get; set; }
        public int ProductId { get; set; }
        public int TechParaID { get; set; }
        public string TechParaVal { get; set; }
        public string TechParaRequirment { get; set; }
        public int TechSpecifType { get; set; }
        public int TechHeadId { get; set; }
        public string TechHead { get; set; }
    }
         


    public class TOModel
    {
        public int TOId { get; set; }
        public int InqId { get; set; }
        public int ToTypeId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string Remark { get; set; }
        public string InqNo { get; set; }
        public string TOType { get; set; }
        public string BuyerName { get; set; }

        public List<TOItemModel> TOItemDetail { get; set; }
        public List<Techniclspec> TOSpecification { get; set; }
        public List<Packingspec> ToPacking { get; set; }
    }
}
