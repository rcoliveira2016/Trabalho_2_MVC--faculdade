using System.Net;
using System.Web.Mvc;
using Trabalho_2_MVC.Dominio.Entidades;
using Trabalho_2_MVC.Dominio.Interfaces.Data;

namespace Trabalho_2_MVC.Controllers
{
    public class ServicosController : BaseController
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
            return RetornarDetalhes(serviosRepositorio, id);
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
            return RetornarEditar(serviosRepositorio, id);
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
            return RetornarDeletar(serviosRepositorio, id);
        }

        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletarConfirmed(long id)
        {
            return DeletarAposConfirmar(serviosRepositorio, id);
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
