using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaMart.Core.Entities.Product;
using MegaMart.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using static System.Net.WebRequestMethods;


namespace MegaMart.Infrastructure.Repositories.Services
{
    public class ImageManagementService : IImageManagementService
    {
        public ImageManagementService()
        {

        }
        public async Task<List<Photo>> FormFileCollectionIntoPhotos(IFormFileCollection files, int productId)
        {
            var list = new List<Photo>();
            foreach (var file in files)
            {
                var photo = new Photo()
                {
                    ProductId = productId,
                    ImageName = file.FileName,
                    ImageByteArray = await FormFileIntoByteArray(file)
                };
                list.Add(photo);
            }

            return list;
        }

        public async Task<List<byte[]>> FormFileCollectionIntoByteArrays(IFormFileCollection fileCollection)
        {
            var byteArrayList = new List<byte[]>();

            foreach (var file in fileCollection)
            {
                if (file.Length == 0) continue;

                using var ms = new MemoryStream();
                await file.CopyToAsync(ms);
                byteArrayList.Add(ms.ToArray());
            }
            return byteArrayList;
        }
        public async Task<byte[]> FormFileIntoByteArray(IFormFile file)
        {

            if (file.Length == 0) return null;

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var imageByteArray = ms.ToArray();

            return imageByteArray;
        }
    }
}
