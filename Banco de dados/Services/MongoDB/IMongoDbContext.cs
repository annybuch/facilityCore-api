using FacilityCore.API.Banco_de_dados.Data.MongoDB;
using FacilityCore.API.Banco_de_dados.Domain.MongoDB;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace FacilityCore.API.Banco_de_dados.Services.MongoDB
{
    public interface IMongoDbContext<D> where D : FacilityMongoDbContext
    {
        // ** Remove
        Task RemoveAsync<T>(Expression<Func<T, bool>> filterExpression) where T : FacilityCollectionId;
        void Remove<T>(Expression<Func<T, bool>> filterExpression) where T : FacilityCollectionId;

        // ** Get
        IEnumerable<T> Get<T>(Expression<Func<T, bool>> filterExpression = null) where T : FacilityCollectionId;
        Task<IEnumerable<T>> GetAsync<T>(Expression<Func<T, bool>> filterExpression = null) where T : FacilityCollectionId;

        // ** Insert
        void Insert<T>(T model) where T : FacilityCollectionId;
        Task InsertAsync<T>(T model) where T : FacilityCollectionId;

        // ** Update
        void Update<T>(T doc, Expression<Func<T, bool>> filterExpression) where T : FacilityCollectionId;
        Task UpdateAsync<T>(T doc, Expression<Func<T, bool>> filterExpression) where T : FacilityCollectionId;

        // ** Querys
        Task<IEnumerable<T>> GetName<T>(string propertyName, string searchValue) where T : FacilityCollectionId;
        IMongoQueryable<T> Where<T>(Expression<Func<T, bool>> filterExpression = null) where T : FacilityCollectionId;
        
    }
}
