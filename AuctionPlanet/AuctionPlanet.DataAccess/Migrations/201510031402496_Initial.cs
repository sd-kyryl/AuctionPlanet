using System.Data.Entity.Migrations;

namespace AuctionPlanet.DataAccess.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuctionUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Lots",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        ImageUrl = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        StartPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        NewOwner_Id = c.Guid(),
                        OriginalOwner_Id = c.Guid(),
                        AuctionUser_Id = c.Guid(),
                        AuctionUser_Id1 = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AuctionUsers", t => t.NewOwner_Id)
                .ForeignKey("dbo.AuctionUsers", t => t.OriginalOwner_Id)
                .ForeignKey("dbo.AuctionUsers", t => t.AuctionUser_Id)
                .ForeignKey("dbo.AuctionUsers", t => t.AuctionUser_Id1)
                .Index(t => t.NewOwner_Id)
                .Index(t => t.OriginalOwner_Id)
                .Index(t => t.AuctionUser_Id)
                .Index(t => t.AuctionUser_Id1);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
            DropForeignKey("dbo.Lots", "AuctionUser_Id1", "dbo.AuctionUsers");
            DropForeignKey("dbo.AuctionUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Lots", "AuctionUser_Id", "dbo.AuctionUsers");
            DropForeignKey("dbo.Lots", "OriginalOwner_Id", "dbo.AuctionUsers");
            DropForeignKey("dbo.Lots", "NewOwner_Id", "dbo.AuctionUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Lots", new[] { "AuctionUser_Id1" });
            DropIndex("dbo.Lots", new[] { "AuctionUser_Id" });
            DropIndex("dbo.Lots", new[] { "OriginalOwner_Id" });
            DropIndex("dbo.Lots", new[] { "NewOwner_Id" });
            DropIndex("dbo.AuctionUsers", new[] { "ApplicationUser_Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Lots");
            DropTable("dbo.AuctionUsers");
        }
    }
}
