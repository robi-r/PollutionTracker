namespace PollutionTracker.Models
{
    public class Area
    {
        public int AreaID { get; set; }
        public string AreaName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Sensor> Sensors { get; set; }
    }
}
