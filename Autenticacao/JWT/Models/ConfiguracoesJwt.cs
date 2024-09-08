namespace FacilityCore.API.Autenticacao.JWT.Models
{
    public class ConfiguracoesJwt
    {
        public string? Secret { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public int Expiracao { get; set; }
    }
}
