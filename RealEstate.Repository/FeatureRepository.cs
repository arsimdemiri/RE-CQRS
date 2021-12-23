using RealEstate.Data;
using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Repository
{
    public class FeatureRepository : GenericRepository<Feature>, IFeatureRepository
    {
        private readonly RealEstateDbContext _context;
        public FeatureRepository(RealEstateDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
