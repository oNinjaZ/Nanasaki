using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanasaki.Apis
{
	public class AnimePic
	{
		public List<Results> results { get; set; }

		public class Results
		{
			public string image_url { get; set; }
		}
	}
}
