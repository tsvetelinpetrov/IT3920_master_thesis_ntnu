using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoomLight : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Indicates whether the room light can be toggled.")]
    public bool canToggleLight = true;

    [SerializeField]
    [Tooltip("Input action reference for toggling the room light.")]
    public InputActionReference toggleLightAction;

    private void Awake()
    {
        toggleLightAction.action.Enable();
        toggleLightAction.action.performed += ToggleLight;
    }

    private void OnDestroy()
    {
        toggleLightAction.action.performed -= ToggleLight;
        toggleLightAction.action.Disable();
    }

    private void ToggleLight(InputAction.CallbackContext context)
    {
        if (!canToggleLight)
            return;

        bool isLightOn = this.gameObject.activeSelf;
        this.gameObject.SetActive(!isLightOn);
    }
}
