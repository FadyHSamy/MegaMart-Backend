using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaMart.Core.Entities.Product;
using Microsoft.AspNetCore.Http;

namespace MegaMart.Core.Services
{
    public interface IImageManagementService
    {
        Task<List<Photo>> FormFileCollectionIntoPhotos(IFormFileCollection file, int productId);
        Task<List<byte[]>> FormFileCollectionIntoByteArrays(IFormFileCollection fileCollection);
        Task<byte[]> FormFileIntoByteArray(IFormFile file);
    }
}
