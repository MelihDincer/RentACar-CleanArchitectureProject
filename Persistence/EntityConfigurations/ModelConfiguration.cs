using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ModelConfiguration : IEntityTypeConfiguration<Model>
{
    public void Configure(EntityTypeBuilder<Model> builder)
    {
        builder.ToTable("Models").HasKey(m => m.Id);

        builder.Property(m => m.Id).HasColumnName("Id").IsRequired();
        builder.Property(m => m.Name).HasColumnName("Name").IsRequired();
        builder.Property(m => m.BrandId).HasColumnName("BrandId").IsRequired();
        builder.Property(m => m.FuelId).HasColumnName("FuelId").IsRequired();
        builder.Property(m => m.TransmissionId).HasColumnName("TransmissionId").IsRequired();
        builder.Property(m => m.DailyPrice).HasColumnName("DailyPrice").IsRequired();
        builder.Property(m => m.ImageUrl).HasColumnName("ImageUrl").IsRequired();
        builder.Property(m => m.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(m => m.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(t => t.DeletedDate).HasColumnName("DeletedDate");

        //Veritabanı seviyesinde marka isimlerinin tekrar etmemesini burada istiyoruz.
        builder.HasIndex(indexExpression: b => b.Name, name: "UK_Transmissions_Name").IsUnique(); //Buradaki "name" bir önemi yok bir standart olması için yazıldı.

        //Bir modelin bir markası olur
        builder.HasOne(m => m.Brand);

        //Bir modelin bir yakıt tipi olur
        builder.HasOne(m => m.Fuel);

        //Bir modelin bir vites tipi olur
        builder.HasOne(m => m.Transmission);

        //Bir modelde birden fazla araba olabilir.
        builder.HasMany(m => m.Cars);

        //HasQueryFilter => Global Query Filter 
        //Bu konfigürasyondan sonra ilgili uygulamada Brand üzerine yapılan tüm sorgular default olarak aşağıdaki şarta tabi tutulacaktır.
        builder.HasQueryFilter(b => !b.DeletedDate.HasValue); //DeletedDate'in HasValue değeri yok ise. Default olarak her query e bunu uygula. Yani DeletedDate değeri olmayanlar(silinmeyen veriler) listelenecek örneğiyle düşünebiliriz.
    }
}
