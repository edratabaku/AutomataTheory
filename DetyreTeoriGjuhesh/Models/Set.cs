using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DetyreTeoriGjuhesh.Models
{
    /// <summary>
    /// The set class
    /// </summary>
    public class Set
    { 
        /// <summary>
        /// Identifier of the set
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// List of the set states
        /// </summary>
        public List<GjendjeSeti> gjendjetESetit { get; set; }
    }
}