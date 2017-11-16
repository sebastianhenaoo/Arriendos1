namespace Arriendos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
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
                "dbo.Fotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Imagen = c.Binary(),
                        IdPropiedad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Propiedads", t => t.IdPropiedad, cascadeDelete: true)
                .Index(t => t.IdPropiedad);
            
            CreateTable(
                "dbo.Propiedads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Direccion = c.String(),
                        Precio = c.Double(nullable: false),
                        IdCiudad = c.Int(nullable: false),
                        Descripcion = c.String(),
                        Estado = c.Boolean(nullable: false),
                        IdUsuario = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ciudads", t => t.IdCiudad, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.IdUsuario)
                .Index(t => t.IdCiudad)
                .Index(t => t.IdUsuario);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Identificacion = c.Int(nullable: false),
                        Nombre = c.String(nullable: false),
                        Apellido = c.String(nullable: false),
                        Direccion = c.String(),
                        FotoPerfil = c.Binary(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Postulars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdPropiedad = c.Int(nullable: false),
                        IdUsuario = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Propiedads", t => t.IdPropiedad, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.IdUsuario)
                .Index(t => t.IdPropiedad)
                .Index(t => t.IdUsuario);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Postulars", "IdUsuario", "dbo.AspNetUsers");
            DropForeignKey("dbo.Postulars", "IdPropiedad", "dbo.Propiedads");
            DropForeignKey("dbo.Fotoes", "IdPropiedad", "dbo.Propiedads");
            DropForeignKey("dbo.Propiedads", "IdUsuario", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Propiedads", "IdCiudad", "dbo.Ciudads");
            DropForeignKey("dbo.Ciudads", "IdDepartamento", "dbo.Departamentoes");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Postulars", new[] { "IdUsuario" });
            DropIndex("dbo.Postulars", new[] { "IdPropiedad" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Propiedads", new[] { "IdUsuario" });
            DropIndex("dbo.Propiedads", new[] { "IdCiudad" });
            DropIndex("dbo.Fotoes", new[] { "IdPropiedad" });
            DropIndex("dbo.Ciudads", new[] { "IdDepartamento" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Postulars");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Propiedads");
            DropTable("dbo.Fotoes");
            DropTable("dbo.Departamentoes");
            DropTable("dbo.Ciudads");
        }
    }
}
