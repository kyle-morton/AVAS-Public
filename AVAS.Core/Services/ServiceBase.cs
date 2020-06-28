using AVAS.Core.Data;

namespace AVAS.Core.Services
{
    public abstract class ServiceBase
    {
        protected readonly AVASDbContext _dbContext;

        public ServiceBase(AVASDbContext dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
