using Trabalho_2_MVC.Dominio.Entidades;

namespace Trabalho_2_MVC.Dominio.Interfaces.Data
{
    public interface IUsuariosRepositorio : IRepositorioBase<Usuario>
    {
        Usuario BuscarPorLogin(string login);
        bool ExisteLogin(string login);
    }
}
