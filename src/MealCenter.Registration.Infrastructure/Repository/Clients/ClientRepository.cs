using MealCenter.Core.Data;
using MealCenter.Registration.Domain.Clients;
using MealCenter.Registration.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace MealCenter.Registration.Infrastructure.Repository.Clients
{
    public class ClientRepository : IClientRepository
    {
        private readonly RegistrationContext _context;

        public ClientRepository(RegistrationContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(Client entity)
        {
            _context.Clients.Add(entity);
        }

        public void Update(Client entity)
        {
            _context.Clients.Update(entity);
        }

        public void Remove(Client entity)
        {
            _context.Clients.Remove(entity);
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _context.Clients.AsNoTracking().ToListAsync();
        }

        public async Task<Client> GetById(Guid id)
        {
            return await _context.Clients.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

    }
}
