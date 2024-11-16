public class ControlsEventsModule
{
    // Defining the delegates for the events
    public delegate void ControlsEvent();

    // Defining the events
    public event ControlsEvent OnTurnOnLights;
    public event ControlsEvent OnTurnOffLights;
    public event ControlsEvent OnTurnOnUpperFan;
    public event ControlsEvent OnTurnOffUpperFan;
    public event ControlsEvent OnTurnOnLowerFan;
    public event ControlsEvent OnTurnOffLowerFan;

    /// <summary>
    /// Invoking this event will turn on the lights in the scene. It will also change the lights status in GlobalSettings to true.
    /// </summary>
    public void TurnOnLights()
    {
        OnTurnOnLights?.Invoke();
    }

    /// <summary>
    /// Invoking this event will turn off the lights in the scene. It will also change the lights status in GlobalSettings to false.
    /// </summary>
    public void TurnOffLights()
    {
        OnTurnOffLights?.Invoke();
    }

    public void TurnOnUpperFan()
    {
        OnTurnOnUpperFan?.Invoke();
    }

    public void TurnOffUpperFan()
    {
        OnTurnOffUpperFan?.Invoke();
    }

    public void TurnOnLowerFan()
    {
        OnTurnOnLowerFan?.Invoke();
    }

    public void TurnOffLowerFan()
    {
        OnTurnOffLowerFan?.Invoke();
    }
}
