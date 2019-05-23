using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.DTOModel;

namespace CRM_Repository.ServiceContract
{
    public interface IBuyerContactDetail_Repository : IDisposable
    {
        void AddBuyerContactDetail(BuyerContactDetail obj);
        void UpdateBuyerContactDetail(BuyerContactDetail obj);
        void DeleteBuyerContactDetail(int id);
        IQueryable<vmBuyerContactDetail> GetById(int id);
        IQueryable<BuyerContactDetail> GetAllBuyerContactDetail();
        IQueryable<BuyerAddressDetail> GetAddressByBuyerId(int id);
        IQueryable<BuyerContactDetail> GetContactDetailbybuyerId(int id);
        IQueryable<BuyerContactDetail> GetByBuyerId(int id);
        IQueryable<BuyerLicenseDetail> GetLicenseByBuyerId(int id);
        BuyerContactDetail GetBCbyid(int id);
        BuyerAddressDetail GetBAddbyid(int BAddId);
        IQueryable<BuyerContactDetail> GetContactDetailbyContactId(int id);
    }
}
