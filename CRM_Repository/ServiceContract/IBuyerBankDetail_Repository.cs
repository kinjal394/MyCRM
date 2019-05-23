using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface IBuyerBankDetail_Repository : IDisposable
    {
        void AddBuyerBankDetail(BuyerBankDetail obj);
        void UpdateBuyerBankDetail(BuyerBankDetail obj);
        void DeleteBuyerBankDetail(int id);
        BuyerBankDetail GetById(int id);
        IQueryable<BuyerBankDetail> GetAllBuyerBankDetail();
        IQueryable<BuyerBankDetail> GetByBuyerId(int id);
    }
}
