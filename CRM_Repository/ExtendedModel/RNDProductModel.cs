using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.Data
{
    public partial class RNDSupplierMaster
    {
       public int Status { get; set; }
    }

    public class RNDProductModel
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
        public Nullable<int> SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int EmailSpeechId { get; set; }
        public string Title { get; set; }
        public int SMSSpeechId { get; set; }
        public string SMSTitle { get; set; }

        public string RMPhotos { get; set; }
        public string MPPhotos { get; set; }
        public string FMPhotos { get; set; }
        //public string EmailSpeech { get; set; }
        public string SMSSpeech { get; set; }
        public string ChatSpeech { get; set; }

        public List<RNDSupplierMaster> objRndSupplierList { get; set; }
    }
}
