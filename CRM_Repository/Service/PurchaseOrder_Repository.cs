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
using System.Data;

namespace CRM_Repository.Service
{
    public class PurchaseOrder_Repository : IPurchaseOrder_Repository, IDisposable
    {
        dalc odal = new dalc();
        private CRM_Repository.Data.elaunch_crmEntities context;
        private IPurchaseOrderDetail_Repository _IPurchaseOrderDetail_Repository;

        public PurchaseOrder_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
            this._IPurchaseOrderDetail_Repository = new PurchaseOrderDetail_Repository(_context);
        }

        public PurchaseOrder_Repository()
        {
            context = new CRM_Repository.Data.elaunch_crmEntities();
        }

        public PurchaseOrderMaster AddPurchaseOrder(PurchaseOrderMaster obj)
        {
            try
            {
                context.PurchaseOrderMasters.Add(obj);
                //context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdatePurchaseOrder(PurchaseOrderMaster obj)
        {
            try
            {
                context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                // context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void DeletePurchaseOrder(int id)
        {
            try
            {
                PurchaseOrderMaster PurchaseOrder = context.PurchaseOrderMasters.Find(id);
                if (PurchaseOrder != null)
                {
                    context.PurchaseOrderMasters.Remove(PurchaseOrder);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PurchaseOrderModel GetById(int PoId)
        {
            try
            {

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@PoId", PoId);
                return odal.GetDataTable_Text(@"select PO.*,BC.ContactId As BuyerContactId,D.DeliveryName As TermsCondition,p.PortName As LandingPortName,T.PortName As DischargePortName,BMC.ContactId As SupplierContactId,BM.CompanyName As BuyerComName,BMS.CompanyName As SupplierComName,CP.ComName AS CompanyName from PurchaseOrderMaster As PO 
                                            left join buyerMaster as BM on BM.BuyerId=PO.buyerId
                                            left join buyermaster as BMS on BMS.BuyerId=PO.SupplierId
                                            left join buyerContactDetail as BC on BC.BuyerId=PO.buyerId
											left join buyerContactDetail as BMC on BMC.BuyerId=PO.SupplierId
                                            left join CompanyMaster AS CP With(NOLOCK) On CP.ComId = PO.ComId
                                            left join portmaster As p on p.PortId=PO.LandingPort
											left join portmaster As T on T.PortId=PO.DischargePort
                                            left join DeliveryTermsMaster As D on D.TermsId=PO.DeliveryTermId
                                             Where PO.PoId =@PoId AND ISNULL(PO.IsActive,0)=1", para).ConvertToList<PurchaseOrderModel>().AsQueryable().FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<PurchaseOrderMaster> GetAllPurchaseOrder()
        {
            try
            {

                return new dalc().selectbyquerydt("SELECT * FROM PurchaseOrderMaster with(nolock)").ConvertToList<PurchaseOrderMaster>().AsQueryable();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public PurchaseOrderMaster CreateUpdate(PurchaseOrderModel objInputPurchaseOrder)
        {
            PurchaseOrderMaster resVal;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    PurchaseOrderMaster ObjPurchaseOrderMaster = new PurchaseOrderMaster();
                    if (objInputPurchaseOrder.PoId <= 0)
                    {
                        #region "INSERT"
                        //PoId,PoNo,PoRefNo,PoDate,SupplierId,Remark,TermsConditionId,TotalAmount,TotalTax,PayableAmount,ModeOfShipmentId,PriceCode,
                        //CreatedBy,CreatedDate,ModifyBy,ModifyDate,DeletedBy,DeletedDate,IsActive,Address,Tel,Email,Website,Attn,LandingPort,DischargePort,DeliveryTermId
                        ObjPurchaseOrderMaster.ComId = objInputPurchaseOrder.ComId;
                        ObjPurchaseOrderMaster.PoId = objInputPurchaseOrder.PoId;
                        ObjPurchaseOrderMaster.PoNo = objInputPurchaseOrder.PoNo;
                        ObjPurchaseOrderMaster.PoRefNo = objInputPurchaseOrder.PoRefNo;
                        ObjPurchaseOrderMaster.PoDate = objInputPurchaseOrder.PoDate;
                        ObjPurchaseOrderMaster.SupplierId = objInputPurchaseOrder.SupplierId;
                        ObjPurchaseOrderMaster.BuyerId = objInputPurchaseOrder.BuyerId;
                        ObjPurchaseOrderMaster.BuyerEmail = objInputPurchaseOrder.BuyerEmail;
                        ObjPurchaseOrderMaster.BuyerAddress = objInputPurchaseOrder.BuyerAddress;
                        ObjPurchaseOrderMaster.BuyerTax = objInputPurchaseOrder.BuyerTax;
                        ObjPurchaseOrderMaster.SupplierTax = objInputPurchaseOrder.SupplierTax;
                        ObjPurchaseOrderMaster.BuyerTel = objInputPurchaseOrder.BuyerTel;
                        ObjPurchaseOrderMaster.BuyerContactperson = objInputPurchaseOrder.BuyerContactperson;
                        ObjPurchaseOrderMaster.SupplierContactperson = objInputPurchaseOrder.SupplierContactperson;
                        ObjPurchaseOrderMaster.Remark = objInputPurchaseOrder.Remark;
                        ObjPurchaseOrderMaster.BuyerWebsite = objInputPurchaseOrder.BuyerWebsite;
                        if (objInputPurchaseOrder.TermsConditionId > 0)
                        {
                            ObjPurchaseOrderMaster.TermsConditionId = objInputPurchaseOrder.TermsConditionId;
                        }
                        ObjPurchaseOrderMaster.TotalAmount = objInputPurchaseOrder.TotalAmount;
                        ObjPurchaseOrderMaster.TotalTax = objInputPurchaseOrder.TotalTax;
                        ObjPurchaseOrderMaster.PayableAmount = objInputPurchaseOrder.PayableAmount;
                        ObjPurchaseOrderMaster.ModeOfShipmentId = objInputPurchaseOrder.ModeOfShipmentId;
                        ObjPurchaseOrderMaster.PriceCode = objInputPurchaseOrder.PriceCode;
                        ObjPurchaseOrderMaster.Address = objInputPurchaseOrder.Address;
                        ObjPurchaseOrderMaster.Tel = objInputPurchaseOrder.Tel;
                        ObjPurchaseOrderMaster.Email = objInputPurchaseOrder.Email;
                        ObjPurchaseOrderMaster.Website = objInputPurchaseOrder.Website;
                        if (objInputPurchaseOrder.LandingPort > 0)
                        {
                            ObjPurchaseOrderMaster.LandingPort = objInputPurchaseOrder.LandingPort;
                        }
                        if (objInputPurchaseOrder.DischargePort > 0)
                        {
                            ObjPurchaseOrderMaster.DischargePort = objInputPurchaseOrder.DischargePort;
                        }
                        if (objInputPurchaseOrder.DeliveryTermId > 0)
                        {
                            ObjPurchaseOrderMaster.DeliveryTermId = objInputPurchaseOrder.DeliveryTermId;
                        }
                        ObjPurchaseOrderMaster.CreatedDate = DateTime.Now;
                        ObjPurchaseOrderMaster.CreatedBy = objInputPurchaseOrder.CreatedBy;
                        ObjPurchaseOrderMaster.IsActive = true;
                        ObjPurchaseOrderMaster = AddPurchaseOrder(ObjPurchaseOrderMaster);

                        if (objInputPurchaseOrder.PurchaseOrderDetails != null)
                        {
                            foreach (var item in objInputPurchaseOrder.PurchaseOrderDetails)
                            {
                                PurchaseOrderDetailMaster ObjPurchaseOrderDetailMaster = new PurchaseOrderDetailMaster();
                                //ADD
                                ObjPurchaseOrderDetailMaster.PoDetailId = item.PoDetailId;
                                ObjPurchaseOrderDetailMaster.PoId = ObjPurchaseOrderMaster.PoId;
                                ObjPurchaseOrderDetailMaster.ProductId = item.ProductId;
                                ObjPurchaseOrderDetailMaster.Description = item.Description;
                                ObjPurchaseOrderDetailMaster.QtyCode = item.QtyCode;
                                ObjPurchaseOrderDetailMaster.Qty = item.Qty;
                                ObjPurchaseOrderDetailMaster.PriceCode = item.PriceCode;
                                ObjPurchaseOrderDetailMaster.UnitPrice = item.UnitPrice;
                                ObjPurchaseOrderDetailMaster.Amount = item.Amount;
                                ObjPurchaseOrderDetailMaster.IsActive = true;
                                ObjPurchaseOrderDetailMaster.ModelNo = item.ModelNo;
                                ObjPurchaseOrderDetailMaster.ProductPhotoes = item.ProductPhotoes;
                                ObjPurchaseOrderDetailMaster.MachinaryPhotoes = item.MachinaryPhotoes;
                                context.PurchaseOrderDetailMasters.Add(ObjPurchaseOrderDetailMaster);
                                if (objInputPurchaseOrder.TechSpecParameterMasters != null)
                                {
                                    foreach (var item1 in objInputPurchaseOrder.TechSpecParameterMasters)
                                    {
                                        PurchaseOrderTechnicalDetail ObjPurchaseOrderTechnicalDetail = new PurchaseOrderTechnicalDetail();
                                        //ADD
                                        if (item1.PoDetailIndex == item.PoDetailIndex)
                                        {
                                            //ObjPurchaseOrderTechnicalDetail.POSpecId = item.POSpecId;
                                            ObjPurchaseOrderTechnicalDetail.SpecId = item1.SpecId;
                                            ObjPurchaseOrderTechnicalDetail.SpecVal = item1.SpecVal;
                                            ObjPurchaseOrderTechnicalDetail.PoDetailId = ObjPurchaseOrderDetailMaster.PoDetailId;
                                            context.PurchaseOrderTechnicalDetails.Add(ObjPurchaseOrderTechnicalDetail);
                                        }
                                    }
                                }
                            }
                        }
                        resVal = ObjPurchaseOrderMaster;
                        #endregion
                    }
                    else
                    {
                        #region "EDIT"
                        //ObjPurchaseOrderMaster = GetById(objInputPurchaseOrder.PoId);
                        ObjPurchaseOrderMaster = context.PurchaseOrderMasters.Find(objInputPurchaseOrder.PoId);
                        ObjPurchaseOrderMaster.ComId = objInputPurchaseOrder.ComId;
                        ObjPurchaseOrderMaster.PoNo = objInputPurchaseOrder.PoNo;
                        ObjPurchaseOrderMaster.PoRefNo = objInputPurchaseOrder.PoRefNo;
                        ObjPurchaseOrderMaster.PoDate = objInputPurchaseOrder.PoDate;
                        ObjPurchaseOrderMaster.SupplierId = objInputPurchaseOrder.SupplierId;
                        ObjPurchaseOrderMaster.Remark = objInputPurchaseOrder.Remark;
                        ObjPurchaseOrderMaster.BuyerId = objInputPurchaseOrder.BuyerId;
                        ObjPurchaseOrderMaster.BuyerAddress = objInputPurchaseOrder.BuyerAddress;
                        ObjPurchaseOrderMaster.BuyerTel = objInputPurchaseOrder.BuyerTel;
                        ObjPurchaseOrderMaster.BuyerEmail = objInputPurchaseOrder.BuyerEmail;
                        ObjPurchaseOrderMaster.BuyerTax = objInputPurchaseOrder.BuyerTax;
                        ObjPurchaseOrderMaster.SupplierTax = objInputPurchaseOrder.SupplierTax;
                        ObjPurchaseOrderMaster.BuyerContactperson = objInputPurchaseOrder.BuyerContactperson;
                        ObjPurchaseOrderMaster.SupplierContactperson = objInputPurchaseOrder.SupplierContactperson;
                        ObjPurchaseOrderMaster.BuyerWebsite = objInputPurchaseOrder.BuyerWebsite;

                        if (objInputPurchaseOrder.TermsConditionId > 0)
                        {
                            ObjPurchaseOrderMaster.TermsConditionId = objInputPurchaseOrder.TermsConditionId;
                        }
                        ObjPurchaseOrderMaster.TotalAmount = objInputPurchaseOrder.TotalAmount;
                        ObjPurchaseOrderMaster.TotalTax = objInputPurchaseOrder.TotalTax;
                        ObjPurchaseOrderMaster.PayableAmount = objInputPurchaseOrder.PayableAmount;
                        ObjPurchaseOrderMaster.ModeOfShipmentId = objInputPurchaseOrder.ModeOfShipmentId;
                        ObjPurchaseOrderMaster.PriceCode = objInputPurchaseOrder.PriceCode;
                        ObjPurchaseOrderMaster.Address = objInputPurchaseOrder.Address;
                        ObjPurchaseOrderMaster.Tel = objInputPurchaseOrder.Tel;
                        ObjPurchaseOrderMaster.Email = objInputPurchaseOrder.Email;
                        ObjPurchaseOrderMaster.Website = objInputPurchaseOrder.Website;
                        if (objInputPurchaseOrder.LandingPort > 0)
                        {
                            ObjPurchaseOrderMaster.LandingPort = objInputPurchaseOrder.LandingPort;
                        }
                        if (objInputPurchaseOrder.DischargePort > 0)
                        {
                            ObjPurchaseOrderMaster.DischargePort = objInputPurchaseOrder.DischargePort;
                        }
                        if (objInputPurchaseOrder.DeliveryTermId > 0)
                        {
                            ObjPurchaseOrderMaster.DeliveryTermId = objInputPurchaseOrder.DeliveryTermId;
                        }
                        ObjPurchaseOrderMaster.ModifyDate = DateTime.Now;
                        ObjPurchaseOrderMaster.ModifyBy = objInputPurchaseOrder.ModifyBy;
                        UpdatePurchaseOrder(ObjPurchaseOrderMaster);

                        if (objInputPurchaseOrder.PurchaseOrderDetails != null)
                        {
                            foreach (var item in objInputPurchaseOrder.PurchaseOrderDetails)
                            {

                                PurchaseOrderDetailMaster ObjPurchaseOrderDetailMaster = new PurchaseOrderDetailMaster();
                                if (item.Status == 1)
                                {
                                    //ADD
                                    ObjPurchaseOrderDetailMaster.PoDetailId = item.PoDetailId;
                                    ObjPurchaseOrderDetailMaster.PoId = objInputPurchaseOrder.PoId;
                                    ObjPurchaseOrderDetailMaster.ProductId = item.ProductId;
                                    ObjPurchaseOrderDetailMaster.Description = item.Description;
                                    ObjPurchaseOrderDetailMaster.QtyCode = item.QtyCode;
                                    ObjPurchaseOrderDetailMaster.Qty = item.Qty;
                                    ObjPurchaseOrderDetailMaster.PriceCode = item.PriceCode;
                                    ObjPurchaseOrderDetailMaster.UnitPrice = item.UnitPrice;
                                    ObjPurchaseOrderDetailMaster.Amount = item.Amount;
                                    ObjPurchaseOrderDetailMaster.IsActive = true;
                                    ObjPurchaseOrderDetailMaster.ModelNo = item.ModelNo;
                                    ObjPurchaseOrderDetailMaster.ProductPhotoes = item.ProductPhotoes;
                                    ObjPurchaseOrderDetailMaster.MachinaryPhotoes = item.MachinaryPhotoes;
                                    context.PurchaseOrderDetailMasters.Add(ObjPurchaseOrderDetailMaster);
                                    //  _IPurchaseOrderDetail_Repository.AddPurchaseOrderDetail(ObjPurchaseOrderDetailMaster);
                                }
                                else if (item.Status == 2 || item.Status == 3)
                                {
                                    ObjPurchaseOrderDetailMaster = context.PurchaseOrderDetailMasters.Find(item.PoDetailId);
                                    ObjPurchaseOrderDetailMaster.PoDetailId = item.PoDetailId;
                                    ObjPurchaseOrderDetailMaster.PoId = objInputPurchaseOrder.PoId;
                                    ObjPurchaseOrderDetailMaster.ProductId = item.ProductId;
                                    ObjPurchaseOrderDetailMaster.Description = item.Description;
                                    ObjPurchaseOrderDetailMaster.QtyCode = item.QtyCode;
                                    ObjPurchaseOrderDetailMaster.Qty = item.Qty;
                                    ObjPurchaseOrderDetailMaster.PriceCode = item.PriceCode;
                                    ObjPurchaseOrderDetailMaster.UnitPrice = item.UnitPrice;
                                    ObjPurchaseOrderDetailMaster.Amount = item.Amount;
                                    ObjPurchaseOrderDetailMaster.ModelNo = item.ModelNo;
                                    ObjPurchaseOrderDetailMaster.ProductPhotoes = item.ProductPhotoes;
                                    ObjPurchaseOrderDetailMaster.MachinaryPhotoes = item.MachinaryPhotoes;
                                    if (item.Status == 3)
                                        ObjPurchaseOrderDetailMaster.IsActive = false;
                                    context.Entry(ObjPurchaseOrderDetailMaster).State = System.Data.Entity.EntityState.Modified;
                                }
                                if (objInputPurchaseOrder.TechSpecParameterMasters != null)
                                {
                                    foreach (var item1 in objInputPurchaseOrder.TechSpecParameterMasters)
                                    {

                                        if (item1.Status == 1)
                                        {
                                            PurchaseOrderTechnicalDetail ObjPurchaseOrderTechnicalDetail = new PurchaseOrderTechnicalDetail();
                                            //ADD
                                            if (item1.PoDetailIndex == item.PoDetailIndex)
                                            {
                                                //ObjPurchaseOrderTechnicalDetail.POSpecId = item.POSpecId;
                                                ObjPurchaseOrderTechnicalDetail.SpecId = item1.SpecId;
                                                ObjPurchaseOrderTechnicalDetail.SpecVal = item1.SpecVal;
                                                ObjPurchaseOrderTechnicalDetail.PoDetailId = ObjPurchaseOrderDetailMaster.PoDetailId;
                                                context.PurchaseOrderTechnicalDetails.Add(ObjPurchaseOrderTechnicalDetail);
                                            }
                                            //  _IPurchaseOrderDetail_Repository.AddPurchaseOrderDetail(ObjPurchaseOrderDetailMaster);
                                        }
                                        else if (item1.Status == 2 || item1.Status == 3)
                                        {
                                            PurchaseOrderTechnicalDetail ObjPurchaseOrderTechnicalDetail = context.PurchaseOrderTechnicalDetails.Find(item1.POSpecId);
                                            //ADD
                                            if (item1.PoDetailIndex == item.PoDetailIndex)
                                            {
                                                //ObjPurchaseOrderTechnicalDetail.POSpecId = item.POSpecId;
                                                ObjPurchaseOrderTechnicalDetail.SpecId = item1.SpecId;
                                                ObjPurchaseOrderTechnicalDetail.SpecVal = item1.SpecVal;
                                                ObjPurchaseOrderTechnicalDetail.PoDetailId = ObjPurchaseOrderDetailMaster.PoDetailId;
                                                context.Entry(ObjPurchaseOrderTechnicalDetail).State = System.Data.Entity.EntityState.Modified;
                                                if (item1.Status == 3)
                                                {
                                                    context.PurchaseOrderTechnicalDetails.Remove(ObjPurchaseOrderTechnicalDetail);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        resVal = ObjPurchaseOrderMaster;
                        #endregion
                    }
                    context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {

                    ex.SetLog("Purchase Order.");
                    dbContextTransaction.Rollback();
                    resVal = null;
                }
            }
            return resVal;
        }

        public int Delete(PurchaseOrderModel objInputPurchaseOrder)
        {
            int resVal;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    PurchaseOrderMaster ObjPurchaseOrderMaster = context.PurchaseOrderMasters.Find(objInputPurchaseOrder.PoId);
                    ObjPurchaseOrderMaster.IsActive = false;
                    ObjPurchaseOrderMaster.DeletedBy = objInputPurchaseOrder.DeleteBy;
                    ObjPurchaseOrderMaster.DeletedDate = DateTime.Now;
                    UpdatePurchaseOrder(ObjPurchaseOrderMaster);

                    List<PurchaseOrderDetailMaster> ObjPurchaseOrderDetailMaster = context.PurchaseOrderDetailMasters.Where(x => x.PoId == objInputPurchaseOrder.PoId).ToList();
                    if (ObjPurchaseOrderDetailMaster.Count > 0)
                    {
                        foreach (var item in ObjPurchaseOrderDetailMaster)
                        {
                            PurchaseOrderDetailMaster ObjPurchaseOrderDetail = context.PurchaseOrderDetailMasters.Find(item.PoDetailId);
                            item.IsActive = false;
                            context.Entry(ObjPurchaseOrderDetail).State = System.Data.Entity.EntityState.Modified;
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
        public DataSet GetPurchaseOrderReport(ReportPara obj)
        {
            dalc odal = new dalc();
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
            ds = odal.GetDataset("GetPurchaseOrder_Report", para);
            ds.Tables[0].TableName = "PurchaseOrder";
            return ds;

        }
        public System.Data.DataSet GetPurchaseOrderReportData(ReportPara obj)
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
            ds = odal.GetDataset("PurchaseOrder_Report", para);
            ds.Tables[0].TableName = "PurchaseOrderMain";
            ds.Tables[1].TableName = "PurchaseOrderItems";
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
