using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using CRM_Repository.Data;
using CRM_Repository.ServiceContract;
using CRM_Repository.DTOModel;
using CRM_Repository.DataServices;


using System.Data.SqlClient;

namespace CRM_Repository.Service
{
    public class UploadProductData_Repository : IUploadProductData_Repository, IDisposable
    {
        dalc odal = new dalc();
        private CRM_Repository.Data.elaunch_crmEntities context;
        public UploadProductData_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public ProductLinkMaster AddAdvLink(ProductLinkMaster obj)
        {
            try
            {
                context.ProductLinkMasters.Add(obj);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void UpdateAdvLink(ProductLinkMaster obj)
        {
            try
            {
                context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void DeleteAdvLink(int id)
        {
            try
            {
                ProductLinkMaster AdvLink = context.ProductLinkMasters.Find(id);
                if (AdvLink != null)
                {
                    context.ProductLinkMasters.Remove(AdvLink);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<UploadProductDataModel> FetchById(int ProductId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ProductId", ProductId);
                return odal.GetDataTable_Text(@"Select SM.AdId,SM.Url,SM.Remark,SM.CatalogId,SM.AdSourceId,AM.SiteName As AdSource,PM.GplusLink As GooglePlusUrl ,PM.FbLink As FbUrl,PM.HSCode,PM.Keywords,
                                                PM.ProductId,PM.ProductName,PM.ProductCode,PM.GDriveLink,PM.DropboxLink,
                                                SC.SubCategoryId,SC.SubCategoryName As SubCategory,CC.CategoryId,CC.CategoryName As Category
                                                from
                                                ProductMaster As PM With(NOLOCK)
                                                Left join ProductLinkMaster As SM With(NOLOCK) On PM.ProductId = SM.ProductId
                                                Left join AdvertisementSourceMaster As AM With(NOLOCK) On AM.SiteId = SM.AdSourceId
                                                Left join SubCategoryMaster As SC With(NOLOCK) On SC.SubCategoryId = PM.SubCategoryId
                                                Left join CategoryMaster As CC With(NOLOCK) On CC.CategoryId = SC.CategoryId
                                                Where PM.ProductId =@ProductId AND ISNULL(PM.IsActive,0)=1", para).ConvertToList<UploadProductDataModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public List<UploadProductDataModel> FetchByCode(string ProductCode)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ProductCode", ProductCode);
                return odal.GetDataTable_Text(@"Select PM.Description,SM.AdId,SM.Url,SM.Remark,SM.CatalogId,SM.AdSourceId,AM.SiteName As AdSource,PM.GplusLink As GooglePlusUrl ,PM.FbLink As FbUrl,PM.HSCode,PM.Keywords,
                                                PM.ProductId,PM.ProductName,PM.ProductCode,PM.GDriveLink,PM.DropboxLink,
                                                SC.SubCategoryId,SC.SubCategoryName As SubCategory,CC.CategoryId,CC.CategoryName As Category,PC.ProductModelNo
                                                from
                                                ProductMaster As PM With(NOLOCK)
                                                Left join ProductLinkMaster As SM With(NOLOCK) On PM.ProductId = SM.ProductId
                                                Left join AdvertisementSourceMaster As AM With(NOLOCK) On AM.SiteId = SM.AdSourceId
                                                Left join SubCategoryMaster As SC With(NOLOCK) On SC.SubCategoryId = PM.SubCategoryId
                                                Left join CategoryMaster As CC With(NOLOCK) On CC.CategoryId = SC.CategoryId
                                                Left join ProductCatalogMAster as PC With(NOLOCK) on PC.ProductId=PM.ProductId
                                                Where PM.ProductCode = @ProductCode AND ISNULL(PM.IsActive, 0) = 1", para).ConvertToList<UploadProductDataModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public ProductLinkMaster GetAdvLinkById(int id)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var AdvLink = context.ProductLinkMasters.Find(id);
                //    scope.Complete();
                //    return AdvLink;
                //}

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ADId", id);
                return new dalc().GetDataTable_Text("SELECT * FROM ProductLinkMaster with(nolock) WHERE ADId=@ADId ", para).ConvertToList<ProductLinkMaster>().FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IQueryable<ProductLinkMaster> GetAllAdvLink()
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var AdvLinks = context.ProductLinkMasters.Where(x => x.IsActive == true);
                //    scope.Complete();
                //    return AdvLinks.AsQueryable();
                //}
                return new dalc().selectbyquerydt("SELECT * FROM ProductLinkMaster with(nolock) WHERE IsActive = 1").ConvertToList<ProductLinkMaster>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public int CreateUpdate(UploadProductDataModel objInputAdvertisementLinkMaster)
        {
            int resVal;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    ProductMaster ObjProductMaster = context.ProductMasters.Find(objInputAdvertisementLinkMaster.ProductId);
                    ObjProductMaster.SubCategoryId = objInputAdvertisementLinkMaster.SubCategoryId;
                    ObjProductMaster.ProductName = objInputAdvertisementLinkMaster.ProductName;
                    ObjProductMaster.HSCode = objInputAdvertisementLinkMaster.HSCode;
                    ObjProductMaster.Keywords = objInputAdvertisementLinkMaster.Keywords;
                    ObjProductMaster.GPlusLink = objInputAdvertisementLinkMaster.GooglePlusUrl;
                    ObjProductMaster.FbLink = objInputAdvertisementLinkMaster.FbUrl;
                    ObjProductMaster.GDriveLink = objInputAdvertisementLinkMaster.GDriveLink;
                    ObjProductMaster.DropboxLink = objInputAdvertisementLinkMaster.DropboxLink;
                    ObjProductMaster.ModifyBy = objInputAdvertisementLinkMaster.ModifyBy;
                    ObjProductMaster.ModifyDate = DateTime.Now;
                    context.Entry(ObjProductMaster).State = System.Data.Entity.EntityState.Modified;

                    if (objInputAdvertisementLinkMaster.AdId <= 0)
                    {
                        //Insert
                        if (objInputAdvertisementLinkMaster.SourceUrlDetails != null)
                        {
                            foreach (var item in objInputAdvertisementLinkMaster.SourceUrlDetails)
                            {
                                //ADD
                                ProductLinkMaster ObjAdvertisementLinkMaster = new ProductLinkMaster();
                                ObjAdvertisementLinkMaster.AdSourceId = item.AdSourceId;
                                ObjAdvertisementLinkMaster.ProductId = item.ProductId;
                                ObjAdvertisementLinkMaster.CatalogId = item.CatalogId;
                                ObjAdvertisementLinkMaster.Url = item.Url;
                                ObjAdvertisementLinkMaster.Remark = item.Remark;
                                ObjAdvertisementLinkMaster.CreatedBy = objInputAdvertisementLinkMaster.CreatedBy;
                                ObjAdvertisementLinkMaster.CreatedDate = DateTime.Now;
                                ObjAdvertisementLinkMaster.IsActive = true;
                                ObjAdvertisementLinkMaster = AddAdvLink(ObjAdvertisementLinkMaster);
                            }
                        }
                        if (objInputAdvertisementLinkMaster.VideoUrlDetails != null)
                        {
                            foreach (var vitem in objInputAdvertisementLinkMaster.VideoUrlDetails)
                            {
                                ProductVideoMaster ObjvideoMaster = new ProductVideoMaster();
                                ObjvideoMaster.ProductId = vitem.ProductId;
                                ObjvideoMaster.CatalogId = vitem.CatalogId;
                                ObjvideoMaster.URL = vitem.URL;
                                ObjvideoMaster.IsDefault = vitem.IsDefault;
                                ObjvideoMaster.IsActive = true;
                                context.ProductVideoMasters.Add(ObjvideoMaster);
                            }
                        }
                        resVal = 1;
                    }
                    else
                    {

                        //EDIT
                        if (objInputAdvertisementLinkMaster.SourceUrlDetails != null)
                        {
                            foreach (var item in objInputAdvertisementLinkMaster.SourceUrlDetails)
                            {
                                //Edit
                                if (item.Status == 1)
                                {
                                    ProductLinkMaster ObjAdvertisementLinkMaster = new ProductLinkMaster();
                                    ObjAdvertisementLinkMaster.AdSourceId = item.AdSourceId;
                                    ObjAdvertisementLinkMaster.ProductId = item.ProductId;
                                    ObjAdvertisementLinkMaster.CatalogId = item.CatalogId;
                                    ObjAdvertisementLinkMaster.Url = item.Url;
                                    ObjAdvertisementLinkMaster.Remark = item.Remark;
                                    ObjAdvertisementLinkMaster.CreatedBy = objInputAdvertisementLinkMaster.CreatedBy;
                                    ObjAdvertisementLinkMaster.CreatedDate = DateTime.Now;
                                    ObjAdvertisementLinkMaster.IsActive = true;
                                    ObjAdvertisementLinkMaster = AddAdvLink(ObjAdvertisementLinkMaster);
                                }
                                else if (item.Status == 2 || item.Status == 3)
                                {
                                    ProductLinkMaster ObjAdvertisementLinkMaster = context.ProductLinkMasters.Find(item.AdId);
                                    ObjAdvertisementLinkMaster.AdSourceId = item.AdSourceId;
                                    ObjAdvertisementLinkMaster.ProductId = item.ProductId;
                                    ObjAdvertisementLinkMaster.Url = item.Url;
                                    ObjAdvertisementLinkMaster.Remark = item.Remark;
                                    if (item.Status == 2)
                                    {
                                        ObjAdvertisementLinkMaster.IsActive = true;
                                        ObjAdvertisementLinkMaster.ModifyBy = objInputAdvertisementLinkMaster.ModifyBy;
                                        ObjAdvertisementLinkMaster.ModifyDate = DateTime.Now;
                                    }
                                    if (item.Status == 3)
                                    {
                                        ObjAdvertisementLinkMaster.IsActive = false;
                                        ObjAdvertisementLinkMaster.DeletedBy = objInputAdvertisementLinkMaster.DeletedBy;
                                        ObjAdvertisementLinkMaster.DeletedDate = DateTime.Now;
                                    }
                                    UpdateAdvLink(ObjAdvertisementLinkMaster);
                                }
                            }
                        }

                        if (objInputAdvertisementLinkMaster.VideoUrlDetails != null)
                        {
                            foreach (var vitem in objInputAdvertisementLinkMaster.VideoUrlDetails)
                            {
                                //Edit
                                if (vitem.Status == 1)
                                {
                                    ProductVideoMaster ObjvideoMaster = new ProductVideoMaster();
                                    ObjvideoMaster.ProductId = vitem.ProductId;
                                    ObjvideoMaster.CatalogId = vitem.CatalogId;
                                    ObjvideoMaster.URL = vitem.URL;
                                    ObjvideoMaster.IsDefault = vitem.IsDefault;
                                    ObjvideoMaster.IsActive = true;
                                    context.ProductVideoMasters.Add(ObjvideoMaster);
                                }
                                else if (vitem.Status == 2 || vitem.Status == 3)
                                {
                                    ProductVideoMaster ObjvideoMaster = context.ProductVideoMasters.Find(vitem.VideoId);
                                    ObjvideoMaster.ProductId = vitem.ProductId;
                                    ObjvideoMaster.CatalogId = vitem.CatalogId;
                                    ObjvideoMaster.URL = vitem.URL;
                                    ObjvideoMaster.IsDefault = vitem.IsDefault;
                                    context.ProductVideoMasters.Add(ObjvideoMaster);
                                    if (vitem.Status == 2)
                                    {
                                        ObjvideoMaster.IsActive = true;
                                    }
                                    if (vitem.Status == 3)
                                    {
                                        ObjvideoMaster.IsActive = false;
                                    }
                                    context.Entry(ObjvideoMaster).State = System.Data.Entity.EntityState.Modified;
                                    context.SaveChanges();
                                }
                            }
                        }

                        resVal = 2;
                    }
                    context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    resVal = 0;
                    throw ex;
                }
            }
            return resVal;
        }

        public int Delete(UploadProductDataModel objInputAdvertisementLinkMaster)
        {
            int resVal;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    ProductLinkMaster objAdvertisementLinkMaster = context.ProductLinkMasters.Find(objInputAdvertisementLinkMaster.AdId);
                    objAdvertisementLinkMaster.IsActive = false;
                    objAdvertisementLinkMaster.DeletedBy = objInputAdvertisementLinkMaster.DeletedBy;
                    objAdvertisementLinkMaster.DeletedDate = DateTime.Now;
                    UpdateAdvLink(objAdvertisementLinkMaster);

                    resVal = 1;
                    context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    resVal = 0;
                }
            }
            return resVal;
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

        public ProductCatalogMaster AddAdvLink(ProductCatalogMaster obj)
        {
            throw new NotImplementedException();
        }

        public void UpdateAdvLink(ProductCatalogMaster obj)
        {
            throw new NotImplementedException();
        }

        ProductCatalogMaster IUploadProductData_Repository.GetAdvLinkById(int id)
        {
            throw new NotImplementedException();
        }

        IQueryable<ProductCatalogMaster> IUploadProductData_Repository.GetAllAdvLink()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
