using AVAS.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AVAS.Core.Extensions
{
    public static class DbContextExtensions
    {
        public static async Task<int> SaveChangesWithTimesAsync(this DbContext dbContext)
        {
            var entries = dbContext.ChangeTracker.Entries().ToList();
            foreach (var entry in entries)
            {
                var model = entry.Entity as EntityBase;
                if (model == null)
                    continue;

                if (entry.State == EntityState.Added)
                {
                    model.CreateDate = DateTime.UtcNow;
                    model.ModifyDate = DateTime.UtcNow;
                }
                else
                {
                    model.ModifyDate = DateTime.UtcNow;
                    if (model.CreateDate == default)
                    {
                        model.CreateDate = model.ModifyDate;
                    }
                }
            }

            return await dbContext.SaveChangesAsync();
        }
    }
}

