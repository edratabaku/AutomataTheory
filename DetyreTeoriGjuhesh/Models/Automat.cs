
using DetyreTeoriGjuhesh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DetyreTeoriGjuhesh.Models
{
    public class Automat
    {
        public int Id { get; set; }
        public string LlojiAutomatit { get; set; }
        public List<string> Alfabeti { get; set; }
        public List<string> BashkesiaEGjendjeve { get; set; }
        public string GjendjaFillestare { get; set; }
        public List<string> GjendjetFundore { get; set; }
        public List<Kalim> Kalimet { get; set; }
    }
}