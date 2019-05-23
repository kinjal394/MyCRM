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
    public class Buyer_Repository : IBuyer_Repository, IDisposable
    {
        // dalc odal = new dalc();
        private CRM_Repository.Data.elaunch_crmEntities context;

        public Buyer_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public BuyerMaster AddBuyer(BuyerMaster obj)
        {
            try
            {
                context.BuyerMasters.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateBuyer(BuyerMaster obj)
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

        public void DeleteBuyer(int id)
        {
            try
            {
                BuyerMaster Buyer = context.BuyerMasters.Find(id);

                if (Buyer != null)
                {
                    context.BuyerMasters.Remove(Buyer);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BuyerModel FetchById(int id)
        {
            dalc odal = new dalc();
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@BuyerId", id);
                //BM.GST,BM.VAT,BM.CST,BM.PAN,BM.[TAN]
                return odal.GetDataTable_Text(@"Select BM.BuyerId,BM.CompanyName,BM.Email,BM.ContactType,ag.AgencyType,BM.AgencyTypeId,BM.Fax,BM.Telephone,BM.WebAddress,BM.CreatedBy,BM.CreatedDate,BM.ModifyBy,BM.ModifyDate,BM.DeletedBy,BM.DeletedDate,BM.IsActive,BM.Remark,ISNUll(BM.DocumentsData,'') As DocumentsData,ISNUll(BM.ConInvId,'') As ConInvId
                                            from BuyerMaster As BM  with(nolock)
                                            left join AgencyTypeMaster ag on ag.AgencyTypeId=BM.AgencyTypeId
                                            Where BM.BuyerId = @BuyerId AND ISNULL(BM.IsActive,0)=1", para).ConvertToList<BuyerModel>().AsQueryable().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BuyerMaster GetById(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@BuyerId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM BuyerMaster with(nolock) WHERE BuyerId = @BuyerId ", para).ConvertToList<BuyerMaster>().FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool CheckBuyerDuplication(BuyerModel Obj, bool isUpdate)
        {
            try
            {
                if (isUpdate)
                {
                    SqlParameter[] para = new SqlParameter[2];
                    para[0] = new SqlParameter().CreateParameter("@CompanyName", Obj.CompanyName);
                    para[1] = new SqlParameter().CreateParameter("@BuyerId", Obj.BuyerId);
                    return new dalc().GetDataTable_Text("SELECT * FROM BuyerMaster with(nolock) WHERE RTRIM(LTRIM(CompanyName)) = RTRIM(LTRIM(@CompanyName)) AND BuyerId <> @BuyerId AND IsActive = 1", para).Rows.Count > 0 ? true : false;

                }
                else
                {
                    SqlParameter[] para = new SqlParameter[1];
                    para[0] = new SqlParameter().CreateParameter("@CompanyName", Obj.CompanyName);
                    return new dalc().GetDataTable_Text("SELECT * FROM BuyerMaster with(nolock) WHERE RTRIM(LTRIM(CompanyName)) = RTRIM(LTRIM(@CompanyName)) AND  IsActive = 1", para).Rows.Count > 0 ? true : false;

                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public BuyerMaster GetConsigneById(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@BuyerId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM BuyerMaster with(nolock) WHERE BuyerId=@BuyerId AND IsActive = 1", para).ConvertToList<BuyerMaster>().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public BuyerAddressDetail GetConsigneAddressById(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@BuyerId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM BuyerAddressDetail with(nolock) WHERE AddressId=@BuyerId", para).ConvertToList<BuyerAddressDetail>().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public BuyerContactDetail GetConsigneContactById(int id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@BuyerId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM BuyerContactDetail with(nolock) WHERE ContactId=@BuyerId AND IsActive = 1", para).ConvertToList<BuyerContactDetail>().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<BuyerModel> GetBuyerById(int BuyerId)
        {
            dalc odal = new dalc();
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@BuyerId", BuyerId);
                return odal.GetDataTable_Text(@"Select BM.*,BAM.Address,BCM.Email As EmailAddress,BCM.MobileNo
                                            from BuyerMaster As BM  with(nolock)
                                            left join BuyerContactDetail as BCM with(nolock) on 
                                            BM.BuyerId = BCM.BuyerId left join BuyerAddressDetail as BAM with(nolock) on BAM.buyerId=BM.buyerId where BM.BuyerId =@BuyerId", para)
                                        .ConvertToList<BuyerModel>().AsQueryable();
                // }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<BuyerMaster> GetAllBuyer()
        {
            try
            {

                return new dalc().selectbyquerydt("SELECT * FROM BuyerMaster with(nolock)").ConvertToList<BuyerMaster>().AsQueryable();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<BuyerMaster> GetAllBuyerCompany(int UserId)
        {
            dalc odal = new dalc();
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@CreatedBy", UserId);
                return odal.GetDataTable_Text(@"SELECT BuyerId,CompanyName FROM BuyerMaster with(nolock) where IsActive = 1 AND CreatedBy=@CreatedBy", para).ConvertToList<BuyerMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<BuyerMaster> GetBuyerEmail(int id)
        {
            dalc odal = new dalc();
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@BuyerId", id);
                return odal.GetDataTable_Text(@"SELECT Email FROM BuyerMaster with(nolock) where IsActive = 1 AND BuyerId=@BuyerId", para).ConvertToList<BuyerMaster>().AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BuyerMaster CreateUpdate(BuyerModel objInputBuyerMaster)
        {
            BuyerMaster resVal;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {

                    BuyerMaster ObjBuyerMastyer = new BuyerMaster();
                    if (objInputBuyerMaster.BuyerId <= 0)
                    {
                        #region "INSERT"
                        ObjBuyerMastyer.CompanyName = objInputBuyerMaster.CompanyName;
                        ObjBuyerMastyer.Email = objInputBuyerMaster.Email;
                        ObjBuyerMastyer.Fax = objInputBuyerMaster.Fax;
                        ObjBuyerMastyer.WebAddress = objInputBuyerMaster.WebAddress;
                        ObjBuyerMastyer.Telephone = objInputBuyerMaster.Telephone;
                        ObjBuyerMastyer.ContactType = objInputBuyerMaster.ContactType;
                        ObjBuyerMastyer.AgencyTypeId = objInputBuyerMaster.AgencyTypeId;
                        ObjBuyerMastyer.Remark = objInputBuyerMaster.Remark;
                        ObjBuyerMastyer.DocumentsData = objInputBuyerMaster.DocumentsData;
                        ObjBuyerMastyer.ConInvId = objInputBuyerMaster.ConInvId;
                        ObjBuyerMastyer.CreatedBy = objInputBuyerMaster.CreatedBy;
                        ObjBuyerMastyer.CreatedDate = DateTime.Now;
                        ObjBuyerMastyer.IsActive = true;
                        ObjBuyerMastyer = AddBuyer(ObjBuyerMastyer);

                        if (objInputBuyerMaster.BuyerContactDetails != null)
                        {
                            foreach (var item in objInputBuyerMaster.BuyerContactDetails)
                            {
                                BuyerContactDetail ObjBuyerContactDetail = new BuyerContactDetail();
                                ObjBuyerContactDetail.ContactId = item.ContactId;
                                ObjBuyerContactDetail.BuyerId = ObjBuyerMastyer.BuyerId;
                                ObjBuyerContactDetail.ContactPerson = item.ContactPerson;
                                ObjBuyerContactDetail.Surname = item.Surname;
                                ObjBuyerContactDetail.DesignationId = item.DesignationId;
                                ObjBuyerContactDetail.MobileNo = item.MobileNo;
                                ObjBuyerContactDetail.Email = item.Email;
                                ObjBuyerContactDetail.ChatDetails = item.Chat;
                                ObjBuyerContactDetail.IsActive = true;
                                context.BuyerContactDetails.Add(ObjBuyerContactDetail);
                            }
                        }
                        if (objInputBuyerMaster.BuyerAddressDetails != null)
                        {
                            // Buyer Address Detail
                            foreach (var additem in objInputBuyerMaster.BuyerAddressDetails)
                            {
                                BuyerAddressDetail ObjBuyerAddressDetail = new BuyerAddressDetail();
                                ObjBuyerAddressDetail.AddressId = additem.AddressId;
                                ObjBuyerAddressDetail.BuyerId = ObjBuyerMastyer.BuyerId;
                                ObjBuyerAddressDetail.CityId = additem.CityId;
                                ObjBuyerAddressDetail.Address = additem.Address;
                                ObjBuyerAddressDetail.PinCode = additem.PinCode;
                                ObjBuyerAddressDetail.WeeklyOff = additem.WeeklyOff;
                                ObjBuyerAddressDetail.Telephone = additem.Telephone;
                                ObjBuyerAddressDetail.Email = additem.Email;
                                ObjBuyerAddressDetail.WebAddress = additem.WebAddress;
                                ObjBuyerAddressDetail.Fax = additem.Fax;
                                ObjBuyerAddressDetail.AddressTypeId = additem.AddressTypeId;
                                //ObjBuyerAddressDetail.IsActive = true;
                                context.BuyerAddressDetails.Add(ObjBuyerAddressDetail);
                            }
                        }

                        if (objInputBuyerMaster.BuyerBankDetails != null)
                        {
                            // Buyer Bank Detail
                            foreach (var bankitem in objInputBuyerMaster.BuyerBankDetails)
                            {
                                BuyerBankDetail ObjBuyerBankDetail = new BuyerBankDetail();
                                ObjBuyerBankDetail.BankDetailID = bankitem.BankDetailID;
                                ObjBuyerBankDetail.BuyerId = ObjBuyerMastyer.BuyerId;
                                ObjBuyerBankDetail.BeneficiaryName = bankitem.BeneficiaryName;
                                ObjBuyerBankDetail.NickName = bankitem.NickName;
                                ObjBuyerBankDetail.BankNameId = bankitem.BankNameId;
                                ObjBuyerBankDetail.BranchName = bankitem.BranchName;
                                ObjBuyerBankDetail.AccountTypeId = bankitem.AccountTypeId;
                                ObjBuyerBankDetail.AccountNo = bankitem.AccountNo;
                                ObjBuyerBankDetail.IFSCCode = bankitem.IFSCCode;
                                ObjBuyerBankDetail.SwiftCode = bankitem.SwiftCode;
                                ObjBuyerBankDetail.IsActive = true;
                                context.BuyerBankDetails.Add(ObjBuyerBankDetail);
                            }
                        }
                        if (objInputBuyerMaster.BuyerLicenseDetails != null)
                        {
                            // Buyer License Detail
                            foreach (var taxitem in objInputBuyerMaster.BuyerLicenseDetails)
                            {
                                BuyerLicenseDetail ObjBuyerLicenseDetail = new BuyerLicenseDetail();
                                ObjBuyerLicenseDetail.BuyerLicenseId = taxitem.BuyerLicenseId;
                                ObjBuyerLicenseDetail.BuyerId = ObjBuyerMastyer.BuyerId;
                                ObjBuyerLicenseDetail.LicenseId = taxitem.LicenseId;
                                ObjBuyerLicenseDetail.LicenseNo = taxitem.LicenseNo;
                                context.BuyerLicenseDetails.Add(ObjBuyerLicenseDetail);
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region "UPDATE"
                        //EDIT
                        ObjBuyerMastyer = context.BuyerMasters.Find(objInputBuyerMaster.BuyerId);
                        ObjBuyerMastyer.CompanyName = objInputBuyerMaster.CompanyName;
                        ObjBuyerMastyer.Fax = objInputBuyerMaster.Fax;
                        ObjBuyerMastyer.WebAddress = objInputBuyerMaster.WebAddress;
                        ObjBuyerMastyer.Telephone = objInputBuyerMaster.Telephone;
                        ObjBuyerMastyer.ContactType = objInputBuyerMaster.ContactType;
                        ObjBuyerMastyer.AgencyTypeId = objInputBuyerMaster.AgencyTypeId;
                        ObjBuyerMastyer.Remark = objInputBuyerMaster.Remark;
                        ObjBuyerMastyer.DocumentsData = objInputBuyerMaster.DocumentsData;
                        ObjBuyerMastyer.ConInvId = objInputBuyerMaster.ConInvId;
                        ObjBuyerMastyer.ModifyBy = objInputBuyerMaster.ModifyBy;
                        ObjBuyerMastyer.ModifyDate = DateTime.Now;
                        UpdateBuyer(ObjBuyerMastyer);
                        // Buyer Contact Detail
                        if (objInputBuyerMaster.BuyerContactDetails != null)
                        {
                            foreach (var item in objInputBuyerMaster.BuyerContactDetails)
                            {
                                if (item.Status == 1)
                                {
                                    BuyerContactDetail ObjBuyerContactDetail = new BuyerContactDetail();
                                    //ADD
                                    ObjBuyerContactDetail.ContactId = item.ContactId;
                                    ObjBuyerContactDetail.BuyerId = ObjBuyerMastyer.BuyerId;
                                    ObjBuyerContactDetail.ContactPerson = item.ContactPerson;
                                    ObjBuyerContactDetail.Surname = item.Surname;
                                    ObjBuyerContactDetail.DesignationId = item.DesignationId;
                                    ObjBuyerContactDetail.MobileNo = item.MobileNo;
                                    ObjBuyerContactDetail.Email = item.Email;
                                    ObjBuyerContactDetail.ChatDetails = item.Chat;
                                    ObjBuyerContactDetail.IsActive = true;
                                    context.BuyerContactDetails.Add(ObjBuyerContactDetail);
                                }
                                else if (item.Status == 2 || item.Status == 3)
                                {
                                    BuyerContactDetail ObjBuyerContactDetail = context.BuyerContactDetails.Find(item.ContactId);
                                    ObjBuyerContactDetail.ContactId = item.ContactId;
                                    ObjBuyerContactDetail.BuyerId = ObjBuyerMastyer.BuyerId;
                                    ObjBuyerContactDetail.ContactPerson = item.ContactPerson;
                                    ObjBuyerContactDetail.Surname = item.Surname;
                                    ObjBuyerContactDetail.DesignationId = item.DesignationId;
                                    ObjBuyerContactDetail.MobileNo = item.MobileNo;
                                    ObjBuyerContactDetail.Email = item.Email;
                                    ObjBuyerContactDetail.ChatDetails = item.Chat;
                                    if (item.Status == 3)
                                        ObjBuyerContactDetail.IsActive = false;
                                    context.Entry(ObjBuyerContactDetail).State = System.Data.Entity.EntityState.Modified;
                                }
                            }
                        }
                        // Buyer Address Detail
                        if (objInputBuyerMaster.BuyerAddressDetails != null)
                        {
                            foreach (var additem in objInputBuyerMaster.BuyerAddressDetails)
                            {
                                if (additem.Status == 1)
                                {
                                    BuyerAddressDetail ObjBuyerAddressDetail = new BuyerAddressDetail();
                                    ObjBuyerAddressDetail.AddressId = additem.AddressId;
                                    ObjBuyerAddressDetail.BuyerId = ObjBuyerMastyer.BuyerId;
                                    ObjBuyerAddressDetail.CityId = additem.CityId;
                                    ObjBuyerAddressDetail.Address = additem.Address;
                                    ObjBuyerAddressDetail.PinCode = additem.PinCode;
                                    ObjBuyerAddressDetail.WeeklyOff = additem.WeeklyOff;
                                    ObjBuyerAddressDetail.Telephone = additem.Telephone;
                                    ObjBuyerAddressDetail.Email = additem.Email;
                                    ObjBuyerAddressDetail.WebAddress = additem.WebAddress;
                                    ObjBuyerAddressDetail.Fax = additem.Fax;
                                    ObjBuyerAddressDetail.AddressTypeId = additem.AddressTypeId;
                                    //ObjBuyerAddressDetail.IsActive = true;
                                    context.BuyerAddressDetails.Add(ObjBuyerAddressDetail);
                                }
                                else if (additem.Status == 2 || additem.Status == 3)
                                {
                                    BuyerAddressDetail ObjBuyerAddressDetail = context.BuyerAddressDetails.Find(additem.AddressId);
                                    ObjBuyerAddressDetail.AddressId = additem.AddressId;
                                    ObjBuyerAddressDetail.BuyerId = ObjBuyerMastyer.BuyerId;
                                    ObjBuyerAddressDetail.CityId = additem.CityId;
                                    ObjBuyerAddressDetail.Address = additem.Address;
                                    ObjBuyerAddressDetail.PinCode = additem.PinCode;
                                    ObjBuyerAddressDetail.WeeklyOff = additem.WeeklyOff;
                                    ObjBuyerAddressDetail.Telephone = additem.Telephone;
                                    ObjBuyerAddressDetail.Email = additem.Email;
                                    ObjBuyerAddressDetail.WebAddress = additem.WebAddress;
                                    ObjBuyerAddressDetail.Fax = additem.Fax;
                                    ObjBuyerAddressDetail.AddressTypeId = additem.AddressTypeId;
                                    if (additem.Status == 2)
                                    {
                                        //ObjBuyerAddressDetail.IsActive = true;
                                        context.Entry(ObjBuyerAddressDetail).State = System.Data.Entity.EntityState.Modified;
                                    }
                                    else if (additem.Status == 3)
                                    {
                                        //ObjBuyerAddressDetail.IsActive = false;
                                        context.BuyerAddressDetails.Remove(ObjBuyerAddressDetail);
                                    }
                                }
                            }
                        }
                           
                        // Buyer Bank Detail
                        if (objInputBuyerMaster.BuyerBankDetails != null)
                        {
                            foreach (var bankitem in objInputBuyerMaster.BuyerBankDetails)
                            {
                                if (bankitem.Status == 1)
                                {
                                    BuyerBankDetail ObjBuyerBankDetail = new BuyerBankDetail();
                                    ObjBuyerBankDetail.BankDetailID = bankitem.BankDetailID;
                                    ObjBuyerBankDetail.BuyerId = ObjBuyerMastyer.BuyerId;
                                    ObjBuyerBankDetail.BeneficiaryName = bankitem.BeneficiaryName;
                                    ObjBuyerBankDetail.NickName = bankitem.NickName;
                                    ObjBuyerBankDetail.BankNameId = bankitem.BankNameId;
                                    ObjBuyerBankDetail.BranchName = bankitem.BranchName;
                                    ObjBuyerBankDetail.AccountNo = bankitem.AccountNo;
                                    ObjBuyerBankDetail.IFSCCode = bankitem.IFSCCode;
                                    ObjBuyerBankDetail.SwiftCode = bankitem.SwiftCode;
                                    ObjBuyerBankDetail.AccountTypeId = bankitem.AccountTypeId;
                                    ObjBuyerBankDetail.IsActive = true;
                                    context.BuyerBankDetails.Add(ObjBuyerBankDetail);
                                }
                                else if (bankitem.Status == 2 || bankitem.Status == 3)
                                {
                                    BuyerBankDetail ObjBuyerBankDetail = context.BuyerBankDetails.Find(bankitem.BankDetailID);
                                    ObjBuyerBankDetail.BankDetailID = bankitem.BankDetailID;
                                    ObjBuyerBankDetail.BuyerId = ObjBuyerMastyer.BuyerId;
                                    ObjBuyerBankDetail.BeneficiaryName = bankitem.BeneficiaryName;
                                    ObjBuyerBankDetail.NickName = bankitem.NickName;
                                    ObjBuyerBankDetail.BankNameId = bankitem.BankNameId;
                                    ObjBuyerBankDetail.BranchName = bankitem.BranchName;
                                    ObjBuyerBankDetail.AccountNo = bankitem.AccountNo;
                                    ObjBuyerBankDetail.IFSCCode = bankitem.IFSCCode;
                                    ObjBuyerBankDetail.SwiftCode = bankitem.SwiftCode;
                                    ObjBuyerBankDetail.AccountTypeId = bankitem.AccountTypeId;
                                    if (bankitem.Status == 2)
                                    {
                                        ObjBuyerBankDetail.IsActive = true;
                                    }
                                    else if (bankitem.Status == 3)
                                    {
                                        ObjBuyerBankDetail.IsActive = false;
                                    }
                                    context.Entry(ObjBuyerBankDetail).State = System.Data.Entity.EntityState.Modified;
                                }
                            }
                        }


                        // Buyer Tax Detail
                        if (objInputBuyerMaster.BuyerLicenseDetails != null)
                        {
                            foreach (var taxitem in objInputBuyerMaster.BuyerLicenseDetails)
                            {
                                if (taxitem.Status == 1)
                                {
                                    BuyerLicenseDetail ObjBuyerLicenseDetail = new BuyerLicenseDetail();
                                    ObjBuyerLicenseDetail.BuyerLicenseId = taxitem.BuyerLicenseId;
                                    ObjBuyerLicenseDetail.BuyerId = ObjBuyerMastyer.BuyerId;
                                    ObjBuyerLicenseDetail.LicenseId = taxitem.LicenseId;
                                    ObjBuyerLicenseDetail.LicenseNo = taxitem.LicenseNo;
                                    context.BuyerLicenseDetails.Add(ObjBuyerLicenseDetail);
                                }
                                else if (taxitem.Status == 2 || taxitem.Status == 3)
                                {
                                    BuyerLicenseDetail ObjBuyerLicenseDetail = context.BuyerLicenseDetails.Find(taxitem.BuyerLicenseId);
                                    ObjBuyerLicenseDetail.BuyerId = ObjBuyerMastyer.BuyerId;
                                    ObjBuyerLicenseDetail.LicenseId = taxitem.LicenseId;
                                    ObjBuyerLicenseDetail.LicenseNo = taxitem.LicenseNo;
                                    if (taxitem.Status == 2)
                                    {
                                        context.Entry(ObjBuyerLicenseDetail).State = System.Data.Entity.EntityState.Modified;
                                    }
                                    else if (taxitem.Status == 3)
                                    {
                                        context.BuyerLicenseDetails.Remove(ObjBuyerLicenseDetail);
                                    }
                                }
                            }
                        }
                           
                        #endregion 
                    }
                    resVal = ObjBuyerMastyer;
                    context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    resVal = null;
                }
            }
            return resVal;
        }

        public int Delete(BuyerModel objInputBuyer)
        {
            int resVal;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    BuyerMaster ObjBuyerMaster = context.BuyerMasters.Find(objInputBuyer.BuyerId);
                    ObjBuyerMaster.IsActive = false;
                    ObjBuyerMaster.DeletedBy = objInputBuyer.DeleteBy;
                    ObjBuyerMaster.DeletedDate = DateTime.Now;
                    UpdateBuyer(ObjBuyerMaster);

                    List<BuyerContactDetail> ObjBuyerContactMaster = context.BuyerContactDetails.Where(x => x.BuyerId == objInputBuyer.BuyerId).ToList();
                    if (ObjBuyerContactMaster.Count > 0)
                    {
                        foreach (var item in ObjBuyerContactMaster)
                        {
                            BuyerContactDetail ObjBuyerDetail = context.BuyerContactDetails.Find(item.ContactId);
                            item.IsActive = false;
                            context.Entry(ObjBuyerDetail).State = System.Data.Entity.EntityState.Modified;
                        }
                    }

                    List<BuyerBankDetail> ObjBuyerBankMaster = context.BuyerBankDetails.Where(x => x.BuyerId == objInputBuyer.BuyerId).ToList();
                    if (ObjBuyerBankMaster.Count > 0)
                    {
                        foreach (var item in ObjBuyerBankMaster)
                        {
                            BuyerBankDetail ObjBankDetail = context.BuyerBankDetails.Find(item.BankDetailID);
                            item.IsActive = false;
                            context.Entry(ObjBankDetail).State = System.Data.Entity.EntityState.Modified;
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
