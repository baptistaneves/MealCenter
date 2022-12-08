using AutoMapper;
using MealCenter.Registration.Application.Contracts.Clients;
using MealCenter.Registration.Application.Interfaces;
using MealCenter.Registration.Application.Validators.Clients;
using MealCenter.Registration.Domain.Clients;

namespace MealCenter.Registration.Application.Services
{
    public class ClientAppService : BaseService, IClientAppService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        public ClientAppService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task Add(CreateClient request, string identityUserId, CancellationToken cancellationToken)
        {
            if(!Validate(new CreateClientValidation(), request)) return;

            var newClient = new Client(identityUserId, request.FirstName, request.LastName, request.Status, request.ImageUrl, request.Description);

            _clientRepository.Add(newClient);

            await _clientRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _clientRepository.GetAll();
        }

        public async Task<Client> GetById(Guid id)
        {
            return await _clientRepository.GetById(id);
        }

        public async Task Remove(Guid id, CancellationToken cancellationToken)
        {
            _clientRepository.Remove(await _clientRepository.GetById(id));

            await _clientRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task Update(UpdateClient client, CancellationToken cancellationToken)
        {
            if (!Validate(new UpdateClientValidation(), client)) return;

            _clientRepository.Update(_mapper.Map<Client>(client));

            await _clientRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public void Dispose()
        {
            _clientRepository?.Dispose();
        }
    }
}
