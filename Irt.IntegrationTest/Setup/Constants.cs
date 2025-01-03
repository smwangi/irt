namespace Irt.IntegrationTest.Setup
{
    public static class Constants
    {
        private const string BaseUrl = "http://localhost:5000";
        private const string BaseUrlHttps = "https://localhost:5001";
        private const string BaseUrlApi = "/irt/api/v1";
        // Route Constants
        public const string DatasourceRoute = $"{BaseUrlApi}/datasources";

        public const string DatasourceCollectionName = "Datasources";
        public const string DatasetCollectionName = "Datasets";
        public const string NameCollectionName = "Names";
    }
}