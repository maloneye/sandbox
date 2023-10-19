using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuinessLogic
{
    public interface IWebSettings
    {
        public string Host { get; }
        public string Database { get; }
        public string User { get; }
        public string Password { get; }
        public string SfKey { get; }

        public string ConnectionString => $"Server={Host}; Database={Database}; Uid={User}; Pwd={Password};";
    }
}
