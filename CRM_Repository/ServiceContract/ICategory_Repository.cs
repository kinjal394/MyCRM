using CRM_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.ServiceContract
{
   public interface ICategory_Repository : IDisposable
    {
        void SaveCategory(CategoryMaster Category);
        void UpdateCategory(CategoryMaster Category);
        void DeleteCategory(int UserId, int CategoryId);
        CategoryMaster GetCategoryById(int CategoryId);
        IQueryable<CategoryMaster> GetCategory();
        IQueryable<CategoryMaster> IsCategoryRepeat(CategoryMaster objCategory);
    }
}
