using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using CRM_Repository.DTOModel;
using CRM_Repository.ExtendedModel;

namespace CRM_Repository.ServiceContract
{
    public interface IInquiry_Repository : IDisposable
    {
        InquiryMaster InsertInquiry(InquiryMaster objinquiry);
        void UpdateInquiry(InquiryMaster objinquiry);
        //IQueryable<InquiryMaster> DuplicateInquiry(string Name, string Email, string Mobileno, string Requirement);
        int CreateUpdate(InquiryModel objInputInquiryModel);
        IQueryable<InquiryMaster> DuplicateEditInquiry(int InqId, string Name, string Email, string Mobileno, string Requirement);
        InquiryMaster GetInquiryId(int InqId);
        InquiryModel GetInquiryById(int id);
        string DuplicateInquiryNo();
        int GetAreaByCityId(int cityId);
        int Delete(InquiryModel objInputInquiryModel);
        IQueryable<InquiryMaster> GetAllInquiry();
        InquiryFollowupModel FetchInquiryFollowUpById(int id);
        InquiryFollowupModel GetInquiryFollowUpById(int id);
        InquiryFollowupMaster InsertInquiryFollowUp(InquiryFollowupMaster objinquiry);
        void UpdateInquiryFollowUp(InquiryFollowupMaster objinquiry);
        InquiryFollowupMaster GetInquiryFollowById(int id);
        IQueryable<InquiryFollowupModel> GetAllInquiryFollowup(string id,int usertype);

        IQueryable<InquiryMaster> GetInqGridInquiry();
        IQueryable<InquiryFollowupModel> GetInqGridFollowup(string userid);
    }
}
