namespace School.Models.IRepository
{
    public interface ITokenServices
    {
        Task<Token> GetTokens(Login login);
    }
}
