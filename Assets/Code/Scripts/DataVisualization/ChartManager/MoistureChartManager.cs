using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XCharts.Runtime;

public class MoistureChartManager : ChartManagerBase<List<Measurement>>
{
    public override void UpdateChart(List<Measurement> data)
    {
        var moistureChart = gameObject.GetComponent<LineChart>();
        if (moistureChart == null)
        {
            moistureChart = gameObject.AddComponent<LineChart>();
            moistureChart.Init();
        }
        moistureChart.ClearData();

        // Sort measurements by time (just in case)
        data = data.OrderBy(m => m.MeasurementTime).ToList();

        Dictionary<string, List<int>> dayIndices = new Dictionary<string, List<int>>();

        for (int i = 0; i < data.Count; i++)
        {
            string day = data[i].MeasurementTime.ToString("yyyy-MM-dd"); // Extract day part
            if (!dayIndices.ContainsKey(day))
                dayIndices[day] = new List<int>();
            dayIndices[day].Add(i);

            // X-Axis: Time in hours and minutes
            string timeLabel = data[i].MeasurementTime.ToString("HH:mm");
            moistureChart.AddXAxisData(timeLabel);

            // Y-Axis: Moisture
            moistureChart.AddData(0, data[i].Moisture);
        }

        // Add day labels at the middle of each day's data
        foreach (var dayEntry in dayIndices)
        {
            int middleIndex = dayEntry.Value[dayEntry.Value.Count / 2]; // Middle index
            moistureChart.AddXAxisData(dayEntry.Key, middleIndex);
        }
    }
}
