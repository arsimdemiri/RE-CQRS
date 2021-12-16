using RealEstate.Models.Common;
using RealEstate.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public class Property : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public Guid PropertyTypeId { get; set; }
        public PropertySellType PropertySellType { get; set; }
        public PropertyType PropertyType { get; set; }
        public List<PropertyFeatures> PropertyFeatures { get; set; }

    }
}
