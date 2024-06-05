using MongoDB.Driver;

namespace FacilityCore.API.Banco_de_dados.Data.MongoDB
{
    // ** Herdando a classe para se conectar ao MongoDB.
    public class FacilityMongoDbContext : MongoClient
    {
        // ** Campo privado para armazenar o nome do banco de dados.
        public readonly string _data;

        // ** Construtor que recebe um cliente MongoClient e o nome do banco de dados;
        // ** Chama o construtor da classe base (MongoClient) passando as configurações do cliente e atribui o nome do banco de dados ao _data;
        public FacilityMongoDbContext(MongoClient client, string dbContext) : base(client.Settings) => this._data = dbContext;

        // ** Chamando o método GetDatabase do objeto MongoClient para obter o banco de dados.
        public IMongoDatabase Db => GetDatabase(_data);
    }      
 }
