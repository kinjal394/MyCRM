using CRM_Repository.Data;
using CRM_Repository.DataServices;
using CRM_Repository.DTOModel;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data.SqlClient;

namespace CRM_Repository.Service
{
    public class InquiryItemDetail_Repository : IInquiryItemDetail_Repository , IDisposable
    {
        dalc odal = new dalc();
        private CRM_Repository.Data.elaunch_crmEntities context;
        public InquiryItemDetail_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddInquiryItemDetail(InquiryItemMaster obj)
        {
            try
            {
                context.InquiryItemMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void UpdateInquiryItemDetail(InquiryItemMaster obj)
        {
            try
            {
                context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void DeleteInquiryItemDetail(int id)
        {
            try
            {
                InquiryItemMaster InquiryItem = context.InquiryItemMasters.Find(id);
                if (InquiryItem != null)
                {
                    context.InquiryItemMasters.Remove(InquiryItem);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<InquiryItemModel> GetInquiryItemDetailById(int InqId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@InqId", InqId);
                return odal.GetDataTable_Text(@"Select SM.*,UM.UnitName As QtyCodeData,PM.ProductId,PM.ProductName,SC.SubCategoryId,SC.SubCategoryName As SubCategory,CC.CategoryId,CC.CategoryName As Category,MP.MainProductId,MP.MainProductName
                                            from InquiryItemMaster As SM With(NOLOCK)
                                            Inner join UnitMaster As UM With(NOLOCK) On UM.UnitId = SM.QtyCode
                                            Inner join ProductMaster As PM With(NOLOCK) On PM.ProductId = SM.ProductId
                                            Inner join MainProductMaster As MP With(NOLOCK) On MP.MainProductId = PM.MainProductId
                                            Inner join SubCategoryMaster As SC With(NOLOCK) On SC.SubCategoryId = MP.SubCategoryId
                                            Inner join CategoryMaster As CC With(NOLOCK) On CC.CategoryId = SC.CategoryId
                                            Where SM.InqId =@InqId And ISNULL(SM.IsActive,0)=1",para).ConvertToList<InquiryItemModel>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<InquiryItemMaster> GetAllInquiryItemDetail()
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var InquiryItem = context.InquiryItemMasters.Where(x => x.IsActive == true);
                //    scope.Complete();
                //    return InquiryItem;
                //}

                return new dalc().selectbyquerydt("SELECT * FROM InquiryItemMaster with(nolock) WHERE IsActive=1").ConvertToList<InquiryItemMaster>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<InquiryItemMaster> GetByInquiryId(int InqId)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var InquiryItem = context.InquiryItemMasters.Where(z => z.InqId == id && z.IsActive == true);
                //    scope.Complete();
                //    return InquiryItem;
                //}
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@InqId", InqId);
                return new dalc().GetDataTable_Text("SELECT * FROM InquiryItemMaster with(nolock) WHERE InqId=@InqId and IsActive=1", para).ConvertToList<InquiryItemMaster>().AsQueryable();
              
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
