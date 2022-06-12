namespace Trabalho_2_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracao_2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cliente", "Nome", c => c.String(nullable: false));
            AlterColumn("dbo.Cliente", "CPF", c => c.String(nullable: false));
            AlterColumn("dbo.Cliente", "Endereco", c => c.String(nullable: false));
            AlterColumn("dbo.Cliente", "Telefone", c => c.String(nullable: false));
            AlterColumn("dbo.Servico", "Nome", c => c.String(nullable: false));
            AlterColumn("dbo.Servico", "Descricao", c => c.String(nullable: false));
            AlterColumn("dbo.Usuario", "NomeCompleto", c => c.String(nullable: false));
            AlterColumn("dbo.Usuario", "Login", c => c.String(nullable: false));
            AlterColumn("dbo.Usuario", "Senha", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usuario", "Senha", c => c.String());
            AlterColumn("dbo.Usuario", "Login", c => c.String());
            AlterColumn("dbo.Usuario", "NomeCompleto", c => c.String());
            AlterColumn("dbo.Servico", "Descricao", c => c.String());
            AlterColumn("dbo.Servico", "Nome", c => c.String());
            AlterColumn("dbo.Cliente", "Telefone", c => c.String());
            AlterColumn("dbo.Cliente", "Endereco", c => c.String());
            AlterColumn("dbo.Cliente", "CPF", c => c.String());
            AlterColumn("dbo.Cliente", "Nome", c => c.String());
        }
    }
}
