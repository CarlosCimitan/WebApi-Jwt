using WepApiProjetoReact.Dto;
using WepApiProjetoReact.Models;

namespace WepApiProjetoReact.Services.Aluno
{
    public interface IAlunoInterface
    {
        Task<ResponseModel<List<AlunoModel>>> AlunoCriacao(AlunoCriacaoDto alunoCriacaoDto);
        Task<ResponseModel<List<AlunoModel>>> AlunoEdicao(AlunoEdicaoDto alunoEdicaoDto);
        Task<ResponseModel<AlunoModel>> BuscarAlunoPorId(int idAluno);
        Task<ResponseModel<List<AlunoModel>>> ExcluirAluno(int idAluno);
        Task<ResponseModel<List<AlunoModel>>> ListarAlunos();
        Task<ResponseModel<List<AlunoModel>>> BuscarAlunoPorNome(string nomeAluno);
    }
}
