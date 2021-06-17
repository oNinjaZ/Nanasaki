using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanasaki.Common
{
	/// <summary>
	/// Custom embed builder theme for Nanasaki
	/// </summary>
	internal class NanasakiEmbedBuilder : EmbedBuilder
	{
		public NanasakiEmbedBuilder()
		{
			this.WithColor(new Color(138, 118, 118));
		}
	}
}
