namespace StreamingClientTest.Streams
{
    public enum ApiStream
    {
        CompanyProfile,
        Officers,
        PersonsWithSignificantControl,
        Test,
        HistoryTest
    }

    public static class ApiStreamExtensions
    {
        public static string GetUrl(this ApiStream apiStream)
        {
            return apiStream switch
            {
                ApiStream.HistoryTest => "companies",
                ApiStream.Test => "stream/429",
                ApiStream.CompanyProfile => "companies",
                ApiStream.Officers => "officers",
                ApiStream.PersonsWithSignificantControl => "persons-with-significant-control",
                _ => string.Empty
            };
        }

        public static string GetTable(this ApiStream apiStream)
        {
            return apiStream switch
            {
                ApiStream.HistoryTest => "HistoryTest",
                ApiStream.Test => "Test",
                ApiStream.CompanyProfile => "CompanyProfile",
                ApiStream.Officers => "Officers",
                ApiStream.PersonsWithSignificantControl => "PersonsWithSignificantControl",
                _ => string.Empty
            };
        }

        public static string GetBaseUrl(this ApiStream apiStream)
        {
            return apiStream switch
            {
                ApiStream.Test => "https://localhost:5901",
                _ => "https://stream.companieshouse.gov.uk"
            };
        }
    }
}
