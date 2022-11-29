namespace GraphQLDemoBase.Authorization
{
    public interface ITokenManager
    {
        bool ValidateCurrentToken(string token);
        string GenerateToken(int userId);
    }
}