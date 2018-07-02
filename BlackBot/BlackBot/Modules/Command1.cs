using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Discord;
using System.Linq;

namespace BlackBot.Modules
{
    public class Command1 : ModuleBase<SocketCommandContext>
    {
        public Program program = new Program();

        string[] admins = { "Lkklkklkk", "caulin_26546" };
        string[] commands = { "b!hi", "b!bye", "b!count", "b!echo {message}", "b!agree", "b!roll {max number}", "b!suck {object}","b!gay {object}","b!pixar","b!registerusers (admin only)", "b!setpoints {username} {amount} (admin only)","b!addpoints {username} {amount} (admin only)","b!givepoints {username} {amount}","b!balance {username}" };
        string[] pixar = { "Toy Story", "A Bug's Life", "Toy Story 2", "Monsters, Inc.", "Finding Nemo", "The Incredibles", "Cars", "Ratatouille", "WALL-E", "Up", "Toy Story 3", "Cars 2", "Brave", "Monsters University", "Inside Out", "The Good Dinosaur", "Finding Dory", "Cars 3", "Coco" };
        string helplist = "";
        List<string> usernames = new List<string>();
        [Command("hi")]
        public async Task hiAsync()
        {
            await ReplyAsync("Hello!");
        }
        [Command("bye")]
        public async Task byeAsync()
        {
            await ReplyAsync("Goodbye!");
        }
        [Command("count")]
        public async Task countAsync()
        {
            for (int i = 1; i < 6; i++)
            {
                await ReplyAsync(i.ToString());
                Thread.Sleep(200);
            }
        }
        [Command("echo")]
        public async Task echoAsync([Remainder]string echo)
        {
            if (Context.User.Username == "Lkklkklkk" || Context.User.Username == "caulin_26546")
            {
                await ReplyAsync(echo);
            }
            else
            {
                await ReplyAsync($"No echo permission, {Context.User.Username}.");
            }
        }
        [Command("agree")]
        public async Task echoAsync()
        {
            if (Context.User.Username == "Lkklkklkk")
            {
                await ReplyAsync($"I agree with {Context.User.Username}.");
            }
            else
            {
                await ReplyAsync($"Fuck off {Context.User.Username}.");
            }
        }
        [Command("roll")]
        public async Task rollAsync(int range)
        {
            Random random = new Random();
            await ReplyAsync($"{Context.User.Username} rolled a {random.Next(0, range)} out of {range}");
        }
        [Command("suck")]
        public async Task suckAsync([Remainder]string thing)
        {
            await ReplyAsync($"*succs {thing}*");
        }
        [Command("help")]
        public async Task helpAsync()
        {
            await ReplyAsync("I can:");
            helplist = "";
            foreach (string command in commands)
            {
                helplist += command;
                helplist += "\n";
            }
            await ReplyAsync(helplist);

        }
        [Command("creator")]
        public async Task creatorAsync()
        {
            await ReplyAsync("I was created by Laurent (Lkklkklkk)");

        }
        [Command("gay")]
        public async Task gayAsync()
        {
            User purchaser = Program.data.Purchase(Context.User.Username,50);
            if (purchaser!=null)
            {
                Random random = new Random();
                usernames.Clear();
                foreach (var user in Context.Guild.Users)
                {
                    usernames.Add(user.Username);
                }
                await ReplyAsync(usernames.ElementAt(random.Next(0, usernames.Count)) + " has the big gay \n 50 BP has been removed from your account");
            }
            else
            {
                await ReplyAsync("Insufficient funds! (50)");
            }
        }
        [Command("pixar")]
        public async Task pixarAsync()
        {
            Random random = new Random();
            await ReplyAsync(pixar[random.Next(0, 19)]);
        }
        [Command("registerusers")]
        public async Task regusersAsync()
        {
            if (Context.User.Username == "Lkklkklkk")
            {
                Program.data.Load();
                string userlist = "";
                usernames.Clear();
                foreach (var user in Context.Guild.Users)
                {
                    usernames.Add(user.Username);
                }
                List<User> users = Program.data.registerusers(Context, usernames);
                foreach (User user in users)
                {
                    userlist += "\n";
                    userlist += user.username + ": " + user.points.ToString()+" BP";
                }
                await ReplyAsync("Registered Users:" + userlist);
            }
            else
            {
                await ReplyAsync("This command is restricted to admins only.");
            }
        }
        [Command("addpoints")]
        public async Task addpointsAsync(int amount,[Remainder]string username)
        {
            if (Context.User.Username == "Lkklkklkk")
            {
                User user = Program.data.AddPoints(username, amount);
                await ReplyAsync($"{amount}Blackpoints has been added to {user.username}, New: {user.points}BP");
            }
            else
            {
                await ReplyAsync("This command is restricted to admins only.");
            }

        }
        [Command("setpoints")]
        public async Task setpointsAsync(int amount, [Remainder]string username)
        {
            if (Context.User.Username == "Lkklkklkk" || Context.User.Username == "caulin_26546")
            {
                User user = Program.data.SetPoints(username, amount);
                await ReplyAsync($"{user.username}'s balance has been set to {amount} Blackpoints");
            }
            else
            {
                await ReplyAsync("This command is restricted to admins only.");
            }

        }
        [Command("balance")]
        public async Task balanceAsync([Remainder]string username)
        {
            User user = Program.data.UserBalance(username);
            if (user==null)
            {
                await ReplyAsync($"User '{username}' not found.");
            }
            else
            {
                await ReplyAsync($"{username} has a balance of {user.points} Blackpoints");
            }
        }
        [Command("givepoints")]
        public async Task givepointsAsync(int amount, [Remainder]string username)
        {
            if (amount>0) {
                User recipient = Program.data.givepoints(Context.User.Username, username, amount);
                if (recipient == null)
                {
                    await ReplyAsync("User(s) not found or insufficient funds.");
                }
                else
                {
                    await ReplyAsync($"{Context.User.Username} has given {amount} BP to {username}");
                }
            }
            else
            {
                await ReplyAsync("Invalid amount!");
            }
        }
    }
}
