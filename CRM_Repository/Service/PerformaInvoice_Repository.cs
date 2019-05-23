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
    public class PerformaInvoice_Repository : IPerformaInvoice_Repository, IDisposable
    {
        dalc odal = new dalc();
        private CRM_Repository.Data.elaunch_crmEntities context;
        public PerformaInvoice_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public PerformaInvoice_Repository()
        {
            context = new CRM_Repository.Data.elaunch_crmEntities();
        }

        public PerformaInvoiceMaster AddPerformaInvoice(PerformaInvoiceMaster PIR)
        {
            try
            {
                context.PerformaInvoiceMasters.Add(PIR);
                context.SaveChanges();
                return PIR;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public void UpdatePerformaInvoice(PerformaInvoiceMaster PIM)
        {
            try
            {
                context.Entry(PIM).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public void DeletePerformaInvoice(int id, int UserId)
        {
            try
            {
                PerformaInvoiceMaster Cat = context.PerformaInvoiceMasters.Single(u => u.PerformaInvId == id);
                Cat.DeletedBy = UserId;
                Cat.IsActive = false;
                Cat.DeletedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                context.Entry(Cat).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public PerformaInvoiceMaster GetPerformaInvoiceById(int Id)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var data = context.PerformaInvoiceMasters.Find(Id);
                //    scope.Complete();
                //    return data;
                //}

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@PerformaInvId", Id);
                return new dalc().GetDataTable_Text("SELECT * FROM PerformaInvoiceMaster with(nolock) WHERE PerformaInvId=@PerformaInvId", para).ConvertToList<PerformaInvoiceMaster>().FirstOrDefault();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<PerformaInvoiceMaster> GetAllPerformaInvoice()
        {
            try
            {
                //return context.PerformaInvoiceMasters.OrderByDescending(x => x.PerformaInvId);
                return new dalc().selectbyquerydt("SELECT * FROM PerformaInvoiceMaster with(nolock) WHERE  IsActive = 1 Order By PerformaInvId DESC").ConvertToList<PerformaInvoiceMaster>().AsQueryable();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public PerformaInvoiceMaster CreateUpdate(PerformaInvoiceMaster objInputPerforma)
        {
            PerformaInvoiceMaster resVal;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    PerformaInvoiceMaster ObjPerformaInvoice = new PerformaInvoiceMaster();
                    if (objInputPerforma.PerformaInvId <= 0)
                    {
                        #region "INSERT"
                        ObjPerformaInvoice.PerformaInvId = objInputPerforma.PerformaInvId;
                        ObjPerformaInvoice.PerformaInvNo = objInputPerforma.PerformaInvNo;
                        ObjPerformaInvoice.PerformaInvDate = objInputPerforma.PerformaInvDate;
                        ObjPerformaInvoice.RptFormatId = objInputPerforma.RptFormatId;
                        ObjPerformaInvoice.DeliveryTermId = objInputPerforma.DeliveryTermId;
                        ObjPerformaInvoice.PaymentTermId = objInputPerforma.PaymentTermId;
                        ObjPerformaInvoice.ModeOfShipmentId = objInputPerforma.ModeOfShipmentId;
                        ObjPerformaInvoice.LoadingPortId = objInputPerforma.LoadingPortId;
                        ObjPerformaInvoice.DischargePortId = objInputPerforma.DischargePortId;
                        ObjPerformaInvoice.ShippingMarks = objInputPerforma.ShippingMarks;
                        ObjPerformaInvoice.ConsigneId = objInputPerforma.ConsigneId;
                        ObjPerformaInvoice.ConsigneName = objInputPerforma.ConsigneName;
                        ObjPerformaInvoice.ConsigneAddress = objInputPerforma.ConsigneAddress;
                        ObjPerformaInvoice.ConsigneEmail = objInputPerforma.ConsigneEmail;
                        ObjPerformaInvoice.ConsigneTel = objInputPerforma.ConsigneTel;
                        ObjPerformaInvoice.ConsigneTax = objInputPerforma.ConsigneTax;
                        ObjPerformaInvoice.ContactName = objInputPerforma.ContactName;
                        ObjPerformaInvoice.ContactTel = objInputPerforma.ContactTel;
                        ObjPerformaInvoice.Contactmail = objInputPerforma.Contactmail;
                        ObjPerformaInvoice.BankNameId = objInputPerforma.BankNameId;
                        ObjPerformaInvoice.AccountTypeId = objInputPerforma.AccountTypeId;
                        ObjPerformaInvoice.BeneficiaryName = objInputPerforma.BeneficiaryName;
                        ObjPerformaInvoice.BranchName = objInputPerforma.BranchName;
                        ObjPerformaInvoice.BankAddress = objInputPerforma.BankAddress;
                        ObjPerformaInvoice.AccountNo = objInputPerforma.AccountNo;
                        ObjPerformaInvoice.IFSCCode = objInputPerforma.IFSCCode;
                        ObjPerformaInvoice.SwiftCode = objInputPerforma.SwiftCode;
                        ObjPerformaInvoice.CreatedBy = objInputPerforma.CreatedBy;
                        ObjPerformaInvoice.CreatedDate = DateTime.Now;
                        ObjPerformaInvoice.IsActive = true;
                        ObjPerformaInvoice = AddPerformaInvoice(ObjPerformaInvoice);

                        if (objInputPerforma.PerformaProductMasters != null)
                        {
                            foreach (var item in objInputPerforma.PerformaProductMasters)
                            {
                                PerformaProductMaster ObjPerformaProduct = new PerformaProductMaster();
                                ObjPerformaProduct.PerformaInvId = ObjPerformaInvoice.PerformaInvId;
                                ObjPerformaProduct.ProductId = item.ProductId;
                                ObjPerformaProduct.ProductCode = item.ProductCode;
                                ObjPerformaProduct.ProductModelNo = item.ProductModelNo;
                                ObjPerformaProduct.CountryOfOriginId = item.CountryOfOriginId;
                                ObjPerformaProduct.ProductDescription = item.ProductDescription;
                                ObjPerformaProduct.QtyCode = item.QtyCode;
                                ObjPerformaProduct.Qty = item.Qty;
                                ObjPerformaProduct.CurrencyCode = item.CurrencyCode;
                                ObjPerformaProduct.DealerPrice = item.DealerPrice;
                                ObjPerformaProduct.AddPerValue = item.AddPerValue;
                                ObjPerformaProduct.OfferPrice = item.OfferPrice;
                                ObjPerformaProduct.IsActive = true;
                                context.PerformaProductMasters.Add(ObjPerformaProduct);
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region "UPDATE"
                        //EDIT
                        ObjPerformaInvoice = context.PerformaInvoiceMasters.Find(objInputPerforma.PerformaInvId);
                        ObjPerformaInvoice.PerformaInvNo = objInputPerforma.PerformaInvNo;
                        ObjPerformaInvoice.PerformaInvDate = objInputPerforma.PerformaInvDate;
                        ObjPerformaInvoice.RptFormatId = objInputPerforma.RptFormatId;
                        ObjPerformaInvoice.DeliveryTermId = objInputPerforma.DeliveryTermId;
                        ObjPerformaInvoice.PaymentTermId = objInputPerforma.PaymentTermId;
                        ObjPerformaInvoice.ModeOfShipmentId = objInputPerforma.ModeOfShipmentId;
                        ObjPerformaInvoice.LoadingPortId = objInputPerforma.LoadingPortId;
                        ObjPerformaInvoice.DischargePortId = objInputPerforma.DischargePortId;
                        ObjPerformaInvoice.ShippingMarks = objInputPerforma.ShippingMarks;
                        ObjPerformaInvoice.ConsigneId = objInputPerforma.ConsigneId;
                        ObjPerformaInvoice.ConsigneName = objInputPerforma.ConsigneName;
                        ObjPerformaInvoice.ConsigneAddress = objInputPerforma.ConsigneAddress;
                        ObjPerformaInvoice.ConsigneEmail = objInputPerforma.ConsigneEmail;
                        ObjPerformaInvoice.ConsigneTel = objInputPerforma.ConsigneTel;
                        ObjPerformaInvoice.ConsigneTax = objInputPerforma.ConsigneTax;
                        ObjPerformaInvoice.ContactName = objInputPerforma.ContactName;
                        ObjPerformaInvoice.ContactTel = objInputPerforma.ContactTel;
                        ObjPerformaInvoice.Contactmail = objInputPerforma.Contactmail;
                        ObjPerformaInvoice.BankNameId = objInputPerforma.BankNameId;
                        ObjPerformaInvoice.AccountTypeId = objInputPerforma.AccountTypeId;
                        ObjPerformaInvoice.BeneficiaryName = objInputPerforma.BeneficiaryName;
                        ObjPerformaInvoice.BranchName = objInputPerforma.BranchName;
                        ObjPerformaInvoice.BankAddress = objInputPerforma.BankAddress;
                        ObjPerformaInvoice.AccountNo = objInputPerforma.AccountNo;
                        ObjPerformaInvoice.IFSCCode = objInputPerforma.IFSCCode;
                        ObjPerformaInvoice.SwiftCode = objInputPerforma.SwiftCode;
                        ObjPerformaInvoice.ModifyBy = objInputPerforma.ModifyBy;
                        ObjPerformaInvoice.ModifyDate = DateTime.Now;
                        UpdatePerformaInvoice(ObjPerformaInvoice);
                        // Buyer Contact Detail
                        if (objInputPerforma.PerformaProductMasters != null)
                        {
                            foreach (var item in objInputPerforma.PerformaProductMasters)
                            {
                                if (item.Status == 1)
                                {
                                    PerformaProductMaster ObjPerformaProduct = new PerformaProductMaster();
                                    ObjPerformaProduct.PerformaInvId = ObjPerformaInvoice.PerformaInvId;
                                    ObjPerformaProduct.ProductId = item.ProductId;
                                    ObjPerformaProduct.ProductCode = item.ProductCode;
                                    ObjPerformaProduct.ProductModelNo = item.ProductModelNo;
                                    ObjPerformaProduct.CountryOfOriginId = item.CountryOfOriginId;
                                    ObjPerformaProduct.ProductDescription = item.ProductDescription;
                                    ObjPerformaProduct.QtyCode = item.QtyCode;
                                    ObjPerformaProduct.Qty = item.Qty;
                                    ObjPerformaProduct.CurrencyCode = item.CurrencyCode;
                                    ObjPerformaProduct.DealerPrice = item.DealerPrice;
                                    ObjPerformaProduct.AddPerValue = item.AddPerValue;
                                    ObjPerformaProduct.OfferPrice = item.OfferPrice;
                                    ObjPerformaProduct.IsActive = true;
                                    context.PerformaProductMasters.Add(ObjPerformaProduct);
                                }
                                else if (item.Status == 2 || item.Status == 3)
                                {
                                    PerformaProductMaster ObjPerformaProduct = context.PerformaProductMasters.Find(item.Id);
                                    ObjPerformaProduct.ProductId = item.ProductId;
                                    ObjPerformaProduct.ProductCode = item.ProductCode;
                                    ObjPerformaProduct.ProductModelNo = item.ProductModelNo;
                                    ObjPerformaProduct.CountryOfOriginId = item.CountryOfOriginId;
                                    ObjPerformaProduct.ProductDescription = item.ProductDescription;
                                    ObjPerformaProduct.QtyCode = item.QtyCode;
                                    ObjPerformaProduct.Qty = item.Qty;
                                    ObjPerformaProduct.CurrencyCode = item.CurrencyCode;
                                    ObjPerformaProduct.DealerPrice = item.DealerPrice;
                                    ObjPerformaProduct.AddPerValue = item.AddPerValue;
                                    ObjPerformaProduct.OfferPrice = item.OfferPrice;
                                    if (item.Status == 3)
                                        ObjPerformaProduct.IsActive = false;
                                    context.Entry(ObjPerformaProduct).State = System.Data.Entity.EntityState.Modified;
                                }
                            }
                        }
                        #endregion
                    }
                    resVal = ObjPerformaInvoice;
                    context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    resVal = null;
                }
            }
            return resVal;
        }

        public PerformaInvoiceMaster GetAllPerformaInvoiceById(int Id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@PerformaInvId", Id);
                return odal.GetDataTable_Text(@"Select PO.*,RM.CompanyCode As RptCompany,DT.DeliveryName AS DeliveryTerm,PM.TermName As PaymentTerm,
                                            SP.ModeOfShipment,BB.CompanyName As Contact,PR.PortName As LoadingPort,PR1.PortName As DischargePort,
                                            BM.BankName ,AT.AccountType
                                            from 
                                            PerformaInvoiceMaster As PO With(NOLOCK)
                                            Inner join ReportFOrmatMaster As RM With(NOLOCK) On RM.RotFormatId = PO.RptFormatId
                                            Inner join DeliveryTermsMaster As DT With(NOLOCK) On DT.TermsId = PO.DeliveryTermId
                                            Inner join PaymentTermsMaster As PM With(NOLOCK) On PM.PaymentTermId = PO.PaymentTermId
                                            Inner join ShipmentMaster As SP With(NOLOCK) On SP.ShipmentId = PO.ModeOfShipmentId
                                            Inner join BuyerMaster As BB With(NOLOCK) On BB.BuyerId = PO.ConsigneId
                                            Inner join PortMaster As PR With(NOLOCK) On PR.PortId = PO.LoadingPortId
                                            Inner join PortMaster As PR1 With(NOLOCK) On PR1.PortId = PO.DischargePortId
                                            Inner join BankNameMaster  As BM With(NOLOCK) On BM.BankId = PO.BankNameId
                                            Inner join AccountTypeMaster  As AT With(NOLOCK) On AT.AccountTypeId = PO.AccountTypeId
                                            Where PO.PerformaInvId =@PerformaInvId AND ISNULL(PO.IsActive,0)=1", para).ConvertToList<PerformaInvoiceMaster>().AsQueryable().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<PerformaProductMaster> GetByPerfomaInvId(int Id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@PerformaInvId", Id);
                return odal.GetDataTable_Text(@"Select PO.*,PM.ProductName,PM.ProductCode,SCM.SubCategoryId,SCM.SubcategoryName As [SubCategory],CM.CategoryId,CM.CategoryName[Category] 
                                            ,UM.UnitName As [QtyCodeValue],CR.CurrencyName As [CurrencyCodeValue],Co.CountryOfOrigin, ISNULL(Po.OfferPrice, 0 ) - (( ISNULL( Po.AddPerValue,0) *  ISNULL( Po.OfferPrice,0)) /100) as FinalPrice
                                            from 
                                            PerformaProductMaster As PO With(NOLOCK)
                                            Inner join ProductMaster PM With(NOLOCK) on PM.ProductId=PO.ProductId
                                            Inner join SubCategoryMaster SCM With(NOLOCK) on PM.SubCategoryId=SCM.SubCategoryId
                                            Inner join CategoryMaster CM With(NOLOCK) on SCM.CategoryId=CM.CategoryId
                                            Inner join CurrencyMaster CR With(NOLOCK) On CR.CurrencyId = PO.CurrencyCode
                                            Inner join UnitMaster UM With(NOLOCK) on UM.UnitId=PO.QtyCode
                                            Inner join CountryOfOriginMaster As CO With(NOLOCK) On CO.OriginId = PO.CountryOfOriginId
                                            Where PO.PerformaInvId =@PerformaInvId AND ISNULL(PO.IsActive,0)=1", para).ConvertToList<PerformaProductMaster>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<PerformaPaymentMaster> GetByPerfomaPaymentInvId(int Id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@PerformaInvId", Id);
                return odal.GetDataTable_Text(@"select P.* ,PMM.PaymentMode ,TTM.TranType
                                                from PerformaPaymentMaster as P
                                                Inner join PaymentModeMaster PMM With(NOLOCK) on PMM.PaymentModeId =p.PaymentModeId
                                                Inner join TransactionTypeMaster TTM With(NOLOCK) on TTM.TranTypeId =p.TransactionTypeId
                                                Where P.PerformaInvId =@PerformaInvId AND ISNULL(P.IsActive,0)=1", para).ConvertToList<PerformaPaymentMaster>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public System.Data.DataSet GetPerformaReportData(ReportPara obj)
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
            ds = odal.GetDataset("Performa_Report", para);
            ds.Tables[0].TableName = "PerformaMain";
            ds.Tables[1].TableName = "PerformaItems";
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
