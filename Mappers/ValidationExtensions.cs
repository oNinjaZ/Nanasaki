using System.Collections.Generic;
using FluentValidation.Results;
using Nanasaki.Embeds;
using Nanasaki.Models;

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

        public static UserEmbedBuilder MapAllToUserEmbed(this IEnumerable<User> list)
    {   
        var embed = new UserEmbedBuilder();
        foreach (var item in list)
        {
            embed.AddField($"{item.Username}", $"Since {item.RegistrationDate.ToShortDateString()}");
        }
        return embed;
    }
}