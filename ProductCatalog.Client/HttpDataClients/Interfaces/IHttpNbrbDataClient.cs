namespace ProductCatalog.Client.HttpDataClients.Interfaces
{
    public interface IHttpNbrbDataClient
    {
        Task<decimal> GetExchangeRateAsync();
    }
}
