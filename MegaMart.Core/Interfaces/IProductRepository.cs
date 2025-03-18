using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaMart.Core.DTO;
using MegaMart.Core.Entities.Product;

namespace MegaMart.Core.Interfaces
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<bool> AddAsync (AddProductDTO productDto);
        Task<bool> UpdateAsync (UpdateProductDTO productDto);
    }
}
