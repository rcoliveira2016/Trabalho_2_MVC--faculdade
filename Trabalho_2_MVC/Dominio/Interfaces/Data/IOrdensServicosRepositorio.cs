using Trabalho_2_MVC.Dominio.Entidades;

namespace Trabalho_2_MVC.Dominio.Interfaces.Data
{
    public interface IOrdensServicosRepositorio : IRepositorioBase<OrdemServico>
    {
        bool PossuiCliente(long id);
        bool PossuiServico(long id);
        bool PossuiUsuario(long id);
    }
}
