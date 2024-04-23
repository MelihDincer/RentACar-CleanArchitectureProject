using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class TransmissionConfiguration : IEntityTypeConfiguration<Transmission>
{
    public void Configure(EntityTypeBuilder<Transmission> builder)
    {
        builder.ToTable("Transmissions").HasKey(t => t.Id);

        builder.Property(t => t.Id).HasColumnName("Id").IsRequired();
        builder.Property(t => t.Name).HasColumnName("Name").IsRequired();
        builder.Property(t => t.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(t => t.DeletedDate).HasColumnName("DeletedDate");

        //Veritabanı seviyesinde marka isimlerinin tekrar etmemesini burada istiyoruz.
        builder.HasIndex(indexExpression: t => t.Name, name: "UK_Transmissions_Name").IsUnique(); //Buradaki "name" bir önemi yok bir standart olması için yazıldı.

        //Bir marka birden fazla modele sahip olabilir.
        builder.HasMany(t => t.Models);

        //HasQueryFilter => Global Query Filter 
        //Bu konfigürasyondan sonra ilgili uygulamada Brand üzerine yapılan tüm sorgular default olarak aşağıdaki şarta tabi tutulacaktır.
        builder.HasQueryFilter(t => !t.DeletedDate.HasValue); //DeletedDate'in HasValue değeri yok ise. Default olarak her query e bunu uygula. Yani DeletedDate değeri olmayanlar(silinmeyen veriler) listelenecek örneğiyle düşünebiliriz.
    }
}