namespace Task2_EF.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Task2_EF.NorthwindContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NorthwindContext context)
        {
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Categories.AddOrUpdate(c => c.CategoryName,
                new Category { CategoryName = "Tours" },
                new Category { CategoryName = "Drinks" });

            context.Regions.AddOrUpdate(r => r.RegionID,
                new Region { RegionDescription = "Minsk", RegionID = 7 });

            context.Territories.AddOrUpdate(t => t.TerritoryID,
                new Territory { TerritoryID = "12345", TerritoryDescription = "Stolitsa Mall", RegionID = 7 },
                new Territory { TerritoryID = "13579", TerritoryDescription = "Galileo Mall", RegionID = 7 });
        }
    }
}
