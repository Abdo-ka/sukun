using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Sukun.Domin.Entities;

namespace Sukun.Infrastructure.Common
{
    public class VersionInterceptor : SaveChangesInterceptor
    {
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;

            if (context == null) return await base.SavingChangesAsync(eventData, result, cancellationToken);

            var entries = context.ChangeTracker.Entries<BaseEntity>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted);

            var tableNames = new HashSet<string>();

            foreach (var entry in entries)
            {
                entry.Property("Version").CurrentValue = (long)entry.Property("Version").CurrentValue! + 1;
                var tableName = entry.Metadata.GetTableName();
                if (tableName != null)
                    tableNames.Add(tableName);
            }

            // زيادة Version في جدول DataVersions
            foreach (var tableName in tableNames)
            {
                var versionEntity = await context.Set<DataVersion>()
                    .FirstOrDefaultAsync(v => v.TableName == tableName, cancellationToken);

                if (versionEntity == null)
                {
                    versionEntity = new DataVersion
                    {
                        Id = Guid.NewGuid(),
                        TableName = tableName,
                        Version = 1,
                        LastUpdated = DateTime.UtcNow
                    };
                    context.Set<DataVersion>().Add(versionEntity);
                }
                else
                {
                    versionEntity.Version++;
                    versionEntity.LastUpdated = DateTime.UtcNow;
                }
            }

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
