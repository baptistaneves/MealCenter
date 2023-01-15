using MealCenter.Core.Data;

namespace MealCenter.Registration.Domain.Clients
{
    public interface IClientRepository : IRepository<Client> 
    {
        Task<IEnumerable<Client>> GetAll();
        Task<Client> GetById(Guid id);
        Task<Client> GetClientByIdentityId(string identityId);
        Task<int> GetTheNumberOfRegisteredClients();
        void Add(Client entity);
        void Update(Client entity);
        void Remove(Client entity);
    }
}
