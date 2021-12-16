using System.Collections.Generic;

namespace RealEstate.Features.DTOs.Properties
{
    public class PropertyPaginatedList
    {
        public int CurrentPage { get; init; }

        public int TotalItems { get; init; }

        public int TotalPages { get; init; }

        public List<CreatePropertyDTO> Items { get; init; }
    }
}
