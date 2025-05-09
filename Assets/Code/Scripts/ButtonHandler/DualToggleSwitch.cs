using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DualToggleSwitch : MonoBehaviour
{
    private enum ToggleInitialState
    {
        Left,
        Right,
    }

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
        if (isLeftSelected && !reInvokeEvent)
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
        if (!isLeftSelected && !reInvokeEvent)
            return; // Prevent re-invoking if already selected and reInvokeEvent is false
        Toggle(false);
        OnRightSelected?.Invoke(); // Invoke the event for right selection
    }

    /// <summary>
    /// Check if the left side is selected.
    /// </summary>
    /// <remarks>
    /// This method returns true if the left side is selected, false otherwise.
    /// It can be used to determine the current state of the toggle switch.
    /// </remarks>
    public bool IsLeftSelected()
    {
        return isLeftSelected;
    }
}
