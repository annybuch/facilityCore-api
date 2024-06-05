using FacilityCore.API.Banco_de_dados.Data.MongoDB;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using System.Linq.Expressions;
using FacilityCore.API.Banco_de_dados.Domain.MongoDB;
using FacilityCore.API.Attributes.MongoDB;
using MongoDB.Bson;

namespace FacilityCore.API.Banco_de_dados.Services.MongoDB
{
    public class MongoDbContext<D> : IMongoDbContext<D> where D : FacilityMongoDbContext
    {
        private readonly D _context;
        public MongoDbContext(D context)
        {
            _context = context;
        }

        // ** Obtém o nome da coleção.
        private static string GetCollectionName<T>() where T : FacilityCollectionId
        {
            return (typeof(T).GetCustomAttributes(typeof(FacilityMongoCollection), true)
                .FirstOrDefault() as FacilityMongoCollection)!.CollectionName;
        }

        #region Remove
        // ** Remove um documento de forma async de acordo com o filtro passado.
        public virtual async Task RemoveAsync<T>(Expression<Func<T, bool>> filterExpression) where T : FacilityCollectionId
        {
            await _context.GetDatabase(_context._data).GetCollection<T>(GetCollectionName<T>()).FindOneAndDeleteAsync(filterExpression);
        }

        // ** Remove um documento de acordo com o filtro passado.
        public virtual void Remove<T>(Expression<Func<T, bool>> filterExpression) where T : FacilityCollectionId
        {
            _context.GetDatabase(_context._data).GetCollection<T>(GetCollectionName<T>()).FindOneAndDelete(filterExpression);
        }
        #endregion Remove

        #region Insert
        // ** Insere um novo documento de forma async na coleção.
        public async Task InsertAsync<T>(T model) where T : FacilityCollectionId
        {
            await _context.GetDatabase(_context._data).GetCollection<T>(GetCollectionName<T>()).InsertOneAsync(model);
        }

        // ** Insere um novo documento na coleção.
        public void Insert<T>(T model) where T : FacilityCollectionId
        {
            _context.GetDatabase(_context._data).GetCollection<T>(GetCollectionName<T>()).InsertOne(model);
        }
        #endregion Insert

        #region Get
        // ** Obtém algo na collection de forma assíncrona de acordo com o filtro passado.
        public async Task<IEnumerable<T>> GetAsync<T>(Expression<Func<T, bool>> filterExpression = null) where T : FacilityCollectionId
        {
            var collection = _context.GetDatabase(_context._data).GetCollection<T>(GetCollectionName<T>());
            if (filterExpression == null)
            {
                return await collection.AsQueryable().ToListAsync();
            }
            return (await collection.FindAsync(filterExpression)).ToEnumerable();
        }

        // ** Obtém algo na collection de acordo com o filtro passado.
        public IEnumerable<T> Get<T>(Expression<Func<T, bool>> filterExpression = null) where T : FacilityCollectionId
        {
            var collection = _context.GetDatabase(_context._data).GetCollection<T>(GetCollectionName<T>());
            if (filterExpression == null)
            {
                return collection.AsQueryable().ToList();
            }
            return collection.Find(filterExpression).ToEnumerable();
        }
        #endregion Get

        #region Update
        // ** Atualiza de forma assíncrona um documento de acordo com o filtro passado.
        public virtual async Task UpdateAsync<T>(T doc, Expression<Func<T, bool>> filterExpression) where T : FacilityCollectionId
        {
            await _context.GetDatabase(_context._data).GetCollection<T>(GetCollectionName<T>()).FindOneAndReplaceAsync(filterExpression, doc);
        }

        // ** Atualiza um documento de acordo com o filtro passado.
        public virtual void Update<T>(T doc, Expression<Func<T, bool>> filterExpression) where T : FacilityCollectionId
        {
            _context.GetDatabase(_context._data).GetCollection<T>(GetCollectionName<T>()).FindOneAndReplace(filterExpression, doc);
        }
        #endregion Update

        #region Querys
        // ** Pesquisa algo na coleção de acordo com o filtro passado.
        public IMongoQueryable<T> Where<T>(Expression<Func<T, bool>> filterExpression = null) where T : FacilityCollectionId
        {
            var collection = _context.GetDatabase(_context._data).GetCollection<T>(GetCollectionName<T>()).AsQueryable();
            if (filterExpression != null)
            {
                return collection.Where(filterExpression);
            }
            return collection;
        }

        // ** Responsável por fazer uma pesquisa por nome.
        public async Task<IEnumerable<T>> GetName<T>(string propertyName, string searchValue) where T : FacilityCollectionId
        {
            var collection = _context.GetDatabase(_context._data).GetCollection<T>(GetCollectionName<T>());
            var allFields = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            var property = allFields.FirstOrDefault(p => LevenshteinDistance(p.Name.ToLower(), propertyName.ToLower()) <= 2);
            if (property == null) throw new Exception("Propriedade não encontrada");

            var filter = Builders<T>.Filter.Regex(property.Name, new BsonRegularExpression(searchValue, "i"));

            return (await collection.FindAsync(filter)).ToEnumerable();
        }

        // ** Responsável por calcular a distância de Levenshtein entre duas strings.
        private int LevenshteinDistance(string a, string b)
        {
            var n = a.Length;
            var m = b.Length;
            var d = new int[n + 1, m + 1];

            if (n == 0) return m;
            if (m == 0) return n;

            for (var i = 0; i <= n; d[i, 0] = i++) ;
            for (var j = 0; j <= m; d[0, j] = j++) ;

            for (var i = 1; i <= n; i++)
            {
                for (var j = 1; j <= m; j++)
                {
                    var cost = (b[j - 1] == a[i - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            }

            return d[n, m];
        }
        #endregion Querys
    }
}
