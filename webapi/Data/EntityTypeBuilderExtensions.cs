﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using webapi.Model.BaseEntities;

namespace webapi.Data
{
    public static class EntityTypeBuilderExtensions
    {
        public static void ToTemporalTable<T>(this EntityTypeBuilder<T> entity, string table, string schema, string auditSchema = "Auditing") where T : class, IAuditModel
        {
            entity.ToTable(table, schema);

            entity.ToTable(tb => tb.IsTemporal(ttb =>
            {
                ttb.UseHistoryTable($"{schema}_{table}_HISTORY", auditSchema);
                ttb.HasPeriodStart("SystemStart").HasColumnName("SystemStart");
                ttb.HasPeriodEnd("SystemEnd").HasColumnName("SystemEnd");
            }));

            entity.Property(e => e.RowVersion)
            .IsRequired()
            .IsRowVersion()
            .IsConcurrencyToken();

            entity.Property(e => e.ChangedByUser)
            .IsRequired()
            .HasMaxLength(200)
            .HasDefaultValueSql("(user_name())");
        }
    }
}
