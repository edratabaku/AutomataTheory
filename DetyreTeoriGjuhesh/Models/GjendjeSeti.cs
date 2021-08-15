using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DetyreTeoriGjuhesh.Models
{
    public class GjendjeSeti
    {
        public string Gjendje { get; set; }
        public bool SetMeVete { get; set; }
        public bool EshteGjendjeFundore { get; set; }

        public int? IdESetitKuNdodhet { get; set; }
    }
}