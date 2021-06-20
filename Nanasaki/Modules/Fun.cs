using Discord;
using Discord.Commands;
using Nanasaki.Apis;
using Nanasaki.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nanasaki.Modules
{
	public class Fun : ModuleBase<SocketCommandContext>
	{
		[Command("waifu")]
		public async Task Waifu()
		{
			var client = new HttpClient();
			var result = await client.GetStringAsync("https://api.waifu.pics/sfw/waifu");

			Waifu waifu = JsonConvert.DeserializeObject<Waifu>(result);

			await Context.Channel.TriggerTypingAsync();
			await Context.Channel.SendMessageAsync(waifu.url);
		}
		[Command("quote")]
		public async Task Quote()
		{
			var client = new HttpClient();
			var result = await client.GetStringAsync("https://animechan.vercel.app/api/random");

			AnimeQuote quote = JsonConvert.DeserializeObject<AnimeQuote>(result);

			await Context.Channel.TriggerTypingAsync();
			await Context.Channel.SendMessageAsync($"{quote.anime}\n" +
				$"{quote.character}\n" +
				$"{quote.quote}");
		}
	}
}
