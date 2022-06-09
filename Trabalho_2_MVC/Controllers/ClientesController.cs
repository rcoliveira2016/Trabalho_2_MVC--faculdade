using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Trabalho_2_MVC.Dominio.Context;
using Trabalho_2_MVC.Dominio.Entidades;
using static Trabalho_2_MVC.Dominio.Infra.RepositorioSingleton;
namespace Trabalho_2_MVC.Controllers
{
    public class ClientesController : Controller
    {
        
        public ActionResult Index()
        {
            return View(ClienteRepositorio.ListaTodos());
        }

        
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = ClienteRepositorio.BuscarPorId(id.Value);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,CPF,DataNascimento,Endereco,Telefone")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                ClienteRepositorio.Adiciona(cliente);
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = ClienteRepositorio.BuscarPorId(id.Value);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,CPF,DataNascimento,Endereco,Telefone")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                ClienteRepositorio.Alterar(cliente);
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = ClienteRepositorio.BuscarPorId(id.Value);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {            
            ClienteRepositorio.Deletar(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
