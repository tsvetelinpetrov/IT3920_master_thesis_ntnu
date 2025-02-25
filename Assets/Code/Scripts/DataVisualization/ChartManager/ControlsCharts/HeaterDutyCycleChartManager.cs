using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XCharts.Runtime;

public class HeaterDutyCycleChartManager : ChartManagerBase<List<Controls>>
{
    public override void UpdateChart(List<Controls> data)
    {
        var heaterChart = gameObject.GetComponent<LineChart>();
        if (heaterChart == null)
        {
            heaterChart = gameObject.AddComponent<LineChart>();
            heaterChart.Init();
        }
        heaterChart.ClearData();

        Dictionary<string, List<int>> dayIndices = new Dictionary<string, List<int>>();

        for (int i = 0; i < data.Count; i++)
        {
            string day = data[i].MeasurementTime.ToString("yyyy-MM-dd"); // Extract day part
            if (!dayIndices.ContainsKey(day))
                dayIndices[day] = new List<int>();
            dayIndices[day].Add(i);

            // X-Axis: Time in hours and minutes
            string timeLabel = data[i].MeasurementTime.ToString("HH:mm");
            heaterChart.AddXAxisData(timeLabel);

            // Y-Axis: HeaterDutyCycle value
            heaterChart.AddData(0, data[i].HeaterDutyCycle);
        }

        // Add day labels at the middle of each day's data
        foreach (var dayEntry in dayIndices)
        {
            int middleIndex = dayEntry.Value[dayEntry.Value.Count / 2]; // Middle index
            heaterChart.AddXAxisData(dayEntry.Key, middleIndex);
        }
    }
}
