public class EventCenter
{
    // Event Modules
    private static ControlsEventsModule _controlsEventsModule = new ControlsEventsModule();
    private static MeasurementsEventsModule _measurementsEventsModule =
        new MeasurementsEventsModule();

    // Event Modules Accessors

    /// <summary>
    /// Controls all the events related to the controls (Fans, Lights, etc.) in the scene.
    /// </summary>
    public static ControlsEventsModule Controls => _controlsEventsModule;

    /// <summary>
    /// Controls all the events related to the measurements (Temperature, Humidity, etc.) in the scene.
    /// </summary>
    public static MeasurementsEventsModule Measurements => _measurementsEventsModule;
}
