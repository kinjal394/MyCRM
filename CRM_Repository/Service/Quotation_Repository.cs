using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using System.Transactions;
using CRM_Repository.DataServices;
using System.Data;
using System.Data.SqlClient;
using CRM_Repository.DTOModel;

namespace CRM_Repository.Service
{
    public class Quotation_Repository : IQuotation_Repositroy, IDisposable
    {

        dalc odal = new dalc();
        private CRM_Repository.Data.elaunch_crmEntities context;
        private IQuotationItem_Repository _IQuotationItem_Repository;
        public Quotation_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
            this._IQuotationItem_Repository = new QuotationItem_Repository(_context);
        }
        public Quotation_Repository()
        {
            context = new CRM_Repository.Data.elaunch_crmEntities();
        }
        public QuotationMaster AddQuotation(QuotationMaster Quotation)
        {
            try
            {
                context.QuotationMasters.Add(Quotation);
                context.SaveChanges();
                return Quotation;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int CreateUpdateQuotation(QuotationMaster objQuotationMaster, int userId)
        {


            if (objQuotationMaster.QuotationItemMasters.Count() > 0)
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        QuotationMaster ObjQMaster = new QuotationMaster();
                        if (objQuotationMaster.QuotationId <= 0)
                        {
                            decimal totalAmount = 0m;
                            foreach (var item in objQuotationMaster.QuotationItemMasters.Where(i => i.Status != 3))
                            {
                                totalAmount = item.OfferPrice * item.Qty;
                            }
                            //QuotationId, QuotationNo, CompanyId, QuotationDate, InqId, Total, DeliveryTermId, DeliveryTerm, TermPlaceId, 
                            //TermPlace, PaymentTermId, PaymentTerm, OfferValiddate, QuotationMadeBy, Note, CreatedBy, CreatedDate, 
                            //ModifyBy, ModifyDate, DeletedBy, DeletedDate, IsActive
                            //ADD
                            ObjQMaster.QuotationNo = objQuotationMaster.QuotationNo;
                            ObjQMaster.CompanyId = objQuotationMaster.CompanyId;
                            ObjQMaster.QuotationDate = objQuotationMaster.QuotationDate;
                            ObjQMaster.InqId = objQuotationMaster.InqId;
                            ObjQMaster.Total = totalAmount;
                            ObjQMaster.DeliveryTermId = objQuotationMaster.DeliveryTermId;
                            ObjQMaster.DeliveryTerm = objQuotationMaster.DeliveryTerm;
                            ObjQMaster.PaymentTermId = objQuotationMaster.PaymentTermId;
                            ObjQMaster.PaymentTerm = objQuotationMaster.PaymentTerm;
                            ObjQMaster.TermPlaceId = objQuotationMaster.TermPlaceId;
                            ObjQMaster.TermPlace = objQuotationMaster.TermPlace;

                            ObjQMaster.ExRate = objQuotationMaster.ExRate;
                            ObjQMaster.ExTotal = objQuotationMaster.ExTotal;
                            ObjQMaster.CurrencyId = objQuotationMaster.CurrencyId;
                            ObjQMaster.ExCurrencyId = objQuotationMaster.ExCurrencyId;

                            ObjQMaster.OfferValiddate = objQuotationMaster.OfferValiddate;
                            ObjQMaster.QuotationMadeBy = objQuotationMaster.QuotationMadeBy;
                            ObjQMaster.Note = objQuotationMaster.Note;
                            ObjQMaster.CreatedBy = Convert.ToInt32(userId);
                            ObjQMaster.CreatedDate = DateTime.Now;
                            ObjQMaster.IsActive = true;
                            ObjQMaster = AddQuotation(ObjQMaster);
                            //throw new Exception();
                            foreach (var item in objQuotationMaster.QuotationItemMasters)
                            {
                                //ItemId, QuotationId, ProductId, SupplierId, ProductDescription, OfferPriceCode, OfferPrice, QtyCode, Qty, 
                                //Total, IsActive, Persontage, DealerPrice, PriceId, CurrSymbol, ExRate
                                QuotationItemMaster ObjQuotationItemDetail = new QuotationItemMaster();
                                //ADD
                                ObjQuotationItemDetail.QuotationId = ObjQMaster.QuotationId;
                                ObjQuotationItemDetail.ProductId = item.ProductId;
                                ObjQuotationItemDetail.SupplierId = item.SupplierId;
                                ObjQuotationItemDetail.ProductDescription = item.ProductDescription;
                                //ObjQuotationItemDetail.OfferPriceCode = item.OfferPriceCode;
                                ObjQuotationItemDetail.OfferPrice = item.OfferPrice;
                                //ObjQuotationItemDetail.ProductPrice = item.ProductPrice;
                                ObjQuotationItemDetail.Percentage = item.Percentage;
                                ObjQuotationItemDetail.QtyCode = item.QtyCode;
                                ObjQuotationItemDetail.Qty = item.Qty;
                                ObjQuotationItemDetail.Total = item.Qty * item.OfferPrice;
                                ObjQuotationItemDetail.IsActive = true;
                                ObjQuotationItemDetail.DealerPrice = item.DealerPrice;
                                ObjQuotationItemDetail.PriceId = item.PriceId;
                                //ObjQuotationItemDetail.CurrSymbol = item.CurrSymbol;
                                //ObjQuotationItemDetail.ExRate = item.ExRate;
                                context.QuotationItemMasters.Add(ObjQuotationItemDetail);
                            }
                            context.SaveChanges();
                            dbContextTransaction.Commit();
                            return 1;
                        }
                        else
                        {
                            decimal totalAmount = 0m;
                            foreach (var item in objQuotationMaster.QuotationItemMasters.Where(i => i.Status != 3))
                            {
                                totalAmount = item.OfferPrice * item.Qty;
                            }
                            //EDIT
                            ObjQMaster = context.QuotationMasters.Find(objQuotationMaster.QuotationId);
                            ObjQMaster.QuotationNo = objQuotationMaster.QuotationNo;
                            ObjQMaster.CompanyId = objQuotationMaster.CompanyId;
                            ObjQMaster.QuotationDate = objQuotationMaster.QuotationDate;
                            ObjQMaster.InqId = objQuotationMaster.InqId;
                            ObjQMaster.Total = totalAmount;
                            ObjQMaster.DeliveryTermId = objQuotationMaster.DeliveryTermId;
                            ObjQMaster.DeliveryTerm = objQuotationMaster.DeliveryTerm;
                            ObjQMaster.PaymentTermId = objQuotationMaster.PaymentTermId;
                            ObjQMaster.PaymentTerm = objQuotationMaster.PaymentTerm;
                            ObjQMaster.TermPlaceId = objQuotationMaster.TermPlaceId;
                            ObjQMaster.TermPlace = objQuotationMaster.TermPlace;

                            ObjQMaster.ExRate = objQuotationMaster.ExRate;
                            ObjQMaster.ExTotal = objQuotationMaster.ExTotal;
                            ObjQMaster.CurrencyId = objQuotationMaster.CurrencyId;
                            ObjQMaster.ExCurrencyId = objQuotationMaster.ExCurrencyId;

                            ObjQMaster.OfferValiddate = objQuotationMaster.OfferValiddate;
                            ObjQMaster.QuotationMadeBy = objQuotationMaster.QuotationMadeBy;
                            ObjQMaster.Note = objQuotationMaster.Note;
                            ObjQMaster.ModifyBy = userId;
                            ObjQMaster.ModifyDate = DateTime.Now;
                            context.Entry(ObjQMaster).State = System.Data.Entity.EntityState.Modified;
                            foreach (var item in objQuotationMaster.QuotationItemMasters)
                            {

                                if (item.Status == 1)
                                {
                                    QuotationItemMaster ObjQuotationItemDetail = new QuotationItemMaster();
                                    //ADD
                                    ObjQuotationItemDetail.QuotationId = ObjQMaster.QuotationId;
                                    ObjQuotationItemDetail.ProductId = item.ProductId;
                                    ObjQuotationItemDetail.SupplierId = item.SupplierId;
                                    ObjQuotationItemDetail.ProductDescription = item.ProductDescription;
                                    //ObjQuotationItemDetail.OfferPriceCode = item.OfferPriceCode;
                                    ObjQuotationItemDetail.OfferPrice = item.OfferPrice;
                                   // ObjQuotationItemDetail.ProductPrice = item.ProductPrice;
                                    ObjQuotationItemDetail.Percentage = item.Percentage;
                                    ObjQuotationItemDetail.QtyCode = item.QtyCode;
                                    ObjQuotationItemDetail.Qty = item.Qty;
                                    ObjQuotationItemDetail.Total = item.Qty * item.OfferPrice;
                                    ObjQuotationItemDetail.IsActive = true;
                                    ObjQuotationItemDetail.DealerPrice = item.DealerPrice;
                                    ObjQuotationItemDetail.PriceId = item.PriceId;
                                    //ObjQuotationItemDetail.CurrSymbol = item.CurrSymbol;
                                    //ObjQuotationItemDetail.ExRate = item.ExRate;
                                    context.QuotationItemMasters.Add(ObjQuotationItemDetail);
                                }
                                else if (item.Status == 2 || item.Status == 3)
                                {
                                    QuotationItemMaster ObjQuotationItemDetail = context.QuotationItemMasters.Find(item.ItemId);
                                    ObjQuotationItemDetail.QuotationId = ObjQMaster.QuotationId;
                                    ObjQuotationItemDetail.ProductId = item.ProductId;
                                    ObjQuotationItemDetail.SupplierId = item.SupplierId;
                                    ObjQuotationItemDetail.ProductDescription = item.ProductDescription;
                                    //ObjQuotationItemDetail.OfferPriceCode = item.OfferPriceCode;
                                    ObjQuotationItemDetail.OfferPrice = item.OfferPrice;
                                    //ObjQuotationItemDetail.ProductPrice = item.ProductPrice;
                                    ObjQuotationItemDetail.Percentage = item.Percentage;
                                    ObjQuotationItemDetail.QtyCode = item.QtyCode;
                                    ObjQuotationItemDetail.Qty = item.Qty;
                                    ObjQuotationItemDetail.DealerPrice = item.DealerPrice;
                                    ObjQuotationItemDetail.PriceId = item.PriceId;
                                    //ObjQuotationItemDetail.CurrSymbol = item.CurrSymbol;
                                    //ObjQuotationItemDetail.ExRate = item.ExRate;

                                    ObjQuotationItemDetail.Total = item.Qty * item.OfferPrice;
                                    if (item.Status == 3)
                                        ObjQuotationItemDetail.IsActive = false;
                                    context.Entry(ObjQuotationItemDetail).State = System.Data.Entity.EntityState.Modified;
                                }
                            }
                            context.SaveChanges();
                            dbContextTransaction.Commit();
                            return 2;
                        }

                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        return 0;

                    }
                }
            }
            else
            {
                return 3;
            }
        }

        //public bool CheckCurrencyType(CurrencyMaster obj, bool isUpdate)
        //{
        //    try
        //    {
        //        if (isUpdate)
        //        {
        //            var data = context.CurrencyMasters.Where(e => e.CurrencyName.Trim() == obj.CurrencyName.Trim() && e.CurrencyId != obj.CurrencyId).ToList();
        //            return data.Count > 0 ? true : false;
        //        }
        //        else
        //        {
        //            var data = context.CurrencyMasters.Where(e => e.CurrencyName.Trim() == obj.CurrencyName.Trim()).ToList();
        //            return data.Count > 0 ? true : false;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        public IQueryable<QuotationMaster> GetAllQuotation()
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var Quotation = context.QuotationMasters;
                //    scope.Complete();
                //    return Quotation.AsQueryable();
                //}
                return new dalc().selectbyquerydt("SELECT * FROM QuotationMaster with(nolock) ").ConvertToList<QuotationMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public QuotationMaster GetQuotationById(int Quotationid)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var Quotation = context.QuotationMasters.Find(id);
                //    scope.Complete();
                //    return Quotation;
                //}
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@Quotationid", Quotationid);
                return new dalc().GetDataTable_Text("SELECT * FROM QuotationMaster with(nolock) WHERE Quotationid = @Quotationid", para).ConvertToList<QuotationMaster>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }
        }
        public IQueryable<QuotationMaster> FatchQuotationById(int QuotationId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@QuotationId", QuotationId);

                return odal.GetDataTable_Text(@"Select QM.DeliveryTerm As DeliveryName,
                                                QM.PaymentTerm As TermName,QM.*,CCM.ComName[CompanyName],
                                                UC.UserName,IM.BuyerName,IM.Inqno,CR1.CurrencyName As CurrSymbol,CR2.CurrencyName As ExCurrSymbol
                                                from QuotationMaster QM With(NOLOCK)                                     
                                                left join UserMaster As UC With(NOLOCK) On UC.UserId = QM.QuotationMadeBy
                                                left join CompanyMaster As CCM With(NOLOCK) On CCM.ComId = QM.CompanyId
                                                left join InquiryMaster As IM With(NOLOCK) On IM.InqId = QM.InqId
                                                left join CurrencyMaster CR1 With(NOLOCK) ON  CR1.CurrencyId = QM.CurrencyId
                                                left join CurrencyMaster CR2 With(NOLOCK) ON  CR2.CurrencyId = QM.ExCurrencyId
                                                where QM.QuotationId=@QuotationId", para).ConvertToList<QuotationMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdateQuotation(QuotationMaster Quotation)
        {
            try
            {
                context.Entry(Quotation).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ProductPrice GetDelarePriceById(int ProductId,int SupplierId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@ProductId", ProductId);
                para[1] = new SqlParameter().CreateParameter("@SupplierId", SupplierId);
                return new dalc().GetDataTable_Text("Select A.* from ProductPrices AS A INNER JOIN CurrencyMaster AS B ON A.CurrencyId = B.CurrencyId Where 1=1 AND A.ProductId=@ProductId AND A.SupplierId=@SupplierId", para).ConvertToList<ProductPrice>().FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        

        public System.Data.DataSet GetQuotationReportData(ReportPara obj)
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
            ds = odal.GetDataset("Quotation_Report", para);
            ds.Tables[0].TableName = "QuotationMain";
            ds.Tables[1].TableName = "QuotationItems";
            return ds;
            //            if (ID == 0)
            //            {
            //                if (UserType == 1)
            //                {
            //                    dt = new dalc().GetDataTable_Text(@"select Q.QuotationId,Q.QuotationNo,Q.QuotationDate,Q.Total,Q.OfferValiddate,C.ComName,C.RegOffAdd,C.CorpOffAdd,
            //C.TelNos, C.Email, I.BuyerName, I.Address, D.DeliveryName, P.TermName, U.Name CreateName, U.Email CreateEmail, U.MobNo CreateMobile
            //from QuotationMaster Q with(nolock)
            //INNER JOIN CompanyMaster C with(nolock) ON C.ComId = Q.CompanyId
            //INNER JOIN InquiryMaster I with(nolock) ON I.InqId = Q.InqId
            //INNER JOIN DeliveryTermsMaster D with(nolock) ON D.TermsId = Q.DeliveryTermId
            //INNER JOIN PaymentTermsMaster P with(nolock) ON P.PaymentTermId = Q.PaymentTermId
            //INNER JOIN UserMaster U with(nolock) ON U.UserId = Q.QuotationMadeBy
            //WHERE Q.IsActive=1", para);
            //                    dt1 = new dalc().selectbyquerydt(@"select Q.QuotationId,P.ProductName,P.Description,Q.Qty,Q.OfferPrice,Q.Total,U.UnitName,C.CurrencySymbol,PP.Photo from QuotationItemMaster Q
            //            INNER JOIN ProductMaster P ON P.ProductId=Q.ProductId
            //            INNER JOIN CurrencyMaster C on C.CurrencyId=Q.OfferPriceCode
            //            INNER JOIN UnitMaster U ON U.UnitId=Q.QtyCode
            //            LEFT JOIN ProductPhotoMaster PP ON PP.ProductId=P.ProductId AND PP.IsDefault=1 AND PP.IsActive=1");

            //                }
            //                else
            //                {
            //                    dt = new dalc().GetDataTable_Text(@"select Q.QuotationId,Q.QuotationNo,Q.QuotationDate,Q.Total,Q.OfferValiddate,C.ComName,C.RegOffAdd,C.CorpOffAdd,
            //C.TelNos, C.Email, I.BuyerName, I.Address, D.DeliveryName, P.TermName, U.Name CreateName, U.Email CreateEmail, U.MobNo CreateMobile
            //from QuotationMaster Q with(nolock)
            //INNER JOIN CompanyMaster C with(nolock) ON C.ComId = Q.CompanyId
            //INNER JOIN InquiryMaster I with(nolock) ON I.InqId = Q.InqId
            //INNER JOIN DeliveryTermsMaster D with(nolock) ON D.TermsId = Q.DeliveryTermId
            //INNER JOIN PaymentTermsMaster P with(nolock) ON P.PaymentTermId = Q.PaymentTermId
            //INNER JOIN UserMaster U with(nolock) ON U.UserId = Q.QuotationMadeBy
            //WHERE Q.IsActive=1 AND Q.CreatedBy=@UserId", para);
            //                    dt1 = new dalc().GetDataTable_Text(@"select Q.QuotationId,P.ProductName,P.Description,Q.Qty,Q.OfferPrice,Q.Total,U.UnitName,C.CurrencySymbol,PP.Photo from QuotationItemMaster Q
            //            INNER JOIN QuotationMaster QM ON QM.QuotationId=Q.QuotationId
            //            INNER JOIN ProductMaster P ON P.ProductId=Q.ProductId
            //            INNER JOIN CurrencyMaster C on C.CurrencyId=Q.OfferPriceCode
            //            INNER JOIN UnitMaster U ON U.UnitId=Q.QtyCode
            //            LEFT JOIN ProductPhotoMaster PP ON PP.ProductId=P.ProductId AND PP.IsDefault=1 AND PP.IsActive=1 WHERE QM.CreatedBy=@UserId", para);
            //                }
            //            }
            //            else
            //            {
            //                dt = new dalc().GetDataTable_Text(@"select Q.QuotationId,Q.QuotationNo,Q.QuotationDate,Q.Total,Q.OfferValiddate,C.ComName,C.RegOffAdd,C.CorpOffAdd,
            //C.TelNos, C.Email, I.BuyerName, I.Address, D.DeliveryName, P.TermName, U.Name CreateName, U.Email CreateEmail, U.MobNo CreateMobile
            //from QuotationMaster Q with(nolock)
            //INNER JOIN CompanyMaster C with(nolock) ON C.ComId = Q.CompanyId
            //INNER JOIN InquiryMaster I with(nolock) ON I.InqId = Q.InqId
            //INNER JOIN DeliveryTermsMaster D with(nolock) ON D.TermsId = Q.DeliveryTermId
            //INNER JOIN PaymentTermsMaster P with(nolock) ON P.PaymentTermId = Q.PaymentTermId
            //INNER JOIN UserMaster U with(nolock) ON U.UserId = Q.QuotationMadeBy WHERE Q.QuotationId=@ID", para);
            //                // dt.TableName = "QuotationMain";
            //                dt1 = new dalc().GetDataTable_Text(@"select Q.QuotationId,P.ProductName,P.Description,Q.Qty,Q.OfferPrice,Q.Total,U.UnitName,C.CurrencySymbol,PP.Photo from QuotationItemMaster Q
            //            INNER JOIN ProductMaster P ON P.ProductId=Q.ProductId
            //            INNER JOIN CurrencyMaster C on C.CurrencyId=Q.OfferPriceCode
            //            INNER JOIN UnitMaster U ON U.UnitId=Q.QtyCode
            //            LEFT JOIN ProductPhotoMaster PP ON PP.ProductId=P.ProductId AND PP.IsDefault=1 AND PP.IsActive=1  WHERE Q.QuotationId=@ID", para);
            //                //  dt1.TableName = "QuotationItems";
            //            }
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
