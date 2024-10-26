using EffectiveMobile.Common.EntityModel.Sqlite;
using EffectiveMobile.Common.EntityModel.Sqlite.Entities;

namespace EffectiveMobile.Tests.Integration.Helpers;

public class SeadDataHelper(EffectiveMobileContext context)
{
    public async Task Sead()
    {
        string[] cityDistricts =
        [
            "Ленинский",
            "Парковый",
            "Центральный",
            "Тракторозаводский",
            "Курчатовский"
        ];
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                context.Orders.Add(new Order
                {
                    Weight = 10,
                    CityDistrict = cityDistricts[i],
                    DeliveryTime = DateTime.Now.AddMinutes(j * 6)
                });
            }
        }
        await context.SaveChangesAsync();
    }

    public async Task ClearDb()
    {
        context.Orders.RemoveRange(context.Orders);
        await context.SaveChangesAsync();
    }
}