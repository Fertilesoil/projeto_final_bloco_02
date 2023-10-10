using Microsoft.EntityFrameworkCore;
using projeto_final_bloco_02.Data;
using projeto_final_bloco_02.Models;

namespace projeto_final_bloco_02.Service.Implements
{
    public class ProdutoService : IProdutoService
    {
        private readonly FarmaciaDbContext _context;
        public ProdutoService(FarmaciaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> ListarTodosProdutos()
        {
            return await _context.Produtos.Include(c => c.Categoria).ToListAsync();
        }
        public async Task<Produto?> BuscarPorId(long id)
        {
            try
            {
                var Produto = await _context.Produtos
                .Include(c => c.Categoria)
                .FirstAsync(i => i.Id == id);
                return Produto;
            }
            catch
            {
                return null;
            }
        }
        public async Task<IEnumerable<Produto>> BuscarPorNome(string nome)
        {
            var Produto = await _context.Produtos
            .Include(c => c.Categoria)
            .Where(p => p.Nome.Contains(nome))
            .ToListAsync();

            return Produto;
        }
        public async Task<Produto?> Criar(Produto produto)
        {
            if (produto.Categoria is not null)
            {
                var BuscarCategoria = await _context.Categorias.FindAsync(produto.Categoria.Id);

                if (BuscarCategoria is null)
                    return null;
            }

            produto.Categoria = produto.Categoria is not null ? _context.Categorias
                .FirstOrDefault(c => c.Id == produto.Categoria.Id) : null;

            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();

            return produto;
        }
        public async Task<Produto?> Atualizar(Produto produto)
        {
            var ProdutoUpdate = await _context.Produtos.FindAsync(produto.Id);

            if (ProdutoUpdate is null)
                return null;

            if (produto.Categoria is not null)
            {
                var BuscarCategoria = await _context.Categorias.FindAsync(produto.Categoria.Id);

                if (BuscarCategoria is null)
                    return null;
            }

            produto.Categoria = produto.Categoria is not null ? _context.Categorias
                .FirstOrDefault(c => c.Id == produto.Categoria.Id) : null;

            _context.Entry(ProdutoUpdate).State = EntityState.Detached;
            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return produto;
        }
        public async Task Deletar(Produto produto)
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }
    }
}