using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
    public interface IBuyerChatDetail_Repository : IDisposable
    {
        void AddBuyerChatDetail(BuyerChatDetail obj);
        void UpdateBuyerChatDetail(BuyerChatDetail obj);
        void DeleteBuyerChatDetail(int id);
        BuyerChatDetail GetById(int id);
        IQueryable<BuyerChatDetail> GetAllBuyerChatDetail();
        IQueryable<BuyerChatDetail> GetByBuyerId(int id);
    }
}
