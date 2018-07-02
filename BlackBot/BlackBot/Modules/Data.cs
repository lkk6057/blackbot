using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Discord;
using Discord.Commands;

namespace BlackBot.Modules
{
    public class DataLoad
    {
        static Data datadata = new Data();
        static BinaryFormatter bf = new BinaryFormatter();
        public List<User> users = new List<User>();
        public void Save()
        {
            FileStream file = File.Create("C:/Users/lkk60/AppData/Roaming/BlackBot/userdata.dat");
            datadata.users = users;
            bf.Serialize(file,datadata);
            file.Close();
        }
        public void Load()
        {
            FileStream file = File.Open("C:/Users/lkk60/AppData/Roaming/BlackBot/userdata.dat", FileMode.Open);
            if (File.Exists("C:/Users/lkk60/AppData/Roaming/BlackBot/userdata.dat") &&file.Length>0)
            {
                datadata = (Data)bf.Deserialize(file);
                users = datadata.users;
            }
            file.Close();
        }
        public User AddPoints(string username, int amount)
        {
            if (users.Find(x=>x.username==username)==null)
            {
                RegisterUser(username,amount);
                return users.Find(x => x.username == username);
            }
            else
            {
                users.Find(x => x.username == username).points += amount;
                Save();
                return users.Find(x => x.username == username);
            }

        }
        public User SetPoints(string username, int amount)
        {
            if (users.Find(x => x.username == username) == null)
            {
                RegisterUser(username, amount);
                return users.Find(x => x.username == username);
            }
            else
            {
                users.Find(x => x.username == username).points = amount;
                Save();
                return users.Find(x => x.username == username);
            }

        }
        public User Purchase(string username, int amount)
        {
            if (users.Find(x => x.username == username) == null|| users.Find(x => x.username == username).points<amount)
            {
                if (users.Find(x => x.username == username) != null)
                {
                    return null;
                }
                else
                {
                    RegisterUser(username, 0);
                    return null;
                }
            }
            else
            {
                users.Find(x => x.username == username).points -= amount;
                Save();
                return users.Find(x => x.username == username);
            }
        }
        public User UserBalance(string username)
        {
            if (users.Find(x => x.username == username) == null)
            {
                return null;
            }
            else
            {
                return users.Find(x => x.username == username);
            }
        }
        public User givepoints(string giver,string recipient,int amount)
        {
            if (users.Find(x => x.username == recipient) == null|| users.Find(x => x.username == giver) == null|| users.Find(x => x.username == giver).points<amount)
            {
                return null;
            }
            else
            {
                users.Find(x => x.username == giver).points -= amount;
                users.Find(x => x.username == recipient).points += amount;
                Save();
                return users.Find(x => x.username == recipient);
            }
        }
        public List<User> registerusers(SocketCommandContext context,List<string> usernames)
        {
            foreach (string username in usernames)
            {
                if (users.Find(x => x.username == username)==null)
                {
                    RegisterUser(username,0);
                }
            }
            Save();
            return users;
        }
        public void RegisterUser(string user,int offset)
        {
            users.Add(new User(user,offset));
            Save();
        }
    }
    [Serializable]
    class Data
    {
        public List<User> users;
    }
}
