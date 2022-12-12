using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Entities
{
    [Table("Produto")]
    public class Produto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [Column(Order = 1)]
        public string NomeProduto{ get; set; }

        [Column(Order = 2), DataType(DataType.Currency)]
        public decimal Valor { get; set; }
    }
}
