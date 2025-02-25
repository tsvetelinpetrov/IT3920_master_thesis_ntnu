using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XCharts.Runtime;

public class FanOnChartManager : ChartManagerBase<List<Controls>>
{
    public override void UpdateChart(List<Controls> data)
    {
        var fanChart = gameObject.GetComponent<LineChart>();
        if (fanChart == null)
        {
            fanChart = gameObject.AddComponent<LineChart>();
            fanChart.Init();

            // Configure as step line for boolean values
            var serie = fanChart.GetSerie(0);
            if (serie != null)
            {
                serie.lineType = LineType.StepEnd;
            }
        }
        fanChart.ClearData();

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
            fanChart.AddXAxisData(timeLabel);

            // Y-Axis: FanOn boolean represented as 0 or 1
            fanChart.AddData(0, data[i].FanOn ? 1 : 0);
        }

        // Add day labels at the middle of each day's data
        foreach (var dayEntry in dayIndices)
        {
            int middleIndex = dayEntry.Value[dayEntry.Value.Count / 2]; // Middle index
            fanChart.AddXAxisData(dayEntry.Key, middleIndex);
        }
    }
}
