using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContratosAPI.Data;
using ContratosAPI.DTOs.Common;
using ContratosAPI.Models;

namespace ContratosAPI.Controllers
{
    /// <summary>
    /// Controller para estados brasileiros
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EstadosController(ApplicationDbContext context) : ControllerBase
    {
        /// <summary>
        /// Retorna todos os estados
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoDto>>> GetEstados()
        {
            var estados = await context.Estados
                .OrderBy(e => e.Nome)
                .Select(e => new EstadoDto
                {
                    Id = e.Id,
                    Sigla = e.Sigla,
                    Nome = e.Nome
                })
                .ToListAsync();
            return Ok(estados);
        }


        /// <summary>
        /// Retorna um estado pelo ID
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EstadoDto>> GetEstado(int id)
        {
            Estado? estado = await context.Estados.FindAsync(id);
            if (estado == null)
            {
                return NotFound();
            }

            return Ok(new EstadoDto
            {
                Id = estado.Id,
                Sigla = estado.Sigla,
                Nome = estado.Nome
            });
        }
    }
}