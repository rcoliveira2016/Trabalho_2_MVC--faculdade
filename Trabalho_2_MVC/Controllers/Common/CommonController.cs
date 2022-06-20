using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Trabalho_2_MVC.Dominio.Entidades;
using Trabalho_2_MVC.Dominio.Interfaces.Data;

namespace Trabalho_2_MVC.Controllers
{
    public abstract class CommonController : Controller
    {


        protected ActionResult RetornarDetalhes<T>(IRepositorioBase<T> enidadeRepositorio, long? id) where T: Entidade
        {
            return RetornarEntidadeViewModel(enidadeRepositorio, id);
        }

        protected ActionResult RetornarEditar<T>(IRepositorioBase<T> enidadeRepositorio, long? id) where T : Entidade
        {
            return RetornarEntidadeViewModel(enidadeRepositorio, id, "Cadastro");
        }

        protected ActionResult RetornarDeletar<T>(IRepositorioBase<T> enidadeRepositorio, long? id) where T : Entidade
        {
            return RetornarEntidadeViewModel(enidadeRepositorio, id);
        }

        protected ActionResult RetornarEntidadeViewModel<T>(IRepositorioBase<T> enidadeRepositorio, long? id, string nomeView = null) where T : Entidade
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entidade = enidadeRepositorio.BuscarPorId(id.Value);
            if (entidade == null)
            {
                return HttpNotFound();
            }
            return nomeView==null? 
                View(entidade): 
                View(nomeView, entidade);
        }

        public ActionResult DeletarAposConfirmar<T>(IRepositorioBase<T> enidadeRepositorio, long id) where T : Entidade
        {
            var entidadeParaExcluir =  enidadeRepositorio.BuscarPorId(id);
            if (!ValidarExclusao(entidadeParaExcluir))
            {
                //return RedirectToAction(ConstsWeb.DescricaoAcaoDeletar, new { id = id });
                return View(ConstsWeb.DescricaoAcaoDeletar, entidadeParaExcluir);
            }
            enidadeRepositorio.Deletar(id);
            return RedirectToAction("Index");
        }

        protected void ValidarStateModel<T>(T entidade) where T : Entidade
        {
            if (!entidade.ValidarDados(out var erros))
            {
                erros.ForEach(e => ModelState.AddModelError(string.Empty, e));
            }
        }

        protected bool ValidarExclusao<T>(T entidade) where T : Entidade
        {
            if (!entidade.ValidarExclusao(out var erro))
            {
                ModelState.AddModelError(string.Empty, erro??"Item não pode ser excluído");
                return false;
            }
            return true;
        }
    }
}