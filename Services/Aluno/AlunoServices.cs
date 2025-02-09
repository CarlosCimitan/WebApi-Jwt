using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WepApiProjetoReact.Context;
using WepApiProjetoReact.Dto;
using WepApiProjetoReact.Models;

namespace WepApiProjetoReact.Services.Aluno
{
    public class AlunoServices : IAlunoInterface
    {
        private readonly AppDbContext _context;

        public AlunoServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<AlunoModel>>> AlunoCriacao(AlunoCriacaoDto alunoCriacaoDto)
        {
            ResponseModel<List<AlunoModel>> resposta = new ResponseModel<List<AlunoModel>>();
            try
            {
                var aluno = new AlunoModel()
                {
                    Nome = alunoCriacaoDto.Nome,
                    Email = alunoCriacaoDto.Email,
                    Idade = alunoCriacaoDto.Idade
                };

                _context.Add(aluno);
                await _context.SaveChangesAsync();

                resposta.dados = await _context.Alunos.ToListAsync();
                resposta.Mensagem = "Sucesso";

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<AlunoModel>>> AlunoEdicao(AlunoEdicaoDto alunoEdicaoDto)
        {
            ResponseModel<List<AlunoModel>> resposta = new ResponseModel<List<AlunoModel>>();
            try
            {
                var aluno = await _context.Alunos
                    .FirstOrDefaultAsync(alunoId => alunoId.Id == alunoEdicaoDto.Id);

                if (aluno == null)
                {
                    resposta.Mensagem = "Aluno nao encontrado";
                    return resposta;
                }

                aluno.Nome = alunoEdicaoDto.Nome;
                aluno.Email = alunoEdicaoDto.Email;
                aluno.Idade = alunoEdicaoDto.Idade;

                _context.Update(aluno);
                await _context.SaveChangesAsync();

                resposta.dados = await _context.Alunos.ToListAsync();
                resposta.Mensagem = "Sucesso";

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                return resposta;
            }
        }

        public async Task<ResponseModel<AlunoModel>> BuscarAlunoPorId(int idAluno)
        {
            ResponseModel<AlunoModel> resposta = new ResponseModel<AlunoModel>();

            try
            {
                var aluno = await _context.Alunos.FirstOrDefaultAsync(alunoId => alunoId.Id == idAluno);

                if (aluno == null)
                {
                    resposta.Mensagem = "Aluno nao encontrado";
                    return resposta;
                }

                resposta.dados = aluno;
                resposta.Mensagem = "Sucesso";

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<AlunoModel>>> BuscarAlunoPorNome(string nomeAluno)
        {
            ResponseModel<List<AlunoModel>> resposta = new ResponseModel<List<AlunoModel>>();
            try
            {
                var aluno = await _context.Alunos.Where(nome => nome.Nome.Contains(nomeAluno)).ToListAsync();

                if (aluno == null)
                {
                    resposta.Mensagem = "Aluno nao encontrado";
                    return resposta;
                }

                resposta.dados = aluno;
                resposta.Mensagem = "Sucesso";

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<AlunoModel>>> ExcluirAluno(int idAluno)
        {
            ResponseModel<List<AlunoModel>> resposta = new ResponseModel<List<AlunoModel>>();

            try
            {
                var aluno = await _context.Alunos.FirstOrDefaultAsync(alunoId => alunoId.Id == idAluno);

                if (aluno == null)
                {
                    resposta.Mensagem = "Aluno nao encontrado";
                    return resposta;
                }

                _context.Remove(aluno);
                await _context.SaveChangesAsync();

                resposta.dados = await _context.Alunos.ToListAsync();
                resposta.Mensagem = "Sucesso";

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<AlunoModel>>> ListarAlunos()
        {
            ResponseModel<List<AlunoModel>> resposta = new ResponseModel<List<AlunoModel>>();
            try
            {
                var alunos = await _context.Alunos.ToListAsync();

                resposta.dados = alunos;
                resposta.Mensagem = "Sucesso";

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                return resposta;
            }
        }
    }
}
