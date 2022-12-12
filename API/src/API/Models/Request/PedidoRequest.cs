namespace API.Models.Request
{
    public class PedidoRequest
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public string Email { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Pago { get; set; }
        public decimal ValorTotal { get; set; }
        public List<ItensPedidoRequest> ItensPedido { get; set; }
    }
}
