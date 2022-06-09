namespace Trabalho_2_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migraco_1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nome = c.String(),
                        CPF = c.String(),
                        DataNascimento = c.DateTime(nullable: false),
                        Endereco = c.String(),
                        Telefone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrdemServico",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IdCliente = c.Long(nullable: false),
                        IdUsuario = c.Long(nullable: false),
                        IdServico = c.Long(nullable: false),
                        Unitario = c.Int(nullable: false),
                        DataAbertura = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cliente", t => t.IdCliente, cascadeDelete: true)
                .ForeignKey("dbo.Servico", t => t.IdServico, cascadeDelete: true)
                .ForeignKey("dbo.Usuario", t => t.IdUsuario, cascadeDelete: true)
                .Index(t => t.IdCliente)
                .Index(t => t.IdUsuario)
                .Index(t => t.IdServico);
            
            CreateTable(
                "dbo.Pagamento",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        ValorTotal = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrdemServico", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.FormaPagamento",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Tipo = c.Int(nullable: false),
                        CodigoPix = c.String(),
                        CodigoBarra = c.String(),
                        NumeroCartão = c.String(),
                        CodigoSegurança = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pagamento", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Servico",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nome = c.String(),
                        Descricao = c.String(),
                        ValorUnitario = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NomeCompleto = c.String(),
                        Login = c.String(),
                        Senha = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrdemServico", "IdUsuario", "dbo.Usuario");
            DropForeignKey("dbo.OrdemServico", "IdServico", "dbo.Servico");
            DropForeignKey("dbo.Pagamento", "Id", "dbo.OrdemServico");
            DropForeignKey("dbo.FormaPagamento", "Id", "dbo.Pagamento");
            DropForeignKey("dbo.OrdemServico", "IdCliente", "dbo.Cliente");
            DropIndex("dbo.FormaPagamento", new[] { "Id" });
            DropIndex("dbo.Pagamento", new[] { "Id" });
            DropIndex("dbo.OrdemServico", new[] { "IdServico" });
            DropIndex("dbo.OrdemServico", new[] { "IdUsuario" });
            DropIndex("dbo.OrdemServico", new[] { "IdCliente" });
            DropTable("dbo.Usuario");
            DropTable("dbo.Servico");
            DropTable("dbo.FormaPagamento");
            DropTable("dbo.Pagamento");
            DropTable("dbo.OrdemServico");
            DropTable("dbo.Cliente");
        }
    }
}
