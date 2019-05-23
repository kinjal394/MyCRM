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
    public class PurchaseOrderDetail_Repository : IPurchaseOrderDetail_Repository,IDisposable
    {
        dalc odal = new dalc();
        private CRM_Repository.Data.elaunch_crmEntities context;
        public PurchaseOrderDetail_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public void AddPurchaseOrderDetail(PurchaseOrderDetailMaster obj)
        {
            try
            {
                context.PurchaseOrderDetailMasters.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdatePurchaseOrderDetail(PurchaseOrderDetailMaster obj)
        {
            try
            {
                context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DeletePurchaseOrderDetail(int id)
        {
            try
            {
                PurchaseOrderDetailMaster PurchaseOrder = context.PurchaseOrderDetailMasters.Where(z => z.PoDetailId == id).SingleOrDefault();
                if (PurchaseOrder != null)
                {
                    context.PurchaseOrderDetailMasters.Remove(PurchaseOrder);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PurchaseOrderDetailMaster GetById(int id)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                {
                    var PurchaseOrderDetail = context.PurchaseOrderDetailMasters.Where(z => z.PoDetailId == id).SingleOrDefault();
                    scope.Complete();
                    return PurchaseOrderDetail;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<PurchaseOrderDetailMaster> GetAllPurchaseOrderDetail()
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var PurchaseOrderDetails = context.PurchaseOrderDetailMasters;
                //    scope.Complete();
                //    return PurchaseOrderDetails;
                //}
                return new dalc().selectbyquerydt("SELECT * FROM PurchaseOrderDetailMaster with(nolock) ").ConvertToList<PurchaseOrderDetailMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<PurchaseOrderDetailModel> GetByPurchaseOrderId(int PoId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@PoId", PoId);
                return odal.GetDataTable_Text(@"Select PO.*,UM.UnitId,UM.UnitName As QtyCodeData,CM.CurrencyId,CM.CurrencyName As PriceCodeData,PM.ProductId,PM.ProductName As Product,
                                            SC.SubCategoryId,SC.SubCategoryName As SubCategory,CC.CategoryId,CC.CategoryName As Category
                                            from PurchaseOrderDetailMaster As PO With(NOLOCK)
                                            Inner join UnitMaster As UM With(NOLOCK) On UM.UnitId = PO.QtyCode
                                            Inner join CurrencyMaster As CM With(NOLOCK) On CM.CurrencyId = PO.PriceCode
                                            Inner join ProductMaster As PM With(NOLOCK) On PM.ProductId = PO.ProductId
                                            Inner join SubCategoryMaster As SC With(NOLOCK) On SC.SubCategoryId = PM.SubCategoryId
                                            Inner join CategoryMaster As CC With(NOLOCK) On CC.CategoryId = SC.CategoryId
                                            Where PO.PoId =@PoId And ISNULL(PO.IsActive,0)=1", para).ConvertToList<PurchaseOrderDetailModel>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IQueryable<PurchaseOrderTechnicalDetailModel> GetTechDetailByPurchaseId(int PoId)
        {
            try
            {

                //return odal.selectbyquerydt(@"select PPM.TechDetailId,PPM.ProductId,PPM.TechParaId,TSM.TechSpec,PPM.Value
                //                              from  ProductParameterMaster as PPM  with(nolock)
                //                              inner join TechnicalSpecMaster TSM   with(nolock)  on TSM.SpecificationId = PPM.TechParaId
                //                              Where PPM.ProductId = " + ProductId).ConvertToList<vmProductParameterMaster>().AsQueryable();

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@PoId", PoId);
                return odal.GetDataTable_Text(@"select PPM.*,TSM.TechSpec [SpecName],T.TechHeadId As SpecHeadId,T.TechHead As SpecHead
                                                from  PurchaseOrderTechnicalDetail as PPM  with(nolock)
                                                inner join TechnicalSpecMaster TSM   with(nolock)  on TSM.SpecificationId = PPM.SpecId
                                                inner join TechnicalSpecHeadMaster T with(nolock)  on TSM.TechHeadId = T.TechHeadId
                                                Where PPM.PoDetailId =@PoId ", para).ConvertToList<PurchaseOrderTechnicalDetailModel>().AsQueryable();

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
