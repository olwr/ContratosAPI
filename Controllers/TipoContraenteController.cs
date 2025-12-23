using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContratosAPI.Data;
using ContratosAPI.DTOs.Common;
using ContratosAPI.Models;

namespace ContratosAPI.Controllers
{
    /// <summary>
    /// Controller para os tipos de contraente
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TipoContraenteController(ApplicationDbContext context) : ControllerBase
    {
        /// <summary>
        /// Retorna todos os tipos de contraente
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoContraenteDto>>> GetTipoContraentes()
        {
            var tipos = await context.TiposContraente
                .OrderBy(t => t.Nome)
                .Select(t => new TipoContraenteDto
                {
                    Id = t.Id,
                    Nome = t.Nome,
                    Descricao = t.Descricao,
                })
                .ToListAsync();
            return Ok(tipos);
        }

        /// <summary>
        /// Retorna um tipo de contraente pelo ID
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoContraenteDto>> GetTipoContraente(int id)
        {
            TipoContraente? tipo = await context.TiposContraente.FindAsync(id);
            if (tipo == null)
            {
                return NotFound();
            }

            return Ok(new TipoContraenteDto
            {
                Id = tipo.Id,
                Nome = tipo.Nome,
                Descricao = tipo.Descricao,
            });
        }
    }
}