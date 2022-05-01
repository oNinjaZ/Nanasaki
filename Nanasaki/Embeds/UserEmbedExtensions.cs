namespace Nanasaki.Embeds;

public static class UserEmbedExtensions
{
    public static UserEmbedBuilder WithHelpCommands(this UserEmbedBuilder embed)
    {
        
        embed.AddField("-user register <username>", "Create an account (1 max). <username> parameter is optional.")
            .AddField("-user change <new username>", "Change your username.");  

        return embed;
    }
}