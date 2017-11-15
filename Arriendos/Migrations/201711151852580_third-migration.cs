namespace Arriendos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thirdmigration : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Propiedads", name: "Usuario_Id", newName: "IdUsuario");
            RenameColumn(table: "dbo.Postulars", name: "usuario_Id", newName: "IdUsuario");
            RenameIndex(table: "dbo.Propiedads", name: "IX_Usuario_Id", newName: "IX_IdUsuario");
            RenameIndex(table: "dbo.Postulars", name: "IX_usuario_Id", newName: "IX_IdUsuario");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Postulars", name: "IX_IdUsuario", newName: "IX_usuario_Id");
            RenameIndex(table: "dbo.Propiedads", name: "IX_IdUsuario", newName: "IX_Usuario_Id");
            RenameColumn(table: "dbo.Postulars", name: "IdUsuario", newName: "usuario_Id");
            RenameColumn(table: "dbo.Propiedads", name: "IdUsuario", newName: "Usuario_Id");
        }
    }
}
