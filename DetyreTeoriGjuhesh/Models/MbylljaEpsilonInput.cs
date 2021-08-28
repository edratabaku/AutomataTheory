using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DetyreTeoriGjuhesh.Models
{
    /// <summary>
    /// Class needed for the second step of the epsilon closure algorithm
    /// </summary>
    public class MbylljaEpsilonInput
    {
        /// <summary>
        /// Start state
        /// </summary>
        public string gjendjaFill { get; set; }
        /// <summary>
        /// Input
        /// </summary>
        public string Input { get; set; }
        /// <summary>
        /// List of the final states of the step
        /// </summary>
        public List<string> GjendjetFund { get; set; }
    }
}