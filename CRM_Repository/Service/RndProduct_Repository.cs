using CRM_Repository.Data;
using CRM_Repository.DataServices;
using CRM_Repository.ExtendedModel;
using CRM_Repository.ServiceContract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Repository.Service
{
    public class RndProduct_Repository : IRndProduct_Repository, IDisposable
    {
        private bool disposedValue = false; // To detect redundant calls
        private CRM_Repository.Data.elaunch_crmEntities context;
        public RndProduct_Repository(CRM_Repository.Data.elaunch_crmEntities _context)
        {
            context = _context;
        }

        public RNDProductModel GetProductById(int id)
        {
            try
            {
                var data = (from p in context.RNDProductMasters
                                //join pm in context.MainProductMasters on p.MainProductId equals pm.MainProductId
                            join ps in context.SubCategoryMasters on p.SubCategoryId equals ps.SubCategoryId
                            join pc in context.CategoryMasters on ps.CategoryId equals pc.CategoryId
                            select new RNDProductModel
                            {
                                RNDProductId = p.RNDProductId,
                                ProductName = p.ProductName,
                                Description = p.Description,
                                Keyword = p.Keyword,
                                //  EmailSpeech = p.EmailSpeech,
                                CreatedBy = p.CreatedBy,
                                CreatedDate = p.CreatedDate,
                                //  Cataloges = p.Cataloges,
                                //  Photoes = p.Photoes,
                                Videoes = p.Videoes,
                                CategoryId = pc.CategoryId,
                                CategoryName = pc.CategoryName,
                                SubCategoryId = ps.SubCategoryId,
                                SubCategoryName = ps.SubCategoryName,
                                //MainProductId = p.MainProductId,
                                //MainProductName = p.MainProductName,
                            });
                return data.FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int CreateUpdate(RNDProductModel objInputRNDProductMaster)
        {
            string TempImgPath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["TempImgPath"]);
            string RndProductPath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["RndProduct"]);
            string RndProductSupplierPath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["RndProductSupplier"]);

            int resVal;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    RNDProductMaster ObjRNDProductMaster = new RNDProductMaster();
                    if (objInputRNDProductMaster.RNDProductId <= 0)
                    {
                        #region "INSERT"
                        // Product Data
                        ObjRNDProductMaster.SubCategoryId = objInputRNDProductMaster.SubCategoryId;
                        ObjRNDProductMaster.ProductName = objInputRNDProductMaster.ProductName;
                        ObjRNDProductMaster.Description = objInputRNDProductMaster.Description;
                        ObjRNDProductMaster.Keyword = objInputRNDProductMaster.Keyword;
                        ObjRNDProductMaster.EmailSpeech = objInputRNDProductMaster.EmailSpeech;
                        ObjRNDProductMaster.SMSSpeechId = objInputRNDProductMaster.SMSSpeechId;
                        ObjRNDProductMaster.EmailSpeechId = objInputRNDProductMaster.EmailSpeechId;
                        //ObjRNDProductMaster.Cataloges = objInputRNDProductMaster.Cataloges;
                        //ObjRNDProductMaster.Photoes = objInputRNDProductMaster.Photoes;
                        ObjRNDProductMaster.Videoes = objInputRNDProductMaster.Videoes;
                        ObjRNDProductMaster.CreatedBy = objInputRNDProductMaster.CreatedBy;
                        ObjRNDProductMaster.CreatedDate = DateTime.Now;
                        ObjRNDProductMaster.IsActive = true;
                        ObjRNDProductMaster.FMPhotos = objInputRNDProductMaster.FMPhotos;
                        ObjRNDProductMaster.RMPhotos = objInputRNDProductMaster.RMPhotos;
                        ObjRNDProductMaster.MPPhotos = objInputRNDProductMaster.MPPhotos;
                        ObjRNDProductMaster.FMPhotos = objInputRNDProductMaster.FMPhotos;
                        ObjRNDProductMaster.SMSSpeech = objInputRNDProductMaster.SMSSpeech;
                        ObjRNDProductMaster.ChatSpeech = objInputRNDProductMaster.ChatSpeech;

                        ObjRNDProductMaster = AddRNDProduct(ObjRNDProductMaster);
                        //Cateloges
                        if (!string.IsNullOrEmpty(objInputRNDProductMaster.RMPhotos))
                        {
                            string[] CatalogesData = objInputRNDProductMaster.RMPhotos.Split('|');
                            if (CatalogesData.Length > 0)
                            {
                                foreach (var item in CatalogesData)
                                {
                                    if (System.IO.File.Exists(TempImgPath.ToString() + "/" + item.ToString()))
                                    {
                                        if (!System.IO.Directory.Exists(RndProductPath.ToString()))
                                        {
                                            System.IO.Directory.CreateDirectory(RndProductPath.ToString());
                                        }
                                        if (!System.IO.File.Exists(RndProductPath.ToString() + "/" + item.ToString()))
                                        {
                                            System.IO.File.Move(TempImgPath.ToString() + "/" + item.ToString(), RndProductPath.ToString() + "/" + item.ToString());
                                        }
                                    }
                                }
                            }
                        }
                        //Photos
                        if (!string.IsNullOrEmpty(objInputRNDProductMaster.MPPhotos))
                        {
                            string[] PhotoesData = objInputRNDProductMaster.MPPhotos.Split('|');
                            if (PhotoesData.Length > 0)
                            {
                                foreach (var item in PhotoesData)
                                {
                                    if (System.IO.File.Exists(TempImgPath.ToString() + "/" + item.ToString()))
                                    {
                                        if (!System.IO.Directory.Exists(RndProductPath.ToString()))
                                        {
                                            System.IO.Directory.CreateDirectory(RndProductPath.ToString());
                                        }
                                        if (!System.IO.File.Exists(RndProductPath.ToString() + "/" + item.ToString()))
                                        {
                                            System.IO.File.Move(TempImgPath.ToString() + "/" + item.ToString(), RndProductPath.ToString() + "/" + item.ToString());
                                        }
                                    }
                                }
                            }
                        }
                        //Product Photos
                        if (!string.IsNullOrEmpty(objInputRNDProductMaster.FMPhotos))
                        {
                            string[] PhotoesData = objInputRNDProductMaster.FMPhotos.Split('|');
                            if (PhotoesData.Length > 0)
                            {
                                foreach (var item in PhotoesData)
                                {
                                    if (System.IO.File.Exists(TempImgPath.ToString() + "/" + item.ToString()))
                                    {
                                        if (!System.IO.Directory.Exists(RndProductPath.ToString()))
                                        {
                                            System.IO.Directory.CreateDirectory(RndProductPath.ToString());
                                        }
                                        if (!System.IO.File.Exists(RndProductPath.ToString() + "/" + item.ToString()))
                                        {
                                            System.IO.File.Move(TempImgPath.ToString() + "/" + item.ToString(), RndProductPath.ToString() + "/" + item.ToString());
                                        }
                                    }
                                }
                            }
                        }
                        // Supplier Data
                        if (objInputRNDProductMaster.objRndSupplierList != null)
                        {
                            foreach (var item in objInputRNDProductMaster.objRndSupplierList)
                            {
                                RNDSupplierMaster ObjRNDSupplierDetail = new RNDSupplierMaster();
                                ObjRNDSupplierDetail.RNDSupplierId = item.RNDSupplierId;
                                ObjRNDSupplierDetail.RNDProductId = ObjRNDProductMaster.RNDProductId;
                                ObjRNDSupplierDetail.CompanyName = item.CompanyName;
                                if (item.Email != "" && item.Email != null)
                                    ObjRNDSupplierDetail.Email = item.Email;
                                ObjRNDSupplierDetail.MobileNo = item.MobileNo;
                                ObjRNDSupplierDetail.Address = item.Address;
                                ObjRNDSupplierDetail.Attende = item.Attende;
                                ObjRNDSupplierDetail.Price = item.Price;
                                ObjRNDSupplierDetail.Remark = item.Remark;
                                ObjRNDSupplierDetail.Quotations = item.Quotations;
                                ObjRNDSupplierDetail.CityId = item.CityId;
                                ObjRNDSupplierDetail.Website = item.Website;
                                ObjRNDSupplierDetail.TeliPhoneNo = item.TeliPhoneNo;
                                ObjRNDSupplierDetail.UnitId = item.UnitId;
                                ObjRNDSupplierDetail.Catalogue = item.Catalogue;
                                ObjRNDSupplierDetail.Chat = item.Chat;
                                ObjRNDSupplierDetail.ContactEmail = item.ContactEmail;
                                ObjRNDSupplierDetail.CommunicationRecord = item.CommunicationRecord;
                                ObjRNDSupplierDetail.Pincode = item.Pincode;

                                context.RNDSupplierMasters.Add(ObjRNDSupplierDetail);

                                if (!string.IsNullOrEmpty(item.Quotations))
                                {
                                    string[] QuotationsData = item.Quotations.Split('|');
                                    if (QuotationsData.Length > 0)
                                    {
                                        foreach (var qitem in QuotationsData)
                                        {
                                            if (System.IO.File.Exists(TempImgPath.ToString() + "/" + qitem.ToString()))
                                            {
                                                if (!System.IO.Directory.Exists(RndProductSupplierPath.ToString()))
                                                {
                                                    System.IO.Directory.CreateDirectory(RndProductSupplierPath.ToString());
                                                }
                                                if (!System.IO.File.Exists(RndProductSupplierPath.ToString() + "/" + qitem.ToString()))
                                                {
                                                    System.IO.File.Move(TempImgPath.ToString() + "/" + qitem.ToString(), RndProductSupplierPath.ToString() + "/" + qitem.ToString());
                                                }
                                            }
                                        }
                                    }
                                }
                                if (!string.IsNullOrEmpty(item.Catalogue))
                                {
                                    string[] CatalogueData = item.Catalogue.Split('|');
                                    if (CatalogueData.Length > 0)
                                    {
                                        foreach (var qitem in CatalogueData)
                                        {
                                            if (System.IO.File.Exists(TempImgPath.ToString() + "/" + qitem.ToString()))
                                            {
                                                if (!System.IO.Directory.Exists(RndProductSupplierPath.ToString()))
                                                {
                                                    System.IO.Directory.CreateDirectory(RndProductSupplierPath.ToString());
                                                }
                                                if (!System.IO.File.Exists(RndProductSupplierPath.ToString() + "/" + qitem.ToString()))
                                                {
                                                    System.IO.File.Move(TempImgPath.ToString() + "/" + qitem.ToString(), RndProductSupplierPath.ToString() + "/" + qitem.ToString());
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        resVal = 1;
                        #endregion
                    }
                    else
                    {
                        #region "UPDATE"
                        //EDIT
                        //Product Detail
                        ObjRNDProductMaster = context.RNDProductMasters.Find(objInputRNDProductMaster.RNDProductId);
                        ObjRNDProductMaster.SubCategoryId = objInputRNDProductMaster.SubCategoryId;
                        ObjRNDProductMaster.ProductName = objInputRNDProductMaster.ProductName;
                        ObjRNDProductMaster.Description = objInputRNDProductMaster.Description;
                        ObjRNDProductMaster.Keyword = objInputRNDProductMaster.Keyword;
                        ObjRNDProductMaster.EmailSpeech = objInputRNDProductMaster.EmailSpeech;
                        //   ObjRNDProductMaster.Cataloges = objInputRNDProductMaster.Cataloges;
                        ObjRNDProductMaster.SMSSpeechId = objInputRNDProductMaster.SMSSpeechId;
                        ObjRNDProductMaster.EmailSpeechId = objInputRNDProductMaster.EmailSpeechId;
                        //  ObjRNDProductMaster.Photoes = objInputRNDProductMaster.Photoes;
                        ObjRNDProductMaster.Videoes = objInputRNDProductMaster.Videoes;
                        ObjRNDProductMaster.ModifyBy = objInputRNDProductMaster.CreatedBy;
                        ObjRNDProductMaster.ModifyDate = DateTime.Now;
                        ObjRNDProductMaster.IsActive = true;
                        ObjRNDProductMaster.FMPhotos = objInputRNDProductMaster.FMPhotos;
                        ObjRNDProductMaster.RMPhotos = objInputRNDProductMaster.RMPhotos;
                        ObjRNDProductMaster.MPPhotos = objInputRNDProductMaster.MPPhotos;
                        ObjRNDProductMaster.FMPhotos = objInputRNDProductMaster.FMPhotos;
                        ObjRNDProductMaster.SMSSpeech = objInputRNDProductMaster.SMSSpeech;
                        ObjRNDProductMaster.ChatSpeech = objInputRNDProductMaster.ChatSpeech;



                        UpdateRNDProduct(ObjRNDProductMaster);
                        //Cateloges
                        if (!string.IsNullOrEmpty(objInputRNDProductMaster.RMPhotos))
                        {
                            string[] CatalogesData = objInputRNDProductMaster.RMPhotos.Split('|');
                            if (CatalogesData.Length > 0)
                            {
                                foreach (var item in CatalogesData)
                                {
                                    if (System.IO.File.Exists(TempImgPath.ToString() + "/" + item.ToString()))
                                    {
                                        if (!System.IO.Directory.Exists(RndProductPath.ToString()))
                                        {
                                            System.IO.Directory.CreateDirectory(RndProductPath.ToString());
                                        }
                                        if (!System.IO.File.Exists(RndProductPath.ToString() + "/" + item.ToString()))
                                        {
                                            System.IO.File.Move(TempImgPath.ToString() + "/" + item.ToString(), RndProductPath.ToString() + "/" + item.ToString());
                                        }
                                    }
                                }
                            }
                        }
                        //Photos
                        if (!string.IsNullOrEmpty(objInputRNDProductMaster.MPPhotos))
                        {
                            string[] PhotoesData = objInputRNDProductMaster.MPPhotos.Split('|');
                            if (PhotoesData.Length > 0)
                            {
                                foreach (var item in PhotoesData)
                                {
                                    if (System.IO.File.Exists(TempImgPath.ToString() + "/" + item.ToString()))
                                    {
                                        if (!System.IO.Directory.Exists(RndProductPath.ToString()))
                                        {
                                            System.IO.Directory.CreateDirectory(RndProductPath.ToString());
                                        }
                                        if (!System.IO.File.Exists(RndProductPath.ToString() + "/" + item.ToString()))
                                        {
                                            System.IO.File.Move(TempImgPath.ToString() + "/" + item.ToString(), RndProductPath.ToString() + "/" + item.ToString());
                                        }
                                    }
                                }
                            }
                        }
                        //Product Photos
                        if (!string.IsNullOrEmpty(objInputRNDProductMaster.FMPhotos))
                        {
                            string[] PhotoesData = objInputRNDProductMaster.FMPhotos.Split('|');
                            if (PhotoesData.Length > 0)
                            {
                                foreach (var item in PhotoesData)
                                {
                                    if (System.IO.File.Exists(TempImgPath.ToString() + "/" + item.ToString()))
                                    {
                                        if (!System.IO.Directory.Exists(RndProductPath.ToString()))
                                        {
                                            System.IO.Directory.CreateDirectory(RndProductPath.ToString());
                                        }
                                        if (!System.IO.File.Exists(RndProductPath.ToString() + "/" + item.ToString()))
                                        {
                                            System.IO.File.Move(TempImgPath.ToString() + "/" + item.ToString(), RndProductPath.ToString() + "/" + item.ToString());
                                        }
                                    }
                                }
                            }
                        }
                        // RND Supplier Detail
                        if (objInputRNDProductMaster.objRndSupplierList != null)
                        {
                            foreach (var item in objInputRNDProductMaster.objRndSupplierList)
                            {

                                if (item.Status == 1)
                                {
                                    RNDSupplierMaster ObjRNDSupplierDetail = new RNDSupplierMaster();
                                    ObjRNDSupplierDetail.RNDSupplierId = item.RNDSupplierId;
                                    ObjRNDSupplierDetail.RNDProductId = ObjRNDProductMaster.RNDProductId;
                                    ObjRNDSupplierDetail.CompanyName = item.CompanyName;
                                    ObjRNDSupplierDetail.TeliPhoneNo = item.TeliPhoneNo;
                                    if (item.Email != "" && item.Email != null)
                                        ObjRNDSupplierDetail.Email = item.Email;
                                    ObjRNDSupplierDetail.MobileNo = item.MobileNo;
                                    ObjRNDSupplierDetail.Address = item.Address;
                                    ObjRNDSupplierDetail.Attende = item.Attende;
                                    ObjRNDSupplierDetail.Price = item.Price;
                                    ObjRNDSupplierDetail.Remark = item.Remark;
                                    ObjRNDSupplierDetail.Quotations = item.Quotations;
                                    ObjRNDSupplierDetail.UnitId = item.UnitId;
                                    ObjRNDSupplierDetail.CityId = item.CityId;
                                    ObjRNDSupplierDetail.Website = item.Website;
                                    ObjRNDSupplierDetail.Catalogue = item.Catalogue;
                                    ObjRNDSupplierDetail.Chat = item.Chat;
                                    ObjRNDSupplierDetail.ContactEmail = item.ContactEmail;
                                    ObjRNDSupplierDetail.CommunicationRecord = item.CommunicationRecord;
                                    ObjRNDSupplierDetail.Pincode = item.Pincode;

                                    context.RNDSupplierMasters.Add(ObjRNDSupplierDetail);
                                }
                                else if (item.Status == 2)
                                {
                                    RNDSupplierMaster ObjRNDSupplierDetail = context.RNDSupplierMasters.Find(item.RNDSupplierId);
                                    ObjRNDSupplierDetail.RNDSupplierId = item.RNDSupplierId;
                                    ObjRNDSupplierDetail.RNDProductId = ObjRNDProductMaster.RNDProductId;
                                    ObjRNDSupplierDetail.CompanyName = item.CompanyName;
                                    ObjRNDSupplierDetail.TeliPhoneNo = item.TeliPhoneNo;
                                    if (item.Email != "" && item.Email != null)
                                        ObjRNDSupplierDetail.Email = item.Email;
                                    ObjRNDSupplierDetail.MobileNo = item.MobileNo;
                                    ObjRNDSupplierDetail.Address = item.Address;
                                    ObjRNDSupplierDetail.Attende = item.Attende;
                                    ObjRNDSupplierDetail.Price = item.Price;
                                    ObjRNDSupplierDetail.Remark = item.Remark;
                                    ObjRNDSupplierDetail.Quotations = item.Quotations;
                                    ObjRNDSupplierDetail.UnitId = item.UnitId;
                                    ObjRNDSupplierDetail.Website = item.Website;
                                    ObjRNDSupplierDetail.CityId = item.CityId;
                                    ObjRNDSupplierDetail.Website = item.Website;
                                    ObjRNDSupplierDetail.Catalogue = item.Catalogue;
                                    ObjRNDSupplierDetail.Chat = item.Chat;
                                    ObjRNDSupplierDetail.ContactEmail = item.ContactEmail;
                                    ObjRNDSupplierDetail.CommunicationRecord = item.CommunicationRecord;
                                    ObjRNDSupplierDetail.Pincode = item.Pincode;
                                    context.Entry(ObjRNDSupplierDetail).State = System.Data.Entity.EntityState.Modified;
                                }
                                else if (item.Status == 3)
                                {
                                    RNDSupplierMaster ObjRNDSupplierDetail = context.RNDSupplierMasters.Find(item.RNDSupplierId);
                                    context.RNDSupplierMasters.Remove(ObjRNDSupplierDetail);
                                }
                                //Supplier Quotation
                                if (!string.IsNullOrEmpty(item.Quotations))
                                {
                                    string[] QuotationsData = item.Quotations.Split('|');
                                    if (QuotationsData.Length > 0)
                                    {
                                        foreach (var qitem in QuotationsData)
                                        {
                                            if (System.IO.File.Exists(TempImgPath.ToString() + "/" + qitem.ToString()))
                                            {
                                                if (!System.IO.Directory.Exists(RndProductSupplierPath.ToString()))
                                                {
                                                    System.IO.Directory.CreateDirectory(RndProductSupplierPath.ToString());
                                                }
                                                if (!System.IO.File.Exists(RndProductSupplierPath.ToString() + "/" + qitem.ToString()))
                                                {
                                                    System.IO.File.Move(TempImgPath.ToString() + "/" + qitem.ToString(), RndProductSupplierPath.ToString() + "/" + qitem.ToString());
                                                }
                                            }
                                        }
                                    }
                                }
                                if (!string.IsNullOrEmpty(item.Catalogue))
                                {
                                    string[] CatalogueData = item.Catalogue.Split('|');
                                    if (CatalogueData.Length > 0)
                                    {
                                        foreach (var qitem in CatalogueData)
                                        {
                                            if (System.IO.File.Exists(TempImgPath.ToString() + "/" + qitem.ToString()))
                                            {
                                                if (!System.IO.Directory.Exists(RndProductSupplierPath.ToString()))
                                                {
                                                    System.IO.Directory.CreateDirectory(RndProductSupplierPath.ToString());
                                                }
                                                if (!System.IO.File.Exists(RndProductSupplierPath.ToString() + "/" + qitem.ToString()))
                                                {
                                                    System.IO.File.Move(TempImgPath.ToString() + "/" + qitem.ToString(), RndProductSupplierPath.ToString() + "/" + qitem.ToString());
                                                }
                                            }
                                        }
                                    }
                                }

                            }
                        }
                        resVal = 2;
                        #endregion 
                    }
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

        public int Delete(RNDProductModel objInputRNDProductMaster)
        {
            int resVal;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    RNDProductMaster ObjRNDProductMaster = context.RNDProductMasters.Find(objInputRNDProductMaster.RNDProductId);
                    ObjRNDProductMaster.IsActive = false;
                    ObjRNDProductMaster.DeletedBy = objInputRNDProductMaster.DeleteBy;
                    ObjRNDProductMaster.DeletedDate = DateTime.Now;
                    UpdateRNDProduct(ObjRNDProductMaster);

                    List<RNDSupplierMaster> ObjRNDSupplier = context.RNDSupplierMasters.Where(x => x.RNDProductId == objInputRNDProductMaster.RNDProductId).ToList();
                    if (ObjRNDSupplier.Count > 0)
                    {
                        foreach (var item in ObjRNDSupplier)
                        {
                            RNDSupplierMaster ObjRNDSupplierDetail = context.RNDSupplierMasters.Find(item.RNDSupplierId);
                            context.RNDSupplierMasters.Remove(ObjRNDSupplierDetail);
                        }
                    }
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

        //public void SaveProduct(RNDProductModel modal, List<ExtendedModel.RNDSupplierMaster> supplierList)
        //{   //update product

        //    RNDProductMaster product = new RNDProductMaster
        //    {
        //        SubCategoryId = modal.SubCategoryId,
        //        ProductName = modal.ProductName,
        //        Description = modal.Description,
        //        Keyword = modal.Keyword,
        //        Photoes = modal.Photoes,
        //        Cataloges = modal.Cataloges,
        //        EmailSpeech = modal.EmailSpeech,
        //        Videoes = modal.Videoes,
        //        //ModifyBy = modal.CreatedBy,
        //        ModifyDate = DateTime.Now,
        //        IsActive = true,
        //        CreatedDate = DateTime.Now
        //    };
        //    if (modal.RNDProductId > 0)
        //    {
        //        product.RNDProductId = modal.RNDProductId;
        //        context.Entry(product).State = System.Data.Entity.EntityState.Modified;
        //        var itemsToDelete = context.RNDSupplierMasters.Where(x => x.RNDProductId == modal.RNDProductId);
        //        context.RNDSupplierMasters.RemoveRange(itemsToDelete);
        //        foreach (var i in supplierList)
        //        {
        //            i.RNDProductId = modal.RNDProductId;
        //            context.RNDSupplierMasters.Add(i);
        //        }

        //    }
        //    else
        //    {
        //        //add product
        //        product = AddRNDProduct(product);
        //        foreach (var i in supplierList)
        //        {
        //            i.RNDProductId = product.RNDProductId;
        //            context.RNDSupplierMasters.Add(i);
        //        }

        //    }
        //    context.SaveChanges();
        //}
        public RNDProductMaster AddRNDProduct(RNDProductMaster obj)
        {
            try
            {
                context.RNDProductMasters.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateRNDProduct(RNDProductMaster obj)
        {
            try
            {
                context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public RNDProductModel GetAllRndProductInfoById(int Id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@Id", Id);
                return new dalc().GetDataTable_Text(@"Select P.*,SC.SubCategoryId,SC.SubCategoryName,CC.CategoryId,CC.CategoryName,P.EmailSpeechId,Title,
                                                        P.SMSSpeechId,SS.SMSTitle
                                                        FROM RNDProductMaster As P With(NOLOCK) 
                                                        LEFT join SubCategoryMaster As SC With(NOLOCK) On SC.SubCategoryId = P.SubCategoryId
                                                        LEFT join CategoryMaster As CC With(NOLOCK) On CC.CategoryId = SC.CategoryId
                                                        LEFT JOIN EmailSpeechMaster as ES on ES.SpeechId = P.EmailSpeechId  
                                                        LEFT JOIN SMSSpeechMaster as SS on SS.SMSId = P.SMSSpeechId 
                                                    WHERE RNDProductId = @Id  And ISNULL(P.IsActive,0)=1 ", para).ConvertToList<RNDProductModel>().FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<RNDSupplierMaster> GetAllRNDSupplierDetail(int Id)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter().CreateParameter("@Id", Id);
                return new dalc().GetDataTable_Text(@"SELECT RNDS.*,C.CityName,S.StateId,S.StateName,CM.CountryId,CM.CountryName,RNDS.UnitId,CRM.CurrencyName,SM.SupplierId
                                                        FROM RNDSupplierMaster AS RNDS With(NOLOCK)
                                                        Inner JOIN CityMaster AS C  With(NOLOCK) ON C.CityId=RNDS.CityId
                                                        Left JOIN StateMaster AS S  With(NOLOCK) ON S.StateId=C.StateId
                                                        Left JOIN CountryMaster AS CM  With(NOLOCK) ON CM.CountryId=S.CountryId  
                                                        Left JOIN CurrencyMaster AS CRM  With(NOLOCK) ON CRM.CurrencyId=RNDS.UnitId 
                                                        Left JOIN SupplierMaster AS SM  With(NOLOCK) ON RTRIM(LTRIM(SM.CompanyName))=RTRIM(LTRIM(RNDS.CompanyName)) 
                                                        WHERE RNDProductId = @Id ", para).ConvertToList<RNDSupplierMaster>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

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
    }
}
