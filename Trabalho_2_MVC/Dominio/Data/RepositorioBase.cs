using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Trabalho_2_MVC.Dominio.Context;
using Trabalho_2_MVC.Dominio.Entidades;
using Trabalho_2_MVC.Dominio.Infra.Singleton;

namespace Trabalho_2_MVC.Dominio.Data
{
    public class RepositorioBase<T> where T : Entidade
    {
        private BancoDadosContexto bd;
        protected DbSet<T> DbSet => bd.Set<T>();

        public RepositorioBase()
        {
            bd = BancoDadosContextoSingleton.BancoDados;
        }

        public T BuscarPorId(long id)
        {

            var produtos = DbSet.Where(p => p.Id == id);

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

        public void Alterar(T o)
        {
            bd.Entry(o).State = EntityState.Modified;
            SaveChanges();
        }

        public virtual void Deletar(long id)
        {
            var item = DbSet.Find(id);
            bd.Entry(item).State = EntityState.Deleted;
            SaveChanges();
        }

        public List<T> ListaTodos()
        {
            var todos = DbSet.ToList();
            return todos;
        }

        protected void SaveChanges()
        {
            bd.SaveChanges();
        }

        protected void ExcluirItemFilho<TFilho>(TFilho entidadeFilha) where TFilho : Entidade
        {
            bd.Entry(entidadeFilha).State = EntityState.Deleted;
        }

        public bool PossuiRegistro()
        {
            return DbSet.ToList().Any();
        }
    }
}