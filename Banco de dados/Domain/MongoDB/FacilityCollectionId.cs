namespace FacilityCore.API.Banco_de_dados.Domain.MongoDB
{
    public class FacilityCollectionId
    {
        // ** Id do registro.
        public Guid Id { get; set; }
        
        // ** Se está deletado.
        public bool? IsDeleted { get; set; }
        
        // ** Quer criou o registro.
        public Guid? InsertBy { get; set; }
        
        // ** Quem atualizou na última vez.
        public Guid? UpdateBy { get; set; }
        
        // ** Data de criação.
        public DateTime? InsertDate { get; set; }
        
        // ** Data de Atualização.
        public DateTime? UpdateDate { get; set; }
    }
}
