using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpticalsWholesaleandRetail.Utilities;
//using Microsoft.AspNetCore.Identity.Data;
using OpticalsWholesaleandRetail.Models.DTO;
using OpticalsWholesaleandRetail.Models.Entity;
using System.Collections.Generic;

namespace OpticalsWholesaleandRetail.DAL
{
    public class LensRepo:ILensRepo
    {
        private readonly MyDbContext _context;

        public LensRepo(MyDbContext context)
        {
            _context = context;
        }

        public List<LensTypeDto> GetLensTypes()
        {
            var data = (from l in _context.LTypeEntities
                        join s in _context.suppliersEntity
                        on l.SupplierId equals s.SupplierId into sup
                        from s in sup.DefaultIfEmpty()

                        where l.IsDeleted == false || l.IsDeleted == null

                        select new LensTypeDto
                        {
                            LensTypeId = l.LensTypeId,
                            TypeName = l.TypeName,
                            SupplierId = l.SupplierId,
                            SupplierName = s.ContactPerson
                        }).ToList();

            return data;
        }

        public bool AddLensType(LensTypeDto dto)
        {
            LensTypeEntity obj = new LensTypeEntity
            {
                TypeName = dto.TypeName,
                SupplierId = dto.SupplierId,
                IsDeleted = false
            };

            _context.LTypeEntities.Add(obj);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateLensType(LensTypeDto dto)
        {
            var data = _context.LTypeEntities.FirstOrDefault(x => x.LensTypeId == dto.LensTypeId);

            if (data != null)
            {
                data.TypeName = dto.TypeName;
                data.SupplierId = dto.SupplierId;
                _context.SaveChanges();
            }

            return true;
        }

        public bool DeleteLensType(int id)
        {
            var data = _context.LTypeEntities.FirstOrDefault(x => x.LensTypeId == id);

            if (data != null)
            {
                data.IsDeleted = true;
                _context.SaveChanges();
            }

            return true;
        }
        public List<LensCategoryDto> GetLensCategories()
        {
            return (from l in _context.LCategoryEntity
                    join s in _context.suppliersEntity
                    on l.SupplierId equals s.SupplierId into sup
                    from s in sup.DefaultIfEmpty()
                    where l.IsDeleted == false || l.IsDeleted == null
                    select new LensCategoryDto
                    {
                        LensCategoryId = l.LensCategoryId,
                        CategoryName = l.CategoryName,
                        SupplierId = l.SupplierId,
                        SupplierName = s.ContactPerson
                    }).ToList();
        }

        public bool Add(LensCategoryDto dto)
        {
            _context.LCategoryEntity.Add(new LensCategoryEntity
            {
                CategoryName = dto.CategoryName,
                SupplierId = dto.SupplierId,
                IsDeleted = false
            });
            _context.SaveChanges();
            return true;
        }

        public bool Update(LensCategoryDto dto)
        {
            var data = _context.LCategoryEntity.Find(dto.LensCategoryId);
            if (data != null)
            {
                data.CategoryName = dto.CategoryName;
                data.SupplierId = dto.SupplierId;
                _context.SaveChanges();
            }
            return true;
        }

        public bool Delete(int id)
        {
            var data = _context.LCategoryEntity.Find(id);
            if (data != null)
            {
                data.IsDeleted = true;
                _context.SaveChanges();
            }
            return true;
        }
    }
}
