using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XCharts.Runtime;

public class ValveOpenChartManager : ChartManagerBase<List<Controls>>
{
    public override void UpdateChart(List<Controls> data)
    {
        var valveChart = gameObject.GetComponent<LineChart>();
        if (valveChart == null)
        {
            valveChart = gameObject.AddComponent<LineChart>();
            valveChart.Init();

            // Configure as step line for boolean values
            var serie = valveChart.GetSerie(0);
            if (serie != null)
            {
                serie.lineType = LineType.StepEnd;
            }
        }
        valveChart.ClearData();

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
            valveChart.AddXAxisData(timeLabel);

            // Y-Axis: ValveOpen boolean represented as 0 or 1
            valveChart.AddData(0, data[i].ValveOpen ? 1 : 0);
        }

        // Get the YAxis component
        YAxis yAxis = valveChart.EnsureChartComponent<YAxis>();

        // Set to auto min/max which calculates appropriate values
        yAxis.minMaxType = Axis.AxisMinMaxType.MinMaxAuto;
    }
}
