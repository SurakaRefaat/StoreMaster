namespace Store.Service.CaheSeervice
{
    public interface IcashService
    {
        Task SetCashResponseAsync(String Key, object response, TimeSpan TimeToLive);

        Task<string> GetCashResponeAsync(string Key);
    }
}
