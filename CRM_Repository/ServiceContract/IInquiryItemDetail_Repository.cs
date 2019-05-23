using CRM_Repository.Data;
using CRM_Repository.DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IInquiryItemDetail_Repository : IDisposable
    {
        void AddInquiryItemDetail(InquiryItemMaster obj);
        void UpdateInquiryItemDetail(InquiryItemMaster obj);
        void DeleteInquiryItemDetail(int id);
        IQueryable<InquiryItemModel> GetInquiryItemDetailById(int id);
        IQueryable<InquiryItemMaster> GetAllInquiryItemDetail();
        IQueryable<InquiryItemMaster> GetByInquiryId(int id);
    }
}
