using System.Reflection.Emit;
using Discord;
using Nanasaki.Common;

namespace Nanasaki.Embeds;

public class UserEmbedBuilder : NanasakiEmbedBuilder
{
    public UserEmbedBuilder()
    {

    }

    public UserEmbedBuilder WithHelpCommands()
    {
        this.AddField("-user register <username>", "Create an account (1 max). <username> parameter is optional.")
            .AddField("-user change <new username>", "Change your username."); 
        
        return this; 
    }
}