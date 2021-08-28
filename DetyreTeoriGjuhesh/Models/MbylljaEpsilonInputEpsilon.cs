using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DetyreTeoriGjuhesh.Models
{
    /// <summary>
    /// Class needed to save data for the third final step of the epsilon closure algorithm
    /// </summary>
    public class MbylljaEpsilonInputEpsilon
    {
        /// <summary>
        /// The start state
        /// </summary>
        public string gjendjaFill { get; set; }
        /// <summary>
        /// Input
        /// </summary>
        public string Input { get; set; }
        /// <summary>
        /// List of the final states of the third step
        /// </summary>
        public List<string> GjendjetFund { get; set; }
    }
}