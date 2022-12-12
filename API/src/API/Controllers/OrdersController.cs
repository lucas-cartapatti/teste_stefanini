using API.Models;
using API.Models.Entities;
using API.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromServices] Context context)
        {
            try
            {
                var listResult = new List<PedidoRequest>();
                var result = new PedidoRequest();
                var listItemPedidoRequest = new List<ItensPedidoRequest>();

                List<Pedido> listPedido = await context.Pedido.Include(x => x.ItensPedido).ToListAsync();
                foreach (Pedido pedido in listPedido)
                {
                    result = new PedidoRequest
                    {
                        Id = pedido.Id,
                        NomeCliente = pedido.NomeCliente,
                        Email = pedido.Email,
                        DataCriacao = pedido.DataCriacao,
                        Pago = pedido.Pago,
                        ValorTotal = pedido.ValorTotal,
                    };
                    foreach (ItensPedido itemPedido in pedido.ItensPedido)
                    {
                        itemPedido.Produto = await context.Produto.FindAsync(itemPedido.IdProduto);
                        listItemPedidoRequest.Add( new ItensPedidoRequest
                        {
                            Id = itemPedido.IdProduto,
                            NomeProduto = itemPedido.Produto.NomeProduto,
                            Valor = itemPedido.Produto.Valor,
                            Qtd = itemPedido.Quantidade
                        });

                    }
                    result.ItensPedido = listItemPedidoRequest;
                    listItemPedidoRequest = new List<ItensPedidoRequest>();
                    listResult.Add(result);
                }

                return Ok(listResult);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error : {ex.Message}");
                throw;
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PedidoRequest model, [FromServices] Context context)
        {
            try
            {
                List<ItensPedido> itensPedido = new List<ItensPedido>();
                Pedido pedido = new Pedido
                {
                    NomeCliente = model.NomeCliente,
                    Email = model.Email,
                    Pago = model.Pago,
                    ValorTotal = model.ValorTotal,
                    DataCriacao = DateTime.Now
                };

                context.Pedido.Add(pedido);
                foreach (var item in model.ItensPedido)
                {
                    if(item.Qtd >= 1)
                    {
                        itensPedido.Add(new ItensPedido
                        {
                            IdPedido = pedido.Id,
                            IdProduto = item.Id,
                            Quantidade = item.Qtd
                        });
                    }
                }
                context.ItensPedido.AddRange(itensPedido);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error : {ex.Message}");
                throw;
            }
        }

        // PUT api/values/
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]PedidoRequest model, [FromServices] Context context)
        {
            try
            {
                Pedido pedido = new Pedido
                {
                    Id = model.Id,
                    NomeCliente = model.NomeCliente,
                    Email = model.Email,
                    Pago = model.Pago,
                    ValorTotal = model.ValorTotal,
                    DataCriacao = DateTime.Now
                };

                var data = context.Pedido.Update(pedido);
                await context.SaveChangesAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error : {ex.Message}");
                throw;
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromServices] Context context)
        {
            try
            {
                var data = await context.Pedido.Include(x => x.ItensPedido).FirstOrDefaultAsync(x => x.Id == id);
                context.Remove(data);
                context.SaveChanges();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error : {ex.Message}");
                throw;
            }
        }
    }
}
