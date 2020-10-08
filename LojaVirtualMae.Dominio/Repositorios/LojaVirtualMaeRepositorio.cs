using LojaVirtualMae.Dominio.DbContexto;
using LojaVirtualMae.Dominio.Entidades;
using LojaVirtualMae.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LojaVirtualMae.Dominio.Repositorios
{
    public class LojaVirtualRepositorio : ILojaVirtualMaeRepositorio
    {
        protected readonly LojaVirtualDbContexto _lojaContext;

        public LojaVirtualRepositorio(LojaVirtualDbContexto lojaVirtualContexto)
        {
            _lojaContext = lojaVirtualContexto;
            _lojaContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        private async Task<bool> SaveChangesAsync() => await _lojaContext.SaveChangesAsync() > 0;

        public async Task<bool> AdicionarAsync<T>(T entity) where T : class
        {
            await _lojaContext.AddAsync(entity);

            return await SaveChangesAsync();
        }

        public async Task<bool> AtualizarAsync<T>(T entity) where T : class
        {
            if (_lojaContext.Entry(entity).State == EntityState.Detached)
            {
                _lojaContext.Attach(entity);
                _lojaContext.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                _lojaContext.Update(entity);
            }

            return await SaveChangesAsync();
        }

        public async Task<bool> DeletarAsync<T>(T entity) where T : class
        {
            _lojaContext.Remove(entity);
            return await SaveChangesAsync();
        }
        public async Task<bool> DeletarRangeAsync<T>(T[] entityarray) where T : class
        {
            _lojaContext.RemoveRange(entityarray);
            return await SaveChangesAsync();
        }

        public async Task<List<T>> SelecionarTodosAsync<T>() where T : class 
            => await _lojaContext.Set<T>().ToListAsync();
        
    }
}
