using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Nanasaki.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanasaki.Modules
{

	/// <summary>
	/// The general module containing commands
	/// </summary>
	public class General : ModuleBase<SocketCommandContext>
	{

		/// <summary>
		/// Responds with "Pong!".
		/// </summary>
		/// <returns></returns>
		[Command("ping")]
		public async Task PingAsync()
		{
			await Context.Channel.TriggerTypingAsync();
			await Context.Channel.SendMessageAsync("Pong!");
		}

        [Command("help")]
        public async Task HelpAsync()
        {
            await Context.Channel.TriggerTypingAsync();
            await Context.Channel.SendMessageAsync("fu");
        }

		/// <summary>
		/// Gets information about a user.
		/// </summary>
		/// <param name="socketGuildUser">An optional user to get the information from.</param>
		/// <returns></returns>
		[Command("info")]
		public async Task InfoAsync(SocketGuildUser socketGuildUser = null)
		{
			await Context.Channel.TriggerTypingAsync();
			if (socketGuildUser == null)
			{
				socketGuildUser = Context.User as SocketGuildUser;
			}

			var embed = new NanasakiEmbedBuilder()
				.WithTitle($"{socketGuildUser.Username}#{socketGuildUser.Discriminator}")
				.AddField("ID", socketGuildUser.Id, true)
				.AddField("Name", $"{socketGuildUser.Username}#{socketGuildUser.Discriminator}", true)
				.AddField("Created at", socketGuildUser.CreatedAt.ToString("yyyy年M月d日"), true)
				.WithThumbnailUrl(socketGuildUser.GetAvatarUrl() ?? socketGuildUser.GetDefaultAvatarUrl())
				.WithCurrentTimestamp()
				.Build();

			await this.ReplyAsync(embed: embed);
		}
	}
}
