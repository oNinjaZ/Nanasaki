using FluentValidation.Results;
using Nanasaki.Embeds;

namespace Nanasaki.Mappers;

public static class ValidationExtensions
{
    public static UserEmbedBuilder MapErrorsToUserEmbed(this ValidationResult validation)
    {   
        var embed = new UserEmbedBuilder();
        embed.WithFooter("Error");
        foreach (var error in validation.Errors)
        {
            embed.AddField($"{error.PropertyName}", error.ErrorMessage);
        }
        return embed;
    }
}