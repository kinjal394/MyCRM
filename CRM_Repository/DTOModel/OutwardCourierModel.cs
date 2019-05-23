using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.DTOModel
{
    public class OutwardCourierModel
    {
        public int CourierId { get; set; }
        public DateTime CourierDate { get; set; }
        public TimeSpan CourierTime { get; set; }
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int VendorId { get; set; }
        public string Vendor { get; set; }
        public int ReceiverId { get; set; }
        public string Receiver { get; set; }
        public int SenderBy { get; set; }
        public string Sender { get; set; }
        public string ReceiverType { get; set; }
        public string ShipmentRefNo { get; set; }
        public decimal Amount { get; set; }
        public string PaymentBy { get; set; }
        public string Remark { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string POD { get; set; }
        public string ShipmentPhoto { get; set; }
        public int ReceiverAddressId { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverCity { get; set; }
        public string ReceiverCountry { get; set; }
        public string ReceiverState { get; set; }
        public string ReceiverCompanyName { get; set; }
        public string CourierReffNo { get; set; }
        public string CourierType { get; set; }
        public Nullable<int> CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public Nullable<int> CourierTypeId { get; set; }
    }
}
