using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DetyreTeoriGjuhesh.Models
{
    /// <summary>
    /// Equivalence class
    /// </summary>
    public class Ekuivalencat
    {
        /// <summary>
        /// Identifier of the equivalence
        /// </summary>
        public int IdEkuivalence { get; set; }
        /// <summary>
        /// List of the sets
        /// </summary>
        public List<Set> Setet { get; set; }
    }
}