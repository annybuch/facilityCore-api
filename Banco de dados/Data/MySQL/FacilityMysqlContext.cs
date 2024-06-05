using Microsoft.EntityFrameworkCore;

namespace FacilityCore.API.Banco_de_dados.Data.MySQL
{
    public abstract class FacilityMysqlContext : DbContext
    {
        public FacilityMysqlContext(DbContextOptions options) : base(options) { }

        public abstract Task<bool> Commit();
        public abstract Task<bool> Commit(Guid userId);
    }
}
