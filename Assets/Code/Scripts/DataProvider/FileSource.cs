using System.Collections.Generic;
using System.IO;

public struct FilesLocations
{
    // TODO: Add the file paths
}

public class FileSource : IDataSource
{
    public FileSource() {}

    public void GetPlantObjModel(System.Action<string> callback, bool highPoly)
    {
        // TODO: Implement this method
        throw new System.NotImplementedException();
    }

    public void GetControlsByDays(int days, System.Action<List<Controls>> callback)
    {
        // TODO: Implement this method
        throw new System.NotImplementedException();
    }
}