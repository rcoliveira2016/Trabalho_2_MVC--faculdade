using System;
using System.Net;
using System.Web.Mvc;
using Trabalho_2_MVC.Dominio.Entidades;
using Trabalho_2_MVC.Dominio.Infra.Factory;
using Trabalho_2_MVC.Dominio.Interfaces.Data;

namespace Trabalho_2_MVC.Controllers
{
    public class ServicosController : CommonController
    {
        private readonly IServicosRepositorio serviosRepositorio;
        public ServicosController(IServicosRepositorio serviosRepositorio)
        {
            this.serviosRepositorio = serviosRepositorio;
        }

        public ActionResult Index()
        {
            return View(serviosRepositorio.ListaTodos());
        }


        public ActionResult Detalhes(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var servico = serviosRepositorio.BuscarPorId(id.Value);
            if (servico == null)
            {
                return HttpNotFound();
            }
            return View(servico);
        }


        public ActionResult Cadastro()
        {
            return View("Cadastro", new Servico());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro(Servico servico)
        {
            if (ModelState.IsValid)
            {
                serviosRepositorio.Adiciona(servico);
                return RedirectToAction("Index");
            }

            return View(servico);
        }


        public ActionResult Editar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = serviosRepositorio.BuscarPorId(id.Value);
            if (servico == null)
            {
                return HttpNotFound();
            }
            return View("Cadastro", servico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Servico servico)
        {
            if (ModelState.IsValid)
            {
                serviosRepositorio.Alterar(servico);
                return RedirectToAction("Index");
            }
            return View(servico);
        }


        public ActionResult Deletar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = serviosRepositorio.BuscarPorId(id.Value);
            if (servico == null)
            {
                return HttpNotFound();
            }
            return View(servico);
        }

        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletarConfirmed(long id)
        {
            serviosRepositorio.Deletar(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                serviosRepositorio.Dispose();
            }
        }

    }
}
