using EffectiveMobile.Common.EntityModel.Sqlite.Entities;

namespace EffectiveMobile.Repositories.Interfaces;

public interface IOrdersRepository
{
    Task<Order?> CreateAsync(Order order);
    Task<IEnumerable<Order?>> RetrieveAllAsync();
}