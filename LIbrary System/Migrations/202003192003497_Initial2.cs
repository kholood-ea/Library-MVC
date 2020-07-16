namespace LIbrary_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Borrowers", newName: "People");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.People", newName: "Borrowers");
        }
    }
}
