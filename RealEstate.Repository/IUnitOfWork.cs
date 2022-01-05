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
        IFeatureRepository FeatureRepository { get; }
        IRefreshTokenRepository RefreshTokenRepository { get; }
        Task Save();
    }
}
