namespace GraphQLDemoBase.Authorization
{
    public class TokenInfo
    {
        public string JWTIssuer { get; set; }
        public string JWTAudience { get; set; }
        public string JWTSecretKey { get; set; }
        public int JWTExpiration { get; set; }
    }
}
