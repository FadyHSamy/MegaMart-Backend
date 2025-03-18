using AutoMapper;
using MegaMart.API.Helper;
using MegaMart.Core.DTO;
using MegaMart.Core.Entities.Product;
using MegaMart.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MegaMart.API.Controllers
{
    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await _work.ProductRepository
                    .GetAllAsync(x => x.Category, x => x.Photos);

                var result = _mapper.Map<List<ProductDTO>>(products);

                if (products is null) return BadRequest(new ResponseAPI(400));

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await _work.ProductRepository.GetByIdAsync(id, x => x.Category, x => x.Photos);

                var result = _mapper.Map<ProductDTO>(product);

                if (result is null) return BadRequest(new ResponseAPI(400));

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }

        [HttpPost("Add"), DisableRequestSizeLimit]
        public async Task<IActionResult> Add([FromForm] AddProductDTO productDto)
        {
            try
            {
                var product = await _work.ProductRepository.AddAsync(productDto);

                return Ok(new ResponseAPI(200, "Product Added Successfully"));

            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromForm] UpdateProductDTO productDto)
        {
            try
            {
                var product = await _work.ProductRepository.UpdateAsync(productDto);

                return Ok(new ResponseAPI(200, "Product Updated Successfully"));

            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _work.ProductRepository.DeleteAsync(id);


                return Ok(new ResponseAPI(200, "Product Deleted Successfully"));

            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }

    }
}
