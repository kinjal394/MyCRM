using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using CRM_Repository.DTOModel;
using CRM_Repository.DataServices;


using System.Data.SqlClient;
using System.Data;

namespace CRM_Repository.Service
{
    public class SalesOrder_Repository : ISalesOrder_Repository, IDisposable
    {
        dalc odal = new dalc();
        private CRM_Repository.Data.elaunch_crmEntities context;
        public SalesOrder_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public SalesOrder_Repository()
        {
            context = new CRM_Repository.Data.elaunch_crmEntities();
        }

        public SalesOrderMaster AddSalesOrder(SalesOrderMaster obj)
        {
            try
            {
                context.SalesOrderMasters.Add(obj);
                //context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void UpdateSalesOrder(SalesOrderMaster obj)
        {
            try
            {
                context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                //context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void DeleteSalesOrder(int id)
        {
            try
            {
                SalesOrderMaster SalesOrder = context.SalesOrderMasters.Find(id);
                if (SalesOrder != null)
                {
                    context.SalesOrderMasters.Remove(SalesOrder);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public SalesOrderModel GetSalesOrderById(int SOId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SOId", SOId);
                return odal.GetDataTable_Text(@"Select So.*,BM.BuyerId,BM.CompanyName,
                                            BC.ContactId,BC.ContactPerson,BC.Email As BEmail,CM.ComId As CompanyId,CM.ComName As CompanyName,CM.RegOffAdd,
                                            DT.TermsId,DT.DeliveryName,PT.PaymentTermId,PT.TermName,CR.CurrencyId,CR.CurrencyName
                                            from SalesOrderMaster As SO With(NOLOCK)
                                            Inner join BuyerMaster As BM With(NOLOCK) On BM.BuyerId = SO.BuyerId
                                            Inner join BuyerContactDetail As BC With(NOLOCK) On BC.BuyerId = BM.BuyerId AND BC.ContactId = SO.BuyerContactId
                                            Inner join CompanyMaster As CM With(NOLOCK) On CM.ComId = SO.CompanyId
                                            Inner join DeliveryTermsMaster As DT With(NOLOCK) On DT.TermsId = SO.DeliveryTermId
                                            Inner join PaymentTermsMaster As PT With(NOLOCK) On PT.PaymentTermId = SO.PaymentTermId
                                            Inner join CurrencyMaster As CR With(NOLOCK) On CR.CurrencyId = SO.TotalAmountCode
                                            Where So.SOId =@SOId And ISNULL(SO.IsActive,0)=1",para).ConvertToList<SalesOrderModel>().AsQueryable().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public SalesOrderMaster GetSOById(int id)
        {
            try
            {
                
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SOId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM SalesOrderMaster with(nolock) WHERE SOId=@SOId ", para).ConvertToList<SalesOrderMaster>().FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<SalesOrderMaster> GetAllSalesOrder()
        {
            try
            {
                             
                return new dalc().selectbyquerydt("SELECT * FROM SalesOrderMaster with(nolock) WHERE IsActive = 1 ORDER BY SOId Desc").ConvertToList<SalesOrderMaster>().AsQueryable();

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public int CreateUpdate(SalesOrderModel objInputSalesOrderMaster)
        {
            int resVal;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    SalesOrderMaster ObjSalesOrderMaster = new SalesOrderMaster();
                    if (objInputSalesOrderMaster.SOId <= 0)
                    {
                        //ADD
                        ObjSalesOrderMaster.SoNo = objInputSalesOrderMaster.SoNo;
                        ObjSalesOrderMaster.SoRefNo = objInputSalesOrderMaster.SoRefNo;
                        ObjSalesOrderMaster.SoDate = objInputSalesOrderMaster.SoDate;
                        ObjSalesOrderMaster.CompanyId = objInputSalesOrderMaster.CompanyId;
                        ObjSalesOrderMaster.BuyerId = objInputSalesOrderMaster.BuyerId;
                        ObjSalesOrderMaster.BuyerContactId = objInputSalesOrderMaster.BuyerContactId;
                        ObjSalesOrderMaster.Remark = objInputSalesOrderMaster.Remark;
                        ObjSalesOrderMaster.TotalAmount = objInputSalesOrderMaster.TotalAmount;
                        ObjSalesOrderMaster.TotalAmountCode = objInputSalesOrderMaster.TotalAmountCode;
                        ObjSalesOrderMaster.DeliveryTermId = objInputSalesOrderMaster.DeliveryTermId;
                        ObjSalesOrderMaster.PaymentTermId = objInputSalesOrderMaster.PaymentTermId;
                        ObjSalesOrderMaster.CreatedBy = objInputSalesOrderMaster.CreatedBy;
                        ObjSalesOrderMaster.CreatedDate = DateTime.Now;
                        ObjSalesOrderMaster.IsActive = true;
                        ObjSalesOrderMaster = AddSalesOrder(ObjSalesOrderMaster);
                        if (objInputSalesOrderMaster.SalesItemDetails != null)
                        {
                            foreach (var item in objInputSalesOrderMaster.SalesItemDetails)
                            {
                                SalesItemMaster ObjSalesItemDetail = new SalesItemMaster();
                                //ADD
                                ObjSalesItemDetail.SOId = ObjSalesOrderMaster.SOId;
                                ObjSalesItemDetail.ProductId = item.ProductId;
                                ObjSalesItemDetail.ProductDescription = item.ProductDescription;
                                ObjSalesItemDetail.ModelNo = item.ModelNo;
                                ObjSalesItemDetail.CountryOfOriginId = item.OriginId;
                                ObjSalesItemDetail.QtyCode = item.QtyCode;
                                ObjSalesItemDetail.Qty = item.Qty;
                                ObjSalesItemDetail.UnitPriceCode = item.UnitPriceCode;
                                ObjSalesItemDetail.UnitPrice = item.UnitPrice;
                                ObjSalesItemDetail.IsActive = true;
                                context.SalesItemMasters.Add(ObjSalesItemDetail);

                                if (item.SalesTechnicalDetails != null)
                                {
                                    foreach (var subitem in item.SalesTechnicalDetails)
                                    {
                                        if (subitem.Status == 1)
                                        {
                                            SalesTechnicalDetailMaster ObjSalesTechnicalDetail = new SalesTechnicalDetailMaster();
                                            ObjSalesTechnicalDetail.ItemId = ObjSalesItemDetail.ItemId;
                                            ObjSalesTechnicalDetail.TechParaId = subitem.TechParaId;
                                            ObjSalesTechnicalDetail.Value = subitem.Value;
                                            ObjSalesTechnicalDetail.IsActive = true;
                                            context.SalesTechnicalDetailMasters.Add(ObjSalesTechnicalDetail);
                                        }
                                    }
                                }
                            }
                        }
                        resVal = 1;
                    }
                    else
                    {

                        //EDIT
                        //ObjSalesOrderMaster = GetSalesOrderById(objInputSalesOrderMaster.SOId);
                        ObjSalesOrderMaster = context.SalesOrderMasters.Find(objInputSalesOrderMaster.SOId);
                        ObjSalesOrderMaster.SoNo = objInputSalesOrderMaster.SoNo;
                        ObjSalesOrderMaster.SoRefNo = objInputSalesOrderMaster.SoRefNo;
                        ObjSalesOrderMaster.SoDate = objInputSalesOrderMaster.SoDate;
                        ObjSalesOrderMaster.CompanyId = objInputSalesOrderMaster.CompanyId;
                        ObjSalesOrderMaster.BuyerId = objInputSalesOrderMaster.BuyerId;
                        ObjSalesOrderMaster.BuyerContactId = objInputSalesOrderMaster.BuyerContactId;
                        ObjSalesOrderMaster.Remark = objInputSalesOrderMaster.Remark;
                        ObjSalesOrderMaster.TotalAmount = objInputSalesOrderMaster.TotalAmount;
                        ObjSalesOrderMaster.TotalAmountCode = objInputSalesOrderMaster.TotalAmountCode;
                        ObjSalesOrderMaster.DeliveryTermId = objInputSalesOrderMaster.DeliveryTermId;
                        ObjSalesOrderMaster.PaymentTermId = objInputSalesOrderMaster.PaymentTermId;
                        ObjSalesOrderMaster.ModifyBy = objInputSalesOrderMaster.ModifyBy;
                        ObjSalesOrderMaster.ModifyDate = DateTime.Now;
                        UpdateSalesOrder(ObjSalesOrderMaster);

                        if (objInputSalesOrderMaster.SalesItemDetails != null)
                        {
                            foreach (var item in objInputSalesOrderMaster.SalesItemDetails)
                            {
                                if (item.Status == 1)
                                {
                                    SalesItemMaster ObjSalesItemDetail = new SalesItemMaster();
                                    //ADD
                                    ObjSalesItemDetail.SOId = ObjSalesOrderMaster.SOId;
                                    ObjSalesItemDetail.ProductId = item.ProductId;
                                    ObjSalesItemDetail.ProductDescription = item.ProductDescription;
                                    ObjSalesItemDetail.ModelNo = item.ModelNo;
                                    ObjSalesItemDetail.CountryOfOriginId = item.OriginId;
                                    ObjSalesItemDetail.QtyCode = item.QtyCode;
                                    ObjSalesItemDetail.Qty = item.Qty;
                                    ObjSalesItemDetail.UnitPriceCode = item.UnitPriceCode;
                                    ObjSalesItemDetail.UnitPrice = item.UnitPrice;
                                    ObjSalesItemDetail.IsActive = true;
                                    context.SalesItemMasters.Add(ObjSalesItemDetail);

                                    if (item.SalesTechnicalDetails != null)
                                    {
                                        foreach (var subitem in item.SalesTechnicalDetails)
                                        {
                                            if (item.ItemId == subitem.ItemId)
                                            {
                                                if (subitem.Status == 1)
                                                {
                                                    SalesTechnicalDetailMaster ObjSalesTechnicalDetail = new SalesTechnicalDetailMaster();
                                                    ObjSalesTechnicalDetail.ItemId = ObjSalesItemDetail.ItemId;
                                                    ObjSalesTechnicalDetail.TechParaId = subitem.TechParaId;
                                                    ObjSalesTechnicalDetail.Value = subitem.Value;
                                                    ObjSalesTechnicalDetail.IsActive = true;
                                                    context.SalesTechnicalDetailMasters.Add(ObjSalesTechnicalDetail);
                                                }
                                                else if (subitem.Status == 2)
                                                {
                                                    SalesTechnicalDetailMaster ObjSalesTechnicalDetail = context.SalesTechnicalDetailMasters.Where(x => x.ItemId == ObjSalesItemDetail.ItemId && x.TechDetailId == subitem.TechDetailId && x.IsActive == true).SingleOrDefault();
                                                    ObjSalesTechnicalDetail.ItemId = ObjSalesItemDetail.ItemId;
                                                    ObjSalesTechnicalDetail.TechParaId = subitem.TechParaId;
                                                    ObjSalesTechnicalDetail.Value = subitem.Value;
                                                    ObjSalesTechnicalDetail.IsActive = true;
                                                    context.Entry(ObjSalesTechnicalDetail).State = System.Data.Entity.EntityState.Modified;
                                                }
                                                else if (subitem.Status == 3)
                                                {
                                                    SalesTechnicalDetailMaster ObjSalesTechnicalDetail = context.SalesTechnicalDetailMasters.Where(x => x.ItemId == ObjSalesItemDetail.ItemId && x.TechDetailId == subitem.TechDetailId && x.IsActive == true).SingleOrDefault();
                                                    context.SalesTechnicalDetailMasters.Remove(ObjSalesTechnicalDetail);
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (item.Status == 2 || item.Status == 3)
                                {
                                    SalesItemMaster ObjSalesItemDetail = context.SalesItemMasters.Find(item.ItemId);
                                    ObjSalesItemDetail.SOId = ObjSalesOrderMaster.SOId;
                                    ObjSalesItemDetail.ProductId = item.ProductId;
                                    ObjSalesItemDetail.ProductDescription = item.ProductDescription;
                                    ObjSalesItemDetail.ModelNo = item.ModelNo;
                                    ObjSalesItemDetail.CountryOfOriginId = item.OriginId;
                                    ObjSalesItemDetail.QtyCode = item.QtyCode;
                                    ObjSalesItemDetail.Qty = item.Qty;
                                    ObjSalesItemDetail.UnitPriceCode = item.UnitPriceCode;
                                    ObjSalesItemDetail.UnitPrice = item.UnitPrice;
                                    if (item.Status == 3)
                                        ObjSalesItemDetail.IsActive = false;
                                    context.Entry(ObjSalesItemDetail).State = System.Data.Entity.EntityState.Modified;

                                    if (item.SalesTechnicalDetails != null)
                                    {
                                        foreach (var subitem in item.SalesTechnicalDetails)
                                        {
                                            if (item.ItemId == subitem.ItemId)
                                            {
                                                if (item.Status == 3)
                                                {
                                                    SalesTechnicalDetailMaster ObjSalesTechnicalDetail = context.SalesTechnicalDetailMasters.Where(x => x.ItemId == ObjSalesItemDetail.ItemId && x.TechDetailId == subitem.TechDetailId && x.IsActive == true).SingleOrDefault();
                                                    context.SalesTechnicalDetailMasters.Remove(ObjSalesTechnicalDetail);
                                                }
                                                else
                                                {
                                                    if (subitem.Status == 1)
                                                    {
                                                        SalesTechnicalDetailMaster ObjSalesTechnicalDetail = new SalesTechnicalDetailMaster();
                                                        ObjSalesTechnicalDetail.ItemId = ObjSalesItemDetail.ItemId;
                                                        ObjSalesTechnicalDetail.TechParaId = subitem.TechParaId;
                                                        ObjSalesTechnicalDetail.Value = subitem.Value;
                                                        ObjSalesTechnicalDetail.IsActive = true;
                                                        context.SalesTechnicalDetailMasters.Add(ObjSalesTechnicalDetail);
                                                    }
                                                    else if (subitem.Status == 2)
                                                    {
                                                        SalesTechnicalDetailMaster ObjSalesTechnicalDetail = context.SalesTechnicalDetailMasters.Where(x => x.ItemId == ObjSalesItemDetail.ItemId && x.TechDetailId == subitem.TechDetailId && x.IsActive == true).SingleOrDefault();
                                                        ObjSalesTechnicalDetail.ItemId = ObjSalesItemDetail.ItemId;
                                                        ObjSalesTechnicalDetail.TechParaId = subitem.TechParaId;
                                                        ObjSalesTechnicalDetail.Value = subitem.Value;
                                                        ObjSalesTechnicalDetail.IsActive = true;
                                                        context.Entry(ObjSalesTechnicalDetail).State = System.Data.Entity.EntityState.Modified;
                                                    }
                                                    else if (subitem.Status == 3)
                                                    {
                                                        SalesTechnicalDetailMaster ObjSalesTechnicalDetail = context.SalesTechnicalDetailMasters.Where(x => x.ItemId == ObjSalesItemDetail.ItemId && x.TechDetailId == subitem.TechDetailId && x.IsActive == true).SingleOrDefault();
                                                        context.SalesTechnicalDetailMasters.Remove(ObjSalesTechnicalDetail);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        resVal = 2;
                    }
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

        public int Delete(SalesOrderModel objInputSalesOrderMaster)
        {
            int resVal;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    SalesOrderMaster objSalesOrderMaster = context.SalesOrderMasters.Find(objInputSalesOrderMaster.SOId);
                    objSalesOrderMaster.IsActive = false;
                    objSalesOrderMaster.DeletedBy = objInputSalesOrderMaster.DeleteBy;
                    objSalesOrderMaster.DeletedDate = DateTime.Now;
                    UpdateSalesOrder(objSalesOrderMaster);

                    List<SalesItemMaster> ObjSalesItemDetail = context.SalesItemMasters.Where(x => x.SOId == objInputSalesOrderMaster.SOId).ToList();
                    if (ObjSalesItemDetail.Count > 0)
                    {
                        foreach (var item in ObjSalesItemDetail)
                        {
                            SalesItemMaster ObjSalesItemSingle = context.SalesItemMasters.Find(item.ItemId);
                            item.IsActive = false;
                            context.Entry(ObjSalesItemSingle).State = System.Data.Entity.EntityState.Modified;

                            List<SalesTechnicalDetailMaster> ObjSalesTechnicalDetail = context.SalesTechnicalDetailMasters.Where(x => x.ItemId == item.ItemId && x.IsActive == true).ToList();
                            if (ObjSalesTechnicalDetail.Count > 0)
                            {
                                foreach (var subitem in ObjSalesTechnicalDetail)
                                {
                                    if (item.ItemId == subitem.ItemId)
                                    {
                                        SalesTechnicalDetailMaster ObjSalesTechnical = context.SalesTechnicalDetailMasters.Where(x => x.ItemId == item.ItemId && x.TechDetailId == subitem.TechDetailId && x.IsActive == true).SingleOrDefault();
                                        context.SalesTechnicalDetailMasters.Remove(ObjSalesTechnical);
                                    }
                                }
                            }
                        }
                    }
                    resVal = 1;
                    context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    resVal = 0;
                }
            }
            return resVal;
        }


        public System.Data.DataSet GetSaleOrderReportData(ReportPara obj)
        {
            DataSet ds = new DataSet();
            int ID = 0;
            try
            {
                ID = Convert.ToInt32(obj.ID);
            }
            catch
            {
                ID = 0;
            }
            SqlParameter[] para = new SqlParameter[9];
            para[0] = new SqlParameter().CreateParameter("@ID", ID);
            para[1] = new SqlParameter().CreateParameter("@UserId", obj.UserId);
            para[2] = new SqlParameter().CreateParameter("@UserType", obj.UserType);
            para[3] = new SqlParameter().CreateParameter("@A", obj.A);
            para[4] = new SqlParameter().CreateParameter("@B", obj.B);
            para[5] = new SqlParameter().CreateParameter("@C", obj.C);
            para[6] = new SqlParameter().CreateParameter("@D", obj.D);
            para[7] = new SqlParameter().CreateParameter("@E", obj.E);
            para[8] = new SqlParameter().CreateParameter("@F", obj.F);
            ds = odal.GetDataset("SaleOrder_Report", para);
            ds.Tables[0].TableName = "SalesOrderMain";
            ds.Tables[1].TableName = "SalesItemDetails";
            return ds;

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
