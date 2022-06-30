namespace Trabalho_2_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using Trabalho_2_MVC.App_Start;

    public partial class PopularDados : DbMigration
    {
        public override void Up()
        {
            PopulacaoBaseConfig.CriarDados();
        }
        
        public override void Down()
        {
        }
    }
}
