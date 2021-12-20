using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Data.Configurations
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired();
            builder.Property(x => x.Code)
                .IsRequired();
            builder.Property(x => x.PropertyTypeId)
                .IsRequired();
            builder.Property(x => x.PropertySellType)
                .IsRequired();

        }
    }
}
