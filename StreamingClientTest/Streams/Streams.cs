namespace StreamingClientTest.Streams
{
    public enum ApiStream
    {
        CompanyProfile,
        Officers,
        PersonsWithSignificantControl
    }

    public static class ApiStreamExtensions
    {
        public static string GetUrl(this ApiStream apiStream)
        {
            return apiStream switch
            {
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
                ApiStream.CompanyProfile => "CompanyProfile",
                ApiStream.Officers => "Officers",
                ApiStream.PersonsWithSignificantControl => "PersonsWithSignificantControl",
                _ => string.Empty
            };
        }
    }
}
