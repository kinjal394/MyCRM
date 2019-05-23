using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.DTOModel;

namespace CRM_Repository.ServiceContract
{
    public interface IBuyer_Repository : IDisposable
    {
        BuyerMaster AddBuyer(BuyerMaster obj);
        void UpdateBuyer(BuyerMaster obj);
        void DeleteBuyer(int id);
        BuyerModel FetchById(int id);
        BuyerMaster GetById(int id);
        IQueryable<BuyerModel> GetBuyerById(int id);
        BuyerMaster GetConsigneById(int id);
        BuyerAddressDetail GetConsigneAddressById(int id);
        BuyerContactDetail GetConsigneContactById(int id);
        IQueryable<BuyerMaster> GetAllBuyer();
        IQueryable<BuyerMaster> GetAllBuyerCompany(int UserId);
        IQueryable<BuyerMaster> GetBuyerEmail(int id);
        BuyerMaster CreateUpdate(BuyerModel objInputBuyer);
        int Delete(BuyerModel objInputBuyer);
        bool CheckBuyerDuplication(BuyerModel Obj, bool isUpdate);
    }
}
