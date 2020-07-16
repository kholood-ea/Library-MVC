namespace LIbrary_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Book_Borrower", "ReturnedDate", c => c.DateTime());
            DropColumn("dbo.Book_Borrower", "ReturnedDae");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Book_Borrower", "ReturnedDae", c => c.DateTime(nullable: false));
            DropColumn("dbo.Book_Borrower", "ReturnedDate");
        }
    }
}
