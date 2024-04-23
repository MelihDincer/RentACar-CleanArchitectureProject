using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class FuelConfiguration : IEntityTypeConfiguration<Fuel>
{
    public void Configure(EntityTypeBuilder<Fuel> builder)
    {
        builder.ToTable("Fuels").HasKey(f => f.Id);

        builder.Property(f => f.Id).HasColumnName("Id").IsRequired();
        builder.Property(f => f.Name).HasColumnName("Name").IsRequired();
        builder.Property(f => f.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(f => f.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(f => f.DeletedDate).HasColumnName("DeletedDate");

        //Veritabanı seviyesinde marka isimlerinin tekrar etmemesini burada istiyoruz.
        builder.HasIndex(indexExpression: f => f.Name, name: "UK_Fuels_Name").IsUnique(); //Buradaki "name" bir önemi yok bir standart olması için yazıldı.

        //Bir marka birden fazla modele sahip olabilir.
        builder.HasMany(f => f.Models);

        //HasQueryFilter => Global Query Filter 
        //Bu konfigürasyondan sonra ilgili uygulamada Brand üzerine yapılan tüm sorgular default olarak aşağıdaki şarta tabi tutulacaktır.
        builder.HasQueryFilter(f => !f.DeletedDate.HasValue); //DeletedDate'in HasValue değeri yok ise. Default olarak her query e bunu uygula. Yani DeletedDate değeri olmayanlar(silinmeyen veriler) listelenecek örneğiyle düşünebiliriz.
    }
}
