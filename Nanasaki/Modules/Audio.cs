using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanasaki.Modules
{
    public class Audio : ModuleBase<SocketCommandContext>
    {
		[Command("join", RunMode = RunMode.Async)]
		public async Task JoinChannel(IVoiceChannel channel = null)
		{
			// Get the audio channel
			channel = channel ?? (Context.User as IGuildUser)?.VoiceChannel;
			if (channel == null) { await Context.Channel.SendMessageAsync("User must be in a voice channel, or a voice channel must be passed as an argument."); return; }

			// For the next step with transmitting audio, you would want to pass this Audio Client in to a service.
			var audioClient = await channel.ConnectAsync();
		}

		[Command("leave", RunMode = RunMode.Async)]
		public async Task LeaveChannel(IVoiceChannel channel = null)
		{
			// Get the audio channel
			channel = channel ?? (Context.User as IGuildUser)?.VoiceChannel;
			if (channel == null) { await Context.Channel.SendMessageAsync("User must be in a voice channel, or a voice channel must be passed as an argument."); return; }

			// For the next step with transmitting audio, you would want to pass this Audio Client in to a service.
			await channel.DisconnectAsync();
		}
	}
}
