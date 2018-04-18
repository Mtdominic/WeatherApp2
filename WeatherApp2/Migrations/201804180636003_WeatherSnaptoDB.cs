namespace WeatherApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WeatherSnaptoDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WeatherSnaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                        Temp = c.Double(nullable: false),
                        Humidity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WeatherSnaps");
        }
    }
}
