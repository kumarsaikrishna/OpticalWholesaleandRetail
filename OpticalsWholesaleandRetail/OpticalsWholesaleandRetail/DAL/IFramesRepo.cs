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
        List<FrameBrandDto> GetFramesB(int id);
        GenericResponse AddFrameBrands(FrameBrandDto obj, int id);
        GenericResponse UpdateFrameBrand(FrameBrandDto obj, int id);
        GenericResponse DeleteFrameBrand(int id);
        List<FrameModelDto> GetFrameModelsById(int id);
        GenericResponse AddFrameModels(FrameModelDto obj, int id);
        GenericResponse UpdateFrameModel(FrameModelDto obj, int id);
        GenericResponse DeleteFrameModel(int id);
        List<FrameSizeDto> GetFrameSizes();
        bool AddFrameSize(FrameSizeDto obj);
        bool UpdateFrameSize(FrameSizeDto obj);
        bool DeleteFrameSize(int id);
        List<FrameCategoryDto> GetFrameCategories();

        FrameCategoryDto GetFrameCategoryById(int id);

        bool AddFrameCategory(FrameCategoryDto dto);

        bool UpdateFrameCategory(FrameCategoryDto dto);

        bool DeleteFrameCategory(int id);
        List<FrameDto> GetFrames();

        FrameDto GetFrameById(int id);

        bool AddFrame(FrameDto dto);

        bool UpdateFrame(FrameDto dto);

        bool DeleteFrame(int id);
    }
}
