namespace AromaGrao.Models
{
    /// <summary>
    /// Classe que representa um pedido da cafeteria Aroma & Grão
    /// Contém lógica de cálculo de total, aplicação de desconto e classificação de pedidos
    /// </summary>
    public class Pedido
    {
        /// <summary>
        /// Calcula o total do pedido multiplicando o valor unitário pela quantidade
        /// </summary>
        /// <param name="valor">Valor unitário do produto (em reais)</param>
        /// <param name="quantidade">Quantidade de itens</param>
        /// <returns>Total do pedido</returns>
        /// <exception cref="ArgumentException">Lançado quando valor ou quantidade são negativos</exception>
        public double CalcularTotal(double valor, int quantidade)
        {
            if (valor < 0)
                throw new ArgumentException("O valor não pode ser negativo");
            
            if (quantidade < 0)
                throw new ArgumentException("A quantidade não pode ser negativa");

            return valor * quantidade;
        }

        /// <summary>
        /// Aplica desconto de 10% para totais >= 100
        /// </summary>
        /// <param name="total">Valor total do pedido</param>
        /// <returns>Total com desconto aplicado (se aplicável)</returns>
        public double AplicarDesconto(double total)
        {
            if (total >= 100)
                return total * 0.9;
            return total;
        }

        /// <summary>
        /// Classifica o pedido conforme o valor total
        /// </summary>
        /// <param name="total">Valor total do pedido</param>
        /// <returns>Classificação: "Pequeno" (< 20), "Médio" (>= 20 e < 100) ou "Grande" (>= 100)</returns>
        public string StatusPedido(double total)
        {
            if (total < 20)
                return "Pequeno";
            if (total < 100)
                return "Médio";
            return "Grande";
        }

        /// <summary>
        /// Calcula o total com desconto aplicado
        /// </summary>
        /// <param name="valor">Valor unitário do produto</param>
        /// <param name="quantidade">Quantidade de itens</param>
        /// <returns>Total com desconto aplicado</returns>
        public double CalcularTotalComDesconto(double valor, int quantidade)
        {
            double total = CalcularTotal(valor, quantidade);
            return AplicarDesconto(total);
        }

        /// <summary>
        /// Calcula o resumo completo do pedido
        /// </summary>
        public ResumoPedido GerarResumoPedido(double valor, int quantidade)
        {
            double total = CalcularTotal(valor, quantidade);
            double desconto = total >= 100 ? total * 0.1 : 0;
            double totalComDesconto = AplicarDesconto(total);
            string status = StatusPedido(total);

            return new ResumoPedido
            {
                ValorUnitario = valor,
                Quantidade = quantidade,
                Subtotal = total,
                Desconto = desconto,
                TotalComDesconto = totalComDesconto,
                Status = status
            };
        }
    }

    /// <summary>
    /// Classe que representa o resumo de um pedido
    /// </summary>
    public class ResumoPedido
    {
        public double ValorUnitario { get; set; }
        public int Quantidade { get; set; }
        public double Subtotal { get; set; }
        public double Desconto { get; set; }
        public double TotalComDesconto { get; set; }
        public string? Status { get; set; }
    }
}
