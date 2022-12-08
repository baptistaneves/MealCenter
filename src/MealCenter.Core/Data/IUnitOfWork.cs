namespace MealCenter.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> SaveAsync(CancellationToken cancellationToken);
    }
}
