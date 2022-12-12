using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Entities
{
    [Table("ItensPedido")]
    public class ItensPedido
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [ForeignKey("Pedido"), Column(Order = 1)]
        public int IdPedido { get; set; }

        [ForeignKey("Produto"), Column(Order = 2)]
        public int IdProduto { get; set; }

        [Column(Order = 3)]
        public int Quantidade { get; set; }

        public Pedido Pedido { get; set; }
        public Produto Produto { get; set; }
    }
}
