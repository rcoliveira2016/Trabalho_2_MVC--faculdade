using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trabalho_2_MVC.Dominio.Entidades;
using Trabalho_2_MVC.Dominio.Infra.Ioc;
using Trabalho_2_MVC.Dominio.Interfaces.Data;
using Bogus.Extensions.Brazil;

namespace Trabalho_2_MVC.App_Start
{
    public static class PopulacaoBaseConfig
    {

        private static readonly IOrdensServicosRepositorio ordensServicosRepositorio;
        private static readonly IClientesRepositorio clienteRepositorio;
        private static readonly IServicosRepositorio serviosRepositorio;
        private static readonly IUsuariosRepositorio usuariosRepositorio;
        private const int QUANTIDADE_ITENS_FAKE = 7;

        static PopulacaoBaseConfig()
        {
            clienteRepositorio = InMemory.GetService<IClientesRepositorio>();
            usuariosRepositorio = InMemory.GetService<IUsuariosRepositorio>();
            serviosRepositorio = InMemory.GetService<IServicosRepositorio>();
            ordensServicosRepositorio = InMemory.GetService<IOrdensServicosRepositorio>();
        }

        public static void CriarDados()
        {
            if (clienteRepositorio.PossuiRegistro()) return;
            if (usuariosRepositorio.PossuiRegistro()) return;
            if (serviosRepositorio.PossuiRegistro()) return;

            var clienteCraidos = CriarCliente().ToList();
            var servicoCraidos = CriarServico().ToList();
            var usuariosCraidos = CriarUsuarios().ToList();

            var faker = new Faker("pt_BR");
            for (int i = 0; i < QUANTIDADE_ITENS_FAKE; i++)
            {
                var idClinete = faker.Random.Int(1, QUANTIDADE_ITENS_FAKE);
                var idServico = faker.Random.Int(1, QUANTIDADE_ITENS_FAKE);
                var idUsario = faker.Random.Int(1, QUANTIDADE_ITENS_FAKE);

                var ordenServico = new OrdemServico()
                {
                    IdCliente = idClinete,
                    IdServico = idServico,
                    IdUsuario = idUsario,
                    Unitario = faker.Random.Int(1, 10)
                };

                ordenServico.Pagamento = new Pagamento()
                {
                    FormaPagamento = ObterFormaPagamento()
                };

                ordenServico.SetarValorParaSalvar();
                ordensServicosRepositorio.Adiciona(ordenServico);
            }
        }

        private static FormaPagamento ObterFormaPagamento()
        {
            var faker = new Faker("pt_BR");
            var tipoPagamento = faker.Random.Enum<eTipoFormaPagamento>();
            switch (tipoPagamento)
            {
                case eTipoFormaPagamento.Cartao:
                    return new FormaPagamento()
                    {
                        NumeroCartão = faker.Finance.CreditCardNumber(),
                        CodigoSegurança = faker.Finance.CreditCardCvv(),
                        Tipo = tipoPagamento
                    };
                case eTipoFormaPagamento.Boleto:
                    return new FormaPagamento()
                    {
                        CodigoBarra = faker.Commerce.Ean13(),
                        Tipo = tipoPagamento
                    };
                case eTipoFormaPagamento.Pix:
                    return new FormaPagamento()
                    {
                        CodigoPix = Guid.NewGuid().ToString(),
                        Tipo = tipoPagamento
                    };
                default:
                    return null;
            }
        }

        private static IEnumerable<Cliente> CriarCliente()
        {

            for (int i = 0; i < QUANTIDADE_ITENS_FAKE; i++)
            {
                var faker = new Faker("pt_BR");
                var clienteFake = new Cliente()
                {
                    CPF = faker.Person.Cpf(),
                    DataNascimento = faker.Person.DateOfBirth,
                    Endereco = faker.Address.FullAddress(),
                    Nome = faker.Person.FullName,
                    Telefone = faker.Person.Phone,
                };
                clienteRepositorio.Adiciona(clienteFake);
                yield return clienteFake;
            }            
        }

        private static IEnumerable<Servico> CriarServico()
        {

            for (int i = 0; i < QUANTIDADE_ITENS_FAKE; i++)
            {
                var faker = new Faker("pt_BR");
                var servicoFake = new Servico()
                {
                    Descricao = faker.Lorem.Paragraph(1),
                    Nome = faker.Commerce.ProductName(),
                    ValorUnitario = Math.Round(faker.Random.Decimal(1, 500), 2),
                };
                serviosRepositorio.Adiciona(servicoFake);
                yield return servicoFake;
            }
        }

        private static IEnumerable<Usuario> CriarUsuarios()
        {
            var usuarioADM = new Usuario()
            {
                Login = "adm",
                Senha = "adm",
                NomeCompleto = "Administrador"
            };

            usuariosRepositorio.Adiciona(usuarioADM);


            for (int i = 0; i < QUANTIDADE_ITENS_FAKE; i++)
            {
            var faker = new Faker("pt_BR");
                var usuarioFake = new Usuario()
                {
                    NomeCompleto = faker.Person.FullName,
                    Login = faker.Person.UserName,
                    Senha = "dado-fake"
                };
                usuariosRepositorio.Adiciona(usuarioFake);
                yield return usuarioFake;
            }
        }
    }
}