using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projeto_final_bloco_02.Models;

namespace projeto_final_bloco_02.Service
{
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> ListarTodasCategorias();

        Task<Categoria?> BuscarPorId(long id);

        Task<IEnumerable<Categoria>> BuscarPorSetor(string setor);

        Task<Categoria?> Criar(Categoria Categorias);

        Task<Categoria?> Atualizar(Categoria Categorias);

        Task Deletar(Categoria Categorias);
    }
}