namespace BuinessLogic
{
    public class WebsiteSettings : IWebSettings
    {
        public string Host { get; }
        public string Database { get; }
        public string User { get; }
        public string Password { get; }
        public string SfKey { get; }
        
        public string ConnectionString => $"Server={Host}; Database={Database}; Uid={User}; Pwd={Password};";

        public WebsiteSettings(string host,string database,string user,string password,string sfKey) 
        {
            Host = host;
            Database = database;
            User = user;
            Password = password;
            SfKey = sfKey;
        }



    }
}
