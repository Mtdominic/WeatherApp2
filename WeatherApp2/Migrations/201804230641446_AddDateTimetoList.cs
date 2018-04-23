namespace WeatherApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateTimetoList : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WeatherSnaps", "Time", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WeatherSnaps", "Time");
        }
    }
}
