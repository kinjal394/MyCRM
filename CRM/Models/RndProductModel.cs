using CRM_Repository.Data;
using CRM_Repository.ExtendedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class RndProductModel
    {
        public int RNDProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Keyword { get; set; }
        public string EmailSpeech { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifyBy { get; set; }
        public int DeleteBy { get; set; }
        public string Cataloges { get; set; }
        public string Photoes { get; set; }
        public string Videoes { get; set; }
        //public int MainProductId { get; set; }
        //public string MainProductName { get; set; }
        public int ProductId { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        //public List<RNDSupplierMaster> SupplierList { get; set; }
    }
}