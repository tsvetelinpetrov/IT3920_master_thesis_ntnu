using System.Collections.Generic;

[System.Serializable]
public class Current
{
    public Measurement Measurements { get; set; }
    public Controls Controls { get; set; }
    public List<Disruptive> Disruptive { get; set; }
}
