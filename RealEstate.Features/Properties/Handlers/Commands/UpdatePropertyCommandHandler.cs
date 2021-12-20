using AutoMapper;
using MediatR;
using RealEstate.Features.Exceptions;
using RealEstate.Features.Properties.Requests.Commands;
using RealEstate.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Features.Properties.Handlers.Commands
{
    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePropertyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            var propertyToUpdate = await _unitOfWork.PropertyRepository.Get(request.PropertyToUpdate.Id);

            if (propertyToUpdate == null) 
                throw new NotFoundException(nameof(propertyToUpdate), request.PropertyToUpdate.Id);

            _mapper.Map(request.PropertyToUpdate, propertyToUpdate);

            await _unitOfWork.PropertyRepository.Update(propertyToUpdate);

            await _unitOfWork.Save();

            return true;
        }
    }
}
