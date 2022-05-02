using Discord;
using Discord.Commands;
using Discord.WebSocket;
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

        [Command("hey")]
        public async Task InfoAsync()
        {

            var socketGuildUser = Context.User as SocketGuildUser;

            await Context.Channel.TriggerTypingAsync();

            if (socketGuildUser.Id == 559401993878110241)
            {
                await Context.Channel.SendMessageAsync("こんにちは、先輩！ " + Context.Message.Author.Mention + " ❤️");
            }
            else if (socketGuildUser.Id == 355553281357119490)
            {
                await Context.Channel.SendMessageAsync("hi ate! " + Context.Message.Author.Mention);
            }
            else if (socketGuildUser.Id == 306931050460741632)
            {
                await Context.Channel.SendMessageAsync("your mom said hi too, " + Context.Message.Author.Mention);
            }
            else
            {
                await Context.Channel.SendMessageAsync("hello " + Context.Message.Author.Mention);
            }

        }

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

        [Command("echi")]
        public async Task EchoAsync()
        {

            var socketGuildUser = Context.User as SocketGuildUser;
            if (socketGuildUser.Id == 559401993878110241)
            {
                await Context.Channel.TriggerTypingAsync();
                await Context.Channel.SendMessageAsync("hey baby! " + Context.Message.Author.Mention + " ❤️");
            }
            else
            {
                await Context.Channel.TriggerTypingAsync();
                await Context.Channel.SendMessageAsync("hello " + Context.Message.Author.Mention);
            }


        }

        [Command("quote")]
        public async Task Quote()
        {
            var client = new HttpClient();

            var quoteJson = await client.GetStringAsync("https://animechan.vercel.app/api/random");
            var quote = JsonConvert.DeserializeObject<AnimeQuote>(quoteJson);

            var animePicJson = await client.GetStringAsync($"https://api.jikan.moe/v3/search/anime?q={quote.anime}&limit=1");
            var animeCoverPic = JsonConvert.DeserializeObject<AnimePic>(animePicJson);

            var embed = new NanasakiEmbedBuilder()
                .AddField("アニメ", quote.anime, true)
                .AddField("キャラ", quote.character, true)
                .AddField("名言", quote.quote, false)
                .WithThumbnailUrl(animeCoverPic.results.First().image_url)
                .Build();

            await this.ReplyAsync(embed: embed);
        }
    }
}
