using MealCenter.Registration.Application.Contracts.Clients;
using MealCenter.Registration.Domain.Clients;

namespace MealCenter.Registration.Application.Interfaces
{
    public interface IClientAppService : IDisposable
    {
        Task<IEnumerable<Client>> GetAll();
        Task<Client> GetById(Guid id);
        Task<Client> GetClientByIdentityId(string identityId);
        Task<int> GetTheNumberOfRegisteredClients();
        Task<Client> Add(CreateClient client, string identityUserId, CancellationToken cancellationToken);
        Task Update(Guid id, UpdateClient client, CancellationToken cancellationToken);
        Task Remove(Client Client, CancellationToken cancellationToken);
        Task ActivateClient(Guid id, CancellationToken cancellationToken);
        Task DeactivateClient(Guid id, CancellationToken cancellationToken);
    }
}
