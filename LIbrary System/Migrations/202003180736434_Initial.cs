namespace LIbrary_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Auther = c.String(),
                        Copies = c.Int(nullable: false),
                        MaxCop = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Book_Borrower",
                c => new
                    {
                        ProcessNo = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProcessNo)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Persons", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.Persons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Book_Borrower", "PersonId", "dbo.Persons");
            DropForeignKey("dbo.Book_Borrower", "BookId", "dbo.Books");
            DropIndex("dbo.Book_Borrower", new[] { "PersonId" });
            DropIndex("dbo.Book_Borrower", new[] { "BookId" });
            DropTable("dbo.Persons");
            DropTable("dbo.Book_Borrower");
            DropTable("dbo.Books");
        }
    }
}
