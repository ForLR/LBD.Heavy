using Heavy.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heavy.Data.Mappings
{
    public class AlbumMap : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.ToTable("album");
            builder.Property(x => x.Id).HasColumnName("id").IsRequired();
            builder.Property(x => x.CreateDateTime).HasColumnName("create_date_time") ;
            builder.Property(x => x.Name).HasColumnName("name").HasColumnType("varchar(20)").HasMaxLength(20).IsRequired();
            builder.Property(x => x.Title).HasColumnName("title").HasMaxLength(20);
            builder.Property(x => x.ImgUrl).HasColumnName("img_url").HasMaxLength(200).IsRequired();
        }
    }
}
