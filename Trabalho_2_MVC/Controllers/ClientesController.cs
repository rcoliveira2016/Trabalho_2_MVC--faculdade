using System;
using System.Net;
using System.Web.Mvc;
using Trabalho_2_MVC.Dominio.Entidades;
using Trabalho_2_MVC.Dominio.Interfaces.Data;

namespace Trabalho_2_MVC.Controllers
{
    public class ClientesController : BaseController
    {
        private readonly IClientesRepositorio clienteRepositorio;
        public ClientesController(IClientesRepositorio clienteRepositorio)
        {
            this.clienteRepositorio = clienteRepositorio;
        }
        public ActionResult Index()
        {
            return View(clienteRepositorio.ListaTodos());
        }


        public ActionResult Detalhes(long? id)
        {
            return RetornarDetalhes(clienteRepositorio, id);
        }


        public ActionResult Cadastro()
        {
            return View("Cadastro", new Cliente() { DataNascimento = DateTime.Now });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro( Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                clienteRepositorio.Adiciona(cliente);
                return RedirectToAction("Index");
            }

            return View(cliente);
        }


        public ActionResult Editar(long? id)
        {
            return RetornarEditar(clienteRepositorio, id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,Nome,CPF,DataNascimento,Endereco,Telefone")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                clienteRepositorio.Alterar(cliente);
                return RedirectToAction("Index");
            }
            return View("Cadastro", cliente);
        }


        public ActionResult Deletar(long? id)
        {
            return RetornarDeletar(clienteRepositorio, id);
        }

        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletarConfirmed(long id)
        {
            return DeletarAposConfirmar(clienteRepositorio, id);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                clienteRepositorio.Dispose();
            }
        }

    }
}
