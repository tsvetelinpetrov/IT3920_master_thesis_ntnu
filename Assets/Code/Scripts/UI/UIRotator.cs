using UnityEngine;

public class UIRotator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        // Main camera
        Transform playerCamera = Camera.main?.transform;

        if (playerCamera == null)
        {
            Camera mainCam = Camera.main;
            if (mainCam != null)
                playerCamera = mainCam.transform;
            else
                return;
        }

        Vector3 direction = transform.position - playerCamera.position; // ← reversed direction
        direction.y = 0; // Optional: keep the panel upright
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
