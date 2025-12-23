using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ContratosAPI.Data;
using ContratosAPI.Models;
using ContratosAPI.DTOs.Funcionario;

namespace ContratosAPI.Controllers
{
    /// <summary>
    /// Controller para gerenciamento de funcionários
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class FuncionariosController(
        ApplicationDbContext context,
        IMapper mapper) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;

        // ========== GET: api/funcionarios ==========
        /// <summary>
        /// Retorna todos os funcionários
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<FuncionarioListDto>>> GetFuncionarios(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;
            List<Funcionario> funcionarios = await context.Funcionarios
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            List<FuncionarioListDto>? funcionariosDto = _mapper.Map<List<FuncionarioListDto>>(funcionarios);
            int totalRecords = await context.Funcionarios.CountAsync();
            Response.Headers.Append("X-Total-Count", totalRecords.ToString());
            return Ok(funcionariosDto);
        }
        // ========== GET: api/funcionarios/5 ==========

        /// <summary>
        /// Retorna um funcionário específico pelo ID
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FuncionarioResponseDto>> GetFuncionario(int id)
        {
            Funcionario? funcionario = await context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound(new
                {
                    error = "Funcionário não encontrado",
                    message = $"Funcionário com ID {id:int} não existe"
                });
            }

            FuncionarioResponseDto? funcionarioDto = _mapper.Map<FuncionarioResponseDto>(funcionario);

            // Adicionar dados do estado
            Estado? estado = await context.Estados
                .FindAsync(funcionario.CidadeEstado.EstadoId);
            if (estado != null)
            {
                funcionarioDto.EstadoSigla = estado.Sigla;
                funcionarioDto.EstadoNome = estado.Nome;
            }

            // Adicionar contagem de contratos
            funcionarioDto.TotalContratos = await context.Contratos
                .CountAsync(c => c.TipoContraenteId == 2 &&
                                 c.ContraenteId == id);
            return Ok(funcionarioDto);
        }

        // ========== GET: api/funcionarios/cpf/12345678901 ==========

        /// <summary>
        /// Busca funcionário por CPF
        /// </summary>
        [HttpGet("cpf/{cpf}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FuncionarioResponseDto>> GetFuncionarioByCpf(
            string cpf)
        {
            Funcionario? funcionario = await context.Funcionarios
                .FirstOrDefaultAsync(f => f.CPF == cpf);
            if (funcionario == null)
            {
                return NotFound(new
                {
                    error = "Funcionário não encontrado",
                    message = $"Funcionário com CPF {cpf} não existe"
                });
            }

            FuncionarioResponseDto? funcionarioDto = _mapper.Map<FuncionarioResponseDto>(funcionario);
            return Ok(funcionarioDto);
        }

        // ========== POST: api/funcionarios ==========

        /// <summary>
        /// Cria um novo funcionário
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<FuncionarioResponseDto>> PostFuncionario(
            FuncionarioCreateDto funcionarioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verificar se CPF já existe
            bool cpfExiste = await context.Funcionarios
                .AnyAsync(f => f.CPF == funcionarioDto.CPF);
            if (cpfExiste)
            {
                return Conflict(new
                {
                    error = "CPF já cadastrado",
                    message = $"Já existe um funcionário com o CPF {funcionarioDto.CPF}"
                });
            }

            // Verificar idade mínima(14 anos - menor aprendiz)
            int idade = DateTime.Today.Year - funcionarioDto.DataNascimento.Year;
            if (funcionarioDto.DataNascimento.Date > DateTime.Today.AddYears(-idade))
                idade--;
            if (idade < 14)
            {
                return BadRequest(new
                {
                    error = "Idade inválida",
                    message = "O funcionário deve ter pelo menos 14 anos"
                });
            }

            Funcionario? funcionario = _mapper.Map<Funcionario>(funcionarioDto);
            context.Funcionarios.Add(funcionario);
            await context.SaveChangesAsync();
            FuncionarioResponseDto? responseDto = _mapper.Map<FuncionarioResponseDto>(funcionario);
            return CreatedAtAction(
                nameof(GetFuncionario),
                new { id = funcionario.Id },
                responseDto);
        }

        // ========== PUT: api/funcionarios/5 ==========

        /// <summary>
        /// Atualiza um funcionário existente
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutFuncionario(
            int id,
            FuncionarioPutDto funcionarioDto)
        {
            Funcionario? funcionario = await context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }

            // Verificar se está alterando CPF para um já existente
            if (funcionario.CPF != funcionarioDto.CPF)
            {
                bool cpfExiste = await context.Funcionarios
                    .AnyAsync(f => f.CPF == funcionarioDto.CPF && f.Id != id);
                if (cpfExiste)
                {
                    return Conflict(new
                    {
                        error = "CPF já cadastrado"
                    });
                }
            }

            _mapper.Map(funcionarioDto, funcionario);
            context.Entry(funcionario).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await FuncionarioExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // ========== DELETE: api/funcionarios/5 ==========

        /// <summary>
        /// Remove um funcionário
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteFuncionario(int id)
        {
            Funcionario? funcionario = await context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }

            // Verificar se existem contratos vinculados
            bool temContratos = await context.Contratos
                .AnyAsync(c => c.TipoContraenteId == 2 &&
                               c.ContraenteId == id);
            if (temContratos)
            {
                return Conflict(new
                {
                    error = "Funcionário possui contratos vinculados"
                });
            }

            context.Funcionarios.Remove(funcionario);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> FuncionarioExists(int id)
        {
            return await context.Funcionarios.AnyAsync(f => f.Id == id);
        }
    }
}