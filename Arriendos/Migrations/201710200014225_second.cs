namespace Arriendos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ciudads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.Int(nullable: false),
                        Nombre = c.String(),
                        IdDepartamento = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departamentoes", t => t.IdDepartamento, cascadeDelete: true)
                .Index(t => t.IdDepartamento);
            
            CreateTable(
                "dbo.Departamentoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.Int(nullable: false),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Postulars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdPropiedad = c.Int(nullable: false),
                        usuario_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Propiedads", t => t.IdPropiedad, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.usuario_Id)
                .Index(t => t.IdPropiedad)
                .Index(t => t.usuario_Id);
            
            CreateTable(
                "dbo.Propiedads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Direccion = c.String(),
                        Precio = c.Double(nullable: false),
                        IdCiudad = c.Int(nullable: false),
                        Usuario_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ciudads", t => t.IdCiudad, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Usuario_Id)
                .Index(t => t.IdCiudad)
                .Index(t => t.Usuario_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Postulars", "usuario_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Postulars", "IdPropiedad", "dbo.Propiedads");
            DropForeignKey("dbo.Propiedads", "Usuario_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Propiedads", "IdCiudad", "dbo.Ciudads");
            DropForeignKey("dbo.Ciudads", "IdDepartamento", "dbo.Departamentoes");
            DropIndex("dbo.Propiedads", new[] { "Usuario_Id" });
            DropIndex("dbo.Propiedads", new[] { "IdCiudad" });
            DropIndex("dbo.Postulars", new[] { "usuario_Id" });
            DropIndex("dbo.Postulars", new[] { "IdPropiedad" });
            DropIndex("dbo.Ciudads", new[] { "IdDepartamento" });
            DropTable("dbo.Propiedads");
            DropTable("dbo.Postulars");
            DropTable("dbo.Departamentoes");
            DropTable("dbo.Ciudads");
        }
    }
}
