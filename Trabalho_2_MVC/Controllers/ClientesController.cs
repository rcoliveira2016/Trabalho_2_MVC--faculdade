using System;
using System.Net;
using System.Web.Mvc;
using Trabalho_2_MVC.Dominio.Entidades;
using Trabalho_2_MVC.Dominio.Infra.Factory;
using Trabalho_2_MVC.Dominio.Interfaces.Data;

namespace Trabalho_2_MVC.Controllers
{
    public class ClientesController : CommonController
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = clienteRepositorio.BuscarPorId(id.Value);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }


        public ActionResult Cadastro()
        {
            return View("Cadastro", new Cliente() { DataNascimento = DateTime.Now});
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro([Bind(Include = "Id,Nome,CPF,DataNascimento,Endereco,Telefone")] Cliente cliente)
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = clienteRepositorio.BuscarPorId(id.Value);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View("Cadastro", cliente);
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
            return View(cliente);
        }


        public ActionResult Deletar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = clienteRepositorio.BuscarPorId(id.Value);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletarConfirmed(long id)
        {            
            clienteRepositorio.Deletar(id);
            return RedirectToAction("Index");
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
