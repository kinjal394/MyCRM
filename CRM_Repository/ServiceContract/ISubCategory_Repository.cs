using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
   public interface ISubCategory_Repository : IDisposable
    {
        void SaveSubCategory(SubCategoryMaster objSubCategoryMaster);
        void UpdateSubCategory(SubCategoryMaster objSubCategoryMaster);
        void DeleteSubCategory(int SubCategoryId, int UserId);
        IQueryable<SubCategoryMaster> GetSubCategory();
        SubCategoryMaster GetSubCategorybyId(int SubCategoryId);
        IQueryable<SubCategoryMaster> GetSubCategorybyCategory(int categoryId);
        bool IsExist(SubCategoryMaster objSubCategory,string mode);
    }
}
