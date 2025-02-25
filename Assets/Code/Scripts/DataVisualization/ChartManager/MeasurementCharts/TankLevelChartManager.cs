using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XCharts.Runtime;

public class TankLevelChartManager : ChartManagerBase<List<Measurement>>
{
    public override void UpdateChart(List<Measurement> data)
    {
        var tankLevelChart = gameObject.GetComponent<LineChart>();
        if (tankLevelChart == null)
        {
            tankLevelChart = gameObject.AddComponent<LineChart>();
            tankLevelChart.Init();
        }
        tankLevelChart.ClearData();

        Dictionary<string, List<int>> dayIndices = new Dictionary<string, List<int>>();

        for (int i = 0; i < data.Count; i++)
        {
            string day = data[i].MeasurementTime.ToString("yyyy-MM-dd"); // Extract day part
            if (!dayIndices.ContainsKey(day))
                dayIndices[day] = new List<int>();
            dayIndices[day].Add(i);

            // X-Axis: Time in hours and minutes
            string timeLabel = data[i].MeasurementTime.ToString("HH:mm");
            tankLevelChart.AddXAxisData(timeLabel);

            // Y-Axis: Tank Level
            tankLevelChart.AddData(0, data[i].TankLevel);
        }

        // Get the YAxis component
        YAxis yAxis = tankLevelChart.EnsureChartComponent<YAxis>();

        // Set to auto min/max which calculates appropriate values
        yAxis.minMaxType = Axis.AxisMinMaxType.MinMaxAuto;

        // Add day labels at the middle of each day's data
        foreach (var dayEntry in dayIndices)
        {
            int middleIndex = dayEntry.Value[dayEntry.Value.Count / 2]; // Middle index
            tankLevelChart.AddXAxisData(dayEntry.Key, middleIndex);
        }
    }
}
