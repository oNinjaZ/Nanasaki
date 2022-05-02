using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanasaki.Apis
{
    public class Kanji
    {
        public string kanji { get; set; }
        public string grade { get; set; }
        public List<string> meanings { get; set; }
        public List<string> kun_readings { get; set; }
        public List<string> on_readings { get; set; }
    }
}
