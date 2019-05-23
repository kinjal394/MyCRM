using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using CRM_Repository.DTOModel;
using CRM_Repository.DataServices;
using System.Data.SqlClient;
namespace CRM_Repository.Service
{
    public class Vendor_Repository : IVendor_Repository, IDisposable
    {
        dalc odal = new dalc();
        private CRM_Repository.Data.elaunch_crmEntities context;
        public Vendor_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public VendorMaster AddVendor(VendorMaster obj)
        {
            try
            {
                context.VendorMasters.Add(obj);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void UpdateVendor(VendorMaster obj)
        {
            try
            {
                context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void DeleteVendor(int id)
        {
            try
            {
                VendorMaster Vendor = context.VendorMasters.Find(id);
                if (Vendor != null)
                {
                    context.VendorMasters.Remove(Vendor);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public int CreateUpdate(VendorModel objInputVendorMaster)
        {
            int resVal;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    VendorMaster ObjVendorMaster = new VendorMaster();


                    if (objInputVendorMaster.VendorId <= 0)
                    {
                        #region INSERT
                        //ADD
                        ObjVendorMaster.AgencyTypeId = objInputVendorMaster.AgencyTypeId;
                        ObjVendorMaster.CompanyName = objInputVendorMaster.CompanyName;
                        //ObjVendorMaster.Address = objInputVendorMaster.Address;
                        //ObjVendorMaster.CityId = objInputVendorMaster.CityId;
                        //ObjVendorMaster.Fax = objInputVendorMaster.Fax;
                        ObjVendorMaster.Website = objInputVendorMaster.Website;
                        ObjVendorMaster.Remark = objInputVendorMaster.Remark;
                        //ObjVendorMaster.GST = objInputVendorMaster.GST;
                        //ObjVendorMaster.PAN = objInputVendorMaster.PAN;
                        //ObjVendorMaster.TAN = objInputVendorMaster.TAN;
                        //ObjVendorMaster.ServiceTaxNo = objInputVendorMaster.ServiceTaxNo;
                        ObjVendorMaster.CreatedBy = objInputVendorMaster.CreatedBy;
                        ObjVendorMaster.CreatedDate = DateTime.Now;
                        ObjVendorMaster.IsActive = true;
                        ObjVendorMaster = AddVendor(ObjVendorMaster);

                        if (objInputVendorMaster.VendorAddressDetails != null)
                        {
                            // Vendor Address Detail
                            foreach (var additem in objInputVendorMaster.VendorAddressDetails)
                            {
                                VendorAddressDetail ObjVendorAddressDetail = new VendorAddressDetail();
                                ObjVendorAddressDetail.AddressId = additem.AddressId;
                                ObjVendorAddressDetail.VendorId = ObjVendorMaster.VendorId;
                                ObjVendorAddressDetail.CityId = additem.CityId;
                                ObjVendorAddressDetail.Address = additem.Address;
                                ObjVendorAddressDetail.Pincode = additem.Pincode;
                                ObjVendorAddressDetail.Fax = additem.Fax;
                                //ObjVendorAddressDetail.IsActive = true;
                                context.VendorAddressDetails.Add(ObjVendorAddressDetail);
                            }
                        }
                        if (objInputVendorMaster.VendorContactDetails != null)
                        {
                            // Vendor Contact Detail
                            foreach (var item in objInputVendorMaster.VendorContactDetails)
                            {
                                VendorContactDetail ObjVendorContactDetail = new VendorContactDetail();
                                //ADD
                                ObjVendorContactDetail.ContactId = item.ContactId;
                                ObjVendorContactDetail.VendorId = ObjVendorMaster.VendorId;
                                ObjVendorContactDetail.ContactName = item.ContactName;
                                ObjVendorContactDetail.MobileNo = item.MobileNo;
                                ObjVendorContactDetail.DesignationId = item.DesignationId;
                                ObjVendorContactDetail.Email = item.Email;
                                ObjVendorContactDetail.IsActive = true;
                                ObjVendorContactDetail.Surname = item.Surname;
                                context.VendorContactDetails.Add(ObjVendorContactDetail);
                            }
                        }

                        if (objInputVendorMaster.VendorBankDetails != null)
                        {
                            foreach (var bankitem in objInputVendorMaster.VendorBankDetails)
                            {
                                VendorBankMaster ObjVendorBankDetail = new VendorBankMaster();
                                ObjVendorBankDetail.VendorId = ObjVendorMaster.VendorId;
                                ObjVendorBankDetail.BankDetailId = bankitem.BankDetailId;
                                ObjVendorBankDetail.VendorId = ObjVendorMaster.VendorId;
                                ObjVendorBankDetail.BeneficiaryName = bankitem.BeneficiaryName;
                                ObjVendorBankDetail.NickName = bankitem.NickName;
                                ObjVendorBankDetail.BankNameId = bankitem.BankNameId;
                                ObjVendorBankDetail.BranchName = bankitem.BranchName;
                                ObjVendorBankDetail.AccountTypeId = bankitem.AccountTypeId;
                                ObjVendorBankDetail.AccountNo = bankitem.AccountNo;
                                ObjVendorBankDetail.IFSCCode = bankitem.IFSCCode;
                                ObjVendorBankDetail.SwiftCode = bankitem.SwiftCode;
                                ObjVendorBankDetail.IsActive = true;
                                ObjVendorBankDetail.CreatedBy = objInputVendorMaster.CreatedBy;
                                ObjVendorBankDetail.CreatedDate = DateTime.Now;
                                context.VendorBankMasters.Add(ObjVendorBankDetail);
                            }
                        }
                        if (objInputVendorMaster.VendorChatDetails != null)
                        {
                            // Vendor Chat Detail
                            foreach (var chatitem in objInputVendorMaster.VendorChatDetails)
                            {
                                VendorChatMaster ObjVendorChatDetail = new VendorChatMaster();
                                ObjVendorChatDetail.VendorChatId = chatitem.VendorChatId;
                                ObjVendorChatDetail.VendorId = ObjVendorMaster.VendorId;
                                ObjVendorChatDetail.ChatId = chatitem.ChatId;
                                ObjVendorChatDetail.ChatValue = chatitem.ChatValue;
                                ObjVendorChatDetail.IsActive = true;
                                context.VendorChatMasters.Add(ObjVendorChatDetail);
                            }
                        }
                        resVal = 1;
                        #endregion
                    }
                    else
                    {
                        #region EDIT
                        //EDIT
                        ObjVendorMaster = context.VendorMasters.Find(objInputVendorMaster.VendorId);
                        ObjVendorMaster.AgencyTypeId = objInputVendorMaster.AgencyTypeId;
                        ObjVendorMaster.CompanyName = objInputVendorMaster.CompanyName;
                        //ObjVendorMaster.Address = objInputVendorMaster.Address;
                        //ObjVendorMaster.CityId = objInputVendorMaster.CityId;
                        //ObjVendorMaster.Fax = objInputVendorMaster.Fax;
                        ObjVendorMaster.Website = objInputVendorMaster.Website;
                        ObjVendorMaster.Remark = objInputVendorMaster.Remark;
                        //ObjVendorMaster.VAT = objInputVendorMaster.VAT;
                        //ObjVendorMaster.CST = objInputVendorMaster.CST;
                        //ObjVendorMaster.PAN = objInputVendorMaster.PAN;
                        //ObjVendorMaster.TAN = objInputVendorMaster.TAN;
                        //ObjVendorMaster.ServiceTaxNo = objInputVendorMaster.ServiceTaxNo;
                        ObjVendorMaster.ModifyBy = objInputVendorMaster.ModifyBy;
                        ObjVendorMaster.ModifyDate = DateTime.Now;
                        ObjVendorMaster.Remark = objInputVendorMaster.Remark;
                        UpdateVendor(ObjVendorMaster);

                        // Supplier Address Detail
                        if (objInputVendorMaster.VendorAddressDetails != null)
                        {
                            foreach (var additem in objInputVendorMaster.VendorAddressDetails)
                            {
                                if (additem.Status == 1)
                                {
                                    VendorAddressDetail ObjVendorAddressDetail = new VendorAddressDetail();
                                    ObjVendorAddressDetail.AddressId = additem.AddressId;
                                    ObjVendorAddressDetail.VendorId = ObjVendorMaster.VendorId;
                                    ObjVendorAddressDetail.CityId = additem.CityId;
                                    ObjVendorAddressDetail.Address = additem.Address;
                                    ObjVendorAddressDetail.Pincode = additem.Pincode;
                                    ObjVendorAddressDetail.Fax = additem.Fax;
                                    //ObjVendorAddressDetail.IsActive = true;
                                    context.VendorAddressDetails.Add(ObjVendorAddressDetail);
                                }
                                else if (additem.Status == 2 || additem.Status == 3)
                                {
                                    VendorAddressDetail ObjVendorAddressDetail = context.VendorAddressDetails.Find(additem.AddressId);
                                    ObjVendorAddressDetail.AddressId = additem.AddressId;
                                    ObjVendorAddressDetail.VendorId = ObjVendorMaster.VendorId;
                                    ObjVendorAddressDetail.CityId = additem.CityId;
                                    ObjVendorAddressDetail.Address = additem.Address;
                                    ObjVendorAddressDetail.Pincode = additem.Pincode;
                                    ObjVendorAddressDetail.Fax = additem.Fax;
                                    if (additem.Status == 2)
                                    {
                                        //ObjVendorAddressDetail.IsActive = true;
                                        context.Entry(ObjVendorAddressDetail).State = System.Data.Entity.EntityState.Modified;
                                    }
                                    else if (additem.Status == 3)
                                    {
                                        //ObjVendorAddressDetail.IsActive = false;
                                        context.VendorAddressDetails.Remove(ObjVendorAddressDetail);
                                    }
                                    //context.Entry(ObjVendorAddressDetail).State = System.Data.Entity.EntityState.Modified;
                                }
                            }
                        }
                            
                        if (objInputVendorMaster.VendorContactDetails != null)
                        {
                            foreach (var item in objInputVendorMaster.VendorContactDetails)
                            {
                                if (item.Status == 1)
                                {
                                    VendorContactDetail ObjVendorContactDetail = new VendorContactDetail();
                                    //ADD
                                    ObjVendorContactDetail.ContactId = item.ContactId;
                                    ObjVendorContactDetail.VendorId = ObjVendorMaster.VendorId;
                                    ObjVendorContactDetail.ContactName = item.ContactName;
                                    ObjVendorContactDetail.MobileNo = item.MobileNo;
                                    ObjVendorContactDetail.DesignationId = item.DesignationId;
                                    ObjVendorContactDetail.Email = item.Email;
                                    ObjVendorContactDetail.IsActive = true;
                                    ObjVendorContactDetail.Surname = item.Surname;
                                    context.VendorContactDetails.Add(ObjVendorContactDetail);
                                }
                                else if (item.Status == 2 || item.Status == 3)
                                {
                                    VendorContactDetail ObjVendorContactDetail = context.VendorContactDetails.Find(item.ContactId);
                                    ObjVendorContactDetail.ContactId = item.ContactId;
                                    ObjVendorContactDetail.VendorId = ObjVendorMaster.VendorId;
                                    ObjVendorContactDetail.ContactName = item.ContactName;
                                    ObjVendorContactDetail.MobileNo = item.MobileNo;
                                    ObjVendorContactDetail.DesignationId = item.DesignationId;
                                    ObjVendorContactDetail.Email = item.Email;
                                    ObjVendorContactDetail.Surname = item.Surname;
                                    if (item.Status == 3)
                                        ObjVendorContactDetail.IsActive = false;
                                    context.Entry(ObjVendorContactDetail).State = System.Data.Entity.EntityState.Modified;
                                }
                            }


                        }

                        if (objInputVendorMaster.VendorBankDetails != null)
                        {
                            foreach (var bankitem in objInputVendorMaster.VendorBankDetails)
                            {
                                if (bankitem.Status == 1)
                                {
                                    VendorBankMaster ObjVendorBankDetail = new VendorBankMaster();
                                    ObjVendorBankDetail.VendorId = ObjVendorMaster.VendorId;
                                    ObjVendorBankDetail.BankDetailId = bankitem.BankDetailId;
                                    ObjVendorBankDetail.VendorId = ObjVendorMaster.VendorId;
                                    ObjVendorBankDetail.BeneficiaryName = bankitem.BeneficiaryName;
                                    ObjVendorBankDetail.NickName = bankitem.NickName;
                                    ObjVendorBankDetail.BankNameId = bankitem.BankNameId;
                                    ObjVendorBankDetail.BranchName = bankitem.BranchName;
                                    ObjVendorBankDetail.AccountTypeId = bankitem.AccountTypeId;
                                    ObjVendorBankDetail.AccountNo = bankitem.AccountNo;
                                    ObjVendorBankDetail.IFSCCode = bankitem.IFSCCode;
                                    ObjVendorBankDetail.SwiftCode = bankitem.SwiftCode;
                                    ObjVendorBankDetail.IsActive = true;
                                    ObjVendorBankDetail.CreatedBy = objInputVendorMaster.CreatedBy;
                                    ObjVendorBankDetail.CreatedDate = DateTime.Now;
                                    context.VendorBankMasters.Add(ObjVendorBankDetail);
                                }
                                else if (bankitem.Status == 2 || bankitem.Status == 3)
                                {
                                    VendorBankMaster ObjVendorBankDetail = context.VendorBankMasters.Find(bankitem.BankDetailId);
                                    ObjVendorBankDetail.VendorId = ObjVendorMaster.VendorId;
                                    ObjVendorBankDetail.BankDetailId = bankitem.BankDetailId;
                                    ObjVendorBankDetail.VendorId = ObjVendorMaster.VendorId;
                                    ObjVendorBankDetail.BeneficiaryName = bankitem.BeneficiaryName;
                                    ObjVendorBankDetail.NickName = bankitem.NickName;
                                    ObjVendorBankDetail.BankNameId = bankitem.BankNameId;
                                    ObjVendorBankDetail.BranchName = bankitem.BranchName;
                                    ObjVendorBankDetail.AccountTypeId = bankitem.AccountTypeId;
                                    ObjVendorBankDetail.AccountNo = bankitem.AccountNo;
                                    ObjVendorBankDetail.IFSCCode = bankitem.IFSCCode;
                                    ObjVendorBankDetail.SwiftCode = bankitem.SwiftCode;
                                    if (bankitem.Status == 2)
                                    {
                                        ObjVendorBankDetail.IsActive = true;
                                        ObjVendorBankDetail.ModifyBy = objInputVendorMaster.ModifyBy;
                                        ObjVendorBankDetail.ModifyDate = DateTime.Now;
                                    }
                                    else if (bankitem.Status == 3)
                                    {
                                        ObjVendorBankDetail.IsActive = false;
                                        ObjVendorBankDetail.DeletedBy = objInputVendorMaster.DeletedBy;
                                        ObjVendorBankDetail.DeletedDate = DateTime.Now;
                                    }
                                    context.Entry(ObjVendorBankDetail).State = System.Data.Entity.EntityState.Modified;
                                }
                            }

                            if (objInputVendorMaster.VendorChatDetails != null)
                            {
                                foreach (var chatitem in objInputVendorMaster.VendorChatDetails)
                                {
                                    if (chatitem.Status == 1)
                                    {
                                        VendorChatMaster ObjVendorChatDetail = new VendorChatMaster();
                                        ObjVendorChatDetail.VendorChatId = chatitem.VendorChatId;
                                        ObjVendorChatDetail.VendorId = ObjVendorMaster.VendorId;
                                        ObjVendorChatDetail.ChatId = chatitem.ChatId;
                                        ObjVendorChatDetail.ChatValue = chatitem.ChatValue;
                                        ObjVendorChatDetail.IsActive = true;
                                        context.VendorChatMasters.Add(ObjVendorChatDetail);
                                    }
                                    else if (chatitem.Status == 2 || chatitem.Status == 3)
                                    {
                                        VendorChatMaster ObjVendorChatDetail = context.VendorChatMasters.Find(chatitem.VendorChatId);
                                        ObjVendorChatDetail.VendorChatId = chatitem.VendorChatId;
                                        ObjVendorChatDetail.VendorId = ObjVendorMaster.VendorId;
                                        ObjVendorChatDetail.ChatId = chatitem.ChatId;
                                        ObjVendorChatDetail.ChatValue = chatitem.ChatValue;
                                        ObjVendorChatDetail.IsActive = true;
                                        if (chatitem.Status == 2)
                                        {
                                            ObjVendorChatDetail.IsActive = true;
                                        }
                                        else if (chatitem.Status == 3)
                                        {
                                            ObjVendorChatDetail.IsActive = false;
                                        }
                                        context.Entry(ObjVendorChatDetail).State = System.Data.Entity.EntityState.Modified;
                                    }
                                }
                            }
                        }
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

        public VendorModel GetVendorById(int VendorId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@VendorId", VendorId);
                return odal.GetDataTable_Text(@"Select BM.VendorId,BM.CompanyName,BM.Website,BM.CreatedBy,BM.CreatedDate,BM.ModifyBy,BM.ModifyDate,
                                            BM.DeletedBy,BM.DeletedDate,BM.Remark,BM.GST,BM.PAN,BM.TAN,BM.ServiceTaxNo,BM.IsActive,
                                            BM.AgencyTypeId,AM.AgencyType
                                            from VendorMaster As BM  WITH(NOLOCK) 
                                            Inner join AgencyTypeMaster As  AM WITH(NOLOCK) on AM.AgencyTypeId = BM.AgencyTypeId
                                            Where BM.VendorId =@VendorId AND ISNULL(BM.IsActive,0)=1",para).ConvertToList<VendorModel>().AsQueryable().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<VendorMaster> GetAllVendor()
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var Vendors = context.VendorMasters.Where(x => x.IsActive == true);
                //    scope.Complete();
                //    return Vendors.AsQueryable();
                //}

                return new dalc().selectbyquerydt("SELECT * FROM VendorMaster with(nolock) WHERE IsActive = 1").ConvertToList<VendorMaster>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public int Delete(VendorModel objInputVendor)
        {
            int resVal;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    VendorMaster ObjVendorMaster = context.VendorMasters.Find(objInputVendor.VendorId);
                    ObjVendorMaster.IsActive = false;
                    ObjVendorMaster.DeletedBy = objInputVendor.DeletedBy;
                    ObjVendorMaster.DeletedDate = DateTime.Now;
                    UpdateVendor(ObjVendorMaster);

                    List<VendorContactDetail> ObjVendorDetailMaster = context.VendorContactDetails.Where(x => x.VendorId == objInputVendor.VendorId).ToList();
                    if (ObjVendorDetailMaster.Count > 0)
                    {
                        foreach (var item in ObjVendorDetailMaster)
                        {
                            VendorContactDetail ObjVendorDetail = context.VendorContactDetails.Find(item.ContactId);
                            item.IsActive = false;
                            context.Entry(ObjVendorDetail).State = System.Data.Entity.EntityState.Modified;
                        }
                    }

                    List<VendorBankMaster> ObjVendorBankDetails = context.VendorBankMasters.Where(x => x.VendorId == objInputVendor.VendorId).ToList();
                    if (ObjVendorBankDetails.Count > 0)
                    {
                        foreach (var item in ObjVendorBankDetails)
                        {
                            VendorBankMaster ObjVendorBankMaster = context.VendorBankMasters.Find(item.BankDetailId);
                            item.IsActive = false;
                            context.Entry(ObjVendorBankMaster).State = System.Data.Entity.EntityState.Modified;
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
    }
}






