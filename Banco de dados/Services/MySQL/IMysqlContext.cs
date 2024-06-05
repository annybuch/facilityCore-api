using FacilityCore.API.Banco_de_dados.Data.MySQL;
using FacilityCore.API.Banco_de_dados.Domain.MySQL;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace FacilityCore.API.Banco_de_dados.Services.MySQL
{
    public interface IMysqlContext<D> where D : FacilityMysqlContext
    {
        // ** Adicionar.
        void Add<T>(T entity) where T : FacilityEntityId;
        Task AddAsync<T>(T entity) where T : FacilityEntityId;
        Task<bool> Commit();

        // ** Remover.
        void SoftRemove<T>(T entity) where T : FacilityEntityId;
        void HardRemove<T>(T entity) where T : FacilityEntityId;
        Task HardRemoveAsync<T>(T entity) where T : FacilityEntityId;

        // ** Obter.
        IQueryable<T> Get<T>() where T : FacilityEntityId;
        Task<IEnumerable<T>> GetAsync<T>(Expression<Func<T, bool>> filterExpression = null) where T : FacilityEntityId;

        // ** Atualizar.
        Task UpdateAsync<T>(T entity) where T : FacilityEntityId;
        void Update<T>(T entity) where T : FacilityEntityId;

        // ** Querys.
        Task<IEnumerable<T>> GetName<T>(string propriedadeNome, string valor) where T : FacilityEntityId;
        bool Any<T>(Expression<Func<T, bool>> expression) where T : FacilityEntityId;
        T With<T, K>(Expression<Func<T, K>> expression, Guid id) where T : FacilityEntityId;
        IIncludableQueryable<T, K> With<T, K>(Expression<Func<T, K>> expression) where T : FacilityEntityId;
        IQueryable<T> Where<T>(Expression<Func<T, bool>> filterExpression = null) where T : FacilityEntityId;
    }
}
