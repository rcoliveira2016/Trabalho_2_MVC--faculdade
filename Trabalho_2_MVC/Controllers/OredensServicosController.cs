using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Trabalho_2_MVC.Dominio.Entidades;
using Trabalho_2_MVC.Dominio.Infra.Extensoes;
using Trabalho_2_MVC.Dominio.Interfaces.Data;
using Trabalho_2_MVC.ViewModels;

namespace Trabalho_2_MVC.Controllers
{

    public class OredensServicosController : CommonController
    {
        private readonly IOrdensServicosRepositorio ordensServicosRepositorio;
        private readonly IClientesRepositorio clienteRepositorio;
        private readonly IServicosRepositorio serviosRepositorio;
        private readonly IUsuariosRepositorio usuariosRepositorio;

        public OredensServicosController(
            IOrdensServicosRepositorio ordensServicosRepositorio,
            IClientesRepositorio clienteRepositorio,
            IServicosRepositorio serviosRepositorio,
            IUsuariosRepositorio usuariosRepositorio)
        {
            this.ordensServicosRepositorio = ordensServicosRepositorio;
            this.clienteRepositorio = clienteRepositorio;
            this.serviosRepositorio = serviosRepositorio;
            this.usuariosRepositorio = usuariosRepositorio;
        }

        public ActionResult Index()
        {
            var ordemServicoes = ordensServicosRepositorio.ListaTodos();
            return View(ordemServicoes);
        }

        public ActionResult Detalhes(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdemServico ordemServico = ordensServicosRepositorio.BuscarPorId(id.Value);
            if (ordemServico == null)
            {
                return HttpNotFound();
            }
            return View(ordemServico);
        }

        public ActionResult Cadastro()
        {
            MontarViewBagCombos();
            return View(MontarOrdemServicoViewModel(OrdemServico.Criar()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro(OrdemServicoViewModel ordemServicoViewModel)
        {
            var ordemServico = MontarEntidadeOrdemServico(ordemServicoViewModel);
            ordemServico.SetarValorParaSalvar();
            ValidarStateModel(ordemServico);
            if (ModelState.IsValid)
            {
                ordensServicosRepositorio.Adiciona(ordemServico);
                return RedirectToAction("Index");
            }

            MontarViewBagCombos(ordemServico.IdCliente, ordemServico.IdServico, ordemServico.IdUsuario);
            return View(MontarOrdemServicoViewModel(ordemServico));
        }

        public ActionResult Editar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdemServico ordemServico = ordensServicosRepositorio.BuscarPorId(id.Value);
            if (ordemServico == null)
            {
                return HttpNotFound();
            }
            MontarViewBagCombos(ordemServico.IdCliente, ordemServico.IdServico, ordemServico.IdUsuario, ordemServico.Pagamento.FormaPagamento.Tipo);
            return View("Cadastro", MontarOrdemServicoViewModel(ordemServico));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(OrdemServicoViewModel ordemServicoViewModel)
        {
            var ordemServico = MontarEntidadeOrdemServico(ordemServicoViewModel);
            ordemServico.SetarValorParaEditar(ordensServicosRepositorio);       
            ValidarStateModel(ordemServico);
            if (ModelState.IsValid)
            {
                ordensServicosRepositorio.Alterar(ordemServico);
                return RedirectToAction("Index");
            }

            MontarViewBagCombos(ordemServico.IdCliente, ordemServico.IdServico, ordemServico.IdUsuario, ordemServico.Pagamento.FormaPagamento.Tipo);
            return View("Cadastro", MontarOrdemServicoViewModel(ordemServico));
        }

        public ActionResult Deletar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdemServico ordemServico = ordensServicosRepositorio.BuscarPorId(id.Value);
            if (ordemServico == null)
            {
                return HttpNotFound();
            }
            return View(ordemServico);
        }

        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ordensServicosRepositorio.Deletar(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                clienteRepositorio.Dispose();
                serviosRepositorio.Dispose();
                usuariosRepositorio.Dispose();
                ordensServicosRepositorio.Dispose();
            }
            base.Dispose(disposing);
        }

        protected void MontarViewBagCombos(long? idCliente = null, long? idServico = null, long? idUsuario = null, eTipoFormaPagamento? tipoFormaPagamento = null)
        {
            ViewBag.IdCliente = new SelectList(ObterSelectList(clienteRepositorio.ListaTodos()), "Value", "Text", idCliente);
            ViewBag.IdServico = new SelectList(ObterSelectList(serviosRepositorio.ListaTodos()), "Value", "Text", idServico);
            ViewBag.IdUsuario = new SelectList(ObterSelectList(usuariosRepositorio.ListaTodos()), "Value", "Text", idUsuario?? AplicacaoWeb.UsuarioLogado.Id);
            ViewBag.TipoPagamento = new SelectList(ObterTipoFormaPagamento(), "Value", "Text", (int?)tipoFormaPagamento);
        }

        public IEnumerable<SelectListItem> ObterSelectList(IEnumerable<Entidade> opcoes)
        {
            return new SelectListItem[] { new SelectListItem() { Value = "", Text = "" } }.Concat(
                opcoes.Select(x => new SelectListItem()
                {
                    Text = x.DescricaoCombo,
                    Value = x.Id.ToString(),
                })
                );
        }

        private IEnumerable<SelectListItem> ObterTipoFormaPagamento()
        {
            return Enum.GetValues(typeof(eTipoFormaPagamento))
                .Cast<eTipoFormaPagamento>()
                .Select(x => new SelectListItem() { Value = ((int)x).ToString(), Text = x.GetEnumDescription() });
        }

        private OrdemServico MontarEntidadeOrdemServico(OrdemServicoViewModel ordemServicoViewModel)
        {
            return new OrdemServico()
            {
                Id = ordemServicoViewModel.Id,
                IdCliente = ordemServicoViewModel.IdCliente,
                IdServico = ordemServicoViewModel.IdServico,
                IdUsuario = ordemServicoViewModel.IdUsuario,
                Unitario = ordemServicoViewModel.Unitario,
                Pagamento = new Pagamento()
                {
                    Id = ordemServicoViewModel.Id,
                    FormaPagamento = new FormaPagamento()
                    {
                        Id = ordemServicoViewModel.Id,
                        CodigoBarra = ordemServicoViewModel.CodigoBarra,
                        CodigoSegurança = ordemServicoViewModel.CodigoSegurança,
                        NumeroCartão = ordemServicoViewModel.NumeroCartão,
                        CodigoPix = ordemServicoViewModel.CodigoPix,
                        Tipo = ordemServicoViewModel.TipoPagamento
                    }
                }
            };
        }

        private OrdemServicoViewModel MontarOrdemServicoViewModel(OrdemServico ordemServico)
        {
            return new OrdemServicoViewModel()
            {
                Id = ordemServico.Id,
                IdCliente = ordemServico.IdCliente,
                IdServico = ordemServico.IdServico,
                IdUsuario = ordemServico.IdUsuario,
                Unitario = ordemServico.Unitario,
                CodigoBarra = ordemServico.Pagamento.FormaPagamento.CodigoBarra,
                CodigoPix = ordemServico.Pagamento.FormaPagamento.CodigoPix,
                CodigoSegurança = ordemServico.Pagamento.FormaPagamento.CodigoSegurança,
                NumeroCartão = ordemServico.Pagamento.FormaPagamento.NumeroCartão,
                TipoPagamento = ordemServico.Pagamento.FormaPagamento.Tipo,
            };
        }
    }
}
