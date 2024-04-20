using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable("Brands").HasKey(b=>b.Id);

        builder.Property(b => b.Id).HasColumnName("Id").IsRequired();
        builder.Property(b => b.Name).HasColumnName("Name").IsRequired();
        builder.Property(b => b.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(b => b.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(b => b.DeletedDate).HasColumnName("DeletedDate");

        //HasQueryFilter => Global Query Filter 
        //Bu konfigürasyondan sonra ilgili uygulamada Brand üzerine yapılan tüm sorgular default olarak aşağıdaki şarta tabi tutulacaktır.
        builder.HasQueryFilter(b => !b.DeletedDate.HasValue); //DeletedDate'in HasValue değeri yok ise. Default olarak her query e bunu uygula. Yani DeletedDate değeri olmayanlar(silinmeyen veriler) listelenecek örneğiyle düşünebiliriz.
    }
}
