using System.Collections.Generic;

public interface IDataSource
{
    void GetPlantObjModel(System.Action<string> callback, bool highPoly = false);
    void GetControlsByDays(int days, System.Action<List<Controls>> callback);
}