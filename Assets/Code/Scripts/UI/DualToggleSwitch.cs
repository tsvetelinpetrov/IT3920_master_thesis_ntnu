using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DualToggleSwitch : MonoBehaviour
{
    public RectTransform selector;
    public HorizontalLayoutGroup selectorLaybotGroup;
    public TMPro.TMP_Text labelLeft;
    public TMPro.TMP_Text labelRight;
    public Color LeftBackgroundColor = new Color(0.3f, 0.3f, 0.35f); // Dark gray for OFF
    public Color RightBackgroundColor = new Color(0.1f, 0.8f, 0.1f); // Warm green for ON

    // Initial state of the toggle switch
    [SerializeField]
    [Tooltip("Initial state of the toggle switch (Left or Right)")]
    private ToggleInitialState initialState = ToggleInitialState.Left; // Default to left side

    public bool isDisabled = false;

    // Should we re-invoke the event when the toggle is already in the desired state?
    [SerializeField]
    [Tooltip(
        "Should we re-invoke the event when the toggle is already in the desired state? (E.g if the left is already selected and we click it again)"
    )]
    private bool reInvokeEvent = false; // Default to false

    // Events for Unity Inspector
    public UnityEvent OnLeftSelected;
    public UnityEvent OnRightSelected;

    private bool isLeftSelected = true;

    void Start()
    {
        // Initialize the toggle switch to the left side
        Toggle(initialState == ToggleInitialState.Left);

        // Call Disable() if the toggle is disabled by default
        if (isDisabled)
            Disable();
    }

    private void Toggle(bool left)
    {
        isLeftSelected = left;

        // Update selector background color
        selector.GetComponent<Image>().color = left ? LeftBackgroundColor : RightBackgroundColor;

        // Reverse selector position
        selectorLaybotGroup.reverseArrangement = !left;
    }

    /// <summary>
    /// Select the left side of the toggle switch.
    /// </summary>
    /// <remarks>
    /// This method is called when the left side of the toggle switch is selected.
    /// It updates the toggle state and invokes the OnLeftSelected event.
    /// </remarks>
    public void SelectLeft()
    {
        if ((isLeftSelected && !reInvokeEvent) || isDisabled)
            return; // Prevent re-invoking if already selected and reInvokeEvent is false
        Toggle(true);
        OnLeftSelected?.Invoke(); // Invoke the event for left selection
    }

    /// <summary>
    /// Select the right side of the toggle switch.
    /// </summary>
    /// <remarks>
    /// This method is called when the right side of the toggle switch is selected.
    /// It updates the toggle state and invokes the OnRightSelected event.
    /// </remarks>
    public void SelectRight()
    {
        if ((!isLeftSelected && !reInvokeEvent) || isDisabled)
            return; // Prevent re-invoking if already selected and reInvokeEvent is false
        Toggle(false);
        OnRightSelected?.Invoke(); // Invoke the event for right selection
    }

    /// <summary>
    /// Change the state of the toggle switch without invoking events.
    /// </summary>
    /// <param name="left">True to select the left side, false for the right side.</param>
    /// <remarks>
    /// This method changes the state of the toggle switch without invoking any events.
    /// It can be used to programmatically change the state of the toggle switch
    /// without triggering the OnLeftSelected or OnRightSelected events.
    /// </remarks>
    public void ChangeStateWithoutEventInvoke(bool left)
    {
        Toggle(left);
    }

    /// <summary>
    /// Check if the left side is selected.
    /// </summary>
    /// <remarks>
    /// This method returns true if the left side is selected, false otherwise.
    /// It can be used to determine the current state of the toggle switch.
    /// </remarks>
    public bool IsLeftSelected() => isLeftSelected;

    /// <summary>
    /// Disable the toggle switch.
    /// </summary>
    /// <remarks>
    /// This method disables the toggle switch, preventing any interaction with it.
    /// It can be used to temporarily disable the toggle switch in the UI.
    /// </remarks>
    public void Disable()
    {
        isDisabled = true;
        selector.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f); // Gray color for disabled state

        // Gray out the labels
        labelLeft.color = new Color(0.5f, 0.5f, 0.5f);
        labelRight.color = new Color(0.5f, 0.5f, 0.5f);
    }

    /// <summary>
    /// Enable the toggle switch.
    /// </summary>
    /// <remarks>
    /// This method enables the toggle switch, allowing interaction with it again.
    /// It can be used to re-enable the toggle switch after it has been disabled.
    /// </remarks>
    public void Enable()
    {
        isDisabled = false;
        selector.GetComponent<Image>().color = isLeftSelected
            ? LeftBackgroundColor
            : RightBackgroundColor;

        // Reset the labels to their original colors
        labelLeft.color = Color.black;
        labelRight.color = Color.black;
    }

    private enum ToggleInitialState
    {
        Left,
        Right,
    }
}
