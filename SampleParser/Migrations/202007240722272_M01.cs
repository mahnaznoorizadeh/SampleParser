namespace SampleParser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KeyId = c.String(),
                        DateOfPayment = c.DateTime(),
                        Description = c.String(),
                        Sum = c.Decimal(precision: 18, scale: 2),
                        Balance = c.Decimal(precision: 18, scale: 2),
                        InsertDateTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Payments");
        }
    }
}
