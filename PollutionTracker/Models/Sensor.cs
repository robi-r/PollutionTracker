using PollutionTracker.Models;

public class Sensor
{
    public int SensorID { get; set; } // Primary Key
    public string SensorType { get; set; }
    public string ModelNumber { get; set; }
    public int AreaID { get; set; } // Foreign Key for Area
    public Area Area { get; set; } // Navigation property for Area

    public int? AlertThresholdID { get; set; } // Foreign Key for AlertThreshold (nullable)
    public AlertThreshold AlertThreshold { get; set; } // Navigation property for AlertThreshold

    public ICollection<Pollution> Pollutions { get; set; } // Navigation property for Pollutions
}
