using System;

namespace LayoutRestService.Models.Configuration
{
    public class DatabaseConfiguration
    {
        public string Server { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }

        public string GetConnectionString()
        {
            return $"Server={Server};Database={Database};User={Username};Password={Password};";
        }
    }
}
