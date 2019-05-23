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
    public class Supplier_Repository : ISupplier_Repository
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        dalc odal = new dalc();
        public Supplier_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public SupplierMaster AddSupplier(SupplierMaster obj)
        {
            try
            {
                context.SupplierMasters.Add(obj);
                //context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateSupplier(SupplierMaster obj)
        {
            try
            {
                context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                //context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteSupplier(int id)
        {
            try
            {
                SupplierMaster Supplier = context.SupplierMasters.Find(id);

                if (Supplier != null)
                {
                    context.SupplierMasters.Remove(Supplier);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SupplierModel FetchById(int SupplierId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SupplierId", SupplierId);
                return odal.GetDataTable_Text(@"Select BM.BuyerId,BM.CompanyName,BM.CreatedBy,BAM.Address,BCM.ContactPerson
                                            from BuyerMaster As BM inner join BuyerAddressDetail as BAM on BAM.BuyerId=BM.BuyerId
											inner join BuyercontactDetail as BCM on BCM.BuyerId=BM.BuyerId
                                            Where BM.BuyerId =@SupplierId AND ISNULL(BM.IsActive,0)=1", para).ConvertToList<SupplierModel>().AsQueryable().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SupplierMaster GetById(int SupplierId)
        {
            try
            {

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SupplierId", SupplierId);
                return odal.GetDataTable_Text(@"Select SM.*,SM.SupplierId
                                                        from SupplierMaster As SM With(NOLOCK)
                                                         where SM.SupplierId =@SupplierId", para).ConvertToList<SupplierMaster>().FirstOrDefault();


            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<SupplierMaster> GetAllSupplier()
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var Supplier = context.SupplierMasters;
                //    scope.Complete();
                //    return Supplier;
                //}

                return new dalc().selectbyquerydt("SELECT * FROM SupplierMaster with(nolock) ").ConvertToList<SupplierMaster>().AsQueryable();

            }
            catch (Exception)
            {
                throw;
            }
        }
        public int CreateUpdate(SupplierModel objInputSupplierMaster)
        {
            int resVal;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    SupplierMaster ObjSupplierMaster = new SupplierMaster();

                    if (objInputSupplierMaster.SupplierId <= 0)
                    {
                        #region "INSERT"
                        //ADD
                        ObjSupplierMaster.CompanyName = objInputSupplierMaster.CompanyName;
                        //ObjSupplierMaster.Address = objInputSupplierMaster.Address;
                        //ObjSupplierMaster.CityId = objInputSupplierMaster.CityId;
                        //ObjSupplierMaster.PinCode = objInputSupplierMaster.PinCode;
                        //ObjSupplierMaster.Fax = objInputSupplierMaster.Fax;
                        ObjSupplierMaster.Website = objInputSupplierMaster.Website;
                        ObjSupplierMaster.CreatedBy = objInputSupplierMaster.CreatedBy;
                        ObjSupplierMaster.CreatedDate = DateTime.Now;
                        ObjSupplierMaster.IsActive = true;
                        ObjSupplierMaster = AddSupplier(ObjSupplierMaster);

                        if (objInputSupplierMaster.SupplierAddressDetails != null)
                        {
                            // Supplier Address Detail
                            foreach (var additem in objInputSupplierMaster.SupplierAddressDetails)
                            {
                                SupplierAddressMaster ObjSupplierAddressDetail = new SupplierAddressMaster();
                                ObjSupplierAddressDetail.AddressId = additem.AddressId;
                                ObjSupplierAddressDetail.SupplierId = ObjSupplierMaster.SupplierId;
                                ObjSupplierAddressDetail.CityId = additem.CityId;
                                ObjSupplierAddressDetail.Address = additem.Address;
                                ObjSupplierAddressDetail.PinCode = additem.PinCode;
                                ObjSupplierAddressDetail.Fax = additem.Fax;
                                //ObjSupplierAddressDetail.IsActive = true;
                                context.SupplierAddressMasters.Add(ObjSupplierAddressDetail);
                            }
                        }
                        if (objInputSupplierMaster.SupplierContactDetails != null)
                        {
                            // Supplier Contact Detail
                            foreach (var item in objInputSupplierMaster.SupplierContactDetails)
                            {
                                SupplierContactDetail ObjSupplierContactDetail = new SupplierContactDetail();
                                ObjSupplierContactDetail.ContactId = item.ContactId;
                                ObjSupplierContactDetail.SupplierId = ObjSupplierMaster.SupplierId;
                                ObjSupplierContactDetail.ContactName = item.ContactName;
                                ObjSupplierContactDetail.Surname = item.Surname;
                                ObjSupplierContactDetail.MobileNo = item.MobileNo;
                                ObjSupplierContactDetail.Email = item.Email;
                                //ObjSupplierContactDetail.QQCode = item.QQCode;
                                //ObjSupplierContactDetail.Skype = item.Skype; ObjSupplierMaster
                                ObjSupplierContactDetail.IsActive = true;
                                context.SupplierContactDetails.Add(ObjSupplierContactDetail);
                            }
                        }
                        if (objInputSupplierMaster.SupplierBankDetails != null)
                        {
                            // Supplier Bank Detail
                            foreach (var bankitem in objInputSupplierMaster.SupplierBankDetails)
                            {
                                SupplierBankMaster ObjSupplierBankDetail = new SupplierBankMaster();
                                ObjSupplierBankDetail.BankDetailId = bankitem.BankDetailId;
                                ObjSupplierBankDetail.SupplierId = ObjSupplierMaster.SupplierId;
                                ObjSupplierBankDetail.BeneficiaryName = bankitem.BeneficiaryName;
                                ObjSupplierBankDetail.NickName = bankitem.NickName;
                                ObjSupplierBankDetail.BankNameId = bankitem.BankNameId;
                                ObjSupplierBankDetail.BranchName = bankitem.BranchName;
                                ObjSupplierBankDetail.AccountNo = bankitem.AccountNo;
                                ObjSupplierBankDetail.AccountTypeId = bankitem.AccountTypeId;
                                ObjSupplierBankDetail.IFSCCode = bankitem.IFSCCode;
                                ObjSupplierBankDetail.SwiftCode = bankitem.SwiftCode;
                                ObjSupplierBankDetail.IsActive = true;
                                context.SupplierBankMasters.Add(ObjSupplierBankDetail);
                            }
                        }
                        if (objInputSupplierMaster.SupplierChatDetails != null)
                        {
                            // Supplier Bank Detail
                            foreach (var chatitem in objInputSupplierMaster.SupplierChatDetails)
                            {
                                SupplierChatMaster ObjSupplierChatDetail = new SupplierChatMaster();
                                ObjSupplierChatDetail.SupplierChatId = chatitem.SupplierChatId;
                                ObjSupplierChatDetail.SupplierId = ObjSupplierMaster.SupplierId;
                                ObjSupplierChatDetail.ChatId = chatitem.ChatId;
                                ObjSupplierChatDetail.ChatValue = chatitem.ChatValue;
                                ObjSupplierChatDetail.IsActive = true;
                                context.SupplierChatMasters.Add(ObjSupplierChatDetail);
                            }
                        }
                        resVal = 1;
                        #endregion
                    }
                    else
                    {
                        #region "UPDATE"
                        //EDIT
                        ObjSupplierMaster = context.SupplierMasters.Find(objInputSupplierMaster.SupplierId);
                        ObjSupplierMaster.CompanyName = objInputSupplierMaster.CompanyName;
                        //ObjSupplierMaster.Address = objInputSupplierMaster.Address;
                        //ObjSupplierMaster.CityId = objInputSupplierMaster.CityId;
                        //ObjSupplierMaster.PinCode = objInputSupplierMaster.PinCode;
                        //ObjSupplierMaster.Fax = objInputSupplierMaster.Fax;
                        ObjSupplierMaster.Website = objInputSupplierMaster.Website;
                        ObjSupplierMaster.ModifyBy = objInputSupplierMaster.ModifyBy;
                        ObjSupplierMaster.ModifyDate = DateTime.Now;
                        UpdateSupplier(ObjSupplierMaster);
                        if (objInputSupplierMaster.SupplierAddressDetails != null)
                        {
                            // Supplier Address Detail
                            foreach (var additem in objInputSupplierMaster.SupplierAddressDetails)
                            {
                                if (additem.Status == 1)
                                {
                                    SupplierAddressMaster ObjSupplierAddressDetail = new SupplierAddressMaster();
                                    ObjSupplierAddressDetail.AddressId = additem.AddressId;
                                    ObjSupplierAddressDetail.SupplierId = ObjSupplierMaster.SupplierId;
                                    ObjSupplierAddressDetail.CityId = additem.CityId;
                                    ObjSupplierAddressDetail.Address = additem.Address;
                                    ObjSupplierAddressDetail.PinCode = additem.PinCode;
                                    ObjSupplierAddressDetail.Fax = additem.Fax;
                                    //ObjSupplierAddressDetail.IsActive = true;
                                    context.SupplierAddressMasters.Add(ObjSupplierAddressDetail);
                                }
                                else if (additem.Status == 2 || additem.Status == 3)
                                {
                                    SupplierAddressMaster ObjSupplierAddressDetail = context.SupplierAddressMasters.Find(additem.AddressId);
                                    ObjSupplierAddressDetail.AddressId = additem.AddressId;
                                    ObjSupplierAddressDetail.SupplierId = ObjSupplierMaster.SupplierId;
                                    ObjSupplierAddressDetail.CityId = additem.CityId;
                                    ObjSupplierAddressDetail.Address = additem.Address;
                                    ObjSupplierAddressDetail.PinCode = additem.PinCode;
                                    ObjSupplierAddressDetail.Fax = additem.Fax;
                                    if (additem.Status == 2)
                                    {
                                        //ObjSupplierAddressDetail.IsActive = true;
                                        context.Entry(ObjSupplierAddressDetail).State = System.Data.Entity.EntityState.Modified;
                                    }
                                    else if (additem.Status == 3)
                                    {
                                        //ObjSupplierAddressDetail.IsActive = false;
                                        context.SupplierAddressMasters.Remove(ObjSupplierAddressDetail);
                                    }
                                    //context.Entry(ObjSupplierAddressDetail).State = System.Data.Entity.EntityState.Modified;
                                }
                            }
                        }
                        if (objInputSupplierMaster.SupplierContactDetails != null)
                        {
                            // Supplier Contact Detail
                            foreach (var item in objInputSupplierMaster.SupplierContactDetails)
                            {
                                if (item.Status == 1)
                                {
                                    SupplierContactDetail ObjSupplierContactDetail = new SupplierContactDetail();
                                    ObjSupplierContactDetail.ContactId = item.ContactId;
                                    ObjSupplierContactDetail.SupplierId = ObjSupplierMaster.SupplierId;
                                    ObjSupplierContactDetail.ContactName = item.ContactName;
                                    ObjSupplierContactDetail.Surname = item.Surname;
                                    ObjSupplierContactDetail.MobileNo = item.MobileNo;
                                    ObjSupplierContactDetail.Email = item.Email;
                                    //ObjSupplierContactDetail.QQCode = item.QQCode;
                                    //ObjSupplierContactDetail.Skype = item.Skype;
                                    ObjSupplierContactDetail.IsActive = true;
                                    context.SupplierContactDetails.Add(ObjSupplierContactDetail);
                                }
                                else if (item.Status == 2 || item.Status == 3)
                                {
                                    SupplierContactDetail ObjSupplierContactDetail = context.SupplierContactDetails.Find(item.ContactId);
                                    ObjSupplierContactDetail.ContactId = item.ContactId;
                                    ObjSupplierContactDetail.SupplierId = ObjSupplierMaster.SupplierId;
                                    ObjSupplierContactDetail.ContactName = item.ContactName;
                                    ObjSupplierContactDetail.Surname = item.Surname;
                                    ObjSupplierContactDetail.MobileNo = item.MobileNo;
                                    ObjSupplierContactDetail.Email = item.Email;
                                    //ObjSupplierContactDetail.QQCode = item.QQCode;
                                    //ObjSupplierContactDetail.Skype = item.Skype;
                                    if (item.Status == 3)
                                        ObjSupplierContactDetail.IsActive = false;
                                    context.Entry(ObjSupplierContactDetail).State = System.Data.Entity.EntityState.Modified;
                                }
                            }
                            // Supplier Bank Detail
                            if (objInputSupplierMaster.SupplierBankDetails != null)
                            {
                                foreach (var bankitem in objInputSupplierMaster.SupplierBankDetails)
                                {
                                    if (bankitem.Status == 1)
                                    {
                                        SupplierBankMaster ObjSupplierBankDetail = new SupplierBankMaster();
                                        ObjSupplierBankDetail.BankDetailId = bankitem.BankDetailId;
                                        ObjSupplierBankDetail.SupplierId = ObjSupplierMaster.SupplierId;
                                        ObjSupplierBankDetail.BeneficiaryName = bankitem.BeneficiaryName;
                                        ObjSupplierBankDetail.NickName = bankitem.NickName;
                                        ObjSupplierBankDetail.BankNameId = bankitem.BankNameId;
                                        ObjSupplierBankDetail.BranchName = bankitem.BranchName;
                                        ObjSupplierBankDetail.AccountNo = bankitem.AccountNo;
                                        ObjSupplierBankDetail.AccountTypeId = bankitem.AccountTypeId;
                                        ObjSupplierBankDetail.IFSCCode = bankitem.IFSCCode;
                                        ObjSupplierBankDetail.SwiftCode = bankitem.SwiftCode;
                                        ObjSupplierBankDetail.IsActive = true;
                                        context.SupplierBankMasters.Add(ObjSupplierBankDetail);
                                    }
                                    else if (bankitem.Status == 2 || bankitem.Status == 3)
                                    {
                                        SupplierBankMaster ObjSupplierBankDetail = context.SupplierBankMasters.Find(bankitem.BankDetailId);
                                        ObjSupplierBankDetail.BankDetailId = bankitem.BankDetailId;
                                        ObjSupplierBankDetail.SupplierId = ObjSupplierMaster.SupplierId;
                                        ObjSupplierBankDetail.BeneficiaryName = bankitem.BeneficiaryName;
                                        ObjSupplierBankDetail.NickName = bankitem.NickName;
                                        ObjSupplierBankDetail.BankNameId = bankitem.BankNameId;
                                        ObjSupplierBankDetail.BranchName = bankitem.BranchName;
                                        ObjSupplierBankDetail.AccountNo = bankitem.AccountNo;
                                        ObjSupplierBankDetail.AccountTypeId = bankitem.AccountTypeId;
                                        ObjSupplierBankDetail.IFSCCode = bankitem.IFSCCode;
                                        ObjSupplierBankDetail.SwiftCode = bankitem.SwiftCode;
                                        if (bankitem.Status == 2)
                                        {
                                            ObjSupplierBankDetail.IsActive = true;
                                        }
                                        else if (bankitem.Status == 3)
                                        {
                                            ObjSupplierBankDetail.IsActive = false;
                                        }
                                        context.Entry(ObjSupplierBankDetail).State = System.Data.Entity.EntityState.Modified;
                                    }
                                }
                            }
                            // Supplier Chat Detail
                            if (objInputSupplierMaster.SupplierChatDetails != null)
                            {
                                foreach (var chatitem in objInputSupplierMaster.SupplierChatDetails)
                                {
                                    if (chatitem.Status == 1)
                                    {
                                        SupplierChatMaster ObjSupplierChatDetail = new SupplierChatMaster();
                                        ObjSupplierChatDetail.SupplierChatId = chatitem.SupplierChatId;
                                        ObjSupplierChatDetail.SupplierId = ObjSupplierMaster.SupplierId;
                                        ObjSupplierChatDetail.ChatId = chatitem.ChatId;
                                        ObjSupplierChatDetail.ChatValue = chatitem.ChatValue;
                                        ObjSupplierChatDetail.IsActive = true;
                                        context.SupplierChatMasters.Add(ObjSupplierChatDetail);
                                    }
                                    else if (chatitem.Status == 2 || chatitem.Status == 3)
                                    {
                                        SupplierChatMaster ObjSupplierChatDetail = context.SupplierChatMasters.Find(chatitem.SupplierChatId);
                                        ObjSupplierChatDetail.SupplierId = ObjSupplierMaster.SupplierId;
                                        ObjSupplierChatDetail.ChatId = chatitem.ChatId;
                                        ObjSupplierChatDetail.ChatValue = chatitem.ChatValue;
                                        ObjSupplierChatDetail.IsActive = true;
                                        if (chatitem.Status == 2)
                                        {
                                            ObjSupplierChatDetail.IsActive = true;
                                        }
                                        else if (chatitem.Status == 3)
                                        {
                                            ObjSupplierChatDetail.IsActive = false;
                                        }
                                        context.Entry(ObjSupplierChatDetail).State = System.Data.Entity.EntityState.Modified;
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

        public int Delete(SupplierModel objInputSupplier)
        {
            int resVal;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    SupplierMaster ObjSupplierMaster = context.SupplierMasters.Find(objInputSupplier.SupplierId);
                    ObjSupplierMaster.IsActive = false;
                    ObjSupplierMaster.DeletedBy = objInputSupplier.DeleteBy;
                    ObjSupplierMaster.DeletedDate = DateTime.Now;
                    UpdateSupplier(ObjSupplierMaster);

                    List<SupplierContactDetail> ObjSupplierContactDetail = context.SupplierContactDetails.Where(x => x.SupplierId == objInputSupplier.SupplierId).ToList();
                    if (ObjSupplierContactDetail.Count > 0)
                    {
                        foreach (var item in ObjSupplierContactDetail)
                        {
                            SupplierContactDetail ObjSupplierContact = context.SupplierContactDetails.Find(item.ContactId);
                            item.IsActive = false;
                            context.Entry(ObjSupplierContact).State = System.Data.Entity.EntityState.Modified;
                        }
                    }

                    List<SupplierBankMaster> ObjSupplierBankMaster = context.SupplierBankMasters.Where(x => x.SupplierId == objInputSupplier.SupplierId).ToList();
                    if (ObjSupplierBankMaster.Count > 0)
                    {
                        foreach (var item in ObjSupplierBankMaster)
                        {
                            SupplierBankMaster ObjBankDetail = context.SupplierBankMasters.Find(item.BankDetailId);
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
