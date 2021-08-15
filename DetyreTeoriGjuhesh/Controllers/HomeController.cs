
using DetyreTeoriGjuhesh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DetyreTeoriGjuhesh.Controllers
{
    public class HomeController : Controller
    {
        #region Inicializime
        //Inicializimi i automateve si statike
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
        public HomeController() { }
        #region Paraqitje
        public ActionResult Index()
        {
            return View();
        }
        //Shfaq 5shen per automatin e-afjd
        public ActionResult ShfaqEAFjD()
        {
            return View("ShfaqAutomat", Eafjd);
        }
        //Shfaq 5shen per automatin afjd
        public ActionResult ShfaqAFjD()
        {
            EafjdToAfjd();
            return View("ShfaqAutomat", Afjd);
        }
        //Shfaq 5shen per automatin afd
        public ActionResult ShfaqAFD()
        {
            AfjdToAfd();
            return View("ShfaqAutomat", Afd);
        }
        public ActionResult ShfaqAFDMinimal()
        {
            AfdToAfdMinimal();
            return View("ShfaqAutomat", AfdMinimal);
        }
        #endregion
        #region Leximi i te dhenave EAFjD
        public ActionResult LexoEAFjDStep1()
        {
            return View();
        }
        //Shton nje gjendje te re ne bashkesine e gjendjeve te automatit eafjd
        public JsonResult ShtoGjendje(string gjendje)
        {
            gjendje = gjendje.Trim().ToLower();
            if (Eafjd.BashkesiaEGjendjeve.Count() > 0)
            {
                foreach (var gj in Eafjd.BashkesiaEGjendjeve)
                {
                    if (gj.ToLower() == gjendje.ToLower())
                    {
                        return Json(new { status = "Success" });
                    }
                }
                Eafjd.BashkesiaEGjendjeve.Add(gjendje);
                return Json(new { status = "Success" });
            }
            else
            {
                Eafjd.BashkesiaEGjendjeve.Add(gjendje);
                return Json(new { status = "Success" });
            }
        }
        public JsonResult KaloEAFjDStep2()
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("LexoEAFjDStep2", "Home");
            return Json(new { Url = redirectUrl });
        }
        public ActionResult LexoEAFjDStep2()
        {
            return View();
        }
        //Shton nje karakter ne alfabetin e automatit eafjd
        public JsonResult ShtoKarakter(string karakter)
        {
            try
            {
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
        public JsonResult KaloEAFjDStep3()
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("LexoEAFjDStep3", "Home");
            return Json(new { Url = redirectUrl });
        }
        public ActionResult LexoEAFjDStep3()
        {
            List<string> Gjendjet = Eafjd.BashkesiaEGjendjeve;
            return View(Gjendjet);
        }
        //Vendos gjendjen fillestare te automatit eafjd
        public JsonResult VendosGjendjeFillestare(string gjendjeFill)
        {
            Eafjd.GjendjaFillestare = gjendjeFill;
            ShtoEpsilon();
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("LexoEAFjDStep4", "Home");
            return Json(new { status = "Success", Url = redirectUrl });
        }
        public ActionResult LexoEAFjDStep4()
        {
            List<string> Gjendjet = Eafjd.BashkesiaEGjendjeve;
            return View(Gjendjet);
        }
        //Shton gjendje fundore per automatin eafjd
        public JsonResult VendosGjendjeFundore(string gjendjeFund)
        {
            var res = Eafjd.BashkesiaEGjendjeve.Where(gjend => gjend.ToLower().Equals(gjendjeFund.ToLower())).FirstOrDefault();
            Eafjd.GjendjetFundore.Add(res);
            return Json(new { status = "Success" });
        }
        public ActionResult LexoEAFjDStep5()
        {
            return View(Eafjd);
        }
        //Shton epsilonin ne alfabet dhe shton kalimin me epsilon per cdo gjendje tek vetvetja
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
        //Shton nje kalim per automatin eafjd ku cdo kalim ruan gjendjen paraardhese, gjendjen pasardhese dhe inputin
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
        #region Konvertime
        #region EafjdtoAfjd
        //Metoda qe konverton automatin eafjd ne automat afjd
        public void EafjdToAfjd()
        {
            List<MbylljaEpsilon> mbylljetEpsilon = new List<MbylljaEpsilon>();
            List<MbylljaEpsilonInput> mbylljetEpsilonInput = new List<MbylljaEpsilonInput>();
            List<MbylljaEpsilonInputEpsilon> mbylljetEpsilonInputEpsilon = new List<MbylljaEpsilonInputEpsilon>();
            //Llogaritet mbyllja epsilon per cdo gjendje (cilat jane gjendjet qe arrin vetem duke pare epsilon)
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
            //per cdo karakter jo epsilon te alfabetit llogarisim ku shkojne gjendjet e marra nga mbyllja epsilon e gjendjeve te automatit
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
            //per cdo gjendje qe morem nga hapi i mesiperm llogarisim ku shkon gjendja nese sheh vetem epsilon 
            foreach (var mbyllje in mbylljetEpsilonInput)
            {
                MbylljaEpsilonInputEpsilon mbylljaEpsilonInputEpsilon = new MbylljaEpsilonInputEpsilon()
                {
                    //MbylljaEpsilonInput = mbyllje,
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
            //bashkesia e gjendjeve te automatit afjd do jete njesoj si ajo e automatit afjd
            Afjd.BashkesiaEGjendjeve = Eafjd.BashkesiaEGjendjeve;
            //alfabeti per automatin afjd eshte i njejte me ate te automatit eafjd pa perfshire epsilon
            Afjd.Alfabeti = Eafjd.Alfabeti.Where(karakter => karakter != "eps").ToList();
            //gjendja fillestare per automatin afjd do jete e njejte me ate te automatit eafjd
            Afjd.GjendjaFillestare = Eafjd.GjendjaFillestare;
            //vendosim kalimet per automatin afjd
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
            //caktojme gjendjet fundore te automatit afjd
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
        //Metoda qe na ndihmon te gjejme cilat gjendje arrin nje gjendje vetem duke pare epsilon
        public void LlogaritMeEpsilon(string gjendje, List<string> gjendjeFund, List<string> llogaritur)
        {
            llogaritur.Add(gjendje);
            var kalimet = Eafjd.Kalimet.Where(kalim => kalim.GjendjaEPare.ToLower() == gjendje.ToLower() && kalim.Input.ToLower() == "eps");
            foreach (var gjendjeF in kalimet.Select(kalim => kalim.GjendjaEDyte))
            {
                if (!gjendjeFund.Contains(gjendjeF))
                    gjendjeFund.Add(gjendjeF);
                if (!llogaritur.Contains(gjendjeF))
                    LlogaritMeEpsilon(gjendjeF, gjendjeFund, llogaritur);
            }

        }
        //metode qe na ndihmon te gjejme gjendjet fundore te afjd duke pare nese nje gjendje arrin gjendjen fundore te automatit eafjd duke pare vetem epsilon
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
        #region AfjdToAfd
        //konvertimi i automatit nga afjd ne afd
        public void AfjdToAfd()
        {
            List<string> gjendjetAfd = new List<string>();
            List<Kalim> kalimetAfd = new List<Kalim>();
            //vendosim si gjendje, gjendjen fillestare te automatit afjd
            gjendjetAfd.Add(Afjd.GjendjaFillestare);
            //shtojme nje gjendje errori ne rast se do te kemi gjendje qe nuk kane nje gjendje pasardhese per nje input te caktuar
            string gjendjeError = "Gjendje error";
            gjendjetAfd.Add(gjendjeError);
            for (int i = 0; i < gjendjetAfd.Count(); i++)
            {
                //llogaritja do te behet per cdo gjendje pervec gjendjes se errorit pasi pas errorit nuk mund te shkojme ne nje gjendje tjeter
                if (gjendjetAfd[i] != gjendjeError)
                {
                    foreach (var input in Afjd.Alfabeti)
                    {
                        //nese gjendja eshte pjese e basheksise se gjendjeve te automatit afjd
                        if (Afjd.BashkesiaEGjendjeve.Contains(gjendjetAfd[i]))
                        {
                            var kalimet = Afjd.Kalimet.Where(kalim => kalim.GjendjaEPare == gjendjetAfd[i] && kalim.Input == input);
                            //nese per nje input shkojme vetem ne nje gjendje, shtojme kalimin dhe shtojme gjendjen te gjendjet e afd nese ajo nuk eshte shtuar me pare
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
                            //nese per nje input shkojme me shume se ne nje gjendje krijohet gjendja e re dhe kalimi
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
                            //nese nuk ka nje kalim shkojme ne gjendje errori
                            else
                            {
                                kalimetAfd.Add(new Kalim()
                                {
                                    GjendjaEPare = gjendjetAfd[i],
                                    GjendjaEDyte = gjendjeError,
                                    Input = input
                                });
                            }
                            //nese eshte gjendje fundore ne afjd vendose si gjendje fundore dhe per afd
                            if (Afjd.GjendjetFundore.Contains(gjendjetAfd[i]))
                                Afd.GjendjetFundore.Add(gjendjetAfd[i]);
                        }
                        //nese gjendja eshte nje gjendje e re
                        else
                        {
                            try
                            {
                                char test = Convert.ToChar(Afjd.GjendjaFillestare);
                                string gjendja = gjendjetAfd[i];
                                //llogarisim per cdo gjendje qe perben gjendjen e re se ku shkon ajo per inputin e caktuar
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
                                //nqs ka nje gjendjePasardhese
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
                                //nese nuk ka, gjendja pasardhese do jete gjendja e errorit
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
                //nese gjendja eshte gjendja e errorit per cdo input do qendrojme ne gjendje errori
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
            //nqs gjendja permban nje gjendje fundore te automatit afjd, do jete gjendje fundor per afd
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
            //nese asnje nga gjendjet nuk shkon ne gjendje errori, atehere e heqim gjendjen e errorit dhe kalimet qe ka gjendja e errorit
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
        #region AfdToAfdMinimal
        //funksioni qe konverton automatin e fundem determinist ne automat te fundem determinist minimal
        public void AfdToAfdMinimal()
        {
            var gjendjet = new List<GjendjeSeti>();
            var setet = new List<Set>();
            //heqim gjendjet e pakapshme dhe marrim vetem gjendjet e kapshme
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
            //krijojme setin e gjendjeve fundore
            var setGjendjeFundore = gjendjet.Where(gjendje => gjendje.EshteGjendjeFundore == true).ToList();
            //krijojme setin e gjendjeve jofundore
            var setGjendjeJoFundore = gjendjet.Where(gjendje => gjendje.EshteGjendjeFundore == false).ToList();
            setet.Add(new Set() { gjendjetESetit = setGjendjeJoFundore });
            setet.Add(new Set() { gjendjetESetit = setGjendjeFundore });
            var setetEReja = Minimizimi(setet);
            //gjendjet e reja te automatit minimal do jene gjendjet sipas seteve pas perfundimit te minimizimit
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
            //fillimisht asnje nga gjendjet nuk i perket asnje seti te ri
            foreach (var set in setet)
            {
                foreach (var gjendje in set.gjendjetESetit)
                {
                    gjendje.IdESetitKuNdodhet = null;
                    gjendje.SetMeVete = false;
                }
            }
            int idSet = 0;
            //setet e reja qe do sherbejne si sete fillestare per perseritjen e ketij funksioni
            List<Set> setetEReja = new List<Set>();
            //algoritmi do te marri fund kur nuk do kete me ndryshime
            bool changes = false;
            //per cdo set
            foreach (var set in setet)
            {
                //per cdo set me me shume se nje gjendje
                if (set.gjendjetESetit.Count != 1)
                {
                    //per cdo gjendje te setit
                    for (int i = 0; i < set.gjendjetESetit.Count(); i++)
                    {
                        //nqs jemi ne gjendjen e pare krijojme nje set te ri qe ruan kete gjendje
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
                            //krahasojme gjendjen me paraardhesit e saj qe kane krijuar set me pare, pra vetem me nje element per cdo set te ri
                            for (int j = i - 1; j >= 0; j--)
                            {
                                var gjendjeParaardhese = set.gjendjetESetit.ElementAt(j);
                                if (gjendjeParaardhese.SetMeVete == true && set.gjendjetESetit.ElementAt(i).IdESetitKuNdodhet != null)
                                {
                                    //llogarisim nese gjendjet jane ne te njejtin set
                                    bool neTeNjejtinSet = PjeseETeNjejtitSet(set.gjendjetESetit.ElementAt(i), gjendjeParaardhese, setet);
                                    //nese po shtojme gjendjen ne setin perkates
                                    if (neTeNjejtinSet)
                                    {
                                        setetEReja.ElementAt(gjendjeParaardhese.IdESetitKuNdodhet.GetValueOrDefault()).gjendjetESetit.Add(set.gjendjetESetit.ElementAt(i));
                                        set.gjendjetESetit.ElementAt(i).IdESetitKuNdodhet = gjendjeParaardhese.IdESetitKuNdodhet;
                                    }
                                }
                            }
                            //nese id e setit te cilit i perket gjendja eshte null dmth qe gjendja do krijoje nje set te ri
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
                //nese gjendja eshte e vetme ne set, do krijohet seti qe mban vetem kete gjendje dhe nuk do behen modifikime ne te
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
            //nqs ka patur ndryshime therrit funksionin per setet e reja te krijuara
            if (changes) { return setetEReja = Minimizimi(setetEReja); }
            //nqs jo kthe setet e reja
            else return setetEReja;
        }
        //funksioni qe sheh nese dy gjendje bejne pjese ne te njejtin set
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
        public ActionResult EAFjDiRi()
        {
            KrijoTeRi();
            return View("LexoEAFjDStep1");
        }
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
