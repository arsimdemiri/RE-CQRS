using RealEstate.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public class PropertyFeatures : BaseEntity
    {
        public Guid FeatureId { get; set; }
        public Guid PropertyId { get; set; }

        public Feature Feature { get; set; }
        public Property Property { get; set; }
    }
}
