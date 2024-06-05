using FacilityCore.API.Banco_de_dados.Data.MySQL;
using FacilityCore.API.Banco_de_dados.Domain.MySQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Reflection;

namespace FacilityCore.API.Banco_de_dados.Services.MySQL
{
    public class MysqlContext<D> : IMysqlContext<D> where D : FacilityMysqlContext
    {
        private readonly D _context;
        public MysqlContext(D context)
        {
            _context = context;
        }

        // ** Responsável por salvar.
        public async Task<bool> Commit()
        {
            return await _context.Commit();
        }

        #region ADD
        // ** Adiciona uma entidade de forma assíncrona
        public async Task AddAsync<T>(T entity) where T : FacilityEntityId
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        // ** Adiciona uma entidade.
        public void Add<T>(T entity) where T : FacilityEntityId
        {
            _context.Set<T>().Add(entity);
        }
        #endregion ADD

        #region Remove
        // ** Remove uma entidade de forma assíncrona definitivo.
        public async Task HardRemoveAsync<T>(T entity) where T : FacilityEntityId
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        // ** Deleta algo do banco definitivo.
        public void HardRemove<T>(T entity) where T : FacilityEntityId
        {
            _context.Set<T>().Remove(entity);
        }

        // ** Aqui só marca o bool isDelet como true.
        public void SoftRemove<T>(T entity) where T : FacilityEntityId
        {
            entity.IsDeleted = true;
            _context.Set<T>().Update(entity);
        }

        #endregion Remove

        #region Update
        // ** Atualiza uma entidade.
        public void Update<T>(T entity) where T : FacilityEntityId
        {
            _context.Set<T>().Update(entity);
        }

        // **  Atualiza uma entidade de forma assíncrona.
        public async Task UpdateAsync<T>(T entity) where T : FacilityEntityId
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
        #endregion Update

        #region Gets
        // ** Obtém entidades de forma assíncrona com base em um filtro
        public async Task<IEnumerable<T>> GetAsync<T>(Expression<Func<T, bool>> filterExpression = null) where T : FacilityEntityId
        {
            if (filterExpression == null)
            {
                return await _context.Set<T>().ToListAsync();
            }
            return await _context.Set<T>().Where(filterExpression).ToListAsync();
        }

        // ** Obtém entidades.
        public IQueryable<T> Get<T>() where T : FacilityEntityId
        {
            return _context.Set<T>().AsQueryable();
        }

        #endregion Gets

        #region Querys
        // ** Consulta entidades com base em um filtro
        public IQueryable<T> Where<T>(Expression<Func<T, bool>> filterExpression = null) where T : FacilityEntityId
        {
            if (filterExpression == null)
            {
                return _context.Set<T>();
            }
            return _context.Set<T>().Where(filterExpression);
        }

        // ** Inclui entidades relacionadas na consulta.
        public IIncludableQueryable<T, K> With<T, K>(Expression<Func<T, K>> expression) where T : FacilityEntityId
        {
            return _context.Set<T>().Include(expression);
        }

        // ** Obtém uma entidade com entidades relacionadas incluídas.
        public T With<T, K>(Expression<Func<T, K>> expression, Guid id) where T : FacilityEntityId
        {
            var entity = _context.Set<T>().Include(expression).FirstOrDefault(e => EF.Property<Guid>(e, "Id") == id);
            if (entity == null)
            {
                throw new Exception($"{typeof(T).Name} com ID {id} não encntrado.");
            }
            return entity;
        }

        // ** Verifica se alguma entidade satisfaz um filtro específico
        public bool Any<T>(Expression<Func<T, bool>> expression) where T : FacilityEntityId
        {
            return _context.Set<T>().Any(expression);
        }

        // **  Pesquisa entidades por nome usando distância de Levenshtein.
        public async Task<IEnumerable<T>> GetName<T>(string propriedadeNome, string valor) where T : FacilityEntityId
        {
            // ** Obtém todas as propriedades públicas e de instância do tipo T
            var propriedades = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // ** Encontra a primeira propriedade cujo nome tenha uma distância de Levenshtein de no máximo 2 em relação ao nome fornecido.
            var propriedade = propriedades.FirstOrDefault(p => Distancia(p.Name.ToLower(), propriedadeNome.ToLower()) <= 2);

            if (propriedade == null) 
                throw new Exception("Propriedade não encontrada.");

            // **  Cria um parâmetro de expressão para representar a entidade do tipo T na expressão lambda
            var parametro = Expression.Parameter(typeof(T), "x");

            // ** Acessa a propriedade específica da entidade com base no parâmetro criado
            var member = Expression.Property(parametro, propriedade.Name);

            // ** Cria uma constante com o valor de pesquisa formatado como uma expressão "LIKE" do SQL
            var constant = Expression.Constant($"%{valor}%", typeof(string));

            // ** Obtém o método "Like" da classe DbFunctionsExtensions, que é usado para gerar a expressão "LIKE" no SQL
            var metodo = typeof(DbFunctionsExtensions).GetMethod("Like", new[] { typeof(DbFunctions), typeof(string), typeof(string) });

            // ** Cria uma chamada de método para o método "Like" usando EF.Functions, a propriedade e o valor constante de pesquisa
            var like = Expression.Call(null, metodo, Expression.Constant(EF.Functions), member, constant);

            // ** Cria uma expressão lambda que representa a expressão "LIKE" a ser usada no método Where do LINQ
            var lambda = Expression.Lambda<Func<T, bool>>(like, parametro);

            // ** Executa a consulta no banco de dados, aplicando a expressão "LIKE" e retorna a lista de resultados.
            return await _context.Set<T>().Where(lambda).ToListAsync();
        }

        // ** Calcula a distância de Levenshtein entre duas strings.
        private int Distancia(string a, string b)
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
