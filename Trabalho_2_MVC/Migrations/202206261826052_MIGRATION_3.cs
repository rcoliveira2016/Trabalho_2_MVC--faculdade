namespace Trabalho_2_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MIGRATION_3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pagamento", "ValorTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Servico", "ValorUnitario", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Servico", "ValorUnitario", c => c.Double(nullable: false));
            AlterColumn("dbo.Pagamento", "ValorTotal", c => c.Double(nullable: false));
        }
    }
}
