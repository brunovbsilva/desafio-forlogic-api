using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using static System.Net.Mime.MediaTypeNames;

namespace Infra.Data.Configurations
{
    public class PeopleConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("people");

            var dateTimeConverter = new ValueConverter<DateTime, long>(v => v.Ticks, v => new DateTime(v));

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("id").HasColumnType("uuid").ValueGeneratedNever().IsRequired();
            builder.Property(p => p.CreatedAt).HasColumnName("CreatedAt").HasColumnType("bigint").HasConversion(dateTimeConverter).IsRequired();
            builder.Property(p => p.Name).HasColumnName("Name").HasColumnType("text").IsRequired();
            builder.Property(p => p.Email).HasColumnName("Email").HasColumnType("text").IsRequired();
            builder.Property(p => p.Active).HasColumnName("Active").HasColumnType("boolean").IsRequired();
            builder.Property(p => p.Age).HasColumnName("Age").HasColumnType("text");
            builder.Property(p => p.Address).HasColumnName("Address").HasColumnType("text");
            builder.Property(p => p.OtherInformation).HasColumnName("OtherInformation").HasColumnType("text");
            builder.Property(p => p.Interests).HasColumnName("Interests").HasColumnType("text");
            builder.Property(p => p.Feelings).HasColumnName("Feelings").HasColumnType("text");
            builder.Property(p => p.Values).HasColumnName("Values").HasColumnType("text");
        }
    }
}
