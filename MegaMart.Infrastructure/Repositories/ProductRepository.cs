using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MegaMart.Core.DTO;
using MegaMart.Core.Entities.Product;
using MegaMart.Core.Interfaces;
using MegaMart.Core.Services;
using MegaMart.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MegaMart.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IImageManagementService _imageManagementService;

        public ProductRepository(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _imageManagementService = imageManagementService;
        }

        public async Task<bool> AddAsync(AddProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            var imagesByteArray = await _imageManagementService.FormFileCollectionIntoPhotos(productDto.Photos, product.Id);

            await _context.Photos.AddRangeAsync(imagesByteArray);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAsync(UpdateProductDTO productDto)
        {

            // Find the existing product by ID
            var existProduct = await _context.Products
                .Include(product => product.Category)
                .Include(product => product.Photos)
                .FirstOrDefaultAsync(p => p.Id == productDto.Id);

            if (existProduct is null) return false;

            // Update fields using AutoMapper (excluding Photos)
            _mapper.Map(productDto, existProduct);

            // Handle photos update
            if (productDto.Photos.Any())
            {
                _context.Photos.RemoveRange(existProduct.Photos);

                // Convert new photos and add them
                var newPhotos = await _imageManagementService.FormFileCollectionIntoPhotos(productDto.Photos, productDto.Id);
                existProduct.Photos = newPhotos;
            }

            await _context.SaveChangesAsync();


            return true;
        }

    }
}
