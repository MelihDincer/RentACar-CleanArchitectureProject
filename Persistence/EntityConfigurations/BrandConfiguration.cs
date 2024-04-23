using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable("Brands").HasKey(b => b.Id);

        builder.Property(b => b.Id).HasColumnName("Id").IsRequired();
        builder.Property(b => b.Name).HasColumnName("Name").IsRequired();
        builder.Property(b => b.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(b => b.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(b => b.DeletedDate).HasColumnName("DeletedDate");

        //Veritabanı seviyesinde marka isimlerinin tekrar etmemesini burada istiyoruz.
        builder.HasIndex(indexExpression: b => b.Name, name: "UK_Brands_Name").IsUnique(); //Buradaki "name" bir önemi yok bir standart olması için yazıldı.

        //Bir marka birden fazla modele sahip olabilir.
        builder.HasMany(b => b.Models);

        //HasQueryFilter => Global Query Filter 
        //Bu konfigürasyondan sonra ilgili uygulamada Brand üzerine yapılan tüm sorgular default olarak aşağıdaki şarta tabi tutulacaktır.
        builder.HasQueryFilter(b => !b.DeletedDate.HasValue); //DeletedDate'in HasValue değeri yok ise. Default olarak her query e bunu uygula. Yani DeletedDate değeri olmayanlar(silinmeyen veriler) listelenecek örneğiyle düşünebiliriz.
    }
}