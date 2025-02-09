using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WepApiProjetoReact.Dto;
using WepApiProjetoReact.Models;
using WepApiProjetoReact.Services.Aluno;

namespace WepApiProjetoReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoInterface _alunoInterface;

        public AlunoController(IAlunoInterface alunoInterface)
        {
            _alunoInterface = alunoInterface;
        }

        [HttpPost("CriarAluno")]
        public async Task<ActionResult<AlunoModel>> AlunoCriacao(AlunoCriacaoDto alunoCriacaoDto)
        {
            var aluno = await _alunoInterface.AlunoCriacao(alunoCriacaoDto);
            return Ok(aluno);
        }

        [HttpPut("EditarAluno")]
        public async Task<ActionResult<AlunoModel>> AlunoEdicao(AlunoEdicaoDto alunoEdicaoDto)
        {
            var aluno = await _alunoInterface.AlunoEdicao(alunoEdicaoDto);
            return Ok(aluno);
        }

        [HttpDelete("ExcluirAluno")]
        public async Task<ActionResult<AlunoModel>> ExcluirAluno(int idAluno)
        {
            var aluno = await _alunoInterface.ExcluirAluno(idAluno);
            return Ok(aluno);
        }

        [HttpGet("BuscarAlunoPeloId")]
        public async Task<ActionResult<AlunoModel>> BuscarAlunoPorId(int idAluno)
        {
            var aluno = await _alunoInterface.BuscarAlunoPorId(idAluno);
            return Ok(aluno);
        }

        [HttpGet("ListaDosAlunos")]
        public async Task<ActionResult<AlunoModel>> ListarAlunos()
        {
            var aluno = await _alunoInterface.ListarAlunos();
            return Ok(aluno);
        }

        [HttpGet("BuscarAlunosPorNome")]
        public async Task<ActionResult<AlunoModel>> BuscarAlunosPorNome(string nomeAluno)
        {
            var aluno = await _alunoInterface.BuscarAlunoPorNome(nomeAluno);
            return Ok(aluno);
        }
    }
}
