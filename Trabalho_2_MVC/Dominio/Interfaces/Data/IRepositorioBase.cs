using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho_2_MVC.Dominio.Entidades;

namespace Trabalho_2_MVC.Dominio.Interfaces.Data
{
    public interface IRepositorioBase<T> where T : Entidade
    {
        void Adiciona(T o);
        void Alterar(T entidade);
        T BuscarPorId(long id);
        void Deletar(long id);
        void Dispose();
        List<T> ListaTodos();
        bool PossuiRegistro();
        void Salvar(T o);
    }
}
