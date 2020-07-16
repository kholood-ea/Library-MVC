namespace LIbrary_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial5 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Book_Borrower");
            AddColumn("dbo.Book_Borrower", "BookName", c => c.String(nullable: false));
            AddColumn("dbo.Book_Borrower", "BorrowerEmail", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Auther", c => c.String(nullable: false));
            AlterColumn("dbo.People", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.People", "Email", c => c.String(nullable: false));
            AddPrimaryKey("dbo.Book_Borrower", new[] { "BookId", "BorrowerId" });
            DropColumn("dbo.Book_Borrower", "ProcessNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Book_Borrower", "ProcessNo", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Book_Borrower");
            AlterColumn("dbo.People", "Email", c => c.String());
            AlterColumn("dbo.People", "Name", c => c.String());
            AlterColumn("dbo.Books", "Auther", c => c.String());
            AlterColumn("dbo.Books", "Name", c => c.String());
            DropColumn("dbo.Book_Borrower", "BorrowerEmail");
            DropColumn("dbo.Book_Borrower", "BookName");
            AddPrimaryKey("dbo.Book_Borrower", "ProcessNo");
        }
    }
}
