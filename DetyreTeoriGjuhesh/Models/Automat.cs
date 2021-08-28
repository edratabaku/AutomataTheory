
using DetyreTeoriGjuhesh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DetyreTeoriGjuhesh.Models
{
    /// <summary>
    /// The Automata class
    /// </summary>
    public class Automat
    {
        /// <summary>
        /// Identifier of the automata
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of the automata type: Epsilon-NFA, NFA, DFA or Minimal-DFA
        /// </summary>
        public string LlojiAutomatit { get; set; }
        /// <summary>
        /// The alphabet that the automata recognizes
        /// </summary>
        public List<string> Alfabeti { get; set; }
        /// <summary>
        /// The states of the automata
        /// </summary>
        public List<string> BashkesiaEGjendjeve { get; set; }
        /// <summary>
        /// The initial state
        /// </summary>
        public string GjendjaFillestare { get; set; }
        /// <summary>
        /// The final states
        /// </summary>
        public List<string> GjendjetFundore { get; set; }
        /// <summary>
        /// The paths of the automata (Format: state-input-state)
        /// </summary>
        public List<Kalim> Kalimet { get; set; }
    }
}