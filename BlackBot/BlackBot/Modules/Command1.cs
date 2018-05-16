using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
    }
}
