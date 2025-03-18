using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaMart.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MegaMart.Infrastructure.Data.Config
{
    public class PhotoConfiguration:IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.ImageByteArray).IsRequired();
        }
    }
}
