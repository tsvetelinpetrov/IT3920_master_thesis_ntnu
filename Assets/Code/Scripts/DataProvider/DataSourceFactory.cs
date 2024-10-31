public class DataSourceFactory
{
    public static IDataSource GetDataSource()
    {
        return GlobalSettings.Instance.DataSourceType switch
        {
            DataSourceType.Api => new ApiSource(),
            DataSourceType.File => new FileSource(),
            _ => throw new System.Exception("Invalid data source type")
        };
    }
}