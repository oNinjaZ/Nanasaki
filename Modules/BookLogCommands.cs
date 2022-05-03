using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Nanasaki.Common;
using Nanasaki.Models;
using Nanasaki.Services;

namespace Nanasaki.Modules;

public class BookLogCommmands : ModuleBase<SocketCommandContext>
{
    private readonly IBookLogService _bookLogService;

    public BookLogCommmands(IBookLogService bookLogService)
    {
        _bookLogService = bookLogService;
    }

    [Command("book log")]
    public async Task AddLog(int number)
    {
        var socketGuildUser = Context.User as SocketGuildUser;
        var bookLog = new BookLog
        {
            Id = Guid.NewGuid(),
            User = socketGuildUser.Id.ToString(),
            PagesRead = number,
            LogDate = DateTime.UtcNow
        };
        await Context.Channel.TriggerTypingAsync();
        var isCreated = await _bookLogService.CreateAsync(bookLog);
        if (isCreated)
        {
            var embed = new NanasakiEmbedBuilder()
                .WithDescription($"`Log added!` [test template]")
                .Build();
            await this.ReplyAsync(embed: embed);
            return;
        }
        await this.ReplyAsync("Error");
    }

    [RequireOwner]
    [Command("book log delete")]
    public async Task AddLog(string id)
    {
        await Context.Channel.TriggerTypingAsync();
        var isDeleted = await _bookLogService.DeleteAsync(id);
        if (isDeleted)
        {
            var embed = new NanasakiEmbedBuilder()
                .WithDescription($"`Log Deleted!` [test template]")
                .WithColor(Discord.Color.Red)
                .Build();
            await this.ReplyAsync(embed: embed);
            return;
        }
        //todo
    }
}