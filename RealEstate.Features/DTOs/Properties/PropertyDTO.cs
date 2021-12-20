using RealEstate.Features.DTOs.Common;
using RealEstate.Models.Enums;
using System;

namespace RealEstate.Features.DTOs.Properties
{
    public class PropertyDTO : BaseEntityDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public Guid PropertyTypeId { get; set; }
        public PropertySellType PropertySellType { get; set; }
    }
}
