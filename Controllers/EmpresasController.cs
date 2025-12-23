using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ContratosAPI.Data;
using ContratosAPI.Models;
using ContratosAPI.DTOs.Empresa;

namespace ContratosAPI.Controllers
{
    /// <summary>
    /// Controller para gerenciamento de empresas
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class EmpresasController(
        ApplicationDbContext context,
        IMapper mapper) : ControllerBase
    {
        // ========== GET: api/empresas ==========

        /// <summary>
        /// Retorna todas as empresas
        /// </summary>
        /// <param name="pageNumber">Número da página (padrão: 1)</param>
        /// <param name="pageSize">Tamanho da página (padrão: 10)</param>
        /// <returns>Lista paginada de empresas</returns>
        /// <response code="200">Retorna a lista de empresas</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EmpresaListDto>>> GetEmpresas(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            // Validar parâmetros de paginação
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100; // Limite máximo
            var empresas = await context.Empresas
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var empresasDto = mapper.Map<List<EmpresaListDto>>(empresas);

            // Adicionar informações de paginação no header
            int totalRecords = await context.Empresas.CountAsync();
            Response.Headers.Append("X-Total-Count", totalRecords.ToString());
            Response.Headers.Append("X-Page-Number", pageNumber.ToString());
            Response.Headers.Append("X-Page-Size", pageSize.ToString());
            return Ok(empresasDto);
        }

        // ========== GET: api/empresas/5 ==========

        /// <summary>
        /// Retorna uma empresa específica pelo ID
        /// </summary>
        /// <param name="id">ID da empresa</param>/// <returns>Dados completos da empresa</returns>
        /// <response code="200">Retorna a empresa</response>
        /// <response code="404">Empresa não encontrada</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmpresaResponseDto>> GetEmpresa(int id)
        {
            Empresa? empresa = await context.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return NotFound(new
                {
                    error = "Empresa não encontrada",
                    message = $"Empresa com ID {id:int} não existe"
                });
            }

            // Buscar estado para incluir na resposta
            Estado? estado = await context.Estados
                .FindAsync(empresa.CidadeEstado.EstadoId);
            EmpresaResponseDto? empresaDto = mapper.Map<EmpresaResponseDto>(empresa);

            // Adicionar dados do estado
            if (estado != null)
            {
                empresaDto.EstadoSigla = estado.Sigla;
                empresaDto.EstadoNome = estado.Nome;
            }

            // Adicionar contagem de contratos(opcional)
            empresaDto.TotalContratos = await context.Contratos
                .CountAsync(c => c.ContratanteId == id);
            return Ok(empresaDto);
        }

        // ========== GET: api/empresas/cnpj/12345678901234 ==========

        /// <summary>
        /// Busca empresa por CNPJ
        /// </summary>
        /// <param name="cnpj">CNPJ da empresa (14 dígitos)</param>
        /// <returns>Dados da empresa</returns>
        /// <response code="200">Retorna a empresa</response>
        /// <response code="404">Empresa não encontrada</response>
        [HttpGet("cnpj/{cnpj}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmpresaResponseDto>> GetEmpresaByCnpj(string cnpj)
        {
            Empresa? empresa = await context.Empresas
                .FirstOrDefaultAsync(e => e.CNPJ == cnpj);
            if (empresa == null)
            {
                return NotFound(new
                {
                    error = "Empresa não encontrada",
                    message = $"Empresa com CNPJ {cnpj} não existe"
                });
            }

            EmpresaResponseDto? empresaDto = mapper.Map<EmpresaResponseDto>(empresa);
            return Ok(empresaDto);
        }

        // ========== POST: api/empresas ==========

        /// <summary>
        /// Cria uma nova empresa
        /// </summary>
        /// <param name="empresaDto">Dados da nova empresa</param>
        /// <returns>Empresa criada</returns>
        /// <response code="201">Empresa criada com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="409">CNPJ já cadastrado</response>[HttpPost]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<EmpresaResponseDto>> PostEmpresa(
            EmpresaCreateDto empresaDto)
        {
            // Validar se modelo é válido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verificar se CNPJ já existe
            bool cnpjExiste = await context.Empresas
                .AsNoTracking()
                .AnyAsync(e => e.CNPJ == empresaDto.CNPJ);
            if (cnpjExiste)
            {
                return Conflict(new
                {
                    error = "CNPJ já cadastrado",
                    message = $"Já existe uma empresa com o CNPJ {empresaDto.CNPJ}"
                });
            }

            // Verificar se estado existe
            bool estadoExiste = await context.Estados
                .AnyAsync(e => e.Id == empresaDto.CidadeEstado.EstadoId);
            if (!estadoExiste)
            {
                return BadRequest(new
                {
                    error = "Estado inválido",
                    message = "O estado informado não existe"
                });
            }

            // Mapear DTO para Entity
            Empresa? empresa = mapper.Map<Empresa>(empresaDto);

            // Adicionar ao contexto
            context.Empresas.Add(empresa);
            await context.SaveChangesAsync();

            // Mapear para Response DTO
            EmpresaResponseDto? responseDto = mapper.Map<EmpresaResponseDto>(empresa);

            // Retornar 201 Created com location header
            return CreatedAtAction(
                nameof(GetEmpresa),
                new { id = empresa.Id },
                responseDto);
        }

        // ========== PUT: api/empresas/5 ==========

        /// <summary>
        /// Atualiza uma empresa existente
        /// </summary>
        /// <param name="id">ID da empresa</param>
        /// <param name="empresaDto">Dados atualizados</param>
        /// <returns>No content</returns>
        /// <response code="204">Empresa atualizada com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="404">Empresa não encontrada</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutEmpresa(
            int id,
            EmpresaPutDto empresaDto)
        {
            // Buscar empresa existente
            Empresa? empresa = await context.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return NotFound(new
                {
                    error = "Empresa não encontrada",
                    message = $"Empresa com ID {id:int} não existe"
                });
            }

            // Verificar se está tentando alterar CNPJ para um já existente
            if (empresa.CNPJ != empresaDto.CNPJ)
            {
                bool cnpjExiste = await context.Empresas
                    .AsNoTracking()
                    .AnyAsync(e => e.CNPJ == empresaDto.CNPJ && e.Id != id);
                
                if (cnpjExiste)
                {
                    return Conflict(new
                    {
                        error = "CNPJ já cadastrado",
                        message = $"Já existe outra empresa com o CNPJ {empresaDto.CNPJ}"
                    });
                }
            }

            // Mapear DTO para Entity(atualizar propriedades)
            mapper.Map(empresaDto, empresa);

            // Marcar como modificado
            context.Entry(empresa).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EmpresaExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // ========== DELETE: api/empresas/5 ==========

        /// <summary>
        /// Remove uma empresa
        /// </summary>
        /// <param name="id">ID da empresa</param>
        /// <returns>No content</returns>
        /// <response code="204">Empresa removida com sucesso</response>
        /// <response code="404">Empresa não encontrada</response>
        /// <response code="409">Empresa possui contratos vinculados</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            Empresa? empresa = await context.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return NotFound(new
                {
                    error = "Empresa não encontrada",
                    message = $"Empresa com ID {id:int} não existe"
                });
            }

            // Verificar se existem contratos vinculados
            bool temContratos = await context.Contratos
                .AsNoTracking()
                .AnyAsync(c => c.ContratanteId == id ||
                               (c.TipoContraenteId == 1 && c.ContraenteId == id));
            if (temContratos)
            {
                int totalContratos = await context.Contratos
                    .AsNoTracking()
                    .CountAsync(c => c.ContratanteId == id);
                
                return Conflict(new
                {
                    error = "Empresa possui contratos",
                    message = $"Não é possível excluir. Existem {totalContratos} contrato(s) vinculado(s)."
                });
            }

            context.Empresas.Remove(empresa);
            await context.SaveChangesAsync();
            return NoContent();
        }


        // ========== MÉTODOS AUXILIARES ==========
        private async Task<bool> EmpresaExists(int id)
        {
            return await context.Empresas.AsNoTracking().AnyAsync(e => e.Id == id);
        }
    }
}