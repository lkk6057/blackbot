using System;
using System.Collections.Generic;
using System.Text;

namespace BlackBot.Modules
{
    [Serializable]
    public class User
    {
        public string username;
        public int points;
        public User(string user, int bp)
        {
            username = user;
            points = bp;
        }
    }
}
