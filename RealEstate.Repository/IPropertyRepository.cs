using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Repository
{
    public interface IPropertyRepository : IGenericRepository<Property>
    {
        Task<PagedModel<Property>> PaginatedProperties(int page, int pageSize);
    }
}
