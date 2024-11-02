using System.Collections.Generic;
using System.IO;

public struct FilesLocations
{
    // TODO: Add the file paths
}

public class FileSource : IDataSource
{
    public FileSource() { }

    public void GetPlantObjModel(
        System.Action<string> callback,
        bool highPoly,
        System.Action<string> errorCallback
    )
    {
        // TODO: Implement this method
        throw new System.NotImplementedException();
    }

    public void GetControlsByDays(
        int days,
        System.Action<List<Controls>> callback,
        System.Action<string> errorCallback
    )
    {
        // TODO: Implement this method
        throw new System.NotImplementedException();
    }
}
