using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpticalsWholesaleandRetail.Utilities;
using OpticalsWholesaleandRetail.Models.DTO;

namespace OpticalsWholesaleandRetail.DAL
{
    public interface ILensRepo
    {
        List<LensTypeDto> GetLensTypes();
        bool AddLensType(LensTypeDto dto);
        bool UpdateLensType(LensTypeDto dto);
        bool DeleteLensType(int id);
        List<LensCategoryDto> GetLensCategories();
        bool Add(LensCategoryDto dto);
        bool Update(LensCategoryDto dto);
        bool Delete(int id);
    }
}
