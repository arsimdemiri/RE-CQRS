using MediatR;
using RealEstate.Features.Exceptions;
using RealEstate.Features.Properties.Requests.Commands;
using RealEstate.Models;
using RealEstate.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Features.Properties.Handlers.Commands
{
    public class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePropertyCommandHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
        {
            var itemToDelete = await _unitOfWork.PropertyRepository.Get(request.PropertyId);

            if (itemToDelete != null)
                throw new NotFoundException(nameof(Property), request.PropertyId);

            await _unitOfWork.PropertyRepository.Delete(itemToDelete);
            await _unitOfWork.Save();

            return true;
        }
    }
}
