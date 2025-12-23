using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContratosAPI.Data;
using ContratosAPI.DTOs.Common;
using ContratosAPI.Models;

namespace ContratosAPI.Controllers
{
    /// <summary>
    /// Controller para os status de contrato
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class StatusContratoController(ApplicationDbContext context) : ControllerBase
    {
        /// <summary>
        /// Retorna todos os status de contrato
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusContratoDto>>> GetStatusContratos()
        {
            var status = await context.StatusContratos
                .OrderBy(s => s.Nome)
                .Select(s => new StatusContratoDto
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    Descricao = s.Descricao,
                })
                .ToListAsync();
            return Ok(status);
        }

        /// <summary>
        /// Retorna um status de contrato por ID
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoContraenteDto>> GetTipoContraente(int id)
        {
            StatusContrato? status = await context.StatusContratos.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }

            return Ok(new StatusContratoDto
            {
                Id = status.Id,
                Nome = status.Nome,
                Descricao = status.Descricao,
            });
        }
    }
}