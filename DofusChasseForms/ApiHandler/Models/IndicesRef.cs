using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DofusChasseForms.ApiHandler.Models
{
    public class IndicesRef
    {
        public From from { get; set; }
        public List<Hint> hints { get; set; }
    }

    public class From
    {
        public int x { get; set; }
        public int y { get; set; }
        public string di { get; set; }
    }

    public class Hint
    {
        public int n { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int d { get; set; }
    }
}
