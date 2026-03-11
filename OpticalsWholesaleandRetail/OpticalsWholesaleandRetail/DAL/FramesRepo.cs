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

        public List<FrameBrandDto> GetFrames(int id)
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
                    entity.IsDeleted = false;
                    entity.CreatedBy = id;
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
                    result.IsDeleted = false;
                    result.CreatedBy = id;
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
    }
}
