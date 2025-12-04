using DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WAPI_practica_integradora.Data;

namespace WAPI_practica_integradora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly gym_context _context;

        public ClienteController(gym_context context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Cliente cliente)
        {
            if (cliente == null)
                return BadRequest("Cliente vacío");

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Cliente guardado en BD", cliente.id });
        }
    }
}

