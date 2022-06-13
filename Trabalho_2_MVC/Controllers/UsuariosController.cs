using System.Net;
using System.Web.Mvc;
using Trabalho_2_MVC.Dominio.Entidades;
using Trabalho_2_MVC.Dominio.Interfaces.Data;

namespace Trabalho_2_MVC.Controllers
{
    public class UsuariosController : CommonController
    {
        private readonly IUsuariosRepositorio usuariosRepositorio;

        public UsuariosController(IUsuariosRepositorio usuariosRepositorio)
        {
            this.usuariosRepositorio = usuariosRepositorio;
        }

        public ActionResult Index()
        {
            return View(usuariosRepositorio.ListaTodos());
        }


        public ActionResult Detalhes(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var usuario = usuariosRepositorio.BuscarPorId(id.Value);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }


        public ActionResult Cadastro()
        {
            return View("Cadastro", new Usuario());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuariosRepositorio.Adiciona(usuario);
                return RedirectToAction("Index");
            }

            return View(usuario);
        }


        public ActionResult Editar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = usuariosRepositorio.BuscarPorId(id.Value);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View("Cadastro", usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuariosRepositorio.Alterar(usuario);
                return RedirectToAction("Index");
            }
            return View(usuario);
        }


        public ActionResult Deletar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = usuariosRepositorio.BuscarPorId(id.Value);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletarConfirmed(long id)
        {
            usuariosRepositorio.Deletar(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                usuariosRepositorio.Dispose();
            }
        }

    }
}
