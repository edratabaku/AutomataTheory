using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DetyreTeoriGjuhesh.Models
{
    /// <summary>
    /// State of a set
    /// </summary>
    public class GjendjeSeti
    {
        /// <summary>
        /// The  state
        /// </summary>
        public string Gjendje { get; set; }
        /// <summary>
        /// Indicates if the state created the set or not
        /// </summary>
        public bool SetMeVete { get; set; }
        /// <summary>
        /// If true, it indicates that the state is a final state
        /// </summary>
        public bool EshteGjendjeFundore { get; set; }
        /// <summary>
        /// Identifier of the set that the state belongs to
        /// </summary>
        public int? IdESetitKuNdodhet { get; set; }
    }
}