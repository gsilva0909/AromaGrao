namespace AromaGrao.Models
{
    /// <summary>
    /// DTO para receber requisição de cálculo de pedido
    /// </summary>
    public class PedidoRequest
    {
        public double Valor { get; set; }
        public int Quantidade { get; set; }
    }

    /// <summary>
    /// DTO para resposta do cálculo de pedido
    /// </summary>
    public class PedidoResponse
    {
        public double Total { get; set; }
        public double TotalComDesconto { get; set; }
        public double Desconto { get; set; }
        public string? Status { get; set; }
        public bool Sucesso { get; set; }
        public string? Mensagem { get; set; }
    }
}
