using System.Reflection.Emit;
using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Nanasaki.Common;
using Nanasaki.Embeds;
using Nanasaki.Mappers;
using Nanasaki.Models;
using Nanasaki.Services;
using Nanasaki.Validators;

namespace Nanasaki.Modules;
public class UserCommands : ModuleBase<SocketCommandContext>
{
    private readonly IUserService _userService;
    private readonly UserValidator _validator;

    public UserCommands(IUserService userService, UserValidator validator)
    {
        _userService = userService;
        _validator = validator;
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

        if (username is null) username = socketGuildUser.Username;

        var userToRegister = new User
        {
            Id = socketGuildUser.Id.ToString(),
            Username = username,
            RegistrationDate = DateTime.UtcNow
        };

        var validationResult = await _validator.ValidateAsync(userToRegister);
        if (!validationResult.IsValid)
        {
            var embed = validationResult.MapErrorsToUserEmbed()
                .Build();
            await Context.Channel.TriggerTypingAsync();
            await this.ReplyAsync(embed: embed);
            return;
        }

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

        var validationResult = await _validator.ValidateAsync(new User
        {
            Id = existingUser.Id,
            Username = newUsername,
            RegistrationDate = existingUser.RegistrationDate
        });

        if (!validationResult.IsValid)
        {
            var embed = validationResult.MapErrorsToUserEmbed()
                .Build();
            await Context.Channel.TriggerTypingAsync();
            await this.ReplyAsync(embed: embed);
            return;
        }

        var id = socketGuildUser.Id.ToString();
        var isUpdated = await _userService.UpdateUsernameAsync(id, newUsername);
        if (isUpdated)
        {
            var embed = new UserEmbedBuilder()
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
        var username = socketGuildUser.Username;

        var isDeleted = await _userService.DeleteAsync(id);

        await Context.Channel.TriggerTypingAsync();
        if (isDeleted)
        {
            var successEmbed = new NanasakiEmbedBuilder()
                .WithDescription($"Removed {username}'s account.")
                .Build();
            await this.ReplyAsync(embed: successEmbed);
            return;
        }
        var errorEmbed = new NanasakiEmbedBuilder()
            .WithDescription($"No existing account found for {username}")
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

    [Command("user get all")]
    public async Task GetAllUsers()
    {
        await Context.Channel.TriggerTypingAsync();
        var users = await _userService.GetAllAsync();
        var embed = users.MapAllToUserEmbed()
        .Build();
        await this.ReplyAsync(embed: embed);
    }

        [Command("user get")]
    public async Task GetUser(SocketGuildUser socketGuildUser)
    {
        await Context.Channel.TriggerTypingAsync();
        await this.ReplyAsync("`user get @user` triggered [test]");
    }
}