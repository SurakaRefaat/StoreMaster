namespace Store.Service.Services.CaheService
{
    public interface IcashService
    {
        Task SetCashResponseAsync(string Key, object response, TimeSpan TimeToLive);

        Task<string> GetCashResponeAsync(string Key);
    }
}
