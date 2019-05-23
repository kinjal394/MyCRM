using CRM_Repository.Data;
using CRM_Repository.DTOModel;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using CRM_Repository.DataServices;
using System.Data.SqlClient;

namespace CRM_Repository.Service
{
    public class Exhibition_Repository : IExhibition_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        dalc odal = new dalc();
        public Exhibition_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public ExhibitionMaster AddExhibition(ExhibitionMaster obj)
        {
            try
            {
                context.ExhibitionMasters.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ExhibitionModel FetchById(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ExId", id);
                return odal.GetDataTable_Text(@"Select BM.*,
                                            CO.CountryId,CO.CountryName,S.StateId,S.StateName,CT.CityId,CT.CityName,BM.TelCode1,CT1.CountryCallCode As CountryCallCode1,BM.TelCode2,CT2.CountryCallCode As CountryCallCode2
                                            from ExhibitionMaster As BM  with(nolock)
                                            Inner join CityMaster As CT  with(nolock) on CT.CityId = BM.CityId
                                            Inner join StateMaster As S  with(nolock) on S.StateId = CT.StateId
                                            Inner join CountryMaster As  CO with(nolock)on Co.CountryId = S.CountryId
                                            Inner join CountryMaster As  CT1 with(nolock) on CT1.CountryId = BM.TelCode1
                                            Inner join CountryMaster As  CT2 with(nolock) on CT2.CountryId = BM.TelCode2 
                                            Where BM.ExId = @ExId AND ISNULL(BM.IsActive,0)=1", para).ConvertToList<ExhibitionModel>().AsQueryable().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int CreateUpdate(ExhibitionModel exibitionobj)
        {
            int resVal;
            try
            {
                ExhibitionMaster Objexibition = new ExhibitionMaster();

                //if (exibitionobj.AreaId == 0)
                //{
                //    exibitionobj.AreaId = context.CityMasters.Join(
                //                context.CityMasters,
                //                A => new { A.CityId },
                //                C => new { C.CityId },
                //                (A, C) => new { A, C })
                //                .Select(X => X.A).ToList().Single().CityId;
                //}
                if (exibitionobj.ExId <= 0)
                {
                    #region "INSERT"
                    Objexibition.ExId = exibitionobj.ExId;
                    Objexibition.ExName = exibitionobj.ExName;
                    Objexibition.Address = exibitionobj.Address;
                    Objexibition.CityId = exibitionobj.CityId;
                    Objexibition.Venue = exibitionobj.Venue;
                    //  Objexibition.NoofYears = exibitionobj.NoofYears;
                    Objexibition.ExProfile = exibitionobj.ExProfile;
                    Objexibition.OrganizerDetail = exibitionobj.OrganizerDetail;
                    //  Objexibition.BankDetail = exibitionobj.BankDetail;
                    Objexibition.ExDate = exibitionobj.ExDate;
                    Objexibition.Tel = exibitionobj.Tel;
                    Objexibition.MobileNo = exibitionobj.MobileNo;
                    Objexibition.Email = exibitionobj.Email;
                    Objexibition.Web = exibitionobj.Web;
                    Objexibition.ContactPerson = exibitionobj.ContactPerson;
                    Objexibition.Chat = exibitionobj.Chat;
                    Objexibition.CreatedBy = exibitionobj.CreatedBy;
                    Objexibition.CreatedDate = DateTime.Now;
                    Objexibition.IsActive = true;
                    Objexibition = AddExhibition(Objexibition);
                    resVal = 1;
                    #endregion
                }
                else
                {
                    #region "UPDATE"
                    Objexibition = context.ExhibitionMasters.Find(exibitionobj.ExId);
                    //Objexibition.ExId = exibitionobj.ExId;
                    Objexibition.ExName = exibitionobj.ExName;
                    Objexibition.Address = exibitionobj.Address;
                    Objexibition.CityId = exibitionobj.CityId;
                    Objexibition.Venue = exibitionobj.Venue;
                    // Objexibition.NoofYears = exibitionobj.NoofYears;
                    Objexibition.ExProfile = exibitionobj.ExProfile;
                    Objexibition.OrganizerDetail = exibitionobj.OrganizerDetail;
                    //  Objexibition.BankDetail = exibitionobj.BankDetail;
                    Objexibition.ExDate = exibitionobj.ExDate;
                    Objexibition.Tel = exibitionobj.Tel;
                    Objexibition.MobileNo = exibitionobj.MobileNo;
                    Objexibition.Email = exibitionobj.Email;
                    Objexibition.Web = exibitionobj.Web;
                    Objexibition.ContactPerson = exibitionobj.ContactPerson;
                    Objexibition.Chat = exibitionobj.Chat;
                    Objexibition.ModifyBy = exibitionobj.ModifyBy;
                    Objexibition.ModifyDate = DateTime.Now;
                    Objexibition.IsActive = true;
                    UpdateExhibition(Objexibition);
                    context.Entry(Objexibition).State = System.Data.Entity.EntityState.Modified;
                    resVal = 2;
                }
                #endregion
                context.SaveChanges();
            }
            //context.ExhibitionMasters.Add(exibitionobj);
            //context.SaveChanges();
            catch (Exception ex)
            {
                throw ex;
            }
            return resVal;
        }
        public bool CheckExhibitionType(ExhibitionMaster ExObj, bool isUpdate)
        {
            try
            {
                if (isUpdate)
                {
                    SqlParameter[] para = new SqlParameter[2];
                    para[0] = new SqlParameter().CreateParameter("@ExName", ExObj.ExName);
                    para[1] = new SqlParameter().CreateParameter("@ExId", ExObj.ExId);
                    return new dalc().GetDataTable_Text("SELECT * FROM ExhibitionMaster with(nolock) WHERE RTRIM(LTRIM(ExName)) = RTRIM(LTRIM(@ExName)) AND ExId <> @ExId AND IsActive = 1", para).Rows.Count > 0 ? true : false;

                }
                else
                {
                    SqlParameter[] para = new SqlParameter[1];
                    para[0] = new SqlParameter().CreateParameter("@ExName", ExObj.ExName);
                    return new dalc().GetDataTable_Text("SELECT * FROM ExhibitionMaster with(nolock) WHERE RTRIM(LTRIM(ExName)) = RTRIM(LTRIM(@ExName)) AND  IsActive = 1", para).Rows.Count > 0 ? true : false;

                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void DeleteExhibition(int id)
        {
            try
            {
                ExhibitionMaster exibitionobj = context.ExhibitionMasters.Find(id);
                if (exibitionobj != null)
                {
                    context.ExhibitionMasters.Remove(exibitionobj);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IQueryable<ExhibitionMaster> getAllExhibition()
        {
            try
            {
                return new dalc().selectbyquerydt("SELECT * FROM ExhibitionMaster with(nolock) ").ConvertToList<ExhibitionMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ExhibitionMaster GetExhibitionById(int ExId)
        {
            try
            {

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ExId", ExId);
                return new dalc().GetDataTable_Text("SELECT * FROM ExhibitionMaster with(nolock) WHERE ExId=@ExId", para).ConvertToList<ExhibitionMaster>().FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public void UpdateExhibition(ExhibitionMaster Exhibitionobj)
        {
            try
            {
                context.Entry(Exhibitionobj).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
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
