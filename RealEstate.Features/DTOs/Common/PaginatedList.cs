using System.Collections.Generic;

namespace RealEstate.Features.DTOs.Common
{
    public class PaginatedList<T>
    {
        public int CurrentPage { get; init; }

        public int TotalItems { get; init; }

        public int TotalPages { get; init; }

        public List<T> Items { get; init; }
    }
}
