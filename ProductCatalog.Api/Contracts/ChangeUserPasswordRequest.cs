namespace ProductCatalog.Api.Contracts
{
    public class ChangeUserPasswordRequest
    {
        public string Id { get; set; } = string.Empty;

        public string NewPassword { get; set; } = string.Empty;
    }
}
