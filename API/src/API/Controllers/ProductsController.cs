using API.Models;
using API.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromServices] Context context)
        {
            try
            {
                List<Produto> data = await context.Produto.ToListAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error : {ex.Message}");
                throw;
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]List<Produto> produtos, [FromServices] Context context)
        {
            try
            {
                foreach(var produto in produtos)
                {
                    context.Produto.Add(produto);
                    await context.SaveChangesAsync();   
                }
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error : {ex.Message}");
                throw;
            }

        }
    }
}
