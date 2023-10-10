using projeto_final_bloco_02.Models;

namespace projeto_final_bloco_02.Service
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> ListarTodosProdutos();

        Task<Produto?> BuscarPorId(long id);

        Task<IEnumerable<Produto>> BuscarPorNome(string nome);

        Task<Produto?> Criar(Produto produto);

        Task<Produto?> Atualizar(Produto produto);

        Task Deletar(Produto produto);
    }
}