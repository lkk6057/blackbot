﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Threading;
using BlackBot.Modules;

namespace BlackBot
{
    public class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;
        private readonly Func<SocketMessage, Task> source;
        public static DataLoad data = new DataLoad();
        public async Task RunBotAsync()
        {
            data.Load();
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();
            string botToken = "[REDACTED]";
            _client.Log += Log;
            _client.UserJoined += AnnounceUserJoined;
            await RegisterCommandsAsync();
            await _client.LoginAsync(Discord.TokenType.Bot, botToken);
            await _client.StartAsync();
            await Task.Delay(-1);
        }

        private async Task AnnounceUserJoined(SocketGuildUser user)
        {
            var guild = user.Guild;
            var channel = guild.DefaultChannel;
            await channel.SendMessageAsync($"Welcome to the discord, {user.Mention}");
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }
        public async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            if (message is null || message.Author.IsBot)
            {
                return;
            }
            int argPos = 0;
            var context2 = new SocketCommandContext(_client, message);
            Console.WriteLine(context2.Channel.Id);
            if (message.Channel.Id == 446817453868318743)
            {
                await ((ISocketMessageChannel)_client.GetChannel(446797500859285527)).SendMessageAsync(message.Content);
            }
            if (message.HasStringPrefix("b!", ref argPos)||message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(_client, message);
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess)
                {
                    Console.WriteLine(result.ErrorReason);
                }
                
            }

        }
        static void OnProcessExit(object sender, EventArgs e)
        {
            Console.WriteLine("Saving data");
            data.Save();
        }
    }
}
