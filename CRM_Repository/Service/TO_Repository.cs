using CRM_Repository.Data;
using CRM_Repository.DataServices;
using CRM_Repository.DTOModel;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.Service
{
    public class TO_Repository : ITO_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        dalc odal = new dalc();
        public TO_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public int CreateUpdate(TOModel obj)
        {
            int resVal;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    TOMaster todet = new TOMaster();
                    if (obj.TOId > 0)
                    {
                        #region "UPDATE"
                        todet = context.TOMasters.Find(obj.TOId);
                        //todet.ToTypeId = obj.ToTypeId;
                        todet.InqId = obj.InqId;
                        todet.Remark = obj.Remark;
                        todet.IsActive = true;
                        todet.ModifyBy = obj.ModifyBy;
                        todet.ModifyDate = DateTime.Now;
                        context.Entry(todet).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        if (obj.TOItemDetail != null)
                        {
                            foreach (var item in obj.TOItemDetail)
                            {
                                if (item.Status == 1)
                                {
                                    TOItemMaster objTOItemMaster = new TOItemMaster();

                                    objTOItemMaster.TOId = todet.TOId;
                                    objTOItemMaster.ProductId = item.ProductId;
                                    //objTOItemMaster.SpecId = item.SpecId;
                                    //objTOItemMaster.SpecValue = item.SpecValue;
                                    context.TOItemMasters.Add(objTOItemMaster);
                                }
                                else if (item.Status == 2 || item.Status == 3)
                                {
                                    TOItemMaster objTOItemMaster = context.TOItemMasters.Find(item.TOItemId);
                                    objTOItemMaster.TOId = todet.TOId;
                                    objTOItemMaster.ProductId = item.ProductId;
                                    //objTOItemMaster.SpecId = item.SpecId;
                                    //objTOItemMaster.SpecValue = item.SpecValue;
                                    context.Entry(objTOItemMaster).State = System.Data.Entity.EntityState.Modified;
                                    if (item.Status == 3)
                                    {
                                        context.TOItemMasters.Remove(objTOItemMaster);
                                    }
                                }
                            }
                        }

                        if (obj.TOSpecification != null)
                        {
                            //delete all technical detail
                            //deletetechnicaldetail(obj.TOItemDetail[0].ProductId, 1);

                            foreach (var tech in obj.TOSpecification)
                            {
                                if (tech.Status == 1)
                                {
                                    TOTechnicalDetail techobj = new TOTechnicalDetail();
                                    techobj.TOId = todet.TOId;
                                    techobj.TOItemId = obj.TOItemDetail[0].TOItemId;
                                    techobj.ProductId = tech.ProductId;
                                    techobj.TechParaID = tech.TechParaID;
                                    techobj.TechParaVal = tech.TechParaVal;
                                    techobj.TechParaRequirment = tech.TechParaRequirment;
                                    techobj.TechSpecifType = 1;
                                    context.TOTechnicalDetails.Add(techobj);
                                }
                                else if (tech.Status == 3)
                                {
                                    TOTechnicalDetail techobj = context.TOTechnicalDetails.Find(tech.TechDetailId);
                                    context.TOTechnicalDetails.Remove(techobj);
                                }
                            }
                        }
                        else
                        {
                            //delete all technical detail
                            deletetechnicaldetail(obj.TOItemDetail[0].ProductId, 1);
                        }

                        if (obj.ToPacking != null)
                        {
                            //Delete all Packing detail
                            //deletetechnicaldetail(obj.TOItemDetail[0].ProductId, 2);
                            foreach (var pack in obj.ToPacking)
                            {
                                if (pack.Status == 1)
                                {
                                    TOTechnicalDetail techobj = new TOTechnicalDetail();
                                    techobj.TOId = todet.TOId;
                                    techobj.TOItemId = obj.TOItemDetail[0].TOItemId;
                                    techobj.ProductId = pack.ProductId;
                                    techobj.TechParaID = pack.TechParaID;
                                    techobj.TechParaVal = pack.PackParaVal;
                                    techobj.TechParaRequirment = pack.PackParaRequirment;
                                    techobj.TechSpecifType = 2;
                                    context.TOTechnicalDetails.Add(techobj);
                                }
                                else if (pack.Status == 3)
                                {
                                    TOTechnicalDetail techobj = context.TOTechnicalDetails.Find(pack.TechDetailId);
                                    context.TOTechnicalDetails.Remove(techobj);
                                }
                            }
                        }
                        else
                        {
                            //Delete all Packing detail
                            deletetechnicaldetail(obj.TOItemDetail[0].ProductId, 2);
                        }
                        context.SaveChanges();

                        resVal = 2;
                        #endregion
                    }
                    else
                    {
                        #region "INSERT"
                        todet.ToTypeId = null;
                        todet.InqId = obj.InqId;
                        todet.Remark = obj.Remark;
                        todet.IsActive = true;
                        todet.CreatedBy = obj.CreatedBy;
                        todet.CreatedDate = DateTime.Now;
                        context.TOMasters.Add(todet);
                        context.SaveChanges();
                        if (obj.TOItemDetail != null)
                        {
                            TOItemMaster objTOItemMaster = new TOItemMaster();
                            foreach (var item in obj.TOItemDetail)
                            {
                                if (item.Status == 1)
                                {

                                    objTOItemMaster.TOId = todet.TOId;
                                    objTOItemMaster.ProductId = item.ProductId;
                                    //objTOItemMaster.SpecId = item.SpecId;
                                    //objTOItemMaster.SpecValue = item.SpecValue;
                                    context.TOItemMasters.Add(objTOItemMaster);
                                    context.SaveChanges();
                                }
                                else if (item.Status == 2 || item.Status == 3)
                                {
                                    TOItemMaster objTOItemMast = context.TOItemMasters.Find(item.TOItemId);
                                    objTOItemMast.TOId = todet.TOId;
                                    objTOItemMast.ProductId = item.ProductId;
                                    //objTOItemMaster.SpecId = item.SpecId;
                                    //objTOItemMaster.SpecValue = item.SpecValue;
                                    context.Entry(objTOItemMast).State = System.Data.Entity.EntityState.Modified;
                                    context.SaveChanges();
                                    if (item.Status == 3)
                                    {
                                        context.TOItemMasters.Remove(objTOItemMast);
                                        context.SaveChanges();
                                    }
                                }
                            }
                            if (obj.TOSpecification != null)
                            {
                                foreach (var tech in obj.TOSpecification)
                                {
                                    TOTechnicalDetail techobj = new TOTechnicalDetail();
                                    techobj.TOId = todet.TOId;
                                    techobj.TOItemId = objTOItemMaster.TOItemId;
                                    techobj.ProductId = objTOItemMaster.ProductId;
                                    techobj.TechParaID = tech.TechParaID;
                                    techobj.TechParaVal = tech.TechParaVal;
                                    techobj.TechParaRequirment = tech.TechParaRequirment;
                                    techobj.TechSpecifType = 1;
                                    context.TOTechnicalDetails.Add(techobj);
                                    context.SaveChanges();
                                }
                            }
                            if (obj.ToPacking != null)
                            {
                                foreach (var pack in obj.ToPacking)
                                {
                                    TOTechnicalDetail techobj = new TOTechnicalDetail();
                                    techobj.TOId = todet.TOId;
                                    techobj.TOItemId = objTOItemMaster.TOItemId;
                                    techobj.ProductId = objTOItemMaster.ProductId;
                                    techobj.TechParaID = pack.TechParaID;
                                    techobj.TechParaVal = pack.PackParaVal;
                                    techobj.TechParaRequirment = pack.PackParaRequirment;
                                    techobj.TechSpecifType = 2;
                                    context.TOTechnicalDetails.Add(techobj);
                                    context.SaveChanges();
                                }
                            }
                        }
                        resVal = 1;
                        #endregion
                    }

                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    resVal = 0;
                }
            }
            return resVal;
        }
        public TOModel GetTOById(int TOId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@TOId", TOId);
                //return odal.GetDataTable_Text(@"SELECT tom.TOId,tom.InqId,inq.InqNo,tom.ToTypeId,tot.TOType,inq.BuyerName,tom.Remark,tom.IsActive
                //                    FROM gurjari_crmuser.TOMaster tom  WITH(nolock) 
                //                    INNER JOIN gurjari_crmuser.InquiryMaster inq  WITH(nolock) ON inq.InqId=tom.InqId
                //                    INNER JOIN gurjari_crmuser.TOTypeMaster tot  WITH(nolock) ON tot.TOTypeId=tom.ToTypeId
                //                    WHERE tom.TOId =@TOId  AND ISNULL(tom.IsActive,0)=1", para).ConvertToList<TOModel>().AsQueryable().FirstOrDefault();
                return odal.GetDataTable_Text(@"SELECT tom.TOId,tom.InqId,inq.InqNo,inq.BuyerName,tom.IsActive
                                    FROM gurjari_crmuser.TOMaster tom  WITH(nolock) 
                                    INNER JOIN gurjari_crmuser.InquiryMaster inq  WITH(nolock) ON inq.InqId=tom.InqId
                                    WHERE tom.TOId =@TOId AND ISNULL(tom.IsActive,0)=1", para).ConvertToList<TOModel>().AsQueryable().FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public IQueryable<TOModel> GetAllTO()
        {
            try
            {
                return odal.selectbyquerydt(@"SELECT tom.TOId,tom.InqId,inq.InqNo,tom.ToTypeId,tot.TOType,tom.Remark,tom.IsActive
                                    FROM gurjari_crmuser.TOMaster tom
                                    INNER JOIN gurjari_crmuser.InquiryMaster inq ON inq.InqId=tom.InqId
                                    INNER JOIN gurjari_crmuser.TOTypeMaster tot ON tot.TOTypeId=tom.ToTypeId
                                    WHERE ISNULL(tom.IsActive,0)=1").ConvertToList<TOModel>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public int Delete(TOModel obj)
        {
            int resVal;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    TOMaster ObjTOMaster = context.TOMasters.Find(obj.TOId);
                    ObjTOMaster.IsActive = false;
                    ObjTOMaster.DeletedBy = obj.DeletedBy;
                    ObjTOMaster.DeletedDate = DateTime.Now;
                    context.Entry(ObjTOMaster).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();

                    List<TOItemMaster> ObjTOItemMaster = context.TOItemMasters.Where(x => x.TOId == obj.TOId).ToList();
                    if (ObjTOItemMaster.Count > 0)
                    {
                        foreach (var item in ObjTOItemMaster)
                        {
                            context.TOItemMasters.Remove(item);
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

        public TechnicalSpecMaster getSpec(int id)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter().CreateParameter("@id", id);
            return odal.GetDataTable_Text(@"select * from  TechnicalSpecMaster where SpecificationId=@id", para).ConvertToList<TechnicalSpecMaster>().FirstOrDefault();
        }

        public IQueryable<TechnicleDetail> GetTechnicalDetailbyToid(int id)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter().CreateParameter("@TOId", id);
            return odal.GetDataTable_Text(@"select * from  TOTechnicalDetail As T inner join TechnicalSpecMaster As S on S.SpecificationId=T.TechParaID inner join TechnicalSpecHeadMaster as H on H.techheadId=S.TechHeadId  where TOId=@TOId", para).ConvertToList<TechnicleDetail>().AsQueryable();

        }

        public void deletetechnicaldetail(int pid, int spectype)
        {
            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter().CreateParameter("@pid", pid);
            para[1] = new SqlParameter().CreateParameter("@spectype", spectype);
            odal.GetDataTable_Text(@"delete  from  TOTechnicalDetail  where ProductId=@pid and TechSpecifType=@spectype", para);
        }
    }
}
