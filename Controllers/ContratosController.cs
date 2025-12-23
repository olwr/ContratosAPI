using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ContratosAPI.Data;
using ContratosAPI.Models;
using ContratosAPI.DTOs.Contrato;

namespace ContratosAPI.Controllers
{
    /// <summary>
    /// Controller para gerenciamento de contratos
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ContratosController(
        ApplicationDbContext context,
        IMapper mapper) : ControllerBase
    {
        // ========== GET: api/contratos ==========

        /// <summary>
        /// Retorna todos os contratos
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ContratoListDto>>> GetContratos(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? statusId = null,
            [FromQuery] int? tipoId = null)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;
            var query = context.Contratos
                .Include(c => c.Contratante)
                .Include(c => c.TipoContrato)
                .Include(c => c.StatusContrato)
                .Include(c => c.TipoContraente)
                .AsQueryable();

            // Aplicar filtros
            if (statusId.HasValue)
            {
                query = query.Where(c => c.StatusContratoId == statusId.Value);
            }

            if(tipoId.HasValue)
            {
                query = query.Where(c => c.TipoContratoId == tipoId.Value);
            }

            var contratos = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var contratosDto = mapper.Map<List<ContratoListDto>>(contratos); // Carregar nomes dos contraentes
            foreach (ContratoListDto contratoDto in contratosDto)
            {
                Contrato contrato = contratos.First(c => c.Id == contratoDto.Id);
                contratoDto.ContraenteNome = await ObterNomeContraente(
                    contrato.ContraenteId,
                    contrato.TipoContraenteId);
            }

            int totalRecords = await query.CountAsync();
            Response.Headers.Append("X-Total-Count", totalRecords.ToString());
            return Ok(contratosDto);
        } 
        
        // ========== GET: api/contratos/5 ==========

        /// <summary>
        /// Retorna um contrato específico pelo ID
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContratoResponseDto>> GetContrato(int id)
        {
            Contrato? contrato = await context.Contratos
                .Include(c => c.Contratante)
                .Include(c => c.TipoContrato)
                .Include(c => c.StatusContrato)
                .Include(c => c.TipoContraente)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (contrato == null)
            {
                return NotFound(new
                {
                    error = "Contrato não encontrado",
                    message = $"Contrato com ID {id:int} não existe"
                });
            }

            ContratoResponseDto? contratoDto = mapper.Map<ContratoResponseDto>(contrato); 
            
            // Carregar dados do contraente
            contratoDto.ContraenteNome = await ObterNomeContraente(
                contrato.ContraenteId,
                contrato.TipoContraenteId);
            contratoDto.ContraenteDocumento = await ObterDocumentoContraente(
                contrato.ContraenteId,
                contrato.TipoContraenteId);
            return Ok(contratoDto);
        } 
        
        // ========== POST: api/contratos ==========

        /// <summary>
        /// Cria um novo contrato
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContratoResponseDto>> PostContrato(
            ContratoCreateDto contratoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } 
            
            // Validar se contratante existe
            bool contratanteExiste = await context.Empresas
                .AsNoTracking()
                .AnyAsync(e => e.Id == contratoDto.ContratanteId);
            if (!contratanteExiste)
            {
                return NotFound(new
                {
                    error = "Contratante não encontrado",
                    message = $"Empresa com ID {contratoDto.ContratanteId} não existe"
                });
            } 
            
            // Validar se contraente existe
            if (contratoDto.TipoContraenteId == 1) // Empresa
            {
                bool empresaExiste = await context.Empresas
                    .AsNoTracking()
                    .AnyAsync(e => e.Id == contratoDto.ContraenteId);
                
                if (!empresaExiste)
                {
                    return NotFound(new
                    {
                        error = "Contraente não encontrado",
                        message = $"Empresa com ID {contratoDto.ContraenteId} não existe"
                    });
                }
            }

            else if (contratoDto.TipoContraenteId == 2) // Funcionário
            {
                bool funcionarioExiste = await context.Funcionarios
                    .AsNoTracking()
                    .AnyAsync(f => f.Id == contratoDto.ContraenteId);
                
                if (!funcionarioExiste)
                {
                    return NotFound(new
                    {
                        error = "Contraente não encontrado",
                        message = $"Funcionário com ID {contratoDto.ContraenteId} não existe"
                    });
                }
            }

            Contrato? contrato = mapper.Map<Contrato>(contratoDto);
            context.Contratos.Add(contrato);
            await context.SaveChangesAsync();
            ContratoResponseDto? responseDto = mapper.Map<ContratoResponseDto>(contrato);
            return CreatedAtAction(
                nameof(GetContrato),
                new { id = contrato.Id },
                responseDto);
        } 
        
        // ========== PUT: api/contratos/5 ==========

        /// <summary>
        /// Atualiza um contrato existente
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutContrato(
            int id,
            ContratoPutDto contratoDto)
        {
            Contrato? contrato = await context.Contratos.FindAsync(id);
            if (contrato == null)
            {
                return NotFound();
            }

            mapper.Map(contratoDto, contrato);
            context.Entry(contrato).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!await ContratoExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        } 
        
        // ========== DELETE: api/contratos/5 ==========

        /// <summary>
        /// Remove um contrato
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteContrato(int id)
        {
            Contrato? contrato = await context.Contratos.FindAsync(id);
            if (contrato == null)
            {
                return NotFound();
            }

            context.Contratos.Remove(contrato);
            await context.SaveChangesAsync();
            return NoContent();
        } 
        
        // ========== MÉTODOS AUXILIARES ==========

        private async Task<string> ObterNomeContraente(
            int contraenteId,
            int tipoContraenteId)
        {
            if (tipoContraenteId == 1) // Empresa
            {
                Empresa? empresa = await context.Empresas.FindAsync(contraenteId);
                return empresa?.RazaoSocial ?? "Não encontrado";
            }

            else if (tipoContraenteId == 2) // Funcionário
            {
                Funcionario? funcionario = await context.Funcionarios
                    .FindAsync(contraenteId);
                return funcionario?.NomeCompleto ?? "Não encontrado";
            }

            return "Tipo desconhecido";
        }

        private async Task<string> ObterDocumentoContraente(
            int contraenteId,
            int tipoContraenteId)
        {
            if (tipoContraenteId == 1)
            {
                Empresa? empresa = await context.Empresas.FindAsync(contraenteId);
                return empresa?.CNPJ ?? "";
            }

            else if (tipoContraenteId == 2)
            {
                Funcionario? funcionario = await context.Funcionarios
                    .FindAsync(contraenteId);
                return funcionario?.CPF ?? "";
            }

            return "";
        }

        private async Task<bool> ContratoExists(int id)
        {
            return await context.Contratos.AsNoTracking().AnyAsync(c => c.Id == id);
        }
    }
}