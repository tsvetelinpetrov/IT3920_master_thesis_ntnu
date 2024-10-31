public class DataSourceFactory
{
    public static IDataSource GetDataSource(DataSourceType dataSourceType, string dataSource)
    {
        return dataSourceType switch
        {
            DataSourceType.Api => new ApiSource(dataSource),
            DataSourceType.File => new FileSource(dataSource),
            _ => throw new System.Exception("Invalid data source type")
        };
    }
}