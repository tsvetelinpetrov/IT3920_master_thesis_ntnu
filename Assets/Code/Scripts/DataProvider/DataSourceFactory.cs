/// <summary>
/// Factory class for creating data sources.
/// </summary>
/// <remarks>
/// The data source type is set in the global settings and can be either API (ApiSource) or file (FileSource).
/// </remarks>
public class DataSourceFactory
{
    /// <summary>
    /// Returns the data source based on the global settings.
    /// </summary>
    /// <returns>The data source.</returns>
    /// <exception cref="System.Exception">Invalid data source type</exception>
    /// <remarks>
    /// The data source type is set in the global settings and can be either API (ApiSource) or file (FileSource).
    /// </remarks>
    public static IDataSource GetDataSource()
    {
        return GlobalSettings.Instance.DataSourceType switch
        {
            DataSourceType.Api => new ApiSource(),
            DataSourceType.File => new FileSource(),
            _ => throw new System.Exception("Invalid data source type"),
        };
    }
}
