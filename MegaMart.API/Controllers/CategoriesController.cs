using AutoMapper;
using MegaMart.API.Helper;
using MegaMart.Core.DTO;
using MegaMart.Core.Entities.Product;
using MegaMart.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MegaMart.API.Controllers
{
    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await _work.CategoryRepository.GetAllAsync();
                if (categories is null)
                {
                    return BadRequest(new ResponseAPI(400));
                }

                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var category = await _work.CategoryRepository.GetByIdAsync(id);

                if (category is null) return BadRequest(new ResponseAPI(400, $"Category id:{id} Not Found"));

                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(CategoryDTO categoryDto)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryDto);
                await _work.CategoryRepository.AddAsync(category);
                return Ok(new ResponseAPI(200, "Item Has Been Added Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateCategoryDTO categoryDto)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryDto);

                await _work.CategoryRepository.UpdateAsync(category);
                return Ok(new ResponseAPI(200, "Item Has Been Updated Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _work.CategoryRepository.DeleteAsync(id);
                return Ok(new ResponseAPI(200, "Item Has Been Deleted Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
