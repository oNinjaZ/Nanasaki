using Discord.Commands;
using Nanasaki.Apis;
using Nanasaki.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nanasaki.Modules
{
    public class Japanese : ModuleBase<SocketCommandContext>
    {
        /// <summary>
        /// Searches for a single kanji character
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        [Command("k")]
        public async Task KanjiSearch(params string[] test)
        {
            if (test[0].Length == 1)
            {
                var kanjiQuery = $"https://kanjiapi.dev/v1/kanji/{test[0]}";

                var client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(kanjiQuery);
                if (response.IsSuccessStatusCode)
                {
                    string quoteJson = await client.GetStringAsync(kanjiQuery);

                    Kanji kanji = JsonConvert.DeserializeObject<Kanji>(quoteJson);

                    /////////////////////
                    ///
                    var result = await client.GetStringAsync($"https://kanjiapi.dev/v1/words/{test[0]}");

                    List<Word> words = JsonConvert.DeserializeObject<List<Word>>(result);

                    List<Variant> variantList = new();

                    foreach (var variant in words)
                    {
                        variantList.AddRange(variant.variants);
                    }

                    variantList.RemoveAll(a => !a.priorities.Any() || a.priorities == null || a.written.Length < 2);
                    variantList = variantList.OrderBy(a => a.written.Length).ToList();
                    if (variantList.Count > 10) variantList = variantList.GetRange(0, 8);

                    //////////////////////
                    ///
                    var grade = "N/A";
                    if (kanji.grade != null) grade = kanji.grade + "年";

                    var embed = new NanasakiEmbedBuilder()
                        .WithTitle($"{kanji.kanji}")
                        .WithUrl($"https://dictionary.goo.ne.jp/word/kanji/{test}/")
                        .AddField("おん読み", getReadings(kanji.on_readings), true)
                        .AddField("くん読み", getReadings(kanji.kun_readings), true)
                        .AddField("意味", string.Join(", ", kanji.meanings))
                        .AddField("Words", getOutput(variantList))
                        .WithFooter(footer => footer.Text = $"学習漢字: {grade}")
                        .Build();

                    await Context.Channel.TriggerTypingAsync();
                    await Context.Channel.SendMessageAsync(embed: embed);


                }
                else
                {
                    var embed = new NanasakiEmbedBuilder()
                            .WithTitle($"ななさき漢字辞典")
                            .WithDescription($"**{test[0]}**と一致する情報は見つかりませんでした")
                            .Build();

                    await Context.Channel.TriggerTypingAsync();
                    await Context.Channel.SendMessageAsync(embed: embed);
                }


            }
            else
            {
                await Context.Channel.TriggerTypingAsync();
                await Context.Channel.SendMessageAsync("`Usage:` -k {character}");
            }

        }

        private string getReadings(List<string> str)
        {
            if (str.Any())
            {
                return string.Join(" **|** ", str);
            }
            else return "N/A";
        }

        private string getOutput(List<Variant> variants)
        {
            List<string> result = new();
            foreach (var item in variants)
            {
                result.Add($"{item.written} `({item.pronounced})`");
            }
            return string.Join(" | ", result);
        }

    }
}
