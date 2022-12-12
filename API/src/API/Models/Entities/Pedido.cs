using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Entities
{
    [Table("Pedido")]
    public class Pedido
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Column(Order = 1)]
        public string NomeCliente { get; set; }
        [Column(Order = 2)]
        public string Email { get; set; }
        [Column(Order = 3)]
        public DateTime DataCriacao { get; set; }
        [Column(Order = 4)]
        public bool Pago { get; set; }
        [Column(Order = 5), DataType(DataType.Currency)]
        public decimal ValorTotal { get; set; }

        public List<ItensPedido> ItensPedido { get; set; }
    }
}
