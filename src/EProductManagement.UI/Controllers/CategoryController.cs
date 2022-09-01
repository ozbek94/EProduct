using AutoMapper;
using EProductManagement.Domain.DTOs;
using EProductManagement.Domain.Entities;
using EProductManagement.Domain.Helpers;
using EProductManagement.Domain.Repositories;
using EProductManagement.UI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EProductManagement.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this._categoryRepository = categoryRepository;
            this._mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Category(CategoryCreationModel categoryModel)
        {
            //CustomHttpResponseMessage<PartyDTO> response = new CustomHttpResponseMessage<PartyDTO>();

            //if (!HttpContext.User.DecideIfAdmin())
            //{
            //    response.Success = false;
            //    response.ErrorMessage = "Yetkiniz yoktur.";
            //    return BadRequest(response.ErrorMessage);
            //};
            var category = _mapper.Map<Category>(categoryModel);
            await _categoryRepository.CreateCategory(category);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetById(int Id)
        {
            var category = await _categoryRepository.GetById(Id);
            var categoryModel = _mapper.Map<CategoryModel>(category);
            return Ok(categoryModel);
        }

        [HttpGet("ByCategoryId")]
        public async Task<IActionResult> GetByUpperCategoryId(int UpperCategoryId)
        {
            var category = await _categoryRepository.GetByUpperCategoryId(UpperCategoryId);
            var categoryModel = _mapper.Map<List<CategoryModel>>(category);
            return Ok(categoryModel);
        }

        [HttpGet("MasterCategory")]
        public async Task<IActionResult> GetMasterCategory()
        {
            var categories = await _categoryRepository.GetByMasterCategories();
            var categoriesModel = _mapper.Map<List<CategoryModel>>(categories);

            return Ok(categoriesModel);

        }

        [HttpDelete]
        public async Task<IActionResult> UpdateCategory(int Id)
        {
            CustomHttpResponseMessage<PartyDTO> response = new CustomHttpResponseMessage<PartyDTO>();

            if (!HttpContext.User.DecideIfAdmin())
            {
                response.Success = false;
                response.ErrorMessage = "Yetkiniz yoktur.";
                return BadRequest(response.ErrorMessage);
            };

            await _categoryRepository.DeleteCategory(Id);

            return Ok();

        }
    }
}
