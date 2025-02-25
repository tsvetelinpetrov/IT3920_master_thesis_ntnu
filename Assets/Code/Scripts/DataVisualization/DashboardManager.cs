using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashboardManager : MonoBehaviour
{
    [SerializeField]
    private ChartManagerBase<List<Measurement>>[] measurementCharts;

    [SerializeField]
    private ChartManagerBase<List<Controls>>[] controlCharts;

    [SerializeField]
    private ChartManagerBase<List<Disruptive>>[] disruptiveCharts;

    void Start()
    {
        FetchAndDisplayMeasurements();
        FetchAndDisplayControls();
    }

    private void FetchAndDisplayMeasurements()
    {
        IDataSource dataSource = DataSourceFactory.GetDataSource();

        dataSource.GetMeasurementsByDays(
            1,
            (measurement) =>
            {
                foreach (var chartManager in measurementCharts)
                {
                    chartManager.UpdateChart(measurement);
                }
            },
            (error) =>
            {
                Debug.LogError($"Failed to get current data: {error}");
            }
        );
    }

    private void FetchAndDisplayControls()
    {
        IDataSource dataSource = DataSourceFactory.GetDataSource();

        dataSource.GetControlsByDays(
            1,
            (Controls) =>
            {
                foreach (var chartManager in controlCharts)
                {
                    chartManager.UpdateChart(Controls);
                }
            },
            (error) =>
            {
                Debug.LogError($"Failed to get current data: {error}");
            }
        );
    }
};
