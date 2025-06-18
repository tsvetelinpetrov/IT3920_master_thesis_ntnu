using UnityEngine;

public class WorkingModeSwitch : MonoBehaviour
{
    DualToggleSwitch modeToggleSwitch;

    void Start()
    {
        modeToggleSwitch = GetComponent<DualToggleSwitch>();
        WorkingMode mode = SettingsManager.WorkingMode;

        if (mode == WorkingMode.Offline)
        {
            modeToggleSwitch?.SelectLeft();
        }
        else if (mode == WorkingMode.Online)
        {
            modeToggleSwitch?.SelectRight();
        }
    }

    public void ChangeWorkingMode(int mode)
    {
        SettingsManager.WorkingMode = (WorkingMode)mode;

        // Refresh the plant model, plant data, image, airflow, and charts
    }
}
