using Xunit;
using Xunit.Abstractions;
using AromaGrao.Models;

namespace AromaGrao.Testes
{
    /// <summary>
    /// Testes de Caixa Branca para a classe Pedido
    /// Objetivo: Validar todos os caminhos independentes do código
    /// </summary>
    public class PedidoTestesCaixaBrancaTests
    {
        private readonly Pedido _pedido = new Pedido();
        private readonly ITestOutputHelper _output;

        public PedidoTestesCaixaBrancaTests(ITestOutputHelper output)
        {
            _output = output;
        }

        #region Testes do Método StatusPedido (Caixa Branca)

        [Fact(DisplayName = "TC-01: StatusPedido - Entrada pequena (15) retorna 'Pequeno'")]
        public void StatusPedido_EntradaPequena_RetornaPequeno()
        {
            // Arrange
            double total = 15;
            string esperado = "Pequeno";

            // Act
            string resultado = _pedido.StatusPedido(total);

            // Assert
            Assert.Equal(esperado, resultado);
            
            // Output
            _output.WriteLine($"✓ TC-01 PASSOU | Entrada: {total} | Esperado: '{esperado}' | Obtido: '{resultado}'");
        }

        [Fact(DisplayName = "TC-02: StatusPedido - Entrada média (50) retorna 'Médio'")]
        public void StatusPedido_EntradaMedia_RetornaMedio()
        {
            // Arrange
            double total = 50;
            string esperado = "Médio";

            // Act
            string resultado = _pedido.StatusPedido(total);

            // Assert
            Assert.Equal(esperado, resultado);
            
            // Output
            _output.WriteLine($"✓ TC-02 PASSOU | Entrada: {total} | Esperado: '{esperado}' | Obtido: '{resultado}'");
        }

        [Fact(DisplayName = "TC-03: StatusPedido - Entrada grande (150) retorna 'Grande'")]
        public void StatusPedido_EntradaGrande_ReturnaGrande()
        {
            // Arrange
            double total = 150;
            string esperado = "Grande";

            // Act
            string resultado = _pedido.StatusPedido(total);

            // Assert
            Assert.Equal(esperado, resultado);
            
            // Output
            _output.WriteLine($"✓ TC-03 PASSOU | Entrada: {total} | Esperado: '{esperado}' | Obtido: '{resultado}'");
        }

        #endregion

        #region Testes de Caixa-Preta (Valores Limite e Particionamento de Equivalência)

        [Fact(DisplayName = "TC-04: StatusPedido - Valor limite inferior (19.99) retorna 'Pequeno'")]
        public void StatusPedido_ValorLimiteInferior_RetornaPequeno()
        {
            // Arrange
            double total = 19.99;

            // Act
            string resultado = _pedido.StatusPedido(total);

            // Assert
            Assert.Equal("Pequeno", resultado);
            
            // Output
            _output.WriteLine($"✓ TC-04 PASSOU | Valor limite: {total} | Resultado: '{resultado}'");
        }

        [Fact(DisplayName = "TC-05: StatusPedido - Valor limite superior (20.00) retorna 'Médio'")]
        public void StatusPedido_ValorLimiteSuperior_RetornaMedio()
        {
            // Arrange
            double total = 20.00;

            // Act
            string resultado = _pedido.StatusPedido(total);

            // Assert
            Assert.Equal("Médio", resultado);
            
            // Output
            _output.WriteLine($"✓ TC-05 PASSOU | Valor limite: {total} | Resultado: '{resultado}'");
        }

        [Fact(DisplayName = "TC-06: StatusPedido - Valor limite (99.99) retorna 'Médio'")]
        public void StatusPedido_ValorLimite99_RetornaMedio()
        {
            // Arrange
            double total = 99.99;

            // Act
            string resultado = _pedido.StatusPedido(total);

            // Assert
            Assert.Equal("Médio", resultado);
        }

        [Fact(DisplayName = "TC-07: StatusPedido - Valor limite (100.00) retorna 'Grande'")]
        public void StatusPedido_ValorLimite100_ReturnaGrande()
        {
            // Arrange
            double total = 100.00;

            // Act
            string resultado = _pedido.StatusPedido(total);

            // Assert
            Assert.Equal("Grande", resultado);
        }

        #endregion

        #region Testes do Método CalcularTotal

        [Fact(DisplayName = "TC-08: CalcularTotal - Valor 10 x Quantidade 5 = 50")]
        public void CalcularTotal_Valor10Quantidade5_Retorna50()
        {
            // Arrange
            double valor = 10;
            int quantidade = 5;
            double esperado = 50;

            // Act
            double resultado = _pedido.CalcularTotal(valor, quantidade);

            // Assert
            Assert.Equal(esperado, resultado);
            
            // Output
            _output.WriteLine($"✓ TC-08 PASSOU | Cálculo: {valor} x {quantidade} = {resultado} (Esperado: {esperado})");
        }

        [Fact(DisplayName = "TC-09: CalcularTotal - Quantidade 0 retorna 0")]
        public void CalcularTotal_Quantidade0_Retorna0()
        {
            // Arrange
            double valor = 10;
            int quantidade = 0;
            double esperado = 0;

            // Act
            double resultado = _pedido.CalcularTotal(valor, quantidade);

            // Assert
            Assert.Equal(esperado, resultado);
        }

        #endregion

        #region Testes do Método AplicarDesconto

        [Fact(DisplayName = "TC-10: AplicarDesconto - Total 150 recebe desconto de 10%")]
        public void AplicarDesconto_Total150_Retorna135()
        {
            // Arrange
            double total = 150;
            double esperado = 135; // 150 * 0.9

            // Act
            double resultado = _pedido.AplicarDesconto(total);

            // Assert
            Assert.Equal(esperado, resultado);
            
            // Output
            _output.WriteLine($"✓ TC-10 PASSOU | Total: {total} | Desconto aplicado: 10% | Resultado: {resultado}");
        }

        [Fact(DisplayName = "TC-11: AplicarDesconto - Total 50 não recebe desconto")]
        public void AplicarDesconto_Total50_Retorna50()
        {
            // Arrange
            double total = 50;
            double esperado = 50;

            // Act
            double resultado = _pedido.AplicarDesconto(total);

            // Assert
            Assert.Equal(esperado, resultado);
        }

        [Fact(DisplayName = "TC-12: AplicarDesconto - Total 100 recebe desconto")]
        public void AplicarDesconto_Total100_Retorna90()
        {
            // Arrange
            double total = 100;
            double esperado = 90; // 100 * 0.9

            // Act
            double resultado = _pedido.AplicarDesconto(total);

            // Assert
            Assert.Equal(esperado, resultado);
        }

        #endregion
    }

    /// <summary>
    /// Testes Ad Hoc para validar entradas inválidas
    /// </summary>
    public class PedidoTestesAdHocTests
    {
        private readonly Pedido _pedido = new Pedido();
        private readonly ITestOutputHelper _output;

        public PedidoTestesAdHocTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact(DisplayName = "TC-13: Ad Hoc - Valor negativo (-10) lança exceção")]
        public void CalcularTotal_ValorNegativo_LancaExcecao()
        {
            // Arrange
            double valor = -10;
            int quantidade = 5;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _pedido.CalcularTotal(valor, quantidade));
            Assert.Contains("não pode ser negativo", exception.Message);
            
            // Output
            _output.WriteLine($"✓ TC-13 PASSOU | Valor negativo (-10) lançou exceção corretamente: {exception.Message}");
        }

        [Fact(DisplayName = "TC-14: Ad Hoc - Quantidade negativa (-5) lança exceção")]
        public void CalcularTotal_QuantidadeNegativa_LancaExcecao()
        {
            // Arrange
            double valor = 10;
            int quantidade = -5;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _pedido.CalcularTotal(valor, quantidade));
            Assert.Contains("não pode ser negativa", exception.Message);
        }

        [Fact(DisplayName = "TC-15: Ad Hoc - Quantidade muito grande (999999999) calcula corretamente")]
        public void CalcularTotal_QuantidadeGrande_CalculaCorretamente()
        {
            // Arrange
            double valor = 10.5;
            int quantidade = 999999999;

            // Act
            double resultado = _pedido.CalcularTotal(valor, quantidade);

            // Assert
            Assert.Equal(valor * quantidade, resultado);
        }

        [Fact(DisplayName = "TC-16: Ad Hoc - Valor zero funciona corretamente")]
        public void CalcularTotal_ValorZero_Retorna0()
        {
            // Arrange
            double valor = 0;
            int quantidade = 5;

            // Act
            double resultado = _pedido.CalcularTotal(valor, quantidade);

            // Assert
            Assert.Equal(0, resultado);
        }

        [Fact(DisplayName = "TC-17: Ad Hoc - Valor com muitas casas decimais")]
        public void CalcularTotal_ValorComDecimais_CalculaCorretamente()
        {
            // Arrange
            double valor = 9.99;
            int quantidade = 3;
            double esperado = 29.97;

            // Act
            double resultado = _pedido.CalcularTotal(valor, quantidade);

            // Assert
            Assert.Equal(esperado, resultado, 2);
        }
    }

    /// <summary>
    /// Testes de Regressão
    /// Verifica se mudanças na lógica quebram funcionalidades existentes
    /// </summary>
    public class PedidoTestesRegressaoTests
    {
        private readonly Pedido _pedido = new Pedido();
        private readonly ITestOutputHelper _output;

        public PedidoTestesRegressaoTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact(DisplayName = "TC-18: Regressão - Desconto continua funcionando após mudanças")]
        public void AplicarDesconto_Regressao_FuncionalidadeMantem()
        {
            // Arrange
            double[] totais = { 100, 150, 200, 250, 500 };

            // Act & Assert
            _output.WriteLine("═══ TC-18 - Teste de Regressão do Desconto ═══");
            foreach (double total in totais)
            {
                double resultado = _pedido.AplicarDesconto(total);
                double esperado = total * 0.9;
                Assert.Equal(esperado, resultado);
                _output.WriteLine($"  ✓ Total: {total:C} → Desconto: {total - resultado:C} → Resultado: {resultado:C}");
            }
            _output.WriteLine("═══ Regressão PASSOU ═══");
        }

        [Fact(DisplayName = "TC-19: Regressão - Status mantém comportamento com vários valores")]
        public void StatusPedido_Regressao_ComMultiplosValores()
        {
            // Arrange
            var testes = new[]
            {
                (total: 15.0, esperado: "Pequeno"),
                (total: 50.0, esperado: "Médio"),
                (total: 150.0, esperado: "Grande"),
                (total: 19.99, esperado: "Pequeno"),
                (total: 100.0, esperado: "Grande")
            };

            // Act & Assert
            foreach (var teste in testes)
            {
                string resultado = _pedido.StatusPedido(teste.total);
                Assert.Equal(teste.esperado, resultado);
            }
        }
    }

    /// <summary>
    /// Testes de Sanidade
    /// Verificam funcionalidades específicas após correções
    /// </summary>
    public class PedidoTestesSanidadeTests
    {
        private readonly Pedido _pedido = new Pedido();
        private readonly ITestOutputHelper _output;

        public PedidoTestesSanidadeTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact(DisplayName = "TC-20: Sanidade - Desconto é aplicado corretamente")]
        public void ValidarDesconto_AplicadoCorretamente()
        {
            // Arrange
            double total = 150;
            double esperado = 135; // 10% de desconto

            // Act
            double resultado = _pedido.AplicarDesconto(total);

            // Assert
            Assert.Equal(esperado, resultado);
        }

        [Fact(DisplayName = "TC-21: Sanidade - Cálculo total com múltiplas operações")]
        public void ValidarCalculoTotal_ComDecimaisComplexos()
        {
            // Arrange
            double valor = 12.50;
            int quantidade = 4;
            double esperado = 50.00;

            // Act
            double resultado = _pedido.CalcularTotal(valor, quantidade);

            // Assert
            Assert.Equal(esperado, resultado);
        }
    }

    /// <summary>
    /// Testes de Fumaça
    /// Validam rapidamente as funcionalidades principais
    /// </summary>
    public class PedidoTestesFumaçaTests
    {
        private readonly Pedido _pedido = new Pedido();
        private readonly ITestOutputHelper _output;

        public PedidoTestesFumaçaTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact(DisplayName = "TC-22: Fumaça - Funcionalidades principais funcionam")]
        public void FuncionalidadesPrincipais_Trabalham()
        {
            // Arrange
            double valor = 25;
            int quantidade = 5;

            // Act
            double total = _pedido.CalcularTotal(valor, quantidade);
            double totalComDesconto = _pedido.AplicarDesconto(total);
            string status = _pedido.StatusPedido(total);

            // Assert - Deve completar sem exceções
            Assert.Equal(125, total);
            Assert.Equal(112.5, totalComDesconto);
            Assert.Equal("Grande", status);
            
            // Output
            _output.WriteLine("═══ TC-22 - Teste de Fumaça ═══");
            _output.WriteLine($"  ✓ Cálculo: {valor} x {quantidade} = {total}");
            _output.WriteLine($"  ✓ Com desconto: {totalComDesconto}");
            _output.WriteLine($"  ✓ Status: {status}");
            _output.WriteLine("═══ Fumaça PASSOU ═══");
        }
    }

    /// <summary>
    /// Testes de Integração - Método GerarResumoPedido
    /// </summary>
    public class PedidoTestesIntegracaoTests
    {
        private readonly Pedido _pedido = new Pedido();
        private readonly ITestOutputHelper _output;

        public PedidoTestesIntegracaoTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact(DisplayName = "TC-23: Integração - Resumo do pedido com desconto")]
        public void GerarResumoPedido_ComDesconto()
        {
            // Arrange
            double valor = 30;
            int quantidade = 4; // Total = 120

            // Act
            var resumo = _pedido.GerarResumoPedido(valor, quantidade);

            // Assert
            Assert.Equal(120, resumo.Subtotal);
            Assert.Equal(12, resumo.Desconto); // 10% de desconto
            Assert.Equal(108, resumo.TotalComDesconto);
            Assert.Equal("Grande", resumo.Status);
            
            // Output
            _output.WriteLine("═══ TC-23 - Integração com Desconto ═══");
            _output.WriteLine($"  ✓ Subtotal: {resumo.Subtotal:C}");
            _output.WriteLine($"  ✓ Desconto (10%): {resumo.Desconto:C}");
            _output.WriteLine($"  ✓ Total com desconto: {resumo.TotalComDesconto:C}");
            _output.WriteLine($"  ✓ Status: {resumo.Status}");
            _output.WriteLine("═══ Integração PASSOU ═══");
        }

        [Fact(DisplayName = "TC-24: Integração - Resumo do pedido sem desconto")]
        public void GerarResumoPedido_SemDesconto()
        {
            // Arrange
            double valor = 15;
            int quantidade = 2; // Total = 30

            // Act
            var resumo = _pedido.GerarResumoPedido(valor, quantidade);

            // Assert
            Assert.Equal(30, resumo.Subtotal);
            Assert.Equal(0, resumo.Desconto);
            Assert.Equal(30, resumo.TotalComDesconto);
            Assert.Equal("Médio", resumo.Status);
            
            // Output
            _output.WriteLine("═══ TC-24 - Integração sem Desconto ═══");
            _output.WriteLine($"  ✓ Subtotal: {resumo.Subtotal:C}");
            _output.WriteLine($"  ✓ Desconto: {resumo.Desconto:C}");
            _output.WriteLine($"  ✓ Total: {resumo.TotalComDesconto:C}");
            _output.WriteLine($"  ✓ Status: {resumo.Status}");
            _output.WriteLine("═══ Integração PASSOU ═══");
        }
    }
}
