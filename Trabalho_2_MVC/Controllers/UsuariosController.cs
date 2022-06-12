﻿using System;
using System.Net;
using System.Web.Mvc;
using Trabalho_2_MVC.Dominio.Entidades;
using Trabalho_2_MVC.Dominio.Infra.Factory;
using Trabalho_2_MVC.Dominio.Interfaces.Data;

namespace Trabalho_2_MVC.Controllers
{
    public class UsuariosController : CommonController
    {
        private readonly IUsuariosRepositorio serviosRepositorio = RepositorioFactory.CriarUsuarios();

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
            var usuario = serviosRepositorio.BuscarPorId(id.Value);
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
                serviosRepositorio.Adiciona(usuario);
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
            Usuario usuario = serviosRepositorio.BuscarPorId(id.Value);
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
                serviosRepositorio.Alterar(usuario);
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
            Usuario usuario = serviosRepositorio.BuscarPorId(id.Value);
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
