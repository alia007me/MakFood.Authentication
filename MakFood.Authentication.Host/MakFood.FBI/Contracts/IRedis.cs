namespace MakFood.FBI.Contracts
{
    public interface IRedis
    {
        Task<bool> CheckKeyExistanceInRedis(string key);

    }
}
