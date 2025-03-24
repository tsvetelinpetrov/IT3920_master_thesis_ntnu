public class AirflowEventsModule
{
    // Defining the delegates for the events
    public delegate void GenericAirflowEvent();
    public delegate void NewAirflowDataEvent(Airflow airflow);

    // Defining the events
    public event NewAirflowDataEvent OnNewAirflowData;

    /// <summary>
    /// Invoking this event will change the airflow data in the scene.
    /// </summary>
    /// <param name="airflow">The new airflow data to be set</param>
    public void NewAirflowData(Airflow airflow)
    {
        OnNewAirflowData?.Invoke(airflow);
    }
}
