
using System.Data.Entity;

public class ShipContext : DbContext
    {
    static ShipContext ()
        {
        Database.SetInitializer<ShipContext>
            (new MigrateDatabaseToLatestVersion<ShipContext,MigrationConfig>("dbShip"));
        }
    public ShipContext () :
        base("name=dbShip")
        {

        }

    protected override void OnModelCreating ( DbModelBuilder modelBuilder )
        {
        modelBuilder.Configurations.Add(new CargoConfig());
        base.OnModelCreating(modelBuilder);
        }

    public virtual DbSet<Cargo> Cargos {  get; set; }
    }

