using API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class Context : DbContext
    {
        public Context()
        {
        }
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Pedido> Pedido {get;set;}
        public DbSet<ItensPedido> ItensPedido { get; set; }
        public DbSet<Produto> Produto { get; set; }
    }
}
