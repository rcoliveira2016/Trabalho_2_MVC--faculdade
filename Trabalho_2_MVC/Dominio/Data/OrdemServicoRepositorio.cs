using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Trabalho_2_MVC.Dominio.Entidades;
using Trabalho_2_MVC.Dominio.Interfaces.Context;
using Trabalho_2_MVC.Dominio.Interfaces.Data;

namespace Trabalho_2_MVC.Dominio.Data
{
    public class OrdemServicoRepositorio : RepositorioBase<OrdemServico>, IOrdensServicosRepositorio
    {
        public OrdemServicoRepositorio(IDbContext dbContext) : base(dbContext)
        {
        }

        public override void Deletar(long id)
        {
            var item = BuscarPorId(id);

            
            ExcluirItemFilho(item.Pagamento.FormaPagamento);
            ExcluirItemFilho(item.Pagamento);

            DbSet.Remove(item);
            SaveChanges();
        }

        public bool PossuiCliente(long id)
        {
            return ListaTodos().Any(x => x.IdCliente == id);
        }

        public bool PossuiServico(long id)
        {
            return ListaTodos().Any(x => x.IdServico == id);
        }

        public bool PossuiUsuario(long id)
        {
            return ListaTodos().Any(x => x.IdUsuario == id);
        }
    }
}