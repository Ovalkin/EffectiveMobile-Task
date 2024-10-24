using EffectiveMobile.Common.EntityModel.Sqlite;
using EffectiveMobile.Common.EntityModel.Sqlite.Entities;
using EffectiveMobile.Repositories.Interfaces;

namespace EffectiveMobile.Repositories;

public class OrdersRepository(EffectiveMobileContext db) : IOrdersRepository
{
    public async Task<Order?> CreateAsync(Order order)
    {
        db.Add(order);
        int affected = await db.SaveChangesAsync();
        return affected == 1 ? order : null;
    }

    public Task<IEnumerable<Order?>> RetrieveAllAsync()
    {
        IEnumerable<Order?> orders = db.Orders.AsEnumerable();
        return Task.FromResult(orders);
    }
}