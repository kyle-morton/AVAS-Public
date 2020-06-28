using AVAS.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace AVAS.Core.Data
{
    public class AVASDbContext : DbContext
    {

        public AVASDbContext(DbContextOptions<AVASDbContext> options) : base(options)
        { }

        public DbSet<VideoRequest> VideoRequests { get; set; }

    }
}
