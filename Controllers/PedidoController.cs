using Microsoft.AspNetCore.Mvc;
using AromaGrao.Models;

namespace AromaGrao.Controllers
{
    /// <summary>
    /// Controller para gerenciar pedidos da cafeteria Aroma & Grão
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly Pedido _pedido;

        public PedidoController()
        {
            _pedido = new Pedido();
        }

        /// <summary>
        /// Calcula o total de um pedido
        /// </summary>
        /// <param name="request">Objeto com valor unitário e quantidade</param>
        /// <returns>Resposta com o total calculado e desconto aplicado</returns>
        [HttpPost("calcular")]
        public ActionResult<PedidoResponse> CalcularPedido([FromBody] PedidoRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest(new PedidoResponse 
                    { 
                        Sucesso = false, 
                        Mensagem = "Requisição inválida" 
                    });

                double total = _pedido.CalcularTotal(request.Valor, request.Quantidade);
                double totalComDesconto = _pedido.AplicarDesconto(total);
                double desconto = total - totalComDesconto;
                string status = _pedido.StatusPedido(total);

                return Ok(new PedidoResponse
                {
                    Total = total,
                    TotalComDesconto = totalComDesconto,
                    Desconto = desconto,
                    Status = status,
                    Sucesso = true,
                    Mensagem = "Pedido calculado com sucesso"
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new PedidoResponse
                {
                    Sucesso = false,
                    Mensagem = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new PedidoResponse
                {
                    Sucesso = false,
                    Mensagem = "Erro ao processar pedido: " + ex.Message
                });
            }
        }

        /// <summary>
        /// Gera um resumo completo do pedido
        /// </summary>
        [HttpPost("resumo")]
        public ActionResult<ResumoPedido> GerarResumoPedido([FromBody] PedidoRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest("Requisição inválida");

                var resumo = _pedido.GerarResumoPedido(request.Valor, request.Quantidade);
                return Ok(resumo);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao gerar resumo: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtém o status de um pedido baseado no valor total
        /// </summary>
        [HttpGet("status/{total}")]
        public ActionResult<string> ObterStatus(double total)
        {
            try
            {
                string status = _pedido.StatusPedido(total);
                return Ok(new { status });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao obter status: " + ex.Message);
            }
        }
    }
}
