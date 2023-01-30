using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DofusChasseForms
{
    public class Phoreur : WindowsGesture
    {
        private IntPtr dofusHandle;
        private List<position> PhoreurList;
        public Phoreur(IntPtr DofusHandle) 
        {
            this.dofusHandle = DofusHandle;
            PhoreurList = new List<position>();
        }

        public async Task<string> Launch(int x, int y, string dir)
        {
            for(int i = 0; i<10; i++)
            {
                await moveToDirection(dir, x, y);
                switch (dir)
                {
                    case "up":
                        y--;
                        break;
                    case "right":
                        x++;
                        break;
                    case "down":
                        y++;
                        break;
                    case "left":
                        x--;
                        break;
                }
                if (await SearchPhoreur())
                {
                    return x.ToString() +";"+y.ToString(); //MAP PHOREUR TROUVER
                }
                if(i == 9)
                {
                    i= 0;
                    switch (dir)
                    {
                        case "up":
                            dir = "down";
                            break;
                        case "right":
                            dir = "left";
                            break;
                        case "down":
                            dir = "up";
                            break;
                        case "left":
                            dir = "right";
                            break;
                    }
                }
            }
            return null;
        }

        public async Task<bool> SearchPhoreur() 
        {
            List<string> tabDeInt = new List<string>();

            Bitmap printscreen = new Bitmap(1920, 1080);

            using (Graphics g = Graphics.FromImage(printscreen))
            {
                PrintWindow(this.dofusHandle, g.GetHdc(), 0);
            }


            Rectangle cropRect = new Rectangle(322, 31, 1291, 905);  // screen only game without border
            using (Bitmap target = new Bitmap(cropRect.Width, cropRect.Height))
            {
                using (Graphics g = Graphics.FromImage(target))
                {
                    g.DrawImage(printscreen, new Rectangle(0, 0, target.Width, target.Height),
                        cropRect,
                        GraphicsUnit.Pixel);
                }
                target.Save("Screenphoreur.png", System.Drawing.Imaging.ImageFormat.Png);


                int RBaseValue = 71;
                int GBaseValue = 105;
                int BBaseValue = 86;

                int ColorCoefCalc = 4;

                for (int Xcount = 0; Xcount < target.Width; Xcount++)
                {
                    for (int Ycount = 0; Ycount < target.Height; Ycount++)
                    {
                        var color = target.GetPixel(Xcount, Ycount);

                        if (color.R < (RBaseValue + ColorCoefCalc) && color.R > (RBaseValue - ColorCoefCalc) &&
                            color.G < (GBaseValue + ColorCoefCalc) && color.G > (GBaseValue - ColorCoefCalc) &&
                            color.B < (BBaseValue + ColorCoefCalc) && color.B > (BBaseValue - ColorCoefCalc))
                        {
                            Console.WriteLine("TROUVER");
                            //tabDeInt.Add(Xcount + " ; " + Ycount);


                            Rectangle cropRectBis = new Rectangle(Xcount-59, Ycount-66, 118, 132);
                            Bitmap targetBis = new Bitmap(cropRectBis.Width, cropRectBis.Height);

                            using (Graphics gBis = Graphics.FromImage(targetBis))
                            {
                                gBis.DrawImage(target, new Rectangle(0, 0, targetBis.Width, targetBis.Height),
                                    cropRectBis,
                                    GraphicsUnit.Pixel);
                            }
                            targetBis.Save("phoreur.png", System.Drawing.Imaging.ImageFormat.Png);

                            int RBaseValueBis = 227;
                            int GBaseValueBis = 211;
                            int BBaseValueBis = 154;

                            int ColorCoefCalcBis = 4;

                            for (int XcountBis = 0; XcountBis < targetBis.Width; XcountBis++)
                            {
                                for (int YcountBis = 0; YcountBis < targetBis.Height; YcountBis++)
                                {
                                    var colorBis = targetBis.GetPixel(XcountBis, YcountBis);

                                    if (colorBis.R < (RBaseValueBis + ColorCoefCalcBis) && colorBis.R > (RBaseValueBis - ColorCoefCalcBis) &&
                                        colorBis.G < (GBaseValueBis + ColorCoefCalcBis) && colorBis.G > (GBaseValueBis - ColorCoefCalcBis) &&
                                        colorBis.B < (BBaseValueBis + ColorCoefCalcBis) && colorBis.B > (BBaseValueBis - ColorCoefCalcBis))
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        private async Task moveToDirection(string direction, int x, int y)
        {
            switch(direction)
            {
                case "up":
                    PerformLeftClick(dofusHandle, new Point(497, 1004)); //Click chat
                    await Task.Delay(150);
                    await this.WriteString(dofusHandle, "/travel " + x + " " + (y-1));
                    break;
                case "right":
                    PerformLeftClick(dofusHandle, new Point(497, 1004)); //Click chat
                    await Task.Delay(150);
                    await this.WriteString(dofusHandle, "/travel " + (x+1) + " " + y);
                    break;
                case "down":
                    PerformLeftClick(dofusHandle, new Point(497, 1004)); //Click chat
                    await Task.Delay(150);
                    await this.WriteString(dofusHandle, "/travel " + x + " " + (y+1));
                    break;
                case "left":
                    PerformLeftClick(dofusHandle, new Point(497, 1004)); //Click chat
                    await Task.Delay(150);
                    await this.WriteString(dofusHandle, "/travel " + (x-1) + " " + y);
                    break;
            }

            await Task.Delay(50);
            PostMessage(dofusHandle, WM_KEYUP, (IntPtr)Keys.Enter, (IntPtr)0x0); //presse a to finish text
            PostMessage(dofusHandle, WM_KEYDOWN, (IntPtr)Keys.Enter, (IntPtr)0x0); //presse a to finish text
            await Task.Delay(350);
            PostMessage(dofusHandle, WM_KEYUP, (IntPtr)Keys.Enter, (IntPtr)0x0); //presse a to finish text
            PostMessage(dofusHandle, WM_KEYDOWN, (IntPtr)Keys.Enter, (IntPtr)0x0); //presse a to finish text

            await Task.Delay(6500);
        }
    }

    public class position
    { public int x; public int y; }
}
