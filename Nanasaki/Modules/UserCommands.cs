using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Nanasaki.Common;
using Nanasaki.Embeds;
using Nanasaki.Models;
using Nanasaki.Services;

namespace Nanasaki.Modules;
public class UserCommands : ModuleBase<SocketCommandContext>
{
    private readonly IUserService _userService;

    public UserCommands(IUserService userService)
    {
        _userService = userService;
    }

    [Command("user register")]
    public async Task RegisterUser(string username = null)
    {
        var socketGuildUser = Context.User as SocketGuildUser;

        var existingUser = await _userService.GetUserByIdAsync(socketGuildUser.Id.ToString());
        if (existingUser is not null)
        {
            await Context.Channel.TriggerTypingAsync();
            await Context.Channel.SendMessageAsync($"Already Registered!");
            return;
        }

        if (username is null) username = socketGuildUser.Nickname;

        var userToRegister = new User
        {
            Id = socketGuildUser.Id.ToString(),
            Username = username,
            RegistrationDate = DateTime.UtcNow
        };

        var isCreated = await _userService.CreateAsync(userToRegister);

        await Context.Channel.TriggerTypingAsync();
        if (isCreated)
        {
            var embed = new NanasakiEmbedBuilder()
                .WithDescription($"Account `{username}` successfully registered!")
                .Build();
            await Context.Channel.TriggerTypingAsync();
            await this.ReplyAsync(embed: embed);
            return;
        }
        await Context.Channel.SendMessageAsync($"Error: failed to register"); //implement
    }

    [Command("user change")]
    public async Task ChangeUsername(string newUsername)
    {
        var socketGuildUser = Context.User as SocketGuildUser;
        var existingUser = await _userService.GetUserByIdAsync(socketGuildUser.Id.ToString());
        if (existingUser is null)
        {
            await Context.Channel.TriggerTypingAsync();
            await Context.Channel.SendMessageAsync("User not found. Register first with `-user register <username>`");
            return;
        }
        var id = socketGuildUser.Id.ToString();
        var isUpdated = await _userService.UpdateUsernameAsync(id, newUsername);
        if (isUpdated)
        {
            var embed = new NanasakiEmbedBuilder()
                .WithDescription($"Username updated to `{newUsername}`!")
                .Build();
            await Context.Channel.TriggerTypingAsync();
            await this.ReplyAsync(embed: embed);
            return;
        }
    }

    [Command("user delete")]
    [RequireOwner]
    public async Task DeleteUser(SocketGuildUser socketGuildUser = null)
    {
        if (socketGuildUser is null) socketGuildUser = Context.User as SocketGuildUser;

        var id = socketGuildUser.Id.ToString();
        var nickname = socketGuildUser.Nickname;

        var isDeleted = await _userService.DeleteAsync(id);

        await Context.Channel.TriggerTypingAsync();
        if (isDeleted)
        {
            var successEmbed = new NanasakiEmbedBuilder()
                .WithDescription($"Removed {nickname}'s account.")
                .Build();
            await this.ReplyAsync(embed: successEmbed);
            return;
        }
        var errorEmbed = new NanasakiEmbedBuilder()
            .WithDescription($"No existing account found for {nickname}")
            .Build();
        await this.ReplyAsync(embed: errorEmbed);
    }

    [Command("help user")]
    public async Task UserHelp()
    {
        var embed = new UserEmbedBuilder()
            .WithHelpCommands()
            .Build();
            
        await Context.Channel.TriggerTypingAsync();
        await this.ReplyAsync(embed: embed);
    }
}