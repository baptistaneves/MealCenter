using AutoMapper;
using MealCenter.Core.Communication.Mediator;
using MealCenter.Core.Messages.CommonMessages.Notifications;
using MealCenter.Registration.Application.Contracts.Clients;
using MealCenter.Registration.Application.ErrorMessages;
using MealCenter.Registration.Application.Interfaces;
using MealCenter.Registration.Application.Validators.Clients;
using MealCenter.Registration.Domain.Clients;

namespace MealCenter.Registration.Application.Services
{
    public class ClientAppService : BaseService, IClientAppService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;
        public ClientAppService(IClientRepository clientRepository, IMapper mapper, IMediatorHandler mediatorHandler)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<Client> Add(CreateClient request, string identityUserId, CancellationToken cancellationToken)
        {
            if(!Validate(new CreateClientValidation(), request)) return null;

            var newClient = new Client(identityUserId, request.FirstName, request.LastName, request.Status, request.ImageUrl, request.Description, request.Phone, request.EmailAddress);

            _clientRepository.Add(newClient);

            await _clientRepository.UnitOfWork.SaveAsync(cancellationToken);
            return newClient;
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _clientRepository.GetAll();
        }

        public async Task<Client> GetById(Guid id)
        {
            return await _clientRepository.GetById(id);
        }

        public async Task<Client> GetClientByIdentityId(string identityId)
        {
            var client = await _clientRepository.GetClientByIdentityId(identityId);
            if(client == null)
            {
                _mediatorHandler.PublishNotification(new DomainNotification("Restaurant", "There is no Client data for this identity"));
                return null;
            }

            return client;
        }

        public async Task<int> GetTheNumberOfRegisteredClients()
        {
            return await _clientRepository.GetTheNumberOfRegisteredClients();
        }

        public async Task Remove(Client client, CancellationToken cancellationToken)
        {
            _clientRepository.Remove(client);

            await _clientRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task Update(Guid id, UpdateClient clientUpdated, CancellationToken cancellationToken)
        {
            if (!Validate(new UpdateClientValidation(), clientUpdated)) return;

            var client = await _clientRepository.GetById(id);

            if (client == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("client", ClientErrorMessages.ClientNotFound));
                return;
            }

            client.UpdateClient(clientUpdated.FirstName, clientUpdated.LastName, clientUpdated.Description, clientUpdated.Phone);

            _clientRepository.Update(client);

            await _clientRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task ActivateClient(Guid id, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetById(id);
            if (client == null) 
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("client", ClientErrorMessages.ClientNotFound));
                return; 
            }

            client.Activate();

            _clientRepository.Update(client);
            await _clientRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task DeactivateClient(Guid id, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetById(id);
            if (client == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("client", ClientErrorMessages.ClientNotFound));
                return;
            }

            client.Deactivate();

            _clientRepository.Update(client);
            await _clientRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public void Dispose()
        {
            _clientRepository?.Dispose();
        }
    }
}
