using Trabalho_2_MVC.Dominio.Entidades;
using Trabalho_2_MVC.Dominio.Interfaces.Context;
using Trabalho_2_MVC.Dominio.Interfaces.Data;

namespace Trabalho_2_MVC.Dominio.Data
{
    public class ClienteRepositorio : RepositorioBase<Cliente>, IClientesRepositorio
    {
        public ClienteRepositorio(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}