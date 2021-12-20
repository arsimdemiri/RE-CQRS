using RealEstate.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Models.Common
{
    public class PagedModel<TModel>
    {
        private int _pageSize;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > Constants.MaxPageSize) ? Constants.MaxPageSize : value;
        }

        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public IList<TModel> Items { get; set; }

        public PagedModel()
        {
            Items = new List<TModel>();
        }
    }
}
