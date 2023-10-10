using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projeto_final_bloco_02.Data;
using projeto_final_bloco_02.Models;

namespace projeto_final_bloco_02.Service.Implements
{
    public class CategoriaService : ICategoriaService
    {
        private readonly FarmaciaDbContext _context;

        public CategoriaService(FarmaciaDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Categoria>> ListarTodasCategorias()
        {
            return await _context.Categorias
            .Include(t => t.Produto)
            .ToListAsync();
        }
        public async Task<Categoria?> BuscarPorId(long id)
        {
            try
            {
                var Categoria = await _context.Categorias
                                .Include(t => t.Produto)
                                .FirstAsync(i => i.Id == id);

                return Categoria;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Categoria>> BuscarPorSetor(string setor)
        {
            var Setor = await _context.Categorias
                .Include(c => c.Produto)
                .Where(s => s.Setor.Contains(setor))
                .ToListAsync();

            return Setor;
        }
        public async Task<Categoria?> Criar(Categoria Categorias)
        {
            _context.Categorias.Add(Categorias);
            await _context.SaveChangesAsync();

            return Categorias;
        }
        public async Task<Categoria?> Atualizar(Categoria Categorias)
        {
            var CategoriaUpdate = await _context.Categorias.FindAsync(Categorias.Id);

            if (CategoriaUpdate is null)
                return null;

            _context.Entry(CategoriaUpdate).State = EntityState.Detached;
            _context.Entry(Categorias).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Categorias;
        }

        public async Task Deletar(Categoria Categorias)
        {
            _context.Remove(Categorias);
            await _context.SaveChangesAsync();
        }

    }
}