namespace DataServeFunction.Configuration
{
    public class ServiceConfiguration
    {
        public string? StorageAccountConnectionString { get; set; }

        public ServiceConfiguration(string StorageAccountConnectionString)
        {
            this.StorageAccountConnectionString = StorageAccountConnectionString;
        }
    }
}
