using DofusChasseForms.ApiHandler.Services;
using Emgu.CV.Dnn;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Windows.Forms;
using static Emgu.Util.Platform;

namespace DofusChasseForms
{
    public class Chasse : WindowsGesture, INotifyPropertyChanged
    {
        private int indiceCount = 1;
        private int etapeCount = 1;
        private string pseudo = "Eithingg";
        private string indice;
        private string x;
        private string y;
        private bool isAutoPilot = true;

        public int IndiceCount
        {
            get { return indiceCount; }
            set
            {
                indiceCount = value;
                OnPropertyChanged("IndiceCount");
            }
        }
        public int EtapeCount
        {
            get { return etapeCount; }
            set
            {
                etapeCount = value;
                OnPropertyChanged("EtapeCount");
            }
        }
        public string Pseudo
        {
            get { return pseudo; }
            set
            {
                pseudo = value;
                OnPropertyChanged("Pseudo");
            }
        }
        public string Indice
        {
            get { return indice; }
            set
            {
                indice = value;
                OnPropertyChanged("Indice");
            }
        }
        public string X
        {
            get { return x; }
            set
            {
                x = value;
                OnPropertyChanged("X");
            }
        }
        public string Y
        {
            get { return y; }
            set
            {
                y = value;
                OnPropertyChanged("Y");
            }
        }
        public bool IsAutoPilot
        {
            get { return isAutoPilot; }
            set
            {
                isAutoPilot = value;
                OnPropertyChanged("IsAutoPilot");
            }
        }
        protected virtual void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IDofusMap _dofusMapService;

        public bool IsPaused = false;
        private bool IsLastIndice = false;
        private bool IsLastEtape = false;

        private string[] _actualPos = new string[] { "0", "0"};

        Process WebWindow;
        Process DofusWindow;

        public Chasse()
        {
            _dofusMapService = RestService.For<IDofusMap>("https://dofus-map.com");
        }

        public async Task Launch()
        {
            Process[] processlist = Process.GetProcesses();
            this.WebWindow = processlist.Where(p => p.MainWindowTitle.Contains("Chasse au t")).FirstOrDefault();
            this.DofusWindow = processlist.Where(p => p.MainWindowTitle.Contains(Pseudo)).FirstOrDefault();
            Bitmap printscreenPos = new Bitmap(1920, 1080);
            printscreenPos.SetResolution(5000F, 5000F);
            using (Graphics g = Graphics.FromImage(printscreenPos))
            {
                PrintWindow(DofusWindow.MainWindowHandle, g.GetHdc(), 0);
            }
            printscreenPos.SetResolution(5000F, 5000F);

            IsLastIndice = false;

            OCR Ocr = new OCR();
            ImageRecognition imgReco = new ImageRecognition();
            Ocr.MajScreen(printscreenPos);
            imgReco.MajScreen(printscreenPos);
            //Go to Chasse

            //Get Zaap from pos
            //PathFinding travel to pos
            _actualPos[0] = this.X;
            _actualPos[1] = this.Y;

            while (!IsPaused && !IsLastEtape) // BOUCLE ETAPE
            {
                using (Graphics g = Graphics.FromImage(printscreenPos))
                {
                    PrintWindow(DofusWindow.MainWindowHandle, g.GetHdc(), 0);
                }
                printscreenPos.SetResolution(5000F, 5000F);

                Ocr.MajScreen(printscreenPos);
                imgReco.MajScreen(printscreenPos);

                if (Ocr.GetFight())
                {
                    Console.WriteLine("[LOG] : FIGHT DISPO");

                    PerformLeftClick(DofusWindow.MainWindowHandle, new Point(1749, 154)); //lancement Fight
                    IsLastEtape = true;

                    Fight mFight = new Fight();
                    await mFight.MakeFight(DofusWindow.MainWindowHandle);
                    Console.WriteLine("[LOG] : FIGHT END");
                    IndiceCount = 1;

                    //RETOUR CHASSE START
                    await Task.Delay(1000);

                    PostMessage(DofusWindow.MainWindowHandle, WM_KEYDOWN, (IntPtr)Keys.H, (IntPtr)0x0); //go havre sac
                    await Task.Delay(50);
                    PostMessage(DofusWindow.MainWindowHandle, WM_KEYUP, (IntPtr)Keys.H, (IntPtr)0x0);
                    await Task.Delay(1500);
                    PerformLeftClick(DofusWindow.MainWindowHandle, new Point(787, 339)); //click zaap
                    await Task.Delay(1000);
                    await this.WriteString(DofusWindow.MainWindowHandle, "champs de cani");
                    await Task.Delay(200);
                    PostMessage(DofusWindow.MainWindowHandle, WM_KEYUP, (IntPtr)Keys.A, (IntPtr)0x0); //presse a to finish text
                    await Task.Delay(1000);
                    PostMessage(DofusWindow.MainWindowHandle, WM_KEYUP, (IntPtr)Keys.Enter, (IntPtr)0x0); //presse a to finish text
                    PostMessage(DofusWindow.MainWindowHandle, WM_KEYDOWN, (IntPtr)Keys.Enter, (IntPtr)0x0); //presse a to finish text
                    await Task.Delay(1000);
                    PerformLeftClick(DofusWindow.MainWindowHandle, new Point(497, 1004)); //Click chat
                    await Task.Delay(500);
                    await this.WriteString(DofusWindow.MainWindowHandle, "/travel -25 -36");
                    await Task.Delay(500);
                    PostMessage(DofusWindow.MainWindowHandle, WM_KEYUP, (IntPtr)Keys.Enter, (IntPtr)0x0);
                    PostMessage(DofusWindow.MainWindowHandle, WM_KEYDOWN, (IntPtr)Keys.Enter, (IntPtr)0x0);
                    await Task.Delay(500);
                    PostMessage(DofusWindow.MainWindowHandle, WM_KEYUP, (IntPtr)Keys.Enter, (IntPtr)0x0);
                    PostMessage(DofusWindow.MainWindowHandle, WM_KEYDOWN, (IntPtr)Keys.Enter, (IntPtr)0x0);
                    await Task.Delay(8750);
                    PerformLeftClick(DofusWindow.MainWindowHandle, new Point(968, 449)); //click door chasse
                    await Task.Delay(5000);
                    PerformLeftClick(DofusWindow.MainWindowHandle, new Point(1400, 459)); //click 2e salle
                    await Task.Delay(5000);
                    PerformLeftClick(DofusWindow.MainWindowHandle, new Point(1054, 494)); //click coffre 1
                    await Task.Delay(5000);
                    PerformLeftClick(DofusWindow.MainWindowHandle, new Point(1185, 538)); //click coffre 1
                    await Task.Delay(5000);
                    PerformLeftClick(DofusWindow.MainWindowHandle, new Point(497, 1004)); //Click chat
                    await Task.Delay(150);
                    await this.WriteString(DofusWindow.MainWindowHandle, "/travel -25 -36");
                    await Task.Delay(50);
                    PostMessage(DofusWindow.MainWindowHandle, WM_KEYUP, (IntPtr)Keys.Enter, (IntPtr)0x0); //presse a to finish text
                    PostMessage(DofusWindow.MainWindowHandle, WM_KEYDOWN, (IntPtr)Keys.Enter, (IntPtr)0x0); //presse a to finish text
                    await Task.Delay(500);
                    PostMessage(DofusWindow.MainWindowHandle, WM_KEYUP, (IntPtr)Keys.Enter, (IntPtr)0x0); //presse a to finish text
                    PostMessage(DofusWindow.MainWindowHandle, WM_KEYDOWN, (IntPtr)Keys.Enter, (IntPtr)0x0); //presse a to finish text
                    EtapeCount = 1;
                    break;
                }
                IsLastIndice = false;

                Phoreur myPhoreur = new Phoreur(DofusWindow.MainWindowHandle);

                //START BOUCLE INDICES ICI
                while (!IsPaused && !IsLastIndice)  //Boucle Indice
                {
                    using (Graphics g = Graphics.FromImage(printscreenPos))
                    {
                        PrintWindow(DofusWindow.MainWindowHandle, g.GetHdc(), 0);
                    }
                    printscreenPos.SetResolution(5000F, 5000F);

                    Ocr.MajScreen(printscreenPos);
                    imgReco.MajScreen(printscreenPos);

                    if (!imgReco.FindIfBlackFlagPresent())
                    {
                        Console.WriteLine("[LOG] : 1/2 DERNIER INDICE");

                        //Search Loupe & click on
                        var coord = imgReco.FindLoupeCoord();
                        PerformLeftClick(DofusWindow.MainWindowHandle, new Point(coord[0], coord[1])); //click loupe
                        PerformLeftClick(DofusWindow.MainWindowHandle, new Point(781, 964)); // RESET MOUSE
                        IsLastIndice = true;
                        await Task.Delay(300);
                        PerformLeftClick(DofusWindow.MainWindowHandle, new Point(781, 964)); // RESET MOUSE
                        await Task.Delay(500);
                        EtapeCount++;
                        Console.WriteLine("[LOG] : 2/2 DERNIER INDICE");
                        IndiceCount = 1;
                        break;
                    }

                    //Get Arrow Direction
                    string arrowDirection = imgReco.FindArrowDirection();

                    //Get Indice
                    //string indice = Ocr.GetIndice(IndiceIndex);
                    this.Indice = Ocr.GetIndice(IndiceCount-1);

                    //GetPos DDB
                    if(IndiceCount < 2)
                        this.UpdatePos(Ocr);

                    if(this.Indice.Contains("Phorreur"))
                    {
                        string newpos = await myPhoreur.Launch(int.Parse(this.X), int.Parse(this.Y), arrowDirection);

                        var newPosTab = newpos.Split(';');
                        this.X = newPosTab[0];
                        this.Y = newPosTab[1];

                        PerformLeftClick(WebWindow.MainWindowHandle, new Point(94, 57));    // Click X
                        PostMessage(WebWindow.MainWindowHandle, WM_KEYDOWN, (IntPtr)Keys.Back, (IntPtr)0x0);
                        PostMessage(WebWindow.MainWindowHandle, WM_KEYDOWN, (IntPtr)Keys.Back, (IntPtr)0x0);
                        PostMessage(WebWindow.MainWindowHandle, WM_KEYDOWN, (IntPtr)Keys.Back, (IntPtr)0x0);
                        await WriteString(WebWindow.MainWindowHandle, this.X);
                        await Task.Delay(100);
                        PerformLeftClick(WebWindow.MainWindowHandle, new Point(242, 57));   // Click Y
                        PostMessage(WebWindow.MainWindowHandle, WM_KEYDOWN, (IntPtr)Keys.Back, (IntPtr)0x0);
                        PostMessage(WebWindow.MainWindowHandle, WM_KEYDOWN, (IntPtr)Keys.Back, (IntPtr)0x0);
                        PostMessage(WebWindow.MainWindowHandle, WM_KEYDOWN, (IntPtr)Keys.Back, (IntPtr)0x0);
                        await WriteString(WebWindow.MainWindowHandle, this.Y);
                        await Task.Delay(100);
                    }
                    else
                    {
                        string coordTravel = await SearchPosDB(WebWindow.MainWindowHandle, this.X, this.Y, this.Indice, arrowDirection, IndiceCount == 1);

                        if (IsAutoPilot)
                        {
                            await Task.Delay(300);

                            PerformLeftClick(DofusWindow.MainWindowHandle, new Point(497, 1004)); //Click chat
                            await WriteString(DofusWindow.MainWindowHandle, Clipboard.GetText());
                            await Task.Delay(500);
                            PostMessage(DofusWindow.MainWindowHandle, WM_KEYDOWN, (IntPtr)Keys.Enter, (IntPtr)0x0);
                            await Task.Delay(50);
                            PostMessage(DofusWindow.MainWindowHandle, WM_KEYUP, (IntPtr)Keys.Enter, (IntPtr)0x0);
                            await Task.Delay(500);
                            PostMessage(DofusWindow.MainWindowHandle, WM_KEYDOWN, (IntPtr)Keys.Enter, (IntPtr)0x0);
                            await Task.Delay(50);
                            PostMessage(DofusWindow.MainWindowHandle, WM_KEYUP, (IntPtr)Keys.Enter, (IntPtr)0x0); //Click Accept travel
                            await Task.Delay(200);


                            var objectivPos = Clipboard.GetText().Replace("/travel ", "").Split(' '); //return something like 'travel -31 -39'
                            var xa = int.Parse(this.X) - int.Parse(objectivPos[0]);
                            var ya = int.Parse(this.Y) - int.Parse(objectivPos[1]);
                            if (xa < 0) { xa = xa * -1; }
                            if (ya < 0) { ya = ya * -1; }

                            int mapNumbers = xa + ya;
                            for (int i = 0; i < mapNumbers; i++)
                            {
                                await WaitChangeMap(DofusWindow.MainWindowHandle);
                            }

                            this.X = objectivPos[0];
                            this.Y = objectivPos[1];
                        }
                        else
                        {
                            //TODO REPAIR ENTRE 2 Etape
                            var objectivPos = Clipboard.GetText().Replace("/travel ", "").Split(' '); //return something like 'travel -31 -39'

                            var nx = int.Parse(this.X);
                            var ny = int.Parse(this.Y);


                            while (_actualPos[0] != objectivPos[0] || _actualPos[1] != objectivPos[1] && !IsPaused)
                            {
                                switch (arrowDirection)
                                {
                                    case "up":
                                        PerformLeftClick(DofusWindow.MainWindowHandle, new Point(915, 10));
                                        ny--;
                                        _actualPos[1] = ny.ToString();
                                        break;
                                    case "right":
                                        PerformLeftClick(DofusWindow.MainWindowHandle, new Point(1593, 498));
                                        nx++;
                                        _actualPos[0] = nx.ToString();
                                        break;
                                    case "left":
                                        PerformLeftClick(DofusWindow.MainWindowHandle, new Point(324, 473));
                                        nx--;
                                        _actualPos[0] = nx.ToString();
                                        break;
                                    case "down":
                                        PerformLeftClick(DofusWindow.MainWindowHandle, new Point(945, 894));
                                        ny++;
                                        _actualPos[1] = ny.ToString();
                                        break;
                                }
                                await WaitChangeMap(DofusWindow.MainWindowHandle); // WAIT TO NEXT MAP VALIDATION
                                this.X = nx.ToString();
                                this.Y = ny.ToString();
                            }
                        } //NON AUTOPILOT NON FONCTIONNEL
                    }

                    switch (IndiceCount-1)
                    {
                        case 0:
                            PerformLeftClick(DofusWindow.MainWindowHandle, new Point(1885, 156));
                            break;
                        case 1:
                            PerformLeftClick(DofusWindow.MainWindowHandle, new Point(1886, 184));
                            break;
                        case 2:
                            PerformLeftClick(DofusWindow.MainWindowHandle, new Point(1885, 213));
                            break;
                        case 3:
                            PerformLeftClick(DofusWindow.MainWindowHandle, new Point(1885, 241));
                            break;
                        case 4:
                            PerformLeftClick(DofusWindow.MainWindowHandle, new Point(1885, 270));
                            break;
                        case 5:
                            PerformLeftClick(DofusWindow.MainWindowHandle, new Point(1885, 298));
                            break;
                    }
                    await Task.Delay(300);
                    PerformLeftClick(DofusWindow.MainWindowHandle, new Point(781, 964)); // RESET MOUSE
                    await Task.Delay(300);
                    PerformLeftClick(DofusWindow.MainWindowHandle, new Point(1829, 50));
                    await Task.Delay(500);
                    PerformLeftClick(DofusWindow.MainWindowHandle, new Point(781, 964)); // RESET MOUSE

                    IndiceCount++;
                    Console.WriteLine("[LOG] : "+ IndiceCount.ToString());
                }
            }
            //Check if Fight rdy

            //Make Fight

            //Return to Chasse gg

        }

        private async Task WaitChangeMap(IntPtr DofusHandle)
        {
            Bitmap printscreen = new Bitmap(1920, 1080);

            using (Graphics g = Graphics.FromImage(printscreen))
            {
                PrintWindow(DofusWindow.MainWindowHandle, g.GetHdc(), 0);
            }
            //printscreen.Save("WaitMap1.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            List<Color> colors = new List<Color>
            {
                printscreen.GetPixel(401, 65),
                printscreen.GetPixel(1514, 106),
                printscreen.GetPixel(1552, 407),
                printscreen.GetPixel(1563, 694),
                printscreen.GetPixel(1152, 905),
                printscreen.GetPixel(491, 860),
                printscreen.GetPixel(425, 717),
                printscreen.GetPixel(395, 351),
                printscreen.GetPixel(432, 258),
                printscreen.GetPixel(611, 79),
                printscreen.GetPixel(1543, 284),
                printscreen.GetPixel(952, 786)
            };

            int indexWait = 0;
            while (true)
            {
                Bitmap printscreen2 = new Bitmap(1920, 1080);
                using (Graphics g = Graphics.FromImage(printscreen2))
                {
                    PrintWindow(DofusHandle, g.GetHdc(), 0);
                }
                //printscreen2.Save("WaitMap2.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                int index = 0;
                if(colors[0] != printscreen2.GetPixel(401, 65) && printscreen2.GetPixel(401, 65).R != 0 && printscreen2.GetPixel(401, 65).G != 0 && printscreen2.GetPixel(401, 65).B != 0 ) { index++; }
                if(colors[1] != printscreen2.GetPixel(1514, 106) && printscreen2.GetPixel(1514, 106).R != 0 && printscreen2.GetPixel(1514, 106).G != 0 && printscreen2.GetPixel(1514, 106).B != 0) { index++; }
                if(colors[2] != printscreen2.GetPixel(1552, 407) && printscreen2.GetPixel(1552, 407).R != 0 && printscreen2.GetPixel(1552, 407).G != 0 && printscreen2.GetPixel(1552, 407).B != 0) { index++; }
                if(colors[3] != printscreen2.GetPixel(1563, 694) && printscreen2.GetPixel(1563, 694).R != 0 && printscreen2.GetPixel(1563, 694).G != 0 && printscreen2.GetPixel(1563, 694).B != 0) { index++; }
                if(colors[4] != printscreen2.GetPixel(1152, 905) && printscreen2.GetPixel(1152, 905).R != 0 && printscreen2.GetPixel(1152, 905).G != 0 && printscreen2.GetPixel(1152, 905).B != 0) { index++; }
                if(colors[5] != printscreen2.GetPixel(491, 860) && printscreen2.GetPixel(491, 860).R != 0 && printscreen2.GetPixel(491, 860).G != 0 && printscreen2.GetPixel(491, 860).B != 0) { index++; }
                if(colors[6] != printscreen2.GetPixel(425, 717) && printscreen2.GetPixel(425, 717).R != 0 && printscreen2.GetPixel(425, 717).G != 0 && printscreen2.GetPixel(425, 717).B != 0) { index++; }
                if(colors[7] != printscreen2.GetPixel(395, 351) && printscreen2.GetPixel(395, 351).R != 0 && printscreen2.GetPixel(395, 351).G != 0 && printscreen2.GetPixel(395, 351).B != 0) { index++; }
                if(colors[8] != printscreen2.GetPixel(432, 258) && printscreen2.GetPixel(432, 258).R != 0 && printscreen2.GetPixel(432, 258).G != 0 && printscreen2.GetPixel(432, 258).B != 0) { index++; }
                if(colors[9] != printscreen2.GetPixel(611, 79) && printscreen2.GetPixel(611, 79).R != 0 && printscreen2.GetPixel(611, 79).G != 0 && printscreen2.GetPixel(611, 79).B != 0) { index++; }
                if(colors[10] != printscreen2.GetPixel(1543, 284) && printscreen2.GetPixel(1543, 284).R != 0 && printscreen2.GetPixel(1543, 284).G != 0 && printscreen2.GetPixel(1543, 284).B != 0) { index++; }
                if(colors[11] != printscreen2.GetPixel(952, 786) && printscreen2.GetPixel(952, 786).R != 0 && printscreen2.GetPixel(952, 786).G != 0 && printscreen2.GetPixel(952, 786).B != 0) { index++; }

                if (index >= 8)
                {
                    Console.WriteLine("BREAKKKKKKKKKKKKKKK   : " + index);
                    index = 0;
                    using (Graphics g = Graphics.FromImage(printscreen))
                    {
                        PrintWindow(DofusWindow.MainWindowHandle, g.GetHdc(), 0);
                    }
                    //printscreen.Save("WaitMap1.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    await Task.Delay(250);
                    break;
                }
                await Task.Delay(250);
                indexWait++;
                if (indexWait > 40) // on attend 10 au cas ou et on sort (cas d'un changement de map non detecté)
                    return;

            }
            return;
        }

        [STAThread]
        private async Task<string> SearchPosDB(IntPtr windowHandle, string x, string y, string indice, string arrowDir, bool isFirst)
        {
            await Task.Delay(100);
            if(isFirst)
            {
                PerformLeftClick(windowHandle, new Point(94, 57));    // Click X
                PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.Back, (IntPtr)0x0);
                PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.Back, (IntPtr)0x0);
                PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.Back, (IntPtr)0x0);
                await WriteString(windowHandle, x);
                await Task.Delay(100);
                PerformLeftClick(windowHandle, new Point(242, 57));   // Click Y
                PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.Back, (IntPtr)0x0);
                PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.Back, (IntPtr)0x0);
                PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.Back, (IntPtr)0x0);
                await WriteString(windowHandle, y);
                await Task.Delay(100);
            }

            switch (arrowDir)                                       // Click arrow
            {
                case "up":
                    PerformLeftClick(windowHandle, new Point(149, 132));
                    break;
                case "right":
                    PerformLeftClick(windowHandle, new Point(185, 174));
                    break;
                case "left":
                    PerformLeftClick(windowHandle, new Point(112, 173));
                    break;
                case "down":
                    PerformLeftClick(windowHandle, new Point(150, 209));
                    break;
            }

            await Task.Delay(750);

            PerformLeftClick(windowHandle, new Point(53, 267));
            await Task.Delay(750);
            await WriteString(windowHandle, indice);
            await Task.Delay(750);
            PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.Down, (IntPtr)0x0);
            await Task.Delay(100);
            PostMessage(windowHandle, WM_KEYUP, (IntPtr)Keys.Down, (IntPtr)0x0);
            await Task.Delay(750);
            PostMessage(windowHandle, WM_KEYDOWN, (IntPtr)Keys.Enter, (IntPtr)0x0);
            await Task.Delay(100);
            PostMessage(windowHandle, WM_KEYUP, (IntPtr)Keys.Enter, (IntPtr)0x0);
            await Task.Delay(300);

            return Clipboard.GetText();
        }

        private void UpdatePos(OCR Ocr)
        {
            var positions = Ocr.GetPos();
            this.X = "";
            this.Y = "";


            positions = positions.Replace(@"\n", "");
            if(positions.Contains("]"))
            {
                positions = positions.Remove(positions.Length - 1);
            }
            if (!positions.Contains(",")) //CAS PARTICULIER OU POS = x,-Y  (virugile suivit de moins)
            {
                string[] substringWithOutVirugile = positions.Split('-');

                var nVirugule = positions.Count(p => p == '-');
                if (nVirugule == 1)
                {
                    this.X = substringWithOutVirugile[0];
                    this.Y = "-" + substringWithOutVirugile[1];
                }
                if (nVirugule == 2)
                {
                    this.X = "-" + substringWithOutVirugile[0];
                    this.Y = "-" + substringWithOutVirugile[1];
                }
                this.X = this.X.Replace("[", "");
                this.Y = this.Y.Replace("]", "");
                this.X = this.X.Replace("(", "");
                this.Y = this.Y.Replace(")", "");
            }
            else
            {
                string[] substringPos = positions.Split(',');
                this.X = substringPos[0].Replace("[", "");
                this.Y = substringPos[1].Replace("]", "");
                this.X = this.X.Replace("(", "");
                this.Y = this.Y.Replace(")", "");
            }
        }
    }
}
