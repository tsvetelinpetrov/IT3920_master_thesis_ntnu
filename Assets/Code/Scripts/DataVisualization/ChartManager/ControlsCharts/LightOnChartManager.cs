using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XCharts.Runtime;

public class LightOnChartManager : ChartManagerBase<List<Controls>>
{
    public override void UpdateChart(List<Controls> data)
    {
        var lightChart = gameObject.GetComponent<LineChart>();
        if (lightChart == null)
        {
            lightChart = gameObject.AddComponent<LineChart>();
            lightChart.Init();

            // Configure as step line for boolean values
            var serie = lightChart.GetSerie(0);
            if (serie != null)
            {
                serie.lineType = LineType.StepEnd;
            }
        }
        lightChart.ClearData();

        // Sort controls by time
        data = data.OrderBy(c => c.MeasurementTime).ToList();

        Dictionary<string, List<int>> dayIndices = new Dictionary<string, List<int>>();

        for (int i = 0; i < data.Count; i++)
        {
            string day = data[i].MeasurementTime.ToString("yyyy-MM-dd");
            if (!dayIndices.ContainsKey(day))
                dayIndices[day] = new List<int>();
            dayIndices[day].Add(i);

            // X-Axis: Time in hours and minutes
            string timeLabel = data[i].MeasurementTime.ToString("HH:mm");
            lightChart.AddXAxisData(timeLabel);

            // Y-Axis: LightOn boolean represented as 0 or 1
            lightChart.AddData(0, data[i].LightOn ? 1 : 0);
        }

        // Add day labels at the middle of each day's data
        foreach (var dayEntry in dayIndices)
        {
            int middleIndex = dayEntry.Value[dayEntry.Value.Count / 2]; // Middle index
            lightChart.AddXAxisData(dayEntry.Key, middleIndex);
        }
    }
}
