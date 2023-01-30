using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Windows.Forms;

namespace DofusChasseForms
{
    public class Fight : WindowsGesture
    {
        public Fight() { }

        public async Task MakeFight(IntPtr DofusHandle)
        {
            await Task.Delay(1000);
            PerformLeftClick(DofusHandle, new Point(1366, 974)); // CLICK PRET
            await Task.Delay(7000); // wait spawn coffre

            //Detect coffre avant langue
            string colorcode = "#BC6CB8";
            int argb = Int32.Parse(colorcode.Replace("#", ""), NumberStyles.HexNumber);
            Color clr = Color.FromArgb(argb);

            Bitmap printscreenPos = new Bitmap(1920, 1080);
            printscreenPos.SetResolution(5000F, 5000F);
            

            Point[] FindedPixel;
            OCR oCR = new OCR();

            while (true)
            {
                PerformLeftClick(DofusHandle, new Point(778, 959));
                await Task.Delay(150);
                PerformLeftClick(DofusHandle, new Point(1366, 974)); //Passe Tour
                await Task.Delay(150);
                PerformLeftClick(DofusHandle, new Point(778, 959));

                await Task.Delay(500);

                for (int i = 0; i<5; i++)
                {
                    using (Graphics g = Graphics.FromImage(printscreenPos))
                    {
                        PrintWindow(DofusHandle, g.GetHdc(), 0);
                    }
                    printscreenPos.SetResolution(5000F, 5000F);

                    Rectangle cropRect = new Rectangle(1553, 851, 320, 76);
                    using (Bitmap target = new Bitmap(cropRect.Width, cropRect.Height))
                    {
                        using (Graphics g = Graphics.FromImage(target))
                        {
                            g.DrawImage(printscreenPos, new Rectangle(0, 0, target.Width, target.Height),
                                cropRect,
                                GraphicsUnit.Pixel);
                        }
                        target.SetResolution(160F, 38F); // /!\ pixel /2 reduction du temps de recherche a test ??
                        target.Save("FightLulz.png", System.Drawing.Imaging.ImageFormat.Png);

                        FindedPixel = IsPixelFindInBMap(clr, target);
                    }

                    if (FindedPixel.Length > 0)
                    {
                        switch (i)
                        {
                            case 0:
                            case 1:
                                PostMessage(DofusHandle, WM_KEYDOWN, (IntPtr)Keys.D1, (IntPtr)0x0);
                                await Task.Delay(50);
                                PostMessage(DofusHandle, WM_KEYUP, (IntPtr)Keys.D1, (IntPtr)0x0);
                                break;
                            case 2:
                            case 4:
                                PostMessage(DofusHandle, WM_KEYDOWN, (IntPtr)Keys.D2, (IntPtr)0x0);
                                await Task.Delay(50);
                                PostMessage(DofusHandle, WM_KEYUP, (IntPtr)Keys.D2, (IntPtr)0x0);
                                break;
                            case 3:
                                PostMessage(DofusHandle, WM_KEYDOWN, (IntPtr)Keys.D3, (IntPtr)0x0);
                                await Task.Delay(50);
                                PostMessage(DofusHandle, WM_KEYUP, (IntPtr)Keys.D3, (IntPtr)0x0);
                                break;
                        }
                        await Task.Delay(100);
                        FindedPixel[0].X = FindedPixel[0].X + 1553;
                        FindedPixel[0].Y = FindedPixel[0].Y + 851;
                        PerformLeftClick(DofusHandle, FindedPixel[0]);
                        await Task.Delay(800);

                        oCR.MajScreen(printscreenPos);

                        if (oCR.GetFinishFight())
                        {
                            await Task.Delay(1000);
                            PostMessage(DofusHandle, WM_KEYDOWN, (IntPtr)Keys.Enter, (IntPtr)0x0);
                            await Task.Delay(50);
                            PostMessage(DofusHandle, WM_KEYUP, (IntPtr)Keys.Enter, (IntPtr)0x0);
                            break;
                        }
                    }
                }
                if (oCR.GetFinishFight())
                {
                    await Task.Delay(1000);
                    PostMessage(DofusHandle, WM_KEYDOWN, (IntPtr)Keys.Enter, (IntPtr)0x0);
                    await Task.Delay(50);
                    PostMessage(DofusHandle, WM_KEYUP, (IntPtr)Keys.Enter, (IntPtr)0x0);

                    break;
                }
            }
        }

        private Point[] IsPixelFindInBMap(Color pixelColor, Bitmap bMap)
        {
            List<Point> result = new List<Point>();
            using (bMap)
            {
                for (int x = 0; x < bMap.Width; x++)
                {
                    for (int y = 0; y < bMap.Height; y++)
                    {
                        var tesfef = bMap.GetPixel(x, y);
                        if (pixelColor.R == bMap.GetPixel(x, y).R && pixelColor.G == bMap.GetPixel(x, y).G && pixelColor.B == bMap.GetPixel(x, y).B)
                            result.Add(new Point(x, y));
                    }
                }
            }
            return result.ToArray();
        }
    }
}
