using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaMart.Core.DTO;
using MegaMart.Core.Entities.Product;
using MegaMart.Core.Shared;

namespace MegaMart.Core.Interfaces
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<IEnumerable<ProductDTO>> GetAllAsync(GetAllProductParams productParams);
        Task<bool> AddAsync (AddProductDTO productDto);
        Task<bool> UpdateAsync (UpdateProductDTO productDto);
    }
}
