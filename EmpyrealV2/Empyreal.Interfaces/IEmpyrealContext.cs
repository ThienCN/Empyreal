using Empyreal.Models;
using Microsoft.EntityFrameworkCore;

namespace Empyreal.Entities
{
    public interface IEmpyrealContext
    {
        DbSet<Cart> Cart { get; set; }
        DbSet<CartDetail> CartDetail { get; set; }
        DbSet<Catalog> Catalog { get; set; }
        DbSet<Image> Image { get; set; }
        DbSet<Order> Order { get; set; }
        DbSet<OrderDetail> OrderDetail { get; set; }
        DbSet<Product> Product { get; set; }
        DbSet<ProductDetail> ProductDetail { get; set; }
        DbSet<ProductPrice> ProductPrice { get; set; }
        DbSet<ProductType> ProductType { get; set; }
        DbSet<Rate> Rate { get; set; }
    }
}