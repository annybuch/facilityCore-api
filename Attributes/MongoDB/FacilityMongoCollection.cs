namespace FacilityCore.API.Attributes.MongoDB
{
    /// <summary>
    /// Atributo para especificar o nome da coleção MongoDB associada a uma classe.
    /// Inherited: Define onde o atributo pode ser usado (classe) e impede herança.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class FacilityMongoCollection : Attribute
    {
        // ** Campo privado para armazenar o nome da coleção.
        private string _collectionName;

        /// <summary>
        /// Construtor que recebe o nome da coleção.
        /// </summary>
        /// <param name="collectionName">Nome da coleção MongoDB associada à classe.</param>
        public FacilityMongoCollection(string collectionName)
        {
            // ** Atribui o nome da coleção ao campo privado.
            _collectionName = collectionName;
        }

        // ** Propriedade pública que retorna o nome da coleção.
        public string CollectionName => _collectionName;
    }
}
