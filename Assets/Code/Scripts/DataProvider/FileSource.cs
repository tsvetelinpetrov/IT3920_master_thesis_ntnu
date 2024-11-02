using System.Collections.Generic;
using System.IO;

/// <summary>
/// Structure for storing file paths.
/// </summary>
/// <remarks>
/// This structure is used to store the paths of the files that the FileSource class reads data from.
/// </remarks>
public struct FilesLocations
{
    // TODO: Add the file paths
}

/// <summary>
/// Data source for reading data from files.
/// </summary>
/// <remarks>
/// This class implements the IDataSource interface and provides methods for reading data from files.
/// </remarks>
public class FileSource : IDataSource
{
    public FileSource() { }

    public void GetPlantObjModel(
        System.Action<string> successCallback,
        bool highPoly,
        System.Action<string> errorCallback
    )
    {
        // TODO: Implement this method
        throw new System.NotImplementedException();
    }

    public void GetControlsByDays(
        int days,
        System.Action<List<Controls>> successCallback,
        System.Action<string> errorCallback
    )
    {
        // TODO: Implement this method
        throw new System.NotImplementedException();
    }
}
