namespace Arriendos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fotoes", "Imagen", c => c.Binary());
            AddColumn("dbo.Fotoes", "IdPropiedad", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "FotoPerfil", c => c.Binary());
            CreateIndex("dbo.Fotoes", "IdPropiedad");
            AddForeignKey("dbo.Fotoes", "IdPropiedad", "dbo.Propiedads", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fotoes", "IdPropiedad", "dbo.Propiedads");
            DropIndex("dbo.Fotoes", new[] { "IdPropiedad" });
            DropColumn("dbo.AspNetUsers", "FotoPerfil");
            DropColumn("dbo.Fotoes", "IdPropiedad");
            DropColumn("dbo.Fotoes", "Imagen");
        }
    }
}
