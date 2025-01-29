namespace PollutionTracker.Models
{
    public class AlertThreshold
    {
        public int? ThresholdID { get; set; } //Primary Key
        public string Parameter { get; set; }
        public double ThresholdValue { get; set; }
        public string SeverityLevel { get; set; }
        public DateTime CreatedAt { get; set; }

        public int AreaID { get; set; }  // Foreign key to Area
        public Area Area { get; set; }    // Navigation property to Area
    }
}
