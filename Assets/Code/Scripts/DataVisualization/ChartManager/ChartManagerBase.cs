using UnityEngine;

public abstract class ChartManagerBase<T> : MonoBehaviour
{
    public abstract void UpdateChart(T data);
}
