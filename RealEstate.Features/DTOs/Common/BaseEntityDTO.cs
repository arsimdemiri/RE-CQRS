using System;

namespace RealEstate.Features.DTOs.Common
{
    public class BaseEntityDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
