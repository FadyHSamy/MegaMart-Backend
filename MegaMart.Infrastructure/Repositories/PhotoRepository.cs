using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaMart.Core.Entities.Product;
using MegaMart.Core.Interfaces;
using MegaMart.Infrastructure.Data;

namespace MegaMart.Infrastructure.Repositories
{
    public class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
