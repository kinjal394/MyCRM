using CRM_Repository.Data;
using CRM_Repository.DataServices;
using CRM_Repository.DTOModel;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data.SqlClient;
using System.IO;

namespace CRM_Repository.Service
{
    public class Product_Repository : IProduct_Repository, IDisposable
    {
        dalc odal = new dalc();
        private CRM_Repository.Data.elaunch_crmEntities context;
        private IProductCatalogMaster_Repository _IProductCatalogMaster_Repository;
        public Product_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
            this._IProductCatalogMaster_Repository = new ProductCatalogMaster_Repository(_context);
        }

        public Product_Repository()
        {
            context = new CRM_Repository.Data.elaunch_crmEntities();
        }
        public void saveProduct(ProductMaster objProductMaster)
        {
            try
            {
                context.ProductMasters.Add(objProductMaster);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateProduct(ProductMaster objProductMaster)
        {
            try
            {
                context.Entry(objProductMaster).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteProduct(int ProdictId, int userId)
        {
            try
            {
                ProductMaster objProductMaster = context.ProductMasters.Where(z => z.ProductId == ProdictId).SingleOrDefault();
                objProductMaster.IsActive = false;
                objProductMaster.DeletedBy = userId;
                objProductMaster.DeletedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                context.Entry(objProductMaster).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateProductDetail(ProductMaster objProductMaster, int UserId)
        {
            int resVal;
            try
            {
                ProductMaster PM = context.ProductMasters.Single(X => X.ProductId == objProductMaster.ProductId);
                PM.SubCategoryId = objProductMaster.SubCategoryId;
                PM.HSCode = objProductMaster.HSCode;
                PM.ProductName = objProductMaster.ProductName;
                PM.ProductCode = objProductMaster.ProductCode;
                PM.Functionality = objProductMaster.Functionality;
                //PM.Price = objProductMaster.Price;
                //PM.OursModelNo = objProductMaster.OursModelNo;
                //PM.ModelNo = objProductMaster.ModelNo;
                //PM.GrossWeight = objProductMaster.GrossWeight;
                //PM.NetWeight = objProductMaster.NetWeight;
                PM.Keywords = objProductMaster.Keywords;
                PM.ModifyBy = UserId;
                PM.ModifyDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                context.Entry(PM).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                var PPRM = objProductMaster.ProductParameterMasters.ToList();
                var PPM = context.ProductParameterMasters.Where(X => X.ProductId == PM.ProductId);
                var PM1 = PPM.ToList();
                if (PM1.Count > 0)
                {
                    foreach (var data in PM1)
                    {
                        ProductParameterMaster comobj = context.ProductParameterMasters.Find(data.TechDetailId);
                        context.ProductParameterMasters.Remove(comobj);
                        context.SaveChanges();
                    }
                    foreach (var data1 in PPRM)
                    {
                        ProductParameterMaster PM3 = new ProductParameterMaster();
                        PM3.ProductId = objProductMaster.ProductId;
                        PM3.Value = data1.Value;
                        PM3.TechParaId = data1.TechParaId;
                        context.ProductParameterMasters.Add(PM3);
                        context.SaveChanges();
                    }
                }
                else
                {
                    var PPRM1 = objProductMaster.ProductParameterMasters.ToList();
                    if (PPRM1.Count > 0)
                    {
                        foreach (var data1 in PPRM1)
                        {
                            ProductParameterMaster PM3 = new ProductParameterMaster();
                            PM3.ProductId = objProductMaster.ProductId;
                            PM3.Value = data1.Value;
                            PM3.TechParaId = data1.TechParaId;
                            context.ProductParameterMasters.Add(PM3);
                            context.SaveChanges();
                        }
                    }
                }
                resVal = 1;
            }
            catch (Exception ex)
            {
                resVal = 0;
                throw ex;
            }
            return resVal;
        }

        public ProductMaster GetProductById(int Productid)
        {
            try
            {
                //return context.ProductMasters.Find(Productid);
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@Productid", Productid);
                return new dalc().GetDataTable_Text("SELECT * FROM ProductMaster with(nolock) WHERE Productid=@Productid AND IsActive = 1", para).ConvertToList<ProductMaster>().FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<ProductPrice> GetProductPriceById(int ProductId,int catelogId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[2];
                para[0] = new SqlParameter().CreateParameter("@ProductId", ProductId);
                para[1] = new SqlParameter().CreateParameter("@supplierId", catelogId);
                return new dalc().GetDataTable_Text("SELECT PM.*,CM.CurrencyName FROM ProductPrices As PM inner join CurrencyMaster As CM on CM.CurrencyId=PM.CurrencyId WHERE ProductId=@ProductId And supplierId=@supplierId", para).ConvertToList<ProductPrice>().AsQueryable();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<ProductApplicableCharge> GetProductApplicableChargeById(int ProductId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ProductId", ProductId);
                return new dalc().GetDataTable_Text("SELECT PM.SupplierId,PM.ProductId,PA.ApplicableId,PA.ProductPriceId,PA.AppChargeId,PA.AppCharge,PA.Percentage,PA.Amount,PA.ApplicableAmount FROM ProductApplicableCharges As PA left join ProductPrices as PM on pm.ProductPriceId=pa.ProductPriceId where PM.productId=@productId", para).ConvertToList<ProductApplicableCharge>().AsQueryable();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<ProductMaster> GetAllProducts()
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var product = context.ProductMasters;
                //    scope.Complete();
                //    return product.AsQueryable();
                //}

                return new dalc().selectbyquerydt("SELECT * FROM ProductMaster with(nolock)").ConvertToList<ProductMaster>().AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<ProductMaster> GetProductBySubCategoryId(int SubCategoryId)
        {
            try
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                //{
                //    var product = context.ProductMasters.Where(z => z.MainProductId == id && z.IsActive == true);
                //    scope.Complete();
                //    return product.AsQueryable();
                //}

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@SubCategoryId", SubCategoryId);
                return new dalc().GetDataTable_Text("SELECT * FROM ProductMaster with(nolock) WHERE SubCategoryId=@SubCategoryId AND IsActive = 1", para).ConvertToList<ProductMaster>().AsQueryable();

            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool IsExist(int ProductId, string ProductName, int SubCategoryId)
        {
            try
            {

                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter().CreateParameter("@SubCategoryId", SubCategoryId);
                para[1] = new SqlParameter().CreateParameter("@ProductName", ProductName);
                para[2] = new SqlParameter().CreateParameter("@ProductId", ProductId);
                //return new dalc().GetDataTable_Text(" SELECT * FROM ProductMaster WHERE ProductId <> @ProductId AND RTRIM(LTRIM(ProductName)) = RTRIM(LTRIM(@ProductName)) AND SubCategoryId=@SubCategoryId AND IsActive = 1", para).Rows.Count > 0 ? true : false;
                return new dalc().GetDataTable_Text(" SELECT * FROM ProductMaster WHERE ProductId <> @ProductId AND RTRIM(LTRIM(ProductName)) = RTRIM(LTRIM(@ProductName)) AND IsActive = 1", para).Rows.Count > 0 ? true : false;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable ProductData(int ProductId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ProductId", ProductId);

                DataTable ds = odal.GetDataTable_Text(@"select * from productmaster P with(nolock) 
                INNER JOIN SubCategoryMaster S with(nolock)ON S.SubCategoryId = P.SubCategoryId 
                INNER JOIN CategoryMaster C with(nolock)ON C.CategoryId = S.CategoryId 
                Left JOIN ProductParameterMaster PPM with(nolock)ON PPM.ProductId = P.ProductId 
                where P.ProductId=@ProductId ", para);
                return ds;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ProductFormModel FetchById(int ProductId)
        {
            try
            {
                //    return odal.selectbyquerydt(@"select PM.ProductId,PM.ProductName,PM.MainProductId,PM.ProductCode,PM.FbLink,PM.GPlusLink,MPM.MainProductName,SM.SubCategoryId,SM.SubCategoryName,CM.CategoryId,CM.CategoryName,PM.HSCode,PM.Price,PM.ModelNo,PM.Height,PM.CBM,PM.Width,PM.Length,PM.GrossWeight,PM.NetWeight,PM.Description,PM.Keywords,PM.IsActive
                //                                  from ProductMaster PM 
                //                                  inner join MainProductMaster MPM on MPM.MainProductId = PM.MainProductId
                //                                  inner join SubCategoryMaster SM on SM.SubCategoryId = MPM.SubCategoryId
                //                                  inner join CategoryMaster CM on CM.CategoryId = SM.CategoryId
                //                                  where PM.ProductId = " + id + " AND ISNULL(PM.IsActive,0)=1").ConvertToList<ProductFormModel>().AsQueryable().FirstOrDefault();



                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@ProductId", ProductId);
                return odal.GetDataTable_Text(@"select PM.ProductId,PM.ProductName,PM.ProductCode,PM.FbLink,PM.GPlusLink,SM.SubCategoryId,SM.SubCategoryName,CM.CategoryId,CM.CategoryName,PM.HSCode,PM.Functionality,PM.Description,PM.Keywords,PM.IsActive
                                              from ProductMaster PM 
                                              inner join SubCategoryMaster SM on SM.SubCategoryId = PM.SubCategoryId
                                              inner join CategoryMaster CM on CM.CategoryId = SM.CategoryId
                                              where PM.ProductId =@ProductId  AND ISNULL(PM.IsActive,0)=1", para).ConvertToList<ProductFormModel>().AsQueryable().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateProductFrom(ProductFormModel objProductFormModel)
        {
            int resVal;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    #region "1st stage table // Product Master" 
                    ProductMaster objProductMaster = context.ProductMasters.Find(objProductFormModel.ProductId);
                    objProductMaster.ProductId = (objProductFormModel.ProductId != 0) ? objProductFormModel.ProductId : 0;
                    objProductMaster.ProductName = (objProductFormModel.ProductName != null || objProductFormModel.ProductName != "") ? objProductFormModel.ProductName : "NULL";
                    objProductMaster.SubCategoryId = (objProductFormModel.SubCategoryId != 0) ? objProductFormModel.SubCategoryId : 0;
                    objProductMaster.HSCode = (objProductFormModel.HSCode != null || objProductFormModel.HSCode != "") ? objProductFormModel.HSCode : "0";
                    objProductMaster.ProductCode = (objProductFormModel.ProductCode != null || objProductFormModel.ProductCode != "") ? objProductFormModel.ProductCode : "NULL";
                    objProductMaster.Functionality = (objProductFormModel.Functionality != null || objProductFormModel.Functionality != "") ? objProductFormModel.Functionality : "NULL";
                    //objProductMaster.Price = (objProductFormModel.Price != null) ? objProductFormModel.Price : 0;
                    //objProductMaster.OursModelNo = (objProductFormModel.OursModelNo != null || objProductFormModel.OursModelNo != "") ? objProductFormModel.OursModelNo : "NULL";
                    //objProductMaster.ModelNo = (objProductFormModel.ModelNo != null || objProductFormModel.ModelNo != "") ? objProductFormModel.ModelNo : "NULL";
                    //objProductMaster.Height = (objProductFormModel.Height != null || objProductFormModel.Height != "") ? objProductFormModel.Height : "NULL";
                    //objProductMaster.CBM = (objProductFormModel.CBM != null || objProductFormModel.CBM != "") ? objProductFormModel.CBM : "NULL";
                    //objProductMaster.Dimension = (objProductFormModel.Dimension != null || objProductFormModel.Dimension != "") ? objProductFormModel.Dimension : "NULL";
                    //objProductMaster.Width = (objProductFormModel.Width != null || objProductFormModel.Width != "") ? objProductFormModel.Width : "NULL";
                    //objProductMaster.Length = (objProductFormModel.Length != null || objProductFormModel.Length != "") ? objProductFormModel.Length : "NULL";
                    //objProductMaster.GrossWeight = (objProductFormModel.GrossWeight != null || objProductFormModel.GrossWeight != "") ? objProductFormModel.GrossWeight : "NULL";
                    //objProductMaster.NetWeight = (objProductFormModel.NetWeight != null || objProductFormModel.NetWeight != "") ? objProductFormModel.NetWeight : "NULL";
                    //objProductMaster.Description = (objProductFormModel.Description != null || objProductFormModel.Description != "") ? objProductFormModel.Description : "NULL";
                    objProductMaster.Keywords = (objProductFormModel.Keywords != null || objProductFormModel.Keywords != "") ? objProductFormModel.Keywords : "NULL";
                    objProductMaster.FbLink = (objProductFormModel.FbLink != null || objProductFormModel.FbLink != "") ? objProductFormModel.FbLink : "NULL";
                    objProductMaster.GPlusLink = (objProductFormModel.GPlusLink != null || objProductFormModel.GPlusLink != "") ? objProductFormModel.GPlusLink : "NULL";
                    objProductMaster.ModifyBy = objProductFormModel.ModifyBy;
                    objProductMaster.ModifyDate = DateTime.Now;
                    objProductMaster.OurCatalogImg = objProductFormModel.OurCatalogImg;
                    objProductMaster.SupplierCatalogimg = objProductFormModel.SupplierCatalogimg;
                    context.Entry(objProductMaster).State = System.Data.Entity.EntityState.Modified;

                    #region "2nd stage table // Product Catelog Master"
                    ProductCatalogMaster objProductCatalogMaster = new ProductCatalogMaster();
                    if (objProductFormModel.ProductCatalogMasters != null)
                    {
                        //context.ProductCatalogMasters.RemoveRange(context.ProductCatalogMasters.Where(x => x.ProductId == objProductFormModel.ProductId));
                        foreach (var item in objProductFormModel.ProductCatalogMasters)
                        {
                            if (item.Status == 1)
                            {
                                objProductCatalogMaster.ProductId = objProductFormModel.ProductId;
                                objProductCatalogMaster.SupplierId = item.SupplierId;
                                objProductCatalogMaster.ProductModelNo = item.ProductModelNo;
                                objProductCatalogMaster.SupplierModelNo = item.SupplierModelNo;
                                objProductCatalogMaster.CatalogPath = item.CatalogPath;
                                objProductCatalogMaster.CountryOfOriginId = item.CountryOfOriginId;
                                objProductCatalogMaster.Capacity = item.Capacity;
                                objProductCatalogMaster.IsActive = true;
                                objProductCatalogMaster = _IProductCatalogMaster_Repository.SaveProductCatalog(objProductCatalogMaster);
                                //context.ProductCatalogMasters.Add(objProductCatalogMaster);

                            }
                            else if (item.Status == 2 || item.Status == 3)
                            {
                                objProductCatalogMaster = context.ProductCatalogMasters.Find(item.CatalogId);
                                objProductCatalogMaster.SupplierId = item.SupplierId;
                                objProductCatalogMaster.ProductModelNo = item.ProductModelNo;
                                objProductCatalogMaster.SupplierModelNo = item.SupplierModelNo;
                                objProductCatalogMaster.CatalogPath = item.CatalogPath;
                                objProductCatalogMaster.CountryOfOriginId = item.CountryOfOriginId;
                                objProductCatalogMaster.Capacity = item.Capacity;
                                if (item.Status == 2)
                                {
                                    objProductCatalogMaster.IsActive = true;
                                }
                                if (item.Status == 3)
                                {
                                    //context.ProductCatalogMasters.Remove(objProductCatalogMaster);
                                    objProductCatalogMaster.IsActive = false;
                                }
                                context.Entry(objProductCatalogMaster).State = System.Data.Entity.EntityState.Modified;
                            }

                            #region "3rd stage table // Product Photo Master, Product Parameter Master, Product Link Master, Product Video Master, //  Product Packing Details"
                            // Product Photo Master
                            if (objProductFormModel.ProductPhotoMasters != null)
                            {
                                //context.ProductPhotoMasters.RemoveRange(context.ProductPhotoMasters.Where(x => x.ProductId == objProductFormModel.ProductId));
                                foreach (var Photoitem in objProductFormModel.ProductPhotoMasters)
                                {
                                    if (objProductCatalogMaster.CatalogId == Photoitem.CatalogId)
                                    {
                                        ProductPhotoMaster objProductPhotoMaster = new ProductPhotoMaster();
                                        if (Photoitem.Status == 1)
                                        {
                                            objProductPhotoMaster.ProductId = objProductFormModel.ProductId;
                                            objProductPhotoMaster.CatalogId = objProductCatalogMaster.CatalogId;
                                            objProductPhotoMaster.Photo = Photoitem.Photo;
                                            objProductPhotoMaster.IsDefault = Photoitem.IsDefault;
                                            objProductPhotoMaster.IsActive = true;
                                            objProductPhotoMaster.CreatedBy = objProductFormModel.CreatedBy;
                                            objProductPhotoMaster.CreatedDate = DateTime.Now;
                                            context.ProductPhotoMasters.Add(objProductPhotoMaster);
                                        }
                                        else if (Photoitem.Status == 2 || Photoitem.Status == 3)
                                        {
                                            objProductPhotoMaster = context.ProductPhotoMasters.Find(Photoitem.PhotoId);
                                            objProductPhotoMaster.IsDefault = Photoitem.IsDefault;
                                            //var cnt = objProductFormModel.ProductPhotoMasters.Where(x => x.IsDefault == true).Count();
                                            objProductPhotoMaster.Photo = Photoitem.Photo;
                                            //if (objProductFormModel.Isphotonext == true)
                                            //{
                                            //    if (objProductPhotoMaster.IsDefault == true)
                                            //    {
                                            //        if (cnt > 1)
                                            //        {
                                            //            objProductPhotoMaster.IsDefault = false;
                                            //        }
                                            //        else
                                            //        {
                                            //            objProductPhotoMaster.IsDefault = Photoitem.IsDefault;

                                            //        }
                                            //    }
                                            //    else
                                            //    {
                                            //        objProductPhotoMaster.IsDefault = Photoitem.IsDefault;
                                            //    }
                                            //}
                                            if (Photoitem.Status == 2)
                                            {
                                                objProductPhotoMaster.ModifyBy = objProductFormModel.ModifyBy;
                                                objProductPhotoMaster.ModifyDate = DateTime.Now;
                                            }
                                            if (Photoitem.Status == 3)
                                            {
                                                objProductPhotoMaster.IsActive = false;
                                                objProductPhotoMaster.DeletedBy = objProductFormModel.DeletedBy;
                                                objProductPhotoMaster.DeletedDate = DateTime.Now;
                                            }
                                            context.Entry(objProductPhotoMaster).State = System.Data.Entity.EntityState.Modified;
                                        }
                                    }
                                }
                            }
                            //Product Supp Document Detail
                            if (objProductFormModel.ProductSuppDocumentDetail != null)
                            {
                                foreach (var paraitem in objProductFormModel.ProductSuppDocumentDetail)
                                {
                                    if (objProductCatalogMaster.CatalogId == paraitem.CatalogId)
                                    {
                                        context.ProductSuppDocumentDetails.RemoveRange(context.ProductSuppDocumentDetails.Where(x => x.ProductId == objProductFormModel.ProductId && x.CatalogId == objProductCatalogMaster.CatalogId));
                                        ProductSuppDocumentDetail objsuppDoc = new ProductSuppDocumentDetail();
                                        if (paraitem.Status != 3)
                                        {
                                            objsuppDoc.ProductId = objProductFormModel.ProductId;
                                            objsuppDoc.CatalogId = objProductCatalogMaster.CatalogId;
                                            objsuppDoc.PrdSupDocId = paraitem.PrdSupDocId;
                                            objsuppDoc.PrdDocId = paraitem.PrdDocId;
                                            objsuppDoc.DocPath = paraitem.DocPath;
                                            objsuppDoc.Remark = paraitem.Remark;
                                            objsuppDoc.Date = paraitem.Date;
                                            objsuppDoc.IsActive = true;
                                            context.ProductSuppDocumentDetails.Add(objsuppDoc);
                                        }
                                        else if (paraitem.Status == 3)
                                        {
                                            objsuppDoc.IsActive = false;
                                        }
                                    }
                                }
                            }
                            // Product Parameter Master
                            if (objProductFormModel.ProductParameterMasters != null)
                            {
                                foreach (var Parameteritem in objProductFormModel.ProductParameterMasters)
                                {
                                    if (objProductCatalogMaster.CatalogId == Parameteritem.CatalogId)
                                    {
                                        context.ProductParameterMasters.RemoveRange(context.ProductParameterMasters.Where(x => x.ProductId == objProductFormModel.ProductId && x.CatalogId == objProductCatalogMaster.CatalogId));
                                        if (Parameteritem.Value != null)
                                        {
                                            if (Parameteritem.Status != 3)
                                            {
                                                ProductParameterMaster objProductParameterMaster = new ProductParameterMaster();
                                                objProductParameterMaster.ProductId = objProductFormModel.ProductId;
                                                objProductParameterMaster.CatalogId = objProductCatalogMaster.CatalogId;
                                                objProductParameterMaster.TechParaId = Parameteritem.TechParaId;
                                                objProductParameterMaster.Value = Parameteritem.Value;
                                                context.ProductParameterMasters.Add(objProductParameterMaster);
                                            }
                                        }
                                    }
                                }
                            }
                            // Product Link Master
                            if (objProductFormModel.ProductSocialMasters != null)
                            {
                                foreach (var Socialitem in objProductFormModel.ProductSocialMasters)
                                {
                                    if (objProductCatalogMaster.CatalogId == Socialitem.CatalogId)
                                    {
                                        context.ProductLinkMasters.RemoveRange(context.ProductLinkMasters.Where(x => x.ProductId == objProductFormModel.ProductId && x.CatalogId == objProductCatalogMaster.CatalogId)); ProductLinkMaster objProductLinkMaster = new ProductLinkMaster();
                                        if (Socialitem.Status != 3)
                                        {
                                            objProductLinkMaster.ProductId = objProductFormModel.ProductId;
                                            objProductLinkMaster.CatalogId = objProductCatalogMaster.CatalogId;
                                            objProductLinkMaster.AdSourceId = Socialitem.AdSourceId;
                                            objProductLinkMaster.Url = Socialitem.Url;
                                            objProductLinkMaster.IsActive = true;
                                            objProductLinkMaster.CreatedBy = objProductFormModel.CreatedBy;
                                            objProductLinkMaster.CreatedDate = DateTime.Now;
                                            context.ProductLinkMasters.Add(objProductLinkMaster);
                                        }
                                    }
                                }
                            }
                            // Product Video Master
                            if (objProductFormModel.ProductVideoMasters != null)
                            {
                                foreach (var Videoitem in objProductFormModel.ProductVideoMasters)
                                {
                                    if (objProductCatalogMaster.CatalogId == Videoitem.CatalogId)
                                    {
                                        context.ProductVideoMasters.RemoveRange(context.ProductVideoMasters.Where(x => x.ProductId == objProductFormModel.ProductId && x.CatalogId == objProductCatalogMaster.CatalogId));
                                        ProductVideoMaster objProductVideoMaster = new ProductVideoMaster();
                                        if (Videoitem.Status != 3)
                                        {
                                            objProductVideoMaster.ProductId = objProductFormModel.ProductId;
                                            objProductVideoMaster.CatalogId = objProductCatalogMaster.CatalogId;
                                            objProductVideoMaster.VideoId = Videoitem.VideoId;
                                            objProductVideoMaster.URL = Videoitem.URL;
                                            objProductVideoMaster.IsActive = true;
                                            objProductVideoMaster.IsDefault = Videoitem.IsDefault;
                                            context.ProductVideoMasters.Add(objProductVideoMaster);
                                        }
                                    }
                                }
                            }
                            //  Product Packing Details
                            if (objProductFormModel.ProductPackingDetails != null)
                            {
                                foreach (var Packingitem in objProductFormModel.ProductPackingDetails)
                                {
                                    if (objProductCatalogMaster.CatalogId == Packingitem.CatalogId)
                                    {
                                        context.ProductPackingDetails.RemoveRange(context.ProductPackingDetails.Where(x => x.ProductId == objProductFormModel.ProductId && x.CatalogId == objProductCatalogMaster.CatalogId));
                                        ProductPackingDetail objProductPackingDetail = new ProductPackingDetail();
                                        if (Packingitem.Status != 3)
                                        {
                                            objProductPackingDetail.ProductId = objProductFormModel.ProductId;
                                            objProductPackingDetail.CatalogId = objProductCatalogMaster.CatalogId;
                                            objProductPackingDetail.Description = Packingitem.Description;
                                            objProductPackingDetail.NetWeight = Packingitem.NetWeight;
                                            objProductPackingDetail.GrossWeight = Packingitem.GrossWeight;
                                            objProductPackingDetail.Length = Packingitem.Length;
                                            objProductPackingDetail.Width = Packingitem.Width;
                                            objProductPackingDetail.Height = Packingitem.Height;
                                            objProductPackingDetail.CBM = Packingitem.CBM;
                                            objProductPackingDetail.Dimension = Packingitem.Dimension;
                                            objProductPackingDetail.DealerPrice = Packingitem.DealerPrice;
                                            //objProductPackingDetail.AppliChargeDetail = Packingitem.AppliChargeDetail;
                                            //objProductPackingDetail.CurrencyDetail = Packingitem.CurrencyDetail;
                                            objProductPackingDetail.CurrencyId = Packingitem.CurrencyId;
                                            //objProductPackingDetail.TaxId = Packingitem.TaxId;
                                            objProductPackingDetail.PackingTypeId = Packingitem.PackingTypeId;
                                            objProductPackingDetail.PhaseId = Packingitem.PhaseId;
                                            objProductPackingDetail.PlugShapeId = Packingitem.PlugShapeId;
                                            objProductPackingDetail.FrequencyId = Packingitem.FrequencyId;
                                            objProductPackingDetail.VoltageId = Packingitem.VoltageId;
                                            objProductPackingDetail.Power = Packingitem.Power;
                                            objProductPackingDetail.IsActive = true;
                                            context.ProductPackingDetails.Add(objProductPackingDetail);
                                        }
                                    }
                                }
                            }

                            if (objProductFormModel.ProductpriceDetail != null)
                            {
                                foreach (var productpriceobj in objProductFormModel.ProductpriceDetail)
                                {
                                    ProductPrice obj = new ProductPrice();
                                    if (objProductCatalogMaster.CatalogId == productpriceobj.SupplierId)
                                    {
                                        if (productpriceobj.Status == 1)
                                        {
                                            obj.ProductId = productpriceobj.ProductId;
                                            obj.ProductPriceId = productpriceobj.ProductPriceId;
                                            obj.CurrencyId = productpriceobj.CurrencyId;
                                            obj.BaseAmount = productpriceobj.BaseAmount;
                                            obj.TotalCharge = productpriceobj.TotalCharge;
                                            obj.SupplierId = productpriceobj.SupplierId;
                                            obj.TotalAmount = productpriceobj.TotalAmount;
                                            obj = _IProductCatalogMaster_Repository.SaveProductPrice(obj);
                                            //context.ProductPrices.Add(obj);

                                            if (objProductFormModel.ProductApplicableChargeDetail != null)
                                            {
                                                foreach (var ProductApplicableChargeobj in objProductFormModel.ProductApplicableChargeDetail)
                                                {
                                                    if (productpriceobj.ProductPriceId == ProductApplicableChargeobj.ProductPriceId)
                                                    {
                                                        ProductApplicableCharge obj1 = new ProductApplicableCharge();
                                                        if (ProductApplicableChargeobj.Status == 1)
                                                        {
                                                            obj1.ApplicableId = ProductApplicableChargeobj.ApplicableId;
                                                            obj1.ProductPriceId = obj.ProductPriceId;
                                                            obj1.AppCharge = ProductApplicableChargeobj.AppCharge;
                                                            obj1.AppChargeId = ProductApplicableChargeobj.AppChargeId;
                                                            obj1.Percentage = ProductApplicableChargeobj.Percentage;
                                                            obj1.Amount = ProductApplicableChargeobj.Amount;
                                                            obj1.ApplicableAmount = ProductApplicableChargeobj.ApplicableAmount;
                                                            context.ProductApplicableCharges.Add(obj1);
                                                        }
                                                    }
                                                }
                                            }

                                        }
                                        else if (productpriceobj.Status == 2)
                                        {
                                            obj = context.ProductPrices.Find(productpriceobj.ProductPriceId);
                                            obj.ProductId = productpriceobj.ProductId;
                                            obj.CurrencyId = productpriceobj.CurrencyId;
                                            obj.BaseAmount = productpriceobj.BaseAmount;
                                            obj.TotalCharge = productpriceobj.TotalCharge;
                                            obj.SupplierId = productpriceobj.SupplierId;
                                            obj.TotalAmount = productpriceobj.TotalAmount;
                                            context.Entry(obj).State = System.Data.Entity.EntityState.Modified;

                                            if (objProductFormModel.ProductApplicableChargeDetail != null)
                                            {
                                                foreach (var ProductApplicableChargeobj in objProductFormModel.ProductApplicableChargeDetail)
                                                {
                                                    if (productpriceobj.ProductPriceId == ProductApplicableChargeobj.ProductPriceId)
                                                    {
                                                        ProductApplicableCharge obj1 = new ProductApplicableCharge();
                                                        if (ProductApplicableChargeobj.Status == 1)
                                                        {
                                                            obj1.ApplicableId = ProductApplicableChargeobj.ApplicableId;
                                                            obj1.ProductPriceId = obj.ProductPriceId;
                                                            obj1.AppCharge = ProductApplicableChargeobj.AppCharge;
                                                            obj1.AppChargeId = ProductApplicableChargeobj.AppChargeId;
                                                            obj1.Percentage = ProductApplicableChargeobj.Percentage;
                                                            obj1.Amount = ProductApplicableChargeobj.Amount;
                                                            obj1.ApplicableAmount = ProductApplicableChargeobj.ApplicableAmount;
                                                            context.ProductApplicableCharges.Add(obj1);
                                                        }
                                                        else if (ProductApplicableChargeobj.Status == 2)
                                                        {
                                                            obj1 = context.ProductApplicableCharges.Find(ProductApplicableChargeobj.ApplicableId);
                                                            obj1.ApplicableId = ProductApplicableChargeobj.ApplicableId;
                                                            obj1.AppCharge = ProductApplicableChargeobj.AppCharge;
                                                            obj1.AppChargeId = ProductApplicableChargeobj.AppChargeId;
                                                            obj1.Percentage = ProductApplicableChargeobj.Percentage;
                                                            obj1.Amount = ProductApplicableChargeobj.Amount;
                                                            obj1.ApplicableAmount = ProductApplicableChargeobj.ApplicableAmount;
                                                            context.Entry(obj1).State = System.Data.Entity.EntityState.Modified;
                                                        }
                                                        else if (ProductApplicableChargeobj.Status == 3)
                                                        {
                                                            obj1 = context.ProductApplicableCharges.Find(ProductApplicableChargeobj.ApplicableId);
                                                            context.ProductApplicableCharges.Remove(obj1);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else if (productpriceobj.Status == 3)
                                        {
                                            obj = context.ProductPrices.Find(productpriceobj.ProductPriceId);
                                            if (objProductFormModel.ProductApplicableChargeDetail != null)
                                            {
                                                foreach (var ProductApplicableChargeobj in objProductFormModel.ProductApplicableChargeDetail)
                                                {
                                                    if (obj.ProductPriceId == ProductApplicableChargeobj.ProductPriceId)
                                                    {
                                                        ProductApplicableCharge obj1 = new ProductApplicableCharge();
                                                        if (ProductApplicableChargeobj.Status == 3)
                                                        {
                                                            obj1 = context.ProductApplicableCharges.Find(ProductApplicableChargeobj.ApplicableId);
                                                            context.ProductApplicableCharges.Remove(obj1);
                                                        }
                                                    }
                                                }
                                            }
                                            context.ProductPrices.Remove(obj);
                                        }
                                    }

                                }
                            }

                            //if (objProductFormModel.ProductApplicableChargeDetail != null)
                            //{
                            //    foreach (var ProductApplicableChargeobj in objProductFormModel.ProductApplicableChargeDetail)
                            //    {
                            //        if (objProductCatalogMaster.CatalogId == ProductApplicableChargeobj.SupplierId)
                            //        {
                            //            context.ProductApplicableCharges.RemoveRange(context.ProductApplicableCharges.Where(x => x.ProductId == objProductFormModel.ProductId && x.SupplierId == objProductCatalogMaster.CatalogId));
                            //            ProductApplicableCharge obj = new ProductApplicableCharge();
                            //            if (ProductApplicableChargeobj.Status != 3)
                            //            {
                            //                obj.ApplicableId = ProductApplicableChargeobj.ApplicableId;
                            //                obj.ProductPriceId = ProductApplicableChargeobj.ProductPriceId;
                            //                obj.AppCharge = ProductApplicableChargeobj.AppCharge;
                            //                obj.AppChargeId = ProductApplicableChargeobj.AppChargeId;
                            //                obj.Percentage = ProductApplicableChargeobj.Percentage;
                            //                obj.Amount = ProductApplicableChargeobj.Amount;
                            //                obj.ApplicableAmount = ProductApplicableChargeobj.ApplicableAmount;
                            //                context.ProductApplicableCharges.Add(obj);
                            //            }
                            //            //else if (ProductApplicableChargeobj.Status == 2)
                            //            //{
                            //            //    obj = context.ProductApplicableCharges.Find(ProductApplicableChargeobj.ApplicableId);
                            //            //    obj.ApplicableId = ProductApplicableChargeobj.ApplicableId;
                            //            //    obj.ProductPriceId = ProductApplicableChargeobj.ProductPriceId;
                            //            //    obj.AppCharge = ProductApplicableChargeobj.AppCharge;
                            //            //    obj.AppChargeId = ProductApplicableChargeobj.AppChargeId;
                            //            //    obj.Percentage = ProductApplicableChargeobj.Percentage;
                            //            //    obj.Amount = ProductApplicableChargeobj.Amount;
                            //            //    obj.ApplicableAmount = ProductApplicableChargeobj.ApplicableAmount;
                            //            //    context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                            //            //}
                            //            //else if (ProductApplicableChargeobj.Status == 3)
                            //            //{
                            //            //    obj = context.ProductApplicableCharges.Find(ProductApplicableChargeobj.ApplicableId);
                            //            //    context.ApplicableChargeMasters.Remove();
                            //            //}
                            //        }
                            //    }
                            //}
                            #endregion
                        }
                    }
                    #endregion



                    resVal = 1;
                    context.SaveChanges();
                    dbContextTransaction.Commit();
                    #endregion
                }

                catch (Exception ex)
                {
                    string msg = "";
                    if (ex is System.Data.Entity.Validation.DbEntityValidationException)
                    {
                        foreach (var validationErrors in ((System.Data.Entity.Validation.DbEntityValidationException)ex).EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                msg += ";" + string.Format("{0}:{1}",
                                    validationErrors.Entry.Entity.ToString(),
                                    validationError.ErrorMessage);
                            }
                        }
                    }
                    dbContextTransaction.Rollback();
                    resVal = 0;
                }
            }
            return resVal;
        }
        public DataSet GetProductDetail(ReportPara obj)
        {
            dalc odal = new dalc();
            DataSet ds = new DataSet();
            int ID = 0;
            try
            {
                ID = Convert.ToInt32(obj.ID);
            }
            catch
            {
                ID = 0;
            }
            SqlParameter[] para = new SqlParameter[9];
            para[0] = new SqlParameter().CreateParameter("@ID", ID);
            para[1] = new SqlParameter().CreateParameter("@UserId", obj.UserId);
            para[2] = new SqlParameter().CreateParameter("@UserType", obj.UserType);
            para[3] = new SqlParameter().CreateParameter("@A", obj.A);
            para[4] = new SqlParameter().CreateParameter("@B", obj.B);
            para[5] = new SqlParameter().CreateParameter("@C", obj.C);
            para[6] = new SqlParameter().CreateParameter("@D", obj.D);
            para[7] = new SqlParameter().CreateParameter("@E", obj.E);
            para[8] = new SqlParameter().CreateParameter("@F", obj.F);
            ds = odal.GetDataset("ProductDetail_Report", para);
            ds.Tables[0].TableName = "ProductDetails";
            return ds;

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
