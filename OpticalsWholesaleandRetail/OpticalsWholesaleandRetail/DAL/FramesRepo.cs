using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpticalsWholesaleandRetail.Models.DTO;
using OpticalsWholesaleandRetail.Models.Entity;
using Microsoft.Extensions.Configuration;
using OpticalsWholesaleandRetail.Utilities;

namespace OpticalsWholesaleandRetail.DAL
{
    public class FramesRepo:IFramesRepo
    {
        private readonly MyDbContext _context;
        private readonly IConfiguration _config;

        public FramesRepo(MyDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public List<FrameBrandDto> GetFramesB(int id)
        {
            var result = (from frames in _context.fBrandEntity
                          where frames.IsDeleted == false
                          select new FrameBrandDto
                          {
                              BrandId = frames.BrandId,
                              BrandName = frames.BrandName,
                              Category=frames.Category,
                              SupplierId=frames.SupplierId
                          }).ToList();

            return result;
        }


        public GenericResponse AddFrameBrands(FrameBrandDto obj, int id)
        {
            GenericResponse response = new GenericResponse();
            FrameBrandEntity entity = new FrameBrandEntity();
            int sid = _context.suppliersEntity
     .Where(s => _context.userEntity
         .Any(u => u.UserId == s.CustomerId
                && u.FullName == obj.SupplierName
                && u.IsDeleted == false))
     .Select(s => s.SupplierId)
     .FirstOrDefault();
            int count = _context.fBrandEntity.Where(a => a.BrandName == obj.BrandName&&a.SupplierId==sid && a.IsDeleted == false).Count();
            try
            {
                if (count == 0)
                {
                    entity.BrandName = obj.BrandName;
                    entity.Category = obj.Category;
                    entity.SupplierId = sid;
                    entity.IsDeleted = false;

                    _context.fBrandEntity.Add(entity);
                    _context.SaveChanges();
                    response.statuCode = 1;
                    response.message = "Success";
                    response.currentId = entity.BrandId;
                }
                else
                {
                    response.statuCode = 0;
                    response.message = "Frame Brand Alredy exist";
                }
            }
            catch (Exception ex)
            {
                response.statuCode = 0;
                response.message = "failed to add Frame Brand" + ex;
            }
            return response;
        }

        public GenericResponse UpdateFrameBrand(FrameBrandDto obj, int id)
        {
            GenericResponse response = new GenericResponse();
            var result = _context.fBrandEntity.Where(a => a.BrandId == obj.BrandId && a.IsDeleted == false).FirstOrDefault();
            int count = _context.fBrandEntity.Where(a => a.BrandName == obj.BrandName).Count();
            try
            {
                if (count == 0)
                {
                    result.BrandId = obj.BrandId;
                    result.BrandName = obj.BrandName;
                    result.Category = obj.Category;
                    result.SupplierId = obj.SupplierId;
                    result.IsDeleted = false;
                    _context.fBrandEntity.Update(result);
                    _context.SaveChanges();
                    response.statuCode = 1;
                    response.message = "Success";
                    response.currentId = result.BrandId;
                }
                else
                {
                    response.statuCode = 0;
                    response.message = "Frame Brand Alredy exist";
                }

            }
            catch (Exception ex)
            {
                response.statuCode = 0;
                response.message = "failed to Update Frame Brand" + ex;
            }
            return response;
        }

        public GenericResponse DeleteFrameBrand(int id)
        {
            GenericResponse response = new GenericResponse();
            var result = _context.fBrandEntity.Where(a => a.BrandId == id && a.IsDeleted == false).FirstOrDefault();
            try
            {
                result.IsDeleted = true;
                _context.fBrandEntity.Update(result);
                _context.SaveChanges();
                response.statuCode = 1;
                response.message = "Success";
                response.currentId = result.BrandId;

            }
            catch (Exception ex)
            {
                response.statuCode = 0;
                response.message = "Failed to Delete Frame Brand" + ex;
            }
            return response;
        }
        public List<FrameModelDto> GetFrameModelsById(int id)
        {
            var result = (from frames in _context.fModelEntity
                          where frames.IsDeleted == false && frames.BrandId==id
                          select new FrameModelDto
                          {  ModelId=frames.ModelId,
                              BrandId = frames.BrandId,
                              BrandName = _context.fBrandEntity.Where(a=>a.BrandId==frames.BrandId && a.IsDeleted==false).Select(a=>a.BrandName).FirstOrDefault(),
                              ModelNumber = frames.ModelNumber
                          }).ToList();

            return result;
        }


        public GenericResponse AddFrameModels(FrameModelDto obj, int id)
        {
            GenericResponse response = new GenericResponse();
            FrameModelEntity entity = new FrameModelEntity();
            int Bid = _context.fBrandEntity
     .Where(s =>s.BrandName==obj.BrandName && s.IsDeleted==false).Select(a=>a.BrandId)
     .FirstOrDefault();
            int count = _context.fModelEntity.Where(a => a.ModelNumber == obj.ModelNumber && a.BrandId == Bid && a.IsDeleted == false).Count();
            try
            {
                if (count == 0)
                {
                    entity.BrandId = Bid;
                    entity.ModelNumber = obj.ModelNumber;
                    entity.Color = obj.Color;
                    entity.FrameSizeId = obj.FrameSizeId;
                    entity.GenderType = obj.GenderType;
                    entity.PurchasePrice = obj.PurchasePrice;
                    entity.SellingPrice = obj.SellingPrice;
                    entity.StockQuantity = obj.StockQuantity;
                    entity.IsDeleted = false;
                    _context.fModelEntity.Add(entity);
                    _context.SaveChanges();
                    response.statuCode = 1;
                    response.message = "Success";
                    response.currentId = entity.ModelId;
                }
                else
                {
                    response.statuCode = 0;
                    response.message = "Frame Model Alredy exist";
                }
            }
            catch (Exception ex)
            {
                response.statuCode = 0;
                response.message = "failed to add Frame Model" + ex;
            }
            return response;
        }

        public GenericResponse UpdateFrameModel(FrameModelDto obj, int id)
        {
            GenericResponse response = new GenericResponse();
            var result = _context.fModelEntity.Where(a => a.ModelId == obj.ModelId && a.IsDeleted == false).FirstOrDefault();
            int Bid = _context.fBrandEntity
     .Where(s => s.BrandName == obj.BrandName && s.IsDeleted == false).Select(a => a.BrandId)
     .FirstOrDefault();
            int count = _context.fModelEntity.Where(a => a.ModelNumber == obj.ModelNumber).Count();
            try
            {
                if (count == 0)
                {
                    result.ModelId = obj.ModelId;
                    result.BrandId = Bid;
                    result.ModelNumber = obj.ModelNumber;
                    result.Color = obj.Color;
                    result.FrameSizeId = obj.FrameSizeId;
                    result.GenderType = obj.GenderType;
                    result.PurchasePrice = obj.PurchasePrice;
                    result.SellingPrice = obj.SellingPrice;
                    result.StockQuantity = obj.StockQuantity;
                    result.IsDeleted = false;
                    _context.fModelEntity.Update(result);
                    _context.SaveChanges();
                    response.statuCode = 1;
                    response.message = "Success";
                    response.currentId = result.ModelId;
                }
                else
                {
                    response.statuCode = 0;
                    response.message = "Frame Model Alredy exist";
                }

            }
            catch (Exception ex)
            {
                response.statuCode = 0;
                response.message = "failed to Update Frame Model" + ex;
            }
            return response;
        }

        public GenericResponse DeleteFrameModel(int id)
        {
            GenericResponse response = new GenericResponse();
            var result = _context.fModelEntity.Where(a => a.ModelId == id && a.IsDeleted == false).FirstOrDefault();
            try
            {
                result.IsDeleted = true;
                _context.fModelEntity.Update(result);
                _context.SaveChanges();
                response.statuCode = 1;
                response.message = "Success";
                response.currentId = result.ModelId;

            }
            catch (Exception ex)
            {
                response.statuCode = 0;
                response.message = "Failed to Delete Frame Model" + ex;
            }
            return response;
        }

        public List<FrameSizeDto> GetFrameSizes()
        {
            var data = (from s in _context.fSizeEntity
                        where s.IsDeleted == false
                        select new FrameSizeDto
                        {
                            SizeId = s.SizeId,
                            SizeName = s.SizeName,
                            SupplierId = s.SupplierId,
                            IsDeleted = s.IsDeleted
                        }).ToList();

            return data;
        }

        public bool AddFrameSize(FrameSizeDto obj)
        {
            var size = new FrameSizeEntity
            {
                SizeName = obj.SizeName,
                SupplierId = obj.SupplierId,
                IsDeleted = false
            };

            _context.fSizeEntity.Add(size);
            _context.SaveChanges();

            return true;
        }

        public bool UpdateFrameSize(FrameSizeDto obj)
        {
            var size = _context.fSizeEntity.FirstOrDefault(x => x.SizeId == obj.SizeId);

            if (size != null)
            {
                size.SizeName = obj.SizeName;
                size.SupplierId = obj.SupplierId;

                _context.SaveChanges();
            }

            return true;
        }

        public bool DeleteFrameSize(int id)
        {
            var size = _context.fSizeEntity.FirstOrDefault(x => x.SizeId == id);

            if (size != null)
            {
                size.IsDeleted = true;
                _context.SaveChanges();
            }

            return true;
        }

        public List<FrameCategoryDto> GetFrameCategories()
        {
            var data = (from c in _context.fCategoryEntities
                        join s in _context.suppliersEntity
                        on c.SupplierId equals s.SupplierId into sup
                        from s in sup.DefaultIfEmpty()

                        where c.IsDeleted == false || c.IsDeleted == null

                        select new FrameCategoryDto
                        {
                            CategoryId = c.CategoryId,
                            CategoryName = c.CategoryName,
                            SupplierId = c.SupplierId,
                            SupplierName = s.ContactPerson
                        }).ToList();

            return data;
        }

        public FrameCategoryDto GetFrameCategoryById(int id)
        {
            var data = _context.fCategoryEntities
                .Where(x => x.CategoryId == id)
                .Select(x => new FrameCategoryDto
                {
                    CategoryId = x.CategoryId,
                    CategoryName = x.CategoryName,
                    SupplierId = x.SupplierId
                }).FirstOrDefault();

            return data;
        }

        public bool AddFrameCategory(FrameCategoryDto dto)
        {
            FrameCategoryEntity entity = new FrameCategoryEntity();

            entity.CategoryName = dto.CategoryName;
            entity.SupplierId = dto.SupplierId;
            entity.IsDeleted = false;

            _context.fCategoryEntities.Add(entity);
            _context.SaveChanges();

            return true;
        }

        public bool UpdateFrameCategory(FrameCategoryDto dto)
        {
            var data = _context.fCategoryEntities
                .FirstOrDefault(x => x.CategoryId == dto.CategoryId);

            if (data != null)
            {
                data.CategoryName = dto.CategoryName;
                data.SupplierId = dto.SupplierId;

                _context.SaveChanges();
            }

            return true;
        }

        public bool DeleteFrameCategory(int id)
        {
            var data = _context.fCategoryEntities
                .FirstOrDefault(x => x.CategoryId == id);

            if (data != null)
            {
                data.IsDeleted = true;
                _context.SaveChanges();
            }

            return true;
        }
        public List<FrameDto> GetFrames()
        {
            var data = (from f in _context.frameEntities
                        join b in _context.fBrandEntity on f.BrandId equals b.BrandId
                        join m in _context.fModelEntity on f.ModelId equals m.ModelId
                        join c in _context.fCategoryEntities on f.CategoryId equals c.CategoryId
                        join sup in _context.suppliersEntity on f.SupplierId equals sup.SupplierId

                        where f.IsDeleted == false || f.IsDeleted == null

                        select new FrameDto
                        {
                            FrameId = f.FrameId,
                            BrandId = f.BrandId,
                            BrandName = b.BrandName,
                            ModelId = f.ModelId,
                            ModelNumber = m.ModelNumber,
                            CategoryId = f.CategoryId,
                            CategoryName = c.CategoryName,
                            SupplierId = f.SupplierId,
                            SupplierName = sup.ContactPerson,
                            CostPrice = f.CostPrice,
                            SellingPrice = f.SellingPrice,
                            StockQty = f.StockQty,
                            ReorderLevel = f.ReorderLevel
                        }).ToList();

            return data;
        }

        public FrameDto GetFrameById(int id)
        {
            return _context.frameEntities
                .Where(x => x.FrameId == id)
                .Select(x => new FrameDto
                {
                    FrameId = x.FrameId,
                    BrandId = x.BrandId,
                    ModelId = x.ModelId,
                    CategoryId = x.CategoryId,
                    SupplierId = x.SupplierId,
                    CostPrice = x.CostPrice,
                    SellingPrice = x.SellingPrice,
                    StockQty = x.StockQty,
                    ReorderLevel = x.ReorderLevel
                }).FirstOrDefault();
        }

        public bool AddFrame(FrameDto dto)
        {
            FrameEntity entity = new FrameEntity();

            entity.BrandId = dto.BrandId;
            entity.ModelId = dto.ModelId;
            entity.CategoryId = dto.CategoryId;
            entity.SupplierId = dto.SupplierId;
            entity.CostPrice = dto.CostPrice;
            entity.SellingPrice = dto.SellingPrice;
            entity.StockQty = dto.StockQty;
            entity.ReorderLevel = dto.ReorderLevel;
            entity.IsDeleted = false;

            _context.frameEntities.Add(entity);
            _context.SaveChanges();

            return true;
        }

        public bool UpdateFrame(FrameDto dto)
        {
            var data = _context.frameEntities.FirstOrDefault(x => x.FrameId == dto.FrameId);

            if (data != null)
            {
                data.BrandId = dto.BrandId;
                data.ModelId = dto.ModelId;
                data.CategoryId = dto.CategoryId;
                data.SupplierId = dto.SupplierId;
                data.CostPrice = dto.CostPrice;
                data.SellingPrice = dto.SellingPrice;
                data.StockQty = dto.StockQty;
                data.ReorderLevel = dto.ReorderLevel;

                _context.SaveChanges();
            }

            return true;
        }

        public bool DeleteFrame(int id)
        {
            var data = _context.frameEntities.FirstOrDefault(x => x.FrameId == id);

            if (data != null)
            {
                data.IsDeleted = true;
                _context.SaveChanges();
            }

            return true;
        }
    }

}
