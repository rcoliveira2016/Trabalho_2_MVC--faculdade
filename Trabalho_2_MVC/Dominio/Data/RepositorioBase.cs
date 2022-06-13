using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Trabalho_2_MVC.Dominio.Context;
using Trabalho_2_MVC.Dominio.Entidades;
using Trabalho_2_MVC.Dominio.Interfaces.Context;
using Trabalho_2_MVC.Dominio.Interfaces.Data;

namespace Trabalho_2_MVC.Dominio.Data
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : Entidade
    {
        private IDbContext dbContext;
        protected DbSet<T> DbSet => dbContext.Set<T>();

        public RepositorioBase(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public T BuscarPorId(long id)
        {

            var produtos = DbSet.AsNoTracking().Where(p => p.Id == id);

            var prod = produtos.FirstOrDefault();

            return prod;
        }

        public void Salvar(T o)
        {
            if (o.RegistroNovo)
                Adiciona(o);
            else
                Alterar(o);
        }

        public void Adiciona(T o)
        {
            DbSet.Add(o);
            SaveChanges();
        }

        public void Alterar(T entidade)
        {
            var entry = dbContext.Entry(entidade);
            if (entry.State == EntityState.Detached)
                entry.State = EntityState.Modified;
            SaveChanges();
        }

        public virtual void Deletar(long id)
        {
            var item = DbSet.Find(id);
            dbContext.Entry(item).State = EntityState.Deleted;
            SaveChanges();
        }

        public List<T> ListaTodos()
        {
            var todos = DbSet.AsNoTracking().ToList();
            return todos;
        }

        protected void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        protected void ExcluirItemFilho<TFilho>(TFilho entidadeFilha) where TFilho : Entidade
        {
            dbContext.Entry(entidadeFilha).State = EntityState.Deleted;
        }

        public bool PossuiRegistro()
        {
            return DbSet.ToList().Any();
        }

        public void Dispose()
        {
            //dbContext?.Dispose();
        }
    }
}