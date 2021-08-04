namespace CassandraAPI.Models
{
    public class MobileHomeResponse
    {
        public UserEntity UserInfo { get; set; }
        public double TotalCarbon { get; set; }
        public double TotalEarn { get; set; }
        public double TodayCarbon { get; set; }
        public double TodayEarn { get; set; }
        public double AvgCarbon { get; set; }
        public double TotalTime { get; set; }

    }
}