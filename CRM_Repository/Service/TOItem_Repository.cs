using CRM_Repository.DataServices;
using CRM_Repository.DTOModel;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.Service
{
    public class TOItem_Repository : ITOItem_Repository, IDisposable
    {
        private CRM_Repository.Data.elaunch_crmEntities context;
        dalc odal = new dalc();
        public TOItem_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }
        public TOItemModel GetTOItemById(int TOItemId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@TOItemId", TOItemId);
                return odal.GetDataTable_Text(@"SELECT tom.TOId,toi.TOItemId,toi.SpecId,tos.TechSpec,toi.SpecValue,toi.ProductId,prod.ProductName
                                    ,sc.SubCategoryId,sc.SubCategoryName,cat.CategoryId,cat.CategoryName 
                                    FROM gurjari_crmuser.TOItemMaster  WITH(nolock) toi
                                    INNER JOIN gurjari_crmuser.TOMaster tom  WITH(nolock)  ON tom.TOId=toi.TOId
                                    INNER JOIN gurjari_crmuser.TechnicalSpecMaster tos  WITH(nolock) ON tos.SpecificationId=toi.SpecId
                                    INNER JOIN gurjari_crmuser.ProductMaster prod  WITH(nolock) ON prod.ProductId = toi.ProductId
                                    INNER JOIN gurjari_crmuser.SubCategoryMaster sc  WITH(nolock) ON sc.SubCategoryId = prod.SubCategoryId
                                    INNER JOIN gurjari_crmuser.CategoryMaster cat  WITH(nolock) ON cat.CategoryId = sc.CategoryId
                                    WHERE toi.TOItemId =@TOItemId", para).ConvertToList<TOItemModel>().AsQueryable().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public IQueryable<TOItemModel> GetAllTOItem()
        {
            try
            {
                return odal.selectbyquerydt(@"SELECT tom.TOId,toi.TOItemId,toi.SpecId,tos.TechSpec,toi.SpecValue,toi.ProductId,prod.ProductName
                                    ,sc.SubCategoryId,sc.SubCategoryName,cat.CategoryId,cat.CategoryName 
                                    FROM gurjari_crmuser.TOItemMaster  WITH(nolock) toi
                                    INNER JOIN gurjari_crmuser.TOMaster tom  WITH(nolock)  ON tom.TOId=toi.TOId
                                    INNER JOIN gurjari_crmuser.TechnicalSpecMaster tos  WITH(nolock) ON tos.SpecificationId=toi.SpecId
                                    INNER JOIN gurjari_crmuser.ProductMaster prod  WITH(nolock) ON prod.ProductId = toi.ProductId
                                    INNER JOIN gurjari_crmuser.SubCategoryMaster sc  WITH(nolock) ON sc.SubCategoryId = prod.SubCategoryId
                                    INNER JOIN gurjari_crmuser.CategoryMaster cat  WITH(nolock) ON cat.CategoryId = sc.CategoryId
                                    WHERE toi.TOItemId =@TOItemId").ConvertToList<TOItemModel>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }

        }
        public IQueryable<TOItemModel> GetTOItembyTOId(int TOId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@TOId", TOId);
                //return odal.GetDataTable_Text(@"SELECT tom.TOId,toi.TOItemId,toi.SpecId,tos.TechSpec,toi.SpecValue,toi.ProductId,prod.ProductName
                //                    ,sc.SubCategoryId,sc.SubCategoryName,cat.CategoryId,cat.CategoryName 
                //                    FROM gurjari_crmuser.TOItemMaster toi WITH(nolock)
                //                    INNER JOIN gurjari_crmuser.TOMaster tom  WITH(nolock)  ON tom.TOId=toi.TOId
                //                    INNER JOIN gurjari_crmuser.TechnicalSpecMaster tos  WITH(nolock) ON tos.SpecificationId=toi.SpecId
                //                    INNER JOIN gurjari_crmuser.ProductMaster prod  WITH(nolock) ON prod.ProductId = toi.ProductId
                //                    INNER JOIN gurjari_crmuser.SubCategoryMaster sc  WITH(nolock) ON sc.SubCategoryId = prod.SubCategoryId
                //                    INNER JOIN gurjari_crmuser.CategoryMaster cat  WITH(nolock) ON cat.CategoryId = sc.CategoryId
                //                    WHERE toi.TOId =@TOId", para).ConvertToList<TOItemModel>().AsQueryable();
                return odal.GetDataTable_Text(@"
SELECT tom.TOId,toi.TOItemId,toi.ProductId,prod.ProductName,sc.SubCategoryId,sc.SubCategoryName,cat.CategoryId,cat.CategoryName
                                    FROM gurjari_crmuser.TOItemMaster toi WITH(nolock)
                                    INNER JOIN gurjari_crmuser.TOMaster tom  WITH(nolock)  ON tom.TOId=toi.TOId
                                    INNER JOIN gurjari_crmuser.ProductMaster prod  WITH(nolock) ON prod.ProductId = toi.ProductId
                                    INNER JOIN gurjari_crmuser.SubCategoryMaster sc  WITH(nolock) ON sc.SubCategoryId = prod.SubCategoryId
                                    INNER JOIN gurjari_crmuser.CategoryMaster cat  WITH(nolock) ON cat.CategoryId = sc.CategoryId
                                    WHERE toi.TOId =@TOId", para).ConvertToList<TOItemModel>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }

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
