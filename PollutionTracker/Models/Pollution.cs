namespace PollutionTracker.Models
{
    public class Pollution
    {
        public int PollutionID { get; set; }
        public int SensorID { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double LPG_Isobutane { get; set; }
        public double CarbonMonoxide { get; set; }
        public double Hydrogen { get; set; }
        public double CO2 { get; set; }
        public double NH3 { get; set; }
        public DateTime RecordedAt { get; set; }

        public Sensor Sensor { get; set; }


    }
}
