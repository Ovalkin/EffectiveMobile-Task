using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EffectiveMobile.Common.EntityModel.Sqlite.Entities;

[Table("orders")]
public class Order
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("weight")]
    public double Weight { get; set; }

    [Column("city_district")]
    public string CityDistrict { get; set; } = null!;
    
    [Column("delivery_time")]
    public DateTime DeliveryTime { get; set; }
}