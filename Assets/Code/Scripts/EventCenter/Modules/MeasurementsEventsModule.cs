public class MeasurementsEventsModule
{
    // Defining the delegates for the events
    public delegate void MeasurementsChangeEvent(Measurement measurement);
    public delegate void MeasurementsRefreshingStatusEvent(bool refreshing);

    // Defining the events
    public event MeasurementsChangeEvent OnMeasurementsChange;
    public event MeasurementsRefreshingStatusEvent OnMeasurementsRefreshingStatusChange;

    /// <summary>
    /// Invoking this event will change the measurements in the scene.
    /// </summary>
    /// <param name="measurement">The new measurements to be set</param>
    /// <returns></returns>
    /// <remarks>
    /// This event is used to change the measurements in the scene.
    /// </remarks>
    public void ChangeMeasurements(Measurement measurement)
    {
        OnMeasurementsChange?.Invoke(measurement);
    }

    /// <summary>
    /// Invoking this event will change the refreshing status in the scene.
    /// </summary>
    /// <param name="refreshing">The new refreshing status to be set</param>
    /// <returns></returns>
    /// <remarks>
    /// This event is used to change the refreshing status in the scene.
    /// </remarks>
    public void ChangeRefreshingStatus(bool refreshing)
    {
        OnMeasurementsRefreshingStatusChange?.Invoke(refreshing);
    }
}
