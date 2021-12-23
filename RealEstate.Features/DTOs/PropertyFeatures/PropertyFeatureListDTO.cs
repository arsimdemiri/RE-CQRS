using RealEstate.Features.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Features.DTOs.PropertyFeatures
{
    public class PropertyFeatureListDTO : BaseEntityDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
