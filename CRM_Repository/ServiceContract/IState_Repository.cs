using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Repository.Data;
using System.Data;

namespace CRM_Repository.ServiceContract
{
    public interface IState_Repository : IDisposable
    {
        void InsertState(StateMaster objstate);
        void UpdateState(StateMaster objstate);
        IQueryable<StateMaster> GetAllState(); 
        IQueryable<StateMaster> CheckForDuplicateState(int StateID, string Statename);
        IQueryable<StateMaster> GetStateByCountryID(int CountryID);
        StateMaster GetStateByID(int StateID); 
    }
}
