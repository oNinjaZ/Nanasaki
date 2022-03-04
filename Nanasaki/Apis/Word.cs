using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanasaki.Apis
{
    public class Word
    {
        public List<Variant> variants { get; set; }
    }

    public class Variant
    {
        public string written { get; set; }
        public string pronounced { get; set; }
        public List<string> priorities { get; set; }
    }
}
