using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContratosAPI.Data;
using ContratosAPI.DTOs.Common;
using ContratosAPI.Models;

namespace ContratosAPI.Controllers
{
    /// <summary>
    /// Controller para os tipos de contrato
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TipoContratoController(ApplicationDbContext context) : ControllerBase
    {
        /// <summary>
        /// Retorna todos os tipos de contrato
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoContratoDto>>> GetTipoContratos()
        {
            var tipos = await context.TiposContrato
                .OrderBy(t => t.Id)
                .Select(t => new TipoContratoDto
                {
                    Id = t.Id,
                    Nome = t.Nome,
                    Descricao = t.Descricao
                })
                .ToListAsync();
            return Ok(tipos);;
        }

        /// <summary>
        /// Retorna um tipo de contrato pelo ID
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoContratoDto>> GetTipoContrato(int id)
        {
            TipoContrato? tipo = await context.TiposContrato.FindAsync(id);
            if (tipo == null)
            {
                return NotFound();
            }

            return Ok(new TipoContratoDto
            {
                Id = tipo.Id,
                Nome = tipo.Nome,
                Descricao = tipo.Descricao
            });
        }
    }
}