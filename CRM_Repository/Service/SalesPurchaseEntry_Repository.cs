using CRM_Repository.Data;
using CRM_Repository.DataServices;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.Service
{
    public class SalesPurchaseEntry_Repository : ISalesPurchaseEntry_Repository, IDisposable
    {
        private elaunch_crmEntities context;
        public SalesPurchaseEntry_Repository(elaunch_crmEntities _context)
        {
            context = _context;
        }
        public void InsertSalePurchaseEntry(SalesPurchaseEntryMaster objsalespurchase)
        {
            try
            {
                context.SalesPurchaseEntryMasters.Add(objsalespurchase);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void UpdateSalePurchaseEntry(SalesPurchaseEntryMaster objsalespurchase)
        {
            try
            {
                context.Entry(objsalespurchase).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public int CreateUpdateSalePurchaseEntry(SalesPurchaseEntryMaster objsalespurchase)
        {
            int resVal;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    SalesPurchaseEntryMaster obj = new SalesPurchaseEntryMaster();

                    if (objsalespurchase.SalesPurchaseId <= 0)
                    {
                        obj.FinicialYearId = objsalespurchase.FinicialYearId;
                        obj.InvoiceNo = objsalespurchase.InvoiceNo;
                        obj.InvoiceDate = objsalespurchase.InvoiceDate;
                        obj.PartyType = objsalespurchase.PartyType;
                        obj.PartyId = objsalespurchase.PartyId;
                        obj.PartyName = objsalespurchase.PartyName;
                        obj.IsActive = true;
                        obj.CreatedBy = objsalespurchase.CreatedBy;
                        obj.CreatedDate = objsalespurchase.CreatedDate;
                        InsertSalePurchaseEntry(obj);
                    }
                    else
                    {
                        obj = context.SalesPurchaseEntryMasters.Find(objsalespurchase.SalesPurchaseId);
                        obj.FinicialYearId = objsalespurchase.FinicialYearId;
                        obj.InvoiceNo = objsalespurchase.InvoiceNo;
                        obj.InvoiceDate = objsalespurchase.InvoiceDate;
                        obj.PartyType = objsalespurchase.PartyType;
                        obj.PartyId = objsalespurchase.PartyId;
                        obj.PartyName = objsalespurchase.PartyName;
                        obj.IsActive = true;
                        obj.ModifyBy = objsalespurchase.ModifyBy;
                        obj.ModifyDate = objsalespurchase.ModifyDate;
                        UpdateSalePurchaseEntry(obj);
                    }
                    if (objsalespurchase.SalesPurchaseDocMasters != null)
                    {
                        //context.ProductCatalogMasters.RemoveRange(context.ProductCatalogMasters.Where(x => x.ProductId == objProductFormModel.ProductId));
                        foreach (var item in objsalespurchase.SalesPurchaseDocMasters)
                        {
                            if (item.Status == 1)
                            {
                                SalesPurchaseDocumentMaster objSalesPurchaseDoc = new SalesPurchaseDocumentMaster();
                                objSalesPurchaseDoc.DocId = item.DocId;
                                objSalesPurchaseDoc.DocPath = item.DocPath;
                                objSalesPurchaseDoc.SalesPurchaseId = obj.SalesPurchaseId;
                                context.SalesPurchaseDocumentMasters.Add(objSalesPurchaseDoc);
                            }
                            else if (item.Status == 2 || item.Status == 3)
                            {
                                SalesPurchaseDocumentMaster objSalesPurchaseDoc = context.SalesPurchaseDocumentMasters.Find(item.SalesPurchaseDocId);
                                objSalesPurchaseDoc.DocId = item.DocId;
                                objSalesPurchaseDoc.DocPath = item.DocPath;
                                objSalesPurchaseDoc.SalesPurchaseId = objsalespurchase.SalesPurchaseId;
                                context.Entry(objSalesPurchaseDoc).State = System.Data.Entity.EntityState.Modified;
                                if (item.Status == 3)
                                {
                                    context.SalesPurchaseDocumentMasters.Remove(objSalesPurchaseDoc);
                                }
                            }
                        }
                    }
                    resVal = 1;
                    context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception )
                {
                    dbContextTransaction.Rollback();
                    resVal = 0;
                }
            }
            return resVal;
        }
        public SalesPurchaseEntryMaster GetSalePurchaseEntryByID(int SalesPurchaseId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SalesPurchaseId", SalesPurchaseId);
                return new dalc().GetDataTable_Text("SELECT spe.*,fy.FinancialYear [FinancialYear] FROM SalesPurchaseEntryMaster spe with(nolock) inner join FinancialYearMaster fy WITH(nolock) on spe.FinicialYearId=fy.FinancialYearId WHERE spe.SalesPurchaseId=@SalesPurchaseId", para).ConvertToList<SalesPurchaseEntryMaster>().FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }

        }
        public IQueryable<SalesPurchaseEntryMaster> GetAllSalePurchaseEntry()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM SalesPurchaseEntryMaster with(nolock) WHERE  IsActive = 1").ConvertToList<SalesPurchaseEntryMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public IQueryable<SalesPurchaseDocumentMaster> GetSalePurchaseDocBySaleID(int SalesPurchaseId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SalesPurchaseId", SalesPurchaseId);
                return new dalc().GetDataTable_Text("SELECT spd.*,sdn.SalesDocument [DocName] FROM SalesPurchaseDocumentMaster spd with(nolock) Inner Join SalesDocumentNameMaster sdn with(nolock) on sdn.SalesDocId=spd.DocId WHERE spd.SalesPurchaseId=@SalesPurchaseId", para).ConvertToList<SalesPurchaseDocumentMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }

        }
        public int DeleteSalePurchaseEntry(SalesPurchaseEntryMaster objsalespurchase)
        {
            int resval;
            try
            {
                List<SalesPurchaseDocumentMaster> data = GetSalePurchaseDocBySaleID(objsalespurchase.SalesPurchaseId).ToList();
                if (data.Count > 0)
                {
                    foreach (var item in data)
                    {
                        SalesPurchaseDocumentMaster obj = context.SalesPurchaseDocumentMasters.Find(item.SalesPurchaseDocId);
                        context.SalesPurchaseDocumentMasters.Remove(obj);
                        context.SaveChanges();
                    }
                }
                context.Entry(objsalespurchase).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                resval = 1;
            }
            catch (Exception ex)
            {
                resval = 0;
                throw ex;
            }
            return resval;
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
