namespace API.Models.Request
{
    public class ItensPedidoRequest
    {
        public int Id { get; set; }
        public string NomeProduto { get; set; }
        public decimal Valor { get; set; }
        public int Qtd { get; set; }
    }
}
