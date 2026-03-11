using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpticalsWholesaleandRetail.Utilities;
//using Microsoft.AspNetCore.Identity.Data;
using OpticalsWholesaleandRetail.Models.DTO;
using System.Collections.Generic;

namespace OpticalsWholesaleandRetail.DAL
{
    public interface IFramesRepo
    {
        List<FrameBrandDto> GetFrames(int id);
        GenericResponse AddFrameBrands(FrameBrandDto obj, int id);
        GenericResponse UpdateFrameBrand(FrameBrandDto obj, int id);
        GenericResponse DeleteFrameBrand(int id);
        List<FrameModelDto> GetFrameModelsById(int id);
        GenericResponse AddFrameModels(FrameModelDto obj, int id);
        GenericResponse UpdateFrameModel(FrameModelDto obj, int id);
        GenericResponse DeleteFrameModel(int id);
    }
}
