using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Models.Common;
using RealEstate.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Repository
{
    public class PropertyRepository : GenericRepository<Property>, IPropertyRepository
    {
        private readonly RealEstateDbContext _context;
        public PropertyRepository(RealEstateDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PagedModel<Property>> PaginatedProperties(int page, int pageSize)
        {
            return await this._context.Properties.AsQueryable().PaginateAsync(page, pageSize);
        }
    }
}
