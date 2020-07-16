namespace LIbrary_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Book_Borrower", "ReturnedDae", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Book_Borrower", "ReturnedDae");
        }
    }
}
