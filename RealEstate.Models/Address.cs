using RealEstate.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public class Address: BaseEntity
    {
        public Guid PropertyId { get; set; }
        public string Longtitude { get; set; }
        public string Latitude { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
    }
}
