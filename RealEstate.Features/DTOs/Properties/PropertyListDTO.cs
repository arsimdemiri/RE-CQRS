using RealEstate.Features.DTOs.Common;
using RealEstate.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Features.DTOs.Properties
{
    public class PropertyListDTO : BaseEntityDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public Guid PropertyTypeId { get; set; }
        public PropertySellType PropertySellType { get; set; }
    }
}
