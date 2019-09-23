namespace OrienteeringTvResults.Model.Configuration
{
    public class DatabaseConfiguration
    {
        public string Server { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public int Competition { get; set; }
        public int Stage { get; set; }
        public double PollWaitTime { get; set; }
    }
}
