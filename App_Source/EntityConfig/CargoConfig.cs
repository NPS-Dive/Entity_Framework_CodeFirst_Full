using System.Data.Entity.ModelConfiguration;

public class CargoConfig : EntityTypeConfiguration<Cargo>
    {
    public CargoConfig ()
        {
        HasKey(ca => ca.ID);
        Property(ca => ca.Name).HasMaxLength(50).IsRequired();
        Property(ca => ca.Material).HasMaxLength(50).IsRequired();
        Property(ca => ca.Weight).IsRequired();
        Property(ca => ca.Destination).HasMaxLength(50).IsRequired();
        }
    }

