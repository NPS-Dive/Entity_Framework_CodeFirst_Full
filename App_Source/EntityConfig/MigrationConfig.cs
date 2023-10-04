
using System.Data.Entity.Migrations;

public class MigrationConfig : DbMigrationsConfiguration<ShipContext>
    {
    public MigrationConfig ()
        {
        AutomaticMigrationsEnabled = true;
        AutomaticMigrationDataLossAllowed = true;
        }
    }

