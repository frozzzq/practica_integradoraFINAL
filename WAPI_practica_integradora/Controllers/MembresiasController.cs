using DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WAPI_practica_integradora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembresiasController : ControllerBase
    {
        private readonly gym_context _context;
        public MembresiasController(gym_context context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Membresias membresia)
        {
            if (membresia == null)
                return BadRequest("Membresía vacía");

            _context.Membresias.Add(membresia);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Membresía registrada", membresia.id_membresia });
        }


       
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var membresia = await _context.Membresias.FindAsync(id);

            if (membresia == null)
                return NotFound(new { mensaje = "Membresía no encontrada" });

            _context.Membresias.Remove(membresia);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Membresía eliminada" });
        }




    }
}
