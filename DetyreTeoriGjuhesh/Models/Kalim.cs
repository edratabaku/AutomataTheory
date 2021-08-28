using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DetyreTeoriGjuhesh.Models
{
    /// <summary>
    /// The path class
    /// </summary>
    public class Kalim
    {
        /// <summary>
        /// The start state of the path
        /// </summary>
        public string GjendjaEPare { get; set; }
        /// <summary>
        /// The input
        /// </summary>
        public string Input { get; set; }
        /// <summary>
        /// The end state where the start state goes to when input is entered
        /// </summary>
        public string GjendjaEDyte { get; set; }
    }
}