using MealCenter.Registration.Application.Contracts.Clients;
using MealCenter.Registration.Domain.Clients;

namespace MealCenter.Registration.Application.Interfaces
{
    public interface IClientAppService : IDisposable
    {
        Task<IEnumerable<Client>> GetAll();
        Task<Client> GetById(Guid id);
        Task Add(CreateClient client, string identityUserId, CancellationToken cancellationToken);
        Task Update(UpdateClient client, CancellationToken cancellationToken);
        Task Remove(Guid id, CancellationToken cancellationToken);
    }
}
