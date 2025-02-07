using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XCharts.Runtime;

public class CO2ChartManager : ChartManagerBase<List<Measurement>>
{
    public override void UpdateChart(List<Measurement> data)
    {
        var co2Chart = gameObject.GetComponent<LineChart>();
        if (co2Chart == null)
        {
            co2Chart = gameObject.AddComponent<LineChart>();
            co2Chart.Init();
        }
        co2Chart.ClearData();

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
            co2Chart.AddXAxisData(timeLabel);

            // Y-Axis: CO2
            co2Chart.AddData(0, data[i].CO2);
        }

        // Add day labels at the middle of each day's data
        foreach (var dayEntry in dayIndices)
        {
            int middleIndex = dayEntry.Value[dayEntry.Value.Count / 2]; // Middle index
            co2Chart.AddXAxisData(dayEntry.Key, middleIndex);
        }
    }
}
