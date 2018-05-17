using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Discord;

namespace BlackBot.Modules
{
    public class Command1 : ModuleBase<SocketCommandContext>
    {
        
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
            if (Context.User.Username == "Lkklkklkk"||Context.User.Username == "caulin_26546")
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
            if (Context.User.Username=="Lkklkklkk")
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
            await ReplyAsync($"{Context.User.Username} rolled a {random.Next(0,range)} out of {range}");
        }

    }
}
