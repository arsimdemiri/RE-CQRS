using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IPropertyRepository PropertyRepository { get; }
        Task Save();
    }
}
