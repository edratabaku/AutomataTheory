using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DetyreTeoriGjuhesh.Models
{
    /// <summary>
    /// The Epsilon closure class
    /// </summary>
    public class MbylljaEpsilon
    {
        /// <summary>
        /// The start state
        /// </summary>
        public string gjendjaFill { get; set; }
        /// <summary>
        /// List of the states after the end of the first step of the epsilon closure algorithm
        /// </summary>
        public List <string> gjendjetFundStep1 { get; set; }
    }
}