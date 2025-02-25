using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XCharts.Runtime;

public class TemperatureChartManager : ChartManagerBase<List<Measurement>>
{
    public override void UpdateChart(List<Measurement> data)
    {
        var temperatureChart = gameObject.GetComponent<LineChart>();
        if (temperatureChart == null)
        {
            temperatureChart = gameObject.AddComponent<LineChart>();
            temperatureChart.Init();
        }
        temperatureChart.ClearData();

        Dictionary<string, List<int>> dayIndices = new Dictionary<string, List<int>>();

        for (int i = 0; i < data.Count; i++)
        {
            string day = data[i].MeasurementTime.ToString("yyyy-MM-dd"); // Extract day part
            if (!dayIndices.ContainsKey(day))
                dayIndices[day] = new List<int>();
            dayIndices[day].Add(i);

            // X-Axis: Time in hours and minutes
            string timeLabel = data[i].MeasurementTime.ToString("HH:mm");
            temperatureChart.AddXAxisData(timeLabel);

            // Y-Axis: Temperature
            temperatureChart.AddData(0, data[i].Temperature);
        }

        // Get the YAxis component
        YAxis yAxis = temperatureChart.EnsureChartComponent<YAxis>();

        // Set to auto min/max which calculates appropriate values
        yAxis.minMaxType = Axis.AxisMinMaxType.MinMaxAuto;
    }
}
