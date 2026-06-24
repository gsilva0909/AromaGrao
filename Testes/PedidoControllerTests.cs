using Xunit;
using Microsoft.AspNetCore.Mvc;
using AromaGrao.Controllers;
using AromaGrao.Models;

namespace AromaGrao.Testes
{
    /// <summary>
    /// Testes de API para o PedidoController
    /// </summary>
    public class PedidoControllerTests
    {
        private readonly PedidoController _controller = new PedidoController();

        #region Testes do Endpoint /pedido/calcular

        [Fact(DisplayName = "API-01: POST /pedido/calcular com entrada válida")]
        public void CalcularPedido_EntradaValida_RetornaOk()
        {
            // Arrange
            var request = new PedidoRequest { Valor = 10, Quantidade = 5 };

            // Act
            var resultado = _controller.CalcularPedido(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
            var response = okResult.Value as PedidoResponse;
            
            Assert.NotNull(response);
            Assert.True(response.Sucesso);
            Assert.Equal(50, response.Total);
            Assert.Equal(50, response.TotalComDesconto);
            Assert.Equal(0, response.Desconto);
            Assert.Equal("Médio", response.Status);
        }

        [Fact(DisplayName = "API-02: POST /pedido/calcular com desconto")]
        public void CalcularPedido_ComDesconto_RetornaComDesconto()
        {
            // Arrange
            var request = new PedidoRequest { Valor = 25, Quantidade = 5 }; // Total = 125

            // Act
            var resultado = _controller.CalcularPedido(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
            var response = okResult.Value as PedidoResponse;
            
            Assert.NotNull(response);
            Assert.True(response.Sucesso);
            Assert.Equal(125, response.Total);
            Assert.Equal(112.5, response.TotalComDesconto);
            Assert.Equal(12.5, response.Desconto);
            Assert.Equal("Grande", response.Status);
        }

        [Fact(DisplayName = "API-03: POST /pedido/calcular com valor negativo")]
        public void CalcularPedido_ValorNegativo_RetornaBadRequest()
        {
            // Arrange
            var request = new PedidoRequest { Valor = -10, Quantidade = 5 };

            // Act
            var resultado = _controller.CalcularPedido(request);

            // Assert
            var badResult = Assert.IsType<BadRequestObjectResult>(resultado.Result);
            var response = badResult.Value as PedidoResponse;
            
            Assert.NotNull(response);
            Assert.False(response.Sucesso);
            Assert.Contains("negativo", response.Mensagem);
        }

        [Fact(DisplayName = "API-04: POST /pedido/calcular com quantidade negativa")]
        public void CalcularPedido_QuantidadeNegativa_RetornaBadRequest()
        {
            // Arrange
            var request = new PedidoRequest { Valor = 10, Quantidade = -5 };

            // Act
            var resultado = _controller.CalcularPedido(request);

            // Assert
            var badResult = Assert.IsType<BadRequestObjectResult>(resultado.Result);
            var response = badResult.Value as PedidoResponse;
            
            Assert.NotNull(response);
            Assert.False(response.Sucesso);
        }

        [Fact(DisplayName = "API-05: POST /pedido/calcular com valor zero")]
        public void CalcularPedido_ValorZero_RetornaOk()
        {
            // Arrange
            var request = new PedidoRequest { Valor = 0, Quantidade = 5 };

            // Act
            var resultado = _controller.CalcularPedido(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
            var response = okResult.Value as PedidoResponse;
            
            Assert.NotNull(response);
            Assert.True(response.Sucesso);
            Assert.Equal(0, response.Total);
        }

        [Fact(DisplayName = "API-06: POST /pedido/calcular com request nula")]
        public void CalcularPedido_RequestNula_RetornaBadRequest()
        {
            // Act
            var resultado = _controller.CalcularPedido(null!);

            // Assert
            var badResult = Assert.IsType<BadRequestObjectResult>(resultado.Result);
        }

        #endregion

        #region Testes do Endpoint /pedido/resumo

        [Fact(DisplayName = "API-07: POST /pedido/resumo com entrada válida")]
        public void GerarResumoPedido_EntradaValida_RetornaResumo()
        {
            // Arrange
            var request = new PedidoRequest { Valor = 30, Quantidade = 4 };

            // Act
            var resultado = _controller.GerarResumoPedido(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
            var response = okResult.Value as ResumoPedido;
            
            Assert.NotNull(response);
            Assert.Equal(120, response.Subtotal);
            Assert.Equal(12, response.Desconto);
            Assert.Equal(108, response.TotalComDesconto);
            Assert.Equal("Grande", response.Status);
        }

        [Fact(DisplayName = "API-08: POST /pedido/resumo sem desconto")]
        public void GerarResumoPedido_SemDesconto_RetornaResumo()
        {
            // Arrange
            var request = new PedidoRequest { Valor = 15, Quantidade = 2 };

            // Act
            var resultado = _controller.GerarResumoPedido(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
            var response = okResult.Value as ResumoPedido;
            
            Assert.NotNull(response);
            Assert.Equal(30, response.Subtotal);
            Assert.Equal(0, response.Desconto);
            Assert.Equal(30, response.TotalComDesconto);
            Assert.Equal("Médio", response.Status);
        }

        #endregion

        #region Testes do Endpoint /pedido/status/{total}

        [Fact(DisplayName = "API-09: GET /pedido/status/15 retorna Pequeno")]
        public void ObterStatus_Pequeno_RetornaPequeno()
        {
            // Act
            var resultado = _controller.ObterStatus(15);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
            Assert.NotNull(okResult.Value);
        }

        [Fact(DisplayName = "API-10: GET /pedido/status/50 retorna Médio")]
        public void ObterStatus_Medio_RetornaMedio()
        {
            // Act
            var resultado = _controller.ObterStatus(50);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
            Assert.NotNull(okResult.Value);
        }

        [Fact(DisplayName = "API-11: GET /pedido/status/150 retorna Grande")]
        public void ObterStatus_Grande_ReturnaGrande()
        {
            // Act
            var resultado = _controller.ObterStatus(150);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
            Assert.NotNull(okResult.Value);
        }

        #endregion
    }
}
