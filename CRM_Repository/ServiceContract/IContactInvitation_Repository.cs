using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;

namespace CRM_Repository.ServiceContract
{
    public interface IContactInvitation_Repository: IDisposable
    {
        void AddConInv(ContactInvitationMaster obj);
        void UpdateConInv(ContactInvitationMaster obj);
        void DeleteConInv(int id);
        ContactInvitationMaster GetConInvByID(int id);
        IQueryable<ContactInvitationMaster> GetAllConInv();
        IQueryable<ContactInvitationMaster> DuplicateConInv(string ContactInvitation);
        IQueryable<ContactInvitationMaster> DuplicateEditConInv(int ContactInvitationId, string ContactInvitation);
    }
}
