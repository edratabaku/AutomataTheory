
using DetyreTeoriGjuhesh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DetyreTeoriGjuhesh.Controllers
{
    /// <summary>
    /// Home Controller
    /// </summary>
    public class HomeController : Controller
    {
        #region Inicializime
        //Initializing all automatas
        public static Automat Eafjd = new Automat()
        {
            Id = 1,
            LlojiAutomatit = "Ɛ-NFA",
            Alfabeti = new List<string>(),
            BashkesiaEGjendjeve = new List<string>(),
            GjendjaFillestare = "",
            GjendjetFundore = new List<string>(),
            Kalimet = new List<Kalim>()
        };
        public static Automat Afjd = new Automat()
        {
            Id = 2,
            LlojiAutomatit = "NFA",
            Alfabeti = new List<string>(),
            BashkesiaEGjendjeve = new List<string>(),
            GjendjaFillestare = "",
            GjendjetFundore = new List<string>(),
            Kalimet = new List<Kalim>()
        };
        public static Automat Afd = new Automat()
        {
            Id = 3,
            LlojiAutomatit = "DFA",
            Alfabeti = new List<string>(),
            BashkesiaEGjendjeve = new List<string>(),
            GjendjaFillestare = "",
            GjendjetFundore = new List<string>(),
            Kalimet = new List<Kalim>()
        };
        public static Automat AfdMinimal = new Automat()
        {
            Id = 4,
            LlojiAutomatit = "Minimal DFA",
            Alfabeti = new List<string>(),
            BashkesiaEGjendjeve = new List<string>(),
            GjendjaFillestare = "",
            GjendjetFundore = new List<string>(),
            Kalimet = new List<Kalim>()
        };
        #endregion
        /// <summary>
        /// Parameterless constructor of the Home controller
        /// </summary>
        public HomeController() { }
        #region Display
        /// <summary>
        /// Displays the data entered by the user for the Epsilon-NFA
        /// </summary>
        /// <returns></returns>
        public ActionResult ShfaqEAFjD()
        {
            return View("ShfaqAutomat", Eafjd);
        }
        /// <summary>
        /// Displays data after converting the Epsilon-NFA to NFA
        /// </summary>
        /// <returns></returns>
        public ActionResult ShfaqAFjD()
        {
            EafjdToAfjd();
            return View("ShfaqAutomat", Afjd);
        }
        /// <summary>
        /// Displays data after converting the NFA to DFA
        /// </summary>
        /// <returns></returns>
        public ActionResult ShfaqAFD()
        {
            AfjdToAfd();
            return View("ShfaqAutomat", Afd);
        }
        /// <summary>
        /// Displays data after converting the DFA to Minimal DFA
        /// </summary>
        /// <returns></returns>
        public ActionResult ShfaqAFDMinimal()
        {
            AfdToAfdMinimal();
            return View("ShfaqAutomat", AfdMinimal);
        }
        #endregion
        #region Epsilon-NFA input
        /// <summary>
        /// Returns a view that initializes the user input reading process
        /// </summary>
        /// <returns></returns>
        public ActionResult LexoEAFjDStep1()
        {
            return View();
        }
        /// <summary>
        /// Adds a new state to the Epsilon-NFA 
        /// </summary>
        /// <param name="gjendje">The new state entered by the user</param>
        /// <returns></returns>
        public JsonResult ShtoGjendje(string gjendje)
        {
            gjendje = gjendje.Trim().ToLower();
            if (Eafjd.BashkesiaEGjendjeve.Count() > 0)
            {
                foreach (var gj in Eafjd.BashkesiaEGjendjeve)
                {
                    //if the state already exists, return
                    if (gj.ToLower() == gjendje.ToLower())
                    {
                        return Json(new { status = "Success" });
                    }
                }
                //if the state does not exist, add to the states list
                Eafjd.BashkesiaEGjendjeve.Add(gjendje);
                return Json(new { status = "Success" });
            }
            else
            {
                Eafjd.BashkesiaEGjendjeve.Add(gjendje);
                return Json(new { status = "Success" });
            }
        }
        /// <summary>
        /// Used to send us to the second step of reading data for the Epsilon-NFA
        /// </summary>
        /// <returns></returns>
        public JsonResult KaloEAFjDStep2()
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("LexoEAFjDStep2", "Home");
            return Json(new { Url = redirectUrl });
        }
        /// <summary>
        /// The second step of reading data to build the Epsilon-NFA  
        /// </summary>
        /// <returns></returns>
        public ActionResult LexoEAFjDStep2()
        {
            return View();
        }
        /// <summary>
        /// Adds a new character the alphabet for Epsilon-NFA
        /// </summary>
        /// <param name="karakter"></param>
        /// <returns></returns>
        public JsonResult ShtoKarakter(string karakter)
        {
            try
            {
                //only add character to the alfphabet if it doesn't exist
                if (!Eafjd.Alfabeti.Contains(karakter))
                {
                    Eafjd.Alfabeti.Add(karakter);
                }
                return Json(new { status = "Success" });
            }
            catch (Exception e)
            {
                return Json(new { message = "Nuk mund te vendosen disa karaktere." });
            }
        }
        /// <summary>
        /// Method that sends us to the third step of reading data for the Epsilon-NFA
        /// </summary>
        /// <returns></returns>
        public JsonResult KaloEAFjDStep3()
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("LexoEAFjDStep3", "Home");
            return Json(new { Url = redirectUrl });
        }
        /// <summary>
        /// The third step to build the Epsilon-NFA
        /// </summary>
        /// <returns></returns>
        public ActionResult LexoEAFjDStep3()
        {
            List<string> Gjendjet = Eafjd.BashkesiaEGjendjeve;
            return View(Gjendjet);
        }
        /// <summary>
        /// Adds a state as the initial state of the Epsilon-NFA
        /// </summary>
        /// <param name="gjendjeFill">The initial state for the epsilon-NFA</param>
        /// <returns></returns>
        public JsonResult VendosGjendjeFillestare(string gjendjeFill)
        {
            Eafjd.GjendjaFillestare = gjendjeFill;
            ShtoEpsilon();
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("LexoEAFjDStep4", "Home");
            return Json(new { status = "Success", Url = redirectUrl });
        }
        /// <summary>
        /// The fourth step of building the Epsilon-NFA
        /// </summary>
        /// <returns></returns>
        public ActionResult LexoEAFjDStep4()
        {
            List<string> Gjendjet = Eafjd.BashkesiaEGjendjeve;
            return View(Gjendjet);
        }
        /// <summary>
        /// Adds a state as a final state of the Epsilon-NFA
        /// </summary>
        /// <param name="gjendjeFund">The state that will be added as a final state for the Epsilon-NFA</param>
        /// <returns></returns>
        public JsonResult VendosGjendjeFundore(string gjendjeFund)
        {
            var res = Eafjd.BashkesiaEGjendjeve.Where(gjend => gjend.ToLower().Equals(gjendjeFund.ToLower())).FirstOrDefault();
            Eafjd.GjendjetFundore.Add(res);
            return Json(new { status = "Success" });
        }
        /// <summary>
        /// The final step for building the Epsilon-NFA
        /// </summary>
        /// <returns></returns>
        public ActionResult LexoEAFjDStep5()
        {
            return View(Eafjd);
        }
        /// <summary>
        /// Adds epsilon to the alphabet and adds the state--epsilon-->state path for each state
        /// </summary>
        public void ShtoEpsilon()
        {
            Eafjd.Alfabeti.Add("eps");
            foreach (var gjendje in Eafjd.BashkesiaEGjendjeve)
            {
                Eafjd.Kalimet.Add(new Kalim()
                {
                    GjendjaEPare = gjendje,
                    Input = "eps",
                    GjendjaEDyte = gjendje
                });
            }
        }
        /// <summary>
        /// Adds a new path for the Epsilon-NFA
        /// </summary>
        /// <param name="gjendjePara">The first state</param>
        /// <param name="gjendjePas">The second state</param>
        /// <param name="input">Input needed to change the state from the first one to the second</param>
        /// <returns></returns>
        public JsonResult ShtoKalim(string gjendjePara, string gjendjePas, string input)
        {
            Kalim kalimIRi = new Kalim()
            {
                GjendjaEPare = gjendjePara,
                GjendjaEDyte = gjendjePas,
                Input = input
            };
            Kalim kalimEkzistues = Eafjd.Kalimet.Where(k => k.GjendjaEPare == gjendjePara && k.GjendjaEDyte == gjendjePas && k.Input == input).FirstOrDefault();
            if (kalimEkzistues == null)
            {
                Eafjd.Kalimet.Add(kalimIRi);
            }
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("LexoEAFjDStep5", "Home");
            return Json(new { status = "Success", Url = redirectUrl });
        }
        #endregion
        #region Conversions
        #region Epsilon-NFA to NFA
        public void EafjdToAfjd()
        {
            List<MbylljaEpsilon> mbylljetEpsilon = new List<MbylljaEpsilon>();
            List<MbylljaEpsilonInput> mbylljetEpsilonInput = new List<MbylljaEpsilonInput>();
            List<MbylljaEpsilonInputEpsilon> mbylljetEpsilonInputEpsilon = new List<MbylljaEpsilonInputEpsilon>();
            //Calculate the epsilon-closure for each state (the states it can go to by epsilon only)
            foreach (var gjendje in Eafjd.BashkesiaEGjendjeve)
            {
                MbylljaEpsilon mbyllje = new MbylljaEpsilon()
                {
                    gjendjaFill = gjendje
                };
                List<string> gjendjeFund = new List<string>();
                List<string> llogaritur = new List<string>();
                LlogaritMeEpsilon(gjendje, gjendjeFund, llogaritur);
                mbyllje.gjendjetFundStep1 = gjendjeFund;
                mbylljetEpsilon.Add(mbyllje);
            }
            // foreach character that is not epsilon, calculate at what state the result states of the epsilon closure will go to
            foreach (var input in Eafjd.Alfabeti)
            {
                if (input.ToLower() != "eps")
                {
                    foreach (var mbyllje in mbylljetEpsilon)
                    {
                        MbylljaEpsilonInput mbylljaEpsilonInput = new MbylljaEpsilonInput()
                        {
                            Input = input,
                            gjendjaFill = mbyllje.gjendjaFill,
                            GjendjetFund = new List<string>()
                        };
                        foreach (var gjendjeFund in mbyllje.gjendjetFundStep1)
                        {
                            var kalimet = Eafjd.Kalimet.Where(kalim => kalim.GjendjaEPare.ToLower() == gjendjeFund.ToLower() && kalim.Input.ToLower() == input.ToLower());
                            foreach (var kalim in kalimet)
                            {
                                if (!mbylljaEpsilonInput.GjendjetFund.Contains(kalim.GjendjaEDyte))
                                    mbylljaEpsilonInput.GjendjetFund.Add(kalim.GjendjaEDyte);
                            }
                        }
                        mbylljetEpsilonInput.Add(mbylljaEpsilonInput);
                    }
                }
            }
            //foreach result state of the above mentioned step, check whats the result state by only seeing epsilon
            foreach (var mbyllje in mbylljetEpsilonInput)
            {
                MbylljaEpsilonInputEpsilon mbylljaEpsilonInputEpsilon = new MbylljaEpsilonInputEpsilon()
                {
                    gjendjaFill = mbyllje.gjendjaFill,
                    Input = mbyllje.Input,
                    GjendjetFund = new List<string>()
                };
                List<string> gjendjeFund = new List<string>();
                List<string> llogaritur = new List<string>();
                foreach (var gjendje in mbyllje.GjendjetFund)
                {

                    LlogaritMeEpsilon(gjendje, gjendjeFund, llogaritur);
                    mbylljaEpsilonInputEpsilon.GjendjetFund = gjendjeFund;

                }
                mbylljetEpsilonInputEpsilon.Add(mbylljaEpsilonInputEpsilon);
            }
            //the nfa will have the same states as the epsilon-nfa
            Afjd.BashkesiaEGjendjeve = Eafjd.BashkesiaEGjendjeve;
            //the alphabet for the nfa will be the same one as the one for the epsilon-nfa but without the epsilon
            Afjd.Alfabeti = Eafjd.Alfabeti.Where(karakter => karakter != "eps").ToList();
            //the nfa has the same initial state as the epsilon-nfa
            Afjd.GjendjaFillestare = Eafjd.GjendjaFillestare;
            //paths for the nfa
            foreach (var mbyllje in mbylljetEpsilonInputEpsilon)
            {
                string gjendjeEPare = mbyllje.gjendjaFill;
                string input = mbyllje.Input;
                foreach (var gjendje in mbyllje.GjendjetFund)
                {
                    Kalim kalimIRi = new Kalim()
                    {
                        GjendjaEPare = gjendjeEPare,
                        Input = input,
                        GjendjaEDyte = gjendje
                    };
                    Afjd.Kalimet.Add(kalimIRi);
                }
            }
            //determine the final states for the nfa
            foreach (var gjendje in Eafjd.BashkesiaEGjendjeve)
            {
                List<string> afjdGjendjetFundore = new List<string>();
                bool arrinGjendjenFundore = GjendjetFundoreTeAfjd(gjendje, afjdGjendjetFundore);
                if (arrinGjendjenFundore)
                {
                    Afjd.GjendjetFundore.Add(gjendje);
                }
            }
        }
        /// <summary>
        /// Method that helps us find where each state goes by only seeing epsilon
        /// </summary>
        /// <param name="gjendje">the start state</param>
        /// <param name="gjendjeFund">the end states</param>
        /// <param name="llogaritur">list of already calculated states</param>
        public void LlogaritMeEpsilon(string gjendje, List<string> gjendjeFund, List<string> llogaritur)
        {
            llogaritur.Add(gjendje); //add to the list of already calculated states
            //find where the start state goes with the input of epsilon
            var kalimet = Eafjd.Kalimet.Where(kalim => kalim.GjendjaEPare.ToLower() == gjendje.ToLower() && kalim.Input.ToLower() == "eps"); 
            foreach (var gjendjeF in kalimet.Select(kalim => kalim.GjendjaEDyte))
            {
                if (!gjendjeFund.Contains(gjendjeF))
                    gjendjeFund.Add(gjendjeF);
                if (!llogaritur.Contains(gjendjeF))
                    LlogaritMeEpsilon(gjendjeF, gjendjeFund, llogaritur);
            }

        }
        /// <summary>
        /// Method that helps us find the final states of the nfa by checking if a state can get to the final state of the epsilon-nfa by only seeing epsilon
        /// </summary>
        /// <param name="gjendje">the state we're running a check for</param>
        /// <param name="afjdGjendjetFundore">the list of the final states of the epsilon-nfa</param>
        /// <returns></returns>
        public bool GjendjetFundoreTeAfjd(string gjendje, List<string> afjdGjendjetFundore)
        {
            afjdGjendjetFundore.Add(gjendje);
            var kalime = Eafjd.Kalimet.Where(kalim => kalim.GjendjaEPare.ToLower() == gjendje.ToLower() && kalim.Input.ToLower() == "eps");
            foreach (var kalim in kalime)
            {
                if (Eafjd.GjendjetFundore.Contains(kalim.GjendjaEDyte))
                {
                    return true;
                }
                else
                {
                    if (!afjdGjendjetFundore.Contains(kalim.GjendjaEDyte))
                    {
                        string gjendjeERe = kalim.GjendjaEDyte;
                        return GjendjetFundoreTeAfjd(gjendjeERe, afjdGjendjetFundore);
                    }

                }
            }
            return false;
        }
        #endregion
        #region NFA to DFA
        /// <summary>
        /// The conversion of NFA to DFA
        /// </summary>
        public void AfjdToAfd()
        {
            List<string> gjendjetAfd = new List<string>();
            List<Kalim> kalimetAfd = new List<Kalim>();
            //add as a state for DFA, the start state of the NFA
            gjendjetAfd.Add(Afjd.GjendjaFillestare);
            // add an error state in case we have states that do not arrive to a state by seeing a certain input
            string gjendjeError = "Gjendje error";
            gjendjetAfd.Add(gjendjeError);
            for (int i = 0; i < gjendjetAfd.Count(); i++)
            {
                //the calculation that  will be made for each state except the error state, since from the error state we can't go to any other state
                if (gjendjetAfd[i] != gjendjeError)
                {
                    foreach (var input in Afjd.Alfabeti)
                    {
                        //if the state is a part of the nfa states
                        if (Afjd.BashkesiaEGjendjeve.Contains(gjendjetAfd[i]))
                        {
                            var kalimet = Afjd.Kalimet.Where(kalim => kalim.GjendjaEPare == gjendjetAfd[i] && kalim.Input == input);
                            //if for an input we only go to one state, add the path and add the state to the dfa states, if it hasn't already been added
                            if (kalimet.Count() == 1)
                            {
                                kalimetAfd.Add(new Kalim()
                                {
                                    GjendjaEPare = gjendjetAfd[i],
                                    GjendjaEDyte = kalimet.FirstOrDefault().GjendjaEDyte,
                                    Input = input
                                });
                                if (!gjendjetAfd.Contains(kalimet.FirstOrDefault().GjendjaEDyte))
                                    gjendjetAfd.Add(kalimet.FirstOrDefault().GjendjaEDyte);
                            }
                            //if for an input we go to more than a state, we create a new state and add the corresponding path
                            else if (kalimet.Count() > 1)
                            {
                                string gjendjeERe = "";
                                List<string> gjendjetPerberese = new List<string>();
                                foreach (var gjendjeEDyte in kalimet.Select(kalim => kalim.GjendjaEDyte))
                                {
                                    if (!gjendjetPerberese.Contains(gjendjeEDyte))
                                    {
                                        gjendjeERe += gjendjeEDyte;
                                        gjendjetPerberese.Add(gjendjeEDyte);
                                    }
                                }
                                if (!gjendjetAfd.Contains(gjendjeERe))
                                    gjendjetAfd.Add(gjendjeERe);
                                kalimetAfd.Add(new Kalim()
                                {
                                    GjendjaEPare = gjendjetAfd[i],
                                    GjendjaEDyte = gjendjeERe,
                                    Input = input
                                });
                            }
                            //if there's no path go to the error state
                            else
                            {
                                kalimetAfd.Add(new Kalim()
                                {
                                    GjendjaEPare = gjendjetAfd[i],
                                    GjendjaEDyte = gjendjeError,
                                    Input = input
                                });
                            }
                            //if it's a final state for the nfa it's also a final state for the dfa
                            if (Afjd.GjendjetFundore.Contains(gjendjetAfd[i]))
                                Afd.GjendjetFundore.Add(gjendjetAfd[i]);
                        }
                        //if the state is a new state
                        else
                        {
                            try
                            {
                                char test = Convert.ToChar(Afjd.GjendjaFillestare);
                                string gjendja = gjendjetAfd[i];
                                //calculate where does each state that makes up the new state go for a certain input
                                List<Kalim> kalimetTotale = new List<Kalim>();
                                for (int j = 0; j < gjendja.Length; j++)
                                {
                                    string gjendjaPerberese = Convert.ToString(gjendja[j]);
                                    var kalimePerGjendje = Afjd.Kalimet.Where(kalim => kalim.GjendjaEPare == gjendjaPerberese && kalim.Input == input);
                                    foreach (var kalim in kalimePerGjendje)
                                        kalimetTotale.Add(kalim);
                                }
                                var kalimetPerInput = kalimetTotale.Where(kalim => kalim.Input == input).Select(kalim => kalim.GjendjaEDyte).Distinct();
                                var gjendjaPasardhese = "";
                                for (int k = 0; k < kalimetPerInput.Count(); k++)
                                {
                                    gjendjaPasardhese += kalimetPerInput.ElementAt(k);
                                }
                                //if there's an end state
                                if (kalimetPerInput.Count() >= 1)
                                {
                                    kalimetAfd.Add(new Kalim()
                                    {
                                        GjendjaEPare = gjendja,
                                        GjendjaEDyte = gjendjaPasardhese,
                                        Input = input
                                    });
                                    if (!gjendjetAfd.Contains(gjendjaPasardhese))
                                        gjendjetAfd.Add(gjendjaPasardhese);
                                }
                                //if not, go to the error state
                                else
                                {
                                    kalimetAfd.Add(new Kalim()
                                    {
                                        GjendjaEPare = gjendja,
                                        GjendjaEDyte = gjendjeError,
                                        Input = input
                                    });
                                }
                            }
                            catch (Exception)
                            {
                                string gjendja = gjendjetAfd[i];
                                List<Kalim> kalimetTotale = new List<Kalim>();
                                for (int j = 0; j < gjendja.Length - 1; j = j + 2)
                                {
                                    string gjendjaPerberese = gjendja[j] + "" + gjendja[j + 1];
                                    var kalimePerGjendje = Afjd.Kalimet.Where(kalim => kalim.GjendjaEPare == gjendjaPerberese && kalim.Input == input);
                                    foreach (var kalim in kalimePerGjendje)
                                        kalimetTotale.Add(kalim);
                                }
                                var kalimetPerInput = kalimetTotale.Where(kalim => kalim.Input == input).Select(kalim => kalim.GjendjaEDyte).Distinct();
                                var gjendjaPasardhese = "";
                                for (int k = 0; k < kalimetPerInput.Count(); k++)
                                {
                                    gjendjaPasardhese += kalimetPerInput.ElementAt(k);
                                }
                                if (kalimetPerInput.Count() >= 1)
                                {
                                    kalimetAfd.Add(new Kalim()
                                    {
                                        GjendjaEPare = gjendja,
                                        GjendjaEDyte = gjendjaPasardhese,
                                        Input = input
                                    });
                                    if (!gjendjetAfd.Contains(gjendjaPasardhese))
                                        gjendjetAfd.Add(gjendjaPasardhese);
                                }
                                else
                                {
                                    kalimetAfd.Add(new Kalim()
                                    {
                                        GjendjaEPare = gjendja,
                                        GjendjaEDyte = gjendjeError,
                                        Input = input
                                    });
                                }
                            }
                        }
                    }
                }
                //if the state is the error state, for each input add the path that leads to itself
                else
                {
                    foreach (var input in Afjd.Alfabeti)
                    {
                        kalimetAfd.Add(new Kalim()
                        {
                            GjendjaEPare = gjendjetAfd[i],
                            GjendjaEDyte = gjendjetAfd[i],
                            Input = input
                        });
                    }
                }
            }
            // if the state contains an end state of the nfa, it'll be an end state for the dfa
            foreach (var gjendje in gjendjetAfd)
            {
                try
                {
                    char test = Convert.ToChar(Afjd.GjendjaFillestare);
                    for (int i = 0; i < gjendje.Length; i++)
                    {
                        if (gjendje != gjendjeError)
                        {
                            if (Afjd.GjendjetFundore.Contains(gjendje[i].ToString()))
                            {
                                Afd.GjendjetFundore.Add(gjendje);
                                break;
                            }
                        }

                    }
                }
                catch (Exception e)
                {
                    for (int i = 0; i < gjendje.Length - 1; i = i + 2)
                    {
                        if (gjendje != gjendjeError)
                        {
                            if (Afjd.GjendjetFundore.Contains(gjendje[i].ToString()))
                            {
                                Afd.GjendjetFundore.Add(gjendje);
                                break;
                            }
                        }
                    }
                }
            }
            //if none of the states goes to an error state, then remove the error state and all the paths that lead to it
            if (kalimetAfd.Where(kalim => kalim.GjendjaEDyte == gjendjeError && kalim.GjendjaEPare != gjendjeError).Count() == 0)
            {
                for (int i = 0; i < kalimetAfd.Count(); i++)
                {
                    if (kalimetAfd[i].GjendjaEPare == gjendjeError || kalimetAfd[i].GjendjaEDyte == gjendjeError)
                    {
                        kalimetAfd.Remove(kalimetAfd[i]);
                        i--;
                    }

                }
                gjendjetAfd.Remove(gjendjeError);
            }
            Afd.Alfabeti = Afjd.Alfabeti;
            Afd.BashkesiaEGjendjeve = gjendjetAfd;
            Afd.GjendjaFillestare = Afjd.GjendjaFillestare;
            Afd.Kalimet = kalimetAfd;
        }
        #endregion
        #region Minimizing the DFA
        /// <summary>
        /// Method that minimizes the DFA
        /// </summary>
        public void AfdToAfdMinimal()
        {
            var gjendjet = new List<GjendjeSeti>();
            var setet = new List<Set>();
            //remove all the unreachable states
            foreach (var gjendje in Afd.BashkesiaEGjendjeve)
            {
                if ((Afd.Kalimet.Select(gj => gj.GjendjaEDyte).Contains(gjendje) || Afd.GjendjaFillestare == gjendje))
                {
                    gjendjet.Add(new GjendjeSeti()
                    {
                        Gjendje = gjendje,
                        SetMeVete = false,
                        EshteGjendjeFundore = Afd.GjendjetFundore.Contains(gjendje) ? true : false,
                        IdESetitKuNdodhet = null
                    });
                }

            }
            //create the set of the final states
            var setGjendjeFundore = gjendjet.Where(gjendje => gjendje.EshteGjendjeFundore == true).ToList();
            //create the set of the non-final states
            var setGjendjeJoFundore = gjendjet.Where(gjendje => gjendje.EshteGjendjeFundore == false).ToList();
            setet.Add(new Set() { gjendjetESetit = setGjendjeJoFundore });
            setet.Add(new Set() { gjendjetESetit = setGjendjeFundore });
            var setetEReja = Minimizimi(setet);
            //the new states of the minimal-DFA will be the states of the sets after the minimization process
            foreach (var set in setetEReja)
            {
                string gjendjaERe = "";
                foreach (var gjendje in set.gjendjetESetit)
                {
                    gjendjaERe += gjendje.Gjendje;
                }
                AfdMinimal.BashkesiaEGjendjeve.Add(gjendjaERe);
                if (set.gjendjetESetit.Select(gj => gj.Gjendje).Contains(Afd.GjendjaFillestare))
                    AfdMinimal.GjendjaFillestare = gjendjaERe;
                foreach (var gjendje in Afd.GjendjetFundore)
                {
                    if (set.gjendjetESetit.Select(gj => gj.Gjendje).Contains(gjendje))
                        AfdMinimal.GjendjetFundore.Add(gjendje);
                }
            }

            AfdMinimal.Alfabeti = Afd.Alfabeti;
            KalimetAfdToAfdMinimal(setetEReja);
        }
        public List<Set> Minimizimi(List<Set> setet)
        {
            //initially none of the states belongs to a set
            foreach (var set in setet)
            {
                foreach (var gjendje in set.gjendjetESetit)
                {
                    gjendje.IdESetitKuNdodhet = null;
                    gjendje.SetMeVete = false;
                }
            }
            int idSet = 0;
            //the new sets will serve as initial sets for the repetition of this function
            List<Set> setetEReja = new List<Set>();
            //the algorithm will end when there are no longer any changes to the sets
            bool changes = false;
            //for each set
            foreach (var set in setet)
            {
                //for each set with more than a state
                if (set.gjendjetESetit.Count != 1)
                {
                    //for each state of the set
                    for (int i = 0; i < set.gjendjetESetit.Count(); i++)
                    {
                        //if it'S the first state, create a new set to save this state
                        if (i == 0)
                        {
                            set.gjendjetESetit.ElementAt(i).SetMeVete = true;
                            set.gjendjetESetit.ElementAt(i).IdESetitKuNdodhet = idSet;
                            setetEReja.Add(new Set()
                            {
                                Id = idSet,
                                gjendjetESetit = new List<GjendjeSeti>()
                            {
                                new GjendjeSeti()
                                {
                                    Gjendje = set.gjendjetESetit.ElementAt(i).Gjendje,
                                    SetMeVete = true,
                                    IdESetitKuNdodhet = idSet
                                }
                            }
                            });
                            idSet++;
                        }
                        else
                        {
                            //compare the state with its ancestors within the set, so with only an element for each new set
                            for (int j = i - 1; j >= 0; j--)
                            {
                                var gjendjeParaardhese = set.gjendjetESetit.ElementAt(j);
                                if (gjendjeParaardhese.SetMeVete == true && set.gjendjetESetit.ElementAt(i).IdESetitKuNdodhet != null)
                                {
                                    //calculate if the states are in the same set
                                    bool neTeNjejtinSet = PjeseETeNjejtitSet(set.gjendjetESetit.ElementAt(i), gjendjeParaardhese, setet);
                                    //if yes, add to the set
                                    if (neTeNjejtinSet)
                                    {
                                        setetEReja.ElementAt(gjendjeParaardhese.IdESetitKuNdodhet.GetValueOrDefault()).gjendjetESetit.Add(set.gjendjetESetit.ElementAt(i));
                                        set.gjendjetESetit.ElementAt(i).IdESetitKuNdodhet = gjendjeParaardhese.IdESetitKuNdodhet;
                                    }
                                }
                            }
                            //if the identifier of the set where the state belongs is null, the state will create a new set
                            if (set.gjendjetESetit.ElementAt(i).IdESetitKuNdodhet == null)
                            {
                                set.gjendjetESetit.ElementAt(i).SetMeVete = true;
                                set.gjendjetESetit.ElementAt(i).IdESetitKuNdodhet = idSet;
                                setetEReja.Add(new Set()
                                {
                                    Id = idSet,
                                    gjendjetESetit = new List<GjendjeSeti>()
                            {
                                new GjendjeSeti()
                                {
                                    Gjendje = set.gjendjetESetit.ElementAt(i).Gjendje,
                                    EshteGjendjeFundore = set.gjendjetESetit.ElementAt(i).EshteGjendjeFundore,
                                    SetMeVete = true,
                                    IdESetitKuNdodhet = idSet
                                }
                            }
                                });
                                idSet++;
                                changes = true;
                            }

                        }
                    }
                }
                //if the state is alone in a set, we'll create the set that only holds this state and there will be no other modifications in it
                else
                {
                    setetEReja.Add(new Set()
                    {
                        Id = idSet,
                        gjendjetESetit = new List<GjendjeSeti>()
                            {
                                new GjendjeSeti()
                                {
                                    Gjendje = set.gjendjetESetit.FirstOrDefault().Gjendje,
                                    EshteGjendjeFundore = set.gjendjetESetit.FirstOrDefault().EshteGjendjeFundore,
                                    SetMeVete = true,
                                    IdESetitKuNdodhet = idSet
                                }
                            }
                    });
                    idSet++;
                }
            }
            //if there were any changes call the function recursively for the new created sets
            if (changes) { return setetEReja = Minimizimi(setetEReja); }
            //if not return the new sets
            else return setetEReja;
        }
        //function checks if there are two states in the same set
        public bool PjeseETeNjejtitSet(GjendjeSeti gjendjaEPare, GjendjeSeti gjendjaEDyte, List<Set> setet)
        {
            int neNjeSetPer = 0;
            foreach (var input in Afd.Alfabeti)
            {
                var gjendjaPasardheseEGjendjesSePare = Afd.Kalimet.Where(gjendje => gjendje.GjendjaEPare == gjendjaEPare.Gjendje && gjendje.Input == input).FirstOrDefault().GjendjaEDyte;
                var gjendjaPasardheseEGjendjesSeDyte = Afd.Kalimet.Where(gjendje => gjendje.GjendjaEPare == gjendjaEDyte.Gjendje && gjendje.Input == input).FirstOrDefault().GjendjaEDyte;
                foreach (var set in setet)
                {
                    if ((set.gjendjetESetit.Select(gjendje => gjendje.Gjendje).Contains(gjendjaPasardheseEGjendjesSePare) && set.gjendjetESetit.Select(gjendje => gjendje.Gjendje).Contains(gjendjaPasardheseEGjendjesSeDyte)))
                    {
                        gjendjaEPare.IdESetitKuNdodhet = gjendjaEDyte.IdESetitKuNdodhet;
                        neNjeSetPer++;
                    }
                }
            }
            if (neNjeSetPer != Afd.Alfabeti.Count())
            {
                gjendjaEPare.IdESetitKuNdodhet = null;
                return false;
            }
            return true;
        }
        /// <summary>
        /// Creating the paths for the minimization of the DFA
        /// </summary>
        /// <param name="setet">list of the sets created during minimizing the DFA</param>
        public void KalimetAfdToAfdMinimal(List<Set> setet)
        {
            foreach (var set in setet)
            {
                string gjendjeERe = "";
                foreach (var gjendje in set.gjendjetESetit)
                {
                    gjendjeERe += gjendje.Gjendje;
                }
                if (Afd.BashkesiaEGjendjeve.Contains(gjendjeERe))
                {
                    var kalimet = Afd.Kalimet.Where(gjendje => gjendje.GjendjaEPare == gjendjeERe);
                    foreach (var kalim in kalimet)
                    {
                        AfdMinimal.Kalimet.Add(new Kalim()
                        {
                            GjendjaEPare = gjendjeERe,
                            Input = kalim.Input,
                            GjendjaEDyte = kalim.GjendjaEDyte
                        });
                    }
                }
                else
                {
                    var gjendjaEPareESetit = set.gjendjetESetit.First();
                    var kalimetNeAfd = Afd.Kalimet.Where(gjendje => gjendje.GjendjaEPare == gjendjaEPareESetit.Gjendje);
                    foreach (var inp in Afd.Alfabeti)
                    {
                        var kalimi = Afd.Kalimet.Where(gjendje => gjendje.GjendjaEPare == gjendjaEPareESetit.Gjendje && gjendje.Input == inp).FirstOrDefault();
                        Set setiKuShkon = new Set();
                        foreach (var setPas in setet)
                        {
                            if (setPas.gjendjetESetit.Select(gj => gj.Gjendje).Contains(kalimi.GjendjaEDyte))
                                setiKuShkon = setPas;
                        }
                        string gjendjaPas = "";
                        foreach (var gjendje in setiKuShkon.gjendjetESetit)
                        {
                            gjendjaPas += gjendje.Gjendje;
                        }
                        AfdMinimal.Kalimet.Add(new Kalim()
                        {
                            GjendjaEPare = gjendjeERe,
                            Input = inp,
                            GjendjaEDyte = gjendjaPas
                        });
                    }
                }
            }
        }
        #endregion
        /// <summary>
        /// Start the program from the beginning
        /// </summary>
        /// <returns></returns>
        public ActionResult EAFjDiRi()
        {
            KrijoTeRi();
            return View("LexoEAFjDStep1");
        }
        /// <summary>
        /// Method that empties the automatas from pre-existing data so that the program can start over again
        /// </summary>
        public void KrijoTeRi()
        {
            Eafjd = new Automat()
            {
                Id = 1,
                LlojiAutomatit = "Automat Ɛ-AFjD",
                Alfabeti = new List<string>(),
                BashkesiaEGjendjeve = new List<string>(),
                GjendjaFillestare = "",
                GjendjetFundore = new List<string>(),
                Kalimet = new List<Kalim>()
            };
            Afjd = new Automat()
            {
                Id = 2,
                LlojiAutomatit = "Automat AFjD",
                Alfabeti = new List<string>(),
                BashkesiaEGjendjeve = new List<string>(),
                GjendjaFillestare = "",
                GjendjetFundore = new List<string>(),
                Kalimet = new List<Kalim>()
            };
            Afd = new Automat()
            {
                Id = 3,
                LlojiAutomatit = "Automat AFD",
                Alfabeti = new List<string>(),
                BashkesiaEGjendjeve = new List<string>(),
                GjendjaFillestare = "",
                GjendjetFundore = new List<string>(),
                Kalimet = new List<Kalim>()
            };
            AfdMinimal = new Automat()
            {
                Id = 4,
                LlojiAutomatit = "Automat AFD Minimal",
                Alfabeti = new List<string>(),
                BashkesiaEGjendjeve = new List<string>(),
                GjendjaFillestare = "",
                GjendjetFundore = new List<string>(),
                Kalimet = new List<Kalim>()
            };
        }
        #endregion
    }
}
