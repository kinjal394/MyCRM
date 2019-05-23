using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.ServiceContract;
using System.Data.Entity;
using CRM_Repository.Data;
using System.Data;
using System.Transactions;
using CRM_Repository.DataServices;
using CRM_Repository.DTOModel;
using CRM_Repository.ExtendedModel;

using System.Data.SqlClient;

namespace CRM_Repository.Service
{
    public class Inquiry_Repository : IInquiry_Repository, IDisposable
    {
        dalc odal = new dalc();
        private elaunch_crmEntities context;
        public Inquiry_Repository(elaunch_crmEntities _context)
        {
            context = _context;
        }

        public InquiryMaster InsertInquiry(InquiryMaster objinquiry)
        {
            try
            {
                context.InquiryMasters.Add(objinquiry);
                context.SaveChanges();
                return objinquiry;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public InquiryFollowupMaster InsertInquiryFollowUp(InquiryFollowupMaster objinquiry)
        {
            try
            {
                context.InquiryFollowupMasters.Add(objinquiry);
                context.SaveChanges();
                return objinquiry;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void UpdateInquiry(InquiryMaster objinquiry)
        {
            try
            {
                context.Entry(objinquiry).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdateInquiryFollowUp(InquiryFollowupMaster objinquiry)
        {
            try
            {
                using (elaunch_crmEntities _context = new elaunch_crmEntities())
                {
                    _context.Entry(objinquiry).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int GetAreaByCityId(int cityId)
        {
            try
            {
                var areaId = context.CityMasters.Where(c => c.CityId == cityId).FirstOrDefault().CityId;
                return areaId;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string DuplicateInquiryNo()
        {
            try
            {
                dalc odal = new dalc();
                DataTable dt = new DataTable();
                string autogenerate = "";
                int cnt = 0;
                while (cnt == 0)
                {
                    //dt = odal.selectbyquerydt(@"SELECT convert(numeric(12,0),rand() * 9999999999)");
                    int inqcount = context.InquiryMasters.ToList().Count();
                    int inqno = 500001 + inqcount;
                    autogenerate = string.Concat("INQ", inqno.ToString());
                    var inqdata = context.InquiryMasters.Where(x => x.InqNo == autogenerate).ToList();
                    if (inqdata.Count == 0)
                    {
                        cnt = 1;
                    }
                }
                return autogenerate;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<InquiryMaster> GetAllInquiry()
        {
            try
            {

                return new dalc().selectbyquerydt("SELECT * FROM InquiryMaster with(nolock) Where IsActive=1 Order By InqId ").ConvertToList<InquiryMaster>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<InquiryFollowupModel> GetAllInquiryFollowup(string userid, int usertype)
        {
            try
            {
                string str = @"select T.FollowupId[Id],T.InqId,T.AssignId,T.CurrentUpdate[title],T.Status,T.NextFollowDate[Date],T.NextFollowTime[Time],'InquiryFollowUp' [typeOfEventTitle]
                               from inquiryfollowupmaster T
                               LEFT JOIN gurjari_crmuser.TaskStatusMaster AS TS ON T.Status = TS.StatusId 
                               where T.isactive=1 ";
                if (usertype > 1)
                {
                    str += "and (T.CreatedBy= " + userid + " OR T.AssignId = " + userid + ") ";
                }
                str += @"union all
                         select T.TaskId[Id],''[InqId],''[AssignId],T.Task[title],T.Status,case when TDM.FollowDate is NULL then '23:59:59' else TDM.FollowDate end As[Date],
                         case when TDM.FollowTime is NULL then '1001/01/01' else TDM.FollowTime end As[Time],'Task'[typeOfEventTitle]
                         from TaskMaster T
                         LEFT JOIN gurjari_crmuser.TaskStatusMaster AS TS ON T.Status = TS.StatusId
                         LEFT JOIN gurjari_crmuser.TaskDetailMaster as TDM  on TDM.TaskId = T.TaskId  AND TDM.ToId = T.CreatedBy";
                str += " where T.isactive = 1 ";
                if (usertype > 1)
                {
                    str += " AND TDM.ToId=" + userid + "";
                }
                else if (usertype == 1)
                {
                    str += " AND TS.TaskStatus != 'Completed'";
                }

                return new dalc().selectbyquerydt(str).ConvertToList<InquiryFollowupModel>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public InquiryMaster GetInquiryId(int InqId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@Inqid", InqId);
                return new dalc().GetDataTable_Text("SELECT * FROM InquiryMaster with(nolock) WHERE Inqid=@Inqid ", para).ConvertToList<InquiryMaster>().FirstOrDefault();

            }
            catch (Exception)
            {
                throw;
            }
        }
        public InquiryFollowupMaster GetInquiryFollowById(int id)
        {
            try
            {
                //return context.InquiryFollowupMasters.Find(id);
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@Followupid", id);
                return new dalc().GetDataTable_Text("SELECT * FROM InquiryFollowupMaster with(nolock) WHERE Followupid=@Followupid ", para).ConvertToList<InquiryFollowupMaster>().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }


        }

        public InquiryModel GetInquiryById(int InqId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@Inqid", InqId);
                return odal.GetDataTable_Text(@"SELECT I.InqId,I.ContactPersonname,I.Subject,I.CityId,C.CityName,I.InqDate,I.InqNo,I.BuyerName,I.MobileNo,I.Email,I.Requirement,I.AssignTo,
                                            I.[Address],I.SourceId,S.SourceName,I.Remark,ST.StateId,ST.StateName,ST.CountryId,CM.CountryName,I.BuyerType,I.SubjectType,I.CompanyName,I.ContactPersonname,
                                            I.[Status] As StatusId,TS.TaskStatus As StatusName,I.IsActive 
                                            FROM gurjari_crmuser.InquiryMaster as I 
                                            INNER join gurjari_crmuser.CityMaster as C on C.CityId = I.CityId 
                                            INNER join gurjari_crmuser.StateMaster as ST on ST.StateId = C.StateId 
                                            INNER join gurjari_crmuser.CountryMaster as CM on CM.CountryId = ST.CountryId 
                                            INNER join gurjari_crmuser.SourceMaster as S on S.SourceId = I.SourceId 
                                            INNER join gurjari_crmuser.TaskStatusMaster  as ts on ts.StatusId = I.[Status]
                                            Where I.InqId =@Inqid  And ISNULL(I.IsActive,0)=1", para).ConvertToList<InquiryModel>().AsQueryable().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public InquiryFollowupModel GetInquiryFollowUpById(int id)
        {
            //var data = from d in context.InquiryFollowupMasters.Where(a => a.InqId == id)
            //           join st in context.TaskStatusMasters on d.Status equals st.StatusId
            //           select new InquiryFollowupModel
            //           {
            //               FollowupId = d.FollowupId,
            //               InqId = d.InqId,
            //               CurrentUpdate = d.CurrentUpdate,
            //               NextFollowDate = d.NextFollowDate,
            //               NextFollowTime = d.NextFollowTime,
            //               StatusName = st.TaskStatus,
            //               Status = d.Status,
            //               CreatedDate = d.CreatedDate,
            //               CreatedBy = d.CreatedBy,
            //               IsActive = d.IsActive,
            //               AssignId = d.AssignId
            //           };
            //return data.FirstOrDefault();
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@Inqid", id);
                return new dalc().GetDataTable_Text(@"  SELECT Top 1 FollowupId,InqId,CurrentUpdate,NextFollowDate,NextFollowTime,TaskStatus AS StatusName,
                                                  Status, CreatedDate, CreatedBy, A.IsActive, AssignId
                                                  FROM InquiryFollowupMaster AS A
                                                  INNER JOIN TaskStatusMaster AS B ON A.Status = B.StatusId WHERE Inqid =@Inqid Order by  FollowupId Desc", para).ConvertToList<InquiryFollowupModel>().FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }


        }

        public InquiryFollowupModel FetchInquiryFollowUpById(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@FollowupId", id);
                return new dalc().GetDataTable_Text(@"  SELECT FollowupId,InqId,CurrentUpdate,NextFollowDate,NextFollowTime,TaskStatus AS StatusName,
                                                  Status, CreatedDate, CreatedBy, A.IsActive, AssignId
                                                  FROM InquiryFollowupMaster AS A
                                                  INNER JOIN TaskStatusMaster AS B ON A.Status = B.StatusId WHERE FollowupId =@FollowupId", para).ConvertToList<InquiryFollowupModel>().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IQueryable<InquiryMaster> DuplicateEditInquiry(int InqId, string Name, string Email, string Mobileno, string Requirement)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var inquiry = context.InquiryMasters.Where(x => x.BuyerName.ToLower().Trim() == Name.ToLower().Trim() && x.Email.ToLower().Trim() == Email.ToLower().Trim() && x.MobileNo.Trim() == Mobileno.Trim() && x.Requirement.ToLower().Trim() == Requirement.ToLower().Trim() && x.InqId != InqId);
                //    scope.Complete();
                //    return inquiry.AsQueryable();
                //}

                SqlParameter[] para = new SqlParameter[5];
                para[0] = new SqlParameter().CreateParameter("@Inqid", InqId);
                para[1] = new SqlParameter().CreateParameter("@BuyerName", Name);
                para[2] = new SqlParameter().CreateParameter("@Email", Email);
                para[3] = new SqlParameter().CreateParameter("@MobileNo", Mobileno);
                para[4] = new SqlParameter().CreateParameter("@Requirement", Requirement);

                return new dalc().GetDataTable_Text(@" SELECT * FROM InquiryMaster WHERE RTRIM(LTRIM(BuyerName))=RTRIM(LTRIM(@BuyerName)) 
                            AND RTRIM(LTRIM(Email))=RTRIM(LTRIM(@Email)) AND RTRIM(LTRIM(MobileNo))=RTRIM(LTRIM(@MobileNo)) 
                            AND RTRIM(LTRIM(Requirement))=RTRIM(LTRIM(@Requirement)) AND InqId<>@InqId ", para).ConvertToList<InquiryMaster>().AsQueryable();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public int CreateUpdate(InquiryModel objInputInquiryModel)
        {
            int resVal;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    InquiryMaster ObjInquiryMaster = new InquiryMaster();
                    if (objInputInquiryModel.InqId <= 0)
                    {
                        #region //ADD
                        ObjInquiryMaster.InqId = objInputInquiryModel.InqId;
                        ObjInquiryMaster.InqNo = objInputInquiryModel.InqNo;
                        ObjInquiryMaster.InqDate = objInputInquiryModel.InqDate;
                        ObjInquiryMaster.SourceId = objInputInquiryModel.SourceId;
                        ObjInquiryMaster.BuyerType = objInputInquiryModel.BuyerType;
                        ObjInquiryMaster.BuyerName = objInputInquiryModel.BuyerName;
                        ObjInquiryMaster.ContactPersonname = objInputInquiryModel.ContactPersonname;
                        ObjInquiryMaster.CompanyName = objInputInquiryModel.CompanyName;
                        ObjInquiryMaster.AssignTo = objInputInquiryModel.AssignTo;
                        //ObjInquiryMaster.MobileCode1 = objInputInquiryModel.MobileCode1;
                        //ObjInquiryMaster.MobileCode2 = objInputInquiryModel.MobileCode2;
                        ObjInquiryMaster.MobileNo = objInputInquiryModel.MobileNo;
                        //ObjInquiryMaster.MobileNo2 = objInputInquiryModel.MobileNo2;
                        ObjInquiryMaster.Email = objInputInquiryModel.Email;
                        ObjInquiryMaster.CityId = objInputInquiryModel.CityId;
                        ObjInquiryMaster.Requirement = objInputInquiryModel.Requirement;
                        ObjInquiryMaster.Address = objInputInquiryModel.Address;
                        ObjInquiryMaster.CreatedBy = objInputInquiryModel.CreatedBy;
                        ObjInquiryMaster.SubjectType = objInputInquiryModel.SubjectType;
                        ObjInquiryMaster.Subject = objInputInquiryModel.Subject;
                        ObjInquiryMaster.CreatedDate = DateTime.Now;
                        ObjInquiryMaster.IsActive = true;
                        ObjInquiryMaster.Status = 1;
                        ObjInquiryMaster = InsertInquiry(ObjInquiryMaster);
                        //FollowupId,InqId,CurrentUpdate,NextFollowTime,NextFollowDate,Status,CreatedDate,CreatedBy,Modifydate,ModifyBy,DeletedDate,DeletedBy,IsActive,AssignId
                        InquiryFollowupMaster objInquiryFollowupMaster = new InquiryFollowupMaster();
                        objInquiryFollowupMaster.InqId = ObjInquiryMaster.InqId;
                        objInquiryFollowupMaster.CurrentUpdate = "Self Assigne";
                        objInquiryFollowupMaster.Status = 1;
                        objInquiryFollowupMaster.CreatedBy = objInputInquiryModel.CreatedBy;
                        objInquiryFollowupMaster.CreatedDate = DateTime.Now;
                        objInquiryFollowupMaster.IsActive = true;
                        objInquiryFollowupMaster.AssignId = objInputInquiryModel.CreatedBy;
                        context.InquiryFollowupMasters.Add(objInquiryFollowupMaster);

                        //if (objInputInquiryModel.InquiryItemDetails != null)
                        //{
                        //    foreach (var item in objInputInquiryModel.InquiryItemDetails)
                        //    {
                        //        InquiryItemMaster ObjInquiryItemDetail = new InquiryItemMaster();
                        //        //ADD
                        //        ObjInquiryItemDetail.InqId = ObjInquiryMaster.InqId;
                        //        ObjInquiryItemDetail.InqDetailId = item.InqDetailId;
                        //        ObjInquiryItemDetail.ProductId = item.ProductId;
                        //        ObjInquiryItemDetail.ProductDescription = item.ProductDescription;
                        //        ObjInquiryItemDetail.QtyCode = item.QtyCode;
                        //        ObjInquiryItemDetail.Qty = item.Qty;
                        //        ObjInquiryItemDetail.IsActive = true;
                        //        context.InquiryItemMasters.Add(ObjInquiryItemDetail);
                        //    }
                        //}
                        resVal = 1;
                        #endregion
                    }
                    else
                    {
                        //InqId,InqNo,InqDate,SourceId,BuyerName,MobileNo,Email,CityId,Requirement,Address,CreatedBy,CreatedDate,ModifyBy,ModifyDate,DeletedBy,DeletedDate,IsActive,Status,Remark
                        #region //EDIT
                        ObjInquiryMaster = context.InquiryMasters.Find(objInputInquiryModel.InqId);
                        ObjInquiryMaster.InqId = objInputInquiryModel.InqId;
                        ObjInquiryMaster.InqNo = objInputInquiryModel.InqNo;
                        ObjInquiryMaster.InqDate = objInputInquiryModel.InqDate;
                        ObjInquiryMaster.SourceId = objInputInquiryModel.SourceId;
                        ObjInquiryMaster.BuyerType = objInputInquiryModel.BuyerType;
                        ObjInquiryMaster.BuyerName = objInputInquiryModel.BuyerName;
                        ObjInquiryMaster.ContactPersonname = objInputInquiryModel.ContactPersonname;
                        ObjInquiryMaster.CompanyName = objInputInquiryModel.CompanyName;
                        ObjInquiryMaster.AssignTo = objInputInquiryModel.AssignTo;
                        //ObjInquiryMaster.MobileCode1 = objInputInquiryModel.MobileCode1;
                        //ObjInquiryMaster.MobileCode2 = objInputInquiryModel.MobileCode2;
                        ObjInquiryMaster.MobileNo = objInputInquiryModel.MobileNo;
                        // ObjInquiryMaster.MobileNo2 = objInputInquiryModel.MobileNo2;
                        ObjInquiryMaster.Email = objInputInquiryModel.Email;
                        ObjInquiryMaster.CityId = objInputInquiryModel.CityId;
                        ObjInquiryMaster.Requirement = objInputInquiryModel.Requirement;
                        ObjInquiryMaster.Address = objInputInquiryModel.Address;
                        ObjInquiryMaster.ModifyBy = objInputInquiryModel.ModifyBy;
                        ObjInquiryMaster.SubjectType = objInputInquiryModel.SubjectType;
                        ObjInquiryMaster.Subject = objInputInquiryModel.Subject;
                        ObjInquiryMaster.ModifyDate = DateTime.Now;
                        ObjInquiryMaster.IsActive = true;
                        ObjInquiryMaster.Status = objInputInquiryModel.StatusId;
                        ObjInquiryMaster.Remark = objInputInquiryModel.Remark;
                        UpdateInquiry(ObjInquiryMaster);

                        //if (objInputInquiryModel.InquiryItemDetails != null)
                        //{
                        //    foreach (var item in objInputInquiryModel.InquiryItemDetails)
                        //    {
                        //        if (item.Status == 1)
                        //        {
                        //            InquiryItemMaster ObjInquiryItemDetail = new InquiryItemMaster();
                        //            ObjInquiryItemDetail.InqId = ObjInquiryMaster.InqId;
                        //            ObjInquiryItemDetail.InqDetailId = item.InqDetailId;
                        //            ObjInquiryItemDetail.ProductId = item.ProductId;
                        //            ObjInquiryItemDetail.ProductDescription = item.ProductDescription;
                        //            ObjInquiryItemDetail.QtyCode = item.QtyCode;
                        //            ObjInquiryItemDetail.Qty = item.Qty;
                        //            ObjInquiryItemDetail.IsActive = true;
                        //            context.InquiryItemMasters.Add(ObjInquiryItemDetail);
                        //        }
                        //        else if (item.Status == 2 || item.Status == 3)
                        //        {
                        //            InquiryItemMaster ObjInquiryItemDetail = context.InquiryItemMasters.Find(item.InqDetailId);
                        //            ObjInquiryItemDetail.InqId = ObjInquiryMaster.InqId;
                        //            ObjInquiryItemDetail.InqDetailId = item.InqDetailId;
                        //            ObjInquiryItemDetail.ProductId = item.ProductId;
                        //            ObjInquiryItemDetail.ProductDescription = item.ProductDescription;
                        //            ObjInquiryItemDetail.QtyCode = item.QtyCode;
                        //            ObjInquiryItemDetail.Qty = item.Qty;
                        //            if (item.Status == 3)
                        //                ObjInquiryItemDetail.IsActive = false;
                        //            context.Entry(ObjInquiryItemDetail).State = System.Data.Entity.EntityState.Modified;
                        //        }
                        //    }
                        //}
                        resVal = 2;
                        #endregion
                    }
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
        public int Delete(InquiryModel objInputInquiryModel)
        {
            int resVal;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    InquiryMaster objInquiryMaster = context.InquiryMasters.Find(objInputInquiryModel.InqId);
                    objInquiryMaster.IsActive = false;
                    objInquiryMaster.DeletedBy = objInputInquiryModel.DeletedBy;
                    objInquiryMaster.DeletedDate = DateTime.Now;
                    UpdateInquiry(objInquiryMaster);

                    List<InquiryItemMaster> ObjInquiryItemDetail = context.InquiryItemMasters.Where(x => x.InqId == objInputInquiryModel.InqId).ToList();
                    if (ObjInquiryItemDetail.Count > 0)
                    {
                        foreach (var item in ObjInquiryItemDetail)
                        {
                            InquiryItemMaster ObjSalesItemSingle = context.InquiryItemMasters.Find(item.InqDetailId);
                            item.IsActive = false;
                            context.Entry(ObjSalesItemSingle).State = System.Data.Entity.EntityState.Modified;
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

        public IQueryable<InquiryMaster> GetInqGridInquiry()
        {
            try
            {

                return new dalc().selectbyquerydt(@"SELECT top 50 *,
                                                (SELECT STUFF((
                                                SELECT top 50 ', ' + Cast(I.InqId as nvarchar) 
                                                FROM InquiryMaster AS I Where I.IsActive = 1
                                                FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) As AllInqId
                                                FROM InquiryMaster with(nolock) Where IsActive=1 Order By InqId ").ConvertToList<InquiryMaster>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<InquiryFollowupModel> GetInqGridFollowup(string userid)
        {
            try
            {
                string str = @"SELECT FollowupId,InqId,CurrentUpdate,NextFollowDate,NextFollowTime,TaskStatus AS StatusName,
                                Status, CreatedDate, CreatedBy, A.IsActive, AssignId
                                FROM InquiryFollowupMaster AS A
                                INNER JOIN TaskStatusMaster AS B ON A.Status = B.StatusId 
                                WHERE Inqid in ( "+ userid + ")  And A.IsActive=1 Order by  InqId ";
                return new dalc().selectbyquerydt(str).ConvertToList<InquiryFollowupModel>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
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
