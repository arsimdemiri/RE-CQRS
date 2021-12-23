using RealEstate.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public class PropertyDocument : BaseEntity
    {
        public Guid PropertyId { get; set; }
        public Guid FileId { get; set; }

        public Property Property { get; set; }
        public File File { get; set; }
    }
}
