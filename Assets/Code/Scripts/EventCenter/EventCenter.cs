public class EventCenter
{
    // Event Modules
    private static ControlsEventsModule _controlsEventsModule = new ControlsEventsModule();

    // Event Modules Accessors

    /// <summary>
    /// Controls all the events related to the controls (Fans, Lights, etc.) in the scene.
    /// </summary>
    public static ControlsEventsModule Controls => _controlsEventsModule;
}
